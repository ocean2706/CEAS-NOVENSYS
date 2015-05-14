// Decompiled with JetBrains decompiler
// Type: Novensys.ASN1.Type.Asn1StringType
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
  public abstract class Asn1StringType : Asn1Type
  {
    protected string __value;

    public override string PrintableValue
    {
      get
      {
        if (this.__value == null)
          return "\"\"";
        else
          return "\"" + this.__value + "\"";
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

    public Asn1StringType()
    {
      this.ResetType();
    }

    public Asn1StringType(string str)
    {
      this.__value = str;
    }

    protected internal string __getCharsDefnPrintableValue(bool quadrupleOrTuple)
    {
      if (this.__value == null || this.__value.Length == 0)
        return "\"\"";
      StringBuilder stringBuilder = new StringBuilder();
      bool flag1 = false;
      bool flag2 = false;
      bool flag3 = false;
      char[] chArray = this.__value.ToCharArray();
      for (int index = 0; index < chArray.Length; ++index)
      {
        char c = chArray[index];
        if (char.IsControl(c))
        {
          if (flag1)
          {
            stringBuilder.Append('"');
            stringBuilder.Append(',');
          }
          else if (flag2)
            stringBuilder.Append(',');
          if (quadrupleOrTuple)
          {
            int num1 = 0;
            int num2 = 0;
            int num3 = (int) c / 256;
            int num4 = (int) c % 256;
            stringBuilder.Append('{').Append(num1).Append(',').Append(num2).Append(',').Append(num3).Append(',').Append(num4).Append('}');
          }
          else
          {
            int num1 = (int) c / 16;
            int num2 = (int) c % 16;
            stringBuilder.Append('{').Append(num1).Append(',').Append(num2).Append('}');
          }
          flag3 = true;
          flag1 = false;
          flag2 = true;
        }
        else
        {
          if (index == 0)
            stringBuilder.Append('"');
          if (flag2)
          {
            stringBuilder.Append(',');
            stringBuilder.Append('"');
          }
          stringBuilder.Append(c);
          flag1 = true;
          flag2 = false;
          if (index == chArray.Length - 1)
            stringBuilder.Append('"');
        }
      }
      if (flag3)
      {
        stringBuilder.Insert(0, new char[1]
        {
          '{'
        });
        stringBuilder.Append('}');
      }
      return ((object) stringBuilder).ToString();
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal virtual string __getXerControlCharacterNamespacePrefix()
    {
      return (string) null;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal virtual string __getXerControlCharacterNamespaceUri()
    {
      return (string) null;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal override string __getXerTag()
    {
      return this.TypeName;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal virtual bool __isCDATAValue()
    {
      return false;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal virtual bool __isXerBase64()
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
    protected internal override void __read(Asn1TypeXerReader reader)
    {
      reader.__decodeStringType(this);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal override void __read(Asn1TypeBerReader reader, bool isExplicit, bool primitive, long len)
    {
      reader.__decodeStringType(this, isExplicit, primitive, len);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal override void __readValue(Asn1TypeXerReader reader, string text)
    {
      reader.__decodeStringValue(this, text);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal override void __write(Asn1TypeBerWriter writer)
    {
      writer.__encodeStringType(this, this.__getUniversalTagNumber(), 1, this.IsIndefiniteLengthForm());
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal override void __write(Asn1TypeXerWriter writer)
    {
      writer.__encodeStringType(this);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal override void __write(Asn1TypeBerWriter writer, int tagNumber, int tagClass)
    {
      writer.__encodeStringType(this, tagNumber, tagClass, this.IsIndefiniteLengthForm());
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal override string __writeValue(Asn1TypeXerWriter writer)
    {
      return writer.__encodeStringValue(this);
    }

    public virtual byte[] GetByteArrayValue()
    {
      return Tools.getByteArrayFrom8BitsChars(this.__value);
    }

    public virtual byte[] GetByteArrayValue(Encoding encoding)
    {
      if (this.__value != null)
        return encoding.GetBytes(this.__value);
      else
        return (byte[]) null;
    }

    public override int GetHashCode()
    {
      if (this.__value == null)
        return 0;
      else
        return this.__value.GetHashCode();
    }

    public virtual long GetLowerSize()
    {
      return 0L;
    }

    public virtual string GetStringValue()
    {
      return this.__value;
    }

    public virtual long GetUpperSize()
    {
      return -1L;
    }

    public virtual bool HasEqualValue(Asn1StringType that)
    {
      if (that == null)
        return false;
      if (this.__value == null)
        return that.StringValue == null;
      else
        return this.__value.Equals(that.StringValue);
    }

    public override void ResetType()
    {
      this.__resetCommons();
      this.__value = (string) null;
      this.SetIndefiniteLengthForm(false);
      if (this.__getDefaultInstance() == null)
        return;
      this.__setTypeValue(this.__getDefaultInstance());
    }

    public virtual void SetCDATAValue(string str)
    {
      this.__value = str;
    }

    public virtual void SetValue(byte[] data)
    {
      if (data != null)
      {
        char[] chArray = new char[data.Length];
        for (int index = 0; index < chArray.Length; ++index)
          chArray[index] = (char) data[index];
        this.__value = new string(chArray);
      }
      else
        this.__value = (string) null;
    }

    public virtual void SetValue(string str)
    {
      this.__value = str;
    }

    public virtual void SetValue(byte[] data, Encoding encoding)
    {
      if (data != null)
        this.__value = encoding.GetString(data, 0, data.Length);
      else
        this.__value = (string) null;
    }

    public override void Validate()
    {
      if (this.__isPerConstraintExtensible())
        return;
      int num = this.__value != null ? this.__value.Length : 0;
      if ((long) num < this.GetLowerSize())
      {
        throw new Asn1ValidationException(19, (string) (object) num + (object) " < " + (string) (object) this.GetLowerSize() + " in type <" + this.GetType().FullName + ">");
      }
      else
      {
        if (this.GetUpperSize() == -1L || (long) num <= this.GetUpperSize())
          return;
        throw new Asn1ValidationException(20, (string) (object) num + (object) " > " + (string) (object) this.GetUpperSize() + " in type <" + this.GetType().FullName + ">");
      }
    }
  }
}
