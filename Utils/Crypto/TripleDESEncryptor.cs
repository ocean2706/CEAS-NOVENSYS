// Decompiled with JetBrains decompiler
// Type: Novensys.eCard.SDK.Utils.Crypto.TripleDESEncryptor
// Assembly: Novensys.eCard.SDK, Version=1.1.56.0, Culture=neutral, PublicKeyToken=null
// MVID: 59F9E456-D3DA-4066-A4A4-692B516A775E
// Assembly location: C:\Program Files (x86)\CNAS\SIUI-SPITAL\Novensys.eCard.SDK.dll

using System.Security.Cryptography;

namespace Novensys.eCard.SDK.Utils.Crypto
{
  public static class TripleDESEncryptor
  {
    public static byte[] Encrypt(byte[] input, byte[] key, byte[] iv, CipherMode cipherMode, PaddingMode paddingMode)
    {
      TripleDESCryptoServiceProvider cryptoServiceProvider = new TripleDESCryptoServiceProvider();
      cryptoServiceProvider.Key = key;
      cryptoServiceProvider.Mode = cipherMode;
      if (cipherMode == CipherMode.CBC)
        cryptoServiceProvider.IV = iv;
      cryptoServiceProvider.Padding = paddingMode;
      byte[] numArray = cryptoServiceProvider.CreateEncryptor().TransformFinalBlock(input, 0, input.Length);
      cryptoServiceProvider.Clear();
      return numArray;
    }

    public static byte[] Decrypt(byte[] input, byte[] key, byte[] iv, CipherMode cipherMode, PaddingMode paddingMode)
    {
      TripleDESCryptoServiceProvider cryptoServiceProvider = new TripleDESCryptoServiceProvider();
      cryptoServiceProvider.Key = key;
      cryptoServiceProvider.Mode = cipherMode;
      if (cipherMode == CipherMode.CBC)
        cryptoServiceProvider.IV = iv;
      cryptoServiceProvider.Padding = paddingMode;
      byte[] numArray = cryptoServiceProvider.CreateDecryptor().TransformFinalBlock(input, 0, input.Length);
      cryptoServiceProvider.Clear();
      return numArray;
    }
  }
}
