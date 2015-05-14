// Decompiled with JetBrains decompiler
// Type: Novensys.ASN1.Type.Asn1TypePerReader
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
  public class Asn1TypePerReader : Asn1TypeReader
  {
    public const string KEY_ALLOWING_ZERO_LENGTH_EXTENSIONS = "allowingZeroLengthExtensions";
    public const string KEY_IGNORING_PADDING_BIT_VALUE = "ignoringPaddingBitValue";
    public const string KEY_IGNORING_POST_PADDING_BITS = "ignoringPostPaddingBits";
    protected bool _aligned;
    private Asn1BitInputStream _in;
    private Stack _inStreamStack;
    protected bool _isAllowingZeroLengthExtensions;
    protected bool _isExtendedEncodingEnabled;
    protected bool _isIgnoringPaddingBitValue;
    protected bool _isIgnoringPostPaddingBits;
    private int _usedBytes;

    public bool IsAligned
    {
      get
      {
        return this._aligned;
      }
    }

    public bool IsAllowingZeroLengthExtensions
    {
      get
      {
        return this._isAllowingZeroLengthExtensions;
      }
      set
      {
        this._isAllowingZeroLengthExtensions = value;
      }
    }

    public bool IsIgnoringPaddingBitValue
    {
      get
      {
        return this._isIgnoringPaddingBitValue;
      }
      set
      {
        this._isIgnoringPaddingBitValue = value;
      }
    }

    public bool IsIgnoringPostPaddingBits
    {
      get
      {
        return this._isIgnoringPostPaddingBits;
      }
      set
      {
        this._isIgnoringPostPaddingBits = value;
      }
    }

    public Asn1TypePerReader()
    {
      this._inStreamStack = new Stack();
      this._isIgnoringPostPaddingBits = false;
      this._isIgnoringPaddingBitValue = true;
      this._isAllowingZeroLengthExtensions = true;
      this._isExtendedEncodingEnabled = false;
      this._aligned = true;
    }

    public Asn1TypePerReader(bool aligned)
      : this()
    {
      this._aligned = aligned;
    }

    protected internal virtual void __decodeBitStringType(Asn1BitStringType typeInstance)
    {
      BitFieldWrapper bitFieldWrapper = BufferFactory.getBitFieldWrapper();
      bitFieldWrapper.setGranularity(1);
      if (typeInstance.__isPerConstraintExtensible() && this._in.readOneBitNoAlign())
      {
        this.readLengthAndValue(true, 0L, -1L, (AbstractBuffer) bitFieldWrapper);
        typeInstance.SetValue(bitFieldWrapper.getByteArray(), bitFieldWrapper.getLength());
        BufferFactory.putBuffer((AbstractBuffer) bitFieldWrapper);
      }
      else
      {
        if (typeInstance.GetUpperSize() == 0L)
          this._in.printEmpty();
        else if (typeInstance.GetUpperSize() != -1L && typeInstance.GetLowerSize() == typeInstance.GetUpperSize() && typeInstance.GetUpperSize() <= 16L)
          this.read(false, (AbstractBitField) bitFieldWrapper, (int) typeInstance.GetLowerSize());
        else if (typeInstance.GetUpperSize() != -1L && typeInstance.GetLowerSize() == typeInstance.GetUpperSize() && typeInstance.GetUpperSize() < 65536L)
          this.read(true, (AbstractBitField) bitFieldWrapper, (int) typeInstance.GetLowerSize());
        else
          this.readLengthAndValue(true, typeInstance.GetLowerSize(), typeInstance.GetUpperSize(), (AbstractBuffer) bitFieldWrapper);
        typeInstance.SetValue(bitFieldWrapper.getByteArray(), bitFieldWrapper.getLength());
        if (typeInstance.__isContainingType())
        {
          typeInstance.__setDecoder((IDecoder) this.getReader());
          this._isContentToBeResolved = true;
        }
        BufferFactory.putBuffer((AbstractBuffer) bitFieldWrapper);
      }
    }

    protected internal virtual void __decodeBooleanType(Asn1BooleanType typeInstance)
    {
      typeInstance.SetValue(this._in.readOneBitNoAlign());
    }

    protected internal virtual void __decodeChoiceType(Asn1ChoiceType typeInstance)
    {
      if (!this._isExtendedEncodingEnabled || !typeInstance.__isPerUseTag())
      {
        bool flag = false;
        if (typeInstance.IsExtensible())
          flag = this._in.readOneBitNoAlign();
        if (!flag)
        {
          int relativeIndex = typeInstance.__getNbRootComponents() <= 1 ? 0 : (int) this.decodeConstrainedNumber(0L, (long) (typeInstance.__getNbRootComponents() - 1), false);
          Asn1Type matchingComponent = typeInstance.__getMatchingComponent(relativeIndex, true);
          if (matchingComponent == null)
            throw new Asn1Exception(29, "index=" + (object) relativeIndex + " for type <" + typeInstance.GetType().FullName + ">");
          else
            matchingComponent.__read(this);
        }
        else
        {
          int relativeIndex = (int) this.decodeSmallPositiveNumber();
          Asn1Type matchingComponent = typeInstance.__getMatchingComponent(relativeIndex, false);
          if (matchingComponent != null)
          {
            this.decodeOpenHelper_Begin();
            matchingComponent.__read(this);
            this.decodeOpenHelper_End();
          }
          else
            typeInstance.__addUnknownExtension(this.getCompleteEncodingValue(this._isAllowingZeroLengthExtensions));
        }
      }
      else
      {
        int num = (int) this.decodeSizedNumber(typeInstance.__getPerEmbeddedTagTagSize());
        Asn1Type asn1Type = (Asn1Type) null;
        int[] tagTagNumbersSet = typeInstance.__getPerEmbeddedTagTagNumbersSet();
        if (tagTagNumbersSet != null)
        {
          for (int relativeIndex = 0; relativeIndex < tagTagNumbersSet.Length; ++relativeIndex)
          {
            if (num == tagTagNumbersSet[relativeIndex])
            {
              asn1Type = typeInstance.__getMatchingComponent(relativeIndex, true);
              break;
            }
          }
        }
        if (asn1Type != null)
          asn1Type.__read(this);
        else if (!typeInstance.IsExtensible())
        {
          throw new Asn1Exception(49, "for CHOICE type using tag <" + (object) typeInstance.GetType().FullName + "> (tag = " + (string) (object) num + ")");
        }
        else
        {
          int perLengthSize = typeInstance.__getPerLengthSize();
          if (perLengthSize != -1 || typeInstance.__isPerLengthIn())
          {
            BitFieldWrapper bitFieldWrapper = BufferFactory.getBitFieldWrapper();
            bitFieldWrapper.setGranularity(2);
            int length = !typeInstance.__isPerLengthIn() ? (int) this.decodeSizedNumber(perLengthSize) : typeInstance.__getPerLength();
            if (typeInstance.__getPerCountDelta() != 0)
              length -= typeInstance.__getPerCountDelta() >> 3;
            this.read(false, (AbstractBitField) bitFieldWrapper, length);
            typeInstance.__addUnknownExtension(bitFieldWrapper.getByteArray());
          }
          else
            typeInstance.__addUnknownExtension(this.getCompleteEncodingValue(this._isAllowingZeroLengthExtensions));
        }
      }
    }

    protected internal virtual void __decodeConstructedOfType(Asn1ConstructedOfType typeInstance)
    {
      TypeVectorBuffer typeVectorBuffer = BufferFactory.getTypeVectorBuffer();
      typeVectorBuffer.setAsn1TypeList(typeInstance);
      if (typeInstance.__isPerConstraintExtensible() && this._in.readOneBitNoAlign())
      {
        this.readLengthAndValue(false, 0L, -1L, (AbstractBuffer) typeVectorBuffer);
        BufferFactory.putBuffer((AbstractBuffer) typeVectorBuffer);
      }
      else
      {
        if (this._isExtendedEncodingEnabled && (typeInstance.__isPerTerminatedByCarrier() || typeInstance.__isPerTerminatedByEndOf()))
        {
          if (typeInstance.__isPerTerminatedByEndOf())
          {
            while (typeInstance.__getPerCountBeforeTerminated(this._in.getBitIndex()) > 0)
              typeInstance.__getMatchingElement().__read(this);
          }
          else if (typeInstance.__isPerTerminatedByCarrier())
          {
            while (!this._in.isTerminated())
              typeInstance.__getMatchingElement().__read(this);
          }
        }
        else if (typeInstance.GetUpperSize() != -1L && typeInstance.GetLowerSize() == typeInstance.GetUpperSize() && typeInstance.GetUpperSize() < 65536L)
          typeVectorBuffer.readElements(0, (int) typeInstance.GetLowerSize(), this);
        else
          this.readLengthAndValue(false, typeInstance.GetLowerSize(), typeInstance.GetUpperSize(), (AbstractBuffer) typeVectorBuffer);
        BufferFactory.putBuffer((AbstractBuffer) typeVectorBuffer);
      }
    }

    protected internal virtual void __decodeConstructedOfTypeElement(Asn1ConstructedOfType typeInstance)
    {
      typeInstance.__getMatchingElement().__read(this);
    }

    protected internal virtual void __decodeConstructedType(Asn1ConstructedType typeInstance)
    {
      bool flag = false;
      if (typeInstance.IsExtensible())
        flag = this._in.readOneBitNoAlign();
      this.decodeConstructedHelper(typeInstance, typeInstance.__getRootOptDefDescriptor(), 0, typeInstance.__getNbRootComponents());
      if (!flag)
        return;
      BitFieldWrapper bitFieldWrapper = BufferFactory.getBitFieldWrapper();
      bitFieldWrapper.setGranularity(1);
      this.readSmallLengthAndValue((AbstractBuffer) bitFieldWrapper);
      int length = bitFieldWrapper.getLength();
      BitString bitString = length > 0 ? new BitString(bitFieldWrapper.getByteArray(), length) : new BitString(new byte[0], length);
      BufferFactory.putBuffer((AbstractBuffer) bitFieldWrapper);
      BitString extensionDescriptor = typeInstance.__getExtensionDescriptor();
      int val2 = extensionDescriptor == null ? 0 : extensionDescriptor.length();
      int num = Math.Min(length, val2);
      int bitIndex1 = 0;
      int index = 0;
      for (; bitIndex1 < num; ++bitIndex1)
      {
        int extensionAdditionGroup = typeInstance.__getComponentExtensionAdditionGroup(index);
        if (bitString.get(bitIndex1))
        {
          this.decodeOpenHelper_Begin();
          if (extensionAdditionGroup == -1)
          {
            typeInstance.__getMatchingComponent(index + typeInstance.__getNbRootComponents()).__read(this);
          }
          else
          {
            BitString descriptor = typeInstance.__newExtensionAdditionGroupOptDefDescriptor(extensionAdditionGroup);
            this.decodeConstructedHelper(typeInstance, descriptor, index + typeInstance.__getNbRootComponents(), typeInstance.__getExtensionAdditionGroupNbComponents(extensionAdditionGroup));
          }
          this.decodeOpenHelper_End();
        }
        index += extensionAdditionGroup == -1 ? 1 : typeInstance.__getExtensionAdditionGroupNbComponents(extensionAdditionGroup);
      }
      for (int bitIndex2 = num; bitIndex2 < length; ++bitIndex2)
      {
        if (bitString.get(bitIndex2))
          typeInstance.__addUnknownExtension(this.getCompleteEncodingValue(this._isAllowingZeroLengthExtensions));
      }
    }

    protected internal void __decodeEmbeddedPdvValue(Asn1ConstructedType typeInstance)
    {
      for (int absoluteIndex = 0; absoluteIndex < typeInstance.__getNbRootComponents(); ++absoluteIndex)
      {
        if (absoluteIndex != 1)
          typeInstance.__getMatchingComponent(absoluteIndex).__read(this);
      }
    }

    protected internal virtual void __decodeEnumeratedType(Asn1EnumeratedType typeInstance)
    {
      if (!typeInstance.IsExtensible())
      {
        int index = (int) this.decodeConstrainedNumber(0L, (long) (typeInstance.__getRootValueSetSize() - 1), false);
        typeInstance.__setIndex(index, true);
      }
      else
      {
        bool flag = this._in.readOneBitNoAlign();
        int index = !flag ? (int) this.decodeConstrainedNumber(0L, (long) (typeInstance.__getRootValueSetSize() - 1), false) : (int) this.decodeSmallPositiveNumber();
        typeInstance.__setIndex(index, !flag);
      }
    }

    protected internal virtual void __decodeIntegerType(Asn1IntegerType typeInstance)
    {
      if (typeInstance.__isPerConstraintExtensible() && this._in.readOneBitNoAlign())
        this.decodeUnconstrainedNumber(typeInstance);
      else if (typeInstance.IsUpperBoundDefined() && typeInstance.GetLowerBound() == typeInstance.GetUpperBound())
      {
        this._in.printEmpty();
        typeInstance.SetValue(typeInstance.GetLowerBound());
      }
      else if (typeInstance.IsLowerBoundDefined() && typeInstance.IsUpperBoundDefined())
        typeInstance.SetValue(this.decodeConstrainedNumber(typeInstance.GetLowerBound(), typeInstance.GetUpperBound(), typeInstance.__isPerReverseOctets()));
      else if (typeInstance.IsLowerBoundDefined())
        typeInstance.SetValue(this.decodeSemiConstrainedNumber(typeInstance.GetLowerBound()));
      else
        this.decodeUnconstrainedNumber(typeInstance);
    }

    protected internal virtual void __decodeKnownMultiplierStringValue(Asn1KnownMultiplierStringType typeInstance)
    {
      long lowerBound = typeInstance.GetLowerBound();
      long num = typeInstance.GetUpperBound();
      long lengthLowerBound = typeInstance.GetLowerSize();
      long lengthUpperBound = typeInstance.GetUpperSize();
      string alphabet = typeInstance.__getAlphabet();
      bool flag = false;
      if (typeInstance.__isPerConstraintExtensible() && this._in.readOneBitNoAlign())
      {
        flag = true;
        lowerBound = typeInstance.__getBaseAlphabetLowerBound();
        num = typeInstance.__getBaseAlphabetUpperBound();
        lengthLowerBound = 0L;
        lengthUpperBound = -1L;
        alphabet = typeInstance.__getBaseAlphabet();
      }
      int nbBitsPerChar = !this._aligned ? (flag ? typeInstance.__getBaseAlphabetUnalignedNbBitPerChar() : typeInstance.__getUnalignedNbBitPerChar()) : (flag ? typeInstance.__getBaseAlphabetAlignedNbBitPerChar() : typeInstance.__getAlignedNbBitPerChar());
      KnownMultiplierStringBuffer multiplierStringBuffer;
      if (num <= (long) ((1 << nbBitsPerChar) - 1))
      {
        BoundStringBuffer boundStringBuffer = BufferFactory.getBoundStringBuffer();
        boundStringBuffer.setParameters(typeInstance, nbBitsPerChar, 0L);
        multiplierStringBuffer = (KnownMultiplierStringBuffer) boundStringBuffer;
      }
      else if ((!flag || !typeInstance.__isAlphabetConstraintResetable()) && alphabet != null)
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
      if (this._isExtendedEncodingEnabled && (typeInstance.__getPerLengthSize() != -1 || typeInstance.__isPerLengthIn()))
      {
        int perLengthSize = typeInstance.__getPerLengthSize();
        int length = !typeInstance.__isPerLengthIn() ? (int) this.decodeSizedNumber(perLengthSize) : typeInstance.__getPerLength();
        if (typeInstance.__getPerCountDelta() != 0)
          length -= typeInstance.__getPerCountDelta() >> 3;
        this.read(false, (AbstractBitField) multiplierStringBuffer, length);
      }
      else if (lengthUpperBound != -1L && lengthLowerBound == lengthUpperBound && lengthUpperBound < 65536L)
        this.read(lengthUpperBound * (long) nbBitsPerChar > 16L, (AbstractBitField) multiplierStringBuffer, (int) lengthLowerBound);
      else if (lengthUpperBound != -1L)
        this.readLengthAndValue(lengthUpperBound * (long) nbBitsPerChar >= 16L, lengthLowerBound, lengthUpperBound, (AbstractBuffer) multiplierStringBuffer);
      else
        this.readLengthAndValue(true, lengthLowerBound, lengthUpperBound, (AbstractBuffer) multiplierStringBuffer);
      multiplierStringBuffer.updateTypeInstance();
      if (this._isExtendedEncodingEnabled && typeInstance.__isPerAlignRightToOctet())
        this._in.restoreAlignment();
      BufferFactory.putBuffer((AbstractBuffer) multiplierStringBuffer);
    }

    protected internal virtual void __decodeNullType(Asn1NullType typeInstance)
    {
    }

    protected internal virtual void __decodeObjectIdentifierType(Asn1ObjectIdentifierType typeInstance)
    {
      BitFieldWrapper bitFieldWrapper = BufferFactory.getBitFieldWrapper();
      bitFieldWrapper.setGranularity(2);
      this.readLengthAndValue(true, 0L, -1L, (AbstractBuffer) bitFieldWrapper);
      if (bitFieldWrapper.getLength() != 0)
        typeInstance.SetValue(bitFieldWrapper.getByteArray());
      BufferFactory.putBuffer((AbstractBuffer) bitFieldWrapper);
    }

    protected internal virtual void __decodeOctetStringType(Asn1OctetStringType typeInstance)
    {
      BitFieldWrapper bitFieldWrapper = BufferFactory.getBitFieldWrapper();
      bitFieldWrapper.setGranularity(2);
      if (typeInstance.__isPerConstraintExtensible() && this._in.readOneBitNoAlign())
      {
        this.readLengthAndValue(true, 0L, -1L, (AbstractBuffer) bitFieldWrapper);
        typeInstance.SetValue(bitFieldWrapper.getByteArray());
        BufferFactory.putBuffer((AbstractBuffer) bitFieldWrapper);
      }
      else
      {
        if (this._isExtendedEncodingEnabled && (typeInstance.__getPerLengthSize() != -1 || typeInstance.__isPerLengthIn()))
        {
          int perLengthSize = typeInstance.__getPerLengthSize();
          int length = !typeInstance.__isPerLengthIn() ? (int) this.decodeSizedNumber(perLengthSize) : typeInstance.__getPerLength();
          if (typeInstance.__getPerCountDelta() != 0)
            length -= typeInstance.__getPerCountDelta() >> 3;
          this.read(false, (AbstractBitField) bitFieldWrapper, length);
        }
        else if (typeInstance.GetUpperSize() == 0L)
          this._in.printEmpty();
        else if (typeInstance.GetUpperSize() != -1L && typeInstance.GetLowerSize() == typeInstance.GetUpperSize() && typeInstance.GetUpperSize() <= 2L)
          this.read(false, (AbstractBitField) bitFieldWrapper, (int) typeInstance.GetLowerSize());
        else if (typeInstance.GetUpperSize() != -1L && typeInstance.GetLowerSize() == typeInstance.GetUpperSize() && typeInstance.GetUpperSize() < 65536L)
          this.read(true, (AbstractBitField) bitFieldWrapper, (int) typeInstance.GetLowerSize());
        else
          this.readLengthAndValue(true, typeInstance.GetLowerSize(), typeInstance.GetUpperSize(), (AbstractBuffer) bitFieldWrapper);
        byte[] data = bitFieldWrapper.getByteArray();
        if (this._isExtendedEncodingEnabled && typeInstance.__isPerReverseOctets())
          data = ByteArray.ReverseByteArray(data);
        typeInstance.SetValue(data);
        if (typeInstance.__isContainingType())
        {
          typeInstance.__setDecoder((IDecoder) this.getReader());
          this._isContentToBeResolved = true;
        }
        BufferFactory.putBuffer((AbstractBuffer) bitFieldWrapper);
      }
    }

    protected internal void __decodeOpenType(Asn1OpenType typeInstance)
    {
      typeInstance.SetValue(this.getCompleteEncodingValue(false));
      typeInstance.__setDecoder((IDecoder) this.getReader());
      this._isContentToBeResolved = true;
    }

    protected internal virtual void __decodeRealType(Asn1RealType typeInstance)
    {
      BitFieldWrapper bitFieldWrapper = BufferFactory.getBitFieldWrapper();
      bitFieldWrapper.setGranularity(2);
      this.readLengthAndValue(true, 0L, -1L, (AbstractBuffer) bitFieldWrapper);
      byte[] byteArray = bitFieldWrapper.getByteArray();
      BufferFactory.putBuffer((AbstractBuffer) bitFieldWrapper);
      new Asn1TypeBerReader().__decodeRealValue((Stream) new MemoryStream(byteArray), typeInstance, (long) byteArray.Length);
    }

    protected internal virtual void __decodeRelativeOIDType(Asn1RelativeOIDType typeInstance)
    {
      BitFieldWrapper bitFieldWrapper = BufferFactory.getBitFieldWrapper();
      bitFieldWrapper.setGranularity(2);
      this.readLengthAndValue(true, 0L, -1L, (AbstractBuffer) bitFieldWrapper);
      if (bitFieldWrapper.getLength() != 0)
        typeInstance.SetValue(bitFieldWrapper.getByteArray());
      BufferFactory.putBuffer((AbstractBuffer) bitFieldWrapper);
    }

    protected internal virtual void __decodeTimeType(Asn1TimeType typeInstance)
    {
      Asn1TimeTypeAdapterFactory.getAsn1TimeTypeAdapter(typeInstance).__read(this);
      typeInstance.SetValue((string) null);
    }

    protected internal virtual void __decodeUnknownMultiplierStringValue(Asn1UnknownMultiplierStringType typeInstance)
    {
      BitFieldWrapper bitFieldWrapper = BufferFactory.getBitFieldWrapper();
      bitFieldWrapper.setGranularity(2);
      this.readLengthAndValue(true, 0L, -1L, (AbstractBuffer) bitFieldWrapper);
      try
      {
        typeInstance.SetValue(bitFieldWrapper.getByteArray());
      }
      catch (IOException ex)
      {
        throw new Asn1Exception(21, "exception " + ex.Message + " for type <" + typeInstance.GetType().FullName + ">");
      }
      BufferFactory.putBuffer((AbstractBuffer) bitFieldWrapper);
    }

    protected internal void __decodeUnrestrictedCharacterStringValue(Asn1ConstructedType typeInstance)
    {
      for (int absoluteIndex = 0; absoluteIndex < typeInstance.__getNbRootComponents(); ++absoluteIndex)
      {
        if (absoluteIndex != 1)
          typeInstance.__getMatchingComponent(absoluteIndex).__read(this);
      }
    }

    protected internal virtual void __read(AbstractBitField buffer, int start, int length)
    {
      buffer.read(start, length, (IAsn1BitInputStream) this._in);
    }

    protected override void close()
    {
      if (this._in != null)
        this._usedBytes = this._in.getByteCount();
      this._inStreamStack.Clear();
    }

    private void completeDecoding()
    {
      if (this._in.getBitCount() == 0)
        this._in.readPaddingBit(8);
      else
        this._in.restoreAlignment();
    }

    private long decodeConstrainedNumber(long lb, long ub, bool reversed)
    {
      long num1 = ub - lb + 1L;
      if (num1 == 1L)
        return lb;
      IntegerBuffer integerBuffer = BufferFactory.getIntegerBuffer();
      if (this._aligned)
      {
        if (2L <= num1 && num1 < 256L)
        {
          int length = Tools.nbBitsForPositiveNumber(num1 - 1L);
          integerBuffer.setGranularity(1);
          this.read(false, (AbstractBitField) integerBuffer, length);
        }
        else if (num1 == 256L)
        {
          integerBuffer.setGranularity(2);
          this.read(true, (AbstractBitField) integerBuffer, 1);
        }
        else if (256L < num1 && num1 <= 65536L)
        {
          integerBuffer.setGranularity(2);
          this.read(true, (AbstractBitField) integerBuffer, 2);
        }
        else
        {
          int num2 = Tools.nbBytesForPositiveNumber(num1 - 1L);
          integerBuffer.setGranularity(2);
          this.readLengthAndValue(true, 1L, (long) num2, (AbstractBuffer) integerBuffer);
        }
      }
      else
      {
        int length = Tools.nbBitsForPositiveNumber(num1 - 1L);
        integerBuffer.setGranularity(1);
        if (this._isExtendedEncodingEnabled)
          integerBuffer.setReversed(reversed);
        this.read(false, (AbstractBitField) integerBuffer, length);
      }
      long num3 = lb + this.decodePositiveNumber(integerBuffer);
      BufferFactory.putBuffer((AbstractBuffer) integerBuffer);
      return num3;
    }

    private void decodeConstructedHelper(Asn1ConstructedType typeInstance, BitString descriptor, int firstAbsoluteIndex, int length)
    {
      if (descriptor != null)
      {
        BitFieldWrapper bitFieldWrapper = BufferFactory.getBitFieldWrapper();
        bitFieldWrapper.setGranularity(1);
        descriptor.clearAll();
        bitFieldWrapper.setByteArray(descriptor.getInternalBytes());
        if (this._isExtendedEncodingEnabled && typeInstance.__isPerOptionalityIn())
          descriptor = typeInstance.__getPerOptionalityDescriptor();
        else if (descriptor.length() < 65536)
          this.read(false, (AbstractBitField) bitFieldWrapper, descriptor.length());
        else
          this.readLengthAndValue(false, (long) descriptor.length(), (long) descriptor.length(), (AbstractBuffer) bitFieldWrapper);
        BufferFactory.putBuffer((AbstractBuffer) bitFieldWrapper);
      }
      if (this._isExtendedEncodingEnabled && (typeInstance.__getPerLengthSize() != -1 || typeInstance.__isPerLengthIn()))
      {
        int perLengthSize = typeInstance.__getPerLengthSize();
        int encodingLength = !typeInstance.__isPerLengthIn() ? (int) this.decodeSizedNumber(perLengthSize) : typeInstance.__getPerLength();
        if (typeInstance.__isPerCountOctets())
          encodingLength <<= 3;
        if (typeInstance.__isPerTerminatingComponent())
          typeInstance.__setPerTerminateInfos(encodingLength, this._in.getBitCount());
      }
      int bitIndex = 0;
      for (int index = firstAbsoluteIndex; index < firstAbsoluteIndex + length; ++index)
      {
        if (typeInstance.IsComponentDefault(index) || typeInstance.IsComponentOptional(index))
        {
          if (descriptor.get(bitIndex))
            typeInstance.__getMatchingComponent(index).__read(this);
          ++bitIndex;
        }
        else
          typeInstance.__getMatchingComponent(index).__read(this);
      }
    }

    protected override void decodeImpl(Stream inputStream, Asn1Type type)
    {
      this.init(inputStream);
      type.__read(this);
      if (!this._isIgnoringPostPaddingBits)
        this.completeDecoding();
      this.close();
    }

    public void decodeOpenHelper_Begin()
    {
      this.pushInputStream(this.getCompleteEncodingValue(this._isAllowingZeroLengthExtensions));
    }

    public void decodeOpenHelper_End()
    {
      this.completeDecoding();
      this.popInputStream();
    }

    private long decodePositiveNumber(IntegerBuffer buffer)
    {
      return buffer.getLong();
    }

    private long decodeSemiConstrainedNumber(long lb)
    {
      IntegerBuffer integerBuffer = BufferFactory.getIntegerBuffer();
      integerBuffer.setGranularity(2);
      this.readLengthAndValue(true, 0L, -1L, (AbstractBuffer) integerBuffer);
      long num = lb + this.decodePositiveNumber(integerBuffer);
      BufferFactory.putBuffer((AbstractBuffer) integerBuffer);
      return num;
    }

    private long decodeSizedNumber(int size)
    {
      IntegerBuffer integerBuffer = BufferFactory.getIntegerBuffer();
      integerBuffer.setGranularity(1);
      this.read(false, (AbstractBitField) integerBuffer, size);
      long num = this.decodePositiveNumber(integerBuffer);
      BufferFactory.putBuffer((AbstractBuffer) integerBuffer);
      return num;
    }

    private long decodeSmallPositiveNumber()
    {
      if (this._in.readOneBitNoAlign())
        return this.decodeSemiConstrainedNumber(0L);
      IntegerBuffer integerBuffer = BufferFactory.getIntegerBuffer();
      integerBuffer.setGranularity(1);
      this.read(false, (AbstractBitField) integerBuffer, 6);
      long num = this.decodePositiveNumber(integerBuffer);
      BufferFactory.putBuffer((AbstractBuffer) integerBuffer);
      return num;
    }

    private BigInteger decodeTwoComplBigNumber(IntegerBuffer buffer)
    {
      return new BigInteger(buffer.getBigInteger());
    }

    private long decodeTwoComplNumber(IntegerBuffer buffer)
    {
      long number = buffer.getLong();
      if (buffer.isNegative())
      {
        int num = Tools.nbBitsForPositiveNumber(number);
        if (num % 8 == 0 && num != 0 && num <= 63)
          number = -1L << num | number;
      }
      return number;
    }

    private void decodeUnconstrainedNumber(Asn1IntegerType typeInstance)
    {
      IntegerBuffer integerBuffer = BufferFactory.getIntegerBuffer();
      integerBuffer.setGranularity(2);
      this.readLengthAndValue(true, 0L, -1L, (AbstractBuffer) integerBuffer);
      if (integerBuffer.getBigInteger() == null)
        typeInstance.SetValue(this.decodeTwoComplNumber(integerBuffer));
      else
        typeInstance.SetValue(this.decodeTwoComplBigNumber(integerBuffer));
      BufferFactory.putBuffer((AbstractBuffer) integerBuffer);
    }

    private byte[] getCompleteEncodingValue(bool allowsZeroLength)
    {
      BitFieldWrapper bitFieldWrapper = BufferFactory.getBitFieldWrapper();
      bitFieldWrapper.setGranularity(2);
      this.readLengthAndValue(true, 0L, -1L, (AbstractBuffer) bitFieldWrapper);
      byte[] byteArray = bitFieldWrapper.getByteArray();
      if (byteArray.Length == 0 && !allowsZeroLength)
        throw new Asn1Exception(12, "should not be '0' for a complete encoding");
      BufferFactory.putBuffer((AbstractBuffer) bitFieldWrapper);
      return byteArray;
    }

    public override string GetProperty(string key)
    {
      if (key.Equals("validating"))
        return this._isValidating.ToString();
      if (key.Equals("resolvingContent"))
        return this._isResolvingContent.ToString();
      if (key.Equals("internalErrorLogDir"))
        return this._internalErrorLogDir;
      if (key.Equals("ignoringPostPaddingBits"))
        return this._isIgnoringPostPaddingBits.ToString();
      if (key.Equals("ignoringPaddingBitValue"))
        return this._isIgnoringPaddingBitValue.ToString();
      if (key.Equals("allowingZeroLengthExtensions"))
        return this._isAllowingZeroLengthExtensions.ToString();
      else
        return (string) null;
    }

    public virtual Asn1TypeReader getReader()
    {
      Asn1TypePerReader asn1TypePerReader = new Asn1TypePerReader();
      asn1TypePerReader._isResolvingContent = this._isResolvingContent;
      asn1TypePerReader._aligned = this._aligned;
      asn1TypePerReader._isValidating = this._isValidating;
      asn1TypePerReader._isIgnoringPostPaddingBits = this._isIgnoringPostPaddingBits;
      asn1TypePerReader._isIgnoringPaddingBitValue = this._isIgnoringPaddingBitValue;
      asn1TypePerReader._isAllowingZeroLengthExtensions = this._isAllowingZeroLengthExtensions;
      return (Asn1TypeReader) asn1TypePerReader;
    }

    protected override void init(Stream input)
    {
      this._in = new Asn1BitInputStream(input, 0);
      this._in.setIgnoringPaddingBitValue(this._isIgnoringPaddingBitValue);
      this._usedBytes = 0;
      this._isContentToBeResolved = false;
    }

    private void popInputStream()
    {
      this._in = (Asn1BitInputStream) this._inStreamStack.Pop();
    }

    private ByteArrayAsn1BitInputStream pushInputStream(byte[] data)
    {
      this._inStreamStack.Push((object) this._in);
      ByteArrayAsn1BitInputStream asn1BitInputStream = new ByteArrayAsn1BitInputStream(data == null ? new byte[0] : data, this._in.getBitCount());
      this._in = (Asn1BitInputStream) asn1BitInputStream;
      return asn1BitInputStream;
    }

    private void read(bool alignedBuffer, AbstractBitField buffer, int length)
    {
      if (this._aligned && alignedBuffer)
        this._in.restoreAlignment();
      buffer.read(0, length, (IAsn1BitInputStream) this._in);
    }

    private void readLengthAndValue(bool alignedElement, long lengthLowerBound, long lengthUpperBound, AbstractBuffer valueBuffer)
    {
      if (this._aligned)
      {
        if (lengthUpperBound != -1L && lengthUpperBound < 65536L)
        {
          int length = (int) this.decodeConstrainedNumber(lengthLowerBound, lengthUpperBound, false);
          if (alignedElement && length != 0)
            this.restoreAlignment();
          valueBuffer.readElements(0, length, this);
        }
        else
          this.readLengthAndValueFrom(0, lengthLowerBound, lengthUpperBound, valueBuffer);
      }
      else if (lengthUpperBound != -1L && lengthUpperBound < 65536L)
      {
        IntegerBuffer integerBuffer = BufferFactory.getIntegerBuffer();
        long num1 = lengthUpperBound - lengthLowerBound + 1L;
        long num2 = lengthLowerBound;
        if (num1 > 1L)
        {
          int length = Tools.nbBitsForPositiveNumber(num1 - 1L);
          integerBuffer.setGranularity(1);
          this.read(false, (AbstractBitField) integerBuffer, length);
          num2 = lengthLowerBound + this.decodePositiveNumber(integerBuffer);
        }
        valueBuffer.readElements(0, (int) num2, this);
        BufferFactory.putBuffer((AbstractBuffer) integerBuffer);
      }
      else
        this.readLengthAndValueFrom(0, lengthLowerBound, lengthUpperBound, valueBuffer);
    }

    private void readLengthAndValueFrom(int startIndex, long lengthLowerBound, long lengthUpperBound, AbstractBuffer valueBuffer)
    {
      this.restoreAlignment();
      if (!this._in.readOneBitNoAlign())
      {
        IntegerBuffer integerBuffer = BufferFactory.getIntegerBuffer();
        integerBuffer.setGranularity(1);
        this.read(false, (AbstractBitField) integerBuffer, 7);
        long num = this.decodePositiveNumber(integerBuffer);
        valueBuffer.readElements(startIndex, (int) num, this);
        BufferFactory.putBuffer((AbstractBuffer) integerBuffer);
      }
      else if (!this._in.readOneBitNoAlign())
      {
        IntegerBuffer integerBuffer = BufferFactory.getIntegerBuffer();
        integerBuffer.setGranularity(1);
        this.read(false, (AbstractBitField) integerBuffer, 14);
        long num = this.decodePositiveNumber(integerBuffer);
        valueBuffer.readElements(startIndex, (int) num, this);
        BufferFactory.putBuffer((AbstractBuffer) integerBuffer);
      }
      else
      {
        IntegerBuffer integerBuffer = BufferFactory.getIntegerBuffer();
        integerBuffer.setGranularity(1);
        this.read(false, (AbstractBitField) integerBuffer, 6);
        int length;
        switch ((int) this.decodePositiveNumber(integerBuffer))
        {
          case 2:
            length = 32768;
            break;
          case 3:
            length = 49152;
            break;
          case 4:
            length = 65536;
            break;
          default:
            length = 16384;
            break;
        }
        BufferFactory.putBuffer((AbstractBuffer) integerBuffer);
        valueBuffer.readElements(startIndex, length, this);
        this.readLengthAndValueFrom(startIndex + length, 0L, -1L, valueBuffer);
      }
    }

    protected bool readOneBitNoAlign()
    {
      return this._in.readOneBitNoAlign();
    }

    private void readSmallLengthAndValue(AbstractBuffer valueBuffer)
    {
      if (!this._in.readOneBitNoAlign())
      {
        IntegerBuffer integerBuffer = BufferFactory.getIntegerBuffer();
        integerBuffer.setGranularity(1);
        this.read(false, (AbstractBitField) integerBuffer, 6);
        long num = this.decodePositiveNumber(integerBuffer);
        BufferFactory.putBuffer((AbstractBuffer) integerBuffer);
        valueBuffer.readElements(0, (int) num + 1, this);
      }
      else
        this.readLengthAndValueFrom(0, 0L, -1L, valueBuffer);
    }

    private void restoreAlignment()
    {
      if (!this._aligned)
        return;
      this._in.restoreAlignment();
    }

    public override void SetProperty(string key, string property)
    {
      if (!this.PropertyNames().Contains((object) key))
        return;
      if (key.Equals("validating"))
        this._isValidating = bool.Parse(property);
      else if (key.Equals("resolvingContent"))
        this._isResolvingContent = bool.Parse(property);
      else if (key.Equals("internalErrorLogDir"))
        this._internalErrorLogDir = property;
      else if (key.Equals("ignoringPostPaddingBits"))
        this._isIgnoringPostPaddingBits = bool.Parse(property);
      else if (key.Equals("ignoringPaddingBitValue"))
        this._isIgnoringPaddingBitValue = bool.Parse(property);
      else if (key.Equals("allowingZeroLengthExtensions"))
        this._isAllowingZeroLengthExtensions = bool.Parse(property);
    }

    public override int UsedBytes()
    {
      return this._usedBytes;
    }
  }
}
