// Decompiled with JetBrains decompiler
// Type: Novensys.eCard.SDK.ISO8583.Formatter.BcdFormatter
// Assembly: Novensys.eCard.SDK, Version=1.1.56.0, Culture=neutral, PublicKeyToken=null
// MVID: 59F9E456-D3DA-4066-A4A4-692B516A775E
// Assembly location: C:\Program Files (x86)\CNAS\SIUI-SPITAL\Novensys.eCard.SDK.dll

using System;

namespace Novensys.eCard.SDK.ISO8583.Formatter
{
  public class BcdFormatter : IFormatter
  {
    public byte[] GetBytes(string value)
    {
      if (value.Length % 2 == 1)
        value = value.PadLeft(value.Length + 1, '0');
      char[] chArray = value.ToCharArray();
      int length = chArray.Length / 2;
      byte[] numArray = new byte[length];
      for (int index = 0; index < length; ++index)
      {
        byte num1 = byte.Parse(chArray[2 * index].ToString());
        byte num2 = byte.Parse(chArray[2 * index + 1].ToString());
        numArray[index] = (byte) ((uint) (byte) ((uint) num1 << 4) | (uint) num2);
      }
      return numArray;
    }

    public string GetString(byte[] data)
    {
      return BitConverter.ToString(data).Replace("-", string.Empty);
    }

    public int GetPackedLength(int unpackedLength)
    {
      return (int) Math.Ceiling((double) unpackedLength / 2.0);
    }
  }
}
