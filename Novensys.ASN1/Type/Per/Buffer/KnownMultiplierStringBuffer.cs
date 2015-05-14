// Decompiled with JetBrains decompiler
// Type: Novensys.ASN1.Type.Per.Buffer.KnownMultiplierStringBuffer
// Assembly: Novensys.eCard.SDK, Version=1.1.56.0, Culture=neutral, PublicKeyToken=null
// MVID: 59F9E456-D3DA-4066-A4A4-692B516A775E
// Assembly location: C:\Program Files (x86)\CNAS\SIUI-SPITAL\Novensys.eCard.SDK.dll

using Novensys.ASN1.Type;

namespace Novensys.ASN1.Type.Per.Buffer
{
  public abstract class KnownMultiplierStringBuffer : AbstractBitField
  {
    protected int _firstBitIndex;
    protected int _length;
    protected int _nbBitsPerChar;
    protected int _nbBytesPerChar;
    protected Asn1KnownMultiplierStringType _typeInstance;

    public override int getNbElements()
    {
      return this._length;
    }

    public override int getNbElementsFrom(int start)
    {
      return this._length - start;
    }

    protected internal override void reset()
    {
      this._typeInstance = (Asn1KnownMultiplierStringType) null;
      this._length = 0;
      this._nbBitsPerChar = 0;
      this._nbBytesPerChar = 0;
      this._firstBitIndex = 0;
    }

    public virtual void setParameters(Asn1KnownMultiplierStringType typeInstance, int nbBitsPerChar)
    {
      this._typeInstance = typeInstance;
      this._nbBitsPerChar = nbBitsPerChar;
      this._nbBytesPerChar = (nbBitsPerChar + 7) / 8;
      this._firstBitIndex = 7 - (nbBitsPerChar + 7) % 8;
    }

    public abstract void updateTypeInstance();
  }
}
