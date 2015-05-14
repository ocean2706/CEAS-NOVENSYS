// Decompiled with JetBrains decompiler
// Type: Novensys.ASN1.IO.Asn1BitOutputStream
// Assembly: Novensys.eCard.SDK, Version=1.1.56.0, Culture=neutral, PublicKeyToken=null
// MVID: 59F9E456-D3DA-4066-A4A4-692B516A775E
// Assembly location: C:\Program Files (x86)\CNAS\SIUI-SPITAL\Novensys.eCard.SDK.dll

using Novensys.ASN1;
using Novensys.ASN1.Util;
using System;
using System.IO;

namespace Novensys.ASN1.IO
{
  [Serializable]
  public class Asn1BitOutputStream : IAsn1BitOutputStream
  {
    private byte _buffer;
    private int _flushedBitCount;
    private int _nextBit;
    protected Stream _out;
    private bool _zeroPadded;

    public Asn1BitOutputStream(Stream output)
    {
      this._out = output;
      this.reset();
      this._flushedBitCount = 0;
    }

    public virtual void flush()
    {
      this.restoreAlignment();
      try
      {
        this._out.Flush();
      }
      catch (IOException ex)
      {
        throw new Asn1Exception(2, ex.Message);
      }
    }

    private void flushBuffer()
    {
      try
      {
        this._out.WriteByte(this._buffer);
        this._flushedBitCount += 8;
      }
      catch (IOException ex)
      {
        throw new Asn1Exception(2, ex.Message);
      }
      this.reset();
    }

    public int getBitCount()
    {
      return this._flushedBitCount + this._nextBit;
    }

    public virtual int getNbPostPaddingBits()
    {
      return 8 - this._nextBit;
    }

    public virtual void printEmpty()
    {
    }

    public virtual void printLineSeparator()
    {
    }

    private void reset()
    {
      this._buffer = (byte) 0;
      this._nextBit = 0;
    }

    public virtual void restoreAlignment()
    {
      if (this._nextBit <= 0)
        return;
      this.writePaddingBit(8 - this._nextBit);
    }

    public void setZeroPadded(bool zeroPadded)
    {
      this._zeroPadded = zeroPadded;
    }

    public virtual void write(byte theBits, int nbBits)
    {
      if (nbBits <= 0)
        return;
      this._buffer = (byte) ((uint) this._buffer | (uint) (byte) Tools.URShift((int) theBits & (int) byte.MaxValue, this._nextBit));
      int num = this._nextBit + nbBits <= 8 ? nbBits : 8 - this._nextBit;
      this._nextBit += num;
      if (this._nextBit == 8)
      {
        this.flushBuffer();
        if (num < nbBits)
        {
          this._buffer = (byte) ((uint) theBits << num);
          this._nextBit = nbBits - num;
        }
      }
    }

    public virtual void write(byte theBits, int start, int nbBits)
    {
      this.write((byte) ((uint) theBits << start), nbBits);
    }

    public virtual void writeOneBitNoAlign(bool aBit)
    {
      this.write(aBit ? (byte) sbyte.MinValue : (byte) 0, 1);
    }

    public void writePaddingBit(int nbBits)
    {
      this.write(this._zeroPadded ? (byte) 0 : byte.MaxValue, nbBits);
    }
  }
}
