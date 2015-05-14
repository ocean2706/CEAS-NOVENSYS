// Decompiled with JetBrains decompiler
// Type: Novensys.ASN1.Type.Asn1TimeType
// Assembly: Novensys.eCard.SDK, Version=1.1.56.0, Culture=neutral, PublicKeyToken=null
// MVID: 59F9E456-D3DA-4066-A4A4-692B516A775E
// Assembly location: C:\Program Files (x86)\CNAS\SIUI-SPITAL\Novensys.eCard.SDK.dll

using Novensys.ASN1;
using Novensys.ASN1.Util;
using System;
using System.ComponentModel;
using System.Globalization;
using System.Text;

namespace Novensys.ASN1.Type
{
  [Serializable]
  public class Asn1TimeType : Asn1Type
  {
    private const int SETTING_ADDITIONAL = 9;
    private const int SETTING_ADDITIONAL_COUNT = 3;
    public const int SETTING_BASIC = 0;
    public const int SETTING_BASIC_DATE = 0;
    public const int SETTING_BASIC_DATE_TIME = 2;
    public const int SETTING_BASIC_INTERVAL = 3;
    public const int SETTING_BASIC_REC_INTERVAL = 4;
    public const int SETTING_BASIC_TIME = 1;
    private const int SETTING_COUNT = 9;
    public const int SETTING_DATE = 1;
    public const int SETTING_DATE_CENTURY = 0;
    public const int SETTING_DATE_YEAR = 1;
    public const int SETTING_DATE_YEAR_DAY = 4;
    public const int SETTING_DATE_YEAR_MONTH = 2;
    public const int SETTING_DATE_YEAR_MONTH_DAY = 3;
    public const int SETTING_DATE_YEAR_WEEK = 5;
    public const int SETTING_DATE_YEAR_WEEK_DAY = 6;
    public const int SETTING_INTERVAL_TYPE = 5;
    public const int SETTING_INTERVAL_TYPE_DURATION = 1;
    public const int SETTING_INTERVAL_TYPE_DURATION_END = 3;
    public const int SETTING_INTERVAL_TYPE_START_DURATION = 2;
    public const int SETTING_INTERVAL_TYPE_START_END = 0;
    public const int SETTING_LOCAL_OR_UTC = 4;
    public const int SETTING_LOCAL_OR_UTC_LOCAL = 0;
    public const int SETTING_LOCAL_OR_UTC_LOCAL_AND_DIFFERENCE = 2;
    public const int SETTING_LOCAL_OR_UTC_UTC = 1;
    public const int SETTING_MIDNIGHT = 8;
    public const int SETTING_MIDNIGHT_END = 1;
    public const int SETTING_MIDNIGHT_START = 0;
    public const int SETTING_RECURRENCE = 7;
    public const int SETTING_RECURRENCE_NUMBER_DIGITS = 1;
    public const int SETTING_RECURRENCE_NUMBER_DIGITS_NUMBER = 11;
    public const int SETTING_RECURRENCE_UNLIMITED = 0;
    public const int SETTING_SE_POINT = 6;
    public const int SETTING_SE_POINT_DATE = 0;
    public const int SETTING_SE_POINT_DATE_TIME = 2;
    public const int SETTING_SE_POINT_TIME = 1;
    public const int SETTING_TIME = 3;
    public const int SETTING_TIME_FRACTION_NUMBER = 10;
    public const int SETTING_TIME_HOUR = 0;
    public const int SETTING_TIME_HOUR_FRACTION = 3;
    public const int SETTING_TIME_HOUR_MINUTE = 1;
    public const int SETTING_TIME_HOUR_MINUTE_FRACTION = 4;
    public const int SETTING_TIME_HOUR_MINUTE_SECOND = 2;
    public const int SETTING_TIME_HOUR_MINUTE_SECOND_FRACTION = 5;
    public const int SETTING_YEAR = 2;
    public const int SETTING_YEAR_BASIC = 0;
    public const int SETTING_YEAR_BASIC_OR_PROLEPTIC = 2;
    public const int SETTING_YEAR_LARGE = 4;
    public const int SETTING_YEAR_LARGE_NUMBER = 9;
    public const int SETTING_YEAR_NEGATIVE = 3;
    public const int SETTING_YEAR_NEGATIVE_OR_LARGE = 5;
    public const int SETTING_YEAR_PROLEPTIC = 1;
    public const int UNLIMITED_RECURRENCE = -2;
    private int __day;
    private int __differenceFromUTC;
    private Asn1DurationType __duration;
    private Asn1TimeType __endPoint;
    private double __fraction;
    private int __fractionNumberDigits;
    private int __hour;
    private bool __isTimeUTC;
    private int __minute;
    private int __month;
    private int __recurrence;
    private int __second;
    private Asn1TimeType __startPoint;
    private string __value;
    private int __week;
    private int __year;
    private int __yearSetting;
    protected int[] settings;

    public int Century
    {
      get
      {
        this.ensureTimeComponentsDefined();
        return this.__year;
      }
      set
      {
        this.__year = value;
        this.__yearSetting = 0;
      }
    }

    public DateTime DateTimeValue
    {
      get
      {
        return this.GetDateTimeValue();
      }
      set
      {
        this.SetValue(value);
      }
    }

    public int Day
    {
      get
      {
        this.ensureTimeComponentsDefined();
        return this.__day;
      }
      set
      {
        this.__day = value;
      }
    }

    public int DifferenceFromUTC
    {
      get
      {
        this.ensureTimeComponentsDefined();
        return this.__differenceFromUTC;
      }
      set
      {
        this.__differenceFromUTC = value;
        this.__isTimeUTC = false;
      }
    }

    public Asn1DurationType Duration
    {
      get
      {
        this.ensureTimeComponentsDefined();
        return this.__duration;
      }
      set
      {
        this.__duration = value;
      }
    }

    public Asn1TimeType EndPoint
    {
      get
      {
        this.ensureTimeComponentsDefined();
        return this.__endPoint;
      }
      set
      {
        if (value != null)
        {
          value.__setPropertySetting(0, this.settings[6]);
          value.__setPropertySetting(1, this.settings[1]);
          value.__setPropertySetting(2, this.settings[2]);
          value.__setPropertySetting(9, this.settings[9]);
          value.__setPropertySetting(3, this.settings[3]);
          value.__setPropertySetting(10, this.settings[10]);
          value.__setPropertySetting(4, this.settings[4]);
          value.__setPropertySetting(8, this.settings[8]);
        }
        this.__endPoint = value;
      }
    }

    public double Fraction
    {
      get
      {
        this.ensureTimeComponentsDefined();
        return this.__fraction;
      }
      set
      {
        this.__fraction = value;
        if (this.settings[5] == 1 || this.settings[10] == -1)
          return;
        this.FractionNumberDigits = this.settings[10];
      }
    }

    public int FractionNumberDigits
    {
      get
      {
        this.ensureTimeComponentsDefined();
        return this.__fractionNumberDigits;
      }
      set
      {
        this.__fractionNumberDigits = value;
      }
    }

    public int Hour
    {
      get
      {
        this.ensureTimeComponentsDefined();
        return this.__hour;
      }
      set
      {
        this.__hour = value;
      }
    }

    public bool IsTimeUTC
    {
      get
      {
        this.ensureTimeComponentsDefined();
        return this.__isTimeUTC;
      }
      set
      {
        this.__isTimeUTC = value;
        this.__differenceFromUTC = int.MinValue;
      }
    }

    public int Minute
    {
      get
      {
        this.ensureTimeComponentsDefined();
        return this.__minute;
      }
      set
      {
        this.__minute = value;
      }
    }

    public int Month
    {
      get
      {
        this.ensureTimeComponentsDefined();
        return this.__month;
      }
      set
      {
        this.__month = value;
      }
    }

    public override string PrintableValue
    {
      get
      {
        return "\"" + this.GetStringValue() + "\"";
      }
    }

    public int Recurrence
    {
      get
      {
        this.ensureTimeComponentsDefined();
        return this.__recurrence;
      }
      set
      {
        this.__recurrence = value;
      }
    }

    public int Second
    {
      get
      {
        this.ensureTimeComponentsDefined();
        return this.__second;
      }
      set
      {
        this.__second = value;
      }
    }

    public Asn1TimeType StartPoint
    {
      get
      {
        this.ensureTimeComponentsDefined();
        return this.__startPoint;
      }
      set
      {
        if (value != null)
        {
          value.__setPropertySetting(0, this.settings[6]);
          value.__setPropertySetting(1, this.settings[1]);
          value.__setPropertySetting(2, this.settings[2]);
          value.__setPropertySetting(9, this.settings[9]);
          value.__setPropertySetting(3, this.settings[3]);
          value.__setPropertySetting(10, this.settings[10]);
          value.__setPropertySetting(4, this.settings[4]);
          value.__setPropertySetting(8, this.settings[8]);
        }
        this.__startPoint = value;
      }
    }

    public string StringValue
    {
      get
      {
        return this.GetStringValue();
      }
      set
      {
        this.SetValue(value);
      }
    }

    public override string TypeName
    {
      get
      {
        return "TIME";
      }
    }

    public int Week
    {
      get
      {
        this.ensureTimeComponentsDefined();
        return this.__week;
      }
      set
      {
        this.__week = value;
      }
    }

    public int Year
    {
      get
      {
        this.ensureTimeComponentsDefined();
        return this.__year;
      }
      set
      {
        this.__year = value;
        this.__yearSetting = 1;
      }
    }

    public Asn1TimeType()
    {
      this.settings = new int[12];
      for (int index = 0; index < this.settings.Length; ++index)
        this.settings[index] = -1;
      this.ResetType();
      this.__initPropertySettings();
    }

    public Asn1TimeType(DateTime dateTime)
      : this()
    {
      this.SetValue(dateTime);
    }

    public Asn1TimeType(string val)
      : this()
    {
      this.SetValue(val);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal override int __getUniversalTagNumber()
    {
      return 14;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal override string __getXerTag()
    {
      return "TIME";
    }

    protected internal virtual void __initPropertySettings()
    {
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal override void __read(Asn1TypePerReader reader)
    {
      reader.__decodeTimeType(this);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal override void __read(Asn1TypeXerReader reader)
    {
      reader.__decodeTimeType(this);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal override void __read(Asn1TypeBerReader reader, bool isExplicit, bool primitive, long len)
    {
      reader.__decodeTimeType(this, isExplicit, primitive, len);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal override void __readValue(Asn1TypeXerReader reader, string text)
    {
      reader.__decodeTimeValue(this, text);
    }

    protected void __setPropertySetting(int propertySetting, int propertySettingValue)
    {
      this.settings[propertySetting] = propertySettingValue;
    }

    protected internal override void __setTypeValue(Asn1Type typeInstance)
    {
      if (typeInstance == null)
      {
        this.ResetType();
      }
      else
      {
        if (!(typeInstance is Asn1TimeType))
          return;
        this.SetValue(((Asn1TimeType) typeInstance).StringValue);
      }
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal override void __write(Asn1TypeBerWriter writer)
    {
      writer.__encodeTimeType(this, this.__getUniversalTagNumber(), 1);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal override void __write(Asn1TypePerWriter writer)
    {
      writer.__encodeTimeType(this);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal override void __write(Asn1TypeXerWriter writer)
    {
      writer.__encodeTimeType(this);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal override void __write(Asn1TypeBerWriter writer, int tagNumber, int tagClass)
    {
      writer.__encodeTimeType(this, tagNumber, tagClass);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal override string __writeValue(Asn1TypeXerWriter writer)
    {
      return writer.__encodeTimeValue(this);
    }

    protected void buildDateTime(StringBuilder sb, bool canonicalize, bool addDifferenceFromUTC)
    {
      if (this.settings[0] == 0 || this.settings[0] == 2 || this.settings[0] == -1 && (this.IsYearDefined() || this.__month != -1 || (this.__week != -1 || this.__day != -1)))
      {
        if (this.settings[2] == 0 || this.settings[2] == 1 || this.settings[2] == 2 || this.settings[2] == -1 && this.IsYearDefined() && (this.IsYearCentury() && this.__year <= 99 && this.__year >= 0 || !this.IsYearCentury() && this.__year <= 9999 && this.__year >= 0))
        {
          if (this.settings[1] == 0 || this.IsYearCentury())
          {
            if (this.__year < 10)
              sb.Append('0');
            sb.Append(this.__year);
            sb.Append('C');
          }
          else
          {
            if (this.__year < 1000)
              sb.Append('0');
            if (this.__year < 100)
              sb.Append('0');
            if (this.__year < 10)
              sb.Append('0');
            sb.Append(this.__year);
          }
        }
        else if (this.settings[2] == 3 || this.settings[2] == 4 || (this.settings[2] == 5 || this.settings[2] == -1 && this.IsYearDefined()))
        {
          if (this.settings[1] == 0 || this.IsYearCentury())
          {
            int num1 = Tools.abs(this.__year);
            if (this.settings[2] == 3 || this.settings[2] == -1 && this.__year >= -99 && this.__year <= -1)
            {
              sb.Append('-');
              if (num1 < 10)
                sb.Append('0');
              sb.Append(num1);
            }
            else
            {
              if (this.__year < 0)
                sb.Append('-');
              else
                sb.Append('+');
              if (this.settings[2] != -1)
              {
                int num2 = this.settings[2] != 5 || this.__year < -99 || this.__year > -1 ? this.settings[9] - 2 : 2;
                string str = num1.ToString();
                if (str.Length < num2)
                {
                  for (int index = 0; index < num2 - str.Length; ++index)
                    sb.Append('0');
                }
              }
              sb.Append(num1);
            }
            sb.Append('C');
          }
          else
          {
            int num1 = Tools.abs(this.__year);
            if (this.settings[2] == 3 || this.settings[2] == -1 && this.__year >= -9999 && this.__year <= -1)
            {
              sb.Append('-');
              if (num1 < 1000)
                sb.Append('0');
              if (num1 < 100)
                sb.Append('0');
              if (num1 < 10)
                sb.Append('0');
              sb.Append(num1);
            }
            else
            {
              if (this.__year < 0)
                sb.Append('-');
              else
                sb.Append('+');
              if (this.settings[2] != -1)
              {
                int num2 = this.settings[2] != 5 || this.__year < -9999 || this.__year > -1 ? this.settings[9] : 4;
                string str = num1.ToString();
                if (str.Length < num2)
                {
                  for (int index = 0; index < num2 - str.Length; ++index)
                    sb.Append('0');
                }
              }
              sb.Append(num1);
            }
          }
        }
        if (this.settings[1] == 2 || this.settings[1] == 3 || (this.settings[1] == 1 || this.settings[1] == -1) && this.__month != -1)
        {
          sb.Append('-');
          if (this.__month < 10)
            sb.Append('0');
          sb.Append(this.__month);
        }
        else if (this.settings[1] == 5 || this.settings[1] == 6 || (this.settings[1] == 1 || this.settings[1] == -1) && this.__week != -1)
        {
          sb.Append('-');
          sb.Append('W');
          if (this.__week < 10)
            sb.Append('0');
          sb.Append(this.__week);
        }
        if (this.settings[1] == 6 || (this.settings[1] == 1 || this.settings[1] == -1) && (this.__week != -1 && this.__day != -1))
        {
          sb.Append('-');
          sb.Append(this.__day);
        }
        else if (this.settings[1] == 3 || (this.settings[1] == 1 || this.settings[1] == -1) && (this.__month != -1 && this.__day != -1))
        {
          sb.Append('-');
          if (this.__day < 10)
            sb.Append('0');
          sb.Append(this.__day);
        }
        else if (this.settings[1] == 4 || (this.settings[1] == 1 || this.settings[1] == -1) && (this.IsYearDefined() && this.__day != -1) && (this.__week == -1 && this.__month == -1))
        {
          sb.Append('-');
          if (this.__day < 100)
            sb.Append('0');
          if (this.__day < 10)
            sb.Append('0');
          sb.Append(this.__day);
        }
      }
      if (this.settings[0] != 1 && this.settings[0] != 2 && (this.settings[0] != -1 || this.__hour == -1 && this.__minute == -1 && this.__second == -1))
        return;
      if (this.settings[0] == 2 || this.settings[0] == -1 && (this.IsYearDefined() || this.__month != -1 || (this.__week != -1 || this.__day != -1)))
        sb.Append('T');
      if ((this.settings[3] == 0 || this.settings[3] == 3) && (this.__hour == 0 || this.__hour == 24) && (this.__fraction == -1.0 || this.__fraction == 0.0) || (this.settings[3] == 1 || this.settings[3] == 4) && (this.__hour == 0 || this.__hour == 24) && (this.__minute == 0 && (this.__fraction == -1.0 || this.__fraction == 0.0)) || (this.settings[3] == 2 || this.settings[3] == 5) && (this.__hour == 0 || this.__hour == 24) && (this.__minute == 0 && this.__second == 0 && (this.__fraction == -1.0 || this.__fraction == 0.0)))
      {
        if (this.settings[8] == 0)
          sb.Append("00");
        else if (this.settings[8] == 1)
          sb.Append("24");
        else if (this.__hour == 0)
          sb.Append("00");
        else
          sb.Append("24");
        if (this.settings[3] == 1 || this.settings[3] == 4 || (this.settings[3] == 2 || this.settings[3] == 5))
        {
          sb.Append(':');
          sb.Append("00");
        }
        if (this.settings[3] == 2 || this.settings[3] == 5)
        {
          sb.Append(':');
          sb.Append("00");
        }
      }
      else
      {
        if (this.__hour != -1)
        {
          if (this.__hour < 10)
            sb.Append("0");
          sb.Append(this.__hour);
        }
        else if (this.__minute != -1 || this.__second != -1 || this.__fraction != -1.0)
          sb.Append("00");
        if (this.settings[3] == 1 || this.settings[3] == 2 || (this.settings[3] == 4 || this.settings[3] == 5) || this.settings[3] == -1 && (this.__minute != -1 || this.__second != -1 || this.__fraction != -1.0 && this.__hour == -1))
        {
          sb.Append(':');
          if (this.__minute != -1)
          {
            if (this.__minute < 10)
              sb.Append('0');
            sb.Append(this.__minute);
          }
          else
            sb.Append("00");
        }
        if (this.settings[3] == 2 || this.settings[3] == 5 || this.settings[3] == -1 && this.__second != -1)
        {
          sb.Append(':');
          if (this.__second < 10)
            sb.Append('0');
          sb.Append(this.__second);
        }
      }
      if (this.settings[3] == 3 || this.settings[3] == 4 || (this.settings[3] == 5 || this.settings[3] == -1 && this.__fraction != -1.0))
        this.buildFraction(sb, this.settings[10] != -1 ? this.settings[10] : this.__fractionNumberDigits);
      if (this.settings[4] == 1 || this.settings[4] == -1 && this.IsTimeUTC)
        sb.Append('Z');
      else if ((this.settings[4] == 2 || this.settings[4] == -1 && this.__differenceFromUTC != int.MinValue) && addDifferenceFromUTC)
      {
        int num1;
        if (this.__differenceFromUTC >= 0)
        {
          sb.Append('+');
          num1 = this.__differenceFromUTC;
        }
        else
        {
          sb.Append('-');
          num1 = -this.__differenceFromUTC;
        }
        int num2 = num1 % 60;
        int num3 = num1 / 60;
        if (num3 < 10)
          sb.Append('0');
        sb.Append(num3);
        if (num2 != 0)
        {
          sb.Append(':');
          if (num2 < 10)
            sb.Append('0');
          sb.Append(num2);
        }
      }
    }

    protected void buildDuration(StringBuilder sb, bool canonicalize)
    {
      sb.Append('P');
      if (this.__week != -1)
      {
        sb.Append(this.__week);
        if (this.__fraction != -1.0)
          this.buildFraction(sb, this.__fractionNumberDigits);
        sb.Append('W');
      }
      else
      {
        if (this.__year != -1)
        {
          bool flag = true;
          if (canonicalize && this.__year == 0)
            flag = this.__month == -1 && this.__day == -1 && (this.__hour == -1 && this.__minute == -1) && this.__second == -1;
          if (flag)
          {
            sb.Append(this.__year);
            if (this.__fraction != -1.0 && this.__month == -1 && (this.__day == -1 && this.__hour == -1) && (this.__minute == -1 && this.__second == -1))
              this.buildFraction(sb, this.__fractionNumberDigits);
            sb.Append('Y');
          }
        }
        if (this.__month != -1)
        {
          bool flag = true;
          if (canonicalize && this.__month == 0)
            flag = this.__day == -1 && this.__hour == -1 && (this.__minute == -1 && this.__second == -1);
          if (flag)
          {
            sb.Append(this.__month);
            if (this.__fraction != -1.0 && this.__day == -1 && (this.__hour == -1 && this.__minute == -1) && this.__second == -1)
              this.buildFraction(sb, this.__fractionNumberDigits);
            sb.Append('M');
          }
        }
        if (this.__day != -1)
        {
          bool flag = true;
          if (canonicalize && this.__day == 0)
            flag = this.__hour == -1 && this.__minute == -1 && this.__second == -1;
          if (flag)
          {
            sb.Append(this.__day);
            if (this.__fraction != -1.0 && this.__hour == -1 && (this.__minute == -1 && this.__second == -1))
              this.buildFraction(sb, this.__fractionNumberDigits);
            sb.Append('D');
          }
        }
        if (this.__hour != -1 || this.__minute != -1 || this.__second != -1)
        {
          sb.Append('T');
          if (this.__hour != -1)
          {
            bool flag = true;
            if (canonicalize && this.__hour == 0)
              flag = this.__minute == -1 && this.__second == -1;
            if (flag)
            {
              sb.Append(this.__hour);
              if (this.__fraction != -1.0 && this.__minute == -1 && this.__second == -1)
                this.buildFraction(sb, this.__fractionNumberDigits);
              sb.Append('H');
            }
          }
          if (this.__minute != -1)
          {
            bool flag = true;
            if (canonicalize && this.__minute == 0)
              flag = this.__second == -1;
            if (flag)
            {
              sb.Append(this.__minute);
              if (this.__fraction != -1.0 && this.__second == -1)
                this.buildFraction(sb, this.__fractionNumberDigits);
              sb.Append('M');
            }
          }
          if (this.__second != -1)
          {
            sb.Append(this.__second);
            if (this.__fraction != -1.0)
              this.buildFraction(sb, this.__fractionNumberDigits);
            sb.Append('S');
          }
        }
      }
    }

    private void buildFraction(StringBuilder sb, int numberOfDecimalDigits)
    {
      sb.Append('.');
      if (numberOfDecimalDigits == -1)
      {
        if (this.__fraction == -1.0)
          sb.Append(Tools.fractionalPartAsString(0.0));
        else
          sb.Append(Tools.fractionalPartAsString(this.__fraction));
      }
      else if (this.__fraction == -1.0)
        sb.Append(Tools.fractionalPartAsString(0.0, numberOfDecimalDigits));
      else
        sb.Append(Tools.fractionalPartAsString(this.__fraction, numberOfDecimalDigits));
    }

    private string buildValue(bool canonicalize)
    {
      StringBuilder sb = new StringBuilder();
      if (this.settings[0] == 4 || this.settings[0] == -1 && this.__recurrence != -1)
      {
        sb.Append('R');
        if (this.settings[7] == 1)
        {
          string str = this.__recurrence.ToString();
          if (this.settings[11] > str.Length)
          {
            for (int index = 0; index < this.settings[11] - str.Length; ++index)
              sb.Append('0');
          }
          sb.Append(str);
        }
        else if (this.__recurrence != -2)
          sb.Append(this.__recurrence);
        sb.Append('/');
      }
      if (this.settings[0] == 3 || this.settings[0] == 4 || this.settings[0] == -1 && (this.__startPoint != null || this.__endPoint != null || this.__duration != null))
      {
        if (this.settings[5] == 1)
        {
          this.buildDuration(sb, canonicalize);
        }
        else
        {
          if (this.__startPoint != null)
          {
            this.__startPoint.buildDateTime(sb, canonicalize, true);
            sb.Append('/');
          }
          if (this.__duration != null)
          {
            this.__duration.buildDuration(sb, canonicalize);
            if (this.__endPoint != null)
              sb.Append('/');
          }
          if (this.__endPoint != null)
          {
            if (canonicalize && this.__startPoint != null && this.__endPoint.DifferenceFromUTC == this.__startPoint.DifferenceFromUTC)
              this.__endPoint.buildDateTime(sb, canonicalize, false);
            else
              this.__endPoint.buildDateTime(sb, canonicalize, true);
          }
        }
      }
      else if (this.settings[0] == 0 || this.settings[0] == 1 || this.settings[0] == 2 || this.settings[0] == -1 && (this.IsYearDefined() || this.__month != -1 || (this.__week != -1 || this.__day != -1) || (this.__hour != -1 || this.__minute != -1 || this.__second != -1)))
        this.buildDateTime(sb, canonicalize, true);
      return ((object) sb).ToString();
    }

    public override object Clone()
    {
      Asn1TimeType asn1TimeType = (Asn1TimeType) this.MemberwiseClone();
      asn1TimeType.StringValue = this.StringValue;
      return (object) asn1TimeType;
    }

    private void ensureTimeComponentsDefined()
    {
      if (this.isTimeComponentsDefined() || this.__value == null || this.__value.Length == 0)
        return;
      try
      {
        this.parseValue();
      }
      catch (Asn1Exception ex)
      {
      }
    }

    public override bool Equals(object anObject)
    {
      return this == anObject || anObject is Asn1TimeType && this.HasEqualValue((Asn1TimeType) anObject);
    }

    public DateTime GetDateTimeValue()
    {
      return new DateTime(this.__year, this.__month, this.__day, this.__hour, this.__minute, this.__second);
    }

    public override int GetHashCode()
    {
      string stringValue = this.GetStringValue(true);
      if (stringValue == null)
        return 0;
      else
        return stringValue.GetHashCode();
    }

    public int GetPropertySetting(int propertySetting)
    {
      return this.settings[propertySetting];
    }

    public string GetStringValue()
    {
      return this.GetStringValue(false);
    }

    public string GetStringValue(bool canonicalize)
    {
      if (this.__value != null)
      {
        if (!canonicalize)
          return this.__value;
        this.ensureTimeComponentsDefined();
      }
      if (this.isTimeComponentsDefined())
        return this.buildValue(canonicalize);
      else
        return "";
    }

    public virtual bool HasEqualValue(Asn1TimeType that)
    {
      if (that == null)
        return false;
      string stringValue1 = this.GetStringValue(true);
      string stringValue2 = that.GetStringValue(true);
      if (stringValue1 == null)
        return stringValue2 == null;
      else
        return stringValue1.Equals(stringValue2);
    }

    private bool isTimeComponentsDefined()
    {
      if (this.__year == -1 && this.__yearSetting == -1 && (this.__month == -1 && this.__week == -1) && (this.__day == -1 && this.__hour == -1 && (this.__minute == -1 && this.__second == -1)) && (this.__startPoint == null && this.__endPoint == null))
        return this.__duration != null;
      else
        return true;
    }

    public bool IsYearCentury()
    {
      this.ensureTimeComponentsDefined();
      return this.__yearSetting == 0;
    }

    public bool IsYearDefined()
    {
      this.ensureTimeComponentsDefined();
      return this.__yearSetting != -1;
    }

    private int parseDate(string val, int startIndex)
    {
      int length1 = val.Length;
      int startIndex1 = startIndex;
      int index1;
      if (this.settings[2] == 0 || this.settings[2] == 1 || this.settings[2] == 2)
      {
        int num = val.IndexOf('C');
        if (this.settings[1] == 0 || num != -1)
        {
          if (num == -1)
            throw new Asn1Exception(64, "the value '" + val + "' should contain a C to indicate a century");
          string s = val.Substring(startIndex1, num - startIndex1);
          try
          {
            this.Century = int.Parse(s);
          }
          catch (Exception ex)
          {
            throw new Asn1Exception(64, "century '" + s + "' is out of range");
          }
          index1 = num + 1;
        }
        else
        {
          if (startIndex1 + 4 > length1)
            throw new Asn1Exception(64, "year cannot be parsed because the value '" + val + "' does not contain enough characters");
          string s = val.Substring(startIndex1, 4);
          try
          {
            this.Year = int.Parse(s);
          }
          catch (Exception ex)
          {
            throw new Asn1Exception(64, "year '" + s + "' is out of range");
          }
          index1 = 4;
        }
      }
      else if (this.settings[2] != 3 && this.settings[2] != 4 && this.settings[2] != 5)
      {
        char ch = val[startIndex1];
        switch (ch)
        {
          case '+':
          case '-':
            ++startIndex1;
            break;
        }
        int length2 = 0;
        for (int index2 = startIndex1; index2 < length1 && ((int) val[index2] >= 48 && (int) val[index2] <= 57); ++index2)
          ++length2;
        string s = val.Substring(startIndex1, length2);
        try
        {
          int num = int.Parse(s);
          if ((int) ch == 45)
            num = -num;
          if (val.IndexOf('C') != -1)
            this.Century = num;
          else
            this.Year = num;
        }
        catch (Exception ex)
        {
          throw new Asn1Exception(64, "year '" + s + "' is out of range");
        }
        index1 = startIndex1 + length2;
      }
      else
      {
        char ch = val[0];
        if ((int) ch != 43 && (int) ch != 45)
          throw new Asn1Exception(64, "the value '" + val + "' should start with a '+' or '-'");
        bool flag = (int) ch == 45;
        int startIndex2 = startIndex1 + 1;
        int num1 = val.IndexOf('C');
        if (this.settings[1] == 0 || num1 != -1)
        {
          if (num1 == -1)
            throw new Asn1Exception(64, "the value '" + val + "' should contain a C to indicate a century");
          string s = val.Substring(startIndex2, num1 - startIndex2);
          try
          {
            int num2 = int.Parse(s);
            if (flag)
              num2 = -num2;
            this.Century = num2;
          }
          catch (Exception ex)
          {
            throw new Asn1Exception(64, "century '" + s + "' is out of range");
          }
          index1 = num1 + 1;
        }
        else if (this.settings[2] == 3)
        {
          if (startIndex2 + 4 > length1)
            throw new Asn1Exception(64, "year cannot be parsed because the value '" + val + "' does not contain enough characters");
          string s = val.Substring(startIndex2, 4);
          try
          {
            int num2 = int.Parse(s);
            if (flag)
              num2 = -num2;
            this.Year = num2;
          }
          catch (Exception ex)
          {
            throw new Asn1Exception(64, "year '" + s + "' is out of range");
          }
          index1 = 5;
        }
        else
        {
          int length2 = 0;
          char[] chArray = val.ToCharArray();
          for (int index2 = 1; index2 < length1 && ((int) chArray[index2] >= 48 && (int) chArray[index2] <= 57); ++index2)
            ++length2;
          string s = val.Substring(startIndex2, length2);
          try
          {
            int num2 = int.Parse(s);
            if (flag)
              num2 = -num2;
            this.Year = num2;
          }
          catch (Exception ex)
          {
            throw new Asn1Exception(64, "year '" + s + "' is out of range");
          }
          index1 = startIndex2 + length2;
        }
      }
      if (this.settings[1] == 2 || this.settings[1] == 3 || (this.settings[1] == 1 || this.settings[1] == -1) && (val.IndexOf('W') == -1 && index1 < length1 && (int) val[index1] == 45 && (index1 + 3 == length1 || index1 + 3 < length1 && (int) val[index1 + 3] == 45)))
      {
        if (index1 >= length1)
          throw new Asn1Exception(64, "month cannot be parsed because the value '" + val + "' does not contain enough characters");
        if ((int) val[index1] != 45)
        {
          throw new Asn1Exception(64, "the value '" + (object) val + "' should contain '-' separator at index '" + (string) (object) index1 + "'");
        }
        else
        {
          int startIndex2 = index1 + 1;
          if (startIndex2 + 2 > length1)
            throw new Asn1Exception(64, "month cannot be parsed because the value '" + val + "' does not contain enough characters");
          string s = val.Substring(startIndex2, 2);
          try
          {
            this.Month = int.Parse(s);
          }
          catch (Exception ex)
          {
            throw new Asn1Exception(64, "month '" + s + "' is out of range");
          }
          index1 = startIndex2 + 2;
        }
      }
      else if (this.settings[1] == 5 || this.settings[1] == 6 || (this.settings[1] == 1 || this.settings[1] == -1) && (index1 + 1 < length1 && (int) val[index1] == 45 && (int) val[index1 + 1] == 87))
      {
        if (index1 > length1)
          throw new Asn1Exception(64, "week cannot be parsed because the value '" + val + "' does not contain enough characters");
        if ((int) val[index1] != 45)
        {
          throw new Asn1Exception(64, "the value '" + (object) val + "' should contain '-' separator at index '" + (string) (object) index1 + "'");
        }
        else
        {
          int num1 = index1 + 1;
          if (num1 >= length1)
            throw new Asn1Exception(64, "week cannot be parsed because the value '" + val + "' does not contain enough characters");
          string str = val;
          int index2 = num1;
          int num2 = 1;
          int startIndex2 = index2 + num2;
          if ((int) str[index2] != 87)
            throw new Asn1Exception(64, "the value should contain a 'W' to indicate the week");
          if (startIndex2 + 2 > length1)
            throw new Asn1Exception(64, "week cannot be parsed because the value '" + val + "' does not contain enough characters");
          string s = val.Substring(startIndex2, 2);
          try
          {
            this.Week = int.Parse(s);
          }
          catch (Exception ex)
          {
            throw new Asn1Exception(64, "week '" + s + "' is out of range");
          }
          index1 = startIndex2 + 2;
        }
      }
      if (this.settings[1] == 6 || (this.settings[1] == 1 || this.settings[1] == -1) && (this.__week != -1 && index1 < length1 && (int) val[index1] == 45))
      {
        if (index1 >= length1)
          throw new Asn1Exception(64, "day cannot be parsed because the value '" + val + "' does not contain enough characters");
        if ((int) val[index1] != 45)
        {
          throw new Asn1Exception(64, "the value '" + (object) val + "' should contain '-' separator at index '" + (string) (object) index1 + "'");
        }
        else
        {
          int startIndex2 = index1 + 1;
          if (startIndex2 + 1 > length1)
            throw new Asn1Exception(64, "day cannot be parsed because the value '" + val + "' does not contain enough characters");
          string s = val.Substring(startIndex2, 1);
          try
          {
            this.Day = int.Parse(s);
          }
          catch (Exception ex)
          {
            throw new Asn1Exception(64, "day '" + s + "' is out of range");
          }
          return startIndex2 + 1;
        }
      }
      else if (this.settings[1] == 3 || (this.settings[1] == 1 || this.settings[1] == -1) && (this.__month != -1 && index1 < length1 && (int) val[index1] == 45))
      {
        if (index1 >= length1)
          throw new Asn1Exception(64, "day cannot be parsed because the value '" + val + "' does not contain enough characters");
        if ((int) val[index1] != 45)
        {
          throw new Asn1Exception(64, "the value '" + (object) val + "' should contain '-' separator at index '" + (string) (object) index1 + "'");
        }
        else
        {
          int startIndex2 = index1 + 1;
          if (startIndex2 + 2 > length1)
            throw new Asn1Exception(64, "day cannot be parsed because the value '" + val + "' does not contain enough characters");
          string s = val.Substring(startIndex2, 2);
          try
          {
            this.Day = int.Parse(s);
          }
          catch (Exception ex)
          {
            throw new Asn1Exception(64, "day '" + s + "' is out of range");
          }
          return startIndex2 + 2;
        }
      }
      else
      {
        if (this.settings[1] != 4 && (this.settings[1] != 1 && this.settings[1] != -1 || (index1 >= length1 || (int) val[index1] != 45)))
          return index1;
        if (index1 >= length1)
          throw new Asn1Exception(64, "day cannot be parsed because the value '" + val + "' does not contain enough characters");
        if ((int) val[index1] != 45)
        {
          throw new Asn1Exception(64, "the value '" + (object) val + "' should contain '-' separator at index '" + (string) (object) index1 + "'");
        }
        else
        {
          int startIndex2 = index1 + 1;
          if (startIndex2 + 3 > length1)
            throw new Asn1Exception(64, "day cannot be parsed because the value '" + val + "' does not contain enough characters");
          string s = val.Substring(startIndex2, 3);
          try
          {
            this.Day = int.Parse(s);
          }
          catch (Exception ex)
          {
            throw new Asn1Exception(64, "day '" + s + "'is out of range");
          }
          return startIndex2 + 3;
        }
      }
    }

    private void parseDateTime(string val, bool addDifferenceFromUTC)
    {
      int startIndex = 0;
      if (this.settings[0] == 0 || this.settings[0] == 2)
        startIndex = this.parseDate(val, startIndex);
      if (this.settings[0] == 1 || this.settings[0] == 2)
        this.parseTime(val, startIndex, addDifferenceFromUTC);
      if (this.settings[0] != -1)
        return;
      if (val.IndexOf('T') != -1)
      {
        int num = this.parseDate(val, 0);
        this.parseTime(val, num + 1, true);
      }
      else if (val.IndexOf('C') != -1)
      {
        this.parseDate(val, 0);
      }
      else
      {
        int num = val.IndexOf('+');
        if (num == -1)
          num = val.IndexOf('-');
        if (val.IndexOf(':') != -1 || val.IndexOf('.') != -1 || (val.IndexOf(',') != -1 || val.Length < 4 || num == 2))
          this.parseTime(val, startIndex, true);
        else
          this.parseDate(val, startIndex);
      }
    }

    protected void parseDuration(string val)
    {
      int num1 = 0;
      if (val.Length < 1)
        throw new Asn1Exception(64, "the value '" + val + "' cannot be parsed because it does not contain enough characters");
      if ((int) val[0] != 80)
        throw new Asn1Exception(64, "the value '" + val + "' should contain a 'P' to indicate a duration");
      int startIndex1 = 1;
      int num2 = val.IndexOf('W');
      if (num2 != -1)
      {
        string s = this.parseFraction(val.Substring(startIndex1, num2 - startIndex1));
        try
        {
          this.Week = int.Parse(s);
        }
        catch (Exception ex)
        {
          throw new Asn1Exception(64, "week '" + s + "' is out of range");
        }
        num1 = num2 + 1;
      }
      else
      {
        int num3 = val.IndexOf('T');
        int num4 = val.IndexOf('Y');
        if (num4 != -1)
        {
          string s = this.parseFraction(val.Substring(startIndex1, num4 - startIndex1));
          try
          {
            this.Year = int.Parse(s);
          }
          catch (Exception ex)
          {
            throw new Asn1Exception(64, "year '" + s + "' is out of range");
          }
          startIndex1 = num4 + 1;
        }
        int num5 = val.IndexOf('M');
        if (num3 != -1 && num5 > num3)
          num5 = -1;
        if (num5 != -1)
        {
          string s = this.parseFraction(val.Substring(startIndex1, num5 - startIndex1));
          try
          {
            this.Month = int.Parse(s);
          }
          catch (Exception ex)
          {
            throw new Asn1Exception(64, "month '" + s + "' is out of range");
          }
          startIndex1 = num5 + 1;
          if (this.__year == -1)
            this.Year = 0;
        }
        int num6 = val.IndexOf('D');
        if (num6 != -1)
        {
          string s = this.parseFraction(val.Substring(startIndex1, num6 - startIndex1));
          try
          {
            this.Day = int.Parse(s);
          }
          catch (Exception ex)
          {
            throw new Asn1Exception(64, "day '" + s + "' is out of range");
          }
          num1 = num6 + 1;
          if (this.__year == -1)
            this.Year = 0;
          if (this.__month == -1)
            this.__month = 0;
        }
        if (num3 != -1)
        {
          int startIndex2 = num3 + 1;
          int num7 = val.IndexOf('H', startIndex2);
          if (num7 != -1)
          {
            string s = this.parseFraction(val.Substring(startIndex2, num7 - startIndex2));
            try
            {
              this.Hour = int.Parse(s);
            }
            catch (Exception ex)
            {
              throw new Asn1Exception(64, "hour '" + s + "' is out of range");
            }
            startIndex2 = num7 + 1;
            if (this.__year == -1)
              this.Year = 0;
            if (this.__month == -1)
              this.__month = 0;
            if (this.__day == -1)
              this.__day = 0;
          }
          int num8 = val.IndexOf('M', startIndex2);
          if (num8 != -1)
          {
            string s = this.parseFraction(val.Substring(startIndex2, num8 - startIndex2));
            try
            {
              this.Minute = int.Parse(s);
            }
            catch (Exception ex)
            {
              throw new Asn1Exception(64, "minute '" + s + "' is out of range");
            }
            startIndex2 = num8 + 1;
            if (this.__year == -1)
              this.Year = 0;
            if (this.__month == -1)
              this.__month = 0;
            if (this.__day == -1)
              this.__day = 0;
            if (this.__hour == -1)
              this.__hour = 0;
          }
          int num9 = val.IndexOf('S', startIndex2);
          if (num9 != -1)
          {
            string s = this.parseFraction(val.Substring(startIndex2, num9 - startIndex2));
            try
            {
              this.Second = int.Parse(s);
            }
            catch (Exception ex)
            {
              throw new Asn1Exception(64, "second '" + s + "' is out of range");
            }
            num1 = num9 + 1;
            if (this.__year == -1)
              this.Year = 0;
            if (this.__month == -1)
              this.__month = 0;
            if (this.__day == -1)
              this.__day = 0;
            if (this.__hour == -1)
              this.__hour = 0;
            if (this.__minute == -1)
              this.__minute = 0;
          }
        }
      }
    }

    private void parseDurationEndInterval(string val, int index)
    {
      int num = val.IndexOf('/', index);
      if (num == -1)
        throw new Asn1Exception(64, "the value '" + val + "' should contain a '/' to mark the end of the duration");
      string val1 = val.Substring(index, num - index);
      this.__duration = new Asn1DurationType();
      this.Duration = this.__duration;
      this.__duration.parseDuration(val1);
      index = num + 1;
      this.__endPoint = new Asn1TimeType();
      this.EndPoint = this.__endPoint;
      this.__endPoint.parseDateTime(val.Substring(index, val.Length - index), true);
    }

    private string parseFraction(string val)
    {
      if (this.__fraction != -1.0)
        throw new Asn1Exception(64, "fraction can be set on the last time component only");
      string str = val;
      int length = val.IndexOf('.');
      if (length == -1)
        length = val.IndexOf(',');
      if (length != -1)
      {
        str = val.Substring(0, length);
        string s = "0." + val.Substring(length + 1);
        try
        {
          this.__fraction = double.Parse(s, (IFormatProvider) CultureInfo.InvariantCulture);
          this.__fractionNumberDigits = s.Length - 2;
        }
        catch (Exception ex)
        {
          throw new Asn1Exception(64, "fraction '" + s + "' is out of range");
        }
      }
      return str;
    }

    private void parseInterval(string val, int index)
    {
      if (this.settings[5] == 1)
      {
        if (index == 0)
          this.parseDuration(val);
        else
          this.parseDuration(val.Substring(index));
      }
      else if (this.settings[5] == 0)
        this.parseStartEndInterval(val, index);
      else if (this.settings[5] == 2)
        this.parseStartDurationInterval(val, index);
      else if (this.settings[5] == 3)
        this.parseDurationEndInterval(val, index);
      else if (val.IndexOf('/', index) != -1)
      {
        int num = val.IndexOf('P', index);
        if (num == -1)
          this.parseStartEndInterval(val, index);
        else if (num == index)
          this.parseDurationEndInterval(val, index);
        else
          this.parseStartDurationInterval(val, index);
      }
      else
      {
        if ((int) val[index] != 80)
          return;
        this.__duration = new Asn1DurationType();
        this.Duration = this.__duration;
        if (index == 0)
          this.__duration.parseDuration(val);
        else
          this.__duration.parseDuration(val.Substring(index));
      }
    }

    private int parseRecurrence(string val)
    {
      if ((int) val[0] != 82)
        throw new Asn1Exception(64, "the value '" + val + "' should contain a 'R' to indicate a recurrence at index 0");
      int num = val.IndexOf('/');
      switch (num)
      {
        case -1:
          throw new Asn1Exception(64, "the value '" + val + "' should contain a '/' to mark the end of the recurrence component");
        case 1:
          this.__recurrence = -2;
          break;
        default:
          string s = val.Substring(1, num - 1);
          try
          {
            this.__recurrence = int.Parse(s);
            break;
          }
          catch (Exception ex)
          {
            throw new Asn1Exception(64, "recurrence '" + s + "' is out of range");
          }
      }
      return num + 1;
    }

    private void parseStartDurationInterval(string val, int index)
    {
      this.__startPoint = new Asn1TimeType();
      this.StartPoint = this.__startPoint;
      int num = val.IndexOf('/', index);
      if (num == -1)
        throw new Asn1Exception(64, "the value '" + val + "' should contain a '/' to mark the end of the start point");
      this.__startPoint.parseDateTime(val.Substring(index, num - index), true);
      index = num + 1;
      this.__duration = new Asn1DurationType();
      this.Duration = this.__duration;
      this.__duration.parseDuration(val.Substring(index, val.Length - index));
    }

    private void parseStartEndInterval(string val, int index)
    {
      this.__startPoint = new Asn1TimeType();
      this.StartPoint = this.__startPoint;
      int num = val.IndexOf('/', index);
      if (num == -1)
        throw new Asn1Exception(64, "the value '" + val + "' should contain a '/' to mark the end of the start point");
      this.__startPoint.parseDateTime(val.Substring(index, num - index), true);
      index = num + 1;
      this.__endPoint = new Asn1TimeType();
      this.EndPoint = this.__endPoint;
      this.__endPoint.parseDateTime(val.Substring(index, val.Length - index), false);
      if (this.__endPoint.DifferenceFromUTC != int.MinValue || this.__endPoint.IsTimeUTC)
        return;
      this.__endPoint.DifferenceFromUTC = this.__startPoint.DifferenceFromUTC;
    }

    private void parseTime(string val, int startIndex, bool addDifferenceFromUTC)
    {
      int length = val.Length;
      int startIndex1 = startIndex;
      if (this.settings[0] == 2)
      {
        if (startIndex1 >= length)
          throw new Asn1Exception(64, "time cannot be parsed because the value '" + val + "' does not contain enough characters");
        if ((int) val[startIndex1] != 84)
          throw new Asn1Exception(64, "the value '" + (object) val + "' should contain 'T' separator at index '" + (string) (object) startIndex1 + "'");
        else
          ++startIndex1;
      }
      if (startIndex1 + 2 > length)
        throw new Asn1Exception(64, "hour cannot be parsed because the value '" + val + "' does not contain enough characters");
      string s1 = val.Substring(startIndex1, 2);
      try
      {
        this.Hour = int.Parse(s1);
      }
      catch (Exception ex)
      {
        throw new Asn1Exception(64, "hour '" + s1 + "' is out of range");
      }
      int index1 = startIndex1 + 2;
      if (this.settings[3] == 1 || this.settings[3] == 2 || (this.settings[3] == 4 || this.settings[3] == 5) || this.settings[3] == -1 && index1 < length && (int) val[index1] == 58)
      {
        if (index1 >= length)
          throw new Asn1Exception(64, "minute cannot be parsed because the value '" + val + "' does not contain enough characters");
        if ((int) val[index1] != 58)
        {
          throw new Asn1Exception(64, "the value '" + (object) val + "' should contain ':' separator at index '" + (string) (object) index1 + "'");
        }
        else
        {
          int startIndex2 = index1 + 1;
          if (startIndex2 + 2 > length)
            throw new Asn1Exception(64, "minute cannot be parsed because the value '" + val + "' does not contain enough characters");
          string s2 = val.Substring(startIndex2, 2);
          try
          {
            this.Minute = int.Parse(s2);
          }
          catch (Exception ex)
          {
            throw new Asn1Exception(64, "minute '" + s2 + "' is out of range");
          }
          index1 = startIndex2 + 2;
        }
      }
      if (this.settings[3] == 2 || this.settings[3] == 5 || this.settings[3] == -1 && index1 < length && (int) val[index1] == 58)
      {
        if (index1 >= length)
          throw new Asn1Exception(64, "second cannot be parsed because the value '" + val + "' does not contain enough characters");
        if ((int) val[index1] != 58)
        {
          throw new Asn1Exception(64, "the value '" + (object) val + "' should contain ':' separator at index '" + (string) (object) index1 + "'");
        }
        else
        {
          int startIndex2 = index1 + 1;
          if (startIndex2 + 2 > length)
            throw new Asn1Exception(64, "second cannot be parsed because the value '" + val + "' does not contain enough characters");
          string s2 = val.Substring(startIndex2, 2);
          try
          {
            this.Second = int.Parse(s2);
          }
          catch (Exception ex)
          {
            throw new Asn1Exception(64, "second '" + s2 + "' is out of range");
          }
          index1 = startIndex2 + 2;
        }
      }
      if (this.settings[3] == 3 || this.settings[3] == 4 || this.settings[3] == 5 || this.settings[3] == -1 && index1 < length && ((int) val[index1] == 46 || (int) val[index1] == 44))
      {
        StringBuilder stringBuilder = new StringBuilder();
        for (int index2 = index1 + 1; index2 < length && ((int) val[index2] >= 48 && (int) val[index2] <= 57); ++index2)
          stringBuilder.Append(val[index2]);
        this.FractionNumberDigits = stringBuilder.Length;
        if ((int) val[index1] != 46 && (int) val[index1] != 44)
        {
          throw new Asn1Exception(64, "the value '" + (object) val + "' should contain '.' or ',' separator for fraction at index '" + (string) (object) index1 + "'");
        }
        else
        {
          int num1 = index1 + 1;
          try
          {
            double num2 = (double) long.Parse(((object) stringBuilder).ToString());
            for (int index2 = 0; index2 < this.FractionNumberDigits; ++index2)
              num2 /= 10.0;
            this.Fraction = num2;
            this.FractionNumberDigits = stringBuilder.Length;
          }
          catch (Exception ex)
          {
            throw new Asn1Exception(64, "hour/minute/second fraction '" + ((object) stringBuilder).ToString() + "' is out of range");
          }
          index1 = num1 + this.FractionNumberDigits;
        }
      }
      if (this.settings[4] == 0)
      {
        if (index1 == length)
          return;
        throw new Asn1Exception(64, "A local time value should contain no other information after index '" + (object) index1 + "' in value '" + val + "'");
      }
      else if (this.settings[4] == 1 || this.settings[4] == -1 && index1 < length && (int) val[index1] == 90)
      {
        if (index1 + 1 != length || (int) val[index1] != 90)
          throw new Asn1Exception(64, "A UTC time value should contain 'Z' at index '" + (object) index1 + "' in value '" + val + "'");
        else
          this.IsTimeUTC = true;
      }
      else
      {
        if (this.settings[4] != 2 && (this.settings[4] != -1 || index1 >= length || (int) val[index1] != 43 && (int) val[index1] != 45) || !addDifferenceFromUTC && index1 == length)
          return;
        if (index1 + 3 > length)
        {
          throw new Asn1Exception(64, "A local with difference from UTC time value should contain a difference '+'/'-'hh[:mm] after index '" + (object) index1 + "' in value '" + val + "'");
        }
        else
        {
          char ch = val[index1];
          if ((int) ch != 43 && (int) ch != 45)
          {
            throw new Asn1Exception(64, "A local with difference from UTC time value should contain '+' or '-' for difference at index '" + (object) index1 + "' in value '" + val + "'");
          }
          else
          {
            int startIndex2 = index1 + 1;
            int num1 = val.IndexOf(':', startIndex2);
            string s2 = num1 == -1 ? val.Substring(startIndex2) : val.Substring(startIndex2, num1 - startIndex2);
            int num2;
            try
            {
              num2 = int.Parse(s2) * 60;
            }
            catch (Exception ex)
            {
              throw new Asn1Exception(64, "difference from UTC '" + s2 + "' is out of range");
            }
            if (num1 != -1)
            {
              string s3 = val.Substring(num1 + 1);
              try
              {
                num2 += int.Parse(s3);
              }
              catch (Exception ex)
              {
                throw new Asn1Exception(64, "difference from UTC minutes '" + s3 + "' is out of range");
              }
            }
            if ((int) ch == 45)
              num2 = -num2;
            this.DifferenceFromUTC = num2;
          }
        }
      }
    }

    private void parseValue()
    {
      string val = this.__value;
      if (val.Length >= 2 && (int) val[0] == 34)
      {
        if ((int) val[val.Length - 1] != 34)
          throw new Asn1Exception(64, "time value in " + val + " starting with a '\"' should end with '\"'");
        val = this.__value.Substring(1, this.__value.Length - 2);
        if (val.Length == 0)
          throw new Asn1Exception(64, "time value is empty");
      }
      int index = 0;
      if (this.settings[0] == 4)
        index = this.parseRecurrence(val);
      if (this.settings[0] == 4 || this.settings[0] == 3)
        this.parseInterval(val, index);
      else if (this.settings[0] == 0 || this.settings[0] == 1 || this.settings[0] == 2)
      {
        this.parseDateTime(val, true);
      }
      else
      {
        if ((int) val[0] == 82)
          index = this.parseRecurrence(val);
        if (val.IndexOf('/', index) != -1)
          this.parseInterval(val, index);
        else if ((int) val[index] == 80)
          this.parseInterval(val, index);
        else if (val.IndexOf('T') != -1)
        {
          int num = this.parseDate(val, index);
          this.parseTime(val, num + 1, true);
        }
        else if (val.IndexOf('C') != -1)
        {
          this.parseDate(val, index);
        }
        else
        {
          int num = val.IndexOf('+');
          if (num == -1)
            num = val.IndexOf('-');
          if (val.IndexOf(':') != -1 || val.IndexOf('.') != -1 || (val.IndexOf(',') != -1 || val.Length < 4 || num == 2))
            this.parseTime(val, index, true);
          else
            this.parseDate(val, index);
        }
      }
    }

    public override void ResetType()
    {
      this.__resetCommons();
      this.SetValue((string) null);
      this.ResetYear();
      this.Month = -1;
      this.Week = -1;
      this.Day = -1;
      this.Hour = -1;
      this.Minute = -1;
      this.Second = -1;
      this.DifferenceFromUTC = int.MinValue;
      this.IsTimeUTC = false;
      this.Recurrence = -1;
      this.Fraction = -1.0;
      this.FractionNumberDigits = -1;
      this.StartPoint = (Asn1TimeType) null;
      this.EndPoint = (Asn1TimeType) null;
      this.Duration = (Asn1DurationType) null;
      if (this.__getDefaultInstance() == null)
        return;
      this.__setTypeValue(this.__getDefaultInstance());
    }

    public void ResetYear()
    {
      this.__year = -1;
      this.__yearSetting = -1;
    }

    public void SetValue(DateTime dt)
    {
      this.__year = dt.Year;
      this.__month = dt.Month;
      this.__day = dt.Day;
      this.__hour = dt.Hour;
      this.__minute = dt.Minute;
      this.__second = dt.Second;
      if (dt.Millisecond <= 0)
        return;
      this.__fraction = (double) (dt.Millisecond / 1000);
    }

    public void SetValue(string val)
    {
      this.__value = val;
    }

    public override void Validate()
    {
      if (!this.isTimeComponentsDefined())
      {
        if (this.__value == null || this.__value.Length == 0)
          throw new Asn1ValidationException(64, "value is not defined in type <" + this.GetType().FullName + ">");
        try
        {
          this.parseValue();
        }
        catch (Asn1Exception ex)
        {
          throw new Asn1ValidationException(64, ex.Message + " in type <" + this.GetType().FullName + ">");
        }
      }
      if (this.settings[0] == 4 || this.settings[0] == 3)
      {
        if (this.settings[0] == 4 && this.__recurrence != -2 && this.__recurrence <= 0)
          throw new Asn1ValidationException(64, "recurrence '" + (object) this.__recurrence + "' must be defined (use Asn1TimeType.UNLIMITED_RECURRENCE or a non negative value) in type <" + this.GetType().FullName + ">");
        else if (this.settings[0] == 4 && this.settings[7] == 1 && this.__recurrence.ToString().Length > this.settings[11])
          throw new Asn1ValidationException(64, "recurrence '" + (object) this.__recurrence + "' must be defined on " + (string) (object) this.settings[11] + " digits in type <" + this.GetType().FullName + ">");
        else if (this.settings[5] == 1)
        {
          if (!this.IsYearDefined() && this.__month == -1 && (this.__week == -1 && this.__day == -1) && (this.__hour == -1 && this.__minute == -1 && this.__second == -1))
            throw new Asn1ValidationException(64, "at least one duration component should be defined in type <" + this.GetType().FullName + ">");
          if (this.__fraction != -1.0 && (this.__fractionNumberDigits == -1 || this.__fractionNumberDigits == 0))
            throw new Asn1ValidationException(64, "fraction number digits should be specified (and not 0) in type <" + this.GetType().FullName + ">");
        }
        else if (this.settings[5] == 2)
        {
          if (this.__startPoint == null)
            throw new Asn1ValidationException(64, "the start point is not defined in type <" + this.GetType().FullName + ">");
          if (this.__duration == null)
            throw new Asn1ValidationException(64, "the duration is not defined in type <" + this.GetType().FullName + ">");
          this.__startPoint.Validate();
          this.__duration.Validate();
        }
        else if (this.settings[5] == 0)
        {
          if (this.__startPoint == null)
            throw new Asn1ValidationException(64, "the start point is not defined in type <" + this.GetType().FullName + ">");
          if (this.__endPoint == null)
            throw new Asn1ValidationException(64, "the end point is not defined in type <" + this.GetType().FullName + ">");
          this.__startPoint.Validate();
          this.__endPoint.Validate();
        }
        else if (this.settings[5] == 3)
        {
          if (this.__duration == null)
            throw new Asn1ValidationException(64, "the duration is not defined in type <" + this.GetType().FullName + ">");
          if (this.__endPoint == null)
            throw new Asn1ValidationException(64, "the end point is not defined in type <" + this.GetType().FullName + ">");
          this.__duration.Validate();
          this.__endPoint.Validate();
        }
      }
      else
      {
        if (this.settings[0] == 2)
        {
          if (!this.IsYearDefined() && this.__month == -1 && (this.__week == -1 && this.__day == -1))
            throw new Asn1ValidationException(64, "a date (and a time) should be defined in type <" + this.GetType().FullName + "> with a Date-Time basic setting");
          if (this.__hour == -1 && this.__minute == -1 && this.__second == -1)
            throw new Asn1ValidationException(64, "a time (and a date) should be defined in type <" + this.GetType().FullName + "> with a Date-Time basic setting");
        }
        else if (this.settings[0] == 0)
        {
          if (!this.IsYearDefined() && this.__month == -1 && (this.__week == -1 && this.__day == -1))
            throw new Asn1ValidationException(64, "a date should be defined in type <" + this.GetType().FullName + "> with a Date basic setting");
        }
        else if (this.settings[0] == 1 && this.__hour == -1 && (this.__minute == -1 && this.__second == -1))
          throw new Asn1ValidationException(64, "a time should be defined in type <" + this.GetType().FullName + "> with a Time basic setting");
        if (this.settings[2] != -1 && this.IsYearDefined())
        {
          if (this.settings[2] == 0 || this.settings[2] == 1 || this.settings[2] == 2)
          {
            if (this.settings[1] != 0 && !this.IsYearCentury())
            {
              if (this.settings[2] == 1 && (this.__year < 0 || this.__year > 1581))
                throw new Asn1ValidationException(64, "year '" + (object) this.__year + "' should be in the range 0000 to 1581 in type <" + this.GetType().FullName + ">");
              else if (this.settings[2] == 0 && (this.__year < 1582 || this.__year > 9999))
                throw new Asn1ValidationException(64, "year '" + (object) this.__year + "' should be in the range 1582 to 9999 in type <" + this.GetType().FullName + ">");
              else if (this.settings[2] == 2 && (this.__year < 0 || this.__year > 9999))
                throw new Asn1ValidationException(64, "year '" + (object) this.__year + "' should be in the range 0000 to 9999 in type <" + this.GetType().FullName + ">");
            }
            else if (this.settings[2] == 1 && (this.__year < 0 || this.__year > 14))
              throw new Asn1ValidationException(64, "year '" + (object) this.__year + "' should be in the range 00 to 14 in type <" + this.GetType().FullName + ">");
            else if (this.settings[2] == 0 && (this.__year < 15 || this.__year > 99))
              throw new Asn1ValidationException(64, "year '" + (object) this.__year + "' should be in the range 15 to 99 in type <" + this.GetType().FullName + ">");
            else if (this.settings[2] == 2 && (this.__year < 0 || this.__year > 99))
              throw new Asn1ValidationException(64, "year '" + (object) this.__year + "' should be in the range 00 to 99 in type <" + this.GetType().FullName + ">");
          }
          else if (this.settings[2] == 3 || this.settings[2] == 4 || this.settings[2] == 5)
          {
            if (this.settings[1] == 0 || this.IsYearCentury())
            {
              if (this.settings[2] != 3)
              {
                if (this.settings[2] == 5)
                {
                  if (0 <= this.__year && this.__year <= 99)
                    throw new Asn1ValidationException(64, "year '" + (object) this.__year + "' should not be in the range 00 to 99 and should be defined on " + (string) (object) (this.settings[9] - 2) + " digits at most in type <" + this.GetType().FullName + ">");
                }
                else if (-99 <= this.__year && this.__year <= 99)
                  throw new Asn1ValidationException(64, "year '" + (object) this.__year + "' should not be in the range -99 to 99 and should be defined on " + (string) (object) (this.settings[9] - 2) + " digits at most in type <" + this.GetType().FullName + ">");
                if (this.__year < -99 || this.__year > 99)
                {
                  int length = this.__year.ToString().Length;
                  if (this.__year < 0)
                    --length;
                  if (length > this.settings[9] - 2)
                    throw new Asn1ValidationException(64, "year '" + (object) this.__year + "' should be defined on " + (string) (object) (this.settings[9] - 2) + " digits at most in type <" + this.GetType().FullName + ">");
                }
              }
              else if (this.__year < -99 || this.__year > -1)
                throw new Asn1ValidationException(64, "year '" + (object) this.__year + "' should be in the range -99 to -01 in type <" + this.GetType().FullName + ">");
            }
            else if (this.settings[2] == 3)
            {
              if (this.__year < -9999 || this.__year > -1)
                throw new Asn1ValidationException(64, "year '" + (object) this.__year + "' should be in the range -9999 to -0001 in type <" + this.GetType().FullName + ">");
            }
            else
            {
              if (this.settings[2] == 5)
              {
                if (0 <= this.__year && this.__year <= 9999)
                  throw new Asn1ValidationException(64, "year '" + (object) this.__year + "' should not be in the range 0000 to 9999 and should be defined on " + (string) (object) this.settings[9] + " digits at most in type <" + this.GetType().FullName + ">");
              }
              else if (-9999 <= this.__year && this.__year <= 9999)
                throw new Asn1ValidationException(64, "year '" + (object) this.__year + "' should not be in the range -9999 to 9999 and should be defined on " + (string) (object) this.settings[9] + " digits at most in type <" + this.GetType().FullName + ">");
              if (this.__year < -9999 || this.__year > 9999)
              {
                int length = this.__year.ToString().Length;
                if (this.__year < 0)
                  --length;
                if (length > this.settings[9])
                  throw new Asn1ValidationException(64, "year '" + (object) this.__year + "' should be defined on " + (string) (object) this.settings[9] + " digits at most in type <" + this.GetType().FullName + ">");
              }
            }
          }
        }
        if (this.settings[1] != -1 && (this.IsYearDefined() || this.__month != -1 || (this.__week != -1 || this.__day != -1)))
        {
          if (!this.IsYearDefined())
            throw new Asn1ValidationException(64, "a year should be defined in type <" + this.GetType().FullName + ">");
          if (this.settings[1] == 1 || this.settings[1] == 0)
          {
            if (this.__week != -1 || this.__month != -1 || this.__day != -1)
              throw new Asn1ValidationException(64, "only a year should be defined in type <" + this.GetType().FullName + ">");
          }
          else if (this.settings[1] == 5)
          {
            if (this.__month != -1 || this.__day != -1)
              throw new Asn1ValidationException(64, "only year and week should be defined in type <" + this.GetType().FullName + ">");
            if (this.__week < 1 || this.__week > 53)
              throw new Asn1ValidationException(64, "week '" + (object) this.__week + "' should be in the range 1 to 53 in type <" + this.GetType().FullName + ">");
          }
          else if (this.settings[1] == 6)
          {
            if (this.__month != -1)
              throw new Asn1ValidationException(64, "only year, week and day should be defined in type <" + this.GetType().FullName + ">");
            if (this.__week < 1 || this.__week > 53)
              throw new Asn1ValidationException(64, "week '" + (object) this.__week + "' should be in the range 1 to 53 in type <" + this.GetType().FullName + ">");
            else if (this.__day < 1 || this.__day > 7)
              throw new Asn1ValidationException(64, "day '" + (object) this.__day + "' should be in the range 1 to 7 in type <" + this.GetType().FullName + ">");
          }
          else if (this.settings[1] == 2)
          {
            if (this.__week != -1 || this.__day != -1)
              throw new Asn1ValidationException(64, "only month and day should be defined in type <" + this.GetType().FullName + ">");
            if (this.__month < 1 || this.__month > 12)
              throw new Asn1ValidationException(64, "month '" + (object) this.__month + "' should be in the range 1 to 12 in type <" + this.GetType().FullName + ">");
          }
          else if (this.settings[1] == 3)
          {
            if (this.__week != -1)
              throw new Asn1ValidationException(64, "only year, month and day should be defined in type <" + this.GetType().FullName + ">");
            if (this.__month < 1 || this.__month > 12)
              throw new Asn1ValidationException(64, "month '" + (object) this.__month + "' should be in the range 1 to 12 in type <" + this.GetType().FullName + ">");
            else if (this.__day < 1 || this.__day > 31)
              throw new Asn1ValidationException(64, "day '" + (object) this.__day + "' should be in the range 1 to 31 in type <" + this.GetType().FullName + ">");
          }
          else if (this.settings[1] == 4)
          {
            if (this.__week != -1 || this.__month != -1)
              throw new Asn1ValidationException(64, "only year and day should be defined in type <" + this.GetType().FullName + ">");
            if (this.__day < 1 || this.__day > 366)
              throw new Asn1ValidationException(64, "day '" + (object) this.__day + "' should be in the range 1 to 366 in type <" + this.GetType().FullName + ">");
          }
        }
        if (this.settings[3] != -1 && (this.__hour != -1 || this.__minute != -1 || this.__second != -1))
        {
          if (this.__hour < 0 || this.__hour > 24)
            throw new Asn1ValidationException(64, "hour '" + (object) this.__hour + "' should be in the range 00 to 24 in type <" + this.GetType().FullName + ">");
          else if (this.settings[3] == 0 || this.settings[3] == 3)
          {
            if (this.__minute != -1 || this.__second != -1)
              throw new Asn1ValidationException(64, "only hour should be defined in type <" + this.GetType().FullName + ">");
          }
          else if (this.settings[3] == 1 || this.settings[3] == 4)
          {
            if (this.__minute < 0 || this.__minute >= 60)
              throw new Asn1ValidationException(64, "minute '" + (object) this.__minute + "' should be in the range 00 to 59 in type <" + this.GetType().FullName + ">");
            else if (this.__second != -1)
              throw new Asn1ValidationException(64, "only hour and minute should be defined in type <" + this.GetType().FullName + ">");
          }
          else if (this.settings[3] == 2 || this.settings[3] == 5)
          {
            if (this.__minute < 0 || this.__minute >= 60)
              throw new Asn1ValidationException(64, "minute '" + (object) this.__minute + "' should be in the range 00 to 59 in type <" + this.GetType().FullName + ">");
            else if (this.__second < 0 || this.__second > 60)
              throw new Asn1ValidationException(64, "second '" + (object) this.__second + "' should be in the range 00 to 60 in type <" + this.GetType().FullName + ">");
          }
        }
        if (this.settings[8] == 0 && this.__hour == 24 && (this.__minute == 0 && this.__second == 0) && (this.__fraction == -1.0 || this.__fraction == 0.0))
          throw new Asn1ValidationException(64, "hour should be 00 for Midnight=Start in type <" + this.GetType().FullName + ">");
        if (this.settings[8] == 1 && this.__hour == 0 && (this.__minute == 0 && this.__second == 0) && (this.__fraction == -1.0 || this.__fraction == 0.0))
          throw new Asn1ValidationException(64, "hour should be 24 for Midnight=End in type <" + this.GetType().FullName + ">");
        if (this.__fraction != -1.0)
        {
          if (this.__fraction < 0.0 || this.__fraction >= 1.0)
            throw new Asn1ValidationException(64, "fraction '" + (object) this.__fraction + "' must be between 0 and 1 excluded in type <" + this.GetType().FullName + ">");
          else if (this.settings[3] == 3 || this.settings[3] == 4 || this.settings[3] == 5)
          {
            if (this.__fractionNumberDigits != -1 && this.settings[10] != -1 && this.settings[10] != this.__fractionNumberDigits)
              throw new Asn1ValidationException(64, "fraction number digits should be '" + (object) this.settings[10] + "' in type <" + this.GetType().FullName + ">");
          }
          else if (this.__fractionNumberDigits == -1 || this.__fractionNumberDigits == 0)
            throw new Asn1ValidationException(64, "fraction number digits should be specified (and not 0) in type <" + this.GetType().FullName + ">");
        }
        if (this.__differenceFromUTC == int.MinValue || this.__differenceFromUTC <= 900 || this.__differenceFromUTC >= -900)
          return;
        throw new Asn1ValidationException(64, "difference from UTC '" + (object) this.__differenceFromUTC + "' should be in the range -15*60 to +15*60 (expressed in minutes) in type <" + this.GetType().FullName + ">");
      }
    }
  }
}
