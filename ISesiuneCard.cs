// Decompiled with JetBrains decompiler
// Type: Novensys.eCard.SDK.ISesiuneCard
// Assembly: Novensys.eCard.SDK, Version=1.1.56.0, Culture=neutral, PublicKeyToken=null
// MVID: 59F9E456-D3DA-4066-A4A4-692B516A775E
// Assembly location: C:\Program Files (x86)\CNAS\SIUI-SPITAL\Novensys.eCard.SDK.dll

using Novensys.eCard.SDK.Entities;
using Novensys.eCard.SDK.Entities.SmartCard;
using Novensys.eCard.SDK.PCSC;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

namespace Novensys.eCard.SDK
{
  public interface ISesiuneCard
  {
    string Token { get; set; }

    WinSCard Card { get; set; }

    string TerminalCurent { get; }

    string TerminalId { get; }

    bool TerminalConectat { get; }

    bool TerminalCuTastatura { get; }

    StariAutentificare StareAutentificare { get; }

    StareCardInTerminal StareCardInTerminal { get; }

    int NumarIncercariRamase { get; }

    List<ProfileCard> Profile { get; }

    int? ProfilId { get; }

    StareCard StareCard { get; }

    ModEditarePeTerminal EditarePeTerminal { get; }

    StareComunicatieCuUM StareComunicatieCuUM { get; }

    bool NecesitaActualizare { get; }

    X509Certificate CertificatMAI { get; }

    event StareCardInTerminalSchimbataEventHandler StareCardInTerminalSchimbata;

    event DupaStareCardInTerminalSchimbataEventHandler DupaStareCardInTerminalSchimbata;

    event StareAutentificareSchimbataEventHandler StareAutentificareSchimbata;

    event StareCardSchimbataEventHandler StareCardSchimbata;

    event StareEditarePeTerminalSchimbataEventHandler StareEditarePeTerminalSchimbata;

    event StareComunicatieCuUMSchimbataEventHandler StareComunicatieCuUMSchimbata;

    event EventHandler CardActivatExtern;

    event EventHandler<ReadCardTextEventArgs> UserInputRequired;

    void Stop();

    string ObtineToken(string cif, IdentificatorDrepturi identificatorDrepturi);

    int ActiveazaCard(string token);

    int CitesteDate(string token, List<CoduriCampuriCard> campuriDeCitit, ref CardData cardData, ref Dictionary<CoduriCampuriCard, CoduriRaspunsOperatieCamp> rezultatOperatie);

    int EditeazaDate(string token, string cid, List<CoduriCampuriCard> campuriDeEditat, ref CardData cardData, ref Dictionary<CoduriCampuriCard, CoduriRaspunsOperatieCamp> rezultatOperatie);

    int SchimbaPIN(string token);

    int ReseteazaPIN(string token);

    byte[] ComputeHash(byte[] buffer);

    byte[] ComputeHash(byte[] buffer, int offset, int count);

    byte[] ComputeHashMAI(byte[] buffer);

    void SchimbareStareCardInTerminal(StareCardInTerminal stareCardInTerminal);

    void DupaSchimbareStareCardInTerminal(StareCardInTerminal stareCardInTerminal, CoduriRaspunsOperatieCard raspunsOperatieCard);

    void SchimbareStareAutentificare(StariAutentificare stareAutentificare);

    void SchimbareStareCard(StareCard stareCard);

    void SchimbareStareEditarePeTerminal(ModEditarePeTerminal editarePeTerminal);

    void SchimbareStareComunicatieCuUM(StareComunicatieCuUM stareComunicatieCuUM);
  }
}
