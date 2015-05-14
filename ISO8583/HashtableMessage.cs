// Decompiled with JetBrains decompiler
// Type: Novensys.eCard.SDK.ISO8583.HashtableMessage
// Assembly: Novensys.eCard.SDK, Version=1.1.56.0, Culture=neutral, PublicKeyToken=null
// MVID: 59F9E456-D3DA-4066-A4A4-692B516A775E
// Assembly location: C:\Program Files (x86)\CNAS\SIUI-SPITAL\Novensys.eCard.SDK.dll

using System.Collections.Generic;
using System.Text;

namespace Novensys.eCard.SDK.ISO8583
{
  public class HashtableMessage : Dictionary<string, string>
  {
    public void FromMessageString(string message)
    {
      int startIndex1 = 0;
      if (message == null)
        return;
      while (startIndex1 < message.Length)
      {
        int length1 = int.Parse(message.Substring(startIndex1, 1));
        int startIndex2 = startIndex1 + 1;
        int length2 = int.Parse(message.Substring(startIndex2, length1));
        int startIndex3 = startIndex2 + length1;
        string key = message.Substring(startIndex3, length2);
        int startIndex4 = startIndex3 + length2;
        int length3 = int.Parse(message.Substring(startIndex4, 1));
        int startIndex5 = startIndex4 + 1;
        int length4 = int.Parse(message.Substring(startIndex5, length3));
        int startIndex6 = startIndex5 + length3;
        string str = message.Substring(startIndex6, length4);
        startIndex1 = startIndex6 + length4;
        this.Add(key, str);
      }
    }

    public string ToMessageString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      foreach (string index in this.Keys)
      {
        string str = this[index];
        if (!string.IsNullOrEmpty(str))
        {
          int length1 = index.Length;
          int length2 = length1.ToString().Length;
          stringBuilder.Append(length2);
          stringBuilder.Append(length1);
          stringBuilder.Append(index);
          int length3 = str.Length;
          int length4 = length3.ToString().Length;
          stringBuilder.Append(length4);
          stringBuilder.Append(length3);
          stringBuilder.Append(str);
        }
      }
      return ((object) stringBuilder).ToString();
    }
  }
}
