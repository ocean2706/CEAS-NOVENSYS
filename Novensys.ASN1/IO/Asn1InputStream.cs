// Decompiled with JetBrains decompiler
// Type: Novensys.ASN1.IO.Asn1InputStream
// Assembly: Novensys.eCard.SDK, Version=1.1.56.0, Culture=neutral, PublicKeyToken=null
// MVID: 59F9E456-D3DA-4066-A4A4-692B516A775E
// Assembly location: C:\Program Files (x86)\CNAS\SIUI-SPITAL\Novensys.eCard.SDK.dll

using Novensys.ASN1;
using System;
using System.IO;

namespace Novensys.ASN1.IO
{
  [Serializable]
  public class Asn1InputStream : IAsn1InputStream
  {
    private int position = 0;
    private int positionMarked = 0;
    private const int BYTE_ARRAY_CAPACITY = 256;
    private byte[] buf;
    private int count;
    private Stream input;

    public Asn1InputStream(Stream input)
    {
      this.input = input;
      try
      {
        if (input.CanSeek)
          this.buf = (byte[]) null;
        else
          this.buf = new byte[256];
      }
      catch (Exception ex)
      {
        throw new Asn1Exception(46, "while intializing the stream " + ex.Message, ex);
      }
    }

    public long getPosition()
    {
      return (long) this.position;
    }

    public void mark()
    {
      this.positionMarked = this.position;
    }

    public byte readByte()
    {
      int num;
      if (this.buf != null)
      {
        if (this.position == this.count)
        {
          try
          {
            num = this.input.ReadByte();
          }
          catch (Exception ex)
          {
            throw new Asn1Exception(46, "upon ReadByte() " + ex.Message, ex);
          }
          if (num != -1)
            this.writeByte((byte) num);
        }
        else
          num = this.position < this.count ? (int) this.buf[this.position++] & (int) byte.MaxValue : -1;
      }
      else
      {
        try
        {
          num = this.input.ReadByte();
        }
        catch (Exception ex)
        {
          throw new Asn1Exception(46, "upon ReadByte() " + ex.Message, ex);
        }
      }
      if (num == -1)
        throw new Asn1Exception(7, "end of stream");
      ++this.position;
      return (byte) num;
    }

    public virtual byte[] readBytes(int len)
    {
      byte[] numArray = new byte[len];
      int offset;
      try
      {
        offset = this.input.Read(numArray, 0, len);
      }
      catch (Exception ex)
      {
        throw new Asn1Exception(46, "upon Read(buffer, 0, length) " + ex.Message, ex);
      }
      if (offset == 0)
        throw new Asn1Exception(7, "end of stream");
      if (offset != len)
      {
        while (offset < len)
        {
          int num;
          try
          {
            num = this.input.Read(numArray, offset, len - offset);
          }
          catch (Exception ex)
          {
            throw new Asn1Exception(46, "upon read(buffer, readBytes, len-readBytes) " + ex.Message, ex);
          }
          if (num == 0)
            throw new Asn1Exception(7, "end of stream");
          offset += num;
        }
      }
      if (this.buf != null)
        this.write(numArray);
      this.position += len;
      return numArray;
    }

    public void reset()
    {
      if (this.buf == null)
      {
        try
        {
          this.input.Seek((long) (this.positionMarked - this.position), SeekOrigin.Current);
        }
        catch (Exception ex)
        {
          throw new Asn1Exception(46, "while setting Position " + ex.Message, ex);
        }
      }
      this.position = this.positionMarked;
    }

    private void write(byte[] b)
    {
      int length = b.Length;
      if (length == 0)
        return;
      int val2 = this.count + length;
      if (val2 > this.buf.Length)
      {
        byte[] numArray = new byte[Math.Max(this.buf.Length << 1, val2)];
        Array.Copy((Array) this.buf, 0, (Array) numArray, 0, this.count);
        this.buf = numArray;
      }
      Array.Copy((Array) b, 0, (Array) this.buf, this.count, length);
      this.count = val2;
    }

    private void writeByte(byte b)
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
  }
}
