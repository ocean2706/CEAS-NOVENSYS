// Decompiled with JetBrains decompiler
// Type: Novensys.eCard.SDK.ISO8583.LengthFormatters.FixedLengthFormatter
// Assembly: Novensys.eCard.SDK, Version=1.1.56.0, Culture=neutral, PublicKeyToken=null
// MVID: 59F9E456-D3DA-4066-A4A4-692B516A775E
// Assembly location: C:\Program Files (x86)\CNAS\SIUI-SPITAL\Novensys.eCard.SDK.dll

namespace Novensys.eCard.SDK.ISO8583.LengthFormatters
{
  public class FixedLengthFormatter : ILengthFormatter
  {
    private readonly int _packedLength;

    public int LengthOfLengthIndicator
    {
      get
      {
        return 0;
      }
    }

    public string MaxLength
    {
      get
      {
        return this._packedLength.ToString();
      }
    }

    public string Description
    {
      get
      {
        return "Fixed";
      }
    }

    public FixedLengthFormatter(int packedLength)
    {
      this._packedLength = packedLength;
    }

    public int GetLengthOfField(byte[] msg, int offset)
    {
      return this._packedLength;
    }

    public int Pack(byte[] msg, int length, int offset)
    {
      return offset;
    }

    public bool IsValidLength(int packedLength)
    {
      return packedLength == this._packedLength;
    }
  }
}
