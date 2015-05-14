// Decompiled with JetBrains decompiler
// Type: Novensys.eCard.SDK.ASN1.EHCDG4.DG4
// Assembly: Novensys.eCard.SDK, Version=1.1.56.0, Culture=neutral, PublicKeyToken=null
// MVID: 59F9E456-D3DA-4066-A4A4-692B516A775E
// Assembly location: C:\Program Files (x86)\CNAS\SIUI-SPITAL\Novensys.eCard.SDK.dll

using Novensys.ASN1;
using Novensys.ASN1.IO;
using Novensys.ASN1.Type;
using Novensys.eCard.SDK.ASN1;
using System;
using System.Collections;
using System.ComponentModel;

namespace Novensys.eCard.SDK.ASN1.EHCDG4
{
  [Serializable]
  public class DG4 : Asn1SequenceType
  {
    public string DoctorLastName
    {
      get
      {
        return this.doctorLastName().StringValue;
      }
      set
      {
        ((Asn1StringType) this.doctorLastName()).SetValue(value);
      }
    }

    public string DoctorFirstName
    {
      get
      {
        return this.doctorFirstName().StringValue;
      }
      set
      {
        ((Asn1StringType) this.doctorFirstName()).SetValue(value);
      }
    }

    public string DoctorReference
    {
      get
      {
        return this.doctorReference().StringValue;
      }
      set
      {
        ((Asn1StringType) this.doctorReference()).SetValue(value);
      }
    }

    public string DoctorPhone
    {
      get
      {
        return this.doctorPhone().StringValue;
      }
      set
      {
        ((Asn1StringType) this.doctorPhone()).SetValue(value);
      }
    }

    public DG4._contactPersons ContactPersons
    {
      get
      {
        return this.contactPersons();
      }
    }

    public string BloodType
    {
      get
      {
        return this.bloodType().StringValue;
      }
      set
      {
        ((Asn1StringType) this.bloodType()).SetValue(value);
      }
    }

    public string BloodRH
    {
      get
      {
        return this.bloodRH().StringValue;
      }
      set
      {
        ((Asn1StringType) this.bloodRH()).SetValue(value);
      }
    }

    public string OrganDonorStatus
    {
      get
      {
        return this.organDonorStatus().StringValue;
      }
      set
      {
        ((Asn1StringType) this.organDonorStatus()).SetValue(value);
      }
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public override string TypeName
    {
      get
      {
        return "DG4";
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
      this.__initializeComponents(8);
      this.__setComponentTag(0, 0, 4);
      this.__setComponentTag(1, 1, 4);
      this.__setComponentTag(2, 2, 4);
      this.__setComponentTag(3, 3, 4);
      this.__setComponentIsOptional(4);
      this.__setComponentTag(4, 4, 4);
      this.__setComponentTag(5, 5, 4);
      this.__setComponentTag(6, 6, 4);
      this.__setComponentTag(7, 7, 4);
    }

    public void SetValue(DG4 typeInstance)
    {
      this.__setTypeValue((Asn1Type) typeInstance);
    }

    public DG4._doctorLastName doctorLastName()
    {
      this.__setComponentIsDefined(0);
      return (DG4._doctorLastName) this.__instantiateTypeByIndex(0);
    }

    public DG4._doctorFirstName doctorFirstName()
    {
      this.__setComponentIsDefined(1);
      return (DG4._doctorFirstName) this.__instantiateTypeByIndex(1);
    }

    public DG4._doctorReference doctorReference()
    {
      this.__setComponentIsDefined(2);
      return (DG4._doctorReference) this.__instantiateTypeByIndex(2);
    }

    public DG4._doctorPhone doctorPhone()
    {
      this.__setComponentIsDefined(3);
      return (DG4._doctorPhone) this.__instantiateTypeByIndex(3);
    }

    public DG4._contactPersons contactPersons()
    {
      this.__setComponentIsDefined(4);
      return (DG4._contactPersons) this.__instantiateTypeByIndex(4);
    }

    public bool HasContactPersons()
    {
      return this.__isComponentDefined(4);
    }

    public void HasContactPersons(bool has)
    {
      this.__setComponentIsDefined(4, has);
    }

    public DG4._bloodType bloodType()
    {
      this.__setComponentIsDefined(5);
      return (DG4._bloodType) this.__instantiateTypeByIndex(5);
    }

    public DG4._bloodRH bloodRH()
    {
      this.__setComponentIsDefined(6);
      return (DG4._bloodRH) this.__instantiateTypeByIndex(6);
    }

    public DG4._organDonorStatus organDonorStatus()
    {
      this.__setComponentIsDefined(7);
      return (DG4._organDonorStatus) this.__instantiateTypeByIndex(7);
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
            typeInstance = (Asn1Type) new DG4._doctorLastName();
            this.__setComponentTypeInstance(0, typeInstance);
            break;
          }
          else
            break;
        case 1:
          typeInstance = this.GetAsn1Type(1);
          if (typeInstance == null)
          {
            typeInstance = (Asn1Type) new DG4._doctorFirstName();
            this.__setComponentTypeInstance(1, typeInstance);
            break;
          }
          else
            break;
        case 2:
          typeInstance = this.GetAsn1Type(2);
          if (typeInstance == null)
          {
            typeInstance = (Asn1Type) new DG4._doctorReference();
            this.__setComponentTypeInstance(2, typeInstance);
            break;
          }
          else
            break;
        case 3:
          typeInstance = this.GetAsn1Type(3);
          if (typeInstance == null)
          {
            typeInstance = (Asn1Type) new DG4._doctorPhone();
            this.__setComponentTypeInstance(3, typeInstance);
            break;
          }
          else
            break;
        case 4:
          typeInstance = this.GetAsn1Type(4);
          if (typeInstance == null)
          {
            typeInstance = (Asn1Type) new DG4._contactPersons();
            this.__setComponentTypeInstance(4, typeInstance);
            break;
          }
          else
            break;
        case 5:
          typeInstance = this.GetAsn1Type(5);
          if (typeInstance == null)
          {
            typeInstance = (Asn1Type) new DG4._bloodType();
            this.__setComponentTypeInstance(5, typeInstance);
            break;
          }
          else
            break;
        case 6:
          typeInstance = this.GetAsn1Type(6);
          if (typeInstance == null)
          {
            typeInstance = (Asn1Type) new DG4._bloodRH();
            this.__setComponentTypeInstance(6, typeInstance);
            break;
          }
          else
            break;
        case 7:
          typeInstance = this.GetAsn1Type(7);
          if (typeInstance == null)
          {
            typeInstance = (Asn1Type) new DG4._organDonorStatus();
            this.__setComponentTypeInstance(7, typeInstance);
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
      if (!(anObject is DG4))
        return false;
      else
        return base.Equals(anObject);
    }

    public override int GetHashCode()
    {
      return base.GetHashCode();
    }

    [Serializable]
    public class _doctorLastName : Asn1UTF8StringType
    {
      [EditorBrowsable(EditorBrowsableState.Never)]
      public override string TypeName
      {
        get
        {
          return "doctorLastName";
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
      static _doctorLastName()
      {
      }

      public _doctorLastName()
      {
      }

      public _doctorLastName(string val)
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
        return 180L;
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
    public class _doctorFirstName : Asn1UTF8StringType
    {
      [EditorBrowsable(EditorBrowsableState.Never)]
      public override string TypeName
      {
        get
        {
          return "doctorFirstName";
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
      static _doctorFirstName()
      {
      }

      public _doctorFirstName()
      {
      }

      public _doctorFirstName(string val)
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
        return 200L;
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
    public class _doctorReference : Asn1UTF8StringType
    {
      [EditorBrowsable(EditorBrowsableState.Never)]
      public override string TypeName
      {
        get
        {
          return "doctorReference";
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
      static _doctorReference()
      {
      }

      public _doctorReference()
      {
      }

      public _doctorReference(string val)
        : base(val)
      {
      }

      public void SetValue(Asn1UTF8StringType typeInstance)
      {
        this.__setTypeValue((Asn1Type) typeInstance);
      }

      public override long GetLowerSize()
      {
        return 6L;
      }

      public override long GetUpperSize()
      {
        return 10L;
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
    public class _doctorPhone : Asn1NumericStringType
    {
      [EditorBrowsable(EditorBrowsableState.Never)]
      public override string TypeName
      {
        get
        {
          return "doctorPhone";
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
      static _doctorPhone()
      {
      }

      public _doctorPhone()
      {
      }

      public _doctorPhone(string val)
        : base(val)
      {
      }

      public void SetValue(Asn1NumericStringType typeInstance)
      {
        this.__setTypeValue((Asn1Type) typeInstance);
      }

      public override long GetLowerSize()
      {
        return 10L;
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
    public class _contactPersons : Asn1SequenceOfType
    {
      public DG4._contactPersons._element_ this[int index]
      {
        get
        {
          return (DG4._contactPersons._element_) base[index];
        }
        set
        {
          base[index] = (Asn1Type) value;
        }
      }

      [EditorBrowsable(EditorBrowsableState.Never)]
      public override string TypeName
      {
        get
        {
          return "contactPersons";
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
      protected internal override IEncoder __getPreEncoder()
      {
        if (ASN_EHCFactory.IsPreEncodingForConstructedOf())
          return ASN_EHCFactory.CreateEncoder();
        else
          return (IEncoder) null;
      }

      [EditorBrowsable(EditorBrowsableState.Never)]
      protected internal override IDecoder __getPostDecoder()
      {
        if (ASN_EHCFactory.IsPostDecodingForConstructedOf())
          return ASN_EHCFactory.CreateDecoder();
        else
          return (IDecoder) null;
      }

      [EditorBrowsable(EditorBrowsableState.Never)]
      protected internal override bool __isPostDecoding()
      {
        return ASN_EHCFactory.IsPostDecodingForConstructedOf();
      }

      public override Asn1Type NewAsn1Type()
      {
        return (Asn1Type) this.NewElement();
      }

      [EditorBrowsable(EditorBrowsableState.Never)]
      protected internal override Asn1Type __instantiateType()
      {
        DG4._contactPersons._element_ element = new DG4._contactPersons._element_();
        this.__addNewElementInstance((Asn1Type) element);
        return (Asn1Type) element;
      }

      public DG4._contactPersons._element_ NewElement()
      {
        return new DG4._contactPersons._element_();
      }

      public override void SetAsn1TypeList(IList elements)
      {
        this.Clear();
        if (elements == null)
          return;
        for (int index = 0; index < elements.Count; ++index)
        {
          object obj = elements[index];
          if (obj is DG4._contactPersons._element_)
            this.AddAsn1Type((Asn1Type) obj);
          else if (obj is ContactPerson)
            this.__addAsn1Type((Asn1Type) obj, (Asn1Type) this.NewElement());
        }
      }

      public void Add(DG4._contactPersons._element_ element)
      {
        this.AddAsn1Type((Asn1Type) element);
      }

      public void Add(ContactPerson element)
      {
        if (element is DG4._contactPersons._element_)
          this.AddAsn1Type((Asn1Type) element);
        else
          this.__addAsn1Type((Asn1Type) element, (Asn1Type) this.NewElement());
      }

      public DG4._contactPersons._element_ Get(int index)
      {
        return (DG4._contactPersons._element_) base[index];
      }

      public void Set(int index, DG4._contactPersons._element_ element)
      {
        base[index] = (Asn1Type) element;
      }

      public void SetValue(DG4._contactPersons typeInstance)
      {
        this.__setTypeValue((Asn1Type) typeInstance);
      }

      public override long GetLowerSize()
      {
        return 0L;
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

      [Serializable]
      public class _element_ : ContactPerson
      {
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override string TypeName
        {
          get
          {
            return (string) null;
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
      }
    }

    [Serializable]
    public class _bloodType : Asn1UTF8StringType
    {
      [EditorBrowsable(EditorBrowsableState.Never)]
      public override string TypeName
      {
        get
        {
          return "bloodType";
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
      static _bloodType()
      {
      }

      public _bloodType()
      {
      }

      public _bloodType(string val)
        : base(val)
      {
      }

      public void SetValue(Asn1UTF8StringType typeInstance)
      {
        this.__setTypeValue((Asn1Type) typeInstance);
      }

      public override long GetLowerSize()
      {
        return 3L;
      }

      public override long GetUpperSize()
      {
        return 3L;
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
    public class _bloodRH : Asn1UTF8StringType
    {
      [EditorBrowsable(EditorBrowsableState.Never)]
      public override string TypeName
      {
        get
        {
          return "bloodRH";
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
      static _bloodRH()
      {
      }

      public _bloodRH()
      {
      }

      public _bloodRH(string val)
        : base(val)
      {
      }

      public void SetValue(Asn1UTF8StringType typeInstance)
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

    [Serializable]
    public class _organDonorStatus : Asn1UTF8StringType
    {
      [EditorBrowsable(EditorBrowsableState.Never)]
      public override string TypeName
      {
        get
        {
          return "organDonorStatus";
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
      static _organDonorStatus()
      {
      }

      public _organDonorStatus()
      {
      }

      public _organDonorStatus(string val)
        : base(val)
      {
      }

      public void SetValue(Asn1UTF8StringType typeInstance)
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
        return 7;
      }

      [EditorBrowsable(EditorBrowsableState.Never)]
      protected internal override void __write(Asn1TypeBerWriter writer)
      {
        base.__write(writer, 7, 4);
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
          bool primitive1 = reader.__decodeTag((Asn1Type) this, 7, 4);
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
