// Decompiled with JetBrains decompiler
// Type: Novensys.ASN1.Type.Asn1OctetStringType
// Assembly: Novensys.eCard.SDK, Version=1.1.56.0, Culture=neutral, PublicKeyToken=null
// MVID: 59F9E456-D3DA-4066-A4A4-692B516A775E
// Assembly location: C:\Program Files (x86)\CNAS\SIUI-SPITAL\Novensys.eCard.SDK.dll

using Novensys.ASN1;
using Novensys.ASN1.Util;
using System;
using System.ComponentModel;

namespace Novensys.ASN1.Type
{
  [Serializable]
  public class Asn1OctetStringType : Asn1Type
  {
    private byte[] __value;

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

    public override string PrintableValue
    {
      get
      {
        if (this.__value == null)
          return "''H";
        else
          return "'" + ByteArray.ByteArrayToHexString(this.__value, "", -1, true) + "'H";
      }
    }

    public override string TypeName
    {
      get
      {
        return "OCTET STRING";
      }
    }

    public Asn1OctetStringType()
    {
      this.ResetType();
    }

    public Asn1OctetStringType(byte[] data)
    {
      this.SetValue(data);
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
    protected internal override int __getUniversalTagNumber()
    {
      return 4;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal override string __getXerTag()
    {
      return "OCTET_STRING";
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal virtual bool __isContainingType()
    {
      return false;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal virtual bool __isPerReverseOctets()
    {
      return false;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal virtual bool __isValueOutOfRoot()
    {
      return this.__value != null && (this.GetLowerSize() != 0L && (long) this.__value.Length < this.GetLowerSize() || this.GetUpperSize() != -1L && this.GetUpperSize() < (long) this.__value.Length);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal virtual bool __isXerBase64()
    {
      return false;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal override void __read(Asn1TypePerReader reader)
    {
      reader.__decodeOctetStringType(this);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal override void __read(Asn1TypeXerReader reader)
    {
      reader.__decodeOctetStringType(this);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal override void __read(Asn1TypeBerReader reader, bool isExplicit, bool primitive, long len)
    {
      reader.__decodeOctetStringType(this, isExplicit, primitive, len);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal override void __readValue(Asn1TypeXerReader reader, string text)
    {
      reader.__decodeOctetStringValue(this, text);
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
        if (!(typeInstance is Asn1OctetStringType))
          return;
        this.SetValue(((Asn1OctetStringType) typeInstance).GetByteArrayValue());
      }
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal override void __write(Asn1TypeBerWriter writer)
    {
      writer.__encodeOctetStringType(this, this.__getUniversalTagNumber(), 1, this.IsIndefiniteLengthForm());
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal override void __write(Asn1TypePerWriter writer)
    {
      writer.__encodeOctetStringType(this);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal override void __write(Asn1TypeXerWriter writer)
    {
      writer.__encodeOctetStringType(this);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal override void __write(Asn1TypeBerWriter writer, int tagNumber, int tagClass)
    {
      writer.__encodeOctetStringType(this, tagNumber, tagClass, this.IsIndefiniteLengthForm());
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal override string __writeValue(Asn1TypeXerWriter writer)
    {
      return writer.__encodeOctetStringValue(this);
    }

    public override object Clone()
    {
      Asn1OctetStringType asn1OctetStringType = (Asn1OctetStringType) this.MemberwiseClone();
      if (this.GetTypeValue() != null)
        asn1OctetStringType.__setType((Asn1Type) this.GetTypeValue().Clone());
      return (object) asn1OctetStringType;
    }

    public override bool Equals(object anObject)
    {
      return this == anObject || anObject is Asn1OctetStringType && this.HasEqualValue((Asn1OctetStringType) anObject);
    }

    public virtual string GetBinaryStringValue()
    {
      return ByteArray.ByteArrayToBinaryString(this.__value, "", -1);
    }

    public virtual byte[] GetByteArrayValue()
    {
      return this.__value;
    }

    public override int GetHashCode()
    {
      byte[] byteArrayValue = this.GetByteArrayValue();
      if (byteArrayValue == null)
        return 0;
      int num = 1;
      for (int index = 0; index < byteArrayValue.Length; ++index)
        num = 31 * num + (int) byteArrayValue[index];
      return num;
    }

    public virtual string GetHexStringValue()
    {
      return ByteArray.ByteArrayToHexString(this.__value, "", -1);
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

    public virtual bool HasEqualValue(Asn1OctetStringType that)
    {
      if (that == null)
        return false;
      if (this.__value == null)
        return that.ByteArrayValue == null;
      if (that.ByteArrayValue == null || that.ByteArrayValue.Length != this.__value.Length)
        return false;
      for (int index = 0; index < this.__value.Length; ++index)
      {
        if ((int) that.ByteArrayValue[index] != (int) this.__value[index])
          return false;
      }
      return true;
    }

    public override void ResetType()
    {
      this.__resetCommons();
      this.__value = (byte[]) null;
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
        throw new Asn1Exception(45, "type <" + type.GetType().FullName + "> for OCTET SRING type <" + this.GetType().FullName + "> [internal exception : no reader is defined]");
      }
      else
      {
        try
        {
          type.__setEnclosingType((Asn1Type) this);
          if (reader != null)
            reader.Decode(this.__value, type);
          else if (decoder != null)
            decoder.Decode(this.__value, type);
        }
        catch (Asn1ValidationException ex)
        {
          throw ex;
        }
        catch (Exception ex)
        {
          string str1 = ByteArray.ByteArrayToHexString(this.__value, ".", -1);
          string str2 = "internal exception at index '" + (object) (reader != null ? reader.UsedBytes() : decoder.UsedBytes()) + "' in (" + str1 + ") : " + ex.Message;
          throw new Asn1Exception(45, "type <" + type.GetType().FullName + "> for OCTET STRING type <" + this.GetType().FullName + "> [" + str2 + "]");
        }
      }
    }

    public virtual void SetBinaryStringValue(string data)
    {
      if (data == null)
        this.__value = (byte[]) null;
      else
        this.SetValue(ByteArray.BinStringToByteArray(data));
    }

    public virtual void SetHexStringValue(string data)
    {
      if (data == null)
        this.__value = (byte[]) null;
      else
        this.SetValue(ByteArray.HexStringToByteArray(data));
    }

    public virtual void SetValue(byte[] data)
    {
      if (data == null)
      {
        this.__value = (byte[]) null;
      }
      else
      {
        this.__value = new byte[data.Length];
        data.CopyTo((Array) this.__value, 0);
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
        int num = this.__value != null ? this.__value.Length : 0;
        if ((long) num < this.GetLowerSize())
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
