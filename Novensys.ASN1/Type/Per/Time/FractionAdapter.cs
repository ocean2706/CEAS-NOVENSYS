// Decompiled with JetBrains decompiler
// Type: Novensys.ASN1.Type.Per.Time.FractionAdapter
// Assembly: Novensys.eCard.SDK, Version=1.1.56.0, Culture=neutral, PublicKeyToken=null
// MVID: 59F9E456-D3DA-4066-A4A4-692B516A775E
// Assembly location: C:\Program Files (x86)\CNAS\SIUI-SPITAL\Novensys.eCard.SDK.dll

using Novensys.ASN1.Type;
using Novensys.ASN1.Util;
using System;

namespace Novensys.ASN1.Type.Per.Time
{
  public class FractionAdapter : Asn1IntegerTypeAdapter
  {
    public FractionAdapter(Asn1TimeType typeInstance)
      : base(typeInstance, 0L, 999L, true)
    {
    }

    public static int getNbDigitsToEncode(Asn1TimeType typeInstance)
    {
      int num = typeInstance.GetPropertySetting(10);
      if (num == -1)
        num = typeInstance.FractionNumberDigits;
      if (num == -1)
        num = Tools.fractionalPartLength(typeInstance.Fraction);
      return num;
    }

    protected override void postRead()
    {
      int nbDigits = this.typeInstance.GetPropertySetting(10);
      if (nbDigits == -1)
        nbDigits = this.typeInstance.FractionNumberDigits;
      if (nbDigits == -1)
        throw new NotSupportedException("PER Time Type decoding : fraction number digits not set");
      this.typeInstance.Fraction = Tools.fractionalPartLongToDouble(this.LongValue, nbDigits);
    }

    protected override void preWrite()
    {
      this.SetValue(Tools.fractionalPartAsLong(this.typeInstance.Fraction, FractionAdapter.getNbDigitsToEncode(this.typeInstance)));
    }
  }
}
