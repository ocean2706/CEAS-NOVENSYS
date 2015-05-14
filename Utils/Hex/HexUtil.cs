// Decompiled with JetBrains decompiler
// Type: Novensys.eCard.SDK.Utils.Hex.HexUtil
// Assembly: Novensys.eCard.SDK, Version=1.1.56.0, Culture=neutral, PublicKeyToken=null
// MVID: 59F9E456-D3DA-4066-A4A4-692B516A775E
// Assembly location: C:\Program Files (x86)\CNAS\SIUI-SPITAL\Novensys.eCard.SDK.dll

using System;
using System.Globalization;

namespace Novensys.eCard.SDK.Utils.Hex
{
  public class HexUtil
  {
    private HexUtil()
    {
    }

    public static byte HexToByte(string value, int startindex)
    {
      int length = 0;
      if (string.IsNullOrEmpty(value))
        throw new ArgumentNullException("hexString");
      if (value.Length > 1 && startindex + 2 > value.Length)
        throw new ArgumentOutOfRangeException("startindex", "startindex and hexString.Length must refer to a location within the hexString.");
      if (value.Length == 1)
        length = 1;
      else if (value.Length >= 2)
        length = 2;
      return byte.Parse(value.Substring(startindex, length), NumberStyles.HexNumber);
    }

    public static bool IsHexChar(char value)
    {
      value = char.ToUpper(value);
      return (int) value >= 48 && (int) value <= 57 || (int) value >= 65 && (int) value <= 70;
    }

    public static byte[] UintToByteArray(uint value)
    {
      return HexUtil.UintToByteArray(value, false);
    }

    public static byte[] UintToByteArray(uint value, bool reverseByteOrder)
    {
      byte[] numArray1 = new byte[4];
      int length = 0;
      do
      {
        numArray1[length++] = (byte) (value % 256U);
      }
      while ((value /= 256U) > 0U);
      byte[] numArray2 = new byte[length];
      if (reverseByteOrder)
      {
        for (int index = 0; index < numArray2.Length; ++index)
          numArray2[index] = numArray1[index];
        return numArray2;
      }
      else
      {
        int num = 0;
        while (--length >= 0)
          numArray2[num++] = numArray1[length];
        return numArray2;
      }
    }

    public static byte[] UshortToByteArray(ushort value)
    {
      return HexUtil.UshortToByteArray(value, false);
    }

    public static byte[] UshortToByteArray(ushort value, bool reverseByteOrder)
    {
      return HexUtil.UintToByteArray((uint) value, reverseByteOrder);
    }

    public static ushort HexStringToUShort(string hexString)
    {
      return ushort.Parse(hexString, NumberStyles.HexNumber);
    }
  }
}
