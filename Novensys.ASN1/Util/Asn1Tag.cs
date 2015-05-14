// Decompiled with JetBrains decompiler
// Type: Novensys.ASN1.Util.Asn1Tag
// Assembly: Novensys.eCard.SDK, Version=1.1.56.0, Culture=neutral, PublicKeyToken=null
// MVID: 59F9E456-D3DA-4066-A4A4-692B516A775E
// Assembly location: C:\Program Files (x86)\CNAS\SIUI-SPITAL\Novensys.eCard.SDK.dll

namespace Novensys.ASN1.Util
{
  public class Asn1Tag
  {
    public const int TAGCLASS_APPLICATION = 2;
    public const int TAGCLASS_CONTEXT = 4;
    public const int TAGCLASS_PRIVATE = 3;
    public const int TAGCLASS_UNIVERSAL = 1;
    public const int TAGCLASS_UNUSED = -1;
    public const int UTAG_BMP_STRING = 30;
    public const int UTAG_CHARACTER_STRING = 29;
    public const int UTAG_DATE = 31;
    public const int UTAG_DATE_TIME = 33;
    public const int UTAG_DURATION = 34;
    public const int UTAG_GENERAL_STRING = 27;
    public const int UTAG_GENERALIZED_TIME = 24;
    public const int UTAG_GRAPHIC_STRING = 25;
    public const int UTAG_IA5_STRING = 22;
    public const int UTAG_ISO646_STRING = 26;
    public const int UTAG_NUMERIC_STRING = 18;
    public const int UTAG_OBJECT_DESCRIPTOR = 7;
    public const int UTAG_PRINTABLE_STRING = 19;
    public const int UTAG_T61_STRING = 20;
    public const int UTAG_TELETEX_STRING = 20;
    public const int UTAG_TIME_OF_DAY = 32;
    public const int UTAG_TYPE_BITSTRING = 3;
    public const int UTAG_TYPE_BOOLEAN = 1;
    public const int UTAG_TYPE_EMBEDDED_PDV = 11;
    public const int UTAG_TYPE_ENUMERATED = 10;
    public const int UTAG_TYPE_EXTERNAL = 8;
    public const int UTAG_TYPE_INSTANCE_OF = 8;
    public const int UTAG_TYPE_INTEGER = 2;
    public const int UTAG_TYPE_NULL = 5;
    public const int UTAG_TYPE_OBJECT_IDENTIFIER = 6;
    public const int UTAG_TYPE_OCTETSTRING = 4;
    public const int UTAG_TYPE_REAL = 9;
    public const int UTAG_TYPE_RELATIVE_OID = 13;
    public const int UTAG_TYPE_SEQUENCE = 16;
    public const int UTAG_TYPE_SET = 17;
    public const int UTAG_TYPE_TIME = 14;
    public const int UTAG_UNIVERSAL_STRING = 28;
    public const int UTAG_UTC_TIME = 23;
    public const int UTAG_UTF8_STRING = 12;
    public const int UTAG_VIDEOTEX_STRING = 21;
    public const int UTAG_VISIBLE_STRING = 26;

    public static string GetPrintableTagClass(int c)
    {
      switch (c)
      {
        case 1:
          return "UNIVERSAL";
        case 2:
          return "APPLICATION";
        case 3:
          return "PRIVATE";
        case 4:
          return "CONTEXT";
        default:
          return "unknown";
      }
    }

    public static int GetTagClass(byte b)
    {
      switch ((int) b & 192)
      {
        case 128:
          return 4;
        case 192:
          return 3;
        case 0:
          return 1;
        case 64:
          return 2;
        default:
          return -1;
      }
    }
  }
}
