// Decompiled with JetBrains decompiler
// Type: Novensys.eCard.SDK.PCSC.IWinSCardTextReader
// Assembly: Novensys.eCard.SDK, Version=1.1.56.0, Culture=neutral, PublicKeyToken=null
// MVID: 59F9E456-D3DA-4066-A4A4-692B516A775E
// Assembly location: C:\Program Files (x86)\CNAS\SIUI-SPITAL\Novensys.eCard.SDK.dll

using System;

namespace Novensys.eCard.SDK.PCSC
{
  internal interface IWinSCardTextReader : IDisposable
  {
    IntPtr PHContext { get; set; }

    IntPtr PHCard { get; set; }

    string ReaderName { get; set; }

    string ReadText(string prompt, bool password);
  }
}
