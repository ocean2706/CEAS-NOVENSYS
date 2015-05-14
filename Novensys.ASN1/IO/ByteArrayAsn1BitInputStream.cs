// Decompiled with JetBrains decompiler
// Type: Novensys.ASN1.IO.ByteArrayAsn1BitInputStream
// Assembly: Novensys.eCard.SDK, Version=1.1.56.0, Culture=neutral, PublicKeyToken=null
// MVID: 59F9E456-D3DA-4066-A4A4-692B516A775E
// Assembly location: C:\Program Files (x86)\CNAS\SIUI-SPITAL\Novensys.eCard.SDK.dll

using System;
using System.IO;

namespace Novensys.ASN1.IO
{
  [Serializable]
  public class ByteArrayAsn1BitInputStream : Asn1BitInputStream
  {
    public ByteArrayAsn1BitInputStream(byte[] data, int startBit)
      : base((Stream) new MemoryStream(data), startBit)
    {
    }
  }
}
