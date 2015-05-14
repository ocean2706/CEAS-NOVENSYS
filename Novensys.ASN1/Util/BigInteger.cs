// Decompiled with JetBrains decompiler
// Type: Novensys.ASN1.Util.BigInteger
// Assembly: Novensys.eCard.SDK, Version=1.1.56.0, Culture=neutral, PublicKeyToken=null
// MVID: 59F9E456-D3DA-4066-A4A4-692B516A775E
// Assembly location: C:\Program Files (x86)\CNAS\SIUI-SPITAL\Novensys.eCard.SDK.dll

using System;

namespace Novensys.ASN1.Util
{
  [Serializable]
  public class BigInteger : ICloneable
  {
    private const int maxLength = 70;
    private uint[] data;
    private int dataLength;

    public BigInteger()
    {
      this.data = (uint[]) null;
      this.data = new uint[70];
      this.dataLength = 1;
    }

    public BigInteger(BigInteger bi)
    {
      this.data = (uint[]) null;
      this.data = new uint[70];
      this.dataLength = bi.dataLength;
      for (int index = 0; index < this.dataLength; ++index)
        this.data[index] = bi.data[index];
    }

    public BigInteger(long value)
    {
      this.data = (uint[]) null;
      this.data = new uint[70];
      long num = value;
      for (this.dataLength = 0; value != 0L && this.dataLength < 70; ++this.dataLength)
      {
        this.data[this.dataLength] = (uint) ((ulong) value & (ulong) uint.MaxValue);
        value >>= 32;
      }
      if (num > 0L)
      {
        if (value != 0L || ((int) this.data[69] & int.MinValue) != 0)
          throw new ArithmeticException("Positive overflow in constructor.");
      }
      else if (num < 0L && (value != -1L || ((int) this.data[this.dataLength - 1] & int.MinValue) == 0))
        throw new ArithmeticException("Negative underflow in constructor.");
      if (this.dataLength != 0)
        return;
      this.dataLength = 1;
    }

    public BigInteger(string value)
      : this(value, 10)
    {
    }

    public BigInteger(ulong value)
    {
      this.data = (uint[]) null;
      this.data = new uint[70];
      for (this.dataLength = 0; (long) value != 0L && this.dataLength < 70; ++this.dataLength)
      {
        this.data[this.dataLength] = (uint) (value & (ulong) uint.MaxValue);
        value >>= 32;
      }
      if ((long) value != 0L || ((int) this.data[69] & int.MinValue) != 0)
        throw new ArithmeticException("Positive overflow in constructor.");
      if (this.dataLength != 0)
        return;
      this.dataLength = 1;
    }

    public BigInteger(byte[] value)
    {
      this.data = (uint[]) null;
      if (value == null || value.Length == 0)
        throw new ArgumentException("parameter value is null or empty.");
      this.dataLength = value.Length >> 2;
      int num1 = value.Length & 3;
      if (num1 != 0)
        ++this.dataLength;
      if (this.dataLength > 70)
        throw new ArithmeticException("Byte overflow in constructor.");
      bool flag = ((int) value[0] & 128) == 128;
      this.data = new uint[70];
      int index1 = value.Length - 1;
      int index2 = 0;
      while (index1 >= 3)
      {
        this.data[index2] = (uint) (((int) value[index1 - 3] << 24) + ((int) value[index1 - 2] << 16) + ((int) value[index1 - 1] << 8)) + (uint) value[index1];
        index1 -= 4;
        ++index2;
      }
      switch (num1)
      {
        case 1:
          this.data[this.dataLength - 1] = (uint) value[0];
          if (flag)
          {
            IntPtr num2;
            this.data[(int) (num2 = (IntPtr) (this.dataLength - 1))] = this.data[(int) num2] | 4294967040U;
            break;
          }
          else
            break;
        case 2:
          this.data[this.dataLength - 1] = ((uint) value[0] << 8) + (uint) value[1];
          if (flag)
          {
            IntPtr num2;
            this.data[(int) (num2 = (IntPtr) (this.dataLength - 1))] = this.data[(int) num2] | 4294901760U;
            break;
          }
          else
            break;
        case 3:
          this.data[this.dataLength - 1] = (uint) (((int) value[0] << 16) + ((int) value[1] << 8)) + (uint) value[2];
          if (flag)
          {
            IntPtr num2;
            this.data[(int) (num2 = (IntPtr) (this.dataLength - 1))] = this.data[(int) num2] | 4278190080U;
            break;
          }
          else
            break;
      }
      if (flag)
      {
        for (int index3 = this.dataLength; index3 < 70; ++index3)
          this.data[index3] = uint.MaxValue;
        this.dataLength = 70;
      }
      else
      {
        while (this.dataLength > 1 && (int) this.data[this.dataLength - 1] == 0)
          --this.dataLength;
      }
    }

    public BigInteger(uint[] value)
    {
      this.data = (uint[]) null;
      this.dataLength = value.Length;
      if (this.dataLength > 70)
        throw new ArithmeticException("Byte overflow in constructor.");
      this.data = new uint[70];
      int index1 = this.dataLength - 1;
      int index2 = 0;
      while (index1 >= 0)
      {
        this.data[index2] = value[index1];
        --index1;
        ++index2;
      }
      while (this.dataLength > 1 && (int) this.data[this.dataLength - 1] == 0)
        --this.dataLength;
    }

    public BigInteger(string value, int radix)
    {
      this.data = (uint[]) null;
      if (value == null || value.Length == 0)
        throw new ArgumentException("parameter value is null or empty.");
      BigInteger bigInteger1 = new BigInteger(1L);
      BigInteger bigInteger2 = new BigInteger();
      value = value.ToUpper().Trim();
      bool flag = (int) value[0] == 45;
      int num1 = flag ? 1 : 0;
      for (int index = value.Length - 1; index >= num1; --index)
      {
        int num2 = (int) value[index];
        int num3 = num2 < 48 || num2 > 57 ? (num2 < 65 || num2 > 90 ? 9999999 : num2 - 65 + 10) : num2 - 48;
        if (num3 >= radix)
          throw new ArithmeticException("Invalid string in constructor.");
        bigInteger2 += bigInteger1 * (BigInteger) num3;
        if (index - 1 >= num1)
          bigInteger1 *= (BigInteger) radix;
      }
      if (flag)
      {
        if (bigInteger2.dataLength == 1 && (int) bigInteger2.data[0] == 0)
          flag = false;
        else
          bigInteger2 = -bigInteger2;
      }
      if (flag)
      {
        if (((int) bigInteger2.data[69] & int.MinValue) == 0)
          throw new ArithmeticException("Negative underflow in constructor.");
      }
      else if (((int) bigInteger2.data[69] & int.MinValue) != 0)
        throw new ArithmeticException("Positive overflow in constructor.");
      this.data = new uint[70];
      for (int index = 0; index < 70; ++index)
        this.data[index] = bigInteger2.data[index];
      this.dataLength = bigInteger2.dataLength;
    }

    public static implicit operator BigInteger(int value)
    {
      return new BigInteger((long) value);
    }

    public static implicit operator BigInteger(long value)
    {
      return new BigInteger(value);
    }

    public static implicit operator BigInteger(uint value)
    {
      return new BigInteger((ulong) value);
    }

    public static implicit operator BigInteger(ulong value)
    {
      return new BigInteger(value);
    }

    public static BigInteger operator +(BigInteger bi1, BigInteger bi2)
    {
      BigInteger bigInteger = new BigInteger();
      bigInteger.dataLength = bi1.dataLength > bi2.dataLength ? bi1.dataLength : bi2.dataLength;
      long num1 = 0L;
      for (int index = 0; index < bigInteger.dataLength; ++index)
      {
        long num2 = (long) (bi1.data[index] + bi2.data[index]) + num1;
        num1 = num2 >> 32;
        bigInteger.data[index] = (uint) ((ulong) num2 & (ulong) uint.MaxValue);
      }
      if (num1 != 0L && bigInteger.dataLength < 70)
      {
        bigInteger.data[bigInteger.dataLength] = (uint) num1;
        ++bigInteger.dataLength;
      }
      while (bigInteger.dataLength > 1 && (int) bigInteger.data[bigInteger.dataLength - 1] == 0)
        --bigInteger.dataLength;
      int index1 = 69;
      if (((int) bi1.data[index1] & int.MinValue) == ((int) bi2.data[index1] & int.MinValue) && ((int) bigInteger.data[index1] & int.MinValue) != ((int) bi1.data[index1] & int.MinValue))
        throw new ArithmeticException();
      else
        return bigInteger;
    }

    public static BigInteger operator &(BigInteger bi1, BigInteger bi2)
    {
      BigInteger bigInteger = new BigInteger();
      int num = bi1.dataLength > bi2.dataLength ? bi1.dataLength : bi2.dataLength;
      for (int index = 0; index < num; ++index)
        bigInteger.data[index] = bi1.data[index] & bi2.data[index];
      bigInteger.dataLength = 70;
      while (bigInteger.dataLength > 1 && (int) bigInteger.data[bigInteger.dataLength - 1] == 0)
        --bigInteger.dataLength;
      return bigInteger;
    }

    public static BigInteger operator |(BigInteger bi1, BigInteger bi2)
    {
      BigInteger bigInteger = new BigInteger();
      int num = bi1.dataLength > bi2.dataLength ? bi1.dataLength : bi2.dataLength;
      for (int index = 0; index < num; ++index)
        bigInteger.data[index] = bi1.data[index] | bi2.data[index];
      bigInteger.dataLength = 70;
      while (bigInteger.dataLength > 1 && (int) bigInteger.data[bigInteger.dataLength - 1] == 0)
        --bigInteger.dataLength;
      return bigInteger;
    }

    public static BigInteger operator --(BigInteger bi1)
    {
      BigInteger bigInteger = new BigInteger(bi1);
      bool flag = true;
      int index1;
      for (index1 = 0; flag && index1 < 70; ++index1)
      {
        long num = (long) bigInteger.data[index1] - 1L;
        bigInteger.data[index1] = (uint) ((ulong) num & (ulong) uint.MaxValue);
        if (num >= 0L)
          flag = false;
      }
      if (index1 > bigInteger.dataLength)
        bigInteger.dataLength = index1;
      while (bigInteger.dataLength > 1 && (int) bigInteger.data[bigInteger.dataLength - 1] == 0)
        --bigInteger.dataLength;
      int index2 = 69;
      if (((int) bi1.data[index2] & int.MinValue) != 0 && ((int) bigInteger.data[index2] & int.MinValue) != ((int) bi1.data[index2] & int.MinValue))
        throw new ArithmeticException("Underflow in --.");
      else
        return bigInteger;
    }

    public static BigInteger operator /(BigInteger bi1, BigInteger bi2)
    {
      BigInteger outQuotient = new BigInteger();
      BigInteger outRemainder = new BigInteger();
      int index = 69;
      bool flag1 = false;
      bool flag2 = false;
      if (((int) bi1.data[index] & int.MinValue) != 0)
      {
        bi1 = -bi1;
        flag2 = true;
      }
      if (((int) bi2.data[index] & int.MinValue) != 0)
      {
        bi2 = -bi2;
        flag1 = true;
      }
      if (bi1 >= bi2)
      {
        if (bi2.dataLength == 1)
          BigInteger.singleByteDivide(bi1, bi2, outQuotient, outRemainder);
        else
          BigInteger.multiByteDivide(bi1, bi2, outQuotient, outRemainder);
        if (flag2 != flag1)
          return -outQuotient;
      }
      return outQuotient;
    }

    public static BigInteger operator ^(BigInteger bi1, BigInteger bi2)
    {
      BigInteger bigInteger = new BigInteger();
      int num = bi1.dataLength > bi2.dataLength ? bi1.dataLength : bi2.dataLength;
      for (int index = 0; index < num; ++index)
        bigInteger.data[index] = bi1.data[index] ^ bi2.data[index];
      bigInteger.dataLength = 70;
      while (bigInteger.dataLength > 1 && (int) bigInteger.data[bigInteger.dataLength - 1] == 0)
        --bigInteger.dataLength;
      return bigInteger;
    }

    public static bool operator >(BigInteger bi1, BigInteger bi2)
    {
      int index1 = 69;
      if (((int) bi1.data[index1] & int.MinValue) != 0 && ((int) bi2.data[index1] & int.MinValue) == 0)
        return false;
      if (((int) bi1.data[index1] & int.MinValue) == 0 && ((int) bi2.data[index1] & int.MinValue) != 0)
        return true;
      int index2 = (bi1.dataLength > bi2.dataLength ? bi1.dataLength : bi2.dataLength) - 1;
      while (index2 >= 0 && (int) bi1.data[index2] == (int) bi2.data[index2])
        --index2;
      if (index2 < 0)
        return false;
      else
        return bi1.data[index2] > bi2.data[index2];
    }

    public static bool operator >=(BigInteger bi1, BigInteger bi2)
    {
      if (bi1 != bi2)
        return bi1 > bi2;
      else
        return true;
    }

    public static BigInteger operator ++(BigInteger bi1)
    {
      BigInteger bigInteger = new BigInteger(bi1);
      long num1 = 1L;
      int index1;
      for (index1 = 0; num1 != 0L && index1 < 70; ++index1)
      {
        long num2 = (long) bigInteger.data[index1] + 1L;
        bigInteger.data[index1] = (uint) ((ulong) num2 & (ulong) uint.MaxValue);
        num1 = num2 >> 32;
      }
      if (index1 <= bigInteger.dataLength)
      {
        while (bigInteger.dataLength > 1 && (int) bigInteger.data[bigInteger.dataLength - 1] == 0)
          --bigInteger.dataLength;
      }
      else
        bigInteger.dataLength = index1;
      int index2 = 69;
      if (((int) bi1.data[index2] & int.MinValue) == 0 && ((int) bigInteger.data[index2] & int.MinValue) != ((int) bi1.data[index2] & int.MinValue))
        throw new ArithmeticException("Overflow in ++.");
      else
        return bigInteger;
    }

    public static BigInteger operator <<(BigInteger bi1, int shiftVal)
    {
      BigInteger bigInteger = new BigInteger(bi1);
      bigInteger.dataLength = BigInteger.shiftLeft(bigInteger.data, shiftVal);
      return bigInteger;
    }

    public static bool operator <(BigInteger bi1, BigInteger bi2)
    {
      int index1 = 69;
      if (((int) bi1.data[index1] & int.MinValue) != 0 && ((int) bi2.data[index1] & int.MinValue) == 0)
        return true;
      if (((int) bi1.data[index1] & int.MinValue) == 0 && ((int) bi2.data[index1] & int.MinValue) != 0)
        return false;
      int index2 = (bi1.dataLength > bi2.dataLength ? bi1.dataLength : bi2.dataLength) - 1;
      while (index2 >= 0 && (int) bi1.data[index2] == (int) bi2.data[index2])
        --index2;
      if (index2 < 0)
        return false;
      else
        return bi1.data[index2] < bi2.data[index2];
    }

    public static bool operator <=(BigInteger bi1, BigInteger bi2)
    {
      if (bi1 != bi2)
        return bi1 < bi2;
      else
        return true;
    }

    public static BigInteger operator %(BigInteger bi1, BigInteger bi2)
    {
      BigInteger outQuotient = new BigInteger();
      BigInteger outRemainder = new BigInteger(bi1);
      int index = 69;
      bool flag = false;
      if (((int) bi1.data[index] & int.MinValue) != 0)
      {
        bi1 = -bi1;
        flag = true;
      }
      if (((int) bi2.data[index] & int.MinValue) != 0)
        bi2 = -bi2;
      if (bi1 >= bi2)
      {
        if (bi2.dataLength == 1)
          BigInteger.singleByteDivide(bi1, bi2, outQuotient, outRemainder);
        else
          BigInteger.multiByteDivide(bi1, bi2, outQuotient, outRemainder);
        if (flag)
          return -outRemainder;
      }
      return outRemainder;
    }

    public static BigInteger operator *(BigInteger bi1, BigInteger bi2)
    {
      int index1 = 69;
      bool flag1 = false;
      bool flag2 = false;
      try
      {
        if (((int) bi1.data[index1] & int.MinValue) != 0)
        {
          flag1 = true;
          bi1 = -bi1;
        }
        if (((int) bi2.data[index1] & int.MinValue) != 0)
        {
          flag2 = true;
          bi2 = -bi2;
        }
      }
      catch (Exception ex)
      {
      }
      BigInteger bigInteger = new BigInteger();
      try
      {
        for (int index2 = 0; index2 < bi1.dataLength; ++index2)
        {
          if ((int) bi1.data[index2] != 0)
          {
            ulong num1 = 0UL;
            int index3 = 0;
            int index4 = index2;
            while (index3 < bi2.dataLength)
            {
              ulong num2 = (ulong) (bi1.data[index2] * bi2.data[index3] + bigInteger.data[index4]) + num1;
              bigInteger.data[index4] = (uint) (num2 & (ulong) uint.MaxValue);
              num1 = num2 >> 32;
              ++index3;
              ++index4;
            }
            if ((long) num1 != 0L)
              bigInteger.data[index2 + bi2.dataLength] = (uint) num1;
          }
        }
      }
      catch (Exception ex)
      {
        throw new ArithmeticException("Multiplication overflow.");
      }
      bigInteger.dataLength = bi1.dataLength + bi2.dataLength;
      if (bigInteger.dataLength > 70)
        bigInteger.dataLength = 70;
      while (bigInteger.dataLength > 1 && (int) bigInteger.data[bigInteger.dataLength - 1] == 0)
        --bigInteger.dataLength;
      if (((int) bigInteger.data[index1] & int.MinValue) != 0)
      {
        if (flag1 != flag2 && (int) bigInteger.data[index1] == int.MinValue)
        {
          if (bigInteger.dataLength == 1)
            return bigInteger;
          bool flag3 = true;
          for (int index2 = 0; index2 < bigInteger.dataLength - 1 && flag3; ++index2)
          {
            if ((int) bigInteger.data[index2] != 0)
              flag3 = false;
          }
          if (flag3)
            return bigInteger;
        }
        throw new ArithmeticException("Multiplication overflow.");
      }
      else if (flag1 != flag2)
        return -bigInteger;
      else
        return bigInteger;
    }

    public static BigInteger operator ~(BigInteger bi1)
    {
      BigInteger bigInteger = new BigInteger(bi1);
      for (int index = 0; index < 70; ++index)
        bigInteger.data[index] = ~bi1.data[index];
      bigInteger.dataLength = 70;
      while (bigInteger.dataLength > 1 && (int) bigInteger.data[bigInteger.dataLength - 1] == 0)
        --bigInteger.dataLength;
      return bigInteger;
    }

    public static BigInteger operator >>(BigInteger bi1, int shiftVal)
    {
      BigInteger bigInteger = new BigInteger(bi1);
      bigInteger.dataLength = BigInteger.shiftRight(bigInteger.data, shiftVal);
      if (((int) bi1.data[69] & int.MinValue) != 0)
      {
        for (int index = 69; index >= bigInteger.dataLength; --index)
          bigInteger.data[index] = uint.MaxValue;
        uint num1 = (uint) uint.MinValue;
        for (int index = 0; index < 32 && ((int) bigInteger.data[bigInteger.dataLength - 1] & (int) num1) == 0; ++index)
        {
          IntPtr num2;
          bigInteger.data[(int) (num2 = (IntPtr) (bigInteger.dataLength - 1))] = bigInteger.data[(int) num2] | num1;
          num1 >>= 1;
        }
        bigInteger.dataLength = 70;
      }
      return bigInteger;
    }

    public static BigInteger operator -(BigInteger bi1, BigInteger bi2)
    {
      BigInteger bigInteger = new BigInteger();
      bigInteger.dataLength = bi1.dataLength > bi2.dataLength ? bi1.dataLength : bi2.dataLength;
      long num1 = 0L;
      for (int index = 0; index < bigInteger.dataLength; ++index)
      {
        long num2 = (long) (bi1.data[index] - bi2.data[index]) - num1;
        bigInteger.data[index] = (uint) ((ulong) num2 & (ulong) uint.MaxValue);
        num1 = num2 >= 0L ? 0L : 1L;
      }
      if (num1 != 0L)
      {
        for (int index = bigInteger.dataLength; index < 70; ++index)
          bigInteger.data[index] = uint.MaxValue;
        bigInteger.dataLength = 70;
      }
      while (bigInteger.dataLength > 1 && (int) bigInteger.data[bigInteger.dataLength - 1] == 0)
        --bigInteger.dataLength;
      int index1 = 69;
      if (((int) bi1.data[index1] & int.MinValue) != ((int) bi2.data[index1] & int.MinValue) && ((int) bigInteger.data[index1] & int.MinValue) != ((int) bi1.data[index1] & int.MinValue))
        throw new ArithmeticException();
      else
        return bigInteger;
    }

    public static BigInteger operator -(BigInteger bi1)
    {
      if (bi1.dataLength == 1 && (int) bi1.data[0] == 0)
        return new BigInteger();
      BigInteger bigInteger = new BigInteger(bi1);
      for (int index = 0; index < 70; ++index)
        bigInteger.data[index] = ~bi1.data[index];
      long num1 = 1L;
      for (int index = 0; num1 != 0L && index < 70; ++index)
      {
        long num2 = (long) bigInteger.data[index] + 1L;
        bigInteger.data[index] = (uint) ((ulong) num2 & (ulong) uint.MaxValue);
        num1 = num2 >> 32;
      }
      if (((int) bi1.data[69] & int.MinValue) == ((int) bigInteger.data[69] & int.MinValue))
        throw new ArithmeticException("Overflow in negation.\n");
      bigInteger.dataLength = 70;
      while (bigInteger.dataLength > 1 && (int) bigInteger.data[bigInteger.dataLength - 1] == 0)
        --bigInteger.dataLength;
      return bigInteger;
    }

    public BigInteger Abs()
    {
      if (((int) this.data[69] & int.MinValue) != 0)
        return -this;
      else
        return new BigInteger(this);
    }

    public object Clone()
    {
      return this.MemberwiseClone();
    }

    public override bool Equals(object anObject)
    {
      if (this != anObject)
      {
        if (!(anObject is BigInteger))
          return false;
        BigInteger bigInteger = (BigInteger) anObject;
        if (this.dataLength != bigInteger.dataLength)
          return false;
        for (int index = 0; index < this.dataLength; ++index)
        {
          if ((int) this.data[index] != (int) bigInteger.data[index])
            return false;
        }
      }
      return true;
    }

    public byte[] GetBytes()
    {
      bool flag1 = ((int) this.data[69] & int.MinValue) != 0;
      int num1 = this.dataLength;
      if (!flag1)
      {
        while (num1 > 1 && (int) this.data[num1 - 1] == 0 && ((int) this.data[num1 - 2] & int.MinValue) == 0)
          --num1;
      }
      else
      {
        while (num1 > 1 && (int) this.data[num1 - 1] == -1 && ((int) this.data[num1 - 2] & int.MinValue) != 0)
          --num1;
      }
      uint num2 = this.data[num1 - 1];
      uint num3 = num2;
      byte num4 = !flag1 ? (byte) 0 : byte.MaxValue;
      byte num5 = !flag1 ? (byte) 0 : (byte) sbyte.MinValue;
      int num6 = 1;
      int num7 = 1;
      while (num7 < 4)
      {
        bool flag2 = (int) (byte) (num3 & 128U) == (int) num5;
        num3 >>= 8;
        ++num7;
        bool flag3 = (int) (byte) (num3 & (uint) byte.MaxValue) == (int) num4;
        if (!flag2 || !flag3)
          num6 = num7;
      }
      int length = num6 + (num1 - 1 << 2);
      if (!flag1 && ((int) num2 & int.MinValue) != 0)
        ++length;
      byte[] numArray = new byte[length];
      int num8 = 0;
      if (!flag1 && ((int) num2 & int.MinValue) != 0)
        numArray[num8++] = (byte) 0;
      int num9 = 0;
      uint num10 = num2;
      for (int index = 1; index <= num6; ++index)
      {
        int num11 = num6 - index << 3;
        numArray[num8++] = (byte) (num10 >> num11 & (uint) byte.MaxValue);
      }
      for (int index1 = num1 - 2; index1 >= 0; --index1)
      {
        uint num11 = this.data[index1];
        num9 = 0;
        for (int index2 = 1; index2 <= 4; ++index2)
        {
          int num12 = 4 - index2 << 3;
          numArray[num8++] = (byte) (num11 >> num12 & (uint) byte.MaxValue);
        }
      }
      return numArray;
    }

    public override int GetHashCode()
    {
      return base.ToString().GetHashCode();
    }

    public int GetIntValue()
    {
      return (int) this.data[0];
    }

    public BigInteger Max(BigInteger bi)
    {
      if (this > bi)
        return new BigInteger(this);
      else
        return new BigInteger(bi);
    }

    public BigInteger Min(BigInteger bi)
    {
      if (this < bi)
        return new BigInteger(this);
      else
        return new BigInteger(bi);
    }

    private static void multiByteDivide(BigInteger bi1, BigInteger bi2, BigInteger outQuotient, BigInteger outRemainder)
    {
      uint[] numArray1 = new uint[70];
      int length1 = bi1.dataLength + 1;
      uint[] buffer = new uint[length1];
      uint num1 = (uint) int.MinValue;
      uint num2 = bi2.data[bi2.dataLength - 1];
      int shiftVal = 0;
      int num3 = 0;
      while ((int) num1 != 0 && ((int) num2 & (int) num1) == 0)
      {
        ++shiftVal;
        num1 >>= 1;
      }
      for (int index = 0; index < bi1.dataLength; ++index)
        buffer[index] = bi1.data[index];
      BigInteger.shiftLeft(buffer, shiftVal);
      bi2 <<= shiftVal;
      int num4 = length1 - bi2.dataLength;
      int index1 = length1 - 1;
      ulong num5 = (ulong) bi2.data[bi2.dataLength - 1];
      ulong num6 = (ulong) bi2.data[bi2.dataLength - 2];
      int length2 = bi2.dataLength + 1;
      uint[] numArray2 = new uint[length2];
      for (; num4 > 0; --num4)
      {
        ulong num7 = (ulong) (buffer[index1] + buffer[index1 - 1]);
        ulong num8 = num7 / num5;
        ulong num9 = num7 % num5;
        bool flag = false;
        while (!flag)
        {
          flag = true;
          if ((long) num8 == 4294967296L || num8 * num6 > (num9 << 32) + (ulong) buffer[index1 - 2])
          {
            --num8;
            num9 += num5;
            if (num9 < 4294967296UL)
              flag = false;
          }
        }
        for (int index2 = 0; index2 < length2; ++index2)
          numArray2[index2] = buffer[index1 - index2];
        BigInteger bigInteger1 = new BigInteger(numArray2);
        BigInteger bigInteger2 = bi2 * (BigInteger) num8;
        while (bigInteger2 > bigInteger1)
        {
          --num8;
          bigInteger2 -= bi2;
        }
        BigInteger bigInteger3 = bigInteger1 - bigInteger2;
        for (int index2 = 0; index2 < length2; ++index2)
          buffer[index1 - index2] = bigInteger3.data[bi2.dataLength - index2];
        numArray1[num3++] = (uint) num8;
        --index1;
      }
      outQuotient.dataLength = num3;
      int index3 = 0;
      int index4 = outQuotient.dataLength - 1;
      while (index4 >= 0)
      {
        outQuotient.data[index3] = numArray1[index4];
        --index4;
        ++index3;
      }
      for (; index3 < 70; ++index3)
        outQuotient.data[index3] = 0U;
      while (outQuotient.dataLength > 1 && (int) outQuotient.data[outQuotient.dataLength - 1] == 0)
        --outQuotient.dataLength;
      if (outQuotient.dataLength == 0)
        outQuotient.dataLength = 1;
      outRemainder.dataLength = BigInteger.shiftRight(buffer, shiftVal);
      int index5;
      for (index5 = 0; index5 < outRemainder.dataLength; ++index5)
        outRemainder.data[index5] = buffer[index5];
      for (; index5 < 70; ++index5)
        outRemainder.data[index5] = 0U;
    }

    private static int shiftLeft(uint[] buffer, int shiftVal)
    {
      int num1 = 32;
      int length = buffer.Length;
      while (length > 1 && (int) buffer[length - 1] == 0)
        --length;
      int num2 = shiftVal;
      while (num2 > 0)
      {
        if (num2 < num1)
          num1 = num2;
        ulong num3 = 0UL;
        for (int index = 0; index < length; ++index)
        {
          ulong num4 = (ulong) (buffer[index] << num1) | num3;
          buffer[index] = (uint) (num4 & (ulong) uint.MaxValue);
          num3 = num4 >> 32;
        }
        if ((long) num3 != 0L && length + 1 <= buffer.Length)
        {
          buffer[length] = (uint) num3;
          ++length;
        }
        num2 -= num1;
      }
      return length;
    }

    private static int shiftRight(uint[] buffer, int shiftVal)
    {
      int num1 = 32;
      int num2 = 0;
      int length = buffer.Length;
      while (length > 1 && (int) buffer[length - 1] == 0)
        --length;
      int num3 = shiftVal;
      while (num3 > 0)
      {
        if (num3 < num1)
        {
          num1 = num3;
          num2 = 32 - num1;
        }
        ulong num4 = 0UL;
        for (int index = length - 1; index >= 0; --index)
        {
          ulong num5 = (ulong) (buffer[index] >> num1) | num4;
          num4 = (ulong) (buffer[index] << num2);
          buffer[index] = (uint) num5;
        }
        num3 -= num1;
      }
      while (length > 1 && (int) buffer[length - 1] == 0)
        --length;
      return length;
    }

    public int Signum()
    {
      if (this.dataLength == 1 && (int) this.data[0] == 0)
        return 0;
      return ((int) this.data[69] & int.MinValue) != 0 ? -1 : 1;
    }

    private int for1idx(int i)
    {
       
        ulong num4;
        outRemainder.data[index2--] = (uint) (num4 % num2);
      
    }
    private static void singleByteDivide(BigInteger bi1, BigInteger bi2, BigInteger outQuotient, BigInteger outRemainder)
    {
      uint[] numArray = new uint[70];
      int num1 = 0;
      for (int index = 0; index < 70; ++index)
        outRemainder.data[index] = bi1.data[index];
      outRemainder.dataLength = bi1.dataLength;
      while (outRemainder.dataLength > 1 && (int) outRemainder.data[outRemainder.dataLength - 1] == 0)
        --outRemainder.dataLength;
      ulong num2 = (ulong) bi2.data[0];
      int index1 = outRemainder.dataLength - 1;
      ulong num3 = (ulong) outRemainder.data[index1];
      if (num3 >= num2)
      {
        ulong num4 = num3 / num2;
        numArray[num1++] = (uint) num4;
        outRemainder.data[index1] = (uint) (num3 % num2);
      }
      for (int index2 = index1 - 1; index2 >= 
      )
      {
        num4 = (ulong) (outRemainder.data[index2 + 1] + outRemainder.data[index2]);
        ulong num4 = num4 / num2;
        numArray[num1++] = (uint) num4;
        outRemainder.data[index2 + 1] = 0U;
      }
      outQuotient.dataLength = num1;
      int index3 = 0;
      int index4 = outQuotient.dataLength - 1;
      while (index4 >= 0)
      {
        outQuotient.data[index3] = numArray[index4];
        --index4;
        ++index3;
      }
      for (; index3 < 70; ++index3)
        outQuotient.data[index3] = 0U;
      while (outQuotient.dataLength > 1 && (int) outQuotient.data[outQuotient.dataLength - 1] == 0)
        --outQuotient.dataLength;
      if (outQuotient.dataLength == 0)
        outQuotient.dataLength = 1;
      while (outRemainder.dataLength > 1 && (int) outRemainder.data[outRemainder.dataLength - 1] == 0)
        --outRemainder.dataLength;
    }

    public override string ToString()
    {
      return this.ToString(10);
    }

    public string ToString(int radix)
    {
      if (radix < 2 || radix > 36)
        throw new ArgumentException("Radix must be >= 2 and <= 36");
      string str1 = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
      string str2 = "";
      BigInteger bi1 = this;
      bool flag = false;
      if (((int) bi1.data[69] & int.MinValue) != 0)
      {
        flag = true;
        try
        {
          bi1 = -bi1;
        }
        catch (Exception ex)
        {
        }
      }
      BigInteger outQuotient = new BigInteger();
      BigInteger outRemainder = new BigInteger();
      BigInteger bi2 = new BigInteger((long) radix);
      if (bi1.dataLength == 1 && (int) bi1.data[0] == 0)
        return "0";
      for (; bi1.dataLength > 1 || bi1.dataLength == 1 && (int) bi1.data[0] != 0; bi1 = outQuotient)
      {
        BigInteger.singleByteDivide(bi1, bi2, outQuotient, outRemainder);
        str2 = outRemainder.data[0] >= 10U ? (string) (object) str1[(int) outRemainder.data[0] - 10] + (object) str2 : (string) (object) outRemainder.data[0] + (object) str2;
      }
      if (flag)
        str2 = "-" + str2;
      return str2;
    }
  }
}
