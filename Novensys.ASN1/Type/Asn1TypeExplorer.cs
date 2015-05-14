// Decompiled with JetBrains decompiler
// Type: Novensys.ASN1.Type.Asn1TypeExplorer
// Assembly: Novensys.eCard.SDK, Version=1.1.56.0, Culture=neutral, PublicKeyToken=null
// MVID: 59F9E456-D3DA-4066-A4A4-692B516A775E
// Assembly location: C:\Program Files (x86)\CNAS\SIUI-SPITAL\Novensys.eCard.SDK.dll

using Novensys.ASN1;
using System;
using System.Collections;
using System.Text;

namespace Novensys.ASN1.Type
{
  public class Asn1TypeExplorer
  {
    private static IEnumerator EMPTY_ENUMERATION = (IEnumerator) new Asn1TypeExplorer.EmptyEnumeration();
    private Asn1Type _currentNode;
    private static Exception _exception;
    private bool _move;
    private Asn1Type _root;
    private Stack _stack;
    private Asn1Type lastInstrospectedNode;
    private Asn1TypeExplorer.NodeInfos nodeInfos;
    private bool seeAllSyntaxNodes;

    public bool SeeAllSyntaxNodes
    {
      get
      {
        return this.seeAllSyntaxNodes;
      }
      set
      {
        this.seeAllSyntaxNodes = value;
      }
    }

    public Asn1Type TypeInstance
    {
      get
      {
        return this._root;
      }
      set
      {
        this.SetTypeInstance(value);
      }
    }

    public Asn1TypeExplorer()
    {
      this._move = false;
      this.seeAllSyntaxNodes = false;
      this._stack = new Stack();
      this.nodeInfos = new Asn1TypeExplorer.NodeInfos();
      Asn1TypeExplorer._exception = (Exception) null;
    }

    public Asn1TypeExplorer(Asn1Type type)
    {
      this._move = false;
      this.seeAllSyntaxNodes = false;
      this._stack = new Stack();
      this.nodeInfos = new Asn1TypeExplorer.NodeInfos();
      this.TypeInstance = type;
    }

    public Asn1TypeExplorer(Asn1Type type, bool seeAllSyntaxNodes)
      : this(type)
    {
      this.SeeAllSyntaxNodes = seeAllSyntaxNodes;
    }

    private static string buildDecoderErrorMessage(string info)
    {
      return "[decoder error = " + info + "]";
    }

    private static string buildDecoderErrorWrappedTypeMessage(string typeName)
    {
      return "[decoder error in " + typeName + "]";
    }

    private bool checkNodeIdentifier(string identifier)
    {
      if (!this.instrospectCurrentNode())
        return false;
      if ("*".Equals(identifier))
        return true;
      if (this.nodeInfos.typeName != null)
        return this.nodeInfos.typeName.Equals(identifier);
      else
        return this.nodeInfos.referenceType.Equals(identifier);
    }

    public virtual IEnumerator GetChildrenEnumeration()
    {
      this._currentNode = this.getEffectiveAsn1Type();
      if (this._currentNode is Asn1ConstructedType)
        return (IEnumerator) new Asn1TypeExplorer.ConstructedEnumeration(this, (Asn1ConstructedType) this._currentNode);
      if (this._currentNode is Asn1ConstructedOfType)
        return (IEnumerator) new Asn1TypeExplorer.ConstructedOfEnumeration(this, (Asn1ConstructedOfType) this._currentNode);
      if (this.isContainingType(this._currentNode))
        return (IEnumerator) new Asn1TypeExplorer.ContainingTypeEnumeration(this, this._currentNode);
      else
        return Asn1TypeExplorer.EMPTY_ENUMERATION;
    }

    private Asn1Type getEffectiveAsn1Type()
    {
      if (this._currentNode is Asn1TypeExplorer.PostDecodingComponentWrapper)
        return ((Asn1TypeExplorer.PostDecodingComponentWrapper) this._currentNode).getComponent();
      else
        return this._currentNode;
    }

    private Asn1Type getElementAt(Asn1ConstructedOfType typeInstance, int index)
    {
      try
      {
        return typeInstance.__getElementAt(index);
      }
      catch (Asn1Exception ex)
      {
        Asn1TypeExplorer._exception = (Exception) ex;
        return (Asn1Type) new Asn1TypeExplorer.SimulatedAsn1Type((string) null, (string) null, Asn1TypeExplorer.buildDecoderErrorMessage(ex.Message));
      }
    }

    public virtual Exception getException()
    {
      return Asn1TypeExplorer._exception;
    }

    private int getNbDefinedComponents(Asn1ConstructedType typeInstance)
    {
      int num = 0;
      for (int index = 0; index < typeInstance.Length; ++index)
      {
        if (this.seeAllSyntaxNodes || typeInstance.__isComponentDefined(index))
          ++num;
      }
      IList unknownExtensions = typeInstance.GetUnknownExtensions();
      if (unknownExtensions != null)
        num += unknownExtensions.Count;
      return num;
    }

    public virtual string GetNodeAsNameRef()
    {
      this.instrospectCurrentNode();
      StringBuilder stringBuilder = new StringBuilder();
      if (this.nodeInfos.typeName != null)
        stringBuilder.Append(this.nodeInfos.typeName).Append(" ");
      stringBuilder.Append(this.nodeInfos.referenceType);
      return ((object) stringBuilder).ToString();
    }

    public virtual string GetNodeAsString()
    {
      this.instrospectCurrentNode();
      StringBuilder stringBuilder = new StringBuilder();
      if (this.nodeInfos.typeName != null)
        stringBuilder.Append(this.nodeInfos.typeName);
      if (this.nodeInfos.referenceType != null)
      {
        if (this.nodeInfos.typeName != null)
          stringBuilder.Append(" ");
        stringBuilder.Append(this.nodeInfos.referenceType);
      }
      if (this.nodeInfos.val != null)
      {
        if (stringBuilder.Length > 0)
          stringBuilder.Append(" = ");
        stringBuilder.Append(this.nodeInfos.val);
      }
      return ((object) stringBuilder).ToString();
    }

    public string GetNodeReferenceTypeName()
    {
      this.instrospectCurrentNode();
      return this.nodeInfos.referenceType;
    }

    public string[] GetNodeSmartValue()
    {
      this.instrospectCurrentNode();
      return this.nodeInfos.smartVal;
    }

    public Asn1Type GetNodeType()
    {
      this.instrospectCurrentNode();
      return this._currentNode;
    }

    public string GetNodeTypeName()
    {
      this.instrospectCurrentNode();
      return this.nodeInfos.typeName;
    }

    public string GetNodeTypeValue()
    {
      this.instrospectCurrentNode();
      return this.nodeInfos.val;
    }

    private Asn1Type GetRoot()
    {
      return this._root;
    }

    private Asn1Type GetTypeValue(Asn1Type typeInstance)
    {
      return (!(typeInstance is Asn1OpenType) ? (!(typeInstance is Asn1OctetStringType) ? (!(typeInstance is Asn1BitStringType) ? (Asn1Type) null : ((Asn1BitStringType) typeInstance).GetTypeValue()) : ((Asn1OctetStringType) typeInstance).GetTypeValue()) : ((Asn1OpenType) typeInstance).GetTypeValue()) ?? (Asn1Type) new Asn1TypeExplorer.SimulatedAsn1Type((string) null, (string) null, typeInstance.PrintableValue);
    }

    internal static Asn1Type getVisibleComponent(Asn1ConstructedType constructedTypeInstance, int index, bool anyComponent)
    {
      if (anyComponent)
        return constructedTypeInstance.__getInstantiatedComponentTypeInstance(index);
      if (constructedTypeInstance.__isComponentDefined(index))
        return constructedTypeInstance.__getDefinedComponentTypeInstance(index);
      else
        return (Asn1Type) null;
    }

    public virtual bool GotoFirstChild()
    {
      this._move = false;
      this._currentNode = this.getEffectiveAsn1Type();
      if (this._currentNode is Asn1SequenceType || this._currentNode is Asn1ChoiceType || this._currentNode is Asn1SetType)
      {
        Asn1ConstructedType constructedTypeInstance = (Asn1ConstructedType) this._currentNode;
        int num;
        for (num = 0; !this._move && num < constructedTypeInstance.Length; ++num)
        {
          Asn1Type visibleComponent = Asn1TypeExplorer.getVisibleComponent(constructedTypeInstance, num, this.seeAllSyntaxNodes);
          if (visibleComponent != null)
          {
            this._stack.Push((object) new Asn1TypeExplorer.StackItem(this._currentNode, num));
            this._currentNode = visibleComponent;
            this._move = true;
          }
        }
        IList unknownExtensions = constructedTypeInstance.GetUnknownExtensions();
        if (unknownExtensions != null)
        {
          for (; !this._move && num < constructedTypeInstance.Length + unknownExtensions.Count; ++num)
          {
            this._stack.Push((object) new Asn1TypeExplorer.StackItem(this._currentNode, num));
            this._currentNode = (Asn1Type) new Asn1TypeExplorer.SimulatedUnknownExtensionOpenType((byte[]) unknownExtensions[num - constructedTypeInstance.Length]);
            this._move = true;
          }
        }
      }
      else if (this._currentNode is Asn1ConstructedOfType)
      {
        Asn1ConstructedOfType typeInstance = (Asn1ConstructedOfType) this._currentNode;
        bool flag = false;
        if (this.seeAllSyntaxNodes && typeInstance.Count == 0)
        {
          typeInstance.__getMatchingElement();
          flag = true;
        }
        if (typeInstance.Count > 0)
        {
          this._stack.Push((object) new Asn1TypeExplorer.StackItem(this._currentNode, 0));
          this._currentNode = this.getElementAt(typeInstance, 0);
          this._move = true;
        }
        if (flag)
          typeInstance.Clear();
      }
      else if (this.isContainingType(this._currentNode))
      {
        Asn1Type typeValue = this.GetTypeValue(this._currentNode);
        if (typeValue != null)
        {
          this._stack.Push((object) new Asn1TypeExplorer.StackItem(this._currentNode, 0));
          this._currentNode = typeValue;
          this._move = true;
        }
      }
      else
        this._move = false;
      return this._move;
    }

    public virtual bool GotoNextSibling()
    {
      this._move = false;
      if (this._stack.Count != 0)
      {
        Asn1TypeExplorer.StackItem stackItem = (Asn1TypeExplorer.StackItem) this._stack.Peek();
        Asn1Type asn1Type = stackItem.getAsn1Type();
        if (asn1Type is Asn1SequenceType || asn1Type is Asn1ChoiceType || asn1Type is Asn1SetType)
        {
          Asn1ConstructedType constructedTypeInstance = (Asn1ConstructedType) asn1Type;
          int num;
          for (num = stackItem.getIndexOfCurrentChild() + 1; !this._move && num < constructedTypeInstance.Length; ++num)
          {
            Asn1Type visibleComponent = Asn1TypeExplorer.getVisibleComponent(constructedTypeInstance, num, this.seeAllSyntaxNodes);
            if (visibleComponent != null)
            {
              this._currentNode = visibleComponent;
              stackItem.setIndexOfCurrentChild(num);
              this._move = true;
            }
          }
          IList unknownExtensions = constructedTypeInstance.GetUnknownExtensions();
          if (unknownExtensions != null)
          {
            for (; !this._move && num < constructedTypeInstance.Length + unknownExtensions.Count; ++num)
            {
              this._currentNode = (Asn1Type) new Asn1TypeExplorer.SimulatedUnknownExtensionOpenType((byte[]) unknownExtensions[num - constructedTypeInstance.Length]);
              stackItem.setIndexOfCurrentChild(num);
              this._move = true;
            }
          }
        }
        else if (asn1Type is Asn1ConstructedOfType)
        {
          Asn1ConstructedOfType typeInstance = (Asn1ConstructedOfType) asn1Type;
          bool flag = false;
          if (this.seeAllSyntaxNodes && typeInstance.Count == 0)
          {
            typeInstance.__getMatchingElement();
            flag = true;
          }
          if (stackItem.getIndexOfCurrentChild() + 1 < typeInstance.Count)
          {
            this._currentNode = this.getElementAt(typeInstance, stackItem.getIndexOfCurrentChild() + 1);
            stackItem.setIndexOfCurrentChild(stackItem.getIndexOfCurrentChild() + 1);
            this._move = true;
          }
          if (flag)
            typeInstance.Clear();
        }
        else
          this._move = false;
      }
      else
        this._move = false;
      return this._move;
    }

    public virtual bool GotoNode(string path)
    {
      this._currentNode = this.getEffectiveAsn1Type();
      Asn1Type asn1Type = this._currentNode;
      this._move = false;
      if (path == null)
        return false;
      string[] strArray = path.Split('.');
      int index1 = 0;
      if (strArray.Length == 0 || !this.checkNodeIdentifier(strArray[index1]))
        return false;
      this._move = true;
      int index2 = index1 + 1;
      while (index2 < strArray.Length && this._move)
      {
        string identifier = strArray[index2];
        ++index2;
        if (this.GotoFirstChild())
        {
          while (this._move && !this.checkNodeIdentifier(identifier))
            this.GotoNextSibling();
        }
      }
      if (index2 < strArray.Length || !this._move)
      {
        this._move = false;
        this._currentNode = asn1Type;
      }
      return this._move;
    }

    public virtual bool GotoParent()
    {
      if (this._stack.Count != 0)
      {
        this._currentNode = ((Asn1TypeExplorer.StackItem) this._stack.Pop()).getAsn1Type();
        this._move = true;
      }
      else
        this._move = false;
      return this._move;
    }

    public virtual bool GotoRoot()
    {
      this._currentNode = this.GetRoot();
      this._stack.Clear();
      this._move = this._currentNode != null;
      return this._move;
    }

    private bool instrospectCurrentNode()
    {
      this.resetNodeInfos();
      if (this._currentNode == null)
        return false;
      if (this._currentNode != this.lastInstrospectedNode)
      {
        this.nodeInfos.typeName = this._currentNode.TypeName;
        if (this.isContainingType(this._currentNode))
        {
          this.nodeInfos.referenceType = this._currentNode.ReferenceTypeName;
        }
        else
        {
          this.nodeInfos.referenceType = this._currentNode.ReferenceTypeName;
          this.nodeInfos.val = this._currentNode.PrintableValue;
          this.nodeInfos.smartVal = this._currentNode.GetSmartValue();
        }
        this.lastInstrospectedNode = this._currentNode;
      }
      return true;
    }

    private bool isContainingType(Asn1Type typeInstance)
    {
      if (typeInstance is Asn1OpenType)
        return true;
      if (typeInstance is Asn1OctetStringType)
        return ((Asn1OctetStringType) typeInstance).__isContainingType();
      else
        return typeInstance is Asn1BitStringType && ((Asn1BitStringType) typeInstance).__isContainingType();
    }

    public virtual bool IsCurrentNode()
    {
      return this._move;
    }

    public virtual bool IsLeaf()
    {
      this._currentNode = this.getEffectiveAsn1Type();
      return this.isLeafImpl(this._currentNode);
    }

    private bool isLeafImpl(Asn1Type typeInstance)
    {
      return !(typeInstance is Asn1ConstructedType) && !(typeInstance is Asn1ConstructedOfType) && !this.isContainingType(this._currentNode);
    }

    private void resetNodeInfos()
    {
      this.nodeInfos.typeName = (string) null;
      this.nodeInfos.referenceType = (string) null;
      this.nodeInfos.val = (string) null;
      this.lastInstrospectedNode = (Asn1Type) null;
    }

    public virtual void SetTypeInstance(Asn1Type type)
    {
      this._root = type;
      this._currentNode = (Asn1Type) null;
      Asn1TypeExplorer._exception = (Exception) null;
      this.resetNodeInfos();
      this.GotoRoot();
    }

    private class ConstructedEnumeration : IEnumerator
    {
      private int _definedChildCount = 0;
      private int _nbDefinedComponentsRead = 0;
      private Asn1TypeExplorer _enclosingInstance;
      private int _index;
      private Asn1ConstructedType _typeInstance;

      public object Current
      {
        get
        {
          if (this._index < 0 || this._index >= this._typeInstance.Length)
            throw new InvalidOperationException("Enumerator is out of range.");
          else
            return (object) this._typeInstance.__getDefinedComponentTypeInstance(this._index);
        }
      }

      internal ConstructedEnumeration(Asn1TypeExplorer enclosingInstance, Asn1ConstructedType typeInstance)
      {
        this._enclosingInstance = enclosingInstance;
        this._typeInstance = typeInstance;
        this._definedChildCount = this._enclosingInstance.getNbDefinedComponents(typeInstance);
      }

      public bool MoveNext()
      {
        if (this._nbDefinedComponentsRead < this._definedChildCount)
        {
          Asn1Type asn1Type = (Asn1Type) null;
          if (this._index < 0)
            this._index = 0;
          for (; this._index < this._typeInstance.Length && asn1Type == null; ++this._index)
            asn1Type = Asn1TypeExplorer.getVisibleComponent(this._typeInstance, this._index, this._enclosingInstance.SeeAllSyntaxNodes);
          IList unknownExtensions = this._typeInstance.GetUnknownExtensions();
          if (unknownExtensions != null)
          {
            for (; this._index < this._typeInstance.Length + unknownExtensions.Count && asn1Type == null; ++this._index)
              asn1Type = (Asn1Type) new Asn1TypeExplorer.SimulatedUnknownExtensionOpenType((byte[]) unknownExtensions[this._index - this._typeInstance.Length]);
          }
          if (asn1Type != null)
          {
            ++this._nbDefinedComponentsRead;
            --this._index;
            return true;
          }
        }
        return false;
      }

      public void Reset()
      {
        this._index = -1;
      }
    }

    private class ConstructedOfEnumeration : IEnumerator
    {
      private int _childCount = 0;
      private Asn1Type _component = (Asn1Type) null;
      private bool childrenAreLeaf = true;
      private Asn1TypeExplorer _enclosingInstance;
      private int _index;
      private bool _postDecoding;
      private Asn1ConstructedOfType _typeInstance;

      public object Current
      {
        get
        {
          if (this._index < 0 || this._index >= this._childCount)
            throw new InvalidOperationException("Enumerator is out of range.");
          if (this._postDecoding && !this.childrenAreLeaf)
            return (object) new Asn1TypeExplorer.PostDecodingComponentWrapper(this._typeInstance, this._index, this._component.TypeName, this._component.ReferenceTypeName);
          else
            return (object) this._enclosingInstance.getElementAt(this._typeInstance, this._index);
        }
      }

      internal ConstructedOfEnumeration(Asn1TypeExplorer enclosingInstance, Asn1ConstructedOfType typeInstance)
      {
        this._enclosingInstance = enclosingInstance;
        this._typeInstance = typeInstance;
        this._postDecoding = this._typeInstance.__isPostDecoding();
        this._childCount = this._typeInstance.Count;
        if (!this._postDecoding)
          return;
        try
        {
          this._component = typeInstance.NewAsn1Type();
          this.childrenAreLeaf = this._enclosingInstance.isLeafImpl(this._component);
        }
        catch (Exception ex)
        {
          throw new SystemException();
        }
      }

      public bool MoveNext()
      {
        ++this._index;
        return this._index < this._childCount;
      }

      public void Reset()
      {
        this._index = -1;
      }
    }

    private class ContainingTypeEnumeration : IEnumerator
    {
      private Asn1Type _element;
      private bool _hasMoreElements;

      public object Current
      {
        get
        {
          return (object) this._element;
        }
      }

      public ContainingTypeEnumeration(Asn1TypeExplorer enclosingInstance, Asn1Type typeInstance)
      {
        this._element = enclosingInstance.GetTypeValue(typeInstance);
        this._hasMoreElements = this._element != null;
      }

      public bool MoveNext()
      {
        if (!this._hasMoreElements)
          return false;
        this._hasMoreElements = false;
        return true;
      }

      public void Reset()
      {
      }
    }

    private class EmptyEnumeration : IEnumerator
    {
      public object Current
      {
        get
        {
          return (object) null;
        }
      }

      public bool MoveNext()
      {
        return false;
      }

      public void Reset()
      {
      }
    }

    private class NodeInfos
    {
      public string referenceType;
      public string[] smartVal;
      public string typeName;
      public string val;
    }

    private class SimulatedAsn1Type : Asn1Type
    {
      private string _referenceType;
      private string _typeName;
      private string _value;

      public override string PrintableValue
      {
        get
        {
          return this._value;
        }
      }

      public override string ReferenceTypeName
      {
        get
        {
          return this._referenceType;
        }
      }

      public override string TypeName
      {
        get
        {
          return this._typeName;
        }
      }

      internal SimulatedAsn1Type(string typeName, string referenceType, string val)
      {
        this._typeName = typeName;
        this._referenceType = referenceType;
        this._value = val;
      }

      protected internal override Asn1Type __getDefaultInstance()
      {
        this.panic();
        return (Asn1Type) null;
      }

      protected internal override bool __hasDefaultInstance()
      {
        this.panic();
        return false;
      }

      protected internal override void __read(Asn1TypePerReader reader)
      {
        this.panic();
      }

      protected internal override void __read(Asn1TypeXerReader reader)
      {
        this.panic();
      }

      protected internal override void __read(Asn1TypeBerReader reader, bool isExplicit, bool primitive, long len)
      {
        this.panic();
      }

      protected internal override void __readValue(Asn1TypeXerReader reader, string text)
      {
        this.panic();
      }

      protected internal override void __setTypeValue(Asn1Type type)
      {
        this.panic();
      }

      protected internal override void __write(Asn1TypeBerWriter writer)
      {
        this.panic();
      }

      protected internal override void __write(Asn1TypePerWriter writer)
      {
        this.panic();
      }

      protected internal override void __write(Asn1TypeXerWriter writer)
      {
        this.panic();
      }

      protected internal override void __write(Asn1TypeBerWriter writer, int tagNumber, int tagClass)
      {
        this.panic();
      }

      protected internal override string __writeValue(Asn1TypeXerWriter writer)
      {
        this.panic();
        return (string) null;
      }

      public override bool Equals(object anObject)
      {
        this.panic();
        return false;
      }

      public override int GetHashCode()
      {
        this.panic();
        return 0;
      }

      public override string[] GetSmartValue()
      {
        return (string[]) null;
      }

      private void panic()
      {
        throw new SystemException("PostDecodingComponentWrapper");
      }

      public override void ResetType()
      {
        this.panic();
      }

      protected virtual void setSimulatedValue(string val)
      {
        this._value = val;
      }

      public override void Validate()
      {
        this.panic();
      }
    }

    private class PostDecodingComponentWrapper : Asn1TypeExplorer.SimulatedAsn1Type
    {
      private int _componentIndex;
      private Asn1ConstructedOfType _parent;

      internal PostDecodingComponentWrapper(Asn1ConstructedOfType typeInstance, int index, string typeName, string referenceType)
        : base(typeName, referenceType, (string) null)
      {
        this._parent = typeInstance;
        this._componentIndex = index;
      }

      internal virtual Asn1Type getComponent()
      {
        try
        {
          return this._parent.__getElementAt(this._componentIndex);
        }
        catch (Asn1Exception ex)
        {
          Asn1TypeExplorer._exception = (Exception) ex;
          this.setSimulatedValue(Asn1TypeExplorer.buildDecoderErrorMessage(ex.Message));
          return (Asn1Type) this;
        }
      }
    }

    private class SimulatedErrorType : Asn1SequenceType
    {
      private Asn1Type containedType;

      public override string ReferenceTypeName
      {
        get
        {
          if (this.containedType.TypeName != null)
            return Asn1TypeExplorer.buildDecoderErrorWrappedTypeMessage(this.containedType.TypeName);
          else
            return Asn1TypeExplorer.buildDecoderErrorWrappedTypeMessage(this.containedType.ReferenceTypeName);
        }
      }

      public override string TypeName
      {
        get
        {
          return (string) null;
        }
      }

      public SimulatedErrorType(Asn1Type type, Exception ex)
      {
        this.containedType = type;
        this.__setComponentTypeInstance(0, this.containedType);
        this.__setComponentIsDefined(0);
        this.__setComponentTypeInstance(1, (Asn1Type) new Asn1TypeExplorer.SimulatedAsn1Type((string) null, (string) null, Asn1TypeExplorer.buildDecoderErrorMessage(ex.Message)));
        this.__setComponentIsDefined(1);
      }

      protected internal override int __getNbRootComponents()
      {
        return 2;
      }

      protected internal override void __initialize()
      {
        this.__initializeComponents(2);
      }
    }

    private class SimulatedUnknownExtensionOpenType : Asn1OpenType
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
          return "unknownExtension";
        }
      }

      public SimulatedUnknownExtensionOpenType(byte[] data)
        : base(data)
      {
      }
    }

    internal class StackItem
    {
      private int indexOfCurrentChild;
      private Asn1Type type;

      public StackItem(Asn1Type type, int indexOfCurrentChild)
      {
        this.type = type;
        this.indexOfCurrentChild = indexOfCurrentChild;
      }

      public virtual Asn1Type getAsn1Type()
      {
        return this.type;
      }

      public virtual int getIndexOfCurrentChild()
      {
        return this.indexOfCurrentChild;
      }

      public virtual void setIndexOfCurrentChild(int val)
      {
        this.indexOfCurrentChild = val;
      }
    }
  }
}
