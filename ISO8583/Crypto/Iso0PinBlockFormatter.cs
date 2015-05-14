// Decompiled with JetBrains decompiler
// Type: Novensys.eCard.SDK.ISO8583.Crypto.Iso0PinBlockFormatter
// Assembly: Novensys.eCard.SDK, Version=1.1.56.0, Culture=neutral, PublicKeyToken=null
// MVID: 59F9E456-D3DA-4066-A4A4-692B516A775E
// Assembly location: C:\Program Files (x86)\CNAS\SIUI-SPITAL\Novensys.eCard.SDK.dll

using Novensys.eCard.SDK.ISO8583.Formatter;

namespace Novensys.eCard.SDK.ISO8583.Crypto
{
  public class Iso0PinBlockFormatter
  {
    public static string CreatePinBlock(string pan, string pin)
    {
      string str1 = (pin.Length.ToString().PadLeft(2, '0') + pin).PadRight(16, 'F');
      string str2 = string.Format("0000{0}", (object) pan.Substring(pan.Length - 13, 12));
      byte[] bytes1 = BinaryFormatter.GetBytes(str1);
      byte[] bytes2 = BinaryFormatter.GetBytes(str2);
      byte[] data = new byte[8];
      for (int index = 0; index < 8; ++index)
        data[index] = (byte) ((uint) bytes1[index] ^ (uint) bytes2[index]);
      return BinaryFormatter.GetString(data);
    }
  }
}
