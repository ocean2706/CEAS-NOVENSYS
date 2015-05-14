// Decompiled with JetBrains decompiler
// Type: Novensys.ASN1.Type.Per.Time.WeekAdapter
// Assembly: Novensys.eCard.SDK, Version=1.1.56.0, Culture=neutral, PublicKeyToken=null
// MVID: 59F9E456-D3DA-4066-A4A4-692B516A775E
// Assembly location: C:\Program Files (x86)\CNAS\SIUI-SPITAL\Novensys.eCard.SDK.dll

using Novensys.ASN1.Type;

namespace Novensys.ASN1.Type.Per.Time
{
  public class WeekAdapter : Asn1IntegerTypeAdapter
  {
    public WeekAdapter(Asn1TimeType typeInstance)
      : base(typeInstance, 1L, 53L, false)
    {
    }

    public WeekAdapter(Asn1TimeType typeInstance, long lowerBound, long upperBound, bool extensible)
      : base(typeInstance, lowerBound, upperBound, extensible)
    {
    }

    protected override void postRead()
    {
      this.typeInstance.Week = this.IntValue;
    }

    protected override void preWrite()
    {
      this.SetValue((long) this.typeInstance.Week);
    }
  }
}
