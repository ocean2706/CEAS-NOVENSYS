// Decompiled with JetBrains decompiler
// Type: Novensys.ASN1.Type.Asn1DurationType
// Assembly: Novensys.eCard.SDK, Version=1.1.56.0, Culture=neutral, PublicKeyToken=null
// MVID: 59F9E456-D3DA-4066-A4A4-692B516A775E
// Assembly location: C:\Program Files (x86)\CNAS\SIUI-SPITAL\Novensys.eCard.SDK.dll

using System;
using System.ComponentModel;

namespace Novensys.ASN1.Type
{
  [Serializable]
  public class Asn1DurationType : Asn1TimeType
  {
    public override string TypeName
    {
      get
      {
        return "DURATION";
      }
    }

    public Asn1DurationType()
    {
    }

    public Asn1DurationType(DateTime dateTime)
      : base(dateTime)
    {
    }

    public Asn1DurationType(string val)
      : base(val)
    {
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal override int __getUniversalTagNumber()
    {
      return 34;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal override string __getXerTag()
    {
      return "DURATION";
    }

    protected internal override void __initPropertySettings()
    {
      this.__setPropertySetting(0, 3);
      this.__setPropertySetting(5, 1);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal override void __read(Asn1TypeBerReader reader, bool isExplicit, bool primitive, long len)
    {
      reader.__decodeDurationType(this, isExplicit, primitive, len);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal override void __write(Asn1TypeBerWriter writer)
    {
      writer.__encodeDurationType(this, this.__getUniversalTagNumber(), 1);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal override void __write(Asn1TypeBerWriter writer, int tagNumber, int tagClass)
    {
      writer.__encodeDurationType(this, tagNumber, tagClass);
    }

    public override int GetHashCode()
    {
      return base.GetHashCode();
    }
  }
}
