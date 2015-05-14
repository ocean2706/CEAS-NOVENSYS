// Decompiled with JetBrains decompiler
// Type: Novensys.ASN1.Type.Asn1NumericStringType
// Assembly: Novensys.eCard.SDK, Version=1.1.56.0, Culture=neutral, PublicKeyToken=null
// MVID: 59F9E456-D3DA-4066-A4A4-692B516A775E
// Assembly location: C:\Program Files (x86)\CNAS\SIUI-SPITAL\Novensys.eCard.SDK.dll

using System;
using System.ComponentModel;

namespace Novensys.ASN1.Type
{
  [Serializable]
  public class Asn1NumericStringType : Asn1KnownMultiplierStringType
  {
    public override string TypeName
    {
      get
      {
        return "NumericString";
      }
    }

    public Asn1NumericStringType()
    {
    }

    public Asn1NumericStringType(string str)
      : base(str)
    {
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal override string __getBaseAlphabet()
    {
      return " 0123456789";
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal override int __getBaseAlphabetAlignedNbBitPerChar()
    {
      return 4;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal override long __getBaseAlphabetLowerBound()
    {
      return 32L;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal override int __getBaseAlphabetUnalignedNbBitPerChar()
    {
      return 4;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal override long __getBaseAlphabetUpperBound()
    {
      return 57L;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal override int __getUniversalTagNumber()
    {
      return 18;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal override bool __isAlphabetConstraintResetable()
    {
      return false;
    }

    protected internal override void __setTypeValue(Asn1Type typeInstance)
    {
      if (typeInstance == null)
      {
        this.ResetType();
      }
      else
      {
        if (!(typeInstance is Asn1NumericStringType))
          return;
        this.SetValue(((Asn1StringType) typeInstance).GetStringValue());
      }
    }

    public override bool Equals(object anObject)
    {
      return this == anObject || anObject is Asn1NumericStringType && this.HasEqualValue((Asn1StringType) anObject);
    }

    public override int GetHashCode()
    {
      return base.GetHashCode();
    }
  }
}
