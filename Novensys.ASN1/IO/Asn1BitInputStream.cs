// Decompiled with JetBrains decompiler
// Type: Novensys.ASN1.IO.Asn1BitInputStream
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
  public class Asn1BitInputStream : IAsn1BitInputStream
  {
    private static bool _zeroPadded = true;
    private int _bitCount;
    private byte _buffer;
    protected Stream _in;
    private bool _isIgnoringPaddingBitValue;
    private int _nextBit;

    public Asn1BitInputStream(Stream input, int startBit)
    {
      this._in = input;
      this.reset();
      this._bitCount = startBit;
    }

    public int getBitCount()
    {
      return this._bitCount;
    }

    public int getBitIndex()
    {
      if (this._bitCount >= 8)
        return this._bitCount - 8 + this._nextBit;
      else
        return this._bitCount;
    }

    public int getByteCount()
    {
      return this._bitCount >> 3;
    }

    public bool isTerminated()
    {
      if (this._nextBit == -1 || this._nextBit >= 8)
      {
        try
        {
          this.readBuffer();
        }
        catch (Asn1Exception ex)
        {
          if (ex.ErrorCode != 7)
            throw ex;
          else
            return true;
        }
      }
      return false;
    }

    public virtual void printEmpty()
    {
    }

    public virtual void printLineSeparator()
    {
    }

    public virtual byte read(int nbBits)
    {
      int num = 0;
      if (nbBits > 0)
      {
        if (this._nextBit == -1 || this._nextBit >= 8)
          this.readBuffer();
        int bits1 = this._nextBit + nbBits <= 8 ? nbBits : 8 - this._nextBit;
        num |= (int) this._buffer << this._nextBit & Tools.URShift(65280, bits1);
        this._nextBit += bits1;
        int bits2 = nbBits - bits1;
        if (bits2 > 0)
        {
          this.readBuffer();
          num |= Tools.URShift((int) this._buffer & Tools.URShift(65280, bits2) & (int) byte.MaxValue, bits1);
          this._nextBit += bits2;
        }
      }
      return (byte) num;
    }

    public virtual byte read(int start, int nbBits)
    {
      return (byte) Tools.URShift((int) this.read(nbBits) & (int) byte.MaxValue, start);
    }

    private void readBuffer()
    {
      int num;
      try
      {
        num = this._in.ReadByte();
      }
      catch (Exception ex)
      {
        throw new Asn1Exception(46, "upon ReadByte() " + ex.Message, ex);
      }
      if (num == -1)
        throw new Asn1Exception(7, "end of stream");
      this._bitCount += 8;
      this._buffer = (byte) num;
      this._nextBit = 0;
    }

    public virtual bool readOneBitNoAlign()
    {
      return (int) this.read(1) == 128;
    }

    public void readPaddingBit(int nbBits)
    {
      if (nbBits <= 0)
        return;
      byte num = Asn1BitInputStream._zeroPadded ? (byte) 0 : (byte) (65280 >> nbBits);
      if (!this._isIgnoringPaddingBitValue && (int) this.read(nbBits) != (int) num)
        throw new Asn1Exception(47, "byte " + (object) this.getByteCount());
    }

    private void reset()
    {
      this._buffer = (byte) 0;
      this._isIgnoringPaddingBitValue = true;
      this._nextBit = -1;
    }

    public virtual void restoreAlignment()
    {
      if (this._nextBit <= 0)
        return;
      this.readPaddingBit(8 - this._nextBit);
    }

    public void setIgnoringPaddingBitValue(bool ignore)
    {
      this._isIgnoringPaddingBitValue = ignore;
    }

    public static void setZeroPadded(bool zeroPadded)
    {
      Asn1BitInputStream._zeroPadded = zeroPadded;
    }
  }
}
