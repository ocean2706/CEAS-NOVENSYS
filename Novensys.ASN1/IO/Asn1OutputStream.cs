// Decompiled with JetBrains decompiler
// Type: Novensys.ASN1.IO.Asn1OutputStream
// Assembly: Novensys.eCard.SDK, Version=1.1.56.0, Culture=neutral, PublicKeyToken=null
// MVID: 59F9E456-D3DA-4066-A4A4-692B516A775E
// Assembly location: C:\Program Files (x86)\CNAS\SIUI-SPITAL\Novensys.eCard.SDK.dll

using Novensys.ASN1;
using System;
using System.IO;

namespace Novensys.ASN1.IO
{
  [Serializable]
  public class Asn1OutputStream : IAsn1OutputStream
  {
    protected Stream _output;

    public Asn1OutputStream(Stream output)
    {
      this._output = output;
    }

    public void flush()
    {
      try
      {
        this._output.Flush();
      }
      catch (IOException ex)
      {
        throw new Asn1Exception(2, ex.Message);
      }
    }

    public Stream getStream()
    {
      return this._output;
    }

    public void insertByteAt(int index, byte b)
    {
      throw new Asn1Exception(2, "Insertion is not supported.");
    }

    public int size()
    {
      return -1;
    }

    public byte[] toByteArray()
    {
      return (byte[]) null;
    }

    public void writeByte(byte b)
    {
      try
      {
        this._output.WriteByte(b);
      }
      catch (IOException ex)
      {
        throw new Asn1Exception(2, ex.Message);
      }
    }

    public void writeByte(byte b, int index)
    {
      this.writeByte(b);
    }

    public void writeBytes(byte[] b)
    {
      try
      {
        this._output.Write(b, 0, b.Length);
      }
      catch (IOException ex)
      {
        throw new Asn1Exception(2, ex.Message);
      }
    }

    public void writeBytes(byte[] b, int len)
    {
      try
      {
        this._output.Write(b, 0, len);
      }
      catch (IOException ex)
      {
        throw new Asn1Exception(2, ex.Message);
      }
    }

    public void writeTo(IAsn1OutputStream asn1OutputStream)
    {
      throw new Asn1Exception(2, "writeTo is not supported.");
    }
  }
}
