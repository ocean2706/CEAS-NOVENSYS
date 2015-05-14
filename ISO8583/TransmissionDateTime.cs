// Decompiled with JetBrains decompiler
// Type: Novensys.eCard.SDK.ISO8583.TransmissionDateTime
// Assembly: Novensys.eCard.SDK, Version=1.1.56.0, Culture=neutral, PublicKeyToken=null
// MVID: 59F9E456-D3DA-4066-A4A4-692B516A775E
// Assembly location: C:\Program Files (x86)\CNAS\SIUI-SPITAL\Novensys.eCard.SDK.dll

using System;

namespace Novensys.eCard.SDK.ISO8583
{
  public class TransmissionDateTime
  {
    private readonly AMessage _message;

    public TransmissionDateTime(AMessage message)
    {
      this._message = message;
    }

    public void SetNow()
    {
      AMessage amessage = this._message;
      int index = 7;
      DateTime dateTime = DateTime.Now;
      dateTime = dateTime.ToUniversalTime();
      string str = dateTime.ToString("MMddHHmmss");
      amessage[index] = str;
    }
  }
}
