// Decompiled with JetBrains decompiler
// Type: Novensys.eCard.SDK.Remoting.BaseRemotingClient
// Assembly: Novensys.eCard.SDK, Version=1.1.56.0, Culture=neutral, PublicKeyToken=null
// MVID: 59F9E456-D3DA-4066-A4A4-692B516A775E
// Assembly location: C:\Program Files (x86)\CNAS\SIUI-SPITAL\Novensys.eCard.SDK.dll

using Novensys.eCard.SDK.Utils.Log;
using System;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Ipc;

namespace Novensys.eCard.SDK.Remoting
{
  public abstract class BaseRemotingClient : IRemoting
  {
    private bool Connected { get; set; }

    private IpcClientChannel Channel { get; set; }

    public string PortName { get; set; }

    public string ChannelName { get; set; }

    public virtual string ApplicationName
    {
      get
      {
        return (string) null;
      }
    }

    public BaseRemotingClient()
    {
      this.Connected = false;
    }

    public void Register()
    {
      try
      {
        this.Channel = RemotingHelper.RegisterClientChannel(this.PortName, this.ChannelName);
      }
      catch (RemotingException ex)
      {
        LogManager.FileLog((Exception) ex);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public void Connect()
    {
      if (this.Connected)
        return;
      try
      {
        this.CreateProxyObjects();
        this.Connected = true;
      }
      catch (Exception ex)
      {
        this.Connected = false;
        throw ex;
      }
    }

    public void Disconnect()
    {
      if (!this.Connected)
        return;
      RemotingHelper.UnregisterChannel((IChannel) this.Channel);
    }

    protected virtual void CreateProxyObjects()
    {
    }

    protected virtual void ActivateRemoteObjects()
    {
    }
  }
}
