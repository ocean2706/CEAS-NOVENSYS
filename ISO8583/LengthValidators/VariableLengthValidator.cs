// Decompiled with JetBrains decompiler
// Type: Novensys.eCard.SDK.ISO8583.LengthValidators.VariableLengthValidator
// Assembly: Novensys.eCard.SDK, Version=1.1.56.0, Culture=neutral, PublicKeyToken=null
// MVID: 59F9E456-D3DA-4066-A4A4-692B516A775E
// Assembly location: C:\Program Files (x86)\CNAS\SIUI-SPITAL\Novensys.eCard.SDK.dll

namespace Novensys.eCard.SDK.ISO8583.LengthValidators
{
  public class VariableLengthValidator : ILengthValidator
  {
    private readonly int _maxLength;
    private readonly int _minLength;

    public VariableLengthValidator(int maximumLength)
      : this(0, maximumLength)
    {
    }

    public VariableLengthValidator(int minimumLength, int maximumLength)
    {
      this._minLength = minimumLength;
      this._maxLength = maximumLength;
    }

    public bool IsValid(string value)
    {
      return value.Length >= this._minLength && value.Length <= this._maxLength;
    }
  }
}
