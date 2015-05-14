// Decompiled with JetBrains decompiler
// Type: Novensys.ASN1.Type.Per.Buffer.TypeVectorBuffer
// Assembly: Novensys.eCard.SDK, Version=1.1.56.0, Culture=neutral, PublicKeyToken=null
// MVID: 59F9E456-D3DA-4066-A4A4-692B516A775E
// Assembly location: C:\Program Files (x86)\CNAS\SIUI-SPITAL\Novensys.eCard.SDK.dll

using Novensys.ASN1.Type;

namespace Novensys.ASN1.Type.Per.Buffer
{
  public class TypeVectorBuffer : AbstractBuffer
  {
    private Asn1ConstructedOfType _typeInstance;

    public override int getNbElements()
    {
      return this._typeInstance.GetAsn1TypeList().Count;
    }

    public override int getNbElementsFrom(int start)
    {
      return this._typeInstance.GetAsn1TypeList().Count - start;
    }

    public override void readElements(int start, int length, Asn1TypePerReader reader)
    {
      for (int index = 0; index < length; ++index)
        reader.__decodeConstructedOfTypeElement(this._typeInstance);
    }

    protected internal override void reset()
    {
      this._typeInstance = (Asn1ConstructedOfType) null;
    }

    public void setAsn1TypeList(Asn1ConstructedOfType typeInstance)
    {
      this._typeInstance = typeInstance;
    }

    public override void writeElements(int start, int length, Asn1TypePerWriter writer)
    {
      for (int index = 0; index < length; ++index)
        writer.__encodeConstructedOfTypeElement(this._typeInstance, start + index);
    }
  }
}
