// Decompiled with JetBrains decompiler
// Type: Novensys.eCard.SDK.ISO8583.Bitmap
// Assembly: Novensys.eCard.SDK, Version=1.1.56.0, Culture=neutral, PublicKeyToken=null
// MVID: 59F9E456-D3DA-4066-A4A4-692B516A775E
// Assembly location: C:\Program Files (x86)\CNAS\SIUI-SPITAL\Novensys.eCard.SDK.dll

using Novensys.eCard.SDK.ISO8583.Formatter;
using System;

namespace Novensys.eCard.SDK.ISO8583
{
  public class Bitmap
  {
    private readonly bool[] bits;
    private readonly IFormatter formatter;

    public bool IsExtendedBitmap
    {
      get
      {
        return this.IsFieldSet(1);
      }
    }

    public int PackedLength
    {
      get
      {
        return this.formatter.GetPackedLength(this.IsExtendedBitmap ? 32 : 16);
      }
    }

    public bool this[int field]
    {
      get
      {
        return this.IsFieldSet(field);
      }
      set
      {
        this.SetField(field, value);
      }
    }

    public Bitmap()
      : this((IFormatter) new BinaryFormatter())
    {
    }

    public Bitmap(IFormatter formatter)
    {
      this.formatter = formatter;
      this.bits = new bool[128];
    }

    public bool IsFieldSet(int field)
    {
      return this.bits[field - 1];
    }

    public void SetField(int field, bool on)
    {
      this.bits[field - 1] = on;
      this.bits[0] = false;
      for (int index = 64; index <= (int) sbyte.MaxValue; ++index)
      {
        if (this.bits[index])
        {
          this.bits[0] = true;
          break;
        }
      }
    }

    public virtual byte[] ToMsg()
    {
      int length = this.IsExtendedBitmap ? 16 : 8;
      byte[] data = new byte[length];
      for (int index1 = 0; index1 < length; ++index1)
      {
        for (int index2 = 0; index2 < 8; ++index2)
        {
          if (this.bits[index1 * 8 + index2])
            data[index1] = (byte) ((uint) data[index1] | 128U / (uint) (int) Math.Pow(2.0, (double) index2));
        }
      }
      if (this.formatter is BinaryFormatter)
        return data;
      else
        return this.formatter.GetBytes(((IFormatter) new BinaryFormatter()).GetString(data));
    }

    public int Unpack(byte[] msg, int offset)
    {
      int packedLength = this.formatter.GetPackedLength(16);
      if (this.formatter is BinaryFormatter)
      {
        if ((int) msg[offset] >= 128)
          packedLength += 8;
      }
      else if ((int) msg[offset] >= 56)
        packedLength += 16;
      byte[] data = new byte[packedLength];
      Array.Copy((Array) msg, offset, (Array) data, 0, packedLength);
      if (!(this.formatter is BinaryFormatter))
        data = ((IFormatter) new BinaryFormatter()).GetBytes(this.formatter.GetString(data));
      for (int index1 = 0; index1 < data.Length; ++index1)
      {
        for (int index2 = 0; index2 < 8; ++index2)
          this.bits[index1 * 8 + index2] = ((int) data[index1] & 128 / (int) Math.Pow(2.0, (double) index2)) > 0;
      }
      return offset + packedLength;
    }
  }
}
