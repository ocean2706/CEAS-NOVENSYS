// Decompiled with JetBrains decompiler
// Type: Novensys.eCard.SDK.PCSC.Apdu.ICmdApdu
// Assembly: Novensys.eCard.SDK, Version=1.1.56.0, Culture=neutral, PublicKeyToken=null
// MVID: 59F9E456-D3DA-4066-A4A4-692B516A775E
// Assembly location: C:\Program Files (x86)\CNAS\SIUI-SPITAL\Novensys.eCard.SDK.dll

namespace Novensys.eCard.SDK.PCSC.Apdu
{
  internal interface ICmdApdu
  {
    byte CLA { get; set; }

    byte[] Data { get; set; }

    byte INS { get; set; }

    int? Lc { get; set; }

    int? Le { get; set; }

    byte P1 { get; set; }

    byte P2 { get; set; }

    object Clone();

    bool Equals(object obj);

    byte[] GetBytes();

    int GetHashCode();

    void SetValue(string cmdApduString);

    void SetValue(byte[] cmdApduBuffer);

    string ToString();
  }
}
