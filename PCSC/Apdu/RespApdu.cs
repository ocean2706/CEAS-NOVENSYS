// Decompiled with JetBrains decompiler
// Type: Novensys.eCard.SDK.PCSC.Apdu.RespApdu
// Assembly: Novensys.eCard.SDK, Version=1.1.56.0, Culture=neutral, PublicKeyToken=null
// MVID: 59F9E456-D3DA-4066-A4A4-692B516A775E
// Assembly location: C:\Program Files (x86)\CNAS\SIUI-SPITAL\Novensys.eCard.SDK.dll

using Novensys.eCard.SDK.Utils.Hex;
using System;
using System.Text;

namespace Novensys.eCard.SDK.PCSC.Apdu
{
  public class RespApdu : IRespApdu
  {
    public const int SW_LENGTH = 2;
    private int readerStatus;
    private byte[] respApdu;
    private byte[] respData;
    private int respLength;
    private byte? sw1;
    private ushort? sw1sw2;
    private byte? sw2;

    public byte[] Data
    {
      get
      {
        return this.respData;
      }
    }

    public int ReaderStatus
    {
      get
      {
        return this.readerStatus;
      }
    }

    public int RespLength
    {
      get
      {
        return this.respLength;
      }
    }

    public byte? SW1
    {
      get
      {
        return this.sw1;
      }
    }

    public ushort? SW1SW2
    {
      get
      {
        return this.sw1sw2;
      }
    }

    public byte? SW2
    {
      get
      {
        return this.sw2;
      }
      set
      {
        this.sw2 = value;
      }
    }

    public bool IsValid
    {
      get
      {
        return HexFormatting.ToHexString((uint) this.SW1SW2.Value) == "9000";
      }
    }

    public bool ContainsData
    {
      get
      {
        return ApduUtil.ContainsData(this.Data);
      }
    }

    public RespApdu(byte[] respApdu)
      : this(respApdu, respApdu.Length)
    {
    }

    public RespApdu(string respApdu)
      : this(HexEncoding.GetBytes(respApdu), HexEncoding.GetBytes(respApdu).Length)
    {
    }

    public RespApdu(int readStatus, byte[] respApdu)
      : this(readStatus, respApdu, respApdu.Length)
    {
    }

    public RespApdu(int readStatus, string respApdu)
      : this(readStatus, HexEncoding.GetBytes(respApdu), HexEncoding.GetBytes(respApdu).Length)
    {
    }

    public RespApdu(byte[] respApdu, int length)
    {
      this.respApdu = (byte[]) null;
      this.respLength = 0;
      this.respData = (byte[]) null;
      this.sw1 = new byte?();
      this.sw2 = new byte?();
      this.sw1sw2 = new ushort?();
      this.readerStatus = 0;
      if (respApdu == null || length < 2)
        return;
      this.respApdu = respApdu;
      this.respLength = length;
      if (this.respLength > 2)
      {
        this.respData = new byte[this.respLength - 2];
        for (int index = 0; index < this.respLength - 2; ++index)
          this.respData[index] = respApdu[index];
      }
      this.sw1 = new byte?(respApdu[this.respLength - 2]);
      this.sw2 = new byte?(respApdu[this.respLength - 1]);
      byte? nullable1 = this.sw1;
      int? nullable2 = nullable1.HasValue ? new int?((int) nullable1.GetValueOrDefault() << 8) : new int?();
      nullable1 = this.sw2;
      this.sw1sw2 = new ushort?((ushort) (nullable2.HasValue & nullable1.HasValue ? new int?(nullable2.GetValueOrDefault() | (int) nullable1.GetValueOrDefault()) : new int?()).Value);
    }

    public RespApdu(int readStatus, byte[] respApdu, int respLength)
      : this(respApdu, respLength)
    {
      this.readerStatus = readStatus;
    }

    public static bool operator ==(RespApdu obj1, RespApdu obj2)
    {
      return ApduUtil.DataAreEqual(obj1.respApdu, obj2.respApdu);
    }

    public static bool operator !=(RespApdu obj1, RespApdu obj2)
    {
      return !(obj1 == obj2);
    }

    public override bool Equals(object obj)
    {
      if (obj == null || this.GetType() != obj.GetType())
        return false;
      else
        return this == (RespApdu) obj;
    }

    public override int GetHashCode()
    {
      return this.respApdu.GetHashCode();
    }

    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      if (this.respApdu != null && this.respApdu.Length > 0)
      {
        stringBuilder.Append(HexFormatting.Dump("<-- R-APDU: 0x", this.respApdu, this.respLength, 16, ValueFormat.HexASCII));
        stringBuilder.Append(Environment.NewLine);
        stringBuilder.Append(string.Format("    SW1SW2: 0x{0:X04} ", (object) this.sw1sw2));
      }
      if (this.readerStatus != 0)
      {
        stringBuilder.Append(Environment.NewLine);
        stringBuilder.Append(string.Format("    Reader Status: 0x{0:X08} ", (object) this.readerStatus));
      }
      return ((object) stringBuilder).ToString();
    }
  }
}
