// Decompiled with JetBrains decompiler
// Type: Novensys.ASN1.Type.Asn1TypeBerWriter
// Assembly: Novensys.eCard.SDK, Version=1.1.56.0, Culture=neutral, PublicKeyToken=null
// MVID: 59F9E456-D3DA-4066-A4A4-692B516A775E
// Assembly location: C:\Program Files (x86)\CNAS\SIUI-SPITAL\Novensys.eCard.SDK.dll

using Novensys.ASN1;
using Novensys.ASN1.IO;
using Novensys.ASN1.Util;
using System;
using System.Collections;
using System.IO;
using System.Text;

namespace Novensys.ASN1.Type
{
  [Serializable]
  public class Asn1TypeBerWriter : Asn1TypeWriter
  {
    private static byte[] EPILOGUE_OF_INDEFINITE_LENGTH = new byte[2];
    private static byte[] PROLOGUE_OF_INDEFINITE_LENGTH = new byte[1]
    {

        //use unchecked
        
        (byte) uint.MinValue
       
    };
    protected bool _isCanonicalizingTimeValues = true;
    protected bool _isIndefiniteLengthForm = false;
    protected bool _isSortingSetOf = false;
    public const string KEY_CANONICALIZING_TIME_VALUES = "canonicalizingTimeValues";
    public const string KEY_INDEFINITE_LENGTH_FORM = "indefiniteLengthForm";
    public const string KEY_REMOVING_TRAILING_ZEROES_IN_BIT_STRING_WITH_NAMED_BITS = "removingTrailingZeroesInBitStringWithNamedBits";
    public const string KEY_SORTING_SET_OF = "sortingSetOf";
    protected bool _isRemovingTrailingZeroesInBitStringWithNamedBits;
    private IAsn1OutputStream _mainStream;
    private IAsn1OutputStream _out;

    public Asn1TypeBerWriter()
    {
      this._isEncodingDefaultValues = false;
      this._isRemovingTrailingZeroesInBitStringWithNamedBits = true;
    }

    protected internal virtual void __encodeBitStringType(Asn1BitStringType typeInstance, int tagNumber, int tagClass, bool indefiniteLengthForm)
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
      this.encodeTag((Asn1Type) typeInstance, tagClass, true, tagNumber);
      BitString bitString = typeInstance.__getBitString();
      int len = bitString != null ? (!typeInstance.__isWithNamedBitList() || !this._isRemovingTrailingZeroesInBitStringWithNamedBits ? bitString.getNbBytes() : bitString.getNbTrimmedBytes()) : 0;
      if (len == 0)
      {
        this.encodeLengthOctetShortForm(1L);
        this._out.writeByte((byte) 0);
      }
      else
      {
        this.encodeLengthDefiniteForm((long) (len + 1));
        if (typeInstance.__isWithNamedBitList() && this._isRemovingTrailingZeroesInBitStringWithNamedBits)
        {
          this._out.writeByte((byte) bitString.getUnusedBitsInLastTrimmedByte());
          this._out.writeBytes(bitString.getBytes(), len);
        }
        else
        {
          this._out.writeByte((byte) bitString.getUnusedBitsInLastByte());
          this._out.writeBytes(bitString.getBytes(), len);
        }
      }
    }

    protected internal virtual void __encodeBooleanType(Asn1BooleanType typeInstance, int tagNumber, int tagClass)
    {
      this.encodeTag((Asn1Type) typeInstance, tagClass, true, tagNumber);
      this.encodeLengthOctetShortForm(1L);
      if (typeInstance.GetBoolValue())
        this._out.writeByte(byte.MaxValue);
      else
        this._out.writeByte((byte) 0);
    }

    protected internal virtual void __encodeChoiceType(Asn1ChoiceType typeInstance)
    {
      Asn1Type typeValue = typeInstance.GetTypeValue();
      if (typeValue == null)
        return;
      typeValue.__write(this);
    }

    protected internal void __encodeDateTimeType(Asn1DateTimeType typeInstance, int tagNumber, int tagClass)
    {
      this.encodeTag((Asn1Type) typeInstance, tagClass, true, tagNumber);
      char[] chArray = typeInstance.GetStringValue(this._isCanonicalizingTimeValues).ToCharArray();
      int length = chArray.Length;
      byte[] b;
      if (length != 19)
      {
        b = new byte[length];
        for (int index = 0; index < length; ++index)
          b[index] = (byte) chArray[index];
      }
      else
      {
        length = 14;
        b = new byte[14];
        for (int index = 0; index < 4; ++index)
          b[index] = (byte) chArray[index];
        for (int index = 5; index < 7; ++index)
          b[index - 1] = (byte) chArray[index];
        for (int index = 8; index < 10; ++index)
          b[index - 2] = (byte) chArray[index];
        for (int index = 11; index < 13; ++index)
          b[index - 3] = (byte) chArray[index];
        for (int index = 14; index < 16; ++index)
          b[index - 4] = (byte) chArray[index];
        for (int index = 17; index < 19; ++index)
          b[index - 5] = (byte) chArray[index];
      }
      this.encodeLengthDefiniteForm((long) length);
      this._out.writeBytes(b);
    }

    protected internal void __encodeDateType(Asn1DateType typeInstance, int tagNumber, int tagClass)
    {
      this.encodeTag((Asn1Type) typeInstance, tagClass, true, tagNumber);
      char[] chArray = typeInstance.GetStringValue(this._isCanonicalizingTimeValues).ToCharArray();
      int length = chArray.Length;
      byte[] b;
      if (length != 10)
      {
        b = new byte[length];
        for (int index = 0; index < length; ++index)
          b[index] = (byte) chArray[index];
      }
      else
      {
        length = 8;
        b = new byte[8];
        for (int index = 0; index < 4; ++index)
          b[index] = (byte) chArray[index];
        for (int index = 5; index < 7; ++index)
          b[index - 1] = (byte) chArray[index];
        for (int index = 8; index < 10; ++index)
          b[index - 2] = (byte) chArray[index];
      }
      this.encodeLengthDefiniteForm((long) length);
      this._out.writeBytes(b);
    }

    protected internal void __encodeDurationType(Asn1DurationType typeInstance, int tagNumber, int tagClass)
    {
      this.encodeTag((Asn1Type) typeInstance, tagClass, true, tagNumber);
      char[] chArray = typeInstance.GetStringValue(this._isCanonicalizingTimeValues).ToCharArray();
      int length1 = chArray.Length;
      if (length1 >= 1)
      {
        int length2 = length1 - 1;
        byte[] b = new byte[length2];
        for (int index = 0; index < length2; ++index)
          b[index] = (byte) chArray[index + 1];
        this.encodeLengthDefiniteForm((long) length2);
        this._out.writeBytes(b);
      }
      else
        this.encodeLengthDefiniteForm((long) length1);
    }

    protected internal virtual void __encodeEnumeratedType(Asn1EnumeratedType typeInstance, int tagNumber, int tagClass)
    {
      if (typeInstance.__getIndex() == -1)
      {
        throw new Asn1Exception(25, string.Concat(new object[4]
        {
          (object) typeInstance.LongValue,
          (object) " is not authorized in type <",
          (object) typeInstance.GetType().FullName,
          (object) ">"
        }));
      }
      else
      {
        this.encodeTag((Asn1Type) typeInstance, tagClass, true, tagNumber);
        long longValue = typeInstance.GetLongValue();
        int num1 = Tools.nbBytesForNumber(longValue);
        this.encodeLengthDefiniteForm((long) num1);
        for (int index = 1; index <= num1; ++index)
        {
          int num2 = num1 - index << 3;
          this._out.writeByte((byte) ((ulong) (longValue >> num2) & (ulong) byte.MaxValue));
        }
      }
    }

    protected internal virtual void __encodeGeneralizedTimeType(Asn1GeneralizedTimeType typeInstance, int tagNumber, int tagClass)
    {
      this.encodeTag((Asn1Type) typeInstance, tagClass, true, tagNumber);
      byte[] arrayFrom8BitsChars = Tools.getByteArrayFrom8BitsChars(typeInstance.GetStringValue(this._isCanonicalizingTimeValues));
      this.encodeLengthDefiniteForm(arrayFrom8BitsChars == null ? 0L : (long) arrayFrom8BitsChars.Length);
      if (arrayFrom8BitsChars == null)
        return;
      this._out.writeBytes(arrayFrom8BitsChars);
    }

    protected internal virtual void __encodeIntegerType(Asn1IntegerType typeInstance, int tagNumber, int tagClass)
    {
      this.encodeTag((Asn1Type) typeInstance, tagClass, true, tagNumber);
      if (typeInstance.GetBigIntegerValue() == null)
      {
        long longValue = typeInstance.GetLongValue();
        int num1 = Tools.nbBytesForNumber(longValue);
        this.encodeLengthDefiniteForm((long) num1);
        for (int index = 1; index <= num1; ++index)
        {
          int num2 = num1 - index << 3;
          this._out.writeByte((byte) ((ulong) (longValue >> num2) & (ulong) byte.MaxValue));
        }
      }
      else
      {
        byte[] bytes = typeInstance.GetBigIntegerValue().GetBytes();
        this.encodeLengthDefiniteForm((long) bytes.Length);
        this._out.writeBytes(bytes, bytes.Length);
      }
    }

    protected internal virtual void __encodeNullType(Asn1NullType typeInstance, int tagNumber, int tagClass)
    {
      this.encodeTag((Asn1Type) typeInstance, tagClass, true, tagNumber);
      this.encodeLengthOctetShortForm(0L);
    }

    protected internal virtual void __encodeObjectIdentifierType(Asn1ObjectIdentifierType typeInstance, int tagNumber, int tagClass)
    {
      this.encodeTag((Asn1Type) typeInstance, tagClass, true, tagNumber);
      byte[] byteArrayValue = typeInstance.GetByteArrayValue();
      this.encodeLengthDefiniteForm(byteArrayValue == null ? 0L : (long) byteArrayValue.Length);
      if (byteArrayValue == null)
        return;
      this._out.writeBytes(byteArrayValue);
    }

    protected internal virtual void __encodeOctetStringType(Asn1OctetStringType typeInstance, int tagNumber, int tagClass, bool indefiniteLengthForm)
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
      this.encodeTag((Asn1Type) typeInstance, tagClass, true, tagNumber);
      byte[] byteArrayValue = typeInstance.GetByteArrayValue();
      this.encodeLengthDefiniteForm(byteArrayValue == null ? 0L : (long) byteArrayValue.Length);
      if (byteArrayValue == null)
        return;
      this._out.writeBytes(byteArrayValue);
    }

    protected internal virtual void __encodeOpenType(Asn1OpenType typeInstance)
    {
      Asn1Type typeValue = typeInstance.GetTypeValue();
      if (typeValue != null)
      {
        typeValue.__write(this);
      }
      else
      {
        byte[] byteArrayValue = typeInstance.GetByteArrayValue();
        if (byteArrayValue == null || byteArrayValue.Length == 0)
          throw new Asn1Exception(48, " for type <" + typeInstance.GetType().FullName + ">");
        this._out.writeBytes(byteArrayValue);
      }
    }

    protected internal virtual void __encodeRealLengthAndValue(Asn1RealType typeInstance, Stream outputStream)
    {
      this.init(outputStream);
      this.encodeRealLengthAndValue(false, typeInstance);
      this.flush();
    }

    protected internal virtual void __encodeRealType(Asn1RealType typeInstance, int tagNumber, int tagClass)
    {
      this.encodeTag((Asn1Type) typeInstance, tagClass, true, tagNumber);
      this.encodeRealLengthAndValue(true, typeInstance);
    }

    protected internal virtual void __encodeRelativeOIDType(Asn1RelativeOIDType typeInstance, int tagNumber, int tagClass)
    {
      this.encodeTag((Asn1Type) typeInstance, tagClass, true, tagNumber);
      byte[] byteArrayValue = typeInstance.GetByteArrayValue();
      this.encodeLengthDefiniteForm(byteArrayValue == null ? 0L : (long) byteArrayValue.Length);
      if (byteArrayValue == null)
        return;
      this._out.writeBytes(byteArrayValue);
    }

    protected internal virtual void __encodeSequenceOfType(Asn1SequenceOfType typeInstance, int tagNumber, int tagClass, bool indefiniteLengthForm)
    {
      this.encodeTag((Asn1Type) typeInstance, tagClass, false, tagNumber);
      this.encodeConstructedOfLengthAndValue((Asn1ConstructedOfType) typeInstance, false, indefiniteLengthForm | this._isIndefiniteLengthForm);
    }

    protected internal virtual void __encodeSequenceType(Asn1SequenceType typeInstance, int tagNumber, int tagClass, bool indefiniteLengthForm)
    {
      this.encodeTag((Asn1Type) typeInstance, tagClass, false, tagNumber);
      this.encodeConstructedLengthAndValue((Asn1ConstructedType) typeInstance, indefiniteLengthForm | this._isIndefiniteLengthForm);
    }

    protected internal virtual void __encodeSetOfType(Asn1SetOfType typeInstance, int tagNumber, int tagClass, bool indefiniteLengthForm)
    {
      this.encodeTag((Asn1Type) typeInstance, tagClass, false, tagNumber);
      this.encodeConstructedOfLengthAndValue((Asn1ConstructedOfType) typeInstance, this._isSortingSetOf, indefiniteLengthForm | this._isIndefiniteLengthForm);
    }

    protected internal virtual void __encodeSetType(Asn1SetType typeInstance, int tagNumber, int tagClass, bool indefiniteLengthForm)
    {
      this.encodeTag((Asn1Type) typeInstance, tagClass, false, tagNumber);
      this.encodeConstructedLengthAndValue((Asn1ConstructedType) typeInstance, indefiniteLengthForm | this._isIndefiniteLengthForm);
    }

    protected internal virtual void __encodeStringType(Asn1StringType typeInstance, int tagNumber, int tagClass, bool indefiniteLengthForm)
    {
      this.encodeTag((Asn1Type) typeInstance, tagClass, true, tagNumber);
      try
      {
        byte[] byteArrayValue = typeInstance.GetByteArrayValue();
        this.encodeLengthDefiniteForm(byteArrayValue == null ? 0L : (long) byteArrayValue.Length);
        if (byteArrayValue == null)
          return;
        this._out.writeBytes(byteArrayValue);
      }
      catch (IOException ex)
      {
        throw new Asn1Exception(21, "exception " + ex.Message + " for type <" + typeInstance.GetType().FullName + ">");
      }
    }

    public virtual int __encodeTaggedTypeTag(Asn1Type typeInstance, int tagNumber, int tagClass, bool indefiniteLengthForm)
    {
      this.encodeTag(typeInstance, tagClass, false, tagNumber);
      if (this._isIndefiniteLengthForm && indefiniteLengthForm || this._out.size() == -1)
      {
        this._out.writeBytes(Asn1TypeBerWriter.PROLOGUE_OF_INDEFINITE_LENGTH);
        return -1;
      }
      else
      {
        int num = this._out.size();
        this._out.writeByte((byte) 0);
        return num;
      }
    }

    public virtual void __encodeTaggedValueLength(Asn1Type typeInstance, bool indefiniteLengthForm, int mark)
    {
      if (this._isIndefiniteLengthForm && indefiniteLengthForm || this._out.size() == -1)
      {
        this._out.writeBytes(Asn1TypeBerWriter.EPILOGUE_OF_INDEFINITE_LENGTH);
      }
      else
      {
        int num = this._out.size() - mark - 1;
        if (num > 0)
          this.encodeLengthDefiniteForm((long) num, mark);
      }
    }

    protected internal void __encodeTimeOfDayType(Asn1TimeOfDayType typeInstance, int tagNumber, int tagClass)
    {
      this.encodeTag((Asn1Type) typeInstance, tagClass, true, tagNumber);
      char[] chArray = typeInstance.GetStringValue(this._isCanonicalizingTimeValues).ToCharArray();
      int length = chArray.Length;
      byte[] b;
      if (length != 8)
      {
        b = new byte[length];
        for (int index = 0; index < length; ++index)
          b[index] = (byte) chArray[index];
      }
      else
      {
        length = 6;
        b = new byte[6];
        for (int index = 0; index < 2; ++index)
          b[index] = (byte) chArray[index];
        for (int index = 3; index < 5; ++index)
          b[index - 1] = (byte) chArray[index];
        for (int index = 6; index < 8; ++index)
          b[index - 2] = (byte) chArray[index];
      }
      this.encodeLengthDefiniteForm((long) length);
      this._out.writeBytes(b);
    }

    protected internal void __encodeTimeType(Asn1TimeType typeInstance, int tagNumber, int tagClass)
    {
      this.encodeTag((Asn1Type) typeInstance, tagClass, true, tagNumber);
      char[] chArray = typeInstance.GetStringValue(this._isCanonicalizingTimeValues).ToCharArray();
      int length = chArray.Length;
      byte[] b = new byte[length];
      for (int index = 0; index < length; ++index)
        b[index] = (byte) chArray[index];
      this.encodeLengthDefiniteForm((long) length);
      this._out.writeBytes(b);
    }

    protected internal virtual void __encodeUTCTimeType(Asn1UTCTimeType typeInstance, int tagNumber, int tagClass)
    {
      this.encodeTag((Asn1Type) typeInstance, tagClass, true, tagNumber);
      byte[] arrayFrom8BitsChars = Tools.getByteArrayFrom8BitsChars(typeInstance.GetStringValue(this._isCanonicalizingTimeValues));
      this.encodeLengthDefiniteForm(arrayFrom8BitsChars == null ? 0L : (long) arrayFrom8BitsChars.Length);
      if (arrayFrom8BitsChars == null)
        return;
      this._out.writeBytes(arrayFrom8BitsChars);
    }

    private void encodeConstructedLengthAndValue(Asn1ConstructedType typeInstance, bool indefiniteLengthForm)
    {
      int index1 = -1;
      if (this._isIndefiniteLengthForm && indefiniteLengthForm || this._out.size() == -1)
      {
        this._out.writeBytes(Asn1TypeBerWriter.PROLOGUE_OF_INDEFINITE_LENGTH);
      }
      else
      {
        index1 = this._out.size();
        this._out.writeByte((byte) 0);
      }
      int index2 = typeInstance.__getBerFirstComponentIndex();
      for (int length = typeInstance.Length; index2 < length && index2 != -1; index2 = typeInstance.__getBerNextComponentIndex(index2))
      {
        if (typeInstance.GetAsn1Type(index2) == null && typeInstance.IsComponentDefault(index2))
          typeInstance.__setComponentDefaultInstance(index2);
        if (typeInstance.__isComponentDefined(index2) && (this._isEncodingDefaultValues || !typeInstance.HasComponentDefaultValue(index2)))
          typeInstance.__getDefinedComponentTypeInstance(index2).__write(this);
      }
      if (this._isIndefiniteLengthForm && indefiniteLengthForm || this._out.size() == -1)
      {
        this._out.writeBytes(Asn1TypeBerWriter.EPILOGUE_OF_INDEFINITE_LENGTH);
      }
      else
      {
        int num = this._out.size() - index1 - 1;
        if (num > 0)
          this.encodeLengthDefiniteForm((long) num, index1);
      }
    }

    private void encodeConstructedOfLengthAndValue(Asn1ConstructedOfType typeInstance, bool sortingSetOf, bool indefiniteLengthForm)
    {
      int index1 = -1;
      ArrayList arrayList = (ArrayList) null;
      bool flag = false;
      IAsn1OutputStream asn1OutputStream = (IAsn1OutputStream) null;
      if (typeInstance.__getTypePostDecoder() != null)
        flag = !this.isCompatible((object) typeInstance.__getTypePostDecoder());
      if (this._isIndefiniteLengthForm && indefiniteLengthForm || this._out.size() == -1)
      {
        this._out.writeBytes(Asn1TypeBerWriter.PROLOGUE_OF_INDEFINITE_LENGTH);
      }
      else
      {
        index1 = this._out.size();
        this._out.writeByte((byte) 0);
        if (sortingSetOf)
        {
          arrayList = new ArrayList(typeInstance.Count);
          asn1OutputStream = this._out;
        }
      }
      for (int index2 = 0; index2 < typeInstance.Count; ++index2)
      {
        if (sortingSetOf)
          this._out = (IAsn1OutputStream) new ByteArrayAsn1OutputStream();
        object obj = typeInstance.GetAsn1TypeList()[index2];
        if (flag && !(obj is Asn1Type))
          obj = (object) typeInstance.__getElementAt(index2);
        if (obj is Asn1Type)
          ((Asn1Type) obj).__write(this);
        else if (obj != null)
          this._out.writeBytes((byte[]) obj);
        if (sortingSetOf)
          ByteArray.addToSortedArray((IList) arrayList, this._out.toByteArray());
      }
      if (this._isIndefiniteLengthForm && indefiniteLengthForm || this._out.size() == -1)
        this._out.writeBytes(Asn1TypeBerWriter.EPILOGUE_OF_INDEFINITE_LENGTH);
      else if (sortingSetOf)
      {
        this._out = asn1OutputStream;
        int totalLength = ByteArray.getTotalLength((IList) arrayList);
        if (totalLength > 0)
          this.encodeLengthDefiniteForm((long) totalLength, index1);
        this._out.writeBytes(ByteArray.getTotalByteArray((IList) arrayList));
      }
      else
      {
        int num = this._out.size() - index1 - 1;
        if (num > 0)
          this.encodeLengthDefiniteForm((long) num, index1);
      }
    }

    protected override void encodeImpl(Asn1Type type, Stream outputStream)
    {
      this.init(outputStream);
      type.__write(this);
      this.flush();
    }

    protected void encodeIntegerOn7Bits(long l)
    {
      if (l <= (long) sbyte.MaxValue)
        this._out.writeByte((byte) l);
      else
        this._out.writeBytes(Tools.get7BitsArrayFromLong(l));
    }

    protected void encodeLengthDefiniteForm(long length)
    {
      this.encodeLengthDefiniteForm(length, -1);
    }

    protected void encodeLengthDefiniteForm(long length, int index)
    {
      if (length <= (long) sbyte.MaxValue)
        this.encodeLengthOctetShortForm(length, index);
      else
        this.encodeLengthOctetLongForm(length, index);
    }

    protected void encodeLengthOctetLongForm(long length)
    {
      this.encodeLengthOctetLongForm(length, -1);
    }

    protected void encodeLengthOctetLongForm(long length, int index)
    {
      int num1 = Tools.nbBytesForPositiveNumber(length);
      this._out.writeByte((byte) (num1 | 128), index);
      if (index < 0)
      {
        for (int index1 = 1; index1 <= num1; ++index1)
        {
          int num2 = num1 - index1 << 3;
          this._out.writeByte((byte) ((ulong) (length >> num2) & (ulong) byte.MaxValue));
        }
      }
      else
      {
        for (int index1 = 1; index1 <= num1; ++index1)
        {
          int num2 = num1 - index1 << 3;
          this._out.insertByteAt(index + index1, (byte) ((ulong) (length >> num2) & (ulong) byte.MaxValue));
        }
      }
    }

    protected void encodeLengthOctetShortForm(long length)
    {
      this._out.writeByte((byte) ((ulong) length & (ulong) sbyte.MaxValue), -1);
    }

    protected void encodeLengthOctetShortForm(long length, int index)
    {
      this._out.writeByte((byte) ((ulong) length & (ulong) sbyte.MaxValue), index);
    }

    private void encodeRealLengthAndValue(bool encodeLength, Asn1RealType typeInstance)
    {
      if (typeInstance.base_().GetIntValue() != 2)
      {
        long mantissa = typeInstance.Mantissa;
        if (mantissa == 0L)
        {
          if (!encodeLength)
            return;
          this.encodeLengthOctetShortForm(0L);
        }
        else
        {
          long exponent = typeInstance.Exponent;
          while (mantissa % 10L == 0L)
          {
            ++exponent;
            mantissa /= 10L;
          }
          byte[] bytes = Encoding.ASCII.GetBytes(exponent == 0L ? mantissa.ToString() + ".E+0" : mantissa.ToString() + ".E" + exponent.ToString());
          if (encodeLength)
            this.encodeLengthDefiniteForm((long) (bytes.Length + 1));
          this._out.writeByte((byte) 3);
          this._out.writeBytes(bytes);
        }
      }
      else
      {
        long number1 = typeInstance.exponent().GetLongValue();
        long number2 = typeInstance.mantissa().GetLongValue();
        bool flag = number2 < 0L;
        if (number2 == 0L && number1 == 0L)
        {
          double doubleValue = typeInstance.GetDoubleValue();
          flag = doubleValue < 0.0;
          if (doubleValue == double.PositiveInfinity)
          {
            if (encodeLength)
              this.encodeLengthOctetShortForm(1L);
            this._out.writeByte((byte) 64);
            return;
          }
          else if (doubleValue == double.NegativeInfinity)
          {
            if (encodeLength)
              this.encodeLengthOctetShortForm(1L);
            this._out.writeByte((byte) 65);
            return;
          }
          else if (doubleValue == 0.0)
          {
            if ("-0".Equals(typeInstance.StringValue))
            {
              if (encodeLength)
                this.encodeLengthOctetShortForm(1L);
              this._out.writeByte((byte) 67);
              return;
            }
            else
            {
              if (!encodeLength)
                return;
              this.encodeLengthOctetShortForm(0L);
              return;
            }
          }
          else if (doubleValue.Equals(double.PositiveInfinity))
          {
            if (encodeLength)
              this.encodeLengthOctetShortForm(1L);
            this._out.writeByte((byte) 66);
            return;
          }
          else if (doubleValue % 2.0 == 0.0)
          {
            number2 = (long) doubleValue;
            while (number2 % 2L == 0L)
            {
              ++number1;
              number2 >>= 1;
            }
          }
          else
          {
            long num1 = BitConverter.ToInt64(BitConverter.GetBytes(doubleValue), 0);
            long num2 = Tools.URShift(num1 & long.MaxValue, 52);
            long num3 = num1 & 4503599627370495L;
            if (num2 == 0L)
            {
              number1 = -1074L;
              number2 = num3;
            }
            else
            {
              number1 = num2 - 1023L - 52L;
              number2 = num3 + 4503599627370496L;
            }
          }
        }
        else if (flag)
          number2 = -number2;
        while (number2 % 2L == 0L)
        {
          ++number1;
          number2 >>= 1;
        }
        int num4 = Tools.nbBytesForNumber(number1);
        int num5 = Tools.nbBytesForPositiveNumber(number2);
        int num6 = 1 + num5;
        byte b = flag ? (byte) 192 : (byte) sbyte.MinValue;
        int num7;
        switch (num4)
        {
          case 1:
            num7 = num6 + 1;
            break;
          case 2:
            b |= (byte) 1;
            num7 = num6 + 2;
            break;
          case 3:
            b |= (byte) 2;
            num7 = num6 + 3;
            break;
          default:
            b |= (byte) 3;
            num7 = num6 + (num4 + 1);
            break;
        }
        if (encodeLength)
          this.encodeLengthDefiniteForm((long) num7);
        this._out.writeByte(b);
        if (num4 > 3)
          this.encodeLengthDefiniteForm((long) num4);
        for (int index = 1; index <= num4; ++index)
        {
          int num1 = num4 - index << 3;
          this._out.writeByte((byte) ((ulong) (number1 >> num1) & (ulong) byte.MaxValue));
        }
        for (int index = 1; index <= num5; ++index)
        {
          int num1 = num5 - index << 3;
          this._out.writeByte((byte) ((ulong) (number2 >> num1) & (ulong) byte.MaxValue));
        }
      }
    }

    protected void encodeTag(Asn1Type typeInstance, int tagClass, bool isPrimitive, int tagNumber)
    {
      if (tagNumber < 0)
        throw new Asn1Exception(1, "tag number = " + (object) tagNumber + " for type <" + typeInstance.GetType().FullName + ">");
      else if (tagNumber <= 30)
        this.encodeTagSmallNumber(tagClass, isPrimitive, tagNumber);
      else
        this.encodeTagHighNumber(tagClass, isPrimitive, tagNumber);
    }

    protected void encodeTagHighNumber(int tagClass, bool isPrimitive, int tagNumber)
    {
      this.encodeTagSmallNumber(tagClass, isPrimitive, 31);
      this.encodeIntegerOn7Bits((long) tagNumber);
    }

    protected void encodeTagSmallNumber(int tagClass, bool isPrimitive, int tagNumber)
    {
      this._out.writeByte(tagClass != 1 ? (tagClass != 4 ? (tagClass != 2 ? (!isPrimitive ? (byte) (224 | tagNumber & 31) : (byte) (192 | tagNumber & 31)) : (!isPrimitive ? (byte) (96 | tagNumber & 31) : (byte) (64 | tagNumber & 31))) : (!isPrimitive ? (byte) (160 | tagNumber & 31) : (byte) (128 | tagNumber & 31))) : (!isPrimitive ? (byte) (32 | tagNumber & 31) : (byte) (tagNumber & 31)));
    }

    protected override void flush()
    {
      if (!this._isIndefiniteLengthForm)
        this._out.writeTo(this._mainStream);
      this._mainStream.flush();
    }

    public override string GetProperty(string key)
    {
      if (key.Equals("validating"))
        return this._isValidating.ToString();
      if (key.Equals("internalErrorLogDir"))
        return this._internalErrorLogDir;
      if (key.Equals("indefiniteLengthForm"))
        return this._isIndefiniteLengthForm.ToString();
      if (key.Equals("sortingSetOf"))
        return this._isSortingSetOf.ToString();
      if (key.Equals("canonicalizingTimeValues"))
        return this._isCanonicalizingTimeValues.ToString();
      if (key.Equals("encodingDefaultValues"))
        return this._isEncodingDefaultValues.ToString();
      if (key.Equals("removingTrailingZeroesInBitStringWithNamedBits"))
        return this._isRemovingTrailingZeroesInBitStringWithNamedBits.ToString();
      else
        return (string) null;
    }

    public virtual Asn1TypeWriter getWriter()
    {
      Asn1TypeBerWriter asn1TypeBerWriter = new Asn1TypeBerWriter();
      asn1TypeBerWriter._isValidating = this._isValidating;
      asn1TypeBerWriter._isIndefiniteLengthForm = this._isIndefiniteLengthForm;
      asn1TypeBerWriter._isSortingSetOf = this._isSortingSetOf;
      asn1TypeBerWriter._isCanonicalizingTimeValues = this._isCanonicalizingTimeValues;
      asn1TypeBerWriter._isEncodingDefaultValues = this._isEncodingDefaultValues;
      asn1TypeBerWriter._isRemovingTrailingZeroesInBitStringWithNamedBits = this._isRemovingTrailingZeroesInBitStringWithNamedBits;
      return (Asn1TypeWriter) asn1TypeBerWriter;
    }

    protected override void init(Stream output)
    {
      this._mainStream = (IAsn1OutputStream) new Asn1OutputStream(output);
      if (!this._isIndefiniteLengthForm)
        this._out = (IAsn1OutputStream) new ByteArrayAsn1OutputStream();
      else
        this._out = this._mainStream;
    }

    protected bool isCompatible(object reader)
    {
      return reader != null && reader is Asn1TypeBerReader;
    }

    protected override void reset()
    {
      this._out = (IAsn1OutputStream) null;
      this._mainStream = (IAsn1OutputStream) null;
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
      else if (key.Equals("indefiniteLengthForm"))
        this._isIndefiniteLengthForm = bool.Parse(property);
      else if (key.Equals("sortingSetOf"))
        this._isSortingSetOf = bool.Parse(property);
      else if (key.Equals("canonicalizingTimeValues"))
        this._isCanonicalizingTimeValues = bool.Parse(property);
      else if (key.Equals("removingTrailingZeroesInBitStringWithNamedBits"))
        this._isRemovingTrailingZeroesInBitStringWithNamedBits = bool.Parse(property);
    }
  }
}
