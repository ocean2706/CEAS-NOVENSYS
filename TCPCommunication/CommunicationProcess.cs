// Decompiled with JetBrains decompiler
// Type: Novensys.eCard.SDK.TCPCommunication.CommunicationProcess
// Assembly: Novensys.eCard.SDK, Version=1.1.56.0, Culture=neutral, PublicKeyToken=null
// MVID: 59F9E456-D3DA-4066-A4A4-692B516A775E
// Assembly location: C:\Program Files (x86)\CNAS\SIUI-SPITAL\Novensys.eCard.SDK.dll

using Novensys.eCard.SDK.ISO8583;
using Novensys.eCard.SDK.ISO8583.NetworkHeaders;
using Novensys.eCard.SDK.Utils.Log;
using System;

namespace Novensys.eCard.SDK.TCPCommunication
{
  public class CommunicationProcess : IDisposable
  {
    public TCPClient TCPClient { get; set; }

    public Iso8583Message IncomingMessage { get; set; }

    public Iso8583Message OutgoingMessage { get; set; }

    public ProcessRequestDelegate ProcessRequest { get; set; }

    public CommunicationProcess()
    {
    }

    public CommunicationProcess(TCPClient tcpClient)
    {
      this.TCPClient = tcpClient;
    }

    public void SendMessage(Iso8583Message isoMsg)
    {
      this.TCPClient.Send(new TwoByteHeader().Pack((IMessage) isoMsg));
    }

    public Iso8583Message ReceiveMessage()
    {
      Iso8583Message iso8583Message = (Iso8583Message) null;
      try
      {
        byte[] msg = this.TCPClient.Receive();
        if (msg == null || msg.Length == 0)
          return (Iso8583Message) null;
        iso8583Message = new Iso8583Message();
        iso8583Message.Unpack(msg, 0);
      }
      catch (Exception ex)
      {
        LogManager.FileLog(ex);
      }
      return iso8583Message;
    }

    public void ManageCommunication()
    {
      this.IncomingMessage = this.ReceiveMessage();
      if (this.IncomingMessage == null)
        return;
      Iso8583Message isoMsg = this.ProcessRequest(this.IncomingMessage);
      this.OutgoingMessage = isoMsg;
      this.SendMessage(isoMsg);
      if (this.IncomingMessage.MessageType != 2048 && this.IncomingMessage.MessageType != 2064)
        this.Terminate();
    }

    private void Terminate()
    {
      if (this.TCPClient == null)
        return;
      this.TCPClient.Close();
      this.TCPClient = (TCPClient) null;
    }

    public void Dispose()
    {
      this.Terminate();
    }
  }
}
