// Decompiled with JetBrains decompiler
// Type: Novensys.eCard.SDK.Entities.SmartCard.DezvoltariUlterioare1
// Assembly: Novensys.eCard.SDK, Version=1.1.56.0, Culture=neutral, PublicKeyToken=null
// MVID: 59F9E456-D3DA-4066-A4A4-692B516A775E
// Assembly location: C:\Program Files (x86)\CNAS\SIUI-SPITAL\Novensys.eCard.SDK.dll

using System;

namespace Novensys.eCard.SDK.Entities.SmartCard
{
  [Serializable]
  public class DezvoltariUlterioare1
  {
    public Camp PartitieDezvoltariUlterioare1 { get; private set; }

    public DezvoltariUlterioare1()
    {
      this.PartitieDezvoltariUlterioare1 = new Camp(CoduriCampuriCard.F1, "Partitie dezvoltari ulterioare 1", 5);
    }
  }
}
