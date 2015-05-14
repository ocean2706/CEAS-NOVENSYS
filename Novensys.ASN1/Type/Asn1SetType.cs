// Decompiled with JetBrains decompiler
// Type: Novensys.ASN1.Type.Asn1SetType
// Assembly: Novensys.eCard.SDK, Version=1.1.56.0, Culture=neutral, PublicKeyToken=null
// MVID: 59F9E456-D3DA-4066-A4A4-692B516A775E
// Assembly location: C:\Program Files (x86)\CNAS\SIUI-SPITAL\Novensys.eCard.SDK.dll

using System;
using System.ComponentModel;

namespace Novensys.ASN1.Type
{
  [Serializable]
  public class Asn1SetType : Asn1ConstructedType
  {
    public override string TypeName
    {
      get
      {
        return "SET";
      }
    }

    public Asn1SetType()
    {
      this.__initialize();
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal override int __getUniversalTagNumber()
    {
      return 17;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal override string __getXerTag()
    {
      return "SET";
    }

    protected internal override void __setTypeValue(Asn1Type typeInstance)
    {
      if (typeInstance == null)
      {
        this.__setConstructedTypeValue((Asn1ConstructedType) null);
      }
      else
      {
        if (!(typeInstance is Asn1SetType))
          return;
        this.__setConstructedTypeValue((Asn1ConstructedType) typeInstance);
      }
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal override void __write(Asn1TypeBerWriter writer)
    {
      writer.__encodeSetType(this, this.__getUniversalTagNumber(), 1, this.IsIndefiniteLengthForm());
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal override void __write(Asn1TypeBerWriter writer, int tagNumber, int tagClass)
    {
      writer.__encodeSetType(this, tagNumber, tagClass, this.IsIndefiniteLengthForm());
    }

    public override bool Equals(object anObject)
    {
      return this == anObject || anObject is Asn1SetType && this.HasEqualValue((Asn1ConstructedType) anObject);
    }

    public override int GetHashCode()
    {
      return base.GetHashCode();
    }
  }
}
