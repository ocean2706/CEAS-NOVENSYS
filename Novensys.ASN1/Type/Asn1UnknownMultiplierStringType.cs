// Decompiled with JetBrains decompiler
// Type: Novensys.ASN1.Type.Asn1UnknownMultiplierStringType
// Assembly: Novensys.eCard.SDK, Version=1.1.56.0, Culture=neutral, PublicKeyToken=null
// MVID: 59F9E456-D3DA-4066-A4A4-692B516A775E
// Assembly location: C:\Program Files (x86)\CNAS\SIUI-SPITAL\Novensys.eCard.SDK.dll

using System;
using System.ComponentModel;

namespace Novensys.ASN1.Type
{
  [Serializable]
  public abstract class Asn1UnknownMultiplierStringType : Asn1StringType
  {
    public Asn1UnknownMultiplierStringType()
    {
    }

    public Asn1UnknownMultiplierStringType(string str)
      : base(str)
    {
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal override void __read(Asn1TypePerReader reader)
    {
      reader.__decodeUnknownMultiplierStringValue(this);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal override void __write(Asn1TypePerWriter writer)
    {
      writer.__encodeUnknownMultiplierStringType(this);
    }
  }
}
