// Decompiled with JetBrains decompiler
// Type: Novensys.eCard.SDK.ISO8583.FieldDescriptor
// Assembly: Novensys.eCard.SDK, Version=1.1.56.0, Culture=neutral, PublicKeyToken=null
// MVID: 59F9E456-D3DA-4066-A4A4-692B516A775E
// Assembly location: C:\Program Files (x86)\CNAS\SIUI-SPITAL\Novensys.eCard.SDK.dll

using Novensys.eCard.SDK.ISO8583.Exceptions;
using Novensys.eCard.SDK.ISO8583.FieldValidator;
using Novensys.eCard.SDK.ISO8583.Formatter;
using Novensys.eCard.SDK.ISO8583.LengthFormatters;
using System;
using System.Text;

namespace Novensys.eCard.SDK.ISO8583
{
  public class FieldDescriptor : IFieldDescriptor
  {
    public Adjuster Adjuster { get; private set; }

    public virtual IFormatter Formatter { get; private set; }

    public virtual ILengthFormatter LengthFormatter { get; private set; }

    public virtual IFieldValidator Validator { get; private set; }

    public FieldDescriptor(ILengthFormatter lengthFormatter, IFieldValidator validator, IFormatter formatter, Adjuster adjuster)
    {
      if (formatter is BinaryFormatter && !(validator is HexFieldValidator))
        throw new FieldDescriptorException("A Binary field must have a hex validator");
      if (formatter is BcdFormatter && !(validator is NumericFieldValidator))
        throw new FieldDescriptorException("A BCD field must have a numeric validator");
      this.LengthFormatter = lengthFormatter;
      this.Validator = validator;
      this.Formatter = formatter;
      this.Adjuster = adjuster;
    }

    public static IFieldDescriptor AsciiAlphaNumeric(int length)
    {
      LambdaAdjuster lambdaAdjuster = new LambdaAdjuster((FuncStringString) null, (FuncStringString) (value => value.PadRight(length, ' ')));
      return FieldDescriptor.Create((ILengthFormatter) new FixedLengthFormatter(length), FieldValidators.Ansp, Formatters.Ascii, (Adjuster) lambdaAdjuster);
    }

    public static IFieldDescriptor AsciiAmount(int length)
    {
      LambdaAdjuster lambdaAdjuster = new LambdaAdjuster((FuncStringString) null, (FuncStringString) (value => value.PadLeft(length, '0')));
      return FieldDescriptor.Create((ILengthFormatter) new FixedLengthFormatter(length), FieldValidators.Rev87AmountValidator, Formatters.Ascii, (Adjuster) lambdaAdjuster);
    }

    public static IFieldDescriptor AsciiFixed(int packedLength, IFieldValidator validator)
    {
      return FieldDescriptor.Create((ILengthFormatter) new FixedLengthFormatter(packedLength), validator, Formatters.Ascii);
    }

    public static IFieldDescriptor AsciiLlCharacter(int maxLength)
    {
      return FieldDescriptor.AsciiVar(2, maxLength, FieldValidators.Ans);
    }

    public static IFieldDescriptor AsciiLlNumeric(int maxLength)
    {
      return FieldDescriptor.AsciiVar(2, maxLength, FieldValidators.N);
    }

    public static IFieldDescriptor AsciiLllBinary(int packedLength)
    {
      return FieldDescriptor.Create((ILengthFormatter) new VariableLengthFormatter(3, packedLength), FieldValidators.Hex, Formatters.Binary);
    }

    public static IFieldDescriptor AsciiLllCharacter(int maxLength)
    {
      return FieldDescriptor.AsciiVar(3, maxLength, FieldValidators.Ans);
    }

    public static IFieldDescriptor AsciiLllNumeric(int maxLength)
    {
      return FieldDescriptor.AsciiVar(3, maxLength, FieldValidators.N);
    }

    public static IFieldDescriptor AsciiNumeric(int length)
    {
      LambdaAdjuster lambdaAdjuster = new LambdaAdjuster((FuncStringString) null, (FuncStringString) (value => value.PadLeft(length, '0')));
      return FieldDescriptor.Create((ILengthFormatter) new FixedLengthFormatter(length), FieldValidators.N, Formatters.Ascii, (Adjuster) lambdaAdjuster);
    }

    public static IFieldDescriptor AsciiVar(int lengthIndicator, int maxLength, IFieldValidator validator)
    {
      return FieldDescriptor.Create((ILengthFormatter) new VariableLengthFormatter(lengthIndicator, maxLength), validator, Formatters.Ascii);
    }

    public static IFieldDescriptor BcdFixed(int length)
    {
      return FieldDescriptor.Create((ILengthFormatter) new FixedLengthFormatter(length), FieldValidators.N, Formatters.Bcd, (Adjuster) null);
    }

    public static IFieldDescriptor BcdVar(int lengthIndicator, int maxLength, IFormatter lengthFormatter)
    {
      return FieldDescriptor.Create((ILengthFormatter) new VariableLengthFormatter(lengthIndicator, maxLength, lengthFormatter), FieldValidators.N, Formatters.Bcd, (Adjuster) null);
    }

    public static IFieldDescriptor BinaryFixed(int packedLength)
    {
      return FieldDescriptor.Create((ILengthFormatter) new FixedLengthFormatter(packedLength), FieldValidators.Hex, Formatters.Binary);
    }

    public static IFieldDescriptor Create(ILengthFormatter lengthFormatter, IFieldValidator fieldValidator, IFormatter formatter, Adjuster adjuster)
    {
      return (IFieldDescriptor) new FieldDescriptor(lengthFormatter, fieldValidator, formatter, adjuster);
    }

    public static IFieldDescriptor Create(ILengthFormatter lengthFormatter, IFieldValidator fieldValidator, IFormatter formatter)
    {
      return (IFieldDescriptor) new FieldDescriptor(lengthFormatter, fieldValidator, formatter, (Adjuster) null);
    }

    public static IFieldDescriptor PanMask(IFieldDescriptor decoratedFieldDescriptor)
    {
      return (IFieldDescriptor) new PanMaskDecorator(decoratedFieldDescriptor);
    }

    public virtual string Display(string prefix, int fieldNumber, string value)
    {
      string str = value == null ? string.Empty : ((object) new StringBuilder().Append("[").Append(value).Append("]")).ToString();
      return ((object) new StringBuilder().AppendFormat("{0}{1}", (object) fieldNumber, (object) str)).ToString();
    }

    public virtual int GetPackedLength(string value)
    {
      return this.LengthFormatter.LengthOfLengthIndicator + this.Formatter.GetPackedLength(value.Length);
    }

    public virtual byte[] Pack(int fieldNumber, string value)
    {
      if (!this.LengthFormatter.IsValidLength(this.Formatter.GetPackedLength(value.Length)))
        throw new FieldLengthException(fieldNumber, "The field length is not valid");
      if (!this.Validator.IsValid(value))
        throw new FieldFormatException(fieldNumber, "Invalid value for field");
      int ofLengthIndicator = this.LengthFormatter.LengthOfLengthIndicator;
      int packedLength = this.Formatter.GetPackedLength(value.Length);
      byte[] msg = new byte[ofLengthIndicator + packedLength];
      this.LengthFormatter.Pack(msg, value.Length, 0);
      Array.Copy((Array) this.Formatter.GetBytes(value), 0, (Array) msg, ofLengthIndicator, packedLength);
      return msg;
    }

    public virtual string Unpack(int fieldNumber, byte[] data, int offset, out int newOffset)
    {
      int ofLengthIndicator = this.LengthFormatter.LengthOfLengthIndicator;
      int lengthOfField = this.LengthFormatter.GetLengthOfField(data, offset);
      int length = lengthOfField;
      if (this.Formatter is BcdFormatter)
        length = this.Formatter.GetPackedLength(length);
      byte[] data1 = new byte[length];
      Array.Copy((Array) data, offset + ofLengthIndicator, (Array) data1, 0, length);
      newOffset = offset + length + ofLengthIndicator;
      string str = this.Formatter.GetString(data1);
      if (!this.Validator.IsValid(str))
        throw new FieldFormatException(fieldNumber, "Invalid field format");
      int num = str.Length;
      if (this.Formatter is BinaryFormatter || this.Formatter is BcdFormatter)
      {
        num = this.Formatter.GetPackedLength(num);
        if (lengthOfField % 2 != 0)
          str = str.Substring(1);
      }
      if (this.LengthFormatter is VariableLengthFormatter && !this.LengthFormatter.IsValidLength(num))
        throw new FieldLengthException(fieldNumber, "Field is too long");
      else
        return str;
    }
  }
}
