// Decompiled with JetBrains decompiler
// Type: Novensys.ASN1.Type.Asn1ChoiceType
// Assembly: Novensys.eCard.SDK, Version=1.1.56.0, Culture=neutral, PublicKeyToken=null
// MVID: 59F9E456-D3DA-4066-A4A4-692B516A775E
// Assembly location: C:\Program Files (x86)\CNAS\SIUI-SPITAL\Novensys.eCard.SDK.dll

using Novensys.ASN1;
using Novensys.ASN1.Util;
using System;
using System.ComponentModel;
using System.Text;

namespace Novensys.ASN1.Type
{
  [Serializable]
  public class Asn1ChoiceType : Asn1ConstructedType
  {
    private int __chosenIndex = -1;

    public override string TypeName
    {
      get
      {
        return "CHOICE";
      }
    }

    public Asn1ChoiceType()
    {
      this.__chosenIndex = -1;
      this.__initialize();
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal virtual int __getListRelativeIndex()
    {
      if (this.__chosenIndex < 0)
        throw new Asn1Exception(3, "<" + this.GetType().FullName + ">");
      if (this.__chosenIndex >= this.__getNbRootComponents())
        return this.__chosenIndex - this.__getNbRootComponents();
      else
        return this.__chosenIndex;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal virtual Asn1Type __getMatchingAmbiguousComponent(string typeIdentification)
    {
      if (this.__isXerUseType())
      {
        bool flag = false;
        for (int index1 = this.__getXerFirstComponentIndex(); index1 < this.__components.Length && index1 != -1; index1 = this.__getXerNextComponentIndex(index1))
        {
          Asn1Type asn1Type = this.__instantiateTypeByIndex(index1);
          if (typeIdentification.Equals(asn1Type.__getXerTag()))
          {
            if (flag)
            {
              this.__setComponentIsDefined(index1);
              return asn1Type;
            }
            else
              flag = true;
          }
          if (asn1Type is Asn1ChoiceType)
          {
            Asn1ChoiceType asn1ChoiceType = (Asn1ChoiceType) asn1Type;
            if (asn1ChoiceType.__isXerUseUnion())
            {
              for (int index2 = asn1ChoiceType.__getXerFirstComponentIndex(); index2 < asn1ChoiceType.__components.Length && index2 != -1; index2 = asn1ChoiceType.__getXerNextComponentIndex(index2))
              {
                string xerTag = asn1ChoiceType.__components[index2].getXerTag();
                if (typeIdentification.Equals(xerTag))
                {
                  if (flag)
                  {
                    this.__setComponentIsDefined(index1);
                    return asn1Type;
                  }
                  else
                    flag = true;
                }
              }
            }
          }
        }
      }
      return (Asn1Type) null;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal virtual Asn1Type __getMatchingComponent(int relativeIndex, bool indexInRoot)
    {
      if (relativeIndex < 0)
        return (Asn1Type) null;
      int index;
      if (indexInRoot)
      {
        if (relativeIndex >= this.__getNbRootComponents())
          return (Asn1Type) null;
        index = relativeIndex;
      }
      else
      {
        if (relativeIndex >= this.__getNbFlatExtensionComponents())
          return (Asn1Type) null;
        index = this.__getNbRootComponents() + relativeIndex;
      }
      this.__instantiateTypeByIndex(index);
      this.__setComponentIsDefined(index);
      return this.GetTypeValue();
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal override Asn1Type __getMatchingInstance(int tagNumber, int tagClass)
    {
      return base.__getMatchingComponent(tagNumber, tagClass);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal Asn1Type __getMatchingUseUnionComponent(string xerTag, string xerNamespace)
    {
      if (this.__components != null)
      {
        for (int index = 0; index < this.__components.Length; ++index)
        {
          Asn1Type asn1Type = (Asn1Type) null;
          if (this.__components[index].isXerUseUnion())
            asn1Type = ((Asn1ConstructedType) this.__instantiateTypeByIndex(index)).__getMatchingComponent(xerTag, xerNamespace);
          if (asn1Type != null)
          {
            this.__setComponentIsDefined(index);
            return asn1Type;
          }
        }
      }
      return (Asn1Type) null;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal virtual int[] __getPerEmbeddedTagTagNumbersSet()
    {
      return (int[]) null;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal virtual int __getPerEmbeddedTagTagSize()
    {
      return -1;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal override int __getUniversalTagNumber()
    {
      return -1;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal override string __getXerTag()
    {
      return "CHOICE";
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal bool __isChosenInstanceFirstComponent()
    {
      return this.__chosenIndex == this.__getXerFirstComponentIndex();
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal bool __isChosenInstanceUseUnion()
    {
      return this.__chosenIndex != -1 && this.__components[this.__chosenIndex].isXerUseUnion();
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal virtual bool __isPerUseTag()
    {
      return false;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal override bool __isValueOutOfRoot()
    {
      return this.__chosenIndex >= this.__getNbRootComponents();
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal virtual bool __isXerUseType()
    {
      return false;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal virtual bool __isXerUseUnion()
    {
      return false;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal override void __read(Asn1TypePerReader reader)
    {
      reader.__decodeChoiceType(this);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal override void __read(Asn1TypeXerReader reader)
    {
      reader.__decodeChoiceType(this);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal override void __read(Asn1TypeBerReader reader, bool isExplicit, bool primitive, long len)
    {
      reader.__decodeChoiceType(this);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal Asn1Type __readAmbiguousValue(Asn1TypeXerReader reader, string text)
    {
      int index = this.__getXerFirstComponentIndex();
      while (index < this.__components.Length && index != -1)
      {
        Asn1Type asn1Type = this.__instantiateTypeByIndex(index);
        try
        {
          asn1Type.__readValue(reader, text);
          asn1Type.Validate();
          this.__setComponentIsDefined(index);
          return asn1Type;
        }
        catch (Exception ex)
        {
          index = this.__getXerNextComponentIndex(index);
        }
      }
      return (Asn1Type) null;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal override void __readValue(Asn1TypeXerReader reader, string text)
    {
      reader.__decodeChoiceValue(this, text);
    }

    protected internal override void __setComponentIsDefined(int index)
    {
      this.__chosenIndex = index;
      this.__components[index].setDefined(true);
    }

    protected internal override void __setComponentIsDefined(int index, bool isDefined)
    {
      this.__components[index].setDefined(isDefined);
      if (isDefined)
        this.__chosenIndex = index;
      else
        this.__chosenIndex = -1;
    }

    protected internal override void __setTypeValue(Asn1Type typeInstance)
    {
      System.Type type =  null;
      Asn1Type typeInstance1 = (Asn1Type) null;
      if (typeInstance == null)
        this.ResetType();
      else if (typeInstance is Asn1ChoiceType)
      {
        typeInstance1 = ((Asn1ChoiceType) typeInstance).GetTypeValue();
        if (typeInstance1 != null)
          type = typeInstance1.GetType();
      }
      else
      {
        type = typeInstance.GetType();
        typeInstance1 = typeInstance;
      }
      if (type == null || typeInstance1 == null)
        return;
      for (int index = 0; index < this.Length; ++index)
      {
        this.__instantiateTypeByIndex(index);
        Asn1Type asn1Type = this.GetAsn1Type(index);
        if (type.IsInstanceOfType((object) asn1Type))
        {
          this.__setComponentIsDefined(index);
          asn1Type.__setTypeValue(typeInstance1);
          break;
        }
        else
          this.__cancelComponentTypeInstance(index);
      }
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal override void __write(Asn1TypeBerWriter writer)
    {
      writer.__encodeChoiceType(this);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal override void __write(Asn1TypePerWriter writer)
    {
      writer.__encodeChoiceType(this);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal override void __write(Asn1TypeXerWriter writer)
    {
      writer.__encodeChoiceType(this);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal override void __write(Asn1TypeBerWriter writer, int tagNumber, int tagClass)
    {
      writer.__encodeChoiceType(this);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal override string __writeValue(Asn1TypeXerWriter writer)
    {
      return writer.__encodeChoiceValue(this);
    }

    public override bool Equals(object anObject)
    {
      return this == anObject || anObject is Asn1ChoiceType && this.HasEqualValue((Asn1ChoiceType) anObject);
    }

    public int GetAsn1TypeIndex()
    {
      return this.__chosenIndex;
    }

    public override int GetHashCode()
    {
      if (this.GetTypeValue() != null)
        return this.GetTypeValue().GetHashCode();
      else
        return 0;
    }

    public override string GetPrintableValue(string indent, string newline)
    {
      StringBuilder stringBuilder = new StringBuilder();
      Asn1Type typeValue = this.GetTypeValue();
      if (typeValue != null)
      {
        stringBuilder.Append(typeValue.TypeName);
        stringBuilder.Append(" : ");
        stringBuilder.Append(typeValue.GetPrintableValue(indent, newline));
      }
      else if (this.__unknownExtensions != null && this.__unknownExtensions.Count == 1)
      {
        stringBuilder.Append("unknownAlternative");
        stringBuilder.Append(" : ");
        stringBuilder.Append("'" + ByteArray.ByteArrayToHexString((byte[]) this.__unknownExtensions[0], "", -1, true) + "'H");
      }
      return ((object) stringBuilder).ToString();
    }

    public Asn1Type GetTypeValue()
    {
      if (this.__chosenIndex == -1)
        return (Asn1Type) null;
      else
        return this.GetAsn1Type(this.__chosenIndex);
    }

    public virtual bool HasEqualValue(Asn1ChoiceType that)
    {
      if (that == null)
        return false;
      Asn1Type typeValue1 = that.GetTypeValue();
      Asn1Type typeValue2 = this.GetTypeValue();
      if (typeValue2 == null)
        return typeValue1 == null;
      else
        return typeValue2.Equals((object) typeValue1);
    }

    public override void ResetType()
    {
      this.__resetCommons();
      if (this.__chosenIndex != -1)
      {
        this.__components[this.__chosenIndex].setDefined(false);
        if (this.GetAsn1Type(this.__chosenIndex) != null)
          this.__components[this.__chosenIndex].ResetType();
        this.__chosenIndex = -1;
      }
      if (this.__getDefaultInstance() == null)
        return;
      this.__setTypeValue(this.__getDefaultInstance());
    }

    public override void ResolveContent()
    {
      if (this.__chosenIndex == -1)
        return;
      this.__components[this.__chosenIndex].resolveContent();
    }

    public override void Validate()
    {
      if (this.IsExtensible() && this.GetUnknownExtensions() != null)
        return;
      if (this.__chosenIndex < 0)
        throw new Asn1ValidationException(3, "<" + this.GetType().FullName + ">");
      Asn1Type asn1Type = this.GetAsn1Type(this.__chosenIndex);
      if (asn1Type != null)
        asn1Type.Validate();
    }
  }
}
