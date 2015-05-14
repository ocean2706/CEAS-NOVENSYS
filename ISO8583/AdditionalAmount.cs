// Decompiled with JetBrains decompiler
// Type: Novensys.eCard.SDK.ISO8583.AdditionalAmount
// Assembly: Novensys.eCard.SDK, Version=1.1.56.0, Culture=neutral, PublicKeyToken=null
// MVID: 59F9E456-D3DA-4066-A4A4-692B516A775E
// Assembly location: C:\Program Files (x86)\CNAS\SIUI-SPITAL\Novensys.eCard.SDK.dll

using System;

namespace Novensys.eCard.SDK.ISO8583
{
  public class AdditionalAmount
  {
    private string _amount;

    public string AccountType { get; set; }

    public string AmountType { get; set; }

    public string CurrencyCode { get; set; }

    public string Sign { get; set; }

    public string Amount
    {
      get
      {
        return this._amount;
      }
      set
      {
        this._amount = value.PadLeft(12, '0');
      }
    }

    public long Value
    {
      get
      {
        if (this.Sign == null || this.Amount == null)
          return 0L;
        long num = long.Parse(this.Amount);
        if (this.Sign == "D")
          return -num;
        else
          return num;
      }
      set
      {
        this.Sign = value < 0L ? "D" : "C";
        this.Amount = (value < 0L ? -value : value).ToString();
      }
    }

    public AdditionalAmount()
    {
    }

    public AdditionalAmount(string value)
    {
      if (value.Length != 20)
        throw new ArgumentException("value incorrect length", "value");
      this.AccountType = value.Substring(0, 2);
      this.AmountType = value.Substring(2, 2);
      this.CurrencyCode = value.Substring(4, 3);
      this.Sign = value.Substring(7, 1);
      this.Amount = value.Substring(8);
    }

    public override string ToString()
    {
      return this.AccountType + this.AmountType + this.CurrencyCode + this.Sign + this.Amount;
    }
  }
}
