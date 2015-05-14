// Decompiled with JetBrains decompiler
// Type: Novensys.ASN1.Type.Asn1BooleanType
// Assembly: Novensys.eCard.SDK, Version=1.1.56.0, Culture=neutral, PublicKeyToken=null
// MVID: 59F9E456-D3DA-4066-A4A4-692B516A775E
// Assembly location: C:\Program Files (x86)\CNAS\SIUI-SPITAL\Novensys.eCard.SDK.dll

using System;
using System.ComponentModel;

namespace Novensys.ASN1.Type
{
  [Serializable]
  public class Asn1BooleanType : Asn1Type
  {
    private bool __value;

    public bool BoolValue
    {
      get
      {
        return this.GetBoolValue();
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
        return !this.__value ? "FALSE" : "TRUE";
      }
    }

    public override string TypeName
    {
      get
      {
        return "BOOLEAN";
      }
    }

    public Asn1BooleanType()
    {
      this.ResetType();
    }

    public Asn1BooleanType(bool b)
    {
      this.SetValue(b);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal override int __getUniversalTagNumber()
    {
      return 1;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal virtual string[] __getXerIdentifierSet()
    {
      return (string[]) null;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal override string __getXerTag()
    {
      return "BOOLEAN";
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal virtual bool __isXerText()
    {
      return false;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal virtual bool __isXerWhiteSpaceCollapse()
    {
      return false;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal virtual bool __isXerWhiteSpaceReplace()
    {
      return false;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal override void __read(Asn1TypePerReader reader)
    {
      reader.__decodeBooleanType(this);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal override void __read(Asn1TypeXerReader reader)
    {
      reader.__decodeBooleanType(this);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal override void __read(Asn1TypeBerReader reader, bool isExplicit, bool primitive, long len)
    {
      reader.__decodeBooleanType(this, isExplicit, primitive, len);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal override void __readValue(Asn1TypeXerReader reader, string text)
    {
      reader.__decodeBooleanValue(this, text);
    }

    protected internal override void __setTypeValue(Asn1Type typeInstance)
    {
      if (typeInstance == null)
      {
        this.ResetType();
      }
      else
      {
        if (!(typeInstance is Asn1BooleanType))
          return;
        this.SetValue(((Asn1BooleanType) typeInstance).GetBoolValue());
      }
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal override void __write(Asn1TypeBerWriter writer)
    {
      writer.__encodeBooleanType(this, this.__getUniversalTagNumber(), 1);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal override void __write(Asn1TypePerWriter writer)
    {
      writer.__encodeBooleanType(this);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal override void __write(Asn1TypeXerWriter writer)
    {
      writer.__encodeBooleanType(this);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal override void __write(Asn1TypeBerWriter writer, int tagNumber, int tagClass)
    {
      writer.__encodeBooleanType(this, tagNumber, tagClass);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal override string __writeValue(Asn1TypeXerWriter writer)
    {
      return writer.__encodeBooleanValue(this);
    }

    public override bool Equals(object anObject)
    {
      return this == anObject || anObject is Asn1BooleanType && this.HasEqualValue((Asn1BooleanType) anObject);
    }

    public virtual bool GetBoolValue()
    {
      return this.__value;
    }

    public override int GetHashCode()
    {
      return !this.__value ? 1237 : 1231;
    }

    public virtual bool HasEqualValue(Asn1BooleanType that)
    {
      if (that == null)
        return false;
      else
        return this.__value == that.BoolValue;
    }

    public override void ResetType()
    {
      this.__resetCommons();
      this.SetValue(false);
      if (this.__getDefaultInstance() == null)
        return;
      this.__setTypeValue(this.__getDefaultInstance());
    }

    public virtual void SetValue(bool b)
    {
      this.__value = b;
    }
  }
}
