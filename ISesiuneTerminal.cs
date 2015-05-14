// Decompiled with JetBrains decompiler
// Type: Novensys.eCard.SDK.ISesiuneTerminal
// Assembly: Novensys.eCard.SDK, Version=1.1.56.0, Culture=neutral, PublicKeyToken=null
// MVID: 59F9E456-D3DA-4066-A4A4-692B516A775E
// Assembly location: C:\Program Files (x86)\CNAS\SIUI-SPITAL\Novensys.eCard.SDK.dll

using Novensys.eCard.SDK.Entities.SmartCard;
using Novensys.eCard.SDK.Entities.Terminal;
using System.Collections.Generic;

namespace Novensys.eCard.SDK
{
  public interface ISesiuneTerminal
  {
    string TerminalCurent { get; }

    string TerminalId { get; }

    List<ProfileCard> Profile { get; }

    event TipTerminalSchimbatEventHandler TipTerminalSchimbat;

    int CitesteDate(ref IdentificareTerminalData terminalData);

    int EditeazaDate(ActualizareTerminalData terminalData);

    void Stop();

    void SchimbareTipTerminal(TipTerminal tipTerminal);
  }
}
