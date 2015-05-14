// Decompiled with JetBrains decompiler
// Type: Novensys.ASN1.IO.XmlAsn1OutputStream
// Assembly: Novensys.eCard.SDK, Version=1.1.56.0, Culture=neutral, PublicKeyToken=null
// MVID: 59F9E456-D3DA-4066-A4A4-692B516A775E
// Assembly location: C:\Program Files (x86)\CNAS\SIUI-SPITAL\Novensys.eCard.SDK.dll

using Novensys.ASN1;
using Novensys.ASN1.Util;
using System;
using System.IO;

namespace Novensys.ASN1.IO
{
  [Serializable]
  public class XmlAsn1OutputStream
  {
    private int depth = 0;
    private string indentationString = (string) null;
    private string lineSeparator = "\n";
    private int namespaceEnd = 0;
    private string[] namespacePrefix = new string[8];
    private bool useXML1_1ControlCharacters = false;
    private static string[] asn1ControlCharacters = new string[32]
    {
      "nul",
      "soh",
      "stx",
      "etx",
      "eot",
      "enq",
      "ack",
      "bel",
      "bs",
      "\t",
      "\n",
      "vt",
      "ff",
      "\r",
      "so",
      "si",
      "dle",
      "dc1",
      "dc2",
      "dc3",
      "dc4",
      "nak",
      "syn",
      "etb",
      "can",
      "em",
      "sub",
      "esc",
      "is4",
      "is3",
      "is2",
      "is1"
    };
    private static string[] precomputedPrefixes = new string[32];
    private const int MAX_INDENTATION_LENGTH = 65;
    private const int MAX_NAMESPACE_LENGTH_BEFORE_INDENTATION = 40;
    public const string XML_URI = "http://www.w3.org/XML/1998/namespace";
    private const string XMLNS_URI = "http://www.w3.org/2000/xmlns/";
    private int autoDeclaredPrefixes;
    private bool doIndent;
    private string[] elName;
    private string[] elNamespace;
    private int[] elNamespaceCount;
    private bool encodingAttributeSingleQuoted;
    private bool finished;
    private char[] indentationBuf;
    private int indentationJump;
    private string listSeparator;
    private string location;
    private int maxIndentLevel;
    private string[] namespaceUri;
    private int offsetNewLine;
    private Stream output;
    private bool seenBracket;
    private bool seenBracketBracket;
    private bool seenTag;
    private bool setPrefixCalled;
    private bool startTagIncomplete;
    private bool writeIndentation;
    private bool writeLineSeparator;
    private StreamWriter writer;

    static XmlAsn1OutputStream()
    {
      for (int index = 0; index < XmlAsn1OutputStream.precomputedPrefixes.Length; ++index)
        XmlAsn1OutputStream.precomputedPrefixes[index] = string.Intern("n" + (object) index);
    }

    public XmlAsn1OutputStream(Stream output)
    {
      this.namespaceUri = new string[this.namespacePrefix.Length];
      this.elNamespace = new string[2];
      this.elNamespaceCount = new int[this.elNamespace.Length];
      this.elName = new string[this.elNamespace.Length];
      this.writer = (StreamWriter) null;
      this.reset();
      this.changeOutput(output);
    }

    public virtual void attribute(string ns, string name, string val)
    {
      this.attributeName(ns, name);
      this.write(this.encodingAttributeSingleQuoted ? '\'' : '"');
      this.writeAttributeValue(val);
      this.write(this.encodingAttributeSingleQuoted ? '\'' : '"');
    }

    public virtual void attributeList(string ns, string name, string[] list)
    {
      this.attributeName(ns, name);
      this.write(this.encodingAttributeSingleQuoted ? '\'' : '"');
      if (list != null)
      {
        for (int index = 0; index < list.Length; ++index)
        {
          this.writeAttributeValue(list[index]);
          if (index < list.Length - 1)
            this.write(this.listSeparator);
        }
      }
      this.write(this.encodingAttributeSingleQuoted ? '\'' : '"');
    }

    protected internal virtual void attributeName(string ns, string name)
    {
      if (!this.startTagIncomplete)
        throw new ArgumentException("startTag() must be called before attribute()" + this.getLocation());
      this.write(' ');
      if (ns != null && ns.Length > 0)
      {
        string s = this.lookupOrDeclarePrefix(ns);
        if (s.Length == 0)
          s = this.generatePrefix(ns);
        this.write(s);
        this.write(':');
      }
      this.write(name);
      this.write('=');
    }

    public virtual void cdsect(string text)
    {
      if (this.startTagIncomplete || this.setPrefixCalled || this.seenBracket)
        this.closeStartTag();
      if (this.doIndent && this.seenTag)
        this.seenTag = false;
      this.write("<![CDATA[");
      this.write(text);
      this.write("]]>");
    }

    public virtual void changeOutput(Stream output)
    {
      if (this.writer != null)
        this.flush();
      this.output = output;
      try
      {
        this.writer = new StreamWriter(output);
      }
      catch (IOException ex)
      {
        throw new Asn1Exception(34, ex.Message, (Exception) ex);
      }
    }

    protected internal virtual void closeStartTag()
    {
      if (this.finished)
        throw new ArgumentException("trying to write past already finished output" + this.getLocation());
      if (this.seenBracket)
        this.seenBracket = this.seenBracketBracket = false;
      if (!this.startTagIncomplete && !this.setPrefixCalled)
        return;
      this.writeNamespaceDeclarations();
      this.write('>');
      this.elNamespaceCount[this.depth] = this.namespaceEnd;
      this.startTagIncomplete = false;
    }

    public virtual void comment(string text)
    {
      if (this.startTagIncomplete || this.setPrefixCalled || this.seenBracket)
        this.closeStartTag();
      if (this.doIndent && this.seenTag)
        this.seenTag = false;
      this.write("<!--");
      this.write(text);
      this.write("-->");
    }

    public virtual void docdecl(string text)
    {
      if (this.startTagIncomplete || this.setPrefixCalled || this.seenBracket)
        this.closeStartTag();
      if (this.doIndent && this.seenTag)
        this.seenTag = false;
      this.write("<!DOCTYPE");
      this.write(text);
      this.write(">");
    }

    public virtual void emptyTag(string name)
    {
      if (this.startTagIncomplete)
        this.closeStartTag();
      this.seenBracket = this.seenBracketBracket = false;
      if (this.doIndent && this.depth > 0 && this.seenTag)
        this.writeIndent();
      this.write('<');
      this.write(name);
      this.write("/>");
    }

    private void encodeAsn1ControlCharacter(char ch, string prefix)
    {
      this.write('<');
      if (prefix != null)
      {
        this.write(prefix);
        this.write(':');
      }
      this.write(XmlAsn1OutputStream.asn1ControlCharacters[(int) ch]);
      this.write("/>");
    }

    public virtual void endTag(string prefix, string name)
    {
      this.seenBracket = this.seenBracketBracket = false;
      if (name == null)
        throw new ArgumentException("end tag name can not be null" + this.getLocation());
      if (this.startTagIncomplete)
      {
        this.writeNamespaceDeclarations();
        this.write("/>");
        --this.depth;
      }
      else
      {
        --this.depth;
        if (this.doIndent && this.seenTag)
          this.writeIndent();
        this.write("</");
        if (prefix != null && prefix.Length > 0)
        {
          this.write(prefix);
          this.write(':');
        }
        this.write(name);
        this.write('>');
      }
      this.namespaceEnd = this.elNamespaceCount[this.depth];
      this.startTagIncomplete = false;
      this.setPrefixCalled = false;
      this.seenTag = true;
    }

    protected internal virtual void ensureElementsCapacity()
    {
      int length1 = this.elName.Length;
      int length2 = (this.depth >= 7 ? 2 * this.depth : 8) + 2;
      bool flag = length1 > 0;
      string[] strArray1 = new string[length2];
      if (flag)
        Array.Copy((Array) this.elName, 0, (Array) strArray1, 0, length1);
      this.elName = strArray1;
      string[] strArray2 = new string[length2];
      if (flag)
        Array.Copy((Array) this.elNamespace, 0, (Array) strArray2, 0, length1);
      this.elNamespace = strArray2;
      int[] numArray = new int[length2];
      if (flag)
        Array.Copy((Array) this.elNamespaceCount, 0, (Array) numArray, 0, length1);
      else
        numArray[0] = 0;
      this.elNamespaceCount = numArray;
    }

    protected virtual void ensureNamespacesCapacity()
    {
      int length = this.namespaceEnd > 7 ? 2 * this.namespaceEnd : 8;
      string[] strArray1 = new string[length];
      string[] strArray2 = new string[length];
      if (this.namespacePrefix != null)
      {
        Array.Copy((Array) this.namespacePrefix, 0, (Array) strArray1, 0, this.namespaceEnd);
        Array.Copy((Array) this.namespaceUri, 0, (Array) strArray2, 0, this.namespaceEnd);
      }
      this.namespacePrefix = strArray1;
      this.namespaceUri = strArray2;
    }

    public virtual void entityRef(string text)
    {
      if (this.startTagIncomplete || this.setPrefixCalled || this.seenBracket)
        this.closeStartTag();
      if (this.doIndent && this.seenTag)
        this.seenTag = false;
      this.write('&');
      this.write(text);
      this.write(';');
    }

    public virtual void flush()
    {
      if (!this.finished && this.startTagIncomplete)
        this.closeStartTag();
      try
      {
        ((TextWriter) this.writer).Flush();
      }
      catch (IOException ex)
      {
        throw new Asn1Exception(2, ex.Message, (Exception) ex);
      }
    }

    private string generatePrefix(string ns)
    {
      ++this.autoDeclaredPrefixes;
      string str = this.autoDeclaredPrefixes < XmlAsn1OutputStream.precomputedPrefixes.Length ? XmlAsn1OutputStream.precomputedPrefixes[this.autoDeclaredPrefixes] : string.Intern("n" + (object) this.autoDeclaredPrefixes);
      for (int index = this.namespaceEnd - 1; index >= 0; --index)
      {
        bool flag = str == this.namespacePrefix[index];
      }
      if (this.namespaceEnd >= this.namespacePrefix.Length)
        this.ensureNamespacesCapacity();
      this.namespacePrefix[this.namespaceEnd] = str;
      this.namespaceUri[this.namespaceEnd] = ns;
      ++this.namespaceEnd;
      return str;
    }

    public int getDepth()
    {
      return this.depth;
    }

    private string getLocation()
    {
      if (this.location == null)
        return "";
      else
        return " @" + this.location;
    }

    public virtual string getPrefix(string ns, bool doGeneratePrefix)
    {
      if (ns == null)
        throw new ArgumentException("namespace must be not null" + this.getLocation());
      if (ns.Length == 0)
        throw new ArgumentException("default namespace cannot have prefix" + this.getLocation());
      for (int index1 = this.namespaceEnd - 1; index1 >= 0; --index1)
      {
        if (ns == this.namespaceUri[index1])
        {
          string str = this.namespacePrefix[index1];
          for (int index2 = this.namespaceEnd - 1; index2 > index1; --index2)
          {
            bool flag = str == this.namespacePrefix[index2];
          }
          return str;
        }
      }
      if (!doGeneratePrefix)
        return (string) null;
      else
        return this.generatePrefix(ns);
    }

    public virtual void ignorableWhitespace(string text)
    {
      if (this.startTagIncomplete || this.setPrefixCalled || this.seenBracket)
        this.closeStartTag();
      if (this.doIndent && this.seenTag)
        this.seenTag = false;
      if (text.Length == 0)
        throw new ArgumentException("empty string is not allowed for ignorable whitespace" + this.getLocation());
      this.write(text);
    }

    public bool isStartTagIncomplete()
    {
      return this.startTagIncomplete;
    }

    protected internal virtual string lookupOrDeclarePrefix(string ns)
    {
      return this.getPrefix(ns, true);
    }

    public virtual void processingInstruction(string text)
    {
      if (this.startTagIncomplete || this.setPrefixCalled || this.seenBracket)
        this.closeStartTag();
      if (this.doIndent && this.seenTag)
        this.seenTag = false;
      this.write("<?");
      this.write(text);
      this.write("?>");
    }

    public virtual void rawBytes(byte[] bytes)
    {
      this.flush();
      try
      {
        this.output.Write(bytes, 0, bytes.Length);
      }
      catch (IOException ex)
      {
        throw new Asn1Exception(2, ByteArray.ByteArrayToHexString(bytes, (string) null, -1) + " can't be written in the stream.");
      }
    }

    protected virtual void rebuildIndentationBuf()
    {
      if (!this.doIndent)
        return;
      int num1 = 65;
      int num2 = 0;
      this.offsetNewLine = 0;
      if (this.writeLineSeparator)
      {
        this.offsetNewLine = this.lineSeparator.Length;
        num2 += this.offsetNewLine;
      }
      this.maxIndentLevel = 0;
      if (this.writeIndentation)
      {
        this.indentationJump = this.indentationString.Length;
        this.maxIndentLevel = num1 / this.indentationJump;
        num2 += this.maxIndentLevel * this.indentationJump;
      }
      if (this.indentationBuf == null || this.indentationBuf.Length < num2)
        this.indentationBuf = new char[num2 + 8];
      int num3 = 0;
      if (this.writeLineSeparator)
      {
        for (int index = 0; index < this.lineSeparator.Length; ++index)
          this.indentationBuf[num3++] = this.lineSeparator[index];
      }
      if (this.writeIndentation)
      {
        for (int index1 = 0; index1 < this.maxIndentLevel; ++index1)
        {
          for (int index2 = 0; index2 < this.indentationString.Length; ++index2)
            this.indentationBuf[num3++] = this.indentationString[index2];
        }
      }
    }

    protected virtual void reset()
    {
      this.location = (string) null;
      this.writer = (StreamWriter) null;
      this.autoDeclaredPrefixes = 0;
      this.depth = 0;
      for (int index = 0; index < this.elNamespaceCount.Length; ++index)
      {
        this.elName[index] = (string) null;
        this.elNamespace[index] = (string) null;
        this.elNamespaceCount[index] = 2;
      }
      this.namespaceEnd = 0;
      this.namespacePrefix[this.namespaceEnd] = "xmlns";
      this.namespaceUri[this.namespaceEnd] = "http://www.w3.org/2000/xmlns/";
      ++this.namespaceEnd;
      this.namespacePrefix[this.namespaceEnd] = "xml";
      this.namespaceUri[this.namespaceEnd] = "http://www.w3.org/XML/1998/namespace";
      ++this.namespaceEnd;
      this.finished = false;
      this.setPrefixCalled = false;
      this.startTagIncomplete = false;
      this.seenTag = false;
      this.seenBracket = false;
      this.seenBracketBracket = false;
    }

    public void setEncodingAttributeSingleQuoted(bool b)
    {
      this.encodingAttributeSingleQuoted = b;
    }

    public void setIndentSpaces(int indentSpaces)
    {
      if (indentSpaces > 0)
      {
        char[] chArray = new char[indentSpaces];
        for (int index = 0; index < indentSpaces; ++index)
          chArray[index] = ' ';
        this.indentationString = new string(chArray);
      }
      else
        this.indentationString = "";
      this.writeIndentation = this.indentationString != null && this.indentationString.Length > 0;
      this.doIndent = this.indentationString != null && (this.writeLineSeparator || this.writeIndentation);
      this.rebuildIndentationBuf();
      this.seenTag = false;
    }

    public void setLineSeparator(string lineSep)
    {
      this.lineSeparator = lineSep;
      this.writeLineSeparator = this.lineSeparator != null && this.lineSeparator.Length > 0;
      this.writeIndentation = this.indentationString != null && this.indentationString.Length > 0;
      this.doIndent = this.indentationString != null && (this.writeLineSeparator || this.writeIndentation);
      this.rebuildIndentationBuf();
      this.seenTag = false;
    }

    public virtual void setPrefix(string prefix, string ns)
    {
      this.setPrefix(prefix, ns, true);
    }

    public virtual void setPrefix(string prefix, string ns, bool doCloseStartTag)
    {
      if (this.startTagIncomplete && doCloseStartTag)
        this.closeStartTag();
      if (prefix == null && ns == null)
        return;
      if (ns == null)
        throw new ArgumentException("namespace must be not null" + this.getLocation());
      string str = prefix;
      if (prefix == null)
        str = "";
      for (int index = this.elNamespaceCount[this.depth]; index < this.namespaceEnd; ++index)
      {
        if (str == this.namespacePrefix[index])
        {
          if (!(this.namespaceUri[index] != ns))
            return;
          else
            throw new Asn1Exception(38, "duplicated prefix '" + str + "'" + this.getLocation());
        }
      }
      if (this.namespaceEnd >= this.namespacePrefix.Length)
        this.ensureNamespacesCapacity();
      this.namespacePrefix[this.namespaceEnd] = str;
      this.namespaceUri[this.namespaceEnd] = ns;
      ++this.namespaceEnd;
      this.setPrefixCalled = true;
    }

    public void setUseXML1_1ControlCharacters(bool b)
    {
      this.useXML1_1ControlCharacters = b;
    }

    public void setWhitespaceWithEscapesListSeparator(string listSeparator)
    {
      this.listSeparator = listSeparator;
    }

    public virtual void startTag(string ns, string name)
    {
      if (this.startTagIncomplete)
        this.closeStartTag();
      this.seenBracket = this.seenBracketBracket = false;
      if (this.doIndent && this.depth > 0 && this.seenTag)
        this.writeIndent();
      this.seenTag = true;
      this.setPrefixCalled = false;
      this.startTagIncomplete = true;
      ++this.depth;
      if (this.depth + 1 >= this.elName.Length)
        this.ensureElementsCapacity();
      this.elNamespace[this.depth] = ns;
      this.elName[this.depth] = name;
      this.write('<');
      if (ns != null)
      {
        if (ns.Length <= 0)
        {
          for (int index = this.namespaceEnd - 1; index >= 0; --index)
          {
            if (this.namespacePrefix[index] == "")
            {
              string str = this.namespaceUri[index];
              if (str != null)
              {
                if (str.Length > 0)
                  throw new SystemException("start tag can not be written in empty default namespace as default namespace is currently bound to '" + str + "'" + this.getLocation());
                else
                  break;
              }
              else
              {
                this.setPrefix("", "");
                break;
              }
            }
          }
        }
        else
        {
          string s = (string) null;
          if (this.depth > 0 && this.namespaceEnd - this.elNamespaceCount[this.depth - 1] == 1)
          {
            string str1 = this.namespaceUri[this.namespaceEnd - 1];
            if (str1 == ns || str1.Equals(ns))
            {
              string str2 = this.namespacePrefix[this.namespaceEnd - 1];
              for (int index = this.elNamespaceCount[this.depth - 1] - 1; index >= 2; --index)
              {
                string str3 = this.namespacePrefix[index];
                if (str3 == str2 || str3.Equals(str2))
                {
                  string str4 = this.namespaceUri[index];
                  if (str4 == str1 || str4.Equals(str1))
                  {
                    --this.namespaceEnd;
                    s = str2;
                    break;
                  }
                  else
                    break;
                }
              }
            }
          }
          if (s == null)
            s = this.lookupOrDeclarePrefix(ns);
          if (s.Length > 0)
          {
            this.write(s);
            this.write(':');
          }
        }
      }
      this.write(name);
    }

    public virtual void text(string text)
    {
      if (this.startTagIncomplete || this.setPrefixCalled)
        this.closeStartTag();
      if (this.doIndent && this.seenTag)
        this.seenTag = false;
      this.write(text);
    }

    public virtual void text(string text, bool escape, string controlCharacterPrefix)
    {
      if (this.startTagIncomplete || this.setPrefixCalled)
        this.closeStartTag();
      if (this.doIndent && this.seenTag)
        this.seenTag = false;
      if (escape)
        this.writeElementContent(text, controlCharacterPrefix);
      else
        this.write(text);
    }

    public virtual void textList(string[] list, bool escape)
    {
      if (this.startTagIncomplete || this.setPrefixCalled)
        this.closeStartTag();
      if (this.doIndent && this.seenTag)
        this.seenTag = false;
      if (list == null)
        return;
      for (int index = 0; index < list.Length; ++index)
      {
        if (escape)
          this.writeElementContent(list[index], (string) null);
        else
          this.write(list[index]);
        if (index < list.Length - 1)
          this.write(this.listSeparator);
      }
    }

    public virtual void write(char c)
    {
      try
      {
        this.writer.Write(c);
      }
      catch (IOException ex)
      {
        throw new Asn1Exception(2, ex.Message, (Exception) ex);
      }
    }

    public virtual void write(string s)
    {
      try
      {
        this.writer.Write(s);
      }
      catch (IOException ex)
      {
        throw new Asn1Exception(2, ex.Message, (Exception) ex);
      }
    }

    public virtual void write(char[] ca, int index, int length)
    {
      try
      {
        this.writer.Write(ca, index, length);
      }
      catch (IOException ex)
      {
        throw new Asn1Exception(2, ex.Message, (Exception) ex);
      }
    }

    protected virtual void writeAttributeValue(string val)
    {
      if (val == null)
        return;
      char ch1 = this.encodingAttributeSingleQuoted ? '\'' : '"';
      string s = this.encodingAttributeSingleQuoted ? "&apos;" : "&quot;";
      int startIndex = 0;
      for (int index = 0; index < val.Length; ++index)
      {
        char ch2 = val[index];
        switch (ch2)
        {
          case '&':
            if (index > startIndex)
              this.write(val.Substring(startIndex, index - startIndex));
            this.write("&amp;");
            startIndex = index + 1;
            goto default;
          case '<':
            if (index > startIndex)
              this.write(val.Substring(startIndex, index - startIndex));
            this.write("&lt;");
            startIndex = index + 1;
            break;
          default:
            int num;
            if ((int) ch2 == (int) ch1)
            {
              if (index > startIndex)
                this.write(val.Substring(startIndex, index - startIndex));
              this.write(s);
              startIndex = index + 1;
            }
            else if ((int) ch2 < 32)
            {
              if ((int) ch2 == 13 || (int) ch2 == 10 || (int) ch2 == 9)
              {
                if (index > startIndex)
                  this.write(val.Substring(startIndex, index - startIndex));
                this.write("&#");
                num = (int) ch2;
                this.write(num.ToString());
                this.write(';');
                startIndex = index + 1;
              }
              else if (!this.useXML1_1ControlCharacters)
              {
                num = (int) ch2;
                throw new Asn1Exception(38, "character " + num.ToString() + " is not allowed in output" + this.getLocation());
              }
              else
              {
                if ((int) ch2 <= 0)
                  throw new Asn1Exception(38, "character zero is not allowed in XML 1.1 output" + this.getLocation());
                if (index > startIndex)
                  this.write(val.Substring(startIndex, index - startIndex));
                this.write("&#");
                this.write(((int) ch2).ToString());
                this.write(';');
                startIndex = index + 1;
              }
            }
            break;
        }
      }
      if (startIndex > 0)
        this.write(val.Substring(startIndex));
      else
        this.write(val);
    }

    protected virtual void writeElementContent(string text, string controlCharacterPrefix)
    {
      if (text == null)
        return;
      int startIndex = 0;
      for (int index = 0; index < text.Length; ++index)
      {
        char ch = text[index];
        switch (ch)
        {
          case '&':
            if (index > startIndex)
              this.write(text.Substring(startIndex, index - startIndex));
            this.write("&amp;");
            startIndex = index + 1;
            break;
          case '<':
            if (index > startIndex)
              this.write(text.Substring(startIndex, index - startIndex));
            this.write("&lt;");
            startIndex = index + 1;
            break;
          case ']':
            if (this.seenBracket)
            {
              this.seenBracketBracket = true;
              continue;
            }
            else
            {
              this.seenBracket = true;
              continue;
            }
          default:
            if (this.seenBracketBracket && (int) ch == 62)
            {
              if (index > startIndex)
                this.write(text.Substring(startIndex, index - startIndex));
              this.write("&gt;");
              startIndex = index + 1;
              break;
            }
            else if ((int) ch < 32 && (int) ch != 9 && ((int) ch != 10 && (int) ch != 13))
            {
              if (!this.useXML1_1ControlCharacters)
              {
                if (index > startIndex)
                  this.write(text.Substring(startIndex, index - startIndex));
                this.encodeAsn1ControlCharacter(ch, controlCharacterPrefix);
                startIndex = index + 1;
              }
              else
              {
                if ((int) ch <= 0)
                  throw new Asn1Exception(38, "character zero is not allowed in XML 1.1 output" + this.getLocation());
                if (index > startIndex)
                  this.write(text.Substring(startIndex, index - startIndex));
                this.write("&#");
                this.write(((int) ch).ToString());
                this.write(';');
                startIndex = index + 1;
              }
              break;
            }
            else
              break;
        }
        if (this.seenBracket)
          this.seenBracketBracket = this.seenBracket = false;
      }
      if (startIndex > 0)
        this.write(text.Substring(startIndex));
      else
        this.write(text);
    }

    protected internal virtual void writeIndent()
    {
      this.write(this.indentationBuf, this.writeLineSeparator ? 0 : this.offsetNewLine, (this.depth > this.maxIndentLevel ? this.maxIndentLevel : this.depth) * this.indentationJump + this.offsetNewLine);
    }

    private void writeNamespaceDeclarations()
    {
      for (int index = this.elNamespaceCount[this.depth - 1]; index < this.namespaceEnd; ++index)
      {
        if (this.doIndent && this.namespaceUri[index].Length > 40)
        {
          this.writeIndent();
          this.write(" ");
        }
        if (this.namespacePrefix[index] != "")
        {
          this.write(" xmlns:");
          this.write(this.namespacePrefix[index]);
          this.write('=');
        }
        else
          this.write(" xmlns=");
        this.write(this.encodingAttributeSingleQuoted ? '\'' : '"');
        this.writeAttributeValue(this.namespaceUri[index]);
        this.write(this.encodingAttributeSingleQuoted ? '\'' : '"');
      }
    }

    public virtual void writeXMLDeclaration()
    {
      if (this.encodingAttributeSingleQuoted)
        this.write("<?xml version='1.0' encoding='UTF-8'?>");
      else
        this.write("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
      if (!this.doIndent)
        return;
      this.writeIndent();
    }
  }
}
