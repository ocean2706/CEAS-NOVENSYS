// Decompiled with JetBrains decompiler
// Type: Novensys.ASN1.Type.Asn1ObjectIdentifierType
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
  public class Asn1ObjectIdentifierType : Asn1Type
  {
    private Asn1ObjectDescriptorType __objectDescriptor;
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

    public Asn1ObjectDescriptorType ObjectDescriptor
    {
      get
      {
        return this.__objectDescriptor;
      }
      set
      {
        this.__objectDescriptor = value;
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
          BigInteger[] arcsFromEncoding = Asn1ObjectIdentifierType.GetOIDBigIntegerArcsFromEncoding(this.__value, false);
          if (arcsFromEncoding != null && arcsFromEncoding.Length > 0)
          {
            int num1 = -1;
            int num2 = -1;
            int length = arcsFromEncoding.Length;
            if (length >= 1)
              num1 = arcsFromEncoding[0].GetIntValue();
            if (length >= 2 && arcsFromEncoding[1].Signum() >= 0 && arcsFromEncoding[1] <= new BigInteger((long) int.MaxValue))
              num2 = arcsFromEncoding[1].GetIntValue();
            string str1;
            switch (num1)
            {
              case 0:
                str1 = length < 2 || num2 != 5 ? "itu-t(0)" : "itu-r(0)";
                break;
              case 1:
                str1 = "iso(1)";
                break;
              case 2:
                str1 = "joint-iso-itu-t(2)";
                break;
              default:
                str1 = num1.ToString();
                break;
            }
            stringBuilder.Append(str1);
            stringBuilder.Append(' ');
            string str2;
            switch (num1)
            {
              case 0:
                switch (num2)
                {
                  case 0:
                    str2 = "recommendation(0)";
                    break;
                  case 1:
                    str2 = "question(1)";
                    break;
                  case 2:
                    str2 = "administration(2)";
                    break;
                  case 3:
                    str2 = "network-operator(3)";
                    break;
                  case 4:
                    str2 = "identified-organization(4)";
                    break;
                  case 5:
                    str2 = "r-recommendation(5)";
                    break;
                  default:
                    str2 = ((object) arcsFromEncoding[1]).ToString();
                    break;
                }
                break;
              case 1:
                switch (num2)
                {
                  case 0:
                    str2 = "standard(0)";
                    break;
                  case 1:
                    str2 = "registration-authority(1)";
                    break;
                  case 2:
                    str2 = "member-body(2)";
                    break;
                  case 3:
                    str2 = "identified-organization(3)";
                    break;
                  default:
                    str2 = ((object) arcsFromEncoding[1]).ToString();
                    break;
                }
                break;
              default:
                str2 = ((object) arcsFromEncoding[1]).ToString();
                break;
            }
            stringBuilder.Append(str2);
            if (arcsFromEncoding.Length >= 3)
            {
              stringBuilder.Append(' ');
              string str3;
              if (num1 == 0 && num2 == 0)
              {
                int intValue = arcsFromEncoding[2].GetIntValue();
                str3 = intValue < 1 || intValue > 26 ? ((object) arcsFromEncoding[2]).ToString() : ((char) (97 + intValue - 1)).ToString() + "(" + intValue.ToString() + ")";
              }
              else
                str3 = ((object) arcsFromEncoding[2]).ToString();
              stringBuilder.Append(str3);
              for (int index = 3; index < length; ++index)
              {
                stringBuilder.Append(' ');
                stringBuilder.Append(((object) arcsFromEncoding[index]).ToString());
              }
            }
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
        return "OBJECT IDENTIFIER";
      }
    }

    public Asn1ObjectIdentifierType()
    {
      this.ResetType();
    }

    public Asn1ObjectIdentifierType(int[] arcs)
    {
      this.SetValue(arcs);
    }

    public Asn1ObjectIdentifierType(long[] arcs)
    {
      this.SetValue(arcs);
    }

    public Asn1ObjectIdentifierType(Asn1ObjectIdentifierType oid, int[] arcs)
    {
      if (oid == null)
        throw new ArgumentException("oid should not be null.");
      int[] arcs1 = (int[]) null;
      if (arcs == null)
      {
        this.__setTypeValue((Asn1Type) oid);
      }
      else
      {
        int[] intArrayValue = oid.IntArrayValue;
        arcs1 = new int[intArrayValue.Length + arcs.Length];
        for (int index = 0; index < intArrayValue.Length; ++index)
          arcs1[index] = intArrayValue[index];
        for (int index = 0; index < arcs.Length; ++index)
          arcs1[index + intArrayValue.Length] = arcs[index];
      }
      this.SetValue(arcs1);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal override int __getUniversalTagNumber()
    {
      return 6;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal override string __getXerTag()
    {
      return "OBJECT_IDENTIFIER";
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal override void __read(Asn1TypePerReader reader)
    {
      reader.__decodeObjectIdentifierType(this);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal override void __read(Asn1TypeXerReader reader)
    {
      reader.__decodeObjectIdentifierType(this);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal override void __read(Asn1TypeBerReader reader, bool isExplicit, bool primitive, long len)
    {
      reader.__decodeObjectIdentifierType(this, isExplicit, primitive, len);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal override void __readValue(Asn1TypeXerReader reader, string text)
    {
      reader.__decodeObjectIdentifierValue(this, text);
    }

    protected internal override void __setTypeValue(Asn1Type typeInstance)
    {
      if (typeInstance == null)
      {
        this.ResetType();
      }
      else
      {
        if (!(typeInstance is Asn1ObjectIdentifierType))
          return;
        this.SetValue(((Asn1ObjectIdentifierType) typeInstance).GetByteArrayValue());
      }
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal override void __write(Asn1TypeBerWriter writer)
    {
      writer.__encodeObjectIdentifierType(this, this.__getUniversalTagNumber(), 1);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal override void __write(Asn1TypePerWriter writer)
    {
      writer.__encodeObjectIdentifierType(this);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal override void __write(Asn1TypeXerWriter writer)
    {
      writer.__encodeObjectIdentifierType(this);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal override void __write(Asn1TypeBerWriter writer, int tagNumber, int tagClass)
    {
      writer.__encodeObjectIdentifierType(this, tagNumber, tagClass);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected internal override string __writeValue(Asn1TypeXerWriter writer)
    {
      return writer.__encodeObjectIdentifierValue(this);
    }

    public override bool Equals(object anObject)
    {
      return this == anObject || anObject is Asn1ObjectIdentifierType && this.HasEqualValue((Asn1ObjectIdentifierType) anObject);
    }

    public BigInteger[] GetBigIntegerValue()
    {
      if (this.__value == null)
        return (BigInteger[]) null;
      else
        return Asn1ObjectIdentifierType.GetOIDBigIntegerArcsFromEncoding(this.__value, false);
    }

    public byte[] GetByteArrayValue()
    {
      return this.__value;
    }

    public static byte[] GetEncodingFromOIDBigIntegerArcs(BigInteger[] arcs, bool isRelative)
    {
      if (arcs == null)
        return (byte[]) null;
      if (arcs.Length == 0)
        return new byte[0];
      DynamicByteArray dynamicByteArray = new DynamicByteArray(32);
      int index = 0;
      if (!isRelative)
      {
        if (arcs.Length < 2)
          return new byte[0];
        if (arcs[0] == null || arcs[1] == null)
          return new byte[0];
        int intValue = arcs[0].GetIntValue();
        byte[] b;
        if (arcs[1] <= new BigInteger((long) int.MaxValue))
        {
          int num = arcs[1] != null ? arcs[1].GetIntValue() : 0;
          b = Tools.get7BitsArrayFrom8BitsArray(new BigInteger((long) (intValue * 40 + num)).GetBytes());
        }
        else
          b = Tools.get7BitsArrayFrom8BitsArray((new BigInteger((long) (intValue * 40)) + arcs[1]).GetBytes());
        dynamicByteArray.addBytes(b);
        index = 2;
      }
      for (; index < arcs.Length; ++index)
      {
        if (arcs[index] != null)
        {
          byte[] b = Tools.get7BitsArrayFrom8BitsArray(arcs[index].GetBytes());
          dynamicByteArray.addBytes(b);
        }
      }
      return dynamicByteArray.toByteArray();
    }

    public static byte[] GetEncodingFromOIDLongArcs(long[] arcs, bool isRelative)
    {
      if (arcs == null)
        return (byte[]) null;
      if (arcs.Length == 0)
        return new byte[0];
      DynamicByteArray dynamicByteArray = new DynamicByteArray(32);
      int index = 0;
      if (!isRelative)
      {
        if (arcs.Length < 2)
          return new byte[0];
        byte[] b = Tools.get7BitsArrayFromLong((long) ((int) arcs[0] * 40 + (int) arcs[1]));
        dynamicByteArray.addBytes(b);
        index = 2;
      }
      for (; index < arcs.Length; ++index)
      {
        byte[] b = Tools.get7BitsArrayFromLong(arcs[index]);
        if (b != null)
          dynamicByteArray.addBytes(b);
      }
      return dynamicByteArray.toByteArray();
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
      long[] arcsFromEncoding = Asn1ObjectIdentifierType.GetOIDLongArcsFromEncoding(this.__value, false);
      if (arcsFromEncoding == null)
        return (int[]) null;
      int[] numArray = new int[arcsFromEncoding.Length];
      for (int index = 0; index < numArray.Length; ++index)
        numArray[index] = (int) arcsFromEncoding[index];
      return numArray;
    }

    public virtual long[] GetLongArrayValue()
    {
      return Asn1ObjectIdentifierType.GetOIDLongArcsFromEncoding(this.__value, false);
    }

    public static BigInteger[] GetOIDBigIntegerArcsFromEncoding(byte[] encoding, bool isRelative)
    {
      if (encoding == null || encoding.Length == 0)
        return (BigInteger[]) null;
      IList list = (IList) new ArrayList();
      int index1 = 0;
      int length = 1;
      for (int index2 = 0; index2 < encoding.Length; ++index2)
      {
        if (((int) encoding[index2] & 128) == 0)
        {
          BigInteger bigInteger1 = new BigInteger(Tools.get8BitsArrayFrom7BitsArray(encoding, index1, length));
          if (list.Count == 0 && !isRelative)
          {
            if (bigInteger1 <= new BigInteger((long) int.MaxValue))
            {
              int intValue = bigInteger1.GetIntValue();
              int num1 = intValue / 40;
              if (num1 > 2)
                num1 = 2;
              int num2 = intValue - num1 * 40;
              list.Add((object) new BigInteger((long) num1));
              list.Add((object) new BigInteger((long) num2));
            }
            else
            {
              BigInteger bigInteger2 = new BigInteger(40L);
              BigInteger bigInteger3 = new BigInteger(2L);
              BigInteger bigInteger4 = bigInteger1 / bigInteger2;
              if (bigInteger4 > bigInteger3)
                bigInteger4 = bigInteger3;
              BigInteger bigInteger5 = bigInteger1 - bigInteger4 * bigInteger2;
              list.Add((object) bigInteger4);
              list.Add((object) bigInteger5);
            }
          }
          else
            list.Add((object) bigInteger1);
          length = 1;
          index1 = index2 + 1;
        }
        else
          ++length;
      }
      BigInteger[] bigIntegerArray = new BigInteger[list.Count];
      for (int index2 = 0; index2 < bigIntegerArray.Length; ++index2)
        bigIntegerArray[index2] = (BigInteger) list[index2];
      return bigIntegerArray;
    }

    public static long[] GetOIDLongArcsFromEncoding(byte[] encoding, bool isRelative)
    {
      if (encoding == null)
        return (long[]) null;
      if (encoding.Length == 0)
        return new long[0];
      IList list = (IList) new ArrayList();
      int index1 = 0;
      int length = 1;
      for (int index2 = 0; index2 < encoding.Length; ++index2)
      {
        if (((int) encoding[index2] & 128) == 0)
        {
          long longFrom7BitsArray = Tools.getLongFrom7BitsArray(encoding, index1, length);
          if (list.Count == 0 && !isRelative)
          {
            int num1 = (int) longFrom7BitsArray;
            int num2 = num1 / 40;
            if (num2 > 2)
              num2 = 2;
            int num3 = num1 - num2 * 40;
            list.Add((object) (long) num2);
            list.Add((object) (long) num3);
          }
          else
            list.Add((object) longFrom7BitsArray);
          length = 1;
          index1 = index2 + 1;
        }
        else
          ++length;
      }
      long[] numArray = new long[list.Count];
      for (int index2 = 0; index2 < numArray.Length; ++index2)
        numArray[index2] = (long) list[index2];
      return numArray;
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

    public virtual bool HasEqualValue(Asn1ObjectIdentifierType that)
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
      this.__objectDescriptor = (Asn1ObjectDescriptorType) null;
      if (this.__getDefaultInstance() == null)
        return;
      this.__setTypeValue(this.__getDefaultInstance());
    }

    public void SetValue(byte[] encoding)
    {
      this.__value = encoding;
    }

    public virtual void SetValue(long[] arcs)
    {
      if (arcs == null)
        this.__value = (byte[]) null;
      else
        this.__value = Asn1ObjectIdentifierType.GetEncodingFromOIDLongArcs(arcs, false);
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
          BigInteger[] bigIntegerArray = (BigInteger[]) null;
          int index1 = 0;
          int num1 = -1;
          int num2 = -1;
          BigInteger bigInteger = (BigInteger) null;
          while (index1 < strArray.Length)
          {
            string s1 = (string) null;
            string str = strArray[index1];
            int length2 = str.IndexOf('(');
            int num3 = str.IndexOf(')');
            string s2;
            if (length2 == -1 && num3 == -1)
              s2 = str;
            else if (length2 != -1 && num3 > length2)
            {
              s2 = str.Substring(0, length2);
              s1 = str.Substring(length2 + 1, num3 - (length2 + 1));
            }
            else
            {
              ++index1;
              continue;
            }
            switch (index1)
            {
              case 0:
                if ("itu-t".Equals(s2) || "ccitt".Equals(s2) || ("itu-r".Equals(s2) || "0".Equals(s2)))
                {
                  num1 = 0;
                  break;
                }
                else if ("iso".Equals(s2) || "1".Equals(s2))
                {
                  num1 = 1;
                  break;
                }
                else if ("joint-iso-itu-t".Equals(s2) || "joint-iso-ccitt".Equals(s2) || "2".Equals(s2))
                {
                  num1 = 2;
                  break;
                }
                else
                  break;
              case 1:
                if (num1 == 0)
                {
                  if ("recommendation".Equals(s2) || "0".Equals(s2))
                  {
                    num2 = 0;
                    break;
                  }
                  else if ("question".Equals(s2) || "1".Equals(s2))
                  {
                    num2 = 1;
                    break;
                  }
                  else if ("administration".Equals(s2) || "2".Equals(s2))
                  {
                    num2 = 2;
                    break;
                  }
                  else if ("network-operator".Equals(s2) || "3".Equals(s2))
                  {
                    num2 = 3;
                    break;
                  }
                  else if ("identified-organization".Equals(s2) || "4".Equals(s2))
                  {
                    num2 = 4;
                    break;
                  }
                  else
                  {
                    try
                    {
                      num2 = s1 == null ? int.Parse(s2) : int.Parse(s1);
                    }
                    catch (Exception ex)
                    {
                    }
                    break;
                  }
                }
                else if (num1 == 1)
                {
                  if ("standard".Equals(s2) || "0".Equals(s2))
                  {
                    num2 = 0;
                    break;
                  }
                  else if ("registration-authority".Equals(s2) || "1".Equals(s2))
                  {
                    num2 = 1;
                    break;
                  }
                  else if ("member-body".Equals(s2) || "2".Equals(s2))
                  {
                    num2 = 2;
                    break;
                  }
                  else if ("identified-organization".Equals(s2) || "3".Equals(s2))
                  {
                    num2 = 3;
                    break;
                  }
                  else
                  {
                    try
                    {
                      num2 = s1 == null ? int.Parse(s2) : int.Parse(s1);
                    }
                    catch (Exception ex)
                    {
                    }
                    break;
                  }
                }
                else
                {
                  try
                  {
                    bigInteger = s1 == null ? new BigInteger(s2) : new BigInteger(s1);
                  }
                  catch (Exception ex)
                  {
                  }
                  break;
                }
              case 2:
                bigIntegerArray = new BigInteger[length1 - 2];
                if (num2 == 0 && s2.Length == 1 && ((int) s2[0] >= 97 && (int) s2[0] <= 122))
                  bigIntegerArray[index1 - 2] = new BigInteger((long) ((int) s2[0] - 97 + 1));
                if (bigIntegerArray[index1 - 2] == null)
                {
                  try
                  {
                    bigIntegerArray[index1 - 2] = s1 == null ? new BigInteger(s2) : new BigInteger(s1);
                  }
                  catch (Exception ex)
                  {
                  }
                  break;
                }
                else
                  break;
              default:
                if (bigIntegerArray != null && bigIntegerArray[index1 - 2] == null)
                {
                  try
                  {
                    bigIntegerArray[index1 - 2] = s1 == null ? new BigInteger(s2) : new BigInteger(s1);
                  }
                  catch (Exception ex)
                  {
                  }
                  break;
                }
                else
                  break;
            }
            ++index1;
          }
          if (num1 < 0 || num2 < 0 && bigInteger == null)
          {
            this.__value = new byte[0];
          }
          else
          {
            int num3 = bigIntegerArray != null ? bigIntegerArray.Length : 0;
            BigInteger[] arcs = new BigInteger[num3 + 2];
            arcs[0] = new BigInteger((long) num1);
            arcs[1] = bigInteger ?? new BigInteger((long) num2);
            for (int index2 = 0; index2 < num3; ++index2)
              arcs[index2 + 2] = bigIntegerArray[index2];
            this.__value = Asn1ObjectIdentifierType.GetEncodingFromOIDBigIntegerArcs(arcs, false);
          }
        }
      }
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

    public void SetValue(int arc1, int arc2, BigInteger[] arcs)
    {
      int num = arcs != null ? arcs.Length : 0;
      BigInteger[] arcs1 = new BigInteger[num + 2];
      arcs1[0] = new BigInteger((long) arc1);
      arcs1[1] = new BigInteger((long) arc2);
      for (int index = 0; index < num; ++index)
        arcs1[index + 2] = arcs[index];
      this.__value = Asn1ObjectIdentifierType.GetEncodingFromOIDBigIntegerArcs(arcs1, false);
    }

    public override void Validate()
    {
      if (this.__value == null || this.__value.Length == 0)
        throw new Asn1ValidationException(18, "at least two arcs with first arc being 0, 1 or 2 and all arcs being positive or zero should be defined in type <" + this.GetType().FullName + ">");
      if (((int) this.__value[this.__value.Length - 1] & 128) == 0)
        return;
      throw new Asn1ValidationException(18, "last arc is not properly encoded (the most significant bit of the last byte should be '0' in '" + ByteArray.ByteArrayToHexString(this.__value, (string) null, -1) + "') in type <" + this.GetType().FullName + ">");
    }
  }
}
