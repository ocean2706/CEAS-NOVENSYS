// Decompiled with JetBrains decompiler
// Type: Novensys.eCard.SDK.MesajeRaspunsTerminal
// Assembly: Novensys.eCard.SDK, Version=1.1.56.0, Culture=neutral, PublicKeyToken=null
// MVID: 59F9E456-D3DA-4066-A4A4-692B516A775E
// Assembly location: C:\Program Files (x86)\CNAS\SIUI-SPITAL\Novensys.eCard.SDK.dll

using Novensys.eCard.SDK.Entities.Terminal;
using System.Collections.Generic;

namespace Novensys.eCard.SDK
{
  public class MesajeRaspunsTerminal : Dictionary<CoduriRaspunsOperatieTerminal, string>
  {
    public MesajeRaspunsTerminal()
    {
      this.Add(CoduriRaspunsOperatieTerminal.OK, "Operatia s-a executat cu succes.");
      this.Add(CoduriRaspunsOperatieTerminal.ERR_TERMINAL_DECONECTAT, "Operatie esuata. Terminalul nu este conectat la PC.");
      this.Add(CoduriRaspunsOperatieTerminal.ERR_TERMINAL_CITIRE, "Eroare in timpul operatiei de citire.");
      this.Add(CoduriRaspunsOperatieTerminal.ERR_TERMINAL_SCRIERE, "Eroare in timpul operatiei de scriere.");
      this.Add(CoduriRaspunsOperatieTerminal.ERR_TERMINAL_MAI_MULT_DE_1, "Nu se poate conecta decat un singur terminal la PC.");
      this.Add(CoduriRaspunsOperatieTerminal.ERR_TERMINAL_NEINROLAT, "Terminalul conectat nu este inrolat.");
      this.Add(CoduriRaspunsOperatieTerminal.ERR_TERMINAL_VERIFICARE, "Eroare la verificarea terminalului.");
    }
  }
}
