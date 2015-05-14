// Decompiled with JetBrains decompiler
// Type: Novensys.eCard.SDK.ISO8583.Utils
// Assembly: Novensys.eCard.SDK, Version=1.1.56.0, Culture=neutral, PublicKeyToken=null
// MVID: 59F9E456-D3DA-4066-A4A4-692B516A775E
// Assembly location: C:\Program Files (x86)\CNAS\SIUI-SPITAL\Novensys.eCard.SDK.dll

using System.Text;

namespace Novensys.eCard.SDK.ISO8583
{
  public static class Utils
  {
    public static string GetLuhn(string pan)
    {
      int num1 = 0;
      bool flag = true;
      for (int index = pan.Length - 1; index >= 0; --index)
      {
        int num2 = int.Parse(pan[index].ToString());
        if (flag)
        {
          num2 *= 2;
          if (num2 > 9)
            num2 -= 9;
        }
        num1 += num2;
        flag = !flag;
      }
      int num3 = 10 - num1 % 10;
      if (num3 == 10)
        num3 = 0;
      return num3.ToString();
    }

    public static bool IsValidPAN(string pan)
    {
      return Utils.GetLuhn(pan.Substring(0, pan.Length - 1)) == pan.Substring(pan.Length - 1);
    }

    public static string MaskPan(string pan)
    {
      if (pan == null)
        return (string) null;
      int length = pan.Length;
      if (length <= 10)
        return pan;
      else
        return ((object) new StringBuilder(length, length).Append(pan.Substring(0, 6)).Append(new string('x', length - 10)).Append(pan.Substring(length - 4, 4))).ToString();
    }
  }
}
