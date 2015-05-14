// Decompiled with JetBrains decompiler
// Type: Novensys.eCard.SDK.ISO8583.FieldValidator.FieldValidators
// Assembly: Novensys.eCard.SDK, Version=1.1.56.0, Culture=neutral, PublicKeyToken=null
// MVID: 59F9E456-D3DA-4066-A4A4-692B516A775E
// Assembly location: C:\Program Files (x86)\CNAS\SIUI-SPITAL\Novensys.eCard.SDK.dll

namespace Novensys.eCard.SDK.ISO8583.FieldValidator
{
  public static class FieldValidators
  {
    public static IFieldValidator A
    {
      get
      {
        return FieldValidators.Alpha;
      }
    }

    public static IFieldValidator Alpha
    {
      get
      {
        return (IFieldValidator) new AlphaFieldValidator();
      }
    }

    public static IFieldValidator An
    {
      get
      {
        return FieldValidators.AlphaNumeric;
      }
    }

    public static IFieldValidator AlphaNumeric
    {
      get
      {
        return (IFieldValidator) new AlphaNumericFieldValidator();
      }
    }

    public static IFieldValidator Ansp
    {
      get
      {
        return FieldValidators.AlphaNumericAndSpace;
      }
    }

    public static IFieldValidator AlphaNumericAndSpace
    {
      get
      {
        return (IFieldValidator) new AlphaNumericAndSpaceFieldValidator();
      }
    }

    public static IFieldValidator Anp
    {
      get
      {
        return FieldValidators.AlphaNumericPrintable;
      }
    }

    public static IFieldValidator AlphaNumericPrintable
    {
      get
      {
        return (IFieldValidator) new AlphaNumericPrintableFieldValidator();
      }
    }

    public static IFieldValidator Ans
    {
      get
      {
        return FieldValidators.AlphaNumericSpecial;
      }
    }

    public static IFieldValidator AlphaNumericSpecial
    {
      get
      {
        return (IFieldValidator) new AlphaNumericSpecialFieldValidator();
      }
    }

    public static IFieldValidator Hex
    {
      get
      {
        return (IFieldValidator) new HexFieldValidator();
      }
    }

    public static IFieldValidator None
    {
      get
      {
        return (IFieldValidator) new NoneFieldValidator();
      }
    }

    public static IFieldValidator N
    {
      get
      {
        return FieldValidators.Numeric;
      }
    }

    public static IFieldValidator Numeric
    {
      get
      {
        return (IFieldValidator) new NumericFieldValidator();
      }
    }

    public static IFieldValidator Track2
    {
      get
      {
        return (IFieldValidator) new Track2FieldValidator();
      }
    }

    public static IFieldValidator Rev87AmountValidator
    {
      get
      {
        return (IFieldValidator) new Rev87AmountFieldValidator();
      }
    }
  }
}
