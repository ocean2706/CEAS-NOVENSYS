// Decompiled with JetBrains decompiler
// Type: Novensys.ASN1.Type.Asn1UniversalStringType
// Assembly: Novensys.eCard.SDK, Version=1.1.56.0, Culture=neutral, PublicKeyToken=null
// MVID: 59F9E456-D3DA-4066-A4A4-692B516A775E
// Assembly location: C:\Program Files (x86)\CNAS\SIUI-SPITAL\Novensys.eCard.SDK.dll

using System;
using System.ComponentModel;
using System.Text;

namespace Novensys.ASN1.Type
{
  [Serializable]
  public class Asn1UniversalStringType : Asn1KnownMultiplierStringType
  {
    public override string PrintableValue
    {
      get
      {
        return this.__getCharsDefnPrintableValue(true);
      }
    }

    public override string TypeName
    {
      get
      {
        return "UniversalString";
      }
    }

    public Asn1UniversalStringType()
    {
    }

    public Asn1UniversalStringType(string str)
      : base(str)
    {
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal override int __getBaseAlphabetAlignedNbBitPerChar()
    {
      return 32;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal override long __getBaseAlphabetLowerBound()
    {
      return 0L;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal override int __getBaseAlphabetUnalignedNbBitPerChar()
    {
      return 32;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal override long __getBaseAlphabetUpperBound()
    {
      return (long) uint.MaxValue;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal override int __getUniversalTagNumber()
    {
      return 28;
    }

    protected internal override void __setTypeValue(Asn1Type typeInstance)
    {
      if (typeInstance == null)
      {
        this.ResetType();
      }
      else
      {
        if (!(typeInstance is Asn1UniversalStringType))
          return;
        this.SetValue(((Asn1StringType) typeInstance).GetStringValue());
      }
    }

    public override bool Equals(object anObject)
    {
      return this == anObject || anObject is Asn1UniversalStringType && this.HasEqualValue((Asn1StringType) anObject);
    }

    public override byte[] GetByteArrayValue()
    {
      byte[] byteArrayValue = base.GetByteArrayValue((Encoding) new UnicodeEncoding(true, false));
      if (byteArrayValue == null)
        return (byte[]) null;
      byte[] numArray = new byte[byteArrayValue.Length * 2];
      int index = 0;
      while (index < byteArrayValue.Length)
      {
        numArray[2 * index] = (byte) 0;
        numArray[2 * index + 1] = (byte) 0;
        numArray[2 * index + 2] = byteArrayValue[index];
        numArray[2 * index + 3] = byteArrayValue[index + 1];
        index += 2;
      }
      return numArray;
    }

    public override int GetHashCode()
    {
      return base.GetHashCode();
    }

    public override void SetValue(byte[] data)
    {
      if (data == null)
      {
        this.SetValue((byte[]) null);
      }
      else
      {
        byte[] data1 = new byte[data.Length / 2];
        int index = 0;
        while (index < data.Length / 2)
        {
          data1[index] = data[2 * index + 2];
          data1[index + 1] = data[2 * index + 3];
          index += 2;
        }
        base.SetValue(data1, (Encoding) new UnicodeEncoding(true, false));
      }
    }
  }
}
