// Decompiled with JetBrains decompiler
// Type: Novensys.ASN1.Type.Per.Time.Asn1IntegerTypeAdapter
// Assembly: Novensys.eCard.SDK, Version=1.1.56.0, Culture=neutral, PublicKeyToken=null
// MVID: 59F9E456-D3DA-4066-A4A4-692B516A775E
// Assembly location: C:\Program Files (x86)\CNAS\SIUI-SPITAL\Novensys.eCard.SDK.dll

using Novensys.ASN1;
using Novensys.ASN1.Type;
using System;

namespace Novensys.ASN1.Type.Per.Time
{
  public abstract class Asn1IntegerTypeAdapter : DynAsn1IntegerType
  {
    protected Asn1TimeType typeInstance;

    public Asn1IntegerTypeAdapter(Asn1TimeType typeInstance)
    {
      this.typeInstance = (Asn1TimeType) null;
      this.typeInstance = typeInstance;
    }

    public Asn1IntegerTypeAdapter(Asn1TimeType typeInstance, long lowerBound)
      : base(lowerBound)
    {
      this.typeInstance = (Asn1TimeType) null;
      this.typeInstance = typeInstance;
    }

    public Asn1IntegerTypeAdapter(Asn1TimeType typeInstance, long lowerBound, long upperBound, bool extensible)
      : base(lowerBound, upperBound, extensible)
    {
      this.typeInstance = (Asn1TimeType) null;
      this.typeInstance = typeInstance;
    }

    protected internal override void __read(Asn1TypePerReader reader)
    {
      base.__read(reader);
      try
      {
        this.postRead();
      }
      catch (InvalidCastException ex)
      {
        throw new Asn1Exception(64, ex.Message + " for type <" + this.typeInstance.GetType().FullName + ">");
      }
    }

    protected internal override void __write(Asn1TypePerWriter writer)
    {
      this.preWrite();
      base.__write(writer);
    }

    protected abstract void postRead();

    protected abstract void preWrite();
  }
}
