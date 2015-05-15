// Decompiled with JetBrains decompiler
// Type: Novensys.eCard.SDK.TerminalManager
// Assembly: Novensys.eCard.SDK, Version=1.1.56.0, Culture=neutral, PublicKeyToken=null
// MVID: 59F9E456-D3DA-4066-A4A4-692B516A775E
// Assembly location: C:\Program Files (x86)\CNAS\SIUI-SPITAL\Novensys.eCard.SDK.dll

using Novensys.eCard.SDK.Entities;
using Novensys.eCard.SDK.Entities.SmartCard;
using Novensys.eCard.SDK.Entities.Terminal;
using Novensys.eCard.SDK.PCSC;
using Novensys.eCard.SDK.PCSC.Apdu;
using Novensys.eCard.SDK.Remoting;
using Novensys.eCard.SDK.TCPCommunication;
using Novensys.eCard.SDK.Utils.ASN1;
using Novensys.eCard.SDK.Utils.Crypto;
using Novensys.eCard.SDK.Utils.Hex;
using Novensys.eCard.SDK.Utils.Log;
using Org.BouncyCastle.Asn1.Cms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography.X509Certificates;

namespace Novensys.eCard.SDK
{
  internal class TerminalManager : ICardTextReader
  {
    private WinSCard card = new WinSCard();
    private StareCard stareCard = StareCard.Activ;
    private StariAutentificare stareAutentificare = StariAutentificare.NEAUTENTIFICAT;
    private StareComunicatieCuUM stareComunicatieCuUM = StareComunicatieCuUM.UM_INDISPONIBIL;
    private const string INITIAL_CARD_NUMBER = "0000000000000000";
    private const string INITIAL_PIN = "0000";
    private const string PIN_RESET = "0000";
    private const string PIN_TRANSPORT = "000";
    private string resolvedTerminalName;
    public int? profilId;
    private ModEditarePeTerminal editarePeTerminal;

    public bool ReadTextSupported
    {
      get
      {
        return this.UserInputRequired != null;
      }
    }

    public SesiuneLucru Sesiune { get; set; }

    public List<FisierCard> CardFiles { get; private set; }

    public TerminalData TerminalData { get; internal set; }

    public MapariEroriWinSCard MapariEroriWinSCard { get; private set; }

    public MesajeRaspunsTerminal MesajeRaspunsTerminal { get; private set; }

    public MesajeRaspunsCard MesajeRaspunsCard { get; private set; }

    public CommunicationRemotingClient CommunicationRemotingClient { get; set; }

    internal UMClient UMClient { get; set; }

    public ActivateStatus FlagStareActivare { get; private set; }

    public ActivateStatusExtern StareActivareExterna { get; private set; }

    public int NumarIncercariRamase { get; private set; }

    public bool NecesitaActualizare { get; private set; }

    public string DesiredTerminalName { get; internal set; }

    public X509Certificate CertificatMAI { get; internal set; }

    private X509Certificate Certificat { get; set; }

    private List<CoduriRaspunsOperatieCard> RaspunsuriUMPentruOffline { get; set; }

    private string PINBlock { get; set; }

    private string CardNumber { get; set; }

    private string PIN { get; set; }

    private bool IsPDAConnected { get; set; }

    public string TerminalDataPath
    {
      get
      {
        return Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\Novensys.eCard.SDK\\TerminalData\\";
      }
    }

    private string PDATerminalDataPath
    {
      get
      {
        return "\\Application Data\\Novensys.eCard.SDK\\TerminalData";
      }
    }

    public string TerminalCurent
    {
      get
      {
        return this.IsPDAConnected ? "PDA" : this.resolvedTerminalName;
      }
    }

    public string TerminalId
    {
      get
      {
        if (this.TerminalData != null)
          return this.TerminalData.TerminalId;
        else
          return (string) null;
      }
    }

    private string CertificateSerialNumber
    {
      get
      {
        if (this.Certificat != null)
          return this.Certificat.GetSerialNumberString();
        else
          return (string) null;
      }
    }

    public WinSCard Card
    {
      get
      {
        return this.card;
      }
    }

    private IApduExchange ApduExchanger
    {
      get
      {
        this.ResolveTerminalName();
        IApduExchange apduExchanger = WinSCardContextJob.Instance.GetApduExchanger(this.resolvedTerminalName);
        if (apduExchanger == null)
          throw new WinSCardException(true, "SCard.Connect", 2148532247U);
        else
          return apduExchanger;
      }
    }

    private ICardTextReader CardTextReader
    {
      get
      {
        this.ResolveTerminalName();
        ICardTextReader cardTextReader = WinSCardContextJob.Instance.GetCardTextReader(this.resolvedTerminalName);
        if (cardTextReader == null)
          throw new WinSCardException(true, "SCard.Connect", 2148532247U);
        else
          return cardTextReader.ReadTextSupported ? cardTextReader : (ICardTextReader) this;
      }
    }

    public bool TerminalCuTastatura
    {
      get
      {
        try
        {
          return !object.ReferenceEquals((object) this, (object) this.CardTextReader);
        }
        catch (WinSCardException ex)
        {
          return false;
        }
      }
    }

    public StareCardInTerminal StareCardInTerminal
    {
      get
      {
        try
        {
          return this.ApduExchanger.CardPresent ? StareCardInTerminal.CardInserat : StareCardInTerminal.CardRetras;
        }
        catch (WinSCardException ex)
        {
          return StareCardInTerminal.CardRetras;
        }
      }
    }

    public bool TerminalConectat
    {
      get
      {
        try
        {
          IApduExchange apduExchanger = this.ApduExchanger;
          return true;
        }
        catch (WinSCardException ex)
        {
          return false;
        }
      }
    }

    public StareComunicatieCuUM StareComunicatieCuUM
    {
      get
      {
        return this.stareComunicatieCuUM;
      }
      set
      {
        this.stareComunicatieCuUM = value;
        (this.Sesiune as SesiuneCard).SchimbareStareComunicatieCuUM(value);
      }
    }

    public StareCard StareCard
    {
      get
      {
        return this.stareCard;
      }
      set
      {
        if (this.stareCard == value)
          return;
        this.stareCard = value;
        (this.Sesiune as SesiuneCard).SchimbareStareCard(value);
      }
    }

    public StariAutentificare StareAutentificare
    {
      get
      {
        return this.stareAutentificare;
      }
      private set
      {
        if (value != this.stareAutentificare)
        {
          this.NecesitaActualizare = value == StariAutentificare.AUTENTIFICAT && this.NecesitaActualizare;
          (this.Sesiune as SesiuneCard).SchimbareStareAutentificare(value);
        }
        this.stareAutentificare = value;
        if (this.stareAutentificare != StariAutentificare.AUTENTIFICAT && this.stareAutentificare != StariAutentificare.AUTENTIFICAT_OFFILINE)
          return;
        this.StareCard = StareCard.Activ;
      }
    }

    public TipTerminal TipTerminal { get; set; }

    public ModEditarePeTerminal EditarePeTerminal
    {
      get
      {
        return this.editarePeTerminal;
      }
      set
      {
        if (value != this.editarePeTerminal)
          (this.Sesiune as SesiuneCard).SchimbareStareEditarePeTerminal(value);
        this.editarePeTerminal = value;
      }
    }

    public int? ProfilId
    {
      get
      {
        return this.profilId;
      }
      set
      {
        int? nullable1 = this.profilId;
        int? nullable2 = value;
        if ((nullable1.GetValueOrDefault() != nullable2.GetValueOrDefault() ? 1 : (nullable1.HasValue != nullable2.HasValue ? 1 : 0)) != 0)
        {
          this.StareAutentificare = StariAutentificare.NEAUTENTIFICAT;
          this.PIN = (string) null;
          this.PINBlock = (string) null;
          this.CardNumber = (string) null;
          this.NumarIncercariRamase = 0;
          this.Certificat = (X509Certificate) null;
        }
        this.profilId = value;
      }
    }

    public event EventHandler CardActivatExtern;

    public event EventHandler<ReadCardTextEventArgs> UserInputRequired;

    public TerminalManager()
    {
      this.MesajeRaspunsCard = new MesajeRaspunsCard();
      this.MesajeRaspunsTerminal = new MesajeRaspunsTerminal();
      this.UMClient = new UMClient();
    }

    public void InitPCSC()
    {
      try
      {
        this.IncarcaFisiereCard();
        this.IncarcaRaspunsuriUMPentruOffline();
        this.MapariEroriWinSCard = new MapariEroriWinSCard();
      }
      catch (Exception ex)
      {
        LogManager.FileLog(ex);
      }
    }

    public CoduriRaspunsOperatieCard AutentificareLaInserareCard()
    {
      if (this.ProfilId.HasValue && this.ProfilId.Value == 5)
      {
        CoduriRaspunsOperatieCard raspunsOperatieCard = this.ExecutaAutentificare((string) null);
        if (raspunsOperatieCard != CoduriRaspunsOperatieCard.OK)
          return raspunsOperatieCard;
      }
      string pin = (string) null;
      CoduriRaspunsOperatieCard raspunsOperatieCard1 = this.CitesteDateEditatePeTerminal(TipOperatieCard.AUTENTIFICARE, ref pin);
      if (raspunsOperatieCard1 != CoduriRaspunsOperatieCard.OK)
        return raspunsOperatieCard1;
      this.PIN = pin;
      CoduriRaspunsOperatieCard raspunsOperatieCard2 = this.ExecutaAutentificare(this.PIN);
      if (raspunsOperatieCard2 != CoduriRaspunsOperatieCard.OK)
        return raspunsOperatieCard2;
      else
        return CoduriRaspunsOperatieCard.OK;
    }

    public CoduriRaspunsOperatieCard Autentificare(bool cuCererePIN)
    {
      return this.Autentificare(cuCererePIN, true);
    }

    public CoduriRaspunsOperatieCard Autentificare(bool cuCererePIN, bool autentificareCuUM)
    {
      string pin = (string) null;
      int num;
      if (cuCererePIN)
      {
        int? profilId = this.ProfilId;
        num = (profilId.GetValueOrDefault() != 5 ? 1 : (!profilId.HasValue ? 1 : 0)) == 0 ? 1 : 0;
      }
      else
        num = 1;
      if (num == 0)
      {
        CoduriRaspunsOperatieCard raspunsOperatieCard = this.CitesteDateEditatePeTerminal(TipOperatieCard.AUTENTIFICARE, ref pin);
        if (raspunsOperatieCard != CoduriRaspunsOperatieCard.OK)
          return raspunsOperatieCard;
        this.PIN = pin;
      }
      CoduriRaspunsOperatieCard raspunsOperatieCard1 = this.ExecutaAutentificare(this.PIN, autentificareCuUM);
      if (raspunsOperatieCard1 != CoduriRaspunsOperatieCard.OK)
        return raspunsOperatieCard1;
      else
        return raspunsOperatieCard1;
    }

    public CoduriRaspunsOperatieCard ExecutaAutentificare(string pin)
    {
      return this.ExecutaAutentificare(pin, true);
    }

    public CoduriRaspunsOperatieCard ExecutaAutentificare(string pin, bool autentificareCuUM)
    {
      CoduriRaspunsOperatieCard raspunsOperatieCard1 = CoduriRaspunsOperatieCard.ERR_AUTENTIFICARE;
      bool canResetPIN = false;
      try
      {
        LogManager.FileLog("Executa autentificare");
        CoduriRaspunsOperatieCard raspunsOperatieCard2 = this.AutentificarePeCard(pin);
        int num;
        switch (raspunsOperatieCard2)
        {
          case CoduriRaspunsOperatieCard.OK:
          case CoduriRaspunsOperatieCard.ERR_AUTENTIFICARE:
            num = 1;
            break;
          default:
            num = raspunsOperatieCard2 == CoduriRaspunsOperatieCard.ERR_CARD_BLOCKED ? 1 : 0;
            break;
        }
        if (num == 0)
          return raspunsOperatieCard2;
        if (this.Certificat == null)
        {
          raspunsOperatieCard1 = this.CitesteCertificat();
          if (raspunsOperatieCard1 != CoduriRaspunsOperatieCard.OK)
            return raspunsOperatieCard1;
        }
        string str = this.ObtineNumarCardDinCertificat();
        if (this.CardNumber != null && this.CardNumber != str)
        {
          this.ReseteazaDateCard();
          return CoduriRaspunsOperatieCard.ERR_CARD_SCHIMBAT_IN_TERMINAL;
        }
        else
        {
          if (this.CertificatMAI == null)
          {
            raspunsOperatieCard1 = this.CitesteCertificatMAI();
            if (raspunsOperatieCard1 != CoduriRaspunsOperatieCard.OK)
              return raspunsOperatieCard1;
          }
          this.CardNumber = str;
          if (!this.ProfilId.HasValue || this.ProfilId.HasValue && this.ProfilId.Value != 5)
          {
            this.PINBlock = CryptoHelper.CreatePinBlock(this.CardNumber, pin, this.TerminalData.CheieCriptarePIN);
            this.PIN = pin;
          }
          else
          {
            this.CardNumber = "0000000000000000";
            this.PIN = "0000";
            this.PINBlock = CryptoHelper.CreatePinBlock(this.CardNumber, this.PIN, this.TerminalData.CheieCriptarePIN);
          }
          int numarIncercariRamase = this.NumarIncercariRamase;
          if (!autentificareCuUM)
          {
            this.StareAutentificare = StariAutentificare.AUTENTIFICAT;
            this.StareComunicatieCuUM = StareComunicatieCuUM.COMUNICATIE_OK;
            return raspunsOperatieCard2;
          }
          else
          {
            CoduriRaspunsOperatieCard raspunsOperatie;
            try
            {
              LogManager.FileLog("UM: Autentificare");
              bool necesitaActualizare = this.NecesitaActualizare;
              raspunsOperatie = this.UMClient.ExecutaAutentificare(this.PINBlock, ref numarIncercariRamase, this.CardNumber, this.TerminalId, ref canResetPIN, this.CertificateSerialNumber, ref necesitaActualizare);
              this.NecesitaActualizare = necesitaActualizare;
            }
            catch (Exception ex)
            {
              LogManager.FileLog(ex);
              raspunsOperatie = CoduriRaspunsOperatieCard.ERR_COM_SERVICE;
            }
            if (raspunsOperatieCard2 == CoduriRaspunsOperatieCard.OK && raspunsOperatie == CoduriRaspunsOperatieCard.OK)
            {
              this.StareAutentificare = StariAutentificare.AUTENTIFICAT;
              this.SeteazaStareComunicatieCuUM(raspunsOperatie);
              return CoduriRaspunsOperatieCard.OK;
            }
            else if (this.RaspunsuriUMPentruOffline.Contains(raspunsOperatie))
            {
              if (raspunsOperatieCard2 == CoduriRaspunsOperatieCard.OK)
                this.StareAutentificare = StariAutentificare.AUTENTIFICAT_OFFILINE;
              this.SeteazaStareComunicatieCuUM(raspunsOperatie);
              return raspunsOperatieCard2;
            }
            else if (raspunsOperatieCard2 != CoduriRaspunsOperatieCard.OK || raspunsOperatie != CoduriRaspunsOperatieCard.OK)
            {
              if (raspunsOperatieCard2 == CoduriRaspunsOperatieCard.ERR_CARD_BLOCKED)
                this.StareCard = StareCard.Blocat;
              this.StareAutentificare = StariAutentificare.AUTENTIFICARE_ESUATA;
              this.SeteazaStareComunicatieCuUM(raspunsOperatie);
              return raspunsOperatie != CoduriRaspunsOperatieCard.OK ? raspunsOperatie : (raspunsOperatieCard2 != CoduriRaspunsOperatieCard.OK ? raspunsOperatieCard2 : CoduriRaspunsOperatieCard.ERR_AUTENTIFICARE);
            }
          }
        }
      }
      catch (Exception ex)
      {
        LogManager.FileLog(ex);
        this.StareAutentificare = StariAutentificare.AUTENTIFICARE_ESUATA;
        return CoduriRaspunsOperatieCard.ERR_AUTENTIFICARE;
      }
      return raspunsOperatieCard1;
    }

    public CoduriRaspunsOperatieCard ValidareTerminal()
    {
      int retryCounter = 0;
      bool canResetPIN = false;
      CoduriRaspunsOperatieCard raspunsOperatieCard1;
      try
      {
        LogManager.FileLog("UM: Validare terminal");
        if (this.Certificat == null)
        {
          CoduriRaspunsOperatieCard raspunsOperatieCard2 = this.CitesteCertificat();
          if (raspunsOperatieCard2 != CoduriRaspunsOperatieCard.OK)
            return raspunsOperatieCard2;
        }
        string pinBlock = CryptoHelper.CreatePinBlock("0000000000000000", "0000", this.TerminalData.CheieCriptarePIN);
        bool necesitaActualizare = this.NecesitaActualizare;
        raspunsOperatieCard1 = this.UMClient.ExecutaAutentificare(pinBlock, ref retryCounter, "0000000000000000", this.TerminalId, ref canResetPIN, this.CertificateSerialNumber, ref necesitaActualizare);
      }
      catch (Exception ex)
      {
        LogManager.FileLog(ex);
        return CoduriRaspunsOperatieCard.ERR_TERMINAL_VERIFICARE;
      }
      return raspunsOperatieCard1;
    }

    private CoduriRaspunsOperatieCard AutentificarePeCard(string pin)
    {
      CoduriRaspunsOperatieCard raspunsOperatieCard1;
      try
      {
        int? profilId = this.ProfilId;
        int num1;
        if (profilId.HasValue)
        {
          profilId = this.ProfilId;
          if (profilId.HasValue)
          {
            profilId = this.ProfilId;
            num1 = (profilId.GetValueOrDefault() != 5 ? 1 : (!profilId.HasValue ? 1 : 0)) == 0 ? 1 : 0;
          }
          else
            num1 = 1;
        }
        else
          num1 = 0;
        if (num1 == 0 && !this.EstePINValid(pin, TipPIN.PIN_AUTENTIFICARE))
          return CoduriRaspunsOperatieCard.ERR_PIN_LUNGIME_INVALIDA;
        profilId = this.ProfilId;
        if (profilId.HasValue)
        {
          profilId = this.ProfilId;
          CoduriRaspunsOperatieCard raspunsOperatieCard2 = this.VerificaRaspunsApdu(TipOperatieCard.AUTENTIFICARE, this.ExternalAuthenticate((ProfileCard) profilId.Value));
          if (raspunsOperatieCard2 != CoduriRaspunsOperatieCard.OK)
            return raspunsOperatieCard2;
        }
        profilId = this.ProfilId;
        int num2;
        if (profilId.HasValue)
        {
          profilId = this.ProfilId;
          if (profilId.HasValue)
          {
            profilId = this.ProfilId;
            num2 = profilId.Value == 5 ? 1 : 0;
          }
          else
            num2 = 1;
        }
        else
          num2 = 0;
        if (num2 == 0)
        {
          RespApdu respApdu = this.VerifyPIN(pin);
          this.NumarIncercariRamase = this.IntoarceNumarIncercariRamase(respApdu);
          CoduriRaspunsOperatieCard raspunsOperatieCard2 = this.VerificaRaspunsApdu(TipOperatieCard.AUTENTIFICARE, respApdu);
          if (raspunsOperatieCard2 != CoduriRaspunsOperatieCard.OK)
            return raspunsOperatieCard2;
        }
        raspunsOperatieCard1 = CoduriRaspunsOperatieCard.OK;
      }
      catch (Exception ex)
      {
        LogManager.FileLog(ex);
        raspunsOperatieCard1 = CoduriRaspunsOperatieCard.ERR_AUTENTIFICARE;
      }
      return raspunsOperatieCard1;
    }

    public void SetProfilId(string token, IdentificatorDrepturi identificatorDrepturi, DateTime serverDateTime)
    {
      string str = CryptoHelper.DecryptToken(token, identificatorDrepturi, serverDateTime);
      this.ProfilId = new int?((int) Convert.ToInt16(str.Substring(str.Length - 1, 1)));
    }

    public CoduriRaspunsOperatieCard VerificaCardActivat()
    {
      CoduriRaspunsOperatieCard index = CoduriRaspunsOperatieCard.ERR_VERIFICARE_CARD_ACTIVAT;
      try
      {
        LogManager.FileLog("Verifica card activat");
        FisierCard fisierCard = this.CardFiles.Find((Predicate<FisierCard>) (x => x.Cod == CoduriFisiereCard.TECH));
        RespApdu resp = this.SelectFile(fisierCard);
        index = this.VerificaRaspunsApdu(TipOperatieCard.VERIFICARE_CARD_ACTIVAT, resp);
        if (index != CoduriRaspunsOperatieCard.OK)
          throw new Exception(this.MesajeRaspunsCard[index]);
        byte[] data = this.ReadBinary(fisierCard, ref resp);
        index = this.VerificaRaspunsApdu(TipOperatieCard.VERIFICARE_CARD_ACTIVAT, resp);
        if (index != CoduriRaspunsOperatieCard.OK)
          throw new Exception(this.MesajeRaspunsCard[index]);
        if (!ApduUtil.ContainsData(data))
        {
          this.StareCard = StareCard.Inactiv;
          return CoduriRaspunsOperatieCard.ERR_CARD_NEACTIVAT;
        }
        else if (!ApduUtil.ContainsValidData(data))
        {
          this.StareCard = StareCard.Inactiv;
          return CoduriRaspunsOperatieCard.ERR_CARD_NEACTIVAT;
        }
        else
        {
          ActivateStatus activateStatus;
          try
          {
            activateStatus = ASN1Helper.DecodeActivateStatus(data);
            this.FlagStareActivare = activateStatus;
            this.StareActivareExterna = ASN1Helper.DecodeActivateStatusExtern(data);
          }
          catch (Exception ex)
          {
            throw ex;
          }
          if (this.StareActivareExterna == ActivateStatusExtern.SCHIMBARE_PIN_EXTERNA && this.CardActivatExtern != null)
            this.CardActivatExtern((object) this, EventArgs.Empty);
          if (activateStatus == ActivateStatus.UM_PIN_SYNCRONIZED)
          {
            this.StareCard = StareCard.Activ;
            return CoduriRaspunsOperatieCard.OK;
          }
          else if (activateStatus == ActivateStatus.PIN_TRANSP_CHANGED_ON_CARD)
          {
            index = this.Autentificare(true, false);
            if (index != CoduriRaspunsOperatieCard.OK)
              throw new Exception(this.MesajeRaspunsCard[index]);
            index = this.ExecutaSchimbarePINTransport((this.Sesiune as SesiuneCard).Token, true);
            if (index != CoduriRaspunsOperatieCard.OK)
              throw new Exception(this.MesajeRaspunsCard[index]);
            this.StareCard = StareCard.Activ;
            return CoduriRaspunsOperatieCard.OK;
          }
        }
      }
      catch (WinSCardException ex)
      {
        index = this.MapariEroriWinSCard.ObtineEroareMapata((SCARD_ERROR) ex.Status);
        LogManager.FileLog((Exception) ex);
      }
      catch (Exception ex)
      {
        LogManager.FileLog(ex);
      }
      return index;
    }

    private CoduriRaspunsOperatieCard ActiveazaCard()
    {
      string str1 = string.Empty;
      string str2 = string.Empty;
      string pinVechi = (string) null;
      string pinNou = (string) null;
      string pinNouConfirmat = (string) null;
      CoduriRaspunsOperatieCard raspunsOperatieCard1;
      try
      {
        CoduriRaspunsOperatieCard raspunsOperatieCard2 = this.CitesteDateEditatePeTerminal(true, ref pinVechi, ref pinNou, ref pinNouConfirmat);
        if (raspunsOperatieCard2 != CoduriRaspunsOperatieCard.OK)
          return raspunsOperatieCard2;
        if (!this.EstePINValid(pinVechi, TipPIN.PIN_TRANSPORT))
          return CoduriRaspunsOperatieCard.ERR_PIN_TRANSPORT_INVALID;
        if (!this.EstePINValid(pinNou, TipPIN.PIN_AUTENTIFICARE) || !this.EstePINValid(pinNouConfirmat, TipPIN.PIN_AUTENTIFICARE))
          return CoduriRaspunsOperatieCard.ERR_PIN_LUNGIME_INVALIDA;
        if (pinNou != pinNouConfirmat)
          return CoduriRaspunsOperatieCard.ERR_PIN_NECONFIRMAT;
        this.SelectRoot();
        this.ExternalAuthenticate((ProfileCard) this.ProfilId.Value);
        CoduriRaspunsOperatieCard raspunsOperatieCard3 = this.VerificaRaspunsApdu(TipOperatieCard.ACTIVARE, this.ChangePIN(pinVechi, pinNou));
        if (raspunsOperatieCard3 != CoduriRaspunsOperatieCard.OK)
          return raspunsOperatieCard3;
        this.NumarIncercariRamase = this.IntoarceNumarIncercariRamase(this.VerifyPIN(pinNou));
        this.CardNumber = ASN1Helper.DecodeCardNumber(this.CitesteDateFiserCard(CoduriFisiereCard.DG1));
        this.PINBlock = CryptoHelper.CreatePinBlock(this.CardNumber, pinNou, this.TerminalData.CheieCriptarePIN);
        this.PIN = pinNou;
        this.WriteActivateStatus(new ActivateStatus?(ActivateStatus.PIN_TRANSP_CHANGED_ON_CARD), new ActivateStatusExtern?(ActivateStatusExtern.NECUNOSCUT));
        raspunsOperatieCard1 = CoduriRaspunsOperatieCard.OK;
      }
      catch (ApduException ex)
      {
        throw ex;
      }
      catch (Exception ex)
      {
        LogManager.FileLog(ex);
        raspunsOperatieCard1 = CoduriRaspunsOperatieCard.ERR_CARD_ACTIVARE;
      }
      return raspunsOperatieCard1;
    }

    public CoduriRaspunsOperatieCard ExecutaSchimbarePINTransport(string token, bool numaiSincronizareCuUM)
    {
      CoduriRaspunsOperatieCard raspunsOperatieCard1;
      try
      {
        LogManager.FileLog("Executa schimbare pin transport");
        if (!numaiSincronizareCuUM)
        {
          CoduriRaspunsOperatieCard raspunsOperatieCard2 = this.ActiveazaCard();
          if (raspunsOperatieCard2 != CoduriRaspunsOperatieCard.OK)
            return raspunsOperatieCard2;
        }
        int numarIncercariRamase = this.NumarIncercariRamase;
        if (this.Certificat == null)
        {
          CoduriRaspunsOperatieCard raspunsOperatieCard2 = this.CitesteCertificat();
          if (raspunsOperatieCard2 != CoduriRaspunsOperatieCard.OK)
            return raspunsOperatieCard2;
        }
        LogManager.FileLog("UM: Schimbare pin transport");
        CoduriRaspunsOperatieCard raspunsOperatieCard3 = this.UMClient.ExecutaSchimbarePINTransport(token, this.CardNumber, ref numarIncercariRamase, this.TerminalId, (string) null, this.PINBlock, Convert.ToBase64String(this.Certificat.GetRawCertData()), this.CertificateSerialNumber);
        if (raspunsOperatieCard3 != CoduriRaspunsOperatieCard.OK)
          return raspunsOperatieCard3;
        raspunsOperatieCard1 = this.ActualizeazaStareActivareCard(ActivateStatus.UM_PIN_SYNCRONIZED, ActivateStatusExtern.NECUNOSCUT, this.PIN);
        if (raspunsOperatieCard1 != CoduriRaspunsOperatieCard.OK || numaiSincronizareCuUM)
          return raspunsOperatieCard1;
        raspunsOperatieCard1 = this.ExecutaAutentificare(this.PIN);
        if (raspunsOperatieCard1 != CoduriRaspunsOperatieCard.OK)
          return raspunsOperatieCard1;
      }
      catch (ApduException ex)
      {
        throw ex;
      }
      catch (Exception ex)
      {
        LogManager.FileLog(ex);
        raspunsOperatieCard1 = CoduriRaspunsOperatieCard.ERR_SCHIMBARE_PIN_TRANSPORT;
      }
      return raspunsOperatieCard1;
    }

    private CoduriRaspunsOperatieCard ActualizeazaStareActivareCard(ActivateStatus status, ActivateStatusExtern statusExtern, string pin)
    {
      CoduriRaspunsOperatieCard raspunsOperatieCard;
      try
      {
        this.NumarIncercariRamase = this.IntoarceNumarIncercariRamase(this.VerifyPIN(pin));
        this.CardNumber = ASN1Helper.DecodeCardNumber(this.CitesteDateFiserCard(CoduriFisiereCard.DG1));
        this.WriteActivateStatus(new ActivateStatus?(status), new ActivateStatusExtern?(statusExtern));
        raspunsOperatieCard = CoduriRaspunsOperatieCard.OK;
      }
      catch (ApduException ex)
      {
        throw ex;
      }
      catch (Exception ex)
      {
        LogManager.FileLog(ex);
        raspunsOperatieCard = CoduriRaspunsOperatieCard.ERR_CARD_ACTIVARE;
      }
      return raspunsOperatieCard;
    }

    private void WriteActivateStatus(ActivateStatus? status, ActivateStatusExtern? statusExtern)
    {
      FisierCard fisierCard = this.CardFiles.Find((Predicate<FisierCard>) (x => x.Cod == CoduriFisiereCard.TECH));
      if (HexFormatting.ToHexString((uint) this.SelectFile(fisierCard).SW1SW2.Value) != "9000")
        throw new Exception(this.MesajeRaspunsCard[CoduriRaspunsOperatieCard.ERR_CITIRE_FISIER_TECH]);
      RespApdu resp = (RespApdu) null;
      byte[] data = this.ReadBinary(fisierCard, ref resp);
      if (HexFormatting.ToHexString((uint) resp.SW1SW2.Value) != "9000")
        throw new Exception(this.MesajeRaspunsCard[CoduriRaspunsOperatieCard.ERR_CITIRE_FISIER_TECH]);
      // ISSUE: explicit reference operation
      // ISSUE: variable of a reference type
      //status?local1 = @status;
      ActivateStatus? nullable1 = status;
      int num1 = nullable1.HasValue ? (int) nullable1.GetValueOrDefault() : (int) ASN1Helper.DecodeActivateStatus(data);
      // ISSUE: explicit reference operation
      var local1 = new ActivateStatus?((ActivateStatus) num1); // nullable constructor
      // ISSUE: explicit reference operation
      // ISSUE: variable of a reference type
      ActivateStatusExtern? local2 = @statusExtern;
      ActivateStatusExtern? nullable2 = statusExtern;
      int num2 = nullable2.HasValue ? (int) nullable2.GetValueOrDefault() : (int) ASN1Helper.DecodeActivateStatusExtern(data);
      // ISSUE: explicit reference operation
       local2 = new ActivateStatusExtern?((ActivateStatusExtern) num2);
      if (HexFormatting.ToHexString((uint) this.UpdateBinary(ASN1Helper.EncodeActivateStatus((int) status.Value, (int) statusExtern.Value)).SW1SW2.Value) != "9000")
        throw new Exception(this.MesajeRaspunsCard[CoduriRaspunsOperatieCard.ERR_SCRIERE_FISIER_TECH]);
    }

    public CoduriRaspunsOperatieCard CitesteDate(List<CoduriCampuriCard> campuriDeCitit, CardData cardData, Dictionary<CoduriCampuriCard, CoduriRaspunsOperatieCamp> rezultatOperatie)
    {
      CoduriRaspunsOperatieCard raspunsOperatieCard;
      try
      {
        this.SelectRoot();
        if (this.ProfilId.HasValue)
          this.ExternalAuthenticate((ProfileCard) this.ProfilId.Value);
        int? profilId = this.ProfilId;
        int num;
        if (profilId.HasValue)
        {
          profilId = this.ProfilId;
          if (profilId.HasValue)
          {
            profilId = this.ProfilId;
            num = profilId.Value == 5 ? 1 : 0;
          }
          else
            num = 1;
        }
        else
          num = 0;
        if (num == 0)
          this.VerifyPIN(this.PIN);
        raspunsOperatieCard = this.CitesteDateCard(campuriDeCitit, cardData, rezultatOperatie);
        if (raspunsOperatieCard != CoduriRaspunsOperatieCard.OK)
          return raspunsOperatieCard;
      }
      catch (ApduException ex)
      {
        throw ex;
      }
      catch (Exception ex)
      {
        LogManager.FileLog(ex);
        raspunsOperatieCard = CoduriRaspunsOperatieCard.ERR_CARD_CITIRE;
      }
      return raspunsOperatieCard;
    }

    private CoduriRaspunsOperatieCard CitesteDateCard(List<CoduriCampuriCard> campuriDeCitit, CardData cardData, Dictionary<CoduriCampuriCard, CoduriRaspunsOperatieCamp> rezultatOperatie)
    {
      List<FisierCard> list = new List<FisierCard>();
      foreach (CoduriCampuriCard coduriCampuriCard in campuriDeCitit)
      {
        CoduriCampuriCard camp = coduriCampuriCard;
        if (!rezultatOperatie.ContainsKey(camp))
          rezultatOperatie.Add(camp, CoduriRaspunsOperatieCamp.ERR_CAMP_CITIRE);
        else
          rezultatOperatie[camp] = CoduriRaspunsOperatieCamp.ERR_CAMP_CITIRE;
        FisierCard fisierCard = this.CardFiles.Find((Predicate<FisierCard>) (x => x.Campuri.Contains(camp)));
        if (fisierCard == null)
          rezultatOperatie.Add(camp, CoduriRaspunsOperatieCamp.ERR_CAMP_INEXISTENT);
        else if (!list.Contains(fisierCard))
          list.Add(fisierCard);
      }
      foreach (FisierCard fisierCard in list)
      {
        this.SelectFile(fisierCard);
        RespApdu resp = (RespApdu) null;
        byte[] data = this.ReadBinary(fisierCard, ref resp);
        this.SeteazaCoduriRaspunsuriCampuri(resp, campuriDeCitit, fisierCard, rezultatOperatie, TipOperatieCard.CITIRE);
        CoduriRaspunsOperatieCard raspunsOperatieCard = this.VerificaRaspunsApdu(TipOperatieCard.CITIRE, resp);
        if (raspunsOperatieCard != CoduriRaspunsOperatieCard.OK)
          return raspunsOperatieCard;
        if (ApduUtil.ContainsData(data))
        {
          this.IncarcaDateFisierCard(data, fisierCard, cardData);
          foreach (CoduriCampuriCard index in fisierCard.Campuri)
            rezultatOperatie[index] = CoduriRaspunsOperatieCamp.OK;
        }
      }
      return CoduriRaspunsOperatieCard.OK;
    }

    private void SeteazaCoduriRaspunsuriCampuri(RespApdu resp, List<CoduriCampuriCard> campuri, FisierCard fisierCard, Dictionary<CoduriCampuriCard, CoduriRaspunsOperatieCamp> rezultatOperatie, TipOperatieCard tipOperatieCard)
    {
      if (campuri == null || campuri.Count == 0 || rezultatOperatie.Count == 0)
        return;
      foreach (CoduriCampuriCard index in fisierCard.Campuri)
      {
        if (campuri.Contains(index))
        {
          switch (HexFormatting.ToHexString((uint) resp.SW1SW2.Value))
          {
            case "6982":
              rezultatOperatie[index] = CoduriRaspunsOperatieCamp.ERR_DREPTURI_INSUFICIENTE;
              break;
            case "6A82":
              rezultatOperatie[index] = CoduriRaspunsOperatieCamp.ERR_CAMP_INEXISTENT;
              break;
            case "9000":
              rezultatOperatie[index] = CoduriRaspunsOperatieCamp.OK;
              break;
            default:
              if (tipOperatieCard == TipOperatieCard.CITIRE)
              {
                rezultatOperatie[index] = CoduriRaspunsOperatieCamp.ERR_CAMP_CITIRE;
                break;
              }
              else if (tipOperatieCard == TipOperatieCard.SCRIERE)
              {
                rezultatOperatie[index] = CoduriRaspunsOperatieCamp.ERR_CAMP_SCRIERE;
                break;
              }
              else
                break;
          }
        }
      }
    }

    private void IncarcaDateFisierCard(byte[] data, FisierCard cardFile, CardData cardData)
    {
      if (cardFile.NecesitaDecodare)
      {
        if (!ApduUtil.ContainsValidData(data))
          return;
        ASN1Helper.DecodeFile(data, cardFile.Cod, cardData);
      }
      else if (cardFile.Cod == CoduriFisiereCard.CERT)
        cardData.SecurityData.CertificateDigitale.Valoare = (object) data;
      else if (cardFile.Cod == CoduriFisiereCard.CERT_MAI)
        cardData.SecurityData.CertificatMAI.Valoare = (object) data;
    }

    public byte[] CitesteDateFiserCard(CoduriFisiereCard codFisierCard)
    {
      FisierCard fisierCard = this.CardFiles.Find((Predicate<FisierCard>) (x => x.Cod == codFisierCard));
      this.SelectFile(fisierCard);
      RespApdu resp = (RespApdu) null;
      return this.ReadBinary(fisierCard, ref resp);
    }

    public CoduriRaspunsOperatieCard ScrieDate(List<CoduriCampuriCard> campuriDeEditat, CardData cardData, Dictionary<CoduriCampuriCard, CoduriRaspunsOperatieCamp> rezultatOperatie)
    {
      CoduriRaspunsOperatieCard raspunsOperatieCard1 = this.VerificaCardData(cardData);
      if (raspunsOperatieCard1 != CoduriRaspunsOperatieCard.OK)
        return raspunsOperatieCard1;
      CoduriRaspunsOperatieCard raspunsOperatieCard2;
      try
      {
        this.SelectRoot();
        this.ExternalAuthenticate((ProfileCard) this.ProfilId.Value);
        int? profilId = this.ProfilId;
        int num1;
        if (profilId.HasValue)
        {
          profilId = this.ProfilId;
          num1 = profilId.Value == 5 ? 1 : 0;
        }
        else
          num1 = 1;
        if (num1 == 0)
          this.VerifyPIN(this.PIN);
        CardData cardData1 = new CardData();
        Dictionary<CoduriCampuriCard, CoduriRaspunsOperatieCamp> rezultatOperatie1 = new Dictionary<CoduriCampuriCard, CoduriRaspunsOperatieCamp>();
        int num2 = (int) this.CitesteDateCard(campuriDeEditat, cardData1, rezultatOperatie1);
        raspunsOperatieCard2 = this.ScrieDataCard(campuriDeEditat, cardData, cardData1, rezultatOperatie);
        if (raspunsOperatieCard2 != CoduriRaspunsOperatieCard.OK)
          return raspunsOperatieCard2;
      }
      catch (ApduException ex)
      {
        throw ex;
      }
      catch (Exception ex)
      {
        LogManager.FileLog(ex);
        raspunsOperatieCard2 = CoduriRaspunsOperatieCard.ERR_CARD_SCRIERE;
      }
      return raspunsOperatieCard2;
    }

    private CoduriRaspunsOperatieCard ScrieDataCard(List<CoduriCampuriCard> campuriDeEditat, CardData cardData, CardData originalCardData, Dictionary<CoduriCampuriCard, CoduriRaspunsOperatieCamp> rezultatOperatie)
    {
      Dictionary<CoduriFisiereCard, List<CoduriCampuriCard>> dictionary = new Dictionary<CoduriFisiereCard, List<CoduriCampuriCard>>();
      foreach (CoduriCampuriCard coduriCampuriCard in campuriDeEditat)
      {
        CoduriCampuriCard camp = coduriCampuriCard;
        if (!rezultatOperatie.ContainsKey(camp))
          rezultatOperatie.Add(camp, CoduriRaspunsOperatieCamp.ERR_CAMP_SCRIERE);
        else
          rezultatOperatie[camp] = CoduriRaspunsOperatieCamp.ERR_CAMP_SCRIERE;
        FisierCard fisierCard = this.CardFiles.Find((Predicate<FisierCard>) (x => x.Campuri.Contains(camp)));
        if (fisierCard == null)
        {
          rezultatOperatie[camp] = CoduriRaspunsOperatieCamp.ERR_CAMP_INEXISTENT;
        }
        else
        {
          if (!dictionary.ContainsKey(fisierCard.Cod))
            dictionary.Add(fisierCard.Cod, new List<CoduriCampuriCard>());
          dictionary[fisierCard.Cod].Add(camp);
        }
      }
      foreach (CoduriFisiereCard coduriFisiereCard in dictionary.Keys)
      {
        CoduriFisiereCard key = coduriFisiereCard;
        List<CoduriCampuriCard> campuriDeEditatPeFisier = dictionary[key];
        FisierCard fisierCard = this.CardFiles.Find((Predicate<FisierCard>) (x => x.Cod == key));
        this.SelectFile(fisierCard);
        byte[] numArray = ASN1Helper.EncodeFile(campuriDeEditatPeFisier, fisierCard, cardData, originalCardData);
        if (!ApduUtil.ContainsData(numArray))
        {
          foreach (CoduriCampuriCard index in campuriDeEditatPeFisier)
            rezultatOperatie[index] = CoduriRaspunsOperatieCamp.ERR_CAMP_READONLY;
        }
        else
        {
          RespApdu respApdu = this.UpdateBinary(numArray, fisierCard.MarimeMaxima);
          this.SeteazaCoduriRaspunsuriCampuri(respApdu, campuriDeEditat, fisierCard, rezultatOperatie, TipOperatieCard.SCRIERE);
          CoduriRaspunsOperatieCard raspunsOperatieCard = this.VerificaRaspunsApdu(TipOperatieCard.SCRIERE, respApdu);
          if (raspunsOperatieCard != CoduriRaspunsOperatieCard.OK)
            return raspunsOperatieCard;
        }
      }
      return CoduriRaspunsOperatieCard.OK;
    }

    public CoduriRaspunsOperatieCard ExecutaSchimbarePIN(string token)
    {
      string pinVechi = (string) null;
      string pinNou = (string) null;
      string pinNouConfirmat = (string) null;
      CoduriRaspunsOperatieCard raspunsOperatieCard1;
      try
      {
        LogManager.FileLog("Executa schimbare pin");
        CoduriRaspunsOperatieCard raspunsOperatieCard2 = this.CitesteDateEditatePeTerminal(false, ref pinVechi, ref pinNou, ref pinNouConfirmat);
        if (raspunsOperatieCard2 != CoduriRaspunsOperatieCard.OK)
          return raspunsOperatieCard2;
        if (!this.EstePINValid(pinVechi, TipPIN.PIN_AUTENTIFICARE))
          return CoduriRaspunsOperatieCard.ERR_PIN_LUNGIME_INVALIDA;
        if (pinVechi != this.PIN)
          return CoduriRaspunsOperatieCard.ERR_INVALID_PIN;
        if (!this.EstePINValid(pinNou, TipPIN.PIN_AUTENTIFICARE))
          return CoduriRaspunsOperatieCard.ERR_PIN_LUNGIME_INVALIDA;
        if (pinNou != pinNouConfirmat)
          return CoduriRaspunsOperatieCard.ERR_PIN_NECONFIRMAT;
        int numarIncercariRamase = this.NumarIncercariRamase;
        string pinBlock1 = CryptoHelper.CreatePinBlock(this.CardNumber, pinVechi, this.TerminalData.CheieCriptarePIN);
        string pinBlock2 = CryptoHelper.CreatePinBlock(this.CardNumber, pinNou, this.TerminalData.CheieCriptarePIN);
        LogManager.FileLog("UM: Schimbare pin");
        CoduriRaspunsOperatieCard raspunsOperatieCard3 = this.UMClient.ExecutaSchimbarePIN(token, this.CardNumber, ref numarIncercariRamase, this.TerminalId, pinBlock1, pinBlock2, this.CertificateSerialNumber);
        if (raspunsOperatieCard3 != CoduriRaspunsOperatieCard.OK)
          return raspunsOperatieCard3;
        CoduriRaspunsOperatieCard raspunsOperatieCard4 = this.SchimbaPIN(pinVechi, pinNou);
        if (raspunsOperatieCard4 != CoduriRaspunsOperatieCard.OK)
          return raspunsOperatieCard4;
        this.PIN = pinNou;
        this.PINBlock = pinBlock2;
        raspunsOperatieCard1 = this.ExecutaAutentificare(this.PIN);
        if (raspunsOperatieCard1 != CoduriRaspunsOperatieCard.OK)
          return raspunsOperatieCard1;
        this.WriteActivateStatus(new ActivateStatus?(), new ActivateStatusExtern?(ActivateStatusExtern.NECUNOSCUT));
      }
      catch (ApduException ex)
      {
        throw ex;
      }
      catch (Exception ex)
      {
        LogManager.FileLog(ex);
        raspunsOperatieCard1 = CoduriRaspunsOperatieCard.ERR_SCHIMBARE_PIN;
      }
      return raspunsOperatieCard1;
    }

    public CoduriRaspunsOperatieCard ExecutaResetarePIN(string token)
    {
      bool canResetPIN = false;
      CoduriRaspunsOperatieCard raspunsOperatieCard1;
      try
      {
        LogManager.FileLog("Executa resetare pin");
        string pin = (string) null;
        CoduriRaspunsOperatieCard raspunsOperatieCard2 = this.CitesteDateEditatePeTerminal(TipOperatieCard.RESETARE_PIN, ref pin);
        if (raspunsOperatieCard2 != CoduriRaspunsOperatieCard.OK)
          return raspunsOperatieCard2;
        int numarIncercariRamase = this.NumarIncercariRamase;
        if (this.Certificat == null)
        {
          CoduriRaspunsOperatieCard raspunsOperatieCard3 = this.CitesteCertificat();
          if (raspunsOperatieCard3 != CoduriRaspunsOperatieCard.OK)
            return raspunsOperatieCard3;
        }
        this.CardNumber = this.ObtineNumarCardDinCertificat();
        if (!this.EstePINValid(pin, TipPIN.PIN_RESET))
          return CoduriRaspunsOperatieCard.ERR_PIN_RESET_INVALID;
        string pinBlock = CryptoHelper.CreatePinBlock(this.CardNumber, pin, this.TerminalData.CheieCriptarePIN);
        LogManager.FileLog("UM: Autentificare");
        bool necesitaActualizare = this.NecesitaActualizare;
        CoduriRaspunsOperatieCard raspunsOperatieCard4 = this.UMClient.ExecutaAutentificare(pinBlock, ref numarIncercariRamase, this.CardNumber, this.TerminalId, ref canResetPIN, this.CertificateSerialNumber, ref necesitaActualizare);
        if (raspunsOperatieCard4 != CoduriRaspunsOperatieCard.OK && raspunsOperatieCard4 != CoduriRaspunsOperatieCard.ERR_CARD_BLOCKED)
          return raspunsOperatieCard4;
        this.NecesitaActualizare = necesitaActualizare;
        if (!canResetPIN)
          return CoduriRaspunsOperatieCard.ERR_RESETARE_PIN_NECONFIRMAT;
        CoduriRaspunsOperatieCard raspunsOperatieCard5 = this.ReseteazaPIN(pin);
        if (raspunsOperatieCard5 != CoduriRaspunsOperatieCard.OK)
          return raspunsOperatieCard5;
        CoduriRaspunsOperatieCard raspunsOperatieCard6 = this.UMClient.ExecutaResetarePIN(token, this.CardNumber, this.TerminalId, this.PINBlock, this.CertificateSerialNumber);
        if (raspunsOperatieCard6 != CoduriRaspunsOperatieCard.OK)
          return raspunsOperatieCard6;
        raspunsOperatieCard1 = this.ExecutaAutentificare(pin);
        if (raspunsOperatieCard1 != CoduriRaspunsOperatieCard.OK)
          return raspunsOperatieCard1;
        this.WriteActivateStatus(new ActivateStatus?(), new ActivateStatusExtern?(ActivateStatusExtern.NECUNOSCUT));
      }
      catch (WinSCardException ex)
      {
        LogManager.FileLog((Exception) ex);
        return this.MapariEroriWinSCard.MapeazaEroare((SCARD_ERROR) ex.Status, TipOperatieCard.RESETARE_PIN);
      }
      catch (ApduException ex)
      {
        LogManager.FileLog((Exception) ex);
        return (CoduriRaspunsOperatieCard) ex.MappingCode;
      }
      catch (Exception ex)
      {
        LogManager.FileLog(ex);
        raspunsOperatieCard1 = CoduriRaspunsOperatieCard.ERR_RESETARE_PIN;
      }
      return raspunsOperatieCard1;
    }

    public CoduriRaspunsOperatieCard SemneazaDate(byte[] dateDeSemnat, TipCertificat tipCertificat, ref byte[] dateSemnate)
    {
      CoduriRaspunsOperatieCard raspunsOperatieCard1;
      try
      {
        this.SelectRoot();
        int num;
        if (this.ProfilId.HasValue)
        {
          int? profilId = this.ProfilId;
          num = (profilId.GetValueOrDefault() != 5 ? 1 : (!profilId.HasValue ? 1 : 0)) == 0 ? 1 : 0;
        }
        else
          num = 1;
        if (num == 0)
          this.VerifyPIN(this.PIN);
        this.MSERestore01();
        CoduriFisiereCard codFisierCard = CoduriFisiereCard.CERT;
        if (tipCertificat == TipCertificat.CertificatAsigurat)
          codFisierCard = CoduriFisiereCard.CERT;
        else if (tipCertificat == TipCertificat.CertificatMAI)
          codFisierCard = CoduriFisiereCard.CERT_MAI;
        byte[] numArray = this.CitesteDateFiserCard(codFisierCard);
        if (numArray == null || numArray.Length == 0)
          throw new NotSupportedException("Nu se poate realiza semnarea datelor deoarece cardul nu contine certificatul digital solicitat.");
        X509Certificate cert = new X509Certificate(numArray);
        Attributes signedAttributes = ASN1Helper.GetSignedAttributes(dateDeSemnat, numArray);
        this.PSOHash(signedAttributes.GetDerEncoded());
        RespApdu digitalSignature = this.PSOComputeDigitalSignature();
        CoduriRaspunsOperatieCard raspunsOperatieCard2 = this.VerificaRaspunsApdu(TipOperatieCard.SEMNARE_DIGITALA, digitalSignature);
        if (raspunsOperatieCard2 != CoduriRaspunsOperatieCard.OK)
          return raspunsOperatieCard2;
        if (digitalSignature.ContainsData)
        {
          byte[] data = digitalSignature.Data;
          SignerInfo signerInfo = ASN1Helper.BuildSignerInfo(signedAttributes, data, cert);
          ContentInfo contentInfo = ASN1Helper.BuildSignedData(dateDeSemnat, signerInfo);
          dateSemnate = contentInfo.GetEncoded();
        }
        raspunsOperatieCard1 = CoduriRaspunsOperatieCard.OK;
      }
      catch (WinSCardException ex)
      {
        LogManager.FileLog((Exception) ex);
        return this.MapariEroriWinSCard.MapeazaEroare((SCARD_ERROR) ex.Status, TipOperatieCard.SEMNARE_DIGITALA);
      }
      catch (ApduException ex)
      {
        LogManager.FileLog((Exception) ex);
        return (CoduriRaspunsOperatieCard) ex.MappingCode;
      }
      catch (Exception ex)
      {
        LogManager.FileLog(ex);
        raspunsOperatieCard1 = CoduriRaspunsOperatieCard.ERR_SEMNATURA;
      }
      return raspunsOperatieCard1;
    }

    [Obsolete]
    public CoduriRaspunsOperatieTerminal IncarcaDateTerminal()
    {
      return CoduriRaspunsOperatieTerminal.ERR_TERMINAL_CITIRE;
    }

    [Obsolete]
    public CoduriRaspunsOperatieTerminal ScrieDateTerminal(ActualizareTerminalData terminalData)
    {
      return CoduriRaspunsOperatieTerminal.ERR_TERMINAL_SCRIERE;
    }

    public CoduriRaspunsOperatieTerminal VerificaTerminal(bool verificaDacaEsteInrolat)
    {
      CoduriRaspunsOperatieTerminal index = CoduriRaspunsOperatieTerminal.ERR_TERMINAL_VERIFICARE;
      try
      {
        LogManager.FileLog("Verifica terminal");
        this.ResolveTerminalName();
        if (string.IsNullOrEmpty(this.TerminalCurent))
        {
          index = CoduriRaspunsOperatieTerminal.ERR_TERMINAL_DECONECTAT;
          throw new Exception(this.MesajeRaspunsTerminal[index]);
        }
        else if (this.TerminalData == null)
        {
          index = CoduriRaspunsOperatieTerminal.ERR_TERMINAL_NEINROLAT;
          throw new Exception(this.MesajeRaspunsTerminal[index]);
        }
        else
          index = CoduriRaspunsOperatieTerminal.OK;
      }
      catch (Exception ex)
      {
        LogManager.FileLog(ex);
      }
      return index;
    }

    private void Card_OnCommandExecuted(int commandId, ref string result)
    {
      SesiuneCard sesiuneCard = this.Sesiune as SesiuneCard;
      if (commandId == 3)
      {
        result = Enum.GetName(typeof (CoduriRaspunsOperatieCard), (object) sesiuneCard.SchimbaPIN(sesiuneCard.Token));
      }
      else
      {
        if (commandId != 4)
          return;
        result = Enum.GetName(typeof (CoduriRaspunsOperatieCard), (object) sesiuneCard.ReseteazaPIN(sesiuneCard.Token));
      }
    }

    public void CitestePIN(string mesaj, ref string pin, bool withReleaseAtFinish)
    {
      pin = this.CardTextReader.ReadText(mesaj, true);
    }

    private CoduriRaspunsOperatieCard CitesteDateEditatePeTerminal(TipOperatieCard tipOperatieCard, ref string pin)
    {
      string mesaj = string.Empty;
      CoduriRaspunsOperatieCard raspunsOperatieCard = CoduriRaspunsOperatieCard.ERR_AUTENTIFICARE;
      try
      {
        if (tipOperatieCard == TipOperatieCard.AUTENTIFICARE)
        {
          this.EditarePeTerminal = ModEditarePeTerminal.AUTENTIFICARE;
          mesaj = "PIN-ul dvs.?";
        }
        else if (tipOperatieCard == TipOperatieCard.RESETARE_PIN)
        {
          this.EditarePeTerminal = ModEditarePeTerminal.RESETARE_PIN;
          mesaj = "PIN-ul de reset?";
        }
        this.CitestePIN(mesaj, ref pin, true);
        raspunsOperatieCard = CoduriRaspunsOperatieCard.OK;
      }
      catch (WinSCardException ex)
      {
        LogManager.FileLog((Exception) ex);
        raspunsOperatieCard = this.MapariEroriWinSCard.MapeazaEroare((SCARD_ERROR) ex.Status, tipOperatieCard);
      }
      catch (Exception ex)
      {
        LogManager.FileLog(ex);
      }
      finally
      {
        this.EditarePeTerminal = ModEditarePeTerminal.NU_SE_EDITEAZA_PE_TERMINAL;
      }
      return raspunsOperatieCard;
    }

    public CoduriRaspunsOperatieCard CitesteDateEditatePeTerminal(bool activareCard, ref string pinVechi, ref string pinNou, ref string pinNouConfirmat)
    {
      string str = string.Empty;
      CoduriRaspunsOperatieCard raspunsOperatieCard = activareCard ? CoduriRaspunsOperatieCard.ERR_SCHIMBARE_PIN_TRANSPORT : CoduriRaspunsOperatieCard.ERR_SCHIMBARE_PIN;
      TipOperatieCard tipOperatieCard = activareCard ? TipOperatieCard.ACTIVARE : TipOperatieCard.SCHIMBARE_PIN;
      try
      {
        this.EditarePeTerminal = activareCard ? ModEditarePeTerminal.SCHIMBARE_PIN_TRANSPORT : ModEditarePeTerminal.SCHIMBARE_PIN;
        this.CitestePIN(activareCard ? "PIN transport?" : "PIN actual?", ref pinVechi, false);
        this.CitestePIN("PIN nou?", ref pinNou, false);
        this.CitestePIN("PIN confirmat?", ref pinNouConfirmat, true);
        raspunsOperatieCard = CoduriRaspunsOperatieCard.OK;
      }
      catch (WinSCardException ex)
      {
        LogManager.FileLog((Exception) ex);
        raspunsOperatieCard = this.MapariEroriWinSCard.MapeazaEroare((SCARD_ERROR) ex.Status, tipOperatieCard);
      }
      catch (Exception ex)
      {
        LogManager.FileLog(ex);
        raspunsOperatieCard = activareCard ? CoduriRaspunsOperatieCard.ERR_SCHIMBARE_PIN_TRANSPORT : CoduriRaspunsOperatieCard.ERR_SCHIMBARE_PIN;
      }
      finally
      {
        this.EditarePeTerminal = ModEditarePeTerminal.NU_SE_EDITEAZA_PE_TERMINAL;
      }
      return raspunsOperatieCard;
    }

    public void SeteazaStareComunicatieCuUM(CoduriRaspunsOperatieCard raspunsOperatie)
    {
      if (raspunsOperatie == CoduriRaspunsOperatieCard.OK)
        this.StareComunicatieCuUM = StareComunicatieCuUM.COMUNICATIE_OK;
      if (raspunsOperatie == CoduriRaspunsOperatieCard.ERR_UM_TIME_OUT)
        this.StareComunicatieCuUM = StareComunicatieCuUM.TIMEOUT;
      else if (raspunsOperatie == CoduriRaspunsOperatieCard.ERR_UM_INDISPONIBILA)
        this.StareComunicatieCuUM = StareComunicatieCuUM.UM_INDISPONIBIL;
      else if (raspunsOperatie == CoduriRaspunsOperatieCard.ERR_UM_ECARD_NETWORK)
        this.StareComunicatieCuUM = StareComunicatieCuUM.ECARD_INDISPONIBIL;
      else if (raspunsOperatie == CoduriRaspunsOperatieCard.ERR_UM_CA_NETWORK)
        this.StareComunicatieCuUM = StareComunicatieCuUM.CA_INDISPONIBIL;
      else if (raspunsOperatie == CoduriRaspunsOperatieCard.ERR_COM_SERVICE)
      {
        this.StareComunicatieCuUM = StareComunicatieCuUM.SDK_SERVICIU_COMUNICATIE_INDISPONIBIL;
      }
      else
      {
        if (raspunsOperatie != CoduriRaspunsOperatieCard.OK)
          return;
        this.stareComunicatieCuUM = StareComunicatieCuUM.COMUNICATIE_OK;
      }
    }

    public CoduriRaspunsOperatieCard VerificaCardValid()
    {
      CoduriRaspunsOperatieCard raspunsOperatieCard = CoduriRaspunsOperatieCard.ERR_CARD_ACCESARE;
      try
      {
        LogManager.FileLog("Verifica card valid");
        raspunsOperatieCard = this.ApduExchanger.CardPresent ? CoduriRaspunsOperatieCard.OK : CoduriRaspunsOperatieCard.ERR_CARD_LIPSA;
      }
      catch (WinSCardException ex)
      {
        raspunsOperatieCard = this.MapariEroriWinSCard.ObtineEroareMapata((SCARD_ERROR) ex.Status);
        LogManager.FileLog((Exception) ex);
      }
      catch (Exception ex)
      {
        LogManager.FileLog(ex);
      }
      return raspunsOperatieCard;
    }

    private CoduriRaspunsOperatieCard VerificaCardData(CardData cardData)
    {
      CoduriRaspunsOperatieCard raspunsOperatieCard = CoduriRaspunsOperatieCard.OK;
      if (cardData.DateAdministrative.PersoaneContact.Valoare != null && (cardData.DateAdministrative.PersoaneContact.Valoare as List<PersoanaContact>).Count > 2)
        return CoduriRaspunsOperatieCard.ERR_CARD_PERSOANE_CONTACT_PESTE_MAX;
      if (cardData.DateClinicePrimare.DiagnosticeMedicaleCuRiscVital.Valoare != null && (cardData.DateClinicePrimare.DiagnosticeMedicaleCuRiscVital.Valoare as List<string>).Count > 10)
        return CoduriRaspunsOperatieCard.ERR_CARD_DIAGNOSTICE_PESTE_MAX;
      if (cardData.DateClinicePrimare.BoliCronice.Valoare != null && (cardData.DateClinicePrimare.BoliCronice.Valoare as List<string>).Count > 20)
        return CoduriRaspunsOperatieCard.ERR_CARD_BOLI_PESTE_MAX;
      else
        return raspunsOperatieCard;
    }

    public CoduriRaspunsOperatieCard VerificaToken(string token)
    {
      LogManager.FileLog("Verifica token");
      if (token == null)
        return CoduriRaspunsOperatieCard.ERR_TOKEN_LIPSA;
      SesiuneCard sesiuneCard = this.Sesiune as SesiuneCard;
      if (sesiuneCard.Token == null)
        return CoduriRaspunsOperatieCard.ERR_TOKEN_RESETAT;
      return sesiuneCard.Token != null && !sesiuneCard.Token.Equals(token) ? CoduriRaspunsOperatieCard.ERR_TOKEN_INVALID : CoduriRaspunsOperatieCard.OK;
    }

    private bool EstePINValid(string pin, TipPIN tipPIN)
    {
      pin = pin.Trim();
      return !string.IsNullOrEmpty(pin) && (tipPIN != TipPIN.PIN_TRANSPORT || !(pin != "000")) && ((tipPIN != TipPIN.PIN_RESET || !(pin != "0000")) && (tipPIN != TipPIN.PIN_AUTENTIFICARE || pin.Length == 4));
    }

    private CoduriRaspunsOperatieCard VerificaRaspunsApdu(TipOperatieCard tipOperatie, RespApdu raspunsApdu)
    {
      string str = HexFormatting.ToHexString((uint) raspunsApdu.SW1SW2.Value);
      LogManager.FileLog(string.Format("APDU: Raspuns {0}", (object) str));
      if (str == "6983")
        return CoduriRaspunsOperatieCard.ERR_CARD_BLOCKED;
      switch (tipOperatie)
      {
        case TipOperatieCard.CITIRE:
          if (str != "9000")
            return CoduriRaspunsOperatieCard.ERR_CARD_CITIRE;
          else
            break;
        case TipOperatieCard.SCRIERE:
          if (str != "9000")
            return CoduriRaspunsOperatieCard.ERR_CARD_SCRIERE;
          else
            break;
        case TipOperatieCard.AUTENTIFICARE:
          if (str != "9000")
          {
            this.StareAutentificare = StariAutentificare.AUTENTIFICARE_ESUATA;
            return CoduriRaspunsOperatieCard.ERR_AUTENTIFICARE;
          }
          else
            break;
        case TipOperatieCard.SCHIMBARE_PIN:
          if (str == "6982")
            return CoduriRaspunsOperatieCard.ERR_SCHIMBARE_PIN_DREPTURI_INSUFIECIENTE;
          if (str != "9000")
            return CoduriRaspunsOperatieCard.ERR_SCHIMBARE_PIN;
          else
            break;
        case TipOperatieCard.RESETARE_PIN:
          if (str == "6982")
            return CoduriRaspunsOperatieCard.ERR_RESETARE_PIN_DREPTURI_INSUFICIENTE;
          if (str != "9000")
            return CoduriRaspunsOperatieCard.ERR_RESETARE_PIN;
          else
            break;
        case TipOperatieCard.ACTIVARE:
          if (str == "6982")
            return CoduriRaspunsOperatieCard.ERR_SCHIMBARE_PIN_DREPTURI_INSUFIECIENTE;
          if (str != "9000")
            return CoduriRaspunsOperatieCard.ERR_SCHIMBARE_PIN_TRANSPORT;
          else
            break;
        case TipOperatieCard.SEMNARE_DIGITALA:
          if (str == "6982")
            return CoduriRaspunsOperatieCard.ERR_SEMNATURA_DREPTURI_INSUFICIENTE;
          if (str != "9000")
            return CoduriRaspunsOperatieCard.ERR_SEMNATURA;
          else
            break;
        case TipOperatieCard.VERIFICARE_CARD_ACTIVAT:
          if (str != "9000")
            return CoduriRaspunsOperatieCard.ERR_VERIFICARE_CARD_ACTIVAT;
          else
            break;
      }
      return CoduriRaspunsOperatieCard.OK;
    }

    public RespApdu SelectRoot()
    {
      LogManager.FileLog("APDU: SelectRoot");
      return this.ApduExchanger.Exchange(string.Format("00 A4 04 04 0B E8 28 BD 08 0F D6 42 00 00 01 01 00"), new ushort?(HexUtil.HexStringToUShort("9000")));
    }

    public RespApdu VerifyPIN(string pin)
    {
      LogManager.FileLog("APDU: VerifyPIN");
      string str = HexFormatting.ToHexString(HexEncoding.GetBytes(HexFormatting.ToHexString(pin)), true);
      return this.ApduExchanger.Exchange(string.Format("00 20 00 01 {0} {1}", (object) HexFormatting.ToHexString((uint) pin.Length), (object) str));
    }

    public RespApdu ChangePIN(string pinVechi, string pinNou)
    {
      LogManager.FileLog("APDU: ChangePIN");
      string hexString1 = HexFormatting.ToHexString(pinVechi);
      string hexString2 = HexFormatting.ToHexString(pinNou);
      return this.ApduExchanger.Exchange(string.Format("00 24 00 01 {0} {1}", (object) HexFormatting.ToHexString((uint) (pinVechi.Length + pinNou.Length)), (object) string.Format("{0} {1}", (object) HexFormatting.ToHexString(HexEncoding.GetBytes(hexString1), true), (object) HexFormatting.ToHexString(HexEncoding.GetBytes(hexString2), true))));
    }

    public RespApdu SelectFile(FisierCard cardFile)
    {
      LogManager.FileLog(string.Format("APDU: Select File {0}", (object) cardFile.Cod));
      return this.ApduExchanger.Exchange(cardFile.ComandaApduSelect);
    }

    private RespApdu ExternalAuthenticate(ProfileCard profilCard)
    {
      RespApdu resp = (RespApdu) null;
      FisierCard fisierCard = this.CardFiles.Find((Predicate<FisierCard>) (x => x.Cod == CoduriFisiereCard.SN_ICC));
      this.SelectFile(fisierCard);
      byte[] numArray1 = this.ReadBinary(fisierCard, ref resp);
      byte[] numArray2 = new byte[8];
      Array.Copy((Array) numArray1, numArray1.Length - 8, (Array) numArray2, 0, 8);
      string SN_ICC = HexFormatting.ToHexString(numArray2);
      this.MSERestore(profilCard);
      string data = HexFormatting.ToHexString(this.GetChallenge().Data) + SN_ICC;
      Dictionary<string, string> dictionary = CryptoHelper.DeriveKeySet(this.TerminalData.Profile.Find((Predicate<Profil>) (x => x.ProfilCard == profilCard)).Cheie, SN_ICC);
      string key = dictionary["SKENC"];
      string hexString1 = dictionary["SKMAC"];
      string hexString2 = HexFormatting.ToHexString(CryptoHelper._3DES(key, data));
      string str = HexFormatting.ToHexString(CryptoHelper.CalculateMac(HexEncoding.GetBytes(hexString1), HexEncoding.GetBytes(hexString2)));
      return this.ExternalAuthenticate(HexEncoding.GetBytes(hexString2 + str));
    }

    public RespApdu MSERestore(ProfileCard profilCard)
    {
      LogManager.FileLog("APDU: MSERestore");
      return this.ApduExchanger.Exchange(string.Format("00 22 F3 {0} 00", (object) this.GetSEID(profilCard)), new ushort?(HexUtil.HexStringToUShort("9000")));
    }

    private RespApdu MSERestore01()
    {
      LogManager.FileLog("APDU: MSERestore01");
      return this.ApduExchanger.Exchange("00 22 F3 01 00", new ushort?(HexUtil.HexStringToUShort("9000")));
    }

    private RespApdu PSOHash(byte[] dataToHash)
    {
      LogManager.FileLog("APDU: PSO Hash");
      string str1 = string.Empty;
      string str2 = string.Empty;
      string str3 = string.Empty;
      string str4 = string.Format("9020{0}", (object) HexFormatting.ToHexString(CryptoHelper.SHA256ComputeHash(dataToHash)));
      return this.ApduExchanger.Exchange(string.Format("00 2A 90 A0 {0} {1}", (object) HexFormatting.ToHexString((uint) (str4.Length / 2)), (object) str4), new ushort?(HexUtil.HexStringToUShort("9000")));
    }

    private RespApdu PSOComputeDigitalSignature()
    {
      LogManager.FileLog("APDU: PSO Compute Digital Signature");
      return this.ApduExchanger.Exchange("00 2A 9E 9A 00");
    }

    private string GetSEID(ProfileCard profilCard)
    {
      string str = (string) null;
      switch (profilCard)
      {
        case ProfileCard.MedicFamilie:
          str = "12";
          break;
        case ProfileCard.Specialist:
          str = "13";
          break;
        case ProfileCard.Farmacist:
          str = "14";
          break;
        case ProfileCard.Urgenta:
          str = "15";
          break;
        case ProfileCard.EmitentCard:
          str = "11";
          break;
        case ProfileCard.ProviderSubventie:
          str = "16";
          break;
        case ProfileCard.FurnizorServiciiSubventie:
          str = "17";
          break;
      }
      return str;
    }

    public RespApdu GetChallenge()
    {
      LogManager.FileLog("APDU: GetChallenge");
      return this.ApduExchanger.Exchange("00 84 00 00 08", new ushort?(HexUtil.HexStringToUShort("9000")));
    }

    public RespApdu ExternalAuthenticate(byte[] bdata)
    {
      LogManager.FileLog("APDU: ExternalAuthenticate");
      return this.ApduExchanger.Exchange(string.Format("00 82 00 00 {0} {1}", (object) HexFormatting.ToHexString((uint) bdata.Length), (object) HexFormatting.ToHexString(bdata, true)), new ushort?(HexUtil.HexStringToUShort("9000")));
    }

    public byte[] ReadBinary(FisierCard fisierCard, ref RespApdu resp)
    {
      LogManager.FileLog("APDU: ReadBinary");
      MemoryStream memoryStream = new MemoryStream();
      string str = string.Empty;
      byte num1 = (byte) 0;
      int num2 = 0;
      RespApdu respApdu = (RespApdu) null;
      int num3;
      do
      {
        string cmdApdu = string.Format("00 B0 {0} 00 00", (object) HexFormatting.ToHexString(num1));
        resp = this.ApduExchanger.Exchange(cmdApdu);
        if (!resp.ContainsData)
        {
          resp = respApdu;
          break;
        }
        else
        {
          memoryStream.Write(resp.Data, 0, resp.Data.Length);
          num2 += resp.Data.Length;
          ++num1;
          respApdu = resp;
          if (resp.ContainsData)
          {
            long? marimeMaxima = fisierCard.MarimeMaxima;
            if (!marimeMaxima.HasValue)
            {
              num3 = 1;
            }
            else
            {
              long num4 = (long) num2;
              marimeMaxima = fisierCard.MarimeMaxima;
              num3 = num4 >= marimeMaxima.GetValueOrDefault() ? 0 : (marimeMaxima.HasValue ? 1 : 0);
            }
          }
          else
            num3 = 0;
        }
      }
      while (num3 != 0);
      return memoryStream.ToArray();
    }

    public RespApdu UpdateBinary(byte[] bdata, long? maxFileSize)
    {
      string str1 = string.Empty;
      int num1 = 0;
      int num2 = 0;
      int sourceIndex = 0;
      int length = (int) byte.MaxValue;
      RespApdu respApdu;
      int num3;
      do
      {
        byte[] numArray = bdata.Length - num1 > length ? new byte[length] : new byte[bdata.Length - num1];
        Array.Copy((Array) bdata, sourceIndex, (Array) numArray, 0, numArray.Length);
        string str2 = HexFormatting.ToHexString((uint) numArray.Length);
        string str3 = HexFormatting.ToHexString(numArray);
        respApdu = this.ApduExchanger.Exchange(string.Format("00 D6 {0} {1} {2} {3}", (object) HexFormatting.ToHexString((uint) num2 / 256U), (object) HexFormatting.ToHexString((uint) num2 % 256U), (object) str2, (object) str3));
        num1 += numArray.Length;
        sourceIndex += numArray.Length;
        num2 += length;
        if (num1 < bdata.Length)
        {
          long num4 = (long) num1;
          long? nullable = maxFileSize;
          num3 = num4 > nullable.GetValueOrDefault() ? 0 : (nullable.HasValue ? 1 : 0);
        }
        else
          num3 = 0;
      }
      while (num3 != 0);
      return respApdu;
    }

    public RespApdu UpdateBinary(byte[] bdata)
    {
      LogManager.FileLog("APDU: UpdateBinary");
      return this.ApduExchanger.Exchange(string.Format("00 D6 00 00 {0} {1}", (object) HexFormatting.ToHexString((uint) bdata.Length), (object) HexFormatting.ToHexString(bdata, true)));
    }

    public RespApdu ReseteazaPINCard(string pinReset)
    {
      LogManager.FileLog("APDU: ResetPIN");
      string hexString = HexFormatting.ToHexString(pinReset);
      return this.ApduExchanger.Exchange(string.Format("00 2C 02 01 {0} {1}", (object) HexFormatting.ToHexString((uint) pinReset.Length), (object) HexFormatting.ToHexString(HexEncoding.GetBytes(hexString))), new ushort?(HexUtil.HexStringToUShort("9000")));
    }

    private CoduriRaspunsOperatieCard SchimbaPIN(string pinVechi, string pinNou)
    {
      CoduriRaspunsOperatieCard raspunsOperatieCard;
      try
      {
        this.SelectRoot();
        this.ExternalAuthenticate((ProfileCard) this.ProfilId.Value);
        raspunsOperatieCard = this.VerificaRaspunsApdu(TipOperatieCard.SCHIMBARE_PIN, this.ChangePIN(pinVechi, pinNou));
        if (raspunsOperatieCard != CoduriRaspunsOperatieCard.OK)
          return raspunsOperatieCard;
      }
      catch (ApduException ex)
      {
        throw ex;
      }
      catch (Exception ex)
      {
        LogManager.FileLog(ex);
        raspunsOperatieCard = CoduriRaspunsOperatieCard.ERR_SCHIMBARE_PIN;
      }
      return raspunsOperatieCard;
    }

    private CoduriRaspunsOperatieCard ReseteazaPIN(string pinReset)
    {
      CoduriRaspunsOperatieCard raspunsOperatieCard;
      try
      {
        Profil profil = this.TerminalData.Profile.Find((Predicate<Profil>) (x => x.ProfilCard == ProfileCard.EmitentCard));
        if (profil == null)
          return CoduriRaspunsOperatieCard.ERR_RESETARE_PIN_DREPTURI_INSUFICIENTE;
        this.SelectRoot();
        this.ExternalAuthenticate(profil.ProfilCard);
        this.ReseteazaPINCard(pinReset);
        raspunsOperatieCard = CoduriRaspunsOperatieCard.OK;
      }
      catch (Exception ex)
      {
        LogManager.FileLog(ex);
        raspunsOperatieCard = CoduriRaspunsOperatieCard.ERR_RESETARE_PIN;
      }
      return raspunsOperatieCard;
    }

    public void Connect()
    {
    }

    private void ResolveTerminalName()
    {
      if (!string.IsNullOrEmpty(this.resolvedTerminalName))
        return;
      string availableReader = WinSCardContextJob.Instance.FindAvailableReader(this.DesiredTerminalName);
      if (string.IsNullOrEmpty(availableReader))
        throw new WinSCardException(true, "SCard.Connect", 2148532247U);
      this.resolvedTerminalName = availableReader;
    }

    [Obsolete("Metoda nu se mai foloseste. Proprietatea CommunicationRemotingClient este intotdeauna null.")]
    public void SetCommunicationRemotingClient()
    {
    }

    private void IncarcaRaspunsuriUMPentruOffline()
    {
      this.RaspunsuriUMPentruOffline = new List<CoduriRaspunsOperatieCard>();
      this.RaspunsuriUMPentruOffline.Add(CoduriRaspunsOperatieCard.ERR_UM_TIME_OUT);
      this.RaspunsuriUMPentruOffline.Add(CoduriRaspunsOperatieCard.ERR_UM_INDISPONIBILA);
      this.RaspunsuriUMPentruOffline.Add(CoduriRaspunsOperatieCard.ERR_UM_ECARD_NETWORK);
      this.RaspunsuriUMPentruOffline.Add(CoduriRaspunsOperatieCard.ERR_UM_CA_NETWORK);
      this.RaspunsuriUMPentruOffline.Add(CoduriRaspunsOperatieCard.ERR_COM_SERVICE);
    }

    private void IncarcaFisiereCard()
    {
      this.CardFiles = new List<FisierCard>();
      FisierCard fisierCard1 = new FisierCard(CoduriFisiereCard.DG1, "DG1", "00 A4 02 0C 02 01 01 00", new int?(291), true);
      fisierCard1.Campuri.AddRange((IEnumerable<CoduriCampuriCard>) new CoduriCampuriCard[7]
      {
        CoduriCampuriCard.A1,
        CoduriCampuriCard.A3,
        CoduriCampuriCard.B1,
        CoduriCampuriCard.B2,
        CoduriCampuriCard.B3,
        CoduriCampuriCard.B4,
        CoduriCampuriCard.C1
      });
      this.CardFiles.Add(fisierCard1);
      FisierCard fisierCard2 = new FisierCard(CoduriFisiereCard.DG2, "DG2", "00 A4 02 0C 02 01 02 00", new int?(256), true);
      fisierCard2.Campuri.AddRange((IEnumerable<CoduriCampuriCard>) new CoduriCampuriCard[1]
      {
        CoduriCampuriCard.C2
      });
      this.CardFiles.Add(fisierCard2);
      FisierCard fisierCard3 = new FisierCard(CoduriFisiereCard.DG3, "DG3", "00 A4 02 0C 02 01 03 00", new int?(32), true);
      fisierCard3.Campuri.AddRange((IEnumerable<CoduriCampuriCard>) new CoduriCampuriCard[1]
      {
        CoduriCampuriCard.C3
      });
      this.CardFiles.Add(fisierCard3);
      FisierCard fisierCard4 = new FisierCard(CoduriFisiereCard.DG4, "DG4", "00 A4 02 0C 02 01 04 00", new int?(3237), true);
      fisierCard4.Campuri.AddRange((IEnumerable<CoduriCampuriCard>) new CoduriCampuriCard[10]
      {
        CoduriCampuriCard.C4,
        CoduriCampuriCard.C5,
        CoduriCampuriCard.C6,
        CoduriCampuriCard.C7,
        CoduriCampuriCard.C8,
        CoduriCampuriCard.C9,
        CoduriCampuriCard.D1,
        CoduriCampuriCard.D2,
        CoduriCampuriCard.D3,
        CoduriCampuriCard.D4
      });
      this.CardFiles.Add(fisierCard4);
      FisierCard fisierCard5 = new FisierCard(CoduriFisiereCard.DG5, "DG5", "00 A4 02 0C 02 01 05 00", new int?(640), true);
      fisierCard5.Campuri.AddRange((IEnumerable<CoduriCampuriCard>) new CoduriCampuriCard[1]
      {
        CoduriCampuriCard.D5
      });
      this.CardFiles.Add(fisierCard5);
      FisierCard fisierCard6 = new FisierCard(CoduriFisiereCard.DG6, "DG6", "00 A4 02 0C 02 01 06 00", new int?(240), true);
      fisierCard6.Campuri.AddRange((IEnumerable<CoduriCampuriCard>) new CoduriCampuriCard[2]
      {
        CoduriCampuriCard.D6,
        CoduriCampuriCard.D7
      });
      this.CardFiles.Add(fisierCard6);
      FisierCard fisierCard7 = new FisierCard(CoduriFisiereCard.DG7, "DG7", "00 A4 02 0C 02 01 07 00", new int?(5120), true);
      fisierCard7.Campuri.AddRange((IEnumerable<CoduriCampuriCard>) new CoduriCampuriCard[1]
      {
        CoduriCampuriCard.E1
      });
      this.CardFiles.Add(fisierCard7);
      FisierCard fisierCard8 = new FisierCard(CoduriFisiereCard.DG8, "DG8", "00 A4 02 0C 02 01 08 00", new int?(576), true);
      fisierCard8.Campuri.AddRange((IEnumerable<CoduriCampuriCard>) new CoduriCampuriCard[1]
      {
        CoduriCampuriCard.E2
      });
      this.CardFiles.Add(fisierCard8);
      FisierCard fisierCard9 = new FisierCard(CoduriFisiereCard.DG9, "DG9", "00 A4 02 0C 02 01 09 00", new int?(102), true);
      fisierCard9.Campuri.AddRange((IEnumerable<CoduriCampuriCard>) new CoduriCampuriCard[1]
      {
        CoduriCampuriCard.E3
      });
      this.CardFiles.Add(fisierCard9);
      FisierCard fisierCard10 = new FisierCard(CoduriFisiereCard.DG10, "DG10", "00 A4 02 0C 02 01 0A 00", new int?(5120), true);
      fisierCard10.Campuri.AddRange((IEnumerable<CoduriCampuriCard>) new CoduriCampuriCard[1]
      {
        CoduriCampuriCard.F1
      });
      this.CardFiles.Add(fisierCard10);
      FisierCard fisierCard11 = new FisierCard(CoduriFisiereCard.DG11, "DG11", "00 A4 02 0C 02 01 0B 00", new int?(256), true);
      fisierCard11.Campuri.AddRange((IEnumerable<CoduriCampuriCard>) new CoduriCampuriCard[1]
      {
        CoduriCampuriCard.H0
      });
      this.CardFiles.Add(fisierCard11);
      this.CardFiles.Add(new FisierCard(CoduriFisiereCard.TECH, "TECH", "00 A4 02 0C 02 4F 01 00", new int?(20), true));
      this.CardFiles.Add(new FisierCard(CoduriFisiereCard.SN_ICC, "SN.ICC", "00 A4 02 0C 02 D0 03 00", new int?(10), false));
      FisierCard fisierCard12 = new FisierCard(CoduriFisiereCard.CERT, "CERT", "00 A4 02 0C 02 C0 01 00", new int?(2048), false);
      fisierCard12.Campuri.AddRange((IEnumerable<CoduriCampuriCard>) new CoduriCampuriCard[1]
      {
        CoduriCampuriCard.G1
      });
      this.CardFiles.Add(fisierCard12);
      FisierCard fisierCard13 = new FisierCard(CoduriFisiereCard.CERT_MAI, "CERT_MAI", "00 A4 02 0C 02 C0 02 00", new int?(2048), false);
      fisierCard13.Campuri.AddRange((IEnumerable<CoduriCampuriCard>) new CoduriCampuriCard[1]
      {
        CoduriCampuriCard.G5
      });
      this.CardFiles.Add(fisierCard13);
      this.CardFiles.Add(new FisierCard(CoduriFisiereCard.PrKCardHolder, "PrK.CardHolder", (string) null, new int?(256), false)
      {
        Campuri = {
          CoduriCampuriCard.G2
        }
      });
    }

    private int IntoarceNumarIncercariRamase(RespApdu resp)
    {
      if (resp.IsValid)
        return 5;
      if (HexFormatting.ToHexString((uint) resp.SW1SW2.Value) == "6983" || !resp.SW2.HasValue)
        return 0;
      else
        return Convert.ToInt32(HexFormatting.ToHexString(resp.SW2.Value).Substring(1, 1), 16);
    }

    public void ReseteazaDateCard()
    {
      LogManager.FileLog("Resetare date card");
      this.PIN = (string) null;
      this.PINBlock = (string) null;
      this.NecesitaActualizare = false;
      this.NumarIncercariRamase = 0;
      this.CardNumber = (string) null;
      this.Certificat = (X509Certificate) null;
      this.StareAutentificare = StariAutentificare.NEAUTENTIFICAT;
      this.EditarePeTerminal = ModEditarePeTerminal.NU_SE_EDITEAZA_PE_TERMINAL;
      this.CertificatMAI = (X509Certificate) null;
    }

    private CoduriRaspunsOperatieCard CitesteCertificat()
    {
      CoduriRaspunsOperatieCard raspunsOperatieCard = CoduriRaspunsOperatieCard.ERR_CITIRE_CERTIFICAT;
      try
      {
        this.SelectRoot();
        byte[] data = this.CitesteDateFiserCard(CoduriFisiereCard.CERT);
        if (data == null || data.Length == 0)
          throw new NotSupportedException("Nu se poate citi certificatul de pe card.");
        this.Certificat = new X509Certificate(data);
        raspunsOperatieCard = CoduriRaspunsOperatieCard.OK;
      }
      catch (Exception ex)
      {
        LogManager.FileLog(ex);
      }
      return raspunsOperatieCard;
    }

    private CoduriRaspunsOperatieCard CitesteCertificatMAI()
    {
      CoduriRaspunsOperatieCard raspunsOperatieCard = CoduriRaspunsOperatieCard.ERR_CITIRE_CERTIFICAT_MAI;
      try
      {
        this.SelectRoot();
        byte[] data = this.CitesteDateFiserCard(CoduriFisiereCard.CERT_MAI);
        this.CertificatMAI = data != null && data.Length != 0 ? new X509Certificate(data) : (X509Certificate) null;
        raspunsOperatieCard = CoduriRaspunsOperatieCard.OK;
      }
      catch (Exception ex)
      {
        LogManager.FileLog(ex);
      }
      return raspunsOperatieCard;
    }

    private string ObtineNumarCardDinCertificat()
    {
      string str = (string) null;
      if (this.Certificat.Subject != null)
      {
        string[] strArray1 = this.Certificat.Subject.Split(',');
        if (strArray1.Length > 2)
        {
          string[] strArray2 = strArray1[2].Split('=');
          if (strArray2.Length > 0)
            str = strArray2[1];
        }
      }
      return str;
    }

    public string ReadText(string prompt, bool password)
    {
      if (!this.ReadTextSupported)
        throw new NotSupportedException("Terminalul nu suporta introducerea de text. \r\nAplicatia curenta nu ofera o metoda alternativa de introducere. \r\nContactati producatorul aplicatiei pentru mai multe detalii.");
      ReadCardTextEventArgs e = new ReadCardTextEventArgs();
      e.Prompt = prompt;
      e.Password = password;
      this.UserInputRequired((object) this, e);
      if (e.Cancel)
        throw new WinSCardException(true, "ReadText", 2148532334U);
      else
        return e.Result;
    }

    private delegate CoduriRaspunsOperatieCard AuthenticationHandler();
  }
}
