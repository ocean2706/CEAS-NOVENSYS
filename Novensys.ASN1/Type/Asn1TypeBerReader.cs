// Decompiled with JetBrains decompiler
// Type: Novensys.ASN1.Type.Asn1TypeBerReader
// Assembly: Novensys.eCard.SDK, Version=1.1.56.0, Culture=neutral, PublicKeyToken=null
// MVID: 59F9E456-D3DA-4066-A4A4-692B516A775E
// Assembly location: C:\Program Files (x86)\CNAS\SIUI-SPITAL\Novensys.eCard.SDK.dll

using Novensys.ASN1;
using Novensys.ASN1.IO;
using Novensys.ASN1.Util;
using System;
using System.IO;

namespace Novensys.ASN1.Type
{
  [Serializable]
  public class Asn1TypeBerReader : Asn1TypeReader
  {
    private const int NB_MAX_BYTES_FOR_INT = 4;
    private const int NB_MAX_BYTES_FOR_LONG = 8;
    private Asn1InputStream _in;
    private int _usedBytes;

    protected internal virtual void __decodeBitStringType(Asn1BitStringType typeInstance, bool isExplicit, bool primitive, long len)
    {
      long length = len;
      bool isPrimitive = primitive;
      if (isExplicit)
      {
        isPrimitive = this.__decodeTag((Asn1Type) typeInstance, typeInstance.__getUniversalTagNumber(), 1);
        length = this.__decodeLength((ByteArrayAsn1OutputStream) null);
      }
      BitString bitString = new BitString();
      this.decodeBitStringValue(bitString, isExplicit, 0, isPrimitive, length);
      typeInstance.__setValue(bitString);
      if (!typeInstance.__isContainingType())
        return;
      typeInstance.__setDecoder((IDecoder) this.getReader());
      this._isContentToBeResolved = true;
    }

    protected internal virtual void __decodeBooleanType(Asn1BooleanType typeInstance, bool isExplicit, bool primitive, long len)
    {
      long num = len;
      bool flag = primitive;
      if (isExplicit)
      {
        flag = this.__decodeTag((Asn1Type) typeInstance, typeInstance.__getUniversalTagNumber(), 1);
        num = this.__decodeLength((ByteArrayAsn1OutputStream) null);
      }
      if (!flag)
        throw new Asn1Exception(9, "for BOOLEAN type <" + typeInstance.GetType().FullName + ">");
      if (num != 1L)
        throw new Asn1Exception(12, "for BOOLEAN type <" + typeInstance.GetType().FullName + ">");
      if ((int) this._in.readByte() == 0)
        typeInstance.SetValue(false);
      else
        typeInstance.SetValue(true);
    }

    protected internal virtual void __decodeChoiceType(Asn1ChoiceType typeInstance)
    {
      this._in.mark();
      byte num = this._in.readByte();
      int tagNumber = this.readTagNumber(num, (ByteArrayAsn1OutputStream) null);
      int tagClass = Asn1Tag.GetTagClass(num);
      long len = this.__decodeLength((ByteArrayAsn1OutputStream) null);
      Asn1Type matchingComponent = ((Asn1ConstructedType) typeInstance).__getMatchingComponent(tagNumber, tagClass);
      if (matchingComponent != null)
      {
        matchingComponent.__read(this, false, this.isPrimitiveForm(num), len);
        if (len >= 0L)
          return;
        this.__decodeEndValue((Asn1Type) typeInstance);
      }
      else if (!typeInstance.IsExtensible())
      {
        throw new Asn1Exception(49, "for CHOICE type <" + (object) typeInstance.GetType().FullName + "> (tag = " + (string) (object) tagNumber + ")");
      }
      else
      {
        this._in.reset();
        ByteArrayAsn1OutputStream data = new ByteArrayAsn1OutputStream();
        byte b = this._in.readByte();
        data.writeByte(b);
        this.readTagNumber(b, data);
        this.decodeOpenValue(data, this.__decodeLength(data));
        typeInstance.__addUnknownExtension(data.toByteArray());
      }
    }

    protected internal virtual void __decodeConstructedOfType(Asn1ConstructedOfType typeInstance, bool isExplicit, bool primitive, long len)
    {
      IDecoder reader = (IDecoder) null;
      if (typeInstance.__isPostDecoding())
      {
        reader = (IDecoder) this.getReader();
        typeInstance.__setTypePostDecoder(reader);
      }
      long num1 = len;
      bool flag1 = primitive;
      if (isExplicit)
      {
        flag1 = this.__decodeTag((Asn1Type) typeInstance, typeInstance.__getUniversalTagNumber(), 1);
        num1 = this.__decodeLength((ByteArrayAsn1OutputStream) null);
      }
      if (flag1)
        throw new Asn1Exception(8, "for SEQUENCE OF/SET OF type <" + typeInstance.GetType().FullName + ">");
      if (num1 == 0L)
        return;
      long position = this._in.getPosition();
      bool flag2 = true;
      while (flag2)
      {
        if (reader != null)
        {
          this._in.mark();
          ByteArrayAsn1OutputStream data = new ByteArrayAsn1OutputStream();
          byte b = this._in.readByte();
          data.writeByte(b);
          this.readTagNumber(b, data);
          long length = this.__decodeLength(data);
          if (num1 < 0L && (int) b == 0 && length == 0L)
          {
            if (!isExplicit)
              this._in.reset();
            flag2 = false;
            continue;
          }
          else
          {
            this.decodeOpenValue(data, length);
            typeInstance.GetAsn1TypeList().Add((object) data.toByteArray());
          }
        }
        else
        {
          this._in.mark();
          byte num2 = this._in.readByte();
          int tagNumber = this.readTagNumber(num2, (ByteArrayAsn1OutputStream) null);
          int tagClass = Asn1Tag.GetTagClass(num2);
          long len1 = this.__decodeLength((ByteArrayAsn1OutputStream) null);
          if (num1 < 0L && (int) num2 == 0 && len1 == 0L)
          {
            if (!isExplicit)
              this._in.reset();
            flag2 = false;
            continue;
          }
          else
          {
            Asn1Type matchingElement = typeInstance.__getMatchingElement(tagNumber, tagClass);
            if (matchingElement == null)
            {
              throw new Asn1Exception(49, "for SEQUENCE OF/SET OF type <" + (object) typeInstance.GetType().FullName + "> (tag = " + (string) (object) tagNumber + ")");
            }
            else
            {
              matchingElement.__read(this, false, this.isPrimitiveForm(num2), len1);
              if (len1 < 0L)
                this.__decodeEndValue((Asn1Type) typeInstance);
            }
          }
        }
        if (num1 > 0L)
        {
          if (this._in.getPosition() - position == num1)
            flag2 = false;
          else if (this._in.getPosition() - position > num1)
            throw new Asn1Exception(12, "for SEQUENCE OF/SET OF type <" + typeInstance.GetType().FullName + ">");
        }
      }
    }

    protected internal virtual void __decodeConstructedType(Asn1ConstructedType typeInstance, bool isExplicit, bool primitive, long len)
    {
      long num1 = len;
      bool flag1 = primitive;
      if (isExplicit)
      {
        flag1 = this.__decodeTag((Asn1Type) typeInstance, typeInstance.__getUniversalTagNumber(), 1);
        num1 = this.__decodeLength((ByteArrayAsn1OutputStream) null);
      }
      if (flag1)
        throw new Asn1Exception(8, "for SEQUENCE/SET type <" + typeInstance.GetType().FullName + ">");
      if (num1 == 0L)
        return;
      long position = this._in.getPosition();
      typeInstance.__initBerFirstComponentIndex();
      bool flag2 = true;
      while (flag2)
      {
        this._in.mark();
        byte num2 = this._in.readByte();
        int tagNumber = this.readTagNumber(num2, (ByteArrayAsn1OutputStream) null);
        int tagClass = Asn1Tag.GetTagClass(num2);
        long len1 = this.__decodeLength((ByteArrayAsn1OutputStream) null);
        if (num1 < 0L && (int) num2 == 0 && len1 == 0L)
        {
          if (!isExplicit)
            this._in.reset();
          flag2 = false;
        }
        else
        {
          Asn1Type matchingComponent = typeInstance.__getMatchingComponent(tagNumber, tagClass);
          if (matchingComponent != null)
          {
            matchingComponent.__read(this, false, this.isPrimitiveForm(num2), len1);
            if (len1 < 0L)
              this.__decodeEndValue((Asn1Type) typeInstance);
          }
          else if (!typeInstance.IsExtensible())
          {
            throw new Asn1Exception(49, "for SET/SEQUENCE type <" + (object) typeInstance.GetType().FullName + "> (tag = " + (string) (object) tagNumber + ")");
          }
          else
          {
            this._in.reset();
            ByteArrayAsn1OutputStream data = new ByteArrayAsn1OutputStream();
            byte b = this._in.readByte();
            data.writeByte(b);
            this.readTagNumber(b, data);
            this.decodeOpenValue(data, this.__decodeLength(data));
            typeInstance.__addUnknownExtension(data.toByteArray());
          }
          if (num1 > 0L)
          {
            if (this._in.getPosition() - position == num1)
              flag2 = false;
            else if (this._in.getPosition() - position > num1)
              throw new Asn1Exception(12, "for SET/SEQUENCE type <" + typeInstance.GetType().FullName + ">");
          }
        }
      }
    }

    protected internal virtual void __decodeDateTimeType(Asn1DateTimeType typeInstance, bool isExplicit, bool primitive, long len)
    {
      long num = len;
      bool flag = primitive;
      if (isExplicit)
      {
        flag = this.__decodeTag((Asn1Type) typeInstance, typeInstance.__getUniversalTagNumber(), 1);
        num = this.__decodeLength((ByteArrayAsn1OutputStream) null);
      }
      if (!flag)
        throw new Asn1Exception(9, "for DATE-TIME type <" + typeInstance.GetType().FullName + ">");
      byte[] data = this._in.readBytes((int) num);
      if (num != 14L)
      {
        throw new Asn1Exception(64, "value should be YYYYMMDDHHMMSS instead of '" + ByteArray.ByteArrayToHexString(data, (string) null, -1) + "'H for type <" + typeInstance.GetType().FullName + ">");
      }
      else
      {
        char[] chArray = new char[19];
        for (int index = 0; index < 4; ++index)
          chArray[index] = (char) data[index];
        chArray[4] = '-';
        for (int index = 4; index < 6; ++index)
          chArray[index + 1] = (char) data[index];
        chArray[7] = '-';
        for (int index = 6; index < 8; ++index)
          chArray[index + 2] = (char) data[index];
        chArray[10] = 'T';
        for (int index = 8; index < 10; ++index)
          chArray[index + 3] = (char) data[index];
        chArray[13] = ':';
        for (int index = 10; index < 12; ++index)
          chArray[index + 4] = (char) data[index];
        chArray[16] = ':';
        for (int index = 12; index < 14; ++index)
          chArray[index + 5] = (char) data[index];
        typeInstance.SetValue(new string(chArray));
      }
    }

    protected internal virtual void __decodeDateType(Asn1DateType typeInstance, bool isExplicit, bool primitive, long len)
    {
      long num = len;
      bool flag = primitive;
      if (isExplicit)
      {
        flag = this.__decodeTag((Asn1Type) typeInstance, typeInstance.__getUniversalTagNumber(), 1);
        num = this.__decodeLength((ByteArrayAsn1OutputStream) null);
      }
      if (!flag)
        throw new Asn1Exception(9, "for DATE type <" + typeInstance.GetType().FullName + ">");
      byte[] data = this._in.readBytes((int) num);
      if (num != 8L)
      {
        throw new Asn1Exception(64, "value should be YYYYMMDD instead of '" + ByteArray.ByteArrayToHexString(data, (string) null, -1) + "'H for type <" + typeInstance.GetType().FullName + ">");
      }
      else
      {
        char[] chArray = new char[10];
        for (int index = 0; index < 4; ++index)
          chArray[index] = (char) data[index];
        chArray[4] = '-';
        for (int index = 4; index < 6; ++index)
          chArray[index + 1] = (char) data[index];
        chArray[7] = '-';
        for (int index = 6; index < 8; ++index)
          chArray[index + 2] = (char) data[index];
        typeInstance.SetValue(new string(chArray));
      }
    }

    protected internal virtual void __decodeDurationType(Asn1DurationType typeInstance, bool isExplicit, bool primitive, long len)
    {
      long num = len;
      bool flag = primitive;
      if (isExplicit)
      {
        flag = this.__decodeTag((Asn1Type) typeInstance, typeInstance.__getUniversalTagNumber(), 1);
        num = this.__decodeLength((ByteArrayAsn1OutputStream) null);
      }
      if (!flag)
        throw new Asn1Exception(9, "for DURATION type <" + typeInstance.GetType().FullName + ">");
      byte[] numArray = this._in.readBytes((int) num);
      char[] chArray = new char[numArray.Length + 1];
      chArray[0] = 'P';
      for (int index = 0; index < numArray.Length; ++index)
        chArray[index + 1] = (char) numArray[index];
      typeInstance.SetValue(new string(chArray));
    }

    public virtual void __decodeEndValue(Asn1Type typeInstance)
    {
      int num1 = (int) this._in.readByte();
      long num2 = (long) this._in.readByte();
      if (num1 != 0 || num2 != 0L)
        throw new Asn1Exception(14, "received " + (object) num1 + " " + (string) (object) num2 + " instead of 00 00 in type <" + typeInstance.GetType().FullName + ">");
      else
        typeInstance.SetIndefiniteLengthForm(true);
    }

    protected internal virtual void __decodeEnumeratedType(Asn1EnumeratedType typeInstance, bool isExplicit, bool primitive, long len)
    {
      long length = len;
      bool flag = primitive;
      if (isExplicit)
      {
        flag = this.__decodeTag((Asn1Type) typeInstance, typeInstance.__getUniversalTagNumber(), 1);
        length = this.__decodeLength((ByteArrayAsn1OutputStream) null);
      }
      if (!flag)
        throw new Asn1Exception(9, "for ENUMERATED type <" + typeInstance.GetType().FullName + ">");
      long val = this.decodeIntegerValue(length);
      typeInstance.SetValue(val);
    }

    protected internal virtual void __decodeIntegerType(Asn1IntegerType typeInstance, bool isExplicit, bool primitive, long len)
    {
      long length = len;
      bool flag = primitive;
      if (isExplicit)
      {
        flag = this.__decodeTag((Asn1Type) typeInstance, typeInstance.__getUniversalTagNumber(), 1);
        length = this.__decodeLength((ByteArrayAsn1OutputStream) null);
      }
      if (!flag)
        throw new Asn1Exception(9, "for INTEGER type <" + typeInstance.GetType().FullName + ">");
      if (length <= 8L)
        typeInstance.SetValue(this.decodeIntegerValue(length));
      else
        typeInstance.SetValue(this.decodeBigIntegerValue(length));
    }

    public virtual long __decodeLength(ByteArrayAsn1OutputStream data)
    {
      byte b1 = this._in.readByte();
      if (data != null)
        data.writeByte(b1);
      if (((int) b1 & 128) == 0)
        return (long) ((int) b1 & (int) sbyte.MaxValue);
      if ((int) b1 == 128)
        return -1L;
      int num1 = (int) b1 & (int) sbyte.MaxValue;
      if (num1 > 8)
      {
        throw new Asn1Exception(5, string.Concat(new object[4]
        {
          (object) "number of bytes in length : ",
          (object) num1,
          (object) " > ",
          (object) 8
        }));
      }
      else
      {
        long num2 = 0L;
        for (int index = 0; index < num1; ++index)
        {
          byte b2 = this._in.readByte();
          if (data != null)
            data.writeByte(b2);
          num2 = (num2 << 8) + (long) ((int) b2 & (int) byte.MaxValue);
        }
        if (num2 < 0L)
          throw new Asn1Exception(22, "received : " + (object) num2);
        else
          return num2;
      }
    }

    protected internal virtual void __decodeNullType(Asn1NullType typeInstance, bool isExplicit, bool primitive, long len)
    {
      long num = len;
      bool flag = primitive;
      if (isExplicit)
      {
        flag = this.__decodeTag((Asn1Type) typeInstance, typeInstance.__getUniversalTagNumber(), 1);
        num = this.__decodeLength((ByteArrayAsn1OutputStream) null);
      }
      if (!flag)
        throw new Asn1Exception(9, "for NULL type <" + typeInstance.GetType().FullName + ">");
      if (num != 0L)
        throw new Asn1Exception(12, "for NULL type <" + typeInstance.GetType().FullName + ">");
    }

    protected internal virtual void __decodeObjectIdentifierType(Asn1ObjectIdentifierType typeInstance, bool isExplicit, bool primitive, long len)
    {
      long num = len;
      bool flag = primitive;
      if (isExplicit)
      {
        flag = this.__decodeTag((Asn1Type) typeInstance, typeInstance.__getUniversalTagNumber(), 1);
        num = this.__decodeLength((ByteArrayAsn1OutputStream) null);
      }
      if (!flag)
        throw new Asn1Exception(9, "for OBJECT IDENTIFIER type <" + typeInstance.GetType().FullName + ">");
      if (num == 0L)
        return;
      typeInstance.SetValue(this._in.readBytes((int) num));
    }

    protected internal virtual void __decodeOctetStringType(Asn1OctetStringType typeInstance, bool isExplicit, bool primitive, long len)
    {
      long length = len;
      bool isPrimitive = primitive;
      if (isExplicit)
      {
        isPrimitive = this.__decodeTag((Asn1Type) typeInstance, typeInstance.__getUniversalTagNumber(), 1);
        length = this.__decodeLength((ByteArrayAsn1OutputStream) null);
      }
      if (isPrimitive)
      {
        if (length > 0L)
          typeInstance.SetValue(this._in.readBytes((int) length));
        else
          typeInstance.SetValue(new byte[0]);
      }
      else
      {
        ByteArrayAsn1OutputStream data = new ByteArrayAsn1OutputStream();
        this.decodeOctetStringValue(data, isExplicit, isPrimitive, length);
        typeInstance.SetValue(data.toByteArray());
      }
      if (!typeInstance.__isContainingType())
        return;
      typeInstance.__setDecoder((IDecoder) this.getReader());
      this._isContentToBeResolved = true;
    }

    protected internal virtual void __decodeOpenType(Asn1OpenType typeInstance, bool isExplicit)
    {
      if (typeInstance.__getTagNumber() == -1 && !isExplicit)
        this._in.reset();
      ByteArrayAsn1OutputStream data = new ByteArrayAsn1OutputStream();
      byte b = this._in.readByte();
      data.writeByte(b);
      this.readTagNumber(b, data);
      long length = this.__decodeLength(data);
      this.decodeOpenValue(data, length);
      if (length < 0L && typeInstance.__getTagNumber() == -1 && !isExplicit)
        this._in.reset();
      typeInstance.SetValue(data.toByteArray());
      typeInstance.__setDecoder((IDecoder) this.getReader());
      this._isContentToBeResolved = true;
    }

    protected internal virtual void __decodeRealType(Asn1RealType typeInstance, bool isExplicit, bool primitive, long len)
    {
      long length = len;
      bool flag = primitive;
      if (isExplicit)
      {
        flag = this.__decodeTag((Asn1Type) typeInstance, typeInstance.__getUniversalTagNumber(), 1);
        length = this.__decodeLength((ByteArrayAsn1OutputStream) null);
      }
      if (!flag)
        throw new Asn1Exception(9, "for REAL type <" + typeInstance.GetType().FullName + ">");
      this.decodeRealValue(typeInstance, length);
    }

    protected internal virtual void __decodeRealValue(Stream inputStream, Asn1RealType typeInstance, long length)
    {
        init(inputStream);
      this.decodeRealValue(typeInstance, length);
    }

    protected internal virtual void __decodeRelativeOIDType(Asn1RelativeOIDType typeInstance, bool isExplicit, bool primitive, long len)
    {
      long num = len;
      bool flag = primitive;
      if (isExplicit)
      {
        flag = this.__decodeTag((Asn1Type) typeInstance, typeInstance.__getUniversalTagNumber(), 1);
        num = this.__decodeLength((ByteArrayAsn1OutputStream) null);
      }
      if (!flag)
        throw new Asn1Exception(9, "for RELATIVE-OID type <" + typeInstance.GetType().FullName + ">");
      if (num == 0L)
        return;
      typeInstance.SetValue(this._in.readBytes((int) num));
    }

    protected internal virtual void __decodeStringType(Asn1StringType typeInstance, bool isExplicit, bool primitive, long len)
    {
      long length = len;
      bool isPrimitive = primitive;
      if (isExplicit)
      {
        isPrimitive = this.__decodeTag((Asn1Type) typeInstance, typeInstance.__getUniversalTagNumber(), 1);
        length = this.__decodeLength((ByteArrayAsn1OutputStream) null);
      }
      try
      {
        if (isPrimitive)
        {
          if (length > 0L)
            typeInstance.SetValue(this._in.readBytes((int) length));
          else
            typeInstance.SetValue(new byte[0]);
        }
        else
        {
          ByteArrayAsn1OutputStream data = new ByteArrayAsn1OutputStream();
          this.decodeOctetStringValue(data, isExplicit, isPrimitive, length);
          typeInstance.SetValue(data.toByteArray());
        }
      }
      catch (IOException ex)
      {
        throw new Asn1Exception(21, "exception " + ex.Message + " for type <" + typeInstance.GetType().FullName + ">");
      }
    }

    public virtual bool __decodeTag(Asn1Type type, int tagNumber, int tagClass)
    {
      byte num1 = this._in.readByte();
      int tagClass1 = Asn1Tag.GetTagClass(num1);
      if (tagClass != tagClass1)
      {
        throw new Asn1Exception(10, "received tag class " + Asn1Tag.GetPrintableTagClass(tagClass1) + " instead of " + Asn1Tag.GetPrintableTagClass(tagClass) + " in type <" + type.GetType().FullName + ">");
      }
      else
      {
        int num2 = this.readTagNumber(num1, (ByteArrayAsn1OutputStream) null);
        if (tagNumber == num2)
          return this.isPrimitiveForm(num1);
        throw new Asn1Exception(11, "received tag number " + (object) num2 + " instead of " + (string) (object) tagNumber + " in type <" + type.GetType().FullName + ">");
      }
    }

    protected internal virtual void __decodeTimeOfDayType(Asn1TimeOfDayType typeInstance, bool isExplicit, bool primitive, long len)
    {
      long num = len;
      bool flag = primitive;
      if (isExplicit)
      {
        flag = this.__decodeTag((Asn1Type) typeInstance, typeInstance.__getUniversalTagNumber(), 1);
        num = this.__decodeLength((ByteArrayAsn1OutputStream) null);
      }
      if (!flag)
        throw new Asn1Exception(9, "for TIME-OF-DAY type <" + typeInstance.GetType().FullName + ">");
      byte[] data = this._in.readBytes((int) num);
      if (num != 6L)
      {
        throw new Asn1Exception(64, "value should be HHMMSS instead of '" + ByteArray.ByteArrayToHexString(data, (string) null, -1) + "'H for type <" + typeInstance.GetType().FullName + ">");
      }
      else
      {
        char[] chArray = new char[8];
        for (int index = 0; index < 2; ++index)
          chArray[index] = (char) data[index];
        chArray[2] = ':';
        for (int index = 2; index < 4; ++index)
          chArray[index + 1] = (char) data[index];
        chArray[5] = ':';
        for (int index = 4; index < 6; ++index)
          chArray[index + 2] = (char) data[index];
        typeInstance.SetValue(new string(chArray));
      }
    }

    protected internal virtual void __decodeTimeType(Asn1TimeType typeInstance, bool isExplicit, bool primitive, long len)
    {
      long num = len;
      bool flag = primitive;
      if (isExplicit)
      {
        flag = this.__decodeTag((Asn1Type) typeInstance, typeInstance.__getUniversalTagNumber(), 1);
        num = this.__decodeLength((ByteArrayAsn1OutputStream) null);
      }
      if (!flag)
        throw new Asn1Exception(9, "for TIME type <" + typeInstance.GetType().FullName + ">");
      byte[] numArray = this._in.readBytes((int) num);
      char[] chArray = new char[numArray.Length];
      for (int index = 0; index < numArray.Length; ++index)
        chArray[index] = (char) numArray[index];
      typeInstance.SetValue(new string(chArray));
    }

    protected override void close()
    {
      if (this._in == null)
        return;
      this._usedBytes = (int) this._in.getPosition();
    }

    private BigInteger decodeBigIntegerValue(long length)
    {
      if (length <= (long) int.MaxValue)
        return new BigInteger(this._in.readBytes((int) length));
      throw new Asn1Exception(6, string.Concat(new object[4]
      {
        (object) "decoding length for INTEGER : ",
        (object) length,
        (object) " > ",
        (object) int.MaxValue
      }));
    }

    private int decodeBitStringValue(BitString bs, bool isExplicit, int bitOffset, bool isPrimitive, long length)
    {
      int num1 = 0;
      if (isPrimitive)
        return this.readBitStringValue(bs, bitOffset, length);
      long position = this._in.getPosition();
      bool flag = true;
      while (flag)
      {
        this._in.mark();
        byte num2 = this._in.readByte();
        this.readTagNumber(num2, (ByteArrayAsn1OutputStream) null);
        long length1 = this.__decodeLength((ByteArrayAsn1OutputStream) null);
        bool isPrimitive1 = this.isPrimitiveForm(num2);
        if (length < 0L && (int) num2 == 0 && length1 == 0L)
        {
          if (!isExplicit)
            this._in.reset();
          flag = false;
        }
        else
        {
          num1 = this.decodeBitStringValue(bs, isExplicit, bitOffset, isPrimitive1, length1);
          bitOffset = num1;
          if (length >= 0L && this._in.getPosition() - position == length)
            flag = false;
        }
      }
      return num1;
    }

    protected override void decodeImpl(Stream inputStream, Asn1Type type)
    {
       init(inputStream);
      type.__read(this, true, false, 0L);
      this.close();
    }

    private long decodeIntegerValue(long length)
    {
      long num1 = 0L;
      for (int index = 0; (long) index < length; ++index)
      {
        byte num2 = this._in.readByte();
        if (index == 0 && ((int) num2 & 128) != 0)
          num1 = -1L;
        int num3 = (int) num2 & (int) byte.MaxValue;
        num1 = (num1 << 8) + (long) num3;
      }
      return num1;
    }

    private void decodeOctetStringValue(ByteArrayAsn1OutputStream data, bool isExplicit, bool isPrimitive, long length)
    {
      if (isPrimitive)
      {
        if ((int) length <= 0)
          return;
        data.write(this._in.readBytes((int) length), 0, (int) length);
      }
      else
      {
        long position = this._in.getPosition();
        bool flag = true;
        while (flag)
        {
          this._in.mark();
          byte num = this._in.readByte();
          this.readTagNumber(num, (ByteArrayAsn1OutputStream) null);
          long length1 = this.__decodeLength((ByteArrayAsn1OutputStream) null);
          bool isPrimitive1 = this.isPrimitiveForm(num);
          if (length < 0L && (int) num == 0 && length1 == 0L)
          {
            if (!isExplicit)
              this._in.reset();
            flag = false;
          }
          else
          {
            this.decodeOctetStringValue(data, isExplicit, isPrimitive1, length1);
            if (length >= 0L && this._in.getPosition() - position == length)
              flag = false;
          }
        }
      }
    }

    private void decodeOpenValue(ByteArrayAsn1OutputStream data, long length)
    {
      if (length > 0L)
      {
        if ((int) length <= 0)
          return;
        data.write(this._in.readBytes((int) length), 0, (int) length);
      }
      else
      {
        if (length >= 0L)
          return;
        bool flag = true;
        while (flag)
        {
          this._in.mark();
          byte b = this._in.readByte();
          data.writeByte(b);
          this.readTagNumber(b, data);
          long length1 = this.__decodeLength(data);
          if (length < 0L && (int) b == 0 && length1 == 0L)
            flag = false;
          else
            this.decodeOpenValue(data, length1);
        }
      }
    }

    private long decodePositiveIntegerValue(long length)
    {
      long num1 = 0L;
      for (int index = 0; (long) index < length; ++index)
      {
        int num2 = (int) this._in.readByte() & (int) byte.MaxValue;
        num1 = (num1 << 8) + (long) num2;
      }
      return num1;
    }

    private void decodeRealValue(Asn1RealType typeInstance, long length)
    {
      if (length == 0L)
      {
        typeInstance.SetValue(0.0);
      }
      else
      {
        byte num1 = this._in.readByte();
        if (length == 1L && (int) num1 == 67)
          typeInstance.SetValue("-0");
        else if (length == 1L && (int) num1 == 64)
          typeInstance.SetValue(double.PositiveInfinity);
        else if (length == 1L && (int) num1 == 65)
          typeInstance.SetValue(double.NegativeInfinity);
        else if (length == 1L && (int) num1 == 66)
          typeInstance.SetValue(double.NaN);
        else if (((int) num1 & 128) != 128)
        {
          char[] chArray = new char[(int) length - 1];
          for (int index = 0; (long) index < length - 1L; ++index)
            chArray[index] = (char) this._in.readByte();
          typeInstance.SetValue(new string(chArray));
          ((Asn1IntegerType) typeInstance.base_()).SetValue(10L);
        }
        else
        {
          int num2 = ((int) num1 & 48) >> 4;
          int num3;
          switch (((int) num1 & 48) >> 4)
          {
            case 0:
              num3 = 2;
              break;
            case 1:
              num3 = 8;
              break;
            case 2:
              num3 = 16;
              break;
            default:
              throw new Asn1Exception(33, "base " + (object) num2 + " is not valid (should be 2, 8 or 16) in type <" + typeInstance.GetType().FullName + ">");
          }
          int num4 = ((int) num1 & 12) >> 2;
          int num5 = (int) num1 & 3;
          int num6;
          long val1;
          if (num5 == 3)
          {
            num6 = (int) this._in.readByte() + 1;
            val1 = this.decodeIntegerValue((long) (num6 - 1));
          }
          else
          {
            num6 = num5 + 1;
            val1 = this.decodeIntegerValue((long) num6);
          }
          long num7 = this.decodePositiveIntegerValue(length - 1L - (long) num6);
          long num8 = 1L;
          switch (num3)
          {
            case 2:
              num8 = 1L;
              for (int index = 0; index < num4; ++index)
                num8 <<= 1;
              break;
            case 8:
              num8 = 8L;
              for (int index = 0; index < num4; ++index)
                num8 <<= 1;
              break;
            case 16:
              num8 = 16L;
              for (int index = 0; index < num4; ++index)
                num8 <<= 1;
              break;
          }
          long val2 = num7 * num8;
          if (((int) num1 & 64) == 64)
            val2 = -val2;
          ((Asn1IntegerType) typeInstance.mantissa()).SetValue(val2);
          ((Asn1IntegerType) typeInstance.base_()).SetValue(2L);
          ((Asn1IntegerType) typeInstance.exponent()).SetValue(val1);
        }
      }
    }

    public override string GetProperty(string key)
    {
      if (key.Equals("validating"))
        return this._isValidating.ToString();
      if (key.Equals("resolvingContent"))
        return this._isResolvingContent.ToString();
      if (key.Equals("internalErrorLogDir"))
        return this._internalErrorLogDir;
      else
        return (string) null;
    }

    public virtual Asn1TypeReader getReader()
    {
      Asn1TypeBerReader asn1TypeBerReader = new Asn1TypeBerReader();
      asn1TypeBerReader._isResolvingContent = this._isResolvingContent;
      asn1TypeBerReader._isValidating = this._isValidating;
      return (Asn1TypeReader) asn1TypeBerReader;
    }

    public void init(Asn1InputStream input)
    {
      this._in = input;
      this._usedBytes = 0;
      this._isContentToBeResolved = false;
    }

    protected override void init(Stream input)
    {
      this._in = new Asn1InputStream(input);
      this._usedBytes = 0;
      this._isContentToBeResolved = false;
    }

    protected bool isPrimitiveForm(byte leadingIdentifierOctet)
    {
      return ((int) leadingIdentifierOctet & 32) == 0;
    }

    private int readBitStringValue(BitString bs, int bitOffset, long length)
    {
      if (length == 0L)
        return 0;
      int num1 = (int) this._in.readByte();
      int bitIndex = bitOffset;
      if (length > 1L)
      {
        for (int index1 = 1; (long) index1 < length; ++index1)
        {
          byte num2 = this._in.readByte();
          bool flag = (long) index1 == length - 1L;
          int number = 128;
          int num3 = !flag ? 0 : num1;
          for (int index2 = 7; index2 >= num3; --index2)
          {
            if (((int) num2 & number) != 0)
              bs.setTrue(bitIndex);
            else
              bs.setFalse(bitIndex);
            ++bitIndex;
            number = Tools.URShift(number, 1);
          }
        }
      }
      return bitIndex;
    }

    protected int readTagNumber(byte b, ByteArrayAsn1OutputStream data)
    {
      int num1 = 0;
      int num2 = (int) b & 31;
      if (num2 != 31)
        return num2;
      bool flag = true;
      int num3 = 0;
      while (flag)
      {
        byte b1 = this._in.readByte();
        if (data != null)
          data.writeByte(b1);
        ++num3;
        if (num3 > 4)
        {
          throw new Asn1Exception(4, string.Concat(new object[4]
          {
            (object) "tag length : ",
            (object) num3,
            (object) " > ",
            (object) 4
          }));
        }
        else
        {
          if (((int) b1 & 128) == 0)
            flag = false;
          num1 = (num1 << 7) + ((int) b1 & (int) sbyte.MaxValue);
        }
      }
      return num1;
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
    }

    public override int UsedBytes()
    {
      return this._usedBytes;
    }
  }
}
