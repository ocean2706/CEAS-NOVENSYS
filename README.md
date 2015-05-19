# CEAS-NOVENSYS
cod sursa pentru NOVENSYS SDK reversed by different tools


############################### blocare  card ###################################
blocarea cardului apare cel mai probabil aici
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
    
    
    Problemele sunt urmatoarele:
    a) nu ar trebui sa se ajunga aici (ar trebui sa nu se ceara niciodata introducere pin daca nu e ok ceva )
    b) daca este o problema cu cardul sa nu se faca blocarea card practic asta inseamna sa nu se ajunga la raspuns din UM card blocat care sa permita executarea codului
    
      else if (raspunsOperatieCard2 != CoduriRaspunsOperatieCard.OK || raspunsOperatie != CoduriRaspunsOperatieCard.OK)
            {
              if (raspunsOperatieCard2 == CoduriRaspunsOperatieCard.ERR_CARD_BLOCKED)
                this.StareCard = StareCard.Blocat;
              this.StareAutentificare = StariAutentificare.AUTENTIFICARE_ESUATA;
              this.SeteazaStareComunicatieCuUM(raspunsOperatie);
              return raspunsOperatie != CoduriRaspunsOperatieCard.OK ? raspunsOperatie : (raspunsOperatieCard2 != CoduriRaspunsOperatieCard.OK ? raspunsOperatieCard2 : CoduriRaspunsOperatieCard.ERR_AUTENTIFICARE);
            }
            
            Blocarea efectiva se face aici: 
            this.StareCard = StareCard.Blocat;
            
        

############################################################

Acest cod este disponibil asa cum este. Pentru clarificare, codul acesta a fost generat in timpul timpului liber,
utilizand diferite unelte disponibile public (Mono.Cecil, JetBrains DotPeek) 
si analizand modul de functionare al sdk actual.
Codul existent nu este identic cu codul sursa detinut de casa nationala de asigurari de sanatate.
Ca urmare pot exista diferente de functionalitate atunci cand se compileaza.
@fixed
#########Pentru a putea compila la un dll valid sunt necesare modificari suplimentare in cod.#####

Acest cod a fost publicat cu scopul de a permite dezvoltatorilor independenti 
identificarea cauzelor generatoare de probleme in cadrul sistemului CEAS al casei nationale 
si  a dovedi in instanta daca e cazul ca problemele din sistem 
sunt generate de componenta Novensys.SDK si nu de aplicatiile dezvoltatorilor independenti,
acesti sabotori marsavi care nu doresc eliminarea fraudei din sistemul de sanatate.


Deasemeni scopul a fost de a imbunatati pe cat posibil aceasta porcarie numita sdk prin contributia comuna
a dezvoltatorilor. Ideal si de buna cuvinta ar fi sa fie publicate aici contributiile aduse codului
de catre cei ce beneficiaza pe aceasta cale de informatiile cuprinse in surse.

Un alt motiv pentru publicarea acestui cod a fost lipsa de informatii si documentarea frugala, acesta fiind dealtfel
si principalul motiv pentru care eu personal numesc acest sdk o porcarie, codul in sine fiind ok pana la un punct iar anumite abordari chiar interesante ( sa nu fie cumva vina decompliatorului ).

Multumesc de aceea dezvoltatorilor ne numiti care pana la un punct au dezvoltat aceasta componenta software si evident Casei Nationale de Asigurari de Sanatate , Novensys. Sper sa isi gaseasca utilitatea cel putin ca material didactic.











