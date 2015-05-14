// Decompiled with JetBrains decompiler
// Type: Novensys.eCard.SDK.PCSC.Apdu.IApduExchange
// Assembly: Novensys.eCard.SDK, Version=1.1.56.0, Culture=neutral, PublicKeyToken=null
// MVID: 59F9E456-D3DA-4066-A4A4-692B516A775E
// Assembly location: C:\Program Files (x86)\CNAS\SIUI-SPITAL\Novensys.eCard.SDK.dll

namespace Novensys.eCard.SDK.PCSC.Apdu
{
  public interface IApduExchange
  {
    bool CardPresent { get; }

    void Transmit(byte[] sendBuffer, int sendLength, byte[] responseBuffer, ref int responseLength);

    RespApdu Exchange(CmdApdu cmdApdu);

    RespApdu Exchange(CmdApdu cmdApdu, ushort? expectedSW1SW2);

    byte[] Exchange(byte[] sendBuffer);

    void Exchange(byte[] sendBuffer, out byte[] responseBuffer);

    void Exchange(byte[] sendBuffer, out byte[] responseBuffer, ushort? expectedSW1SW2);

    void Exchange(byte[] sendBuffer, int sendLength, out byte[] responseBuffer, out int responseLength);

    void Exchange(byte[] sendBuffer, int sendLength, out byte[] responseBuffer, out int responseLength, ushort? expectedSW1SW2);

    void Exchange(byte[] sendBuffer, int sendLength, byte[] responseBuffer, ref int responseLength);

    void Exchange(byte[] sendBuffer, int sendLength, byte[] responseBuffer, ref int responseLength, ushort? expectedSW1SW2);

    byte[] Exchange(byte[] sendBuffer, ushort? expectedSW1SW2);

    RespApdu Exchange(string cmdApdu);

    RespApdu Exchange(string cmdApdu, ushort? expectedSW1SW2);
  }
}
