// Decompiled with JetBrains decompiler
// Type: Novensys.ASN1.Type.Asn1TypeXerWriter
// Assembly: Novensys.eCard.SDK, Version=1.1.56.0, Culture=neutral, PublicKeyToken=null
// MVID: 59F9E456-D3DA-4066-A4A4-692B516A775E
// Assembly location: C:\Program Files (x86)\CNAS\SIUI-SPITAL\Novensys.eCard.SDK.dll

using Novensys.ASN1;
using Novensys.ASN1.IO;
using Novensys.ASN1.Util;
using System;
using System.Collections;
using System.Globalization;
using System.IO;
using System.Text;

namespace Novensys.ASN1.Type
{
  [Serializable]
  public class Asn1TypeXerWriter : Asn1TypeWriter
  {
    protected int _indentSpaces = -1;
    protected bool _isCanonicalizingTimeValues = true;
    protected bool _isSortingSetOf = false;
    protected string _lineSeparator = (string) null;
    private XmlAsn1OutputStream _out = (XmlAsn1OutputStream) null;
    public const string KEY_CANONICALIZING_TIME_VALUES = "canonicalizingTimeValues";
    public const string KEY_ENCODING_ATTRIBUTE_SINGLE_QUOTED = "encodingAttributeSingleQuoted";
    public const string KEY_ENCODING_TYPE_IDENTIFICATION_FOR_USE_TYPE_CHOICE_FIRST_ALT = "encodingTypeIdentificationForUseTypeChoiceFirstAlt";
    public const string KEY_ENCODING_TYPE_IDENTIFICATION_FOR_USE_TYPE_CHOICE_USE_UNION_ALT = "encodingTypeIdentificationForUseTypeChoiceUseUnionAlt";
    public const string KEY_ENCODING_XML_DECLARATION = "encodingXMLDeclaration";
    public const string KEY_HEXSTRING_FORMATTING_UNRESOLVED_OPEN_TYPES = "hexStringFormattingUnresolvedOpenTypes";
    public const string KEY_INDENT_SPACES = "indentSpaces";
    public const string KEY_LINE_SEPARATOR = "lineSeparator";
    public const string KEY_NEWLINES = "newlines";
    public const string KEY_OVERRIDDEN_NAMESPACES = "overriddenNamespaces";
    public const string KEY_SORTING_SET_OF = "sortingSetOf";
    public const string KEY_WHITESPACE_WITH_ESCAPES_LIST_SEPARATOR = "whitespaceWithEscapesListSeparator";
    private int _embedValuesDepth;
    private IEnumerator _embedValuesIterator;
    protected bool _isEncodingAttributeSingleQuoted;
    protected bool _isEncodingTypeIdentificationForUseTypeChoiceFirstAlt;
    protected bool _isEncodingTypeIdentificationForUseTypeChoiceUseUnionAlt;
    protected bool _isEncodingXMLDeclaration;
    protected bool _isExtendedEncodingEnabled;
    protected bool _isHexStringFormattingUnresolvedOpenTypes;
    private bool _isRoot;
    protected IDictionary _overriddenNamespacesPrefix;
    protected IDictionary _overriddenNamespacesUri;
    private bool _skipTag;
    private Stream _underlyingOut;
    protected string _whitespaceWithEscapesListSeparator;

    public Asn1TypeXerWriter()
    {
      this._isEncodingDefaultValues = true;
      this._isEncodingXMLDeclaration = false;
      this._isHexStringFormattingUnresolvedOpenTypes = false;
      this._isExtendedEncodingEnabled = false;
      this._isEncodingAttributeSingleQuoted = false;
      this._whitespaceWithEscapesListSeparator = " ";
      this._isEncodingTypeIdentificationForUseTypeChoiceFirstAlt = false;
      this._isEncodingTypeIdentificationForUseTypeChoiceUseUnionAlt = false;
      this._overriddenNamespacesUri = (IDictionary) null;
      this._overriddenNamespacesPrefix = (IDictionary) null;
    }

    protected internal void __encodeBitStringType(Asn1BitStringType typeInstance)
    {
      string xerTag = typeInstance.__getXerTag();
      bool flag1 = xerTag.Length > 0 && (!typeInstance.__isXerUntagged() && !this._skipTag || this._isRoot);
      this._skipTag = false;
      if (flag1)
        this.writeStartTag((Asn1Type) typeInstance, xerTag);
      if (this._isExtendedEncodingEnabled)
        this.writePiOrComment((Asn1Type) typeInstance, 1);
      if (typeInstance.__isContainingType())
      {
        IEncoder encoder = typeInstance.__getEncoder();
        bool flag2 = encoder != null;
        if (!flag2)
          encoder = (IEncoder) this.getWriter();
        if (typeInstance.GetTypeValue() != null)
        {
          try
          {
            encoder.Encode(typeInstance.GetTypeValue());
            byte[] data = encoder.Data;
            typeInstance.SetValue(data);
            if (!flag2)
              this._out.rawBytes(data);
            else
              this._out.text(typeInstance.__getBitString().ToString());
          }
          catch (Asn1ValidationException ex)
          {
            throw ex;
          }
          catch (Asn1Exception ex)
          {
            throw new Asn1Exception(51, "type <" + typeInstance.GetTypeValue().GetType().FullName + "> for OCTET SRING type <" + typeInstance.GetType().FullName + "> [internal exception : " + ex.Message + "]");
          }
        }
        else
        {
          byte[] byteArrayValue = typeInstance.GetByteArrayValue();
          if (byteArrayValue == null || byteArrayValue.Length == 0)
            throw new Asn1Exception(48, " for type <" + typeInstance.GetType().FullName + ">");
          if (!flag2)
          {
            if (this._isHexStringFormattingUnresolvedOpenTypes)
              this._out.text(new BitString(byteArrayValue, byteArrayValue.Length << 3).ToString());
            else
              this._out.rawBytes(byteArrayValue);
          }
          else
            this._out.text(ByteArray.ByteArrayToHexString(byteArrayValue, (string) null, -1, true));
        }
      }
      else
      {
        string text = this.__encodeBitStringValue(typeInstance);
        if (text.Length > 0)
          this._out.text(text);
      }
      if (this._isExtendedEncodingEnabled)
        this.writePiOrComment((Asn1Type) typeInstance, 2);
      if (!flag1)
        return;
      this.writeEndTag((Asn1Type) typeInstance, xerTag);
    }

    protected internal string __encodeBitStringValue(Asn1BitStringType typeInstance)
    {
      BitString bitString = typeInstance.__getBitString();
      if (bitString == null)
      {
        return typeInstance.__isXerDefaultForEmpty() ? " " : "";
      }
      else
      {
        string str = (string) null;
        string[] idSet = typeInstance.__getXerIdentifierSet();
        if (!this._isExtendedEncodingEnabled || idSet == null)
          idSet = typeInstance.__getIdentifierSet();
        if (this._isExtendedEncodingEnabled && typeInstance.__isXerText() && idSet != null)
        {
          IList identifierList = typeInstance.__getIdentifierList(idSet);
          if (identifierList != null)
          {
            StringBuilder stringBuilder = new StringBuilder();
            int count = identifierList.Count;
            for (int index = 0; index < count; ++index)
            {
              stringBuilder.Append(identifierList[index]);
              if (index < count - 1)
                stringBuilder.Append(this._whitespaceWithEscapesListSeparator);
            }
            str = ((object) stringBuilder).ToString();
          }
        }
        if (str == null)
          str = !typeInstance.__isWithNamedBitList() ? bitString.ToString() : bitString.trimmedToString();
        if ((str == null || str.Length == 0) && typeInstance.__isXerDefaultForEmpty())
          return " ";
        else
          return str;
      }
    }

    protected internal void __encodeBooleanType(Asn1BooleanType typeInstance)
    {
      string xerTag = typeInstance.__getXerTag();
      bool flag = xerTag.Length > 0 && (!typeInstance.__isXerUntagged() && !this._skipTag || this._isRoot);
      this._skipTag = false;
      if (flag)
        this.writeStartTag((Asn1Type) typeInstance, xerTag);
      if (this._isExtendedEncodingEnabled)
        this.writePiOrComment((Asn1Type) typeInstance, 1);
      if (this._isExtendedEncodingEnabled && typeInstance.__isXerText())
        this._out.text(this.__encodeBooleanValue(typeInstance));
      else
        this._out.emptyTag(this.__encodeBooleanValue(typeInstance));
      if (this._isExtendedEncodingEnabled)
        this.writePiOrComment((Asn1Type) typeInstance, 2);
      if (!flag)
        return;
      this.writeEndTag((Asn1Type) typeInstance, xerTag);
    }

    protected internal string __encodeBooleanValue(Asn1BooleanType typeInstance)
    {
      string[] xerIdentifierSet = typeInstance.__getXerIdentifierSet();
      if (!this._isExtendedEncodingEnabled || xerIdentifierSet == null)
        return typeInstance.GetBoolValue() ? "true" : "false";
      else if (typeInstance.GetBoolValue())
        return xerIdentifierSet[1];
      else
        return xerIdentifierSet[0];
    }

    protected internal string __encodeCanonicalRealValue(Asn1RealType typeInstance, string mantissaStr, long exponent)
    {
      StringBuilder stringBuilder = new StringBuilder(mantissaStr);
      if (stringBuilder.Length > 1)
      {
        if ((int) stringBuilder[0] == 45)
        {
          exponent += (long) (stringBuilder.Length - 2);
          stringBuilder.Insert(2, new char[1]
          {
            '.'
          });
        }
        else
        {
          exponent += (long) (stringBuilder.Length - 1);
          stringBuilder.Insert(1, new char[1]
          {
            '.'
          });
        }
      }
      else
        stringBuilder.Append(".0");
      stringBuilder.Append("E");
      stringBuilder.Append(exponent);
      return ((object) stringBuilder).ToString();
    }

    protected internal void __encodeChoiceType(Asn1ChoiceType typeInstance)
    {
      Asn1Type typeValue1 = typeInstance.GetTypeValue();
      string xerTag = typeInstance.__getXerTag();
      bool flag = xerTag.Length > 0 && (!typeInstance.__isXerUntagged() && !this._skipTag);
      this._skipTag = false;
      if (flag)
      {
        if (this._isExtendedEncodingEnabled)
        {
          this.writePiOrComment((Asn1Type) typeInstance, 0);
          string xerNamespaceUri = this.getXerNamespaceUri((Asn1Type) typeInstance);
          this._out.setPrefix(this.getXerNamespacePrefix((Asn1Type) typeInstance), xerNamespaceUri);
          if (typeInstance.__isXerUseType() || typeInstance.__isXerUseUnion())
          {
            string val = typeValue1.__getXerTag();
            if (typeInstance.__isXerUseType() && typeInstance.__isChosenInstanceUseUnion() && this._isEncodingTypeIdentificationForUseTypeChoiceUseUnionAlt)
            {
              Asn1Type typeValue2 = ((Asn1ChoiceType) typeValue1).GetTypeValue();
              if (typeValue2 != null)
                val = typeValue2.__getXerTag();
            }
            if (typeInstance.__isXerUseType() && !this._isEncodingTypeIdentificationForUseTypeChoiceFirstAlt && (typeInstance.__isChosenInstanceFirstComponent() && !typeInstance.__isChosenInstanceUseUnion()))
              val = (string) null;
            if (val != null && val.Length > 0)
            {
              this._out.setPrefix(typeInstance.__getXerControlNamespacePrefix(), typeInstance.__getXerControlNamespaceUri());
              string xerNamespacePrefix = this.getXerNamespacePrefix(typeValue1);
              if (xerNamespacePrefix != null && xerNamespacePrefix.Length > 0)
              {
                this._out.setPrefix(this.getXerNamespacePrefix(typeValue1), this.getXerNamespaceUri(typeValue1));
                val = xerNamespacePrefix + ":" + val;
              }
            }
            this._out.startTag(xerNamespaceUri, xerTag);
            if (val != null && val.Length > 0)
              this._out.attribute(typeInstance.__getXerControlNamespaceUri(), "type", val);
          }
          else
            this._out.startTag(xerNamespaceUri, xerTag);
        }
        else
          this._out.startTag((string) null, xerTag);
        this._isRoot = false;
      }
      else if (typeInstance.__isXerUntagged() && this._isRoot)
        this._isRoot = false;
      if (typeInstance.__isXerUseType() || typeInstance.__isXerUseUnion())
        this._skipTag = true;
      if (this._isExtendedEncodingEnabled)
        this.writePiOrComment((Asn1Type) typeInstance, 1);
      if (typeValue1 != null)
        typeValue1.__write(this);
      if (this._isExtendedEncodingEnabled)
        this.writePiOrComment((Asn1Type) typeInstance, 2);
      if (!flag)
        return;
      this.writeEndTag((Asn1Type) typeInstance, xerTag);
    }

    protected internal string __encodeChoiceValue(Asn1ChoiceType typeInstance)
    {
      return typeInstance.GetTypeValue().__writeValue(this);
    }

    protected internal void __encodeConstructedType(Asn1ConstructedType typeInstance)
    {
      string xerTag = typeInstance.__getXerTag();
      bool flag1 = this._isExtendedEncodingEnabled && (typeInstance.__isXerUseOrder() || typeInstance.__isXerEnclosingTypeUseOrder());
      Asn1Type[] componentTypeInstances = typeInstance.__getXerDefinedComponentTypeInstances(this._isEncodingDefaultValues);
      IEnumerator enumerator = (IEnumerator) null;
      bool flag2 = xerTag.Length > 0 && (!typeInstance.__isXerUntagged() && !this._skipTag || this._isRoot);
      this._skipTag = false;
      if (flag2)
        this.writeStartTag((Asn1Type) typeInstance, xerTag);
      if (this._isExtendedEncodingEnabled && this._out.isStartTagIncomplete() && componentTypeInstances != null)
      {
        for (int index = 0; index < componentTypeInstances.Length; ++index)
        {
          Asn1Type typeInstance1 = componentTypeInstances[index];
          if (typeInstance1 != null)
          {
            if (typeInstance1.__isXerAttribute())
            {
              if (typeInstance1.__isXerUseQName())
              {
                string val = this.writeUseQNameValue((Asn1ConstructedType) typeInstance1);
                this._out.attribute((string) null, typeInstance1.__getXerTag(), val);
              }
              else
              {
                string xerNamespaceUri = this.getXerNamespaceUri(typeInstance1);
                this._out.setPrefix(this.getXerNamespacePrefix(typeInstance1), xerNamespaceUri, false);
                if (typeInstance1.__isXerList())
                  this._out.attributeList(xerNamespaceUri, typeInstance1.__getXerTag(), this.writeListValue(typeInstance1));
                else
                  this._out.attribute(xerNamespaceUri, typeInstance1.__getXerTag(), typeInstance1.__writeValue(this));
              }
            }
            else if (typeInstance1.__isXerAnyAttributes())
              this.writeAnyAttributesValue((Asn1ConstructedOfType) typeInstance1);
          }
          else
            break;
        }
      }
      if (this._isExtendedEncodingEnabled && typeInstance.__isXerUseNil())
      {
        this._out.setPrefix(typeInstance.__getXerControlNamespacePrefix(), typeInstance.__getXerControlNamespaceUri(), false);
        Asn1Type optionalComponent = typeInstance.__getXerDefinedUseNilOptionalComponent();
        if (optionalComponent == null)
        {
          this._out.attribute(typeInstance.__getXerControlNamespaceUri(), "nil", "true");
        }
        else
        {
          this._out.attribute(typeInstance.__getXerControlNamespaceUri(), "nil", "false");
          if (this._isExtendedEncodingEnabled && typeInstance.__isXerEmbedValues() && typeInstance.__getXerEmbedValuesComponent().GetAsn1TypeList() != null)
          {
            this._embedValuesDepth = this._out.getDepth();
            this._embedValuesIterator = typeInstance.__getXerEmbedValuesComponent().GetAsn1TypeList().GetEnumerator();
            this.writeEmbedValuesValue();
          }
          this._skipTag = true;
          optionalComponent.__write(this);
        }
      }
      else
      {
        if (this._isExtendedEncodingEnabled)
          this.writePiOrComment((Asn1Type) typeInstance, 1);
        if (this._isExtendedEncodingEnabled && typeInstance.__isXerUseQName())
        {
          this._out.text(this.writeUseQNameValue(typeInstance));
        }
        else
        {
          if (flag1)
            componentTypeInstances = typeInstance.__getXerUseOrderComponentTypeInstances(componentTypeInstances);
          int index = !this._isExtendedEncodingEnabled || !typeInstance.__isXerEmbedValues() ? 0 : 1;
          if (this._isExtendedEncodingEnabled && typeInstance.__isXerEmbedValues() && typeInstance.__getXerEmbedValuesComponent().GetAsn1TypeList() != null)
          {
            this._embedValuesDepth = this._out.getDepth();
            enumerator = typeInstance.__getXerEmbedValuesComponent().GetAsn1TypeList().GetEnumerator();
            this._embedValuesIterator = enumerator;
            this.writeEmbedValuesValue();
          }
          if (componentTypeInstances != null)
          {
            for (; index < componentTypeInstances.Length; ++index)
            {
              Asn1Type asn1Type = componentTypeInstances[index];
              if (asn1Type != null)
              {
                if (!this._isExtendedEncodingEnabled || !asn1Type.__isXerAttribute() && !asn1Type.__isXerAnyAttributes())
                {
                  asn1Type.__write(this);
                  if (this._isExtendedEncodingEnabled && typeInstance.__isXerEmbedValues() && typeInstance.__getXerEmbedValuesComponent().GetAsn1TypeList() != null)
                  {
                    this._embedValuesIterator = enumerator;
                    if (this._embedValuesDepth == -1)
                      this.writeEmbedValuesValue();
                    this._embedValuesDepth = this._out.getDepth();
                  }
                }
              }
              else
                break;
            }
          }
          if (this._isExtendedEncodingEnabled && typeInstance.__isXerEmbedValues() && typeInstance.__getXerEmbedValuesComponent().GetAsn1TypeList() != null)
          {
            this._embedValuesDepth = -1;
            this._embedValuesIterator = (IEnumerator) null;
          }
        }
        if (this._isExtendedEncodingEnabled)
          this.writePiOrComment((Asn1Type) typeInstance, 2);
      }
      if (!flag2)
        return;
      this.writeEndTag((Asn1Type) typeInstance, xerTag);
    }

    protected internal string __encodeConstructedValue(Asn1ConstructedType typeInstance)
    {
      if (typeInstance.__isXerUseQName())
        return this.writeUseQNameValue(typeInstance);
      else
        return "";
    }

    protected internal string __encodeDecimalRealValue(Asn1RealType typeInstance, string mantissaStr, long exponent)
    {
      if (exponent > (long) int.MaxValue)
      {
        throw new Asn1Exception(59, "mantissa '" + (object) mantissaStr + "' and exponent '" + (string) (object) exponent + "' cannot be encoded as DECIMAL in type <" + typeInstance.GetType().FullName + ">");
      }
      else
      {
        if (exponent == 0L)
          return mantissaStr;
        string str;
        try
        {
          if (exponent > 0L)
          {
            StringBuilder stringBuilder = new StringBuilder(mantissaStr);
            char[] chArray = new char[(int) exponent];
            for (int index = 0; index < chArray.Length; ++index)
              chArray[index] = '0';
            stringBuilder.Append(chArray);
            return ((object) stringBuilder).ToString();
          }
          else
          {
            StringBuilder stringBuilder = new StringBuilder(mantissaStr);
            int length = mantissaStr.Length;
            int num1 = length;
            if ((int) mantissaStr[0] == 45)
              --num1;
            int num2 = num1 + (int) exponent;
            if (num2 == 0)
              stringBuilder.Insert(length - num1, "0.");
            else if (num2 > 0)
            {
              stringBuilder.Insert(length + (int) exponent, ".");
            }
            else
            {
              char[] chArray = new char[-num2 + 2];
              chArray[0] = '0';
              chArray[1] = '.';
              for (int index = 2; index < chArray.Length; ++index)
                chArray[index] = '0';
              stringBuilder.Insert(length - num1, chArray);
            }
            str = ((object) stringBuilder).ToString();
          }
        }
        catch (Exception ex)
        {
          throw new Asn1Exception(59, "mantissa '" + (object) mantissaStr + "' and exponent '" + (string) (object) exponent + "' cannot be encoded as DECIMAL in type <" + typeInstance.GetType().FullName + ">");
        }
        return str;
      }
    }

    protected internal void __encodeEnumeratedType(Asn1EnumeratedType typeInstance)
    {
      string xerTag = typeInstance.__getXerTag();
      bool flag = xerTag.Length > 0 && (!typeInstance.__isXerUntagged() && !this._skipTag || this._isRoot);
      this._skipTag = false;
      if (flag)
        this.writeStartTag((Asn1Type) typeInstance, xerTag);
      if (this._isExtendedEncodingEnabled)
        this.writePiOrComment((Asn1Type) typeInstance, 1);
      string str = this.__encodeEnumeratedValue(typeInstance);
      if (this._isExtendedEncodingEnabled && (typeInstance.__isXerText() || typeInstance.__isXerUseNumber()))
        this._out.text(str);
      else
        this._out.emptyTag(str);
      if (this._isExtendedEncodingEnabled)
        this.writePiOrComment((Asn1Type) typeInstance, 2);
      if (!flag)
        return;
      this.writeEndTag((Asn1Type) typeInstance, xerTag);
    }

    protected internal string __encodeEnumeratedValue(Asn1EnumeratedType typeInstance)
    {
      string[] idSet = typeInstance.__getXerIdentifierSet();
      string[] idExtensionSet = typeInstance.__getXerAdditionalIdentifierSet();
      if (!this._isExtendedEncodingEnabled || idSet == null)
      {
        idSet = typeInstance.__getRootIdentifierSet();
        idExtensionSet = typeInstance.__getExtensionIdentifierSet();
      }
      if (this._isExtendedEncodingEnabled && typeInstance.__isXerUseNumber())
        return typeInstance.GetLongValue().ToString();
      string identifier = typeInstance.__getIdentifier(idSet, idExtensionSet);
      if (identifier != null)
        return identifier;
      throw new Asn1Exception(25, string.Concat(new object[4]
      {
        (object) typeInstance.GetLongValue(),
        (object) " is not authorized in type <",
        (object) typeInstance.GetType().FullName,
        (object) ">"
      }));
    }

    protected internal void __encodeGeneralizedTimeType(Asn1GeneralizedTimeType typeInstance)
    {
      string xerTag = typeInstance.__getXerTag();
      bool flag = xerTag.Length > 0 && (!typeInstance.__isXerUntagged() && !this._skipTag || this._isRoot);
      this._skipTag = false;
      if (flag)
        this.writeStartTag((Asn1Type) typeInstance, xerTag);
      if (this._isExtendedEncodingEnabled)
        this.writePiOrComment((Asn1Type) typeInstance, 1);
      string text = this.__encodeGeneralizedTimeValue(typeInstance);
      if (text.Length > 0)
        this._out.text(text);
      if (this._isExtendedEncodingEnabled)
        this.writePiOrComment((Asn1Type) typeInstance, 2);
      if (!flag)
        return;
      this.writeEndTag((Asn1Type) typeInstance, xerTag);
    }

    protected internal string __encodeGeneralizedTimeValue(Asn1GeneralizedTimeType typeInstance)
    {
      string stringValue = typeInstance.GetStringValue(this._isCanonicalizingTimeValues);
      if (stringValue != null && stringValue.Length != 0)
        return stringValue;
      else
        return "";
    }

    protected internal void __encodeIntegerType(Asn1IntegerType typeInstance)
    {
      string xerTag = typeInstance.__getXerTag();
      bool flag = xerTag.Length > 0 && (!typeInstance.__isXerUntagged() && !this._skipTag || this._isRoot);
      this._skipTag = false;
      if (flag)
        this.writeStartTag((Asn1Type) typeInstance, xerTag);
      if (this._isExtendedEncodingEnabled)
        this.writePiOrComment((Asn1Type) typeInstance, 1);
      this._out.text(this.__encodeIntegerValue(typeInstance));
      if (this._isExtendedEncodingEnabled)
        this.writePiOrComment((Asn1Type) typeInstance, 2);
      if (!flag)
        return;
      this.writeEndTag((Asn1Type) typeInstance, xerTag);
    }

    protected internal string __encodeIntegerValue(Asn1IntegerType typeInstance)
    {
      string str = (string) null;
      string[] idSet = typeInstance.__getXerIdentifierSet();
      if (!this._isExtendedEncodingEnabled || idSet == null)
        idSet = typeInstance.__getIdentifierSet();
      if (this._isExtendedEncodingEnabled && typeInstance.__isXerText() && idSet != null)
        str = typeInstance.__getIdentifier(idSet);
      if (str != null)
        return str;
      if (typeInstance.GetBigIntegerValue() == null)
        return typeInstance.GetLongValue().ToString();
      else
        return ((object) typeInstance.GetBigIntegerValue()).ToString();
    }

    protected internal void __encodeNullType(Asn1NullType typeInstance)
    {
      string xerTag = typeInstance.__getXerTag();
      bool flag = xerTag.Length > 0 && (!typeInstance.__isXerUntagged() && !this._skipTag || this._isRoot);
      this._skipTag = false;
      if (flag)
        this.writeStartTag((Asn1Type) typeInstance, xerTag);
      if (this._isExtendedEncodingEnabled)
      {
        this.writePiOrComment((Asn1Type) typeInstance, 1);
        this.writePiOrComment((Asn1Type) typeInstance, 2);
      }
      if (!flag)
        return;
      this.writeEndTag((Asn1Type) typeInstance, xerTag);
    }

    protected internal string __encodeNullValue(Asn1NullType typeInstance)
    {
      return "";
    }

    protected internal void __encodeObjectIdentifierType(Asn1ObjectIdentifierType typeInstance)
    {
      string xerTag = typeInstance.__getXerTag();
      bool flag = xerTag.Length > 0 && (!typeInstance.__isXerUntagged() && !this._skipTag || this._isRoot);
      this._skipTag = false;
      if (flag)
        this.writeStartTag((Asn1Type) typeInstance, xerTag);
      if (this._isExtendedEncodingEnabled)
        this.writePiOrComment((Asn1Type) typeInstance, 1);
      this._out.text(this.__encodeObjectIdentifierValue(typeInstance));
      if (this._isExtendedEncodingEnabled)
        this.writePiOrComment((Asn1Type) typeInstance, 2);
      if (!flag)
        return;
      this.writeEndTag((Asn1Type) typeInstance, xerTag);
    }

    protected internal string __encodeObjectIdentifierValue(Asn1ObjectIdentifierType typeInstance)
    {
      return typeInstance.GetStringValue();
    }

    protected internal void __encodeOctetStringType(Asn1OctetStringType typeInstance)
    {
      string xerTag = typeInstance.__getXerTag();
      bool flag1 = xerTag.Length > 0 && (!typeInstance.__isXerUntagged() && !this._skipTag || this._isRoot);
      this._skipTag = false;
      if (flag1)
        this.writeStartTag((Asn1Type) typeInstance, xerTag);
      if (this._isExtendedEncodingEnabled)
        this.writePiOrComment((Asn1Type) typeInstance, 1);
      if (typeInstance.__isContainingType())
      {
        IEncoder encoder = typeInstance.__getEncoder();
        bool flag2 = encoder != null;
        if (!flag2)
          encoder = (IEncoder) this.getWriter();
        if (typeInstance.GetTypeValue() != null)
        {
          try
          {
            encoder.Encode(typeInstance.GetTypeValue());
            byte[] data = encoder.Data;
            typeInstance.SetValue(data);
            if (!flag2)
              this._out.rawBytes(data);
            else
              this._out.text(ByteArray.ByteArrayToHexString(data, (string) null, -1, true));
          }
          catch (Asn1ValidationException ex)
          {
            throw ex;
          }
          catch (Asn1Exception ex)
          {
            throw new Asn1Exception(51, "type <" + typeInstance.GetTypeValue().GetType().FullName + "> for OCTET SRING type <" + typeInstance.GetType().FullName + "> [internal exception : " + ex.Message + "]");
          }
        }
        else
        {
          byte[] byteArrayValue = typeInstance.GetByteArrayValue();
          if (byteArrayValue == null || byteArrayValue.Length == 0)
            throw new Asn1Exception(48, " for type <" + typeInstance.GetType().FullName + ">");
          if (!flag2)
          {
            if (this._isHexStringFormattingUnresolvedOpenTypes)
              this._out.text(this.__encodeOctetStringValue(typeInstance));
            else
              this._out.rawBytes(byteArrayValue);
          }
          else
            this._out.text(this.__encodeOctetStringValue(typeInstance));
        }
      }
      else
      {
        string text = this.__encodeOctetStringValue(typeInstance);
        if (text.Length > 0)
          this._out.text(text);
      }
      if (this._isExtendedEncodingEnabled)
        this.writePiOrComment((Asn1Type) typeInstance, 2);
      if (!flag1)
        return;
      this.writeEndTag((Asn1Type) typeInstance, xerTag);
    }

    protected internal string __encodeOctetStringValue(Asn1OctetStringType typeInstance)
    {
      byte[] byteArrayValue = typeInstance.GetByteArrayValue();
      if (byteArrayValue == null || byteArrayValue.Length == 0)
        return typeInstance.__isXerDefaultForEmpty() ? " " : "";
      else if (this._isExtendedEncodingEnabled && typeInstance.__isXerBase64())
        return Base64.Encode(byteArrayValue, false);
      else
        return ByteArray.ByteArrayToHexString(byteArrayValue, (string) null, -1, true);
    }

    protected internal void __encodeOpenType(Asn1OpenType typeInstance)
    {
      string xerTag = typeInstance.__getXerTag();
      bool flag = xerTag.Length > 0 && (!typeInstance.__isXerUntagged() && !this._skipTag || this._isRoot);
      this._skipTag = false;
      if (flag)
        this.writeStartTag((Asn1Type) typeInstance, xerTag);
      if (this._isExtendedEncodingEnabled)
        this.writePiOrComment((Asn1Type) typeInstance, 1);
      Asn1Type typeValue = typeInstance.GetTypeValue();
      if (typeValue != null)
      {
        typeValue.__write(this);
      }
      else
      {
        byte[] byteArrayValue = typeInstance.GetByteArrayValue();
        if (byteArrayValue == null || byteArrayValue.Length == 0)
          throw new Asn1Exception(48, " for type <" + typeInstance.GetType().FullName + ">");
        if (this._isHexStringFormattingUnresolvedOpenTypes)
          this._out.text(this.__encodeOpenValue(typeInstance));
        else
          this._out.rawBytes(byteArrayValue);
      }
      if (this._isExtendedEncodingEnabled)
        this.writePiOrComment((Asn1Type) typeInstance, 2);
      if (!flag)
        return;
      this.writeEndTag((Asn1Type) typeInstance, xerTag);
    }

    protected internal string __encodeOpenValue(Asn1OpenType typeInstance)
    {
      byte[] byteArrayValue = typeInstance.GetByteArrayValue();
      if (byteArrayValue == null || byteArrayValue.Length == 0)
        return "";
      if (this._isExtendedEncodingEnabled && typeInstance.__isXerBase64())
        return Base64.Encode(byteArrayValue, false);
      else
        return ByteArray.ByteArrayToHexString(byteArrayValue, (string) null, -1, true);
    }

    protected internal void __encodeRealType(Asn1RealType typeInstance)
    {
      string xerTag = typeInstance.__getXerTag();
      bool flag = xerTag.Length > 0 && (!typeInstance.__isXerUntagged() && !this._skipTag || this._isRoot);
      this._skipTag = false;
      if (flag)
        this.writeStartTag((Asn1Type) typeInstance, xerTag);
      if (this._isExtendedEncodingEnabled)
        this.writePiOrComment((Asn1Type) typeInstance, 1);
      string str = this.__encodeRealValue(typeInstance);
      if (!this._isExtendedEncodingEnabled && char.IsLetter(str[0]))
        this._out.emptyTag(str);
      else if (!typeInstance.__isXerSpecialAsText() && char.IsLetter(str[0]))
        this._out.emptyTag(str);
      else
        this._out.text(str);
      if (this._isExtendedEncodingEnabled)
        this.writePiOrComment((Asn1Type) typeInstance, 2);
      if (!flag)
        return;
      this.writeEndTag((Asn1Type) typeInstance, xerTag);
    }

    protected internal string __encodeRealValue(Asn1RealType typeInstance)
    {
      if (typeInstance.base_().GetIntValue() == 2)
      {
        double doubleValue = typeInstance.GetDoubleValue();
        if (doubleValue == 0.0)
          return "-0".Equals(typeInstance.StringValue) ? "-0" : "0";
        else if (doubleValue == double.PositiveInfinity)
          return this._isExtendedEncodingEnabled && typeInstance.__isXerSpecialAsText() ? "INF" : "PLUS-INFINITY";
        else if (doubleValue == double.NegativeInfinity)
          return this._isExtendedEncodingEnabled && typeInstance.__isXerSpecialAsText() ? "-INF" : "MINUS-INFINITY";
        else if (doubleValue.Equals(double.PositiveInfinity))
        {
          return this._isExtendedEncodingEnabled && typeInstance.__isXerSpecialAsText() ? "NaN" : "NOT-A-NUMBER";
        }
        else
        {
          string str = typeInstance.__canonicalizeRealValue(doubleValue.ToString((IFormatProvider) CultureInfo.InvariantCulture), false);
          int length = str.IndexOf('E');
          string mantissaStr = str.Substring(0, length);
          long exponent = long.Parse(str.Substring(length + 1));
          if (this._isExtendedEncodingEnabled && typeInstance.__isXerDecimal())
            return this.__encodeDecimalRealValue(typeInstance, mantissaStr, exponent);
          else
            return this.__encodeCanonicalRealValue(typeInstance, mantissaStr, exponent);
        }
      }
      else
      {
        long mantissa = typeInstance.Mantissa;
        if (mantissa == 0L)
          return "0";
        long exponent = typeInstance.Exponent;
        if (this._isExtendedEncodingEnabled && typeInstance.__isXerDecimal())
          return this.__encodeDecimalRealValue(typeInstance, mantissa.ToString(), exponent);
        else
          return this.__encodeCanonicalRealValue(typeInstance, mantissa.ToString(), exponent);
      }
    }

    protected internal void __encodeRelativeOIDType(Asn1RelativeOIDType typeInstance)
    {
      string xerTag = typeInstance.__getXerTag();
      bool flag = xerTag.Length > 0 && (!typeInstance.__isXerUntagged() && !this._skipTag || this._isRoot);
      this._skipTag = false;
      if (flag)
        this.writeStartTag((Asn1Type) typeInstance, xerTag);
      if (this._isExtendedEncodingEnabled)
        this.writePiOrComment((Asn1Type) typeInstance, 1);
      string text = this.__encodeRelativeOIDValue(typeInstance);
      if (text.Length > 0)
        this._out.text(text);
      if (this._isExtendedEncodingEnabled)
        this.writePiOrComment((Asn1Type) typeInstance, 2);
      if (!flag)
        return;
      this.writeEndTag((Asn1Type) typeInstance, xerTag);
    }

    protected internal string __encodeRelativeOIDValue(Asn1RelativeOIDType typeInstance)
    {
      return typeInstance.GetStringValue();
    }

    protected internal void __encodeSequenceOfType(Asn1SequenceOfType typeInstance)
    {
      this.encodeConstructedOfType((Asn1ConstructedOfType) typeInstance, false);
    }

    protected internal string __encodeSequenceOfValue(Asn1SequenceOfType typeInstance)
    {
      return this.encodeConstructedOfValue((Asn1ConstructedOfType) typeInstance, false);
    }

    protected internal void __encodeSetOfType(Asn1SetOfType typeInstance)
    {
      this.encodeConstructedOfType((Asn1ConstructedOfType) typeInstance, this._isSortingSetOf);
    }

    protected internal string __encodeSetOfValue(Asn1SetOfType typeInstance)
    {
      return this.encodeConstructedOfValue((Asn1ConstructedOfType) typeInstance, this._isSortingSetOf);
    }

    protected internal void __encodeStringType(Asn1StringType typeInstance)
    {
      string xerTag = typeInstance.__getXerTag();
      string str = (string) null;
      bool flag = xerTag.Length > 0 && (!typeInstance.__isXerUntagged() && !this._skipTag || this._isRoot);
      this._skipTag = false;
      if (this._isExtendedEncodingEnabled && typeInstance.__isXerAnyElement())
        flag = false;
      if (flag)
        this.writeStartTag((Asn1Type) typeInstance, xerTag);
      if (this._isExtendedEncodingEnabled && this._out.isStartTagIncomplete() && typeInstance.__getXerControlCharacterNamespaceUri() != null)
      {
        str = typeInstance.__getXerControlCharacterNamespacePrefix();
        if (str == null)
          str = this._out.getPrefix(typeInstance.__getXerControlCharacterNamespaceUri(), true);
        else
          this._out.setPrefix(str, typeInstance.__getXerControlCharacterNamespaceUri(), false);
      }
      if (this._isExtendedEncodingEnabled)
        this.writePiOrComment((Asn1Type) typeInstance, 1);
      if (this._isExtendedEncodingEnabled && typeInstance.__isXerAnyElement())
      {
        string stringValue = typeInstance.GetStringValue();
        if (stringValue != null && stringValue.Length > 0)
        {
          this._out.text(stringValue);
          if (this._embedValuesDepth != -1 && this._embedValuesDepth == this._out.getDepth())
            this.writeEmbedValuesValue();
        }
      }
      else
      {
        string text = this.__encodeStringValue(typeInstance);
        if (text.Length > 0)
        {
          if (this._isExtendedEncodingEnabled && typeInstance.__isCDATAValue())
            this._out.cdsect(text);
          else
            this._out.text(text, true, str);
        }
      }
      if (this._isExtendedEncodingEnabled)
        this.writePiOrComment((Asn1Type) typeInstance, 2);
      if (!flag)
        return;
      this.writeEndTag((Asn1Type) typeInstance, xerTag);
    }

    protected internal string __encodeStringValue(Asn1StringType typeInstance)
    {
      string stringValue = typeInstance.GetStringValue();
      if (stringValue == null || stringValue.Length == 0)
      {
        return !typeInstance.__isXerDefaultForEmpty() || !typeInstance.__isXerBase64() && !typeInstance.__isXerWhiteSpaceCollapse() ? "" : " ";
      }
      else
      {
        if (!this._isExtendedEncodingEnabled || !typeInstance.__isXerBase64())
          return stringValue;
        try
        {
          return Base64.Encode(Encoding.UTF8.GetBytes(stringValue), false);
        }
        catch (IOException ex)
        {
          throw new Asn1Exception(21, "exception " + ex.Message + " for type <" + typeInstance.GetType().FullName + ">");
        }
      }
    }

    protected internal void __encodeTimeType(Asn1TimeType typeInstance)
    {
      string xerTag = typeInstance.__getXerTag();
      bool flag = xerTag.Length > 0 && (!typeInstance.__isXerUntagged() && !this._skipTag || this._isRoot);
      this._skipTag = false;
      if (flag)
        this.writeStartTag((Asn1Type) typeInstance, xerTag);
      if (this._isExtendedEncodingEnabled)
        this.writePiOrComment((Asn1Type) typeInstance, 1);
      string text = this.__encodeTimeValue(typeInstance);
      if (text.Length > 0)
        this._out.text(text);
      if (this._isExtendedEncodingEnabled)
        this.writePiOrComment((Asn1Type) typeInstance, 2);
      if (!flag)
        return;
      this.writeEndTag((Asn1Type) typeInstance, xerTag);
    }

    protected internal string __encodeTimeValue(Asn1TimeType typeInstance)
    {
      string stringValue = typeInstance.GetStringValue(this._isCanonicalizingTimeValues);
      if (stringValue != null && stringValue.Length != 0)
        return stringValue;
      else
        return "";
    }

    protected internal void __encodeUTCTimeType(Asn1UTCTimeType typeInstance)
    {
      string xerTag = typeInstance.__getXerTag();
      bool flag = xerTag.Length > 0 && (!typeInstance.__isXerUntagged() && !this._skipTag || this._isRoot);
      this._skipTag = false;
      if (flag)
        this.writeStartTag((Asn1Type) typeInstance, xerTag);
      if (this._isExtendedEncodingEnabled)
        this.writePiOrComment((Asn1Type) typeInstance, 1);
      string text = this.__encodeUTCTimeValue(typeInstance);
      if (text.Length > 0)
        this._out.text(text);
      if (this._isExtendedEncodingEnabled)
        this.writePiOrComment((Asn1Type) typeInstance, 2);
      if (!flag)
        return;
      this.writeEndTag((Asn1Type) typeInstance, xerTag);
    }

    protected internal string __encodeUTCTimeValue(Asn1UTCTimeType typeInstance)
    {
      string stringValue = typeInstance.GetStringValue(this._isCanonicalizingTimeValues);
      if (stringValue != null && stringValue.Length != 0)
        return stringValue;
      else
        return "";
    }

    private void encodeConstructedOfType(Asn1ConstructedOfType typeInstance, bool sortingSetOf)
    {
      bool flag1 = false;
      IList list = (IList) null;
      Stream output = (Stream) null;
      MemoryStream memoryStream = (MemoryStream) null;
      string str = (string) null;
      if (typeInstance.__getTypePostDecoder() != null)
      {
        flag1 = !this.isCompatible((object) typeInstance.__getTypePostDecoder());
        if (this._isExtendedEncodingEnabled)
        {
          Asn1Type asn1Type = typeInstance.NewAsn1Type();
          if (asn1Type.__isXerUntagged())
            str = asn1Type.__getXerTag();
        }
      }
      string xerTag = typeInstance.__getXerTag();
      bool flag2 = xerTag.Length > 0 && (!typeInstance.__isXerUntagged() && !this._skipTag || this._isRoot);
      this._skipTag = false;
      if (flag2)
        this.writeStartTag((Asn1Type) typeInstance, xerTag);
      if (this._isExtendedEncodingEnabled)
        this.writePiOrComment((Asn1Type) typeInstance, 1);
      int count = typeInstance.Count;
      if (count > 0)
      {
        if (this._isExtendedEncodingEnabled && typeInstance.__isXerList())
        {
          this._out.textList(this.writeListValue((Asn1Type) typeInstance), true);
        }
        else
        {
          if (sortingSetOf)
          {
            list = (IList) new ArrayList();
            output = this._underlyingOut;
          }
          for (int index1 = 0; index1 < count; ++index1)
          {
            if (sortingSetOf)
            {
              memoryStream = new MemoryStream();
              this._out.changeOutput((Stream) memoryStream);
              this._underlyingOut = (Stream) memoryStream;
            }
            object obj = typeInstance.GetAsn1TypeList()[index1];
            if (flag1 && !(obj is Asn1Type))
              obj = (object) typeInstance.__getElementAt(index1);
            if (obj is Asn1Type)
            {
              ((Asn1Type) obj).__write(this);
            }
            else
            {
              byte[] bytes = (byte[]) obj;
              if (str != null)
              {
                byte[] numArray = (byte[]) obj;
                int sourceIndex = 0;
                int num = numArray.Length;
                for (int index2 = 0; index2 < numArray.Length; ++index2)
                {
                  if ((int) numArray[index2] == 62)
                    sourceIndex = index2 + 1;
                }
                for (int index2 = numArray.Length - 1; index2 >= 0; --index2)
                {
                  if ((int) numArray[index2] == 60)
                    num = index2;
                }
                if (sourceIndex >= num)
                {
                  bytes = new byte[num - sourceIndex];
                  Array.Copy((Array) numArray, sourceIndex, (Array) bytes, 0, num - sourceIndex);
                }
              }
              this._out.rawBytes(bytes);
            }
            if (sortingSetOf)
            {
              this._out.flush();
              ByteArray.addToSortedArray(list, memoryStream.ToArray());
            }
          }
          if (sortingSetOf)
          {
            this._underlyingOut = output;
            this._out.changeOutput(output);
            this._out.rawBytes(ByteArray.getTotalByteArray(list));
            this._out.flush();
          }
        }
      }
      if (this._isExtendedEncodingEnabled)
        this.writePiOrComment((Asn1Type) typeInstance, 2);
      if (!flag2)
        return;
      this.writeEndTag((Asn1Type) typeInstance, xerTag);
    }

    private string encodeConstructedOfValue(Asn1ConstructedOfType typeInstance, bool sortingSetOf)
    {
      if (typeInstance.Count != 0)
        throw new Asn1Exception(39, "not a character encodable value to be encoded in type <" + typeInstance.GetType().FullName + ">");
      else
        return "";
    }

    protected override void encodeImpl(Asn1Type type, Stream outputStream)
    {
      this.init(outputStream);
      this._isRoot = true;
      this._skipTag = false;
      this._embedValuesDepth = -1;
      this._embedValuesIterator = (IEnumerator) null;
      if (this._isEncodingXMLDeclaration)
        this._out.writeXMLDeclaration();
      type.__write(this);
      this.flush();
    }

    protected override void flush()
    {
      this._out.flush();
    }

    public override string GetProperty(string key)
    {
      if (key.Equals("validating"))
        return this._isValidating.ToString();
      if (key.Equals("internalErrorLogDir"))
        return this._internalErrorLogDir;
      if (key.Equals("encodingDefaultValues"))
        return this._isEncodingDefaultValues.ToString();
      if (key.Equals("lineSeparator"))
        return this._lineSeparator;
      if (key.Equals("newlines"))
        return (this._lineSeparator != null && this._lineSeparator.Length > 0).ToString();
      if (key.Equals("indentSpaces"))
        return this._indentSpaces.ToString();
      if (key.Equals("sortingSetOf"))
        return this._isSortingSetOf.ToString();
      if (key.Equals("canonicalizingTimeValues"))
        return this._isCanonicalizingTimeValues.ToString();
      if (key.Equals("encodingXMLDeclaration"))
        return this._isEncodingXMLDeclaration.ToString();
      if (key.Equals("hexStringFormattingUnresolvedOpenTypes"))
        return this._isHexStringFormattingUnresolvedOpenTypes.ToString();
      if (key.Equals("encodingAttributeSingleQuoted"))
        return this._isEncodingAttributeSingleQuoted.ToString();
      if (key.Equals("whitespaceWithEscapesListSeparator"))
        return this._whitespaceWithEscapesListSeparator;
      if (key.Equals("encodingTypeIdentificationForUseTypeChoiceFirstAlt"))
        return this._isEncodingTypeIdentificationForUseTypeChoiceFirstAlt.ToString();
      if (key.Equals("encodingTypeIdentificationForUseTypeChoiceUseUnionAlt"))
        return this._isEncodingTypeIdentificationForUseTypeChoiceUseUnionAlt.ToString();
      if (!key.Equals("overriddenNamespaces") || this._overriddenNamespacesUri == null)
        return (string) null;
      StringBuilder stringBuilder = new StringBuilder();
      IDictionaryEnumerator enumerator = this._overriddenNamespacesUri.GetEnumerator();
      enumerator.MoveNext();
      while (true)
      {
        stringBuilder.Append(enumerator.Key);
        stringBuilder.Append(",");
        if (this._overriddenNamespacesPrefix != null && this._overriddenNamespacesPrefix[enumerator.Key] != null)
        {
          stringBuilder.Append(this._overriddenNamespacesPrefix[enumerator.Key]);
          stringBuilder.Append("=");
        }
        stringBuilder.Append(enumerator.Value);
        if (enumerator.MoveNext())
          stringBuilder.Append(";");
        else
          break;
      }
      return ((object) stringBuilder).ToString();
    }

    public virtual Asn1TypeWriter getWriter()
    {
      Asn1TypeXerWriter asn1TypeXerWriter = new Asn1TypeXerWriter();
      asn1TypeXerWriter._isValidating = this._isValidating;
      asn1TypeXerWriter._lineSeparator = this._lineSeparator;
      asn1TypeXerWriter._indentSpaces = this._indentSpaces;
      asn1TypeXerWriter._isSortingSetOf = this._isSortingSetOf;
      asn1TypeXerWriter._isCanonicalizingTimeValues = this._isCanonicalizingTimeValues;
      asn1TypeXerWriter._isEncodingDefaultValues = this._isEncodingDefaultValues;
      asn1TypeXerWriter._isEncodingXMLDeclaration = false;
      asn1TypeXerWriter._isHexStringFormattingUnresolvedOpenTypes = this._isHexStringFormattingUnresolvedOpenTypes;
      asn1TypeXerWriter._isExtendedEncodingEnabled = this._isExtendedEncodingEnabled;
      asn1TypeXerWriter._isEncodingAttributeSingleQuoted = this._isEncodingAttributeSingleQuoted;
      asn1TypeXerWriter._isEncodingTypeIdentificationForUseTypeChoiceFirstAlt = this._isEncodingTypeIdentificationForUseTypeChoiceFirstAlt;
      asn1TypeXerWriter._isEncodingTypeIdentificationForUseTypeChoiceUseUnionAlt = this._isEncodingTypeIdentificationForUseTypeChoiceUseUnionAlt;
      asn1TypeXerWriter._overriddenNamespacesUri = this._overriddenNamespacesUri;
      asn1TypeXerWriter._overriddenNamespacesPrefix = this._overriddenNamespacesPrefix;
      return (Asn1TypeWriter) asn1TypeXerWriter;
    }

    private string getXerNamespacePrefix(Asn1Type typeInstance)
    {
      string xerNamespaceUri = typeInstance.__getXerNamespaceUri();
      string str = typeInstance.__getXerNamespacePrefix();
      if (xerNamespaceUri != null)
      {
        if (str != null && this._overriddenNamespacesPrefix != null)
          return (string) this._overriddenNamespacesPrefix[(object) xerNamespaceUri];
        if (str != null && str.Length != 0 || !(xerNamespaceUri == "http://www.w3.org/XML/1998/namespace"))
          return str;
        str = "xml";
      }
      return str;
    }

    private string getXerNamespaceUri(Asn1Type typeInstance)
    {
      string xerNamespaceUri = typeInstance.__getXerNamespaceUri();
      if (xerNamespaceUri != null && this._overriddenNamespacesUri != null)
        return (string) this._overriddenNamespacesUri[(object) xerNamespaceUri];
      else
        return xerNamespaceUri;
    }

    protected override void init(Stream output)
    {
      this._out = new XmlAsn1OutputStream(output);
      this._underlyingOut = output;
      this._out.setIndentSpaces(this._indentSpaces);
      this._out.setLineSeparator(this._lineSeparator);
      this._out.setEncodingAttributeSingleQuoted(this._isEncodingAttributeSingleQuoted);
      this._out.setWhitespaceWithEscapesListSeparator(this._whitespaceWithEscapesListSeparator);
      this._out.setUseXML1_1ControlCharacters(false);
    }

    protected bool isCompatible(object reader)
    {
      return reader != null && reader is Asn1TypeXerReader;
    }

    protected override void reset()
    {
      this._out = (XmlAsn1OutputStream) null;
    }

    public override void SetProperty(string key, string property)
    {
      if (!this.PropertyNames().Contains((object) key))
        return;
      if (key.Equals("validating"))
        this._isValidating = bool.Parse(property);
      else if (key.Equals("internalErrorLogDir"))
        this._internalErrorLogDir = property;
      else if (key.Equals("encodingDefaultValues"))
        this._isEncodingDefaultValues = bool.Parse(property);
      else if (key.Equals("lineSeparator"))
        this._lineSeparator = property;
      else if (key.Equals("newlines"))
        this._lineSeparator = "\r\n";
      else if (key.Equals("indentSpaces"))
      {
        try
        {
          this._indentSpaces = int.Parse(property, (IFormatProvider) CultureInfo.InvariantCulture);
        }
        catch (FormatException ex)
        {
          this._indentSpaces = -1;
        }
      }
      else if (key.Equals("sortingSetOf"))
        this._isSortingSetOf = bool.Parse(property);
      else if (key.Equals("canonicalizingTimeValues"))
        this._isCanonicalizingTimeValues = bool.Parse(property);
      else if (key.Equals("encodingXMLDeclaration"))
        this._isEncodingXMLDeclaration = bool.Parse(property);
      else if (key.Equals("hexStringFormattingUnresolvedOpenTypes"))
        this._isHexStringFormattingUnresolvedOpenTypes = bool.Parse(property);
      else if (key.Equals("encodingAttributeSingleQuoted"))
        this._isEncodingAttributeSingleQuoted = bool.Parse(property);
      else if (key.Equals("whitespaceWithEscapesListSeparator"))
        this._whitespaceWithEscapesListSeparator = property;
      else if (key.Equals("encodingTypeIdentificationForUseTypeChoiceFirstAlt"))
        this._isEncodingTypeIdentificationForUseTypeChoiceFirstAlt = bool.Parse(property);
      else if (key.Equals("encodingTypeIdentificationForUseTypeChoiceUseUnionAlt"))
        this._isEncodingTypeIdentificationForUseTypeChoiceUseUnionAlt = bool.Parse(property);
      else if (key.Equals("overriddenNamespaces"))
      {
        string str1 = property;
        char[] chArray = new char[1]
        {
          ';'
        };
        foreach (string str2 in str1.Split(chArray))
        {
          string[] strArray1 = str2.Trim().Split(',');
          if (strArray1.Length == 2)
          {
            string str3 = strArray1[0].Trim();
            string str4 = strArray1[1].Trim();
            if (str3.Length > 0 && str4.Length > 0)
            {
              string str5 = (string) null;
              string[] strArray2 = str4.Split('=');
              string str6;
              if (strArray2.Length == 2)
              {
                str5 = strArray2[0].Trim();
                str6 = strArray2[1].Trim();
              }
              else
                str6 = strArray2[0].Trim();
              if (str6.IndexOf("'") == 0)
                str6 = str6.Substring(1, str6.Length - 2);
              if (str5 != null)
              {
                if (this._overriddenNamespacesPrefix == null)
                  this._overriddenNamespacesPrefix = (IDictionary) new Hashtable();
                this._overriddenNamespacesPrefix.Add((object) str3, (object) str5);
              }
              if (str6 != null)
              {
                if (this._overriddenNamespacesUri == null)
                  this._overriddenNamespacesUri = (IDictionary) new Hashtable();
                this._overriddenNamespacesUri.Add((object) str3, (object) str6);
              }
            }
          }
        }
      }
    }

    private void writeAfterTag(Asn1Type typeInstance)
    {
      this.writePiOrComment(typeInstance, 3);
      if (!this._isExtendedEncodingEnabled || this._embedValuesDepth == -1 || this._embedValuesDepth != this._out.getDepth())
        return;
      this.writeEmbedValuesValue();
    }

    private void writeAnyAttributesValue(Asn1ConstructedOfType typeInstance)
    {
      int count = typeInstance.Count;
      if (count == 0)
        return;
      for (int index = 0; index < count; ++index)
      {
        string stringValue = ((Asn1StringType) typeInstance.__getElementAt(index)).GetStringValue();
        int length1 = stringValue.IndexOf('=');
        if (length1 == -1)
        {
          throw new Asn1Exception(60, "for the attribute '" + stringValue + "' in type <" + typeInstance.GetType().FullName + ">");
        }
        else
        {
          string str1 = stringValue.Substring(0, length1);
          string str2 = stringValue.Substring(length1 + 1);
          if (str2.Length < 2 || (int) str2[0] != 39 && (int) str2[0] != 34 || (int) str2[str2.Length - 1] != 39 && (int) str2[str2.Length - 1] != 34)
          {
            throw new Asn1Exception(60, "for the attribute '" + stringValue + "' in type <" + typeInstance.GetType().FullName + ">");
          }
          else
          {
            string val = str2.Substring(1, str2.Length - 2);
            string ns = (string) null;
            string name = str1;
            int length2 = str1.IndexOf(' ');
            if (length2 != -1)
            {
              ns = str1.Substring(0, length2);
              name = str1.Substring(length2 + 1);
            }
            this._out.attribute(ns, name, val);
          }
        }
      }
    }

    private void writeEmbedValuesValue()
    {
      if (this._embedValuesIterator == null || !this._embedValuesIterator.MoveNext())
        return;
      Asn1UTF8StringType asn1UtF8StringType = (Asn1UTF8StringType) this._embedValuesIterator.Current;
      string str = (string) null;
      if (this._out.isStartTagIncomplete() && asn1UtF8StringType.__getXerControlCharacterNamespaceUri() != null)
      {
        str = asn1UtF8StringType.__getXerControlCharacterNamespacePrefix();
        if (str == null)
          str = this._out.getPrefix(asn1UtF8StringType.__getXerControlCharacterNamespaceUri(), true);
        else
          this._out.setPrefix(str, asn1UtF8StringType.__getXerControlCharacterNamespaceUri(), false);
      }
      if (asn1UtF8StringType.GetStringValue() != null)
        this._out.text(asn1UtF8StringType.GetStringValue(), true, str);
    }

    private void writeEndTag(Asn1Type typeInstance, string tag)
    {
      if (this._isExtendedEncodingEnabled)
      {
        this._out.endTag(this.getXerNamespacePrefix(typeInstance), tag);
        this.writeAfterTag(typeInstance);
      }
      else
        this._out.endTag((string) null, tag);
    }

    private string[] writeListValue(Asn1Type typeInstance)
    {
      Asn1ConstructedOfType constructedOfType = (Asn1ConstructedOfType) typeInstance;
      int count = constructedOfType.Count;
      if (count == 0)
        return (string[]) null;
      string[] strArray = new string[count];
      for (int index = 0; index < count; ++index)
        strArray[index] = constructedOfType.__getElementAt(index).__writeValue(this);
      return strArray;
    }

    private void writePiOrComment(Asn1Type typeInstance, int position)
    {
      IList piOrCommentList = typeInstance.GetPiOrCommentList();
      if (piOrCommentList == null)
        return;
      for (int index = 0; index < piOrCommentList.Count; ++index)
      {
        PiOrComment piOrComment = (PiOrComment) piOrCommentList[index];
        if (piOrComment.Position == position)
          this._out.text(piOrComment.Value);
      }
    }

    private void writeStartTag(Asn1Type typeInstance, string tag)
    {
      if (this._isExtendedEncodingEnabled)
      {
        this.writePiOrComment(typeInstance, 0);
        string xerNamespaceUri = this.getXerNamespaceUri(typeInstance);
        this._out.setPrefix(this.getXerNamespacePrefix(typeInstance), xerNamespaceUri);
        this._out.startTag(xerNamespaceUri, tag);
      }
      else
        this._out.startTag((string) null, tag);
      this._isRoot = false;
    }

    public string writeUseQNameValue(Asn1ConstructedType typeInstance)
    {
      if (typeInstance.Length != 2)
        return "";
      string str = (string) null;
      if (!typeInstance.__isComponentDefined(1))
        throw new Asn1Exception(39, "USE-QNAME mandatory component is not defined in type <" + typeInstance.GetType().FullName + ">");
      string stringValue = ((Asn1StringType) typeInstance.__getDefinedComponentTypeInstance(1)).GetStringValue();
      if (typeInstance.__isComponentDefined(0))
        str = this._out.getPrefix(((Asn1StringType) typeInstance.__getDefinedComponentTypeInstance(0)).GetStringValue(), true);
      if (str != null)
        return str + ":" + stringValue;
      else
        return stringValue;
    }
  }
}
