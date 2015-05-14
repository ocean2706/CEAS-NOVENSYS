// Decompiled with JetBrains decompiler
// Type: Novensys.eCard.SDK.TCPCommunication.CommunicationService
// Assembly: Novensys.eCard.SDK, Version=1.1.56.0, Culture=neutral, PublicKeyToken=null
// MVID: 59F9E456-D3DA-4066-A4A4-692B516A775E
// Assembly location: C:\Program Files (x86)\CNAS\SIUI-SPITAL\Novensys.eCard.SDK.dll

using Novensys.eCard.SDK;
using Novensys.eCard.SDK.Entities;
using Novensys.eCard.SDK.ISO8583;
using Novensys.eCard.SDK.Remoting;
using Novensys.eCard.SDK.Utils.Log;
using System;
using System.Configuration;
using System.Globalization;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Novensys.eCard.SDK.TCPCommunication
{
  public class CommunicationService
  {
    protected Errors Errors { get; set; }

    protected Informations Informations { get; set; }

    protected Warnings Warnings { get; set; }

    protected UMResponses UMResponses { get; set; }

    public bool UseRemoting { get; set; }

    private TCPCServer Server { get; set; }

    private bool ready { get; set; }

    private IPAddress IPAddressToListen { get; set; }

    private int PortToListen { get; set; }

    private IPAddress IPAddressToConnect { get; set; }

    private int PortToConnect { get; set; }

    private CommunicationRemotingServer RemotingServer { get; set; }

    private IPEndPoint EndPoint { get; set; }

    public CommunicationService()
    {
      this.UseRemoting = true;
      this.Errors = new Errors();
      this.Informations = new Informations();
      this.Warnings = new Warnings();
      this.UMResponses = new UMResponses();
    }

    public void Start()
    {
      try
      {
        this.ready = true;
        this.LoadSettings();
        this.EndPoint = new IPEndPoint(this.IPAddressToConnect, this.PortToConnect);
        this.StartRemotingServer();
        this.Server = new TCPCServer(this.IPAddressToListen, this.PortToListen);
        this.Server.Start();
        LogManager.AddSeparatorLine();
        LogManager.FileLog(this.Informations[InformationCodes.INF_SERVICE_START]);
        ThreadPool.QueueUserWorkItem(new WaitCallback(this.Listen), (object) null);
      }
      catch (Exception ex)
      {
        LogManager.FileLog(string.Format(this.Errors[ErrorCodes.ERR_METHOD_FAILED], (object) Globals.GetCalledMethod()), ex);
      }
    }

    public void Stop()
    {
      try
      {
        this.ready = false;
        this.StopRemotingServer();
        this.Server.Stop();
        LogManager.AddSeparatorLine();
        LogManager.FileLog(this.Informations[InformationCodes.INF_SERVICE_STOP]);
      }
      catch (Exception ex)
      {
        LogManager.FileLog(string.Format(this.Errors[ErrorCodes.ERR_METHOD_FAILED], (object) Globals.GetCalledMethod()), ex);
      }
      finally
      {
        LogManager.CloseLogger();
      }
    }

    private void LoadSettings()
    {
      AppSettingsReader appSettingsReader = new AppSettingsReader();
      string ipString1 = appSettingsReader.GetValue("IPAddressToListen", typeof (string)).ToString();
      this.IPAddressToListen = !string.IsNullOrEmpty(ipString1) ? IPAddress.Parse(ipString1) : this.GetLocalIPAddress();
      string ipString2 = appSettingsReader.GetValue("IPAddressToConnect", typeof (string)).ToString();
      this.IPAddressToConnect = !string.IsNullOrEmpty(ipString2) ? IPAddress.Parse(ipString2) : this.GetLocalIPAddress();
      this.PortToListen = (int) appSettingsReader.GetValue("PortToListen", typeof (int));
      this.PortToConnect = (int) appSettingsReader.GetValue("PortToConnect", typeof (int));
    }

    private void Listen(object state)
    {
      try
      {
        new Thread(new ThreadStart(this.ProcessReceivedMessages)).Start();
      }
      catch (Exception ex)
      {
        LogManager.FileLog(string.Format(this.Errors[ErrorCodes.ERR_METHOD_FAILED], (object) Globals.GetCalledMethod()), ex);
        throw ex;
      }
    }

    private void ProcessReceivedMessages()
    {
      TCPClient tcpClient = (TCPClient) null;
      CommunicationProcess process = (CommunicationProcess) null;
      while (this.ready)
      {
        if (tcpClient == null)
          process = new CommunicationProcess(this.Server.AcceptClient());
        process.ProcessRequest = new ProcessRequestDelegate(this.ProcessRequest);
        this.ManageCommunication(process);
        tcpClient = process.TCPClient;
      }
    }

    private void ManageCommunication(CommunicationProcess process)
    {
      try
      {
        process.ManageCommunication();
        if (process.OutgoingMessage == null)
          return;
        LogManager.FileLog(string.Format(this.Informations[InformationCodes.INF_SEND_RESPONSE], (object) this.GetMessageType(process.OutgoingMessage), (object) ((object) process.OutgoingMessage).ToString()));
      }
      catch (Exception ex)
      {
        LogManager.FileLog(string.Format(this.Errors[ErrorCodes.ERR_METHOD_FAILED], (object) Globals.GetCalledMethod()), ex);
      }
    }

    protected Iso8583Message SendRequest(Iso8583Message requestMsg, CommunicationProcess process)
    {
      Iso8583Message iso8583Message = (Iso8583Message) null;
      process.SendMessage(requestMsg);
      LogManager.FileLog(string.Format(this.Informations[InformationCodes.INF_SEND_REQUEST], (object) this.GetMessageType(requestMsg), (object) ((object) requestMsg).ToString()));
      bool flag = false;
      DateTime now1 = DateTime.Now;
      DateTime now2 = DateTime.Now;
      TimeSpan timeSpan = new TimeSpan();
      while (iso8583Message == null || !flag)
      {
        if ((DateTime.Now - now1).TotalMilliseconds > 30000.0)
          throw new CommunicationException(CommunicationExceptionType.RESPONSE_TIMEOUT, "UM-ul nu a raspuns in intervalul de timp alocat.");
        iso8583Message = process.ReceiveMessage();
        flag = this.IsValidResponse(requestMsg, iso8583Message);
        if (!flag)
          Thread.Sleep(TimeSpan.FromMilliseconds(10.0));
      }
      if (iso8583Message != null)
        LogManager.FileLog(string.Format(this.Informations[InformationCodes.INF_RECEIVE_RESPONSE], (object) this.GetMessageType(iso8583Message), (object) ((object) iso8583Message).ToString()));
      return iso8583Message;
    }

    private bool IsValidResponse(Iso8583Message requestMsg, Iso8583Message responseMsg)
    {
      bool flag = false;
      if (responseMsg == null)
        return false;
      if (responseMsg[11] != null)
        flag = responseMsg[11] == requestMsg[11];
      return flag;
    }

    protected virtual Iso8583Message ProcessRequest(Iso8583Message requestMsg)
    {
      LogManager.FileLog(string.Format(this.Informations[InformationCodes.INF_RECEIVE_REQUEST], (object) this.GetMessageType(requestMsg), (object) ((object) requestMsg).ToString()));
      Iso8583Message iso8583Message = (Iso8583Message) null;
      if (IsoConvert.FromIntToMsgType(Iso8583Message.MsgType.GetResponse(requestMsg.MessageType)) == "0810")
        iso8583Message = ISO8583Util.BuildHandshakeResponse(requestMsg);
      return iso8583Message;
    }

    private void StartRemotingServer()
    {
      if (!this.UseRemoting)
        return;
      this.RemotingServer = new CommunicationRemotingServer();
      this.RemotingServer.Start();
      this.RemotingServer.EvenimenteCard.ObtineToken += new ObtineTokenEventHandler(this.EvenimenteCard_ObtineToken);
      this.RemotingServer.EvenimenteCard.SchimbarePIN += new SchimbarePINEventHandler(this.EvenimenteCard_SchimbarePIN);
      this.RemotingServer.EvenimenteCard.Autentificare += new AutentificareEventHandler(this.EvenimenteCard_Autentificare);
      this.RemotingServer.EvenimenteCard.ResetarePIN += new ResetarePINEventHandler(this.EvenimenteCard_ResetarePIN);
      this.RemotingServer.EvenimenteCard.SchimbarePINTransport += new SchimbarePINTransportEventHandler(this.EvenimenteCard_SchimbarePINTransport);
    }

    private void StopRemotingServer()
    {
      if (!this.UseRemoting)
        return;
      this.RemotingServer.EvenimenteCard.ObtineToken -= new ObtineTokenEventHandler(this.EvenimenteCard_ObtineToken);
      this.RemotingServer.EvenimenteCard.SchimbarePIN -= new SchimbarePINEventHandler(this.EvenimenteCard_SchimbarePIN);
      this.RemotingServer.EvenimenteCard.Autentificare -= new AutentificareEventHandler(this.EvenimenteCard_Autentificare);
      this.RemotingServer.EvenimenteCard.ResetarePIN -= new ResetarePINEventHandler(this.EvenimenteCard_ResetarePIN);
      this.RemotingServer.EvenimenteCard.SchimbarePINTransport -= new SchimbarePINTransportEventHandler(this.EvenimenteCard_SchimbarePINTransport);
      this.RemotingServer.Stop();
    }

    private CoduriRaspunsOperatieCard EvenimenteCard_Autentificare(string pinBlock, ref int retryCounter, string cardNumber, string terminalId, ref bool canResetPIN, string certificateSerialNumber, ref bool needUpdate)
    {
      CoduriRaspunsOperatieCard raspunsOperatieCard = CoduriRaspunsOperatieCard.ERR_AUTENTIFICARE;
      try
      {
        LogManager.AddSeparatorLine();
        LogManager.FileLog("Start Autentificare");
        using (CommunicationProcess process = new CommunicationProcess(new TCPClient(this.EndPoint)))
        {
          raspunsOperatieCard = this.MappResponse(this.SendRequest(ISO8583Util.BuildHandshakeRequest(), process));
          if (raspunsOperatieCard != CoduriRaspunsOperatieCard.OK)
            return raspunsOperatieCard;
          Iso8583Message response = this.SendRequest(ISO8583Util.BuildPINValidationRequest(cardNumber, retryCounter, terminalId, pinBlock, certificateSerialNumber), process);
          if (response[23] != null)
            retryCounter = (int) Convert.ToInt16(response[23]);
          if (response[45] != null)
            canResetPIN = response[45] == "Y";
          if (response[25] != null)
            needUpdate = response[25] == "01";
          raspunsOperatieCard = this.MappResponse(response);
        }
      }
      catch (SocketException ex)
      {
        LogManager.FileLog(string.Format(this.Errors[ErrorCodes.ERR_METHOD_FAILED], (object) Globals.GetCalledMethod()), (Exception) ex);
        raspunsOperatieCard = CoduriRaspunsOperatieCard.ERR_UM_INDISPONIBILA;
      }
      catch (CommunicationException ex)
      {
        LogManager.FileLog(string.Format(this.Errors[ErrorCodes.ERR_METHOD_FAILED], (object) Globals.GetCalledMethod()), (Exception) ex);
        raspunsOperatieCard = ex.ExceptionType != CommunicationExceptionType.RESPONSE_TIMEOUT ? CoduriRaspunsOperatieCard.ERR_AUTENTIFICARE : CoduriRaspunsOperatieCard.ERR_UM_INDISPONIBILA;
      }
      catch (Exception ex)
      {
        LogManager.FileLog(string.Format(this.Errors[ErrorCodes.ERR_METHOD_FAILED], (object) Globals.GetCalledMethod()), ex);
        raspunsOperatieCard = CoduriRaspunsOperatieCard.ERR_AUTENTIFICARE;
      }
      finally
      {
        LogManager.FileLog("Terminat Autentificare");
      }
      return raspunsOperatieCard;
    }

    private string EvenimenteCard_ObtineToken(string cif, IdentificatorDrepturi identificatorDrepturi, string terminalId, ref DateTime serverDateTime, ref CoduriRaspunsOperatieCard raspunsOperatie)
    {
      string str = (string) null;
      try
      {
        LogManager.AddSeparatorLine();
        LogManager.FileLog("Start Obtine Token");
        using (CommunicationProcess process = new CommunicationProcess(new TCPClient(this.EndPoint)))
        {
          Iso8583Message response1 = this.SendRequest(ISO8583Util.BuildHandshakeRequest(), process);
          raspunsOperatie = this.MappResponse(response1);
          if (raspunsOperatie != CoduriRaspunsOperatieCard.OK)
            return (string) null;
          Iso8583Message response2 = this.SendRequest(ISO8583Util.BuildRightsRequest(identificatorDrepturi, terminalId), process);
          raspunsOperatie = this.MappResponse(response2);
          if (raspunsOperatie != CoduriRaspunsOperatieCard.OK)
            return (string) null;
          if (response2[48] != null && response2[48] != "NULL")
            str = response2[48];
          if (response2[7] != null)
            serverDateTime = this.IntoarceServerDateTime(response2[7]);
          raspunsOperatie = CoduriRaspunsOperatieCard.OK;
        }
      }
      catch (SocketException ex)
      {
        LogManager.FileLog(string.Format(this.Errors[ErrorCodes.ERR_METHOD_FAILED], (object) Globals.GetCalledMethod()), (Exception) ex);
        raspunsOperatie = CoduriRaspunsOperatieCard.ERR_UM_INDISPONIBILA;
      }
      catch (CommunicationException ex)
      {
        LogManager.FileLog(string.Format(this.Errors[ErrorCodes.ERR_METHOD_FAILED], (object) Globals.GetCalledMethod()), (Exception) ex);
        raspunsOperatie = ex.ExceptionType != CommunicationExceptionType.RESPONSE_TIMEOUT ? CoduriRaspunsOperatieCard.ERR_UM_TOKEN : CoduriRaspunsOperatieCard.ERR_UM_INDISPONIBILA;
      }
      catch (Exception ex)
      {
        LogManager.FileLog(string.Format(this.Errors[ErrorCodes.ERR_METHOD_FAILED], (object) Globals.GetCalledMethod()), ex);
        raspunsOperatie = CoduriRaspunsOperatieCard.ERR_UM_TOKEN;
      }
      finally
      {
        LogManager.FileLog("Terminat Obtine Token");
      }
      return str;
    }

    private CoduriRaspunsOperatieCard EvenimenteCard_SchimbarePIN(string token, string cardNumber, ref int retryCounter, string terminalId, string pinBlockVechi, string pinBlockNou, string certificateSerialNumber)
    {
      CoduriRaspunsOperatieCard raspunsOperatieCard = CoduriRaspunsOperatieCard.ERR_SCHIMBARE_PIN;
      try
      {
        LogManager.AddSeparatorLine();
        LogManager.FileLog("Start Schimbare PIN");
        using (CommunicationProcess process = new CommunicationProcess(new TCPClient(this.EndPoint)))
        {
          raspunsOperatieCard = this.MappResponse(this.SendRequest(ISO8583Util.BuildHandshakeRequest(), process));
          if (raspunsOperatieCard != CoduriRaspunsOperatieCard.OK)
            return raspunsOperatieCard;
          raspunsOperatieCard = this.MappResponse(this.SendRequest(ISO8583Util.BuildChangePINRequest(pinBlockVechi, pinBlockNou, terminalId, cardNumber, retryCounter, certificateSerialNumber), process));
        }
      }
      catch (SocketException ex)
      {
        LogManager.FileLog(string.Format(this.Errors[ErrorCodes.ERR_METHOD_FAILED], (object) Globals.GetCalledMethod()), (Exception) ex);
        raspunsOperatieCard = CoduriRaspunsOperatieCard.ERR_UM_INDISPONIBILA;
      }
      catch (CommunicationException ex)
      {
        LogManager.FileLog(string.Format(this.Errors[ErrorCodes.ERR_METHOD_FAILED], (object) Globals.GetCalledMethod()), (Exception) ex);
        raspunsOperatieCard = ex.ExceptionType != CommunicationExceptionType.RESPONSE_TIMEOUT ? CoduriRaspunsOperatieCard.ERR_SCHIMBARE_PIN : CoduriRaspunsOperatieCard.ERR_UM_INDISPONIBILA;
      }
      catch (Exception ex)
      {
        LogManager.FileLog(string.Format(this.Errors[ErrorCodes.ERR_METHOD_FAILED], (object) Globals.GetCalledMethod()), ex);
        raspunsOperatieCard = CoduriRaspunsOperatieCard.ERR_SCHIMBARE_PIN;
      }
      finally
      {
        LogManager.FileLog("Terminat Schimbare PIN");
      }
      return raspunsOperatieCard;
    }

    private CoduriRaspunsOperatieCard EvenimenteCard_SchimbarePINTransport(string token, string cardNumber, ref int retryCounter, string terminalId, string pinBlockVechi, string pinBlockNou, string certificat, string certificateSerialNumber)
    {
      CoduriRaspunsOperatieCard raspunsOperatieCard = CoduriRaspunsOperatieCard.ERR_SCHIMBARE_PIN_TRANSPORT;
      try
      {
        LogManager.AddSeparatorLine();
        LogManager.FileLog("Start Schimbare PIN Transport");
        using (CommunicationProcess process = new CommunicationProcess(new TCPClient(this.EndPoint)))
        {
          raspunsOperatieCard = this.MappResponse(this.SendRequest(ISO8583Util.BuildHandshakeRequest(), process));
          if (raspunsOperatieCard != CoduriRaspunsOperatieCard.OK)
            return raspunsOperatieCard;
          raspunsOperatieCard = this.MappResponse(this.SendRequest(ISO8583Util.BuildChangePINTransportRequest(pinBlockVechi, pinBlockNou, terminalId, cardNumber, retryCounter, certificat, certificateSerialNumber), process));
        }
      }
      catch (SocketException ex)
      {
        LogManager.FileLog(string.Format(this.Errors[ErrorCodes.ERR_METHOD_FAILED], (object) Globals.GetCalledMethod()), (Exception) ex);
        raspunsOperatieCard = CoduriRaspunsOperatieCard.ERR_UM_INDISPONIBILA;
      }
      catch (CommunicationException ex)
      {
        LogManager.FileLog(string.Format(this.Errors[ErrorCodes.ERR_METHOD_FAILED], (object) Globals.GetCalledMethod()), (Exception) ex);
        raspunsOperatieCard = ex.ExceptionType != CommunicationExceptionType.RESPONSE_TIMEOUT ? CoduriRaspunsOperatieCard.ERR_SCHIMBARE_PIN_TRANSPORT : CoduriRaspunsOperatieCard.ERR_UM_INDISPONIBILA;
      }
      catch (Exception ex)
      {
        LogManager.FileLog(string.Format(this.Errors[ErrorCodes.ERR_METHOD_FAILED], (object) Globals.GetCalledMethod()), ex);
        raspunsOperatieCard = CoduriRaspunsOperatieCard.ERR_SCHIMBARE_PIN_TRANSPORT;
      }
      finally
      {
        LogManager.FileLog("Terminat Schimbare PIN Transport");
      }
      return raspunsOperatieCard;
    }

    private CoduriRaspunsOperatieCard EvenimenteCard_ResetarePIN(string token, string cardNumber, string terminalId, string pinBlock, string certificateSerialNumber)
    {
      CoduriRaspunsOperatieCard raspunsOperatieCard = CoduriRaspunsOperatieCard.ERR_RESETARE_PIN;
      try
      {
        LogManager.AddSeparatorLine();
        LogManager.FileLog("Start Resetare PIN");
        using (CommunicationProcess process = new CommunicationProcess(new TCPClient(this.EndPoint)))
        {
          raspunsOperatieCard = this.MappResponse(this.SendRequest(ISO8583Util.BuildHandshakeRequest(), process));
          if (raspunsOperatieCard != CoduriRaspunsOperatieCard.OK)
            return raspunsOperatieCard;
          raspunsOperatieCard = this.MappResponse(this.SendRequest(ISO8583Util.BuildResetPINRequest(pinBlock, terminalId, cardNumber, certificateSerialNumber), process));
        }
      }
      catch (SocketException ex)
      {
        LogManager.FileLog(string.Format(this.Errors[ErrorCodes.ERR_METHOD_FAILED], (object) Globals.GetCalledMethod()), (Exception) ex);
        raspunsOperatieCard = CoduriRaspunsOperatieCard.ERR_UM_INDISPONIBILA;
      }
      catch (CommunicationException ex)
      {
        LogManager.FileLog(string.Format(this.Errors[ErrorCodes.ERR_METHOD_FAILED], (object) Globals.GetCalledMethod()), (Exception) ex);
        raspunsOperatieCard = ex.ExceptionType != CommunicationExceptionType.RESPONSE_TIMEOUT ? CoduriRaspunsOperatieCard.ERR_RESETARE_PIN : CoduriRaspunsOperatieCard.ERR_UM_INDISPONIBILA;
      }
      catch (Exception ex)
      {
        LogManager.FileLog(string.Format(this.Errors[ErrorCodes.ERR_METHOD_FAILED], (object) Globals.GetCalledMethod()), ex);
        raspunsOperatieCard = CoduriRaspunsOperatieCard.ERR_RESETARE_PIN;
      }
      finally
      {
        LogManager.FileLog("Terminat Resetare PIN");
      }
      return raspunsOperatieCard;
    }

    private IPAddress GetLocalIPAddress()
    {
      foreach (IPAddress ipAddress in Dns.GetHostEntry(Dns.GetHostName()).AddressList)
      {
        if (ipAddress.AddressFamily == AddressFamily.InterNetwork)
          return ipAddress;
      }
      return (IPAddress) null;
    }

    public DateTime IntoarceServerDateTime(string sDateTime)
    {
      DateTime dateTime = DateTime.Now;
      DateTime result = DateTime.Now;
      if (DateTime.TryParseExact(sDateTime, "MMddHHmmss", (IFormatProvider) CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal, out result))
        dateTime = result.ToLocalTime();
      return dateTime;
    }

    private CoduriRaspunsOperatieCard MappResponse(Iso8583Message response)
    {
      CoduriRaspunsOperatieCard raspunsOperatieCard = CoduriRaspunsOperatieCard.ERR_PROCESARE_RASPUNS_UM;
      int response1 = Iso8583Message.MsgType.GetResponse(response.MessageType);
      string key = ((object) response[39]).ToString();
      if (IsoConvert.FromIntToMsgType(response1) == "0810")
        raspunsOperatieCard = CoduriRaspunsOperatieCard.ERR_HANDSHAKE;
      else if (IsoConvert.FromIntToMsgType(response1) == "0110")
      {
        string str = response[3];
        if (str == ((object) IsoProcessingCode.AUTENTIFICARE).ToString() || str == ((object) IsoProcessingCode.AUTENTIFICARE_ACTIVARE_CARD).ToString())
          raspunsOperatieCard = CoduriRaspunsOperatieCard.ERR_UM_AUTENTIFICARE;
        else if (str == ((object) IsoProcessingCode.CHECK_RIGHTS).ToString())
          raspunsOperatieCard = CoduriRaspunsOperatieCard.ERR_UM_TOKEN;
        else if (str == ((object) IsoProcessingCode.CHANGE_TRANSPORT_PIN).ToString())
          raspunsOperatieCard = CoduriRaspunsOperatieCard.ERR_SCHIMBARE_PIN_TRANSPORT;
        else if (str == ((object) IsoProcessingCode.CHANGE_PIN).ToString())
          raspunsOperatieCard = CoduriRaspunsOperatieCard.ERR_SCHIMBARE_PIN;
        else if (str == ((object) IsoProcessingCode.RESET_PIN).ToString())
          raspunsOperatieCard = CoduriRaspunsOperatieCard.ERR_RESETARE_PIN;
      }
      if (this.UMResponses.ContainsKey(key))
        raspunsOperatieCard = this.UMResponses[key];
      return raspunsOperatieCard;
    }

    public string GetMessageType(Iso8583Message message)
    {
      string str = string.Empty;
      int messageType = message.MessageType;
      if (IsoConvert.FromIntToMsgType(messageType) == "0810" || IsoConvert.FromIntToMsgType(messageType) == "0800")
        return "handshake";
      if (IsoConvert.FromIntToMsgType(messageType) == "0110" || IsoConvert.FromIntToMsgType(messageType) == "0100")
      {
        IsoProcessingCode isoProcessingCode = (IsoProcessingCode) Convert.ToInt32(message[3]);
        if (isoProcessingCode == IsoProcessingCode.AUTENTIFICARE || isoProcessingCode == IsoProcessingCode.AUTENTIFICARE_ACTIVARE_CARD)
          return "autentificare";
        if (isoProcessingCode == IsoProcessingCode.CHECK_RIGHTS)
          return "verificare drepturi";
        if (isoProcessingCode == IsoProcessingCode.CHANGE_TRANSPORT_PIN || isoProcessingCode == IsoProcessingCode.CHANGE_PIN)
          return "schimbare pin";
        if (isoProcessingCode == IsoProcessingCode.RESET_PIN)
          return "resetare pin";
      }
      return str;
    }

    public delegate void ProcessApplicationEventDelegate(string message);
  }
}
