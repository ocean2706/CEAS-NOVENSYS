// Decompiled with JetBrains decompiler
// Type: Novensys.eCard.SDK.ASN1.EHCDG6.ChronicDesease
// Assembly: Novensys.eCard.SDK, Version=1.1.56.0, Culture=neutral, PublicKeyToken=null
// MVID: 59F9E456-D3DA-4066-A4A4-692B516A775E
// Assembly location: C:\Program Files (x86)\CNAS\SIUI-SPITAL\Novensys.eCard.SDK.dll

using Novensys.ASN1.IO;
using Novensys.ASN1.Type;
using System;
using System.ComponentModel;

namespace Novensys.eCard.SDK.ASN1.EHCDG6
{
  [Serializable]
  public class ChronicDesease : Asn1SequenceType
  {
    public string Code
    {
      get
      {
        return this.code().StringValue;
      }
      set
      {
        ((Asn1StringType) this.code()).SetValue(value);
      }
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public override string TypeName
    {
      get
      {
        return "ChronicDesease";
      }
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public override string ReferenceTypeName
    {
      get
      {
        return base.TypeName;
      }
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal override void __initialize()
    {
      this.__initializeComponents(1);
      this.__setComponentTag(0, 0, 4);
    }

    public void SetValue(ChronicDesease typeInstance)
    {
      this.__setTypeValue((Asn1Type) typeInstance);
    }

    public ChronicDesease._code code()
    {
      this.__setComponentIsDefined(0);
      return (ChronicDesease._code) this.__instantiateTypeByIndex(0);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal override Asn1Type __instantiateTypeByIndex(int index)
    {
      Asn1Type typeInstance;
      if (index == 0)
      {
        typeInstance = this.GetAsn1Type(0);
        if (typeInstance == null)
        {
          typeInstance = (Asn1Type) new ChronicDesease._code();
          this.__setComponentTypeInstance(0, typeInstance);
        }
      }
      else
        typeInstance = (Asn1Type) null;
      return typeInstance;
    }

    public override bool Equals(object anObject)
    {
      if (!(anObject is ChronicDesease))
        return false;
      else
        return base.Equals(anObject);
    }

    public override int GetHashCode()
    {
      return base.GetHashCode();
    }

    [Serializable]
    public class _code : Asn1UTF8StringType
    {
      [EditorBrowsable(EditorBrowsableState.Never)]
      public override string TypeName
      {
        get
        {
          return "code";
        }
      }

      [EditorBrowsable(EditorBrowsableState.Never)]
      public override string ReferenceTypeName
      {
        get
        {
          return base.TypeName;
        }
      }

      [EditorBrowsable(EditorBrowsableState.Never)]
      static _code()
      {
      }

      public _code()
      {
      }

      public _code(string val)
        : base(val)
      {
      }

      public void SetValue(Asn1UTF8StringType typeInstance)
      {
        this.__setTypeValue((Asn1Type) typeInstance);
      }

      public override long GetLowerSize()
      {
        return 5L;
      }

      public override long GetUpperSize()
      {
        return 8L;
      }

      [EditorBrowsable(EditorBrowsableState.Never)]
      protected internal override bool __isPerConstraintExtensible()
      {
        return false;
      }

      [EditorBrowsable(EditorBrowsableState.Never)]
      protected internal override int __getTagClass()
      {
        return 4;
      }

      [EditorBrowsable(EditorBrowsableState.Never)]
      protected internal override int __getTagNumber()
      {
        return 0;
      }

      [EditorBrowsable(EditorBrowsableState.Never)]
      protected internal override void __write(Asn1TypeBerWriter writer)
      {
        base.__write(writer, 0, 4);
      }

      [EditorBrowsable(EditorBrowsableState.Never)]
      protected internal override void __write(Asn1TypeBerWriter writer, int tagNumber, int tagClass)
      {
        base.__write(writer, tagNumber, tagClass);
      }

      [EditorBrowsable(EditorBrowsableState.Never)]
      protected internal override void __read(Asn1TypeBerReader reader, bool isExplicit, bool primitive, long len)
      {
        if (isExplicit)
        {
          bool primitive1 = reader.__decodeTag((Asn1Type) this, 0, 4);
          long len1 = reader.__decodeLength((ByteArrayAsn1OutputStream) null);
          base.__read(reader, false, primitive1, len1);
          if (len1 >= 0L)
            return;
          reader.__decodeEndValue((Asn1Type) this);
        }
        else
          base.__read(reader, false, primitive, len);
      }
    }
  }
}
