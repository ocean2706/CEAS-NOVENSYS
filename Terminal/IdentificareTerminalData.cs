// Decompiled with JetBrains decompiler
// Type: Novensys.eCard.SDK.Entities.Terminal.IdentificareTerminalData
// Assembly: Novensys.eCard.SDK, Version=1.1.56.0, Culture=neutral, PublicKeyToken=null
// MVID: 59F9E456-D3DA-4066-A4A4-692B516A775E
// Assembly location: C:\Program Files (x86)\CNAS\SIUI-SPITAL\Novensys.eCard.SDK.dll

using Novensys.eCard.SDK.Entities.SmartCard;

namespace Novensys.eCard.SDK.Entities.Terminal
{
  public class IdentificareTerminalData
  {
    public string TerminalId { get; private set; }

    public string Serie { get; private set; }

    public ProfileCard[] ProfileAsociate { get; private set; }

    public bool AreCheieCriptarePINPrezenta { get; private set; }

    public IdentificareTerminalData(string terminalId, string serie, ProfileCard[] profileAsociate, bool areCheieCriptarePinPrezenta)
    {
      this.TerminalId = terminalId;
      this.Serie = serie;
      this.ProfileAsociate = profileAsociate;
      this.AreCheieCriptarePINPrezenta = areCheieCriptarePinPrezenta;
    }
  }
}
