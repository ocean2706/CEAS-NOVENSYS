// Decompiled with JetBrains decompiler
// Type: Novensys.eCard.SDK.Remoting.BaseRemotingServer
// Assembly: Novensys.eCard.SDK, Version=1.1.56.0, Culture=neutral, PublicKeyToken=null
// MVID: 59F9E456-D3DA-4066-A4A4-692B516A775E
// Assembly location: C:\Program Files (x86)\CNAS\SIUI-SPITAL\Novensys.eCard.SDK.dll

using System;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Ipc;

namespace Novensys.eCard.SDK.Remoting
{
  public abstract class BaseRemotingServer : IRemoting
  {
    private bool ServerActive { get; set; }

    private IpcServerChannel Channel { get; set; }

    public string PortName { get; set; }

    public string ChannelName { get; set; }

    public virtual string ApplicationName
    {
      get
      {
        return (string) null;
      }
    }

    public void Start()
    {
      if (this.ServerActive)
        return;
      try
      {
        this.Channel = RemotingHelper.RegisterServerChannel(this.PortName, this.ChannelName);
        this.ActivateRemoteObjects();
        this.CreateRemoteObjects();
        this.ServerActive = true;
      }
      catch (RemotingException ex)
      {
        throw ex;
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public void Stop()
    {
      if (!this.ServerActive)
        return;
      try
      {
        this.DisconnectRemoteObjects();
        RemotingHelper.UnregisterChannel((IChannel) this.Channel);
        this.ServerActive = false;
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    protected virtual void ActivateRemoteObjects()
    {
    }

    protected virtual void CreateRemoteObjects()
    {
    }

    protected virtual void DisconnectRemoteObjects()
    {
    }
  }
}
