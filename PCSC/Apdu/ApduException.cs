// Decompiled with JetBrains decompiler
// Type: Novensys.eCard.SDK.PCSC.Apdu.ApduException
// Assembly: Novensys.eCard.SDK, Version=1.1.56.0, Culture=neutral, PublicKeyToken=null
// MVID: 59F9E456-D3DA-4066-A4A4-692B516A775E
// Assembly location: C:\Program Files (x86)\CNAS\SIUI-SPITAL\Novensys.eCard.SDK.dll

using System;

namespace Novensys.eCard.SDK.PCSC.Apdu
{
  public class ApduException : ApplicationException
  {
    private ushort expectedSW1SW2;
    private string message;
    private int readerRetValue;
    private ushort? responseSW1SW2;

    public ushort ExpectedSW1SW2
    {
      get
      {
        return this.expectedSW1SW2;
      }
    }

    public override string Message
    {
      get
      {
        return this.message;
      }
    }

    public int ReaderRetValue
    {
      get
      {
        return this.readerRetValue;
      }
    }

    public ushort? ResponseSW1SW2
    {
      get
      {
        return this.responseSW1SW2;
      }
    }

    public int MappingCode { get; private set; }

    public ApduException(bool enableTrace, int readerRetValue, ushort expectedSW1SW2, ushort? responseSW1SW2)
    {
      this.readerRetValue = readerRetValue;
      this.expectedSW1SW2 = expectedSW1SW2;
      this.responseSW1SW2 = responseSW1SW2;
      if (readerRetValue == 0)
      {
        this.message = "Eroare: Raspuns SW1SW2 invalid!";
        this.message = this.message + Environment.NewLine;
        ushort? nullable = responseSW1SW2;
        this.message = !(nullable.HasValue ? new int?((int) nullable.GetValueOrDefault()) : new int?()).HasValue ? this.message + "Raspuns SW1SW2: nimic" : this.message + string.Format("Raspuns SW1SW2: 0x{0:X04} ", (object) responseSW1SW2);
        this.message = this.message + Environment.NewLine;
        this.message = this.message + string.Format("Raspuns asteptat SW1SW2: 0x{0:X04} ", (object) expectedSW1SW2);
      }
      else
        this.message = string.Format("Eroare terminal: 0x{0:X04}", (object) readerRetValue);
      this.MappingCode = -34;
    }
  }
}
