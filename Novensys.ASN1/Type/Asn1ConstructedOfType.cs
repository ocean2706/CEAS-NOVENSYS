// Decompiled with JetBrains decompiler
// Type: Novensys.ASN1.Type.Asn1ConstructedOfType
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
  public abstract class Asn1ConstructedOfType : Asn1Type
  {
    private bool __elementsAdded = false;
    private IDecoder __postDecoder = (IDecoder) null;
    private IEncoder __preEncoder = (IEncoder) null;
    protected IList __typeInstances = (IList) new ArrayList();

    public int Count
    {
      get
      {
        return this.__typeInstances.Count;
      }
    }

    public virtual Asn1Type this[int index]
    {
      get
      {
        if (index < 0 || index >= this.Count)
          throw new ArgumentOutOfRangeException();
        if (this.__typeInstances == null)
          return (Asn1Type) null;
        else
          return this.__getElementAt(index);
      }
      set
      {
        if (index < 0 || index >= this.Count)
          throw new ArgumentOutOfRangeException();
        if (value == null)
          return;
        if (this.__preEncoder == null)
          this.__preEncoder = this.__getPreEncoder();
        if (this.__preEncoder != null)
        {
          this.__preEncoder.Encode(value);
          this.__typeInstances[index] = (object) this.__preEncoder.Data;
        }
        else
          this.__typeInstances[index] = (object) value;
      }
    }

    public Asn1ConstructedOfType()
    {
      this.__initialize();
    }

    protected internal virtual void __addAsn1Type(Asn1Type typeInstance, Asn1Type element)
    {
      element.__setTypeValue(typeInstance);
      this.AddAsn1Type(element);
    }

    protected internal virtual void __addNewElementInstance(Asn1Type typeInstance)
    {
      if (!this.__elementsAdded)
      {
        if (this.__typeInstances.Count != 0)
          this.__typeInstances.Clear();
        this.__elementsAdded = true;
      }
      this.__typeInstances.Add((object) typeInstance);
      if (typeInstance == null)
        return;
      typeInstance.__setEnclosingType((Asn1Type) this);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal virtual Asn1Type __getElementAt(int index)
    {
      Asn1Type type = (Asn1Type) null;
      if (index < this.__typeInstances.Count)
      {
        object obj = this.__typeInstances[index];
        if (obj is Asn1Type)
          return (Asn1Type) obj;
        try
        {
          type = this.NewAsn1Type();
          if (this.__postDecoder == null)
            this.__getPostDecoder().Decode((byte[]) obj, type);
          else
            this.__postDecoder.Decode((byte[]) obj, type);
          type.__setEnclosingType((Asn1Type) this);
          return type;
        }
        catch (Asn1ValidationException ex)
        {
          throw ex;
        }
        catch (Exception ex)
        {
          string str = "internal exception at index '" + (object) (this.__postDecoder == null ? this.__getPostDecoder().UsedBytes() : this.__postDecoder.UsedBytes()) + "' in (" + ByteArray.ByteArrayToHexString((byte[]) obj, ".", -1) + ") : " + ex.Message;
          throw new Asn1Exception(16, "given element type <" + (object) type.GetType().FullName + "> for SEQUENCE OF / SET OF type <" + this.GetType().FullName + "> with decoder " + (string) (object) this.__postDecoder + " [" + str + "]");
        }
      }
      else
        throw new IndexOutOfRangeException((string) (object) index + (object) " is greater than " + (string) (object) (this.__typeInstances.Count - 1) + " for SEQUENCE OF / SET OF type <" + this.GetType().FullName + ">");
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal virtual Asn1Type __getMatchingElement()
    {
      return this.__instantiateType();
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal virtual Asn1Type __getMatchingElement(int tagNumber, int tagClass)
    {
      Asn1Type asn1Type = this.__instantiateType();
      int tagNumber1 = asn1Type.__getTagNumber();
      if (tagNumber1 == -1)
        return asn1Type.__getMatchingInstance(tagNumber, tagClass);
      if (tagNumber1 == tagNumber && asn1Type.__getTagClass() == tagClass)
        return asn1Type;
      else
        return (Asn1Type) null;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal virtual Asn1Type __getMatchingElement(string xerTag, string xerNamespace)
    {
      Asn1Type typeInstance = this.NewAsn1Type();
      if (typeInstance.__isXerUntagged())
      {
        if (!typeInstance.__hasMatchingComponent(xerTag, xerNamespace))
          return (Asn1Type) null;
        this.__addNewElementInstance(typeInstance);
        return typeInstance;
      }
      else
      {
        if (typeInstance.__getXerTag().Length != 0 && typeInstance.__getMatchingInstance(xerTag, xerNamespace) == null && !this.__containsXerAnyElementComponent())
          return (Asn1Type) null;
        this.__addNewElementInstance(typeInstance);
        return typeInstance;
      }
    }

    protected internal virtual IDecoder __getPostDecoder()
    {
      return (IDecoder) null;
    }

    protected internal virtual IEncoder __getPreEncoder()
    {
      return (IEncoder) null;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal virtual IDecoder __getTypePostDecoder()
    {
      return this.__postDecoder;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal virtual string[] __getXerAnyAttributesNamespaceRestrictionExcept()
    {
      return (string[]) null;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal virtual string[] __getXerAnyAttributesNamespaceRestrictionFrom()
    {
      return (string[]) null;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal override bool __hasMatchingComponent(string xerTag, string xerNamespace)
    {
      Asn1Type asn1Type = this.NewAsn1Type();
      if (asn1Type.__isXerUntagged())
        return asn1Type.__hasMatchingComponent(xerTag, xerNamespace);
      else
        return asn1Type.__getMatchingInstance(xerTag, xerNamespace) != null;
    }

    protected internal virtual void __initialize()
    {
    }

    protected internal virtual Asn1Type __instantiateType()
    {
      return (Asn1Type) null;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal virtual bool __isPostDecoding()
    {
      return false;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal virtual bool __isValueOutOfRoot()
    {
      return this.GetLowerSize() != 0L && (long) this.Count < this.GetLowerSize() || this.GetUpperSize() != -1L && this.GetUpperSize() < (long) this.Count;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal virtual bool __isXerAnyAttributesNamespaceRestrictionExceptAbsent()
    {
      return false;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal virtual bool __isXerAnyAttributesNamespaceRestrictionFromAbsent()
    {
      return false;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal override void __read(Asn1TypePerReader reader)
    {
      reader.__decodeConstructedOfType(this);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal override void __read(Asn1TypeXerReader reader)
    {
      reader.__decodeConstructedOfType(this);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal override void __read(Asn1TypeBerReader reader, bool isExplicit, bool primitive, long len)
    {
      reader.__decodeConstructedOfType(this, isExplicit, primitive, len);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal override void __readValue(Asn1TypeXerReader reader, string text)
    {
      reader.__decodeConstructedOfValue(this, text);
    }

    protected internal virtual void __setConstructedOfTypeValue(Asn1ConstructedOfType type)
    {
      if (type == null)
      {
        this.ResetType();
      }
      else
      {
        for (int index = 0; index < type.Count; ++index)
        {
          object obj = type.GetAsn1TypeList()[index];
          if (obj is Asn1Type)
          {
            try
            {
              if (index >= this.Count)
                this.__instantiateType();
              this.__getElementAt(index).__setTypeValue((Asn1Type) obj);
            }
            catch (Asn1Exception ex)
            {
            }
          }
          else if (index >= this.Count)
            this.__typeInstances.Add(obj);
          else
            this.__typeInstances[index] = obj;
        }
        if (this.__getDefaultInstance() != null && type == this.__getDefaultInstance())
          this.__elementsAdded = false;
      }
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal virtual void __setTypePostDecoder(IDecoder reader)
    {
      this.__postDecoder = reader;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal virtual void __setTypePreEncoder(IEncoder writer)
    {
      this.__preEncoder = writer;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal override void __write(Asn1TypePerWriter writer)
    {
      writer.__encodeConstructedOfType(this);
    }

    public virtual void AddAsn1Type(Asn1Type element)
    {
      if (this.__preEncoder == null)
        this.__preEncoder = this.__getPreEncoder();
      if (this.__preEncoder != null)
      {
        this.__preEncoder.Encode(element);
        if (!this.__elementsAdded)
        {
          if (this.__typeInstances.Count != 0)
            this.__typeInstances.Clear();
          this.__elementsAdded = true;
        }
        this.__typeInstances.Add((object) this.__preEncoder.Data);
      }
      else
        this.__addNewElementInstance(element);
    }

    public void Clear()
    {
      if (this.__typeInstances == null)
        return;
      this.__typeInstances.Clear();
    }

    public override object Clone()
    {
      Asn1ConstructedOfType constructedOfType = (Asn1ConstructedOfType) this.MemberwiseClone();
      if (this.__typeInstances != null)
      {
        constructedOfType.__typeInstances = (IList) ((ArrayList) this.__typeInstances).Clone();
        for (int index = 0; index < this.Count; ++index)
        {
          object obj = this.__typeInstances[index];
          if (obj is Asn1Type)
          {
            constructedOfType.__typeInstances[index] = ((Asn1Type) obj).Clone();
          }
          else
          {
            byte[] numArray1 = (byte[]) obj;
            byte[] numArray2 = new byte[numArray1.Length];
            Array.Copy((Array) numArray1, 0, (Array) numArray2, 0, numArray1.Length);
            constructedOfType.__typeInstances[index] = (object) numArray2;
          }
        }
      }
      return (object) constructedOfType;
    }

    [Obsolete]
    public virtual IList GetAsn1TypeElements()
    {
      return this.__typeInstances;
    }

    public virtual IList GetAsn1TypeList()
    {
      return this.__typeInstances;
    }

    public override int GetHashCode()
    {
      int num = 1;
      for (int index1 = 0; index1 < this.Count; ++index1)
      {
        object obj = this.__typeInstances[index1];
        if (obj is Asn1Type)
        {
          num = 31 * num + ((Asn1Type) obj).GetHashCode();
        }
        else
        {
          byte[] numArray = (byte[]) obj;
          int index2 = 0;
          while (index2 < numArray.Length)
          {
            num = 31 * num + (int) numArray[index2];
            ++index1;
          }
        }
      }
      return num;
    }

    public virtual long GetLowerSize()
    {
      return 0L;
    }

    public override string GetPrintableValue(string indent, string newline)
    {
      if (this.__typeInstances == null)
        return "{}";
      string str = "  ";
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append('{');
      for (int index = 0; index < this.__typeInstances.Count; ++index)
      {
        if (index > 0)
          stringBuilder.Append(',');
        stringBuilder.Append(newline);
        stringBuilder.Append(indent);
        stringBuilder.Append(str);
        try
        {
          Asn1Type elementAt = this.__getElementAt(index);
          if (elementAt.TypeName != null)
          {
            stringBuilder.Append(elementAt.TypeName);
            stringBuilder.Append(' ');
          }
          stringBuilder.Append(elementAt.GetPrintableValue(indent + str, newline));
        }
        catch (Asn1Exception ex)
        {
          stringBuilder.Append(ex.Message);
        }
      }
      stringBuilder.Append(newline);
      stringBuilder.Append(indent);
      stringBuilder.Append('}');
      return ((object) stringBuilder).ToString();
    }

    public virtual long GetUpperSize()
    {
      return -1L;
    }

    public virtual bool HasEqualValue(Asn1ConstructedOfType that)
    {
      if (that == null || that.Count != this.Count)
        return false;
      for (int index = 0; index < this.Count; ++index)
      {
        try
        {
          Asn1Type elementAt = this.__getElementAt(index);
          if (!that.__getElementAt(index).Equals((object) elementAt))
            return false;
        }
        catch (Asn1Exception ex)
        {
          return false;
        }
      }
      return true;
    }

    public virtual Asn1Type NewAsn1Type()
    {
      return (Asn1Type) null;
    }

    public override void ResetType()
    {
      this.__resetCommons();
      this.__typeInstances = (IList) new ArrayList();
      this.__setTypePreEncoder((IEncoder) null);
      this.__setTypePostDecoder((IDecoder) null);
      this.SetIndefiniteLengthForm(false);
      if (this.__getDefaultInstance() == null)
        return;
      this.__setTypeValue(this.__getDefaultInstance());
    }

    public override void ResolveContent()
    {
      if (this.__typeInstances == null)
        return;
      for (int index = 0; index < this.__typeInstances.Count; ++index)
      {
        if (this.__typeInstances[index] is Asn1Type)
          ((Asn1Type) this.__typeInstances[index]).ResolveContent();
      }
    }

    public virtual void SetAsn1TypeList(IList elements)
    {
      this.__typeInstances = elements;
    }

    public override void Validate()
    {
      if (!this.__isPerConstraintExtensible())
      {
        if ((long) this.Count < this.GetLowerSize())
          throw new Asn1ValidationException(19, (string) (object) this.Count + (object) " < " + (string) (object) this.GetLowerSize() + " in type <" + this.GetType().FullName + ">");
        else if (this.GetUpperSize() != -1L && (long) this.Count > this.GetUpperSize())
          throw new Asn1ValidationException(20, (string) (object) this.Count + (object) " > " + (string) (object) this.GetUpperSize() + " in type <" + this.GetType().FullName + ">");
      }
      for (int index = 0; index < this.Count; ++index)
      {
        object obj = this.__typeInstances[index];
        if (obj is Asn1Type)
          ((Asn1Type) obj).Validate();
      }
    }
  }
}
