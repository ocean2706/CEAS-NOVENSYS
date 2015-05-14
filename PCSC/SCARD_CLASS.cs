// Decompiled with JetBrains decompiler
// Type: Novensys.eCard.SDK.PCSC.SCARD_CLASS
// Assembly: Novensys.eCard.SDK, Version=1.1.56.0, Culture=neutral, PublicKeyToken=null
// MVID: 59F9E456-D3DA-4066-A4A4-692B516A775E
// Assembly location: C:\Program Files (x86)\CNAS\SIUI-SPITAL\Novensys.eCard.SDK.dll

namespace Novensys.eCard.SDK.PCSC
{
  internal enum SCARD_CLASS : uint
  {
    VENDOR_INFO = 1U,
    COMMUNICATIONS = 2U,
    PROTOCOL = 3U,
    POWER_MGMT = 4U,
    SECURITY = 5U,
    MECHANICAL = 6U,
    VENDOR_DEFINED = 7U,
    IFD_PROTOCOL = 8U,
    ICC_STATE = 9U,
    PERF = 32766U,
    SYSTEM = 32767U,
  }
}
