// Decompiled with JetBrains decompiler
// Type: Novensys.ASN1.Type.Asn1UTF8StringType
// Assembly: Novensys.eCard.SDK, Version=1.1.56.0, Culture=neutral, PublicKeyToken=null
// MVID: 59F9E456-D3DA-4066-A4A4-692B516A775E
// Assembly location: C:\Program Files (x86)\CNAS\SIUI-SPITAL\Novensys.eCard.SDK.dll

using System;
using System.ComponentModel;
using System.Text;

namespace Novensys.ASN1.Type
{
  [Serializable]
  public class Asn1UTF8StringType : Asn1UnknownMultiplierStringType
  {
    public override string PrintableValue
    {
      get
      {
        return this.__getCharsDefnPrintableValue(true);
      }
    }

    public override string TypeName
    {
      get
      {
        return "UTF8String";
      }
    }

    public Asn1UTF8StringType()
    {
    }

    public Asn1UTF8StringType(string str)
      : base(str)
    {
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal override int __getUniversalTagNumber()
    {
      return 12;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal virtual string[] __getXerAnyElementNamespaceRestrictionExcept()
    {
      return (string[]) null;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal virtual string[] __getXerAnyElementNamespaceRestrictionFrom()
    {
      return (string[]) null;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal virtual bool __isXerAnyElementNamespaceRestrictionExceptAbsent()
    {
      return false;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal virtual bool __isXerAnyElementNamespaceRestrictionFromAbsent()
    {
      return false;
    }

    protected internal override void __setTypeValue(Asn1Type typeInstance)
    {
      if (typeInstance == null)
      {
        this.ResetType();
      }
      else
      {
        if (!(typeInstance is Asn1UTF8StringType))
          return;
        this.SetValue(((Asn1StringType) typeInstance).GetStringValue());
      }
    }

    public override bool Equals(object anObject)
    {
      return this == anObject || anObject is Asn1UTF8StringType && this.HasEqualValue((Asn1StringType) anObject);
    }

    public override byte[] GetByteArrayValue()
    {
      return base.GetByteArrayValue((Encoding) new UTF8Encoding());
    }

    public override int GetHashCode()
    {
      return base.GetHashCode();
    }

    public override void SetValue(byte[] data)
    {
      base.SetValue(data, (Encoding) new UTF8Encoding());
    }
  }
}
