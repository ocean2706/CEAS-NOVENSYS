// Decompiled with JetBrains decompiler
// Type: Novensys.eCard.SDK.MesajeRaspunsCard
// Assembly: Novensys.eCard.SDK, Version=1.1.56.0, Culture=neutral, PublicKeyToken=null
// MVID: 59F9E456-D3DA-4066-A4A4-692B516A775E
// Assembly location: C:\Program Files (x86)\CNAS\SIUI-SPITAL\Novensys.eCard.SDK.dll

using System.Collections.Generic;

namespace Novensys.eCard.SDK
{
  public class MesajeRaspunsCard : Dictionary<CoduriRaspunsOperatieCard, string>
  {
    public MesajeRaspunsCard()
    {
      this.Add(CoduriRaspunsOperatieCard.OK, "Operatia s-a executat cu succes.");
      this.Add(CoduriRaspunsOperatieCard.ERR_TOKEN_LIPSA, "Tokenul sesiunii lipseste.");
      this.Add(CoduriRaspunsOperatieCard.ERR_TOKEN_RESETAT, "Tokenul sesiunii a fost resetat. Obtineti alt token valid.");
      this.Add(CoduriRaspunsOperatieCard.ERR_TOKEN_INVALID, "Tokenul sesiunii este invalid.");
      this.Add(CoduriRaspunsOperatieCard.ERR_COM_SERVICE, "Serviciul de comunicatie al SDK cu UM nu functioneaza.");
      this.Add(CoduriRaspunsOperatieCard.ERR_HANDSHAKE, "Eroare handshake raportata de unitatea de management");
      this.Add(CoduriRaspunsOperatieCard.ERR_UM_STARE_CARD_INVALIDA, "Operatie esuata in UM. Starea cardului este invalida.");
      this.Add(CoduriRaspunsOperatieCard.ERR_CARD_LIPSA, "Operatie esuata. Cardul nu este prezent in terminal.");
      this.Add(CoduriRaspunsOperatieCard.ERR_CARD_CITIRE, "Eroare in timpul operatiei de citire.");
      this.Add(CoduriRaspunsOperatieCard.ERR_CARD_SCRIERE, "Eroare in timpul operatiei de scriere.");
      this.Add(CoduriRaspunsOperatieCard.ERR_TERMINAL_DECONECTAT, "Operatie esuata. Terminalul nu este conectat la PC.");
      this.Add(CoduriRaspunsOperatieCard.ERR_TERMINAL_MAI_MULT_DE_1, "Operatie esuata. Mai multe terminale conectate la PC.");
      this.Add(CoduriRaspunsOperatieCard.ERR_INVALID_TERMINAL, "Terminalul curent nu poate fi folosit deoarece nu este inrolat.\r\nVerificati conexiunea la Internet si parametrii de configurare ai aplicatiei si reincercati.");
      this.Add(CoduriRaspunsOperatieCard.ERR_INVALID_PIN, "PIN gresit.");
      this.Add(CoduriRaspunsOperatieCard.ERR_CARD_BLOCKED, "Card blocat.");
      this.Add(CoduriRaspunsOperatieCard.ERR_UM_TIME_OUT, "Timpul asteptare in comunicatia cu UM a expirat.");
      this.Add(CoduriRaspunsOperatieCard.ERR_INVALID_CARD, "Card invalid.");
      this.Add(CoduriRaspunsOperatieCard.ERR_CARD_ALREADY_ACTIVATED, "Cardul a fost deja activat.");
      this.Add(CoduriRaspunsOperatieCard.ERR_CARD_ACTIVARE, "Operatie esuata. Eroare la activare card.");
      this.Add(CoduriRaspunsOperatieCard.ERR_CARD_ACTIVARE_ABANDON, "Operatia de activare a cardului a fost anulata.");
      this.Add(CoduriRaspunsOperatieCard.ERR_CARD_NEACTIVAT, "Cardul nu este activat.");
      this.Add(CoduriRaspunsOperatieCard.ERR_SCHIMBARE_PIN, "Eroare la schimbarea PIN-ului.");
      this.Add(CoduriRaspunsOperatieCard.ERR_SCHIMBARE_PIN_ABANDON, "Operatia de schimbare PIN a fost anulata.");
      this.Add(CoduriRaspunsOperatieCard.ERR_UM_SYSTEM_ERROR, "Eroare de sistem raportata de unitatea de management.");
      this.Add(CoduriRaspunsOperatieCard.ERR_UM_MESAJ_FORMAT_INVALID, "Format invalid pentru mesaj in unitatea de management");
      this.Add(CoduriRaspunsOperatieCard.ERR_UM_TOKEN, "Eroare la obtinerea tokenului raportata de unitatea de management.");
      this.Add(CoduriRaspunsOperatieCard.ERR_AUTENTIFICARE, "Autentificare esuata.");
      this.Add(CoduriRaspunsOperatieCard.ERR_AUTENTIFICARE_ABANDON, "Operatia de autentificare a fost anulata.");
      this.Add(CoduriRaspunsOperatieCard.ERR_UM_INDISPONIBILA, "Unitatea de management indisponibila.");
      this.Add(CoduriRaspunsOperatieCard.ERR_CARD_PERSOANE_CONTACT_PESTE_MAX, "Puteti adauga maxim 2 persoane contact.");
      this.Add(CoduriRaspunsOperatieCard.ERR_CARD_DIAGNOSTICE_PESTE_MAX, "Puteti adauga maxim 10 diagnostice.");
      this.Add(CoduriRaspunsOperatieCard.ERR_CARD_BOLI_PESTE_MAX, "Puteti adauga maxim 20 de boli.");
      this.Add(CoduriRaspunsOperatieCard.ERR_CARD_ACCESARE, "Eroare la accesarea cardului.");
      this.Add(CoduriRaspunsOperatieCard.ERR_CARD_EXECUTIE_APDU, "Eroare executie comanda APDU.");
      this.Add(CoduriRaspunsOperatieCard.ERR_RESETARE_PIN, "Eroare la resetarea PIN-ului.");
      this.Add(CoduriRaspunsOperatieCard.ERR_RESETARE_PIN_NECONFIRMAT, "Resetare PIN neconfirmata.\r\nAnuntati helpdesk de blocarea cardului.");
      this.Add(CoduriRaspunsOperatieCard.ERR_RESETARE_PIN_CARD_NEBLOCAT, "Aceasta operatie se executa numai pe un card blocat.");
      this.Add(CoduriRaspunsOperatieCard.ERR_RESETARE_PIN_ABANDON, "Operatia de resetare pin a fost anulata.");
      this.Add(CoduriRaspunsOperatieCard.ERR_RESETARE_PIN_DREPTURI_INSUFICIENTE, "Nu aveti drepturi suficiente pentru a reseta PIN-ul pe acest terminal.");
      this.Add(CoduriRaspunsOperatieCard.ERR_PIN_LUNGIME_INVALIDA, "PIN invalid. PIN-ul trebuie sa fie format din 4 cifre.");
      this.Add(CoduriRaspunsOperatieCard.ERR_PIN_RESET_INVALID, "PIN de reset invalid.");
      this.Add(CoduriRaspunsOperatieCard.ERR_PIN_TRANSPORT_INVALID, "PIN de transport invalid.");
      this.Add(CoduriRaspunsOperatieCard.ERR_CARD_NEINREGISTRAT, "Cardul acesta nu este inregistrat in eCard.");
      this.Add(CoduriRaspunsOperatieCard.ERR_UM_PROCESARE, "UM nu poate procesa cererea.");
      this.Add(CoduriRaspunsOperatieCard.ERR_UM_CERERE_INVALIDA, "Cerere invalida catre UM.");
      this.Add(CoduriRaspunsOperatieCard.ERR_UM_AUTENTIFICARE, "UM autentificare esuata.");
      this.Add(CoduriRaspunsOperatieCard.ERR_ACTIVARE_PROFIL_INVALID, "Nu aveti drepturi suficiente pentru a executa operatia de activare.");
      this.Add(CoduriRaspunsOperatieCard.ERR_SCHIMBARE_PIN_DREPTURI_INSUFIECIENTE, "Nu aveti drepturi suficiente pentru aceasta operatie.");
      this.Add(CoduriRaspunsOperatieCard.ERR_UM_TRANZACTIE_INVALIDA, "UM tranzactie invalida.");
      this.Add(CoduriRaspunsOperatieCard.ERR_PROCESARE_RASPUNS_UM, "Eroare la procesarea raspunsului de la UM.");
      this.Add(CoduriRaspunsOperatieCard.ERR_CITIRE_CERTIFICAT, "Eroare la citirea certificatului.");
      this.Add(CoduriRaspunsOperatieCard.ERR_PIN_NECONFIRMAT, "PIN-ul nu se confirma.");
      this.Add(CoduriRaspunsOperatieCard.ERR_OPERATIE_CARD, "Eroare la executia operatiei pe card.");
      this.Add(CoduriRaspunsOperatieCard.ERR_CARD_TIMEOUT, "Eroare de timeout la executia operatiei.");
      this.Add(CoduriRaspunsOperatieCard.ERR_CARD_TERMINAL_DUPLICAT, "Driver duplicat pentru acelasi terminal.");
      this.Add(CoduriRaspunsOperatieCard.ERR_TERMINAL_VERIFICARE, "Eroare la verificarea terminalului.");
      this.Add(CoduriRaspunsOperatieCard.ERR_SEMNATURA, "Eroare la semnarea digitala.");
      this.Add(CoduriRaspunsOperatieCard.ERR_SEMNATURA_DREPTURI_INSUFICIENTE, "Nu aveti drepturi suficiente pentru semnatura.");
      this.Add(CoduriRaspunsOperatieCard.ERR_UM_CA_NETWORK, "Eroare la interogarea CA din UM.");
      this.Add(CoduriRaspunsOperatieCard.ERR_UM_ECARD_NETWORK, "Eroare la interogarea eCard din UM.");
      this.Add(CoduriRaspunsOperatieCard.ERR_CARD_SCHIMBAT_IN_TERMINAL, "Operatie esuata. Cardul a fost schimbat in terminal.");
      this.Add(CoduriRaspunsOperatieCard.ERR_SCHIMBARE_PIN_TRANSPORT, "Eroare la schimbarea pinului de transport.");
      this.Add(CoduriRaspunsOperatieCard.ERR_CARD_SCRIERE_ROLLBACK, "Eroare la scriere cu rollback esuat.");
      this.Add(CoduriRaspunsOperatieCard.ERR_VERIFICARE_CARD_ACTIVAT, "Eroare la verificare card activ.");
      this.Add(CoduriRaspunsOperatieCard.ERR_DETECTARE_STARE_CARD_IN_TERMINAL, "Eroare la detectarea starii cardului in terminal.");
      this.Add(CoduriRaspunsOperatieCard.ERR_TERMINAL_AFISARE_MESAJ, "Eroare la afisarea mesajului pe terminal.");
      this.Add(CoduriRaspunsOperatieCard.ERR_CITIRE_CERTIFICAT_MAI, "Eroare la citirea certificatului MAI.");
      this.Add(CoduriRaspunsOperatieCard.ERR_CITIRE_FISIER_TECH, "Eroare la citirea starii de activare.");
      this.Add(CoduriRaspunsOperatieCard.ERR_SCRIERE_FISIER_TECH, "Eroare la scrierea starii de activare.");
      this.Add(CoduriRaspunsOperatieCard.ERR_UM_TERMINAL_DATA, "Eroare la obtinerea datelor de inrolare de la unitatea de management.");
    }
  }
}
