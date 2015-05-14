// Decompiled with JetBrains decompiler
// Type: Novensys.eCard.SDK.ISO8583.PanMaskDecorator
// Assembly: Novensys.eCard.SDK, Version=1.1.56.0, Culture=neutral, PublicKeyToken=null
// MVID: 59F9E456-D3DA-4066-A4A4-692B516A775E
// Assembly location: C:\Program Files (x86)\CNAS\SIUI-SPITAL\Novensys.eCard.SDK.dll

using Novensys.eCard.SDK.ISO8583.FieldValidator;
using Novensys.eCard.SDK.ISO8583.Formatter;
using Novensys.eCard.SDK.ISO8583.LengthFormatters;

namespace Novensys.eCard.SDK.ISO8583
{
  public class PanMaskDecorator : IFieldDescriptor
  {
    private readonly IFieldDescriptor _decoratedFieldDescriptor;

    public ILengthFormatter LengthFormatter
    {
      get
      {
        return this._decoratedFieldDescriptor.LengthFormatter;
      }
    }

    public IFieldValidator Validator
    {
      get
      {
        return this._decoratedFieldDescriptor.Validator;
      }
    }

    public IFormatter Formatter
    {
      get
      {
        return this._decoratedFieldDescriptor.Formatter;
      }
    }

    public Adjuster Adjuster
    {
      get
      {
        return this._decoratedFieldDescriptor.Adjuster;
      }
    }

    public PanMaskDecorator(IFieldDescriptor decoratedFieldDescriptor)
    {
      this._decoratedFieldDescriptor = decoratedFieldDescriptor;
    }

    public int GetPackedLength(string value)
    {
      return this._decoratedFieldDescriptor.GetPackedLength(value);
    }

    public string Display(string prefix, int fieldNumber, string value)
    {
      return this._decoratedFieldDescriptor.Display(prefix, fieldNumber, Utils.MaskPan(value));
    }

    public string Unpack(int fieldNumber, byte[] data, int offset, out int newOffset)
    {
      return this._decoratedFieldDescriptor.Unpack(fieldNumber, data, offset, out newOffset);
    }

    public byte[] Pack(int fieldNumber, string value)
    {
      return this._decoratedFieldDescriptor.Pack(fieldNumber, value);
    }
  }
}
