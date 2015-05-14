// Decompiled with JetBrains decompiler
// Type: Novensys.eCard.SDK.ASN1.EHCDG1.DG1
// Assembly: Novensys.eCard.SDK, Version=1.1.56.0, Culture=neutral, PublicKeyToken=null
// MVID: 59F9E456-D3DA-4066-A4A4-692B516A775E
// Assembly location: C:\Program Files (x86)\CNAS\SIUI-SPITAL\Novensys.eCard.SDK.dll

using Novensys.ASN1.IO;
using Novensys.ASN1.Type;
using System;
using System.ComponentModel;

namespace Novensys.eCard.SDK.ASN1.EHCDG1
{
  [Serializable]
  public class DG1 : Asn1SequenceType
  {
    public byte[] ProfileVersion
    {
      get
      {
        return this.profileVersion().ByteArrayValue;
      }
      set
      {
        ((Asn1OctetStringType) this.profileVersion()).SetValue(value);
      }
    }

    public string LastName
    {
      get
      {
        return this.lastName().StringValue;
      }
      set
      {
        ((Asn1StringType) this.lastName()).SetValue(value);
      }
    }

    public string FirstName
    {
      get
      {
        return this.firstName().StringValue;
      }
      set
      {
        ((Asn1StringType) this.firstName()).SetValue(value);
      }
    }

    public string DateOfBirth
    {
      get
      {
        return this.dateOfBirth().StringValue;
      }
      set
      {
        ((Asn1StringType) this.dateOfBirth()).SetValue(value);
      }
    }

    public string CardHolderID
    {
      get
      {
        return this.cardHolderID().StringValue;
      }
      set
      {
        ((Asn1StringType) this.cardHolderID()).SetValue(value);
      }
    }

    public string CardNumber
    {
      get
      {
        return this.cardNumber().StringValue;
      }
      set
      {
        ((Asn1StringType) this.cardNumber()).SetValue(value);
      }
    }

    public string InsuranceID
    {
      get
      {
        return this.insuranceID().StringValue;
      }
      set
      {
        ((Asn1StringType) this.insuranceID()).SetValue(value);
      }
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public override string TypeName
    {
      get
      {
        return "DG1";
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
      this.__initializeComponents(7);
      this.__setComponentTag(0, 0, 4);
      this.__setComponentTag(1, 1, 4);
      this.__setComponentTag(2, 2, 4);
      this.__setComponentTag(3, 3, 4);
      this.__setComponentTag(4, 4, 4);
      this.__setComponentTag(5, 5, 4);
      this.__setComponentTag(6, 6, 4);
    }

    public void SetValue(DG1 typeInstance)
    {
      this.__setTypeValue((Asn1Type) typeInstance);
    }

    public DG1._profileVersion profileVersion()
    {
      this.__setComponentIsDefined(0);
      return (DG1._profileVersion) this.__instantiateTypeByIndex(0);
    }

    public DG1._lastName lastName()
    {
      this.__setComponentIsDefined(1);
      return (DG1._lastName) this.__instantiateTypeByIndex(1);
    }

    public DG1._firstName firstName()
    {
      this.__setComponentIsDefined(2);
      return (DG1._firstName) this.__instantiateTypeByIndex(2);
    }

    public DG1._dateOfBirth dateOfBirth()
    {
      this.__setComponentIsDefined(3);
      return (DG1._dateOfBirth) this.__instantiateTypeByIndex(3);
    }

    public DG1._cardHolderID cardHolderID()
    {
      this.__setComponentIsDefined(4);
      return (DG1._cardHolderID) this.__instantiateTypeByIndex(4);
    }

    public DG1._cardNumber cardNumber()
    {
      this.__setComponentIsDefined(5);
      return (DG1._cardNumber) this.__instantiateTypeByIndex(5);
    }

    public DG1._insuranceID insuranceID()
    {
      this.__setComponentIsDefined(6);
      return (DG1._insuranceID) this.__instantiateTypeByIndex(6);
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
            typeInstance = (Asn1Type) new DG1._profileVersion();
            this.__setComponentTypeInstance(0, typeInstance);
            break;
          }
          else
            break;
        case 1:
          typeInstance = this.GetAsn1Type(1);
          if (typeInstance == null)
          {
            typeInstance = (Asn1Type) new DG1._lastName();
            this.__setComponentTypeInstance(1, typeInstance);
            break;
          }
          else
            break;
        case 2:
          typeInstance = this.GetAsn1Type(2);
          if (typeInstance == null)
          {
            typeInstance = (Asn1Type) new DG1._firstName();
            this.__setComponentTypeInstance(2, typeInstance);
            break;
          }
          else
            break;
        case 3:
          typeInstance = this.GetAsn1Type(3);
          if (typeInstance == null)
          {
            typeInstance = (Asn1Type) new DG1._dateOfBirth();
            this.__setComponentTypeInstance(3, typeInstance);
            break;
          }
          else
            break;
        case 4:
          typeInstance = this.GetAsn1Type(4);
          if (typeInstance == null)
          {
            typeInstance = (Asn1Type) new DG1._cardHolderID();
            this.__setComponentTypeInstance(4, typeInstance);
            break;
          }
          else
            break;
        case 5:
          typeInstance = this.GetAsn1Type(5);
          if (typeInstance == null)
          {
            typeInstance = (Asn1Type) new DG1._cardNumber();
            this.__setComponentTypeInstance(5, typeInstance);
            break;
          }
          else
            break;
        case 6:
          typeInstance = this.GetAsn1Type(6);
          if (typeInstance == null)
          {
            typeInstance = (Asn1Type) new DG1._insuranceID();
            this.__setComponentTypeInstance(6, typeInstance);
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
      if (!(anObject is DG1))
        return false;
      else
        return base.Equals(anObject);
    }

    public override int GetHashCode()
    {
      return base.GetHashCode();
    }

    [Serializable]
    public class _profileVersion : Asn1OctetStringType
    {
      [EditorBrowsable(EditorBrowsableState.Never)]
      public override string TypeName
      {
        get
        {
          return "profileVersion";
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
      static _profileVersion()
      {
      }

      public _profileVersion()
      {
      }

      public _profileVersion(byte[] val)
        : base(val)
      {
      }

      public void SetValue(Asn1OctetStringType typeInstance)
      {
        this.__setTypeValue((Asn1Type) typeInstance);
      }

      public override long GetLowerSize()
      {
        return 2L;
      }

      public override long GetUpperSize()
      {
        return 2L;
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
    public class _lastName : Asn1UTF8StringType
    {
      [EditorBrowsable(EditorBrowsableState.Never)]
      public override string TypeName
      {
        get
        {
          return "lastName";
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
      static _lastName()
      {
      }

      public _lastName()
      {
      }

      public _lastName(string val)
        : base(val)
      {
      }

      public void SetValue(Asn1UTF8StringType typeInstance)
      {
        this.__setTypeValue((Asn1Type) typeInstance);
      }

      public override long GetLowerSize()
      {
        return 0L;
      }

      public override long GetUpperSize()
      {
        return 100L;
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
          bool primitive1 = reader.__decodeTag((Asn1Type) this, 1, 4);
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
    public class _firstName : Asn1UTF8StringType
    {
      [EditorBrowsable(EditorBrowsableState.Never)]
      public override string TypeName
      {
        get
        {
          return "firstName";
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
      static _firstName()
      {
      }

      public _firstName()
      {
      }

      public _firstName(string val)
        : base(val)
      {
      }

      public void SetValue(Asn1UTF8StringType typeInstance)
      {
        this.__setTypeValue((Asn1Type) typeInstance);
      }

      public override long GetLowerSize()
      {
        return 0L;
      }

      public override long GetUpperSize()
      {
        return 100L;
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
        return 2;
      }

      [EditorBrowsable(EditorBrowsableState.Never)]
      protected internal override void __write(Asn1TypeBerWriter writer)
      {
        base.__write(writer, 2, 4);
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
          bool primitive1 = reader.__decodeTag((Asn1Type) this, 2, 4);
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
    public class _dateOfBirth : Asn1NumericStringType
    {
      [EditorBrowsable(EditorBrowsableState.Never)]
      public override string TypeName
      {
        get
        {
          return "dateOfBirth";
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
      static _dateOfBirth()
      {
      }

      public _dateOfBirth()
      {
      }

      public _dateOfBirth(string val)
        : base(val)
      {
      }

      public void SetValue(Asn1NumericStringType typeInstance)
      {
        this.__setTypeValue((Asn1Type) typeInstance);
      }

      public override long GetLowerSize()
      {
        return 8L;
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
        return 3;
      }

      [EditorBrowsable(EditorBrowsableState.Never)]
      protected internal override void __write(Asn1TypeBerWriter writer)
      {
        base.__write(writer, 3, 4);
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
          bool primitive1 = reader.__decodeTag((Asn1Type) this, 3, 4);
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
    public class _cardHolderID : Asn1NumericStringType
    {
      [EditorBrowsable(EditorBrowsableState.Never)]
      public override string TypeName
      {
        get
        {
          return "cardHolderID";
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
      static _cardHolderID()
      {
      }

      public _cardHolderID()
      {
      }

      public _cardHolderID(string val)
        : base(val)
      {
      }

      public void SetValue(Asn1NumericStringType typeInstance)
      {
        this.__setTypeValue((Asn1Type) typeInstance);
      }

      public override long GetLowerSize()
      {
        return 13L;
      }

      public override long GetUpperSize()
      {
        return 13L;
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
        return 4;
      }

      [EditorBrowsable(EditorBrowsableState.Never)]
      protected internal override void __write(Asn1TypeBerWriter writer)
      {
        base.__write(writer, 4, 4);
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
          bool primitive1 = reader.__decodeTag((Asn1Type) this, 4, 4);
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
    public class _cardNumber : Asn1NumericStringType
    {
      [EditorBrowsable(EditorBrowsableState.Never)]
      public override string TypeName
      {
        get
        {
          return "cardNumber";
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
      static _cardNumber()
      {
      }

      public _cardNumber()
      {
      }

      public _cardNumber(string val)
        : base(val)
      {
      }

      public void SetValue(Asn1NumericStringType typeInstance)
      {
        this.__setTypeValue((Asn1Type) typeInstance);
      }

      public override long GetLowerSize()
      {
        return 16L;
      }

      public override long GetUpperSize()
      {
        return 16L;
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
        return 5;
      }

      [EditorBrowsable(EditorBrowsableState.Never)]
      protected internal override void __write(Asn1TypeBerWriter writer)
      {
        base.__write(writer, 5, 4);
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
          bool primitive1 = reader.__decodeTag((Asn1Type) this, 5, 4);
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
    public class _insuranceID : Asn1PrintableStringType
    {
      [EditorBrowsable(EditorBrowsableState.Never)]
      public override string TypeName
      {
        get
        {
          return "insuranceID";
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
      static _insuranceID()
      {
      }

      public _insuranceID()
      {
      }

      public _insuranceID(string val)
        : base(val)
      {
      }

      public void SetValue(Asn1PrintableStringType typeInstance)
      {
        this.__setTypeValue((Asn1Type) typeInstance);
      }

      public override long GetLowerSize()
      {
        return 0L;
      }

      public override long GetUpperSize()
      {
        return 20L;
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
        return 6;
      }

      [EditorBrowsable(EditorBrowsableState.Never)]
      protected internal override void __write(Asn1TypeBerWriter writer)
      {
        base.__write(writer, 6, 4);
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
          bool primitive1 = reader.__decodeTag((Asn1Type) this, 6, 4);
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
