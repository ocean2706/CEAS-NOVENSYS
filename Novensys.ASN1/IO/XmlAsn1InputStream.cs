// Decompiled with JetBrains decompiler
// Type: Novensys.ASN1.IO.XmlAsn1InputStream
// Assembly: Novensys.eCard.SDK, Version=1.1.56.0, Culture=neutral, PublicKeyToken=null
// MVID: 59F9E456-D3DA-4066-A4A4-692B516A775E
// Assembly location: C:\Program Files (x86)\CNAS\SIUI-SPITAL\Novensys.eCard.SDK.dll

using Novensys.ASN1;
using Novensys.ASN1.Util;
using System;
using System.IO;
using System.Text;

namespace Novensys.ASN1.IO
{
  [Serializable]
  public class XmlAsn1InputStream
  {
    protected char[] buf = new char[512];
    private int bufLoadFactor = 95;
    private char[] charRefOneCharBuf = new char[1];
    private bool doParseEpilog = false;
    private char[] pc = new char[512];
    private static char LOOKUP_MAX_CHAR = 'Ѐ';
    private static bool[] lookupNameChar = new bool[1024];
    private static bool[] lookupNameStartChar = new bool[1024];
    private static char[] NCODING = "ncoding".ToCharArray();
    private static char[] NO = "no".ToCharArray();
    private static char[] TANDALONE = "tandalone".ToCharArray();
    public static string[] TYPES = new string[11]
    {
      "START_DOCUMENT",
      "END_DOCUMENT",
      "START_TAG",
      "END_TAG",
      "TEXT",
      "CDSECT",
      "ENTITY_REF",
      "IGNORABLE_WHITESPACE",
      "PROCESSING_INSTRUCTION",
      "COMMENT",
      "DOCDECL"
    };
    private static char[] VERSION = "version".ToCharArray();
    private static char[][] xerIdentifierEscapedChars = new char[3][]
    {
      new char[4]
      {
        '&',
        'g',
        't',
        ';'
      },
      new char[4]
      {
        '&',
        'l',
        't',
        ';'
      },
      new char[5]
      {
        '&',
        'a',
        'm',
        'p',
        ';'
      }
    };
    private static char[] YES = "yes".ToCharArray();
    public const string ASN1NS_PREFIX = "asn1";
    public const string ASN1NS_URI = "urn:oid:2.1.5.2.0.1";
    private const char b00000111 = '\a';
    private const char b00001111 = '\x000F';
    private const char b00011111 = '\x001F';
    private const char b00111111 = '?';
    private const char b10000000 = '\x0080';
    private const char b11000000 = 'À';
    private const char b11100000 = 'à';
    private const char b11110000 = 'ð';
    private const char b11111000 = 'ø';
    private const char b11111110 = 'þ';
    private const int BUF_SIZE = 512;
    public const int CDSECT = 5;
    public const int COMMENT = 9;
    public const int DOCDECL = 10;
    public const int END_DOCUMENT = 1;
    public const int END_TAG = 3;
    public const int ENTITY_REF = 6;
    public const int IGNORABLE_WHITESPACE = 7;
    private const int LOOKUP_MAX = 1024;
    private const int MAX_CODE_POINT = 1114111;
    private const char MIN_HIGH_SURROGATE = '\xD800';
    private const char MIN_LOW_SURROGATE = '\xDC00';
    private const int MIN_SUPPLEMENTARY_CODE_POINT = 65536;
    public const string NO_NAMESPACE = "";
    public const int PROCESSING_INSTRUCTION = 8;
    public const int START_DOCUMENT = 0;
    public const int START_TAG = 2;
    public const int TEXT = 4;
    private const string XML_URI = "http://www.w3.org/XML/1998/namespace";
    private const string XMLNS_URI = "http://www.w3.org/2000/xmlns/";
    private bool allStringsInterned;
    private int attributeCount;
    private string[] attributeName;
    private int[] attributeNameHash;
    private string[] attributePrefix;
    private string[] attributeUri;
    private string[] attributeValue;
    private int bufAbsoluteStart;
    private int bufEnd;
    private int bufSoftLimit;
    private int bufStart;
    protected byte[] bufStream;
    protected int bufStreamIndex;
    private int columnNumber;
    private int depth;
    private string[] elName;
    private int[] elNamespaceCount;
    private string[] elPrefix;
    private char[][] elRawName;
    private int[] elRawNameEnd;
    private int[] elRawNameLine;
    private string[] elUri;
    private bool emptyElementTag;
    private int entityEnd;
    private string[] entityName;
    private char[][] entityNameBuf;
    private int[] entityNameHash;
    private string entityRefName;
    private string[] entityReplacement;
    private char[][] entityReplacementBuf;
    private int eventType;
    private string inputEncoding;
    private Stream inputStream;
    private bool keepBuffer;
    private int lineNumber;
    private int namespaceEnd;
    private string[] namespacePrefix;
    private int[] namespacePrefixHash;
    private string[] namespaceUri;
    private bool pastEndTag;
    private int pcEnd;
    private int pcStart;
    private int pos;
    private int posEnd;
    private int posStart;
    private bool processNamespaces;
    private bool reachedEnd;
    private int readBytes;
    private bool roundtripSupported;
    private bool seenAmpersand;
    private bool seenDocdecl;
    private bool seenEndTag;
    private bool seenMarkup;
    private bool seenRoot;
    private bool seenStartTag;
    private string text;
    private bool tokenize;
    private int usedChars;
    private bool usePC;
    private string xmlDeclContent;
    private string xmlDeclStandalone;
    private string xmlDeclVersion;

    static XmlAsn1InputStream()
    {
      XmlAsn1InputStream.setNameStart(':');
      for (char ch = 'A'; (int) ch <= 90; ++ch)
        XmlAsn1InputStream.setNameStart(ch);
      XmlAsn1InputStream.setNameStart('_');
      for (char ch = 'a'; (int) ch <= 122; ++ch)
        XmlAsn1InputStream.setNameStart(ch);
      for (char ch = 'À'; (int) ch <= 767; ++ch)
        XmlAsn1InputStream.setNameStart(ch);
      for (char ch = 'Ͱ'; (int) ch <= 893; ++ch)
        XmlAsn1InputStream.setNameStart(ch);
      for (char ch = '\x037F'; (int) ch < 1024; ++ch)
        XmlAsn1InputStream.setNameStart(ch);
      XmlAsn1InputStream.setName('-');
      XmlAsn1InputStream.setName('.');
      for (char ch = '0'; (int) ch <= 57; ++ch)
        XmlAsn1InputStream.setName(ch);
      XmlAsn1InputStream.setName('·');
      for (char ch = '̀'; (int) ch <= 879; ++ch)
        XmlAsn1InputStream.setName(ch);
    }

    public XmlAsn1InputStream(Stream input)
    {
      this.bufSoftLimit = this.bufLoadFactor * this.buf.Length / 100;
      this.bufStream = new byte[this.buf.Length];
      this.setProcessNamespaces(true);
      this.setInput(input);
      this.bufAbsoluteStart = 0;
      this.bufEnd = this.bufStart = 0;
      this.pos = this.posStart = this.posEnd = 0;
      this.pcEnd = this.pcStart = 0;
    }

    public static char decodeAsn1ControlCharacter(string sequence)
    {
      if (sequence == null)
        throw new ArgumentException("sequence is null.");
      char[] chArray = sequence.ToCharArray();
      int length = chArray.Length;
      if (length < 2 || length > 3)
        throw new ArgumentException("control character cannot be found for '" + sequence + "' because length is invalid.");
      if ((int) chArray[0] == 97 && length == 3 && ((int) chArray[1] == 99 && (int) chArray[2] == 107))
        return '\x0006';
      if ((int) chArray[0] == 98)
      {
        if (length == 2 && (int) chArray[1] == 115)
          return '\b';
        if (length == 3 && (int) chArray[1] == 101 && (int) chArray[2] == 108)
          return '\a';
      }
      else
      {
        if ((int) chArray[0] == 99 && length == 3 && ((int) chArray[1] == 97 && (int) chArray[2] == 110))
          return '\x0018';
        if ((int) chArray[0] == 100 && length == 3)
        {
          if ((int) chArray[1] == 108 && (int) chArray[2] == 101)
            return '\x0010';
          if ((int) chArray[1] == 99)
          {
            if ((int) chArray[2] == 49)
              return '\x0011';
            if ((int) chArray[2] == 50)
              return '\x0012';
            if ((int) chArray[2] == 51)
              return '\x0013';
            if ((int) chArray[2] == 52)
              return '\x0014';
          }
        }
        else if ((int) chArray[0] == 101)
        {
          if (length == 2 && (int) chArray[1] == 109)
            return '\x0019';
          if (length == 3)
          {
            if ((int) chArray[1] == 116 && (int) chArray[2] == 120)
              return '\x0003';
            if ((int) chArray[1] == 111 && (int) chArray[2] == 116)
              return '\x0004';
            if ((int) chArray[1] == 110 && (int) chArray[2] == 113)
              return '\x0005';
            if ((int) chArray[1] == 116 && (int) chArray[2] == 98)
              return '\x0017';
            if ((int) chArray[1] == 115 && (int) chArray[2] == 99)
              return '\x001B';
          }
        }
        else
        {
          if ((int) chArray[0] == 102 && length == 2 && (int) chArray[1] == 102)
            return '\f';
          if ((int) chArray[0] == 105 && length == 3)
          {
            if ((int) chArray[1] == 115)
            {
              if ((int) chArray[2] == 49)
                return '\x001F';
              if ((int) chArray[2] == 50)
                return '\x001E';
              if ((int) chArray[2] == 51)
                return '\x001D';
              if ((int) chArray[2] == 52)
                return '\x001C';
            }
          }
          else if ((int) chArray[0] == 110 && length == 3)
          {
            if ((int) chArray[1] == 117 && (int) chArray[2] == 108)
              return char.MinValue;
            if ((int) chArray[1] == 97 && (int) chArray[2] == 107)
              return '\x0015';
          }
          else if ((int) chArray[0] == 115)
          {
            switch (length)
            {
              case 2:
                if ((int) chArray[1] == 111)
                  return '\x000E';
                if ((int) chArray[1] == 105)
                  return '\x000F';
                else
                  break;
              case 3:
                if ((int) chArray[1] == 111 && (int) chArray[2] == 104)
                  return '\x0001';
                if ((int) chArray[1] == 116 && (int) chArray[2] == 120)
                  return '\x0002';
                if ((int) chArray[1] == 121 && (int) chArray[2] == 110)
                  return '\x0016';
                if ((int) chArray[1] == 117 && (int) chArray[2] == 98)
                  return '\x001A';
                else
                  break;
            }
          }
          else if ((int) chArray[0] == 118 && length == 2 && (int) chArray[1] == 116)
            return '\v';
        }
      }
      throw new ArgumentException("control character cannot be found for '" + sequence + "'.");
    }

    public void defineEntityReplacementText(string entityName, string replacementText)
    {
      this.ensureEntityCapacity();
      this.entityName[this.entityEnd] = this.newString(entityName.ToCharArray(), 0, entityName.Length);
      this.entityNameBuf[this.entityEnd] = entityName.ToCharArray();
      this.entityReplacement[this.entityEnd] = replacementText;
      this.entityReplacementBuf[this.entityEnd] = replacementText.ToCharArray();
      if (!this.allStringsInterned)
        this.entityNameHash[this.entityEnd] = XmlAsn1InputStream.fastHash(this.entityNameBuf[this.entityEnd], 0, this.entityNameBuf[this.entityEnd].Length);
      ++this.entityEnd;
    }

    private void ensureAttributesCapacity(int size)
    {
      int length1 = this.attributeName != null ? this.attributeName.Length : 0;
      if (size < length1)
        return;
      int length2 = size > 7 ? 2 * size : 8;
      bool flag = length1 > 0;
      string[] strArray1 = (string[]) null;
      string[] strArray2 = new string[length2];
      if (flag)
        Array.Copy((Array) this.attributeName, 0, (Array) strArray2, 0, length1);
      this.attributeName = strArray2;
      string[] strArray3 = new string[length2];
      if (flag)
        Array.Copy((Array) this.attributePrefix, 0, (Array) strArray3, 0, length1);
      this.attributePrefix = strArray3;
      string[] strArray4 = new string[length2];
      if (flag)
        Array.Copy((Array) this.attributeUri, 0, (Array) strArray4, 0, length1);
      this.attributeUri = strArray4;
      string[] strArray5 = new string[length2];
      if (flag)
        Array.Copy((Array) this.attributeValue, 0, (Array) strArray5, 0, length1);
      this.attributeValue = strArray5;
      if (!this.allStringsInterned)
      {
        int[] numArray = new int[length2];
        if (flag)
          Array.Copy((Array) this.attributeNameHash, 0, (Array) numArray, 0, length1);
        this.attributeNameHash = numArray;
      }
      strArray1 = (string[]) null;
    }

    private void ensureElementsCapacity()
    {
      int length1 = this.elName != null ? this.elName.Length : 0;
      if (this.depth + 1 < length1)
        return;
      int length2 = (this.depth >= 7 ? 2 * this.depth : 8) + 2;
      bool flag = length1 > 0;
      string[] strArray1 = new string[length2];
      if (flag)
        Array.Copy((Array) this.elName, 0, (Array) strArray1, 0, length1);
      this.elName = strArray1;
      string[] strArray2 = new string[length2];
      if (flag)
        Array.Copy((Array) this.elPrefix, 0, (Array) strArray2, 0, length1);
      this.elPrefix = strArray2;
      string[] strArray3 = new string[length2];
      if (flag)
        Array.Copy((Array) this.elUri, 0, (Array) strArray3, 0, length1);
      this.elUri = strArray3;
      int[] numArray1 = new int[length2];
      if (flag)
        Array.Copy((Array) this.elNamespaceCount, 0, (Array) numArray1, 0, length1);
      else
        numArray1[0] = 0;
      this.elNamespaceCount = numArray1;
      int[] numArray2 = new int[length2];
      if (flag)
        Array.Copy((Array) this.elRawNameEnd, 0, (Array) numArray2, 0, length1);
      this.elRawNameEnd = numArray2;
      int[] numArray3 = new int[length2];
      if (flag)
        Array.Copy((Array) this.elRawNameLine, 0, (Array) numArray3, 0, length1);
      this.elRawNameLine = numArray3;
      char[][] chArray = new char[length2][];
      if (flag)
        Array.Copy((Array) this.elRawName, 0, (Array) chArray, 0, length1);
      this.elRawName = chArray;
    }

    private void ensureEntityCapacity()
    {
      if (this.entityEnd < (this.entityReplacementBuf != null ? this.entityReplacementBuf.Length : 0))
        return;
      int length = this.entityEnd > 7 ? 2 * this.entityEnd : 8;
      string[] strArray1 = new string[length];
      char[][] chArray1 = new char[length][];
      string[] strArray2 = new string[length];
      char[][] chArray2 = new char[length][];
      if (this.entityName != null)
      {
        Array.Copy((Array) this.entityName, 0, (Array) strArray1, 0, this.entityEnd);
        Array.Copy((Array) this.entityNameBuf, 0, (Array) chArray1, 0, this.entityEnd);
        Array.Copy((Array) this.entityReplacement, 0, (Array) strArray2, 0, this.entityEnd);
        Array.Copy((Array) this.entityReplacementBuf, 0, (Array) chArray2, 0, this.entityEnd);
      }
      this.entityName = strArray1;
      this.entityNameBuf = chArray1;
      this.entityReplacement = strArray2;
      this.entityReplacementBuf = chArray2;
      if (!this.allStringsInterned)
      {
        int[] numArray = new int[length];
        if (this.entityNameHash != null)
          Array.Copy((Array) this.entityNameHash, 0, (Array) numArray, 0, this.entityEnd);
        this.entityNameHash = numArray;
      }
    }

    private void ensureNamespacesCapacity(int size)
    {
      int num = this.namespacePrefix != null ? this.namespacePrefix.Length : 0;
      if (size < num)
        return;
      int length = size > 7 ? 2 * size : 8;
      string[] strArray1 = new string[length];
      string[] strArray2 = new string[length];
      if (this.namespacePrefix != null)
      {
        Array.Copy((Array) this.namespacePrefix, 0, (Array) strArray1, 0, this.namespaceEnd);
        Array.Copy((Array) this.namespaceUri, 0, (Array) strArray2, 0, this.namespaceEnd);
      }
      this.namespacePrefix = strArray1;
      this.namespaceUri = strArray2;
      if (!this.allStringsInterned)
      {
        int[] numArray = new int[length];
        if (this.namespacePrefixHash != null)
          Array.Copy((Array) this.namespacePrefixHash, 0, (Array) numArray, 0, this.namespaceEnd);
        this.namespacePrefixHash = numArray;
      }
    }

    private void ensurePC(int end)
    {
      char[] chArray = new char[end > 512 ? 2 * end : 1024];
      Array.Copy((Array) this.pc, 0, (Array) chArray, 0, this.pcEnd);
      this.pc = chArray;
    }

    public static string escapeXerIdentifier(string identifier)
    {
      int length = identifier.Length;
      char[] chArray1 = new char[length];
      int index1 = 0;
      int destinationIndex = 0;
      for (; index1 < length; ++index1)
      {
        char ch = identifier[index1];
        int index2 = -1;
        switch (ch)
        {
          case '&':
            index2 = 2;
            break;
          case '<':
            index2 = 1;
            break;
          case '>':
            index2 = 0;
            break;
        }
        if (index2 != -1)
        {
          char[] chArray2 = XmlAsn1InputStream.xerIdentifierEscapedChars[index2];
          char[] chArray3 = chArray1;
          chArray1 = new char[chArray1.Length + chArray2.Length - 1];
          Array.Copy((Array) chArray3, 0, (Array) chArray1, 0, chArray3.Length);
          Array.Copy((Array) chArray2, 0, (Array) chArray1, destinationIndex, chArray2.Length);
          destinationIndex += chArray2.Length;
        }
        else
          chArray1[destinationIndex++] = ch;
      }
      return new string(chArray1);
    }

    private static int fastHash(char[] ch, int off, int len)
    {
      if (len == 0)
        return 0;
      int num = ((int) ch[off] << 7) + (int) ch[off + len - 1];
      if (len > 16)
        num = (num << 7) + (int) ch[off + len / 4];
      if (len > 8)
        num = (num << 7) + (int) ch[off + len / 2];
      return num;
    }

    private void fillBuf()
    {
      if (this.bufEnd > this.bufSoftLimit)
      {
        bool flag1 = this.bufStart > this.bufSoftLimit;
        bool flag2 = false;
        if (this.keepBuffer)
        {
          flag1 = false;
          flag2 = true;
        }
        else if (!flag1)
        {
          if (this.bufStart < this.buf.Length / 2)
            flag2 = true;
          else
            flag1 = true;
        }
        if (flag1)
        {
          Array.Copy((Array) this.buf, this.bufStart, (Array) this.buf, 0, this.bufEnd - this.bufStart);
        }
        else
        {
          if (!flag2)
            throw new Asn1Exception(35, "internal error in fillBuffer()");
          char[] chArray = new char[2 * this.buf.Length];
          Array.Copy((Array) this.buf, this.bufStart, (Array) chArray, 0, this.bufEnd - this.bufStart);
          this.buf = chArray;
          if (this.bufLoadFactor > 0)
            this.bufSoftLimit = (int) ((long) (this.bufLoadFactor * this.buf.Length) / 100L);
        }
        this.bufEnd -= this.bufStart;
        this.pos -= this.bufStart;
        this.posStart -= this.bufStart;
        this.posEnd -= this.bufStart;
        this.bufAbsoluteStart += this.bufStart;
        this.bufStart = 0;
      }
      int utF8Chars = this.getUTF8Chars(this.buf.Length - this.bufEnd > 512 ? 512 : this.buf.Length - this.bufEnd);
      if (utF8Chars > 0)
      {
        this.bufEnd += utF8Chars;
      }
      else
      {
        if (utF8Chars != -1)
          throw new Asn1Exception(46, "error reading input, read returned " + (object) utF8Chars);
        if (this.bufAbsoluteStart == 0 && this.pos == 0)
          throw new Asn1Exception(7, "input contained no data");
        if (!this.seenRoot || this.depth != 0)
          throw new Asn1Exception(7, "no more data available (position: " + this.getPositionDescription() + ")");
        this.reachedEnd = true;
      }
    }

    private static int findFragment(int bufMinPos, char[] b, int start, int end)
    {
      if (start < bufMinPos)
      {
        start = bufMinPos;
        if (start > end)
          start = end;
        return start;
      }
      else
      {
        if (end - start > 65)
          start = end - 10;
        int index = start + 1;
        do
          ;
        while (--index > bufMinPos && end - index <= 65 && ((int) b[index] != 60 || start - index <= 10));
        return index;
      }
    }

    public int getAttributeCount()
    {
      if (this.eventType != 2)
        return -1;
      else
        return this.attributeCount;
    }

    public string getAttributeName(int index)
    {
      if (this.eventType != 2)
        throw new IndexOutOfRangeException("only START_TAG can have attributes");
      if (index >= 0 && index < this.attributeCount)
        return this.attributeName[index];
      throw new IndexOutOfRangeException(string.Concat(new object[4]
      {
        (object) "attribute position must be 0..",
        (object) (this.attributeCount - 1),
        (object) " and not ",
        (object) index
      }));
    }

    public string getAttributeNamespace(int index)
    {
      if (this.eventType != 2)
        throw new IndexOutOfRangeException("only START_TAG can have attributes");
      if (!this.processNamespaces)
        return "";
      if (index >= 0 && index < this.attributeCount)
        return this.attributeUri[index];
      throw new IndexOutOfRangeException(string.Concat(new object[4]
      {
        (object) "attribute position must be 0..",
        (object) (this.attributeCount - 1),
        (object) " and not ",
        (object) index
      }));
    }

    public string getAttributePrefix(int index)
    {
      if (this.eventType != 2)
        throw new IndexOutOfRangeException("only START_TAG can have attributes");
      if (!this.processNamespaces)
        return (string) null;
      if (index >= 0 && index < this.attributeCount)
        return this.attributePrefix[index];
      throw new IndexOutOfRangeException(string.Concat(new object[4]
      {
        (object) "attribute position must be 0..",
        (object) (this.attributeCount - 1),
        (object) " and not ",
        (object) index
      }));
    }

    public string getAttributeType(int index)
    {
      if (this.eventType != 2)
        throw new IndexOutOfRangeException("only START_TAG can have attributes");
      if (index >= 0 && index < this.attributeCount)
        return "CDATA";
      throw new IndexOutOfRangeException(string.Concat(new object[4]
      {
        (object) "attribute position must be 0..",
        (object) (this.attributeCount - 1),
        (object) " and not ",
        (object) index
      }));
    }

    public string getAttributeValue(int index)
    {
      if (this.eventType != 2)
        throw new IndexOutOfRangeException("only START_TAG can have attributes");
      if (index >= 0 && index < this.attributeCount)
        return this.attributeValue[index];
      throw new IndexOutOfRangeException(string.Concat(new object[4]
      {
        (object) "attribute position must be 0..",
        (object) (this.attributeCount - 1),
        (object) " and not ",
        (object) index
      }));
    }

    public string getAttributeValue(string nmspace, string name)
    {
      if (this.eventType != 2)
        throw new IndexOutOfRangeException("only START_TAG can have attributes" + this.getPositionDescription());
      if (name == null)
        throw new ArgumentException("attribute name can not be null");
      if (this.processNamespaces)
      {
        if (nmspace == null)
          nmspace = "";
        for (int index = 0; index < this.attributeCount; ++index)
        {
          if ((nmspace == this.attributeUri[index] || nmspace.Equals(this.attributeUri[index])) && name.Equals(this.attributeName[index]))
            return this.attributeValue[index];
        }
      }
      else
      {
        if (nmspace != null && nmspace.Length == 0)
          nmspace = (string) null;
        if (nmspace != null)
          throw new ArgumentException("when namespaces processing is disabled attribute namespace must be null");
        for (int index = 0; index < this.attributeCount; ++index)
        {
          if (name.Equals(this.attributeName[index]))
            return this.attributeValue[index];
        }
      }
      return (string) null;
    }

    public string getAttributeValueNamespace(string attValue)
    {
      if (attValue != null)
      {
        int length = attValue.IndexOf(':');
        if (length != -1)
          return this.getNamespace(attValue.Substring(0, length));
      }
      return (string) null;
    }

    public string getAttributeValueValue(string attValue)
    {
      if (attValue == null)
        return (string) null;
      int num = attValue.IndexOf(':');
      if (num != -1)
        return attValue.Substring(num + 1);
      else
        return attValue;
    }

    public int getColumnNumber()
    {
      return this.columnNumber;
    }

    public string getDeclContent()
    {
      return this.xmlDeclContent;
    }

    public string getDeclStandalone()
    {
      return this.xmlDeclStandalone;
    }

    public int getDepth()
    {
      return this.depth;
    }

    public int getEventType()
    {
      return this.eventType;
    }

    public string getInputEncoding()
    {
      return this.inputEncoding;
    }

    public string getKeptContent()
    {
      return new string(this.buf, this.bufStart, this.pos - this.bufStart);
    }

    public string getKeptContent(string tag, string ns, string prefix)
    {
      if (tag != null)
      {
        StringBuilder stringBuilder1 = new StringBuilder();
        stringBuilder1.Append("<");
        if (prefix != null)
        {
          stringBuilder1.Append(prefix);
          stringBuilder1.Append(":");
        }
        stringBuilder1.Append(tag);
        StringBuilder stringBuilder2 = new StringBuilder();
        for (int pos = 0; pos < this.getNamespaceCount(this.depth); ++pos)
        {
          string namespaceUri = this.getNamespaceUri(pos);
          if (!ns.Equals(namespaceUri))
          {
            stringBuilder2.Append(" ").Append("xmlns");
            string namespacePrefix = this.getNamespacePrefix(pos);
            if (namespacePrefix != null && namespacePrefix.Length > 0)
              stringBuilder2.Append(":").Append(namespacePrefix);
            stringBuilder2.Append("='").Append(this.getNamespaceUri(pos)).Append("'");
          }
        }
        stringBuilder1.Append((object) stringBuilder2);
        if (stringBuilder1 != null)
        {
          stringBuilder1.Append(" ").Append("xmlns");
          if (prefix != null && prefix.Length > 0)
            stringBuilder1.Append(":").Append(prefix);
          stringBuilder1.Append("='").Append(ns).Append("'");
        }
        stringBuilder1.Append(">");
        stringBuilder1.Append(this.getKeptContent());
        stringBuilder1.Append("</");
        if (prefix != null)
        {
          stringBuilder1.Append(prefix);
          stringBuilder1.Append(":");
        }
        stringBuilder1.Append(tag);
        stringBuilder1.Append(">");
        return ((object) stringBuilder1).ToString();
      }
      else
      {
        int num = 0;
        for (int index = this.bufStart; index < this.pos; ++index)
        {
          if ((int) this.buf[index] == 62)
          {
            num = index - this.bufStart;
            break;
          }
        }
        string str = new string(this.buf, this.bufStart, num);
        StringBuilder stringBuilder1 = new StringBuilder(this.getKeptContent());
        StringBuilder stringBuilder2 = new StringBuilder();
        for (int pos = 0; pos < this.getNamespaceCount(this.depth); ++pos)
        {
          string namespaceUri = this.getNamespaceUri(pos);
          if (str.IndexOf(namespaceUri) == -1)
          {
            stringBuilder2.Append(" ").Append("xmlns");
            string namespacePrefix = this.getNamespacePrefix(pos);
            if (namespacePrefix != null && namespacePrefix.Length > 0)
              stringBuilder2.Append(":").Append(namespacePrefix);
            stringBuilder2.Append("='").Append(this.getNamespaceUri(pos)).Append("'");
          }
        }
        stringBuilder1.Insert(num, ((object) stringBuilder2).ToString());
        return ((object) stringBuilder1).ToString();
      }
    }

    public int getLineNumber()
    {
      return this.lineNumber;
    }

    public string getName()
    {
      if (this.eventType == 2 || this.eventType == 3)
        return this.elName[this.depth];
      if (this.eventType != 6)
        return (string) null;
      if (this.entityRefName == null)
        this.entityRefName = this.newString(this.buf, this.posStart, this.posEnd - this.posStart);
      return this.entityRefName;
    }

    public string getNamespace()
    {
      if (this.eventType == 2)
      {
        if (!this.processNamespaces)
          return "";
        else
          return this.elUri[this.depth];
      }
      else
      {
        if (this.eventType != 3)
          return (string) null;
        if (!this.processNamespaces)
          return "";
        else
          return this.elUri[this.depth];
      }
    }

    public string getNamespace(string prefix)
    {
      if (prefix != null)
      {
        for (int index = this.namespaceEnd - 1; index >= 0; --index)
        {
          if (prefix.Equals(this.namespacePrefix[index]))
            return this.namespaceUri[index];
        }
        if ("xml".Equals(prefix))
          return "http://www.w3.org/XML/1998/namespace";
        if ("xmlns".Equals(prefix))
          return "http://www.w3.org/2000/xmlns/";
      }
      else
      {
        for (int index = this.namespaceEnd - 1; index >= 0; --index)
        {
          if (this.namespacePrefix[index] == null)
            return this.namespaceUri[index];
        }
      }
      return (string) null;
    }

    public int getNamespaceCount(int depth)
    {
      if (!this.processNamespaces || depth == 0)
        return 0;
      if (depth >= 0 && depth <= this.depth)
        return this.elNamespaceCount[depth];
      throw new ArgumentException(string.Concat(new object[4]
      {
        (object) "allowed namespace depth 0..",
        (object) this.depth,
        (object) " not ",
        (object) depth
      }));
    }

    public string getNamespacePrefix(int pos)
    {
      if (pos < this.namespaceEnd)
        return this.namespacePrefix[pos];
      throw new Asn1Exception(35, string.Concat(new object[4]
      {
        (object) "position ",
        (object) pos,
        (object) " exceeded number of available namespaces ",
        (object) this.namespaceEnd
      }));
    }

    public string getNamespaceUri(int pos)
    {
      if (pos < this.namespaceEnd)
        return this.namespaceUri[pos];
      throw new Asn1Exception(35, string.Concat(new object[4]
      {
        (object) "position ",
        (object) pos,
        (object) " exceeded number of available namespaces ",
        (object) this.namespaceEnd
      }));
    }

    public string getPositionDescription()
    {
      string s = (string) null;
      if (this.posStart <= this.pos)
      {
        int fragment = XmlAsn1InputStream.findFragment(0, this.buf, this.posStart, this.pos);
        if (fragment < this.pos)
          s = new string(this.buf, fragment, this.pos - fragment);
        if (this.bufAbsoluteStart > 0 || fragment > 0)
          s = "..." + s;
      }
      return " " + (object) XmlAsn1InputStream.TYPES[this.eventType] + (string) (s != null ? (object) (" seen " + this.printable(s) + "...") : (object) "") + " @" + (string) (object) this.getLineNumber() + ":" + (string) (object) this.getColumnNumber();
    }

    public string getPrefix()
    {
      if (this.eventType == 2 || this.eventType == 3)
        return this.elPrefix[this.depth];
      else
        return (string) null;
    }

    public string getText()
    {
      if (this.eventType == 0 || this.eventType == 1)
        return (string) null;
      if (this.eventType != 6 && this.text == null)
        this.text = this.usePC && this.eventType != 2 && this.eventType != 3 ? new string(this.pc, this.pcStart, this.pcEnd - this.pcStart) : new string(this.buf, this.posStart, this.posEnd - this.posStart);
      return this.text;
    }

    public char[] getTextCharacters(int[] holderForStartAndLength)
    {
      if (this.eventType == 4)
      {
        if (this.usePC)
        {
          holderForStartAndLength[0] = this.pcStart;
          holderForStartAndLength[1] = this.pcEnd - this.pcStart;
          return this.pc;
        }
        else
        {
          holderForStartAndLength[0] = this.posStart;
          holderForStartAndLength[1] = this.posEnd - this.posStart;
          return this.buf;
        }
      }
      else if (this.eventType == 2 || this.eventType == 3 || (this.eventType == 5 || this.eventType == 9) || (this.eventType == 6 || this.eventType == 8 || (this.eventType == 7 || this.eventType == 10)))
      {
        holderForStartAndLength[0] = this.posStart;
        holderForStartAndLength[1] = this.posEnd - this.posStart;
        return this.buf;
      }
      else
      {
        if (this.eventType != 0 && this.eventType != 1)
          throw new ArgumentException("unknown text eventType: " + (object) this.eventType);
        holderForStartAndLength[0] = holderForStartAndLength[1] = -1;
        return (char[]) null;
      }
    }

    public int getUsedBytes()
    {
      return this.readBytes;
    }

    public virtual int getUsedChars()
    {
      return this.usedChars;
    }

    protected int getUTF8Chars(int maxCharsToRead)
    {
      int num1 = this.bufStreamIndex + maxCharsToRead >= this.bufStream.Length ? this.bufStream.Length - this.bufStreamIndex : maxCharsToRead;
      int num2;
      try
      {
        num2 = this.inputStream.Read(this.bufStream, this.bufStreamIndex, num1 - this.bufStreamIndex);
        if (num2 == 0)
          return -1;
      }
      catch (EndOfStreamException ex)
      {
        return -1;
      }
      catch (IOException ex)
      {
        throw new Asn1Exception(46, ex.Message, (Exception) ex);
      }
      int length1 = num2 + this.bufStreamIndex;
      char[] chArray = new char[length1];
      int length2 = 0;
      for (int index = 0; index < length1; ++index)
      {
        byte num3 = this.bufStream[index];
        int num4 = 0;
        if (((int) num3 & 128) == 0 || (int) num3 == 0)
        {
          chArray[length2] = (char) num3;
          ++length2;
        }
        else if (192 == ((int) num3 & 224))
        {
          if (index + 1 >= length1)
          {
            this.readBytes += index + 1 - this.bufStreamIndex;
            this.bufStream[0] = this.bufStream[index];
            this.bufStreamIndex = 1;
            Array.Copy((Array) chArray, 0, (Array) this.buf, this.bufEnd, length2);
            return length2;
          }
          else
          {
            int num5 = (int) this.bufStream[index + 1];
            if (192 == ((int) num3 & 254))
            {
              throw new Asn1Exception(35, string.Concat(new object[4]
              {
                (object) "Invalid UTF-8 format b1=",
                (object) num3,
                (object) " b2=",
                (object) num5
              }));
            }
            else
            {
              int num6 = num4 | num5 & 63 | ((int) num3 & 31) << 6;
              chArray[length2] = (char) num6;
              ++length2;
              ++index;
            }
          }
        }
        else if (224 == ((int) num3 & 240))
        {
          if (index + 1 >= length1)
          {
            this.readBytes += index + 1 - this.bufStreamIndex;
            this.bufStream[0] = this.bufStream[index];
            this.bufStreamIndex = 1;
            Array.Copy((Array) chArray, 0, (Array) this.buf, this.bufEnd, length2);
            return length2;
          }
          else
          {
            int num5 = (int) this.bufStream[index + 1];
            if (index + 2 >= length1)
            {
              this.readBytes += index + 2 - this.bufStreamIndex;
              this.bufStream[0] = this.bufStream[index];
              this.bufStream[1] = this.bufStream[index + 1];
              this.bufStreamIndex = 2;
              Array.Copy((Array) chArray, 0, (Array) this.buf, this.bufEnd, length2);
              return length2;
            }
            else
            {
              int num6 = (int) this.bufStream[index + 2];
              if (224 == (int) num3 && 128 == (num5 & 224))
              {
                throw new Asn1Exception(35, "Invalid UTF-8 format b1=" + (object) num3 + " b2=" + (string) (object) num5 + " b3=" + (string) (object) num6);
              }
              else
              {
                int num7 = num4 | num6 & 63 | (num5 & 63) << 6 | ((int) num3 & 15) << 12;
                if (55296 <= num7 && num7 <= 57343)
                  throw new Asn1Exception(35, "Unauthorized UTF-8 character ch=" + (object) num7);
                if (65534 <= num7 && num7 <= (int) ushort.MaxValue)
                  throw new Asn1Exception(35, "Unauthorized UTF-8 character ch=" + (object) num7);
                chArray[length2] = (char) num7;
                ++length2;
                index += 2;
              }
            }
          }
        }
        else
        {
          if (240 != ((int) num3 & 248))
            throw new Asn1Exception(35, "Invalid UTF-8 format b1=" + (object) num3);
          if (index + 1 >= length1)
          {
            this.readBytes += index + 1 - this.bufStreamIndex;
            this.bufStream[0] = this.bufStream[index];
            this.bufStreamIndex = 1;
            Array.Copy((Array) chArray, 0, (Array) this.buf, this.bufEnd, length2);
            return length2;
          }
          else
          {
            int num5 = (int) this.bufStream[index + 1];
            if (index + 2 >= length1)
            {
              this.readBytes += index + 2 - this.bufStreamIndex;
              this.bufStream[0] = this.bufStream[index];
              this.bufStream[1] = this.bufStream[index + 1];
              this.bufStreamIndex = 2;
              Array.Copy((Array) chArray, 0, (Array) this.buf, this.bufEnd, length2);
              return length2;
            }
            else
            {
              int num6 = (int) this.bufStream[index + 2];
              if (index + 3 >= length1)
              {
                this.readBytes += index + 3 - this.bufStreamIndex;
                this.bufStream[0] = this.bufStream[index];
                this.bufStream[1] = this.bufStream[index + 1];
                this.bufStream[2] = this.bufStream[index + 2];
                this.bufStreamIndex = 3;
                Array.Copy((Array) chArray, 0, (Array) this.buf, this.bufEnd, length2);
                return length2;
              }
              else
              {
                int num7 = (int) this.bufStream[index + 3];
                if (240 == (int) num3 && 128 == (num5 & 240))
                {
                  throw new Asn1Exception(35, "Invalid UTF-8 format b1=" + (object) num3 + " b2=" + (string) (object) num5 + " b3=" + (string) (object) num6 + " b4=" + (string) (object) num7);
                }
                else
                {
                  int num8 = num4 | num7 & 63 | (num6 & 63) << 6 | (num5 & 63) << 12 | ((int) num3 & 7) << 18;
                  if (num8 < 0 || num8 > 1114111)
                  {
                    throw new Asn1Exception(35, "Invalid UTF-8 format b1=" + (object) num3 + " b2=" + (string) (object) num5 + " b3=" + (string) (object) num6 + " b4=" + (string) (object) num7);
                  }
                  else
                  {
                    if (num8 < 65536)
                    {
                      chArray[length2] = (char) num8;
                      ++length2;
                    }
                    else
                    {
                      int number = num8 - 65536;
                      chArray[length2 + 1] = (char) ((number & 1023) + 56320);
                      chArray[length2] = (char) (Tools.URShift(number, 10) + 55296);
                      length2 = length2 + 1 + 1;
                    }
                    index += 3;
                  }
                }
              }
            }
          }
        }
      }
      Array.Copy((Array) chArray, 0, (Array) this.buf, this.bufEnd, length2);
      this.readBytes += num2;
      this.bufStreamIndex = 0;
      return length2;
    }

    public string getXmlDeclVersion()
    {
      return this.xmlDeclVersion;
    }

    public bool isAttributeDefault(int index)
    {
      if (this.eventType != 2)
        throw new IndexOutOfRangeException("only START_TAG can have attributes");
      if (index >= 0 && index < this.attributeCount)
        return false;
      throw new IndexOutOfRangeException(string.Concat(new object[4]
      {
        (object) "attribute position must be 0..",
        (object) (this.attributeCount - 1),
        (object) " and not ",
        (object) index
      }));
    }

    public bool isEmptyElementTag()
    {
      if (this.eventType != 2)
        throw new Asn1Exception(35, "parser must be on START_TAG to check for empty element (position:" + this.getPositionDescription() + ")");
      else
        return this.emptyElementTag;
    }

    private bool isNameChar(char ch)
    {
      if (((int) ch >= (int) XmlAsn1InputStream.LOOKUP_MAX_CHAR || !XmlAsn1InputStream.lookupNameChar[(int) ch]) && ((int) ch < (int) XmlAsn1InputStream.LOOKUP_MAX_CHAR || (int) ch > 8231) && ((int) ch < 8234 || (int) ch > 8591))
        return (int) ch >= 10240 && (int) ch <= 65519;
      else
        return true;
    }

    private bool isNameStartChar(char ch)
    {
      if (((int) ch >= (int) XmlAsn1InputStream.LOOKUP_MAX_CHAR || !XmlAsn1InputStream.lookupNameStartChar[(int) ch]) && ((int) ch < (int) XmlAsn1InputStream.LOOKUP_MAX_CHAR || (int) ch > 8231) && ((int) ch < 8234 || (int) ch > 8591))
        return (int) ch >= 10240 && (int) ch <= 65519;
      else
        return true;
    }

    private bool isS(char ch)
    {
      if ((int) ch != 32 && (int) ch != 10 && (int) ch != 13)
        return (int) ch == 9;
      else
        return true;
    }

    public bool isWhitespace()
    {
      if (this.eventType == 4 || this.eventType == 5)
      {
        if (this.usePC)
        {
          for (int index = this.pcStart; index < this.pcEnd; ++index)
          {
            if (!this.isS(this.pc[index]))
              return false;
          }
          return true;
        }
        else
        {
          for (int index = this.posStart; index < this.posEnd; ++index)
          {
            if (!this.isS(this.buf[index]))
              return false;
          }
          return true;
        }
      }
      else if (this.eventType != 7)
        throw new Asn1Exception(35, "no content available to check for whitespaces");
      else
        return true;
    }

    private void joinPC()
    {
      int length = this.posEnd - this.posStart;
      int end = this.pcEnd + length + 1;
      if (end >= this.pc.Length)
        this.ensurePC(end);
      Array.Copy((Array) this.buf, this.posStart, (Array) this.pc, this.pcEnd, length);
      this.pcEnd += length;
      this.usePC = true;
    }

    private char[] lookuEntityReplacement(int entitNameLen)
    {
      if (!this.allStringsInterned)
      {
        int num = XmlAsn1InputStream.fastHash(this.buf, this.posStart, this.posEnd - this.posStart);
        for (int index1 = this.entityEnd - 1; index1 >= 0; --index1)
        {
          if (num == this.entityNameHash[index1] && entitNameLen == this.entityNameBuf[index1].Length)
          {
            char[] chArray = this.entityNameBuf[index1];
            for (int index2 = 0; index2 < entitNameLen; ++index2)
            {
              if ((int) this.buf[this.posStart + index2] == (int) chArray[index2])
                ;
            }
            if (this.tokenize)
              this.text = this.entityReplacement[index1];
            return this.entityReplacementBuf[index1];
          }
        }
      }
      else
      {
        this.entityRefName = this.newString(this.buf, this.posStart, this.posEnd - this.posStart);
        for (int index = this.entityEnd - 1; index >= 0; --index)
        {
          if (this.entityRefName == this.entityName[index])
          {
            if (this.tokenize)
              this.text = this.entityReplacement[index];
            return this.entityReplacementBuf[index];
          }
        }
      }
      return (char[]) null;
    }

    protected char more()
    {
      if (this.pos >= this.bufEnd)
      {
        this.fillBuf();
        if (this.reachedEnd)
          return char.MaxValue;
      }
      char ch = this.buf[this.pos++];
      if ((int) ch == 10)
      {
        ++this.lineNumber;
        this.columnNumber = 1;
      }
      else
        ++this.columnNumber;
      ++this.usedChars;
      return ch;
    }

    private string newString(char[] cbuf, int off, int len)
    {
      return new string(cbuf, off, len);
    }

    private string newStringIntern(char[] cbuf, int off, int len)
    {
      return string.Intern(new string(cbuf, off, len));
    }

    public int next()
    {
      this.tokenize = false;
      return this.nextImpl();
    }

    private int nextImpl()
    {
      this.text = (string) null;
      this.pcEnd = this.pcStart = 0;
      this.usePC = false;
      if (!this.keepBuffer)
        this.bufStart = this.posEnd;
      if (this.pastEndTag)
      {
        this.pastEndTag = false;
        --this.depth;
        this.namespaceEnd = this.elNamespaceCount[this.depth];
      }
      if (this.emptyElementTag)
      {
        this.emptyElementTag = false;
        this.pastEndTag = true;
        return this.eventType = 3;
      }
      else if (this.depth <= 0)
      {
        if (this.seenRoot)
          return this.parseEpilog();
        else
          return this.parseProlog();
      }
      else if (this.seenStartTag)
      {
        this.seenStartTag = false;
        return this.eventType = this.parseStartTag();
      }
      else if (this.seenEndTag)
      {
        this.seenEndTag = false;
        return this.eventType = this.parseEndTag();
      }
      else
      {
        char ch1;
        if (this.seenMarkup)
        {
          this.seenMarkup = false;
          ch1 = '<';
        }
        else if (this.seenAmpersand)
        {
          this.seenAmpersand = false;
          ch1 = '&';
        }
        else
          ch1 = this.more();
        this.posStart = this.pos - 1;
        bool hadCharData = false;
        bool flag1 = false;
        char ch2;
        char ch3;
        while (true)
        {
          switch (ch1)
          {
            case '&':
              if (!this.tokenize || !hadCharData)
              {
                int num1 = this.posStart + this.bufAbsoluteStart;
                int num2 = this.posEnd + this.bufAbsoluteStart;
                char[] chArray = this.parseEntityRef();
                if (!this.tokenize)
                {
                  if (chArray != null)
                  {
                    this.posStart = num1 - this.bufAbsoluteStart;
                    this.posEnd = num2 - this.bufAbsoluteStart;
                    if (!this.usePC)
                    {
                      if (hadCharData)
                      {
                        this.joinPC();
                        flag1 = false;
                      }
                      else
                      {
                        this.usePC = true;
                        this.pcStart = this.pcEnd = 0;
                      }
                    }
                    for (int index = 0; index < chArray.Length; ++index)
                    {
                      if (this.pcEnd >= this.pc.Length)
                        this.ensurePC(this.pcEnd);
                      this.pc[this.pcEnd++] = chArray[index];
                    }
                    hadCharData = true;
                    break;
                  }
                  else
                    goto label_55;
                }
                else
                  goto label_53;
              }
              else
                goto label_51;
            case '<':
              if (!hadCharData || !this.tokenize)
              {
                ch2 = this.more();
                switch (ch2)
                {
                  case '!':
                    ch3 = this.more();
                    switch (ch3)
                    {
                      case '-':
                        this.parseComment();
                        if (!this.tokenize)
                        {
                          if (!this.usePC && hadCharData)
                          {
                            flag1 = true;
                            break;
                          }
                          else
                          {
                            this.posStart = this.pos;
                            break;
                          }
                        }
                        else
                          goto label_29;
                      case '[':
                        this.parseCDSect(hadCharData);
                        if (!this.tokenize)
                        {
                          if (this.posEnd - this.posStart > 0)
                          {
                            hadCharData = true;
                            if (!this.usePC)
                              flag1 = true;
                            break;
                          }
                          else
                            break;
                        }
                        else
                          goto label_34;
                      default:
                        goto label_39;
                    }
                  case '/':
                    goto label_24;
                  case '?':
                    this.parsePI();
                    if (!this.tokenize)
                    {
                      if (!this.usePC && hadCharData)
                      {
                        flag1 = true;
                        break;
                      }
                      else
                      {
                        this.posStart = this.pos;
                        break;
                      }
                    }
                    else
                      goto label_41;
                  default:
                    goto label_45;
                }
              }
              else
                goto label_22;
            default:
              if (flag1)
              {
                this.joinPC();
                flag1 = false;
              }
              hadCharData = true;
              bool flag2 = false;
              bool flag3 = !this.tokenize || !this.roundtripSupported;
              bool flag4 = false;
              bool flag5 = false;
              do
              {
                if ((int) ch1 == 93)
                {
                  if (flag4)
                    flag5 = true;
                  else
                    flag4 = true;
                }
                else
                {
                  if (flag5 && (int) ch1 == 62)
                    throw new Asn1Exception(35, "characters ]]> are not allowed in content (position:" + this.getPositionDescription() + ")");
                  if (flag4)
                    flag5 = flag4 = false;
                }
                if (flag3)
                {
                  if ((int) ch1 == 13)
                  {
                    flag2 = true;
                    this.posEnd = this.pos - 1;
                    if (!this.usePC)
                    {
                      if (this.posEnd > this.posStart)
                      {
                        this.joinPC();
                      }
                      else
                      {
                        this.usePC = true;
                        this.pcStart = this.pcEnd = 0;
                      }
                    }
                    if (this.pcEnd >= this.pc.Length)
                      this.ensurePC(this.pcEnd);
                    this.pc[this.pcEnd++] = '\n';
                  }
                  else if ((int) ch1 == 10)
                  {
                    if (!flag2 && this.usePC)
                    {
                      if (this.pcEnd >= this.pc.Length)
                        this.ensurePC(this.pcEnd);
                      this.pc[this.pcEnd++] = '\n';
                    }
                    flag2 = false;
                  }
                  else
                  {
                    if (this.usePC)
                    {
                      if (this.pcEnd >= this.pc.Length)
                        this.ensurePC(this.pcEnd);
                      this.pc[this.pcEnd++] = ch1;
                    }
                    flag2 = false;
                  }
                }
                ch1 = this.more();
              }
              while ((int) ch1 != 60 && (int) ch1 != 38);
              this.posEnd = this.pos - 1;
              continue;
          }
          ch1 = this.more();
        }
label_22:
        this.seenMarkup = true;
        return this.eventType = 4;
label_24:
        if (this.tokenize || !hadCharData)
          return this.eventType = this.parseEndTag();
        this.seenEndTag = true;
        return this.eventType = 4;
label_29:
        return this.eventType = 9;
label_34:
        return this.eventType = 5;
label_39:
        throw new Asn1Exception(35, "unexpected character in markup " + this.printable(ch3) + " (position:" + this.getPositionDescription() + ")");
label_41:
        return this.eventType = 8;
label_45:
        if (!this.isNameStartChar(ch2))
        {
          throw new Asn1Exception(35, "unexpected character in markup " + this.printable(ch2) + " (position:" + this.getPositionDescription() + ")");
        }
        else
        {
          if (this.tokenize || !hadCharData)
            return this.eventType = this.parseStartTag();
          this.seenStartTag = true;
          return this.eventType = 4;
        }
label_51:
        this.seenAmpersand = true;
        return this.eventType = 4;
label_53:
        return this.eventType = 6;
label_55:
        if (this.entityRefName == null)
          this.entityRefName = this.newString(this.buf, this.posStart, this.posEnd - this.posStart);
        throw new Asn1Exception(35, "could not resolve entity named '" + this.printable(this.entityRefName) + "' (position:" + this.getPositionDescription() + ")");
      }
    }

    public int nextTag()
    {
      this.next();
      if (this.eventType == 4 && this.isWhitespace())
        this.next();
      if (this.eventType == 2 || this.eventType == 3)
        return this.eventType;
      throw new Asn1Exception(35, "expected START_TAG or END_TAG not " + XmlAsn1InputStream.TYPES[this.getEventType()] + " (position:" + this.getPositionDescription() + ")");
    }

    public string nextText()
    {
      if (this.getEventType() != 2)
        throw new Asn1Exception(35, "parser must be on START_TAG to read next text (position:" + this.getPositionDescription() + ")");
      switch (this.next())
      {
        case 4:
          string text = this.getText();
          if (this.next() == 3)
            return text;
          throw new Asn1Exception(35, "TEXT must be immediately followed by END_TAG and not " + XmlAsn1InputStream.TYPES[this.getEventType()] + " (position:" + this.getPositionDescription() + ")");
        case 3:
          return "";
        default:
          throw new Asn1Exception(35, "parser must be on START_TAG or TEXT to read text (position:" + this.getPositionDescription() + ")");
      }
    }

    public int nextToken()
    {
      this.tokenize = true;
      return this.nextImpl();
    }

    private char parseAttribute()
    {
      int num1 = this.posStart + this.bufAbsoluteStart;
      int num2 = this.pos - 1 + this.bufAbsoluteStart;
      int num3 = -1;
      char ch1 = this.buf[this.pos - 1];
      if ((int) ch1 == 58 && this.processNamespaces)
        throw new Asn1Exception(35, "when namespaces processing enabled colon can not be at attribute name start (position:" + this.getPositionDescription() + ")");
      bool flag1 = this.processNamespaces && (int) ch1 == 120;
      int num4 = 0;
      char ch2;
      for (ch2 = this.more(); this.isNameChar(ch2); ch2 = this.more())
      {
        if (this.processNamespaces)
        {
          if (flag1 && num4 < 5)
          {
            ++num4;
            int num5;
            switch (num4)
            {
              case 1:
                if ((int) ch2 != 109)
                {
                  flag1 = false;
                  goto label_18;
                }
                else
                  goto label_18;
              case 2:
                if ((int) ch2 != 108)
                {
                  flag1 = false;
                  goto label_18;
                }
                else
                  goto label_18;
              case 3:
                if ((int) ch2 != 110)
                {
                  flag1 = false;
                  goto label_18;
                }
                else
                  goto label_18;
              case 4:
                if ((int) ch2 != 115)
                {
                  flag1 = false;
                  goto label_18;
                }
                else
                  goto label_18;
              case 5:
                num5 = (int) ch2 == 58 ? 1 : 0;
                break;
              default:
                num5 = 1;
                break;
            }
            if (num5 == 0)
              throw new Asn1Exception(35, "after xmlns in attribute name must be colon when namespaces are enabled (position:" + this.getPositionDescription() + ")");
          }
label_18:
          if ((int) ch2 == 58)
          {
            if (num3 != -1)
              throw new Asn1Exception(35, "only one colon is allowed in attribute name when namespaces are enabled (position:" + this.getPositionDescription() + ")");
            num3 = this.pos - 1 + this.bufAbsoluteStart;
          }
        }
      }
      this.ensureAttributesCapacity(this.attributeCount);
      string str1 = (string) null;
      if (this.processNamespaces)
      {
        if (num4 < 4)
          flag1 = false;
        if (flag1)
        {
          if (num3 != -1)
          {
            int len = this.pos - 2 - (num3 - this.bufAbsoluteStart);
            if (len == 0)
              throw new Asn1Exception(35, "namespace prefix is required after xmlns: when namespaces are enabled (position:" + this.getPositionDescription() + ")");
            str1 = this.newString(this.buf, num3 - this.bufAbsoluteStart + 1, len);
          }
        }
        else
        {
          if (num3 != -1)
          {
            int len1 = num3 - num2;
            this.attributePrefix[this.attributeCount] = this.newString(this.buf, num2 - this.bufAbsoluteStart, len1);
            int len2 = this.pos - 2 - (num3 - this.bufAbsoluteStart);
            str1 = this.attributeName[this.attributeCount] = this.newString(this.buf, num3 - this.bufAbsoluteStart + 1, len2);
          }
          else
          {
            this.attributePrefix[this.attributeCount] = (string) null;
            str1 = this.attributeName[this.attributeCount] = this.newString(this.buf, num2 - this.bufAbsoluteStart, this.pos - 1 - (num2 - this.bufAbsoluteStart));
          }
          if (!this.allStringsInterned)
            this.attributeNameHash[this.attributeCount] = str1.GetHashCode();
        }
      }
      else
      {
        str1 = this.attributeName[this.attributeCount] = this.newString(this.buf, num2 - this.bufAbsoluteStart, this.pos - 1 - (num2 - this.bufAbsoluteStart));
        if (!this.allStringsInterned)
          this.attributeNameHash[this.attributeCount] = str1.GetHashCode();
      }
      while (this.isS(ch2))
        ch2 = this.more();
      if ((int) ch2 != 61)
        throw new Asn1Exception(35, "expected = after attribute name (position:" + this.getPositionDescription() + ")");
      char ch3 = this.more();
      while (this.isS(ch3))
        ch3 = this.more();
      char ch4 = ch3;
      if ((int) ch4 != 34 && (int) ch4 != 39)
      {
        throw new Asn1Exception(35, "attribute value must start with quotation or apostrophe not " + this.printable(ch4) + " (position:" + this.getPositionDescription() + ")");
      }
      else
      {
        bool flag2 = false;
        this.usePC = false;
        this.pcStart = this.pcEnd;
        this.posStart = this.pos;
        char ch5;
        while (true)
        {
          ch5 = this.more();
          if ((int) ch5 != (int) ch4)
          {
            if ((int) ch5 != 60)
            {
              if ((int) ch5 == 38)
              {
                this.posEnd = this.pos - 1;
                if (!this.usePC)
                {
                  if (this.posEnd > this.posStart)
                  {
                    this.joinPC();
                  }
                  else
                  {
                    this.usePC = true;
                    this.pcStart = this.pcEnd = 0;
                  }
                }
                char[] chArray = this.parseEntityRef();
                if (chArray != null)
                {
                  for (int index = 0; index < chArray.Length; ++index)
                  {
                    if (this.pcEnd >= this.pc.Length)
                      this.ensurePC(this.pcEnd);
                    this.pc[this.pcEnd++] = chArray[index];
                  }
                }
                else
                  goto label_60;
              }
              else if ((int) ch5 == 9 || (int) ch5 == 10 || (int) ch5 == 13)
              {
                if (!this.usePC)
                {
                  this.posEnd = this.pos - 1;
                  if (this.posEnd > this.posStart)
                  {
                    this.joinPC();
                  }
                  else
                  {
                    this.usePC = true;
                    this.pcEnd = this.pcStart = 0;
                  }
                }
                if (this.pcEnd >= this.pc.Length)
                  this.ensurePC(this.pcEnd);
                if ((int) ch5 != 10 || !flag2)
                  this.pc[this.pcEnd++] = ' ';
              }
              else if (this.usePC)
              {
                if (this.pcEnd >= this.pc.Length)
                  this.ensurePC(this.pcEnd);
                this.pc[this.pcEnd++] = ch5;
              }
              flag2 = (int) ch5 == 13;
            }
            else
              break;
          }
          else
            goto label_84;
        }
        throw new Asn1Exception(35, "markup not allowed inside attribute value - illegal <  (position:" + this.getPositionDescription() + ")");
label_60:
        if (this.entityRefName == null)
          this.entityRefName = this.newString(this.buf, this.posStart, this.posEnd - this.posStart);
        throw new Asn1Exception(35, "could not resolve entity named '" + this.printable(this.entityRefName) + "' (position:" + this.getPositionDescription() + ")");
label_84:
        if (this.processNamespaces && flag1)
        {
          string str2 = this.usePC ? this.newStringIntern(this.pc, this.pcStart, this.pcEnd - this.pcStart) : this.newStringIntern(this.buf, this.posStart, this.pos - 1 - this.posStart);
          this.ensureNamespacesCapacity(this.namespaceEnd);
          int num5 = -1;
          if (num3 != -1)
          {
            if (str2.Length == 0)
              throw new Asn1Exception(35, "non-default namespace can not be declared to be empty string (position:" + this.getPositionDescription() + ")");
            this.namespacePrefix[this.namespaceEnd] = str1;
            if (!this.allStringsInterned)
              num5 = this.namespacePrefixHash[this.namespaceEnd] = str1.GetHashCode();
          }
          else
          {
            this.namespacePrefix[this.namespaceEnd] = (string) null;
            if (!this.allStringsInterned)
              num5 = this.namespacePrefixHash[this.namespaceEnd] = -1;
          }
          this.namespaceUri[this.namespaceEnd] = str2;
          int num6 = this.elNamespaceCount[this.depth - 1];
          for (int index = this.namespaceEnd - 1; index >= num6; --index)
          {
            if ((this.allStringsInterned || str1 == null) && this.namespacePrefix[index] == str1 || !this.allStringsInterned && str1 != null && (this.namespacePrefixHash[index] == num5 && str1.Equals(this.namespacePrefix[index])))
              throw new Asn1Exception(35, "duplicated namespace declaration for " + (str1 == null ? "default" : "'" + str1 + "'") + " prefix (position:" + this.getPositionDescription() + ")");
          }
          ++this.namespaceEnd;
        }
        else
        {
          this.attributeValue[this.attributeCount] = this.usePC ? new string(this.pc, this.pcStart, this.pcEnd - this.pcStart) : new string(this.buf, this.posStart, this.pos - 1 - this.posStart);
          ++this.attributeCount;
        }
        this.posStart = num1 - this.bufAbsoluteStart;
        return ch5;
      }
    }

    private void parseCDSect(bool hadCharData)
    {
      if ((int) this.more() != 67)
        throw new Asn1Exception(35, "expected <[CDATA[ for comment start (position:" + this.getPositionDescription() + ")");
      if ((int) this.more() != 68)
        throw new Asn1Exception(35, "expected <[CDATA[ for comment start (position:" + this.getPositionDescription() + ")");
      if ((int) this.more() != 65)
        throw new Asn1Exception(35, "expected <[CDATA[ for comment start (position:" + this.getPositionDescription() + ")");
      if ((int) this.more() != 84)
        throw new Asn1Exception(35, "expected <[CDATA[ for comment start (position:" + this.getPositionDescription() + ")");
      if ((int) this.more() != 65)
        throw new Asn1Exception(35, "expected <[CDATA[ for comment start (position:" + this.getPositionDescription() + ")");
      if ((int) this.more() != 91)
        throw new Asn1Exception(35, "expected <![CDATA[ for comment start (position:" + this.getPositionDescription() + ")");
      int num1 = this.pos + this.bufAbsoluteStart;
      int num2 = this.lineNumber;
      int num3 = this.columnNumber;
      bool flag1 = !this.tokenize || !this.roundtripSupported;
      try
      {
        if (flag1 && hadCharData && !this.usePC)
        {
          if (this.posEnd > this.posStart)
          {
            this.joinPC();
          }
          else
          {
            this.usePC = true;
            this.pcStart = this.pcEnd = 0;
          }
        }
        bool flag2 = false;
        bool flag3 = false;
        bool flag4 = false;
        while (true)
        {
          char ch;
          do
          {
            ch = this.more();
            switch (ch)
            {
              case '>':
                if (!flag2 || !flag3)
                {
                  flag3 = false;
                  flag2 = false;
                  break;
                }
                else
                  goto label_50;
              case ']':
                if (!flag2)
                {
                  flag2 = true;
                  break;
                }
                else
                {
                  flag3 = true;
                  break;
                }
              default:
                if (flag2)
                {
                  flag2 = false;
                  break;
                }
                else
                  break;
            }
          }
          while (!flag1);
          switch (ch)
          {
            case '\n':
              if (!flag4 && this.usePC)
              {
                if (this.pcEnd >= this.pc.Length)
                  this.ensurePC(this.pcEnd);
                this.pc[this.pcEnd++] = '\n';
              }
              flag4 = false;
              continue;
            case '\r':
              flag4 = true;
              this.posStart = num1 - this.bufAbsoluteStart;
              this.posEnd = this.pos - 1;
              if (!this.usePC)
              {
                if (this.posEnd > this.posStart)
                {
                  this.joinPC();
                }
                else
                {
                  this.usePC = true;
                  this.pcStart = this.pcEnd = 0;
                }
              }
              if (this.pcEnd >= this.pc.Length)
                this.ensurePC(this.pcEnd);
              this.pc[this.pcEnd++] = '\n';
              continue;
            default:
              if (this.usePC)
              {
                if (this.pcEnd >= this.pc.Length)
                  this.ensurePC(this.pcEnd);
                this.pc[this.pcEnd++] = ch;
              }
              flag4 = false;
              continue;
          }
        }
      }
      catch (Asn1Exception ex)
      {
        if (ex.ErrorCode != 7)
          throw ex;
        throw new Asn1Exception(7, "CDATA section started on line " + (object) num2 + " and column " + (string) (object) num3 + " was not closed (position:" + this.getPositionDescription() + ")", ex.InnerException);
      }
label_50:
      if (flag1 && this.usePC)
        this.pcEnd -= 2;
      this.posStart = num1 - this.bufAbsoluteStart;
      this.posEnd = this.pos - 3;
    }

    private void parseComment()
    {
      if ((int) this.more() != 45)
        throw new Asn1Exception(35, "expected <!-- for comment start (position:" + this.getPositionDescription() + ")");
      if (this.tokenize)
        this.posStart = this.pos;
      int num1 = this.lineNumber;
      int num2 = this.columnNumber;
      try
      {
        bool flag1 = this.tokenize && !this.roundtripSupported;
        bool flag2 = false;
        bool flag3 = false;
        bool flag4 = false;
        while (true)
        {
          char ch;
          do
          {
            ch = this.more();
            if (flag4 && (int) ch != 62)
              throw new Asn1Exception(35, "in comment after two dashes (--) next character must be > not " + this.printable(ch) + " (position:" + this.getPositionDescription() + ")");
            else if ((int) ch == 45)
            {
              if (!flag3)
              {
                flag3 = true;
              }
              else
              {
                flag4 = true;
                flag3 = false;
              }
            }
            else if ((int) ch == 62)
            {
              if (!flag4)
              {
                flag4 = false;
                flag3 = false;
              }
              else
                goto label_40;
            }
            else
              flag3 = false;
          }
          while (!flag1);
          switch (ch)
          {
            case '\n':
              if (!flag2 && this.usePC)
              {
                if (this.pcEnd >= this.pc.Length)
                  this.ensurePC(this.pcEnd);
                this.pc[this.pcEnd++] = '\n';
              }
              flag2 = false;
              continue;
            case '\r':
              flag2 = true;
              if (!this.usePC)
              {
                this.posEnd = this.pos - 1;
                if (this.posEnd > this.posStart)
                {
                  this.joinPC();
                }
                else
                {
                  this.usePC = true;
                  this.pcStart = this.pcEnd = 0;
                }
              }
              if (this.pcEnd >= this.pc.Length)
                this.ensurePC(this.pcEnd);
              this.pc[this.pcEnd++] = '\n';
              continue;
            default:
              if (this.usePC)
              {
                if (this.pcEnd >= this.pc.Length)
                  this.ensurePC(this.pcEnd);
                this.pc[this.pcEnd++] = ch;
              }
              flag2 = false;
              continue;
          }
        }
      }
      catch (Asn1Exception ex)
      {
        if (ex.ErrorCode != 7)
          throw ex;
        throw new Asn1Exception(7, "comment started on line " + (object) num1 + " and column " + (string) (object) num2 + " was not closed (position:" + this.getPositionDescription() + ")", ex.InnerException);
      }
label_40:
      if (!this.tokenize)
        return;
      this.posEnd = this.pos - 3;
      if (this.usePC)
        this.pcEnd -= 2;
    }

    private void parseDocdecl()
    {
      if ((int) this.more() != 79)
        throw new Asn1Exception(35, "expected <!DOCTYPE (position:" + this.getPositionDescription() + ")");
      if ((int) this.more() != 67)
        throw new Asn1Exception(35, "expected <!DOCTYPE (position:" + this.getPositionDescription() + ")");
      if ((int) this.more() != 84)
        throw new Asn1Exception(35, "expected <!DOCTYPE (position:" + this.getPositionDescription() + ")");
      if ((int) this.more() != 89)
        throw new Asn1Exception(35, "expected <!DOCTYPE (position:" + this.getPositionDescription() + ")");
      if ((int) this.more() != 80)
        throw new Asn1Exception(35, "expected <!DOCTYPE (position:" + this.getPositionDescription() + ")");
      if ((int) this.more() != 69)
        throw new Asn1Exception(35, "expected <!DOCTYPE (position:" + this.getPositionDescription() + ")");
      this.posStart = this.pos;
      int num = 0;
      bool flag1 = this.tokenize && !this.roundtripSupported;
      bool flag2 = false;
      while (true)
      {
        char ch;
        do
        {
          ch = this.more();
          switch (ch)
          {
            case '[':
              ++num;
              break;
            case ']':
              --num;
              break;
          }
          if ((int) ch == 62 && num == 0)
            goto label_39;
        }
        while (!flag1);
        if ((int) ch == 13)
        {
          flag2 = true;
          if (!this.usePC)
          {
            this.posEnd = this.pos - 1;
            if (this.posEnd > this.posStart)
            {
              this.joinPC();
            }
            else
            {
              this.usePC = true;
              this.pcStart = this.pcEnd = 0;
            }
          }
          if (this.pcEnd >= this.pc.Length)
            this.ensurePC(this.pcEnd);
          this.pc[this.pcEnd++] = '\n';
        }
        else if ((int) ch == 10)
        {
          if (!flag2 && this.usePC)
          {
            if (this.pcEnd >= this.pc.Length)
              this.ensurePC(this.pcEnd);
            this.pc[this.pcEnd++] = '\n';
          }
          flag2 = false;
        }
        else
        {
          if (this.usePC)
          {
            if (this.pcEnd >= this.pc.Length)
              this.ensurePC(this.pcEnd);
            this.pc[this.pcEnd++] = ch;
          }
          flag2 = false;
        }
      }
label_39:
      this.posEnd = this.pos - 1;
    }

    private int parseEndTag()
    {
      char ch1 = this.more();
      if (!this.isNameStartChar(ch1))
      {
        throw new Asn1Exception(35, "expected name start and not " + this.printable(ch1) + " (position:" + this.getPositionDescription() + ")");
      }
      else
      {
        this.posStart = this.pos - 3;
        int num = this.pos - 1 + this.bufAbsoluteStart;
        char ch2;
        do
        {
          ch2 = this.more();
        }
        while (this.isNameChar(ch2));
        int startIndex = num - this.bufAbsoluteStart;
        int length = this.pos - 1 - startIndex;
        char[] chArray = this.elRawName[this.depth];
        if (this.elRawNameEnd[this.depth] != length)
        {
          string str = new string(chArray, 0, this.elRawNameEnd[this.depth]);
          throw new Asn1Exception(35, "end tag name </" + (object) new string(this.buf, startIndex, length) + "> must match start tag name <" + str + "> from line " + (string) (object) this.elRawNameLine[this.depth] + " (position:" + this.getPositionDescription() + ")");
        }
        else
        {
          for (int index = 0; index < length; ++index)
          {
            if ((int) this.buf[startIndex++] != (int) chArray[index])
            {
              string str = new string(chArray, 0, length);
              throw new Asn1Exception(35, "end tag name </" + (object) new string(this.buf, startIndex - index - 1, length) + "> must be the same as start tag <" + str + "> from line " + (string) (object) this.elRawNameLine[this.depth] + " (position:" + this.getPositionDescription() + ")");
            }
          }
          while (this.isS(ch2))
            ch2 = this.more();
          if ((int) ch2 != 62)
          {
            throw new Asn1Exception(35, "expected > to finish end tag not " + (object) this.printable(ch2) + " from line " + (string) (object) this.elRawNameLine[this.depth] + " (position:" + this.getPositionDescription() + ")");
          }
          else
          {
            this.posEnd = this.pos;
            this.pastEndTag = true;
            return this.eventType = 3;
          }
        }
      }
    }

    private char[] parseEntityRef()
    {
      this.entityRefName = (string) null;
      this.posStart = this.pos;
      char ch1 = this.more();
      bool flag;
      if ((int) ch1 != 35)
      {
        if (!this.isNameStartChar(ch1))
        {
          throw new Asn1Exception(35, "entity reference names can not start with character '" + this.printable(ch1) + "' (position:" + this.getPositionDescription() + ")");
        }
        else
        {
          char ch2;
          do
          {
            flag = true;
            ch2 = this.more();
            if ((int) ch2 == 59)
              goto label_3;
          }
          while (this.isNameChar(ch2));
          goto label_29;
label_3:
          this.posEnd = this.pos - 1;
          int entitNameLen = this.posEnd - this.posStart;
          if (entitNameLen == 2 && (int) this.buf[this.posStart] == 108 && (int) this.buf[this.posStart + 1] == 116)
          {
            if (this.tokenize)
              this.text = "<";
            this.charRefOneCharBuf[0] = '<';
            return this.charRefOneCharBuf;
          }
          else if (entitNameLen == 3 && (int) this.buf[this.posStart] == 97 && ((int) this.buf[this.posStart + 1] == 109 && (int) this.buf[this.posStart + 2] == 112))
          {
            if (this.tokenize)
              this.text = "&";
            this.charRefOneCharBuf[0] = '&';
            return this.charRefOneCharBuf;
          }
          else if (entitNameLen == 2 && (int) this.buf[this.posStart] == 103 && (int) this.buf[this.posStart + 1] == 116)
          {
            if (this.tokenize)
              this.text = ">";
            this.charRefOneCharBuf[0] = '>';
            return this.charRefOneCharBuf;
          }
          else if (entitNameLen == 4 && (int) this.buf[this.posStart] == 97 && ((int) this.buf[this.posStart + 1] == 112 && (int) this.buf[this.posStart + 2] == 111) && (int) this.buf[this.posStart + 3] == 115)
          {
            if (this.tokenize)
              this.text = "'";
            this.charRefOneCharBuf[0] = '\'';
            return this.charRefOneCharBuf;
          }
          else if (entitNameLen == 4 && (int) this.buf[this.posStart] == 113 && ((int) this.buf[this.posStart + 1] == 117 && (int) this.buf[this.posStart + 2] == 111) && (int) this.buf[this.posStart + 3] == 116)
          {
            if (this.tokenize)
              this.text = "\"";
            this.charRefOneCharBuf[0] = '"';
            return this.charRefOneCharBuf;
          }
          else
          {
            char[] chArray = this.lookuEntityReplacement(entitNameLen);
            if (chArray != null)
              return chArray;
            if (this.tokenize)
              this.text = (string) null;
            return (char[]) null;
          }
label_29:
          throw new Asn1Exception(35, "entity reference name can not contain character " + this.printable(ch2) + "' (position:" + this.getPositionDescription() + ")");
        }
      }
      else
      {
        char ch2 = char.MinValue;
        char ch3 = this.more();
        if ((int) ch3 != 120)
        {
          while (true)
          {
            flag = true;
            if ((int) ch3 >= 48 && (int) ch3 <= 57)
            {
              ch2 = (char) ((int) ch2 * 10 + ((int) ch3 - 48));
              ch3 = this.more();
            }
            else
              break;
          }
          if ((int) ch3 != 59)
            throw new Asn1Exception(35, "character reference (with decimal value) may not contain " + this.printable(ch3) + " (position:" + this.getPositionDescription() + ")");
        }
        else
        {
          char ch4;
          while (true)
          {
            ch4 = this.more();
            if ((int) ch4 >= 48 && (int) ch4 <= 57)
              ch2 = (char) ((int) ch2 * 16 + ((int) ch4 - 48));
            else if ((int) ch4 >= 97 && (int) ch4 <= 102)
              ch2 = (char) ((int) ch2 * 16 + ((int) ch4 - 87));
            else if ((int) ch4 >= 65 && (int) ch4 <= 70)
              ch2 = (char) ((int) ch2 * 16 + ((int) ch4 - 55));
            else
              break;
          }
          if ((int) ch4 != 59)
            throw new Asn1Exception(35, "character reference (with hex value) may not contain " + this.printable(ch4) + " (position:" + this.getPositionDescription() + ")");
        }
        this.posEnd = this.pos - 1;
        this.charRefOneCharBuf[0] = ch2;
        if (this.tokenize)
          this.text = this.newString(this.charRefOneCharBuf, 0, 1);
        return this.charRefOneCharBuf;
      }
    }

    private int parseEpilog()
    {
      if (this.eventType == 1)
        throw new Asn1Exception(35, "already reached end of XML input (position:" + this.getPositionDescription() + ")");
      if (this.reachedEnd || !this.doParseEpilog)
        return this.eventType = 1;
      bool flag1 = false;
      bool flag2 = this.tokenize && !this.roundtripSupported;
      bool flag3 = false;
      try
      {
        char ch1 = !this.seenMarkup ? this.more() : this.buf[this.pos - 1];
        this.seenMarkup = false;
        this.posStart = this.pos - 1;
        if (!this.reachedEnd)
        {
          do
          {
            if ((int) ch1 == 60)
            {
              if (flag1 && this.tokenize)
              {
                this.posEnd = this.pos - 1;
                this.seenMarkup = true;
                return this.eventType = 7;
              }
              else
              {
                char ch2 = this.more();
                if (!this.reachedEnd)
                {
                  switch (ch2)
                  {
                    case '!':
                      char ch3 = this.more();
                      if (!this.reachedEnd)
                      {
                        switch (ch3)
                        {
                          case '-':
                            this.parseComment();
                            if (this.tokenize)
                              return this.eventType = 9;
                            else
                              break;
                          case 'D':
                            this.parseDocdecl();
                            if (this.tokenize)
                              return this.eventType = 10;
                            else
                              break;
                          default:
                            throw new Asn1Exception(35, "unexpected markup <!" + this.printable(ch3) + " (position:" + this.getPositionDescription() + ")");
                        }
                      }
                      else
                        goto label_51;
                    case '/':
                      throw new Asn1Exception(35, "end tag not allowed in epilog but got " + this.printable(ch2) + " (position:" + this.getPositionDescription() + ")");
                    case '?':
                      this.parsePI();
                      if (this.tokenize)
                        return this.eventType = 8;
                      else
                        break;
                    default:
                      if (!this.isNameStartChar(ch2))
                      {
                        throw new Asn1Exception(35, "in epilog expected ignorable content and not " + this.printable(ch2) + " (position:" + this.getPositionDescription() + ")");
                      }
                      else
                      {
                        this.pos = this.posStart;
                        return this.eventType = 1;
                      }
                  }
                }
                else
                  goto label_51;
              }
            }
            else if (!this.isS(ch1))
            {
              throw new Asn1Exception(35, "in epilog non whitespace content is not allowed but got " + this.printable(ch1) + " (position:" + this.getPositionDescription() + ")");
            }
            else
            {
              flag1 = true;
              if (flag2)
              {
                switch (ch1)
                {
                  case '\n':
                    if (!flag3 && this.usePC)
                    {
                      if (this.pcEnd >= this.pc.Length)
                        this.ensurePC(this.pcEnd);
                      this.pc[this.pcEnd++] = '\n';
                    }
                    flag3 = false;
                    break;
                  case '\r':
                    flag3 = true;
                    if (!this.usePC)
                    {
                      this.posEnd = this.pos - 1;
                      if (this.posEnd > this.posStart)
                      {
                        this.joinPC();
                      }
                      else
                      {
                        this.usePC = true;
                        this.pcStart = this.pcEnd = 0;
                      }
                    }
                    if (this.pcEnd >= this.pc.Length)
                      this.ensurePC(this.pcEnd);
                    this.pc[this.pcEnd++] = '\n';
                    break;
                  default:
                    if (this.usePC)
                    {
                      if (this.pcEnd >= this.pc.Length)
                        this.ensurePC(this.pcEnd);
                      this.pc[this.pcEnd++] = ch1;
                    }
                    flag3 = false;
                    break;
                }
              }
            }
            ch1 = this.more();
          }
          while (!this.reachedEnd);
        }
        else
          goto label_51;
      }
      catch (Asn1Exception ex)
      {
        if (ex.ErrorCode != 7)
          throw ex;
        this.reachedEnd = true;
      }
label_51:
      if (!this.reachedEnd)
        throw new Asn1Exception(35, "internal error in parseEpilog");
      if (!this.tokenize || !flag1)
        return this.eventType = 1;
      this.posEnd = this.pos;
      return this.eventType = 7;
    }

    private bool parsePI()
    {
      if (this.tokenize)
        this.posStart = this.pos;
      int num1 = this.lineNumber;
      int num2 = this.columnNumber;
      int index = this.pos + this.bufAbsoluteStart;
      int num3 = -1;
      bool flag1 = this.tokenize && !this.roundtripSupported;
      bool flag2 = false;
      try
      {
        bool flag3 = false;
        char ch = this.more();
        if (this.isS(ch))
          throw new Asn1Exception(35, "processing instruction PITarget must be exactly after <? and not white space character (position:" + this.getPositionDescription() + ")");
        while (true)
        {
          if ((int) ch == 63)
            flag3 = true;
          else if ((int) ch == 62)
          {
            if (!flag3)
              flag3 = false;
            else
              goto label_44;
          }
          else
          {
            if (num3 == -1 && this.isS(ch))
            {
              num3 = this.pos - 1 + this.bufAbsoluteStart;
              if (num3 - index == 3 && ((int) this.buf[index] == 120 || (int) this.buf[index] == 88) && ((int) this.buf[index + 1] == 109 || (int) this.buf[index + 1] == 77) && ((int) this.buf[index + 2] == 108 || (int) this.buf[index + 2] == 76))
                break;
            }
            flag3 = false;
          }
          if (flag1)
          {
            switch (ch)
            {
              case '\n':
                if (!flag2 && this.usePC)
                {
                  if (this.pcEnd >= this.pc.Length)
                    this.ensurePC(this.pcEnd);
                  this.pc[this.pcEnd++] = '\n';
                }
                flag2 = false;
                break;
              case '\r':
                flag2 = true;
                if (!this.usePC)
                {
                  this.posEnd = this.pos - 1;
                  if (this.posEnd > this.posStart)
                  {
                    this.joinPC();
                  }
                  else
                  {
                    this.usePC = true;
                    this.pcStart = this.pcEnd = 0;
                  }
                }
                if (this.pcEnd >= this.pc.Length)
                  this.ensurePC(this.pcEnd);
                this.pc[this.pcEnd++] = '\n';
                break;
              default:
                if (this.usePC)
                {
                  if (this.pcEnd >= this.pc.Length)
                    this.ensurePC(this.pcEnd);
                  this.pc[this.pcEnd++] = ch;
                }
                flag2 = false;
                break;
            }
          }
          ch = this.more();
        }
        if (index > 3)
          throw new Asn1Exception(35, "processing instruction can not have PITarget with reserveld xml name (position:" + this.getPositionDescription() + ")");
        if ((int) this.buf[index] != 120 && (int) this.buf[index + 1] != 109 && (int) this.buf[index + 2] != 108)
          throw new Asn1Exception(35, "XMLDecl must have xml name in lowercase (position:" + this.getPositionDescription() + ")");
        this.parseXmlDecl(ch);
        if (this.tokenize)
          this.posEnd = this.pos - 2;
        int off = index - this.bufAbsoluteStart + 3;
        int len = this.pos - 2 - off;
        this.xmlDeclContent = this.newString(this.buf, off, len);
        return false;
      }
      catch (Asn1Exception ex)
      {
        if (ex.ErrorCode != 7)
          throw ex;
        throw new Asn1Exception(7, "processing instruction started on line " + (object) num1 + " and column " + (string) (object) num2 + " was not closed (position:" + this.getPositionDescription() + ")", ex.InnerException);
      }
label_44:
      if (num3 == -1)
        num3 = this.pos - 2 + this.bufAbsoluteStart;
      int num4 = index - this.bufAbsoluteStart;
      int num5 = num3 - this.bufAbsoluteStart;
      if (this.tokenize)
      {
        this.posEnd = this.pos - 2;
        if (flag1)
          --this.pcEnd;
      }
      return true;
    }

    private int parseProlog()
    {
      char ch1 = !this.seenMarkup ? this.more() : this.buf[this.pos - 1];
      if (this.eventType == 0)
      {
        switch (ch1)
        {
          case '\xFEFF':
            ch1 = this.more();
            break;
          case '\xFFFE':
            throw new Asn1Exception(35, "first character in input was UNICODE noncharacter (0xFFFE) - input requires int swapping (position:" + this.getPositionDescription() + ")");
        }
      }
      this.seenMarkup = false;
      bool flag1 = false;
      this.posStart = this.pos - 1;
      bool flag2 = this.tokenize && !this.roundtripSupported;
      bool flag3 = false;
      char ch2;
      char ch3;
      while (true)
      {
        if ((int) ch1 == 60)
        {
          if (!flag1 || !this.tokenize)
          {
            ch2 = this.more();
            switch (ch2)
            {
              case '!':
                ch3 = this.more();
                switch (ch3)
                {
                  case '-':
                    this.parseComment();
                    if (!this.tokenize)
                      break;
                    else
                      goto label_20;
                  case 'D':
                    if (!this.seenDocdecl)
                    {
                      this.seenDocdecl = true;
                      this.parseDocdecl();
                      if (!this.tokenize)
                        break;
                      else
                        goto label_18;
                    }
                    else
                      goto label_16;
                  default:
                    goto label_21;
                }
              case '/':
                goto label_22;
              case '?':
                if (this.parsePI())
                {
                  if (!this.tokenize)
                    break;
                  else
                    goto label_12;
                }
                else
                {
                  this.posStart = this.pos;
                  flag1 = false;
                  break;
                }
              default:
                goto label_23;
            }
          }
          else
            break;
        }
        else if (this.isS(ch1))
        {
          flag1 = true;
          if (flag2)
          {
            if ((int) ch1 == 13)
            {
              flag3 = true;
              if (!this.usePC)
              {
                this.posEnd = this.pos - 1;
                if (this.posEnd > this.posStart)
                {
                  this.joinPC();
                }
                else
                {
                  this.usePC = true;
                  this.pcStart = this.pcEnd = 0;
                }
              }
              if (this.pcEnd >= this.pc.Length)
                this.ensurePC(this.pcEnd);
              this.pc[this.pcEnd++] = '\n';
            }
            else if ((int) ch1 == 10)
            {
              if (!flag3 && this.usePC)
              {
                if (this.pcEnd >= this.pc.Length)
                  this.ensurePC(this.pcEnd);
                this.pc[this.pcEnd++] = '\n';
              }
              flag3 = false;
            }
            else
            {
              if (this.usePC)
              {
                if (this.pcEnd >= this.pc.Length)
                  this.ensurePC(this.pcEnd);
                this.pc[this.pcEnd++] = ch1;
              }
              flag3 = false;
            }
          }
        }
        else
          goto label_27;
        ch1 = this.more();
      }
      this.posEnd = this.pos - 1;
      this.seenMarkup = true;
      return this.eventType = 7;
label_12:
      return this.eventType = 8;
label_16:
      throw new Asn1Exception(35, "only one docdecl allowed in XML document (position:" + this.getPositionDescription() + ")");
label_18:
      return this.eventType = 10;
label_20:
      return this.eventType = 9;
label_21:
      throw new Asn1Exception(35, "unexpected markup <!" + this.printable(ch3) + " (position:" + this.getPositionDescription() + ")");
label_22:
      throw new Asn1Exception(35, "expected start tag name and not " + this.printable(ch2) + " (position:" + this.getPositionDescription() + ")");
label_23:
      if (!this.isNameStartChar(ch2))
      {
        throw new Asn1Exception(35, "expected start tag name and not " + this.printable(ch2) + " (position:" + this.getPositionDescription() + ")");
      }
      else
      {
        this.seenRoot = true;
        return this.parseStartTag();
      }
label_27:
      throw new Asn1Exception(35, "only whitespace content allowed before start tag and not " + this.printable(ch1) + " (position:" + this.getPositionDescription() + ")");
    }

    private int parseStartTag()
    {
      ++this.depth;
      this.posStart = this.pos - 2;
      this.emptyElementTag = false;
      this.attributeCount = 0;
      int num1 = this.pos - 1 + this.bufAbsoluteStart;
      int num2 = -1;
      if ((int) this.buf[this.pos - 1] == 58 && this.processNamespaces)
        throw new Asn1Exception(35, "when namespaces processing enabled colon can not be at element name start (position:" + this.getPositionDescription() + ")");
      char ch1;
      while (true)
      {
        do
        {
          ch1 = this.more();
          if (!this.isNameChar(ch1))
            goto label_7;
        }
        while ((int) ch1 != 58 || !this.processNamespaces);
        if (num2 == -1)
          num2 = this.pos - 1 + this.bufAbsoluteStart;
        else
          break;
      }
      throw new Asn1Exception(35, "only one colon is allowed in name of element when namespaces are enabled (position:" + this.getPositionDescription() + ")");
label_7:
      this.ensureElementsCapacity();
      int num3 = this.pos - 1 - (num1 - this.bufAbsoluteStart);
      if (this.elRawName[this.depth] == null || this.elRawName[this.depth].Length < num3)
        this.elRawName[this.depth] = new char[2 * num3];
      Array.Copy((Array) this.buf, num1 - this.bufAbsoluteStart, (Array) this.elRawName[this.depth], 0, num3);
      this.elRawNameEnd[this.depth] = num3;
      this.elRawNameLine[this.depth] = this.lineNumber;
      string prefix1 = (string) null;
      if (this.processNamespaces)
      {
        if (num2 != -1)
        {
          prefix1 = this.elPrefix[this.depth] = this.newString(this.buf, num1 - this.bufAbsoluteStart, num2 - num1);
          this.elName[this.depth] = this.newString(this.buf, num2 + 1 - this.bufAbsoluteStart, this.pos - 2 - (num2 - this.bufAbsoluteStart));
        }
        else
        {
          prefix1 = this.elPrefix[this.depth] = (string) null;
          this.elName[this.depth] = this.newString(this.buf, num1 - this.bufAbsoluteStart, num3);
        }
      }
      else
        this.elName[this.depth] = this.newString(this.buf, num1 - this.bufAbsoluteStart, num3);
      while (true)
      {
        while (this.isS(ch1))
          ch1 = this.more();
        switch (ch1)
        {
          case '/':
            goto label_17;
          case '>':
            goto label_24;
          default:
            if (this.isNameStartChar(ch1))
            {
              this.parseAttribute();
              ch1 = this.more();
              continue;
            }
            else
              goto label_22;
        }
      }
label_17:
      if (this.emptyElementTag)
        throw new Asn1Exception(35, "repeated / in tag declaration (position:" + this.getPositionDescription() + ")");
      this.emptyElementTag = true;
      char ch2 = this.more();
      if ((int) ch2 != 62)
        throw new Asn1Exception(35, "expected > to end empty tag not " + this.printable(ch2) + " (position:" + this.getPositionDescription() + ")");
      else
        goto label_24;
label_22:
      throw new Asn1Exception(35, "start tag unexpected character " + this.printable(ch1) + " (position:" + this.getPositionDescription() + ")");
label_24:
      if (this.processNamespaces)
      {
        string str1 = this.getNamespace(prefix1);
        if (str1 == null)
        {
          if (prefix1 != null)
            throw new Asn1Exception(35, "could not determine namespace bound to element prefix " + prefix1 + " (position:" + this.getPositionDescription() + ")");
          else
            str1 = "";
        }
        this.elUri[this.depth] = str1;
        for (int index = 0; index < this.attributeCount; ++index)
        {
          string prefix2 = this.attributePrefix[index];
          if (prefix2 != null)
          {
            string @namespace = this.getNamespace(prefix2);
            if (@namespace == null)
              throw new Asn1Exception(35, "could not determine namespace bound to attribute prefix " + prefix2 + " (position:" + this.getPositionDescription() + ")");
            else
              this.attributeUri[index] = @namespace;
          }
          else
            this.attributeUri[index] = "";
        }
        for (int index1 = 1; index1 < this.attributeCount; ++index1)
        {
          for (int index2 = 0; index2 < index1; ++index2)
          {
            if (this.attributeUri[index2] == this.attributeUri[index1] && (this.allStringsInterned && this.attributeName[index2].Equals(this.attributeName[index1]) || !this.allStringsInterned && this.attributeNameHash[index2] == this.attributeNameHash[index1] && this.attributeName[index2].Equals(this.attributeName[index1])))
            {
              string str2 = this.attributeName[index2];
              if (this.attributeUri[index2] != null)
                str2 = this.attributeUri[index2] + ":" + str2;
              string str3 = this.attributeName[index1];
              if (this.attributeUri[index1] != null)
                str3 = this.attributeUri[index1] + ":" + str3;
              throw new Asn1Exception(35, "duplicated attributes " + str2 + " and " + str3 + " (position:" + this.getPositionDescription() + ")");
            }
          }
        }
      }
      else
      {
        for (int index1 = 1; index1 < this.attributeCount; ++index1)
        {
          for (int index2 = 0; index2 < index1; ++index2)
          {
            if (this.allStringsInterned && this.attributeName[index2].Equals(this.attributeName[index1]) || !this.allStringsInterned && this.attributeNameHash[index2] == this.attributeNameHash[index1] && this.attributeName[index2].Equals(this.attributeName[index1]))
              throw new Asn1Exception(35, "duplicated attributes " + this.attributeName[index2] + " and " + this.attributeName[index1] + " (position:" + this.getPositionDescription() + ")");
          }
        }
      }
      this.elNamespaceCount[this.depth] = this.namespaceEnd;
      this.posEnd = this.pos;
      return this.eventType = 2;
    }

    private void parseXmlDecl(char ch)
    {
      this.keepBuffer = true;
      this.bufStart = 0;
      ch = this.skipS(ch);
      ch = this.requireInput(ch, XmlAsn1InputStream.VERSION);
      ch = this.skipS(ch);
      if ((int) ch != 61)
      {
        throw new Asn1Exception(35, "expected equals sign (=) after version and not " + this.printable(ch) + " (position:" + this.getPositionDescription() + ")");
      }
      else
      {
        ch = this.more();
        ch = this.skipS(ch);
        if ((int) ch != 39 && (int) ch != 34)
        {
          throw new Asn1Exception(35, "expected apostrophe (') or quotation mark (\") after version and not " + this.printable(ch) + " (position:" + this.getPositionDescription() + ")");
        }
        else
        {
          char ch1 = ch;
          int versionStart = this.pos;
          for (ch = this.more(); (int) ch != (int) ch1; ch = this.more())
          {
            if (((int) ch < 97 || (int) ch > 122) && ((int) ch < 65 || (int) ch > 90) && (((int) ch < 48 || (int) ch > 57) && ((int) ch != 95 && (int) ch != 46 && ((int) ch != 58 && (int) ch != 45))))
              throw new Asn1Exception(35, "<?xml version value expected to be in ([a-zA-Z0-9_.:] | '-') not " + this.printable(ch) + " (position:" + this.getPositionDescription() + ")");
          }
          int versionEnd = this.pos - 1;
          this.parseXmlDeclWithVersion(versionStart, versionEnd);
          this.keepBuffer = false;
        }
      }
    }

    private void parseXmlDeclWithVersion(int versionStart, int versionEnd)
    {
      if (versionEnd - versionStart != 3 || (int) this.buf[versionStart] != 49 || ((int) this.buf[versionStart + 1] != 46 || (int) this.buf[versionStart + 2] != 48))
      {
        throw new Asn1Exception(35, "only 1.0 is supported as <?xml version not '" + this.printable(new string(this.buf, versionStart, versionEnd - versionStart)) + "' (position:" + this.getPositionDescription() + ")");
      }
      else
      {
        this.xmlDeclVersion = this.newString(this.buf, versionStart, versionEnd - versionStart);
        char ch1 = this.skipS(this.more());
        if ((int) ch1 == 101)
        {
          char ch2 = this.skipS(this.requireInput(this.more(), XmlAsn1InputStream.NCODING));
          if ((int) ch2 != 61)
          {
            throw new Asn1Exception(35, "expected equals sign (=) after encoding and not " + this.printable(ch2) + " (position:" + this.getPositionDescription() + ")");
          }
          else
          {
            char ch3 = this.skipS(this.more());
            if ((int) ch3 != 39 && (int) ch3 != 34)
            {
              throw new Asn1Exception(35, "expected apostrophe (') or quotation mark (\") after encoding and not " + this.printable(ch3) + " (position:" + this.getPositionDescription() + ")");
            }
            else
            {
              char ch4 = ch3;
              int off = this.pos;
              char ch5 = this.more();
              if (((int) ch5 < 97 || (int) ch5 > 122) && ((int) ch5 < 65 || (int) ch5 > 90))
              {
                throw new Asn1Exception(35, "<?xml encoding name expected to start with [A-Za-z] not " + this.printable(ch5) + " (position:" + this.getPositionDescription() + ")");
              }
              else
              {
                for (char ch6 = this.more(); (int) ch6 != (int) ch4; ch6 = this.more())
                {
                  if (((int) ch6 < 97 || (int) ch6 > 122) && ((int) ch6 < 65 || (int) ch6 > 90) && (((int) ch6 < 48 || (int) ch6 > 57) && ((int) ch6 != 46 && (int) ch6 != 95 && (int) ch6 != 45)))
                    throw new Asn1Exception(35, "<?xml encoding value expected to be in ([A-Za-z0-9._] | '-') not " + this.printable(ch6) + " (position:" + this.getPositionDescription() + ")");
                }
                int num = this.pos - 1;
                this.inputEncoding = this.newString(this.buf, off, num - off);
                ch1 = this.more();
              }
            }
          }
        }
        char ch7 = this.skipS(ch1);
        if ((int) ch7 == 115)
        {
          char ch2 = this.skipS(this.requireInput(this.more(), XmlAsn1InputStream.TANDALONE));
          if ((int) ch2 != 61)
          {
            throw new Asn1Exception(35, "expected equals sign (=) after standalone and not " + this.printable(ch2) + " (position:" + this.getPositionDescription() + ")");
          }
          else
          {
            char ch3 = this.skipS(this.more());
            if ((int) ch3 != 39 && (int) ch3 != 34)
            {
              throw new Asn1Exception(35, "expected apostrophe (') or quotation mark (\") after encoding and not " + this.printable(ch3) + " (position:" + this.getPositionDescription() + ")");
            }
            else
            {
              char ch4 = ch3;
              char ch5 = this.more();
              char ch6;
              switch (ch5)
              {
                case 'n':
                  ch6 = this.requireInput(ch5, XmlAsn1InputStream.NO);
                  this.xmlDeclStandalone = "false";
                  break;
                case 'y':
                  ch6 = this.requireInput(ch5, XmlAsn1InputStream.YES);
                  this.xmlDeclStandalone = "true";
                  break;
                default:
                  throw new Asn1Exception(35, "expected 'yes' or 'no' after standalone and not " + this.printable(ch5) + " (position:" + this.getPositionDescription() + ")");
              }
              if ((int) ch6 != (int) ch4)
                throw new Asn1Exception(35, "expected " + (object) ch4 + " after standalone value not " + this.printable(ch6) + " (position:" + this.getPositionDescription() + ")");
              else
                ch7 = this.more();
            }
          }
        }
        char ch8 = this.skipS(ch7);
        if ((int) ch8 != 63)
        {
          throw new Asn1Exception(35, "expected ?> as last part of <?xml not " + this.printable(ch8) + " (position:" + this.getPositionDescription() + ")");
        }
        else
        {
          char ch2 = this.more();
          if ((int) ch2 == 62)
            return;
          throw new Asn1Exception(35, "expected ?> as last part of <?xml not " + this.printable(ch2) + " (position:" + this.getPositionDescription() + ")");
        }
      }
    }

    private string printable(char ch)
    {
      if ((int) ch == 10)
        return "\\n";
      if ((int) ch == 13)
        return "\\r";
      if ((int) ch == 9)
        return "\\t";
      if ((int) ch == 39)
        return "\\'";
      if ((int) ch <= (int) sbyte.MaxValue && (int) ch >= 32)
        return string.Concat((object) ch);
      else
        return "\\u" + ((int) ch).ToString("X4");
    }

    private string printable(string s)
    {
      if (s == null)
        return (string) null;
      int length = s.Length;
      StringBuilder stringBuilder = new StringBuilder(length + 10);
      for (int index = 0; index < length; ++index)
        stringBuilder.Append(this.printable(s[index]));
      s = ((object) stringBuilder).ToString();
      return s;
    }

    public void readOuterXml()
    {
      int num = 1;
      while (num > 0)
      {
        switch (this.next())
        {
          case 2:
            ++num;
            break;
          case 3:
            --num;
            continue;
        }
      }
    }

    public void readText(StringBuilder sb)
    {
      if (this.eventType == 0 || this.eventType == 1)
        return;
      if (this.eventType == 6)
        sb.Append(this.text);
      if (this.text == null)
      {
        if (!this.usePC || this.eventType == 2 || this.eventType == 3)
          sb.Append(this.buf, this.posStart, this.posEnd - this.posStart);
        else
          sb.Append(this.pc, this.pcStart, this.pcEnd - this.pcStart);
      }
    }

    public void reinit()
    {
      this.lineNumber = 1;
      this.columnNumber = 0;
      this.seenRoot = false;
      this.reachedEnd = false;
      this.eventType = 0;
      this.emptyElementTag = false;
      this.depth = 0;
      this.attributeCount = 0;
      this.namespaceEnd = 0;
      this.entityEnd = 0;
      this.inputEncoding = (string) null;
      this.keepBuffer = false;
      this.readBytes = 0;
      this.usedChars = 0;
      this.usePC = false;
      this.seenStartTag = false;
      this.seenEndTag = false;
      this.pastEndTag = false;
      this.seenAmpersand = false;
      this.seenMarkup = false;
      this.seenDocdecl = false;
      this.xmlDeclVersion = (string) null;
      this.xmlDeclStandalone = (string) null;
      this.xmlDeclContent = (string) null;
      this.resetStringCache();
    }

    private char requireInput(char ch, char[] input)
    {
      for (int index = 0; index < input.Length; ++index)
      {
        if ((int) ch != (int) input[index])
          throw new Asn1Exception(35, "expected " + this.printable(input[index]) + " in " + new string(input) + " and not " + this.printable(ch) + " (position:" + this.getPositionDescription() + ")");
        else
          ch = this.more();
      }
      return ch;
    }

    private void resetStringCache()
    {
    }

    public void setAllStringsInterned(bool b)
    {
      this.allStringsInterned = b;
    }

    public void setInput(Stream stream)
    {
      this.inputStream = stream;
    }

    public void setKeepContent(bool b)
    {
      if (b)
      {
        this.keepBuffer = true;
        this.bufStart = this.posEnd;
      }
      else
        this.keepBuffer = false;
    }

    private static void setName(char ch)
    {
      XmlAsn1InputStream.lookupNameChar[(int) ch] = true;
    }

    private static void setNameStart(char ch)
    {
      XmlAsn1InputStream.lookupNameStartChar[(int) ch] = true;
      XmlAsn1InputStream.setName(ch);
    }

    public void setParseEpilog(bool b)
    {
      this.doParseEpilog = b;
    }

    public void setProcessNamespaces(bool b)
    {
      this.processNamespaces = b;
    }

    public void setRoundtripSupported(bool b)
    {
      this.roundtripSupported = b;
    }

    private char skipS(char ch)
    {
      while (this.isS(ch))
        ch = this.more();
      return ch;
    }
  }
}
