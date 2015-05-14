// Decompiled with JetBrains decompiler
// Type: Novensys.eCard.SDK.ISO8583.Field
// Assembly: Novensys.eCard.SDK, Version=1.1.56.0, Culture=neutral, PublicKeyToken=null
// MVID: 59F9E456-D3DA-4066-A4A4-692B516A775E
// Assembly location: C:\Program Files (x86)\CNAS\SIUI-SPITAL\Novensys.eCard.SDK.dll

using Novensys.eCard.SDK.ISO8583.FieldValidator;

namespace Novensys.eCard.SDK.ISO8583
{
  public class Field : IField
  {
    protected IFieldDescriptor _fieldDescriptor;
    private string _value;

    public string Value
    {
      get
      {
        return this._fieldDescriptor.Adjuster == null ? this._value : this._fieldDescriptor.Adjuster.Get(this._value);
      }
      set
      {
        this._value = this._fieldDescriptor.Adjuster == null ? value : this._fieldDescriptor.Adjuster.Set(value);
      }
    }

    public int FieldNumber { get; private set; }

    public int PackedLength
    {
      get
      {
        return this._fieldDescriptor.GetPackedLength(this.Value);
      }
    }

    public Field(int fieldNumber, IFieldDescriptor fieldDescriptor)
    {
      this.FieldNumber = fieldNumber;
      this._fieldDescriptor = fieldDescriptor;
    }

    public override string ToString()
    {
      return this.ToString(string.Empty);
    }

    public string ToString(string prefix)
    {
      return this._fieldDescriptor.Display(prefix, this.FieldNumber, this.Value);
    }

    public int Unpack(byte[] msg, int offset)
    {
      int newOffset;
      this.Value = this._fieldDescriptor.Unpack(this.FieldNumber, msg, offset, out newOffset);
      return newOffset;
    }

    public byte[] ToMsg()
    {
      return this._fieldDescriptor.Pack(this.FieldNumber, this.Value);
    }

    public static IField AsciiFixed(int fieldNumber, int packedLength, IFieldValidator validator)
    {
      return (IField) new Field(fieldNumber, FieldDescriptor.AsciiFixed(packedLength, validator));
    }

    public static IField AsciiVar(int fieldNumber, int lengthIndicator, int maxLength, IFieldValidator validator)
    {
      return (IField) new Field(fieldNumber, FieldDescriptor.AsciiVar(lengthIndicator, maxLength, validator));
    }

    public static IField BinFixed(int fieldNumber, int packedLength)
    {
      return (IField) new Field(fieldNumber, FieldDescriptor.BinaryFixed(packedLength));
    }
  }
}
