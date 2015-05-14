// Decompiled with JetBrains decompiler
// Type: Novensys.eCard.SDK.ISO8583.FieldValidator.Track2FieldValidator
// Assembly: Novensys.eCard.SDK, Version=1.1.56.0, Culture=neutral, PublicKeyToken=null
// MVID: 59F9E456-D3DA-4066-A4A4-692B516A775E
// Assembly location: C:\Program Files (x86)\CNAS\SIUI-SPITAL\Novensys.eCard.SDK.dll

using System.Text.RegularExpressions;

namespace Novensys.eCard.SDK.ISO8583.FieldValidator
{
  public class Track2FieldValidator : IFieldValidator
  {
    private static readonly Regex Matcher = new Regex("^\\d{1,19}[=D]([=D]|\\d{4})[=D]?\\d*$");

    public string Description
    {
      get
      {
        return "z";
      }
    }

    public bool IsValid(string value)
    {
      return Track2FieldValidator.Matcher.IsMatch(value);
    }
  }
}
