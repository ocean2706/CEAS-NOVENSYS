// Decompiled with JetBrains decompiler
// Type: Novensys.eCard.SDK.ISO8583.IsoConvert
// Assembly: Novensys.eCard.SDK, Version=1.1.56.0, Culture=neutral, PublicKeyToken=null
// MVID: 59F9E456-D3DA-4066-A4A4-692B516A775E
// Assembly location: C:\Program Files (x86)\CNAS\SIUI-SPITAL\Novensys.eCard.SDK.dll

using System.Text;

namespace Novensys.eCard.SDK.ISO8583
{
  public static class IsoConvert
  {
    public static byte[] FromIntToMsgTypeData(int value)
    {
      byte[] numArray = new byte[4];
      for (int index = 3; index >= 0; --index)
      {
        numArray[index] = (byte) IsoConvert.GetHexChar(value & 15);
        value >>= 4;
      }
      return numArray;
    }

    public static int FromMsgTypeDataToInt(byte[] data)
    {
      int num = 0;
      int length = data.Length;
      for (int index = 0; index < length; ++index)
        num = num << 4 | (int) IsoConvert.GetHexNibble(data[index]);
      return num;
    }

    private static byte GetHexNibble(byte data)
    {
      if ((int) data >= 48 && (int) data <= 57)
        return (byte) ((uint) data & 15U);
      if ((int) data >= 65 && (int) data <= 70)
        return (byte) ((uint) data - 55U);
      if ((int) data >= 97 && (int) data <= 102)
        return (byte) ((uint) data - 87U);
      else
        return (byte) 0;
    }

    private static int GetHexChar(int nibble)
    {
      if (nibble < 10)
        return nibble + 48;
      else
        return nibble + 55;
    }

    public static string FromIntToMsgType(int value)
    {
      return Encoding.ASCII.GetString(IsoConvert.FromIntToMsgTypeData(value));
    }

    public static int FromMsgTypeToInt(string msgType)
    {
      return IsoConvert.FromMsgTypeDataToInt(Encoding.ASCII.GetBytes(msgType));
    }
  }
}
