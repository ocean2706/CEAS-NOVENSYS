// Decompiled with JetBrains decompiler
// Type: Novensys.eCard.SDK.ISO8583.Template
// Assembly: Novensys.eCard.SDK, Version=1.1.56.0, Culture=neutral, PublicKeyToken=null
// MVID: 59F9E456-D3DA-4066-A4A4-692B516A775E
// Assembly location: C:\Program Files (x86)\CNAS\SIUI-SPITAL\Novensys.eCard.SDK.dll

using Novensys.eCard.SDK.ISO8583.Formatter;
using System.Collections.Generic;
using System.Text;

namespace Novensys.eCard.SDK.ISO8583
{
  public class Template : Dictionary<int, IFieldDescriptor>
  {
    public IFormatter MsgTypeFormatter { get; set; }

    public IFormatter BitmapFormatter { get; set; }

    public Template()
    {
      this.MsgTypeFormatter = Formatters.Ascii;
      this.BitmapFormatter = Formatters.Binary;
    }

    public string DescribePacking()
    {
      StringBuilder stringBuilder = new StringBuilder();
      foreach (KeyValuePair<int, IFieldDescriptor> keyValuePair in (Dictionary<int, IFieldDescriptor>) this)
      {
        int key = keyValuePair.Key;
        IFieldDescriptor fieldDescriptor = keyValuePair.Value;
        stringBuilder.AppendLine(fieldDescriptor.Display(string.Empty, key, (string) null));
      }
      return ((object) stringBuilder).ToString();
    }
  }
}
