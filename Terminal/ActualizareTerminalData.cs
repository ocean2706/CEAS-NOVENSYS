// Decompiled with JetBrains decompiler
// Type: Novensys.eCard.SDK.Entities.Terminal.ActualizareTerminalData
// Assembly: Novensys.eCard.SDK, Version=1.1.56.0, Culture=neutral, PublicKeyToken=null
// MVID: 59F9E456-D3DA-4066-A4A4-692B516A775E
// Assembly location: C:\Program Files (x86)\CNAS\SIUI-SPITAL\Novensys.eCard.SDK.dll

using System;
using System.Collections.Generic;

namespace Novensys.eCard.SDK.Entities.Terminal
{
  [Serializable]
  public class ActualizareTerminalData
  {
    public string TerminalId { get; set; }

    public List<Profil> Profile { get; set; }

    public byte[] CheieCriptarePIN { get; set; }

    public ActualizareTerminalData()
    {
      this.Profile = new List<Profil>();
    }
  }
}
