// Decompiled with JetBrains decompiler
// Type: Novensys.eCard.SDK.ASN1.ASN_EHCFactory
// Assembly: Novensys.eCard.SDK, Version=1.1.56.0, Culture=neutral, PublicKeyToken=null
// MVID: 59F9E456-D3DA-4066-A4A4-692B516A775E
// Assembly location: C:\Program Files (x86)\CNAS\SIUI-SPITAL\Novensys.eCard.SDK.dll

using Novensys.ASN1;
using Novensys.ASN1.Util;
using System.Collections;

namespace Novensys.eCard.SDK.ASN1
{
  public class ASN_EHCFactory
  {
    private static string _encodingRules = "BER";
    private static Hashtable _encoderProperties_BER = new Hashtable();
    private static Hashtable _decoderProperties_BER = new Hashtable();
    private static Hashtable _encoderProperties_DER = new Hashtable();
    private static Hashtable _decoderProperties_DER = new Hashtable();
    private static Hashtable _encoderProperties = ASN_EHCFactory._encoderProperties_BER;
    private static Hashtable _decoderProperties = ASN_EHCFactory._decoderProperties_BER;
    private static Hashtable _typeProperties = new Hashtable();
    private static bool _isPreEncodingForConstructedOf = false;
    private static bool _isPostDecodingForConstructedOf = false;

    public static string EncodingRules
    {
      get
      {
        return ASN_EHCFactory._encodingRules;
      }
      set
      {
        ASN_EHCFactory.Init();
        string str = Asn1Factory.checkEncodingRules(value);
        ASN_EHCFactory._encodingRules = str;
        if ("BER".Equals(str))
        {
          ASN_EHCFactory._encoderProperties = ASN_EHCFactory._encoderProperties_BER;
          ASN_EHCFactory._decoderProperties = ASN_EHCFactory._decoderProperties_BER;
        }
        if (!"DER".Equals(str))
          return;
        ASN_EHCFactory._encoderProperties = ASN_EHCFactory._encoderProperties_DER;
        ASN_EHCFactory._decoderProperties = ASN_EHCFactory._decoderProperties_DER;
      }
    }

    static ASN_EHCFactory()
    {
      ASN_EHCFactory._encoderProperties_BER[(object) "validating"] = (object) "false";
      ASN_EHCFactory._decoderProperties_BER[(object) "resolvingContent"] = (object) "true";
      ASN_EHCFactory._decoderProperties_BER[(object) "validating"] = (object) "false";
      ASN_EHCFactory._encoderProperties_DER[(object) "validating"] = (object) "false";
      ASN_EHCFactory._decoderProperties_DER[(object) "resolvingContent"] = (object) "true";
      ASN_EHCFactory._decoderProperties_DER[(object) "validating"] = (object) "false";
      ASN_EHCFactory._typeProperties[(object) "preEncodingForConstructedOf"] = (object) "false";
      ASN_EHCFactory._typeProperties[(object) "postDecodingForConstructedOf"] = (object) "false";
    }

    public static void Init()
    {
      Asn1Factory.enableEncodingRules("BER");
      Asn1Factory.enableEncodingRules("DER");
    }

    public static void SetEncoderProperty(string property, string val)
    {
      Asn1Factory.checkEncoderProperty(property, ASN_EHCFactory._encodingRules);
      ASN_EHCFactory._encoderProperties[(object) property] = (object) val;
    }

    public static string GetEncoderProperty(string property)
    {
      Asn1Factory.checkEncoderProperty(property, ASN_EHCFactory._encodingRules);
      return (string) ASN_EHCFactory._encoderProperties[(object) property];
    }

    public static void SetDecoderProperty(string property, string val)
    {
      Asn1Factory.checkDecoderProperty(property, ASN_EHCFactory._encodingRules);
      ASN_EHCFactory._decoderProperties[(object) property] = (object) val;
    }

    public static string GetDecoderProperty(string property)
    {
      Asn1Factory.checkDecoderProperty(property, ASN_EHCFactory._encodingRules);
      return (string) ASN_EHCFactory._decoderProperties[(object) property];
    }

    public static void SetTypeProperty(string property, string val)
    {
      Asn1Factory.checkTypeProperty(property, ASN_EHCFactory._encodingRules);
      ASN_EHCFactory._typeProperties[(object) property] = (object) val;
      if ("postDecodingForConstructedOf".Equals(property))
        ASN_EHCFactory._isPostDecodingForConstructedOf = bool.Parse(val);
      if (!"preEncodingForConstructedOf".Equals(property))
        return;
      ASN_EHCFactory._isPreEncodingForConstructedOf = bool.Parse(val);
    }

    public static string GetTypeProperty(string property)
    {
      Asn1Factory.checkTypeProperty(property, ASN_EHCFactory._encodingRules);
      return (string) ASN_EHCFactory._typeProperties[(object) property];
    }

    public static IDecoder CreateDecoder()
    {
      ASN_EHCFactory.Init();
      IDecoder decoder = Asn1Factory.createDecoder(ASN_EHCFactory._encodingRules);
      decoder.SetProperties(ASN_EHCFactory._decoderProperties);
      return decoder;
    }

    public static IEncoder CreateEncoder()
    {
      ASN_EHCFactory.Init();
      IEncoder encoder = Asn1Factory.createEncoder(ASN_EHCFactory._encodingRules);
      encoder.SetProperties(ASN_EHCFactory._encoderProperties);
      return encoder;
    }

    public static bool IsPreEncodingForConstructedOf()
    {
      return ASN_EHCFactory._isPreEncodingForConstructedOf;
    }

    public static bool IsPostDecodingForConstructedOf()
    {
      return ASN_EHCFactory._isPostDecodingForConstructedOf;
    }
  }
}
