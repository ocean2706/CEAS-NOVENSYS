// Decompiled with JetBrains decompiler
// Type: Novensys.ASN1.Type.Asn1InstanceOfType
// Assembly: Novensys.eCard.SDK, Version=1.1.56.0, Culture=neutral, PublicKeyToken=null
// MVID: 59F9E456-D3DA-4066-A4A4-692B516A775E
// Assembly location: C:\Program Files (x86)\CNAS\SIUI-SPITAL\Novensys.eCard.SDK.dll

using System;
using System.ComponentModel;

namespace Novensys.ASN1.Type
{
  [Serializable]
  public class Asn1InstanceOfType : Asn1SequenceType
  {
    public override string TypeName
    {
      get
      {
        return "INSTANCE OF";
      }
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal override int __getUniversalTagNumber()
    {
      return 8;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal override string __getXerTag()
    {
      return "SEQUENCE";
    }

    public override bool Equals(object anObject)
    {
      return anObject is Asn1InstanceOfType && this.Equals(anObject);
    }

    public override int GetHashCode()
    {
      return base.GetHashCode();
    }
  }
}
