// Decompiled with JetBrains decompiler
// Type: Novensys.ASN1.Type.Asn1DateType
// Assembly: Novensys.eCard.SDK, Version=1.1.56.0, Culture=neutral, PublicKeyToken=null
// MVID: 59F9E456-D3DA-4066-A4A4-692B516A775E
// Assembly location: C:\Program Files (x86)\CNAS\SIUI-SPITAL\Novensys.eCard.SDK.dll

using System;
using System.ComponentModel;

namespace Novensys.ASN1.Type
{
  [Serializable]
  public class Asn1DateType : Asn1TimeType
  {
    public override string TypeName
    {
      get
      {
        return "DATE";
      }
    }

    public Asn1DateType()
    {
    }

    public Asn1DateType(DateTime dateTime)
      : base(dateTime)
    {
    }

    public Asn1DateType(string val)
      : base(val)
    {
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal override int __getUniversalTagNumber()
    {
      return 31;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal override string __getXerTag()
    {
      return "DATE";
    }

    protected internal override void __initPropertySettings()
    {
      this.__setPropertySetting(0, 0);
      this.__setPropertySetting(1, 3);
      this.__setPropertySetting(2, 0);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal override void __read(Asn1TypeBerReader reader, bool isExplicit, bool primitive, long len)
    {
      reader.__decodeDateType(this, isExplicit, primitive, len);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal override void __write(Asn1TypeBerWriter writer)
    {
      writer.__encodeDateType(this, this.__getUniversalTagNumber(), 1);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal override void __write(Asn1TypeBerWriter writer, int tagNumber, int tagClass)
    {
      writer.__encodeDateType(this, tagNumber, tagClass);
    }

    public override int GetHashCode()
    {
      return base.GetHashCode();
    }
  }
}
