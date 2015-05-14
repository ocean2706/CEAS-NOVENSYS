// Decompiled with JetBrains decompiler
// Type: Novensys.ASN1.IO.ByteArrayAsn1OutputStream
// Assembly: Novensys.eCard.SDK, Version=1.1.56.0, Culture=neutral, PublicKeyToken=null
// MVID: 59F9E456-D3DA-4066-A4A4-692B516A775E
// Assembly location: C:\Program Files (x86)\CNAS\SIUI-SPITAL\Novensys.eCard.SDK.dll

using Novensys.ASN1;
using System;
using System.IO;

namespace Novensys.ASN1.IO
{
  [Serializable]
  public class ByteArrayAsn1OutputStream : IAsn1OutputStream
  {
    private const int BYTE_ARRAY_OUTPUT_STREAM_CAPACITY = 256;
    protected byte[] buf;
    protected int count;

    public ByteArrayAsn1OutputStream()
      : this(256)
    {
    }

    public ByteArrayAsn1OutputStream(int size)
    {
      if (size <= 0)
        this.buf = new byte[256];
      else
        this.buf = new byte[size];
    }

    public void flush()
    {
    }

    public Stream getStream()
    {
      return (Stream) null;
    }

    public void insertByteAt(int index, byte b)
    {
      if (index < 0 || index > this.count)
      {
        throw new IndexOutOfRangeException("Invalid index " + (object) index + " to insert byte at in a <" + (string) (object) this.count + "> bytes array.");
      }
      else
      {
        int val2 = this.count + 1;
        if (val2 > this.buf.Length)
        {
          byte[] numArray = new byte[Math.Max(this.buf.Length << 1, val2)];
          Array.Copy((Array) this.buf, 0, (Array) numArray, 0, this.count);
          this.buf = numArray;
        }
        Array.Copy((Array) this.buf, index, (Array) this.buf, index + 1, this.count - index);
        this.buf[index] = b;
        this.count = val2;
      }
    }

    public void reset()
    {
      this.count = 0;
    }

    public int size()
    {
      return this.count;
    }

    public byte[] toByteArray()
    {
      byte[] numArray = new byte[this.count];
      Array.Copy((Array) this.buf, 0, (Array) numArray, 0, this.count);
      return numArray;
    }

    public void write(byte[] b, int off, int len)
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

    public void writeByte(byte b)
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

    public void writeByte(byte b, int index)
    {
      if (index < 0)
        this.writeByte(b);
      else if (index >= this.count)
        throw new IndexOutOfRangeException("Invalid index " + (object) index + " for a <" + (string) (object) this.count + "> bytes array.");
      else
        this.buf[index] = b;
    }

    public void writeBytes(byte[] b)
    {
      if (b == null)
        return;
      this.write(b, 0, b.Length);
    }

    public void writeBytes(byte[] b, int len)
    {
      this.write(b, 0, len);
    }

    public void writeTo(IAsn1OutputStream output)
    {
      if (output.getStream() == null)
        throw new Asn1Exception(2, "underlying stream does not exist.");
      try
      {
        output.getStream().Write(this.buf, 0, this.count);
      }
      catch (IOException ex)
      {
        throw new Asn1Exception(2, ex.Message);
      }
    }
  }
}
