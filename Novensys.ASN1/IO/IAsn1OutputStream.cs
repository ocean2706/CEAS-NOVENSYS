// Decompiled with JetBrains decompiler
// Type: Novensys.ASN1.IO.IAsn1OutputStream
// Assembly: Novensys.eCard.SDK, Version=1.1.56.0, Culture=neutral, PublicKeyToken=null
// MVID: 59F9E456-D3DA-4066-A4A4-692B516A775E
// Assembly location: C:\Program Files (x86)\CNAS\SIUI-SPITAL\Novensys.eCard.SDK.dll

using System.IO;

namespace Novensys.ASN1.IO
{
  public interface IAsn1OutputStream
  {
    void flush();

    Stream getStream();

    void insertByteAt(int index, byte b);

    int size();

    byte[] toByteArray();

    void writeByte(byte b);

    void writeByte(byte b, int index);

    void writeBytes(byte[] b);

    void writeBytes(byte[] b, int len);

    void writeTo(IAsn1OutputStream asn1OutputStream);
  }
}
