// Decompiled with JetBrains decompiler
// Type: Novensys.eCard.SDK.TCPCommunication.Informations
// Assembly: Novensys.eCard.SDK, Version=1.1.56.0, Culture=neutral, PublicKeyToken=null
// MVID: 59F9E456-D3DA-4066-A4A4-692B516A775E
// Assembly location: C:\Program Files (x86)\CNAS\SIUI-SPITAL\Novensys.eCard.SDK.dll

using System.Collections.Generic;

namespace Novensys.eCard.SDK.TCPCommunication
{
  public class Informations : Dictionary<InformationCodes, string>
  {
    public Informations()
    {
      this.Add(InformationCodes.INF_SERVICE_START, "Serviciul de comunicatie a pornit");
      this.Add(InformationCodes.INF_SERVICE_STOP, "Serviciul de comunicatie s-a oprit");
      this.Add(InformationCodes.INF_SEND_REQUEST, "Transmis cerere {0} \r\n{1}");
      this.Add(InformationCodes.INF_RECEIVE_RESPONSE, "Receptionat raspuns {0} \r\n{1}");
      this.Add(InformationCodes.INF_RECEIVE_REQUEST, "Receptionat cerere {0} \r\n{1}");
      this.Add(InformationCodes.INF_SEND_RESPONSE, "Transmis raspuns {0} \r\n{1}");
    }
  }
}
