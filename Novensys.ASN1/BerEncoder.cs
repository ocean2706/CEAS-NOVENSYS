// Decompiled with JetBrains decompiler
// Type: Novensys.ASN1.BerEncoder
// Assembly: Novensys.eCard.SDK, Version=1.1.56.0, Culture=neutral, PublicKeyToken=null
// MVID: 59F9E456-D3DA-4066-A4A4-692B516A775E
// Assembly location: C:\Program Files (x86)\CNAS\SIUI-SPITAL\Novensys.eCard.SDK.dll

using Novensys.ASN1.Type;
using System;
using System.Collections;

namespace Novensys.ASN1
{
  [Serializable]
  public class BerEncoder : Asn1TypeBerWriter
  {
    private static bool _encodingRulesEnabled = false;

    public bool IsCanonicalizingTimeValues
    {
      get
      {
        return this._isCanonicalizingTimeValues;
      }
      set
      {
        this._isCanonicalizingTimeValues = value;
      }
    }

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
        return BerEncoder._encodingRulesEnabled;
      }
    }

    public bool IsIndefiniteLengthForm
    {
      get
      {
        return this._isIndefiniteLengthForm;
      }
      set
      {
        this._isIndefiniteLengthForm = value;
      }
    }

    public bool IsRemovingTrailingZeroesInBitStringWithNamedBits
    {
      get
      {
        return this._isRemovingTrailingZeroesInBitStringWithNamedBits;
      }
      set
      {
        this._isRemovingTrailingZeroesInBitStringWithNamedBits = value;
      }
    }

    public bool IsSortingSetOf
    {
      get
      {
        return this._isSortingSetOf;
      }
      set
      {
        this._isSortingSetOf = value;
      }
    }

    public BerEncoder()
    {
      if (!BerEncoder._encodingRulesEnabled)
        throw new Asn1Exception(44, "the coder '" + this.GetType().FullName + "' has not been enabled for the generated classes.");
    }

    public static void __setEncodingRulesEnabled(bool b)
    {
      BerEncoder._encodingRulesEnabled = b;
    }

    public override IList PropertyNames()
    {
      IList list = (IList) new ArrayList();
      list.Add((object) "validating");
      list.Add((object) "internalErrorLogDir");
      list.Add((object) "encodingDefaultValues");
      list.Add((object) "indefiniteLengthForm");
      list.Add((object) "sortingSetOf");
      list.Add((object) "canonicalizingTimeValues");
      list.Add((object) "removingTrailingZeroesInBitStringWithNamedBits");
      return list;
    }
  }
}
