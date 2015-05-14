// Decompiled with JetBrains decompiler
// Type: Novensys.eCard.SDK.SesiuneCard
// Assembly: Novensys.eCard.SDK, Version=1.1.56.0, Culture=neutral, PublicKeyToken=null
// MVID: 59F9E456-D3DA-4066-A4A4-692B516A775E
// Assembly location: C:\Program Files (x86)\CNAS\SIUI-SPITAL\Novensys.eCard.SDK.dll

using Novensys.eCard.SDK.Entities;
using Novensys.eCard.SDK.Entities.SmartCard;
using Novensys.eCard.SDK.Entities.Terminal;
using Novensys.eCard.SDK.PCSC;
using Novensys.eCard.SDK.PCSC.Apdu;
using Novensys.eCard.SDK.TCPCommunication;
using Novensys.eCard.SDK.Terminal;
using Novensys.eCard.SDK.Utils.Crypto;
using Novensys.eCard.SDK.Utils.Hex;
using Novensys.eCard.SDK.Utils.Log;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Novensys.eCard.SDK
{
  [Serializable]
  public class SesiuneCard : SesiuneLucru, ISesiuneCard
  {
    public string Token { get; set; }

    public WinSCard Card { get; set; }

    public StariAutentificare StareAutentificare
    {
      get
      {
        return this.TerminalManager.StareAutentificare;
      }
    }

    public int NumarIncercariRamase
    {
      get
      {
        return this.TerminalManager.NumarIncercariRamase;
      }
    }

    public StareCardInTerminal StareCardInTerminal
    {
      get
      {
        return this.TerminalManager.StareCardInTerminal;
      }
    }

    public int? ProfilId
    {
      get
      {
        return this.TerminalManager.ProfilId;
      }
    }

    public StareCard StareCard
    {
      get
      {
        return this.TerminalManager.StareCard;
      }
    }

    public bool TerminalConectat
    {
      get
      {
        return this.TerminalManager.TerminalConectat;
      }
    }

    public bool TerminalCuTastatura
    {
      get
      {
        return this.TerminalManager.TerminalCuTastatura;
      }
    }

    public ActivateStatus FlagStareActivare
    {
      get
      {
        return this.TerminalManager.FlagStareActivare;
      }
    }

    public ActivateStatusExtern StareActivareExterna
    {
      get
      {
        return this.TerminalManager.StareActivareExterna;
      }
    }

    public ModEditarePeTerminal EditarePeTerminal
    {
      get
      {
        return this.TerminalManager.EditarePeTerminal;
      }
    }

    public StareComunicatieCuUM StareComunicatieCuUM
    {
      get
      {
        return this.TerminalManager.StareComunicatieCuUM;
      }
    }

    public bool NecesitaActualizare
    {
      get
      {
        return this.TerminalManager.NecesitaActualizare;
      }
    }

    public X509Certificate CertificatMAI
    {
      get
      {
        return this.TerminalManager.CertificatMAI;
      }
    }

    public event StareCardInTerminalSchimbataEventHandler StareCardInTerminalSchimbata = null;

    public event DupaStareCardInTerminalSchimbataEventHandler DupaStareCardInTerminalSchimbata = null;

    public event StareAutentificareSchimbataEventHandler StareAutentificareSchimbata = null;

    public event StareCardSchimbataEventHandler StareCardSchimbata = null;

    public event StareEditarePeTerminalSchimbataEventHandler StareEditarePeTerminalSchimbata = null;

    public event StareComunicatieCuUMSchimbataEventHandler StareComunicatieCuUMSchimbata = null;

    public event EventHandler CardActivatExtern
    {
      add
      {
        this.TerminalManager.CardActivatExtern += value;
      }
      remove
      {
        this.TerminalManager.CardActivatExtern -= value;
      }
    }

    public event EventHandler<ReadCardTextEventArgs> UserInputRequired
    {
      add
      {
        this.TerminalManager.UserInputRequired += value;
      }
      remove
      {
        this.TerminalManager.UserInputRequired -= value;
      }
    }

    private SesiuneCard()
    {
    }

    internal static SesiuneCard GetNewInstance(TerminalManager terminalManager)
    {
      SesiuneCard sesiuneCard = new SesiuneCard();
      terminalManager.InitPCSC();
      terminalManager.Sesiune = (SesiuneLucru) sesiuneCard;
      sesiuneCard.TerminalManager = terminalManager;
      WinSCardContextJob.Instance.ReaderConnected += new EventHandler<CardEventArgs>(sesiuneCard.JobReaderConnected);
      WinSCardContextJob.Instance.CardInserted += new EventHandler<CardEventArgs>(sesiuneCard.JobCardInserted);
      WinSCardContextJob.Instance.CardRemoved += new EventHandler<CardEventArgs>(sesiuneCard.JobCardRemoved);
      if (terminalManager.StareCardInTerminal == StareCardInTerminal.CardInserat)
        sesiuneCard.SchimbareStareCardInTerminal(StareCardInTerminal.CardInserat);
      return sesiuneCard;
    }

    public void Stop()
    {
      WinSCardContextJob.Instance.ReaderConnected -= new EventHandler<CardEventArgs>(this.JobReaderConnected);
      WinSCardContextJob.Instance.CardInserted -= new EventHandler<CardEventArgs>(this.JobCardInserted);
      WinSCardContextJob.Instance.CardRemoved -= new EventHandler<CardEventArgs>(this.JobCardRemoved);
      this.Token = (string) null;
      this.TerminalManager.ProfilId = new int?();
      this.TerminalManager.ReseteazaDateCard();
      LogManager.CloseLogger();
    }

    private void JobReaderConnected(object sender, CardEventArgs e)
    {
      if (this.TerminalManager == null || !string.IsNullOrEmpty(this.TerminalManager.TerminalCurent) || this.TerminalManager.StareCardInTerminal != StareCardInTerminal.CardInserat)
        return;
      this.SchimbareStareCardInTerminal(StareCardInTerminal.CardInserat);
    }

    private void JobCardInserted(object sender, CardEventArgs e)
    {
      if (this.TerminalManager == null || !string.Equals(this.TerminalManager.TerminalCurent, e.ReaderFullName))
        return;
      this.SchimbareStareCardInTerminal(StareCardInTerminal.CardInserat);
    }

    private void JobCardRemoved(object sender, CardEventArgs e)
    {
      if (this.TerminalManager == null || !string.Equals(this.TerminalManager.TerminalCurent, e.ReaderFullName))
        return;
      this.SchimbareStareCardInTerminal(StareCardInTerminal.CardRetras);
    }

    private CoduriRaspunsOperatieCard MapEnum(CoduriRaspunsOperatieTerminal codRaspunsOperatieTerminal)
    {
      CoduriRaspunsOperatieCard raspunsOperatieCard = CoduriRaspunsOperatieCard.OK;
      switch (codRaspunsOperatieTerminal)
      {
        case CoduriRaspunsOperatieTerminal.ERR_TERMINAL_VERIFICARE:
          return CoduriRaspunsOperatieCard.ERR_TERMINAL_VERIFICARE;
        case CoduriRaspunsOperatieTerminal.ERR_TERMINAL_NEINROLAT:
          return CoduriRaspunsOperatieCard.ERR_INVALID_TERMINAL;
        case CoduriRaspunsOperatieTerminal.ERR_TERMINAL_MAI_MULT_DE_1:
          return CoduriRaspunsOperatieCard.ERR_TERMINAL_MAI_MULT_DE_1;
        case CoduriRaspunsOperatieTerminal.ERR_TERMINAL_DECONECTAT:
          return CoduriRaspunsOperatieCard.ERR_TERMINAL_DECONECTAT;
        case CoduriRaspunsOperatieTerminal.OK:
          return CoduriRaspunsOperatieCard.OK;
        default:
          return raspunsOperatieCard;
      }
    }

    public string ObtineToken(string cif, IdentificatorDrepturi identificatorDrepturi)
    {
      string token = (string) null;
      CoduriRaspunsOperatieCard raspunsOperatie = CoduriRaspunsOperatieCard.ERR_TOKEN_INVALID;
      try
      {
        LogManager.AddSeparatorLine();
        LogManager.FileLog("Verificare inrolare terminal");
        TerminalData terminalData = this.Inroleaza(cif, identificatorDrepturi);
        if (terminalData == null)
          throw new Exception(this.TerminalManager.MesajeRaspunsCard[CoduriRaspunsOperatieCard.ERR_INVALID_TERMINAL]);
        this.TerminalManager.TerminalData = terminalData;
        LogManager.FileLog("Obtinere token UM");
        DateTime serverDateTime = DateTime.MinValue;
        token = this.TerminalManager.UMClient.ExecutaObtineToken(cif, identificatorDrepturi, this.TerminalId, ref serverDateTime, ref raspunsOperatie);
        if (raspunsOperatie != CoduriRaspunsOperatieCard.OK)
          throw new Exception(this.TerminalManager.MesajeRaspunsCard[raspunsOperatie]);
        if (token != null)
          this.TerminalManager.SetProfilId(token, identificatorDrepturi, serverDateTime);
        this.Token = token;
      }
      catch (Exception ex)
      {
        LogManager.FileLog(ex);
        throw ex;
      }
      finally
      {
        LogManager.FileLog("Terminat Obtine Token");
      }
      return token;
    }

    private TerminalData Inroleaza(string cif, IdentificatorDrepturi identificatorDrepturi)
    {
      if (identificatorDrepturi == null)
        throw new ArgumentNullException("identificatorDrepturi");
      TerminalDataIdentifier id = new TerminalDataIdentifier();
      id.CIF = cif;
      id.NumarContract = identificatorDrepturi.NumarContract;
      id.DataContract = identificatorDrepturi.DataContract;
      id.CasaAsigurare = identificatorDrepturi.CasaAsigurare;
      id.TipFurnizor = identificatorDrepturi.TipFurnizor;
      id.CUI = identificatorDrepturi.CUI;
      id.NumeStatie = IPAddressUM.HostName;
      id.AdresaUM = IPAddressUM.Address.ToString();
      bool flag = TerminalDataStorage.Instance.Exists(id);
      LogManager.FileLog(string.Format("\r\n{0}\r\n{1}\r\n{2}", (object) id.HashText, (object) id.Hash, (object) (bool) (flag ? 1 : 0)));
      if (flag)
      {
        try
        {
          return TerminalDataStorage.Instance.Read(id);
        }
        catch (Exception ex)
        {
          LogManager.FileLog("Eroare folosire fisier local:");
          LogManager.FileLog(ex);
        }
      }
      TerminalData terminalDataFromUm = this.GetTerminalDataFromUM(id);
      if (terminalDataFromUm != null)
      {
        LogManager.FileLog(string.Format("Inrolarea a reusit, id terminal: {0}", (object) terminalDataFromUm.TerminalId));
        TerminalDataStorage.Instance.Write(id, terminalDataFromUm);
      }
      return terminalDataFromUm;
    }

    private TerminalData GetTerminalDataFromUM(TerminalDataIdentifier id)
    {
      if (id == null)
        throw new ArgumentNullException("id");
      try
      {
        CoduriRaspunsOperatieCard raspunsOperatie = CoduriRaspunsOperatieCard.ERR_UM_TERMINAL_DATA;
        string terminalId;
        string fileContent;
        string terminalMasterKey;
        this.TerminalManager.UMClient.ExecutaObtineTerminalData(id, ref raspunsOperatie, out terminalId, out fileContent, out terminalMasterKey);
        if (raspunsOperatie != CoduriRaspunsOperatieCard.OK)
          throw new Exception(this.TerminalManager.MesajeRaspunsCard[raspunsOperatie]);
        if (string.IsNullOrEmpty(terminalId))
          throw new Exception("In cadrul procesului de inrolare, Unitatea de management nu a generat identificatorul de terminal.");
        if (string.IsNullOrEmpty(terminalMasterKey))
          throw new Exception("In cadrul procesului de inrolare, Unitatea de management nu a returnat campul 59.");
        if (terminalMasterKey.Length < 32)
          throw new Exception("In cadrul procesului de inrolare, Unitatea de management a returnat o valoare incorecta pentru campul 59.");
        if (string.IsNullOrEmpty(fileContent))
          throw new Exception("In cadrul procesului de inrolare, Unitatea de management nu a returnat datele de inrolare.");
        TerminalData terminalData = TerminalDataStorage.Deserialize(DESEncryptor.Decrypt(Convert.FromBase64String(fileContent), Encoding.ASCII.GetBytes("lka@xx1-"), HexEncoding.GetBytes("0000000000000000"), CipherMode.CBC, PaddingMode.PKCS7));
        terminalData.TerminalId = terminalId;
        terminalData.CheieCriptarePIN = terminalMasterKey.Substring(0, 32);
        return terminalData;
      }
      catch (Exception ex)
      {
        LogManager.FileLog("Eroare inrolare prin UM:");
        LogManager.FileLog(ex);
        return (TerminalData) null;
      }
    }

    public int ActiveazaCard(string token)
    {
      CoduriRaspunsOperatieCard raspunsOperatieCard = CoduriRaspunsOperatieCard.ERR_CARD_ACTIVARE;
      try
      {
        LogManager.AddSeparatorLine();
        LogManager.FileLog("Start Activare Card");
        raspunsOperatieCard = this.MapEnum(this.TerminalManager.VerificaTerminal(true));
        if (raspunsOperatieCard != CoduriRaspunsOperatieCard.OK)
          return (int) raspunsOperatieCard;
        raspunsOperatieCard = this.TerminalManager.VerificaCardValid();
        if (raspunsOperatieCard != CoduriRaspunsOperatieCard.OK)
          return (int) raspunsOperatieCard;
        if (this.TerminalManager.StareCard == StareCard.Blocat)
          return -15;
        raspunsOperatieCard = this.TerminalManager.VerificaCardActivat();
        if (raspunsOperatieCard != CoduriRaspunsOperatieCard.OK && raspunsOperatieCard != CoduriRaspunsOperatieCard.ERR_CARD_NEACTIVAT)
          return (int) raspunsOperatieCard;
        if (this.StareCard == StareCard.Activ)
          return -18;
        raspunsOperatieCard = this.TerminalManager.VerificaToken(token);
        if (raspunsOperatieCard != CoduriRaspunsOperatieCard.OK)
          return (int) raspunsOperatieCard;
        int? profilId = this.ProfilId;
        if ((profilId.GetValueOrDefault() != 2 ? 1 : (!profilId.HasValue ? 1 : 0)) != 0)
          return -47;
        raspunsOperatieCard = this.TerminalManager.ValidareTerminal();
        if (raspunsOperatieCard != CoduriRaspunsOperatieCard.OK)
          return (int) raspunsOperatieCard;
        raspunsOperatieCard = this.TerminalManager.ExecutaSchimbarePINTransport(token, false);
        if (raspunsOperatieCard != CoduriRaspunsOperatieCard.OK)
          return (int) raspunsOperatieCard;
        this.TerminalManager.StareCard = StareCard.Activ;
      }
      catch (ApduException ex)
      {
        LogManager.FileLog((Exception) ex);
        return ex.MappingCode;
      }
      catch (WinSCardException ex)
      {
        LogManager.FileLog((Exception) ex);
        return (int) this.TerminalManager.MapariEroriWinSCard.MapeazaEroare((SCARD_ERROR) ex.Status, TipOperatieCard.ACTIVARE);
      }
      catch (Exception ex)
      {
        LogManager.FileLog(ex);
        return -19;
      }
      finally
      {
        LogManager.FileLog("Terminat Activare Card");
      }
      return (int) raspunsOperatieCard;
    }

    public int VerificaCardActivat()
    {
      if (this.TerminalManager.TerminalData == null)
        return -13;
      CoduriRaspunsOperatieCard index = this.TerminalManager.VerificaCardActivat();
      if (index != CoduriRaspunsOperatieCard.OK && index != CoduriRaspunsOperatieCard.ERR_CARD_NEACTIVAT)
        throw new Exception(this.TerminalManager.MesajeRaspunsCard[index]);
      this.SchimbareStareCard(this.StareCard);
      return (int) index;
    }

    public int CitesteDate(string token, List<CoduriCampuriCard> campuriDeCitit, ref CardData cardData, ref Dictionary<CoduriCampuriCard, CoduriRaspunsOperatieCamp> rezultatOperatie)
    {
      CoduriRaspunsOperatieCard raspunsOperatieCard = CoduriRaspunsOperatieCard.ERR_CARD_CITIRE;
      try
      {
        LogManager.AddSeparatorLine();
        LogManager.FileLog("Start Citire Date");
        raspunsOperatieCard = this.MapEnum(this.TerminalManager.VerificaTerminal(true));
        if (raspunsOperatieCard != CoduriRaspunsOperatieCard.OK)
          return (int) raspunsOperatieCard;
        raspunsOperatieCard = this.TerminalManager.VerificaCardValid();
        if (raspunsOperatieCard != CoduriRaspunsOperatieCard.OK)
          return (int) raspunsOperatieCard;
        raspunsOperatieCard = this.TerminalManager.VerificaCardActivat();
        if (raspunsOperatieCard != CoduriRaspunsOperatieCard.OK)
          return (int) raspunsOperatieCard;
        raspunsOperatieCard = this.TerminalManager.StareAutentificare != StariAutentificare.NEAUTENTIFICAT && this.TerminalManager.StareAutentificare != StariAutentificare.AUTENTIFICARE_ESUATA ? this.TerminalManager.Autentificare(false) : this.TerminalManager.Autentificare(true);
        if (this.TerminalManager.StareCard == StareCard.Blocat)
          cardData.StareCard = StareCard.Blocat;
        if (raspunsOperatieCard != CoduriRaspunsOperatieCard.OK)
          return (int) raspunsOperatieCard;
        if (this.TerminalManager.StareAutentificare == StariAutentificare.NEAUTENTIFICAT || this.TerminalManager.StareAutentificare == StariAutentificare.AUTENTIFICARE_ESUATA)
          return -11;
        if (this.TerminalManager.StareAutentificare == StariAutentificare.AUTENTIFICAT)
        {
          raspunsOperatieCard = this.TerminalManager.VerificaToken(token);
          if (raspunsOperatieCard != CoduriRaspunsOperatieCard.OK)
            return (int) raspunsOperatieCard;
        }
        cardData.StareCard = StareCard.Activ;
        raspunsOperatieCard = this.TerminalManager.CitesteDate(campuriDeCitit, cardData, rezultatOperatie);
      }
      catch (ApduException ex)
      {
        LogManager.FileLog((Exception) ex);
        return ex.MappingCode;
      }
      catch (WinSCardException ex)
      {
        LogManager.FileLog((Exception) ex);
        return (int) this.TerminalManager.MapariEroriWinSCard.ObtineEroareMapata((SCARD_ERROR) ex.Status);
      }
      catch (Exception ex)
      {
        LogManager.FileLog(ex);
        return -7;
      }
      finally
      {
        LogManager.FileLog("Terminat Citire Date");
      }
      return (int) raspunsOperatieCard;
    }

    public int EditeazaDate(string token, string cid, List<CoduriCampuriCard> campuriDeEditat, ref CardData cardData, ref Dictionary<CoduriCampuriCard, CoduriRaspunsOperatieCamp> rezultatOperatie)
    {
      CoduriRaspunsOperatieCard raspunsOperatieCard = CoduriRaspunsOperatieCard.ERR_CARD_SCRIERE;
      try
      {
        LogManager.AddSeparatorLine();
        LogManager.FileLog("Start Editare Date");
        raspunsOperatieCard = this.MapEnum(this.TerminalManager.VerificaTerminal(true));
        if (raspunsOperatieCard != CoduriRaspunsOperatieCard.OK)
          return (int) raspunsOperatieCard;
        raspunsOperatieCard = this.TerminalManager.VerificaCardValid();
        if (raspunsOperatieCard != CoduriRaspunsOperatieCard.OK)
          return (int) raspunsOperatieCard;
        raspunsOperatieCard = this.TerminalManager.VerificaCardActivat();
        if (raspunsOperatieCard != CoduriRaspunsOperatieCard.OK)
          return (int) raspunsOperatieCard;
        raspunsOperatieCard = this.TerminalManager.VerificaToken(token);
        if (raspunsOperatieCard != CoduriRaspunsOperatieCard.OK)
          return (int) raspunsOperatieCard;
        raspunsOperatieCard = this.TerminalManager.StareAutentificare != StariAutentificare.NEAUTENTIFICAT && this.TerminalManager.StareAutentificare != StariAutentificare.AUTENTIFICARE_ESUATA ? this.TerminalManager.Autentificare(false) : this.TerminalManager.Autentificare(true);
        if (this.TerminalManager.StareCard == StareCard.Blocat)
          cardData.StareCard = StareCard.Blocat;
        if (raspunsOperatieCard != CoduriRaspunsOperatieCard.OK)
          return (int) raspunsOperatieCard;
        if (this.TerminalManager.StareAutentificare != StariAutentificare.AUTENTIFICAT)
          return -11;
        cardData.StareCard = StareCard.Activ;
        raspunsOperatieCard = this.TerminalManager.ScrieDate(campuriDeEditat, cardData, rezultatOperatie);
      }
      catch (ApduException ex)
      {
        LogManager.FileLog((Exception) ex);
        return ex.MappingCode;
      }
      catch (WinSCardException ex)
      {
        LogManager.FileLog((Exception) ex);
        return (int) this.TerminalManager.MapariEroriWinSCard.ObtineEroareMapata((SCARD_ERROR) ex.Status);
      }
      catch (Exception ex)
      {
        LogManager.FileLog(ex);
        return -6;
      }
      finally
      {
        LogManager.FileLog("Terminat Editare Date");
      }
      return (int) raspunsOperatieCard;
    }

    public int SchimbaPIN(string token)
    {
      CoduriRaspunsOperatieCard raspunsOperatieCard = CoduriRaspunsOperatieCard.ERR_SCHIMBARE_PIN;
      try
      {
        LogManager.AddSeparatorLine();
        LogManager.FileLog("Start Schimbare PIN");
        raspunsOperatieCard = this.MapEnum(this.TerminalManager.VerificaTerminal(true));
        if (raspunsOperatieCard != CoduriRaspunsOperatieCard.OK)
          return (int) raspunsOperatieCard;
        raspunsOperatieCard = this.TerminalManager.VerificaCardValid();
        if (raspunsOperatieCard != CoduriRaspunsOperatieCard.OK)
          return (int) raspunsOperatieCard;
        if (this.TerminalManager.StareCard == StareCard.Blocat)
          return -15;
        raspunsOperatieCard = this.TerminalManager.VerificaCardActivat();
        if (raspunsOperatieCard != CoduriRaspunsOperatieCard.OK)
          return (int) raspunsOperatieCard;
        raspunsOperatieCard = this.TerminalManager.VerificaToken(token);
        if (raspunsOperatieCard != CoduriRaspunsOperatieCard.OK)
          return (int) raspunsOperatieCard;
        int? profilId = this.ProfilId;
        if ((profilId.GetValueOrDefault() != 2 ? 1 : (!profilId.HasValue ? 1 : 0)) != 0)
          return -48;
        raspunsOperatieCard = this.TerminalManager.StareAutentificare != StariAutentificare.NEAUTENTIFICAT && this.TerminalManager.StareAutentificare != StariAutentificare.AUTENTIFICARE_ESUATA ? this.TerminalManager.Autentificare(false) : this.TerminalManager.Autentificare(true);
        if (raspunsOperatieCard != CoduriRaspunsOperatieCard.OK)
          return (int) raspunsOperatieCard;
        raspunsOperatieCard = this.TerminalManager.ExecutaSchimbarePIN(token);
        if (raspunsOperatieCard != CoduriRaspunsOperatieCard.OK)
          return (int) raspunsOperatieCard;
      }
      catch (ApduException ex)
      {
        LogManager.FileLog((Exception) ex);
        return ex.MappingCode;
      }
      catch (WinSCardException ex)
      {
        LogManager.FileLog((Exception) ex);
        return (int) this.TerminalManager.MapariEroriWinSCard.MapeazaEroare((SCARD_ERROR) ex.Status, TipOperatieCard.SCHIMBARE_PIN);
      }
      catch (Exception ex)
      {
        LogManager.FileLog(ex);
        return -22;
      }
      finally
      {
        LogManager.FileLog("Terminat Schimbare PIN");
      }
      return (int) raspunsOperatieCard;
    }

    public int ReseteazaPIN(string token)
    {
      CoduriRaspunsOperatieCard raspunsOperatieCard = CoduriRaspunsOperatieCard.ERR_RESETARE_PIN;
      try
      {
        LogManager.AddSeparatorLine();
        LogManager.FileLog("Start Resetare PIN");
        raspunsOperatieCard = this.MapEnum(this.TerminalManager.VerificaTerminal(true));
        if (raspunsOperatieCard != CoduriRaspunsOperatieCard.OK)
          return (int) raspunsOperatieCard;
        raspunsOperatieCard = this.TerminalManager.VerificaCardValid();
        if (raspunsOperatieCard != CoduriRaspunsOperatieCard.OK)
          return (int) raspunsOperatieCard;
        raspunsOperatieCard = this.TerminalManager.VerificaToken(token);
        if (raspunsOperatieCard != CoduriRaspunsOperatieCard.OK)
          return (int) raspunsOperatieCard;
        raspunsOperatieCard = this.TerminalManager.ValidareTerminal();
        if (raspunsOperatieCard != CoduriRaspunsOperatieCard.OK)
          return (int) raspunsOperatieCard;
        raspunsOperatieCard = this.TerminalManager.ExecutaResetarePIN(token);
        if (raspunsOperatieCard != CoduriRaspunsOperatieCard.OK)
          return (int) raspunsOperatieCard;
      }
      catch (ApduException ex)
      {
        LogManager.FileLog((Exception) ex);
        return ex.MappingCode;
      }
      catch (WinSCardException ex)
      {
        LogManager.FileLog((Exception) ex);
        return (int) this.TerminalManager.MapariEroriWinSCard.MapeazaEroare((SCARD_ERROR) ex.Status, TipOperatieCard.RESETARE_PIN);
      }
      catch (Exception ex)
      {
        LogManager.FileLog(ex);
        return -35;
      }
      finally
      {
        LogManager.FileLog("Terminat Resetare PIN");
      }
      return (int) raspunsOperatieCard;
    }

    public byte[] ComputeHash(byte[] buffer)
    {
      byte[] dateSemnate = (byte[]) null;
      try
      {
        LogManager.AddSeparatorLine();
        LogManager.FileLog("Start Generare Semnatura");
        CoduriRaspunsOperatieCard index1 = this.MapEnum(this.TerminalManager.VerificaTerminal(true));
        if (index1 != CoduriRaspunsOperatieCard.OK)
          throw new Exception(this.TerminalManager.MesajeRaspunsCard[index1]);
        CoduriRaspunsOperatieCard index2 = this.TerminalManager.VerificaCardValid();
        if (index2 != CoduriRaspunsOperatieCard.OK)
          throw new Exception(this.TerminalManager.MesajeRaspunsCard[index2]);
        if (this.TerminalManager.StareCard == StareCard.Blocat)
          throw new Exception(this.TerminalManager.MesajeRaspunsCard[CoduriRaspunsOperatieCard.ERR_CARD_BLOCKED]);
        CoduriRaspunsOperatieCard index3 = this.TerminalManager.VerificaCardActivat();
        if (index3 != CoduriRaspunsOperatieCard.OK)
          throw new Exception(this.TerminalManager.MesajeRaspunsCard[index3]);
        CoduriRaspunsOperatieCard index4 = this.TerminalManager.StareAutentificare != StariAutentificare.NEAUTENTIFICAT && this.TerminalManager.StareAutentificare != StariAutentificare.AUTENTIFICARE_ESUATA ? this.TerminalManager.Autentificare(false) : this.TerminalManager.Autentificare(true);
        if (index4 != CoduriRaspunsOperatieCard.OK)
          throw new Exception(this.TerminalManager.MesajeRaspunsCard[index4]);
        if (this.TerminalManager.StareAutentificare == StariAutentificare.NEAUTENTIFICAT)
          throw new Exception(this.TerminalManager.MesajeRaspunsCard[CoduriRaspunsOperatieCard.ERR_AUTENTIFICARE]);
        CoduriRaspunsOperatieCard index5 = this.TerminalManager.SemneazaDate(buffer, TipCertificat.CertificatAsigurat, ref dateSemnate);
        if (index5 != CoduriRaspunsOperatieCard.OK)
          throw new Exception(this.TerminalManager.MesajeRaspunsCard[index5]);
      }
      catch (Exception ex)
      {
        LogManager.FileLog(ex);
        throw ex;
      }
      finally
      {
        LogManager.FileLog("Terminat Generare Semnatura");
      }
      return dateSemnate;
    }

    public byte[] ComputeHash(byte[] buffer, int offset, int count)
    {
      try
      {
        byte[] buffer1 = new byte[count];
        Array.Copy((Array) buffer, offset, (Array) buffer1, 0, count);
        return this.ComputeHash(buffer1);
      }
      catch (Exception ex)
      {
        LogManager.FileLog(ex);
        throw ex;
      }
    }

    public byte[] ComputeHashMAI(byte[] buffer)
    {
      byte[] dateSemnate = (byte[]) null;
      try
      {
        LogManager.AddSeparatorLine();
        LogManager.FileLog("Start Generare Semnatura");
        CoduriRaspunsOperatieCard index1 = this.MapEnum(this.TerminalManager.VerificaTerminal(true));
        if (index1 != CoduriRaspunsOperatieCard.OK)
          throw new Exception(this.TerminalManager.MesajeRaspunsCard[index1]);
        CoduriRaspunsOperatieCard index2 = this.TerminalManager.VerificaCardValid();
        if (index2 != CoduriRaspunsOperatieCard.OK)
          throw new Exception(this.TerminalManager.MesajeRaspunsCard[index2]);
        if (this.TerminalManager.StareCard == StareCard.Blocat)
          throw new Exception(this.TerminalManager.MesajeRaspunsCard[CoduriRaspunsOperatieCard.ERR_CARD_BLOCKED]);
        CoduriRaspunsOperatieCard index3 = this.TerminalManager.VerificaCardActivat();
        if (index3 != CoduriRaspunsOperatieCard.OK)
          throw new Exception(this.TerminalManager.MesajeRaspunsCard[index3]);
        CoduriRaspunsOperatieCard index4 = this.TerminalManager.StareAutentificare != StariAutentificare.NEAUTENTIFICAT && this.TerminalManager.StareAutentificare != StariAutentificare.AUTENTIFICARE_ESUATA ? this.TerminalManager.Autentificare(false) : this.TerminalManager.Autentificare(true);
        if (index4 != CoduriRaspunsOperatieCard.OK)
          throw new Exception(this.TerminalManager.MesajeRaspunsCard[index4]);
        if (this.TerminalManager.StareAutentificare == StariAutentificare.NEAUTENTIFICAT)
          throw new Exception(this.TerminalManager.MesajeRaspunsCard[CoduriRaspunsOperatieCard.ERR_AUTENTIFICARE]);
        CoduriRaspunsOperatieCard index5 = this.TerminalManager.SemneazaDate(buffer, TipCertificat.CertificatMAI, ref dateSemnate);
        if (index5 != CoduriRaspunsOperatieCard.OK)
          throw new Exception(this.TerminalManager.MesajeRaspunsCard[index5]);
      }
      catch (Exception ex)
      {
        LogManager.FileLog(ex);
        throw ex;
      }
      finally
      {
        LogManager.FileLog("Terminat Generare Semnatura");
      }
      return dateSemnate;
    }

    public void SchimbareStareCardInTerminal(StareCardInTerminal stareCardInTerminal)
    {
      CoduriRaspunsOperatieCard raspunsOperatieCard = CoduriRaspunsOperatieCard.OK;
      if (this.StareCardInTerminalSchimbata == null)
        return;
      this.StareCardInTerminalSchimbata(stareCardInTerminal);
      try
      {
        if (stareCardInTerminal == StareCardInTerminal.CardInserat)
        {
          LogManager.AddSeparatorLine();
          LogManager.FileLog("Start Inserare Card");
          raspunsOperatieCard = this.MapEnum(this.TerminalManager.VerificaTerminal(true));
          if (raspunsOperatieCard != CoduriRaspunsOperatieCard.OK)
            throw new Exception(this.TerminalManager.MesajeRaspunsCard[raspunsOperatieCard]);
          raspunsOperatieCard = this.TerminalManager.VerificaCardValid();
          if (raspunsOperatieCard != CoduriRaspunsOperatieCard.OK)
            throw new Exception(this.TerminalManager.MesajeRaspunsCard[raspunsOperatieCard]);
          raspunsOperatieCard = this.TerminalManager.VerificaCardActivat();
          if (raspunsOperatieCard != CoduriRaspunsOperatieCard.OK)
            throw new Exception(this.TerminalManager.MesajeRaspunsCard[raspunsOperatieCard]);
          if (this.TerminalManager.StareCard == StareCard.Activ)
          {
            if (this.TerminalManager.StareAutentificare == StariAutentificare.NEAUTENTIFICAT || this.TerminalManager.StareAutentificare == StariAutentificare.AUTENTIFICARE_ESUATA)
            {
              raspunsOperatieCard = this.TerminalManager.AutentificareLaInserareCard();
              if (raspunsOperatieCard != CoduriRaspunsOperatieCard.OK)
                throw new Exception(this.TerminalManager.MesajeRaspunsCard[raspunsOperatieCard]);
            }
          }
          else
            this.SchimbareStareCard(StareCard.Inactiv);
        }
        else
        {
          LogManager.AddSeparatorLine();
          LogManager.FileLog("Start Retragere Card");
          this.TerminalManager.ReseteazaDateCard();
        }
      }
      catch (Exception ex)
      {
        if (raspunsOperatieCard == CoduriRaspunsOperatieCard.OK)
          raspunsOperatieCard = CoduriRaspunsOperatieCard.ERR_AUTENTIFICARE;
        LogManager.FileLog(ex.Message);
      }
      finally
      {
        if (this.DupaStareCardInTerminalSchimbata != null)
          this.DupaStareCardInTerminalSchimbata(stareCardInTerminal, raspunsOperatieCard);
        LogManager.FileLog(stareCardInTerminal == StareCardInTerminal.CardInserat ? "Terminat Inserare Card" : "Terminat Retragere Card");
      }
    }

    public void DupaSchimbareStareCardInTerminal(StareCardInTerminal stareCardInTerminal, CoduriRaspunsOperatieCard raspunsOperatieCard)
    {
      if (this.DupaStareCardInTerminalSchimbata == null)
        return;
      this.DupaStareCardInTerminalSchimbata(stareCardInTerminal, raspunsOperatieCard);
    }

    public void SchimbareStareAutentificare(StariAutentificare stareAutentificare)
    {
      if (this.StareAutentificareSchimbata == null)
        return;
      this.StareAutentificareSchimbata(stareAutentificare);
    }

    public void SchimbareStareCard(StareCard stareCard)
    {
      if (this.StareCardSchimbata == null)
        return;
      this.StareCardSchimbata(stareCard);
    }

    public void SchimbareStareEditarePeTerminal(ModEditarePeTerminal editarePeTerminal)
    {
      if (this.StareEditarePeTerminalSchimbata == null)
        return;
      this.StareEditarePeTerminalSchimbata(editarePeTerminal);
    }

    public void SchimbareStareComunicatieCuUM(StareComunicatieCuUM stareComunicatieCuUM)
    {
      if (this.StareComunicatieCuUMSchimbata == null)
        return;
      this.StareComunicatieCuUMSchimbata(stareComunicatieCuUM);
    }
  }
}
