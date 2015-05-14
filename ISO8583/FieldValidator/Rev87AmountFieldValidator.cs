// Decompiled with JetBrains decompiler
// Type: Novensys.eCard.SDK.ISO8583.FieldValidator.Rev87AmountFieldValidator
// Assembly: Novensys.eCard.SDK, Version=1.1.56.0, Culture=neutral, PublicKeyToken=null
// MVID: 59F9E456-D3DA-4066-A4A4-692B516A775E
// Assembly location: C:\Program Files (x86)\CNAS\SIUI-SPITAL\Novensys.eCard.SDK.dll

namespace Novensys.eCard.SDK.ISO8583.FieldValidator
{
  public class Rev87AmountFieldValidator : IFieldValidator
  {
    public string Description
    {
      get
      {
        return "amt";
      }
    }

    public bool IsValid(string value)
    {
      char ch = value[0];
      if ((int) ch != 67 && (int) ch != 68)
        return false;
      else
        return new NumericFieldValidator().IsValid(value.Substring(1));
    }
  }
}
