// Decompiled with JetBrains decompiler
// Type: Novensys.eCard.SDK.TCPCommunication.TCPCServer
// Assembly: Novensys.eCard.SDK, Version=1.1.56.0, Culture=neutral, PublicKeyToken=null
// MVID: 59F9E456-D3DA-4066-A4A4-692B516A775E
// Assembly location: C:\Program Files (x86)\CNAS\SIUI-SPITAL\Novensys.eCard.SDK.dll

using System.Net;
using System.Net.Sockets;

namespace Novensys.eCard.SDK.TCPCommunication
{
  public class TCPCServer
  {
    private TCPClient TCPClient { get; set; }

    public TcpListener Listener { get; private set; }

    public IPEndPoint ServerEndPoint { get; private set; }

    private TCPCServer()
    {
    }

    public TCPCServer(IPAddress ip, int port)
    {
      this.ServerEndPoint = new IPEndPoint(ip, port);
      this.Listener = new TcpListener(ip, port);
    }

    public void Start()
    {
      this.Listener.Start();
    }

    public TCPClient AcceptClient()
    {
      try
      {
        this.TCPClient = new TCPClient(this.Listener.AcceptSocket());
      }
      catch
      {
      }
      return this.TCPClient;
    }

    public void Stop()
    {
      this.Listener.Stop();
    }
  }
}
