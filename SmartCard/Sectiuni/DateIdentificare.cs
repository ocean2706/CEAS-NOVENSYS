// Decompiled with JetBrains decompiler
// Type: Novensys.eCard.SDK.Entities.SmartCard.DateIdentificare
// Assembly: Novensys.eCard.SDK, Version=1.1.56.0, Culture=neutral, PublicKeyToken=null
// MVID: 59F9E456-D3DA-4066-A4A4-692B516A775E
// Assembly location: C:\Program Files (x86)\CNAS\SIUI-SPITAL\Novensys.eCard.SDK.dll

using System;

namespace Novensys.eCard.SDK.Entities.SmartCard
{
  [Serializable]
  public class DateIdentificare
  {
    public Camp Nume { get; private set; }

    public Camp Prenume { get; private set; }

    public Camp DataNastere { get; private set; }

    public Camp CNP { get; private set; }

    public DateIdentificare()
    {
      this.Nume = new Camp(CoduriCampuriCard.B1, "Nume", 1);
      this.Prenume = new Camp(CoduriCampuriCard.B2, "Prenume", 1);
      this.DataNastere = new Camp(CoduriCampuriCard.B3, "Data Nastere", 1);
      this.CNP = new Camp(CoduriCampuriCard.B4, "CNP", 1);
    }
  }
}
