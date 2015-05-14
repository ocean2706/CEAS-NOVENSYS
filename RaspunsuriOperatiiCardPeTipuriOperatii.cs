// Decompiled with JetBrains decompiler
// Type: Novensys.eCard.SDK.RaspunsuriOperatiiCardPeTipuriOperatii
// Assembly: Novensys.eCard.SDK, Version=1.1.56.0, Culture=neutral, PublicKeyToken=null
// MVID: 59F9E456-D3DA-4066-A4A4-692B516A775E
// Assembly location: C:\Program Files (x86)\CNAS\SIUI-SPITAL\Novensys.eCard.SDK.dll

using System.Collections.Generic;

namespace Novensys.eCard.SDK
{
  public class RaspunsuriOperatiiCardPeTipuriOperatii : List<RaspunsOperatieCardPeTipOperatie>
  {
    public RaspunsuriOperatiiCardPeTipuriOperatii()
    {
      this.Add(new RaspunsOperatieCardPeTipOperatie(TipOperatieCard.ORICARE, CoduriRaspunsOperatieCard.ERR_OPERATIE_CARD));
    }
  }
}
