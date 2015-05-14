// Decompiled with JetBrains decompiler
// Type: Novensys.eCard.SDK.TCPCommunication.TCPClient
// Assembly: Novensys.eCard.SDK, Version=1.1.56.0, Culture=neutral, PublicKeyToken=null
// MVID: 59F9E456-D3DA-4066-A4A4-692B516A775E
// Assembly location: C:\Program Files (x86)\CNAS\SIUI-SPITAL\Novensys.eCard.SDK.dll

using Novensys.eCard.SDK.Utils.Log;
using System;
using System.Net;
using System.Net.Sockets;

namespace Novensys.eCard.SDK.TCPCommunication
{
  public class TCPClient : TcpClient
  {
    public NetworkStream NetworkStream { get; set; }

    public TCPClient()
    {
    }

    public TCPClient(Socket socket)
    {
      this.Client = socket;
    }

    public TCPClient(IPEndPoint endPoint)
    {
      this.Connect(endPoint);
    }

    public void Send(byte[] message)
    {
      if (this.NetworkStream == null)
        this.NetworkStream = this.GetStream();
      if (!this.NetworkStream.CanWrite)
        return;
      this.NetworkStream.Write(message, 0, message.Length);
    }

    public byte[] Receive()
    {
      byte[] buffer1 = (byte[]) null;
      try
      {
        if (this.NetworkStream == null)
          this.NetworkStream = this.GetStream();
        byte[] buffer2 = new byte[2];
        if (this.NetworkStream.CanRead)
        {
          this.NetworkStream.Read(buffer2, 0, 2);
          int length = (int) buffer2[0] * 256 + (int) buffer2[1];
          buffer1 = new byte[length];
          int offset = 0;
          while (offset < length)
          {
            int num = this.NetworkStream.Read(buffer1, offset, length - offset);
            offset += num;
          }
        }
      }
      catch (Exception ex)
      {
        LogManager.FileLog(ex);
      }
      return buffer1;
    }

    public new void Connect(IPEndPoint endPoint)
    {
      if (this.Connected)
        return;
      this.Client.Connect((EndPoint) endPoint);
      this.NetworkStream = this.GetStream();
      this.NetworkStream.ReadTimeout = 30000;
    }

    public new void Close()
    {
      this.Client.Shutdown(SocketShutdown.Both);
      base.Close();
      this.Dispose(true);
    }
  }
}
