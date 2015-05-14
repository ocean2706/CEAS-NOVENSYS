// Decompiled with JetBrains decompiler
// Type: Novensys.ASN1.BerDecoder
// Assembly: Novensys.eCard.SDK, Version=1.1.56.0, Culture=neutral, PublicKeyToken=null
// MVID: 59F9E456-D3DA-4066-A4A4-692B516A775E
// Assembly location: C:\Program Files (x86)\CNAS\SIUI-SPITAL\Novensys.eCard.SDK.dll

using Novensys.ASN1.Type;
using System;
using System.Collections;

namespace Novensys.ASN1
{
  [Serializable]
  public class BerDecoder : Asn1TypeBerReader
  {
    private static bool _encodingRulesEnabled = false;

    public static bool IsEncodingRulesEnabled
    {
      get
      {
        return BerDecoder._encodingRulesEnabled;
      }
    }

    public BerDecoder()
    {
      if (!BerDecoder._encodingRulesEnabled)
        throw new Asn1Exception(44, "the coder '" + this.GetType().FullName + "' has not been enabled for the generated classes.");
    }

    public static void __setEncodingRulesEnabled(bool b)
    {
      BerDecoder._encodingRulesEnabled = b;
    }

    public override IList PropertyNames()
    {
      IList list = (IList) new ArrayList();
      list.Add((object) "validating");
      list.Add((object) "resolvingContent");
      list.Add((object) "internalErrorLogDir");
      return list;
    }
  }
}
