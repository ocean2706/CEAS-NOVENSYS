// Decompiled with JetBrains decompiler
// Type: Novensys.eCard.SDK.ASN1.EHCDG11.DG11
// Assembly: Novensys.eCard.SDK, Version=1.1.56.0, Culture=neutral, PublicKeyToken=null
// MVID: 59F9E456-D3DA-4066-A4A4-692B516A775E
// Assembly location: C:\Program Files (x86)\CNAS\SIUI-SPITAL\Novensys.eCard.SDK.dll

using Novensys.ASN1.Type;
using System;
using System.ComponentModel;

namespace Novensys.eCard.SDK.ASN1.EHCDG11
{
  [Serializable]
  public class DG11 : Asn1SequenceType
  {
    [EditorBrowsable(EditorBrowsableState.Never)]
    public override string TypeName
    {
      get
      {
        return "DG11";
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
      this.__initializeComponents(0);
    }

    public void SetValue(DG11 typeInstance)
    {
      this.__setTypeValue((Asn1Type) typeInstance);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal override Asn1Type __instantiateTypeByIndex(int index)
    {
      return (Asn1Type) null;
    }

    public override bool Equals(object anObject)
    {
      if (!(anObject is DG11))
        return false;
      else
        return base.Equals(anObject);
    }

    public override int GetHashCode()
    {
      return base.GetHashCode();
    }
  }
}
