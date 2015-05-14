// Decompiled with JetBrains decompiler
// Type: Novensys.ASN1.Type.Per.Buffer.BitFieldWrapper
// Assembly: Novensys.eCard.SDK, Version=1.1.56.0, Culture=neutral, PublicKeyToken=null
// MVID: 59F9E456-D3DA-4066-A4A4-692B516A775E
// Assembly location: C:\Program Files (x86)\CNAS\SIUI-SPITAL\Novensys.eCard.SDK.dll

using Novensys.ASN1.IO;
using Novensys.ASN1.Util;
using System;

namespace Novensys.ASN1.Type.Per.Buffer
{
  public class BitFieldWrapper : AbstractBitField
  {
    public int _granularity = 0;
    private byte[] _array;
    private int _length;

    internal BitFieldWrapper()
    {
      this.reset();
    }

    private int bitLengthToByteLength(int bitLength)
    {
      if (bitLength != 0)
        return bitLength + 7 >> 3;
      else
        return 0;
    }

    private void ensureAddedByteCapacity(int addedCapacity)
    {
      if (this._array == null)
        this._array = new byte[0];
      int length1 = this._array.Length;
      int length2 = this._granularity == 2 ? this._length : this.bitLengthToByteLength(this._length);
      if (length2 + addedCapacity <= length1)
        return;
      byte[] numArray = this._array;
      this._array = new byte[length1 + addedCapacity];
      Array.Copy((Array) numArray, 0, (Array) this._array, 0, length2);
    }

    public byte[] getByteArray()
    {
      if (this._array == null)
        return new byte[0];
      else
        return this._array;
    }

    public int getLength()
    {
      return this._length;
    }

    public override int getNbElements()
    {
      return this.getLength();
    }

    public override int getNbElementsFrom(int start)
    {
      return this.getLength() - start;
    }

    public override void read(int start, int length, IAsn1BitInputStream input)
    {
      if (length <= 0)
      {
        input.printEmpty();
      }
      else
      {
        switch (this._granularity)
        {
          case 1:
            int index1 = this._length >> 3;
            int start1 = this._length & 7;
            int num = this._length + length + 7 & 7;
            int addedCapacity = this.bitLengthToByteLength(length);
            this.ensureAddedByteCapacity(addedCapacity);
            if (addedCapacity != 1)
            {
              this._array[index1] = input.read(start1, 8 - start1);
              for (int index2 = 1; index2 < addedCapacity - 1; ++index2)
                this._array[index1 + index2] = input.read(0, 8);
              this._array[index1 + addedCapacity - 1] = input.read(0, num + 1);
              break;
            }
            else
            {
              this._array[index1] = input.read(start1, length);
              break;
            }
          case 2:
            this.ensureAddedByteCapacity(length);
            for (int index2 = 0; index2 < length; ++index2)
              this._array[this._length + index2] = input.read(8);
            break;
          default:
            throw new SystemException("granularity not defined");
        }
        this._length += length;
      }
    }

    protected internal override void reset()
    {
      this._granularity = 0;
      this.resetArray();
    }

    protected virtual void resetArray()
    {
      this._array = (byte[]) null;
      this._length = 0;
    }

    public void setByteArray(byte[] array)
    {
      this._array = array;
    }

    public void setGranularity(int granularity)
    {
      this._granularity = granularity;
    }

    public void setLength(int granularity, int length)
    {
      this._granularity = granularity;
      this._length = length;
    }

    public override void write(int start, int length, IAsn1BitOutputStream output)
    {
      if (length <= 0)
      {
        output.printEmpty();
      }
      else
      {
        switch (this._granularity)
        {
          case 1:
            int index1 = Tools.URShift(start, 3);
            int start1 = start & 7;
            int num = this.bitLengthToByteLength(length);
            if (num != 1)
            {
              output.write(this._array[index1], start1, 8 - start1);
              length -= 8 - start1;
              for (int index2 = 1; index2 < num - 1; ++index2)
              {
                output.write(this._array[index1 + index2], 0, 8);
                length -= 8;
              }
              output.write(this._array[num - 1], 0, length);
              break;
            }
            else
            {
              output.write(this._array[index1], start1, length);
              break;
            }
          case 2:
            for (int index2 = 0; index2 < length; ++index2)
              output.write(this._array[start + index2], 8);
            break;
          default:
            throw new SystemException("granularity not defined");
        }
      }
    }
  }
}
