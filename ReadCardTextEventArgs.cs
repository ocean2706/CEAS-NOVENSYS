// Decompiled with JetBrains decompiler
// Type: Novensys.eCard.SDK.ReadCardTextEventArgs
// Assembly: Novensys.eCard.SDK, Version=1.1.56.0, Culture=neutral, PublicKeyToken=null
// MVID: 59F9E456-D3DA-4066-A4A4-692B516A775E
// Assembly location: C:\Program Files (x86)\CNAS\SIUI-SPITAL\Novensys.eCard.SDK.dll

using System;

namespace Novensys.eCard.SDK
{
  public class ReadCardTextEventArgs : EventArgs
  {
    public string Prompt { get; internal set; }

    public bool Password { get; internal set; }

    public string Result { get; set; }

    public bool Cancel { get; set; }
  }
}
