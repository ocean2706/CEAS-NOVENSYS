// Decompiled with JetBrains decompiler
// Type: Novensys.eCard.SDK.ISO8583.IFieldDescriptor
// Assembly: Novensys.eCard.SDK, Version=1.1.56.0, Culture=neutral, PublicKeyToken=null
// MVID: 59F9E456-D3DA-4066-A4A4-692B516A775E
// Assembly location: C:\Program Files (x86)\CNAS\SIUI-SPITAL\Novensys.eCard.SDK.dll

using Novensys.eCard.SDK.ISO8583.FieldValidator;
using Novensys.eCard.SDK.ISO8583.Formatter;
using Novensys.eCard.SDK.ISO8583.LengthFormatters;

namespace Novensys.eCard.SDK.ISO8583
{
  public interface IFieldDescriptor
  {
    Adjuster Adjuster { get; }

    IFormatter Formatter { get; }

    ILengthFormatter LengthFormatter { get; }

    IFieldValidator Validator { get; }

    string Display(string prefix, int fieldNumber, string value);

    int GetPackedLength(string value);

    byte[] Pack(int fieldNumber, string value);

    string Unpack(int fieldNumber, byte[] data, int offset, out int newOffset);
  }
}
