// Decompiled with JetBrains decompiler
// Type: Novensys.eCard.SDK.RaspunsOperatieCardPeTipOperatie
// Assembly: Novensys.eCard.SDK, Version=1.1.56.0, Culture=neutral, PublicKeyToken=null
// MVID: 59F9E456-D3DA-4066-A4A4-692B516A775E
// Assembly location: C:\Program Files (x86)\CNAS\SIUI-SPITAL\Novensys.eCard.SDK.dll

namespace Novensys.eCard.SDK
{
  public class RaspunsOperatieCardPeTipOperatie
  {
    public CoduriRaspunsOperatieCard RaspunsOperatieCard { get; set; }

    public TipOperatieCard TipOperatieCard { get; set; }

    public RaspunsOperatieCardPeTipOperatie(TipOperatieCard tipOperatieCard, CoduriRaspunsOperatieCard raspunsOperatieCard)
    {
      this.TipOperatieCard = tipOperatieCard;
      this.RaspunsOperatieCard = raspunsOperatieCard;
    }

    public RaspunsOperatieCardPeTipOperatie(CoduriRaspunsOperatieCard codRaspunsOperatieCard)
    {
      this.TipOperatieCard = TipOperatieCard.ORICARE;
      this.RaspunsOperatieCard = codRaspunsOperatieCard;
    }
  }
}
