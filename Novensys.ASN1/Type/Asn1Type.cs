// Decompiled with JetBrains decompiler
// Type: Novensys.ASN1.Type.Asn1Type
// Assembly: Novensys.eCard.SDK, Version=1.1.56.0, Culture=neutral, PublicKeyToken=null
// MVID: 59F9E456-D3DA-4066-A4A4-692B516A775E
// Assembly location: C:\Program Files (x86)\CNAS\SIUI-SPITAL\Novensys.eCard.SDK.dll

using Novensys.ASN1.Util;
using System;
using System.Collections;
using System.ComponentModel;
using System.Text;

namespace Novensys.ASN1.Type
{
  [Serializable]
  public abstract class Asn1Type : ICloneable
  {
    protected Asn1Type __enclosingType = (Asn1Type) null;

    public virtual string PrintableValue
    {
      get
      {
        return (string) null;
      }
    }

    public virtual string ReferenceTypeName
    {
      get
      {
        return (string) null;
      }
    }

    public virtual string TypeName
    {
      get
      {
        return (string) null;
      }
    }

    protected Asn1Type()
    {
      this.__resetCommons();
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal virtual bool __containsXerAnyAttributesComponent()
    {
      return false;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal virtual bool __containsXerAnyElementComponent()
    {
      return false;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal virtual Asn1Type __getDefaultInstance()
    {
      return (Asn1Type) null;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal virtual Asn1Type __getMatchingInstance(int tagNumber, int tagClass)
    {
      if (tagNumber == this.__getUniversalTagNumber() && tagClass == 1)
        return this;
      else
        return (Asn1Type) null;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal virtual Asn1Type __getMatchingInstance(string xerTag, string xerNamespace)
    {
      if (!xerTag.Equals(this.__getXerTag()) || xerNamespace != null && xerNamespace.Length > 0 && xerNamespace != this.__getXerNamespaceUri())
        return (Asn1Type) null;
      else
        return this;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal virtual int __getPerCountBeforeTerminated(int length)
    {
      return -1;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected int __getPerCountBeforeTerminated(Asn1Type typeInstance, int length)
    {
      if (typeInstance != null)
        return typeInstance.__getPerCountBeforeTerminated(length);
      else
        return -1;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal virtual int __getPerCountDelta()
    {
      return 0;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal virtual int __getPerLength()
    {
      return -1;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal virtual int __getPerLengthSize()
    {
      return -1;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal virtual int __getTagClass()
    {
      return -1;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal virtual int __getTagNumber()
    {
      return -1;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal virtual int __getUniversalTagNumber()
    {
      return -1;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal virtual Asn1Type __getXerAnyElementComponent()
    {
      return (Asn1Type) null;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal virtual string __getXerControlNamespacePrefix()
    {
      return "asn1";
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal virtual string __getXerControlNamespaceUri()
    {
      return "urn:oid:2.1.5.2.0.1";
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal virtual string __getXerNamespacePrefix()
    {
      return (string) null;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal virtual string __getXerNamespaceUri()
    {
      return (string) null;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal virtual string __getXerTag()
    {
      return this.TypeName;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal virtual bool __hasDefaultInstance()
    {
      return false;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal virtual bool __hasMatchingComponent(string xerTag, string xerNamespace)
    {
      return false;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal virtual bool __isPerAlignRightToOctet()
    {
      return false;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal virtual bool __isPerConstraintExtensible()
    {
      return false;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal virtual bool __isPerCountBits()
    {
      return false;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal virtual bool __isPerCountOctets()
    {
      return false;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal virtual bool __isPerLengthIn()
    {
      return false;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal virtual bool __isPerTerminatedByCarrier()
    {
      return false;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal virtual bool __isPerTerminatedByEndOf()
    {
      return false;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal virtual bool __isXerAnyAttributes()
    {
      return false;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal virtual bool __isXerAnyElement()
    {
      return false;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal virtual bool __isXerAttribute()
    {
      return false;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal virtual bool __isXerDefaultForEmpty()
    {
      return false;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal virtual bool __isXerEmbedValues()
    {
      return false;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal virtual bool __isXerList()
    {
      return false;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal virtual bool __isXerUntagged()
    {
      return false;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal virtual bool __isXerUseQName()
    {
      return false;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal abstract void __read(Asn1TypePerReader reader);

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal abstract void __read(Asn1TypeXerReader reader);

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal abstract void __read(Asn1TypeBerReader reader, bool isExplicit, bool primitive, long len);

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal abstract void __readValue(Asn1TypeXerReader reader, string text);

    protected internal virtual void __resetCommons()
    {
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal void __setEnclosingType(Asn1Type enclosingType)
    {
      this.__enclosingType = enclosingType;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal abstract void __setTypeValue(Asn1Type typeInstance);

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal abstract void __write(Asn1TypeBerWriter writer);

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal abstract void __write(Asn1TypePerWriter writer);

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal abstract void __write(Asn1TypeXerWriter writer);

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal abstract void __write(Asn1TypeBerWriter writer, int tagNumber, int tagClass);

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal abstract string __writeValue(Asn1TypeXerWriter writer);

    public virtual void AddPiOrComment(PiOrComment piOrComment)
    {
    }

    public virtual object Clone()
    {
      return this.MemberwiseClone();
    }

    public Asn1Type GetEnclosingAsn1Type()
    {
      return this.__enclosingType;
    }

    public virtual IList GetPiOrCommentList()
    {
      return (IList) null;
    }

    public virtual string GetPrintableValue(string indent, string newline)
    {
      return this.PrintableValue;
    }

    public virtual string[] GetSmartValue()
    {
      return (string[]) null;
    }

    public virtual bool IsIndefiniteLengthForm()
    {
      return false;
    }

    public virtual void ResetType()
    {
      this.__resetCommons();
    }

    public virtual void ResolveContent()
    {
    }

    public virtual void SetIndefiniteLengthForm(bool indefiniteLengthForm)
    {
    }

    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("value ");
      stringBuilder.Append(this.TypeName);
      stringBuilder.Append(" ::= ");
      stringBuilder.Append(this.GetPrintableValue("", "\r\n"));
      return ((object) stringBuilder).ToString();
    }

    public virtual void Validate()
    {
    }
  }
}
