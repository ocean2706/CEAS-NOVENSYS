// Decompiled with JetBrains decompiler
// Type: Novensys.eCard.SDK.TCPCommunication.Globals
// Assembly: Novensys.eCard.SDK, Version=1.1.56.0, Culture=neutral, PublicKeyToken=null
// MVID: 59F9E456-D3DA-4066-A4A4-692B516A775E
// Assembly location: C:\Program Files (x86)\CNAS\SIUI-SPITAL\Novensys.eCard.SDK.dll

using System.Diagnostics;

namespace Novensys.eCard.SDK.TCPCommunication
{
  public static class Globals
  {
    public const int READ_TIMEOUT = 30000;

    public static string GetCalledMethod()
    {
      return new StackTrace().GetFrame(1).GetMethod().Name;
    }
  }
}
