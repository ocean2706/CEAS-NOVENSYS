// Decompiled with JetBrains decompiler
// Type: Novensys.eCard.SDK.ISO8583.NetworkHeaders.INetworkHeader
// Assembly: Novensys.eCard.SDK, Version=1.1.56.0, Culture=neutral, PublicKeyToken=null
// MVID: 59F9E456-D3DA-4066-A4A4-692B516A775E
// Assembly location: C:\Program Files (x86)\CNAS\SIUI-SPITAL\Novensys.eCard.SDK.dll

using Novensys.eCard.SDK.ISO8583;

namespace Novensys.eCard.SDK.ISO8583.NetworkHeaders
{
  public interface INetworkHeader
  {
    int HeaderLength { get; }

    int GetMessageLength(byte[] header, int offset, out int newOffset);

    byte[] Pack(IMessage message);
  }
}
