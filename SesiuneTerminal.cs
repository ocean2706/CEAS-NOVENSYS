// Decompiled with JetBrains decompiler
// Type: Novensys.eCard.SDK.SesiuneTerminal
// Assembly: Novensys.eCard.SDK, Version=1.1.56.0, Culture=neutral, PublicKeyToken=null
// MVID: 59F9E456-D3DA-4066-A4A4-692B516A775E
// Assembly location: C:\Program Files (x86)\CNAS\SIUI-SPITAL\Novensys.eCard.SDK.dll

using Novensys.eCard.SDK.Entities.Terminal;
using Novensys.eCard.SDK.Utils.Log;
using System;

namespace Novensys.eCard.SDK
{
  public class SesiuneTerminal : SesiuneLucru, ISesiuneTerminal
  {
    public event TipTerminalSchimbatEventHandler TipTerminalSchimbat = null;

    private SesiuneTerminal()
    {
    }

    internal static SesiuneTerminal GetNewInstance(TerminalManager terminalManager)
    {
      SesiuneTerminal sesiuneTerminal = new SesiuneTerminal();
      terminalManager.Sesiune = (SesiuneLucru) sesiuneTerminal;
      sesiuneTerminal.TerminalManager = terminalManager;
      return sesiuneTerminal;
    }

    public void Stop()
    {
      LogManager.CloseLogger();
    }

    [Obsolete]
    public int CitesteDate(ref IdentificareTerminalData terminalData)
    {
      return -3;
    }

    [Obsolete]
    public int EditeazaDate(ActualizareTerminalData terminalData)
    {
      return -4;
    }

    public void SchimbareTipTerminal(TipTerminal tipTerminal)
    {
      if (this.TipTerminalSchimbat == null)
        return;
      this.TipTerminalSchimbat(tipTerminal);
    }
  }
}
