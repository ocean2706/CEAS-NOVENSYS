// Decompiled with JetBrains decompiler
// Type: Novensys.ASN1.Type.Per.Buffer.AbstractBuffer
// Assembly: Novensys.eCard.SDK, Version=1.1.56.0, Culture=neutral, PublicKeyToken=null
// MVID: 59F9E456-D3DA-4066-A4A4-692B516A775E
// Assembly location: C:\Program Files (x86)\CNAS\SIUI-SPITAL\Novensys.eCard.SDK.dll

using Novensys.ASN1.Type;

namespace Novensys.ASN1.Type.Per.Buffer
{
  public abstract class AbstractBuffer
  {
    public abstract int getNbElements();

    public abstract int getNbElementsFrom(int start);

    public abstract void readElements(int start, int length, Asn1TypePerReader reader);

    protected internal abstract void reset();

    public abstract void writeElements(int start, int length, Asn1TypePerWriter writer);
  }
}
