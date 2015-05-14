// Decompiled with JetBrains decompiler
// Type: Novensys.ASN1.Util.DynamicByteArray
// Assembly: Novensys.eCard.SDK, Version=1.1.56.0, Culture=neutral, PublicKeyToken=null
// MVID: 59F9E456-D3DA-4066-A4A4-692B516A775E
// Assembly location: C:\Program Files (x86)\CNAS\SIUI-SPITAL\Novensys.eCard.SDK.dll

using System;

namespace Novensys.ASN1.Util
{
  [Serializable]
  public class DynamicByteArray
  {
    private byte[] buf;
    private int count;

    public DynamicByteArray(int size)
    {
      if (size <= 0)
        this.buf = new byte[32];
      else
        this.buf = new byte[size];
    }

    public void add(byte[] b, int off, int len)
    {
      if (b == null || off < 0 || (off > b.Length || len < 0) || (off + len > b.Length || off + len < 0))
        throw new IndexOutOfRangeException("Invalid parameters (" + (object) b + ").");
      if (len == 0)
        return;
      int val2 = this.count + len;
      if (val2 > this.buf.Length)
      {
        byte[] numArray = new byte[Math.Max(this.buf.Length << 1, val2)];
        Array.Copy((Array) this.buf, 0, (Array) numArray, 0, this.count);
        this.buf = numArray;
      }
      Array.Copy((Array) b, off, (Array) this.buf, this.count, len);
      this.count = val2;
    }

    public void addByte(byte b)
    {
      int val2 = this.count + 1;
      if (val2 > this.buf.Length)
      {
        byte[] numArray = new byte[Math.Max(this.buf.Length << 1, val2)];
        Array.Copy((Array) this.buf, 0, (Array) numArray, 0, this.count);
        this.buf = numArray;
      }
      this.buf[this.count] = b;
      this.count = val2;
    }

    public void addBytes(byte[] b)
    {
      if (b == null)
        return;
      this.add(b, 0, b.Length);
    }

    public void addBytes(byte[] b, int len)
    {
      this.add(b, 0, len);
    }

    public void reset()
    {
      this.count = 0;
    }

    public byte[] toByteArray()
    {
      byte[] numArray = new byte[this.count];
      Array.Copy((Array) this.buf, 0, (Array) numArray, 0, this.count);
      return numArray;
    }
  }
}
