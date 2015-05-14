// Decompiled with JetBrains decompiler
// Type: Novensys.ASN1.Type.Asn1DateTimeType
// Assembly: Novensys.eCard.SDK, Version=1.1.56.0, Culture=neutral, PublicKeyToken=null
// MVID: 59F9E456-D3DA-4066-A4A4-692B516A775E
// Assembly location: C:\Program Files (x86)\CNAS\SIUI-SPITAL\Novensys.eCard.SDK.dll

using System;
using System.ComponentModel;

namespace Novensys.ASN1.Type
{
  [Serializable]
  public class Asn1DateTimeType : Asn1TimeType
  {
    public override string TypeName
    {
      get
      {
        return "DATE-TIME";
      }
    }

    public Asn1DateTimeType()
    {
    }

    public Asn1DateTimeType(DateTime dateTime)
      : base(dateTime)
    {
    }

    public Asn1DateTimeType(string val)
      : base(val)
    {
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal override int __getUniversalTagNumber()
    {
      return 33;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal override string __getXerTag()
    {
      return "DATE_TIME";
    }

    protected internal override void __initPropertySettings()
    {
      this.__setPropertySetting(0, 2);
      this.__setPropertySetting(1, 3);
      this.__setPropertySetting(2, 0);
      this.__setPropertySetting(3, 2);
      this.__setPropertySetting(4, 0);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal override void __read(Asn1TypeBerReader reader, bool isExplicit, bool primitive, long len)
    {
      reader.__decodeDateTimeType(this, isExplicit, primitive, len);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal override void __write(Asn1TypeBerWriter writer)
    {
      writer.__encodeDateTimeType(this, this.__getUniversalTagNumber(), 1);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal override void __write(Asn1TypeBerWriter writer, int tagNumber, int tagClass)
    {
      writer.__encodeDateTimeType(this, tagNumber, tagClass);
    }

    public override int GetHashCode()
    {
      return base.GetHashCode();
    }
  }
}
