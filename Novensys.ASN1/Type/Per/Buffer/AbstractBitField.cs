// Decompiled with JetBrains decompiler
// Type: Novensys.ASN1.Type.Per.Buffer.AbstractBitField
// Assembly: Novensys.eCard.SDK, Version=1.1.56.0, Culture=neutral, PublicKeyToken=null
// MVID: 59F9E456-D3DA-4066-A4A4-692B516A775E
// Assembly location: C:\Program Files (x86)\CNAS\SIUI-SPITAL\Novensys.eCard.SDK.dll

using Novensys.ASN1.IO;
using Novensys.ASN1.Type;

namespace Novensys.ASN1.Type.Per.Buffer
{
  public abstract class AbstractBitField : AbstractBuffer
  {
    public virtual void read(IAsn1BitInputStream input)
    {
      this.read(0, this.getNbElements(), input);
    }

    public abstract void read(int start, int length, IAsn1BitInputStream input);

    public override void readElements(int start, int length, Asn1TypePerReader reader)
    {
      reader.__read(this, start, length);
    }

    public virtual void write(IAsn1BitOutputStream output)
    {
      this.write(0, this.getNbElements(), output);
    }

    public abstract void write(int start, int length, IAsn1BitOutputStream output);

    public override void writeElements(int start, int length, Asn1TypePerWriter writer)
    {
      writer.__write(this, start, length);
    }
  }
}
