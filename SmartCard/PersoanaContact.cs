// Decompiled with JetBrains decompiler
// Type: Novensys.eCard.SDK.Entities.SmartCard.PersoanaContact
// Assembly: Novensys.eCard.SDK, Version=1.1.56.0, Culture=neutral, PublicKeyToken=null
// MVID: 59F9E456-D3DA-4066-A4A4-692B516A775E
// Assembly location: C:\Program Files (x86)\CNAS\SIUI-SPITAL\Novensys.eCard.SDK.dll

using System;

namespace Novensys.eCard.SDK.Entities.SmartCard
{
  [Serializable]
  public class PersoanaContact
  {
    public string NumeSiPrenume { get; set; }

    public string Telefon { get; set; }

    public PersoanaContact(string numeSiPrenume, string telefon)
    {
      this.NumeSiPrenume = numeSiPrenume;
      this.Telefon = telefon;
    }
  }
}
