// Decompiled with JetBrains decompiler
// Type: Novensys.eCard.SDK.PCSC.Apdu.CmdApdu
// Assembly: Novensys.eCard.SDK, Version=1.1.56.0, Culture=neutral, PublicKeyToken=null
// MVID: 59F9E456-D3DA-4066-A4A4-692B516A775E
// Assembly location: C:\Program Files (x86)\CNAS\SIUI-SPITAL\Novensys.eCard.SDK.dll

using Novensys.eCard.SDK.Utils.Hex;
using System;
using System.Collections.Generic;
using System.Text;

namespace Novensys.eCard.SDK.PCSC.Apdu
{
  public class CmdApdu : ICloneable, ICmdApdu
  {
    private byte cla;
    private byte[] data;
    private byte ins;
    private int? lc;
    private int? le;
    private byte p1;
    private byte p2;

    public byte CLA
    {
      get
      {
        return this.cla;
      }
      set
      {
        this.cla = value;
      }
    }

    public byte[] Data
    {
      get
      {
        return this.data;
      }
      set
      {
        this.data = value;
        if (value == null)
          this.Lc = new int?();
        else
          this.Lc = new int?(value.Length);
      }
    }

    public byte INS
    {
      get
      {
        return this.ins;
      }
      set
      {
        this.ins = value;
      }
    }

    public int? Lc
    {
      get
      {
        return this.lc;
      }
      set
      {
        if (value.HasValue)
        {
          int? nullable = this.lc;
          if ((nullable.GetValueOrDefault() != 0 ? 0 : (nullable.HasValue ? 1 : 0)) != 0)
            this.lc = new int?();
          else
            this.lc = value;
        }
        else
          this.lc = new int?();
      }
    }

    public int? Le
    {
      get
      {
        return this.le;
      }
      set
      {
        if (value.HasValue)
          this.le = value;
        else
          this.le = new int?();
      }
    }

    public byte P1
    {
      get
      {
        return this.p1;
      }
      set
      {
        this.p1 = value;
      }
    }

    public byte P2
    {
      get
      {
        return this.p2;
      }
      set
      {
        this.p2 = value;
      }
    }

    public CmdApdu()
    {
      this.CLA = (byte) 0;
      this.INS = (byte) 0;
      this.P1 = (byte) 0;
      this.P2 = (byte) 0;
      this.Lc = new int?();
      this.Data = (byte[]) null;
      this.Le = new int?();
    }

    public CmdApdu(string cmdApduString)
    {
      this.SetValue(cmdApduString);
    }

    public CmdApdu(byte[] cmdApduBuffer)
    {
      this.SetValue(cmdApduBuffer);
    }

    public CmdApdu(CmdApdu cmdApdu)
    {
      this.CLA = cmdApdu.CLA;
      this.INS = cmdApdu.INS;
      this.P1 = cmdApdu.P1;
      this.P2 = cmdApdu.P2;
      this.Data = (byte[]) cmdApdu.Data.Clone();
      this.Le = cmdApdu.Le;
    }

    public CmdApdu(byte cla, byte ins, byte p1, byte p2, byte[] data, int? le)
    {
      this.CLA = cla;
      this.INS = ins;
      this.P1 = p1;
      this.P2 = p2;
      this.Data = data;
      this.Le = le;
    }

    public CmdApdu(byte cla, byte ins, byte p1, byte p2, int? lc, byte[] data, int? le)
    {
      this.CLA = cla;
      this.INS = ins;
      this.P1 = p1;
      this.P2 = p2;
      this.Data = data;
      this.Lc = lc;
      this.Le = le;
    }

    public static bool operator ==(CmdApdu cmdApdu1, CmdApdu cmdApdu2)
    {
      int? nullable1 = new int?();
      int? nullable2 = new int?();
      if ((int) cmdApdu1.CLA == (int) cmdApdu2.CLA && (int) cmdApdu1.INS == (int) cmdApdu2.INS && ((int) cmdApdu1.P1 == (int) cmdApdu2.P1 && (int) cmdApdu1.P2 == (int) cmdApdu2.P2))
      {
        nullable1 = cmdApdu1.Lc;
        nullable2 = cmdApdu2.Lc;
      }
      return nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() && nullable1.HasValue == nullable2.HasValue && ApduUtil.DataAreEqual(cmdApdu1.Data, cmdApdu2.Data) && ((nullable1 = cmdApdu1.Le).GetValueOrDefault() == (nullable2 = cmdApdu2.Le).GetValueOrDefault() && nullable1.HasValue == nullable2.HasValue);
    }

    public static bool operator !=(CmdApdu cmdApdu1, CmdApdu cmdApdu2)
    {
      return !(cmdApdu1 == cmdApdu2);
    }

    public object Clone()
    {
      return (object) new CmdApdu()
      {
        CLA = this.CLA,
        INS = this.INS,
        P1 = this.P1,
        P2 = this.P2,
        Data = (byte[]) this.Data.Clone(),
        Le = this.Le
      };
    }

    public override bool Equals(object obj)
    {
      if (obj == null || this.GetType() != obj.GetType())
        return false;
      else
        return this == (CmdApdu) obj;
    }

    public byte[] GetBytes()
    {
      List<byte> list = new List<byte>();
      bool flag = false;
      list.Add(this.CLA);
      list.Add(this.INS);
      list.Add(this.P1);
      list.Add(this.P2);
      int? nullable1;
      int? nullable2;
      int? nullable3;
      if (this.Lc.HasValue)
      {
        nullable1 = this.Lc;
        if (nullable1.GetValueOrDefault() <= (int) byte.MaxValue && nullable1.HasValue)
        {
          nullable2 = this.Lc;
          int? nullable4 = nullable2.HasValue ? new int?(nullable2.GetValueOrDefault() & (int) byte.MaxValue) : new int?();
          list.Add((byte) nullable4.Value);
        }
        else
        {
          flag = true;
          list.Add((byte) 0);
          nullable1 = this.Lc;
          int? nullable4;
          if (!nullable1.HasValue)
          {
            int? nullable5 = new int?();
            nullable4 = nullable3 = nullable5;
          }
          else
            nullable4 = new int?(nullable1.GetValueOrDefault() & 65280);
          nullable2 = nullable4;
          int? nullable6 = nullable2.HasValue ? new int?(nullable2.GetValueOrDefault() >> 8) : new int?();
          list.Add((byte) nullable6.Value);
          nullable2 = this.Lc;
          int? nullable7 = nullable2.HasValue ? new int?(nullable2.GetValueOrDefault() & (int) byte.MaxValue) : new int?();
          list.Add((byte) nullable7.Value);
        }
        int index = 0;
        while (true)
        {
          int num = index;
          nullable1 = this.Lc;
          if (num < nullable1.GetValueOrDefault() && nullable1.HasValue)
          {
            list.Add(this.Data[index]);
            ++index;
          }
          else
            break;
        }
      }
      nullable2 = this.Le;
      if (nullable2.HasValue)
      {
        int num;
        if (!flag)
        {
          nullable2 = nullable1 = this.Le;
          num = nullable2.GetValueOrDefault() > 256 ? 1 : (!nullable1.HasValue ? 1 : 0);
        }
        else
          num = 1;
        if (num == 0)
        {
          nullable2 = this.Le;
          if ((nullable2.GetValueOrDefault() != 256 ? 0 : (nullable2.HasValue ? 1 : 0)) != 0)
          {
            list.Add((byte) 0);
          }
          else
          {
            nullable2 = this.Le;
            int? nullable4 = nullable2.HasValue ? new int?(nullable2.GetValueOrDefault() & (int) byte.MaxValue) : new int?();
            list.Add((byte) nullable4.Value);
          }
        }
        else
        {
          nullable1 = this.Le;
          if (nullable1.GetValueOrDefault() == 65536 && nullable1.HasValue)
          {
            if (!flag)
            {
              list.Add((byte) 0);
              list.Add((byte) 0);
              list.Add((byte) 0);
            }
            else
            {
              list.Add((byte) 0);
              list.Add((byte) 0);
            }
          }
          else if (!flag)
          {
            list.Add((byte) 0);
            nullable1 = this.Le;
            int? nullable4;
            if (!nullable1.HasValue)
            {
              nullable2 = new int?();
              nullable4 = nullable3 = nullable2;
            }
            else
              nullable4 = new int?(nullable1.GetValueOrDefault() & 65280);
            nullable2 = nullable4;
            int? nullable5 = nullable2.HasValue ? new int?(nullable2.GetValueOrDefault() >> 8) : new int?();
            list.Add((byte) nullable5.Value);
            nullable2 = this.Le;
            int? nullable6 = nullable2.HasValue ? new int?(nullable2.GetValueOrDefault() & (int) byte.MaxValue) : new int?();
            list.Add((byte) nullable6.Value);
          }
          else
          {
            nullable1 = this.Le;
            int? nullable4;
            if (!nullable1.HasValue)
            {
              nullable2 = new int?();
              nullable4 = nullable3 = nullable2;
            }
            else
              nullable4 = new int?(nullable1.GetValueOrDefault() & 65280);
            nullable2 = nullable4;
            int? nullable5 = nullable2.HasValue ? new int?(nullable2.GetValueOrDefault() >> 8) : new int?();
            list.Add((byte) nullable5.Value);
            nullable2 = this.Le;
            int? nullable6 = nullable2.HasValue ? new int?(nullable2.GetValueOrDefault() & (int) byte.MaxValue) : new int?();
            list.Add((byte) nullable6.Value);
          }
        }
      }
      return list.ToArray();
    }

    public override int GetHashCode()
    {
      byte num1 = this.CLA;
      int hashCode1 = num1.GetHashCode();
      num1 = this.INS;
      int hashCode2 = num1.GetHashCode();
      int num2 = hashCode1 ^ hashCode2;
      num1 = this.P1;
      int hashCode3 = num1.GetHashCode();
      int num3 = num2 ^ hashCode3;
      num1 = this.P2;
      int hashCode4 = num1.GetHashCode();
      return num3 ^ hashCode4 ^ this.Lc.GetHashCode() ^ this.Data.GetHashCode() ^ this.Le.GetHashCode();
    }

    public void SetValue(byte[] cmdApduBuffer)
    {
      if (cmdApduBuffer == null)
        throw new ArgumentNullException("cmdApduBuffer");
      if (cmdApduBuffer.Length < 4)
        throw new ArgumentOutOfRangeException("cmdApduBuffer", "The minimum APDU length is 4 bytes!");
      this.CLA = cmdApduBuffer[0];
      this.INS = cmdApduBuffer[1];
      this.P1 = cmdApduBuffer[2];
      this.P2 = cmdApduBuffer[3];
      this.Lc = new int?();
      this.Data = (byte[]) null;
      this.Le = new int?();
      if (cmdApduBuffer.Length == 4)
        return;
      bool flag;
      if (cmdApduBuffer.Length == 5)
      {
        if ((int) cmdApduBuffer[4] == 0)
          this.Le = new int?(256);
        else
          this.Le = new int?((int) cmdApduBuffer[4]);
      }
      else if (cmdApduBuffer.Length <= 5 || (int) cmdApduBuffer[4] == 0)
      {
        if (cmdApduBuffer.Length == 7 && (int) cmdApduBuffer[4] == 0)
        {
          if ((int) cmdApduBuffer[5] == 0 && (int) cmdApduBuffer[6] == 0)
            this.Le = new int?(65536);
          else
            this.Le = new int?((int) cmdApduBuffer[5] * 256 + (int) cmdApduBuffer[6]);
        }
        else
        {
          if (cmdApduBuffer.Length <= 7 || (int) cmdApduBuffer[4] != 0)
            throw new ArgumentOutOfRangeException("cmdApduBuffer", "APDU format error!");
          this.Lc = new int?((int) cmdApduBuffer[5] * 256 + (int) cmdApduBuffer[6]);
          int? nullable1 = this.Lc;
          if ((nullable1.GetValueOrDefault() != 0 ? 0 : (nullable1.HasValue ? 1 : 0)) != 0)
            throw new ArgumentOutOfRangeException("cmdApduBuffer", "APDU format error!");
          int length1 = cmdApduBuffer.Length;
          int? nullable2 = this.lc;
          int num1 = length1;
          int? nullable3;
          if (!nullable2.HasValue)
          {
            nullable1 = new int?();
            nullable3 = nullable1;
          }
          else
            nullable3 = new int?(7 + nullable2.GetValueOrDefault());
          nullable1 = nullable3;
          if ((num1 != nullable1.GetValueOrDefault() ? 1 : (!nullable1.HasValue ? 1 : 0)) != 0)
          {
            int length2 = cmdApduBuffer.Length;
            nullable1 = this.lc;
            nullable1 = nullable1.HasValue ? new int?(9 + nullable1.GetValueOrDefault()) : new int?();
            if ((length2 != nullable1.GetValueOrDefault() ? 1 : (!nullable1.HasValue ? 1 : 0)) != 0)
              throw new ArgumentOutOfRangeException("cmdApduBuffer", "APDU format error!");
            this.Le = (int) cmdApduBuffer[cmdApduBuffer.Length - 2] != 0 || (int) cmdApduBuffer[cmdApduBuffer.Length - 1] != 0 ? new int?((int) cmdApduBuffer[cmdApduBuffer.Length - 2] * 256 + (int) cmdApduBuffer[cmdApduBuffer.Length - 1]) : new int?(65536);
            nullable1 = this.Lc;
            this.Data = new byte[nullable1.Value];
            int index = 0;
            while (true)
            {
              flag = true;
              int num2 = index;
              nullable1 = this.lc;
              if ((num2 < nullable1.GetValueOrDefault() ? 0 : (nullable1.HasValue ? 1 : 0)) == 0)
              {
                this.Data[index] = cmdApduBuffer[7 + index];
                ++index;
              }
              else
                break;
            }
          }
          else
          {
            nullable1 = this.Lc;
            this.Data = new byte[nullable1.Value];
            int index = 0;
            while (true)
            {
              flag = true;
              int num2 = index;
              nullable1 = this.Lc;
              if ((num2 < nullable1.GetValueOrDefault() ? 0 : (nullable1.HasValue ? 1 : 0)) == 0)
              {
                this.Data[index] = cmdApduBuffer[7 + index];
                ++index;
              }
              else
                break;
            }
          }
        }
      }
      else
      {
        this.Lc = new int?((int) cmdApduBuffer[4]);
        int? nullable1 = this.Lc;
        if ((nullable1.GetValueOrDefault() != 0 ? 0 : (nullable1.HasValue ? 1 : 0)) != 0)
          throw new ArgumentOutOfRangeException("cmdApduBuffer", "APDU format error!");
        int length1 = cmdApduBuffer.Length;
        int? lc = this.Lc;
        int num1 = length1;
        int? nullable2;
        if (!lc.HasValue)
        {
          nullable1 = new int?();
          nullable2 = nullable1;
        }
        else
          nullable2 = new int?(5 + lc.GetValueOrDefault());
        nullable1 = nullable2;
        if ((num1 != nullable1.GetValueOrDefault() ? 1 : (!nullable1.HasValue ? 1 : 0)) != 0)
        {
          int length2 = cmdApduBuffer.Length;
          nullable1 = this.Lc;
          nullable1 = nullable1.HasValue ? new int?(6 + nullable1.GetValueOrDefault()) : new int?();
          if ((length2 != nullable1.GetValueOrDefault() ? 1 : (!nullable1.HasValue ? 1 : 0)) != 0)
            throw new ArgumentOutOfRangeException("cmdApduBuffer", "APDU format error!");
          this.Le = (int) cmdApduBuffer[cmdApduBuffer.Length - 1] != 0 ? new int?((int) cmdApduBuffer[cmdApduBuffer.Length - 1]) : new int?(256);
          nullable1 = this.Lc;
          this.Data = new byte[nullable1.Value];
          int index = 0;
          while (true)
          {
            flag = true;
            int num2 = index;
            nullable1 = this.Lc;
            if ((num2 < nullable1.GetValueOrDefault() ? 0 : (nullable1.HasValue ? 1 : 0)) == 0)
            {
              this.Data[index] = cmdApduBuffer[5 + index];
              ++index;
            }
            else
              break;
          }
        }
        else
        {
          nullable1 = this.Lc;
          this.Data = new byte[nullable1.Value];
          int index = 0;
          while (true)
          {
            flag = true;
            int num2 = index;
            nullable1 = this.Lc;
            if ((num2 < nullable1.GetValueOrDefault() ? 0 : (nullable1.HasValue ? 1 : 0)) == 0)
            {
              this.Data[index] = cmdApduBuffer[5 + index];
              ++index;
            }
            else
              break;
          }
        }
      }
    }

    public void SetValue(string cmdApduString)
    {
      this.SetValue(HexEncoding.GetBytes(cmdApduString));
    }

    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      byte[] bytes = this.GetBytes();
      stringBuilder.Append(HexFormatting.Dump("--> C-APDU: 0x", bytes, bytes.Length, 16, ValueFormat.HexASCII));
      return ((object) stringBuilder).ToString();
    }
  }
}
