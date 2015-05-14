// Decompiled with JetBrains decompiler
// Type: Novensys.ASN1.IDecoder
// Assembly: Novensys.eCard.SDK, Version=1.1.56.0, Culture=neutral, PublicKeyToken=null
// MVID: 59F9E456-D3DA-4066-A4A4-692B516A775E
// Assembly location: C:\Program Files (x86)\CNAS\SIUI-SPITAL\Novensys.eCard.SDK.dll

using Novensys.ASN1.Type;
using System.Collections;
using System.IO;

namespace Novensys.ASN1
{
  public interface IDecoder
  {
    byte[] Data { set; }

    string InternalErrorLogDir { get; set; }

    bool IsResolvingContent { get; set; }

    bool IsValidating { get; set; }

    void Decode(Asn1Type type);

    void Decode(byte[] data, Asn1Type type);

    void Decode(Stream inputStream, Asn1Type type);

    void Decode(byte[] data, int offset, int length, Asn1Type type);

    string GetProperty(string key);

    void SetProperties(Hashtable properties);

    void SetProperty(string key, string property);

    int UsedBytes();
  }
}
