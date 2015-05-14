// Decompiled with JetBrains decompiler
// Type: Novensys.eCard.SDK.ISO8583.LengthValidators.FixedLengthValidator
// Assembly: Novensys.eCard.SDK, Version=1.1.56.0, Culture=neutral, PublicKeyToken=null
// MVID: 59F9E456-D3DA-4066-A4A4-692B516A775E
// Assembly location: C:\Program Files (x86)\CNAS\SIUI-SPITAL\Novensys.eCard.SDK.dll

namespace Novensys.eCard.SDK.ISO8583.LengthValidators
{
  public class FixedLengthValidator : ILengthValidator
  {
    private readonly int _length;

    public FixedLengthValidator(int length)
    {
      this._length = length;
    }

    public bool IsValid(string value)
    {
      return value.Length == this._length;
    }
  }
}
