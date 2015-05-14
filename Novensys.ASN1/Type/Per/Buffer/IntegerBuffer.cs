// Decompiled with JetBrains decompiler
// Type: Novensys.ASN1.Type.Per.Buffer.IntegerBuffer
// Assembly: Novensys.eCard.SDK, Version=1.1.56.0, Culture=neutral, PublicKeyToken=null
// MVID: 59F9E456-D3DA-4066-A4A4-692B516A775E
// Assembly location: C:\Program Files (x86)\CNAS\SIUI-SPITAL\Novensys.eCard.SDK.dll

using Novensys.ASN1.IO;
using Novensys.ASN1.Util;
using System;

namespace Novensys.ASN1.Type.Per.Buffer
{
  public class IntegerBuffer : AbstractBitField
  {
    private int _granularity = 0;
    private byte[] _bigInteger;
    private long _buffer;
    private bool _isReversed;
    private int _length;
    private bool _negative;

    internal IntegerBuffer()
    {
      this.reset();
    }

    public byte[] getBigInteger()
    {
      return this._bigInteger;
    }

    public long getLong()
    {
      return this._buffer;
    }

    public override int getNbElements()
    {
      return this._length;
    }

    public override int getNbElementsFrom(int start)
    {
      return this._length - start;
    }

    public bool isNegative()
    {
      return this._negative;
    }

    public override void read(int start, int length, IAsn1BitInputStream input)
    {
      if (length == 0)
      {
        input.printEmpty();
      }
      else
      {
        switch (this._granularity)
        {
          case 1:
            if (!this._isReversed)
            {
              int num = (length + 7) / 8;
              for (int index = 0; index < num - 1; ++index)
              {
                this._buffer = this._buffer << 8;
                this._buffer |= (long) input.read(8) & (long) byte.MaxValue;
                length -= 8;
              }
              this._buffer = this._buffer << length;
              this._buffer |= (long) input.read(8 - length, length) & (long) byte.MaxValue;
              break;
            }
            else
            {
              if (length % 8 != 0)
                length += 8 - length % 8;
              int num = length / 8;
              for (int index = 0; index < num; ++index)
                this._buffer |= ((long) input.read(8) & (long) byte.MaxValue) << (index << 3);
              break;
            }
          case 2:
            if (!this._isReversed)
            {
              if (length > 8)
              {
                this._bigInteger = new byte[length];
                for (int index = 0; index < length; ++index)
                  this._bigInteger[index] = input.read(8);
                break;
              }
              else
              {
                for (int index = 0; index < length; ++index)
                {
                  byte num = input.read(8);
                  if (index == 0 && ((int) num & 128) != 0)
                    this._negative = true;
                  this._buffer = this._buffer << 8;
                  this._buffer |= (long) num & (long) byte.MaxValue;
                }
                break;
              }
            }
            else if (length <= 8)
            {
              for (int index = 0; index < length; ++index)
              {
                byte num = input.read(8);
                if (index == length - 1 && ((int) num & 128) != 0)
                  this._negative = true;
                this._buffer |= ((long) num & (long) byte.MaxValue) << (index << 3);
              }
              break;
            }
            else
            {
              byte[] data = new byte[length];
              for (int index = 0; index < length; ++index)
                data[index] = input.read(8);
              this._bigInteger = ByteArray.ReverseByteArray(data);
              break;
            }
          default:
            throw new SystemException("granularity not defined");
        }
        this._length += length;
      }
    }

    protected internal override void reset()
    {
      this._granularity = 0;
      this._buffer = 0L;
      this._length = 0;
      this._negative = false;
      this._bigInteger = (byte[]) null;
      this._isReversed = false;
    }

    public void setBigInteger(byte[] bigValue)
    {
      this._bigInteger = bigValue;
    }

    public void setGranularity(int granularity)
    {
      this._granularity = granularity;
    }

    public virtual void setLength(int granularity, int length)
    {
      this._granularity = granularity;
      this._length = length;
    }

    public void setLong(long longValue)
    {
      this._buffer = longValue;
    }

    public void setReversed(bool b)
    {
      this._isReversed = b;
    }

    public override void write(int start, int length, IAsn1BitOutputStream output)
    {
      if (length == 0)
      {
        output.printEmpty();
      }
      else
      {
        switch (this._granularity)
        {
          case 1:
            int num1 = (length + 7) / 8;
            for (int index = 1; index <= num1 - 1; ++index)
            {
              int num2 = this._length - start - 8 * index;
              output.write((byte) ((ulong) (this._buffer >> num2) & (ulong) byte.MaxValue), 0, 8);
              length -= 8;
            }
            output.write((byte) ((ulong) this._buffer & (ulong) byte.MaxValue), 8 - length, length);
            break;
          case 2:
            if (this._bigInteger == null)
            {
              for (int index = 1; index <= length; ++index)
              {
                int num2 = this._length - start - index << 3;
                output.write((byte) ((ulong) (this._buffer >> num2) & (ulong) byte.MaxValue), 0, 8);
              }
              break;
            }
            else
            {
              for (int index = 0; index < length; ++index)
                output.write(this._bigInteger[start + index], 8);
              break;
            }
          default:
            throw new SystemException("granularity not defined");
        }
      }
    }
  }
}
