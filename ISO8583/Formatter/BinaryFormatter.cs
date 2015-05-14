// Decompiled with JetBrains decompiler
// Type: Novensys.eCard.SDK.ISO8583.Formatter.BinaryFormatter
// Assembly: Novensys.eCard.SDK, Version=1.1.56.0, Culture=neutral, PublicKeyToken=null
// MVID: 59F9E456-D3DA-4066-A4A4-692B516A775E
// Assembly location: C:\Program Files (x86)\CNAS\SIUI-SPITAL\Novensys.eCard.SDK.dll

using Novensys.eCard.SDK.ISO8583;
using System;

namespace Novensys.eCard.SDK.ISO8583.Formatter
{
  public class BinaryFormatter : IFormatter
  {
    byte[] IFormatter.GetBytes(string value)
    {
      return BinaryFormatter.GetBytes(value);
    }

    string IFormatter.GetString(byte[] data)
    {
      return BinaryFormatter.GetString(data);
    }

    int IFormatter.GetPackedLength(int unpackedLength)
    {
      return BinaryFormatter.GetPackedLength(unpackedLength);
    }

    public static byte[] GetBytes(string value)
    {
      if (!Validators.IsHex(value))
        throw new FormatException("Value is not valid HEX");
      int length = value.Length;
      byte[] numArray = new byte[length / 2];
      int startIndex = 0;
      while (startIndex < length)
      {
        numArray[startIndex / 2] = Convert.ToByte(value.Substring(startIndex, 2), 16);
        startIndex += 2;
      }
      return numArray;
    }

    public static string GetString(byte[] data)
    {
      return BitConverter.ToString(data).Replace("-", string.Empty);
    }

    public static int GetPackedLength(int unpackedLength)
    {
      return unpackedLength / 2;
    }
  }
}
