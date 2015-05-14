// Decompiled with JetBrains decompiler
// Type: Novensys.eCard.SDK.Entities.SmartCard.ExtensiiClinice2
// Assembly: Novensys.eCard.SDK, Version=1.1.56.0, Culture=neutral, PublicKeyToken=null
// MVID: 59F9E456-D3DA-4066-A4A4-692B516A775E
// Assembly location: C:\Program Files (x86)\CNAS\SIUI-SPITAL\Novensys.eCard.SDK.dll

using System;

namespace Novensys.eCard.SDK.Entities.SmartCard
{
  [Serializable]
  public class ExtensiiClinice2
  {
    public Camp SpatiuExtensiiUterioare8 { get; private set; }

    public ExtensiiClinice2()
    {
      this.SpatiuExtensiiUterioare8 = new Camp(CoduriCampuriCard.E3, "Spatiu pentru extensii ulterioare 8", 0);
    }
  }
}
