// Decompiled with JetBrains decompiler
// Type: Novensys.eCard.SDK.ISO8583.ProcessingCode
// Assembly: Novensys.eCard.SDK, Version=1.1.56.0, Culture=neutral, PublicKeyToken=null
// MVID: 59F9E456-D3DA-4066-A4A4-692B516A775E
// Assembly location: C:\Program Files (x86)\CNAS\SIUI-SPITAL\Novensys.eCard.SDK.dll

using System;

namespace Novensys.eCard.SDK.ISO8583
{
  public class ProcessingCode
  {
    public string TranType { get; set; }

    public string FromAccountType { get; set; }

    public string ToAccountType { get; set; }

    public ProcessingCode(string data)
    {
      if (data.Length != 6)
        throw new ArgumentException("Incorrect length for data", "data");
      this.TranType = data.Substring(0, 2);
      this.FromAccountType = data.Substring(2, 2);
      this.ToAccountType = data.Substring(4, 2);
    }
  }
}
