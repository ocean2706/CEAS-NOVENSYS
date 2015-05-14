// Decompiled with JetBrains decompiler
// Type: Novensys.eCard.SDK.ASN1.EHCTECH.TECH
// Assembly: Novensys.eCard.SDK, Version=1.1.56.0, Culture=neutral, PublicKeyToken=null
// MVID: 59F9E456-D3DA-4066-A4A4-692B516A775E
// Assembly location: C:\Program Files (x86)\CNAS\SIUI-SPITAL\Novensys.eCard.SDK.dll

using Novensys.ASN1.IO;
using Novensys.ASN1.Type;
using System;
using System.ComponentModel;

namespace Novensys.eCard.SDK.ASN1.EHCTECH
{
  [Serializable]
  public class TECH : Asn1SequenceType
  {
    public string ActivateStatus
    {
      get
      {
        return this.activateStatus().StringValue;
      }
      set
      {
        ((Asn1StringType) this.activateStatus()).SetValue(value);
      }
    }

    public string ActivateStatusExtern
    {
      get
      {
        return this.activateStatusExtern().StringValue;
      }
      set
      {
        ((Asn1StringType) this.activateStatusExtern()).SetValue(value);
      }
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public override string TypeName
    {
      get
      {
        return "TECH";
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
      this.__initializeComponents(2);
      this.__setComponentTag(0, 0, 4);
      this.__setComponentTag(1, 1, 4);
    }

    public void SetValue(TECH typeInstance)
    {
      this.__setTypeValue((Asn1Type) typeInstance);
    }

    public TECH._activateStatus activateStatus()
    {
      this.__setComponentIsDefined(0);
      return (TECH._activateStatus) this.__instantiateTypeByIndex(0);
    }

    public TECH._activateStatusExtern activateStatusExtern()
    {
      this.__setComponentIsDefined(1);
      return (TECH._activateStatusExtern) this.__instantiateTypeByIndex(1);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal override Asn1Type __instantiateTypeByIndex(int index)
    {
      Asn1Type typeInstance;
      switch (index)
      {
        case 0:
          typeInstance = this.GetAsn1Type(0);
          if (typeInstance == null)
          {
            typeInstance = (Asn1Type) new TECH._activateStatus();
            this.__setComponentTypeInstance(0, typeInstance);
            break;
          }
          else
            break;
        case 1:
          typeInstance = this.GetAsn1Type(1);
          if (typeInstance == null)
          {
            typeInstance = (Asn1Type) new TECH._activateStatusExtern();
            this.__setComponentTypeInstance(1, typeInstance);
            break;
          }
          else
            break;
        default:
          typeInstance = (Asn1Type) null;
          break;
      }
      return typeInstance;
    }

    public override bool Equals(object anObject)
    {
      if (!(anObject is TECH))
        return false;
      else
        return base.Equals(anObject);
    }

    public override int GetHashCode()
    {
      return base.GetHashCode();
    }

    [Serializable]
    public class _activateStatus : Asn1NumericStringType
    {
      [EditorBrowsable(EditorBrowsableState.Never)]
      public override string TypeName
      {
        get
        {
          return "activateStatus";
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
      static _activateStatus()
      {
      }

      public _activateStatus()
      {
      }

      public _activateStatus(string val)
        : base(val)
      {
      }

      public void SetValue(Asn1NumericStringType typeInstance)
      {
        this.__setTypeValue((Asn1Type) typeInstance);
      }

      public override long GetLowerSize()
      {
        return 1L;
      }

      public override long GetUpperSize()
      {
        return 1L;
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

    [Serializable]
    public class _activateStatusExtern : Asn1NumericStringType
    {
      [EditorBrowsable(EditorBrowsableState.Never)]
      public override string TypeName
      {
        get
        {
          return "activateStatusExtern";
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
      static _activateStatusExtern()
      {
      }

      public _activateStatusExtern()
      {
      }

      public _activateStatusExtern(string val)
        : base(val)
      {
      }

      public void SetValue(Asn1NumericStringType typeInstance)
      {
        this.__setTypeValue((Asn1Type) typeInstance);
      }

      public override long GetLowerSize()
      {
        return 1L;
      }

      public override long GetUpperSize()
      {
        return 1L;
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
        return 1;
      }

      [EditorBrowsable(EditorBrowsableState.Never)]
      protected internal override void __write(Asn1TypeBerWriter writer)
      {
        base.__write(writer, 1, 4);
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
