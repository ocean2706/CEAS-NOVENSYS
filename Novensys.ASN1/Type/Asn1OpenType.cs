// Decompiled with JetBrains decompiler
// Type: Novensys.ASN1.Type.Asn1OpenType
// Assembly: Novensys.eCard.SDK, Version=1.1.56.0, Culture=neutral, PublicKeyToken=null
// MVID: 59F9E456-D3DA-4066-A4A4-692B516A775E
// Assembly location: C:\Program Files (x86)\CNAS\SIUI-SPITAL\Novensys.eCard.SDK.dll

using Novensys.ASN1;
using Novensys.ASN1.Util;
using System;
using System.ComponentModel;
using System.Text;

namespace Novensys.ASN1.Type
{
  [Serializable]
  public class Asn1OpenType : Asn1Type
  {
    private IDecoder __reader;
    private Asn1Type __type;
    private byte[] __value;

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
        return "OPEN TYPE";
      }
    }

    public Asn1Type TypeValue
    {
      get
      {
        return this.GetTypeValue();
      }
      set
      {
        this.SetValue(value);
      }
    }

    public Asn1OpenType()
    {
      this.__reader = (IDecoder) null;
      this.ResetType();
    }

    public Asn1OpenType(byte[] data)
    {
      this.__reader = (IDecoder) null;
      this.SetValue(data);
    }

    public Asn1OpenType(Asn1Type typeInstance)
    {
      this.__reader = (IDecoder) null;
      this.SetValue(typeInstance);
    }

    protected internal override Asn1Type __getMatchingInstance(int tagNumber, int tagClass)
    {
      return (Asn1Type) this;
    }

    protected internal override Asn1Type __getMatchingInstance(string xerTag, string xerNamespace)
    {
      return (Asn1Type) this;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal override string __getXerTag()
    {
      return "OPEN_TYPE";
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal virtual bool __isXerBase64()
    {
      return false;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal override void __read(Asn1TypePerReader reader)
    {
      reader.__decodeOpenType(this);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal override void __read(Asn1TypeXerReader reader)
    {
      reader.__decodeOpenType(this);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal override void __read(Asn1TypeBerReader reader, bool isExplicit, bool primitive, long len)
    {
      reader.__decodeOpenType(this, isExplicit);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal override void __readValue(Asn1TypeXerReader reader, string text)
    {
      reader.__decodeOpenValue(this, text);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal virtual void __setDecoder(IDecoder reader)
    {
      this.__reader = reader;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal virtual void __setType(Asn1Type type)
    {
      this.__type = type;
    }

    protected internal override void __setTypeValue(Asn1Type typeInstance)
    {
      if (typeInstance == null)
      {
        this.ResetType();
      }
      else
      {
        if (!(typeInstance is Asn1OpenType))
          return;
        Asn1OpenType asn1OpenType = (Asn1OpenType) typeInstance;
        if (asn1OpenType.GetTypeValue() != null)
          this.SetValue(asn1OpenType.GetTypeValue());
        else
          this.SetValue(asn1OpenType.GetByteArrayValue());
      }
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal override void __write(Asn1TypeBerWriter writer)
    {
      writer.__encodeOpenType(this);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal override void __write(Asn1TypePerWriter writer)
    {
      writer.__encodeOpenType(this);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal override void __write(Asn1TypeXerWriter writer)
    {
      writer.__encodeOpenType(this);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal override void __write(Asn1TypeBerWriter writer, int tagNumber, int tagClass)
    {
      writer.__encodeOpenType(this);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal override string __writeValue(Asn1TypeXerWriter writer)
    {
      return writer.__encodeOpenValue(this);
    }

    public override object Clone()
    {
      Asn1OpenType asn1OpenType = (Asn1OpenType) this.MemberwiseClone();
      if (this.__type != null)
        asn1OpenType.__type = (Asn1Type) this.__type.Clone();
      return (object) asn1OpenType;
    }

    public override bool Equals(object anObject)
    {
      if (anObject == null)
        return false;
      if (this == anObject)
        return true;
      if (!(anObject is Asn1OpenType))
        return false;
      Asn1OpenType asn1OpenType1 = (Asn1OpenType) anObject;
      if (asn1OpenType1.TypeValue == null && this.TypeValue == null)
      {
        if (this.ByteArrayValue == null)
          return false;
        else
          return this.ByteArrayValue.Equals((object) asn1OpenType1.ByteArrayValue);
      }
      else
      {
        if (asn1OpenType1.TypeValue != null && this.TypeValue != null)
          return asn1OpenType1.TypeValue.Equals((object) this.TypeValue);
        Asn1Type typeValue;
        Asn1OpenType asn1OpenType2;
        if (this.TypeValue != null)
        {
          typeValue = this.TypeValue;
          asn1OpenType2 = asn1OpenType1;
        }
        else
        {
          typeValue = asn1OpenType1.TypeValue;
          asn1OpenType2 = this;
        }
        return asn1OpenType2.HasEqualValue(typeValue);
      }
    }

    public virtual byte[] GetByteArrayValue()
    {
      return this.__value;
    }

    public override int GetHashCode()
    {
      if (this.__type != null)
        return this.__type.GetHashCode();
      byte[] byteArrayValue = this.GetByteArrayValue();
      if (byteArrayValue == null)
        return 0;
      int num = 1;
      for (int index = 0; index < byteArrayValue.Length; ++index)
        num = 31 * num + (int) byteArrayValue[index];
      return num;
    }

    public override string GetPrintableValue(string indent, string newline)
    {
      StringBuilder stringBuilder = new StringBuilder();
      Asn1Type typeValue = this.GetTypeValue();
      if (typeValue != null)
      {
        stringBuilder.Append(typeValue.TypeName);
        stringBuilder.Append(" : ");
        stringBuilder.Append(typeValue.GetPrintableValue(indent, newline));
        return ((object) stringBuilder).ToString();
      }
      else if (this.__value == null)
        return "''H";
      else
        return "'" + ByteArray.ByteArrayToHexString(this.__value, "", -1, true) + "'H";
    }

    public virtual Asn1Type GetTypeValue()
    {
      return this.__type;
    }

    public bool HasEqualValue(Asn1Type typeInstance)
    {
      if (typeInstance == null)
        return false;
      try
      {
        this.ResolveContent(typeInstance.GetType());
        return this.TypeValue.Equals((object) typeInstance);
      }
      catch (Asn1Exception ex)
      {
        return false;
      }
    }

    public override void ResetType()
    {
      this.__resetCommons();
      this.__value = (byte[]) null;
      this.__type = (Asn1Type) null;
      this.__reader = (IDecoder) null;
      if (this.__getDefaultInstance() == null)
        return;
      this.__setTypeValue(this.__getDefaultInstance());
    }

    public virtual void ResolveContent(Asn1Type typeInstance)
    {
      if (this.__value == null || this.__type != null)
        return;
      this.__setType(typeInstance);
      try
      {
        this.__reader.Decode(this.__value, typeInstance);
      }
      catch (Asn1ValidationException ex)
      {
        throw ex;
      }
      catch (Exception ex)
      {
        string str = "internal exception at index '" + (object) this.__reader.UsedBytes() + "' in (" + ByteArray.ByteArrayToHexString(this.__value, ".", -1) + ") : " + ex.Message;
        throw new Asn1Exception(16, "class <" + typeInstance.GetType().FullName + "> for open type <" + this.GetType().FullName + "> [" + str + "]");
      }
    }

    public virtual void ResolveContent(string className)
    {
      System.Type type;
      try
      {
        type = System.Type.GetType(className, true);
      }
      catch (TypeLoadException ex)
      {
        throw new Asn1Exception(17, "for open type <" + this.GetType().FullName + "> : " + ex.Message);
      }
      this.ResolveContent(type);
    }

    public virtual void ResolveContent(System.Type cla)
    {
      if (cla == null || this.__type != null)
        return;
      Asn1Type typeInstance;
      try
      {
        typeInstance = (Asn1Type) Activator.CreateInstance(cla);
      }
      catch (Exception ex)
      {
        throw new Asn1Exception(17, (string) (object) cla + (object) " for open type <" + this.GetType().FullName + "> : " + ex.Message);
      }
      this.ResolveContent(typeInstance);
    }

    public virtual void SetValue(Asn1Type typeInstance)
    {
      this.__type = typeInstance;
      this.__value = (byte[]) null;
    }

    public virtual void SetValue(byte[] data)
    {
      if (data != null)
      {
        this.__value = new byte[data.Length];
        data.CopyTo((Array) this.__value, 0);
      }
      else
        this.__value = (byte[]) null;
      this.__type = (Asn1Type) null;
    }

    public override void Validate()
    {
      if (this.__value != null)
      {
        if (this.__value.Length == 0)
          throw new Asn1ValidationException(48, " for type <" + this.GetType().FullName + ">");
      }
      else if (this.__type == null)
        throw new Asn1ValidationException(48, " for type <" + this.GetType().FullName + ">");
      if (this.__type == null)
        return;
      this.__type.Validate();
    }
  }
}
