// Decompiled with JetBrains decompiler
// Type: Novensys.ASN1.XerEncoder
// Assembly: Novensys.eCard.SDK, Version=1.1.56.0, Culture=neutral, PublicKeyToken=null
// MVID: 59F9E456-D3DA-4066-A4A4-692B516A775E
// Assembly location: C:\Program Files (x86)\CNAS\SIUI-SPITAL\Novensys.eCard.SDK.dll

using Novensys.ASN1.Type;
using System;
using System.Collections;

namespace Novensys.ASN1
{
  [Serializable]
  public class XerEncoder : Asn1TypeXerWriter
  {
    private static bool _encodingRulesEnabled = false;

    public int IndentSpaces
    {
      get
      {
        return this._indentSpaces;
      }
      set
      {
        this._indentSpaces = value;
      }
    }

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
        return XerEncoder._encodingRulesEnabled;
      }
    }

    public bool IsEncodingXMLDeclaration
    {
      get
      {
        return this._isEncodingXMLDeclaration;
      }
      set
      {
        this._isEncodingXMLDeclaration = value;
      }
    }

    public bool IsHexStringFormattingUnresolvedOpenTypes
    {
      get
      {
        return this._isHexStringFormattingUnresolvedOpenTypes;
      }
      set
      {
        this._isHexStringFormattingUnresolvedOpenTypes = value;
      }
    }

    public bool IsNewlines
    {
      get
      {
        return this.LineSeparator != null && this.LineSeparator.Length > 0;
      }
      set
      {
        this.LineSeparator = "\r\n";
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

    public string LineSeparator
    {
      get
      {
        return this._lineSeparator;
      }
      set
      {
        this._lineSeparator = value;
      }
    }

    public XerEncoder()
    {
      if (!XerEncoder._encodingRulesEnabled)
        throw new Asn1Exception(44, "the coder '" + this.GetType().FullName + "' has not been enabled for the generated classes.");
    }

    public static void __setEncodingRulesEnabled(bool b)
    {
      XerEncoder._encodingRulesEnabled = b;
    }

    public override IList PropertyNames()
    {
      IList list = (IList) new ArrayList();
      list.Add((object) "validating");
      list.Add((object) "internalErrorLogDir");
      list.Add((object) "encodingDefaultValues");
      list.Add((object) "newlines");
      list.Add((object) "lineSeparator");
      list.Add((object) "indentSpaces");
      list.Add((object) "sortingSetOf");
      list.Add((object) "canonicalizingTimeValues");
      list.Add((object) "encodingXMLDeclaration");
      list.Add((object) "hexStringFormattingUnresolvedOpenTypes");
      return list;
    }
  }
}
