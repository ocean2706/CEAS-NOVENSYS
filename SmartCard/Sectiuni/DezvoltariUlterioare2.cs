// Decompiled with JetBrains decompiler
// Type: Novensys.eCard.SDK.Entities.SmartCard.DezvoltariUlterioare2
// Assembly: Novensys.eCard.SDK, Version=1.1.56.0, Culture=neutral, PublicKeyToken=null
// MVID: 59F9E456-D3DA-4066-A4A4-692B516A775E
// Assembly location: C:\Program Files (x86)\CNAS\SIUI-SPITAL\Novensys.eCard.SDK.dll

using System;

namespace Novensys.eCard.SDK.Entities.SmartCard
{
  [Serializable]
  public class DezvoltariUlterioare2
  {
    public Camp PartitieDezvoltariUlterioare2 { get; private set; }

    public DezvoltariUlterioare2()
    {
      this.PartitieDezvoltariUlterioare2 = new Camp(CoduriCampuriCard.H0, "Partitie dezvoltari ulterioare 2", 7);
    }
  }
}
