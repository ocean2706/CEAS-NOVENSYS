// Decompiled with JetBrains decompiler
// Type: Novensys.ASN1.Type.Asn1TimeOfDayType
// Assembly: Novensys.eCard.SDK, Version=1.1.56.0, Culture=neutral, PublicKeyToken=null
// MVID: 59F9E456-D3DA-4066-A4A4-692B516A775E
// Assembly location: C:\Program Files (x86)\CNAS\SIUI-SPITAL\Novensys.eCard.SDK.dll

using System;
using System.ComponentModel;

namespace Novensys.ASN1.Type
{
  [Serializable]
  public class Asn1TimeOfDayType : Asn1TimeType
  {
    public override string TypeName
    {
      get
      {
        return "TIME-OF-DAY";
      }
    }

    public Asn1TimeOfDayType()
    {
    }

    public Asn1TimeOfDayType(DateTime dateTime)
      : base(dateTime)
    {
    }

    public Asn1TimeOfDayType(string val)
      : base(val)
    {
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal override int __getUniversalTagNumber()
    {
      return 32;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal override string __getXerTag()
    {
      return "TIME_OF_DAY";
    }

    protected internal override void __initPropertySettings()
    {
      this.__setPropertySetting(0, 1);
      this.__setPropertySetting(3, 2);
      this.__setPropertySetting(4, 0);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal override void __read(Asn1TypeBerReader reader, bool isExplicit, bool primitive, long len)
    {
      reader.__decodeTimeOfDayType(this, isExplicit, primitive, len);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal override void __write(Asn1TypeBerWriter writer)
    {
      writer.__encodeTimeOfDayType(this, this.__getUniversalTagNumber(), 1);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal override void __write(Asn1TypeBerWriter writer, int tagNumber, int tagClass)
    {
      writer.__encodeTimeOfDayType(this, tagNumber, tagClass);
    }

    public override int GetHashCode()
    {
      return base.GetHashCode();
    }
  }
}
