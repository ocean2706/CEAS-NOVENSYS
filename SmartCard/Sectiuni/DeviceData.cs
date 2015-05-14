// Decompiled with JetBrains decompiler
// Type: Novensys.eCard.SDK.Entities.SmartCard.DeviceData
// Assembly: Novensys.eCard.SDK, Version=1.1.56.0, Culture=neutral, PublicKeyToken=null
// MVID: 59F9E456-D3DA-4066-A4A4-692B516A775E
// Assembly location: C:\Program Files (x86)\CNAS\SIUI-SPITAL\Novensys.eCard.SDK.dll

using System;

namespace Novensys.eCard.SDK.Entities.SmartCard
{
  [Serializable]
  public class DeviceData
  {
    public Camp NumarCard { get; private set; }

    public Camp CodCIP { get; private set; }

    public Camp Versiune { get; private set; }

    public DeviceData()
    {
      this.NumarCard = new Camp(CoduriCampuriCard.A1, "Numar card", 1);
      this.CodCIP = new Camp(CoduriCampuriCard.A2, "Cod CIP", 1);
      this.Versiune = new Camp(CoduriCampuriCard.A3, "Versiune", 1);
    }
  }
}
