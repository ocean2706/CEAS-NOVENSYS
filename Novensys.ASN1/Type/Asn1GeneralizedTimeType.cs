// Decompiled with JetBrains decompiler
// Type: Novensys.ASN1.Type.Asn1GeneralizedTimeType
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
  public class Asn1GeneralizedTimeType : Asn1VisibleStringType
  {
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

    public override string TypeName
    {
      get
      {
        return "GeneralizedTime";
      }
    }

    public Asn1GeneralizedTimeType()
    {
    }

    public Asn1GeneralizedTimeType(DateTime dateTime)
    {
      this.SetValue(dateTime);
    }

    public Asn1GeneralizedTimeType(string str)
      : base(str)
    {
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal override int __getUniversalTagNumber()
    {
      return 24;
    }

    protected internal override void __setTypeValue(Asn1Type typeInstance)
    {
      if (typeInstance == null)
      {
        this.ResetType();
      }
      else
      {
        if (!(typeInstance is Asn1GeneralizedTimeType))
          return;
        base.SetValue(((Asn1StringType) typeInstance).GetStringValue());
      }
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal override void __write(Asn1TypeBerWriter writer)
    {
      writer.__encodeGeneralizedTimeType(this, this.__getUniversalTagNumber(), 1);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal override void __write(Asn1TypeXerWriter writer)
    {
      writer.__encodeGeneralizedTimeType(this);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal override void __write(Asn1TypeBerWriter writer, int tagNumber, int tagClass)
    {
      writer.__encodeGeneralizedTimeType(this, tagNumber, tagClass);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal override string __writeValue(Asn1TypeXerWriter writer)
    {
      return writer.__encodeGeneralizedTimeValue(this);
    }

    public override bool Equals(object anObject)
    {
      return this == anObject || anObject is Asn1GeneralizedTimeType && this.HasEqualValue((Asn1StringType) anObject);
    }

    public DateTime GetDateTimeValue()
    {
      int[] numArray = this.parseValue();
      DateTime dateTime1;
      try
      {
        DateTime dateTime2 = new DateTime(numArray[0], numArray[1], numArray[2], numArray[3], numArray[4], numArray[5], numArray[6]);
        if (numArray[10] == 0)
        {
          if (numArray[9] != 0)
            dateTime2 = dateTime2.AddMinutes((double) -numArray[9]);
          TimeSpan utcOffset = TimeZone.CurrentTimeZone.GetUtcOffset(DateTime.Now);
          dateTime2 += utcOffset;
        }
        dateTime1 = dateTime2;
      }
      catch (Exception ex)
      {
        throw new FormatException("invalid format for DateTime: " + ex.Message);
      }
      return dateTime1;
    }

    public override int GetHashCode()
    {
      return base.GetHashCode();
    }

    public string GetStringValue(bool canonicalize)
    {
      if (!canonicalize)
        return this.__value;
      int[] numArray = this.parseValue();
      int year = numArray[0];
      int month = numArray[1];
      int day = numArray[2];
      int hour = numArray[3];
      int minute = numArray[4];
      int second = numArray[5];
      int millisecond = numArray[6];
      if (numArray[10] == 1 || numArray[9] != 0)
      {
        bool flag = year <= 1;
        DateTime dateTime1 = !flag ? new DateTime(year, month, day, hour, minute, second, millisecond) : new DateTime(2, month, day, hour, minute, second, millisecond);
        DateTime dateTime2;
        if (numArray[10] == 1)
        {
          TimeSpan utcOffset = TimeZone.CurrentTimeZone.GetUtcOffset(DateTime.Now);
          dateTime2 = dateTime1 - utcOffset;
        }
        else
          dateTime2 = dateTime1.AddMinutes((double) -numArray[9]);
        if (flag)
        {
          year -= 2 - dateTime2.Year;
          if (year < 0)
            throw new Asn1Exception(52, "canonicalization is not possible in <" + this.__value + "> in type <" + this.GetType().FullName + ">");
        }
        else
          year = dateTime2.Year;
        month = dateTime2.Month;
        day = dateTime2.Day;
        hour = dateTime2.Hour;
        minute = dateTime2.Minute;
        second = dateTime2.Second;
      }
      StringBuilder stringBuilder = new StringBuilder();
      if (year < 10)
        stringBuilder.Append("0");
      if (year < 100)
        stringBuilder.Append("0");
      if (year < 1000)
        stringBuilder.Append("0");
      stringBuilder.Append(year);
      if (month < 10)
        stringBuilder.Append("0");
      stringBuilder.Append(month);
      if (day < 10)
        stringBuilder.Append("0");
      stringBuilder.Append(day);
      if (hour < 10)
        stringBuilder.Append("0");
      stringBuilder.Append(hour);
      if (minute < 10)
        stringBuilder.Append("0");
      stringBuilder.Append(minute);
      if (second < 10)
        stringBuilder.Append("0");
      stringBuilder.Append(second);
      if (numArray[7] > 0)
      {
        stringBuilder.Append(".");
        for (int index = 0; index < numArray[8]; ++index)
          stringBuilder.Append("0");
        int num = numArray[7];
        while (num % 10 == 0)
          num /= 10;
        stringBuilder.Append(num);
      }
      stringBuilder.Append("Z");
      return ((object) stringBuilder).ToString();
    }

    private int[] parseValue()
    {
      if (this.__value == null)
        throw new FormatException("Internal value is null.");
      int length = this.__value.Length;
      if (length < 10)
        throw new FormatException("Size should be greater than or equal to 10 instead of " + (object) length);
      int[] numArray = new int[11];
      for (int index = 0; index < numArray.Length; ++index)
        numArray[index] = 0;
      int num1;
      try
      {
        num1 = int.Parse(this.__value.Substring(0, 4));
      }
      catch (Exception ex)
      {
        throw new FormatException("Year cannot be parsed in <" + this.__value + ">");
      }
      numArray[0] = num1;
      int num2;
      try
      {
        num2 = int.Parse(this.__value.Substring(4, 2));
      }
      catch (Exception ex)
      {
        throw new FormatException("Month cannot be parsed in <" + this.__value + ">");
      }
      if (num2 < 1 || num2 > 12)
        throw new FormatException("Month should be between 1 and 12 [" + (object) num2 + "]");
      numArray[1] = num2;
      int num3;
      try
      {
        num3 = int.Parse(this.__value.Substring(6, 2));
      }
      catch (Exception ex)
      {
        throw new FormatException("Day cannot be parsed in <" + this.__value + ">");
      }
      if (num3 < 1 || num3 > 31)
        throw new FormatException("Day should be between 1 and 31 [" + (object) num3 + "]");
      numArray[2] = num3;
      int num4;
      try
      {
        num4 = int.Parse(this.__value.Substring(8, 2));
      }
      catch (Exception ex)
      {
        throw new FormatException("Hour cannot be parsed in <" + this.__value + ">");
      }
      if (num4 < 0 || num4 > 23)
        throw new FormatException("Hour should be between 0 and 23 [" + (object) num4 + "]");
      numArray[3] = num4;
      int num5 = 0;
      double val = 0.0;
      int num6 = this.__value.IndexOf('+');
      int num7 = this.__value.IndexOf('-');
      int num8 = num6 != -1 ? num6 : num7;
      int num9 = this.__value.IndexOf('Z');
      if (num9 != -1 && num9 != length - 1)
        throw new FormatException("Z marker should be at the end [" + (object) num9 + "]");
      if (num8 != -1 && num8 + 3 != length && num8 + 5 != length)
        throw new FormatException("UTC offset should stand on 2 or 4 digits [sign index is <" + (object) num8 + ">]");
      bool flag = (int) this.__value[length - 1] != 90 && num8 == -1;
      int num10 = this.__value.IndexOf('.');
      if (num10 == -1)
        num10 = this.__value.IndexOf(',');
      string str = (string) null;
      if (num10 != -1)
      {
        try
        {
          int num11 = num8 == -1 ? ((int) this.__value[length - 1] == 90 ? length - 1 : length) : num8;
          str = this.__value.Substring(num10 + 1, num11 - (num10 + 1));
          val = double.Parse("0." + str, (IFormatProvider) CultureInfo.InvariantCulture);
        }
        catch (Exception ex)
        {
          Console.WriteLine(ex.StackTrace);
          throw new FormatException("Fraction cannot be parsed in <" + this.__value + ">");
        }
      }
      if (length > 10 && (int) this.__value[10] != 90 && ((int) this.__value[10] != 43 && (int) this.__value[10] != 45))
      {
        if (str != null && ((int) this.__value[10] == 46 || (int) this.__value[10] == 44))
        {
          double num11 = val * 3600.0;
          int num12 = (int) num11;
          if (num12 >= 60)
          {
            num5 = num12 / 60;
            num12 -= num5 * 60;
          }
          numArray[4] = num5;
          numArray[5] = num12;
          val = num11 - (double) (num5 * 60) - (double) num12;
        }
        else
        {
          int num11;
          try
          {
            num11 = int.Parse(this.__value.Substring(10, 2));
          }
          catch (Exception ex)
          {
            throw new FormatException("Minute cannot be parsed in <" + this.__value + ">");
          }
          if (num11 < 0 || num11 > 59)
            throw new FormatException("Minute should be between 0 and 59 [" + (object) num11 + "]");
          numArray[4] = num11;
          if (length > 12 && (int) this.__value[12] != 90 && ((int) this.__value[12] != 43 && (int) this.__value[12] != 45))
          {
            if (str != null && ((int) this.__value[12] == 46 || (int) this.__value[12] == 44))
            {
              double num12 = val * 60.0;
              int num13 = (int) num12;
              numArray[5] = num13;
              val = num12 - (double) num13;
            }
            else
            {
              int num12;
              try
              {
                num12 = int.Parse(this.__value.Substring(12, 2));
              }
              catch (Exception ex)
              {
                throw new FormatException("Second cannot be parsed in <" + this.__value + ">");
              }
              if (num12 < 0 || num12 > 59)
                throw new FormatException("Second should be between 0 and 59 [" + (object) num12 + "]");
              numArray[5] = num12;
              if (length > 15 && str != null && ((int) this.__value[14] == 46 || (int) this.__value[14] == 44))
              {
                try
                {
                  val = double.Parse("0." + str, (IFormatProvider) CultureInfo.InvariantCulture);
                }
                catch (Exception ex)
                {
                  throw new FormatException("Fraction cannot be parsed in <" + this.__value + ">");
                }
              }
            }
          }
        }
        if (val > 0.0)
        {
          try
          {
            string s = Tools.fractionalPartAsString(val, 9);
            int num11 = (int) (val * 1000.0);
            int num12 = 0;
            for (int index = 0; index < s.Length; ++index)
            {
              if (s.StartsWith("0"))
              {
                ++num12;
                s = s.Substring(1);
              }
            }
            int num13 = int.Parse(s);
            numArray[6] = num11;
            numArray[7] = num13;
            numArray[8] = num12;
          }
          catch (Exception ex)
          {
            throw new FormatException("Fraction cannot be parsed in <" + this.__value + ">");
          }
        }
      }
      int num14 = 0;
      if (num8 != -1)
      {
        try
        {
          if (length >= num8 + 1)
            num14 = int.Parse(this.__value.Substring(num8 + 1, 2)) * 60;
          if (length == num8 + 5)
            num14 += int.Parse(this.__value.Substring(num8 + 3, 2));
        }
        catch (Exception ex)
        {
          throw new FormatException("Difference from UTC cannot be parsed in <" + this.__value + ">");
        }
        if (num7 != -1)
          num14 = -num14;
      }
      numArray[9] = num14;
      numArray[10] = flag ? 1 : 0;
      return numArray;
    }

    public void SetValue(DateTime dt)
    {
      TimeSpan utcOffset = TimeZone.CurrentTimeZone.GetUtcOffset(dt);
      DateTime dateTime = dt - utcOffset;
      this.__value = dateTime.ToString("yyyyMMddHHmmss", (IFormatProvider) DateTimeFormatInfo.InvariantInfo);
      int millisecond = dateTime.Millisecond;
      if (millisecond > 0)
      {
        if (millisecond % 100 == 0)
          this.__value = this.__value + "." + (millisecond / 100).ToString();
        else if (millisecond % 10 == 0)
        {
          this.__value = this.__value + ".0" + (millisecond / 10).ToString();
        }
        else
        {
          string str = millisecond.ToString();
          if (str.Length == 1)
            this.__value = this.__value + ".00" + str;
          else if (str.Length == 2)
            this.__value = this.__value + ".0" + str;
          else
            this.__value = this.__value + "." + str;
        }
      }
      this.__value = this.__value + "Z";
    }

    public override void Validate()
    {
      try
      {
        this.parseValue();
      }
      catch (Exception ex)
      {
        throw new Asn1ValidationException(52, "<" + this.__value + "> in type <" + this.GetType().FullName + "> [" + ex.Message + "]");
      }
    }
  }
}
