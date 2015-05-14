// Decompiled with JetBrains decompiler
// Type: Novensys.eCard.SDK.SesiuneLucru
// Assembly: Novensys.eCard.SDK, Version=1.1.56.0, Culture=neutral, PublicKeyToken=null
// MVID: 59F9E456-D3DA-4066-A4A4-692B516A775E
// Assembly location: C:\Program Files (x86)\CNAS\SIUI-SPITAL\Novensys.eCard.SDK.dll

using Novensys.eCard.SDK.Entities.SmartCard;
using Novensys.eCard.SDK.Entities.Terminal;
using System.Collections.Generic;

namespace Novensys.eCard.SDK
{
  public abstract class SesiuneLucru
  {
    internal TerminalManager TerminalManager { get; set; }

    public string TerminalCurent
    {
      get
      {
        return this.TerminalManager.TerminalCurent;
      }
    }

    public string TerminalId
    {
      get
      {
        return this.TerminalManager.TerminalId;
      }
    }

    public List<ProfileCard> Profile
    {
      get
      {
        List<ProfileCard> list = new List<ProfileCard>();
        if (this.TerminalManager.TerminalData != null)
        {
          foreach (Profil profil in this.TerminalManager.TerminalData.Profile)
            list.Add(profil.ProfilCard);
        }
        return list;
      }
    }
  }
}
