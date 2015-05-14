// Decompiled with JetBrains decompiler
// Type: Novensys.ASN1.Type.Asn1T61StringType
// Assembly: Novensys.eCard.SDK, Version=1.1.56.0, Culture=neutral, PublicKeyToken=null
// MVID: 59F9E456-D3DA-4066-A4A4-692B516A775E
// Assembly location: C:\Program Files (x86)\CNAS\SIUI-SPITAL\Novensys.eCard.SDK.dll

using System;
using System.ComponentModel;

namespace Novensys.ASN1.Type
{
  [Serializable]
  public class Asn1T61StringType : Asn1UnknownMultiplierStringType
  {
    public override string TypeName
    {
      get
      {
        return "T61String";
      }
    }

    public Asn1T61StringType()
    {
    }

    public Asn1T61StringType(string str)
      : base(str)
    {
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal override int __getUniversalTagNumber()
    {
      return 20;
    }

    protected internal override void __setTypeValue(Asn1Type typeInstance)
    {
      if (typeInstance == null)
        this.ResetType();
      else if (typeInstance is Asn1T61StringType)
      {
        this.SetValue(((Asn1StringType) typeInstance).GetStringValue());
      }
      else
      {
        if (!(typeInstance is Asn1TeletexStringType))
          return;
        this.SetValue(((Asn1StringType) typeInstance).GetStringValue());
      }
    }

    public override bool Equals(object anObject)
    {
      return this == anObject || (anObject is Asn1T61StringType || anObject is Asn1TeletexStringType) && this.HasEqualValue((Asn1StringType) anObject);
    }

    public override int GetHashCode()
    {
      return base.GetHashCode();
    }
  }
}
