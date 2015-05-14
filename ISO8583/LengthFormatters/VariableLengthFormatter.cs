// Decompiled with JetBrains decompiler
// Type: Novensys.eCard.SDK.ISO8583.LengthFormatters.VariableLengthFormatter
// Assembly: Novensys.eCard.SDK, Version=1.1.56.0, Culture=neutral, PublicKeyToken=null
// MVID: 59F9E456-D3DA-4066-A4A4-692B516A775E
// Assembly location: C:\Program Files (x86)\CNAS\SIUI-SPITAL\Novensys.eCard.SDK.dll

using Novensys.eCard.SDK.ISO8583.Formatter;
using System;

namespace Novensys.eCard.SDK.ISO8583.LengthFormatters
{
  public class VariableLengthFormatter : ILengthFormatter
  {
    private readonly IFormatter _lengthFormatter;
    private readonly int _lengthIndicator;
    private readonly int _maxLength;

    public int LengthOfLengthIndicator { get; private set; }

    public string MaxLength
    {
      get
      {
        return ".." + (object) this._maxLength;
      }
    }

    public string Description
    {
      get
      {
        return new string('L', 1 + (int) Math.Log10((double) this._maxLength)) + "Var";
      }
    }

    public VariableLengthFormatter(int lengthIndicator, int maxLength, IFormatter lengthFormatter)
    {
      this._lengthIndicator = lengthIndicator;
      this._maxLength = maxLength;
      this._lengthFormatter = lengthFormatter;
      this.LengthOfLengthIndicator = this._lengthFormatter.GetPackedLength(this._lengthIndicator);
    }

    public VariableLengthFormatter(int lengthIndicator, int maxLength)
      : this(lengthIndicator, maxLength, (IFormatter) new AsciiFormatter())
    {
    }

    public int GetLengthOfField(byte[] msg, int offset)
    {
      int ofLengthIndicator = this.LengthOfLengthIndicator;
      byte[] data = new byte[ofLengthIndicator];
      Array.Copy((Array) msg, offset, (Array) data, 0, ofLengthIndicator);
      return int.Parse(this._lengthFormatter.GetString(data));
    }

    public int Pack(byte[] msg, int length, int offset)
    {
      Array.Copy((Array) this._lengthFormatter.GetBytes(length.ToString().PadLeft(this.LengthOfLengthIndicator, '0')), 0, (Array) msg, offset, this.LengthOfLengthIndicator);
      return offset + this.LengthOfLengthIndicator;
    }

    public bool IsValidLength(int packedLength)
    {
      return packedLength <= this._maxLength;
    }
  }
}
