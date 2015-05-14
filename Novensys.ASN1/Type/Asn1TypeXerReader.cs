// Decompiled with JetBrains decompiler
// Type: Novensys.ASN1.Type.Asn1TypeXerReader
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
  public class Asn1TypeXerReader : Asn1TypeReader
  {
    protected bool _isExtendedEncodingEnabled = false;
    protected bool _isIgnoringNamespace = false;
    private Stream _underlyingIn = (Stream) null;
    public const string KEY_IGNORING_NAMESPACE = "ignoringNamespace";
    private Asn1ConstructedOfType _embedValuesComponent;
    private int _embedValuesDepth;
    private XmlAsn1InputStream _in;
    private bool _isRoot;
    private bool _keepingContent;
    private bool _nextValueReady;
    private bool _skipTag;
    private int _usedBytes;
    private int _usedChars;

    protected internal void __decodeBitStringType(Asn1BitStringType typeInstance)
    {
      string xerTag = typeInstance.__getXerTag();
      int depth = -1;
      bool flag1 = xerTag.Length > 0 && (!typeInstance.__isXerUntagged() && !this._skipTag || this._isRoot);
      this._skipTag = false;
      if (flag1)
      {
        if (this.readStartTag((Asn1Type) typeInstance, xerTag))
        {
          this._in.nextToken();
          this.readAfterTag((Asn1Type) typeInstance);
          if (this._isExtendedEncodingEnabled && typeInstance.__isXerDefaultForEmpty())
            return;
          this.__decodeBitStringValue(typeInstance, "");
          return;
        }
        else
          depth = this._in.getDepth();
      }
      bool flag2 = false;
      bool flag3 = typeInstance.__getEncoder() != null;
      string text;
      if (typeInstance.__isContainingType() && !flag3)
      {
        this._in.setKeepContent(true);
        this.moveToValue((Asn1Type) typeInstance, true);
        this._nextValueReady = false;
        if (this._in.getEventType() == 4)
        {
          text = this._in.getText();
        }
        else
        {
          this._in.readOuterXml();
          text = this._in.getKeptContent();
          this._in.setKeepContent(false);
          flag2 = true;
        }
        typeInstance.__setDecoder((IDecoder) this.getReader());
        this._isContentToBeResolved = true;
      }
      else
      {
        this.moveToValue((Asn1Type) typeInstance, true);
        this._nextValueReady = false;
        if (this._in.getEventType() == 3)
        {
          if (!this._in.getName().Equals(xerTag))
          {
            throw new Asn1Exception(35, "expecting a END_TAG </" + xerTag + "> in type " + typeInstance.GetType().FullName + this.getPositionMessage());
          }
          else
          {
            this.readAfterTag((Asn1Type) typeInstance);
            this.__decodeBitStringValue(typeInstance, "");
            return;
          }
        }
        else if (this._in.getEventType() == 2)
        {
          if (typeInstance.__getIdentifierSet() == null || !typeInstance.__isXerEmptyElementEnabled())
            throw new Asn1Exception(38, "empty elements are not authorized in type " + typeInstance.GetType().FullName + this.getPositionMessage());
          StringBuilder stringBuilder = new StringBuilder();
          while (this.moveToTag((Asn1Type) typeInstance, true) == 2)
          {
            if (!this._in.isEmptyElementTag())
              throw new Asn1Exception(38, "expecting empty elements in type " + typeInstance.GetType().FullName + this.getPositionMessage());
            this._in.next();
            if (stringBuilder.Length > 0)
              stringBuilder.Append(" ");
            stringBuilder.Append(this._in.getName());
            this.moveToValue((Asn1Type) typeInstance, true);
            this._nextValueReady = false;
          }
          text = ((object) stringBuilder).ToString();
        }
        else
        {
          text = this.readText((Asn1Type) typeInstance);
          if (typeInstance.__isContainingType())
          {
            if (text.Trim().Length == 0)
              throw new Asn1Exception(37, "contained type should be encoded as an 'xmlbstring' for BIT STRING type " + typeInstance.GetType().FullName + this.getPositionMessage());
            typeInstance.__setDecoder((IDecoder) this.getReader());
            this._isContentToBeResolved = true;
          }
        }
      }
      if (flag1)
        this.readEndTag((Asn1Type) typeInstance, xerTag, depth);
      if (flag2)
        this.__decodeBitStringXMLTypedValue(typeInstance, text);
      else
        this.__decodeBitStringValue(typeInstance, text);
    }

    protected internal void __decodeBitStringValue(Asn1BitStringType typeInstance, string text)
    {
      BitString aBitString = new BitString();
      if (text.Trim().Length > 0)
      {
        int bitIndex = 0;
        if (!this._isExtendedEncodingEnabled || char.IsDigit(text[0]))
        {
          foreach (char c in text.ToCharArray())
          {
            switch (c)
            {
              case '0':
                aBitString.setFalse(bitIndex);
                ++bitIndex;
                break;
              case '1':
                aBitString.setTrue(bitIndex);
                ++bitIndex;
                break;
              default:
                if (!char.IsWhiteSpace(c))
                  throw new Asn1Exception(39, string.Concat(new object[4]
                  {
                    (object) c,
                    (object) " is incorrect for BIT STRING type ",
                    (object) typeInstance.GetType().FullName,
                    (object) this.getPositionMessage()
                  }));
                else
                  break;
            }
          }
          typeInstance.__setValue(aBitString);
        }
        else
        {
          if (typeInstance.__getIdentifierSet() == null)
            throw new Asn1Exception(38, "text elements are not authorized in type " + typeInstance.GetType().FullName + this.getPositionMessage());
          string[] idSet = typeInstance.__getXerIdentifierSet() ?? typeInstance.__getIdentifierSet();
          if (!this._isExtendedEncodingEnabled)
          {
            throw new Asn1Exception(38, "'" + text + "' text or empty element encoding is not enabled for BIT STRING type " + typeInstance.GetType().FullName + this.getPositionMessage());
          }
          else
          {
            string str = text;
            char[] chArray = new char[4]
            {
              ' ',
              '\t',
              '\n',
              '\r'
            };
            foreach (string identifier in str.Split(chArray))
              typeInstance.__setValue(identifier, idSet);
          }
        }
      }
      else
        typeInstance.__setValue(aBitString);
    }

    protected internal void __decodeBitStringXMLTypedValue(Asn1BitStringType typeInstance, string text)
    {
      typeInstance.SetValue(Encoding.UTF8.GetBytes(text));
    }

    protected internal void __decodeBooleanType(Asn1BooleanType typeInstance)
    {
      string xerTag = typeInstance.__getXerTag();
      int depth = -1;
      bool flag = xerTag.Length > 0 && (!typeInstance.__isXerUntagged() && !this._skipTag || this._isRoot);
      this._skipTag = false;
      if (flag)
      {
        if (this.readStartTag((Asn1Type) typeInstance, xerTag))
        {
          if (!this._isExtendedEncodingEnabled || !typeInstance.__isXerDefaultForEmpty())
            throw new Asn1Exception(35, "empty value is not authorized in type " + typeInstance.GetType().FullName + this.getPositionMessage());
          this._in.nextToken();
          this.readAfterTag((Asn1Type) typeInstance);
          return;
        }
        else
          depth = this._in.getDepth();
      }
      string text;
      if ((!this._isExtendedEncodingEnabled || !typeInstance.__isXerText()) && (this._in.getEventType() == 2 && this._in.isEmptyElementTag()))
      {
        text = this._in.getName();
        this._in.next();
      }
      else if (this._isExtendedEncodingEnabled && typeInstance.__isXerText())
      {
        text = this.readText((Asn1Type) typeInstance);
      }
      else
      {
        this.moveToValue((Asn1Type) typeInstance, true);
        this._nextValueReady = false;
        if (this._in.getEventType() == 3)
        {
          if (!this._isExtendedEncodingEnabled || !typeInstance.__isXerDefaultForEmpty())
            throw new Asn1Exception(35, "empty value is not authorized in type " + typeInstance.GetType().FullName + this.getPositionMessage());
          this.readAfterTag((Asn1Type) typeInstance);
          return;
        }
        else
        {
          if (this.moveToTag((Asn1Type) typeInstance, true) != 2 || !this._in.isEmptyElementTag())
            throw new Asn1Exception(36, "expecting an empty element tag in type " + typeInstance.GetType().FullName + this.getPositionMessage());
          text = this._in.getName();
          this._in.next();
        }
      }
      if (flag)
        this.readEndTag((Asn1Type) typeInstance, xerTag, depth);
      this.__decodeBooleanValue(typeInstance, text);
    }

    protected internal void __decodeBooleanValue(Asn1BooleanType typeInstance, string text)
    {
      if (text.Length == 0)
      {
        if (this._isExtendedEncodingEnabled && !typeInstance.__isXerDefaultForEmpty())
          throw new Asn1Exception(35, "empty value is not authorized in type " + typeInstance.GetType().FullName + this.getPositionMessage());
      }
      else
      {
        string[] xerIdentifierSet = typeInstance.__getXerIdentifierSet();
        if (!this._isExtendedEncodingEnabled || xerIdentifierSet == null)
        {
          if ("true".Equals(text))
            typeInstance.SetValue(true);
          else if ("false".Equals(text))
            typeInstance.SetValue(false);
          else if (this._isExtendedEncodingEnabled && "1".Equals(text))
            typeInstance.SetValue(true);
          else if (!this._isExtendedEncodingEnabled || !"0".Equals(text))
            throw new Asn1Exception(36, "'" + text + "' is incorrect (expected true or false) in type " + typeInstance.GetType().FullName + this.getPositionMessage());
          else
            typeInstance.SetValue(false);
        }
        else
        {
          string str = text;
          if (this._isExtendedEncodingEnabled && typeInstance.__isXerText())
            str = XmlAsn1InputStream.escapeXerIdentifier(text);
          if (xerIdentifierSet[1].Equals(str))
            typeInstance.SetValue(true);
          else if (xerIdentifierSet[0].Equals(str))
            typeInstance.SetValue(false);
          else if ("1".Equals(str))
            typeInstance.SetValue(true);
          else if (!"0".Equals(str))
            throw new Asn1Exception(36, "'" + str + "' is incorrect (expected " + xerIdentifierSet[1] + " or " + xerIdentifierSet[0] + ") in type " + typeInstance.GetType().FullName + this.getPositionMessage());
          else
            typeInstance.SetValue(false);
        }
      }
    }

    protected internal void __decodeChoiceType(Asn1ChoiceType typeInstance)
    {
      string xerTag = typeInstance.__getXerTag();
      int depth = -1;
      string str = (string) null;
      string text = (string) null;
      Asn1Type asn1Type = (Asn1Type) null;
      bool flag1 = false;
      typeInstance.__initXerFirstComponentIndex();
      bool flag2 = xerTag.Length > 0 && (!typeInstance.__isXerUntagged() && !this._skipTag);
      this._skipTag = false;
      if (flag2)
      {
        if (this.readStartTag((Asn1Type) typeInstance, xerTag) && this._in.getAttributeCount() == 0)
        {
          if (this._isExtendedEncodingEnabled && typeInstance.__isXerUseType())
            ((Asn1ConstructedType) typeInstance).__getMatchingComponent(typeInstance.__getXerFirstComponentIndex()).__readValue(this, "");
          else if (!this._isExtendedEncodingEnabled || !typeInstance.__isXerDefaultForEmpty())
            throw new Asn1Exception(35, "empty value is not authorized in type " + typeInstance.GetType().FullName + this.getPositionMessage());
          this._in.nextToken();
          this.readAfterTag((Asn1Type) typeInstance);
          return;
        }
        else
        {
          depth = this._in.getDepth();
          if (this._isExtendedEncodingEnabled && this._in.getAttributeCount() > 0 && (typeInstance.__isXerUseType() || typeInstance.__isXerUseUnion()))
          {
            str = this._in.getAttributeValue(typeInstance.__getXerControlNamespaceUri(), "type");
            string attributeValueValue = this._in.getAttributeValueValue(str);
            string attributeValueNamespace = this._in.getAttributeValueNamespace(str);
            if (typeInstance.__isXerUseType() && str != null)
              asn1Type = typeInstance.__getMatchingAmbiguousComponent(str);
            if (asn1Type != null)
            {
              flag1 = true;
            }
            else
            {
              if (str != null)
              {
                asn1Type = ((Asn1ConstructedType) typeInstance).__getMatchingComponent(attributeValueValue, !this._isIgnoringNamespace ? attributeValueNamespace : (string) null);
                if (asn1Type == null && typeInstance.__isXerUseType())
                {
                  asn1Type = typeInstance.__getMatchingUseUnionComponent(attributeValueValue, !this._isIgnoringNamespace ? attributeValueNamespace : (string) null);
                  if (asn1Type == null)
                  {
                    this._in.setKeepContent(true);
                    this._keepingContent = true;
                    this.moveToValue((Asn1Type) typeInstance, true);
                    this._nextValueReady = false;
                  }
                }
              }
              this._skipTag = true;
            }
          }
          else
          {
            if (typeInstance.IsExtensible() || this._isExtendedEncodingEnabled && typeInstance.__containsXerAnyElementComponent())
            {
              this._in.setKeepContent(true);
              this._keepingContent = true;
            }
            if (!this._isExtendedEncodingEnabled || !typeInstance.__isXerUseType())
            {
              this.moveToValue((Asn1Type) typeInstance, true);
              this._nextValueReady = false;
            }
          }
        }
      }
      else
      {
        if (typeInstance.__isXerUntagged() && this._isRoot)
          this._isRoot = false;
        if (typeInstance.IsExtensible() || this._isExtendedEncodingEnabled && typeInstance.__containsXerAnyElementComponent())
        {
          this._in.setKeepContent(true);
          this._keepingContent = true;
        }
        if (typeInstance.__isXerUseUnion())
        {
          this.moveToValue((Asn1Type) typeInstance, true);
          this._nextValueReady = false;
        }
      }
      if (this._in.getEventType() == 3 && str == null)
      {
        if (this._isExtendedEncodingEnabled && typeInstance.__isXerUseType())
          ((Asn1ConstructedType) typeInstance).__getMatchingComponent(typeInstance.__getXerFirstComponentIndex()).__readValue(this, "");
        else if (typeInstance.__isXerUseUnion())
          this.__decodeChoiceValue(typeInstance, "");
        else if (!this._isExtendedEncodingEnabled || !typeInstance.__isXerDefaultForEmpty())
          throw new Asn1Exception(35, "empty value is not authorized in type " + typeInstance.GetType().FullName + this.getPositionMessage());
        this.readAfterTag((Asn1Type) typeInstance);
        this._in.setKeepContent(false);
      }
      else
      {
        if (str == null)
        {
          if (this._isExtendedEncodingEnabled && typeInstance.__isXerUseType())
          {
            asn1Type = ((Asn1ConstructedType) typeInstance).__getMatchingComponent(typeInstance.__getXerFirstComponentIndex());
            this._skipTag = true;
          }
          else if (this._isExtendedEncodingEnabled && typeInstance.__isXerUseUnion() && (this._in.getEventType() == 4 || this._in.getEventType() == 5))
          {
            text = this.readText((Asn1Type) typeInstance);
          }
          else
          {
            str = this.moveToTag((Asn1Type) typeInstance, true) == 2 ? this._in.getName() : (string) null;
            if (str == null)
              throw new Asn1Exception(49, "START_TAG is expected for CHOICE type " + typeInstance.GetType().FullName);
            asn1Type = ((Asn1ConstructedType) typeInstance).__getMatchingComponent(str, !this._isIgnoringNamespace ? this._in.getNamespace() : (string) null);
          }
        }
        else if (flag1)
          text = this.readText((Asn1Type) typeInstance);
        if (text != null)
        {
          this._in.setKeepContent(false);
          if (asn1Type != null)
            asn1Type.__readValue(this, text);
          else
            this.__decodeChoiceValue(typeInstance, text);
        }
        else if (asn1Type != null)
        {
          this._in.setKeepContent(false);
          this._keepingContent = false;
          asn1Type.__read(this);
        }
        else
        {
          Asn1Type elementComponent = typeInstance.__getXerAnyElementComponent();
          if (elementComponent != null)
          {
            this._keepingContent = true;
            elementComponent.__read(this);
            this._keepingContent = false;
          }
          else if (!typeInstance.IsExtensible() && (!this._isExtendedEncodingEnabled || !typeInstance.__isXerUseType()))
          {
            throw new Asn1Exception(49, str + " for CHOICE type " + typeInstance.GetType().FullName + " (tag = " + str + "/namespace = " + this._in.getNamespace() + ")");
          }
          else
          {
            this._in.readOuterXml();
            string keptContent = this._in.getKeptContent();
            this._in.setKeepContent(false);
            this._keepingContent = false;
            if (keptContent != null)
              typeInstance.__addUnknownExtension(Encoding.UTF8.GetBytes(keptContent));
          }
        }
        if (flag2)
          this.readEndTag((Asn1Type) typeInstance, xerTag, depth);
      }
    }

    protected internal void __decodeChoiceValue(Asn1ChoiceType typeInstance, string text)
    {
      if (!typeInstance.__isXerUseUnion() && !typeInstance.__isXerUseType())
      {
        throw new Asn1Exception(38, "for decoding '" + text + "' encoding instruction is not USE-UNION in CHOICE type " + typeInstance.GetType().FullName + this.getPositionMessage());
      }
      else
      {
        if (typeInstance.__readAmbiguousValue(this, text) != null)
          return;
        throw new Asn1Exception(38, "no alternatives match for decoding '" + text + "' in CHOICE type " + typeInstance.GetType().FullName + this.getPositionMessage());
      }
    }

    protected internal void __decodeConstructedOfType(Asn1ConstructedOfType typeInstance)
    {
      Asn1TypeReader asn1TypeReader = (Asn1TypeReader) null;
      Asn1Type asn1Type1 = (Asn1Type) null;
      string xerTag1 = (string) null;
      bool flag1 = this._isExtendedEncodingEnabled && typeInstance.__containsXerAnyElementComponent();
      if (typeInstance.__isPostDecoding() && !typeInstance.__isXerList())
      {
        asn1TypeReader = this.getReader();
        typeInstance.__setTypePostDecoder((IDecoder) asn1TypeReader);
        if (this._isExtendedEncodingEnabled)
        {
          Asn1Type asn1Type2 = typeInstance.NewAsn1Type();
          if (asn1Type2.__isXerUntagged() && !(asn1Type2 is Asn1ChoiceType))
            asn1Type1 = asn1Type2;
        }
      }
      string xerTag2 = typeInstance.__getXerTag();
      bool flag2 = xerTag2.Length > 0 && (!typeInstance.__isXerUntagged() && !this._skipTag || this._isRoot);
      this._skipTag = false;
      if (flag2 && this.readStartTag((Asn1Type) typeInstance, xerTag2))
      {
        this._in.nextToken();
        this.readAfterTag((Asn1Type) typeInstance);
      }
      else
      {
        int depth = this._in.getDepth();
        if (this._isExtendedEncodingEnabled && typeInstance.__isXerList())
        {
          string text = this.readText((Asn1Type) typeInstance);
          this.__decodeConstructedOfValue(typeInstance, text);
        }
        else
        {
          if (!this._keepingContent && (asn1TypeReader != null || flag1))
          {
            this._in.setKeepContent(true);
            this._keepingContent = true;
          }
          if (!typeInstance.__isXerUntagged())
          {
            this.moveToValue((Asn1Type) typeInstance, true);
            this._nextValueReady = false;
          }
          if (this._in.getEventType() == 2)
            xerTag1 = this._in.getName();
          for (; xerTag1 != null; xerTag1 = this._in.getEventType() != 2 ? (string) null : this._in.getName())
          {
            Asn1Type asn1Type2 = !this._isExtendedEncodingEnabled ? typeInstance.__getMatchingElement() : typeInstance.__getMatchingElement(xerTag1, !this._isIgnoringNamespace ? this._in.getNamespace() : (string) null);
            if (asn1Type2 == null)
            {
              this._nextValueReady = true;
              break;
            }
            else
            {
              if (asn1TypeReader != null)
              {
                this._in.readOuterXml();
                string s = !this._isExtendedEncodingEnabled ? this._in.getKeptContent() : (asn1Type1 == null ? this._in.getKeptContent((string) null, (string) null, (string) null) : this._in.getKeptContent(asn1Type1.__getXerTag(), asn1Type1.__getXerNamespaceUri(), asn1Type1.__getXerNamespacePrefix()));
                this._in.setKeepContent(false);
                this._keepingContent = false;
                typeInstance.GetAsn1TypeList()[typeInstance.GetAsn1TypeList().Count - 1] = (object) Encoding.UTF8.GetBytes(s);
              }
              else
              {
                if (!this._keepingContent)
                {
                  this._in.setKeepContent(false);
                  this._keepingContent = false;
                }
                asn1Type2.__read(this);
              }
              if (!this._keepingContent && (asn1TypeReader != null || flag1))
              {
                this._in.setKeepContent(true);
                this._keepingContent = true;
              }
              if (this._in.getDepth() > depth || this._in.getDepth() == depth && this._isExtendedEncodingEnabled && !flag2)
              {
                this.moveToValue((Asn1Type) typeInstance, true);
                this._nextValueReady = false;
              }
              if (!this._keepingContent && (asn1TypeReader != null || flag1))
              {
                this._in.setKeepContent(true);
                this._keepingContent = true;
              }
            }
          }
          this._in.setKeepContent(false);
          this._keepingContent = false;
        }
        if (flag2)
          this.readEndTag((Asn1Type) typeInstance, xerTag2, depth);
      }
    }

    protected internal void __decodeConstructedOfValue(Asn1ConstructedOfType typeInstance, string text)
    {
      if (typeInstance.__isXerList())
      {
        if (text.Length == 0)
          return;
        string str = text.Trim();
        char[] chArray = new char[4]
        {
          ' ',
          '\t',
          '\n',
          '\r'
        };
        foreach (string text1 in str.Split(chArray))
          typeInstance.__getMatchingElement().__readValue(this, text1);
      }
      else
      {
        if (!typeInstance.__containsXerAnyElementComponent())
          return;
        typeInstance.__getMatchingElement().__readValue(this, text);
      }
    }

    protected internal void __decodeConstructedType(Asn1ConstructedType typeInstance)
    {
      string xerTag1 = typeInstance.__getXerTag();
      string xerTag2 = (string) null;
      bool flag1 = false;
      bool notOrdered = this._isExtendedEncodingEnabled && (typeInstance.__isXerUseOrder() || typeInstance.__isXerEnclosingTypeUseOrder());
      bool keepContent = this._isExtendedEncodingEnabled && typeInstance.__containsXerAnyElementComponent();
      bool flag2 = this._isExtendedEncodingEnabled && typeInstance.__containsXerAnyAttributesComponent();
      Asn1ConstructedOfType constructedOfType = (Asn1ConstructedOfType) null;
      bool flag3 = xerTag1.Length > 0 && (!typeInstance.__isXerUntagged() && !this._skipTag || this._isRoot);
      this._skipTag = false;
      if (flag3)
        this.readStartTag((Asn1Type) typeInstance, xerTag1);
      int depth = this._in.getDepth();
      typeInstance.__initXerFirstComponentIndex();
      Asn1ConstructedOfType typeInstance1 = (Asn1ConstructedOfType) null;
      if (flag2)
        typeInstance1 = typeInstance.__getXerAnyAttributesComponent();
      if (this._in.getEventType() == 2 && !typeInstance.__isXerUntagged())
      {
        int attributeCount = this._in.getAttributeCount();
        if (!this._isExtendedEncodingEnabled && attributeCount > 0)
          throw new Asn1Exception(49, "attributes are not supported in Basic XER for SEQUENCE/SET type " + typeInstance.GetType().FullName);
        for (int index = 0; index < attributeCount; ++index)
        {
          string attributeName = this._in.getAttributeName(index);
          if ((this._in.getAttributeNamespace(index) != typeInstance.__getXerControlNamespaceUri() || !attributeName.Equals("type")) && (this._in.getAttributeNamespace(index).Length != 0 || !attributeName.Equals("type")))
          {
            string attributeValue = this._in.getAttributeValue(index);
            if (!typeInstance.__isXerUseNil() || (this._in.getAttributeNamespace(index) != typeInstance.__getXerControlNamespaceUri() || !attributeName.Equals("nil")) && (this._in.getAttributeNamespace(index).Length != 0 || typeInstance.__getXerControlNamespaceUri() != "urn:oid:2.1.5.2.0.1" || !attributeName.Equals("nil")))
            {
              Asn1Type matchingComponent = typeInstance.__getMatchingComponent(attributeName, !this._isIgnoringNamespace ? this._in.getAttributeNamespace(index) : (string) null, true);
              if (matchingComponent == null)
              {
                string str1;
                if (this._in.getAttributeNamespace(index).Length <= 0)
                  str1 = attributeName + "='" + attributeValue + "'";
                else
                  str1 = this._in.getAttributeNamespace(index) + " " + attributeName + "='" + attributeValue + "'";
                string str2 = str1;
                if (typeInstance1 == null)
                {
                  if (!typeInstance.IsExtensible() && this._in.getAttributeNamespace(index) != typeInstance.__getXerControlNamespaceUri() && (this._in.getAttributeNamespace(index).Length != 0 || typeInstance.__getXerControlNamespaceUri() != "urn:oid:2.1.5.2.0.1"))
                    throw new Asn1Exception(49, "for SEQUENCE/SET type " + typeInstance.GetType().FullName + " (attribute = " + attributeName + "/namespace = " + this._in.getAttributeNamespace(index) + ")");
                  else
                    typeInstance.__addUnknownExtension(Encoding.UTF8.GetBytes(str2));
                }
                else
                  this.readAnyAttributesValue(typeInstance1, str2);
              }
              else
                matchingComponent.__readValue(this, attributeValue);
            }
            else if (attributeValue.Equals("true") || attributeValue.Equals("1"))
              flag1 = true;
          }
        }
        if (this._in.isEmptyElementTag() && !typeInstance.__isXerUntagged())
        {
          if (this._isExtendedEncodingEnabled && typeInstance.__isXerDefaultForEmpty())
          {
            if (!typeInstance.__isXerUseNil() || !flag1)
              typeInstance.__setDefaultForEmptyComponents();
          }
          else if (typeInstance.__isXerUseNil() && !flag1)
          {
            Asn1Type optionalComponent = typeInstance.__getXerUseNilOptionalComponent();
            this._skipTag = true;
            optionalComponent.__readValue(this, "");
          }
          this._in.nextToken();
          this.readAfterTag((Asn1Type) typeInstance);
          return;
        }
        else
          xerTag2 = (string) null;
      }
      if (this._isExtendedEncodingEnabled && typeInstance.__isXerUseNil())
      {
        if (!flag1)
        {
          this.moveToValue((Asn1Type) typeInstance, true);
          if (this._in.getEventType() == 3 && typeInstance.__isXerDefaultForEmpty())
          {
            typeInstance.__setDefaultForEmptyComponents();
          }
          else
          {
            if (typeInstance.__isXerEmbedValues())
            {
              Asn1ConstructedOfType embedValuesComponent = typeInstance.__getXerEmbedValuesComponent();
              this._embedValuesDepth = depth;
              this._embedValuesComponent = embedValuesComponent;
              this.readEmbedValuesValue(this._in.getEventType() != 2 && this._in.getEventType() != 3 ? this.readText((Asn1Type) typeInstance, true, false) : "");
            }
            Asn1Type optionalComponent = typeInstance.__getXerUseNilOptionalComponent();
            this._skipTag = true;
            optionalComponent.__read(this);
          }
        }
      }
      else
      {
        if (!this._keepingContent && (typeInstance.IsExtensible() || keepContent))
        {
          this._in.setKeepContent(true);
          this._keepingContent = true;
        }
        if (!typeInstance.__isXerUntagged())
        {
          this.moveToValue((Asn1Type) typeInstance, true);
          this._nextValueReady = false;
        }
        if (this._isExtendedEncodingEnabled && this._in.getEventType() == 3 && typeInstance.__isXerDefaultForEmpty())
          typeInstance.__setDefaultForEmptyComponents();
        else if (this._isExtendedEncodingEnabled && !typeInstance.__isXerEmbedValues() && (this._in.getEventType() == 4 || this._in.getEventType() == 5))
        {
          this._in.setKeepContent(false);
          string text = this.readText((Asn1Type) typeInstance);
          this.__decodeConstructedValue(typeInstance, text);
        }
        else
        {
          if (this._isExtendedEncodingEnabled && typeInstance.__isXerEmbedValues())
          {
            constructedOfType = typeInstance.__getXerEmbedValuesComponent();
            this._embedValuesDepth = depth;
            this._embedValuesComponent = constructedOfType;
            string text;
            if (this._in.getEventType() == 2 || this._in.getEventType() == 3)
            {
              text = "";
            }
            else
            {
              text = this.readText((Asn1Type) typeInstance, true, keepContent);
              if (!this._keepingContent)
                this._in.setKeepContent(false);
            }
            this.readEmbedValuesValue(text);
          }
          if (this._in.getEventType() == 2)
            xerTag2 = this._in.getName();
          for (; xerTag2 != null; xerTag2 = this._in.getEventType() != 2 ? (string) null : this._in.getName())
          {
            Asn1Type matchingComponent = typeInstance.__getMatchingComponent(xerTag2, !this._isIgnoringNamespace ? this._in.getNamespace() : (string) null, notOrdered);
            if (matchingComponent != null)
            {
              this._in.setKeepContent(false);
              this._keepingContent = false;
              matchingComponent.__read(this);
              if (notOrdered)
                typeInstance.__addXerUseOrderComponent(matchingComponent.TypeName);
            }
            else if (keepContent)
            {
              Asn1Type elementComponent = typeInstance.__getXerAnyElementComponent();
              if (elementComponent != null)
              {
                this._keepingContent = true;
                elementComponent.__read(this);
                this._keepingContent = false;
              }
            }
            else if (typeInstance.IsExtensible())
            {
              this._in.readOuterXml();
              string keptContent = this._in.getKeptContent();
              this._in.setKeepContent(false);
              this._keepingContent = false;
              if (keptContent != null)
                typeInstance.__addUnknownExtension(Encoding.UTF8.GetBytes(keptContent));
            }
            else if (!typeInstance.__isXerUntagged())
            {
              throw new Asn1Exception(49, "for SEQUENCE/SET type " + typeInstance.GetType().FullName + " (tag = " + xerTag2 + "/namespace = " + this._in.getNamespace() + ")");
            }
            else
            {
              this._nextValueReady = true;
              break;
            }
            if (!this._keepingContent && (typeInstance.IsExtensible() || keepContent))
            {
              this._in.setKeepContent(true);
              this._keepingContent = true;
            }
            if (this._in.getDepth() > depth || this._in.getDepth() == depth && this._isExtendedEncodingEnabled && !flag3)
            {
              this.moveToValue((Asn1Type) typeInstance, true);
              this._nextValueReady = false;
            }
            if (this._isExtendedEncodingEnabled && typeInstance.__isXerEmbedValues())
            {
              this._embedValuesComponent = constructedOfType;
              if (this._embedValuesDepth == -1)
              {
                this.moveToValue((Asn1Type) typeInstance, true, false);
                this._nextValueReady = false;
                string text;
                if (this._in.getEventType() == 2 || this._in.getEventType() == 3)
                {
                  text = "";
                }
                else
                {
                  text = this.readText((Asn1Type) typeInstance, true, keepContent);
                  if (!this._keepingContent)
                    this._in.setKeepContent(false);
                }
                this.readEmbedValuesValue(text);
              }
              this._embedValuesDepth = depth;
            }
            if (!this._keepingContent && (typeInstance.IsExtensible() || keepContent))
            {
              this._in.setKeepContent(true);
              this._keepingContent = true;
            }
          }
          if (typeInstance.__isXerEmbedValues())
          {
            this._embedValuesComponent = (Asn1ConstructedOfType) null;
            this._embedValuesDepth = -1;
          }
          this._in.setKeepContent(false);
          this._keepingContent = false;
          typeInstance.__ensureUntaggedComponentsDefined();
        }
      }
      if (!flag3)
        return;
      this.readEndTag((Asn1Type) typeInstance, xerTag1, depth);
    }

    protected internal void __decodeConstructedValue(Asn1ConstructedType typeInstance, string text)
    {
      if (text.Length == 0)
        return;
      if (typeInstance.__isXerUseQName())
      {
        if (typeInstance.Length == 2)
        {
          string prefix = (string) null;
          string text1 = (string) null;
          string text2 = text;
          int length = text.IndexOf(':');
          if (length != -1)
          {
            prefix = text.Substring(0, length);
            text2 = text.Substring(length + 1);
          }
          if (prefix != null)
            text1 = this._in.getNamespace(prefix);
          if (text1 != null)
            typeInstance.__getMatchingComponent(0).__readValue(this, text1);
          typeInstance.__getMatchingComponent(1).__readValue(this, text2);
        }
      }
      else
      {
        Asn1Type untaggedComponent = typeInstance.__getXerSingleUntaggedComponent();
        if (untaggedComponent == null)
        {
          if (!typeInstance.IsExtensible())
            throw new Asn1Exception(49, "for SEQUENCE/SET type " + typeInstance.GetType().FullName + " (text element is '" + text + "')");
          else
            typeInstance.__addUnknownExtension(Encoding.UTF8.GetBytes(text));
        }
        else
          untaggedComponent.__readValue(this, text);
      }
    }

    protected internal void __decodeEnumeratedType(Asn1EnumeratedType typeInstance)
    {
      string xerTag = typeInstance.__getXerTag();
      int depth = -1;
      bool flag = xerTag.Length > 0 && (!typeInstance.__isXerUntagged() && !this._skipTag || this._isRoot);
      this._skipTag = false;
      if (flag)
      {
        if (this.readStartTag((Asn1Type) typeInstance, xerTag))
        {
          if (!this._isExtendedEncodingEnabled || !typeInstance.__isXerDefaultForEmpty())
            throw new Asn1Exception(35, "empty value is not authorized in type " + typeInstance.GetType().FullName + this.getPositionMessage());
          this._in.nextToken();
          this.readAfterTag((Asn1Type) typeInstance);
          return;
        }
        else
          depth = this._in.getDepth();
      }
      string text;
      if ((!this._isExtendedEncodingEnabled || !typeInstance.__isXerText()) && (this._in.getEventType() == 2 && this._in.isEmptyElementTag()))
      {
        text = this._in.getName();
        this._in.next();
      }
      else if (this._isExtendedEncodingEnabled && typeInstance.__isXerText())
      {
        text = this.readText((Asn1Type) typeInstance);
      }
      else
      {
        this.moveToValue((Asn1Type) typeInstance, true);
        this._nextValueReady = false;
        if (this._isExtendedEncodingEnabled && this._in.getEventType() == 3)
        {
          if (!this._isExtendedEncodingEnabled || !typeInstance.__isXerDefaultForEmpty())
            throw new Asn1Exception(35, "empty value is not authorized in type " + typeInstance.GetType().FullName + this.getPositionMessage());
          this.readAfterTag((Asn1Type) typeInstance);
          return;
        }
        else if (this._isExtendedEncodingEnabled && typeInstance.__isXerUseNumber())
        {
          if (this._in.getEventType() != 4)
            throw new Asn1Exception(35, "expecting a TEXT element in type " + typeInstance.GetType().FullName + this.getPositionMessage());
          text = this._in.getText();
        }
        else
        {
          if (this.moveToTag((Asn1Type) typeInstance, true) != 2 || !this._in.isEmptyElementTag())
            throw new Asn1Exception(25, "expecting an empty element tag in type " + typeInstance.GetType().FullName + this.getPositionMessage());
          text = this._in.getName();
          this._in.next();
        }
      }
      if (flag)
        this.readEndTag((Asn1Type) typeInstance, xerTag, depth);
      this.__decodeEnumeratedValue(typeInstance, text);
    }

    protected internal void __decodeEnumeratedValue(Asn1EnumeratedType typeInstance, string text)
    {
      if (text.Length == 0)
      {
        if (this._isExtendedEncodingEnabled && !typeInstance.__isXerDefaultForEmpty())
          throw new Asn1Exception(35, "empty value is not authorized in type " + typeInstance.GetType().FullName + this.getPositionMessage());
      }
      else
      {
        string[] idSet = typeInstance.__getXerIdentifierSet();
        string[] idExtensionSet = typeInstance.__getXerAdditionalIdentifierSet();
        if (!this._isExtendedEncodingEnabled || idSet == null)
        {
          idSet = typeInstance.__getRootIdentifierSet();
          idExtensionSet = typeInstance.__getExtensionIdentifierSet();
        }
        if (this._isExtendedEncodingEnabled && typeInstance.__isXerUseNumber())
        {
          try
          {
            typeInstance.SetValue(long.Parse(text));
          }
          catch (Exception ex)
          {
            throw new Asn1Exception(39, "'" + text + "' cannot be parsed for ENUMERATED type " + typeInstance.GetType().FullName + this.getPositionMessage());
          }
        }
        else
        {
          string identifier = text;
          if (this._isExtendedEncodingEnabled && typeInstance.__isXerText())
            identifier = XmlAsn1InputStream.escapeXerIdentifier(text);
          typeInstance.__setValue(identifier, idSet, idExtensionSet);
        }
      }
    }

    protected internal void __decodeIntegerType(Asn1IntegerType typeInstance)
    {
      string xerTag = typeInstance.__getXerTag();
      int depth = -1;
      bool flag = xerTag.Length > 0 && (!typeInstance.__isXerUntagged() && !this._skipTag || this._isRoot);
      this._skipTag = false;
      if (flag)
      {
        if (this.readStartTag((Asn1Type) typeInstance, xerTag))
        {
          if (!this._isExtendedEncodingEnabled || !typeInstance.__isXerDefaultForEmpty())
            throw new Asn1Exception(35, "empty value is not authorized in type " + typeInstance.GetType().FullName + this.getPositionMessage());
          this._in.nextToken();
          this.readAfterTag((Asn1Type) typeInstance);
          return;
        }
        else
          depth = this._in.getDepth();
      }
      this.moveToValue((Asn1Type) typeInstance, true);
      this._nextValueReady = false;
      if (this._in.getEventType() == 3)
      {
        if (!this._isExtendedEncodingEnabled || !typeInstance.__isXerDefaultForEmpty())
          throw new Asn1Exception(35, "empty value is not authorized in type " + typeInstance.GetType().FullName + this.getPositionMessage());
        this.readAfterTag((Asn1Type) typeInstance);
      }
      else
      {
        string text;
        if (this._in.getEventType() == 2)
        {
          if (typeInstance.__getIdentifierSet() == null || !typeInstance.__isXerEmptyElementEnabled())
            throw new Asn1Exception(38, "empty elements are not authorized in type " + typeInstance.GetType().FullName + this.getPositionMessage());
          text = this._in.getName();
          this._in.next();
        }
        else
        {
          if (this._in.getEventType() != 4)
            throw new Asn1Exception(35, "expecting a TEXT element in type " + typeInstance.GetType().FullName + this.getPositionMessage());
          text = this._in.getText();
        }
        if (flag)
          this.readEndTag((Asn1Type) typeInstance, xerTag, depth);
        this.__decodeIntegerValue(typeInstance, text);
      }
    }

    protected internal void __decodeIntegerValue(Asn1IntegerType typeInstance, string text)
    {
      if (text.Length == 0)
      {
        if (this._isExtendedEncodingEnabled && !typeInstance.__isXerDefaultForEmpty())
          throw new Asn1Exception(35, "empty value is not authorized in type " + typeInstance.GetType().FullName + this.getPositionMessage());
      }
      else
      {
        string[] idSet = typeInstance.__getXerIdentifierSet() ?? typeInstance.__getIdentifierSet();
        char c = text[0];
        int num;
        switch (c)
        {
          case '+':
          case '-':
            num = 1;
            break;
          default:
            num = char.IsDigit(c) ? 1 : 0;
            break;
        }
        bool flag = num != 0;
        if (!this._isExtendedEncodingEnabled || idSet == null || flag)
        {
          string s = text;
          if ((int) text[0] == 43)
          {
            if (!this._isExtendedEncodingEnabled || !typeInstance.__isXerPositiveWithPlusSignEnabled())
              throw new Asn1Exception(39, "+ in '" + text + "' is not authorized for INTEGER type " + typeInstance.GetType().FullName + this.getPositionMessage());
            else
              s = new string(text.ToCharArray(), 1, text.Length - 1);
          }
          try
          {
            typeInstance.SetValue(long.Parse(s));
          }
          catch (Exception ex1)
          {
            try
            {
              typeInstance.SetValue(new BigInteger(s));
            }
            catch (Exception ex2)
            {
              throw new Asn1Exception(39, "'" + text + "' cannot be parsed for INTEGER type " + typeInstance.GetType().FullName + this.getPositionMessage());
            }
          }
        }
        else
          typeInstance.__setValue(text, idSet);
      }
    }

    protected internal void __decodeNullType(Asn1NullType typeInstance)
    {
      string xerTag = typeInstance.__getXerTag();
      int depth = -1;
      bool flag = xerTag.Length > 0 && (!typeInstance.__isXerUntagged() && !this._skipTag || this._isRoot);
      this._skipTag = false;
      if (flag)
      {
        if (this.readStartTag((Asn1Type) typeInstance, xerTag))
        {
          this._in.nextToken();
          this.readAfterTag((Asn1Type) typeInstance);
          return;
        }
        else
          depth = this._in.getDepth();
      }
      this.moveToValue((Asn1Type) typeInstance, true);
      this._nextValueReady = false;
      if (this._in.getEventType() != 3)
        throw new Asn1Exception(35, "expecting an emtpy text in NULL type " + typeInstance.GetType().FullName + this.getPositionMessage());
      if (!flag)
        return;
      this.readEndTag((Asn1Type) typeInstance, xerTag, depth);
    }

    protected internal void __decodeNullValue(Asn1NullType typeInstance, string text)
    {
      if (text.Length != 0)
        throw new Asn1Exception(35, "expecting an emtpy text for NULL type " + typeInstance.GetType().FullName + this.getPositionMessage());
    }

    protected internal void __decodeObjectIdentifierType(Asn1ObjectIdentifierType typeInstance)
    {
      string xerTag = typeInstance.__getXerTag();
      int depth = -1;
      bool flag = xerTag.Length > 0 && (!typeInstance.__isXerUntagged() && !this._skipTag || this._isRoot);
      this._skipTag = false;
      if (flag)
      {
        if (this.readStartTag((Asn1Type) typeInstance, xerTag))
        {
          this._in.nextToken();
          this.readAfterTag((Asn1Type) typeInstance);
          return;
        }
        else
          depth = this._in.getDepth();
      }
      this.moveToValue((Asn1Type) typeInstance, true);
      this._nextValueReady = false;
      if (this._in.getEventType() == 3)
      {
        if (!this._in.getName().Equals(xerTag))
          throw new Asn1Exception(35, "expecting a END_TAG </" + xerTag + "> in type " + typeInstance.GetType().FullName + this.getPositionMessage());
        else
          this.readAfterTag((Asn1Type) typeInstance);
      }
      else
      {
        if (this._in.getEventType() != 4)
          throw new Asn1Exception(35, "expecting a TEXT element in type " + typeInstance.GetType().FullName + this.getPositionMessage());
        string text = this._in.getText();
        if (flag)
          this.readEndTag((Asn1Type) typeInstance, xerTag, depth);
        this.__decodeObjectIdentifierValue(typeInstance, text);
      }
    }

    protected internal void __decodeObjectIdentifierValue(Asn1ObjectIdentifierType typeInstance, string text)
    {
      if (text.Length == 0)
        return;
      if (text.Length < 2)
        throw new Asn1ValidationException(18, "at least two arcs should be defined in type " + typeInstance.GetType().FullName + this.getPositionMessage());
      typeInstance.SetValue(text);
    }

    protected internal void __decodeOctetStringType(Asn1OctetStringType typeInstance)
    {
      string xerTag = typeInstance.__getXerTag();
      int depth = -1;
      bool flag1 = xerTag.Length > 0 && (!typeInstance.__isXerUntagged() && !this._skipTag || this._isRoot);
      this._skipTag = false;
      if (flag1)
      {
        if (this.readStartTag((Asn1Type) typeInstance, xerTag))
        {
          this._in.nextToken();
          this.readAfterTag((Asn1Type) typeInstance);
          if (this._isExtendedEncodingEnabled && typeInstance.__isXerDefaultForEmpty())
            return;
          this.__decodeOctetStringValue(typeInstance, "");
          return;
        }
        else
          depth = this._in.getDepth();
      }
      bool flag2 = false;
      bool flag3 = typeInstance.__getEncoder() != null;
      string text;
      if (typeInstance.__isContainingType() && !flag3)
      {
        this._in.setKeepContent(true);
        this.moveToValue((Asn1Type) typeInstance, true);
        this._nextValueReady = false;
        if (this._in.getEventType() == 4)
        {
          text = this._in.getText();
        }
        else
        {
          this._in.readOuterXml();
          text = this._in.getKeptContent();
          this._in.setKeepContent(false);
          flag2 = true;
        }
        typeInstance.__setDecoder((IDecoder) this.getReader());
        this._isContentToBeResolved = true;
      }
      else
      {
        this.moveToValue((Asn1Type) typeInstance, true);
        this._nextValueReady = false;
        if (this._in.getEventType() == 3)
        {
          if (!this._in.getName().Equals(xerTag))
          {
            throw new Asn1Exception(35, "expecting a END_TAG </" + xerTag + "> in type " + typeInstance.GetType().FullName + this.getPositionMessage());
          }
          else
          {
            this.readAfterTag((Asn1Type) typeInstance);
            this.__decodeOctetStringValue(typeInstance, "");
            return;
          }
        }
        else
        {
          if (this._in.getEventType() == 2)
            throw new Asn1Exception(37, "a START_TAG is unexpected for OCTET STRING type " + typeInstance.GetType().FullName + this.getPositionMessage());
          text = this.readText((Asn1Type) typeInstance);
          if (typeInstance.__isContainingType())
          {
            if (text.Trim().Length == 0)
              throw new Asn1Exception(37, "contained type should be encoded as an 'xmlhstring' for OCTET STRING type " + typeInstance.GetType().FullName + this.getPositionMessage());
            typeInstance.__setDecoder((IDecoder) this.getReader());
            this._isContentToBeResolved = true;
          }
        }
      }
      if (flag1)
        this.readEndTag((Asn1Type) typeInstance, xerTag, depth);
      if (flag2)
        this.__decodeOctetStringXMLTypedValue(typeInstance, text);
      else
        this.__decodeOctetStringValue(typeInstance, text);
    }

    protected internal void __decodeOctetStringValue(Asn1OctetStringType typeInstance, string text)
    {
      if (this._isExtendedEncodingEnabled && typeInstance.__isXerBase64())
      {
        try
        {
          typeInstance.SetValue(Base64.Decode(text));
        }
        catch (ArgumentException ex)
        {
          throw new Asn1Exception(63, "in '" + text + "' " + ex.Message + ".");
        }
      }
      else
      {
        try
        {
          typeInstance.SetValue(ByteArray.HexStringToByteArray(text));
        }
        catch (ArgumentException ex)
        {
          throw new Asn1Exception(62, "in '" + text + "' " + ex.Message + ".");
        }
      }
    }

    protected internal void __decodeOctetStringXMLTypedValue(Asn1OctetStringType typeInstance, string text)
    {
      typeInstance.SetValue(Encoding.UTF8.GetBytes(text));
    }

    protected internal void __decodeOpenType(Asn1OpenType typeInstance)
    {
      string xerTag = typeInstance.__getXerTag();
      int depth = -1;
      bool flag1 = xerTag.Length > 0 && (!typeInstance.__isXerUntagged() && !this._skipTag || this._isRoot);
      this._skipTag = false;
      if (flag1)
      {
        if (this.readStartTag((Asn1Type) typeInstance, xerTag))
        {
          this._in.nextToken();
          this.readAfterTag((Asn1Type) typeInstance);
          this.__decodeOpenValue(typeInstance, "");
          return;
        }
        else
          depth = this._in.getDepth();
      }
      bool flag2 = false;
      this._in.setKeepContent(true);
      this.moveToValue((Asn1Type) typeInstance, true);
      this._nextValueReady = false;
      string text;
      if (this._in.getEventType() == 4)
      {
        text = this._in.getText();
      }
      else
      {
        this._in.readOuterXml();
        text = this._in.getKeptContent();
        flag2 = true;
      }
      this._in.setKeepContent(false);
      typeInstance.__setDecoder((IDecoder) this.getReader());
      this._isContentToBeResolved = true;
      if (flag1)
        this.readEndTag((Asn1Type) typeInstance, xerTag, depth);
      if (flag2)
        this.__decodeOpenXMLTypedValue(typeInstance, text);
      else
        this.__decodeOpenValue(typeInstance, text);
    }

    protected internal void __decodeOpenValue(Asn1OpenType typeInstance, string text)
    {
      if (text.Length == 0)
        typeInstance.SetValue(new byte[0]);
      else if (this._isExtendedEncodingEnabled && typeInstance.__isXerBase64())
      {
        try
        {
          typeInstance.SetValue(Base64.Decode(text));
        }
        catch (ArgumentException ex)
        {
          throw new Asn1Exception(63, "in '" + text + "' " + ex.Message + ".");
        }
      }
      else
      {
        try
        {
          typeInstance.SetValue(ByteArray.HexStringToByteArray(text));
        }
        catch (ArgumentException ex)
        {
          throw new Asn1Exception(62, "in '" + text + "' " + ex.Message + ".");
        }
      }
    }

    protected internal void __decodeOpenXMLTypedValue(Asn1OpenType typeInstance, string text)
    {
      typeInstance.SetValue(Encoding.UTF8.GetBytes(text));
    }

    protected internal void __decodeRealType(Asn1RealType typeInstance)
    {
      string xerTag = typeInstance.__getXerTag();
      int depth = -1;
      bool flag = xerTag.Length > 0 && (!typeInstance.__isXerUntagged() && !this._skipTag || this._isRoot);
      this._skipTag = false;
      if (flag)
      {
        if (this.readStartTag((Asn1Type) typeInstance, xerTag))
        {
          if (!this._isExtendedEncodingEnabled || !typeInstance.__isXerDefaultForEmpty())
            throw new Asn1Exception(35, "empty value is not authorized in type " + typeInstance.GetType().FullName + this.getPositionMessage());
          this._in.nextToken();
          this.readAfterTag((Asn1Type) typeInstance);
          return;
        }
        else
          depth = this._in.getDepth();
      }
      this.moveToValue((Asn1Type) typeInstance, true);
      this._nextValueReady = false;
      if (this._in.getEventType() == 3)
      {
        if (!this._isExtendedEncodingEnabled || !typeInstance.__isXerDefaultForEmpty())
          throw new Asn1Exception(35, "empty value is not authorized in type " + typeInstance.GetType().FullName + this.getPositionMessage());
        this.readAfterTag((Asn1Type) typeInstance);
      }
      else
      {
        string text;
        if (this._in.getEventType() == 2 && this._in.isEmptyElementTag())
        {
          if (typeInstance.__isXerSpecialAsText())
            throw new Asn1Exception(35, "empty elements are not authorized in type " + typeInstance.GetType().FullName + this.getPositionMessage());
          text = this._in.getName();
          this._in.next();
        }
        else
        {
          if (this._in.getEventType() != 4)
            throw new Asn1Exception(35, "expecting a TEXT element in type " + typeInstance.GetType().FullName + this.getPositionMessage());
          text = this._in.getText();
        }
        if (flag)
          this.readEndTag((Asn1Type) typeInstance, xerTag, depth);
        this.__decodeRealValue(typeInstance, text);
      }
    }

    protected internal void __decodeRealValue(Asn1RealType typeInstance, string text)
    {
      if (text.Length == 0)
      {
        if (this._isExtendedEncodingEnabled && !typeInstance.__isXerDefaultForEmpty())
          throw new Asn1Exception(35, "empty value is not authorized in type " + typeInstance.GetType().FullName + this.getPositionMessage());
      }
      else
      {
        char c = text[0];
        char ch = text.Length > 1 ? text[1] : ' ';
        if ((int) c != 43 && ((int) c != 45 || (int) ch == 73) && !char.IsDigit(c))
        {
          if (this._isExtendedEncodingEnabled && typeInstance.__isXerSpecialAsText())
          {
            if (typeInstance.__isXerDecimal())
              throw new Asn1Exception(38, "special values like '" + text + "' are not authorized for DECIMAL encoded REAL type " + typeInstance.GetType().FullName + this.getPositionMessage());
            else if ("INF".Equals(text))
              typeInstance.SetValue(double.PositiveInfinity);
            else if ("-INF".Equals(text))
            {
              typeInstance.SetValue(double.NegativeInfinity);
            }
            else
            {
              if (!"NaN".Equals(text))
                throw new Asn1Exception(38, "special values should be INF/-INF/NaN for REAL type " + typeInstance.GetType().FullName + this.getPositionMessage());
              typeInstance.SetValue(double.NaN);
            }
          }
          else if ("PLUS-INFINITY".Equals(text))
            typeInstance.SetValue(double.PositiveInfinity);
          else if ("MINUS-INFINITY".Equals(text))
          {
            typeInstance.SetValue(double.NegativeInfinity);
          }
          else
          {
            if (!"NOT-A-NUMBER".Equals(text))
              throw new Asn1Exception(38, "special values should be PLUS-INFINITY/MINUS-INFINITY/NOT-A-NUMBER for REAL type " + typeInstance.GetType().FullName + this.getPositionMessage());
            typeInstance.SetValue(double.NaN);
          }
        }
        else
        {
          string s = text;
          if ((int) c == 43)
          {
            if (!this._isExtendedEncodingEnabled || !typeInstance.__isXerPositiveWithPlusSignEnabled())
              throw new Asn1Exception(39, "+ in '" + text + "' is not authorized for REAL type " + typeInstance.GetType().FullName + this.getPositionMessage());
            else
              s = new string(text.ToCharArray(), 1, text.Length - 1);
          }
          if ("-0".Equals(s))
          {
            typeInstance.SetValue("-0");
          }
          else
          {
            try
            {
              if (typeInstance.__isXerDecimal() && s.ToUpper().IndexOf('E') != -1)
                throw new Asn1Exception(39, "e or E in '" + text + "' is not authorized for DECIMAL encoded REAL type " + typeInstance.GetType().FullName + this.getPositionMessage());
              else
                typeInstance.SetValue(double.Parse(s, (IFormatProvider) CultureInfo.InvariantCulture));
            }
            catch (Exception ex)
            {
              throw new Asn1Exception(39, "'" + text + "' is incorrect for REAL type " + typeInstance.GetType().FullName + this.getPositionMessage());
            }
          }
        }
      }
    }

    protected internal void __decodeRelativeOIDType(Asn1RelativeOIDType typeInstance)
    {
      string xerTag = typeInstance.__getXerTag();
      int depth = -1;
      bool flag = xerTag.Length > 0 && (!typeInstance.__isXerUntagged() && !this._skipTag || this._isRoot);
      this._skipTag = false;
      if (flag)
      {
        if (this.readStartTag((Asn1Type) typeInstance, xerTag))
        {
          this._in.nextToken();
          this.readAfterTag((Asn1Type) typeInstance);
          return;
        }
        else
          depth = this._in.getDepth();
      }
      this.moveToValue((Asn1Type) typeInstance, true);
      this._nextValueReady = false;
      if (this._in.getEventType() == 3)
      {
        if (!this._in.getName().Equals(xerTag))
          throw new Asn1Exception(35, "expecting a END_TAG </" + xerTag + "> in type " + typeInstance.GetType().FullName + this.getPositionMessage());
        else
          this.readAfterTag((Asn1Type) typeInstance);
      }
      else
      {
        if (this._in.getEventType() != 4)
          throw new Asn1Exception(35, "expecting a TEXT element in type " + typeInstance.GetType().FullName + this.getPositionMessage());
        string text = this._in.getText();
        if (flag)
          this.readEndTag((Asn1Type) typeInstance, xerTag, depth);
        this.__decodeRelativeOIDValue(typeInstance, text);
      }
    }

    protected internal void __decodeRelativeOIDValue(Asn1RelativeOIDType typeInstance, string text)
    {
      if (text.Length == 0)
        return;
      typeInstance.SetValue(text);
    }

    protected internal void __decodeStringType(Asn1StringType typeInstance)
    {
      string xerTag = typeInstance.__getXerTag();
      int depth = -1;
      bool flag = xerTag.Length > 0 && (!typeInstance.__isXerUntagged() && !this._skipTag || this._isRoot);
      this._skipTag = false;
      if (this._isExtendedEncodingEnabled && typeInstance.__isXerAnyElement())
        flag = false;
      if (flag)
      {
        if (this.readStartTag((Asn1Type) typeInstance, xerTag))
        {
          this._in.nextToken();
          this.readAfterTag((Asn1Type) typeInstance);
          if (this._isExtendedEncodingEnabled && typeInstance.__isXerDefaultForEmpty())
            return;
          this.__decodeStringValue(typeInstance, "");
          return;
        }
        else
          depth = this._in.getDepth();
      }
      string text;
      if (this._isExtendedEncodingEnabled && typeInstance.__isXerAnyElement())
      {
        if (!this._keepingContent)
        {
          this._in.setKeepContent(true);
          this.moveToValue((Asn1Type) typeInstance, true);
          this._nextValueReady = false;
          if (this._in.getEventType() != 2)
            throw new Asn1Exception(37, "expecting any element for UTF8String type " + typeInstance.GetType().FullName + this.getPositionMessage());
        }
        this._in.readOuterXml();
        text = this._in.getKeptContent();
        this._in.setKeepContent(false);
        this._keepingContent = false;
      }
      else
        text = this.readText((Asn1Type) typeInstance);
      if (flag)
        this.readEndTag((Asn1Type) typeInstance, xerTag, depth);
      this.__decodeStringValue(typeInstance, text);
      if (!this._isExtendedEncodingEnabled || !typeInstance.__isXerAnyElement() || (this._embedValuesDepth == -1 || this._embedValuesDepth + 1 != this._in.getDepth()))
        return;
      this._keepingContent = true;
      this._in.setKeepContent(true);
      this.moveToValue((Asn1Type) typeInstance, true);
      this.readEmbedValuesValue(this._in.getEventType() != 2 && this._in.getEventType() != 3 ? this.readText((Asn1Type) typeInstance, true, true) : "");
      this._nextValueReady = true;
    }

    protected internal void __decodeStringValue(Asn1StringType typeInstance, string text)
    {
      if (this._isExtendedEncodingEnabled && typeInstance.__isXerBase64())
      {
        try
        {
          typeInstance.SetValue(Base64.Decode(text), Encoding.UTF8);
        }
        catch (IOException ex)
        {
          throw new Asn1Exception(21, "exception " + ex.Message + " for type <" + typeInstance.GetType().FullName + ">");
        }
      }
      else if (this._isExtendedEncodingEnabled && typeInstance.__isXerWhiteSpaceCollapse())
        typeInstance.SetValue(text.Trim());
      else
        typeInstance.SetValue(text);
    }

    protected internal void __decodeTimeType(Asn1TimeType typeInstance)
    {
      string xerTag = typeInstance.__getXerTag();
      int depth = -1;
      bool flag = xerTag.Length > 0 && (!typeInstance.__isXerUntagged() && !this._skipTag || this._isRoot);
      this._skipTag = false;
      if (flag)
      {
        if (this.readStartTag((Asn1Type) typeInstance, xerTag))
        {
          this._in.nextToken();
          this.readAfterTag((Asn1Type) typeInstance);
          if (this._isExtendedEncodingEnabled && typeInstance.__isXerDefaultForEmpty())
            return;
          this.__decodeTimeValue(typeInstance, "");
          return;
        }
        else
          depth = this._in.getDepth();
      }
      string text = this.readText((Asn1Type) typeInstance);
      if (flag)
        this.readEndTag((Asn1Type) typeInstance, xerTag, depth);
      this.__decodeTimeValue(typeInstance, text);
    }

    protected internal void __decodeTimeValue(Asn1TimeType typeInstance, string text)
    {
      typeInstance.SetValue(text);
    }

    protected override void close()
    {
      if (this._in == null)
        return;
      this._usedBytes = this._in.getUsedBytes();
      this._usedChars = this._in.getUsedChars();
    }

    protected override void decodeImpl(Stream inputStream, Asn1Type type)
    {
      this.init(inputStream);
      this._isRoot = true;
      this._skipTag = false;
      this._nextValueReady = false;
      this._keepingContent = false;
      this._embedValuesComponent = (Asn1ConstructedOfType) null;
      this._embedValuesDepth = -1;
      type.__read(this);
      if (this._isExtendedEncodingEnabled)
        this.readEpilog(type);
      this.close();
    }

    private string getPositionMessage()
    {
      if (this._in == null)
        return "";
      else
        return " (position:" + this._in.getPositionDescription() + ")";
    }

    public override string GetProperty(string key)
    {
      if (key.Equals("validating"))
        return this._isValidating.ToString();
      if (key.Equals("resolvingContent"))
        return this._isResolvingContent.ToString();
      if (key.Equals("internalErrorLogDir"))
        return this._internalErrorLogDir;
      if (key.Equals("ignoringNamespace"))
        return this._isIgnoringNamespace.ToString();
      else
        return (string) null;
    }

    public virtual Asn1TypeReader getReader()
    {
      Asn1TypeXerReader asn1TypeXerReader = new Asn1TypeXerReader();
      asn1TypeXerReader._isResolvingContent = this._isResolvingContent;
      asn1TypeXerReader._isValidating = this._isValidating;
      asn1TypeXerReader._isExtendedEncodingEnabled = this._isExtendedEncodingEnabled;
      asn1TypeXerReader._isIgnoringNamespace = this._isIgnoringNamespace;
      return (Asn1TypeReader) asn1TypeXerReader;
    }

    protected override void init(Stream input)
    {
      if (input == this._underlyingIn)
      {
        this._in.reinit();
      }
      else
      {
        this._in = new XmlAsn1InputStream(input);
        this._in.setParseEpilog(this._isExtendedEncodingEnabled);
        this._in.setProcessNamespaces(this._isExtendedEncodingEnabled);
        this._underlyingIn = input;
      }
      this._usedBytes = 0;
      this._usedChars = 0;
      this._isContentToBeResolved = false;
    }

    private int moveToTag(Asn1Type typeInstance, bool start)
    {
      int num1 = this._in.getEventType();
      if (num1 == 4 && this._in.isWhitespace())
        num1 = this._in.nextToken();
      while (num1 != 2 && num1 != 3 && (num1 != 4 && num1 != 1))
      {
        num1 = this._in.nextToken();
        int num2;
        switch (num1)
        {
          case 8:
            if (!this._isExtendedEncodingEnabled)
              throw new Asn1Exception(35, "PI or Comment are not allowed in type " + typeInstance.GetType().FullName + this.getPositionMessage());
            typeInstance.AddPiOrComment(new PiOrComment("<?" + this._in.getText() + "?>", start ? 0 : 2));
            num1 = this._in.nextToken();
            continue;
          case 9:
            if (!this._isExtendedEncodingEnabled)
              throw new Asn1Exception(35, "PI or Comment are not allowed in type " + typeInstance.GetType().FullName + this.getPositionMessage());
            typeInstance.AddPiOrComment(new PiOrComment("<!--" + this._in.getText() + "-->", start ? 0 : 2));
            num1 = this._in.nextToken();
            continue;
          case 7:
            num2 = 0;
            break;
          case 4:
            if (this._in.isWhitespace())
              goto case 7;
            else
              goto default;
          default:
            num2 = num1 != 10 ? 1 : 0;
            break;
        }
        if (num2 == 0)
          num1 = this._in.nextToken();
      }
      return num1;
    }

    private int moveToValue(Asn1Type typeInstance, bool start)
    {
      return this.moveToValue(typeInstance, start, true);
    }

    private int moveToValue(Asn1Type typeInstance, bool start, bool skipWhitespace)
    {
      if (this._nextValueReady)
        return this._in.getEventType();
      this._nextValueReady = true;
      int num;
      for (num = this._in.nextToken(); num == 7 && skipWhitespace || (num == 8 || num == 9 || num == 4 && skipWhitespace && this._in.isWhitespace()); num = this._in.nextToken())
      {
        switch (num)
        {
          case 8:
            if (!this._isExtendedEncodingEnabled)
              throw new Asn1Exception(35, "PI or Comment are not allowed in type " + typeInstance.GetType().FullName + this.getPositionMessage());
            typeInstance.AddPiOrComment(new PiOrComment("<?" + this._in.getText() + "?>", start ? 1 : 2));
            break;
          case 9:
            if (!this._isExtendedEncodingEnabled)
              throw new Asn1Exception(35, "PI or Comment are not allowed in type " + typeInstance.GetType().FullName + this.getPositionMessage());
            typeInstance.AddPiOrComment(new PiOrComment("<!--" + this._in.getText() + "-->", start ? 1 : 2));
            break;
        }
      }
      return num;
    }

    private void readAfterTag(Asn1Type typeInstance)
    {
      if (this._isExtendedEncodingEnabled && typeInstance.GetPiOrCommentList() != null)
      {
        IList piOrCommentList = typeInstance.GetPiOrCommentList();
        for (int index = 0; index < piOrCommentList.Count; ++index)
        {
          if (((PiOrComment) piOrCommentList[index]).Position == 3)
          {
            int num = this._in.nextToken();
            if (num != 8 && num != 9)
              throw new Asn1Exception(35, "PI or Comment is expected AFTER_TAG in type " + typeInstance.GetType().FullName + this.getPositionMessage());
          }
        }
      }
      if (!this._isExtendedEncodingEnabled || this._embedValuesDepth == -1 || this._embedValuesDepth + 1 != this._in.getDepth())
        return;
      this.moveToValue(typeInstance, false, false);
      this.readEmbedValuesValue(this._in.getEventType() != 2 && this._in.getEventType() != 3 ? this.readText(typeInstance, true, false) : "");
      this._nextValueReady = true;
    }

    private void readAnyAttributesValue(Asn1ConstructedOfType typeInstance, string text)
    {
      if (text.Length == 0)
        return;
      typeInstance.__getMatchingElement().__readValue(this, text);
    }

    private void readEmbedValuesValue(string text)
    {
      if (this._embedValuesComponent == null)
        return;
      this._embedValuesComponent.__getMatchingElement().__readValue(this, text);
    }

    private void readEndTag(Asn1Type typeInstance, string tag, int depth)
    {
      if (this._in.getDepth() == depth + 1 || this._in.getEventType() == 4 || this._in.getEventType() == 2)
      {
        this.moveToValue(typeInstance, false);
        this._nextValueReady = false;
      }
      if (this._in.getEventType() != 3 || !this._in.getName().Equals(tag))
        throw new Asn1Exception(35, "expecting a END_TAG </" + tag + "> in type " + typeInstance.GetType().FullName + this.getPositionMessage());
      else
        this.readAfterTag(typeInstance);
    }

    private int readEpilog(Asn1Type typeInstance)
    {
      if (this._in.getEventType() == 1)
        return this._in.getEventType();
      int num;
      for (num = this._in.nextToken(); num == 7 || num == 8 || (num == 9 || num == 10) || num == 4 && this._in.isWhitespace(); num = this._in.nextToken())
      {
        switch (num)
        {
          case 8:
            if (!this._isExtendedEncodingEnabled)
              throw new Asn1Exception(35, "PI or Comment are not allowed in type " + typeInstance.GetType().FullName + this.getPositionMessage());
            typeInstance.AddPiOrComment(new PiOrComment("<?" + this._in.getText() + "?>", 3));
            break;
          case 9:
            if (!this._isExtendedEncodingEnabled)
              throw new Asn1Exception(35, "PI or Comment are not allowed in type " + typeInstance.GetType().FullName + this.getPositionMessage());
            typeInstance.AddPiOrComment(new PiOrComment("<!--" + this._in.getText() + "-->", 3));
            break;
        }
      }
      return num;
    }

    private bool readStartTag(Asn1Type typeInstance, string tag)
    {
      if (this.moveToTag(typeInstance, true) != 2 || !this._in.getName().Equals(tag))
      {
        throw new Asn1Exception(35, "expecting a START_TAG <" + tag + "> in type " + typeInstance.GetType().FullName + this.getPositionMessage());
      }
      else
      {
        this._isRoot = false;
        if (this._isIgnoringNamespace || !(typeInstance.__getXerNamespaceUri() != this._in.getNamespace()) || typeInstance.__getXerNamespaceUri() == null && this._in.getNamespace().Length == 0)
          return this._in.isEmptyElementTag();
        throw new Asn1Exception(35, "expecting a START_TAG <" + tag + ">" + (typeInstance.__getXerNamespaceUri() != null ? " in namespace '" + typeInstance.__getXerNamespaceUri() + "'" : "") + " in type " + typeInstance.GetType().FullName + this.getPositionMessage());
      }
    }

    private string readText(Asn1Type typeInstance)
    {
      return this.readText(typeInstance, false, false);
    }

    private string readText(Asn1Type typeInstance, bool stopOnStartTag, bool keepContent)
    {
      StringBuilder sb = new StringBuilder();
      if (this._in.getEventType() == 4 || this._in.getEventType() == 5)
        sb.Append(this._in.getText());
      if (keepContent)
        this._in.setKeepContent(true);
      int num;
      for (num = this._in.nextToken(); num == 8 || num == 9 || (num == 4 || num == 6) || (num == 5 || num == 2 && this._in.isEmptyElementTag()); num = this._in.nextToken())
      {
        if (num == 8)
        {
          if (!this._isExtendedEncodingEnabled)
            throw new Asn1Exception(35, "PI or Comment are not allowed in type " + typeInstance.GetType().FullName + this.getPositionMessage());
          typeInstance.AddPiOrComment(new PiOrComment("<?" + this._in.getText() + "?>", sb.Length == 0 ? 1 : 2));
        }
        else if (num == 9)
        {
          if (!this._isExtendedEncodingEnabled)
            throw new Asn1Exception(35, "PI or Comment are not allowed in type " + typeInstance.GetType().FullName + this.getPositionMessage());
          typeInstance.AddPiOrComment(new PiOrComment("<!--" + this._in.getText() + "-->", sb.Length == 0 ? 1 : 2));
        }
        else if (num == 4 || num == 6 || num == 5)
        {
          if (keepContent)
            this._in.setKeepContent(false);
          this._in.readText(sb);
        }
        else if (num == 2 && this._in.isEmptyElementTag())
        {
          string name = this._in.getName();
          try
          {
            sb.Append(XmlAsn1InputStream.decodeAsn1ControlCharacter(name));
          }
          catch (Exception ex)
          {
            if (!stopOnStartTag)
              throw new Asn1Exception(57, "'" + name + "' in type " + typeInstance.GetType().FullName + this.getPositionMessage());
            else
              break;
          }
          this._in.next();
        }
        if (keepContent)
          this._in.setKeepContent(true);
      }
      if (num != 3 && (!stopOnStartTag || num != 2))
        throw new Asn1Exception(35, "expecting a TEXT element in type " + typeInstance.GetType().FullName + this.getPositionMessage());
      else
        return ((object) sb).ToString();
    }

    public override void SetProperty(string key, string property)
    {
      if (!this.PropertyNames().Contains((object) key))
        return;
      if (key.Equals("validating"))
        this._isValidating = bool.Parse(property);
      else if (key.Equals("resolvingContent"))
        this._isResolvingContent = bool.Parse(property);
      else if (key.Equals("internalErrorLogDir"))
        this._internalErrorLogDir = property;
      else if (key.Equals("ignoringNamespace"))
        this._isIgnoringNamespace = bool.Parse(property);
    }

    public override int UsedBytes()
    {
      return this._usedBytes;
    }

    public int UsedChars()
    {
      return this._usedChars;
    }
  }
}
