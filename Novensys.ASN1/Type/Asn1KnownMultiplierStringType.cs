// Decompiled with JetBrains decompiler
// Type: Novensys.ASN1.Type.Asn1KnownMultiplierStringType
// Assembly: Novensys.eCard.SDK, Version=1.1.56.0, Culture=neutral, PublicKeyToken=null
// MVID: 59F9E456-D3DA-4066-A4A4-692B516A775E
// Assembly location: C:\Program Files (x86)\CNAS\SIUI-SPITAL\Novensys.eCard.SDK.dll

using Novensys.ASN1;
using System;
using System.ComponentModel;

namespace Novensys.ASN1.Type
{
  [Serializable]
  public abstract class Asn1KnownMultiplierStringType : Asn1StringType
  {
    public Asn1KnownMultiplierStringType()
    {
    }

    public Asn1KnownMultiplierStringType(string str)
      : base(str)
    {
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal virtual int __getAlignedNbBitPerChar()
    {
      if (this.GetPermittedAlphabet() == null)
        return this.__getBaseAlphabetAlignedNbBitPerChar();
      else
        return this.__getPermittedAlphabetAlignedNbBitPerChar();
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal virtual string __getAlphabet()
    {
      return this.GetPermittedAlphabet() ?? this.__getBaseAlphabet();
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal virtual string __getBaseAlphabet()
    {
      return (string) null;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal abstract int __getBaseAlphabetAlignedNbBitPerChar();

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal abstract long __getBaseAlphabetLowerBound();

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal abstract int __getBaseAlphabetUnalignedNbBitPerChar();

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal abstract long __getBaseAlphabetUpperBound();

    protected internal virtual int __getPermittedAlphabetAlignedNbBitPerChar()
    {
      return 0;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal virtual long __getPermittedAlphabetLowerBound()
    {
      return 0L;
    }

    protected internal virtual int __getPermittedAlphabetUnalignedNbBitPerChar()
    {
      return 0;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal virtual long __getPermittedAlphabetUpperBound()
    {
      return 0L;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal virtual int __getUnalignedNbBitPerChar()
    {
      if (this.GetPermittedAlphabet() == null)
        return this.__getBaseAlphabetUnalignedNbBitPerChar();
      else
        return this.__getPermittedAlphabetUnalignedNbBitPerChar();
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal virtual bool __isAlphabetConstraintResetable()
    {
      return true;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal virtual bool __isValueOutOfRoot()
    {
      return this.GetLowerSize() != 0L && this.__value != null && (long) this.__value.Length < this.GetLowerSize() || this.GetUpperSize() != -1L && this.__value != null && (long) this.__value.Length > this.GetUpperSize();
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal override void __read(Asn1TypePerReader reader)
    {
      reader.__decodeKnownMultiplierStringValue(this);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal override void __write(Asn1TypePerWriter writer)
    {
      writer.__encodeKnownMultiplierStringType(this);
    }

    public virtual long GetLowerBound()
    {
      if (this.GetPermittedAlphabet() == null)
        return this.__getBaseAlphabetLowerBound();
      else
        return this.__getPermittedAlphabetLowerBound();
    }

    public virtual string GetPermittedAlphabet()
    {
      return (string) null;
    }

    public virtual long GetUpperBound()
    {
      if (this.GetPermittedAlphabet() == null)
        return this.__getBaseAlphabetUpperBound();
      else
        return this.__getPermittedAlphabetUpperBound();
    }

    public override void Validate()
    {
      base.Validate();
      if (this.__isPerConstraintExtensible())
        return;
      string permittedAlphabet = this.GetPermittedAlphabet();
      if (permittedAlphabet != null && this.__value != null)
      {
        for (int index = 0; index < this.__value.Length; ++index)
        {
          if (permittedAlphabet.IndexOf(this.__value[index]) == -1)
            throw new Asn1ValidationException(31, "for character '" + (object) this.__value[index] + "' and alphabet '" + permittedAlphabet + "' in type <" + this.GetType().FullName + ">");
        }
      }
    }
  }
}
