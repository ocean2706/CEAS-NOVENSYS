// Decompiled with JetBrains decompiler
// Type: Novensys.eCard.SDK.PCSC.SCARD_READERSTATE
// Assembly: Novensys.eCard.SDK, Version=1.1.56.0, Culture=neutral, PublicKeyToken=null
// MVID: 59F9E456-D3DA-4066-A4A4-692B516A775E
// Assembly location: C:\Program Files (x86)\CNAS\SIUI-SPITAL\Novensys.eCard.SDK.dll

using System;
using System.Runtime.InteropServices;

namespace Novensys.eCard.SDK.PCSC
{
  public struct SCARD_READERSTATE
  {
    public string m_szReader;
    public IntPtr m_pvUserData;
    public uint m_dwCurrentState;
    public uint m_dwEventState;
    public uint m_cbAtr;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
    public byte[] m_rgbAtr;
  }
}
