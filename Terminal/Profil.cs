// Decompiled with JetBrains decompiler
// Type: Novensys.eCard.SDK.Entities.Terminal.Profil
// Assembly: Novensys.eCard.SDK, Version=1.1.56.0, Culture=neutral, PublicKeyToken=null
// MVID: 59F9E456-D3DA-4066-A4A4-692B516A775E
// Assembly location: C:\Program Files (x86)\CNAS\SIUI-SPITAL\Novensys.eCard.SDK.dll

using Novensys.eCard.SDK.Entities.SmartCard;
using System;

namespace Novensys.eCard.SDK.Entities.Terminal
{
  [Serializable]
  public class Profil
  {
    public ProfileCard ProfilCard { get; set; }

    public byte[] Cheie { get; set; }

    public Profil()
    {
    }

    public Profil(ProfileCard profilCard, byte[] cheie)
    {
      this.ProfilCard = profilCard;
      this.Cheie = cheie;
    }
  }
}
