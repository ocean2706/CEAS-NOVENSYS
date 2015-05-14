// Decompiled with JetBrains decompiler
// Type: Novensys.ASN1.Util.BitString
// Assembly: Novensys.eCard.SDK, Version=1.1.56.0, Culture=neutral, PublicKeyToken=null
// MVID: 59F9E456-D3DA-4066-A4A4-692B516A775E
// Assembly location: C:\Program Files (x86)\CNAS\SIUI-SPITAL\Novensys.eCard.SDK.dll

using Novensys.ASN1.Type;
using System;
using System.Text;

namespace Novensys.ASN1.Util
{
  [Serializable]
  public class BitString : ICloneable
  {
    private static int BIT_INDEX_MASK = BitString.BITS_PER_UNIT - 1;
    private static int BITS_PER_UNIT = 8;
    private const int ADDRESS_BITS_PER_UNIT = 3;
    private byte[] bits;
    private int nbBitsInLastUnit;
    private int unitsInUse;

    public BitString()
      : this(0, 0)
    {
    }

    public BitString(int nbBits)
      : this(nbBits, nbBits)
    {
    }

    public BitString(byte[] data, int length)
    {
      this.nbBitsInLastUnit = 0;
      this.unitsInUse = 0;
      if (data == null)
        throw new ArgumentException("parameter data is null");
      if (length < 0)
        throw new ArgumentOutOfRangeException();
      this.unitsInUse = BitString.unitIndex(length - 1) + 1;
      if (this.unitsInUse > data.Length)
        throw new ArgumentException("there is not length bits in data");
      this.bits = new byte[this.unitsInUse];
      Array.Copy((Array) data, 0, (Array) this.bits, 0, this.unitsInUse);
      if (length > 0)
      {
        this.nbBitsInLastUnit = (length - 1 & BitString.BIT_INDEX_MASK) + 1;
        int num1 = 65408 >> this.nbBitsInLastUnit;
        byte[] numArray;
        IntPtr num2;
        (numArray = this.bits)[(int) (num2 = (IntPtr) (this.unitsInUse - 1))] = (byte) ((uint) numArray[(int) num2] & (uint) (byte) num1);
      }
      else
        this.nbBitsInLastUnit = 0;
    }

    public BitString(int initialSize, int nbBits)
    {
      this.nbBitsInLastUnit = 0;
      this.unitsInUse = 0;
      if (initialSize < 0 || nbBits < 0)
        throw new ArgumentOutOfRangeException();
      this.unitsInUse = BitString.unitIndex(initialSize - 1) + 1;
      this.bits = new byte[this.unitsInUse];
      if (nbBits > 0)
        this.nbBitsInLastUnit = (nbBits - 1 & BitString.BIT_INDEX_MASK) + 1;
      else
        this.nbBitsInLastUnit = 0;
    }

    private static byte bit(int bitIndex)
    {
      return (byte) (1 << BitString.BIT_INDEX_MASK - (bitIndex & BitString.BIT_INDEX_MASK));
    }

    public virtual void clearAll()
    {
      for (int index = 0; index < this.unitsInUse; ++index)
        this.bits[index] = (byte) 0;
    }

    public object Clone()
    {
      return this.MemberwiseClone();
    }

    public virtual bool containsTrue()
    {
      bool flag = false;
      int num = 0;
      while (!flag && num < this.length())
        flag = this.get(num++);
      return flag;
    }

    private void ensureCapacity(int unitsRequired)
    {
      if (this.bits.Length >= unitsRequired)
        return;
      byte[] numArray = new byte[Math.Max(2 * this.bits.Length, unitsRequired)];
      Array.Copy((Array) this.bits, 0, (Array) numArray, 0, this.unitsInUse);
      this.bits = numArray;
    }

    public override bool Equals(object anObject)
    {
      if (anObject == null || !(anObject is BitString))
        return false;
      if (this != anObject)
      {
        BitString bitString = (BitString) anObject;
        if (this.length() != bitString.length() || this.getUnusedBitsInLastByte() != bitString.getUnusedBitsInLastByte())
          return false;
        if (this.length() == 0)
          return true;
        byte[] bytes = bitString.getBytes();
        for (int index = 0; index < this.unitsInUse - 1; ++index)
        {
          if ((int) this.bits[index] != (int) bytes[index])
            return false;
        }
        int num = 65408 >> 8 - this.getUnusedBitsInLastByte();
        if (((int) this.bits[this.unitsInUse - 1] & (int) (byte) num) != ((int) bytes[this.unitsInUse - 1] & (int) (byte) num))
          return false;
      }
      return true;
    }

    public virtual bool get(int bitIndex)
    {
      if (bitIndex < 0)
        throw new IndexOutOfRangeException(Convert.ToString(bitIndex));
      bool flag = false;
      int index = BitString.unitIndex(bitIndex);
      if (index < this.unitsInUse)
        flag = ((int) this.bits[index] & (int) BitString.bit(bitIndex)) != 0;
      return flag;
    }

    public static BitString getBitString(Asn1BitStringType type)
    {
      return new BitString(type.ByteArrayValue, type.Count);
    }

    public virtual byte[] getBytes()
    {
      byte[] numArray = new byte[this.unitsInUse];
      Array.Copy((Array) this.bits, 0, (Array) numArray, 0, this.unitsInUse);
      return numArray;
    }

    public override int GetHashCode()
    {
      byte[] bytes = this.getBytes();
      if (bytes == null)
        return 0;
      int num = 1;
      for (int index = 0; index < bytes.Length; ++index)
        num = 31 * num + (int) bytes[index];
      return num;
    }

    public virtual byte[] getInternalBytes()
    {
      return this.bits;
    }

    public virtual int getNbBytes()
    {
      return this.unitsInUse;
    }

    public virtual int getNbTrimmedBytes()
    {
      bool flag = false;
      int index = this.unitsInUse - 1;
      while (index >= 0 && !flag)
      {
        if ((int) this.bits[index] != 0)
          flag = true;
        else
          --index;
      }
      return index + 1;
    }

    public static BitString getReverseBitString(Asn1BitStringType type)
    {
      BitString bitString1 = new BitString(type.ByteArrayValue, type.Count);
      int count = type.Count;
      BitString bitString2 = new BitString(count);
      for (int bitIndex = 0; bitIndex < count; ++bitIndex)
        bitString2.set(bitIndex, bitString1.get(count - 1 - bitIndex));
      return bitString2;
    }

    private int getUnusedBits(int aByte)
    {
      int num = 0;
      while (aByte != 0)
      {
        aByte = aByte << 1 & (int) byte.MaxValue;
        ++num;
      }
      return BitString.BITS_PER_UNIT - num;
    }

    public virtual int getUnusedBitsInLastByte()
    {
      if (this.unitsInUse == 0)
        return -1;
      else
        return BitString.BITS_PER_UNIT - this.nbBitsInLastUnit;
    }

    public virtual int getUnusedBitsInLastTrimmedByte()
    {
      int nbTrimmedBytes = this.getNbTrimmedBytes();
      if (nbTrimmedBytes == 0)
        return -1;
      else
        return this.getUnusedBits((int) this.bits[nbTrimmedBytes - 1]);
    }

    public virtual int length()
    {
      if (this.unitsInUse == 0)
        return 0;
      else
        return (this.unitsInUse - 1) * BitString.BITS_PER_UNIT + this.nbBitsInLastUnit;
    }

    public virtual void set(int bitIndex, bool bitValue)
    {
      if (bitIndex < 0)
        throw new IndexOutOfRangeException(Convert.ToString(bitIndex));
      int num1 = BitString.unitIndex(bitIndex);
      int unitsRequired = num1 + 1;
      if (this.unitsInUse < unitsRequired)
      {
        this.ensureCapacity(unitsRequired);
        this.unitsInUse = unitsRequired;
        this.nbBitsInLastUnit = bitIndex % BitString.BITS_PER_UNIT + 1;
      }
      else if (this.unitsInUse == unitsRequired)
      {
        int num2 = bitIndex % BitString.BITS_PER_UNIT;
        if (num2 + 1 > this.nbBitsInLastUnit)
          this.nbBitsInLastUnit = num2 + 1;
      }
      if (bitValue)
      {
        byte[] numArray;
        IntPtr num2;
        (numArray = this.bits)[(int) (num2 = (IntPtr) num1)] = (byte) ((uint) numArray[(int) num2] | (uint) BitString.bit(bitIndex));
      }
      else
      {
        byte[] numArray;
        IntPtr num2;
        (numArray = this.bits)[(int) (num2 = (IntPtr) num1)] = (byte) ((uint) numArray[(int) num2] & (uint) ~BitString.bit(bitIndex));
      }
    }

    public virtual void setFalse(int bitIndex)
    {
      this.set(bitIndex, false);
    }

    public virtual void setTrue(int bitIndex)
    {
      this.set(bitIndex, true);
    }

    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder(this.length());
      for (int bitIndex = 0; bitIndex < this.length(); ++bitIndex)
        stringBuilder.Append(this.get(bitIndex) ? "1" : "0");
      return ((object) stringBuilder).ToString();
    }

    public virtual int trimmedLength()
    {
      int nbTrimmedBytes = this.getNbTrimmedBytes();
      if (nbTrimmedBytes == 0)
        return 0;
      else
        return (nbTrimmedBytes - 1) * BitString.BITS_PER_UNIT + (8 - this.getUnusedBits((int) this.bits[nbTrimmedBytes - 1]));
    }

    public virtual string trimmedToString()
    {
      StringBuilder stringBuilder = new StringBuilder(this.trimmedLength());
      for (int bitIndex = 0; bitIndex < this.trimmedLength(); ++bitIndex)
        stringBuilder.Append(this.get(bitIndex) ? "1" : "0");
      return ((object) stringBuilder).ToString();
    }

    private static int unitIndex(int bitIndex)
    {
      return bitIndex >> 3;
    }
  }
}
