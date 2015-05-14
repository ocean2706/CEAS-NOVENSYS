// Decompiled with JetBrains decompiler
// Type: Novensys.ASN1.Type.Per.Time.TimeDifferenceAdapter
// Assembly: Novensys.eCard.SDK, Version=1.1.56.0, Culture=neutral, PublicKeyToken=null
// MVID: 59F9E456-D3DA-4066-A4A4-692B516A775E
// Assembly location: C:\Program Files (x86)\CNAS\SIUI-SPITAL\Novensys.eCard.SDK.dll

using Novensys.ASN1.Type;
using Novensys.ASN1.Util;

namespace Novensys.ASN1.Type.Per.Time
{
  public class TimeDifferenceAdapter : Asn1SequenceTypeAdapter
  {
    private const int SIGN_NEGATIVE = 1;
    private const int SIGN_POSITIVE = 0;

    public TimeDifferenceAdapter(Asn1TimeType typeInstance)
      : base(typeInstance, 3, 1)
    {
    }

    protected override Asn1Type createComponentByIndex(int index)
    {
      switch (index)
      {
        case 0:
          return (Asn1Type) new DynAsn1IntegerType(0L, 1L, false);
        case 1:
          return (Asn1Type) new DynAsn1IntegerType(0L, 15L, false);
        case 2:
          return (Asn1Type) new DynAsn1IntegerType(1L, 59L, false);
        default:
          return (Asn1Type) null;
      }
    }

    protected override void initializeAdapter_impl()
    {
      this.__setComponentIsOptional(2);
    }

    protected override void postReadComponents()
    {
      int intValue1 = ((Asn1IntegerType) this.GetAsn1Type(0)).IntValue;
      int intValue2 = ((Asn1IntegerType) this.GetAsn1Type(1)).IntValue;
      int num1 = 0;
      if (this.__isComponentDefined(2))
        num1 = ((Asn1IntegerType) this.GetAsn1Type(2)).IntValue;
      int num2 = intValue2 * 60 + num1;
      if (intValue1 == 0)
        this.typeInstance.DifferenceFromUTC = num2;
      else
        this.typeInstance.DifferenceFromUTC = -num2;
    }

    protected override void preWriteComponents()
    {
      Asn1IntegerType asn1IntegerType1 = (Asn1IntegerType) this.NewAsn1Type(0);
      Asn1IntegerType asn1IntegerType2 = (Asn1IntegerType) this.NewAsn1Type(1);
      int differenceFromUtc = this.typeInstance.DifferenceFromUTC;
      int num1 = Tools.abs(differenceFromUtc) / 60;
      int num2 = Tools.abs(differenceFromUtc) % 60;
      if (differenceFromUtc >= 0)
        asn1IntegerType1.SetValue(0L);
      else
        asn1IntegerType1.SetValue(1L);
      asn1IntegerType2.SetValue((long) num1);
      if (num2 <= 0)
        return;
      ((Asn1IntegerType) this.NewAsn1Type(2)).SetValue((long) num2);
    }
  }
}
