// Decompiled with JetBrains decompiler
// Type: Novensys.ASN1.Type.Asn1RelativeOIDType
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
  public class Asn1RelativeOIDType : Asn1Type
  {
    private byte[] __value;

    public int[] IntArrayValue
    {
      get
      {
        return this.GetIntArrayValue();
      }
      set
      {
        this.SetValue(value);
      }
    }

    public long[] LongArrayValue
    {
      get
      {
        return this.GetLongArrayValue();
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
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append('{');
        if (this.__value != null && this.__value.Length > 0)
        {
          BigInteger[] arcsFromEncoding = Asn1ObjectIdentifierType.GetOIDBigIntegerArcsFromEncoding(this.__value, true);
          for (int index = 0; index < arcsFromEncoding.Length; ++index)
          {
            if (index > 0)
              stringBuilder.Append(' ');
            stringBuilder.Append(((object) arcsFromEncoding[index]).ToString());
          }
        }
        stringBuilder.Append('}');
        return ((object) stringBuilder).ToString();
      }
    }

    public string StringValue
    {
      get
      {
        return this.GetStringValue();
      }
      set
      {
        this.SetValue(value);
      }
    }

    public override string TypeName
    {
      get
      {
        return "RELATIVE-OID";
      }
    }

    public Asn1RelativeOIDType()
    {
      this.ResetType();
    }

    public Asn1RelativeOIDType(int[] arcs)
    {
      this.SetValue(arcs);
    }

    public Asn1RelativeOIDType(long[] arcs)
    {
      this.SetValue(arcs);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal override int __getUniversalTagNumber()
    {
      return 13;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal override string __getXerTag()
    {
      return "RELATIVE_OID";
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal override void __read(Asn1TypePerReader reader)
    {
      reader.__decodeRelativeOIDType(this);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal override void __read(Asn1TypeXerReader reader)
    {
      reader.__decodeRelativeOIDType(this);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal override void __read(Asn1TypeBerReader reader, bool isExplicit, bool primitive, long len)
    {
      reader.__decodeRelativeOIDType(this, isExplicit, primitive, len);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal override void __readValue(Asn1TypeXerReader reader, string text)
    {
      reader.__decodeRelativeOIDValue(this, text);
    }

    protected internal override void __setTypeValue(Asn1Type typeInstance)
    {
      if (typeInstance == null)
      {
        this.ResetType();
      }
      else
      {
        if (!(typeInstance is Asn1RelativeOIDType))
          return;
        this.SetValue(((Asn1RelativeOIDType) typeInstance).GetByteArrayValue());
      }
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal override void __write(Asn1TypeBerWriter writer)
    {
      writer.__encodeRelativeOIDType(this, this.__getUniversalTagNumber(), 1);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal override void __write(Asn1TypePerWriter writer)
    {
      writer.__encodeRelativeOIDType(this);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal override void __write(Asn1TypeXerWriter writer)
    {
      writer.__encodeRelativeOIDType(this);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal override void __write(Asn1TypeBerWriter writer, int tagNumber, int tagClass)
    {
      writer.__encodeRelativeOIDType(this, tagNumber, tagClass);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal override string __writeValue(Asn1TypeXerWriter writer)
    {
      return writer.__encodeRelativeOIDValue(this);
    }

    public override bool Equals(object anObject)
    {
      return this == anObject || anObject is Asn1RelativeOIDType && this.HasEqualValue((Asn1RelativeOIDType) anObject);
    }

    public BigInteger[] GetBigIntegerValue()
    {
      if (this.__value == null)
        return (BigInteger[]) null;
      else
        return Asn1ObjectIdentifierType.GetOIDBigIntegerArcsFromEncoding(this.__value, true);
    }

    public byte[] GetByteArrayValue()
    {
      return this.__value;
    }

    public override int GetHashCode()
    {
      if (this.__value == null)
        return 0;
      int num = 1;
      for (int index = 0; index < this.__value.Length; ++index)
        num = 31 * num + (int) this.__value[index];
      return num;
    }

    public virtual int[] GetIntArrayValue()
    {
      long[] arcsFromEncoding = Asn1ObjectIdentifierType.GetOIDLongArcsFromEncoding(this.__value, true);
      if (arcsFromEncoding == null)
        return (int[]) null;
      int[] numArray = new int[arcsFromEncoding.Length];
      for (int index = 0; index < numArray.Length; ++index)
        numArray[index] = (int) arcsFromEncoding[index];
      return numArray;
    }

    public virtual long[] GetLongArrayValue()
    {
      return Asn1ObjectIdentifierType.GetOIDLongArcsFromEncoding(this.__value, true);
    }

    public virtual string GetStringValue()
    {
      BigInteger[] bigIntegerValue = this.GetBigIntegerValue();
      StringBuilder stringBuilder = new StringBuilder();
      for (int index = 0; index < bigIntegerValue.Length; ++index)
      {
        if (index > 0)
          stringBuilder.Append('.');
        stringBuilder.Append(((object) bigIntegerValue[index]).ToString());
      }
      return ((object) stringBuilder).ToString();
    }

    public virtual bool HasEqualValue(Asn1RelativeOIDType that)
    {
      if (that == null)
        return false;
      if (this.__value == null)
        return that.GetByteArrayValue() == null;
      if (that.GetByteArrayValue() == null || that.GetByteArrayValue().Length != this.__value.Length)
        return false;
      for (int index = 0; index < this.__value.Length; ++index)
      {
        if ((int) that.GetByteArrayValue()[index] != (int) this.__value[index])
          return false;
      }
      return true;
    }

    public override void ResetType()
    {
      this.__resetCommons();
      this.SetValue((byte[]) null);
      if (this.__getDefaultInstance() == null)
        return;
      this.__setTypeValue(this.__getDefaultInstance());
    }

    public void SetValue(string value)
    {
      if (value == null)
      {
        this.__value = (byte[]) null;
      }
      else
      {
        string[] strArray = value.Split('.');
        int length1 = strArray.Length;
        if (length1 == 0)
        {
          this.__value = new byte[0];
        }
        else
        {
          BigInteger[] arcs = new BigInteger[length1];
          int index = 0;
          while (index < strArray.Length)
          {
            string str1 = (string) null;
            string str2 = strArray[index];
            int length2 = str2.IndexOf('(');
            int num = str2.IndexOf(')');
            string str3;
            if (length2 == -1 && num == -1)
              str3 = str2;
            else if (length2 != -1 && num > length2)
            {
              str3 = str2.Substring(0, length2);
              str1 = str2.Substring(length2 + 1, num - (length2 + 1));
            }
            else
            {
              ++index;
              continue;
            }
            try
            {
              arcs[index] = str1 == null ? new BigInteger(str3) : new BigInteger(str1);
            }
            catch (Exception ex)
            {
            }
            ++index;
          }
          this.SetValue(arcs);
        }
      }
    }

    public void SetValue(BigInteger[] arcs)
    {
      this.__value = Asn1ObjectIdentifierType.GetEncodingFromOIDBigIntegerArcs(arcs, true);
    }

    public void SetValue(byte[] encoding)
    {
      this.__value = encoding;
    }

    public virtual void SetValue(int[] arcs)
    {
      if (arcs == null)
      {
        this.__value = (byte[]) null;
      }
      else
      {
        long[] arcs1 = new long[arcs.Length];
        for (int index = 0; index < arcs1.Length; ++index)
          arcs1[index] = (long) arcs[index];
        this.SetValue(arcs1);
      }
    }

    public virtual void SetValue(long[] arcs)
    {
      if (arcs == null)
        this.__value = (byte[]) null;
      else
        this.__value = Asn1ObjectIdentifierType.GetEncodingFromOIDLongArcs(arcs, true);
    }

    public override void Validate()
    {
      if (this.__value == null || this.__value.Length == 0)
        throw new Asn1ValidationException(28, "in type <" + this.GetType().FullName + ">");
    }
  }
}
