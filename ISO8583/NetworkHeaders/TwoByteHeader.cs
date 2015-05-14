// Decompiled with JetBrains decompiler
// Type: Novensys.eCard.SDK.ISO8583.NetworkHeaders.TwoByteHeader
// Assembly: Novensys.eCard.SDK, Version=1.1.56.0, Culture=neutral, PublicKeyToken=null
// MVID: 59F9E456-D3DA-4066-A4A4-692B516A775E
// Assembly location: C:\Program Files (x86)\CNAS\SIUI-SPITAL\Novensys.eCard.SDK.dll

using Novensys.eCard.SDK.ISO8583;
using System;

namespace Novensys.eCard.SDK.ISO8583.NetworkHeaders
{
  public class TwoByteHeader : INetworkHeader
  {
    public int HeaderLength
    {
      get
      {
        return 2;
      }
    }

    public int GetMessageLength(byte[] header, int offset, out int newOffset)
    {
      newOffset = this.HeaderLength;
      return (int) header[offset] * 256 + (int) header[offset + 1];
    }

    public byte[] Pack(IMessage message)
    {
      byte[] numArray1 = message.ToMsg();
      int length = numArray1.Length;
      byte[] numArray2 = new byte[length + this.HeaderLength];
      numArray2[0] = (byte) (length / 256);
      numArray2[1] = (byte) (length % 256);
      Array.Copy((Array) numArray1, 0, (Array) numArray2, this.HeaderLength, length);
      return numArray2;
    }
  }
}
