// Decompiled with JetBrains decompiler
// Type: Novensys.eCard.SDK.PCSC.SCARD_CARD_STATE
// Assembly: Novensys.eCard.SDK, Version=1.1.56.0, Culture=neutral, PublicKeyToken=null
// MVID: 59F9E456-D3DA-4066-A4A4-692B516A775E
// Assembly location: C:\Program Files (x86)\CNAS\SIUI-SPITAL\Novensys.eCard.SDK.dll

namespace Novensys.eCard.SDK.PCSC
{
  public enum SCARD_CARD_STATE : uint
  {
    UNAWARE = 0U,
    IGNORE = 1U,
    CHANGED = 2U,
    UNKNOWN = 4U,
    UNAVAILABLE = 8U,
    EMPTY = 16U,
    PRESENT = 32U,
    ATRMATCH = 64U,
    EXCLUSIVE = 128U,
    INUSE = 256U,
    MUTE = 512U,
    UNPOWERED = 1024U,
  }
}
