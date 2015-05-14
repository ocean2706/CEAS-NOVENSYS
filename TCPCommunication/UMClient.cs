// Decompiled with JetBrains decompiler
// Type: Novensys.eCard.SDK.TCPCommunication.UMClient
// Assembly: Novensys.eCard.SDK, Version=1.1.56.0, Culture=neutral, PublicKeyToken=null
// MVID: 59F9E456-D3DA-4066-A4A4-692B516A775E
// Assembly location: C:\Program Files (x86)\CNAS\SIUI-SPITAL\Novensys.eCard.SDK.dll

using Novensys.eCard.SDK;
using Novensys.eCard.SDK.Entities;
using Novensys.eCard.SDK.ISO8583;
using Novensys.eCard.SDK.Terminal;
using Novensys.eCard.SDK.Utils.Log;
using System;
using System.Globalization;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Novensys.eCard.SDK.TCPCommunication
{
  internal class UMClient
  {
    private Errors Errors = new Errors();
    private Informations Informations = new Informations();
    private Warnings Warnings = new Warnings();
    private UMResponses UMResponses = new UMResponses();
    private IPEndPoint EndPoint;

    public UMClient()
    {
      try
      {
        this.EndPoint = IPAddressUM.Address;
      }
      catch (Exception ex)
      {
        LogManager.FileLog(ex);
        throw;
      }
    }

    public void ExecutaObtineTerminalData(TerminalDataIdentifier id, ref CoduriRaspunsOperatieCard raspunsOperatie, out string terminalId, out string fileContent, out string terminalMasterKey)
    {
      terminalId = (string) null;
      fileContent = (string) null;
      terminalMasterKey = (string) null;
      try
      {
        LogManager.AddSeparatorLine();
        LogManager.FileLog("Start Obtine Terminal Data");
        using (CommunicationProcess process = new CommunicationProcess(new TCPClient(this.EndPoint)))
        {
          Iso8583Message response1 = this.SendRequest(ISO8583Util.BuildHandshakeRequest(), process);
          raspunsOperatie = this.MappResponse(response1);
          if (raspunsOperatie != CoduriRaspunsOperatieCard.OK)
            return;
          Iso8583Message response2 = this.SendRequest(ISO8583Util.BuildInrolareMessage(id), process);
          raspunsOperatie = this.MappResponse(response2);
          if (raspunsOperatie != CoduriRaspunsOperatieCard.OK)
            return;
          terminalId = response2[41];
          fileContent = response2[61];
          terminalMasterKey = response2[59];
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
        raspunsOperatie = ex.ExceptionType != CommunicationExceptionType.RESPONSE_TIMEOUT ? CoduriRaspunsOperatieCard.ERR_UM_TERMINAL_DATA : CoduriRaspunsOperatieCard.ERR_UM_INDISPONIBILA;
      }
      catch (Exception ex)
      {
        LogManager.FileLog(string.Format(this.Errors[ErrorCodes.ERR_METHOD_FAILED], (object) Globals.GetCalledMethod()), ex);
        raspunsOperatie = CoduriRaspunsOperatieCard.ERR_UM_TERMINAL_DATA;
      }
      finally
      {
        LogManager.FileLog("Terminat Obtine Terminal Data");
      }
    }

    public string ExecutaObtineToken(string cif, IdentificatorDrepturi identificatorDrepturi, string terminalId, ref DateTime serverDateTime, ref CoduriRaspunsOperatieCard raspunsOperatie)
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

    public CoduriRaspunsOperatieCard ExecutaSchimbarePIN(string token, string cardNumber, ref int retryCounter, string terminalId, string pinBlockVechi, string pinBlockNou, string certificateSerialNumber)
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

    public CoduriRaspunsOperatieCard ExecutaSchimbarePINTransport(string token, string cardNumber, ref int retryCounter, string terminalId, string pinBlockVechi, string pinBlockNou, string certificat, string certificateSerialNumber)
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

    public CoduriRaspunsOperatieCard ExecutaAutentificare(string pinBlock, ref int retryCounter, string cardNumber, string terminalId, ref bool canResetPIN, string certificateSerialNumber, ref bool needUpdate)
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

    public CoduriRaspunsOperatieCard ExecutaResetarePIN(string token, string cardNumber, string terminalId, string pinBlock, string certificateSerialNumber)
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

    private Iso8583Message SendRequest(Iso8583Message requestMsg, CommunicationProcess process)
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

    private bool IsValidResponse(Iso8583Message requestMsg, Iso8583Message responseMsg)
    {
      bool flag = false;
      if (responseMsg == null)
        return false;
      if (responseMsg[11] != null)
        flag = responseMsg[11] == requestMsg[11];
      return flag;
    }

    private DateTime IntoarceServerDateTime(string sDateTime)
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
  }
}
