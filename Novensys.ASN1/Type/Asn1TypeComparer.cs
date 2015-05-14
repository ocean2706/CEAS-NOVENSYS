// Decompiled with JetBrains decompiler
// Type: Novensys.ASN1.Type.Asn1TypeComparer
// Assembly: Novensys.eCard.SDK, Version=1.1.56.0, Culture=neutral, PublicKeyToken=null
// MVID: 59F9E456-D3DA-4066-A4A4-692B516A775E
// Assembly location: C:\Program Files (x86)\CNAS\SIUI-SPITAL\Novensys.eCard.SDK.dll

using System;

namespace Novensys.ASN1.Type
{
  public class Asn1TypeComparer
  {
    public int Compare(object o1, object o2)
    {
      if (o1 == null || o2 == null)
        throw new ArgumentException("o1 and o2 should not be null.");
      if (o1 is Asn1BitStringType && o2 is Asn1BitStringType)
      {
        string binaryStringValue1 = ((Asn1BitStringType) o1).GetBinaryStringValue();
        string binaryStringValue2 = ((Asn1BitStringType) o2).GetBinaryStringValue();
        if (binaryStringValue1 == null)
          throw new ArgumentException("Asn1BitString o1 does not contain a value.");
        if (binaryStringValue2 == null)
          throw new ArgumentException("Asn1BitString o2 does not contain a value.");
        else
          return binaryStringValue1.CompareTo(binaryStringValue2);
      }
      else if (o1 is Asn1BooleanType && o2 is Asn1BooleanType)
      {
        bool boolValue1 = ((Asn1BooleanType) o1).BoolValue;
        bool boolValue2 = ((Asn1BooleanType) o2).BoolValue;
        if (boolValue1 == boolValue2)
          return 0;
        return boolValue1 ? 1 : -1;
      }
      else
      {
        if (o1 is Asn1EnumeratedType && o2 is Asn1EnumeratedType)
          return ((Asn1EnumeratedType) o1).GetLongValue().CompareTo(((Asn1EnumeratedType) o2).GetLongValue());
        if (o1 is Asn1IntegerType && o2 is Asn1IntegerType)
          return ((Asn1IntegerType) o1).GetLongValue().CompareTo(((Asn1IntegerType) o2).GetLongValue());
        if (o1 is Asn1ObjectIdentifierType && o2 is Asn1ObjectIdentifierType)
        {
          string stringValue1 = ((Asn1ObjectIdentifierType) o1).GetStringValue();
          string stringValue2 = ((Asn1ObjectIdentifierType) o2).GetStringValue();
          if (stringValue1 == null)
            throw new ArgumentException("Asn1ObjectIdentifierType o1 does not contain a value.");
          if (stringValue2 == null)
            throw new ArgumentException("Asn1ObjectIdentifierType o2 does not contain a value.");
          else
            return stringValue1.CompareTo(stringValue2);
        }
        else if (o1 is Asn1RelativeOIDType && o2 is Asn1RelativeOIDType)
        {
          string stringValue1 = ((Asn1RelativeOIDType) o1).GetStringValue();
          string stringValue2 = ((Asn1RelativeOIDType) o2).GetStringValue();
          if (stringValue1 == null)
            throw new ArgumentException("Asn1RelativeOIDType o1 does not contain a value.");
          if (stringValue2 == null)
            throw new ArgumentException("Asn1RelativeOIDType o2 does not contain a value.");
          else
            return stringValue1.CompareTo(stringValue2);
        }
        else if (o1 is Asn1OctetStringType && o2 is Asn1OctetStringType)
        {
          string hexStringValue1 = ((Asn1OctetStringType) o1).GetHexStringValue();
          string hexStringValue2 = ((Asn1OctetStringType) o2).GetHexStringValue();
          if (hexStringValue1 == null)
            throw new ArgumentException("Asn1OctetStringType o1 does not contain a value.");
          if (hexStringValue2 == null)
            throw new ArgumentException("Asn1OctetStringType o2 does not contain a value.");
          else
            return hexStringValue1.CompareTo(hexStringValue2);
        }
        else
        {
          if (o1 is Asn1RealType && o2 is Asn1RealType)
            return ((Asn1RealType) o1).GetDoubleValue().CompareTo(((Asn1RealType) o2).GetDoubleValue());
          if (o1 is Asn1StringType && o2 is Asn1StringType)
          {
            string stringValue1 = ((Asn1StringType) o1).GetStringValue();
            string stringValue2 = ((Asn1StringType) o2).GetStringValue();
            if (stringValue1 == null)
              throw new ArgumentException("Asn1StringType o1 does not contain a value.");
            if (stringValue2 == null)
              throw new ArgumentException("Asn1StringType o2 does not contain a value.");
            else
              return stringValue1.CompareTo(stringValue2);
          }
          else if (!(o1 is Asn1TimeType) || !(o2 is Asn1TimeType))
            throw new ArgumentException("o1 and o2 cannot be compared.");
          else
            return ((Asn1TimeType) o1).GetDateTimeValue().CompareTo(((Asn1TimeType) o2).GetDateTimeValue());
        }
      }
    }
  }
}
