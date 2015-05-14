// Decompiled with JetBrains decompiler
// Type: Novensys.ASN1.Type.Asn1BitStringType
// Assembly: Novensys.eCard.SDK, Version=1.1.56.0, Culture=neutral, PublicKeyToken=null
// MVID: 59F9E456-D3DA-4066-A4A4-692B516A775E
// Assembly location: C:\Program Files (x86)\CNAS\SIUI-SPITAL\Novensys.eCard.SDK.dll

using Novensys.ASN1;
using Novensys.ASN1.Util;
using System;
using System.Collections;
using System.ComponentModel;
using System.Text;

namespace Novensys.ASN1.Type
{
  [Serializable]
  public class Asn1BitStringType : Asn1Type
  {
    private BitString __value;

    public string BinaryStringValue
    {
      get
      {
        return this.GetBinaryStringValue();
      }
      set
      {
        this.SetBinaryStringValue(value);
      }
    }

    public BitArray BitArrayValue
    {
      get
      {
        return this.GetBitArrayValue();
      }
      set
      {
        this.SetValue(value);
      }
    }

    public byte[] ByteArrayValue
    {
      get
      {
        return this.GetByteArrayValue();
      }
      set
      {
        this.SetValue(value);
      }
    }

    public int Count
    {
      get
      {
        if (this.__value == null)
          return 0;
        else
          return this.__value.length();
      }
    }

    public string HexStringValue
    {
      get
      {
        return this.GetHexStringValue();
      }
      set
      {
        this.SetHexStringValue(value);
      }
    }

    public bool this[int index]
    {
      get
      {
        if (index < 0)
          throw new ArgumentOutOfRangeException();
        if (this.__value == null)
          return false;
        else
          return this.__value.get(index);
      }
      set
      {
        if (index < 0)
          throw new ArgumentOutOfRangeException();
        if (this.__value == null)
          this.__value = new BitString();
        if (value)
          this.__value.setTrue(index);
        else
          this.__value.setFalse(index);
      }
    }

    public override string PrintableValue
    {
      get
      {
        StringBuilder stringBuilder = (StringBuilder) null;
        int[] namedBitSet = this.__getNamedBitSet();
        if (namedBitSet != null)
        {
          stringBuilder = new StringBuilder(64);
          stringBuilder.Append('{');
          if (this.__value != null)
          {
            for (int bitIndex = 0; bitIndex < this.Count; ++bitIndex)
            {
              string str = (string) null;
              if (this.__value.get(bitIndex))
              {
                for (int index = 0; index < namedBitSet.Length; ++index)
                {
                  if (bitIndex == namedBitSet[index])
                  {
                    str = this.__getIdentifierSet()[index];
                    break;
                  }
                }
                if (str == null)
                {
                  stringBuilder = (StringBuilder) null;
                  break;
                }
                else
                {
                  if (stringBuilder.Length != 1)
                    stringBuilder.Append(',');
                  stringBuilder.Append(str);
                }
              }
            }
          }
          if (stringBuilder != null)
            stringBuilder.Append('}');
        }
        if (stringBuilder == null)
        {
          stringBuilder = new StringBuilder(this.Count);
          stringBuilder.Append('\'');
          if (this.__value != null)
          {
            for (int bitIndex = 0; bitIndex < this.Count; ++bitIndex)
            {
              if (bitIndex != 0 && bitIndex % 8 == 0)
                stringBuilder.Append(' ');
              stringBuilder.Append(this.__value.get(bitIndex) ? "1" : "0");
              if (bitIndex > 128)
              {
                stringBuilder.Append(" truncation...");
                break;
              }
            }
          }
          stringBuilder.Append("'B");
        }
        return ((object) stringBuilder).ToString();
      }
    }

    public override string TypeName
    {
      get
      {
        return "BIT STRING";
      }
    }

    public Asn1BitStringType()
    {
      this.ResetType();
    }

    public Asn1BitStringType(string asn1BitString)
    {
      this.SetBinaryStringValue(asn1BitString);
    }

    public Asn1BitStringType(byte[] data)
    {
      this.SetValue(data);
    }

    public Asn1BitStringType(BitArray data)
    {
      this.SetValue(data);
    }

    public Asn1BitStringType(byte[] data, int length)
    {
      this.SetValue(data, length);
    }

    public Asn1BitStringType(BitArray data, int length)
    {
      this.SetValue(data, length);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal BitString __getBitString()
    {
      return this.__value;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal virtual IDecoder __getDecoder()
    {
      return (IDecoder) null;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal virtual IEncoder __getEncoder()
    {
      return (IEncoder) null;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal IList __getIdentifierList(string[] idSet)
    {
      int[] namedBitSet = this.__getNamedBitSet();
      if (namedBitSet == null)
        return (IList) null;
      IList list = (IList) new ArrayList();
      if (this.__value != null)
      {
        for (int bitIndex = 0; bitIndex < this.Count; ++bitIndex)
        {
          string str = (string) null;
          if (this.__value.get(bitIndex))
          {
            for (int index = 0; index < namedBitSet.Length; ++index)
            {
              if (bitIndex == namedBitSet[index])
              {
                str = idSet[index];
                break;
              }
            }
            if (str == null)
              return (IList) null;
            list.Add((object) str);
          }
        }
      }
      return list;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal virtual string[] __getIdentifierSet()
    {
      return (string[]) null;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal virtual int[] __getNamedBitSet()
    {
      return (int[]) null;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal override int __getUniversalTagNumber()
    {
      return 3;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal virtual string[] __getXerIdentifierSet()
    {
      return (string[]) null;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal override string __getXerTag()
    {
      return "BIT_STRING";
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal virtual bool __isContainingType()
    {
      return false;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal bool __isValueOutOfRoot()
    {
      return this.GetLowerSize() != 0L && (long) this.__value.length() < this.GetLowerSize() || this.GetUpperSize() != -1L && this.GetUpperSize() < (long) this.__value.length();
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal virtual bool __isWithNamedBitList()
    {
      return false;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal virtual bool __isXerEmptyElementEnabled()
    {
      return false;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal virtual bool __isXerText()
    {
      return false;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal override void __read(Asn1TypePerReader reader)
    {
      reader.__decodeBitStringType(this);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal override void __read(Asn1TypeXerReader reader)
    {
      reader.__decodeBitStringType(this);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal override void __read(Asn1TypeBerReader reader, bool isExplicit, bool primitive, long len)
    {
      reader.__decodeBitStringType(this, isExplicit, primitive, len);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal override void __readValue(Asn1TypeXerReader reader, string text)
    {
      reader.__decodeBitStringValue(this, text);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal virtual void __setDecoder(IDecoder reader)
    {
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal virtual void __setType(Asn1Type type)
    {
    }

    protected internal override void __setTypeValue(Asn1Type typeInstance)
    {
      if (typeInstance == null)
      {
        this.ResetType();
      }
      else
      {
        if (!(typeInstance is Asn1BitStringType))
          return;
        BitString bitString = ((Asn1BitStringType) typeInstance).__getBitString();
        if (bitString == null)
        {
          this.__setValue((BitString) null);
        }
        else
        {
          byte[] bytes = bitString.getBytes();
          byte[] data = new byte[bytes.Length];
          Array.Copy((Array) bytes, 0, (Array) data, 0, bytes.Length);
          this.__setValue(new BitString(data, bitString.length()));
        }
      }
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal void __setValue(BitString aBitString)
    {
      if (aBitString == null)
        this.__value = (BitString) null;
      else if (this.__isWithNamedBitList())
        this.__value = new BitString(aBitString.getBytes(), aBitString.trimmedLength());
      else
        this.__value = aBitString;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal virtual void __setValue(string identifier, string[] idSet)
    {
      if (identifier == null || idSet == null)
        throw new ArgumentNullException("identifier and isSet must not be null.");
      for (int index = 0; index < idSet.Length; ++index)
      {
        if (identifier.Equals(idSet[index]))
        {
          this.SetValue(this.__getNamedBitSet()[index]);
          return;
        }
      }
      throw new Asn1Exception(58, "invalid identifier (" + identifier + ") in type <" + this.GetType().FullName + ">");
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal override void __write(Asn1TypeBerWriter writer)
    {
      writer.__encodeBitStringType(this, this.__getUniversalTagNumber(), 1, this.IsIndefiniteLengthForm());
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal override void __write(Asn1TypePerWriter writer)
    {
      writer.__encodeBitStringType(this);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal override void __write(Asn1TypeXerWriter writer)
    {
      writer.__encodeBitStringType(this);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal override void __write(Asn1TypeBerWriter writer, int tagNumber, int tagClass)
    {
      writer.__encodeBitStringType(this, tagNumber, tagClass, this.IsIndefiniteLengthForm());
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal override string __writeValue(Asn1TypeXerWriter writer)
    {
      return writer.__encodeBitStringValue(this);
    }

    public override object Clone()
    {
      Asn1BitStringType asn1BitStringType = (Asn1BitStringType) this.MemberwiseClone();
      if (this.__value != null)
        asn1BitStringType.__value = (BitString) this.__value.Clone();
      if (this.GetTypeValue() != null)
        asn1BitStringType.__setType((Asn1Type) this.GetTypeValue().Clone());
      return (object) asn1BitStringType;
    }

    public override bool Equals(object anObject)
    {
      return this == anObject || anObject is Asn1BitStringType && this.HasEqualValue((Asn1BitStringType) anObject);
    }

    public virtual string GetBinaryStringValue()
    {
      if (this.__value == null)
        return (string) null;
      StringBuilder stringBuilder = new StringBuilder(this.Count);
      for (int bitIndex = 0; bitIndex < this.Count; ++bitIndex)
        stringBuilder.Append(this.__value.get(bitIndex) ? "1" : "0");
      return ((object) stringBuilder).ToString();
    }

    public virtual BitArray GetBitArrayValue()
    {
      if (this.__value == null)
        return (BitArray) null;
      BitArray bitArray = new BitArray(this.__value.length(), false);
      for (int index = 0; index < this.__value.length(); ++index)
      {
        if (this.__value.get(index))
          bitArray.Set(index, true);
        else
          bitArray.Set(index, false);
      }
      return bitArray;
    }

    public virtual byte[] GetByteArrayValue()
    {
      if (this.__value == null)
        return (byte[]) null;
      else
        return this.__value.getBytes();
    }

    public override int GetHashCode()
    {
      if (this.__value == null)
        return 0;
      else
        return this.__value.GetHashCode();
    }

    public virtual string GetHexStringValue()
    {
      if (this.__value == null)
        return (string) null;
      string str = ByteArray.ByteArrayToHexString(this.__value.getBytes(), "", -1);
      int num = (str.Length << 2) - this.Count;
      if (this.Count > 0 && num > 0)
        return str.Substring(0, str.Length - (num >> 2));
      else
        return str;
    }

    public IList getIdentifierList()
    {
      return this.__getIdentifierList(this.__getIdentifierSet());
    }

    public virtual long GetLowerSize()
    {
      return 0L;
    }

    public override string GetPrintableValue(string indent, string newline)
    {
      if (this.GetTypeValue() != null)
        return "CONTAINING " + this.GetTypeValue().GetPrintableValue(indent, newline);
      else
        return this.PrintableValue;
    }

    public virtual Asn1Type GetTypeValue()
    {
      return (Asn1Type) null;
    }

    public virtual long GetUpperSize()
    {
      return -1L;
    }

    public virtual bool HasEqualValue(Asn1BitStringType that)
    {
      if (that == null)
        return false;
      if (this.__value == null)
        return that.__value == null;
      else
        return this.__value.Equals((object) that.__getBitString());
    }

    public override void ResetType()
    {
      this.__resetCommons();
      this.__value = (BitString) null;
      this.SetIndefiniteLengthForm(false);
      if (this.__getDefaultInstance() == null)
        return;
      this.__setTypeValue(this.__getDefaultInstance());
    }

    public void ResolveContent(Asn1Type type, IDecoder reader)
    {
      if (this.__value == null)
        return;
      if (type == null)
        throw new ArgumentException("type should not be null.");
      IDecoder decoder = this.__getDecoder();
      if (reader == null && decoder == null)
      {
        throw new Asn1Exception(45, "type <" + type.GetType().FullName + "> for BIT SRING type <" + this.GetType().FullName + "> [internal exception : no reader is defined]");
      }
      else
      {
        try
        {
          type.__setEnclosingType((Asn1Type) this);
          if (reader != null)
            reader.Decode(this.__value.getBytes(), type);
          else if (decoder != null)
            decoder.Decode(this.__value.getBytes(), type);
        }
        catch (Asn1ValidationException ex)
        {
          throw ex;
        }
        catch (Exception ex)
        {
          string str1 = ByteArray.ByteArrayToHexString(this.__value.getBytes(), ".", -1);
          string str2 = "internal exception at index '" + (object) (reader != null ? reader.UsedBytes() : decoder.UsedBytes()) + "' in (" + str1 + ") : " + ex.Message;
          throw new Asn1Exception(45, "type <" + type.GetType().FullName + "> for BIT STRING type <" + this.GetType().FullName + "> [" + str2 + "]");
        }
      }
    }

    public void SetBinaryStringValue(string asn1BitString)
    {
      if (asn1BitString == null)
        this.__value = (BitString) null;
      else
        this.__value = new BitString(ByteArray.BinStringToByteArray(asn1BitString), asn1BitString.Length);
    }

    public virtual void SetHexStringValue(string data)
    {
      if (data == null)
      {
        this.__value = (BitString) null;
      }
      else
      {
        int length = data.Length;
        if (length % 2 == 0)
          this.SetValue(ByteArray.HexStringToByteArray(data), length << 2);
        else
          this.SetValue(ByteArray.HexStringToByteArray(data + "0"), length << 2);
      }
    }

    public void SetValue(byte[] data)
    {
      if (data == null)
        this.__value = (BitString) null;
      else
        this.__value = new BitString(data, data.Length << 3);
    }

    public virtual void SetValue(BitArray ba)
    {
      if (ba == null)
      {
        this.__value = (BitString) null;
      }
      else
      {
        if (this.__value == null)
          this.__value = new BitString();
        for (int bitIndex = 0; bitIndex < ba.Count; ++bitIndex)
          this.__value.set(bitIndex, ba[bitIndex]);
      }
    }

    public void SetValue(int index)
    {
      if (index < 0)
        throw new ArgumentOutOfRangeException();
      if (this.__value == null)
        this.__value = new BitString();
      this.__value.setTrue(index);
    }

    public virtual void SetValue(string identifier)
    {
      if (identifier == null)
        throw new ArgumentException("identifier should not be null.");
      if (this.__getIdentifierSet() == null)
        throw new Asn1Exception(58, "no named bits defined in type <" + this.GetType().FullName + ">");
      this.__setValue(identifier, this.__getIdentifierSet());
    }

    public virtual void SetValue(byte[] data, int length)
    {
      if (data == null)
      {
        this.__value = (BitString) null;
      }
      else
      {
        if (length < 0)
          throw new ArgumentException();
        if (length > data.Length << 3)
          throw new ArgumentException("There is not " + (object) length + " bits in data");
        this.__value = new BitString(data, length);
      }
    }

    public virtual void SetValue(BitArray ba, int length)
    {
      if (ba == null)
      {
        this.__value = (BitString) null;
      }
      else
      {
        if (length < 0 || length > ba.Count)
          throw new ArgumentException("parameter length is incorrect");
        if (this.__value == null)
          this.__value = new BitString();
        for (int bitIndex = 0; bitIndex < length; ++bitIndex)
          this.__value.set(bitIndex, ba[bitIndex]);
      }
    }

    public override void Validate()
    {
      if (this.GetTypeValue() != null)
      {
        this.GetTypeValue().Validate();
      }
      else
      {
        if (this.__isPerConstraintExtensible())
          return;
        int num = this.__value != null ? this.Count : 0;
        if ((long) num < this.GetLowerSize() && !this.__isWithNamedBitList())
        {
          throw new Asn1ValidationException(19, (string) (object) num + (object) " < " + (string) (object) this.GetLowerSize() + " in type <" + this.GetType().FullName + ">");
        }
        else
        {
          if (this.GetUpperSize() == -1L || (long) num <= this.GetUpperSize())
            return;
          throw new Asn1ValidationException(20, (string) (object) num + (object) " > " + (string) (object) this.GetUpperSize() + " in type <" + this.GetType().FullName + ">");
        }
      }
    }
  }
}
