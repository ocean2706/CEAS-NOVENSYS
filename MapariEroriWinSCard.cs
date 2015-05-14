// Decompiled with JetBrains decompiler
// Type: Novensys.eCard.SDK.MapariEroriWinSCard
// Assembly: Novensys.eCard.SDK, Version=1.1.56.0, Culture=neutral, PublicKeyToken=null
// MVID: 59F9E456-D3DA-4066-A4A4-692B516A775E
// Assembly location: C:\Program Files (x86)\CNAS\SIUI-SPITAL\Novensys.eCard.SDK.dll

using Novensys.eCard.SDK.PCSC;
using System;
using System.Collections.Generic;

namespace Novensys.eCard.SDK
{
  public class MapariEroriWinSCard : Dictionary<SCARD_ERROR, RaspunsuriOperatiiCardPeTipuriOperatii>
  {
    public MapariEroriWinSCard()
    {
      RaspunsuriOperatiiCardPeTipuriOperatii peTipuriOperatii1 = new RaspunsuriOperatiiCardPeTipuriOperatii();
      peTipuriOperatii1.Find((Predicate<RaspunsOperatieCardPeTipOperatie>) (x => x.TipOperatieCard == TipOperatieCard.ORICARE)).RaspunsOperatieCard = CoduriRaspunsOperatieCard.OK;
      this.Add(SCARD_ERROR.SCARD_S_SUCCESS, peTipuriOperatii1);
      RaspunsuriOperatiiCardPeTipuriOperatii peTipuriOperatii2 = new RaspunsuriOperatiiCardPeTipuriOperatii();
      peTipuriOperatii2.Find((Predicate<RaspunsOperatieCardPeTipOperatie>) (x => x.TipOperatieCard == TipOperatieCard.ORICARE)).RaspunsOperatieCard = CoduriRaspunsOperatieCard.ERR_CARD_ACCESARE;
      this.Add(SCARD_ERROR.SCARD_E_CARD_UNSUPPORTED, peTipuriOperatii2);
      this.Add(SCARD_ERROR.SCARD_W_UNPOWERED_CARD, peTipuriOperatii2);
      this.Add(SCARD_ERROR.SCARD_W_UNRESPONSIVE_CARD, peTipuriOperatii2);
      this.Add(SCARD_ERROR.SCARD_W_UNSUPPORTED_CARD, peTipuriOperatii2);
      RaspunsuriOperatiiCardPeTipuriOperatii peTipuriOperatii3 = new RaspunsuriOperatiiCardPeTipuriOperatii();
      peTipuriOperatii3.Find((Predicate<RaspunsOperatieCardPeTipOperatie>) (x => x.TipOperatieCard == TipOperatieCard.ORICARE)).RaspunsOperatieCard = CoduriRaspunsOperatieCard.ERR_CARD_LIPSA;
      this.Add(SCARD_ERROR.SCARD_W_REMOVED_CARD, peTipuriOperatii3);
      this.Add(SCARD_ERROR.SCARD_E_NO_SMARTCARD, peTipuriOperatii3);
      RaspunsuriOperatiiCardPeTipuriOperatii peTipuriOperatii4 = new RaspunsuriOperatiiCardPeTipuriOperatii();
      peTipuriOperatii4.Find((Predicate<RaspunsOperatieCardPeTipOperatie>) (x => x.TipOperatieCard == TipOperatieCard.ORICARE)).RaspunsOperatieCard = CoduriRaspunsOperatieCard.ERR_CARD_TERMINAL_DUPLICAT;
      this.Add(SCARD_ERROR.SCARD_E_DUPLICATE_READER, peTipuriOperatii4);
      RaspunsuriOperatiiCardPeTipuriOperatii peTipuriOperatii5 = new RaspunsuriOperatiiCardPeTipuriOperatii();
      peTipuriOperatii5.Find((Predicate<RaspunsOperatieCardPeTipOperatie>) (x => x.TipOperatieCard == TipOperatieCard.ORICARE)).RaspunsOperatieCard = CoduriRaspunsOperatieCard.ERR_INVALID_CARD;
      this.Add(SCARD_ERROR.SCARD_E_INVALID_PARAMETER, peTipuriOperatii5);
      this.Add(SCARD_ERROR.SCARD_E_UNKNOWN_CARD, peTipuriOperatii5);
      this.Add(SCARD_ERROR.SCARD_F_INTERNAL_ERROR, peTipuriOperatii5);
      RaspunsuriOperatiiCardPeTipuriOperatii peTipuriOperatii6 = new RaspunsuriOperatiiCardPeTipuriOperatii();
      peTipuriOperatii6.Find((Predicate<RaspunsOperatieCardPeTipOperatie>) (x => x.TipOperatieCard == TipOperatieCard.ORICARE)).RaspunsOperatieCard = CoduriRaspunsOperatieCard.ERR_CARD_TIMEOUT;
      this.Add(SCARD_ERROR.SCARD_E_TIMEOUT, peTipuriOperatii6);
      this.Add(SCARD_ERROR.SCARD_F_WAITED_TOO_LONG, peTipuriOperatii6);
      RaspunsuriOperatiiCardPeTipuriOperatii peTipuriOperatii7 = new RaspunsuriOperatiiCardPeTipuriOperatii();
      peTipuriOperatii7.Find((Predicate<RaspunsOperatieCardPeTipOperatie>) (x => x.TipOperatieCard == TipOperatieCard.ORICARE)).RaspunsOperatieCard = CoduriRaspunsOperatieCard.ERR_TERMINAL_DECONECTAT;
      this.Add(SCARD_ERROR.SCARD_E_UNKNOWN_READER, peTipuriOperatii7);
      RaspunsuriOperatiiCardPeTipuriOperatii peTipuriOperatii8 = new RaspunsuriOperatiiCardPeTipuriOperatii();
      peTipuriOperatii8.Add(new RaspunsOperatieCardPeTipOperatie(TipOperatieCard.AUTENTIFICARE, CoduriRaspunsOperatieCard.ERR_AUTENTIFICARE_ABANDON));
      peTipuriOperatii8.Add(new RaspunsOperatieCardPeTipOperatie(TipOperatieCard.ACTIVARE, CoduriRaspunsOperatieCard.ERR_CARD_ACTIVARE_ABANDON));
      peTipuriOperatii8.Add(new RaspunsOperatieCardPeTipOperatie(TipOperatieCard.RESETARE_PIN, CoduriRaspunsOperatieCard.ERR_RESETARE_PIN_ABANDON));
      peTipuriOperatii8.Add(new RaspunsOperatieCardPeTipOperatie(TipOperatieCard.SCHIMBARE_PIN, CoduriRaspunsOperatieCard.ERR_SCHIMBARE_PIN_ABANDON));
      this.Add(SCARD_ERROR.SCARD_W_CANCELLED_BY_USER, peTipuriOperatii8);
    }

    public CoduriRaspunsOperatieCard MapeazaEroare(SCARD_ERROR codEroareCard, TipOperatieCard tipOperatieCard)
    {
      if (!this.ContainsKey(codEroareCard))
        return CoduriRaspunsOperatieCard.ERR_OPERATIE_CARD;
      RaspunsuriOperatiiCardPeTipuriOperatii peTipuriOperatii = this[codEroareCard];
      RaspunsOperatieCardPeTipOperatie cardPeTipOperatie1 = peTipuriOperatii.Find((Predicate<RaspunsOperatieCardPeTipOperatie>) (x => x.TipOperatieCard == tipOperatieCard));
      if (cardPeTipOperatie1 != null)
        return cardPeTipOperatie1.RaspunsOperatieCard;
      RaspunsOperatieCardPeTipOperatie cardPeTipOperatie2 = peTipuriOperatii.Find((Predicate<RaspunsOperatieCardPeTipOperatie>) (x => x.TipOperatieCard == TipOperatieCard.ORICARE));
      if (cardPeTipOperatie2 != null)
        return cardPeTipOperatie2.RaspunsOperatieCard;
      else
        return CoduriRaspunsOperatieCard.ERR_OPERATIE_CARD;
    }

    public CoduriRaspunsOperatieCard ObtineEroareMapata(SCARD_ERROR codEroareCard)
    {
      return this.MapeazaEroare(codEroareCard, TipOperatieCard.ORICARE);
    }
  }
}
