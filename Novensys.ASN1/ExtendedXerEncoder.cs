// Decompiled with JetBrains decompiler
// Type: Novensys.ASN1.ExtendedXerEncoder
// Assembly: Novensys.eCard.SDK, Version=1.1.56.0, Culture=neutral, PublicKeyToken=null
// MVID: 59F9E456-D3DA-4066-A4A4-692B516A775E
// Assembly location: C:\Program Files (x86)\CNAS\SIUI-SPITAL\Novensys.eCard.SDK.dll

using Novensys.ASN1.Type;
using System;
using System.Collections;

namespace Novensys.ASN1
{
  [Serializable]
  public class ExtendedXerEncoder : Asn1TypeXerWriter
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

    public bool IsEncodingAttributeSingleQuoted
    {
      get
      {
        return this._isEncodingAttributeSingleQuoted;
      }
      set
      {
        this._isEncodingAttributeSingleQuoted = value;
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
        return ExtendedXerEncoder._encodingRulesEnabled;
      }
    }

    public bool IsEncodingTypeIdentificationForUseTypeChoiceFirstAlt
    {
      get
      {
        return this._isEncodingTypeIdentificationForUseTypeChoiceFirstAlt;
      }
      set
      {
        this._isEncodingTypeIdentificationForUseTypeChoiceFirstAlt = value;
      }
    }

    public bool IsEncodingTypeIdentificationForUseTypeChoiceUseUnionAlt
    {
      get
      {
        return this._isEncodingTypeIdentificationForUseTypeChoiceUseUnionAlt;
      }
      set
      {
        this._isEncodingTypeIdentificationForUseTypeChoiceUseUnionAlt = value;
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

    public string WhitespaceWithEscapesListSeparator
    {
      get
      {
        return this._whitespaceWithEscapesListSeparator;
      }
      set
      {
        this._whitespaceWithEscapesListSeparator = value;
      }
    }

    public ExtendedXerEncoder()
    {
      if (!ExtendedXerEncoder._encodingRulesEnabled)
        throw new Asn1Exception(44, "the coder '" + this.GetType().FullName + "' has not been enabled for the generated classes.");
      this._isExtendedEncodingEnabled = true;
    }

    public static void __setEncodingRulesEnabled(bool b)
    {
      ExtendedXerEncoder._encodingRulesEnabled = b;
    }

    public void AddOverriddenNamespacePrefix(string overriddenNamespace, string replacementNamespacePrefix)
    {
      if (overriddenNamespace == null || replacementNamespacePrefix == null)
        return;
      if (this._overriddenNamespacesPrefix == null)
        this._overriddenNamespacesPrefix = (IDictionary) new Hashtable();
      this._overriddenNamespacesPrefix.Add((object) overriddenNamespace, (object) replacementNamespacePrefix);
    }

    public void AddOverriddenNamespaceUri(string overriddenNamespace, string replacementNamespaceUri)
    {
      if (overriddenNamespace == null || replacementNamespaceUri == null)
        return;
      if (this._overriddenNamespacesUri == null)
        this._overriddenNamespacesUri = (IDictionary) new Hashtable();
      this._overriddenNamespacesUri.Add((object) overriddenNamespace, (object) replacementNamespaceUri);
    }

    public IDictionary GetOverriddenNamespacePrefix()
    {
      return this._overriddenNamespacesPrefix;
    }

    public IDictionary GetOverriddenNamespaceUri()
    {
      return this._overriddenNamespacesUri;
    }

    public override IList PropertyNames()
    {
      IList list = (IList) new ArrayList();
      list.Add((object) "validating");
      list.Add((object) "internalErrorLogDir");
      list.Add((object) "encodingDefaultValues");
      list.Add((object) "lineSeparator");
      list.Add((object) "indentSpaces");
      list.Add((object) "sortingSetOf");
      list.Add((object) "canonicalizingTimeValues");
      list.Add((object) "encodingXMLDeclaration");
      list.Add((object) "hexStringFormattingUnresolvedOpenTypes");
      list.Add((object) "encodingAttributeSingleQuoted");
      list.Add((object) "whitespaceWithEscapesListSeparator");
      list.Add((object) "encodingTypeIdentificationForUseTypeChoiceFirstAlt");
      list.Add((object) "encodingTypeIdentificationForUseTypeChoiceUseUnionAlt");
      list.Add((object) "overriddenNamespaces");
      return list;
    }

    public void RemoveOverriddenNamespace(string overriddenNamespace)
    {
      if (overriddenNamespace == null)
        return;
      if (this._overriddenNamespacesUri != null)
      {
        this._overriddenNamespacesUri.Remove((object) overriddenNamespace);
        if (this._overriddenNamespacesUri.Count == 0)
          this._overriddenNamespacesUri = (IDictionary) null;
      }
      if (this._overriddenNamespacesPrefix != null)
      {
        this._overriddenNamespacesPrefix.Remove((object) overriddenNamespace);
        if (this._overriddenNamespacesPrefix.Count == 0)
          this._overriddenNamespacesPrefix = (IDictionary) null;
      }
    }
  }
}
