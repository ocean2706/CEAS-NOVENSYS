// Decompiled with JetBrains decompiler
// Type: Novensys.eCard.SDK.TCPCommunication.Errors
// Assembly: Novensys.eCard.SDK, Version=1.1.56.0, Culture=neutral, PublicKeyToken=null
// MVID: 59F9E456-D3DA-4066-A4A4-692B516A775E
// Assembly location: C:\Program Files (x86)\CNAS\SIUI-SPITAL\Novensys.eCard.SDK.dll

using System.Collections.Generic;

namespace Novensys.eCard.SDK.TCPCommunication
{
  public class Errors : Dictionary<ErrorCodes, string>
  {
    public Errors()
    {
      this.Add(ErrorCodes.ERR_METHOD_FAILED, "Eroare la executia metodei '{0}'");
      this.Add(ErrorCodes.ERR_INVALID_ISO_MESSAGE, "Mesajul receptionat este invalid.");
    }
  }
}
