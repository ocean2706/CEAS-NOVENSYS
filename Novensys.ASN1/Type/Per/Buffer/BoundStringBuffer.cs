// Decompiled with JetBrains decompiler
// Type: Novensys.ASN1.Type.Per.Buffer.BoundStringBuffer
// Assembly: Novensys.eCard.SDK, Version=1.1.56.0, Culture=neutral, PublicKeyToken=null
// MVID: 59F9E456-D3DA-4066-A4A4-692B516A775E
// Assembly location: C:\Program Files (x86)\CNAS\SIUI-SPITAL\Novensys.eCard.SDK.dll

using Novensys.ASN1.IO;
using Novensys.ASN1.Type;
using System.Text;

namespace Novensys.ASN1.Type.Per.Buffer
{
  public class BoundStringBuffer : KnownMultiplierStringBuffer
  {
    private StringBuilder _valueHelper = new StringBuilder();
    private IntegerBuffer buffer = new IntegerBuffer();
    private long _lowerBound;
    private string _value;

    public override void read(int start, int length, IAsn1BitInputStream input)
    {
      for (int index = 0; index < length; ++index)
      {
        this.buffer.setGranularity(1);
        this.buffer.read(0, this._nbBitsPerChar, input);
        this._valueHelper.Append((char) (this.buffer.getLong() + this._lowerBound));
        this.buffer.reset();
      }
    }

    protected internal override void reset()
    {
      base.reset();
      this._lowerBound = 0L;
      this._valueHelper.Length = 0;
      this._value = (string) null;
      this.buffer.reset();
    }

    public virtual void setParameters(Asn1KnownMultiplierStringType typeInstance, int nbBitsPerChar, long lowerBound)
    {
      base.setParameters(typeInstance, nbBitsPerChar);
      this._lowerBound = lowerBound;
      this._value = typeInstance.StringValue;
      if (this._value == null)
        this._length = 0;
      else
        this._length = this._value.Length;
    }

    public override void updateTypeInstance()
    {
      this._typeInstance.SetValue(((object) this._valueHelper).ToString());
    }

    public override void write(int start, int length, IAsn1BitOutputStream output)
    {
      for (int index = 0; index < length; ++index)
      {
        this.buffer.setLong((long) this._value[start + index] - this._lowerBound);
        this.buffer.setLength(1, this._nbBitsPerChar);
        this.buffer.write(0, this._nbBitsPerChar, output);
        this.buffer.reset();
      }
    }
  }
}
