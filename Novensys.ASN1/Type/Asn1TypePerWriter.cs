// Decompiled with JetBrains decompiler
// Type: Novensys.ASN1.Type.Asn1TypePerWriter
// Assembly: Novensys.eCard.SDK, Version=1.1.56.0, Culture=neutral, PublicKeyToken=null
// MVID: 59F9E456-D3DA-4066-A4A4-692B516A775E
// Assembly location: C:\Program Files (x86)\CNAS\SIUI-SPITAL\Novensys.eCard.SDK.dll

using Novensys.ASN1;
using Novensys.ASN1.IO;
using Novensys.ASN1.Type.Per.Buffer;
using Novensys.ASN1.Type.Per.Time;
using Novensys.ASN1.Util;
using System;
using System.Collections;
using System.IO;

namespace Novensys.ASN1.Type
{
  [Serializable]
  public class Asn1TypePerWriter : Asn1TypeWriter
  {
    public const string KEY_PADDING_WITH_ZERO_BIT = "paddingWithZeroBit";
    protected bool _aligned;
    protected bool _isExtendedEncodingEnabled;
    protected bool _isPaddingWithZeroBit;
    private int _nbPostPaddingBits;
    private Asn1BitOutputStream _out;
    private Stack _outputStreamStack;

    public bool IsAligned
    {
      get
      {
        return this._aligned;
      }
    }

    public Asn1TypePerWriter()
      : this(true)
    {
    }

    public Asn1TypePerWriter(bool aligned)
    {
      this._aligned = false;
      this._nbPostPaddingBits = 0;
      this._outputStreamStack = new Stack();
      this._aligned = aligned;
      this._isPaddingWithZeroBit = true;
      this._isEncodingDefaultValues = false;
      this._isExtendedEncodingEnabled = false;
    }

    protected internal virtual void __encodeBitStringType(Asn1BitStringType typeInstance)
    {
      if (typeInstance.__isContainingType() && typeInstance.GetTypeValue() != null)
      {
        IEncoder encoder = typeInstance.__getEncoder() ?? (IEncoder) this.getWriter();
        try
        {
          encoder.Encode(typeInstance.GetTypeValue());
        }
        catch (Asn1ValidationException ex)
        {
          throw ex;
        }
        catch (Asn1Exception ex)
        {
          throw new Asn1Exception(51, "type <" + typeInstance.GetTypeValue().GetType().FullName + "> for BIT SRING type <" + typeInstance.GetType().FullName + "> [internal exception : " + ex.Message + "]");
        }
        typeInstance.SetValue(encoder.Data, encoder.Data.Length << 3);
      }
      BitFieldWrapper bitFieldWrapper = BufferFactory.getBitFieldWrapper();
      BitString bitString = typeInstance.__getBitString() ?? new BitString();
      int length = bitString.length();
      if (typeInstance.GetLowerSize() == 0L && typeInstance.GetUpperSize() == -1L && typeInstance.__isWithNamedBitList())
        length = bitString.trimmedLength();
      else if (typeInstance.GetLowerSize() >= 0L && typeInstance.__isWithNamedBitList())
      {
        length = Math.Max(bitString.trimmedLength(), (int) typeInstance.GetLowerSize());
        if (length > bitString.length())
          bitString.setFalse(length - 1);
      }
      bitFieldWrapper.setByteArray(bitString.getInternalBytes());
      bitFieldWrapper.setLength(1, length);
      if (typeInstance.__isPerConstraintExtensible())
      {
        this._out.writeOneBitNoAlign(typeInstance.__isValueOutOfRoot());
        if (typeInstance.__isValueOutOfRoot())
        {
          this.writeLengthAndValue(true, 0L, -1L, (AbstractBuffer) bitFieldWrapper);
          BufferFactory.putBuffer((AbstractBuffer) bitFieldWrapper);
          return;
        }
      }
      if (typeInstance.GetUpperSize() == 0L)
        this._out.printEmpty();
      else if (typeInstance.GetUpperSize() != -1L && typeInstance.GetLowerSize() == typeInstance.GetUpperSize() && typeInstance.GetUpperSize() <= 16L)
        this.write(false, (AbstractBitField) bitFieldWrapper);
      else if (typeInstance.GetUpperSize() != -1L && typeInstance.GetLowerSize() == typeInstance.GetUpperSize() && typeInstance.GetUpperSize() < 65536L)
        this.write(true, (AbstractBitField) bitFieldWrapper);
      else
        this.writeLengthAndValue(true, typeInstance.GetLowerSize(), typeInstance.GetUpperSize(), (AbstractBuffer) bitFieldWrapper);
      BufferFactory.putBuffer((AbstractBuffer) bitFieldWrapper);
    }

    protected internal virtual void __encodeBooleanType(Asn1BooleanType typeInstance)
    {
      this._out.writeOneBitNoAlign(typeInstance.GetBoolValue());
    }

    protected internal virtual void __encodeChoiceType(Asn1ChoiceType typeInstance)
    {
      if (this._isExtendedEncodingEnabled && typeInstance.__isPerUseTag())
      {
        Asn1Type typeValue = typeInstance.GetTypeValue();
        if (typeValue == null)
          return;
        int asn1TypeIndex = typeInstance.GetAsn1TypeIndex();
        int[] tagTagNumbersSet = typeInstance.__getPerEmbeddedTagTagNumbersSet();
        if (tagTagNumbersSet != null && asn1TypeIndex < tagTagNumbersSet.Length)
        {
          this.encodeSizedNumber((long) tagTagNumbersSet[asn1TypeIndex], typeInstance.__getPerEmbeddedTagTagSize());
          typeValue.__write(this);
        }
      }
      else
      {
        int listRelativeIndex = typeInstance.__getListRelativeIndex();
        bool aBit = false;
        if (typeInstance.IsExtensible())
        {
          aBit = typeInstance.__isValueOutOfRoot();
          this._out.writeOneBitNoAlign(aBit);
        }
        if (!aBit)
        {
          if (typeInstance.__getNbRootComponents() > 1)
            this.encodeConstrainedNumber((long) listRelativeIndex, 0L, (long) (typeInstance.__getNbRootComponents() - 1));
          typeInstance.__getRootComponentTypeInstance(listRelativeIndex).__write(this);
        }
        else
        {
          this.encodeSmallPositiveNumber((long) typeInstance.__getListRelativeIndex());
          this.encodeOpenHelper_Begin();
          typeInstance.__getExtensionComponentTypeInstance(listRelativeIndex).__write(this);
          this.encodeOpenHelper_End();
        }
      }
    }

    protected internal virtual void __encodeConstructedOfType(Asn1ConstructedOfType typeInstance)
    {
      TypeVectorBuffer typeVectorBuffer = BufferFactory.getTypeVectorBuffer();
      typeVectorBuffer.setAsn1TypeList(typeInstance);
      if (typeInstance.__isPerConstraintExtensible())
      {
        bool aBit = typeInstance.__isValueOutOfRoot();
        this._out.writeOneBitNoAlign(aBit);
        if (aBit)
        {
          this.writeLengthAndValue(false, 0L, -1L, (AbstractBuffer) typeVectorBuffer);
          BufferFactory.putBuffer((AbstractBuffer) typeVectorBuffer);
          return;
        }
      }
      if (typeInstance.GetUpperSize() != -1L && typeInstance.GetLowerSize() == typeInstance.GetUpperSize() && typeInstance.GetUpperSize() < 65536L)
        typeVectorBuffer.writeElements(0, typeVectorBuffer.getNbElements(), this);
      else
        this.writeLengthAndValue(false, typeInstance.GetLowerSize(), typeInstance.GetUpperSize(), (AbstractBuffer) typeVectorBuffer);
      BufferFactory.putBuffer((AbstractBuffer) typeVectorBuffer);
    }

    protected internal virtual void __encodeConstructedOfTypeElement(Asn1ConstructedOfType typeInstance, int index)
    {
      ((Asn1Type) typeInstance.GetAsn1TypeList()[index]).__write(this);
    }

    protected internal virtual void __encodeConstructedType(Asn1ConstructedType typeInstance)
    {
      typeInstance.__fillDescriptors(this._isEncodingDefaultValues);
      bool aBit = false;
      if (typeInstance.IsExtensible())
      {
        aBit = typeInstance.__isValueOutOfRoot();
        this._out.writeOneBitNoAlign(aBit);
      }
      this.encodeConstructedHelper(typeInstance, typeInstance.__getRootOptDefDescriptor(), 0, typeInstance.__getNbRootComponents());
      if (!aBit)
        return;
      BitFieldWrapper bitFieldWrapper = BufferFactory.getBitFieldWrapper();
      BitString extensionDescriptor = typeInstance.__getExtensionDescriptor();
      bitFieldWrapper.setByteArray(extensionDescriptor.getInternalBytes());
      bitFieldWrapper.setLength(1, extensionDescriptor.length());
      this.writeSmallLengthAndValue((AbstractBuffer) bitFieldWrapper);
      BufferFactory.putBuffer((AbstractBuffer) bitFieldWrapper);
      int bitIndex = 0;
      int index = 0;
      for (; bitIndex < extensionDescriptor.length(); ++bitIndex)
      {
        int extensionAdditionGroup = typeInstance.__getComponentExtensionAdditionGroup(index);
        if (extensionDescriptor.get(bitIndex))
        {
          this.encodeOpenHelper_Begin();
          if (extensionAdditionGroup == -1)
            typeInstance.__getExtensionComponentTypeInstance(index).__write(this);
          else
            this.encodeConstructedHelper(typeInstance, typeInstance.__getExtensionAdditionGroupOptDefDescriptor(extensionAdditionGroup), index + typeInstance.__getNbRootComponents(), typeInstance.__getExtensionAdditionGroupNbComponents(extensionAdditionGroup));
          this.encodeOpenHelper_End();
        }
        index += extensionAdditionGroup == -1 ? 1 : typeInstance.__getExtensionAdditionGroupNbComponents(extensionAdditionGroup);
      }
    }

    protected internal void __encodeEmbeddedPdvType(Asn1ConstructedType typeInstance)
    {
      for (int index = 0; index < typeInstance.__getNbRootComponents(); ++index)
      {
        if (index != 1)
          typeInstance.GetAsn1Type(index).__write(this);
      }
    }

    protected internal virtual void __encodeEnumeratedType(Asn1EnumeratedType typeInstance)
    {
      int index = typeInstance.__getIndex();
      if (index == -1)
        throw new Asn1Exception(25, string.Concat(new object[4]
        {
          (object) typeInstance.LongValue,
          (object) " is not authorized in type <",
          (object) typeInstance.GetType().FullName,
          (object) ">"
        }));
      else if (!typeInstance.IsExtensible())
      {
        this.encodeConstrainedNumber((long) index, 0L, (long) (typeInstance.__getRootValueSetSize() - 1));
      }
      else
      {
        this._out.writeOneBitNoAlign(typeInstance.__isValueOutOfRoot());
        if (typeInstance.__isValueOutOfRoot())
          this.encodeSmallPositiveNumber((long) index);
        else
          this.encodeConstrainedNumber((long) index, 0L, (long) (typeInstance.__getRootValueSetSize() - 1));
      }
    }

    protected internal virtual void __encodeIntegerType(Asn1IntegerType typeInstance)
    {
      if (typeInstance.__isPerConstraintExtensible())
      {
        this._out.writeOneBitNoAlign(typeInstance.__isValueOutOfRoot());
        if (typeInstance.__isValueOutOfRoot())
        {
          this.encodeUnconstrainedNumber(typeInstance);
          return;
        }
      }
      if (typeInstance.IsUpperBoundDefined() && typeInstance.GetLowerBound() == typeInstance.GetUpperBound())
        this._out.printEmpty();
      else if (typeInstance.IsLowerBoundDefined() && typeInstance.IsUpperBoundDefined())
      {
        if (typeInstance.GetBigIntegerValue() != null)
          throw new Asn1Exception(50, "for type <" + typeInstance.GetType().FullName + ">");
        this.encodeConstrainedNumber(typeInstance.GetLongValue(), typeInstance.GetLowerBound(), typeInstance.GetUpperBound());
      }
      else if (typeInstance.IsLowerBoundDefined())
      {
        if (typeInstance.GetBigIntegerValue() != null)
          throw new Asn1Exception(50, "for type <" + typeInstance.GetType().FullName + ">");
        this.encodeSemiConstrainedNumber(typeInstance.GetLongValue(), typeInstance.GetLowerBound());
      }
      else
        this.encodeUnconstrainedNumber(typeInstance);
    }

    protected internal virtual void __encodeKnownMultiplierStringType(Asn1KnownMultiplierStringType typeInstance)
    {
      long lowerBound = typeInstance.GetLowerBound();
      long num = typeInstance.GetUpperBound();
      long lengthLowerBound = typeInstance.GetLowerSize();
      long lengthUpperBound = typeInstance.GetUpperSize();
      string alphabet = typeInstance.__getAlphabet();
      bool flag1 = false;
      if (typeInstance.__isPerConstraintExtensible())
      {
        bool flag2 = typeInstance.__isValueOutOfRoot();
        this._out.writeOneBitNoAlign(typeInstance.__isValueOutOfRoot());
        if (flag2)
        {
          flag1 = true;
          lowerBound = typeInstance.__getBaseAlphabetLowerBound();
          num = typeInstance.__getBaseAlphabetUpperBound();
          lengthLowerBound = 0L;
          lengthUpperBound = -1L;
          alphabet = typeInstance.__getBaseAlphabet();
        }
      }
      int nbBitsPerChar = !this._aligned ? (flag1 ? typeInstance.__getBaseAlphabetUnalignedNbBitPerChar() : typeInstance.__getUnalignedNbBitPerChar()) : (flag1 ? typeInstance.__getBaseAlphabetAlignedNbBitPerChar() : typeInstance.__getAlignedNbBitPerChar());
      KnownMultiplierStringBuffer multiplierStringBuffer;
      if (num <= (long) ((1 << nbBitsPerChar) - 1))
      {
        BoundStringBuffer boundStringBuffer = BufferFactory.getBoundStringBuffer();
        boundStringBuffer.setParameters(typeInstance, nbBitsPerChar, 0L);
        multiplierStringBuffer = (KnownMultiplierStringBuffer) boundStringBuffer;
      }
      else if ((!flag1 || !typeInstance.__isAlphabetConstraintResetable()) && alphabet != null)
      {
        AlphabetStringBuffer alphabetStringBuffer = BufferFactory.getAlphabetStringBuffer();
        alphabetStringBuffer.setParameters(typeInstance, nbBitsPerChar, alphabet);
        multiplierStringBuffer = (KnownMultiplierStringBuffer) alphabetStringBuffer;
      }
      else
      {
        BoundStringBuffer boundStringBuffer = BufferFactory.getBoundStringBuffer();
        boundStringBuffer.setParameters(typeInstance, nbBitsPerChar, lowerBound);
        multiplierStringBuffer = (KnownMultiplierStringBuffer) boundStringBuffer;
      }
      if (lengthUpperBound != -1L && lengthLowerBound == lengthUpperBound && lengthUpperBound < 65536L)
        this.write(lengthUpperBound * (long) nbBitsPerChar > 16L, (AbstractBitField) multiplierStringBuffer);
      else if (lengthUpperBound != -1L)
        this.writeLengthAndValue(lengthUpperBound * (long) nbBitsPerChar >= 16L, lengthLowerBound, lengthUpperBound, (AbstractBuffer) multiplierStringBuffer);
      else
        this.writeLengthAndValue(true, lengthLowerBound, lengthUpperBound, (AbstractBuffer) multiplierStringBuffer);
      BufferFactory.putBuffer((AbstractBuffer) multiplierStringBuffer);
    }

    protected internal virtual void __encodeNullType(Asn1NullType typeInstance)
    {
    }

    protected internal virtual void __encodeObjectIdentifierType(Asn1ObjectIdentifierType typeInstance)
    {
      BitFieldWrapper bitFieldWrapper = BufferFactory.getBitFieldWrapper();
      byte[] byteArrayValue = typeInstance.GetByteArrayValue();
      bitFieldWrapper.setByteArray(byteArrayValue);
      bitFieldWrapper.setLength(2, byteArrayValue.Length);
      this.writeLengthAndValue(true, 0L, -1L, (AbstractBuffer) bitFieldWrapper);
      BufferFactory.putBuffer((AbstractBuffer) bitFieldWrapper);
    }

    protected internal virtual void __encodeOctetStringType(Asn1OctetStringType typeInstance)
    {
      if (typeInstance.__isContainingType() && typeInstance.GetTypeValue() != null)
      {
        IEncoder encoder = typeInstance.__getEncoder() ?? (IEncoder) this.getWriter();
        try
        {
          encoder.Encode(typeInstance.GetTypeValue());
        }
        catch (Asn1ValidationException ex)
        {
          throw ex;
        }
        catch (Asn1Exception ex)
        {
          throw new Asn1Exception(51, "type <" + typeInstance.GetTypeValue().GetType().FullName + "> for OCTET SRING type <" + typeInstance.GetType().FullName + "> [internal exception : " + ex.Message + "]");
        }
        typeInstance.SetValue(encoder.Data);
      }
      BitFieldWrapper bitFieldWrapper = BufferFactory.getBitFieldWrapper();
      byte[] byteArrayValue = typeInstance.GetByteArrayValue();
      if (byteArrayValue != null)
      {
        bitFieldWrapper.setByteArray(byteArrayValue);
        bitFieldWrapper.setLength(2, byteArrayValue.Length);
      }
      if (typeInstance.__isPerConstraintExtensible())
      {
        this._out.writeOneBitNoAlign(typeInstance.__isValueOutOfRoot());
        if (typeInstance.__isValueOutOfRoot())
        {
          this.writeLengthAndValue(true, 0L, -1L, (AbstractBuffer) bitFieldWrapper);
          BufferFactory.putBuffer((AbstractBuffer) bitFieldWrapper);
          return;
        }
      }
      if (typeInstance.GetUpperSize() == 0L)
        this._out.printEmpty();
      else if (typeInstance.GetUpperSize() != -1L && typeInstance.GetLowerSize() == typeInstance.GetUpperSize() && typeInstance.GetUpperSize() <= 2L)
        this.write(false, (AbstractBitField) bitFieldWrapper);
      else if (typeInstance.GetUpperSize() != -1L && typeInstance.GetLowerSize() == typeInstance.GetUpperSize() && typeInstance.GetUpperSize() < 65536L)
        this.write(true, (AbstractBitField) bitFieldWrapper);
      else
        this.writeLengthAndValue(true, typeInstance.GetLowerSize(), typeInstance.GetUpperSize(), (AbstractBuffer) bitFieldWrapper);
      BufferFactory.putBuffer((AbstractBuffer) bitFieldWrapper);
    }

    protected internal void __encodeOpenType(Asn1OpenType typeInstance)
    {
      Asn1Type typeValue = typeInstance.GetTypeValue();
      if (typeValue != null)
      {
        this.encodeOpenHelper_Begin();
        typeValue.__write(this);
        this.encodeOpenHelper_End();
      }
      else
      {
        BitFieldWrapper bitFieldWrapper = BufferFactory.getBitFieldWrapper();
        byte[] byteArrayValue = typeInstance.GetByteArrayValue();
        if (byteArrayValue == null || byteArrayValue.Length == 0)
          throw new Asn1Exception(48, " for type <" + typeInstance.GetType().FullName + ">");
        bitFieldWrapper.setByteArray(byteArrayValue);
        bitFieldWrapper.setLength(2, byteArrayValue.Length);
        this.writeLengthAndValue(true, 0L, -1L, (AbstractBuffer) bitFieldWrapper);
        BufferFactory.putBuffer((AbstractBuffer) bitFieldWrapper);
      }
    }

    protected internal virtual void __encodeRealType(Asn1RealType typeInstance)
    {
      Asn1TypeBerWriter asn1TypeBerWriter = new Asn1TypeBerWriter();
      MemoryStream memoryStream = new MemoryStream();
      asn1TypeBerWriter.__encodeRealLengthAndValue(typeInstance, (Stream) memoryStream);
      BitFieldWrapper bitFieldWrapper = BufferFactory.getBitFieldWrapper();
      byte[] array = memoryStream.ToArray();
      bitFieldWrapper.setByteArray(array);
      bitFieldWrapper.setLength(2, array.Length);
      this.writeLengthAndValue(true, 0L, -1L, (AbstractBuffer) bitFieldWrapper);
      BufferFactory.putBuffer((AbstractBuffer) bitFieldWrapper);
    }

    protected internal virtual void __encodeRelativeOIDType(Asn1RelativeOIDType typeInstance)
    {
      BitFieldWrapper bitFieldWrapper = BufferFactory.getBitFieldWrapper();
      byte[] byteArrayValue = typeInstance.GetByteArrayValue();
      bitFieldWrapper.setByteArray(byteArrayValue);
      bitFieldWrapper.setLength(2, byteArrayValue.Length);
      this.writeLengthAndValue(true, 0L, -1L, (AbstractBuffer) bitFieldWrapper);
      BufferFactory.putBuffer((AbstractBuffer) bitFieldWrapper);
    }

    protected internal virtual void __encodeTimeType(Asn1TimeType typeInstance)
    {
      Asn1TimeTypeAdapterFactory.getAsn1TimeTypeAdapter(typeInstance).__write(this);
    }

    protected internal virtual void __encodeUnknownMultiplierStringType(Asn1UnknownMultiplierStringType typeInstance)
    {
      BitFieldWrapper bitFieldWrapper = BufferFactory.getBitFieldWrapper();
      byte[] byteArrayValue;
      try
      {
        byteArrayValue = typeInstance.GetByteArrayValue();
      }
      catch (IOException ex)
      {
        throw new Asn1Exception(21, "exception " + ex.Message + " for type <" + typeInstance.GetType().FullName + ">");
      }
      if (byteArrayValue != null)
      {
        bitFieldWrapper.setByteArray(byteArrayValue);
        bitFieldWrapper.setLength(2, byteArrayValue.Length);
      }
      this.writeLengthAndValue(true, 0L, -1L, (AbstractBuffer) bitFieldWrapper);
      BufferFactory.putBuffer((AbstractBuffer) bitFieldWrapper);
    }

    protected internal void __encodeUnrestrictedCharacterStringType(Asn1ConstructedType typeInstance)
    {
      for (int index = 0; index < typeInstance.__getNbRootComponents(); ++index)
      {
        if (index != 1)
          typeInstance.GetAsn1Type(index).__write(this);
      }
    }

    protected internal virtual void __write(AbstractBitField buffer, int start, int length)
    {
      buffer.write(start, length, (IAsn1BitOutputStream) this._out);
    }

    private void completeEncoding()
    {
      if (this._out.getBitCount() == 0)
        this._out.writePaddingBit(8);
      this._nbPostPaddingBits = this._out.getNbPostPaddingBits();
    }

    private void encodeConstrainedNumber(long val, long lb, long ub)
    {
      if (lb > val || val > ub)
      {
        throw new Asn1Exception(27, " trying to encode an INTEGER value <" + (object) val + "> outside the defined bounds [" + (string) (object) lb + ".." + (string) (object) ub + "]");
      }
      else
      {
        long num = ub - lb + 1L;
        if (num == 1L)
          return;
        IntegerBuffer integerBuffer = BufferFactory.getIntegerBuffer();
        if (this._aligned)
        {
          if (2L <= num && num < 256L)
          {
            this.encodePositiveNumber(val - lb, integerBuffer);
            int length = Tools.nbBitsForPositiveNumber(num - 1L);
            integerBuffer.setLength(1, length);
            this.write(false, (AbstractBitField) integerBuffer);
          }
          else if (num == 256L)
          {
            this.encodePositiveNumber(val - lb, integerBuffer);
            integerBuffer.setLength(2, 1);
            this.write(true, (AbstractBitField) integerBuffer);
          }
          else if (256L < num && num <= 65536L)
          {
            this.encodePositiveNumber(val - lb, integerBuffer);
            integerBuffer.setLength(2, 2);
            this.write(true, (AbstractBitField) integerBuffer);
          }
          else
          {
            this.encodePositiveNumber(val - lb, integerBuffer);
            this.writeLengthAndValue(true, 1L, (long) Tools.nbBytesForPositiveNumber(num - 1L), (AbstractBuffer) integerBuffer);
          }
        }
        else
        {
          this.encodePositiveNumber(val - lb, integerBuffer);
          int length = Tools.nbBitsForPositiveNumber(num - 1L);
          integerBuffer.setLength(1, length);
          this.write(false, (AbstractBitField) integerBuffer);
        }
        BufferFactory.putBuffer((AbstractBuffer) integerBuffer);
      }
    }

    private void encodeConstructedHelper(Asn1ConstructedType typeInstance, BitString descriptor, int firstAbsoluteIndex, int length)
    {
      if (descriptor != null)
      {
        BitFieldWrapper bitFieldWrapper = BufferFactory.getBitFieldWrapper();
        bitFieldWrapper.setByteArray(descriptor.getInternalBytes());
        bitFieldWrapper.setLength(1, descriptor.length());
        if (bitFieldWrapper.getNbElements() < 65536)
          this.write(false, (AbstractBitField) bitFieldWrapper);
        else
          this.writeLengthAndValue(false, (long) bitFieldWrapper.getNbElements(), (long) bitFieldWrapper.getNbElements(), (AbstractBuffer) bitFieldWrapper);
        BufferFactory.putBuffer((AbstractBuffer) bitFieldWrapper);
      }
      int bitIndex = 0;
      for (int index = firstAbsoluteIndex; index < firstAbsoluteIndex + length; ++index)
      {
        if (typeInstance.IsComponentDefault(index) || typeInstance.IsComponentOptional(index))
        {
          if (descriptor.get(bitIndex))
            typeInstance.__getDefinedComponentTypeInstance(index).__write(this);
          ++bitIndex;
        }
        else
          typeInstance.GetAsn1Type(index).__write(this);
      }
    }

    protected override void encodeImpl(Asn1Type type, Stream outputStream)
    {
      this.init(outputStream);
      type.__write(this);
      this._nbPostPaddingBits = 0;
      this.completeEncoding();
      this.flush();
    }

    private void encodeOpenHelper_Begin()
    {
      this.pushOutputStream();
    }

    private void encodeOpenHelper_End()
    {
      this.completeEncoding();
      this._out.flush();
      ByteArrayAsn1BitOutputStream asn1BitOutputStream = this.popOutputStream();
      BitFieldWrapper bitFieldWrapper = BufferFactory.getBitFieldWrapper();
      byte[] bytes = asn1BitOutputStream.getBytes();
      bitFieldWrapper.setByteArray(bytes);
      bitFieldWrapper.setLength(2, bytes.Length);
      this.writeLengthAndValue(true, 0L, -1L, (AbstractBuffer) bitFieldWrapper);
      BufferFactory.putBuffer((AbstractBuffer) bitFieldWrapper);
    }

    private void encodePositiveNumber(long longValue, IntegerBuffer buffer)
    {
      buffer.setLong(longValue);
      int length = Tools.nbBytesForPositiveNumber(longValue);
      buffer.setLength(2, length);
    }

    private void encodeSemiConstrainedNumber(long val, long lb)
    {
      if (lb > val)
      {
        throw new Asn1Exception(27, " trying to encode an INTEGER value <" + (object) val + "> lower than the defined lower bound [" + (string) (object) lb + "]");
      }
      else
      {
        IntegerBuffer integerBuffer = BufferFactory.getIntegerBuffer();
        this.encodePositiveNumber(val - lb, integerBuffer);
        this.writeLengthAndValue(true, 0L, -1L, (AbstractBuffer) integerBuffer);
        BufferFactory.putBuffer((AbstractBuffer) integerBuffer);
      }
    }

    private void encodeSizedNumber(long val, int size)
    {
      IntegerBuffer integerBuffer = BufferFactory.getIntegerBuffer();
      this.encodePositiveNumber(val, integerBuffer);
      integerBuffer.setLength(1, size);
      this.write(false, (AbstractBitField) integerBuffer);
      BufferFactory.putBuffer((AbstractBuffer) integerBuffer);
    }

    private void encodeSmallPositiveNumber(long val)
    {
      if (val < 64L)
      {
        IntegerBuffer integerBuffer = BufferFactory.getIntegerBuffer();
        this._out.writeOneBitNoAlign(false);
        this.encodePositiveNumber(val, integerBuffer);
        integerBuffer.setLength(1, 6);
        this.write(false, (AbstractBitField) integerBuffer);
        BufferFactory.putBuffer((AbstractBuffer) integerBuffer);
      }
      else
      {
        this._out.writeOneBitNoAlign(true);
        this.encodeSemiConstrainedNumber(val, 0L);
      }
    }

    private void encodeTwoComplBigNumber(byte[] bigValue, IntegerBuffer buffer)
    {
      buffer.setBigInteger(bigValue);
      int length = bigValue.Length;
      buffer.setLength(2, length);
    }

    private void encodeTwoComplNumber(long longValue, IntegerBuffer buffer)
    {
      buffer.setLong(longValue);
      int length = Tools.nbBytesForNumber(longValue);
      buffer.setLength(2, length);
    }

    private void encodeUnconstrainedNumber(Asn1IntegerType typeInstance)
    {
      IntegerBuffer integerBuffer = BufferFactory.getIntegerBuffer();
      if (typeInstance.GetBigIntegerValue() == null)
        this.encodeTwoComplNumber(typeInstance.GetLongValue(), integerBuffer);
      else
        this.encodeTwoComplBigNumber(typeInstance.GetBigIntegerValue().GetBytes(), integerBuffer);
      this.writeLengthAndValue(true, 0L, -1L, (AbstractBuffer) integerBuffer);
      BufferFactory.putBuffer((AbstractBuffer) integerBuffer);
    }

    protected override void flush()
    {
      this._out.flush();
    }

    public int GetNbPostPaddingBits()
    {
      return this._nbPostPaddingBits;
    }

    public override string GetProperty(string key)
    {
      if (key.Equals("validating"))
        return this._isValidating.ToString();
      if (key.Equals("internalErrorLogDir"))
        return this._internalErrorLogDir;
      if (key.Equals("encodingDefaultValues"))
        return this._isEncodingDefaultValues.ToString();
      if (key.Equals("paddingWithZeroBit"))
        return this._isPaddingWithZeroBit.ToString();
      else
        return (string) null;
    }

    public virtual Asn1TypeWriter getWriter()
    {
      Asn1TypePerWriter asn1TypePerWriter = new Asn1TypePerWriter();
      asn1TypePerWriter._isValidating = this._isValidating;
      asn1TypePerWriter._aligned = this._aligned;
      asn1TypePerWriter._isEncodingDefaultValues = this._isEncodingDefaultValues;
      asn1TypePerWriter._isPaddingWithZeroBit = this._isPaddingWithZeroBit;
      return (Asn1TypeWriter) asn1TypePerWriter;
    }

    protected override void init(Stream output)
    {
      this._out = new Asn1BitOutputStream(output);
      this._out.setZeroPadded(this._isPaddingWithZeroBit);
    }

    protected bool isCompatible(object reader)
    {
      return reader != null && reader is Asn1TypePerReader && ((Asn1TypePerReader) reader).IsAligned.Equals(this.IsAligned);
    }

    private ByteArrayAsn1BitOutputStream popOutputStream()
    {
      ByteArrayAsn1BitOutputStream asn1BitOutputStream = (ByteArrayAsn1BitOutputStream) this._out;
      this._out = (Asn1BitOutputStream) this._outputStreamStack.Pop();
      return asn1BitOutputStream;
    }

    private ByteArrayAsn1BitOutputStream pushOutputStream()
    {
      this._outputStreamStack.Push((object) this._out);
      ByteArrayAsn1BitOutputStream asn1BitOutputStream = new ByteArrayAsn1BitOutputStream(32);
      asn1BitOutputStream.setZeroPadded(this._isPaddingWithZeroBit);
      this._out = (Asn1BitOutputStream) asn1BitOutputStream;
      return asn1BitOutputStream;
    }

    protected override void reset()
    {
      this._outputStreamStack.Clear();
      this._out = (Asn1BitOutputStream) null;
    }

    private void restoreAlignment()
    {
      if (!this._aligned)
        return;
      this._out.restoreAlignment();
    }

    public override void SetProperty(string key, string property)
    {
      if (!this.PropertyNames().Contains((object) key))
        return;
      if (key.Equals("validating"))
        this._isValidating = bool.Parse(property);
      else if (key.Equals("internalErrorLogDir"))
        this._internalErrorLogDir = property;
      else if (key.Equals("encodingDefaultValues"))
        this._isEncodingDefaultValues = bool.Parse(property);
      else if (key.Equals("paddingWithZeroBit"))
        this._isPaddingWithZeroBit = bool.Parse(property);
    }

    private void write(bool alignedBuffer, AbstractBitField buffer)
    {
      if (this._aligned && alignedBuffer)
        this._out.restoreAlignment();
      this.__write(buffer, 0, buffer.getNbElements());
    }

    private void writeLengthAndValue(bool alignedElement, long lengthLowerBound, long lengthUpperBound, AbstractBuffer valueBuffer)
    {
      if (lengthLowerBound > (long) valueBuffer.getNbElements() || lengthUpperBound != -1L && (long) valueBuffer.getNbElements() > lengthUpperBound)
      {
        throw new Asn1Exception(27, " trying to encode an INTEGER value <" + (object) valueBuffer.getNbElements() + "> outside the defined bounds [" + (string) (object) lengthLowerBound + ".." + (string) (object) lengthUpperBound + "]");
      }
      else
      {
        this._out.printLineSeparator();
        if (this._aligned)
        {
          if (lengthUpperBound != -1L && lengthUpperBound < 65536L)
          {
            this.encodeConstrainedNumber((long) valueBuffer.getNbElements(), lengthLowerBound, lengthUpperBound);
            if (alignedElement && valueBuffer.getNbElements() != 0)
              this.restoreAlignment();
            valueBuffer.writeElements(0, valueBuffer.getNbElements(), this);
          }
          else
            this.writeLengthAndValueFrom(0, lengthLowerBound, lengthUpperBound, valueBuffer);
        }
        else if (lengthUpperBound != -1L && lengthUpperBound < 65536L)
        {
          IntegerBuffer integerBuffer = BufferFactory.getIntegerBuffer();
          this.encodePositiveNumber((long) valueBuffer.getNbElements() - lengthLowerBound, integerBuffer);
          long num = lengthUpperBound - lengthLowerBound + 1L;
          if (num > 1L)
          {
            int length = Tools.nbBitsForPositiveNumber(num - 1L);
            integerBuffer.setLength(1, length);
            this.write(false, (AbstractBitField) integerBuffer);
          }
          valueBuffer.writeElements(0, valueBuffer.getNbElements(), this);
          BufferFactory.putBuffer((AbstractBuffer) integerBuffer);
        }
        else
          this.writeLengthAndValueFrom(0, lengthLowerBound, lengthUpperBound, valueBuffer);
      }
    }

    private void writeLengthAndValueFrom(int startIndex, long lengthLowerBound, long lengthUpperBound, AbstractBuffer valueBuffer)
    {
      this.restoreAlignment();
      if (valueBuffer.getNbElementsFrom(startIndex) < 128)
      {
        IntegerBuffer integerBuffer = BufferFactory.getIntegerBuffer();
        this._out.writeOneBitNoAlign(false);
        this.encodePositiveNumber((long) valueBuffer.getNbElementsFrom(startIndex), integerBuffer);
        integerBuffer.setLength(1, 7);
        this.write(false, (AbstractBitField) integerBuffer);
        valueBuffer.writeElements(startIndex, valueBuffer.getNbElementsFrom(startIndex), this);
        BufferFactory.putBuffer((AbstractBuffer) integerBuffer);
      }
      else if (valueBuffer.getNbElementsFrom(startIndex) < 16384)
      {
        IntegerBuffer integerBuffer = BufferFactory.getIntegerBuffer();
        this._out.writeOneBitNoAlign(true);
        this._out.writeOneBitNoAlign(false);
        this.encodePositiveNumber((long) valueBuffer.getNbElementsFrom(startIndex), integerBuffer);
        integerBuffer.setLength(1, 14);
        this.write(false, (AbstractBitField) integerBuffer);
        valueBuffer.writeElements(startIndex, valueBuffer.getNbElementsFrom(startIndex), this);
        BufferFactory.putBuffer((AbstractBuffer) integerBuffer);
      }
      else
      {
        IntegerBuffer integerBuffer = BufferFactory.getIntegerBuffer();
        this._out.writeOneBitNoAlign(true);
        this._out.writeOneBitNoAlign(true);
        int num = valueBuffer.getNbElementsFrom(startIndex) / 16384;
        int length;
        if (num >= 4)
        {
          length = 65536;
          this.encodePositiveNumber(4L, integerBuffer);
        }
        else if (num >= 3)
        {
          length = 49152;
          this.encodePositiveNumber(3L, integerBuffer);
        }
        else if (num >= 2)
        {
          length = 32768;
          this.encodePositiveNumber(2L, integerBuffer);
        }
        else
        {
          length = 16384;
          this.encodePositiveNumber(1L, integerBuffer);
        }
        integerBuffer.setLength(1, 6);
        this.write(false, (AbstractBitField) integerBuffer);
        BufferFactory.putBuffer((AbstractBuffer) integerBuffer);
        valueBuffer.writeElements(startIndex, length, this);
        this.writeLengthAndValueFrom(startIndex + length, 0L, -1L, valueBuffer);
      }
    }

    protected virtual void writeOneBitNoAlign(bool aBit)
    {
      this._out.writeOneBitNoAlign(aBit);
    }

    private void writeSmallLengthAndValue(AbstractBuffer valueBuffer)
    {
      if (valueBuffer.getNbElements() <= 64)
      {
        IntegerBuffer integerBuffer = BufferFactory.getIntegerBuffer();
        this._out.writeOneBitNoAlign(false);
        this.encodePositiveNumber((long) (valueBuffer.getNbElements() - 1), integerBuffer);
        integerBuffer.setLength(1, 6);
        this.write(false, (AbstractBitField) integerBuffer);
        BufferFactory.putBuffer((AbstractBuffer) integerBuffer);
        valueBuffer.writeElements(0, valueBuffer.getNbElements(), this);
      }
      else
      {
        this._out.writeOneBitNoAlign(true);
        this.writeLengthAndValueFrom(0, 0L, -1L, valueBuffer);
      }
    }
  }
}
