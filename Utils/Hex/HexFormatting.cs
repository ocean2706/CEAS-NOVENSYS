// Decompiled with JetBrains decompiler
// Type: Novensys.eCard.SDK.Utils.Hex.HexFormatting
// Assembly: Novensys.eCard.SDK, Version=1.1.56.0, Culture=neutral, PublicKeyToken=null
// MVID: 59F9E456-D3DA-4066-A4A4-692B516A775E
// Assembly location: C:\Program Files (x86)\CNAS\SIUI-SPITAL\Novensys.eCard.SDK.dll

using System;
using System.Text;

namespace Novensys.eCard.SDK.Utils.Hex
{
  public class HexFormatting
  {
    private HexFormatting()
    {
    }

    public static string ByteToASCII(byte value)
    {
      StringBuilder stringBuilder = new StringBuilder();
      byte[] bytes = new byte[1]
      {
        value
      };
      if ((int) value > 32 && (int) value < 128)
        stringBuilder.Append(Encoding.ASCII.GetString(bytes, 0, 1));
      else if ((int) value > 13)
        stringBuilder.Append((char) value);
      else
        stringBuilder.Append(".");
      return ((object) stringBuilder).ToString();
    }

    public static string Dump(byte[] value)
    {
      return HexFormatting.Dump("", value, value.Length, 16, ValueFormat.HexASCII);
    }

    public static string Dump(string prefixValue, byte[] value)
    {
      return HexFormatting.Dump(prefixValue, value, value.Length, 16, ValueFormat.HexASCII);
    }

    public static string Dump(byte[] value, int count)
    {
      return HexFormatting.Dump("", value, count, 16, ValueFormat.HexASCII);
    }

    public static string Dump(string prefixValue, byte[] value, int count)
    {
      return HexFormatting.Dump(prefixValue, value, count, 16, ValueFormat.HexASCII);
    }

    public static string Dump(byte[] value, int count, int charPerLine)
    {
      return HexFormatting.Dump("", value, count, charPerLine, ValueFormat.HexASCII);
    }

    public static string Dump(string prefixValue, byte[] value, int count, int charPerLine)
    {
      return HexFormatting.Dump(prefixValue, value, count, charPerLine, ValueFormat.HexASCII);
    }

    public static string Dump(byte[] value, int count, int charPerLine, ValueFormat format)
    {
      return HexFormatting.Dump("", value, count, charPerLine, format);
    }

    public static string Dump(string prefixValue, byte[] value, int count, int charPerLine, ValueFormat format)
    {
      int num1 = charPerLine;
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append(prefixValue);
      int length = prefixValue.Length;
      for (int index1 = 0; index1 < count; ++index1)
      {
        if (format == ValueFormat.Hex || format == ValueFormat.HexASCII)
          stringBuilder.Append(HexFormatting.ToHexString(value[index1]) + " ");
        else if (format == ValueFormat.ASCII || format == ValueFormat.ASCIIHex)
          stringBuilder.Append(HexFormatting.ByteToASCII(value[index1]));
        if ((index1 + 1) % num1 == 0 || index1 == count - 1)
        {
          stringBuilder.Append(" ");
          int num2 = (index1 + 1) % num1;
          if (num2 != 0)
          {
            for (int index2 = 0; index2 < num1 - num2; ++index2)
              stringBuilder.Append("   ");
          }
          if (format != ValueFormat.Hex || format != ValueFormat.ASCII)
          {
            int num3 = (index1 + 1) % num1 != 0 ? (index1 + 1) % num1 - 1 : num1 - 1;
            for (int index2 = index1 - num3; index2 < index1 + 1; ++index2)
            {
              if (format == ValueFormat.ASCIIHex)
                stringBuilder.Append(HexFormatting.ToHexString(value[index2]) + " ");
              else if (format == ValueFormat.HexASCII)
                stringBuilder.Append(HexFormatting.ByteToASCII(value[index2]));
            }
          }
          if (index1 != count - 1)
          {
            stringBuilder.Append(Environment.NewLine);
            for (int index2 = 0; index2 < length; ++index2)
              stringBuilder.Append(" ");
          }
          else
            break;
        }
      }
      return ((object) stringBuilder).ToString();
    }

    public static string DumpAscii(byte[] value)
    {
      return HexFormatting.Dump("", value, value.Length, 16, ValueFormat.ASCII);
    }

    public static string DumpAscii(byte[] value, int count)
    {
      return HexFormatting.Dump("", value, count, 16, ValueFormat.ASCII);
    }

    public static string DumpAscii(byte[] value, int count, int charPerLine)
    {
      return HexFormatting.Dump("", value, count, charPerLine, ValueFormat.ASCII);
    }

    public static string DumpAscii(string prefixValue, byte[] value, int count, int charPerLine)
    {
      return HexFormatting.Dump("", value, count, charPerLine, ValueFormat.ASCII);
    }

    public static string DumpHex(byte[] value)
    {
      return HexFormatting.Dump("", value, value.Length, 16, ValueFormat.Hex);
    }

    public static string DumpHex(byte[] value, int count)
    {
      return HexFormatting.Dump("", value, count, 16, ValueFormat.Hex);
    }

    public static string DumpHex(byte[] value, int count, int charPerLine)
    {
      return HexFormatting.Dump("", value, count, charPerLine, ValueFormat.Hex);
    }

    public static string DumpHex(string prefixValue, byte[] value, int count, int charPerLine)
    {
      return HexFormatting.Dump(prefixValue, value, count, charPerLine, ValueFormat.Hex);
    }

    public static string ToHexString(char value)
    {
      return Convert.ToByte(value).ToString("X02");
    }

    public static string ToHexString(byte[] value)
    {
      return HexFormatting.ToHexString(value, false);
    }

    public static string ToHexString(byte value)
    {
      return value.ToString("X02");
    }

    public static string ToHexString(string input)
    {
      StringBuilder stringBuilder = new StringBuilder(string.Empty);
      if (!string.IsNullOrEmpty(input))
      {
        for (int startIndex = 0; startIndex < input.Length; ++startIndex)
          stringBuilder.AppendFormat(string.Format("{0:X}", (object) Encoding.ASCII.GetBytes(input.Substring(startIndex, 1))[0]));
      }
      return ((object) stringBuilder).ToString();
    }

    public static string ToHexString(ushort value)
    {
      return value.ToString("X02");
    }

    public static string ToHexString(uint value)
    {
      return value.ToString("X02");
    }

    public static string ToHexString(ushort value, bool spaces)
    {
      if (spaces)
        return HexFormatting.ToHexString(HexUtil.UshortToByteArray(value), true);
      else
        return HexFormatting.ToHexString(HexUtil.UshortToByteArray(value));
    }

    public static string ToHexString(byte[] value, bool spaces)
    {
      return HexFormatting.ToHexString(value, value.Length, spaces);
    }

    public static string ToHexString(uint value, bool spaces)
    {
      if (spaces)
        return HexFormatting.ToHexString(HexUtil.UintToByteArray(value), true);
      else
        return HexFormatting.ToHexString(HexUtil.UintToByteArray(value));
    }

    public static string ToHexString(ushort value, bool spaces, bool reverseByteOrder)
    {
      if (spaces)
        return HexFormatting.ToHexString(HexUtil.UshortToByteArray(value, reverseByteOrder), true);
      else
        return HexFormatting.ToHexString(HexUtil.UshortToByteArray(value, reverseByteOrder));
    }

    public static string ToHexString(byte[] value, int count, bool spaces)
    {
      StringBuilder stringBuilder = new StringBuilder();
      for (int index = 0; index < count; ++index)
      {
        if (spaces)
          stringBuilder.Append(value[index].ToString("X02") + " ");
        else
          stringBuilder.Append(value[index].ToString("X02"));
      }
      return ((object) stringBuilder).ToString();
    }

    public static string ToHexString(uint value, bool spaces, bool reverseByteOrder)
    {
      if (spaces)
        return HexFormatting.ToHexString(HexUtil.UintToByteArray(value, reverseByteOrder), true);
      else
        return HexFormatting.ToHexString(HexUtil.UintToByteArray(value, reverseByteOrder));
    }
  }
}
