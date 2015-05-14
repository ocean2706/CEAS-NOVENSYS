// Decompiled with JetBrains decompiler
// Type: Novensys.eCard.SDK.Remoting.IRemoting
// Assembly: Novensys.eCard.SDK, Version=1.1.56.0, Culture=neutral, PublicKeyToken=null
// MVID: 59F9E456-D3DA-4066-A4A4-692B516A775E
// Assembly location: C:\Program Files (x86)\CNAS\SIUI-SPITAL\Novensys.eCard.SDK.dll

namespace Novensys.eCard.SDK.Remoting
{
  public interface IRemoting
  {
    string PortName { get; set; }

    string ChannelName { get; set; }

    string ApplicationName { get; }
  }
}
