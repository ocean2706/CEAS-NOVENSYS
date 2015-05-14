// Decompiled with JetBrains decompiler
// Type: Novensys.ASN1.Type.Asn1BMPStringType
// Assembly: Novensys.eCard.SDK, Version=1.1.56.0, Culture=neutral, PublicKeyToken=null
// MVID: 59F9E456-D3DA-4066-A4A4-692B516A775E
// Assembly location: C:\Program Files (x86)\CNAS\SIUI-SPITAL\Novensys.eCard.SDK.dll

using System;
using System.ComponentModel;
using System.Text;

namespace Novensys.ASN1.Type
{
  [Serializable]
  public class Asn1BMPStringType : Asn1KnownMultiplierStringType
  {
    public override string PrintableValue
    {
      get
      {
        return this.__getCharsDefnPrintableValue(true);
      }
    }

    public override string TypeName
    {
      get
      {
        return "BMPString";
      }
    }

    public Asn1BMPStringType()
    {
    }

    public Asn1BMPStringType(string str)
      : base(str)
    {
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal override int __getBaseAlphabetAlignedNbBitPerChar()
    {
      return 16;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal override long __getBaseAlphabetLowerBound()
    {
      return 0L;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal override int __getBaseAlphabetUnalignedNbBitPerChar()
    {
      return 16;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal override long __getBaseAlphabetUpperBound()
    {
      return (long) ushort.MaxValue;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal override int __getUniversalTagNumber()
    {
      return 30;
    }

    protected internal override void __setTypeValue(Asn1Type typeInstance)
    {
      if (typeInstance == null)
      {
        this.ResetType();
      }
      else
      {
        if (!(typeInstance is Asn1BMPStringType))
          return;
        this.SetValue(((Asn1StringType) typeInstance).GetStringValue());
      }
    }

    public override bool Equals(object anObject)
    {
      return this == anObject || anObject is Asn1BMPStringType && this.HasEqualValue((Asn1StringType) anObject);
    }

    public override byte[] GetByteArrayValue()
    {
      return base.GetByteArrayValue((Encoding) new UnicodeEncoding(true, false));
    }

    public override int GetHashCode()
    {
      return base.GetHashCode();
    }

    public override void SetValue(byte[] data)
    {
      base.SetValue(data, (Encoding) new UnicodeEncoding(true, false));
    }
  }
}
