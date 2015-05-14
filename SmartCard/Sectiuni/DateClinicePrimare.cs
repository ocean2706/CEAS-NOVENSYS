// Decompiled with JetBrains decompiler
// Type: Novensys.eCard.SDK.Entities.SmartCard.DateClinicePrimare
// Assembly: Novensys.eCard.SDK, Version=1.1.56.0, Culture=neutral, PublicKeyToken=null
// MVID: 59F9E456-D3DA-4066-A4A4-692B516A775E
// Assembly location: C:\Program Files (x86)\CNAS\SIUI-SPITAL\Novensys.eCard.SDK.dll

using System;

namespace Novensys.eCard.SDK.Entities.SmartCard
{
  [Serializable]
  public class DateClinicePrimare
  {
    public Camp GrupaSanguina { get; private set; }

    public Camp RH { get; private set; }

    public Camp SpatiuExtensiiUterioare4 { get; private set; }

    public Camp StatusDonatorOrgane { get; private set; }

    public Camp SpatiuExtensiiUterioare5 { get; private set; }

    public Camp DiagnosticeMedicaleCuRiscVital { get; private set; }

    public Camp BoliCronice { get; private set; }

    public Camp SpatiuExtensiiUterioare6 { get; private set; }

    public DateClinicePrimare()
    {
      this.GrupaSanguina = new Camp(CoduriCampuriCard.D1, "Grupa Sanguina", 3);
      this.RH = new Camp(CoduriCampuriCard.D2, "RH", 3);
      this.SpatiuExtensiiUterioare4 = new Camp(CoduriCampuriCard.D3, "Spatiu extensii ulterioare 4", 3);
      this.StatusDonatorOrgane = new Camp(CoduriCampuriCard.D4, "Status donator organe", 3);
      this.SpatiuExtensiiUterioare5 = new Camp(CoduriCampuriCard.D5, "Spatiu extensii ulterioare 5", 0);
      this.DiagnosticeMedicaleCuRiscVital = new Camp(CoduriCampuriCard.D6, "Diagnostice medicale cu risc vital", 4);
      this.BoliCronice = new Camp(CoduriCampuriCard.D7, "Boli cronice", 4);
      this.SpatiuExtensiiUterioare6 = new Camp(CoduriCampuriCard.E1, "Spatiu extensii ulterioare 6", 4);
    }
  }
}
