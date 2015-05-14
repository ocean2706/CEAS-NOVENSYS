// Decompiled with JetBrains decompiler
// Type: Novensys.ASN1.Asn1Exception
// Assembly: Novensys.eCard.SDK, Version=1.1.56.0, Culture=neutral, PublicKeyToken=null
// MVID: 59F9E456-D3DA-4066-A4A4-692B516A775E
// Assembly location: C:\Program Files (x86)\CNAS\SIUI-SPITAL\Novensys.eCard.SDK.dll

using System;

namespace Novensys.ASN1
{
  [Serializable]
  public class Asn1Exception : Exception
  {
    private static string[] _messages = new string[100];
    public const int E_BadClassForOpenType = 17;
    public const int E_BadIndexForEnumerated = 30;
    public const int E_BadStreamAccess = 2;
    public const int E_BadTagNumberValue = 1;
    public const int E_BadValueForEnumerated = 25;
    public const int E_BadXERBooleanValue = 36;
    public const int E_BadXERControlSequence = 57;
    public const int E_BigIntegerNotSupported = 50;
    public const int E_CaughtVMError = 98;
    public const int E_CharacterNotInAlphabet = 31;
    public const int E_ChosenRootIndexOutOfBounds = 29;
    public const int E_ContainedTypeCannotBeDecodedWithType = 45;
    public const int E_ContainedTypeCannotBeEncodedWithType = 51;
    public const int E_EmptyOpenValue = 48;
    public const int E_IncorrectXMLEncoding = 40;
    public const int E_InputStreamError = 46;
    public const int E_InternalError = 99;
    public const int E_InvalidAnyAttributeFormat = 60;
    public const int E_InvalidBase64String = 63;
    public const int E_InvalidBaseForReal = 33;
    public const int E_InvalidBitStringNamedBit = 58;
    public const int E_InvalidDecoderForType = 26;
    public const int E_InvalidDecoderProperty = 42;
    public const int E_InvalidEncoderProperty = 41;
    public const int E_InvalidEncodingRules = 44;
    public const int E_InvalidGeneralizedTimeType = 52;
    public const int E_InvalidHexString = 62;
    public const int E_InvalidIntegerNamedNumber = 56;
    public const int E_InvalidIntegerValue = 27;
    public const int E_InvalidLowerBound = 23;
    public const int E_InvalidLowerSize = 19;
    public const int E_InvalidObjectIdentifierValue = 18;
    public const int E_InvalidPaddingBit = 47;
    public const int E_InvalidPiOrCommentPosition = 54;
    public const int E_InvalidPiOrCommentSyntax = 55;
    public const int E_InvalidRelativeOIDValue = 28;
    public const int E_InvalidTimeValue = 64;
    public const int E_InvalidTypeProperty = 43;
    public const int E_InvalidUpperBound = 24;
    public const int E_InvalidUpperSize = 20;
    public const int E_InvalidUTCTimeType = 53;
    public const int E_MandatoryComponentIsNotGiven = 15;
    public const int E_NoChosenAlternativeForChoice = 3;
    public const int E_NoEndOfContent = 14;
    public const int E_NoMoreData = 7;
    [Obsolete]
    public const int E_NotEnoughIdsForObjectIdentifierValue = 18;
    [Obsolete]
    public const int E_NotEnoughIdsForRelativeOIDValue = 28;
    public const int E_OidOverflow = 13;
    public const int E_OpenTypeCannotBeDecodedWithClass = 16;
    public const int E_OrderedComponentNotDefined = 61;
    public const int E_OverflowDecimalReal = 59;
    public const int E_OverflowInteger = 6;
    public const int E_OverflowLength = 5;
    public const int E_OverflowTagNumber = 4;
    public const int E_ReceivedNegativeLength = 22;
    public const int E_StringIndexOutOfBounds = 32;
    public const int E_UnexpectedElement = 49;
    public const int E_UnexpectedXERValue = 38;
    public const int E_UnexpectedXERValueFormat = 39;
    public const int E_UnexpectedXMLChild = 37;
    public const int E_UnsupportedCharacterEncoding = 21;
    public const int E_ValueBadClass = 10;
    public const int E_ValueBadLength = 12;
    public const int E_ValueBadTag = 11;
    public const int E_ValueShouldBeConstructed = 8;
    public const int E_ValueShouldBePrimitive = 9;
    public const int E_WriterCreationError = 34;
    public const int E_XERParsingError = 35;
    protected int _code;

    public virtual int ErrorCode
    {
      get
      {
        return this._code;
      }
    }

    public virtual string ErrorMessage
    {
      get
      {
        return this.Message;
      }
    }

    static Asn1Exception()
    {
      for (int index = 0; index < Asn1Exception._messages.Length; ++index)
        Asn1Exception._messages[index] = "";
      Asn1Exception._messages[1] = "Coding bad tag number";
      Asn1Exception._messages[2] = "Cannot access the underlying stream";
      Asn1Exception._messages[3] = "Coding CHOICE with no chosen alternative";
      Asn1Exception._messages[4] = "Received too big tag number";
      Asn1Exception._messages[5] = "Received too long length";
      Asn1Exception._messages[6] = "Integer received is too big";
      Asn1Exception._messages[7] = "Cannot read any more data";
      Asn1Exception._messages[8] = "Received value should be constructed";
      Asn1Exception._messages[9] = "Received value should be primitive";
      Asn1Exception._messages[10] = "Received bad tag class";
      Asn1Exception._messages[11] = "Received bad tag";
      Asn1Exception._messages[12] = "Received bad length";
      Asn1Exception._messages[13] = "Received too much ids for Object Identifier or RelativeOID";
      Asn1Exception._messages[14] = "Received no indefinite length end";
      Asn1Exception._messages[15] = "Mandatory component is not valued";
      Asn1Exception._messages[16] = "Open type cannot be decoded with given type";
      Asn1Exception._messages[17] = "Given Class cannot be used for decoding open type";
      Asn1Exception._messages[18] = "Invalid OBJECT IDENTIFIER value";
      Asn1Exception._messages[19] = "Lower size is not valid";
      Asn1Exception._messages[20] = "Upper size is not valid";
      Asn1Exception._messages[21] = "Unsupported character encoding";
      Asn1Exception._messages[22] = "Received negative length";
      Asn1Exception._messages[23] = "Lower bound value is not respected";
      Asn1Exception._messages[24] = "Upper bound value is not respected";
      Asn1Exception._messages[25] = "Bad value for ENUMERATED";
      Asn1Exception._messages[26] = "Invalid decoder for type";
      Asn1Exception._messages[27] = "Invalid INTEGER value";
      Asn1Exception._messages[28] = "Invalid RELATIVE-OID value";
      Asn1Exception._messages[29] = "Index out of root choice bounds";
      Asn1Exception._messages[30] = "Bad index for ENUMERATED";
      Asn1Exception._messages[31] = "Character not in alphabet";
      Asn1Exception._messages[32] = "Character index out of bounds";
      Asn1Exception._messages[33] = "Invalid base for REAL";
      Asn1Exception._messages[34] = "Creation of the underlying UTF-8 writer is impossible";
      Asn1Exception._messages[35] = "XER parsing error";
      Asn1Exception._messages[36] = "Bad XER BOOLEAN value";
      Asn1Exception._messages[37] = "Unexpected XML child";
      Asn1Exception._messages[38] = "Unexpected XER value";
      Asn1Exception._messages[39] = "Unexpected XER value format";
      Asn1Exception._messages[40] = "Incorrect XER encoding";
      Asn1Exception._messages[41] = "Invalid encoder property";
      Asn1Exception._messages[42] = "Invalid encoder property";
      Asn1Exception._messages[43] = "Invalid type property";
      Asn1Exception._messages[44] = "Invalid encoding rules";
      Asn1Exception._messages[45] = "Contained type cannot be decoded with given type";
      Asn1Exception._messages[46] = "Exception is thrown on input stream";
      Asn1Exception._messages[47] = "Invalid padding bit";
      Asn1Exception._messages[48] = "Open value contains no encoding";
      Asn1Exception._messages[49] = "Component or alternative is unexpected";
      Asn1Exception._messages[50] = "BigInteger is not supported";
      Asn1Exception._messages[51] = "Contained type cannot be encoded with given type";
      Asn1Exception._messages[52] = "GeneralizedTime value is invalid";
      Asn1Exception._messages[53] = "UTCTime value is invalid";
      Asn1Exception._messages[54] = "XML PI or comment position is invalid";
      Asn1Exception._messages[55] = "XML PI or comment syntax is invalid";
      Asn1Exception._messages[56] = "Invalid INTEGER named number";
      Asn1Exception._messages[57] = "Invalid XER escape sequence for a control character";
      Asn1Exception._messages[58] = "Invalid BIT STRING named bit";
      Asn1Exception._messages[59] = "Overflow while encoding DECIMAL REAL";
      Asn1Exception._messages[60] = "Invalid format for an attribute specified by ANY-ATTRIBUTES";
      Asn1Exception._messages[61] = "Ordered component (specified by USE-ORDER) is not defined";
      Asn1Exception._messages[62] = "Invalid hexadecimal string";
      Asn1Exception._messages[63] = "Invalid BASE64 string";
      Asn1Exception._messages[64] = "Invalid TIME (or useful time type) value";
      Asn1Exception._messages[98] = "Caugth execution environment error - see documentation -";
      Asn1Exception._messages[99] = "Internal error (see documentation for sending log file to support)";
    }

    public Asn1Exception(Asn1Exception ex)
      : base(ex.Message)
    {
      this._code = 0;
      this._code = ex.ErrorCode;
    }

    public Asn1Exception(Asn1Exception ex, int index)
      : base(ex.Message + Asn1Exception.buildIndexInfo(index))
    {
      this._code = 0;
      this._code = ex.ErrorCode;
    }

    public Asn1Exception(int code, string info)
      : base(Asn1Exception.buildErrorMessage(code, info))
    {
      this._code = 0;
      this._code = code;
    }

    public Asn1Exception(Asn1Exception ex, int index, Exception cause)
      : base(ex.Message + Asn1Exception.buildIndexInfo(index), cause)
    {
      this._code = 0;
      this._code = ex.ErrorCode;
    }

    public Asn1Exception(int code, string info, Exception cause)
      : base(Asn1Exception.buildErrorMessage(code, info), cause)
    {
      this._code = 0;
      this._code = code;
    }

    public Asn1Exception(int code, string info, int index)
      : base(Asn1Exception.buildErrorMessage(code, info) + Asn1Exception.buildIndexInfo(index))
    {
      this._code = 0;
      this._code = code;
    }

    private static string buildErrorMessage(int code, string info)
    {
      if (code < Asn1Exception._messages.Length)
        return "[" + (object) code + "] " + Asn1Exception._messages[code] + " (" + info + ")";
      else
        return string.Concat(new object[4]
        {
          (object) "code = ",
          (object) code,
          (object) " info = ",
          (object) info
        });
    }

    private static string buildIndexInfo(int index)
    {
      return " <stopped after '" + (object) index + "' bytes>";
    }
  }
}
