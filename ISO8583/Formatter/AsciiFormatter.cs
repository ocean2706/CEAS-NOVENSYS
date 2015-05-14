// Decompiled with JetBrains decompiler
// Type: Novensys.eCard.SDK.ISO8583.Formatter.AsciiFormatter
// Assembly: Novensys.eCard.SDK, Version=1.1.56.0, Culture=neutral, PublicKeyToken=null
// MVID: 59F9E456-D3DA-4066-A4A4-692B516A775E
// Assembly location: C:\Program Files (x86)\CNAS\SIUI-SPITAL\Novensys.eCard.SDK.dll

using System.Text;

namespace Novensys.eCard.SDK.ISO8583.Formatter
{
  public class AsciiFormatter : IFormatter
  {
    public byte[] GetBytes(string value)
    {
      return Encoding.ASCII.GetBytes(value);
    }

    public string GetString(byte[] data)
    {
      return Encoding.ASCII.GetString(data);
    }

    public int GetPackedLength(int unpackedLength)
    {
      return unpackedLength;
    }
  }
}
