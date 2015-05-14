// Decompiled with JetBrains decompiler
// Type: Novensys.eCard.SDK.ISO8583.FieldValidator.HexFieldValidator
// Assembly: Novensys.eCard.SDK, Version=1.1.56.0, Culture=neutral, PublicKeyToken=null
// MVID: 59F9E456-D3DA-4066-A4A4-692B516A775E
// Assembly location: C:\Program Files (x86)\CNAS\SIUI-SPITAL\Novensys.eCard.SDK.dll

namespace Novensys.eCard.SDK.ISO8583.FieldValidator
{
  public class HexFieldValidator : IFieldValidator
  {
    public string Description
    {
      get
      {
        return "hex";
      }
    }

    public bool IsValid(string value)
    {
      foreach (int num in value)
      {
        if (num < 48 || num > 57 && num < 65 || num > 70 && num < 97 || num > 102)
          return false;
      }
      return true;
    }
  }
}
