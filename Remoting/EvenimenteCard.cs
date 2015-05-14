// Decompiled with JetBrains decompiler
// Type: Novensys.eCard.SDK.Remoting.EvenimenteCard
// Assembly: Novensys.eCard.SDK, Version=1.1.56.0, Culture=neutral, PublicKeyToken=null
// MVID: 59F9E456-D3DA-4066-A4A4-692B516A775E
// Assembly location: C:\Program Files (x86)\CNAS\SIUI-SPITAL\Novensys.eCard.SDK.dll

using Novensys.eCard.SDK;
using Novensys.eCard.SDK.Entities;
using System;
using System.Runtime.Remoting;

namespace Novensys.eCard.SDK.Remoting
{
  public class EvenimenteCard : MarshalByRefObject
  {
    public ObjRef InternalRef { get; set; }

    public event ObtineTokenEventHandler ObtineToken;

    public event SchimbarePINEventHandler SchimbarePIN;

    public event SchimbarePINTransportEventHandler SchimbarePINTransport;

    public event ResetarePINEventHandler ResetarePIN;

    public event AutentificareEventHandler Autentificare;

    public event SchimbareStareCardEventHandler StareCardSchimbata;

    public string ExecutaObtineToken(string cif, IdentificatorDrepturi identificatorDrepturi, string terminalId, ref DateTime serverDateTime, ref CoduriRaspunsOperatieCard raspunsOperatie)
    {
      return this.InvocaObtineToken(cif, identificatorDrepturi, terminalId, ref serverDateTime, ref raspunsOperatie);
    }

    public CoduriRaspunsOperatieCard ExecutaSchimbarePIN(string token, string cardNumber, ref int retryCounter, string terminalId, string pinBlockVechi, string pinBlockNou, string certificateSerialNumber)
    {
      return this.InvocaSchimbarePIN(token, cardNumber, ref retryCounter, terminalId, pinBlockVechi, pinBlockNou, certificateSerialNumber);
    }

    public CoduriRaspunsOperatieCard ExecutaSchimbarePINTransport(string token, string cardNumber, ref int retryCounter, string terminalId, string pinBlockVechi, string pinBlockNou, string certificat, string certificateSerialNumber)
    {
      return this.InvocaSchimbarePINTransport(token, cardNumber, ref retryCounter, terminalId, pinBlockVechi, pinBlockNou, certificat, certificateSerialNumber);
    }

    public CoduriRaspunsOperatieCard ExecutaResetarePIN(string token, string cardNumber, string terminalId, string pinBlock, string certificateSerialNumber)
    {
      return this.InvocaResetarePIN(token, cardNumber, terminalId, pinBlock, certificateSerialNumber);
    }

    public CoduriRaspunsOperatieCard ExecutaAutentificare(string pinBlock, ref int retryCounter, string cardNumber, string terminalId, ref bool canResetPIN, string certificateSerialNumber, ref bool needUpdate)
    {
      return this.InvocaAutentificare(pinBlock, ref retryCounter, cardNumber, terminalId, ref canResetPIN, certificateSerialNumber, ref needUpdate);
    }

    private CoduriRaspunsOperatieCard InvocaAutentificare(string pinBlock, ref int retryCounter, string cardNumber, string terminalId, ref bool canResetPIN, string certificateSerialNumber, ref bool needUpdate)
    {
      CoduriRaspunsOperatieCard raspunsOperatieCard = CoduriRaspunsOperatieCard.OK;
      if (this.Autentificare == null)
        return CoduriRaspunsOperatieCard.ERR_COM_SERVICE;
      AutentificareEventHandler autentificareEventHandler = (AutentificareEventHandler) null;
      foreach (Delegate @delegate in this.Autentificare.GetInvocationList())
      {
        try
        {
          autentificareEventHandler = (AutentificareEventHandler) @delegate;
          raspunsOperatieCard = autentificareEventHandler(pinBlock, ref retryCounter, cardNumber, terminalId, ref canResetPIN, certificateSerialNumber, ref needUpdate);
        }
        catch (Exception ex)
        {
          this.Autentificare -= autentificareEventHandler;
          throw ex;
        }
      }
      return raspunsOperatieCard;
    }

    private string InvocaObtineToken(string cif, IdentificatorDrepturi identificatorDrepturi, string terminalId, ref DateTime serverDateTime, ref CoduriRaspunsOperatieCard raspunsOperatie)
    {
      string str = (string) null;
      if (this.ObtineToken == null)
        return (string) null;
      ObtineTokenEventHandler tokenEventHandler = (ObtineTokenEventHandler) null;
      foreach (Delegate @delegate in this.ObtineToken.GetInvocationList())
      {
        try
        {
          tokenEventHandler = (ObtineTokenEventHandler) @delegate;
          str = tokenEventHandler(cif, identificatorDrepturi, terminalId, ref serverDateTime, ref raspunsOperatie);
        }
        catch (Exception ex)
        {
          this.ObtineToken -= tokenEventHandler;
          throw ex;
        }
      }
      return str;
    }

    private CoduriRaspunsOperatieCard InvocaSchimbarePIN(string token, string cardNumber, ref int retryCounter, string terminalId, string pinBlockVechi, string pinBlockNou, string certificateSerialNumber)
    {
      CoduriRaspunsOperatieCard raspunsOperatieCard = CoduriRaspunsOperatieCard.OK;
      if (this.SchimbarePIN == null)
        return CoduriRaspunsOperatieCard.ERR_COM_SERVICE;
      SchimbarePINEventHandler schimbarePinEventHandler = (SchimbarePINEventHandler) null;
      foreach (Delegate @delegate in this.SchimbarePIN.GetInvocationList())
      {
        try
        {
          schimbarePinEventHandler = (SchimbarePINEventHandler) @delegate;
          raspunsOperatieCard = schimbarePinEventHandler(token, cardNumber, ref retryCounter, terminalId, pinBlockVechi, pinBlockNou, certificateSerialNumber);
        }
        catch (Exception ex)
        {
          this.SchimbarePIN -= schimbarePinEventHandler;
          throw ex;
        }
      }
      return raspunsOperatieCard;
    }

    private CoduriRaspunsOperatieCard InvocaSchimbarePINTransport(string token, string cardNumber, ref int retryCounter, string terminalId, string pinBlockVechi, string pinBlockNou, string certificat, string certificateSerialNumber)
    {
      CoduriRaspunsOperatieCard raspunsOperatieCard = CoduriRaspunsOperatieCard.OK;
      if (this.SchimbarePINTransport == null)
        return CoduriRaspunsOperatieCard.ERR_COM_SERVICE;
      SchimbarePINTransportEventHandler transportEventHandler = (SchimbarePINTransportEventHandler) null;
      foreach (Delegate @delegate in this.SchimbarePINTransport.GetInvocationList())
      {
        try
        {
          transportEventHandler = (SchimbarePINTransportEventHandler) @delegate;
          raspunsOperatieCard = transportEventHandler(token, cardNumber, ref retryCounter, terminalId, pinBlockVechi, pinBlockNou, certificat, certificateSerialNumber);
        }
        catch (Exception ex)
        {
          this.SchimbarePINTransport -= transportEventHandler;
          throw ex;
        }
      }
      return raspunsOperatieCard;
    }

    private CoduriRaspunsOperatieCard InvocaResetarePIN(string token, string cardNumber, string terminalId, string pinBlock, string certificateSerialNumber)
    {
      CoduriRaspunsOperatieCard raspunsOperatieCard = CoduriRaspunsOperatieCard.OK;
      if (this.ResetarePIN == null)
        return CoduriRaspunsOperatieCard.ERR_COM_SERVICE;
      ResetarePINEventHandler resetarePinEventHandler = (ResetarePINEventHandler) null;
      foreach (Delegate @delegate in this.ResetarePIN.GetInvocationList())
      {
        try
        {
          resetarePinEventHandler = (ResetarePINEventHandler) @delegate;
          raspunsOperatieCard = resetarePinEventHandler(token, cardNumber, terminalId, pinBlock, certificateSerialNumber);
        }
        catch (Exception ex)
        {
          this.ResetarePIN -= resetarePinEventHandler;
          throw ex;
        }
      }
      return raspunsOperatieCard;
    }

    public void StareCardEsteSchimbata(StareCardInTerminal statusCardInTerminal)
    {
      SchimbareStareCardEventArgs e = new SchimbareStareCardEventArgs(statusCardInTerminal);
      if (this.StareCardSchimbata == null)
        return;
      this.StareCardSchimbata((object) this, e);
    }

    public override object InitializeLifetimeService()
    {
      return (object) null;
    }
  }
}
