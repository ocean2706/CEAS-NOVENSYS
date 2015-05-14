// Decompiled with JetBrains decompiler
// Type: Novensys.eCard.SDK.ISO8583.FieldValidator.AlphaNumericSpecialFieldValidator
// Assembly: Novensys.eCard.SDK, Version=1.1.56.0, Culture=neutral, PublicKeyToken=null
// MVID: 59F9E456-D3DA-4066-A4A4-692B516A775E
// Assembly location: C:\Program Files (x86)\CNAS\SIUI-SPITAL\Novensys.eCard.SDK.dll

namespace Novensys.eCard.SDK.ISO8583.FieldValidator
{
  public class AlphaNumericSpecialFieldValidator : IFieldValidator
  {
    public string Description
    {
      get
      {
        return "ans";
      }
    }

    public bool IsValid(string value)
    {
      foreach (int num in value)
      {
        if (num < 32)
          return false;
      }
      return true;
    }
  }
}
