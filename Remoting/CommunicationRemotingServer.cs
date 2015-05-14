// Decompiled with JetBrains decompiler
// Type: Novensys.eCard.SDK.Remoting.CommunicationRemotingServer
// Assembly: Novensys.eCard.SDK, Version=1.1.56.0, Culture=neutral, PublicKeyToken=null
// MVID: 59F9E456-D3DA-4066-A4A4-692B516A775E
// Assembly location: C:\Program Files (x86)\CNAS\SIUI-SPITAL\Novensys.eCard.SDK.dll

using System;
using System.Runtime.Remoting;

namespace Novensys.eCard.SDK.Remoting
{
  public class CommunicationRemotingServer : BaseRemotingServer
  {
    public EvenimenteCard EvenimenteCard { get; set; }

    public override string ApplicationName
    {
      get
      {
        return "CommunicationRemotingServer";
      }
    }

    public CommunicationRemotingServer()
    {
      this.PortName = "8002";
      this.ChannelName = "CommunicationServerChannel";
    }

    protected override void ActivateRemoteObjects()
    {
      RemotingConfiguration.RegisterWellKnownServiceType(new WellKnownServiceTypeEntry(typeof (EvenimenteCard), "CardEvents", WellKnownObjectMode.Singleton));
    }

    protected override void CreateRemoteObjects()
    {
      this.EvenimenteCard = new EvenimenteCard();
      this.EvenimenteCard.InternalRef = RemotingServices.Marshal((MarshalByRefObject) this.EvenimenteCard, "CardEvents");
    }

    protected override void DisconnectRemoteObjects()
    {
      RemotingServices.Unmarshal(this.EvenimenteCard.InternalRef);
      RemotingServices.Disconnect((MarshalByRefObject) this.EvenimenteCard);
    }
  }
}
