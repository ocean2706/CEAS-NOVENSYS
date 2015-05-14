// Decompiled with JetBrains decompiler
// Type: Novensys.eCard.SDK.TCPCommunication.ISO8583Util
// Assembly: Novensys.eCard.SDK, Version=1.1.56.0, Culture=neutral, PublicKeyToken=null
// MVID: 59F9E456-D3DA-4066-A4A4-692B516A775E
// Assembly location: C:\Program Files (x86)\CNAS\SIUI-SPITAL\Novensys.eCard.SDK.dll

using Novensys.eCard.SDK.Entities;
using Novensys.eCard.SDK.ISO8583;
using Novensys.eCard.SDK.Terminal;
using System;
using System.Collections.Generic;

namespace Novensys.eCard.SDK.TCPCommunication
{
  public static class ISO8583Util
  {
    private const string INITIAL_CARD_NUMBER = "0000000000000000";

    public static Iso8583Message BuildHandshakeRequest()
    {
      return Iso8583MessageFactory.GetIso8583Message(2048, new Dictionary<Fields, string>()
      {
        {
          Fields.FLD_070_NetworkManagementInformationCode,
          "301"
        },
        {
          Fields.FLD_032_AcquiringInstitutionIdCode,
          "0000000000"
        }
      }, true);
    }

    public static Iso8583Message BuildHandshakeResponse(Iso8583Message handshakeReq)
    {
      return Iso8583MessageFactory.GetIso8583Message(2064, new Dictionary<Fields, string>()
      {
        {
          Fields.FLD_070_NetworkManagementInformationCode,
          handshakeReq[70]
        },
        {
          Fields.FLD_032_AcquiringInstitutionIdCode,
          handshakeReq[32]
        },
        {
          Fields.FLD_011_SystemTraceAuditNumber,
          handshakeReq[11]
        },
        {
          Fields.FLD_039_ResponseCode,
          "00"
        }
      });
    }

    public static Iso8583Message BuildPINValidationRequest(string cardNumber, int retryCounter, string terminalId, string pinBlock, string certificateSerialNumber)
    {
      Dictionary<Fields, string> parameters = new Dictionary<Fields, string>();
      parameters.Add(Fields.FLD_002_PrimaryAccountNumber, cardNumber);
      string str1 = cardNumber == "0000000000000000" ? Convert.ToString(310000) : Convert.ToString(300000);
      parameters.Add(Fields.FLD_003_ProcessingCode, str1);
      parameters.Add(Fields.FLD_023_CardSequenceNumber, Convert.ToString(retryCounter));
      string str2 = string.Format("CNAS{0}", (object) Convert.ToString(Iso8583MessageFactory.SystemTraceAuditNumber + 1));
      parameters.Add(Fields.FLD_037_RetrievalReferenceNumber, str2);
      parameters.Add(Fields.FLD_041_CardAcceptorTerminalId, terminalId);
      parameters.Add(Fields.FLD_042_CardAcceptorIdCode, "CNAS");
      parameters.Add(Fields.FLD_052_PinData, pinBlock);
      parameters.Add(Fields.FLD_045_Track1Data, certificateSerialNumber);
      return Iso8583MessageFactory.GetIso8583Message(256, parameters, true);
    }

    internal static Iso8583Message BuildInrolareMessage(TerminalDataIdentifier id)
    {
      if (id == null)
        throw new ArgumentNullException("id");
      return Iso8583MessageFactory.GetIso8583Message(768, new Dictionary<Fields, string>()
      {
        {
          Fields.FLD_055_ReservedIso,
          id.NumarContract
        },
        {
          Fields.FLD_056_ReservedIso,
          id.TipFurnizor
        },
        {
          Fields.FLD_057_ReservedNational,
          id.CUI
        },
        {
          Fields.FLD_058_ReservedNational,
          id.CasaAsigurare
        },
        {
          Fields.FLD_059_ReservedNational,
          id.DataContract.ToString("yyyy-MM-dd HH:mm:ss")
        }
      }, true);
    }

    public static Iso8583Message BuildRightsRequest(IdentificatorDrepturi identificatorDrepturi, string terminalId)
    {
      return Iso8583MessageFactory.GetIso8583Message(256, new Dictionary<Fields, string>()
      {
        {
          Fields.FLD_003_ProcessingCode,
          Convert.ToString(320000)
        },
        {
          Fields.FLD_055_ReservedIso,
          identificatorDrepturi.NumarContract
        },
        {
          Fields.FLD_056_ReservedIso,
          identificatorDrepturi.TipFurnizor
        },
        {
          Fields.FLD_057_ReservedNational,
          identificatorDrepturi.DataContract.ToString("yyyy-MM-dd HH:mm:ss")
        },
        {
          Fields.FLD_058_ReservedNational,
          identificatorDrepturi.CUI
        },
        {
          Fields.FLD_059_ReservedNational,
          identificatorDrepturi.CasaAsigurare
        },
        {
          Fields.FLD_041_CardAcceptorTerminalId,
          terminalId
        }
      }, true);
    }

    public static Iso8583Message BuildChangePINTransportRequest(string pinBlockVechi, string pinBlockNou, string terminalId, string cardNumber, int retryCounter, string certificat, string certificateSerialNumber)
    {
      string str1 = string.Empty;
      Dictionary<Fields, string> parameters = new Dictionary<Fields, string>();
      parameters.Add(Fields.FLD_002_PrimaryAccountNumber, cardNumber);
      string str2 = Convert.ToString(940001);
      parameters.Add(Fields.FLD_003_ProcessingCode, str2);
      string str3 = string.Format("CNAS{0}", (object) Convert.ToString(Iso8583MessageFactory.SystemTraceAuditNumber + 1));
      parameters.Add(Fields.FLD_037_RetrievalReferenceNumber, str3);
      parameters.Add(Fields.FLD_041_CardAcceptorTerminalId, terminalId);
      parameters.Add(Fields.FLD_042_CardAcceptorIdCode, "CNAS");
      parameters.Add(Fields.FLD_052_PinData, pinBlockNou);
      parameters.Add(Fields.FLD_023_CardSequenceNumber, Convert.ToString(retryCounter));
      parameters.Add(Fields.FLD_045_Track1Data, certificateSerialNumber);
      List<string> list = ISO8583Util.SplitCertificat(certificat);
      string str4 = list.Count > 0 ? list[0] : string.Empty;
      parameters.Add(Fields.FLD_061_ReservedPrivate, str4);
      string str5 = list.Count > 1 ? list[1] : string.Empty;
      parameters.Add(Fields.FLD_062_ReservedPrivate, str5);
      string str6 = list.Count > 2 ? list[1] : string.Empty;
      parameters.Add(Fields.FLD_063_ReservedPrivate, str6);
      return Iso8583MessageFactory.GetIso8583Message(256, parameters, true);
    }

    public static Iso8583Message BuildChangePINRequest(string pinBlockVechi, string pinBlockNou, string terminalId, string cardNumber, int retryCounter, string certificateSerialNumber)
    {
      Dictionary<Fields, string> parameters = new Dictionary<Fields, string>();
      parameters.Add(Fields.FLD_002_PrimaryAccountNumber, cardNumber);
      string str1 = Convert.ToString(940000);
      parameters.Add(Fields.FLD_003_ProcessingCode, str1);
      string str2 = string.Format("CNAS{0}", (object) Convert.ToString(Iso8583MessageFactory.SystemTraceAuditNumber + 1));
      parameters.Add(Fields.FLD_037_RetrievalReferenceNumber, str2);
      parameters.Add(Fields.FLD_041_CardAcceptorTerminalId, terminalId);
      parameters.Add(Fields.FLD_042_CardAcceptorIdCode, "CNAS");
      parameters.Add(Fields.FLD_052_PinData, pinBlockNou);
      parameters.Add(Fields.FLD_065_ExtendedBitmap, pinBlockVechi);
      parameters.Add(Fields.FLD_023_CardSequenceNumber, Convert.ToString(retryCounter));
      parameters.Add(Fields.FLD_045_Track1Data, certificateSerialNumber);
      return Iso8583MessageFactory.GetIso8583Message(256, parameters, true);
    }

    public static Iso8583Message BuildResetPINRequest(string pinBlock, string terminalId, string cardNumber, string certificateSerialNumber)
    {
      Dictionary<Fields, string> parameters = new Dictionary<Fields, string>();
      parameters.Add(Fields.FLD_002_PrimaryAccountNumber, cardNumber);
      parameters.Add(Fields.FLD_003_ProcessingCode, Convert.ToString(940002));
      string str = string.Format("CNAS{0}", (object) Convert.ToString(Iso8583MessageFactory.SystemTraceAuditNumber + 1));
      parameters.Add(Fields.FLD_037_RetrievalReferenceNumber, str);
      parameters.Add(Fields.FLD_041_CardAcceptorTerminalId, terminalId);
      parameters.Add(Fields.FLD_042_CardAcceptorIdCode, "CNAS");
      parameters.Add(Fields.FLD_052_PinData, pinBlock);
      parameters.Add(Fields.FLD_045_Track1Data, certificateSerialNumber);
      return Iso8583MessageFactory.GetIso8583Message(256, parameters, true);
    }

    private static List<string> SplitCertificat(string certificat)
    {
      int val1 = 999;
      List<string> list = new List<string>();
      if (string.IsNullOrEmpty(certificat))
        return list;
      int startIndex = 0;
      while (startIndex < certificat.Length)
      {
        list.Add(certificat.Substring(startIndex, Math.Min(val1, certificat.Length - startIndex)));
        startIndex += val1;
      }
      return list;
    }

    private static string RecompuneCertificat(List<string> splitCertificat)
    {
      string str1 = string.Empty;
      foreach (string str2 in splitCertificat)
        str1 = str1 + (object) splitCertificat;
      return str1;
    }
  }
}
