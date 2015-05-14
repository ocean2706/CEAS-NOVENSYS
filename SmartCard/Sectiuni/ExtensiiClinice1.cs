// Decompiled with JetBrains decompiler
// Type: Novensys.eCard.SDK.Entities.SmartCard.ExtensiiClinice1
// Assembly: Novensys.eCard.SDK, Version=1.1.56.0, Culture=neutral, PublicKeyToken=null
// MVID: 59F9E456-D3DA-4066-A4A4-692B516A775E
// Assembly location: C:\Program Files (x86)\CNAS\SIUI-SPITAL\Novensys.eCard.SDK.dll

using System;

namespace Novensys.eCard.SDK.Entities.SmartCard
{
  [Serializable]
  public class ExtensiiClinice1
  {
    public Camp SpatiuExtensiiUterioare7 { get; private set; }

    public ExtensiiClinice1()
    {
      this.SpatiuExtensiiUterioare7 = new Camp(CoduriCampuriCard.E2, "Spatiu pentru extensii ulterioare 7", 0);
    }
  }
}
