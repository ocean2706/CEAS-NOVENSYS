// Decompiled with JetBrains decompiler
// Type: Novensys.eCard.SDK.ISO8583.IField
// Assembly: Novensys.eCard.SDK, Version=1.1.56.0, Culture=neutral, PublicKeyToken=null
// MVID: 59F9E456-D3DA-4066-A4A4-692B516A775E
// Assembly location: C:\Program Files (x86)\CNAS\SIUI-SPITAL\Novensys.eCard.SDK.dll

namespace Novensys.eCard.SDK.ISO8583
{
  public interface IField
  {
    int FieldNumber { get; }

    int PackedLength { get; }

    string Value { get; set; }

    byte[] ToMsg();

    string ToString(string prefix);

    string ToString();

    int Unpack(byte[] msg, int offset);
  }
}
