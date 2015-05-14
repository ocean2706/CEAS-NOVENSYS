// Decompiled with JetBrains decompiler
// Type: Novensys.ASN1.Type.Asn1ObjectDescriptorType
// Assembly: Novensys.eCard.SDK, Version=1.1.56.0, Culture=neutral, PublicKeyToken=null
// MVID: 59F9E456-D3DA-4066-A4A4-692B516A775E
// Assembly location: C:\Program Files (x86)\CNAS\SIUI-SPITAL\Novensys.eCard.SDK.dll

using System;
using System.ComponentModel;

namespace Novensys.ASN1.Type
{
  [Serializable]
  public class Asn1ObjectDescriptorType : Asn1GraphicStringType
  {
    private Asn1ObjectIdentifierType __oid;

    public Asn1ObjectIdentifierType ObjectIdentifier
    {
      get
      {
        return this.__oid;
      }
      set
      {
        this.__oid = value;
      }
    }

    public override string TypeName
    {
      get
      {
        return "ObjectDescriptor";
      }
    }

    public Asn1ObjectDescriptorType()
    {
    }

    public Asn1ObjectDescriptorType(string str)
      : base(str)
    {
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal override int __getUniversalTagNumber()
    {
      return 7;
    }

    protected internal override void __setTypeValue(Asn1Type typeInstance)
    {
      if (typeInstance == null)
      {
        this.ResetType();
      }
      else
      {
        if (!(typeInstance is Asn1ObjectDescriptorType))
          return;
        this.SetValue(((Asn1StringType) typeInstance).GetStringValue());
      }
    }

    public override bool Equals(object anObject)
    {
      return this == anObject || anObject is Asn1ObjectDescriptorType && this.HasEqualValue((Asn1StringType) anObject);
    }

    public override int GetHashCode()
    {
      return base.GetHashCode();
    }

    public override void ResetType()
    {
      base.ResetType();
      this.__oid = (Asn1ObjectIdentifierType) null;
    }
  }
}
