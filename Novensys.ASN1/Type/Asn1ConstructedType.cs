// Decompiled with JetBrains decompiler
// Type: Novensys.ASN1.Type.Asn1ConstructedType
// Assembly: Novensys.eCard.SDK, Version=1.1.56.0, Culture=neutral, PublicKeyToken=null
// MVID: 59F9E456-D3DA-4066-A4A4-692B516A775E
// Assembly location: C:\Program Files (x86)\CNAS\SIUI-SPITAL\Novensys.eCard.SDK.dll

using Novensys.ASN1;
using Novensys.ASN1.Util;
using System;
using System.Collections;
using System.ComponentModel;
using System.Text;

namespace Novensys.ASN1.Type
{
  [Serializable]
  public abstract class Asn1ConstructedType : Asn1Type
  {
    protected Asn1ConstructedType.ComponentInfo[] __components = (Asn1ConstructedType.ComponentInfo[]) null;
    private Asn1ConstructedType.ExtensionAdditionGroupInfo[] __extensionAdditionGroups = (Asn1ConstructedType.ExtensionAdditionGroupInfo[]) null;
    protected int __nextComponentIndex = 0;
    protected IList __unknownExtensions = (IList) null;

    public int Length
    {
      get
      {
        if (this.__components != null)
          return this.__components.Length;
        else
          return 0;
      }
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal virtual void __addUnknownExtension(byte[] data)
    {
      if (this.__unknownExtensions == null)
        this.__unknownExtensions = (IList) new ArrayList();
      this.__unknownExtensions.Add((object) data);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal void __addXerUseOrderComponent(string identifier)
    {
      if (this.__isXerUseOrder())
      {
        int index = this.__isXerEmbedValues() ? 1 : 0;
        Asn1SequenceOfType asn1SequenceOfType = (Asn1SequenceOfType) this.__instantiateTypeByIndex(index);
        this.__setComponentIsDefined(index);
        ((Asn1EnumeratedType) asn1SequenceOfType.__getMatchingElement()).SetValue(identifier);
      }
      else
        ((Asn1ConstructedType) this.__enclosingType).__addXerUseOrderComponent(identifier);
    }

    protected void __cancelComponentTypeInstance(int index)
    {
      this.__components[index].cancelTypeInstance();
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal void __cancelExtensionAdditionGroupOptDefDescriptor(int group)
    {
      this.__extensionAdditionGroups[group].cancelOptDefDescriptor();
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal virtual void __cancelExtensionDescriptor()
    {
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal override bool __containsXerAnyAttributesComponent()
    {
      return false;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal override bool __containsXerAnyElementComponent()
    {
      return false;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal void __ensureUntaggedComponentsDefined()
    {
      if (this.__components == null)
        return;
      for (int index = 0; index < this.__components.Length; ++index)
      {
        if (this.__components[index].isXerUntagged() && !this.__components[index].isDefined() && !this.__components[index].isOptional())
          this.NewAsn1Type(index);
      }
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal virtual void __fillDescriptors(bool encodingDefaultValues)
    {
      this.__fillRootOptDefDescriptor(encodingDefaultValues);
      this.__fillExtensionDescriptor(encodingDefaultValues);
    }

    private bool __fillExtensionAdditionGroupOptDefDescriptor(int group, int firstExtensionComponentIndex, bool encodingDefaultValues)
    {
      int bitIndex = -1;
      bool flag = false;
      BitString bitString = this.__getExtensionAdditionGroupOptDefDescriptor(group) ?? this.__newExtensionAdditionGroupOptDefDescriptor(group);
      bitString.clearAll();
      for (int index = firstExtensionComponentIndex + this.__getNbRootComponents(); index < firstExtensionComponentIndex + this.__getNbRootComponents() + this.__getExtensionAdditionGroupNbComponents(group); ++index)
      {
        if (this.IsComponentOptional(index))
        {
          ++bitIndex;
          if (this.__isComponentDefined(index))
          {
            flag = true;
            bitString.setTrue(bitIndex);
          }
        }
        else if (this.IsComponentDefault(index))
        {
          ++bitIndex;
          if (this.GetAsn1Type(index) == null && this.IsComponentDefault(index))
            this.__setComponentDefaultInstance(index);
          if (this.__isComponentDefined(index))
          {
            flag = true;
            if (encodingDefaultValues || !this.HasComponentDefaultValue(index))
              bitString.setTrue(bitIndex);
          }
        }
        else if (this.__isComponentDefined(index))
          flag = true;
      }
      if (!bitString.containsTrue() && bitString.length() == this.__getExtensionAdditionGroupNbComponents(group))
      {
        this.__cancelExtensionAdditionGroupOptDefDescriptor(group);
        flag = false;
      }
      return flag;
    }

    private void __fillExtensionDescriptor(bool encodingDefaultValues)
    {
      int bitIndex = -1;
      int group = -1;
      BitString extensionDescriptor = this.__getExtensionDescriptor();
      if (extensionDescriptor == null)
        return;
      extensionDescriptor.clearAll();
      for (int index1 = 0; index1 < this.__getNbFlatExtensionComponents(); ++index1)
      {
        group = this.__getComponentExtensionAdditionGroup(index1);
        if (group != -1)
        {
          ++bitIndex;
          if (this.__fillExtensionAdditionGroupOptDefDescriptor(group, index1, encodingDefaultValues))
            extensionDescriptor.setTrue(bitIndex);
          index1 += this.__getExtensionAdditionGroupNbComponents(group) - 1;
        }
        else
        {
          ++bitIndex;
          int index2 = index1 + this.__getNbRootComponents();
          if (this.GetAsn1Type(index2) == null && this.IsComponentDefault(index2))
            this.__setComponentDefaultInstance(index2);
          if (this.__isComponentDefined(index2) && (encodingDefaultValues || !this.HasComponentDefaultValue(index2)))
            extensionDescriptor.setTrue(bitIndex);
        }
      }
      if (!extensionDescriptor.containsTrue() && group == -1)
        this.__cancelExtensionDescriptor();
    }

    private void __fillRootOptDefDescriptor(bool encodingDefaultValues)
    {
      int bitIndex = -1;
      BitString optDefDescriptor = this.__getRootOptDefDescriptor();
      if (optDefDescriptor == null)
        return;
      optDefDescriptor.clearAll();
      for (int index = 0; index < this.__getNbRootComponents(); ++index)
      {
        if (this.IsComponentDefault(index) || this.IsComponentOptional(index))
        {
          ++bitIndex;
          if (this.GetAsn1Type(index) == null && this.IsComponentDefault(index))
            this.__setComponentDefaultInstance(index);
          if (this.__isComponentDefined(index) && (encodingDefaultValues || !this.HasComponentDefaultValue(index)))
            optDefDescriptor.setTrue(bitIndex);
        }
      }
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal virtual int __getBerFirstComponentIndex()
    {
      return 0;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal virtual int __getBerNextComponentIndex(int index)
    {
      return index + 1;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal int __getComponentExtensionAdditionGroup(int index)
    {
      return this.__components[index + this.__getNbRootComponents()].getExtensionAdditionGroup();
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal Asn1Type __getDefinedComponentTypeInstance(int index)
    {
      if (this.IsComponentDefault(index) && this.GetAsn1Type(index) == null)
        this.__instantiateTypeByIndex(index);
      return this.GetAsn1Type(index);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal int __getExtensionAdditionGroupNbComponents(int group)
    {
      return this.__extensionAdditionGroups[group].getLength();
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal BitString __getExtensionAdditionGroupOptDefDescriptor(int group)
    {
      return this.__extensionAdditionGroups[group].getOptDefDescriptor();
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal int __getExtensionAdditionGroupOptDefDescriptorSize(int group)
    {
      return this.__extensionAdditionGroups[group].getOptDefDescriptorSize();
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal Asn1Type __getExtensionComponentTypeInstance(int index)
    {
      return this.__components[index + this.__getNbRootComponents()].getTypeInstance();
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal virtual BitString __getExtensionDescriptor()
    {
      return (BitString) null;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal Asn1Type __getInstantiatedComponentTypeInstance(int index)
    {
      if (this.__isComponentDefined(index))
        return this.GetAsn1Type(index);
      bool flag = false;
      if (this.GetAsn1Type(index) == null)
      {
        this.__instantiateTypeByIndex(index);
        flag = true;
      }
      Asn1Type asn1Type = this.GetAsn1Type(index);
      if (flag)
        this.__cancelComponentTypeInstance(index);
      return asn1Type;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal virtual Asn1Type __getMatchingComponent(int absoluteIndex)
    {
      this.__setComponentIsDefined(absoluteIndex);
      return this.__instantiateTypeByIndex(absoluteIndex);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal virtual Asn1Type __getMatchingComponent(int tagNumber, int tagClass)
    {
      if (this.__components != null)
      {
        int index = this.__nextComponentIndex;
        while (index < this.__components.Length && index != -1)
        {
          int tagValue = this.__components[index].getTagValue();
          Asn1Type asn1Type = (Asn1Type) null;
          if (tagValue != -1)
          {
            if (tagValue == tagNumber && this.__components[index].getTagClass() == tagClass)
              asn1Type = this.__instantiateTypeByIndex(index);
          }
          else
            asn1Type = this.__instantiateTypeByIndex(index).__getMatchingInstance(tagNumber, tagClass);
          if (asn1Type != null)
          {
            this.__setComponentIsDefined(index);
            if (this.__isOrdered())
              this.__nextComponentIndex = this.__getBerNextComponentIndex(index);
            return asn1Type;
          }
          else if (this.__isOrdered())
            index = this.__getBerNextComponentIndex(index);
          else
            ++index;
        }
      }
      return (Asn1Type) null;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal virtual Asn1Type __getMatchingComponent(string xerTag, string xerNamespace)
    {
      return this.__getMatchingComponent(xerTag, xerNamespace, false);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal virtual Asn1Type __getMatchingComponent(string xerTag, string xerNamespace, bool notOrdered)
    {
      if (this.__components != null)
      {
        int index = this.__nextComponentIndex;
        if (index == 0)
        {
          if (this.__isXerEmbedValues())
            index = this.__isXerUseOrder() ? 2 : 1;
          else if (this.__isXerUseOrder())
            index = 1;
        }
        for (; index < this.__components.Length && index != -1; index = this.__getXerNextComponentIndex(index))
        {
          Asn1Type asn1Type = (Asn1Type) null;
          if (this.__components[index].isXerUntagged())
          {
            asn1Type = this.__instantiateTypeByIndex(index);
            if (xerTag != null && !asn1Type.__hasMatchingComponent(xerTag, xerNamespace))
              asn1Type = (Asn1Type) null;
          }
          else
          {
            string xerTag1 = this.__components[index].getXerTag();
            if (xerTag1 != null)
            {
              if (xerTag1.Equals(xerTag))
              {
                asn1Type = this.__instantiateTypeByIndex(index);
                if (xerNamespace != null && xerNamespace.Length > 0 && xerNamespace != asn1Type.__getXerNamespaceUri())
                  asn1Type = (Asn1Type) null;
              }
            }
            else
              asn1Type = this.__instantiateTypeByIndex(index);
          }
          if (asn1Type != null)
          {
            this.__setComponentIsDefined(index);
            if (!notOrdered && this.__isOrdered())
              this.__nextComponentIndex = this.__getXerNextComponentIndex(index);
            return asn1Type;
          }
        }
      }
      return (Asn1Type) null;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal virtual int __getNbFlatExtensionComponents()
    {
      return 0;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal virtual int __getNbRootComponents()
    {
      return 0;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal virtual BitString __getPerOptionalityDescriptor()
    {
      return (BitString) null;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal Asn1Type __getRootComponentTypeInstance(int index)
    {
      return this.__components[index].getTypeInstance();
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal virtual BitString __getRootOptDefDescriptor()
    {
      return (BitString) null;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal Asn1ConstructedOfType __getXerAnyAttributesComponent()
    {
      if (this.__components != null)
      {
        for (int index = 0; index < this.__components.Length; ++index)
        {
          Asn1Type asn1Type = this.__instantiateTypeByIndex(index);
          if (asn1Type.__isXerAnyAttributes())
          {
            this.__setComponentIsDefined(index);
            return (Asn1ConstructedOfType) asn1Type;
          }
        }
      }
      return (Asn1ConstructedOfType) null;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal override Asn1Type __getXerAnyElementComponent()
    {
      if (this.__components != null)
      {
        for (int index = 0; index < this.__components.Length; ++index)
        {
          Asn1Type asn1Type = this.__instantiateTypeByIndex(index);
          if (asn1Type.__isXerAnyElement())
          {
            this.__setComponentIsDefined(index);
            return asn1Type;
          }
          else if (asn1Type.__isXerUntagged() && asn1Type.__containsXerAnyElementComponent())
          {
            this.__setComponentIsDefined(index);
            return asn1Type;
          }
        }
      }
      return (Asn1Type) null;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    private Asn1Type __getXerDefinedComponentTypeInstance(Asn1Type[] definedComponents, string name)
    {
      if (definedComponents != null)
      {
        for (int index = 0; index < definedComponents.Length; ++index)
        {
          Asn1Type asn1Type = definedComponents[index];
          if (asn1Type != null)
          {
            string typeName = asn1Type.TypeName;
            if (name.Equals(typeName))
              return asn1Type;
          }
        }
      }
      return (Asn1Type) null;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal Asn1Type[] __getXerDefinedComponentTypeInstances(bool encodingDefaultValues)
    {
      if (this.__components == null)
        return (Asn1Type[]) null;
      int length = this.__components.Length;
      Asn1Type[] asn1TypeArray = (Asn1Type[]) null;
      int num = 0;
      for (int index1 = this.__getXerFirstComponentIndex(); index1 < length && index1 != -1; index1 = this.__getXerNextComponentIndex(index1))
      {
        Asn1Type asn1Type = this.__components[index1].getTypeInstance();
        if (asn1Type == null && this.__components[index1].isDefault())
          asn1Type = this.__setComponentDefaultInstance(index1);
        if (this.__components[index1].isDefined() && (encodingDefaultValues || !this.__components[index1].isTypeInstanceWithDefaultValue()))
        {
          if (asn1TypeArray == null)
          {
            asn1TypeArray = new Asn1Type[length];
            for (int index2 = 0; index2 < length; ++index2)
              asn1TypeArray[index2] = (Asn1Type) null;
          }
          asn1TypeArray[num++] = asn1Type;
        }
      }
      return asn1TypeArray;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal Asn1Type __getXerDefinedUseNilOptionalComponent()
    {
      if (this.__components != null)
      {
        int index = 0;
        if (this.__isXerEmbedValues())
          index = this.__isXerUseOrder() ? 2 : 1;
        else if (this.__isXerUseOrder())
          index = 1;
        for (; index < this.__components.Length; ++index)
        {
          if (this.__components[index].isOptional() && this.__components[index].isDefined())
          {
            Asn1Type typeInstance = this.__components[index].getTypeInstance();
            if (!typeInstance.__isXerAttribute() && !typeInstance.__isXerAnyAttributes())
              return typeInstance;
          }
        }
      }
      return (Asn1Type) null;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal Asn1ConstructedOfType __getXerEmbedValuesComponent()
    {
      if (!this.__isXerEmbedValues())
        return (Asn1ConstructedOfType) null;
      this.__setComponentIsDefined(0);
      return (Asn1ConstructedOfType) this.__instantiateTypeByIndex(0);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal virtual int __getXerFirstComponentIndex()
    {
      return 0;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal virtual int __getXerNextComponentIndex(int index)
    {
      return index + 1;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal Asn1Type __getXerSingleUntaggedComponent()
    {
      if (this.__components != null)
      {
        for (int index = 0; index < this.__components.Length; ++index)
        {
          Asn1Type asn1Type = this.__instantiateTypeByIndex(index);
          if (asn1Type.__isXerUntagged())
          {
            this.__setComponentIsDefined(index);
            return asn1Type;
          }
        }
      }
      return (Asn1Type) null;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal Asn1Type __getXerUseNilOptionalComponent()
    {
      if (this.__components != null)
      {
        int index = 0;
        if (this.__isXerEmbedValues())
          index = this.__isXerUseOrder() ? 2 : 1;
        else if (this.__isXerUseOrder())
          index = 1;
        for (; index < this.__components.Length; ++index)
        {
          if (this.__components[index].isOptional())
          {
            Asn1Type asn1Type = this.__instantiateTypeByIndex(index);
            if (!asn1Type.__isXerAttribute() && !asn1Type.__isXerAnyAttributes())
            {
              this.__setComponentIsDefined(index);
              return asn1Type;
            }
          }
        }
      }
      return (Asn1Type) null;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal Asn1Type[] __getXerUseOrderComponentTypeInstances(Asn1Type[] definedComponents)
    {
      Asn1SequenceOfType asn1SequenceOfType;
      if (this.__isXerUseOrder())
      {
        asn1SequenceOfType = (Asn1SequenceOfType) this.__instantiateTypeByIndex(this.__isXerEmbedValues() ? 1 : 0);
      }
      else
      {
        Asn1ConstructedType asn1ConstructedType = (Asn1ConstructedType) this.__enclosingType;
        int index = asn1ConstructedType.__isXerEmbedValues() ? 1 : 0;
        asn1SequenceOfType = (Asn1SequenceOfType) asn1ConstructedType.__instantiateTypeByIndex(index);
      }
      int count = asn1SequenceOfType.Count;
      if (count <= 0)
        return (Asn1Type[]) null;
      Asn1Type[] asn1TypeArray = new Asn1Type[count];
      int num = 0;
      for (int index = 0; index < count; ++index)
      {
        string identifier = ((Asn1EnumeratedType) asn1SequenceOfType.__getElementAt(index)).GetIdentifier();
        Asn1Type componentTypeInstance = this.__getXerDefinedComponentTypeInstance(definedComponents, identifier);
        if (componentTypeInstance == null)
          throw new Asn1Exception(61, "for component '" + identifier + "' in type <" + this.GetType().FullName + ">");
        else
          asn1TypeArray[num++] = componentTypeInstance;
      }
      return asn1TypeArray;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal override bool __hasMatchingComponent(string xerTag, string xerNamespace)
    {
      if (this.__components != null)
      {
        int index = 0;
        if (index == 0)
        {
          if (this.__isXerEmbedValues())
            index = this.__isXerUseOrder() ? 2 : 1;
          else if (this.__isXerUseOrder())
            index = 1;
        }
        for (; index < this.__components.Length; ++index)
        {
          if (this.__components[index].isXerUntagged())
          {
            if (this.__instantiateTypeByIndex(index).__hasMatchingComponent(xerTag, xerNamespace))
              return true;
          }
          else
          {
            string xerTag1 = this.__components[index].getXerTag();
            if (xerTag1 == null)
              return true;
            if (xerTag1.Equals(xerTag))
            {
              Asn1Type asn1Type = this.__instantiateTypeByIndex(index);
              if (xerNamespace == null || xerNamespace.Length <= 0 || xerNamespace == asn1Type.__getXerNamespaceUri())
                return true;
            }
          }
        }
      }
      return false;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal virtual void __initBerFirstComponentIndex()
    {
      this.__nextComponentIndex = 0;
    }

    protected internal virtual void __initialize()
    {
    }

    protected internal virtual void __initializeComponents(int nbComponents)
    {
      this.__nextComponentIndex = 0;
      this.__components = new Asn1ConstructedType.ComponentInfo[nbComponents];
      for (int index = 0; index < this.__components.Length; ++index)
        this.__components[index] = new Asn1ConstructedType.ComponentInfo();
    }

    protected internal virtual void __initializeExtensionAdditionGroups(int nbGroups)
    {
      this.__extensionAdditionGroups = new Asn1ConstructedType.ExtensionAdditionGroupInfo[nbGroups];
      for (int index = 0; index < this.__extensionAdditionGroups.Length; ++index)
        this.__extensionAdditionGroups[index] = new Asn1ConstructedType.ExtensionAdditionGroupInfo();
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal virtual void __initXerFirstComponentIndex()
    {
      this.__nextComponentIndex = 0;
    }

    protected internal virtual Asn1Type __instantiateTypeByIndex(int index)
    {
      return (Asn1Type) null;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal bool __isComponentDefined(int index)
    {
      return this.__components[index].isDefined();
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal virtual bool __isOrdered()
    {
      return false;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal virtual bool __isPerOptionalityIn()
    {
      return false;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal virtual bool __isPerTerminatingComponent()
    {
      return false;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal virtual bool __isValueOutOfRoot()
    {
      return this.__getExtensionDescriptor() != null && this.__getExtensionDescriptor().containsTrue();
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal bool __isXerEnclosingTypeUseOrder()
    {
      if (this.__enclosingType == null || !(this.__enclosingType is Asn1ConstructedType))
        return false;
      Asn1ConstructedType asn1ConstructedType = (Asn1ConstructedType) this.__enclosingType;
      return asn1ConstructedType.__isXerUseOrder() && asn1ConstructedType.__isXerUseNil();
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal virtual bool __isXerUseNil()
    {
      return false;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal virtual bool __isXerUseOrder()
    {
      return false;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal BitString __newExtensionAdditionGroupOptDefDescriptor(int group)
    {
      return this.__extensionAdditionGroups[group].newOptDefDescriptor();
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal override void __read(Asn1TypePerReader reader)
    {
      reader.__decodeConstructedType(this);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal override void __read(Asn1TypeXerReader reader)
    {
      reader.__decodeConstructedType(this);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal override void __read(Asn1TypeBerReader reader, bool isExplicit, bool primitive, long len)
    {
      reader.__decodeConstructedType(this, isExplicit, primitive, len);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal override void __readValue(Asn1TypeXerReader reader, string text)
    {
      reader.__decodeConstructedValue(this, text);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal Asn1Type __setComponentDefaultInstance(int index)
    {
      if (this.__components[index].getTypeInstance() == null)
        this.NewAsn1Type(index);
      this.__components[index].setDefaultInstance();
      return this.__components[index].getTypeInstance();
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal void __setComponentExtensionAdditionGroup(int index, int group, int nbComponents, int optDefDescriptorSize)
    {
      this.__components[index].setExtensionAdditionGroup(group);
      this.__extensionAdditionGroups[group].setLength(nbComponents);
      this.__extensionAdditionGroups[group].setOptDefDescriptorSize(optDefDescriptorSize);
    }

    protected void __setComponentIsDefault(int index)
    {
      this.__components[index].setIsDefault();
    }

    protected internal virtual void __setComponentIsDefined(int index)
    {
      this.__components[index].setDefined(true);
    }

    protected internal virtual void __setComponentIsDefined(int index, bool has)
    {
      this.__components[index].setDefined(has);
    }

    protected void __setComponentIsInExtension(int index)
    {
      this.__components[index].setIsInExtension();
    }

    protected void __setComponentIsOptional(int index)
    {
      this.__components[index].setIsOptional();
    }

    protected void __setComponentTag(int index, int tagValue, int tagClass)
    {
      this.__components[index].setTag(tagValue, tagClass);
    }

    protected void __setComponentTypeInstance(int index, Asn1Type typeInstance)
    {
      this.__components[index].setTypeInstance(typeInstance);
      if (typeInstance == null)
        return;
      typeInstance.__setEnclosingType((Asn1Type) this);
    }

    protected void __setComponentXerIsUntagged(int index)
    {
      this.__components[index].setIsXerUntagged();
    }

    protected void __setComponentXerIsUseUnion(int index)
    {
      this.__components[index].setIsXerUseUnion();
    }

    protected void __setComponentXerTag(int index, string xerTag)
    {
      this.__components[index].setXerTag(xerTag);
    }

    protected internal virtual void __setConstructedTypeValue(Asn1ConstructedType typeInstance)
    {
      if (typeInstance == null)
      {
        this.ResetType();
      }
      else
      {
        for (int index = 0; index < typeInstance.Length; ++index)
        {
          Asn1Type asn1Type = this.GetAsn1Type(index);
          if (typeInstance.__isComponentDefined(index))
          {
            Asn1Type componentTypeInstance = typeInstance.__getDefinedComponentTypeInstance(index);
            this.__instantiateTypeByIndex(index).__setTypeValue(componentTypeInstance);
            this.__setComponentIsDefined(index);
          }
          else if (this.__isComponentDefined(index) && asn1Type != null)
          {
            asn1Type.ResetType();
            this.__setComponentIsDefined(index, false);
          }
        }
        if (typeInstance.__unknownExtensions != null)
        {
          for (int index = 0; index < typeInstance.__unknownExtensions.Count; ++index)
          {
            byte[] numArray = (byte[]) typeInstance.__unknownExtensions[index];
            byte[] data = new byte[numArray.Length];
            Array.Copy((Array) numArray, 0, (Array) data, 0, numArray.Length);
            this.__addUnknownExtension(data);
          }
        }
        else
          this.__unknownExtensions = (IList) null;
      }
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal void __setDefaultForEmptyComponents()
    {
      if (this.__components == null)
        return;
      for (int index = 0; index < this.__components.Length; ++index)
      {
        if (!this.__components[index].isDefined())
        {
          Asn1Type asn1Type = this.__instantiateTypeByIndex(index);
          if (asn1Type.__getDefaultInstance() != null)
          {
            this.__setComponentIsDefined(index);
            asn1Type.__setTypeValue(asn1Type.__getDefaultInstance());
          }
        }
      }
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal virtual void __setPerTerminateInfos(int encodingLength, int encodingStart)
    {
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal override void __write(Asn1TypePerWriter writer)
    {
      writer.__encodeConstructedType(this);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal override void __write(Asn1TypeXerWriter writer)
    {
      writer.__encodeConstructedType(this);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal override string __writeValue(Asn1TypeXerWriter writer)
    {
      return writer.__encodeConstructedValue(this);
    }

    public override object Clone()
    {
      Asn1ConstructedType asn1ConstructedType = (Asn1ConstructedType) this.MemberwiseClone();
      if (this.__components != null)
      {
        asn1ConstructedType.__components = new Asn1ConstructedType.ComponentInfo[this.__components.Length];
        for (int index = 0; index < this.__components.Length; ++index)
          asn1ConstructedType.__components[index] = (Asn1ConstructedType.ComponentInfo) this.__components[index].Clone();
      }
      asn1ConstructedType.__unknownExtensions = (IList) null;
      if (this.__unknownExtensions != null)
      {
        for (int index = 0; index < this.__unknownExtensions.Count; ++index)
        {
          byte[] numArray = (byte[]) this.__unknownExtensions[index];
          byte[] data = new byte[numArray.Length];
          Array.Copy((Array) numArray, 0, (Array) data, 0, numArray.Length);
          asn1ConstructedType.__addUnknownExtension(data);
        }
        return (object) asn1ConstructedType;
      }
      else
      {
        this.__unknownExtensions = (IList) null;
        return (object) asn1ConstructedType;
      }
    }

    public Asn1Type GetAsn1Type(int index)
    {
      return this.__components[index].getTypeInstance();
    }

    public override int GetHashCode()
    {
      if (this.__components == null)
        return 1;
      int num = 1;
      for (int index = 0; index < this.__components.Length; ++index)
      {
        if (this.__isComponentDefined(index))
        {
          Asn1Type componentTypeInstance = this.__getDefinedComponentTypeInstance(index);
          num = 31 * num + componentTypeInstance.GetHashCode();
        }
      }
      return num;
    }

    public override string GetPrintableValue(string indent, string newline)
    {
      if (this.__components == null)
        return "{}";
      string str = "  ";
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append('{');
      for (int index = 0; index < this.__components.Length; ++index)
      {
        Asn1Type asn1Type = this.GetAsn1Type(index);
        if (this.__isComponentDefined(index) && asn1Type != null)
        {
          if (stringBuilder.Length > 1)
            stringBuilder.Append(',');
          stringBuilder.Append(newline);
          stringBuilder.Append(indent);
          stringBuilder.Append(str);
          stringBuilder.Append(asn1Type.TypeName);
          stringBuilder.Append(' ');
          stringBuilder.Append(asn1Type.GetPrintableValue(indent + str, newline));
        }
      }
      if (this.__unknownExtensions != null)
      {
        for (int index = 0; index < this.__unknownExtensions.Count; ++index)
        {
          if (stringBuilder.Length > 1)
            stringBuilder.Append(',');
          stringBuilder.Append(newline);
          stringBuilder.Append(indent);
          stringBuilder.Append(str);
          stringBuilder.Append("unknownExtension");
          stringBuilder.Append(' ');
          stringBuilder.Append("'" + ByteArray.ByteArrayToHexString((byte[]) this.__unknownExtensions[index], "", -1, true) + "'H");
        }
      }
      stringBuilder.Append(newline);
      stringBuilder.Append(indent);
      stringBuilder.Append('}');
      return ((object) stringBuilder).ToString();
    }

    public virtual IList GetUnknownExtensions()
    {
      return this.__unknownExtensions;
    }

    public bool HasComponentDefaultValue(int index)
    {
      return this.__components[index].isTypeInstanceWithDefaultValue();
    }

    public virtual bool HasEqualValue(Asn1ConstructedType that)
    {
      if (that == null || that.Length != this.Length)
        return false;
      for (int index = 0; index < this.Length; ++index)
      {
        if (that.__isComponentDefined(index) && this.__isComponentDefined(index))
        {
          if (!this.__getDefinedComponentTypeInstance(index).Equals((object) that.__getDefinedComponentTypeInstance(index)))
            return false;
        }
        else if (!that.__isComponentDefined(index) && this.__isComponentDefined(index) || that.__isComponentDefined(index) && !this.__isComponentDefined(index))
          return false;
      }
      return true;
    }

    public virtual bool HasUnknownExtensions()
    {
      return this.__unknownExtensions != null;
    }

    public bool IsComponentDefault(int index)
    {
      return this.__components[index].isDefault();
    }

    public bool IsComponentInExtension(int index)
    {
      return this.__components[index].isInExtension();
    }

    public bool IsComponentOptional(int index)
    {
      return this.__components[index].isOptional();
    }

    public virtual bool IsExtensible()
    {
      return false;
    }

    public Asn1Type NewAsn1Type(int index)
    {
      this.__setComponentIsDefined(index);
      return this.__instantiateTypeByIndex(index);
    }

    public virtual void RemoveUnknownExtensions()
    {
      this.__unknownExtensions = (IList) null;
    }

    public override void ResetType()
    {
      this.__resetCommons();
      this.__nextComponentIndex = 0;
      this.SetIndefiniteLengthForm(false);
      this.__unknownExtensions = (IList) null;
      if (this.__getDefaultInstance() != null)
      {
        this.__setTypeValue(this.__getDefaultInstance());
      }
      else
      {
        if (this.__components == null)
          return;
        for (int index = 0; index < this.__components.Length; ++index)
        {
          if (this.GetAsn1Type(index) != null)
          {
            this.__components[index].ResetType();
            if (this.IsComponentOptional(index))
              this.__setComponentIsDefined(index, false);
          }
          else if (this.IsComponentDefault(index))
            this.__setComponentDefaultInstance(index);
        }
      }
    }

    public override void ResolveContent()
    {
      if (this.__components == null)
        return;
      for (int index = 0; index < this.__components.Length; ++index)
        this.__components[index].resolveContent();
    }

    public void SetAsn1Type(Asn1Type typeInstance, int index)
    {
      if (typeInstance == null)
      {
        this.__setComponentIsDefined(index, false);
        this.__setComponentTypeInstance(index, (Asn1Type) null);
      }
      else
      {
        this.__setComponentIsDefined(index);
        this.__setComponentTypeInstance(index, typeInstance);
      }
    }

    public override void Validate()
    {
      if (this.__components == null)
        return;
      for (int index = 0; index < this.__components.Length; ++index)
      {
        if (!this.__isComponentDefined(index) && !this.IsComponentOptional(index) && (!this.IsComponentDefault(index) && !this.IsComponentInExtension(index)))
        {
          this.__instantiateTypeByIndex(index);
          throw new Asn1ValidationException(15, this.GetAsn1Type(index).GetType().FullName + " in type <" + this.GetType().FullName + ">");
        }
        else
        {
          Asn1Type asn1Type = this.GetAsn1Type(index);
          if (asn1Type != null && this.__isComponentDefined(index))
            asn1Type.Validate();
        }
      }
    }

    [Serializable]
    protected class ComponentInfo : ICloneable
    {
      private int _extensionAdditionGroup = -1;
      private bool _isDefault = false;
      private bool _isDefined = false;
      private bool _isInExtension = false;
      private bool _isOptional = false;
      private bool _isXerUntagged = false;
      private bool _isXerUseUnion = false;
      private int _tagClass = -1;
      private int _tagValue = -1;
      private Asn1Type _typeInstance = (Asn1Type) null;
      private string _xerTag;

      protected internal virtual void cancelTypeInstance()
      {
        this._typeInstance = (Asn1Type) null;
        this._isDefined = false;
      }

      public virtual object Clone()
      {
        Asn1ConstructedType.ComponentInfo componentInfo = (Asn1ConstructedType.ComponentInfo) this.MemberwiseClone();
        if (this._typeInstance != null)
          componentInfo._typeInstance = (Asn1Type) this._typeInstance.Clone();
        return (object) componentInfo;
      }

      protected internal virtual int getExtensionAdditionGroup()
      {
        return this._extensionAdditionGroup;
      }

      protected internal virtual int getTagClass()
      {
        return this._tagClass;
      }

      protected internal virtual int getTagValue()
      {
        return this._tagValue;
      }

      protected internal virtual Asn1Type getTypeInstance()
      {
        return this._typeInstance;
      }

      protected internal virtual string getXerTag()
      {
        return this._xerTag;
      }

      protected internal virtual bool isDefault()
      {
        return this._isDefault;
      }

      protected internal virtual bool isDefined()
      {
        return this._isDefined;
      }

      protected internal virtual bool isInExtension()
      {
        return this._isInExtension;
      }

      protected internal virtual bool isOptional()
      {
        return this._isOptional;
      }

      protected internal virtual bool isTypeInstanceWithDefaultValue()
      {
        if (!this._isDefault)
          return false;
        if (this._typeInstance != null)
          return this._typeInstance.__hasDefaultInstance();
        else
          return true;
      }

      protected internal bool isXerUntagged()
      {
        return this._isXerUntagged;
      }

      protected internal bool isXerUseUnion()
      {
        return this._isXerUseUnion;
      }

      protected internal virtual void ResetType()
      {
        if (this._typeInstance == null)
          return;
        this._typeInstance.ResetType();
      }

      protected internal virtual void resolveContent()
      {
        if (!this._isDefined || this._typeInstance == null)
          return;
        this._typeInstance.ResolveContent();
      }

      protected internal virtual void setDefaultInstance()
      {
        this._typeInstance.__setTypeValue(this._typeInstance.__getDefaultInstance());
      }

      protected internal virtual void setDefined(bool b)
      {
        this._isDefined = b;
      }

      protected internal virtual void setExtensionAdditionGroup(int group)
      {
        this._extensionAdditionGroup = group;
      }

      protected internal virtual void setIsDefault()
      {
        this._isDefault = true;
      }

      protected internal virtual void setIsInExtension()
      {
        this._isInExtension = true;
      }

      protected internal virtual void setIsOptional()
      {
        this._isOptional = true;
      }

      protected internal void setIsXerUntagged()
      {
        this._isXerUntagged = true;
      }

      protected internal void setIsXerUseUnion()
      {
        this._isXerUseUnion = true;
      }

      protected internal virtual void setTag(int tagValue, int tagClass)
      {
        this._tagValue = tagValue;
        this._tagClass = tagClass;
      }

      protected internal virtual void setTypeInstance(Asn1Type typeInstance)
      {
        this._typeInstance = typeInstance;
      }

      protected internal virtual void setXerTag(string xerTag)
      {
        this._xerTag = xerTag;
      }
    }

    protected class ExtensionAdditionGroupInfo
    {
      private int _length = 0;
      private BitString _optDefDescriptor = (BitString) null;
      private int _optDefDescriptorSize = 0;

      protected internal virtual void cancelOptDefDescriptor()
      {
        this._optDefDescriptor = (BitString) null;
      }

      protected internal virtual int getLength()
      {
        return this._length;
      }

      protected internal virtual BitString getOptDefDescriptor()
      {
        return this._optDefDescriptor;
      }

      protected internal virtual int getOptDefDescriptorSize()
      {
        return this._optDefDescriptorSize;
      }

      protected internal virtual BitString newOptDefDescriptor()
      {
        this._optDefDescriptor = new BitString(this._optDefDescriptorSize);
        return this._optDefDescriptor;
      }

      protected internal virtual void setLength(int length)
      {
        this._length = length;
      }

      protected internal virtual void setOptDefDescriptorSize(int optDescriptorSize)
      {
        this._optDefDescriptorSize = optDescriptorSize;
      }
    }
  }
}
