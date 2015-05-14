// Decompiled with JetBrains decompiler
// Type: Novensys.eCard.SDK.ISO8583.Iso8583MessageFactory
// Assembly: Novensys.eCard.SDK, Version=1.1.56.0, Culture=neutral, PublicKeyToken=null
// MVID: 59F9E456-D3DA-4066-A4A4-692B516A775E
// Assembly location: C:\Program Files (x86)\CNAS\SIUI-SPITAL\Novensys.eCard.SDK.dll

using System;
using System.Collections.Generic;

namespace Novensys.eCard.SDK.ISO8583
{
  public class Iso8583MessageFactory
  {
    public static int SystemTraceAuditNumber { get; private set; }

    public static Iso8583Message GetIso8583Message(int messageType, Dictionary<Fields, string> parameters)
    {
      return Iso8583MessageFactory.GetIso8583Message(messageType, parameters, false);
    }

    public static Iso8583Message GetIso8583Message(int messageType, Dictionary<Fields, string> parameters, bool generateSystemTraceAuditNumber)
    {
      Iso8583Message iso8583Message = new Iso8583Message();
      iso8583Message.MessageType = messageType;
      iso8583Message.TransmissionDateTime.SetNow();
      iso8583Message[12] = DateTime.Now.ToString("HHmmss");
      iso8583Message[13] = DateTime.Now.ToString("MMdd");
      if (generateSystemTraceAuditNumber)
      {
        Iso8583MessageFactory.SetNextSystemTraceAuditNumber();
        iso8583Message[11] = Iso8583MessageFactory.SystemTraceAuditNumber.ToString("000000");
      }
      if (parameters != null)
        Iso8583MessageFactory.SetParameters(parameters, ref iso8583Message);
      return iso8583Message;
    }

    private static void SetParameters(Dictionary<Fields, string> parameters, ref Iso8583Message iso8583Message)
    {
      foreach (KeyValuePair<Fields, string> keyValuePair in parameters)
        iso8583Message[(int) keyValuePair.Key] = keyValuePair.Value;
    }

    private static void SetNextSystemTraceAuditNumber()
    {
      if (Iso8583MessageFactory.SystemTraceAuditNumber <= 999999)
        ++Iso8583MessageFactory.SystemTraceAuditNumber;
      else
        Iso8583MessageFactory.SystemTraceAuditNumber = 1;
    }
  }
}
