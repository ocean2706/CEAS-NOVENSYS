// Decompiled with JetBrains decompiler
// Type: Novensys.ASN1.Type.Asn1EnumeratedType
// Assembly: Novensys.eCard.SDK, Version=1.1.56.0, Culture=neutral, PublicKeyToken=null
// MVID: 59F9E456-D3DA-4066-A4A4-692B516A775E
// Assembly location: C:\Program Files (x86)\CNAS\SIUI-SPITAL\Novensys.eCard.SDK.dll

using Novensys.ASN1;
using Novensys.ASN1.Util;
using System;
using System.ComponentModel;

namespace Novensys.ASN1.Type
{
  [Serializable]
  public class Asn1EnumeratedType : Asn1Type
  {
    private string __identifier;
    private int __index;
    private long __value;

    public int IntValue
    {
      get
      {
        return this.GetIntValue();
      }
      set
      {
        this.SetValue((long) value);
      }
    }

    public long LongValue
    {
      get
      {
        return this.GetLongValue();
      }
      set
      {
        this.SetValue(value);
      }
    }

    public override string PrintableValue
    {
      get
      {
        if (!this.IsUnknownExtension() || !this.IsExtensible())
          return this.GetIdentifier() ?? "<<invalid identifier>> (" + (object) this.__value + ")";
        return "unknownExtension -- value:" + (this.__identifier != null ? (object) this.__identifier : (object) this.__value.ToString()) + " / index:" + (string) (object) this.GetUnknownExtensionIndex() + " --";
      }
    }

    public override string TypeName
    {
      get
      {
        return "ENUMERATED";
      }
    }

    public Asn1EnumeratedType()
    {
      this.ResetType();
    }

    public Asn1EnumeratedType(long val)
    {
      this.SetValue(val);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal virtual string[] __getExtensionIdentifierSet()
    {
      return (string[]) null;
    }

    protected internal virtual long[] __getExtensionValueSet()
    {
      return (long[]) null;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal virtual string __getIdentifier(string[] idSet, string[] idExtensionSet)
    {
      int index = this.__getIndex();
      if (this.__index != -1)
      {
        if (this.__index < idSet.Length)
          return idSet[index];
        if (idExtensionSet != null && index < idExtensionSet.Length)
          return idExtensionSet[index];
      }
      return this.__identifier;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal virtual int __getIndex()
    {
      int rootValueSetSize = this.__getRootValueSetSize();
      if (this.__index == -1)
      {
        this.__index = this.__getIndexInRootValueSet();
        if (this.__index == -1)
        {
          int extensionValueSet = this.__getIndexInExtensionValueSet();
          if (extensionValueSet == -1)
            return -1;
          this.__index = extensionValueSet + rootValueSetSize;
        }
      }
      if (this.__index >= rootValueSetSize)
        return this.__index - rootValueSetSize;
      else
        return this.__index;
    }

    private int __getIndexInExtensionValueSet()
    {
      long[] extensionValueSet = this.__getExtensionValueSet();
      if (extensionValueSet != null)
      {
        for (int index = 0; index < extensionValueSet.Length; ++index)
        {
          if (this.__value == extensionValueSet[index])
            return index;
        }
      }
      return -1;
    }

    private int __getIndexInRootValueSet()
    {
      long[] rootValueSet = this.__getRootValueSet();
      if (rootValueSet != null)
      {
        for (int index = 0; index < rootValueSet.Length; ++index)
        {
          if (this.__value == rootValueSet[index])
            return index;
        }
      }
      return -1;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal virtual string[] __getRootIdentifierSet()
    {
      return (string[]) null;
    }

    protected internal virtual long[] __getRootValueSet()
    {
      return (long[]) null;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal virtual int __getRootValueSetSize()
    {
      long[] rootValueSet = this.__getRootValueSet();
      if (rootValueSet == null)
        return -1;
      else
        return rootValueSet.Length;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal override int __getUniversalTagNumber()
    {
      return 10;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal virtual string[] __getXerAdditionalIdentifierSet()
    {
      return (string[]) null;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal virtual string[] __getXerIdentifierSet()
    {
      return (string[]) null;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal override string __getXerTag()
    {
      return "ENUMERATED";
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal virtual bool __isValueOutOfRoot()
    {
      this.__getIndex();
      return this.__index >= this.__getRootValueSetSize();
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal virtual bool __isXerText()
    {
      return false;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal virtual bool __isXerUseNumber()
    {
      return false;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal virtual bool __isXerWhiteSpaceCollapse()
    {
      return false;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal virtual bool __isXerWhiteSpaceReplace()
    {
      return false;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal override void __read(Asn1TypePerReader reader)
    {
      reader.__decodeEnumeratedType(this);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal override void __read(Asn1TypeXerReader reader)
    {
      reader.__decodeEnumeratedType(this);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal override void __read(Asn1TypeBerReader reader, bool isExplicit, bool primitive, long len)
    {
      reader.__decodeEnumeratedType(this, isExplicit, primitive, len);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal override void __readValue(Asn1TypeXerReader reader, string text)
    {
      reader.__decodeEnumeratedValue(this, text);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal virtual void __setIndex(int index, bool inRootValueSet)
    {
      if (inRootValueSet)
      {
        this.__updateValueWithRootSetIndex(index);
        this.__index = index;
      }
      else
      {
        this.__updateValueWithExtensionSetIndex(index);
        this.__index = this.__getRootValueSetSize() + index;
      }
    }

    protected internal override void __setTypeValue(Asn1Type typeInstance)
    {
      if (typeInstance == null)
      {
        this.ResetType();
      }
      else
      {
        if (!(typeInstance is Asn1EnumeratedType))
          return;
        this.SetValue(((Asn1EnumeratedType) typeInstance).LongValue);
      }
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal virtual void __setValue(string identifier, string[] idSet, string[] idExtensionSet)
    {
      for (int index = 0; index < idSet.Length; ++index)
      {
        if (identifier.Equals(idSet[index]))
        {
          this.__value = this.__getRootValueSet()[index];
          this.__index = index;
          return;
        }
      }
      if (idExtensionSet != null)
      {
        for (int index = 0; index < idExtensionSet.Length; ++index)
        {
          if (identifier.Equals(idExtensionSet[index]))
          {
            this.__value = this.__getExtensionValueSet()[index];
            this.__index = index + this.__getRootValueSetSize();
            return;
          }
        }
      }
      if (!this.IsExtensible())
      {
        throw new Asn1Exception(25, "invalid identifier (" + identifier + ") in type <" + this.GetType().FullName + ">");
      }
      else
      {
        long num1 = Tools.maxLong(this.__getRootValueSet());
        long num2 = num1;
        if (this.__getExtensionValueSet() != null && this.__getExtensionValueSet().Length != 0)
          num2 = Tools.maxLong(this.__getExtensionValueSet());
        if (num2 > num1)
          num1 = num2;
        this.__index = this.__getExtensionValueSet() != null ? this.__getExtensionValueSet().Length + this.__getRootValueSetSize() : this.__getRootValueSetSize();
        this.__value = num1 + 1L;
        this.__identifier = identifier;
      }
    }

    private void __updateValueWithExtensionSetIndex(int index)
    {
      long[] extensionValueSet = this.__getExtensionValueSet();
      if (extensionValueSet == null)
        throw new Asn1Exception(99, "no extension value set in type <" + this.GetType().FullName + ">");
      if (index < 0)
        throw new Asn1Exception(30, "invalid extension index in type <" + this.GetType().FullName + ">");
      if (index > extensionValueSet.Length - 1)
        return;
      this.__value = extensionValueSet[index];
    }

    private void __updateValueWithRootSetIndex(int index)
    {
      long[] rootValueSet = this.__getRootValueSet();
      if (rootValueSet == null)
        throw new Asn1Exception(99, "no root value set in type <" + this.GetType().FullName + ">");
      if (index < 0 || index > rootValueSet.Length - 1)
        throw new Asn1Exception(30, "invalid root index in type <" + this.GetType().FullName + ">");
      this.__value = rootValueSet[index];
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal override void __write(Asn1TypeBerWriter writer)
    {
      writer.__encodeEnumeratedType(this, this.__getUniversalTagNumber(), 1);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal override void __write(Asn1TypePerWriter writer)
    {
      writer.__encodeEnumeratedType(this);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal override void __write(Asn1TypeXerWriter writer)
    {
      writer.__encodeEnumeratedType(this);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal override void __write(Asn1TypeBerWriter writer, int tagNumber, int tagClass)
    {
      writer.__encodeEnumeratedType(this, tagNumber, tagClass);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal override string __writeValue(Asn1TypeXerWriter writer)
    {
      return writer.__encodeEnumeratedValue(this);
    }

    public override bool Equals(object anObject)
    {
      return this == anObject || anObject is Asn1EnumeratedType && this.HasEqualValue((Asn1EnumeratedType) anObject);
    }

    public override int GetHashCode()
    {
      return (int) this.__value ^ (int) (this.__value >> 32);
    }

    public virtual string GetIdentifier()
    {
      return this.__getIdentifier(this.__getRootIdentifierSet(), this.__getExtensionIdentifierSet());
    }

    public virtual int GetIntValue()
    {
      return (int) this.__value;
    }

    public virtual long GetLongValue()
    {
      return this.__value;
    }

    public virtual string GetUnknownExtensionIdentifier()
    {
      if (!this.IsUnknownExtension())
        return (string) null;
      if (this.__identifier != null)
        return this.__identifier;
      else
        return "unknownExtension";
    }

    public virtual int GetUnknownExtensionIndex()
    {
      int rootValueSetSize = this.__getRootValueSetSize();
      if (this.__index == -1)
      {
        if (this.__getIndexInRootValueSet() != -1 || this.__getIndexInExtensionValueSet() != -1)
          return -1;
        if (this.__getExtensionValueSet() != null)
          return this.__getExtensionValueSet().Length;
        else
          return 0;
      }
      else
      {
        int num = rootValueSetSize;
        if (this.__getExtensionValueSet() != null)
          num += this.__getExtensionValueSet().Length;
        if (this.__index >= num)
          return this.__index - rootValueSetSize;
        else
          return -1;
      }
    }

    public virtual long GetUnknownExtensionValue()
    {
      if (this.IsUnknownExtension())
      {
        if (this.__index == -1)
          return this.__value;
        long[] extensionValueSet = this.__getExtensionValueSet();
        if (extensionValueSet != null)
          return extensionValueSet[extensionValueSet.Length - 1] + 1L;
      }
      return 0L;
    }

    public virtual bool HasEqualValue(Asn1EnumeratedType that)
    {
      if (that == null)
        return false;
      else
        return this.__value == that.LongValue;
    }

    public virtual bool IsExtensible()
    {
      return false;
    }

    public virtual bool IsUnknownExtension()
    {
      return this.GetUnknownExtensionIndex() != -1;
    }

    public override void ResetType()
    {
      this.__resetCommons();
      this.SetValue(0L);
      if (this.__getDefaultInstance() == null)
        return;
      this.__setTypeValue(this.__getDefaultInstance());
    }

    public virtual void SetValue(long val)
    {
      this.__value = val;
      this.__index = -1;
      this.__identifier = (string) null;
    }

    public virtual void SetValue(string identifier)
    {
      if (identifier == null)
        throw new ArgumentException("identifier should not be null.");
      this.__setValue(identifier, this.__getRootIdentifierSet(), this.__getExtensionIdentifierSet());
    }

    public override void Validate()
    {
      if (this.IsExtensible() || this.__getIndexInRootValueSet() != -1)
        return;
      throw new Asn1ValidationException(25, string.Concat(new object[4]
      {
        (object) this.__value,
        (object) " is not authorized in type <",
        (object) this.GetType().FullName,
        (object) ">"
      }));
    }
  }
}
