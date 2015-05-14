// Decompiled with JetBrains decompiler
// Type: Novensys.ASN1.Type.Asn1IntegerType
// Assembly: Novensys.eCard.SDK, Version=1.1.56.0, Culture=neutral, PublicKeyToken=null
// MVID: 59F9E456-D3DA-4066-A4A4-692B516A775E
// Assembly location: C:\Program Files (x86)\CNAS\SIUI-SPITAL\Novensys.eCard.SDK.dll

using Novensys.ASN1;
using Novensys.ASN1.Util;
using System;
using System.ComponentModel;

namespace Novensys.ASN1.Type
{
  [Serializable]
  public class Asn1IntegerType : Asn1Type
  {
    private BigInteger __bigValue;
    private long __value;

    public BigInteger BigIntegerValue
    {
      get
      {
        return this.GetBigIntegerValue();
      }
      set
      {
        this.SetValue(value);
      }
    }

    public int IntValue
    {
      get
      {
        return this.GetIntValue();
      }
      set
      {
        this.SetValue((long) value);
      }
    }

    public long LongValue
    {
      get
      {
        return this.GetLongValue();
      }
      set
      {
        this.SetValue(value);
      }
    }

    public override string PrintableValue
    {
      get
      {
        if (this.__bigValue != null)
          return ((object) this.__bigValue).ToString();
        else
          return this.GetIdentifier() ?? this.__value.ToString();
      }
    }

    public override string TypeName
    {
      get
      {
        return "INTEGER";
      }
    }

    public Asn1IntegerType()
    {
      this.ResetType();
    }

    public Asn1IntegerType(long val)
    {
      this.SetValue(val);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal virtual string __getIdentifier(string[] idSet)
    {
      long[] namedNumberSet = this.__getNamedNumberSet();
      if (namedNumberSet != null)
      {
        for (int index = 0; index < namedNumberSet.Length; ++index)
        {
          if (this.__value == namedNumberSet[index])
            return idSet[index];
        }
      }
      return (string) null;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal virtual string[] __getIdentifierSet()
    {
      return (string[]) null;
    }

    protected internal virtual long[] __getNamedNumberSet()
    {
      return (long[]) null;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal override int __getUniversalTagNumber()
    {
      return 2;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal virtual string[] __getXerIdentifierSet()
    {
      return (string[]) null;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal override string __getXerTag()
    {
      return "INTEGER";
    }

    protected internal virtual bool __isInRootValueSet()
    {
      return true;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal virtual bool __isPerReverseOctets()
    {
      return false;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal virtual bool __isValueOutOfRoot()
    {
      return !this.__isInRootValueSet();
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal virtual bool __isXerEmptyElementEnabled()
    {
      return false;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal virtual bool __isXerPositiveWithPlusSignEnabled()
    {
      return false;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal virtual bool __isXerText()
    {
      return false;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal virtual bool __isXerWhiteSpaceCollapse()
    {
      return false;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal virtual bool __isXerWhiteSpaceReplace()
    {
      return false;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal override void __read(Asn1TypePerReader reader)
    {
      reader.__decodeIntegerType(this);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal override void __read(Asn1TypeXerReader reader)
    {
      reader.__decodeIntegerType(this);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal override void __read(Asn1TypeBerReader reader, bool isExplicit, bool primitive, long len)
    {
      reader.__decodeIntegerType(this, isExplicit, primitive, len);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal override void __readValue(Asn1TypeXerReader reader, string text)
    {
      reader.__decodeIntegerValue(this, text);
    }

    protected internal override void __setTypeValue(Asn1Type typeInstance)
    {
      if (typeInstance == null)
      {
        this.ResetType();
      }
      else
      {
        if (!(typeInstance is Asn1IntegerType))
          return;
        Asn1IntegerType asn1IntegerType = (Asn1IntegerType) typeInstance;
        if (asn1IntegerType.GetBigIntegerValue() == null)
          this.SetValue(asn1IntegerType.GetLongValue());
        else
          this.SetValue(asn1IntegerType.GetBigIntegerValue());
      }
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal virtual void __setValue(string identifier, string[] idSet)
    {
      if (identifier == null || idSet == null)
        throw new ArgumentNullException("identifier and isSet must not be null.");
      for (int index = 0; index < idSet.Length; ++index)
      {
        if (identifier.Equals(idSet[index]))
        {
          this.SetValue(this.__getNamedNumberSet()[index]);
          return;
        }
      }
      throw new Asn1Exception(56, "invalid identifier (" + identifier + ") in type <" + this.GetType().FullName + ">");
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal override void __write(Asn1TypeBerWriter writer)
    {
      writer.__encodeIntegerType(this, this.__getUniversalTagNumber(), 1);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal override void __write(Asn1TypePerWriter writer)
    {
      writer.__encodeIntegerType(this);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal override void __write(Asn1TypeXerWriter writer)
    {
      writer.__encodeIntegerType(this);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal override void __write(Asn1TypeBerWriter writer, int tagNumber, int tagClass)
    {
      writer.__encodeIntegerType(this, tagNumber, tagClass);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal override string __writeValue(Asn1TypeXerWriter writer)
    {
      return writer.__encodeIntegerValue(this);
    }

    public override bool Equals(object anObject)
    {
      return this == anObject || anObject is Asn1IntegerType && this.HasEqualValue((Asn1IntegerType) anObject);
    }

    public BigInteger GetBigIntegerValue()
    {
      return this.__bigValue;
    }

    public override int GetHashCode()
    {
      if (this.__bigValue != null)
        return this.__bigValue.GetHashCode();
      else
        return (int) this.__value ^ (int) (this.__value >> 32);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public virtual string GetIdentifier()
    {
      return this.__getIdentifier(this.__getIdentifierSet());
    }

    public virtual int GetIntValue()
    {
      if (this.__bigValue != null)
        throw new InvalidCastException("Value is outside the limits of a long, use BigInteger instead (" + ((object) this.__bigValue).ToString() + ").");
      if (this.__value < (long) int.MinValue || this.__value > (long) int.MaxValue)
        throw new InvalidCastException("Value is outside the limits of an int, use long instead (" + (object) this.__value + ").");
      else
        return (int) this.__value;
    }

    public virtual long GetLongValue()
    {
      if (this.__bigValue != null)
        throw new InvalidCastException("Value is outside the limits of a long, use BigInteger instead (" + ((object) this.__bigValue).ToString() + ").");
      else
        return this.__value;
    }

    public virtual long GetLowerBound()
    {
      return -1L;
    }

    public virtual long GetUpperBound()
    {
      return -1L;
    }

    public virtual bool HasEqualValue(Asn1IntegerType that)
    {
      if (that == null)
        return false;
      if (that.BigIntegerValue != null)
        return that.BigIntegerValue.Equals((object) this.BigIntegerValue);
      else
        return this.__value == that.LongValue;
    }

    public virtual bool IsLowerBoundDefined()
    {
      return false;
    }

    public virtual bool IsUpperBoundDefined()
    {
      return false;
    }

    public override void ResetType()
    {
      this.__resetCommons();
      this.SetValue(0L);
      if (this.__getDefaultInstance() == null)
        return;
      this.__setTypeValue(this.__getDefaultInstance());
    }

    public virtual void SetValue(BigInteger val)
    {
      this.__value = 0L;
      this.__bigValue = val;
    }

    public virtual void SetValue(long val)
    {
      this.__value = val;
      this.__bigValue = (BigInteger) null;
    }

    public virtual void SetValue(string identifier)
    {
      if (identifier == null)
        throw new ArgumentException("identifier should not be null.");
      if (this.__getIdentifierSet() == null)
        throw new Asn1Exception(56, "no named numbers defined in type <" + this.GetType().FullName + ">");
      this.__setValue(identifier, this.__getIdentifierSet());
    }

    public override void Validate()
    {
      if (this.__bigValue != null || (this.__isPerConstraintExtensible() || this.__isInRootValueSet()))
        return;
      throw new Asn1ValidationException(27, "'" + (object) this.__value + "' is not authorized in type <" + this.GetType().FullName + ">");
    }
  }
}
