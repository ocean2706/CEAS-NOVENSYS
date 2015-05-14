// Decompiled with JetBrains decompiler
// Type: Novensys.eCard.SDK.Entities.SmartCard.FisierCard
// Assembly: Novensys.eCard.SDK, Version=1.1.56.0, Culture=neutral, PublicKeyToken=null
// MVID: 59F9E456-D3DA-4066-A4A4-692B516A775E
// Assembly location: C:\Program Files (x86)\CNAS\SIUI-SPITAL\Novensys.eCard.SDK.dll

using System.Collections.Generic;

namespace Novensys.eCard.SDK.Entities.SmartCard
{
  public class FisierCard
  {
    public CoduriFisiereCard Cod { get; private set; }

    public string Nume { get; private set; }

    public string ComandaApduSelect { get; private set; }

    public List<CoduriCampuriCard> Campuri { get; private set; }

    public long? MarimeMaxima { get; private set; }

    public bool NecesitaDecodare { get; private set; }

    public FisierCard(CoduriFisiereCard cod, string nume, string comandaApduSelect, int? marimeMaxima, bool necesitaDecodare)
    {
      this.Cod = cod;
      this.Nume = nume;
      this.ComandaApduSelect = comandaApduSelect;
      int? nullable = marimeMaxima;
      this.MarimeMaxima = nullable.HasValue ? new long?((long) nullable.GetValueOrDefault()) : new long?();
      this.NecesitaDecodare = necesitaDecodare;
      this.Campuri = new List<CoduriCampuriCard>();
    }
  }
}
