// Decompiled with JetBrains decompiler
// Type: Novensys.ASN1.Type.Asn1SetOfType
// Assembly: Novensys.eCard.SDK, Version=1.1.56.0, Culture=neutral, PublicKeyToken=null
// MVID: 59F9E456-D3DA-4066-A4A4-692B516A775E
// Assembly location: C:\Program Files (x86)\CNAS\SIUI-SPITAL\Novensys.eCard.SDK.dll

using System;
using System.ComponentModel;

namespace Novensys.ASN1.Type
{
  [Serializable]
  public class Asn1SetOfType : Asn1ConstructedOfType
  {
    public override string TypeName
    {
      get
      {
        return "SET OF";
      }
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal override int __getUniversalTagNumber()
    {
      return 17;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal override string __getXerTag()
    {
      return "SET_OF";
    }

    protected internal override void __setTypeValue(Asn1Type typeInstance)
    {
      if (typeInstance == null)
      {
        this.__setConstructedOfTypeValue((Asn1ConstructedOfType) null);
      }
      else
      {
        if (!(typeInstance is Asn1SetOfType))
          return;
        this.__setConstructedOfTypeValue((Asn1ConstructedOfType) typeInstance);
      }
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal override void __write(Asn1TypeBerWriter writer)
    {
      writer.__encodeSetOfType(this, this.__getUniversalTagNumber(), 1, this.IsIndefiniteLengthForm());
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal override void __write(Asn1TypeXerWriter writer)
    {
      writer.__encodeSetOfType(this);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal override void __write(Asn1TypeBerWriter writer, int tagNumber, int tagClass)
    {
      writer.__encodeSetOfType(this, tagNumber, tagClass, this.IsIndefiniteLengthForm());
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal override string __writeValue(Asn1TypeXerWriter writer)
    {
      return writer.__encodeSetOfValue(this);
    }

    public override bool Equals(object anObject)
    {
      return this == anObject || anObject is Asn1SetOfType && this.HasEqualValue((Asn1ConstructedOfType) anObject);
    }

    public override int GetHashCode()
    {
      return base.GetHashCode();
    }
  }
}
