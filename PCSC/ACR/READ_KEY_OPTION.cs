// Decompiled with JetBrains decompiler
// Type: Novensys.eCard.SDK.PCSC.ACR.READ_KEY_OPTION
// Assembly: Novensys.eCard.SDK, Version=1.1.56.0, Culture=neutral, PublicKeyToken=null
// MVID: 59F9E456-D3DA-4066-A4A4-692B516A775E
// Assembly location: C:\Program Files (x86)\CNAS\SIUI-SPITAL\Novensys.eCard.SDK.dll

namespace Novensys.eCard.SDK.PCSC.ACR
{
  internal struct READ_KEY_OPTION
  {
    public byte bTimeOut;
    public byte wPINMaxsize;
    public byte wPINMinsize;
    public byte bKeyReturnCondition;
    public byte bEchoLCDStartPosition;
    public byte bEchoLCDMode;
  }
}
