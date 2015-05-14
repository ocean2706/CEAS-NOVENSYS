// Decompiled with JetBrains decompiler
// Type: Novensys.eCard.SDK.Remoting.CommunicationRemotingClient
// Assembly: Novensys.eCard.SDK, Version=1.1.56.0, Culture=neutral, PublicKeyToken=null
// MVID: 59F9E456-D3DA-4066-A4A4-692B516A775E
// Assembly location: C:\Program Files (x86)\CNAS\SIUI-SPITAL\Novensys.eCard.SDK.dll

using System;
using System.Runtime.Remoting;

namespace Novensys.eCard.SDK.Remoting
{
  public class CommunicationRemotingClient : BaseRemotingClient
  {
    public override string ApplicationName
    {
      get
      {
        return "CommunicationRemotingClient";
      }
    }

    public EvenimenteCard EvenimenteCard { get; set; }

    public CommunicationRemotingClient()
    {
      this.PortName = "8002";
      this.ChannelName = "CommunicationClientChannel";
    }

    protected override void CreateProxyObjects()
    {
      this.EvenimenteCard = (EvenimenteCard) Activator.GetObject(typeof (EvenimenteCard), string.Format("ipc://{0}/CardEvents", (object) this.PortName));
    }

    protected override void ActivateRemoteObjects()
    {
      if (RemotingConfiguration.IsWellKnownClientType(typeof (EvenimenteCard)) != null)
        return;
      RemotingConfiguration.RegisterWellKnownClientType(new WellKnownClientTypeEntry(typeof (EvenimenteCard), string.Format("ipc://{0}/CardEvents", (object) this.PortName)));
    }
  }
}
