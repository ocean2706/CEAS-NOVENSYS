// Decompiled with JetBrains decompiler
// Type: Novensys.ASN1.Util.Tools
// Assembly: Novensys.eCard.SDK, Version=1.1.56.0, Culture=neutral, PublicKeyToken=null
// MVID: 59F9E456-D3DA-4066-A4A4-692B516A775E
// Assembly location: C:\Program Files (x86)\CNAS\SIUI-SPITAL\Novensys.eCard.SDK.dll

using System;
using System.Globalization;
using System.Text;

namespace Novensys.ASN1.Util
{
  public class Tools
  {
    public const int k_128 = 128;
    public const int k_16 = 16;
    public const int k_16K = 16384;
    public const int k_2 = 2;
    public const int k_256 = 256;
    public const int k_2K = 2048;
    public const int k_32K = 32768;
    public const int k_48K = 49152;
    public const int k_64 = 64;
    public const int k_64K = 65536;
    public const int k_8K = 8192;

    public static int abs(int val)
    {
      if (val >= 0)
        return val;
      else
        return -val;
    }

    public static long fractionalPartAsLong(double val, int nbDigits)
    {
      StringBuilder stringBuilder = new StringBuilder(Tools.fractionalPartAsString(val));
      int length = stringBuilder.Length;
      if (nbDigits > length)
      {
        for (int index = 0; index < nbDigits - length; ++index)
          stringBuilder.Append("0");
      }
      return long.Parse(((object) stringBuilder).ToString().Substring(0, nbDigits));
    }

    public static string fractionalPartAsString(double val)
    {
      if (val < 0.0 || val >= 1.0)
        throw new ArgumentException("double must be between 0 and 1 excluded");
      string str1 = val.ToString("#.################E00", (IFormatProvider) CultureInfo.InvariantCulture);
      int length1 = str1.IndexOf(NumberFormatInfo.InvariantInfo.NumberDecimalSeparator);
      int length2 = str1.IndexOf('E');
      string str2 = length1 < 0 ? str1.Substring(0, length2) : str1.Substring(0, length1) + str1.Substring(length1 + 1, length2 - length1 - 1);
      int num1 = str1.IndexOf(NumberFormatInfo.InvariantInfo.NegativeSign);
      int num2 = num1 >= 0 ? int.Parse(str1.Substring(num1 + 1)) : 0;
      StringBuilder stringBuilder = new StringBuilder();
      for (int index = 0; index < num2 - 1; ++index)
        stringBuilder.Append("0");
      return ((object) stringBuilder.Append(str2)).ToString();
    }

    public static string fractionalPartAsString(double val, int nbDigits)
    {
      StringBuilder stringBuilder = new StringBuilder(Tools.fractionalPartAsString(val));
      int length = stringBuilder.Length;
      if (nbDigits > length)
      {
        for (int index = 0; index < nbDigits - length; ++index)
          stringBuilder.Append("0");
      }
      return ((object) stringBuilder).ToString().Substring(0, nbDigits);
    }

    public static int fractionalPartLength(double val)
    {
      if (val < 0.0 || val >= 1.0)
        throw new ArgumentException("double must be between 0 and 1 excluded.");
      int num1 = 0;
      string str = val.ToString("#.################E00", (IFormatProvider) CultureInfo.InvariantCulture);
      if (str.Equals("1E00"))
        throw new ArgumentException("double.ToString() must not be 1E00.");
      int num2 = str.IndexOf(NumberFormatInfo.InvariantInfo.NumberDecimalSeparator);
      if (num2 >= 0)
        num1 += str.IndexOf('E') - (num2 + 1);
      int num3 = str.IndexOf(NumberFormatInfo.InvariantInfo.NegativeSign);
      if (num3 >= 0)
        num1 += int.Parse(str.Substring(num3 + 1));
      return num1;
    }

    public static double fractionalPartLongToDouble(long val, int nbDigits)
    {
      return double.Parse(val.ToString() + "E-" + nbDigits.ToString());
    }

    public static byte[] get7BitsArrayFrom8BitsArray(byte[] ba)
    {
      if (ba == null)
        return (byte[]) null;
      if (ba.Length == 0)
        return ba;
      int length1 = ba.Length;
      int length2 = (length1 << 3) / 7;
      int num1 = 8 - (length1 << 3) % 7;
      if (((int) ba[0] & (int) byte.MaxValue) >> num1 != 0)
        ++length2;
      byte[] numArray = new byte[length2];
      int num2 = 0;
      for (int index1 = length2 - 1; index1 >= 0; --index1)
      {
        int index2 = length1 - num2 / 8 - 1;
        int num3 = num2 % 8;
        byte num4;
        if (num3 == 0)
        {
          num4 = (byte) ((uint) ba[index2] & (uint) sbyte.MaxValue);
        }
        else
        {
          int num5 = ((int) ba[index2] & (int) byte.MaxValue) >> num3;
          if (index2 >= 1)
          {
            int num6 = (int) byte.MaxValue >> 8 - num3 + 1;
            int num7 = ((int) ba[index2 - 1] & num6) << 8 - num3;
            num4 = (byte) (num5 | num7);
          }
          else
            num4 = (byte) num5;
        }
        numArray[index1] = index1 == length2 - 1 ? num4 : (byte) ((uint) num4 | 128U);
        num2 += 7;
      }
      return numArray;
    }

    public static byte[] get7BitsArrayFromLong(long l)
    {
      if (l < 0L)
        return (byte[]) null;
      long number = l;
      int length = (Tools.nbBitsForPositiveNumber(number) + 6) / 7;
      byte[] numArray = new byte[length];
      for (int index = length - 1; index >= 0; --index)
      {
        byte num = (byte) ((ulong) number & (ulong) sbyte.MaxValue);
        numArray[index] = index < length - 1 ? (byte) ((uint) num | 128U) : num;
        number = Tools.URShift(number, 7);
      }
      return numArray;
    }

    public static byte[] get8BitsArrayFrom7BitsArray(byte[] ba)
    {
      return Tools.get8BitsArrayFrom7BitsArray(ba, 0, ba.Length);
    }

    public static byte[] get8BitsArrayFrom7BitsArray(byte[] ba, int index, int length)
    {
      if (ba == null)
        return (byte[]) null;
      if (ba.Length == 0)
        return ba;
      int num1 = length * 7;
      int length1 = num1 / 8;
      if (num1 % 8 != 0)
        ++length1;
      byte[] numArray = new byte[length1];
      int num2 = 0;
      for (int index1 = index + length - 1; index1 >= index; --index1)
      {
        int index2 = length1 - num2 / 8 - 1;
        int num3 = num2 % 8;
        if (num3 == 0)
        {
          numArray[index2] = (byte) ((uint) ba[index1] & (uint) sbyte.MaxValue);
        }
        else
        {
          int num4 = ((int) ba[index1] & (int) sbyte.MaxValue) << num3;
          numArray[index2] = (byte) ((uint) numArray[index2] | (uint) num4);
          int num5 = ((int) ba[index1] & (int) sbyte.MaxValue) >> 8 - num3;
          numArray[index2 - 1] = (byte) num5;
        }
        num2 += 7;
      }
      return numArray;
    }

    public static byte[] getByteArrayFrom8BitsChars(string str)
    {
      if (str == null)
        return (byte[]) null;
      char[] chArray = str.ToCharArray();
      byte[] numArray = new byte[chArray.Length];
      for (int index = 0; index < numArray.Length; ++index)
        numArray[index] = (byte) chArray[index];
      return numArray;
    }

    public static long getLongFrom7BitsArray(byte[] ba, int index, int length)
    {
      if (ba == null || ba.Length == 0)
        return -1L;
      long num = 0L;
      for (int index1 = index; index1 < index + length; ++index1)
        num = (num << 7) + (long) ((int) ba[index1] & (int) sbyte.MaxValue);
      return num;
    }

    public static long maxLong(long[] ls)
    {
      if (ls == null || ls.Length == 0)
        throw new ArgumentException("parameter should not be null.");
      long num = ls[0];
      for (int index = 1; index < ls.Length; ++index)
      {
        if (ls[index] > num)
          num = ls[index];
      }
      return num;
    }

    public static int nbBitsForPositiveNumber(long number)
    {
      int num = Tools.nbBytesForPositiveNumber(number) << 3;
      long number1 = 1L << num - 1;
      while ((number & number1) == 0L && number1 != 1L)
      {
        number1 = Tools.URShift(number1, 1);
        --num;
      }
      return num;
    }

    public static int nbBytesForNumber(long number)
    {
      long num1 = number;
      byte num2 = num1 >= 0L ? (byte) 0 : byte.MaxValue;
      byte num3 = num1 >= 0L ? (byte) 0 : (byte) sbyte.MinValue;
      int num4 = 1;
      int num5 = 1;
      while (num5 < 8)
      {
        bool flag1 = (int) (byte) ((ulong) num1 & 128UL) == (int) num3;
        num1 >>= 8;
        ++num5;
        bool flag2 = (int) (byte) ((ulong) num1 & (ulong) byte.MaxValue) == (int) num2;
        if (!flag1 || !flag2)
          num4 = num5;
      }
      return num4;
    }

    public static int nbBytesForPositiveNumber(long number)
    {
      int num = 8;
      ulong number1 = 18374686479671623680UL;
      while ((number & (long) number1) == 0L && (long) number1 != (long) byte.MaxValue)
      {
        number1 = Tools.URShift(number1, 8);
        --num;
      }
      return num;
    }

    public static double tenPower(long exponent)
    {
      double num = 1.0;
      if (exponent >= 0L)
      {
        for (long index = 0L; index < exponent; ++index)
          num *= 10.0;
        return num;
      }
      else
      {
        for (long index = exponent; index < 0L; ++index)
          num /= 10.0;
        return num;
      }
    }

    public static byte[] ToByteArray(sbyte[] sbyteArray)
    {
      byte[] numArray = new byte[sbyteArray.Length];
      for (int index = 0; index < sbyteArray.Length; ++index)
        numArray[index] = (byte) sbyteArray[index];
      return numArray;
    }

    public static sbyte[] ToSByteArray(byte[] byteArray)
    {
      sbyte[] numArray = new sbyte[byteArray.Length];
      for (int index = 0; index < byteArray.Length; ++index)
        numArray[index] = (sbyte) byteArray[index];
      return numArray;
    }

    public static int URShift(int number, int bits)
    {
      if (number >= 0)
        return number >> bits;
      else
        return (number >> bits) + (2 << ~bits);
    }

    public static int URShift(int number, long bits)
    {
      return Tools.URShift(number, (int) bits);
    }

    public static long URShift(long number, int bits)
    {
      if (number >= 0L)
        return number >> bits;
      else
        return (number >> bits) + (2L << ~bits);
    }

    public static long URShift(long number, long bits)
    {
      return Tools.URShift(number, (int) bits);
    }

    public static ulong URShift(ulong number, int bits)
    {
      return number >> bits;
    }
  }
}
