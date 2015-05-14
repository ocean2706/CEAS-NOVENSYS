// Decompiled with JetBrains decompiler
// Type: Novensys.ASN1.Util.PiOrComment
// Assembly: Novensys.eCard.SDK, Version=1.1.56.0, Culture=neutral, PublicKeyToken=null
// MVID: 59F9E456-D3DA-4066-A4A4-692B516A775E
// Assembly location: C:\Program Files (x86)\CNAS\SIUI-SPITAL\Novensys.eCard.SDK.dll

using Novensys.ASN1;
using Novensys.ASN1.IO;
using System.IO;
using System.Text;

namespace Novensys.ASN1.Util
{
  public class PiOrComment
  {
    public const int AFTER_TAG = 3;
    public const int AFTER_VALUE = 2;
    public const int BEFORE_TAG = 0;
    public const int BEFORE_VALUE = 1;
    private int __position;
    private string __value;

    public int Position
    {
      get
      {
        return this.__position;
      }
      set
      {
        if (value != 0 && value != 3 && (value != 2 && value != 1))
          throw new Asn1Exception(54, "position '" + (object) value + "' is not in BEFORE_TAG, BEFORE_VALUE, AFTER_VALUE or AFTER_TAG.");
        this.__position = value;
      }
    }

    public string Value
    {
      get
      {
        return this.__value;
      }
      set
      {
        this.__value = value;
      }
    }

    public PiOrComment()
    {
    }

    public PiOrComment(string val, int position)
    {
      this.Position = position;
      this.Value = val;
    }

    public static void ValidateSyntax(string piOrCommentValue)
    {
      XmlAsn1InputStream xmlAsn1InputStream = new XmlAsn1InputStream((Stream) new MemoryStream(Encoding.UTF8.GetBytes(piOrCommentValue + "<T/>")));
      xmlAsn1InputStream.nextToken();
      if (xmlAsn1InputStream.getEventType() != 8 && xmlAsn1InputStream.getEventType() != 9)
        throw new Asn1Exception(54, "'" + piOrCommentValue + "' does not conform to an XML 1.0 Processing Instruction or Comment");
    }
  }
}
