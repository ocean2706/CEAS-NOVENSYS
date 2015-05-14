// Decompiled with JetBrains decompiler
// Type: Novensys.ASN1.Type.Per.Time.Asn1TimeTypeAdapterFactory
// Assembly: Novensys.eCard.SDK, Version=1.1.56.0, Culture=neutral, PublicKeyToken=null
// MVID: 59F9E456-D3DA-4066-A4A4-692B516A775E
// Assembly location: C:\Program Files (x86)\CNAS\SIUI-SPITAL\Novensys.eCard.SDK.dll

using Novensys.ASN1.Type;
using System;

namespace Novensys.ASN1.Type.Per.Time
{
  public class Asn1TimeTypeAdapterFactory
  {
    private static int[] MIXED_DATE_List = new int[14]
    {
      1,
      2,
      3,
      4,
      5,
      6,
      7,
      8,
      9,
      10,
      11,
      12,
      13,
      14
    };
    private static int[] MIXED_ENCODING_List = new int[53]
    {
      1,
      2,
      3,
      4,
      5,
      6,
      7,
      8,
      9,
      10,
      11,
      12,
      13,
      14,
      15,
      16,
      17,
      18,
      19,
      20,
      21,
      22,
      23,
      24,
      25,
      26,
      27,
      28,
      29,
      30,
      31,
      32,
      33,
      34,
      35,
      36,
      37,
      38,
      39,
      40,
      41,
      42,
      43,
      44,
      45,
      46,
      47,
      48,
      49,
      50,
      51,
      52,
      53
    };
    private static int[] MIXED_TIME_List = new int[18]
    {
      15,
      16,
      17,
      18,
      19,
      20,
      21,
      22,
      23,
      24,
      25,
      26,
      27,
      28,
      29,
      30,
      31,
      32
    };
    private const int C_DATE_DETERMINING_ROW = 2;
    private const int C_MAIN_DETERMINING_ROW = 1;
    private const int C_NONE = -1;
    private const int C_TIME_DETERMINING_ROW = 3;
    private const int F_duration = 3;
    private const int F_endPoint = 2;
    internal const int F_NONE = -1;
    private const int F_startPoint = 1;
    private const int O_mixedTimeType_fractionNumberDigits_index = 2;
    private const int O_mixedTimeType_fractionNumberDigits_mask = 258;
    internal const int O_NONE = -1;
    private const int O_POSTREAD = 512;
    private const int O_PREWRITE = 256;
    private const int O_recurrence_index = 1;
    private const int O_recurrence_mask = 769;
    private const int O_TESTER_INDEX_MASK = 255;
    private const int T_ANY_CENTURY_ENCODING = 2;
    private const int T_ANY_DATE_ENCODING = 8;
    private const int T_ANY_YEAR_DAY_ENCODING = 10;
    private const int T_ANY_YEAR_ENCODING = 4;
    private const int T_ANY_YEAR_MONTH_ENCODING = 6;
    private const int T_ANY_YEAR_WEEK_DAY_ENCODING = 14;
    private const int T_ANY_YEAR_WEEK_ENCODING = 12;
    private const int T_CENTURY_ENCODING = 1;
    private const int T_DATE_ENCODING = 7;
    private const int T_DATE_TIME_ENCODING = 33;
    private const int T_dateType = 201;
    private const int T_day31 = 102;
    private const int T_day366 = 103;
    private const int T_day7 = 104;
    private const int T_DURATION_END_DATE_INTERVAL_ENCODING = 41;
    private const int T_DURATION_END_DATE_TIME_INTERVAL_ENCODING = 43;
    private const int T_DURATION_END_TIME_INTERVAL_ENCODING = 42;
    private const int T_DURATION_INTERVAL_ENCODING = 37;
    private const int T_fraction = 115;
    private const int T_fractionNumberDigits = 117;
    private const int T_hours = 106;
    private const int T_HOURS_AND_DIFF_AND_FRACTION_ENCODING = 26;
    private const int T_HOURS_AND_DIFF_ENCODING = 17;
    private const int T_HOURS_AND_FRACTION_ENCODING = 24;
    private const int T_HOURS_ENCODING = 15;
    private const int T_hours_UTC = 107;
    private const int T_HOURS_UTC_AND_FRACTION_ENCODING = 25;
    private const int T_HOURS_UTC_ENCODING = 16;
    private const int T_internal_DURATION_END_DATE_INTERVAL_TYPE = 7;
    private const int T_internal_DURATION_END_DATE_TIME_INTERVAL_TYPE = 9;
    private const int T_internal_DURATION_END_TIME_INTERVAL_TYPE = 8;
    private const int T_internal_DURATION_INTERVAL_TYPE = 3;
    private const int T_internal_INTERVAL_BASE = 34;
    private const int T_internal_REC_INTERVAL_BASE = 44;
    private const int T_internal_START_DATE_DURATION_INTERVAL_TYPE = 4;
    private const int T_internal_START_DATE_TIME_DURATION_INTERVAL_TYPE = 6;
    private const int T_internal_START_END_DATE_INTERVAL_TYPE = 0;
    private const int T_internal_START_END_DATE_TIME_INTERVAL_TYPE = 2;
    private const int T_internal_START_END_TIME_INTERVAL_TYPE = 1;
    private const int T_internal_START_TIME_DURATION_INTERVAL_TYPE = 5;
    private const int T_localTimeHM = 111;
    private const int T_localTimeHMF = 113;
    private const int T_localTimeHMS = 112;
    private const int T_localTimeHMSF = 114;
    private const int T_minutes = 108;
    private const int T_MINUTES_AND_DIFF_AND_FRACTION_ENCODING = 29;
    private const int T_MINUTES_AND_DIFF_ENCODING = 20;
    private const int T_MINUTES_AND_FRACTION_ENCODING = 27;
    private const int T_MINUTES_ENCODING = 18;
    private const int T_MINUTES_UTC_AND_FRACTION_ENCODING = 28;
    private const int T_MINUTES_UTC_ENCODING = 19;
    private const int T_mixedTimeType_choiceComponent = 301;
    private const int T_month = 101;
    private const int T_NONE = -1;
    private const int T_REC_DURATION_END_DATE_INTERVAL_ENCODING = 51;
    private const int T_REC_DURATION_END_DATE_TIME_INTERVAL_ENCODING = 53;
    private const int T_REC_DURATION_END_TIME_INTERVAL_ENCODING = 52;
    private const int T_REC_DURATION_INTERVAL_ENCODING = 47;
    private const int T_REC_START_DATE_DURATION_INTERVAL_ENCODING = 48;
    private const int T_REC_START_DATE_TIME_DURATION_INTERVAL_ENCODING = 50;
    private const int T_REC_START_END_DATE_INTERVAL_ENCODING = 44;
    private const int T_REC_START_END_DATE_TIME_INTERVAL_ENCODING = 46;
    private const int T_REC_START_END_TIME_INTERVAL_ENCODING = 45;
    private const int T_REC_START_TIME_DURATION_INTERVAL_ENCODING = 49;
    private const int T_recurrence = 116;
    private const int T_seconds = 109;
    private const int T_START_DATE_DURATION_INTERVAL_ENCODING = 38;
    private const int T_START_DATE_TIME_DURATION_INTERVAL_ENCODING = 40;
    private const int T_START_END_DATE_INTERVAL_ENCODING = 34;
    private const int T_START_END_DATE_TIME_INTERVAL_ENCODING = 36;
    private const int T_START_END_TIME_INTERVAL_ENCODING = 35;
    private const int T_START_TIME_DURATION_INTERVAL_ENCODING = 39;
    private const int T_TIME_DIFFERENCE = 110;
    private const int T_TIME_OF_DAY_AND_DIFF_AND_FRACTION_ENCODING = 32;
    private const int T_TIME_OF_DAY_AND_DIFF_ENCODING = 23;
    private const int T_TIME_OF_DAY_AND_FRACTION_ENCODING = 30;
    private const int T_TIME_OF_DAY_ENCODING = 21;
    private const int T_TIME_OF_DAY_UTC_AND_FRACTION_ENCODING = 31;
    private const int T_TIME_OF_DAY_UTC_ENCODING = 22;
    private const int T_timeType = 202;
    private const int T_week = 105;
    private const int T_YEAR_DAY_ENCODING = 9;
    private const int T_YEAR_ENCODING = 3;
    private const int T_YEAR_MONTH_ENCODING = 5;
    private const int T_YEAR_WEEK_DAY_ENCODING = 13;
    private const int T_YEAR_WEEK_ENCODING = 11;

    internal static void asn1TimeTypeComponentNotDefined(Asn1TimeType typeInstance, int testerID)
    {
      if ((testerID & 512) != 512)
        return;
      if ((testerID & (int) byte.MaxValue) != 1)
        throw new ArgumentException("PER Time Type : invalid optional field tester ID");
      typeInstance.Recurrence = -2;
    }

    private static Asn1Type build_ANY_CENTURY_ENCODING(Asn1TimeType typeInstance)
    {
      return (Asn1Type) new CenturyAdapter(typeInstance);
    }

    private static Asn1Type build_ANY_DATE_ENCODING(Asn1TimeType typeInstance)
    {
      DynAsn1SequenceTypeAdapter sequenceTypeAdapter = new DynAsn1SequenceTypeAdapter(typeInstance, 3, 0);
      sequenceTypeAdapter.addComponentAdapter(4);
      sequenceTypeAdapter.addComponentAdapter(101);
      sequenceTypeAdapter.addComponentAdapter(102);
      return (Asn1Type) sequenceTypeAdapter;
    }

    private static Asn1Type build_ANY_YEAR_DAY_ENCODING(Asn1TimeType typeInstance)
    {
      DynAsn1SequenceTypeAdapter sequenceTypeAdapter = new DynAsn1SequenceTypeAdapter(typeInstance, 2, 0);
      sequenceTypeAdapter.addComponentAdapter(4);
      sequenceTypeAdapter.addComponentAdapter(103);
      return (Asn1Type) sequenceTypeAdapter;
    }

    private static Asn1Type build_ANY_YEAR_ENCODING(Asn1TimeType typeInstance)
    {
      return (Asn1Type) new YearAdapter(typeInstance);
    }

    private static Asn1Type build_ANY_YEAR_MONTH_ENCODING(Asn1TimeType typeInstance)
    {
      DynAsn1SequenceTypeAdapter sequenceTypeAdapter = new DynAsn1SequenceTypeAdapter(typeInstance, 2, 0);
      sequenceTypeAdapter.addComponentAdapter(4);
      sequenceTypeAdapter.addComponentAdapter(101);
      return (Asn1Type) sequenceTypeAdapter;
    }

    private static Asn1Type build_ANY_YEAR_WEEK_DAY_ENCODING(Asn1TimeType typeInstance)
    {
      DynAsn1SequenceTypeAdapter sequenceTypeAdapter = new DynAsn1SequenceTypeAdapter(typeInstance, 3, 0);
      sequenceTypeAdapter.addComponentAdapter(4);
      sequenceTypeAdapter.addComponentAdapter(105);
      sequenceTypeAdapter.addComponentAdapter(104);
      return (Asn1Type) sequenceTypeAdapter;
    }

    private static Asn1Type build_ANY_YEAR_WEEK_ENCODING(Asn1TimeType typeInstance)
    {
      DynAsn1SequenceTypeAdapter sequenceTypeAdapter = new DynAsn1SequenceTypeAdapter(typeInstance, 2, 0);
      sequenceTypeAdapter.addComponentAdapter(4);
      sequenceTypeAdapter.addComponentAdapter(105);
      return (Asn1Type) sequenceTypeAdapter;
    }

    private static Asn1Type build_CENTURY_ENCODING(Asn1TimeType typeInstance)
    {
      return (Asn1Type) new CenturyAdapter(typeInstance, 0L, 99L, false);
    }

    private static Asn1Type build_DATE_ENCODING(Asn1TimeType typeInstance)
    {
      DynAsn1SequenceTypeAdapter sequenceTypeAdapter = new DynAsn1SequenceTypeAdapter(typeInstance, 3, 0);
      sequenceTypeAdapter.addComponentAdapter(3);
      sequenceTypeAdapter.addComponentAdapter(101);
      sequenceTypeAdapter.addComponentAdapter(102);
      return (Asn1Type) sequenceTypeAdapter;
    }

    private static Asn1Type build_DATE_TIME_ENCODING(Asn1TimeType typeInstance, bool mixedSetting)
    {
      DynAsn1SequenceTypeAdapter sequenceTypeAdapter = new DynAsn1SequenceTypeAdapter(typeInstance, 2, 0, mixedSetting);
      sequenceTypeAdapter.addComponentAdapter(201);
      sequenceTypeAdapter.addComponentAdapter(202);
      return (Asn1Type) sequenceTypeAdapter;
    }

    private static Asn1Type build_DURATION_END_DATE_INTERVAL_ENCODING(Asn1TimeType typeInstance, bool mixedSetting)
    {
      DynAsn1SequenceTypeAdapter sequenceTypeAdapter = new DynAsn1SequenceTypeAdapter(typeInstance, 2, 0, mixedSetting);
      sequenceTypeAdapter.addComponentAdapter(37, 3);
      sequenceTypeAdapter.addComponentAdapter(201, 2);
      return (Asn1Type) sequenceTypeAdapter;
    }

    private static Asn1Type build_DURATION_END_DATE_TIME_INTERVAL_ENCODING(Asn1TimeType typeInstance, bool mixedSetting)
    {
      DynAsn1SequenceTypeAdapter sequenceTypeAdapter = new DynAsn1SequenceTypeAdapter(typeInstance, 2, 0, mixedSetting);
      sequenceTypeAdapter.addComponentAdapter(37, 3);
      sequenceTypeAdapter.addComponentAdapter(33, 2);
      return (Asn1Type) sequenceTypeAdapter;
    }

    private static Asn1Type build_DURATION_END_TIME_INTERVAL_ENCODING(Asn1TimeType typeInstance, bool mixedSetting)
    {
      DynAsn1SequenceTypeAdapter sequenceTypeAdapter = new DynAsn1SequenceTypeAdapter(typeInstance, 2, 0, mixedSetting);
      sequenceTypeAdapter.addComponentAdapter(37, 3);
      sequenceTypeAdapter.addComponentAdapter(202, 2);
      return (Asn1Type) sequenceTypeAdapter;
    }

    private static Asn1Type build_DURATION_INTERVAL_ENCODING(Asn1TimeType typeInstance)
    {
      return (Asn1Type) new DurationIntervalAdapter(typeInstance);
    }

    private static Asn1Type build_Fixed_Date_Type(Asn1TimeType typeInstance)
    {
      Asn1Type asn1Type = (Asn1Type) null;
      int adapterID = Asn1TimeTypeAdapterFactory.selectAdapterForDateSetting(typeInstance, Asn1TimeTypeAdapterFactory.TypeSettingsExplorer.getInstance());
      if (adapterID != -1)
        asn1Type = Asn1TimeTypeAdapterFactory.createAsn1TimeTypeAdapter(typeInstance, adapterID, false);
      return asn1Type;
    }

    private static Asn1Type build_Fixed_Time_Type(Asn1TimeType typeInstance)
    {
      Asn1Type asn1Type = (Asn1Type) null;
      int adapterID = Asn1TimeTypeAdapterFactory.selectAdapterForTimeSetting(typeInstance, Asn1TimeTypeAdapterFactory.TypeSettingsExplorer.getInstance());
      if (adapterID != -1)
        asn1Type = Asn1TimeTypeAdapterFactory.createAsn1TimeTypeAdapter(typeInstance, adapterID, false);
      return asn1Type;
    }

    private static Asn1Type build_FRACTIONAL_TIME(Asn1TimeType typeInstance, int timeTypeAdapterID)
    {
      DynAsn1SequenceTypeAdapter sequenceTypeAdapter = new DynAsn1SequenceTypeAdapter(typeInstance, 2, 0, false);
      sequenceTypeAdapter.addComponentAdapter(117);
      sequenceTypeAdapter.addComponentAdapter(timeTypeAdapterID);
      return (Asn1Type) sequenceTypeAdapter;
    }

    private static Asn1Type build_HOURS_AND_DIFF_AND_FRACTION_ENCODING(Asn1TimeType typeInstance)
    {
      DynAsn1SequenceTypeAdapter sequenceTypeAdapter = new DynAsn1SequenceTypeAdapter(typeInstance, 3, 0);
      sequenceTypeAdapter.addComponentAdapter(106);
      sequenceTypeAdapter.addComponentAdapter(115);
      sequenceTypeAdapter.addComponentAdapter(110);
      return (Asn1Type) sequenceTypeAdapter;
    }

    private static Asn1Type build_HOURS_AND_DIFF_ENCODING(Asn1TimeType typeInstance)
    {
      DynAsn1SequenceTypeAdapter sequenceTypeAdapter = new DynAsn1SequenceTypeAdapter(typeInstance, 2, 0);
      sequenceTypeAdapter.addComponentAdapter(106);
      sequenceTypeAdapter.addComponentAdapter(110);
      return (Asn1Type) sequenceTypeAdapter;
    }

    private static Asn1Type build_HOURS_AND_FRACTION_ENCODING(Asn1TimeType typeInstance)
    {
      DynAsn1SequenceTypeAdapter sequenceTypeAdapter = new DynAsn1SequenceTypeAdapter(typeInstance, 2, 0);
      sequenceTypeAdapter.addComponentAdapter(106);
      sequenceTypeAdapter.addComponentAdapter(115);
      return (Asn1Type) sequenceTypeAdapter;
    }

    private static Asn1Type build_HOURS_ENCODING(Asn1TimeType typeInstance)
    {
      return (Asn1Type) new HoursAdapter(typeInstance);
    }

    private static Asn1Type build_HOURS_UTC_AND_FRACTION_ENCODING(Asn1TimeType typeInstance)
    {
      DynAsn1SequenceTypeAdapter sequenceTypeAdapter = new DynAsn1SequenceTypeAdapter(typeInstance, 2, 0);
      sequenceTypeAdapter.addComponentAdapter(107);
      sequenceTypeAdapter.addComponentAdapter(115);
      return (Asn1Type) sequenceTypeAdapter;
    }

    private static Asn1Type build_HOURS_UTC_ENCODING(Asn1TimeType typeInstance)
    {
      return (Asn1Type) new HoursUTCAdapter(typeInstance);
    }

    private static Asn1Type build_localTimeHM(Asn1TimeType typeInstance)
    {
      DynAsn1SequenceTypeAdapter sequenceTypeAdapter = new DynAsn1SequenceTypeAdapter(typeInstance, 2, 0);
      sequenceTypeAdapter.addComponentAdapter(106);
      sequenceTypeAdapter.addComponentAdapter(108);
      return (Asn1Type) sequenceTypeAdapter;
    }

    private static Asn1Type build_localTimeHMF(Asn1TimeType typeInstance)
    {
      DynAsn1SequenceTypeAdapter sequenceTypeAdapter = new DynAsn1SequenceTypeAdapter(typeInstance, 3, 0);
      sequenceTypeAdapter.addComponentAdapter(106);
      sequenceTypeAdapter.addComponentAdapter(108);
      sequenceTypeAdapter.addComponentAdapter(115);
      return (Asn1Type) sequenceTypeAdapter;
    }

    private static Asn1Type build_localTimeHMS(Asn1TimeType typeInstance)
    {
      DynAsn1SequenceTypeAdapter sequenceTypeAdapter = new DynAsn1SequenceTypeAdapter(typeInstance, 3, 0);
      sequenceTypeAdapter.addComponentAdapter(106);
      sequenceTypeAdapter.addComponentAdapter(108);
      sequenceTypeAdapter.addComponentAdapter(109);
      return (Asn1Type) sequenceTypeAdapter;
    }

    private static Asn1Type build_localTimeHMSF(Asn1TimeType typeInstance)
    {
      DynAsn1SequenceTypeAdapter sequenceTypeAdapter = new DynAsn1SequenceTypeAdapter(typeInstance, 4, 0);
      sequenceTypeAdapter.addComponentAdapter(106);
      sequenceTypeAdapter.addComponentAdapter(108);
      sequenceTypeAdapter.addComponentAdapter(109);
      sequenceTypeAdapter.addComponentAdapter(115);
      return (Asn1Type) sequenceTypeAdapter;
    }

    private static Asn1Type build_MINUTES_AND_DIFF_AND_FRACTION_ENCODING(Asn1TimeType typeInstance)
    {
      DynAsn1SequenceTypeAdapter sequenceTypeAdapter = new DynAsn1SequenceTypeAdapter(typeInstance, 2, 0);
      sequenceTypeAdapter.addComponentAdapter(113);
      sequenceTypeAdapter.addComponentAdapter(110);
      return (Asn1Type) sequenceTypeAdapter;
    }

    private static Asn1Type build_MINUTES_AND_DIFF_ENCODING(Asn1TimeType typeInstance)
    {
      DynAsn1SequenceTypeAdapter sequenceTypeAdapter = new DynAsn1SequenceTypeAdapter(typeInstance, 2, 0);
      sequenceTypeAdapter.addComponentAdapter(111);
      sequenceTypeAdapter.addComponentAdapter(110);
      return (Asn1Type) sequenceTypeAdapter;
    }

    private static Asn1Type build_MINUTES_AND_FRACTION_ENCODING(Asn1TimeType typeInstance)
    {
      return Asn1TimeTypeAdapterFactory.build_localTimeHMF(typeInstance);
    }

    private static Asn1Type build_MINUTES_ENCODING(Asn1TimeType typeInstance)
    {
      return Asn1TimeTypeAdapterFactory.build_localTimeHM(typeInstance);
    }

    private static Asn1Type build_MINUTES_UTC_AND_FRACTION_ENCODING(Asn1TimeType typeInstance)
    {
      DynAsn1SequenceTypeAdapter sequenceTypeAdapter = new DynAsn1SequenceTypeAdapter(typeInstance, 3, 0);
      sequenceTypeAdapter.addComponentAdapter(107);
      sequenceTypeAdapter.addComponentAdapter(108);
      sequenceTypeAdapter.addComponentAdapter(115);
      return (Asn1Type) sequenceTypeAdapter;
    }

    private static Asn1Type build_MINUTES_UTC_ENCODING(Asn1TimeType typeInstance)
    {
      DynAsn1SequenceTypeAdapter sequenceTypeAdapter = new DynAsn1SequenceTypeAdapter(typeInstance, 2, 0);
      sequenceTypeAdapter.addComponentAdapter(107);
      sequenceTypeAdapter.addComponentAdapter(108);
      return (Asn1Type) sequenceTypeAdapter;
    }

    private static Asn1Type build_Mixed_Date_Type(Asn1TimeType typeInstance)
    {
      return (Asn1Type) new DynAsn1ChoiceTypeAdapter(typeInstance, Asn1TimeTypeAdapterFactory.MIXED_DATE_List, 2, false);
    }

    private static Asn1Type build_MIXED_ENCODING(Asn1TimeType typeInstance)
    {
      return (Asn1Type) new DynAsn1ChoiceTypeAdapter(typeInstance, Asn1TimeTypeAdapterFactory.MIXED_ENCODING_List, 1, true);
    }

    private static Asn1Type build_Mixed_Time_Type(Asn1TimeType typeInstance)
    {
      DynAsn1SequenceTypeAdapter sequenceTypeAdapter = new DynAsn1SequenceTypeAdapter(typeInstance, 2, 1);
      sequenceTypeAdapter.addOptionalComponentAdapter(117, 258);
      sequenceTypeAdapter.addComponentAdapter(301);
      return (Asn1Type) sequenceTypeAdapter;
    }

    private static Asn1Type build_Mixed_Time_Type_choiceComponent(Asn1TimeType typeInstance)
    {
      return (Asn1Type) new DynAsn1ChoiceTypeAdapter(typeInstance, Asn1TimeTypeAdapterFactory.MIXED_TIME_List, 3, false);
    }

    private static Asn1Type build_REC_DURATION_END_DATE_INTERVAL_ENCODING(Asn1TimeType typeInstance, bool mixedSetting)
    {
      DynAsn1SequenceTypeAdapter sequenceTypeAdapter = new DynAsn1SequenceTypeAdapter(typeInstance, 3, 1, mixedSetting);
      sequenceTypeAdapter.addOptionalComponentAdapter(116, 769);
      sequenceTypeAdapter.addComponentAdapter(37, 3);
      sequenceTypeAdapter.addComponentAdapter(201, 2);
      return (Asn1Type) sequenceTypeAdapter;
    }

    private static Asn1Type build_REC_DURATION_END_DATE_TIME_INTERVAL_ENCODING(Asn1TimeType typeInstance, bool mixedSetting)
    {
      DynAsn1SequenceTypeAdapter sequenceTypeAdapter = new DynAsn1SequenceTypeAdapter(typeInstance, 3, 1, mixedSetting);
      sequenceTypeAdapter.addOptionalComponentAdapter(116, 769);
      sequenceTypeAdapter.addComponentAdapter(37, 3);
      sequenceTypeAdapter.addComponentAdapter(33, 2);
      return (Asn1Type) sequenceTypeAdapter;
    }

    private static Asn1Type build_REC_DURATION_END_TIME_INTERVAL_ENCODING(Asn1TimeType typeInstance, bool mixedSetting)
    {
      DynAsn1SequenceTypeAdapter sequenceTypeAdapter = new DynAsn1SequenceTypeAdapter(typeInstance, 3, 1, mixedSetting);
      sequenceTypeAdapter.addOptionalComponentAdapter(116, 769);
      sequenceTypeAdapter.addComponentAdapter(37, 3);
      sequenceTypeAdapter.addComponentAdapter(202, 2);
      return (Asn1Type) sequenceTypeAdapter;
    }

    private static Asn1Type build_REC_DURATION_INTERVAL_ENCODING(Asn1TimeType typeInstance, bool mixedSetting)
    {
      DynAsn1SequenceTypeAdapter sequenceTypeAdapter = new DynAsn1SequenceTypeAdapter(typeInstance, 2, 1);
      sequenceTypeAdapter.addOptionalComponentAdapter(116, 769);
      sequenceTypeAdapter.addComponentAdapter(37, !mixedSetting ? -1 : 3);
      return (Asn1Type) sequenceTypeAdapter;
    }

    private static Asn1Type build_REC_START_DATE_DURATION_INTERVAL_ENCODING(Asn1TimeType typeInstance, bool mixedSetting)
    {
      DynAsn1SequenceTypeAdapter sequenceTypeAdapter = new DynAsn1SequenceTypeAdapter(typeInstance, 3, 1, mixedSetting);
      sequenceTypeAdapter.addOptionalComponentAdapter(116, 769);
      sequenceTypeAdapter.addComponentAdapter(201, 1);
      sequenceTypeAdapter.addComponentAdapter(37, 3);
      return (Asn1Type) sequenceTypeAdapter;
    }

    private static Asn1Type build_REC_START_DATE_TIME_DURATION_INTERVAL_ENCODING(Asn1TimeType typeInstance, bool mixedSetting)
    {
      DynAsn1SequenceTypeAdapter sequenceTypeAdapter = new DynAsn1SequenceTypeAdapter(typeInstance, 3, 1, mixedSetting);
      sequenceTypeAdapter.addOptionalComponentAdapter(116, 769);
      sequenceTypeAdapter.addComponentAdapter(33, 1);
      sequenceTypeAdapter.addComponentAdapter(37, 3);
      return (Asn1Type) sequenceTypeAdapter;
    }

    private static Asn1Type build_REC_START_END_DATE_INTERVAL_ENCODING(Asn1TimeType typeInstance, bool mixedSetting)
    {
      DynAsn1SequenceTypeAdapter sequenceTypeAdapter = new DynAsn1SequenceTypeAdapter(typeInstance, 3, 1, mixedSetting);
      sequenceTypeAdapter.addOptionalComponentAdapter(116, 769);
      sequenceTypeAdapter.addComponentAdapter(201, 1);
      sequenceTypeAdapter.addComponentAdapter(201, 2);
      return (Asn1Type) sequenceTypeAdapter;
    }

    private static Asn1Type build_REC_START_END_DATE_TIME_INTERVAL_ENCODING(Asn1TimeType typeInstance, bool mixedSetting)
    {
      DynAsn1SequenceTypeAdapter sequenceTypeAdapter = new DynAsn1SequenceTypeAdapter(typeInstance, 3, 1, mixedSetting);
      sequenceTypeAdapter.addOptionalComponentAdapter(116, 769);
      sequenceTypeAdapter.addComponentAdapter(33, 1);
      sequenceTypeAdapter.addComponentAdapter(33, 2);
      return (Asn1Type) sequenceTypeAdapter;
    }

    private static Asn1Type build_REC_START_END_TIME_INTERVAL_ENCODING(Asn1TimeType typeInstance, bool mixedSetting)
    {
      DynAsn1SequenceTypeAdapter sequenceTypeAdapter = new DynAsn1SequenceTypeAdapter(typeInstance, 3, 1, mixedSetting);
      sequenceTypeAdapter.addOptionalComponentAdapter(116, 769);
      sequenceTypeAdapter.addComponentAdapter(202, 1);
      sequenceTypeAdapter.addComponentAdapter(202, 2);
      return (Asn1Type) sequenceTypeAdapter;
    }

    private static Asn1Type build_REC_START_TIME_DURATION_INTERVAL_ENCODING(Asn1TimeType typeInstance, bool mixedSetting)
    {
      DynAsn1SequenceTypeAdapter sequenceTypeAdapter = new DynAsn1SequenceTypeAdapter(typeInstance, 3, 1, mixedSetting);
      sequenceTypeAdapter.addOptionalComponentAdapter(116, 769);
      sequenceTypeAdapter.addComponentAdapter(202, 1);
      sequenceTypeAdapter.addComponentAdapter(37, 3);
      return (Asn1Type) sequenceTypeAdapter;
    }

    private static Asn1Type build_START_DATE_DURATION_INTERVAL_ENCODING(Asn1TimeType typeInstance, bool mixedSetting)
    {
      DynAsn1SequenceTypeAdapter sequenceTypeAdapter = new DynAsn1SequenceTypeAdapter(typeInstance, 2, 0, mixedSetting);
      sequenceTypeAdapter.addComponentAdapter(201, 1);
      sequenceTypeAdapter.addComponentAdapter(37, 3);
      return (Asn1Type) sequenceTypeAdapter;
    }

    private static Asn1Type build_START_DATE_TIME_DURATION_INTERVAL_ENCODING(Asn1TimeType typeInstance, bool mixedSetting)
    {
      DynAsn1SequenceTypeAdapter sequenceTypeAdapter = new DynAsn1SequenceTypeAdapter(typeInstance, 2, 0, mixedSetting);
      sequenceTypeAdapter.addComponentAdapter(33, 1);
      sequenceTypeAdapter.addComponentAdapter(37, 3);
      return (Asn1Type) sequenceTypeAdapter;
    }

    private static Asn1Type build_START_END_DATE_INTERVAL_ENCODING(Asn1TimeType typeInstance, bool mixedSetting)
    {
      DynAsn1SequenceTypeAdapter sequenceTypeAdapter = new DynAsn1SequenceTypeAdapter(typeInstance, 2, 0, mixedSetting);
      sequenceTypeAdapter.addComponentAdapter(201, 1);
      sequenceTypeAdapter.addComponentAdapter(201, 2);
      return (Asn1Type) sequenceTypeAdapter;
    }

    private static Asn1Type build_START_END_DATE_TIME_INTERVAL_ENCODING(Asn1TimeType typeInstance, bool mixedSetting)
    {
      DynAsn1SequenceTypeAdapter sequenceTypeAdapter = new DynAsn1SequenceTypeAdapter(typeInstance, 2, 0, mixedSetting);
      sequenceTypeAdapter.addComponentAdapter(33, 1);
      sequenceTypeAdapter.addComponentAdapter(33, 2);
      return (Asn1Type) sequenceTypeAdapter;
    }

    private static Asn1Type build_START_END_TIME_INTERVAL_ENCODING(Asn1TimeType typeInstance, bool mixedSetting)
    {
      DynAsn1SequenceTypeAdapter sequenceTypeAdapter = new DynAsn1SequenceTypeAdapter(typeInstance, 2, 0, mixedSetting);
      sequenceTypeAdapter.addComponentAdapter(202, 1);
      sequenceTypeAdapter.addComponentAdapter(202, 2);
      return (Asn1Type) sequenceTypeAdapter;
    }

    private static Asn1Type build_START_TIME_DURATION_INTERVAL_ENCODING(Asn1TimeType typeInstance, bool mixedSetting)
    {
      DynAsn1SequenceTypeAdapter sequenceTypeAdapter = new DynAsn1SequenceTypeAdapter(typeInstance, 2, 0, mixedSetting);
      sequenceTypeAdapter.addComponentAdapter(202, 1);
      sequenceTypeAdapter.addComponentAdapter(37, 3);
      return (Asn1Type) sequenceTypeAdapter;
    }

    private static Asn1Type build_TIME_OF_DAY_AND_DIFF_AND_FRACTION_ENCODING(Asn1TimeType typeInstance)
    {
      DynAsn1SequenceTypeAdapter sequenceTypeAdapter = new DynAsn1SequenceTypeAdapter(typeInstance, 2, 0);
      sequenceTypeAdapter.addComponentAdapter(114);
      sequenceTypeAdapter.addComponentAdapter(110);
      return (Asn1Type) sequenceTypeAdapter;
    }

    private static Asn1Type build_TIME_OF_DAY_AND_DIFF_ENCODING(Asn1TimeType typeInstance)
    {
      DynAsn1SequenceTypeAdapter sequenceTypeAdapter = new DynAsn1SequenceTypeAdapter(typeInstance, 2, 0);
      sequenceTypeAdapter.addComponentAdapter(112);
      sequenceTypeAdapter.addComponentAdapter(110);
      return (Asn1Type) sequenceTypeAdapter;
    }

    private static Asn1Type build_TIME_OF_DAY_AND_FRACTION_ENCODING(Asn1TimeType typeInstance)
    {
      return Asn1TimeTypeAdapterFactory.build_localTimeHMSF(typeInstance);
    }

    private static Asn1Type build_TIME_OF_DAY_ENCODING(Asn1TimeType typeInstance)
    {
      return Asn1TimeTypeAdapterFactory.build_localTimeHMS(typeInstance);
    }

    private static Asn1Type build_TIME_OF_DAY_UTC_AND_FRACTION_ENCODING(Asn1TimeType typeInstance)
    {
      DynAsn1SequenceTypeAdapter sequenceTypeAdapter = new DynAsn1SequenceTypeAdapter(typeInstance, 4, 0);
      sequenceTypeAdapter.addComponentAdapter(107);
      sequenceTypeAdapter.addComponentAdapter(108);
      sequenceTypeAdapter.addComponentAdapter(109);
      sequenceTypeAdapter.addComponentAdapter(115);
      return (Asn1Type) sequenceTypeAdapter;
    }

    private static Asn1Type build_TIME_OF_DAY_UTC_ENCODING(Asn1TimeType typeInstance)
    {
      DynAsn1SequenceTypeAdapter sequenceTypeAdapter = new DynAsn1SequenceTypeAdapter(typeInstance, 3, 0);
      sequenceTypeAdapter.addComponentAdapter(107);
      sequenceTypeAdapter.addComponentAdapter(108);
      sequenceTypeAdapter.addComponentAdapter(109);
      return (Asn1Type) sequenceTypeAdapter;
    }

    private static Asn1Type build_YEAR_DAY_ENCODING(Asn1TimeType typeInstance)
    {
      DynAsn1SequenceTypeAdapter sequenceTypeAdapter = new DynAsn1SequenceTypeAdapter(typeInstance, 2, 0);
      sequenceTypeAdapter.addComponentAdapter(3);
      sequenceTypeAdapter.addComponentAdapter(103);
      return (Asn1Type) sequenceTypeAdapter;
    }

    private static Asn1Type build_YEAR_ENCODING(Asn1TimeType typeInstance)
    {
      return (Asn1Type) new YearEncodingAdapter(typeInstance);
    }

    private static Asn1Type build_YEAR_MONTH_ENCODING(Asn1TimeType typeInstance)
    {
      DynAsn1SequenceTypeAdapter sequenceTypeAdapter = new DynAsn1SequenceTypeAdapter(typeInstance, 2, 0);
      sequenceTypeAdapter.addComponentAdapter(3);
      sequenceTypeAdapter.addComponentAdapter(101);
      return (Asn1Type) sequenceTypeAdapter;
    }

    private static Asn1Type build_YEAR_WEEK_DAY_ENCODING(Asn1TimeType typeInstance)
    {
      DynAsn1SequenceTypeAdapter sequenceTypeAdapter = new DynAsn1SequenceTypeAdapter(typeInstance, 3, 0);
      sequenceTypeAdapter.addComponentAdapter(3);
      sequenceTypeAdapter.addComponentAdapter(105);
      sequenceTypeAdapter.addComponentAdapter(104);
      return (Asn1Type) sequenceTypeAdapter;
    }

    private static Asn1Type build_YEAR_WEEK_ENCODING(Asn1TimeType typeInstance)
    {
      DynAsn1SequenceTypeAdapter sequenceTypeAdapter = new DynAsn1SequenceTypeAdapter(typeInstance, 2, 0);
      sequenceTypeAdapter.addComponentAdapter(3);
      sequenceTypeAdapter.addComponentAdapter(105);
      return (Asn1Type) sequenceTypeAdapter;
    }

    private static Asn1Type buildAdapterFromTypeSettings(Asn1TimeType typeInstance)
    {
      Asn1Type asn1Type = (Asn1Type) null;
      int adapterID = Asn1TimeTypeAdapterFactory.selectAdapterForBasicSetting(typeInstance, Asn1TimeTypeAdapterFactory.TypeSettingsExplorer.getInstance());
      if (adapterID != -1)
        asn1Type = Asn1TimeTypeAdapterFactory.createAsn1TimeTypeAdapter(typeInstance, adapterID, false);
      return asn1Type;
    }

    private static Asn1Type buildAdapterFromValueSettings(Asn1TimeType typeInstance)
    {
      return Asn1TimeTypeAdapterFactory.build_MIXED_ENCODING(typeInstance);
    }

    internal static int chooseComponentAdapter(Asn1TimeType typeInstance, int choiceID)
    {
      switch (choiceID)
      {
        case 1:
          return Asn1TimeTypeAdapterFactory.selectAdapterForBasicSetting(typeInstance, Asn1TimeTypeAdapterFactory.ValueSettingsExplorer.getInstance());
        case 2:
          return Asn1TimeTypeAdapterFactory.selectAdapterForDateSetting(typeInstance, Asn1TimeTypeAdapterFactory.ValueSettingsExplorer.getInstance());
        case 3:
          return Asn1TimeTypeAdapterFactory.selectAdapterForTimeSetting(typeInstance, Asn1TimeTypeAdapterFactory.ValueSettingsExplorer.getInstance());
        default:
          throw new ArgumentException("PER Time Type : invalid choice ID");
      }
    }

    internal static Asn1Type createAsn1TimeTypeAdapter(Asn1TimeType typeInstance, int adapterID, bool mixedSetting)
    {
      Asn1Type asn1Type;
      switch (adapterID)
      {
        case 1:
          asn1Type = Asn1TimeTypeAdapterFactory.build_CENTURY_ENCODING(typeInstance);
          break;
        case 2:
          asn1Type = Asn1TimeTypeAdapterFactory.build_ANY_CENTURY_ENCODING(typeInstance);
          break;
        case 3:
          asn1Type = Asn1TimeTypeAdapterFactory.build_YEAR_ENCODING(typeInstance);
          break;
        case 4:
          asn1Type = Asn1TimeTypeAdapterFactory.build_ANY_YEAR_ENCODING(typeInstance);
          break;
        case 5:
          asn1Type = Asn1TimeTypeAdapterFactory.build_YEAR_MONTH_ENCODING(typeInstance);
          break;
        case 6:
          asn1Type = Asn1TimeTypeAdapterFactory.build_ANY_YEAR_MONTH_ENCODING(typeInstance);
          break;
        case 7:
          asn1Type = Asn1TimeTypeAdapterFactory.build_DATE_ENCODING(typeInstance);
          break;
        case 8:
          asn1Type = Asn1TimeTypeAdapterFactory.build_ANY_DATE_ENCODING(typeInstance);
          break;
        case 9:
          asn1Type = Asn1TimeTypeAdapterFactory.build_YEAR_DAY_ENCODING(typeInstance);
          break;
        case 10:
          asn1Type = Asn1TimeTypeAdapterFactory.build_ANY_YEAR_DAY_ENCODING(typeInstance);
          break;
        case 11:
          asn1Type = Asn1TimeTypeAdapterFactory.build_YEAR_WEEK_ENCODING(typeInstance);
          break;
        case 12:
          asn1Type = Asn1TimeTypeAdapterFactory.build_ANY_YEAR_WEEK_ENCODING(typeInstance);
          break;
        case 13:
          asn1Type = Asn1TimeTypeAdapterFactory.build_YEAR_WEEK_DAY_ENCODING(typeInstance);
          break;
        case 14:
          asn1Type = Asn1TimeTypeAdapterFactory.build_ANY_YEAR_WEEK_DAY_ENCODING(typeInstance);
          break;
        case 15:
          asn1Type = Asn1TimeTypeAdapterFactory.build_HOURS_ENCODING(typeInstance);
          break;
        case 16:
          asn1Type = Asn1TimeTypeAdapterFactory.build_HOURS_UTC_ENCODING(typeInstance);
          break;
        case 17:
          asn1Type = Asn1TimeTypeAdapterFactory.build_HOURS_AND_DIFF_ENCODING(typeInstance);
          break;
        case 18:
          asn1Type = Asn1TimeTypeAdapterFactory.build_MINUTES_ENCODING(typeInstance);
          break;
        case 19:
          asn1Type = Asn1TimeTypeAdapterFactory.build_MINUTES_UTC_ENCODING(typeInstance);
          break;
        case 20:
          asn1Type = Asn1TimeTypeAdapterFactory.build_MINUTES_AND_DIFF_ENCODING(typeInstance);
          break;
        case 21:
          asn1Type = Asn1TimeTypeAdapterFactory.build_TIME_OF_DAY_ENCODING(typeInstance);
          break;
        case 22:
          asn1Type = Asn1TimeTypeAdapterFactory.build_TIME_OF_DAY_UTC_ENCODING(typeInstance);
          break;
        case 23:
          asn1Type = Asn1TimeTypeAdapterFactory.build_TIME_OF_DAY_AND_DIFF_ENCODING(typeInstance);
          break;
        case 24:
          asn1Type = !mixedSetting ? Asn1TimeTypeAdapterFactory.build_HOURS_AND_FRACTION_ENCODING(typeInstance) : Asn1TimeTypeAdapterFactory.build_FRACTIONAL_TIME(typeInstance, adapterID);
          break;
        case 25:
          asn1Type = !mixedSetting ? Asn1TimeTypeAdapterFactory.build_HOURS_UTC_AND_FRACTION_ENCODING(typeInstance) : Asn1TimeTypeAdapterFactory.build_FRACTIONAL_TIME(typeInstance, adapterID);
          break;
        case 26:
          asn1Type = !mixedSetting ? Asn1TimeTypeAdapterFactory.build_HOURS_AND_DIFF_AND_FRACTION_ENCODING(typeInstance) : Asn1TimeTypeAdapterFactory.build_FRACTIONAL_TIME(typeInstance, adapterID);
          break;
        case 27:
          asn1Type = !mixedSetting ? Asn1TimeTypeAdapterFactory.build_MINUTES_AND_FRACTION_ENCODING(typeInstance) : Asn1TimeTypeAdapterFactory.build_FRACTIONAL_TIME(typeInstance, adapterID);
          break;
        case 28:
          asn1Type = !mixedSetting ? Asn1TimeTypeAdapterFactory.build_MINUTES_UTC_AND_FRACTION_ENCODING(typeInstance) : Asn1TimeTypeAdapterFactory.build_FRACTIONAL_TIME(typeInstance, adapterID);
          break;
        case 29:
          asn1Type = !mixedSetting ? Asn1TimeTypeAdapterFactory.build_MINUTES_AND_DIFF_AND_FRACTION_ENCODING(typeInstance) : Asn1TimeTypeAdapterFactory.build_FRACTIONAL_TIME(typeInstance, adapterID);
          break;
        case 30:
          asn1Type = !mixedSetting ? Asn1TimeTypeAdapterFactory.build_TIME_OF_DAY_AND_FRACTION_ENCODING(typeInstance) : Asn1TimeTypeAdapterFactory.build_FRACTIONAL_TIME(typeInstance, adapterID);
          break;
        case 31:
          asn1Type = !mixedSetting ? Asn1TimeTypeAdapterFactory.build_TIME_OF_DAY_UTC_AND_FRACTION_ENCODING(typeInstance) : Asn1TimeTypeAdapterFactory.build_FRACTIONAL_TIME(typeInstance, adapterID);
          break;
        case 32:
          asn1Type = !mixedSetting ? Asn1TimeTypeAdapterFactory.build_TIME_OF_DAY_AND_DIFF_AND_FRACTION_ENCODING(typeInstance) : Asn1TimeTypeAdapterFactory.build_FRACTIONAL_TIME(typeInstance, adapterID);
          break;
        case 33:
          asn1Type = Asn1TimeTypeAdapterFactory.build_DATE_TIME_ENCODING(typeInstance, mixedSetting);
          break;
        case 34:
          asn1Type = Asn1TimeTypeAdapterFactory.build_START_END_DATE_INTERVAL_ENCODING(typeInstance, mixedSetting);
          break;
        case 35:
          asn1Type = Asn1TimeTypeAdapterFactory.build_START_END_TIME_INTERVAL_ENCODING(typeInstance, mixedSetting);
          break;
        case 36:
          asn1Type = Asn1TimeTypeAdapterFactory.build_START_END_DATE_TIME_INTERVAL_ENCODING(typeInstance, mixedSetting);
          break;
        case 37:
          asn1Type = Asn1TimeTypeAdapterFactory.build_DURATION_INTERVAL_ENCODING(typeInstance);
          break;
        case 38:
          asn1Type = Asn1TimeTypeAdapterFactory.build_START_DATE_DURATION_INTERVAL_ENCODING(typeInstance, mixedSetting);
          break;
        case 39:
          asn1Type = Asn1TimeTypeAdapterFactory.build_START_TIME_DURATION_INTERVAL_ENCODING(typeInstance, mixedSetting);
          break;
        case 40:
          asn1Type = Asn1TimeTypeAdapterFactory.build_START_DATE_TIME_DURATION_INTERVAL_ENCODING(typeInstance, mixedSetting);
          break;
        case 41:
          asn1Type = Asn1TimeTypeAdapterFactory.build_DURATION_END_DATE_INTERVAL_ENCODING(typeInstance, mixedSetting);
          break;
        case 42:
          asn1Type = Asn1TimeTypeAdapterFactory.build_DURATION_END_TIME_INTERVAL_ENCODING(typeInstance, mixedSetting);
          break;
        case 43:
          asn1Type = Asn1TimeTypeAdapterFactory.build_DURATION_END_DATE_TIME_INTERVAL_ENCODING(typeInstance, mixedSetting);
          break;
        case 44:
          asn1Type = Asn1TimeTypeAdapterFactory.build_REC_START_END_DATE_INTERVAL_ENCODING(typeInstance, mixedSetting);
          break;
        case 45:
          asn1Type = Asn1TimeTypeAdapterFactory.build_REC_START_END_TIME_INTERVAL_ENCODING(typeInstance, mixedSetting);
          break;
        case 46:
          asn1Type = Asn1TimeTypeAdapterFactory.build_REC_START_END_DATE_TIME_INTERVAL_ENCODING(typeInstance, mixedSetting);
          break;
        case 47:
          asn1Type = Asn1TimeTypeAdapterFactory.build_REC_DURATION_INTERVAL_ENCODING(typeInstance, mixedSetting);
          break;
        case 48:
          asn1Type = Asn1TimeTypeAdapterFactory.build_REC_START_DATE_DURATION_INTERVAL_ENCODING(typeInstance, mixedSetting);
          break;
        case 49:
          asn1Type = Asn1TimeTypeAdapterFactory.build_REC_START_TIME_DURATION_INTERVAL_ENCODING(typeInstance, mixedSetting);
          break;
        case 50:
          asn1Type = Asn1TimeTypeAdapterFactory.build_REC_START_DATE_TIME_DURATION_INTERVAL_ENCODING(typeInstance, mixedSetting);
          break;
        case 51:
          asn1Type = Asn1TimeTypeAdapterFactory.build_REC_DURATION_END_DATE_INTERVAL_ENCODING(typeInstance, mixedSetting);
          break;
        case 52:
          asn1Type = Asn1TimeTypeAdapterFactory.build_REC_DURATION_END_TIME_INTERVAL_ENCODING(typeInstance, mixedSetting);
          break;
        case 53:
          asn1Type = Asn1TimeTypeAdapterFactory.build_REC_DURATION_END_DATE_TIME_INTERVAL_ENCODING(typeInstance, mixedSetting);
          break;
        case 101:
          asn1Type = (Asn1Type) new MonthAdapter(typeInstance);
          break;
        case 102:
          asn1Type = (Asn1Type) new DayAdapter(typeInstance, 31L);
          break;
        case 103:
          asn1Type = (Asn1Type) new DayAdapter(typeInstance, 366L);
          break;
        case 104:
          asn1Type = (Asn1Type) new DayAdapter(typeInstance, 7L);
          break;
        case 105:
          asn1Type = (Asn1Type) new WeekAdapter(typeInstance);
          break;
        case 106:
          asn1Type = (Asn1Type) new HoursAdapter(typeInstance);
          break;
        case 107:
          asn1Type = (Asn1Type) new HoursUTCAdapter(typeInstance);
          break;
        case 108:
          asn1Type = (Asn1Type) new MinutesAdapter(typeInstance);
          break;
        case 109:
          asn1Type = (Asn1Type) new SecondsAdapter(typeInstance);
          break;
        case 110:
          asn1Type = (Asn1Type) new TimeDifferenceAdapter(typeInstance);
          break;
        case 111:
          asn1Type = Asn1TimeTypeAdapterFactory.build_localTimeHM(typeInstance);
          break;
        case 112:
          asn1Type = Asn1TimeTypeAdapterFactory.build_localTimeHMS(typeInstance);
          break;
        case 113:
          asn1Type = Asn1TimeTypeAdapterFactory.build_localTimeHMF(typeInstance);
          break;
        case 114:
          asn1Type = Asn1TimeTypeAdapterFactory.build_localTimeHMSF(typeInstance);
          break;
        case 115:
          asn1Type = (Asn1Type) new FractionAdapter(typeInstance);
          break;
        case 116:
          asn1Type = (Asn1Type) new RecurrenceAdapter(typeInstance);
          break;
        case 117:
          asn1Type = (Asn1Type) new FractionNumberDigitsAdapter(typeInstance);
          break;
        case 201:
          asn1Type = !mixedSetting ? Asn1TimeTypeAdapterFactory.build_Fixed_Date_Type(typeInstance) : Asn1TimeTypeAdapterFactory.build_Mixed_Date_Type(typeInstance);
          break;
        case 202:
          asn1Type = !mixedSetting ? Asn1TimeTypeAdapterFactory.build_Fixed_Time_Type(typeInstance) : Asn1TimeTypeAdapterFactory.build_Mixed_Time_Type(typeInstance);
          break;
        case 301:
          asn1Type = Asn1TimeTypeAdapterFactory.build_Mixed_Time_Type_choiceComponent(typeInstance);
          break;
        default:
          throw new ArgumentException("PER Time Type : invalid component ID (" + (object) adapterID + ")");
      }
      if (asn1Type == null)
        throw new ArgumentException("PER Time Type : null adapter for component ID (" + (object) adapterID + ")");
      else
        return asn1Type;
    }

    public static Asn1Type getAsn1TimeTypeAdapter(Asn1TimeType typeInstance)
    {
      try
      {
        return Asn1TimeTypeAdapterFactory.buildAdapterFromTypeSettings(typeInstance);
      }
      catch (FormatException ex)
      {
        return Asn1TimeTypeAdapterFactory.buildAdapterFromValueSettings(typeInstance);
      }
    }

    internal static Asn1TimeType getAsn1TimeTypeComponent(Asn1TimeType typeInstance, int fieldID)
    {
      switch (fieldID)
      {
        case 1:
          Asn1TimeType startPoint = typeInstance.StartPoint;
          if (startPoint == null)
          {
            typeInstance.StartPoint = new Asn1TimeType();
            startPoint = typeInstance.StartPoint;
          }
          return startPoint;
        case 2:
          Asn1TimeType endPoint = typeInstance.EndPoint;
          if (endPoint == null)
          {
            typeInstance.EndPoint = new Asn1TimeType();
            endPoint = typeInstance.EndPoint;
          }
          return endPoint;
        case 3:
          Asn1TimeType asn1TimeType = (Asn1TimeType) typeInstance.Duration;
          if (asn1TimeType == null)
          {
            Asn1DurationType asn1DurationType = new Asn1DurationType();
            typeInstance.Duration = asn1DurationType;
            asn1TimeType = (Asn1TimeType) typeInstance.Duration;
          }
          return asn1TimeType;
        default:
          throw new ArgumentException("PER Time Type : invalid field selector ID");
      }
    }

    internal static Asn1TimeType getAsn1TimeTypeComponent(Asn1TimeType typeInstance, int choiceID, int componentID)
    {
      Asn1TimeType asn1TimeType = typeInstance;
      if (choiceID == 1 && componentID == 37)
        asn1TimeType = Asn1TimeTypeAdapterFactory.getAsn1TimeTypeComponent(typeInstance, 3);
      return asn1TimeType;
    }

    private static int getBasicSettingFromValue(Asn1TimeType typeInstance)
    {
      if (typeInstance.Recurrence != -1)
        return 4;
      if (typeInstance.StartPoint != null || typeInstance.EndPoint != null || typeInstance.Duration != null)
        return 3;
      bool flag1 = Asn1TimeTypeAdapterFactory.isDate(typeInstance);
      bool flag2 = Asn1TimeTypeAdapterFactory.isTime(typeInstance);
      if (flag1 && flag2)
        return 2;
      if (flag1)
        return 0;
      if (!flag2)
        throw new FormatException("PER Time Type encoding : can not define Basic setting");
      else
        return 1;
    }

    private static int getCenturySettingFromValue(Asn1TimeType typeInstance)
    {
      int year = typeInstance.Year;
      if (15 <= year && year <= 99)
        return 0;
      if (0 <= year && year <= 14)
        return 1;
      if (-99 <= year && year <= -1)
        return 3;
      if (year > -100 && 100 > year)
        throw new FormatException("PER Time Type encoding : can not define century setting");
      else
        return 4;
    }

    private static int getDateSettingFromValue(Asn1TimeType typeInstance)
    {
      if (typeInstance.IsYearCentury())
        return 0;
      if (!typeInstance.IsYearDefined())
        throw new FormatException("PER Time Type encoding : can not define Date setting");
      if (typeInstance.Week != -1)
        return typeInstance.Day != -1 ? 6 : 5;
      else if (typeInstance.Month != -1)
        return typeInstance.Day != -1 ? 3 : 2;
      else
        return typeInstance.Day != -1 ? 4 : 1;
    }

    private static int getIntervalTypeSettingFromValue(Asn1TimeType typeInstance)
    {
      if (typeInstance.StartPoint != null)
      {
        if (typeInstance.EndPoint != null)
          return 0;
        if (typeInstance.Duration == null)
          throw new FormatException("PER Time Type encoding : can not define Interval-type setting (start point without end point or duration)");
        else
          return 2;
      }
      else
      {
        if (typeInstance.Duration == null)
          throw new FormatException("PER Time Type encoding : can not define Interval-type setting (no start point and no duration)");
        return typeInstance.EndPoint != null ? 3 : 1;
      }
    }

    private static int getLocalOrUTCSettingFromValue(Asn1TimeType typeInstance)
    {
      if (typeInstance.IsTimeUTC)
        return 1;
      return typeInstance.DifferenceFromUTC == int.MinValue ? 0 : 2;
    }

    private static int getSEPointSettingFromValue(Asn1TimeType typeInstance)
    {
      bool flag1 = Asn1TimeTypeAdapterFactory.isDate(typeInstance);
      bool flag2 = Asn1TimeTypeAdapterFactory.isTime(typeInstance);
      if (flag1 && flag2)
        return 2;
      if (flag1)
        return 0;
      if (!flag2)
        throw new FormatException("PER Time Type encoding : can not define SE-point setting");
      else
        return 1;
    }

    private static int getTimeSettingFromValue(Asn1TimeType typeInstance)
    {
      if (typeInstance.Hour == -1)
        throw new FormatException("PER Time Type encoding : can not define Time setting");
      if (typeInstance.Minute != -1)
      {
        if (typeInstance.Second != -1)
          return typeInstance.Fraction != -1.0 ? 5 : 2;
        else
          return typeInstance.Fraction != -1.0 ? 4 : 1;
      }
      else
        return typeInstance.Fraction != -1.0 ? 3 : 0;
    }

    private static int getYearSettingFromValue(Asn1TimeType typeInstance)
    {
      int year = typeInstance.Year;
      if (1582 <= year && year <= 9999)
        return 0;
      if (0 <= year && year <= 1581)
        return 1;
      if (-9999 <= year && year <= -1)
        return 3;
      if (year > -10000 && 10000 > year)
        throw new FormatException("PER Time Type encoding : can not define Year setting");
      else
        return 4;
    }

    internal static bool isAsn1TimeTypeComponentDefined(Asn1TimeType typeInstance, int testerID)
    {
      bool flag = false;
      if ((testerID & 256) != 256)
        return flag;
      switch (testerID & (int) byte.MaxValue)
      {
        case 1:
          return typeInstance.Recurrence != -2;
        case 2:
          return typeInstance.Fraction != -1.0;
        default:
          throw new ArgumentException("PER Time Type : invalid optional field tester ID");
      }
    }

    private static bool isDate(Asn1TimeType typeInstance)
    {
      if (!typeInstance.IsYearDefined() && typeInstance.Month == -1 && typeInstance.Week == -1)
        return typeInstance.Day != -1;
      else
        return true;
    }

    private static bool isTime(Asn1TimeType typeInstance)
    {
      if (typeInstance.Hour == -1 && typeInstance.Minute == -1)
        return typeInstance.Second != -1;
      else
        return true;
    }

    private static int selectAdapterForBasicSetting(Asn1TimeType typeInstance, Asn1TimeTypeAdapterFactory.ISettingsExplorer settingsExplorer)
    {
      switch (settingsExplorer.getBasicSetting(typeInstance))
      {
        case 0:
          return Asn1TimeTypeAdapterFactory.selectAdapterForDateSetting(typeInstance, settingsExplorer);
        case 1:
          return Asn1TimeTypeAdapterFactory.selectAdapterForTimeSetting(typeInstance, settingsExplorer);
        case 2:
          int num = 33;
          settingsExplorer.validateDateTimeSettings(typeInstance);
          return num;
        case 3:
          return 34 + Asn1TimeTypeAdapterFactory.selectAdapterForIntervalTypeSetting(typeInstance, settingsExplorer);
        case 4:
          return 44 + Asn1TimeTypeAdapterFactory.selectAdapterForIntervalTypeSetting(typeInstance, settingsExplorer);
        default:
          throw new FormatException("PER Time Type encoding : invalid Basic setting");
      }
    }

    private static int selectAdapterForDateSetting(Asn1TimeType typeInstance, Asn1TimeTypeAdapterFactory.ISettingsExplorer settingsExplorer)
    {
      switch (settingsExplorer.getDateSetting(typeInstance))
      {
        case 0:
          switch (settingsExplorer.getCenturySetting(typeInstance))
          {
            case 0:
            case 1:
            case 2:
              return 1;
            case 3:
            case 4:
            case 5:
              return 2;
            default:
              throw new FormatException("PER Time Type encoding : invalid century/century setting");
          }
        case 1:
          switch (settingsExplorer.getYearSetting(typeInstance))
          {
            case 0:
            case 1:
            case 2:
              return 3;
            case 3:
            case 4:
            case 5:
              return 4;
            default:
              throw new FormatException("PER Time Type encoding : invalid year/year setting");
          }
        case 2:
          switch (settingsExplorer.getYearSetting(typeInstance))
          {
            case 0:
            case 1:
            case 2:
              return 5;
            case 3:
            case 4:
            case 5:
              return 6;
            default:
              throw new FormatException("PER Time Type encoding : invalid year/year setting");
          }
        case 3:
          switch (settingsExplorer.getYearSetting(typeInstance))
          {
            case 0:
            case 1:
            case 2:
              return 7;
            case 3:
            case 4:
            case 5:
              return 8;
            default:
              throw new FormatException("PER Time Type encoding : invalid YMD/year setting");
          }
        case 4:
          switch (settingsExplorer.getYearSetting(typeInstance))
          {
            case 0:
            case 1:
            case 2:
              return 9;
            case 3:
            case 4:
            case 5:
              return 10;
            default:
              throw new FormatException("PER Time Type encoding : invalid YD/year setting");
          }
        case 5:
          switch (settingsExplorer.getYearSetting(typeInstance))
          {
            case 0:
            case 1:
            case 2:
              return 11;
            case 3:
            case 4:
            case 5:
              return 12;
            default:
              throw new FormatException("PER Time Type encoding : invalid YW/year setting");
          }
        case 6:
          switch (settingsExplorer.getYearSetting(typeInstance))
          {
            case 0:
            case 1:
            case 2:
              return 13;
            case 3:
            case 4:
            case 5:
              return 14;
            default:
              throw new FormatException("PER Time Type encoding : invalid YWD/year setting");
          }
        default:
          throw new FormatException("PER Time Type encoding : invalid Date setting");
      }
    }

    private static int selectAdapterForIntervalTypeSetting(Asn1TimeType typeInstance, Asn1TimeTypeAdapterFactory.ISettingsExplorer settingsExplorer)
    {
      switch (settingsExplorer.getIntervalTypeSetting(typeInstance))
      {
        case 0:
          switch (settingsExplorer.getStartPointSetting(typeInstance))
          {
            case 0:
              int num1 = 0;
              settingsExplorer.validateDateSettings(typeInstance);
              return num1;
            case 1:
              int num2 = 1;
              settingsExplorer.validateTimeSettings(typeInstance);
              return num2;
            case 2:
              int num3 = 2;
              settingsExplorer.validateDateTimeSettings(typeInstance);
              return num3;
            default:
              throw new FormatException("PER Time Type encoding : invalid SE/SE-point setting");
          }
        case 1:
          return 3;
        case 2:
          switch (settingsExplorer.getStartPointSetting(typeInstance))
          {
            case 0:
              int num4 = 4;
              settingsExplorer.validateDateSettings(typeInstance);
              return num4;
            case 1:
              int num5 = 5;
              settingsExplorer.validateTimeSettings(typeInstance);
              return num5;
            case 2:
              int num6 = 6;
              settingsExplorer.validateDateTimeSettings(typeInstance);
              return num6;
            default:
              throw new FormatException("PER Time Type encoding : invalid SD/SE-point setting");
          }
        case 3:
          switch (settingsExplorer.getEndPointSetting(typeInstance))
          {
            case 0:
              int num7 = 7;
              settingsExplorer.validateDateSettings(typeInstance);
              return num7;
            case 1:
              int num8 = 8;
              settingsExplorer.validateTimeSettings(typeInstance);
              return num8;
            case 2:
              int num9 = 9;
              settingsExplorer.validateDateTimeSettings(typeInstance);
              return num9;
            default:
              throw new FormatException("PER Time Type encoding : invalid DE/SE-point setting");
          }
        default:
          throw new FormatException("PER Time Type encoding : invalid Interval-type setting");
      }
    }

    private static int selectAdapterForTimeSetting(Asn1TimeType typeInstance, Asn1TimeTypeAdapterFactory.ISettingsExplorer settingsExplorer)
    {
      switch (settingsExplorer.getTimeSetting(typeInstance))
      {
        case 0:
          switch (settingsExplorer.getLocalOrUTCSetting(typeInstance))
          {
            case 0:
              return 15;
            case 1:
              return 16;
            case 2:
              return 17;
            default:
              throw new FormatException("PER Time Type encoding : invalid H/Local-or-UTC setting");
          }
        case 1:
          switch (settingsExplorer.getLocalOrUTCSetting(typeInstance))
          {
            case 0:
              return 18;
            case 1:
              return 19;
            case 2:
              return 20;
            default:
              throw new FormatException("PER Time Type encoding : invalid HM/Local-or-UTC setting");
          }
        case 2:
          switch (settingsExplorer.getLocalOrUTCSetting(typeInstance))
          {
            case 0:
              return 21;
            case 1:
              return 22;
            case 2:
              return 23;
            default:
              throw new FormatException("PER Time Type encoding : invalid HMS/Local-or-UTC setting");
          }
        case 3:
          switch (settingsExplorer.getLocalOrUTCSetting(typeInstance))
          {
            case 0:
              return 24;
            case 1:
              return 25;
            case 2:
              return 26;
            default:
              throw new FormatException("PER Time Type encoding : invalid H/Local-or-UTC setting");
          }
        case 4:
          switch (settingsExplorer.getLocalOrUTCSetting(typeInstance))
          {
            case 0:
              return 27;
            case 1:
              return 28;
            case 2:
              return 29;
            default:
              throw new FormatException("PER Time Type encoding : invalid HM/Local-or-UTC setting");
          }
        case 5:
          switch (settingsExplorer.getLocalOrUTCSetting(typeInstance))
          {
            case 0:
              return 30;
            case 1:
              return 31;
            case 2:
              return 32;
            default:
              throw new FormatException("PER Time Type encoding : invalid HMS/Local-or-UTC setting");
          }
        default:
          throw new FormatException("PER Time Type encoding : invalid Time setting");
      }
    }

    private interface ISettingsExplorer
    {
      int getBasicSetting(Asn1TimeType typeInstance);

      int getCenturySetting(Asn1TimeType typeInstance);

      int getDateSetting(Asn1TimeType typeInstance);

      int getEndPointSetting(Asn1TimeType typeInstance);

      int getIntervalTypeSetting(Asn1TimeType typeInstance);

      int getLocalOrUTCSetting(Asn1TimeType typeInstance);

      int getStartPointSetting(Asn1TimeType typeInstance);

      int getTimeSetting(Asn1TimeType typeInstance);

      int getYearSetting(Asn1TimeType typeInstance);

      bool isMixedSetting();

      void validateDateSettings(Asn1TimeType typeInstance);

      void validateDateTimeSettings(Asn1TimeType typeInstance);

      void validateTimeSettings(Asn1TimeType typeInstance);
    }

    private class TypeSettingsExplorer : Asn1TimeTypeAdapterFactory.ISettingsExplorer
    {
      private static Asn1TimeTypeAdapterFactory.TypeSettingsExplorer instance = (Asn1TimeTypeAdapterFactory.TypeSettingsExplorer) null;

      private TypeSettingsExplorer()
      {
      }

      public int getBasicSetting(Asn1TimeType typeInstance)
      {
        return typeInstance.GetPropertySetting(0);
      }

      public int getCenturySetting(Asn1TimeType typeInstance)
      {
        return typeInstance.GetPropertySetting(2);
      }

      public int getDateSetting(Asn1TimeType typeInstance)
      {
        return typeInstance.GetPropertySetting(1);
      }

      public int getEndPointSetting(Asn1TimeType typeInstance)
      {
        return typeInstance.GetPropertySetting(6);
      }

      public static Asn1TimeTypeAdapterFactory.ISettingsExplorer getInstance()
      {
        if (Asn1TimeTypeAdapterFactory.TypeSettingsExplorer.instance == null)
          Asn1TimeTypeAdapterFactory.TypeSettingsExplorer.instance = new Asn1TimeTypeAdapterFactory.TypeSettingsExplorer();
        return (Asn1TimeTypeAdapterFactory.ISettingsExplorer) Asn1TimeTypeAdapterFactory.TypeSettingsExplorer.instance;
      }

      public int getIntervalTypeSetting(Asn1TimeType typeInstance)
      {
        return typeInstance.GetPropertySetting(5);
      }

      public int getLocalOrUTCSetting(Asn1TimeType typeInstance)
      {
        return typeInstance.GetPropertySetting(4);
      }

      public int getStartPointSetting(Asn1TimeType typeInstance)
      {
        return typeInstance.GetPropertySetting(6);
      }

      public int getTimeSetting(Asn1TimeType typeInstance)
      {
        return typeInstance.GetPropertySetting(3);
      }

      public int getYearSetting(Asn1TimeType typeInstance)
      {
        return typeInstance.GetPropertySetting(2);
      }

      public bool isMixedSetting()
      {
        return false;
      }

      public void validateDateSettings(Asn1TimeType typeInstance)
      {
        Asn1TimeTypeAdapterFactory.selectAdapterForDateSetting(typeInstance, (Asn1TimeTypeAdapterFactory.ISettingsExplorer) this);
      }

      public void validateDateTimeSettings(Asn1TimeType typeInstance)
      {
        this.validateDateSettings(typeInstance);
        this.validateTimeSettings(typeInstance);
      }

      public void validateTimeSettings(Asn1TimeType typeInstance)
      {
        Asn1TimeTypeAdapterFactory.selectAdapterForTimeSetting(typeInstance, (Asn1TimeTypeAdapterFactory.ISettingsExplorer) this);
      }
    }

    private class ValueSettingsExplorer : Asn1TimeTypeAdapterFactory.ISettingsExplorer
    {
      private static Asn1TimeTypeAdapterFactory.ValueSettingsExplorer instance = (Asn1TimeTypeAdapterFactory.ValueSettingsExplorer) null;

      private ValueSettingsExplorer()
      {
      }

      public int getBasicSetting(Asn1TimeType typeInstance)
      {
        return Asn1TimeTypeAdapterFactory.getBasicSettingFromValue(typeInstance);
      }

      public int getCenturySetting(Asn1TimeType typeInstance)
      {
        return Asn1TimeTypeAdapterFactory.getCenturySettingFromValue(typeInstance);
      }

      public int getDateSetting(Asn1TimeType typeInstance)
      {
        return Asn1TimeTypeAdapterFactory.getDateSettingFromValue(typeInstance);
      }

      public int getEndPointSetting(Asn1TimeType typeInstance)
      {
        return Asn1TimeTypeAdapterFactory.getSEPointSettingFromValue(typeInstance.EndPoint);
      }

      public static Asn1TimeTypeAdapterFactory.ISettingsExplorer getInstance()
      {
        if (Asn1TimeTypeAdapterFactory.ValueSettingsExplorer.instance == null)
          Asn1TimeTypeAdapterFactory.ValueSettingsExplorer.instance = new Asn1TimeTypeAdapterFactory.ValueSettingsExplorer();
        return (Asn1TimeTypeAdapterFactory.ISettingsExplorer) Asn1TimeTypeAdapterFactory.ValueSettingsExplorer.instance;
      }

      public int getIntervalTypeSetting(Asn1TimeType typeInstance)
      {
        return Asn1TimeTypeAdapterFactory.getIntervalTypeSettingFromValue(typeInstance);
      }

      public int getLocalOrUTCSetting(Asn1TimeType typeInstance)
      {
        return Asn1TimeTypeAdapterFactory.getLocalOrUTCSettingFromValue(typeInstance);
      }

      public int getStartPointSetting(Asn1TimeType typeInstance)
      {
        return Asn1TimeTypeAdapterFactory.getSEPointSettingFromValue(typeInstance.StartPoint);
      }

      public int getTimeSetting(Asn1TimeType typeInstance)
      {
        return Asn1TimeTypeAdapterFactory.getTimeSettingFromValue(typeInstance);
      }

      public int getYearSetting(Asn1TimeType typeInstance)
      {
        return Asn1TimeTypeAdapterFactory.getYearSettingFromValue(typeInstance);
      }

      public bool isMixedSetting()
      {
        return true;
      }

      public void validateDateSettings(Asn1TimeType typeInstance)
      {
      }

      public void validateDateTimeSettings(Asn1TimeType typeInstance)
      {
      }

      public void validateTimeSettings(Asn1TimeType typeInstance)
      {
      }
    }
  }
}
