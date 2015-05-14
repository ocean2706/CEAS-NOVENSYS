// Decompiled with JetBrains decompiler
// Type: Novensys.ASN1.IEncoder
// Assembly: Novensys.eCard.SDK, Version=1.1.56.0, Culture=neutral, PublicKeyToken=null
// MVID: 59F9E456-D3DA-4066-A4A4-692B516A775E
// Assembly location: C:\Program Files (x86)\CNAS\SIUI-SPITAL\Novensys.eCard.SDK.dll

using Novensys.ASN1.Type;
using System.Collections;
using System.IO;

namespace Novensys.ASN1
{
  public interface IEncoder
  {
    byte[] Data { get; }

    string InternalErrorLogDir { get; set; }

    bool IsValidating { get; set; }

    void Encode(Asn1Type type);

    void Encode(Asn1Type type, Stream outputStream);

    string GetProperty(string key);

    void SetProperties(Hashtable properties);

    void SetProperty(string key, string property);
  }
}
