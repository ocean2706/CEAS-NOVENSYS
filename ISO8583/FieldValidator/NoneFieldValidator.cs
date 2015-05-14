// Decompiled with JetBrains decompiler
// Type: Novensys.eCard.SDK.ISO8583.FieldValidator.NoneFieldValidator
// Assembly: Novensys.eCard.SDK, Version=1.1.56.0, Culture=neutral, PublicKeyToken=null
// MVID: 59F9E456-D3DA-4066-A4A4-692B516A775E
// Assembly location: C:\Program Files (x86)\CNAS\SIUI-SPITAL\Novensys.eCard.SDK.dll

namespace Novensys.eCard.SDK.ISO8583.FieldValidator
{
  public class NoneFieldValidator : IFieldValidator
  {
    public string Description
    {
      get
      {
        return "none";
      }
    }

    public bool IsValid(string value)
    {
      return true;
    }
  }
}
