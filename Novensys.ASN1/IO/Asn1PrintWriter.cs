// Decompiled with JetBrains decompiler
// Type: Novensys.ASN1.IO.Asn1PrintWriter
// Assembly: Novensys.eCard.SDK, Version=1.1.56.0, Culture=neutral, PublicKeyToken=null
// MVID: 59F9E456-D3DA-4066-A4A4-692B516A775E
// Assembly location: C:\Program Files (x86)\CNAS\SIUI-SPITAL\Novensys.eCard.SDK.dll

using Novensys.ASN1.Type;
using System.IO;
using System.Text;

namespace Novensys.ASN1.IO
{
  public class Asn1PrintWriter
  {
    protected Asn1TypeExplorer _explorer = new Asn1TypeExplorer();
    protected TextWriter _out;

    public Asn1PrintWriter(TextWriter writer)
    {
      this._out = writer;
    }

    public virtual void Close()
    {
      this._out.Close();
    }

    public virtual void Flush()
    {
      this._out.Flush();
    }

    protected virtual void printAsn1Node(int level, bool newline)
    {
      StringBuilder stringBuilder = new StringBuilder();
      for (int index = 0; index < level; ++index)
        stringBuilder.Append("  ");
      while (this._explorer.IsCurrentNode())
      {
        if (level > 0)
          this._out.WriteLine();
        this._out.Write((object) stringBuilder);
        this._out.Write(this._explorer.GetNodeAsString());
        if (this._explorer.GotoFirstChild())
        {
          this._out.WriteLine();
          this._out.Write((string) (object) stringBuilder + (object) "{");
          this.printAsn1Node(level + 1, true);
          this._out.WriteLine();
          this._out.Write((string) (object) stringBuilder + (object) "}");
          this._explorer.GotoParent();
        }
        this._explorer.GotoNextSibling();
      }
      if (!newline || level != 0)
        return;
      this._out.WriteLine();
    }

    public virtual void Write(Asn1Type type)
    {
      this._explorer.SetTypeInstance(type);
      this._explorer.GotoRoot();
      this.printAsn1Node(0, false);
    }

    public virtual void WriteLine(Asn1Type type)
    {
      this._explorer.SetTypeInstance(type);
      this._explorer.GotoRoot();
      this.printAsn1Node(0, true);
    }
  }
}
