// Decompiled with JetBrains decompiler
// Type: Novensys.eCard.SDK.PCSC.WinSCardException
// Assembly: Novensys.eCard.SDK, Version=1.1.56.0, Culture=neutral, PublicKeyToken=null
// MVID: 59F9E456-D3DA-4066-A4A4-692B516A775E
// Assembly location: C:\Program Files (x86)\CNAS\SIUI-SPITAL\Novensys.eCard.SDK.dll

using System;

namespace Novensys.eCard.SDK.PCSC
{
  public class WinSCardException : ApplicationException
  {
    private string message;
    private uint status;
    private string functionName;

    public override string Message
    {
      get
      {
        return this.message;
      }
    }

    public uint Status
    {
      get
      {
        return this.status;
      }
    }

    public string WinSCardFunctionName
    {
      get
      {
        return this.functionName;
      }
    }

    public WinSCardException(bool enableTrace, string winSCardfunctionName, uint status)
    {
      this.functionName = winSCardfunctionName;
      this.status = status;
      if ((int) this.status == -2146435071)
      {
        WinSCardException winScardException = this;
        string str = winScardException.message + "An internal consistency check failed!";
        winScardException.message = str;
      }
      else if ((int) this.status == -2146435070)
      {
        WinSCardException winScardException = this;
        string str = winScardException.message + "The action was cancelled by an SCardCancel request!";
        winScardException.message = str;
      }
      else if ((int) this.status == -2146435069)
      {
        WinSCardException winScardException = this;
        string str = winScardException.message + "The supplied handle was invalid!";
        winScardException.message = str;
      }
      else if ((int) this.status == -2146435068)
      {
        WinSCardException winScardException = this;
        string str = winScardException.message + "One or more of the supplied parameters could not be properly interpreted!";
        winScardException.message = str;
      }
      else if ((int) this.status == -2146435067)
      {
        WinSCardException winScardException = this;
        string str = winScardException.message + "Registry startup information is missing or invalid!";
        winScardException.message = str;
      }
      else if ((int) this.status == -2146435066)
      {
        WinSCardException winScardException = this;
        string str = winScardException.message + "Not enough memory available to complete this command!";
        winScardException.message = str;
      }
      else if ((int) this.status == -2146435065)
      {
        WinSCardException winScardException = this;
        string str = winScardException.message + "An internal consistency timer has expired!";
        winScardException.message = str;
      }
      else if ((int) this.status == -2146435064)
      {
        WinSCardException winScardException = this;
        string str = winScardException.message + "The data buffer to receive returned data is too small for the returned data!";
        winScardException.message = str;
      }
      else if ((int) this.status == -2146435063)
      {
        WinSCardException winScardException = this;
        string str = winScardException.message + "The specified reader name is not recognized!";
        winScardException.message = str;
      }
      else if ((int) this.status == -2146435062)
      {
        WinSCardException winScardException = this;
        string str = winScardException.message + "The user-specified timeout value has expired!";
        winScardException.message = str;
      }
      else if ((int) this.status == -2146435061)
      {
        WinSCardException winScardException = this;
        string str = winScardException.message + "The smart card cannot be accessed because of other connections outstanding!";
        winScardException.message = str;
      }
      else if ((int) this.status == -2146435060)
      {
        WinSCardException winScardException = this;
        string str = winScardException.message + "The operation requires a Smart Card, but no Smart Card is currently in the device!";
        winScardException.message = str;
      }
      else if ((int) this.status == -2146435059)
      {
        WinSCardException winScardException = this;
        string str = winScardException.message + "The specified smart card name is not recognized!";
        winScardException.message = str;
      }
      else if ((int) this.status == -2146435058)
      {
        WinSCardException winScardException = this;
        string str = winScardException.message + "The system could not dispose of the media in the requested manner!";
        winScardException.message = str;
      }
      else if ((int) this.status == -2146435057)
      {
        WinSCardException winScardException = this;
        string str = winScardException.message + "The requested protocols are incompatible with the protocol currently in use with the smart card!";
        winScardException.message = str;
      }
      else if ((int) this.status == -2146435056)
      {
        WinSCardException winScardException = this;
        string str = winScardException.message + "The reader or smart card is not ready to accept commands!";
        winScardException.message = str;
      }
      else if ((int) this.status == -2146435055)
      {
        WinSCardException winScardException = this;
        string str = winScardException.message + "One or more of the supplied parameters values could not be properly interpreted!";
        winScardException.message = str;
      }
      else if ((int) this.status == -2146435054)
      {
        WinSCardException winScardException = this;
        string str = winScardException.message + "The action was cancelled by the system, presumably to log off or shut down!";
        winScardException.message = str;
      }
      else if ((int) this.status == -2146435053)
      {
        WinSCardException winScardException = this;
        string str = winScardException.message + "An internal communications error has been detected!";
        winScardException.message = str;
      }
      else if ((int) this.status == -2146435052)
      {
        WinSCardException winScardException = this;
        string str = winScardException.message + "An internal error has been detected, but the source is unknown!";
        winScardException.message = str;
      }
      else if ((int) this.status == -2146435051)
      {
        WinSCardException winScardException = this;
        string str = winScardException.message + "An ATR obtained from the registry is not a valid ATR string!";
        winScardException.message = str;
      }
      else if ((int) this.status == -2146435050)
      {
        WinSCardException winScardException = this;
        string str = winScardException.message + "An attempt was made to end a non-existent transaction!";
        winScardException.message = str;
      }
      else if ((int) this.status == -2146435049)
      {
        WinSCardException winScardException = this;
        string str = winScardException.message + "The specified reader is not currently available for use!";
        winScardException.message = str;
      }
      else if ((int) this.status == -2146435048)
      {
        WinSCardException winScardException = this;
        string str = winScardException.message + "The operation has been aborted to allow the server application to exit!";
        winScardException.message = str;
      }
      else if ((int) this.status == -2146435047)
      {
        WinSCardException winScardException = this;
        string str = winScardException.message + "The PCI Receive buffer was too small!";
        winScardException.message = str;
      }
      else if ((int) this.status == -2146435046)
      {
        WinSCardException winScardException = this;
        string str = winScardException.message + "The reader driver does not meet minimal requirements for support!";
        winScardException.message = str;
      }
      else if ((int) this.status == -2146435045)
      {
        WinSCardException winScardException = this;
        string str = winScardException.message + "The reader driver did not produce a unique reader name!";
        winScardException.message = str;
      }
      else if ((int) this.status == -2146435044)
      {
        WinSCardException winScardException = this;
        string str = winScardException.message + "The smart card does not meet minimal requirements for support!";
        winScardException.message = str;
      }
      else if ((int) this.status == -2146435043)
      {
        WinSCardException winScardException = this;
        string str = winScardException.message + "The Smart card resource manager is not running!";
        winScardException.message = str;
      }
      else if ((int) this.status == -2146435042)
      {
        WinSCardException winScardException = this;
        string str = winScardException.message + "The Smart card resource manager has shut down!";
        winScardException.message = str;
      }
      else if ((int) this.status == -2146435041)
      {
        WinSCardException winScardException = this;
        string str = winScardException.message + "An unexpected card error has occurred!";
        winScardException.message = str;
      }
      else if ((int) this.status == -2146435040)
      {
        WinSCardException winScardException = this;
        string str = winScardException.message + "No Primary Provider can be found for the smart card!";
        winScardException.message = str;
      }
      else if ((int) this.status == -2146435039)
      {
        WinSCardException winScardException = this;
        string str = winScardException.message + "The requested order of object creation is not supported!";
        winScardException.message = str;
      }
      else if ((int) this.status == -2146435038)
      {
        WinSCardException winScardException = this;
        string str = winScardException.message + "This smart card does not support the requested feature!";
        winScardException.message = str;
      }
      else if ((int) this.status == -2146435037)
      {
        WinSCardException winScardException = this;
        string str = winScardException.message + "The identified directory does not exist in the smart card!";
        winScardException.message = str;
      }
      else if ((int) this.status == -2146435036)
      {
        WinSCardException winScardException = this;
        string str = winScardException.message + "The identified file does not exist in the smart card!";
        winScardException.message = str;
      }
      else if ((int) this.status == -2146435035)
      {
        WinSCardException winScardException = this;
        string str = winScardException.message + "The supplied path does not represent a smart card directory!";
        winScardException.message = str;
      }
      else if ((int) this.status == -2146435034)
      {
        WinSCardException winScardException = this;
        string str = winScardException.message + "The supplied path does not represent a smart card file!";
        winScardException.message = str;
      }
      else if ((int) this.status == -2146435033)
      {
        WinSCardException winScardException = this;
        string str = winScardException.message + "Access is denied to this file!";
        winScardException.message = str;
      }
      else if ((int) this.status == -2146435032)
      {
        WinSCardException winScardException = this;
        string str = winScardException.message + "The smartcard does not have enough memory to store the information!";
        winScardException.message = str;
      }
      else if ((int) this.status == -2146435031)
      {
        WinSCardException winScardException = this;
        string str = winScardException.message + "There was an error trying to set the smart card file object pointer!";
        winScardException.message = str;
      }
      else if ((int) this.status == -2146435030)
      {
        WinSCardException winScardException = this;
        string str = winScardException.message + "The supplied PIN is incorrect!";
        winScardException.message = str;
      }
      else if ((int) this.status == -2146435029)
      {
        WinSCardException winScardException = this;
        string str = winScardException.message + "An unrecognized error code was returned from a layered component!";
        winScardException.message = str;
      }
      else if ((int) this.status == -2146435028)
      {
        WinSCardException winScardException = this;
        string str = winScardException.message + "The requested certificate does not exist!";
        winScardException.message = str;
      }
      else if ((int) this.status == -2146435027)
      {
        WinSCardException winScardException = this;
        string str = winScardException.message + "The requested certificate could not be obtained!";
        winScardException.message = str;
      }
      else if ((int) this.status == -2146435026)
      {
        WinSCardException winScardException = this;
        string str = winScardException.message + "Cannot find a smart card reader!";
        winScardException.message = str;
      }
      else if ((int) this.status == -2146435025)
      {
        WinSCardException winScardException = this;
        string str = winScardException.message + "A communications error with the smart card has been detected. Retry the operation!";
        winScardException.message = str;
      }
      else if ((int) this.status == -2146435024)
      {
        WinSCardException winScardException = this;
        string str = winScardException.message + "The requested key container does not exist on the smart card!";
        winScardException.message = str;
      }
      else if ((int) this.status == -2146435023)
      {
        WinSCardException winScardException = this;
        string str = winScardException.message + "The Smart card resource manager is too busy to complete this operation!";
        winScardException.message = str;
      }
      else if ((int) this.status == -2146434971)
      {
        WinSCardException winScardException = this;
        string str = winScardException.message + "The reader cannot communicate with the smart card, due to ATR configuration conflicts!";
        winScardException.message = str;
      }
      else if ((int) this.status == -2146434970)
      {
        WinSCardException winScardException = this;
        string str = winScardException.message + "The smart card is not responding to a reset!";
        winScardException.message = str;
      }
      else if ((int) this.status == -2146434969)
      {
        WinSCardException winScardException = this;
        string str = winScardException.message + "Power has been removed from the smart card, so that further communication is not possible!";
        winScardException.message = str;
      }
      else if ((int) this.status == -2146434968)
      {
        WinSCardException winScardException = this;
        string str = winScardException.message + "The smart card has been reset, so any shared state information is invalid!";
        winScardException.message = str;
      }
      else if ((int) this.status == -2146434967)
      {
        WinSCardException winScardException = this;
        string str = winScardException.message + "The smart card has been removed, so that further communication is not possible!";
        winScardException.message = str;
      }
      else if ((int) this.status == -2146434966)
      {
        WinSCardException winScardException = this;
        string str = winScardException.message + "Access was denied because of a security violation!";
        winScardException.message = str;
      }
      else if ((int) this.status == -2146434965)
      {
        WinSCardException winScardException = this;
        string str = winScardException.message + "The card cannot be accessed because the wrong PIN was presented!";
        winScardException.message = str;
      }
      else if ((int) this.status == -2146434964)
      {
        WinSCardException winScardException = this;
        string str = winScardException.message + "The card cannot be accessed because the maximum number of PIN entry attempts has been reached!";
        winScardException.message = str;
      }
      else if ((int) this.status == -2146434963)
      {
        WinSCardException winScardException = this;
        string str = winScardException.message + "The end of the smart card file has been reached!";
        winScardException.message = str;
      }
      else if ((int) this.status == -2146434962)
      {
        WinSCardException winScardException = this;
        string str = winScardException.message + "The action was cancelled by the user!";
        winScardException.message = str;
      }
      else if ((int) this.status == -2146434961)
      {
        WinSCardException winScardException = this;
        string str = winScardException.message + "No PIN was presented to the smart card!";
        winScardException.message = str;
      }
      else if ((int) this.status == -2146434960)
      {
        WinSCardException winScardException = this;
        string str = winScardException.message + "The requested item could not be found in the cache!";
        winScardException.message = str;
      }
      else if ((int) this.status == -2146434959)
      {
        WinSCardException winScardException = this;
        string str = winScardException.message + "The requested cache item is too old and was deleted from the cache!";
        winScardException.message = str;
      }
      else if ((int) this.status == -2146434959)
      {
        WinSCardException winScardException = this;
        string str = winScardException.message + "The requested cache item is too old and was deleted from the cache!";
        winScardException.message = str;
      }
      else if ((int) this.status == -2146434958)
      {
        WinSCardException winScardException = this;
        string str = winScardException.message + "The new cache item exceeds the maximum per-item size defined for the cache!";
        winScardException.message = str;
      }
      else
      {
        WinSCardException winScardException = this;
        string str = winScardException.message + "Unknown error.";
        winScardException.message = str;
      }
    }
  }
}
