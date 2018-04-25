# CEAS-NOVENSYS
cod sursa pentru NOVENSYS SDK reversed by different tools

## Motive
Casa Nationala de Asigurari de Sanatate a considerat necesara securizarea operatiilor online privind serviciile medicale si retetelor, prin verificarea prezentei pacientilor la oferirea serviciilor de catre furnizori. Solutia aleasa a fost introducerea cardului de asigurat in sistemul medical. Pentru a putea citi cardurile pacientilor in aplicatiile casei si cele ale furnizorilor software, a fost pus la punct de catre firma Novensys un set de dll-uri (Novensys SDK) scrise in C#, care sa permita diferite operatii cu cititoarele de carduri inrolate in sistem la momentul dezvoltarii.
Dupa punerea in functiune a sistemului, utilizatorii au observat o serie de anomalii si probleme care de multe ori au condus la imposibilitatea furnizarii serviciilor medicale sau eliberarii retetelor.

SDK-ul Novensys nu a fost pus la dispozitia publicului sub forma codului sursa, ceea ce a creeat numeroase conflicte intre dezvoltatori, clientii acestora, pacienti si casa in mare parte si datorita lipsei de suport din partea furnizorului software.
Una din principalele probleme a fost blocarea cardului in cititor, in momentul in care serverul CEAS ( serverul in care se realizeaza verificarea cardului asiguratului ) nu era disponibil. Ulterior, aplicatiile au fost actualizate cu un mod de lucru "OFFLINE" care a condus la rezolvarea unor probleme.
Necesitatea ca dezvoltatorii sa aiba acces la codul sursa al anumitor componente ale sistemului SIUI/CEAS/DES a ramas.


## Blocare  card 
Blocarea cardului in diferite situatii in care in mod normal NU ar trebui sa se blocheze,  apare cel mai probabil aici

```public CoduriRaspunsOperatieCard ExecutaAutentificare(string pin, bool autentificareCuUM)
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
 ```
 Problemele sunt urmatoarele:
 + nu ar trebui sa se blocheze cardul pe secventa aceasta de cod. (ar trebui sa nu se ceara niciodata introducere pin daca nu e ok ceva )
 + daca este o problema cu cardul sa nu se faca blocarea card. Practic asta inseamna sa nu se ajunga la raspuns din UM card blocat care sa permita executarea codului de mai jos
    
```else if (raspunsOperatieCard2 != CoduriRaspunsOperatieCard.OK || raspunsOperatie != CoduriRaspunsOperatieCard.OK)
            {
              if (raspunsOperatieCard2 == CoduriRaspunsOperatieCard.ERR_CARD_BLOCKED)
                this.StareCard = StareCard.Blocat;
              this.StareAutentificare = StariAutentificare.AUTENTIFICARE_ESUATA;
              this.SeteazaStareComunicatieCuUM(raspunsOperatie);
              return raspunsOperatie != CoduriRaspunsOperatieCard.OK ? raspunsOperatie : (raspunsOperatieCard2 != CoduriRaspunsOperatieCard.OK ? raspunsOperatieCard2 : CoduriRaspunsOperatieCard.ERR_AUTENTIFICARE);
            }
```
            
Blocarea efectiva se face aici: 
```
this.StareCard = StareCard.Blocat;
```
        

## Drepturi de utilizare

Acest cod este disponibil asa cum este (as is) . 
Ccodul acesta a fost generat in timpul timpului liber,utilizand diferite unelte disponibile public (Mono.Cecil, JetBrains DotPeek) 
si analizand modul de functionare al sdk actual.

Din cauzele de mai sus, codul existent nu este identic 100% cu codul sursa detinut de casa nationala de asigurari de sanatate.

######### Pentru a putea compila la un dll valid sunt necesare modificari suplimentare in cod.#####

Acest cod a fost publicat cu scopul de a permite dezvoltatorilor independenti identificarea cauzelor generatoare de probleme in cadrul sistemului CEAS al casei nationale in vederea imbunatatirii interactiunii dintre aplicatii si componenta Novensys

Ca scop secundar, daca cineva doreste poate imbunatatii codul in vederea creerii unui sdk compatibil CEAS dar fara problemele mentionate. Evident, o asemenea componenta nu este suportata de casa nationala/dezvoltatorul initial. 
O a treia utilitate este aceea de material didactic.











