// Decompiled with JetBrains decompiler
// Type: Novensys.eCard.SDK.Utils.Crypto.CryptoHelper
// Assembly: Novensys.eCard.SDK, Version=1.1.56.0, Culture=neutral, PublicKeyToken=null
// MVID: 59F9E456-D3DA-4066-A4A4-692B516A775E
// Assembly location: C:\Program Files (x86)\CNAS\SIUI-SPITAL\Novensys.eCard.SDK.dll

using Novensys.eCard.SDK.Entities;
using Novensys.eCard.SDK.Utils.Hex;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;

namespace Novensys.eCard.SDK.Utils.Crypto
{
  public static class CryptoHelper
  {
    private const string VECTOR_INITIALIZARE = "0000000000000000";

    public static Dictionary<string, string> DeriveKeySet(byte[] key, string SN_ICC)
    {
      Dictionary<string, string> dictionary = new Dictionary<string, string>();
      byte[] bytes = HexEncoding.GetBytes(SN_ICC);
      string str1 = CryptoHelper.EHC_3DES(key, bytes) + CryptoHelper.EHC_3DES(key, CryptoHelper.XOR(bytes, HexEncoding.GetBytes("FFFFFFFFFFFFFFFF")));
      string hexString1 = str1 + "00000001";
      string hexString2 = str1 + "00000002";
      SHA1 shA1 = SHA1.Create();
      byte[] key1 = new byte[16];
      Array.Copy((Array) shA1.ComputeHash(HexEncoding.GetBytes(hexString1)), 0, (Array) key1, 0, 16);
      CryptoHelper.AdjustParityBits(key1);
      string str2 = HexFormatting.ToHexString(key1);
      byte[] key2 = new byte[16];
      Array.Copy((Array) shA1.ComputeHash(HexEncoding.GetBytes(hexString2)), 0, (Array) key2, 0, 16);
      CryptoHelper.AdjustParityBits(key2);
      string str3 = HexFormatting.ToHexString(key2);
      dictionary.Add("SKENC", str2);
      dictionary.Add("SKMAC", str3);
      return dictionary;
    }

    public static byte[] _3DES(string key, string data)
    {
      byte[] bytes = HexEncoding.GetBytes("0000000000000000");
      return TripleDESEncryptor.Encrypt(HexEncoding.GetBytes(data), HexEncoding.GetBytes(key), bytes, CipherMode.CBC, PaddingMode.None);
    }

    private static string EHC_3DES(byte[] key, byte[] data)
    {
      byte[] bytes = HexEncoding.GetBytes("0000000000000000");
      return HexFormatting.ToHexString(TripleDESEncryptor.Encrypt(data, key, bytes, CipherMode.ECB, PaddingMode.None));
    }

    public static byte[] CalculateMac(byte[] key, byte[] data)
    {
      byte[] bytes = HexEncoding.GetBytes("0000000000000000");
      byte[] numArray1 = new byte[8];
      byte[] key1 = new byte[8];
      Array.Copy((Array) key, 0, (Array) key1, 0, 8);
      byte[] key2 = new byte[8];
      Array.Copy((Array) key, 8, (Array) key2, 0, 8);
      byte[] numArray2 = DESEncryptor.Encrypt(HexEncoding.GetBytes(HexFormatting.ToHexString(data) + "8000000000000000"), key1, bytes, CipherMode.CBC, PaddingMode.None);
      byte[] input = new byte[8];
      Array.Copy((Array) numArray2, numArray2.Length - 8, (Array) input, 0, 8);
      byte[] numArray3 = DESEncryptor.Encrypt(DESEncryptor.Decrypt(input, key2, bytes, CipherMode.ECB, PaddingMode.None), key1, bytes, CipherMode.CBC, PaddingMode.None);
      Array.Copy((Array) numArray3, numArray3.Length - 8, (Array) numArray1, 0, 8);
      return numArray1;
    }

    private static void AdjustParityBits(byte[] key)
    {
      for (int index1 = 0; index1 < key.Length; ++index1)
      {
        int num = (int) key[index1] & (int) byte.MaxValue | 1;
        for (int index2 = 7; index2 > 0; --index2)
          num = num & 1 ^ num >> 1;
        key[index1] = (byte) ((int) key[index1] & 254 | num);
      }
    }

    public static byte[] XOR(byte[] byteArr1, byte[] byteArr2)
    {
      for (int index = 0; index < byteArr1.Length; ++index)
        byteArr1[index] ^= byteArr2[index];
      return byteArr1;
    }

    public static string CreatePinBlock(string pan, string pin, string tmk)
    {
      string hexString1 = (pin.Length.ToString().PadLeft(2, '0') + pin).PadRight(16, 'F');
      string hexString2;
      if (string.IsNullOrEmpty(pan))
      {
        hexString2 = new string('0', 16);
      }
      else
      {
        bool flag = true;
        for (int index = 0; index < pan.Length; ++index)
          flag = flag && char.IsDigit(pan, index);
        if (flag)
        {
          hexString2 = string.Format("0000{0}", (object) pan.Substring(pan.Length - 13, 12));
        }
        else
        {
          StringBuilder stringBuilder = new StringBuilder();
          foreach (char ch in pan.ToUpper(CultureInfo.InvariantCulture))
            stringBuilder.AppendFormat("{0:X2}", (object) (int) ch);
          string str = ((object) stringBuilder).ToString();
          hexString2 = str.Length <= 16 ? str.PadRight(16, 'F') : str.Substring(0, 16);
        }
      }
      byte[] bytes1 = HexEncoding.GetBytes(hexString1);
      byte[] bytes2 = HexEncoding.GetBytes(hexString2);
      byte[] input = new byte[8];
      for (int index = 0; index < 8; ++index)
        input[index] = (byte) ((uint) bytes1[index] ^ (uint) bytes2[index]);
      byte[] bytes3 = HexEncoding.GetBytes(tmk);
      byte[] bytes4 = HexEncoding.GetBytes("0000000000000000");
      return HexFormatting.ToHexString(TripleDESEncryptor.Encrypt(input, bytes3, bytes4, CipherMode.CBC, PaddingMode.None));
    }

    public static string DecryptToken(string token, IdentificatorDrepturi identificatorDrepturi, DateTime serverDateTime)
    {
      string str1 = string.Empty;
      byte[] bytes1 = Encoding.ASCII.GetBytes(string.Format("{0}", (object) serverDateTime.ToString("ddMMyyyy")));
      string str2 = string.Empty;
      if (identificatorDrepturi.CasaAsigurare != null)
        str2 = str2 + identificatorDrepturi.CasaAsigurare;
      if (identificatorDrepturi.TipFurnizor != null)
        str2 = str2 + identificatorDrepturi.TipFurnizor;
      if (identificatorDrepturi.NumarContract != null)
        str2 = str2 + identificatorDrepturi.NumarContract;
      byte[] bytes2 = Encoding.ASCII.GetBytes(str2.Substring(0, 8));
      return Encoding.ASCII.GetString(DESEncryptor.Decrypt(HexEncoding.GetBytes(token), bytes1, bytes2, CipherMode.CBC, PaddingMode.PKCS7));
    }

    public static byte[] DecryptTerminalFile(byte[] input)
    {
      byte[] bytes1 = Encoding.ASCII.GetBytes("12345678");
      byte[] bytes2 = HexEncoding.GetBytes("0000000000000000");
      return DESEncryptor.Decrypt(input, bytes1, bytes2, CipherMode.CBC, PaddingMode.PKCS7);
    }

    public static byte[] EncryptTerminalFile(byte[] input)
    {
      byte[] bytes1 = Encoding.ASCII.GetBytes("12345678");
      byte[] bytes2 = HexEncoding.GetBytes("0000000000000000");
      return DESEncryptor.Encrypt(input, bytes1, bytes2, CipherMode.CBC, PaddingMode.PKCS7);
    }

    public static byte[] SHA256ComputeHash(byte[] data)
    {
      return SHA256.Create().ComputeHash(data);
    }

    public static byte[] SHA256ComputeHash(byte[] data, int offset, int count)
    {
      return SHA256.Create().ComputeHash(data, offset, count);
    }

    public static byte[] SHA1ComputeHash(byte[] data)
    {
      return SHA1.Create().ComputeHash(data);
    }
  }
}
