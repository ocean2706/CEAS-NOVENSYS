// Decompiled with JetBrains decompiler
// Type: Novensys.ASN1.Type.Asn1UTCTimeType
// Assembly: Novensys.eCard.SDK, Version=1.1.56.0, Culture=neutral, PublicKeyToken=null
// MVID: 59F9E456-D3DA-4066-A4A4-692B516A775E
// Assembly location: C:\Program Files (x86)\CNAS\SIUI-SPITAL\Novensys.eCard.SDK.dll

using Novensys.ASN1;
using System;
using System.ComponentModel;
using System.Globalization;
using System.Text;

namespace Novensys.ASN1.Type
{
  [Serializable]
  public class Asn1UTCTimeType : Asn1VisibleStringType
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
        return "UTCTime";
      }
    }

    public Asn1UTCTimeType()
    {
    }

    public Asn1UTCTimeType(DateTime dateTime)
    {
      this.SetValue(dateTime);
    }

    public Asn1UTCTimeType(string str)
      : base(str)
    {
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal override int __getUniversalTagNumber()
    {
      return 23;
    }

    protected internal override void __setTypeValue(Asn1Type typeInstance)
    {
      if (typeInstance == null)
      {
        this.ResetType();
      }
      else
      {
        if (!(typeInstance is Asn1UTCTimeType))
          return;
        base.SetValue(((Asn1StringType) typeInstance).GetStringValue());
      }
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal override void __write(Asn1TypeBerWriter writer)
    {
      writer.__encodeUTCTimeType(this, this.__getUniversalTagNumber(), 1);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal override void __write(Asn1TypeXerWriter writer)
    {
      writer.__encodeUTCTimeType(this);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal override void __write(Asn1TypeBerWriter writer, int tagNumber, int tagClass)
    {
      writer.__encodeUTCTimeType(this, tagNumber, tagClass);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal override string __writeValue(Asn1TypeXerWriter writer)
    {
      return writer.__encodeUTCTimeValue(this);
    }

    public override bool Equals(object anObject)
    {
      return this == anObject || anObject is Asn1UTCTimeType && this.HasEqualValue((Asn1StringType) anObject);
    }

    public DateTime GetDateTimeValue()
    {
      int[] numArray = this.parseValue();
      int num = numArray[0];
      int year = num + (num <= 50 ? 2000 : 1900);
      DateTime dateTime1;
      try
      {
        DateTime dateTime2 = new DateTime(year, numArray[1], numArray[2], numArray[3], numArray[4], numArray[5], 0);
        if (numArray[6] != 0)
          dateTime2 = dateTime2.AddMinutes((double) -numArray[6]);
        TimeSpan utcOffset = TimeZone.CurrentTimeZone.GetUtcOffset(DateTime.Now);
        dateTime1 = dateTime2 + utcOffset;
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
      if (numArray[6] != 0)
      {
        bool flag = year <= 1;
        DateTime dateTime = !flag ? new DateTime(year, month, day, hour, minute, second) : new DateTime(2, month, day, hour, minute, second);
        dateTime = dateTime.AddMinutes((double) -numArray[6]);
        if (flag)
        {
          year -= 2 - dateTime.Year;
          if (year < 0)
            throw new Asn1Exception(53, "canonicalization is not possible in <" + this.__value + "> in type <" + this.GetType().FullName + ">");
        }
        else
          year = dateTime.Year;
        month = dateTime.Month;
        day = dateTime.Day;
        hour = dateTime.Hour;
        minute = dateTime.Minute;
        second = dateTime.Second;
      }
      StringBuilder stringBuilder = new StringBuilder();
      if (year < 10)
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
      int[] numArray = new int[7];
      for (int index = 0; index < numArray.Length; ++index)
        numArray[index] = 0;
      int num1;
      try
      {
        num1 = int.Parse(this.__value.Substring(0, 2));
      }
      catch (Exception ex)
      {
        throw new FormatException("Year cannot be parsed in <" + this.__value + ">");
      }
      numArray[0] = num1;
      int num2;
      try
      {
        num2 = int.Parse(this.__value.Substring(2, 2));
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
        num3 = int.Parse(this.__value.Substring(4, 2));
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
        num4 = int.Parse(this.__value.Substring(6, 2));
      }
      catch (Exception ex)
      {
        throw new FormatException("Hour cannot be parsed in <" + this.__value + ">");
      }
      if (num4 < 0 || num4 > 23)
        throw new FormatException("Hour should be between 0 and 23 [" + (object) num4 + "]");
      numArray[3] = num4;
      int num5;
      try
      {
        num5 = int.Parse(this.__value.Substring(8, 2));
      }
      catch (Exception ex)
      {
        throw new FormatException("Minute cannot be parsed in <" + this.__value + ">");
      }
      if (num5 < 0 || num5 > 59)
        throw new FormatException("Minute should be between 0 and 59 [" + (object) num5 + "]");
      numArray[4] = num5;
      int num6 = this.__value.IndexOf('+');
      int num7 = this.__value.IndexOf('-');
      int num8 = num6 != -1 ? num6 : num7;
      int num9 = this.__value.IndexOf('Z');
      if (num9 != -1 && num9 != length - 1)
        throw new FormatException("Z marker should be at the end [" + (object) num9 + "]");
      if (num8 != -1 && num8 + 3 != length && num8 + 5 != length)
        throw new FormatException("UTC offset should stand on 2 or 4 digits [sign index is <" + (object) num8 + ">]");
      if (length > 10 && (int) this.__value[10] != 90 && ((int) this.__value[10] != 43 && (int) this.__value[10] != 45))
      {
        int num10;
        try
        {
          num10 = int.Parse(this.__value.Substring(10, 2));
        }
        catch (Exception ex)
        {
          throw new FormatException("Second cannot be parsed in <" + this.__value + ">");
        }
        if (num10 < 0 || num10 > 59)
          throw new FormatException("Second should be between 0 and 59 [" + (object) num10 + "]");
        numArray[5] = num10;
      }
      int num11 = 0;
      if (num8 != -1)
      {
        try
        {
          if (length >= num8 + 1)
            num11 = int.Parse(this.__value.Substring(num8 + 1, 2)) * 60;
          if (length == num8 + 5)
            num11 += int.Parse(this.__value.Substring(num8 + 3, 2));
        }
        catch (Exception ex)
        {
          throw new FormatException("Difference from UTC cannot be parsed in <" + this.__value + ">");
        }
        if (num7 != -1)
          num11 = -num11;
      }
      numArray[6] = num11;
      return numArray;
    }

    public void SetValue(DateTime dt)
    {
      TimeSpan utcOffset = TimeZone.CurrentTimeZone.GetUtcOffset(dt);
      this.__value = (dt - utcOffset).ToString("yyMMddHHmmss'Z'", (IFormatProvider) DateTimeFormatInfo.InvariantInfo);
    }

    public override void Validate()
    {
      try
      {
        this.parseValue();
      }
      catch (Exception ex)
      {
        throw new Asn1ValidationException(53, "<" + this.__value + "> in type <" + this.GetType().FullName + "> [" + ex.Message + "]");
      }
    }
  }
}
