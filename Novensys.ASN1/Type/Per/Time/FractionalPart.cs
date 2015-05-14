// Decompiled with JetBrains decompiler
// Type: Novensys.ASN1.Type.Per.Time.FractionalPart
// Assembly: Novensys.eCard.SDK, Version=1.1.56.0, Culture=neutral, PublicKeyToken=null
// MVID: 59F9E456-D3DA-4066-A4A4-692B516A775E
// Assembly location: C:\Program Files (x86)\CNAS\SIUI-SPITAL\Novensys.eCard.SDK.dll

using Novensys.ASN1.Type;
using Novensys.ASN1.Util;

namespace Novensys.ASN1.Type.Per.Time
{
  internal class FractionalPart : Asn1SequenceTypeAdapter
  {
    public FractionalPart(Asn1TimeType typeInstance)
      : base(typeInstance, 2, 0)
    {
    }

    protected override Asn1Type createComponentByIndex(int index)
    {
      switch (index)
      {
        case 0:
          return (Asn1Type) new DynAsn1IntegerType(1L, 3L, true);
        case 1:
          return (Asn1Type) new DynAsn1IntegerType(1L, 999L, true);
        default:
          return (Asn1Type) null;
      }
    }

    protected override void initializeAdapter_impl()
    {
    }

    protected override void postReadComponents()
    {
      Asn1IntegerType asn1IntegerType = (Asn1IntegerType) this.GetAsn1Type(0);
      this.typeInstance.Fraction = Tools.fractionalPartLongToDouble(((Asn1IntegerType) this.GetAsn1Type(1)).LongValue, asn1IntegerType.IntValue);
      this.typeInstance.FractionNumberDigits = asn1IntegerType.IntValue;
    }

    protected override void preWriteComponents()
    {
      ((Asn1IntegerType) this.NewAsn1Type(0)).SetValue((long) this.typeInstance.FractionNumberDigits);
      ((Asn1IntegerType) this.NewAsn1Type(1)).SetValue(Tools.fractionalPartAsLong(this.typeInstance.Fraction, this.typeInstance.FractionNumberDigits));
    }
  }
}
