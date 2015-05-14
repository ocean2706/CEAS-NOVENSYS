// Decompiled with JetBrains decompiler
// Type: Novensys.eCard.SDK.Utils.Crypto.Encryptor
// Assembly: Novensys.eCard.SDK, Version=1.1.56.0, Culture=neutral, PublicKeyToken=null
// MVID: 59F9E456-D3DA-4066-A4A4-692B516A775E
// Assembly location: C:\Program Files (x86)\CNAS\SIUI-SPITAL\Novensys.eCard.SDK.dll

using Novensys.eCard.SDK.Utils.Hex;
using System.Security.Cryptography;
using System.Text;

namespace Novensys.eCard.SDK.Utils.Crypto
{
  public static class Encryptor
  {
    public static string Encrypt(string clearText, string key, string sIV)
    {
      string str = string.Empty;
      DESCryptoServiceProvider cryptoServiceProvider = new DESCryptoServiceProvider();
      byte[] bytes1 = Encoding.ASCII.GetBytes(key);
      byte[] bytes2 = Encoding.ASCII.GetBytes(sIV);
      cryptoServiceProvider.IV = bytes2;
      cryptoServiceProvider.Key = bytes1;
      ICryptoTransform encryptor = cryptoServiceProvider.CreateEncryptor();
      byte[] bytes3 = Encoding.ASCII.GetBytes(clearText);
      return HexFormatting.ToHexString(encryptor.TransformFinalBlock(bytes3, 0, bytes3.Length));
    }

    public static string Decrypt(string encryptedText, string key, string sIV)
    {
      string str = string.Empty;
      DESCryptoServiceProvider cryptoServiceProvider = new DESCryptoServiceProvider();
      byte[] bytes1 = Encoding.ASCII.GetBytes(key);
      byte[] bytes2 = Encoding.ASCII.GetBytes(sIV);
      cryptoServiceProvider.IV = bytes2;
      cryptoServiceProvider.Key = bytes1;
      ICryptoTransform decryptor = cryptoServiceProvider.CreateDecryptor();
      byte[] bytes3 = HexEncoding.GetBytes(encryptedText);
      return Encoding.ASCII.GetString(decryptor.TransformFinalBlock(bytes3, 0, bytes3.Length));
    }
  }
}
