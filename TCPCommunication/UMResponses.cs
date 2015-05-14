// Decompiled with JetBrains decompiler
// Type: Novensys.eCard.SDK.TCPCommunication.UMResponses
// Assembly: Novensys.eCard.SDK, Version=1.1.56.0, Culture=neutral, PublicKeyToken=null
// MVID: 59F9E456-D3DA-4066-A4A4-692B516A775E
// Assembly location: C:\Program Files (x86)\CNAS\SIUI-SPITAL\Novensys.eCard.SDK.dll

using Novensys.eCard.SDK;
using System.Collections.Generic;

namespace Novensys.eCard.SDK.TCPCommunication
{
  public class UMResponses : Dictionary<string, CoduriRaspunsOperatieCard>
  {
    public UMResponses()
    {
      this.Add("00", CoduriRaspunsOperatieCard.OK);
      this.Add("04", CoduriRaspunsOperatieCard.ERR_UM_STARE_CARD_INVALIDA);
      this.Add("05", CoduriRaspunsOperatieCard.ERR_UM_PROCESARE);
      this.Add("06", CoduriRaspunsOperatieCard.ERR_INVALID_TERMINAL);
      this.Add("08", CoduriRaspunsOperatieCard.ERR_UM_TIME_OUT);
      this.Add("12", CoduriRaspunsOperatieCard.ERR_UM_TRANZACTIE_INVALIDA);
      this.Add("14", CoduriRaspunsOperatieCard.ERR_INVALID_CARD);
      this.Add("30", CoduriRaspunsOperatieCard.ERR_UM_CERERE_INVALIDA);
      this.Add("44", CoduriRaspunsOperatieCard.ERR_CARD_BLOCKED);
      this.Add("55", CoduriRaspunsOperatieCard.ERR_INVALID_PIN);
      this.Add("56", CoduriRaspunsOperatieCard.ERR_CARD_NEINREGISTRAT);
      this.Add("94", CoduriRaspunsOperatieCard.ERR_UM_CA_NETWORK);
      this.Add("95", CoduriRaspunsOperatieCard.ERR_UM_ECARD_NETWORK);
      this.Add("96", CoduriRaspunsOperatieCard.ERR_UM_SYSTEM_ERROR);
    }
  }
}
