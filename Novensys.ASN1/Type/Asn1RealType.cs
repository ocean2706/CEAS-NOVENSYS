// Decompiled with JetBrains decompiler
// Type: Novensys.ASN1.Type.Asn1RealType
// Assembly: Novensys.eCard.SDK, Version=1.1.56.0, Culture=neutral, PublicKeyToken=null
// MVID: 59F9E456-D3DA-4066-A4A4-692B516A775E
// Assembly location: C:\Program Files (x86)\CNAS\SIUI-SPITAL\Novensys.eCard.SDK.dll

using Novensys.ASN1;
using System;
using System.ComponentModel;
using System.Globalization;
using System.Text;

namespace Novensys.ASN1.Type
{
  [Serializable]
  public class Asn1RealType : Asn1SequenceType
  {
    private double __doubleValue;
    private string __stringValue;

    public int Base
    {
      get
      {
        return this.base_().GetIntValue();
      }
      set
      {
        ((Asn1IntegerType) this.base_()).SetValue((long) value);
      }
    }

    public double DoubleValue
    {
      get
      {
        return this.GetDoubleValue();
      }
      set
      {
        this.SetValue(value);
      }
    }

    public long Exponent
    {
      get
      {
        return this.exponent().GetLongValue();
      }
      set
      {
        ((Asn1IntegerType) this.exponent()).SetValue(value);
      }
    }

    public long Mantissa
    {
      get
      {
        return this.mantissa().GetLongValue();
      }
      set
      {
        ((Asn1IntegerType) this.mantissa()).SetValue(value);
      }
    }

    public override string PrintableValue
    {
      get
      {
        if (this.base_().GetIntValue() == 10)
          return this.__stringValue;
        double doubleValue = this.GetDoubleValue();
        if (doubleValue == double.NegativeInfinity)
          return "NEGATIVE-INFINITY";
        if (doubleValue == double.PositiveInfinity)
          return "POSITIVE-INFINITY";
        if (doubleValue == double.NaN)
          return "NOT-A-NUMBER";
        else
          return doubleValue.ToString();
      }
    }

    public string StringValue
    {
      get
      {
        return this.GetStringValue();
      }
      set
      {
        this.SetValue(value);
      }
    }

    public override string TypeName
    {
      get
      {
        return "REAL";
      }
    }

    public Asn1RealType()
    {
      this.__initialize();
    }

    public Asn1RealType(double val)
    {
      this.SetValue(val);
    }

    public Asn1RealType(string val)
    {
      this.SetValue(val);
    }

    protected internal string __canonicalizeRealValue(string val, bool setInternalFields)
    {
      long val1 = 0L;
      StringBuilder stringBuilder = new StringBuilder(val.Length);
      for (int index = 0; index < val.Length; ++index)
      {
        if ((int) val[index] == 101)
          stringBuilder.Append('E');
        else if (!char.IsSeparator(val[index]) && (int) val[index] != 43)
          stringBuilder.Append(val[index]);
      }
      string str = ((object) stringBuilder).ToString();
      int length = str.IndexOf('E');
      if (length != -1)
      {
        double num = double.Parse(str.Substring(0, length), (IFormatProvider) CultureInfo.InvariantCulture);
        val1 = long.Parse(str.Substring(length + 1));
        stringBuilder = new StringBuilder(num.ToString((IFormatProvider) CultureInfo.InvariantCulture));
      }
      int startIndex = ((object) stringBuilder).ToString().IndexOf('.');
      if (startIndex != -1)
      {
        while ((int) stringBuilder[stringBuilder.Length - 1] == 48)
          stringBuilder.Remove(stringBuilder.Length - 1, 1);
        if (startIndex != stringBuilder.Length - 1)
        {
          val1 -= (long) (stringBuilder.Length - 1 - startIndex);
          stringBuilder.Remove(startIndex, 1);
          while ((int) stringBuilder[0] == 48)
            stringBuilder.Remove(0, 1);
          while ((int) stringBuilder[0] == 45 && (int) stringBuilder[1] == 48)
            stringBuilder.Remove(1, 1);
        }
        else
          stringBuilder.Remove(startIndex, 1);
      }
      if (stringBuilder.Length > 1)
      {
        while ((int) stringBuilder[stringBuilder.Length - 1] == 48)
        {
          stringBuilder.Remove(stringBuilder.Length - 1, 1);
          ++val1;
        }
      }
      string s = ((object) stringBuilder).ToString();
      if (setInternalFields)
      {
        ((Asn1IntegerType) this.base_()).SetValue(10L);
        ((Asn1IntegerType) this.mantissa()).SetValue(long.Parse(s));
        ((Asn1IntegerType) this.exponent()).SetValue(val1);
      }
      return s + (object) "E" + (string) (object) val1;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal override int __getUniversalTagNumber()
    {
      return 9;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal override string __getXerTag()
    {
      return "REAL";
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal override void __initialize()
    {
      this.__initializeComponents(3);
    }

    private Asn1Type __instantiate_base_()
    {
      Asn1Type typeInstance = this.GetAsn1Type(1);
      if (typeInstance == null)
      {
        typeInstance = (Asn1Type) new Asn1RealType._base_();
        this.__setComponentTypeInstance(1, typeInstance);
      }
      return typeInstance;
    }

    private Asn1Type __instantiate_exponent()
    {
      Asn1Type typeInstance = this.GetAsn1Type(2);
      if (typeInstance == null)
      {
        typeInstance = (Asn1Type) new Asn1RealType._exponent();
        this.__setComponentTypeInstance(2, typeInstance);
      }
      return typeInstance;
    }

    private Asn1Type __instantiate_mantissa()
    {
      Asn1Type typeInstance = this.GetAsn1Type(0);
      if (typeInstance == null)
      {
        typeInstance = (Asn1Type) new Asn1RealType._mantissa();
        this.__setComponentTypeInstance(0, typeInstance);
      }
      return typeInstance;
    }

    protected internal override Asn1Type __instantiateTypeByIndex(int index)
    {
      switch (index)
      {
        case 0:
          return this.__instantiate_mantissa();
        case 1:
          return this.__instantiate_base_();
        case 2:
          return this.__instantiate_exponent();
        default:
          return (Asn1Type) null;
      }
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal virtual bool __isXerDecimal()
    {
      return false;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal virtual bool __isXerPositiveWithPlusSignEnabled()
    {
      return false;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal virtual bool __isXerSpecialAsText()
    {
      return false;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal virtual bool __isXerZeroWithPlusSignEnabled()
    {
      return false;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal override void __read(Asn1TypePerReader reader)
    {
      reader.__decodeRealType(this);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal override void __read(Asn1TypeXerReader reader)
    {
      reader.__decodeRealType(this);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal override void __read(Asn1TypeBerReader reader, bool isExplicit, bool primitive, long len)
    {
      reader.__decodeRealType(this, isExplicit, primitive, len);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal override void __readValue(Asn1TypeXerReader reader, string text)
    {
      reader.__decodeRealValue(this, text);
    }

    protected internal override void __setConstructedTypeValue(Asn1ConstructedType constructedTypeInstance)
    {
      if (constructedTypeInstance == null)
      {
        this.ResetType();
      }
      else
      {
        Asn1RealType asn1RealType = (Asn1RealType) constructedTypeInstance;
        for (int index = 0; index < asn1RealType.Length; ++index)
        {
          if (asn1RealType.__isComponentDefined(index))
          {
            this.__instantiateTypeByIndex(index);
            Asn1Type componentTypeInstance = asn1RealType.__getDefinedComponentTypeInstance(index);
            this.GetAsn1Type(index).__setTypeValue(componentTypeInstance);
            this.__setComponentIsDefined(index);
          }
        }
        if (asn1RealType.base_().GetIntValue() == 2)
          this.SetValue(asn1RealType.GetDoubleValue());
        else
          this.SetValue(asn1RealType.GetStringValue());
      }
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal override void __write(Asn1TypeBerWriter writer)
    {
      writer.__encodeRealType(this, this.__getUniversalTagNumber(), 1);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal override void __write(Asn1TypePerWriter writer)
    {
      writer.__encodeRealType(this);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal override void __write(Asn1TypeXerWriter writer)
    {
      writer.__encodeRealType(this);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal override void __write(Asn1TypeBerWriter writer, int tagNumber, int tagClass)
    {
      writer.__encodeRealType(this, tagNumber, tagClass);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal override string __writeValue(Asn1TypeXerWriter writer)
    {
      return writer.__encodeRealValue(this);
    }

    public virtual Asn1RealType._base_ base_()
    {
      if (this.GetAsn1Type(1) == null)
      {
        this.__setComponentIsDefined(1);
        this.__setComponentTypeInstance(1, (Asn1Type) new Asn1RealType._base_());
      }
      return (Asn1RealType._base_) this.GetAsn1Type(1);
    }

    public override bool Equals(object anObject)
    {
      return this == anObject || anObject is Asn1RealType && this.HasEqualValue((Asn1RealType) anObject);
    }

    public virtual Asn1RealType._exponent exponent()
    {
      if (this.GetAsn1Type(2) == null)
      {
        this.__setComponentIsDefined(2);
        this.__setComponentTypeInstance(2, (Asn1Type) new Asn1RealType._exponent());
      }
      return (Asn1RealType._exponent) this.GetAsn1Type(2);
    }

    public virtual double GetDoubleValue()
    {
      if (this.base_().GetIntValue() != 2 || this.mantissa().GetLongValue() != 0L)
        this.__doubleValue = (double) this.mantissa().GetLongValue() * Math.Pow((double) this.base_().GetIntValue(), (double) this.exponent().GetLongValue());
      return this.__doubleValue;
    }

    public override int GetHashCode()
    {
      if (this.__stringValue != null)
        return this.__stringValue.GetHashCode();
      byte[] bytes = BitConverter.GetBytes(this.__doubleValue);
      int num = 1;
      for (int index = 0; index < bytes.Length; ++index)
        num = 31 * num + (int) bytes[index];
      return num;
    }

    public virtual double GetLowerBound()
    {
      return double.NegativeInfinity;
    }

    public virtual string GetStringValue()
    {
      if (this.base_().GetIntValue() != 10 && (this.__stringValue == null || !this.__stringValue.Equals("-0")))
        return this.GetDoubleValue().ToString();
      else
        return this.__stringValue;
    }

    public virtual double GetUpperBound()
    {
      return double.PositiveInfinity;
    }

    public virtual bool HasEqualValue(Asn1RealType that)
    {
      if (that == null)
        return false;
      if (this.__stringValue != null)
        return this.__stringValue == that.StringValue;
      else
        return this.__doubleValue == that.DoubleValue;
    }

    public virtual bool IsLowerBoundDefined()
    {
      return false;
    }

    public virtual bool IsUpperBoundDefined()
    {
      return false;
    }

    public virtual Asn1RealType._mantissa mantissa()
    {
      if (this.GetAsn1Type(0) == null)
      {
        this.__setComponentIsDefined(0);
        this.__setComponentTypeInstance(0, (Asn1Type) new Asn1RealType._mantissa());
      }
      return (Asn1RealType._mantissa) this.GetAsn1Type(0);
    }

    public override void ResetType()
    {
      this.__resetCommons();
      this.__doubleValue = 0.0;
      this.__stringValue = (string) null;
      ((Asn1IntegerType) this.base_()).SetValue(2L);
      ((Asn1IntegerType) this.mantissa()).SetValue(0L);
      ((Asn1IntegerType) this.exponent()).SetValue(0L);
      if (this.__getDefaultInstance() == null)
        return;
      this.__setTypeValue(this.__getDefaultInstance());
    }

    public virtual void SetValue(Asn1RealType typeInstance)
    {
      this.__setConstructedTypeValue((Asn1ConstructedType) typeInstance);
    }

    public virtual void SetValue(double val)
    {
      this.__doubleValue = val;
      ((Asn1IntegerType) this.base_()).SetValue(2L);
    }

    public virtual void SetValue(string val)
    {
      this.__stringValue = val != null ? val : "0.0";
      if (this.__stringValue.Equals("0.0") || this.__stringValue.Equals("0") || (this.__stringValue.Equals("+0") || this.__stringValue.Equals("+0.0")))
      {
        this.__doubleValue = 0.0;
        ((Asn1IntegerType) this.base_()).SetValue(2L);
      }
      else if (this.__stringValue.Equals("-0") || this.__stringValue.Equals("-0.0"))
      {
        this.__doubleValue = 0.0;
        this.__stringValue = "-0";
        ((Asn1IntegerType) this.base_()).SetValue(2L);
      }
      else if (this.__stringValue.Equals("Infinity"))
      {
        this.__doubleValue = double.PositiveInfinity;
        ((Asn1IntegerType) this.base_()).SetValue(2L);
      }
      else if (this.__stringValue.Equals("-Infinity"))
      {
        this.__doubleValue = double.NegativeInfinity;
        ((Asn1IntegerType) this.base_()).SetValue(2L);
      }
      else if (this.__stringValue.Equals("NaN"))
      {
        this.__doubleValue = double.NaN;
        ((Asn1IntegerType) this.base_()).SetValue(2L);
      }
      else
        this.__stringValue = this.__canonicalizeRealValue(val, true);
    }

    public override void Validate()
    {
      if (this.base_().GetIntValue() != 2 && this.base_().GetIntValue() != 10)
        throw new Asn1ValidationException(33, "base " + (object) this.base_().GetIntValue() + " is not valid (should be 2 or 10) in type <" + this.GetType().FullName + ">");
      else if (this.IsLowerBoundDefined() && this.GetDoubleValue() < this.GetLowerBound())
      {
        throw new Asn1ValidationException(23, (string) (object) this.GetDoubleValue() + (object) " < " + (string) (object) this.GetLowerBound() + " in type <" + this.GetType().FullName + ">");
      }
      else
      {
        if (!this.IsUpperBoundDefined() || this.GetDoubleValue() <= this.GetUpperBound())
          return;
        throw new Asn1ValidationException(24, (string) (object) this.GetDoubleValue() + (object) " > " + (string) (object) this.GetUpperBound() + " in type <" + this.GetType().FullName + ">");
      }
    }

    [Serializable]
    public class _base_ : Asn1IntegerType
    {
      public override string ReferenceTypeName
      {
        get
        {
          return base.TypeName;
        }
      }

      public override string TypeName
      {
        get
        {
          return "base";
        }
      }

      [EditorBrowsable(EditorBrowsableState.Never)]
      public _base_()
        : base(2L)
      {
      }

      [EditorBrowsable(EditorBrowsableState.Never)]
      public _base_(long val)
        : base(val)
      {
      }

      [EditorBrowsable(EditorBrowsableState.Never)]
      protected internal override bool __isInRootValueSet()
      {
        long longValue = this.LongValue;
        if (longValue != 2L)
          return longValue == 10L;
        else
          return true;
      }

      [EditorBrowsable(EditorBrowsableState.Never)]
      protected internal override bool __isPerConstraintExtensible()
      {
        return false;
      }

      public override long GetLowerBound()
      {
        return 2L;
      }

      public override long GetUpperBound()
      {
        return 10L;
      }

      [EditorBrowsable(EditorBrowsableState.Never)]
      public override bool IsLowerBoundDefined()
      {
        return true;
      }

      public override bool IsUpperBoundDefined()
      {
        return true;
      }

      public virtual void SetValue(Asn1IntegerType type)
      {
        this.__setTypeValue((Asn1Type) type);
      }
    }

    [Serializable]
    public class _exponent : Asn1IntegerType
    {
      public override string ReferenceTypeName
      {
        get
        {
          return base.TypeName;
        }
      }

      public override string TypeName
      {
        get
        {
          return "exponent";
        }
      }

      [EditorBrowsable(EditorBrowsableState.Never)]
      public _exponent()
        : base(0L)
      {
      }

      [EditorBrowsable(EditorBrowsableState.Never)]
      public _exponent(long val)
        : base(val)
      {
      }

      public virtual void SetValue(Asn1IntegerType type)
      {
        this.__setTypeValue((Asn1Type) type);
      }
    }

    [Serializable]
    public class _mantissa : Asn1IntegerType
    {
      public override string PrintableValue
      {
        get
        {
          if (this.GetLongValue() == 0L)
            return (string) null;
          else
            return base.PrintableValue;
        }
      }

      public override string ReferenceTypeName
      {
        get
        {
          return base.TypeName;
        }
      }

      public override string TypeName
      {
        get
        {
          return "mantissa";
        }
      }

      [EditorBrowsable(EditorBrowsableState.Never)]
      public _mantissa()
        : base(0L)
      {
      }

      [EditorBrowsable(EditorBrowsableState.Never)]
      public _mantissa(long val)
        : base(val)
      {
      }

      public virtual void SetValue(Asn1IntegerType type)
      {
        this.__setTypeValue((Asn1Type) type);
      }
    }
  }
}
