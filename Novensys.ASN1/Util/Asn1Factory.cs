// Decompiled with JetBrains decompiler
// Type: Novensys.ASN1.Util.Asn1Factory
// Assembly: Novensys.eCard.SDK, Version=1.1.56.0, Culture=neutral, PublicKeyToken=null
// MVID: 59F9E456-D3DA-4066-A4A4-692B516A775E
// Assembly location: C:\Program Files (x86)\CNAS\SIUI-SPITAL\Novensys.eCard.SDK.dll

using Novensys.ASN1;
using System;
using System.Collections;
using System.Reflection;
using System.Text;

namespace Novensys.ASN1.Util
{
  public class Asn1Factory
  {
    public static void checkDecoderProperty(string wantedProp, string encodingRules)
    {
      if (wantedProp == null)
        return;
      string className;
      if ("BER".ToUpper().Equals(encodingRules.ToUpper()))
        className = " Novensys.ASN1.BerDecoder";
      else if ("DER".ToUpper().Equals(encodingRules.ToUpper()))
        className = " Novensys.ASN1.BerDecoder";
      else if ("AlignedBasicPER".ToUpper().Equals(encodingRules.ToUpper()))
        className = " Novensys.ASN1.AlignedPerDecoder";
      else if ("UnalignedBasicPER".ToUpper().Equals(encodingRules.ToUpper()))
        className = " Novensys.ASN1.UnalignedPerDecoder";
      else if ("BasicXER".ToUpper().Equals(encodingRules.ToUpper()))
        className = " Novensys.ASN1.XerDecoder";
      else if ("CanonicalXER".ToUpper().Equals(encodingRules.ToUpper()))
        className = " Novensys.ASN1.XerDecoder";
      else if ("ExtendedXER".ToUpper().Equals(encodingRules.ToUpper()))
      {
        className = " Novensys.ASN1.ExtendedXerDecoder";
      }
      else
      {
        if (!"ExtendedPER".ToUpper().Equals(encodingRules.ToUpper()))
          throw new Asn1Exception(44, "unknown encoding rules '" + encodingRules + "'.");
        className = " Novensys.ASN1.ExtendedPerDecoder";
      }
      ArrayList propertiesFor = Asn1Factory.getPropertiesFor(className);
      if (propertiesFor == null || !propertiesFor.Contains((object) wantedProp))
        throw new Asn1Exception(41, encodingRules + " decoder does not support " + wantedProp + " property");
    }

    public static void checkEncoderProperty(string wantedProp, string encodingRules)
    {
      if (wantedProp == null)
        return;
      string className;
      if ("BER".ToUpper().Equals(encodingRules.ToUpper()))
        className = " Novensys.ASN1.BerEncoder";
      else if ("DER".ToUpper().Equals(encodingRules.ToUpper()))
        className = " Novensys.ASN1.DerEncoder";
      else if ("AlignedBasicPER".ToUpper().Equals(encodingRules.ToUpper()))
        className = " Novensys.ASN1.AlignedBasicPerEncoder";
      else if ("UnalignedBasicPER".ToUpper().Equals(encodingRules.ToUpper()))
        className = " Novensys.ASN1.UnalignedBasicPerEncoder";
      else if ("BasicXER".ToUpper().Equals(encodingRules.ToUpper()))
        className = " Novensys.ASN1.XerEncoder";
      else if ("CanonicalXER".ToUpper().Equals(encodingRules.ToUpper()))
        className = " Novensys.ASN1.CanonicalXerEncoder";
      else if ("ExtendedXER".ToUpper().Equals(encodingRules.ToUpper()))
      {
        className = " Novensys.ASN1.ExtendedXerEncoder";
      }
      else
      {
        if (!"ExtendedPER".ToUpper().Equals(encodingRules.ToUpper()))
          throw new Asn1Exception(44, "unknown encoding rules '" + encodingRules + "'.");
        className = " Novensys.ASN1.ExtendedPerEncoder";
      }
      ArrayList propertiesFor = Asn1Factory.getPropertiesFor(className);
      if (propertiesFor == null || !propertiesFor.Contains((object) wantedProp))
        throw new Asn1Exception(41, encodingRules + " encoder does not support " + wantedProp + " property");
    }

    public static string checkEncodingRules(string encodingRules)
    {
      if ("BER".ToUpper().Equals(encodingRules.ToUpper()))
        return "BER";
      if ("DER".ToUpper().Equals(encodingRules.ToUpper()))
        return "DER";
      if ("AlignedBasicPER".ToUpper().Equals(encodingRules.ToUpper()))
        return "AlignedBasicPER";
      if ("UnalignedBasicPER".ToUpper().Equals(encodingRules.ToUpper()))
        return "UnalignedBasicPER";
      if ("BasicXER".ToUpper().Equals(encodingRules.ToUpper()))
        return "BasicXER";
      if ("CanonicalXER".ToUpper().Equals(encodingRules.ToUpper()))
        return "CanonicalXER";
      if ("ExtendedXER".ToUpper().Equals(encodingRules.ToUpper()))
        return "ExtendedXER";
      if (!"ExtendedPER".ToUpper().Equals(encodingRules.ToUpper()))
        throw new Asn1Exception(44, "unknown encoding rules '" + encodingRules + "'.");
      else
        return "ExtendedPER";
    }

    public static void checkTypeProperty(string wantedProp, string encodingRules)
    {
      if (wantedProp == null)
        return;
      if ("BER".ToUpper().Equals(encodingRules.ToUpper()))
      {
        if (wantedProp.Equals("preEncodingForConstructedOf") || wantedProp.Equals("postDecodingForConstructedOf"))
          return;
      }
      else if ("DER".ToUpper().Equals(encodingRules.ToUpper()))
      {
        if (wantedProp.Equals("preEncodingForConstructedOf") || wantedProp.Equals("postDecodingForConstructedOf"))
          return;
      }
      else if (!"AlignedBasicPER".ToUpper().Equals(encodingRules.ToUpper()) && !"UnalignedBasicPER".ToUpper().Equals(encodingRules.ToUpper()) && !"ExtendedPER".ToUpper().Equals(encodingRules.ToUpper()))
      {
        if ("BasicXER".ToUpper().Equals(encodingRules.ToUpper()))
        {
          if (wantedProp.Equals("preEncodingForConstructedOf") || wantedProp.Equals("postDecodingForConstructedOf"))
            return;
        }
        else if ("CanonicalXER".ToUpper().Equals(encodingRules.ToUpper()))
        {
          if (wantedProp.Equals("preEncodingForConstructedOf") || wantedProp.Equals("postDecodingForConstructedOf"))
            return;
        }
        else
        {
          if (!"ExtendedXER".ToUpper().Equals(encodingRules.ToUpper()))
            throw new Asn1Exception(44, "unknown encoding rules '" + encodingRules + "'.");
          if (wantedProp.Equals("preEncodingForConstructedOf") || wantedProp.Equals("postDecodingForConstructedOf"))
            return;
        }
      }
      throw new Asn1Exception(43, encodingRules + " encoder/decoder does not support " + wantedProp + " property");
    }

    public static IDecoder createDecoder(string encodingRules)
    {
      if (encodingRules == null)
        return (IDecoder) null;
      string typeName = (string) null;
      if ("BER".ToUpper().Equals(encodingRules.ToUpper()))
        typeName = " Novensys.ASN1.BerDecoder";
      else if ("DER".ToUpper().Equals(encodingRules.ToUpper()))
        typeName = " Novensys.ASN1.BerDecoder";
      else if ("AlignedBasicPER".ToUpper().Equals(encodingRules.ToUpper()))
        typeName = " Novensys.ASN1.AlignedPerDecoder";
      else if ("UnalignedBasicPER".ToUpper().Equals(encodingRules.ToUpper()))
        typeName = " Novensys.ASN1.UnalignedPerDecoder";
      else if ("BasicXER".ToUpper().Equals(encodingRules.ToUpper()))
        typeName = " Novensys.ASN1.XerDecoder";
      else if ("CanonicalXER".ToUpper().Equals(encodingRules.ToUpper()))
        typeName = " Novensys.ASN1.XerDecoder";
      else if ("ExtendedXER".ToUpper().Equals(encodingRules.ToUpper()))
        typeName = " Novensys.ASN1.ExtendedXerDecoder";
      else if ("ExtendedPER".ToUpper().Equals(encodingRules.ToUpper()))
        typeName = " Novensys.ASN1.ExtendedPerDecoder";
      if (typeName == null)
        return (IDecoder) null;
      try
      {
        return (IDecoder) Activator.CreateInstance(System.Type.GetType(typeName));
      }
      catch (Exception ex)
      {
        return (IDecoder) null;
      }
    }

    public static IEncoder createEncoder(string encodingRules)
    {
      if (encodingRules == null)
        return (IEncoder) null;
      string typeName = (string) null;
      if ("BER".ToUpper().Equals(encodingRules.ToUpper()))
        typeName = " Novensys.ASN1.BerEncoder";
      else if ("DER".ToUpper().Equals(encodingRules.ToUpper()))
        typeName = " Novensys.ASN1.DerEncoder";
      else if ("AlignedBasicPER".ToUpper().Equals(encodingRules.ToUpper()))
        typeName = " Novensys.ASN1.AlignedBasicPerEncoder";
      else if ("UnalignedBasicPER".ToUpper().Equals(encodingRules.ToUpper()))
        typeName = " Novensys.ASN1.UnalignedBasicPerEncoder";
      else if ("BasicXER".ToUpper().Equals(encodingRules.ToUpper()))
        typeName = " Novensys.ASN1.XerEncoder";
      else if ("CanonicalXER".ToUpper().Equals(encodingRules.ToUpper()))
        typeName = " Novensys.ASN1.CanonicalXerEncoder";
      else if ("ExtendedXER".ToUpper().Equals(encodingRules.ToUpper()))
        typeName = " Novensys.ASN1.ExtendedXerEncoder";
      else if ("ExtendedPER".ToUpper().Equals(encodingRules.ToUpper()))
        typeName = " Novensys.ASN1.ExtendedPerEncoder";
      if (typeName == null)
        return (IEncoder) null;
      try
      {
        return (IEncoder) Activator.CreateInstance(System.Type.GetType(typeName));
      }
      catch (Exception ex)
      {
        return (IEncoder) null;
      }
    }

    public static void enableEncodingRules(string encodingRules)
    {
      if ("BER".ToUpper().Equals(encodingRules.ToUpper()))
      {
        Asn1Factory.enableEncodingRulesOnCoder(" Novensys.ASN1.BerEncoder");
        Asn1Factory.enableEncodingRulesOnCoder(" Novensys.ASN1.BerDecoder");
      }
      else if ("DER".ToUpper().Equals(encodingRules.ToUpper()))
      {
        Asn1Factory.enableEncodingRulesOnCoder(" Novensys.ASN1.DerEncoder");
        Asn1Factory.enableEncodingRulesOnCoder(" Novensys.ASN1.BerEncoder");
        Asn1Factory.enableEncodingRulesOnCoder(" Novensys.ASN1.BerDecoder");
      }
      else if ("AlignedBasicPER".ToUpper().Equals(encodingRules.ToUpper()))
      {
        Asn1Factory.enableEncodingRulesOnCoder(" Novensys.ASN1.UnalignedBasicPerEncoder");
        Asn1Factory.enableEncodingRulesOnCoder(" Novensys.ASN1.UnalignedPerDecoder");
        Asn1Factory.enableEncodingRulesOnCoder(" Novensys.ASN1.AlignedBasicPerEncoder");
        Asn1Factory.enableEncodingRulesOnCoder(" Novensys.ASN1.AlignedPerDecoder");
      }
      else if ("UnalignedBasicPER".ToUpper().Equals(encodingRules.ToUpper()))
      {
        Asn1Factory.enableEncodingRulesOnCoder(" Novensys.ASN1.UnalignedBasicPerEncoder");
        Asn1Factory.enableEncodingRulesOnCoder(" Novensys.ASN1.UnalignedPerDecoder");
        Asn1Factory.enableEncodingRulesOnCoder(" Novensys.ASN1.AlignedBasicPerEncoder");
        Asn1Factory.enableEncodingRulesOnCoder(" Novensys.ASN1.AlignedPerDecoder");
      }
      else if ("BasicXER".ToUpper().Equals(encodingRules.ToUpper()))
      {
        Asn1Factory.enableEncodingRulesOnCoder(" Novensys.ASN1.XerEncoder");
        Asn1Factory.enableEncodingRulesOnCoder(" Novensys.ASN1.XerDecoder");
      }
      else if ("CanonicalXER".ToUpper().Equals(encodingRules.ToUpper()))
      {
        Asn1Factory.enableEncodingRulesOnCoder(" Novensys.ASN1.CanonicalXerEncoder");
        Asn1Factory.enableEncodingRulesOnCoder(" Novensys.ASN1.XerEncoder");
        Asn1Factory.enableEncodingRulesOnCoder(" Novensys.ASN1.XerDecoder");
      }
      else if ("ExtendedXER".ToUpper().Equals(encodingRules.ToUpper()))
      {
        Asn1Factory.enableEncodingRulesOnCoder(" Novensys.ASN1.ExtendedXerEncoder");
        Asn1Factory.enableEncodingRulesOnCoder(" Novensys.ASN1.ExtendedXerDecoder");
      }
      else
      {
        if (!"ExtendedPER".ToUpper().Equals(encodingRules.ToUpper()))
          return;
        Asn1Factory.enableEncodingRulesOnCoder(" Novensys.ASN1.ExtendedPerEncoder");
        Asn1Factory.enableEncodingRulesOnCoder(" Novensys.ASN1.ExtendedPerDecoder");
      }
    }

    private static void enableEncodingRulesOnCoder(string className)
    {
      try
      {
        System.Type.GetType(className).GetMethod("__setEncodingRulesEnabled", new System.Type[1]
        {
          typeof (bool)
        }).Invoke((object) null, new object[1]
        {
          (object) true
        });
      }
      catch (Exception ex)
      {
      }
    }

    private static ArrayList getPropertiesFor(string className)
    {
      System.Type type;
      try
      {
        type = System.Type.GetType(className);
      }
      catch (Exception ex)
      {
        throw new Asn1Exception(44, "the coder '" + className + "' is not enabled for the generated classes.");
      }
      ArrayList arrayList;
      try
      {
        MethodInfo method = type.GetMethod("__setEncodingRulesEnabled", new System.Type[1]
        {
          typeof (bool)
        });
        method.Invoke((object) null, new object[1]
        {
          (object) true
        });
        object instance = Activator.CreateInstance(type);
        method.Invoke((object) null, new object[1]
        {
          (object) false
        });
        arrayList = (ArrayList) type.GetMethod("PropertyNames", new System.Type[0]).Invoke(instance, new object[0]);
      }
      catch (Exception ex)
      {
        throw new Asn1Exception(44, "the coder '" + className + "' is not enabled for the generated classes.");
      }
      return arrayList;
    }

    public static string getPropertiesToString(string className)
    {
      ArrayList propertiesFor = Asn1Factory.getPropertiesFor(className);
      if (propertiesFor == null)
        return (string) null;
      StringBuilder stringBuilder = new StringBuilder();
      foreach (string str in propertiesFor)
        stringBuilder.Append(str + (object) "\n");
      return ((object) stringBuilder).ToString();
    }
  }
}
