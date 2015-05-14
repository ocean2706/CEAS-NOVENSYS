// Decompiled with JetBrains decompiler
// Type: Novensys.eCard.SDK.PCSC.Apdu.ApduUtil
// Assembly: Novensys.eCard.SDK, Version=1.1.56.0, Culture=neutral, PublicKeyToken=null
// MVID: 59F9E456-D3DA-4066-A4A4-692B516A775E
// Assembly location: C:\Program Files (x86)\CNAS\SIUI-SPITAL\Novensys.eCard.SDK.dll

using System;

namespace Novensys.eCard.SDK.PCSC.Apdu
{
  public class ApduUtil
  {
    public static bool DataAreEqual(byte[] a, byte[] b)
    {
      if (a != b)
      {
        if (a == null || b == null)
          return false;
        int num = Math.Min(a.Length, b.Length);
        for (int index = 0; index < num; ++index)
        {
          if ((int) a[index] != (int) b[index])
            return false;
        }
      }
      return true;
    }

    public static bool ContainsData(byte[] data)
    {
      return data != null && data.Length != 0;
    }

    public static bool ContainsValidData(byte[] data)
    {
      if (!ApduUtil.ContainsData(data))
        return false;
      foreach (int num in data)
      {
        if (num != 0)
          return true;
      }
      return false;
    }

    public static byte[] RemoveTrailingNulls(byte[] input)
    {
      byte[] numArray = (byte[]) null;
      if (input.Length > 1)
      {
        int index1 = input.Length - 1;
        while ((int) input[index1] == 0)
          --index1;
        numArray = new byte[index1 + 1];
        for (int index2 = 0; index2 < index1 + 1; ++index2)
          numArray[index2] = input[index2];
      }
      return numArray;
    }
  }
}
