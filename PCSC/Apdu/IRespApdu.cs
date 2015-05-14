// Decompiled with JetBrains decompiler
// Type: Novensys.eCard.SDK.PCSC.Apdu.IRespApdu
// Assembly: Novensys.eCard.SDK, Version=1.1.56.0, Culture=neutral, PublicKeyToken=null
// MVID: 59F9E456-D3DA-4066-A4A4-692B516A775E
// Assembly location: C:\Program Files (x86)\CNAS\SIUI-SPITAL\Novensys.eCard.SDK.dll

namespace Novensys.eCard.SDK.PCSC.Apdu
{
  internal interface IRespApdu
  {
    byte[] Data { get; }

    int ReaderStatus { get; }

    int RespLength { get; }

    byte? SW1 { get; }

    ushort? SW1SW2 { get; }

    byte? SW2 { get; set; }

    bool Equals(object obj);

    int GetHashCode();

    string ToString();
  }
}
