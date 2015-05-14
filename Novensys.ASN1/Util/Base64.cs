// Decompiled with JetBrains decompiler
// Type: Novensys.ASN1.Util.Base64
// Assembly: Novensys.eCard.SDK, Version=1.1.56.0, Culture=neutral, PublicKeyToken=null
// MVID: 59F9E456-D3DA-4066-A4A4-692B516A775E
// Assembly location: C:\Program Files (x86)\CNAS\SIUI-SPITAL\Novensys.eCard.SDK.dll

using System;

namespace Novensys.ASN1.Util
{
  public class Base64
  {
    private static char[] CA = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/".ToCharArray();
    private static int[] IA = new int[256];

    static Base64()
    {
      int index1 = 0;
      for (int length = Base64.IA.Length; index1 < length; ++index1)
        Base64.IA[index1] = -1;
      int index2 = 0;
      for (int length = Base64.CA.Length; index2 < length; ++index2)
        Base64.IA[(int) Base64.CA[index2]] = index2;
      Base64.IA[61] = 0;
    }

    public static byte[] Decode(string str)
    {
      int num1 = str != null ? str.Length : 0;
      if (num1 == 0)
        return new byte[0];
      int num2 = 0;
      for (int index = 0; index < num1; ++index)
      {
        if (Base64.IA[(int) str[index]] < 0)
          ++num2;
      }
      if ((num1 - num2) % 4 != 0)
        throw new ArgumentException("Legal characters count is not divisible by 4.");
      int num3 = 0;
      int index1 = num1;
      while (index1 > 1 && Base64.IA[(int) str[--index1]] <= 0)
      {
        if ((int) str[index1] == 61)
          ++num3;
      }
      int length = ((num1 - num2) * 6 >> 3) - num3;
      byte[] numArray = new byte[length];
      int num4 = 0;
      int num5 = 0;
      while (num5 < length)
      {
        int num6 = 0;
        for (int index2 = 0; index2 < 4; ++index2)
        {
          int num7 = Base64.IA[(int) str[num4++]];
          if (num7 >= 0)
            num6 |= num7 << 18 - index2 * 6;
          else
            --index2;
        }
        numArray[num5++] = (byte) (num6 >> 16);
        if (num5 < length)
        {
          numArray[num5++] = (byte) (num6 >> 8);
          if (num5 < length)
            numArray[num5++] = (byte) num6;
        }
      }
      return numArray;
    }

    public static string Encode(byte[] bytes, bool lineSeparator)
    {
      return new string(Base64.EncodeToChar(bytes, lineSeparator));
    }

    public static char[] EncodeToChar(byte[] bytes, bool lineSeparator)
    {
      int num1 = bytes != null ? bytes.Length : 0;
      if (num1 == 0)
        return new char[0];
      int index1 = num1 / 3 * 3;
      int num2 = (num1 - 1) / 3 + 1 << 2;
      int length = num2 + (lineSeparator ? (num2 - 1) / 76 << 1 : 0);
      char[] chArray1 = new char[length];
      int num3 = 0;
      int num4 = 0;
      int num5 = 0;
      while (num3 < index1)
      {
        byte[] numArray1 = bytes;
        int index2 = num3;
        int num6 = 1;
        int num7 = index2 + num6;
        int num8 = ((int) numArray1[index2] & (int) byte.MaxValue) << 16;
        byte[] numArray2 = bytes;
        int index3 = num7;
        int num9 = 1;
        int num10 = index3 + num9;
        int num11 = ((int) numArray2[index3] & (int) byte.MaxValue) << 8;
        int num12 = num8 | num11;
        byte[] numArray3 = bytes;
        int index4 = num10;
        int num13 = 1;
        num3 = index4 + num13;
        int num14 = (int) numArray3[index4] & (int) byte.MaxValue;
        int number = num12 | num14;
        char[] chArray2 = chArray1;
        int index5 = num4;
        int num15 = 1;
        int num16 = index5 + num15;
        int num17 = (int) Base64.CA[Tools.URShift(number, 18) & 63];
        chArray2[index5] = (char) num17;
        char[] chArray3 = chArray1;
        int index6 = num16;
        int num18 = 1;
        int num19 = index6 + num18;
        int num20 = (int) Base64.CA[Tools.URShift(number, 12) & 63];
        chArray3[index6] = (char) num20;
        char[] chArray4 = chArray1;
        int index7 = num19;
        int num21 = 1;
        int num22 = index7 + num21;
        int num23 = (int) Base64.CA[Tools.URShift(number, 6) & 63];
        chArray4[index7] = (char) num23;
        char[] chArray5 = chArray1;
        int index8 = num22;
        int num24 = 1;
        num4 = index8 + num24;
        int num25 = (int) Base64.CA[number & 63];
        chArray5[index8] = (char) num25;
        if (lineSeparator && ++num5 == 19 && num4 < length - 2)
        {
          char[] chArray6 = chArray1;
          int index9 = num4;
          int num26 = 1;
          int num27 = index9 + num26;
          int num28 = 13;
          chArray6[index9] = (char) num28;
          char[] chArray7 = chArray1;
          int index10 = num27;
          int num29 = 1;
          num4 = index10 + num29;
          int num30 = 10;
          chArray7[index10] = (char) num30;
          num5 = 0;
        }
      }
      int num31 = num1 - index1;
      if (num31 > 0)
      {
        int number = ((int) bytes[index1] & (int) byte.MaxValue) << 10 | (num31 == 2 ? ((int) bytes[num1 - 1] & (int) byte.MaxValue) << 2 : 0);
        chArray1[length - 4] = Base64.CA[number >> 12];
        chArray1[length - 3] = Base64.CA[Tools.URShift(number, 6) & 63];
        chArray1[length - 2] = num31 == 2 ? Base64.CA[number & 63] : '=';
        chArray1[length - 1] = '=';
      }
      return chArray1;
    }
  }
}
