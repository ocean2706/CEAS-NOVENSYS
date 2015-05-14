// Decompiled with JetBrains decompiler
// Type: Novensys.ASN1.Type.Per.Time.DurationIntervalAdapter
// Assembly: Novensys.eCard.SDK, Version=1.1.56.0, Culture=neutral, PublicKeyToken=null
// MVID: 59F9E456-D3DA-4066-A4A4-692B516A775E
// Assembly location: C:\Program Files (x86)\CNAS\SIUI-SPITAL\Novensys.eCard.SDK.dll

using Novensys.ASN1.Type;

namespace Novensys.ASN1.Type.Per.Time
{
  public class DurationIntervalAdapter : Asn1SequenceTypeAdapter
  {
    private const int DAYS_INDEX = 3;
    private const int FRACTIONAL_PART_INDEX = 7;
    private const int HOURS_INDEX = 4;
    private const int MINUTES_INDEX = 5;
    private const int MONTHS_INDEX = 1;
    private const int SECONDS_INDEX = 6;
    private const int WEEKS_INDEX = 2;
    private const int YEARS_INDEX = 0;

    public DurationIntervalAdapter(Asn1TimeType typeInstance)
      : base(typeInstance, 8, 8)
    {
    }

    protected override Asn1Type createComponentByIndex(int index)
    {
      switch (index)
      {
        case 0:
          return (Asn1Type) new YearAdapter(this.typeInstance, 0L, 31L, true);
        case 1:
          return (Asn1Type) new MonthAdapter(this.typeInstance, 0L, 15L, true);
        case 2:
          return (Asn1Type) new WeekAdapter(this.typeInstance, 0L, 63L, true);
        case 3:
          return (Asn1Type) new DayAdapter(this.typeInstance, 0L, 31L, true);
        case 4:
          return (Asn1Type) new HoursAdapter(this.typeInstance, 0L, 31L, true);
        case 5:
          return (Asn1Type) new MinutesAdapter(this.typeInstance, 0L, 63L, true);
        case 6:
          return (Asn1Type) new SecondsAdapter(this.typeInstance, 0L, 63L, true);
        case 7:
          return (Asn1Type) new FractionalPart(this.typeInstance);
        default:
          return (Asn1Type) null;
      }
    }

    protected override void initializeAdapter_impl()
    {
      this.__setComponentIsOptional(0);
      this.__setComponentIsOptional(1);
      this.__setComponentIsOptional(2);
      this.__setComponentIsOptional(3);
      this.__setComponentIsOptional(4);
      this.__setComponentIsOptional(5);
      this.__setComponentIsOptional(6);
      this.__setComponentIsOptional(7);
    }

    protected override void postReadComponents()
    {
      if (this.__isComponentDefined(2))
        return;
      if (this.typeInstance.Second != -1 && this.typeInstance.Minute == -1)
        this.typeInstance.Minute = 0;
      if (this.typeInstance.Minute != -1 && this.typeInstance.Hour == -1)
        this.typeInstance.Hour = 0;
      if (this.typeInstance.Hour != -1 && this.typeInstance.Day == -1)
        this.typeInstance.Day = 0;
      if (this.typeInstance.Day != -1 && this.typeInstance.Month == -1)
        this.typeInstance.Month = 0;
      if (this.typeInstance.Month != -1 && this.typeInstance.Year == -1)
        this.typeInstance.Year = 0;
    }

    protected override void preWriteComponents()
    {
      DurationIntervalAdapter.TimeInspector timeInspector = new DurationIntervalAdapter.TimeInspector(this.typeInstance);
      if (this.typeInstance.Week != -1)
      {
        this.NewAsn1Type(2);
      }
      else
      {
        if (this.typeInstance.Year != -1 && (this.typeInstance.Year != 0 || timeInspector.leastSignificant == 7))
          this.NewAsn1Type(0);
        if (this.typeInstance.Month != -1 && (this.typeInstance.Month != 0 || timeInspector.leastSignificant == 6))
          this.NewAsn1Type(1);
        if (this.typeInstance.Day != -1 && (this.typeInstance.Day != 0 || timeInspector.leastSignificant == 4))
          this.NewAsn1Type(3);
        if (this.typeInstance.Hour != -1 && (this.typeInstance.Hour != 0 || timeInspector.leastSignificant == 3))
          this.NewAsn1Type(4);
        if (this.typeInstance.Minute != -1 && (this.typeInstance.Minute != 0 || timeInspector.leastSignificant == 2))
          this.NewAsn1Type(5);
        if (this.typeInstance.Second != -1 && (this.typeInstance.Second != 0 || timeInspector.leastSignificant == 1))
          this.NewAsn1Type(6);
      }
      if (timeInspector.fractionOf == -1)
        return;
      this.NewAsn1Type(7);
    }

    private class TimeInspector
    {
      internal int fractionOf = -1;
      internal int leastSignificant = -1;
      internal const int GRANULARITY_DAY = 4;
      internal const int GRANULARITY_HOUR = 3;
      internal const int GRANULARITY_MINUTE = 2;
      internal const int GRANULARITY_MONTH = 6;
      internal const int GRANULARITY_NONE = -1;
      internal const int GRANULARITY_SECOND = 1;
      internal const int GRANULARITY_WEEK = 5;
      internal const int GRANULARITY_YEAR = 7;

      internal TimeInspector(Asn1TimeType typeInstance)
      {
        if (typeInstance.Week != -1)
          this.leastSignificant = 5;
        else if (typeInstance.Second != -1)
          this.leastSignificant = 1;
        else if (typeInstance.Minute != -1)
          this.leastSignificant = 2;
        else if (typeInstance.Hour != -1)
          this.leastSignificant = 3;
        else if (typeInstance.Day != -1)
          this.leastSignificant = 4;
        else if (typeInstance.Month != -1)
          this.leastSignificant = 6;
        else if (typeInstance.Year != -1)
          this.leastSignificant = 7;
        if (typeInstance.Fraction == -1.0)
          return;
        this.fractionOf = this.leastSignificant;
      }
    }
  }
}
