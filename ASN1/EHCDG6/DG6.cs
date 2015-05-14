// Decompiled with JetBrains decompiler
// Type: Novensys.eCard.SDK.ASN1.EHCDG6.DG6
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

namespace Novensys.eCard.SDK.ASN1.EHCDG6
{
  [Serializable]
  public class DG6 : Asn1SequenceType
  {
    public DG6._diagnostics Diagnostics
    {
      get
      {
        return this.diagnostics();
      }
    }

    public DG6._chronicDiseases ChronicDiseases
    {
      get
      {
        return this.chronicDiseases();
      }
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public override string TypeName
    {
      get
      {
        return "DG6";
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
      this.__setComponentIsOptional(0);
      this.__setComponentTag(0, 0, 4);
      this.__setComponentIsOptional(1);
      this.__setComponentTag(1, 1, 4);
    }

    public void SetValue(DG6 typeInstance)
    {
      this.__setTypeValue((Asn1Type) typeInstance);
    }

    public DG6._diagnostics diagnostics()
    {
      this.__setComponentIsDefined(0);
      return (DG6._diagnostics) this.__instantiateTypeByIndex(0);
    }

    public bool HasDiagnostics()
    {
      return this.__isComponentDefined(0);
    }

    public void HasDiagnostics(bool has)
    {
      this.__setComponentIsDefined(0, has);
    }

    public DG6._chronicDiseases chronicDiseases()
    {
      this.__setComponentIsDefined(1);
      return (DG6._chronicDiseases) this.__instantiateTypeByIndex(1);
    }

    public bool HasChronicDiseases()
    {
      return this.__isComponentDefined(1);
    }

    public void HasChronicDiseases(bool has)
    {
      this.__setComponentIsDefined(1, has);
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
            typeInstance = (Asn1Type) new DG6._diagnostics();
            this.__setComponentTypeInstance(0, typeInstance);
            break;
          }
          else
            break;
        case 1:
          typeInstance = this.GetAsn1Type(1);
          if (typeInstance == null)
          {
            typeInstance = (Asn1Type) new DG6._chronicDiseases();
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
      if (!(anObject is DG6))
        return false;
      else
        return base.Equals(anObject);
    }

    public override int GetHashCode()
    {
      return base.GetHashCode();
    }

    [Serializable]
    public class _diagnostics : Asn1SequenceOfType
    {
      public DG6._diagnostics._element_ this[int index]
      {
        get
        {
          return (DG6._diagnostics._element_) base[index];
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
          return "diagnostics";
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
        DG6._diagnostics._element_ element = new DG6._diagnostics._element_();
        this.__addNewElementInstance((Asn1Type) element);
        return (Asn1Type) element;
      }

      public DG6._diagnostics._element_ NewElement()
      {
        return new DG6._diagnostics._element_();
      }

      public override void SetAsn1TypeList(IList elements)
      {
        this.Clear();
        if (elements == null)
          return;
        for (int index = 0; index < elements.Count; ++index)
        {
          object obj = elements[index];
          if (obj is DG6._diagnostics._element_)
            this.AddAsn1Type((Asn1Type) obj);
          else if (obj is Diagnostic)
            this.__addAsn1Type((Asn1Type) obj, (Asn1Type) this.NewElement());
        }
      }

      public void Add(DG6._diagnostics._element_ element)
      {
        this.AddAsn1Type((Asn1Type) element);
      }

      public void Add(Diagnostic element)
      {
        if (element is DG6._diagnostics._element_)
          this.AddAsn1Type((Asn1Type) element);
        else
          this.__addAsn1Type((Asn1Type) element, (Asn1Type) this.NewElement());
      }

      public DG6._diagnostics._element_ Get(int index)
      {
        return (DG6._diagnostics._element_) base[index];
      }

      public void Set(int index, DG6._diagnostics._element_ element)
      {
        base[index] = (Asn1Type) element;
      }

      public void SetValue(DG6._diagnostics typeInstance)
      {
        this.__setTypeValue((Asn1Type) typeInstance);
      }

      public override long GetLowerSize()
      {
        return 0L;
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

      [Serializable]
      public class _element_ : Diagnostic
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
    public class _chronicDiseases : Asn1SequenceOfType
    {
      public DG6._chronicDiseases._element_ this[int index]
      {
        get
        {
          return (DG6._chronicDiseases._element_) base[index];
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
          return "chronicDiseases";
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
        DG6._chronicDiseases._element_ element = new DG6._chronicDiseases._element_();
        this.__addNewElementInstance((Asn1Type) element);
        return (Asn1Type) element;
      }

      public DG6._chronicDiseases._element_ NewElement()
      {
        return new DG6._chronicDiseases._element_();
      }

      public override void SetAsn1TypeList(IList elements)
      {
        this.Clear();
        if (elements == null)
          return;
        for (int index = 0; index < elements.Count; ++index)
        {
          object obj = elements[index];
          if (obj is DG6._chronicDiseases._element_)
            this.AddAsn1Type((Asn1Type) obj);
          else if (obj is ChronicDesease)
            this.__addAsn1Type((Asn1Type) obj, (Asn1Type) this.NewElement());
        }
      }

      public void Add(DG6._chronicDiseases._element_ element)
      {
        this.AddAsn1Type((Asn1Type) element);
      }

      public void Add(ChronicDesease element)
      {
        if (element is DG6._chronicDiseases._element_)
          this.AddAsn1Type((Asn1Type) element);
        else
          this.__addAsn1Type((Asn1Type) element, (Asn1Type) this.NewElement());
      }

      public DG6._chronicDiseases._element_ Get(int index)
      {
        return (DG6._chronicDiseases._element_) base[index];
      }

      public void Set(int index, DG6._chronicDiseases._element_ element)
      {
        base[index] = (Asn1Type) element;
      }

      public void SetValue(DG6._chronicDiseases typeInstance)
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

      [Serializable]
      public class _element_ : ChronicDesease
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
  }
}
