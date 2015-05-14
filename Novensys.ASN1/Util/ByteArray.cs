// Decompiled with JetBrains decompiler
// Type: Novensys.ASN1.Util.ByteArray
// Assembly: Novensys.eCard.SDK, Version=1.1.56.0, Culture=neutral, PublicKeyToken=null
// MVID: 59F9E456-D3DA-4066-A4A4-692B516A775E
// Assembly location: C:\Program Files (x86)\CNAS\SIUI-SPITAL\Novensys.eCard.SDK.dll

using System;
using System.Collections;
using System.Text;

namespace Novensys.ASN1.Util
{
  public class ByteArray
  {
    private const int BIT_PER_BYTE = 8;
    private const int MASK0 = 128;
    public const string NL = "\r\n";

    public static void addToSortedArray(IList sortedArray, byte[] element)
    {
      if (sortedArray == null || element == null)
        return;
      bool flag = false;
      for (int index = 0; index < sortedArray.Count && !flag; ++index)
      {
        byte[] a2 = (byte[]) sortedArray[index];
        if (ByteArray.compareByteArray(element, a2) < 0)
        {
          sortedArray.Insert(index, (object) element);
          flag = true;
          break;
        }
      }
      if (!flag)
        sortedArray.Add((object) element);
    }

    public static byte[] BinStringToByteArray(string str)
    {
      if (str == null)
        return (byte[]) null;
      int length1 = str.Length;
      if (length1 == 0)
        return new byte[0];
      byte[] numArray1 = new byte[(length1 + 7) / 8];
      for (int index = 0; index < numArray1.Length; ++index)
        numArray1[index] = (byte) 0;
      int length2 = 0;
      int number = 128;
      for (int index = 0; index < length1; ++index)
      {
        char ch = str[index];
        switch (ch)
        {
          case '0':
          case '1':
            if ((int) ch != 48)
            {
              byte[] numArray2;
              IntPtr num;
              (numArray2 = numArray1)[(int) (num = (IntPtr) length2)] = (byte) ((uint) numArray2[(int) num] | (uint) (byte) ((int) byte.MaxValue & number));
            }
            if (number == 1)
            {
              ++length2;
              number = 128;
              break;
            }
            else
            {
              number = Tools.URShift(number, 1);
              break;
            }
        }
      }
      if (number != 128)
        ++length2;
      if (length2 >= numArray1.Length)
        return numArray1;
      byte[] numArray3 = new byte[length2];
      Array.Copy((Array) numArray1, 0, (Array) numArray3, 0, length2);
      return numArray3;
    }

    public static string ByteArrayToBinaryString(byte[] data, string separator, int nbBytesPerLine)
    {
      if (data == null)
        return (string) null;
      StringBuilder stringBuilder = new StringBuilder();
      for (int index1 = 0; index1 < data.Length; ++index1)
      {
        int number = 128;
        for (int index2 = 0; index2 < 8; ++index2)
        {
          stringBuilder.Append(((int) data[index1] & number) == 0 ? "0" : "1");
          number = Tools.URShift(number, 1);
        }
        if (index1 < data.Length - 1)
        {
          string str = separator != null ? separator : "";
          if (nbBytesPerLine > 0 && (index1 + 1) % nbBytesPerLine == 0)
            str = "\r\n";
          stringBuilder.Append(str);
        }
      }
      return ((object) stringBuilder).ToString();
    }

    public static string ByteArrayToHexString(byte[] data, string separator, int nbBytesPerLine)
    {
      return ByteArray.ByteArrayToHexString(data, -1, separator, nbBytesPerLine, false);
    }

    public static string ByteArrayToHexString(byte[] data, string separator, int nbBytesPerLine, bool uppercase)
    {
      return ByteArray.ByteArrayToHexString(data, -1, separator, nbBytesPerLine, uppercase);
    }

    public static string ByteArrayToHexString(byte[] data, int hexCharsCount, string separator, int nbBytesPerLine, bool uppercase)
    {
      if (data == null)
        return (string) null;
      if (hexCharsCount != -1 && hexCharsCount + 1 >> 1 > data.Length)
      {
        throw new ArgumentException("hexCharsCount parameter is not correct ((hexCharsCount+1)/2=" + (object) (hexCharsCount << 1) + " should be greater than " + (string) (object) data.Length + ").");
      }
      else
      {
        int num1 = data.Length;
        bool flag = true;
        if (hexCharsCount != -1)
        {
          num1 = hexCharsCount + 1 >> 1;
          if (hexCharsCount % 2 != 0)
            flag = false;
        }
        if (num1 == 0)
          return "";
        int num2 = 0;
        int num3 = 0;
        char[] chArray1 = (char[]) null;
        if (separator != null && separator.Length > 0)
        {
          chArray1 = separator.ToCharArray();
          num3 = chArray1.Length;
          num2 = (num1 - 1) * num3;
        }
        char[] chArray2 = (char[]) null;
        if (nbBytesPerLine > 0)
        {
          chArray2 = "\r\n".ToCharArray();
          int num4 = num1 / nbBytesPerLine;
          int length = chArray2.Length;
          if (num4 > 0 && num1 % nbBytesPerLine == 0)
            --num4;
          int num5 = num4 * length;
          num2 = chArray1 == null ? num5 : num2 - num4 * num3 + num5;
        }
        char[] chArray3 = flag ? new char[(num1 << 1) + num2] : new char[(num1 << 1) + num2 - 1];
        int num6 = uppercase ? 55 : 87;
        int num7 = 0;
        for (int index1 = 0; index1 < num1; ++index1)
        {
          byte num4 = (byte) (((int) data[index1] & 240) >> 4);
          int num5;
          if ((int) num4 <= 9)
          {
            char[] chArray4 = chArray3;
            int index2 = num7;
            int num8 = 1;
            num5 = index2 + num8;
            int num9 = (int) (ushort) ((uint) num4 + 48U);
            chArray4[index2] = (char) num9;
          }
          else
          {
            char[] chArray4 = chArray3;
            int index2 = num7;
            int num8 = 1;
            num5 = index2 + num8;
            int num9 = (int) (ushort) ((uint) num4 + (uint) num6);
            chArray4[index2] = (char) num9;
          }
          if (flag || index1 != num1 - 1)
          {
            byte num8 = (byte) ((uint) data[index1] & 15U);
            if ((int) num8 <= 9)
            {
              char[] chArray4 = chArray3;
              int index2 = num5;
              int num9 = 1;
              num7 = index2 + num9;
              int num10 = (int) (ushort) ((uint) num8 + 48U);
              chArray4[index2] = (char) num10;
            }
            else
            {
              char[] chArray4 = chArray3;
              int index2 = num5;
              int num9 = 1;
              num7 = index2 + num9;
              int num10 = (int) (ushort) ((uint) num8 + (uint) num6);
              chArray4[index2] = (char) num10;
            }
            if (index1 < num1 - 1)
            {
              char[] chArray4 = chArray1;
              if (nbBytesPerLine > 0 && (index1 + 1) % nbBytesPerLine == 0)
                chArray4 = chArray2;
              if (chArray4 != null)
              {
                for (int index2 = 0; index2 < chArray4.Length; ++index2)
                  chArray3[num7++] = chArray4[index2];
              }
            }
          }
          else
            break;
        }
        return new string(chArray3);
      }
    }

    public static string ByteArrayToPackedBCDString(byte[] data, int digitsCount)
    {
      return ByteArray.ByteArrayToHexString(data, digitsCount, (string) null, -1, true);
    }

    public static string ByteArrayToPackedBCDString(byte[] data, char filler, bool removeFillers)
    {
      string str = ByteArray.ByteArrayToHexString(data, -1, (string) null, -1, true);
      if (str != null && removeFillers)
      {
        char[] chArray = str.ToCharArray();
        int index = chArray.Length - 1;
        char ch = char.ToUpper(filler);
        while (index >= 0 && (int) chArray[index] == (int) ch)
          --index;
        if (index < chArray.Length)
          return str.Substring(0, index + 1);
      }
      return str;
    }

    public static string ByteArrayToPrintableString(byte[] data)
    {
      if (data == null)
        return "<< null >>";
      if (data.Length == 0)
        return "<< empty >>";
      StringBuilder stringBuilder = new StringBuilder(data.Length);
      for (int index = 0; index < data.Length; ++index)
      {
        char c = (char) data[index];
        if (char.IsControl(c))
          stringBuilder.Append('?');
        else
          stringBuilder.Append(c);
      }
      return ((object) stringBuilder).ToString();
    }

    public static int compareByteArray(byte[] a1, byte[] a2)
    {
      if (a1 == null && a2 == null)
        return 0;
      if (a1 != null || a2 == null)
      {
        if (a1 != null && a2 == null)
          return 1;
        for (int index = 0; index < (a1.Length < a2.Length ? a1.Length : a2.Length); ++index)
        {
          int num1 = (int) byte.MaxValue & (int) a1[index];
          int num2 = (int) byte.MaxValue & (int) a2[index];
          if (num1 < num2)
            return -1;
          if (num1 > num2)
            return 1;
        }
        if (a1.Length == a2.Length)
          return 0;
        if (a1.Length >= a2.Length)
          return 1;
      }
      return -1;
    }

    public static byte[] getTotalByteArray(IList v)
    {
      byte[] numArray1 = new byte[ByteArray.getTotalLength(v)];
      int destinationIndex = 0;
      for (int index = 0; index < v.Count; ++index)
      {
        byte[] numArray2 = (byte[]) v[index];
        Array.Copy((Array) numArray2, 0, (Array) numArray1, destinationIndex, numArray2.Length);
        destinationIndex += numArray2.Length;
      }
      return numArray1;
    }

    public static int getTotalLength(IList v)
    {
      int num = 0;
      for (int index = 0; index < v.Count; ++index)
      {
        byte[] numArray = (byte[]) v[index];
        num += numArray.Length;
      }
      return num;
    }

    public static byte[] HexStringToByteArray(string str)
    {
      bool flag = false;
      if (str == null)
        return (byte[]) null;
      int length1 = str.Length;
      if (length1 == 0)
        return new byte[0];
      byte[] numArray1 = new byte[length1 + 1 >> 1];
      for (int index = 0; index < numArray1.Length; ++index)
        numArray1[index] = (byte) 0;
      int index1 = 0;
      int length2 = 0;
      for (; index1 < length1; ++index1)
      {
        char ch = str[index1];
        byte num1;
        if ((int) ch >= 48 && (int) ch <= 57)
          num1 = (byte) ((uint) ch - 48U);
        else if ((int) ch >= 65 && (int) ch <= 70)
          num1 = (byte) ((int) ch - 65 + 10);
        else if ((int) ch >= 97 && (int) ch <= 102)
          num1 = (byte) ((int) ch - 97 + 10);
        else if ((int) ch != 9 && (int) ch != 10 && ((int) ch != 13 && (int) ch != 32))
          throw new ArgumentException("Character '" + (object) ch + "' is not a hexadecimal digit and not a white-space.");
        else
          continue;
        if (!flag)
          num1 <<= 4;
        byte[] numArray2;
        IntPtr num2;
        (numArray2 = numArray1)[(int) (num2 = (IntPtr) length2)] = (byte) ((uint) numArray2[(int) num2] | (uint) num1);
        if (flag)
        {
          flag = false;
          ++length2;
        }
        else
          flag = true;
      }
      if (flag)
        ++length2;
      if (length2 >= numArray1.Length)
        return numArray1;
      byte[] numArray3 = new byte[length2];
      Array.Copy((Array) numArray1, 0, (Array) numArray3, 0, length2);
      return numArray3;
    }

    public static byte[] ReverseByteArray(byte[] data)
    {
      if (data == null)
        return (byte[]) null;
      int length = data.Length;
      byte[] numArray = new byte[length];
      for (int index = 0; index < length; ++index)
        numArray[index] = data[length - index - 1];
      return numArray;
    }
  }
}
