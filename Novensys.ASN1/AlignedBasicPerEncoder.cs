// Decompiled with JetBrains decompiler
// Type: Novensys.ASN1.AlignedBasicPerEncoder
// Assembly: Novensys.eCard.SDK, Version=1.1.56.0, Culture=neutral, PublicKeyToken=null
// MVID: 59F9E456-D3DA-4066-A4A4-692B516A775E
// Assembly location: C:\Program Files (x86)\CNAS\SIUI-SPITAL\Novensys.eCard.SDK.dll

using Novensys.ASN1.Type;
using System;
using System.Collections;

namespace Novensys.ASN1
{
  [Serializable]
  public class AlignedBasicPerEncoder : Asn1TypePerWriter
  {
    private static bool _encodingRulesEnabled = false;

    public bool IsEncodingDefaultValues
    {
      get
      {
        return this._isEncodingDefaultValues;
      }
      set
      {
        this._isEncodingDefaultValues = value;
      }
    }

    public static bool IsEncodingRulesEnabled
    {
      get
      {
        return AlignedBasicPerEncoder._encodingRulesEnabled;
      }
    }

    public bool IsPaddingWithZeroBit
    {
      get
      {
        return this._isPaddingWithZeroBit;
      }
      set
      {
        this._isPaddingWithZeroBit = value;
      }
    }

    public AlignedBasicPerEncoder()
      : base(true)
    {
      if (!AlignedBasicPerEncoder._encodingRulesEnabled)
        throw new Asn1Exception(44, "the coder '" + this.GetType().FullName + "' has not been enabled for the generated classes.");
    }

    public static void __setEncodingRulesEnabled(bool b)
    {
      AlignedBasicPerEncoder._encodingRulesEnabled = b;
    }

    public override IList PropertyNames()
    {
      IList list = (IList) new ArrayList();
      list.Add((object) "validating");
      list.Add((object) "internalErrorLogDir");
      list.Add((object) "encodingDefaultValues");
      list.Add((object) "paddingWithZeroBit");
      return list;
    }
  }
}
