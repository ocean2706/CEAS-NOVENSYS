// Decompiled with JetBrains decompiler
// Type: Novensys.ASN1.Type.Per.Time.DynAsn1IntegerType
// Assembly: Novensys.eCard.SDK, Version=1.1.56.0, Culture=neutral, PublicKeyToken=null
// MVID: 59F9E456-D3DA-4066-A4A4-692B516A775E
// Assembly location: C:\Program Files (x86)\CNAS\SIUI-SPITAL\Novensys.eCard.SDK.dll

using Novensys.ASN1.Type;

namespace Novensys.ASN1.Type.Per.Time
{
  public class DynAsn1IntegerType : Asn1IntegerType
  {
    private bool extensible;
    private long lowerBound;
    private bool lowerBoundDefined;
    private long upperBound;
    private bool upperBoundDefined;

    public DynAsn1IntegerType()
    {
      this.lowerBoundDefined = false;
      this.upperBoundDefined = false;
      this.extensible = false;
    }

    public DynAsn1IntegerType(long lowerBound)
    {
      this.lowerBoundDefined = false;
      this.upperBoundDefined = false;
      this.extensible = false;
      this.lowerBoundDefined = true;
      this.lowerBound = lowerBound;
    }

    public DynAsn1IntegerType(long lowerBound, long upperBound, bool extensible)
    {
      this.lowerBoundDefined = false;
      this.upperBoundDefined = false;
      this.extensible = false;
      this.lowerBoundDefined = true;
      this.lowerBound = lowerBound;
      this.upperBoundDefined = true;
      this.upperBound = upperBound;
      this.extensible = extensible;
    }

    protected internal override bool __isInRootValueSet()
    {
      if (this.lowerBoundDefined && this.upperBoundDefined)
        return this.lowerBound <= this.LongValue && this.LongValue <= this.upperBound;
      if (this.lowerBoundDefined)
        return this.lowerBound <= this.LongValue;
      if (this.upperBoundDefined)
        return this.LongValue <= this.upperBound;
      else
        return true;
    }

    protected internal override bool __isPerConstraintExtensible()
    {
      return this.extensible;
    }

    public override long GetLowerBound()
    {
      return this.lowerBound;
    }

    public override long GetUpperBound()
    {
      return this.upperBound;
    }

    public override bool IsLowerBoundDefined()
    {
      return this.lowerBoundDefined;
    }

    public override bool IsUpperBoundDefined()
    {
      return this.upperBoundDefined;
    }
  }
}
