// Decompiled with JetBrains decompiler
// Type: Novensys.eCard.SDK.Entities.SmartCard.DateAdministrative
// Assembly: Novensys.eCard.SDK, Version=1.1.56.0, Culture=neutral, PublicKeyToken=null
// MVID: 59F9E456-D3DA-4066-A4A4-692B516A775E
// Assembly location: C:\Program Files (x86)\CNAS\SIUI-SPITAL\Novensys.eCard.SDK.dll

using System;

namespace Novensys.eCard.SDK.Entities.SmartCard
{
  [Serializable]
  public class DateAdministrative
  {
    public Camp NumarAsigurat { get; private set; }

    public Camp SpatiuExtensiiUterioare1 { get; private set; }

    public Camp SpatiuExtensiiUterioare2 { get; private set; }

    public Camp NumeMedicFamilie { get; private set; }

    public Camp PrenumeMedicFamilie { get; private set; }

    public Camp CodMedicFamilie { get; private set; }

    public Camp PersoaneContact { get; private set; }

    public Camp TelefonMedicFamilie { get; private set; }

    public Camp SpatiuExtensiiUterioare3 { get; private set; }

    public DateAdministrative()
    {
      this.NumarAsigurat = new Camp(CoduriCampuriCard.C1, "Numar asigurat", 1);
      this.SpatiuExtensiiUterioare1 = new Camp(CoduriCampuriCard.C2, "Spatiu extensii ulterioare 1", 0);
      this.SpatiuExtensiiUterioare2 = new Camp(CoduriCampuriCard.C3, "Spatiu extensii ulterioare 2", 0);
      this.NumeMedicFamilie = new Camp(CoduriCampuriCard.C4, "Nume medic familie", 2);
      this.PrenumeMedicFamilie = new Camp(CoduriCampuriCard.C5, "Prenume medic familie", 2);
      this.CodMedicFamilie = new Camp(CoduriCampuriCard.C6, "Cod medic familie", 2);
      this.TelefonMedicFamilie = new Camp(CoduriCampuriCard.C7, "Telefon medic familie", 2);
      this.PersoaneContact = new Camp(CoduriCampuriCard.C8, "Persoane de contact", 2);
      this.SpatiuExtensiiUterioare3 = new Camp(CoduriCampuriCard.C9, "Spatiu extensii ulterioare 3", 2);
    }
  }
}
