// Decompiled with JetBrains decompiler
// Type: Novensys.ASN1.Type.Per.Time.YearEncodingAdapter
// Assembly: Novensys.eCard.SDK, Version=1.1.56.0, Culture=neutral, PublicKeyToken=null
// MVID: 59F9E456-D3DA-4066-A4A4-692B516A775E
// Assembly location: C:\Program Files (x86)\CNAS\SIUI-SPITAL\Novensys.eCard.SDK.dll

using Novensys.ASN1.Type;

namespace Novensys.ASN1.Type.Per.Time
{
  public class YearEncodingAdapter : Asn1ChoiceTypeAdapter
  {
    public YearEncodingAdapter(Asn1TimeType typeInstance)
      : base(typeInstance, 4)
    {
    }

    protected override Asn1Type createComponentByIndex(int index)
    {
      switch (index)
      {
        case 0:
          return (Asn1Type) new DynAsn1IntegerType(2005L, 2020L, false);
        case 1:
          return (Asn1Type) new DynAsn1IntegerType(2021L, 2276L, false);
        case 2:
          return (Asn1Type) new DynAsn1IntegerType(1749L, 2004L, false);
        case 3:
          return (Asn1Type) new Asn1IntegerType();
        default:
          return (Asn1Type) null;
      }
    }

    protected override void postReadComponents()
    {
      this.typeInstance.Year = ((Asn1IntegerType) this.GetTypeValue()).IntValue;
    }

    protected override void preWriteComponents()
    {
      int year = this.typeInstance.Year;
      (2005 > year || year > 2020 ? (2021 > year || year > 2276 ? (1749 > year || year > 2004 ? (Asn1IntegerType) this.NewAsn1Type(3) : (Asn1IntegerType) this.NewAsn1Type(2)) : (Asn1IntegerType) this.NewAsn1Type(1)) : (Asn1IntegerType) this.NewAsn1Type(0)).SetValue((long) year);
    }
  }
}
