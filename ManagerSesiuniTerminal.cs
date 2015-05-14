// Decompiled with JetBrains decompiler
// Type: Novensys.eCard.SDK.ManagerSesiuniTerminal
// Assembly: Novensys.eCard.SDK, Version=1.1.56.0, Culture=neutral, PublicKeyToken=null
// MVID: 59F9E456-D3DA-4066-A4A4-692B516A775E
// Assembly location: C:\Program Files (x86)\CNAS\SIUI-SPITAL\Novensys.eCard.SDK.dll

using Novensys.eCard.SDK.PCSC;
using Novensys.eCard.SDK.TCPCommunication;
using System;
using System.Collections.Generic;

namespace Novensys.eCard.SDK
{
  public sealed class ManagerSesiuniTerminal
  {
    private static ISesiuneTerminal ultimaSesiune;

    private ManagerSesiuniTerminal()
    {
    }

    public static string[] GetSupportedTerminalNames()
    {
      List<WinSCardReaderInfo> list = new List<WinSCardReaderInfo>(WinSCardReaderInfo.Instances);
      string[] strArray = new string[list.Count];
      for (int index = 0; index < list.Count; ++index)
        strArray[index] = list[index].Name;
      return strArray;
    }

    public static ISesiuneTerminal StartSesiuneNoua()
    {
      return ManagerSesiuniTerminal.StartSesiuneNoua((string) null);
    }

    public static ISesiuneTerminal StartSesiuneNoua(string desiredTerminalName)
    {
      WinSCardContextJob.Instance.Start();
      SesiuneTerminal newInstance = SesiuneTerminal.GetNewInstance(new TerminalManager()
      {
        DesiredTerminalName = desiredTerminalName
      });
      ManagerSesiuniTerminal.ultimaSesiune = (ISesiuneTerminal) newInstance;
      return (ISesiuneTerminal) newInstance;
    }

    [Obsolete("Folositi metoda Stop() a interfetei ISesiuneCard")]
    public static void StopSesiuneCurenta()
    {
      if (ManagerSesiuniTerminal.ultimaSesiune == null)
        return;
      ManagerSesiuniTerminal.ultimaSesiune.Stop();
      ManagerSesiuniTerminal.ultimaSesiune = (ISesiuneTerminal) null;
    }

    public static void SetAdresaUnitateManagement(string ipAddress, int port)
    {
      IPAddressUM.SetAddress(ipAddress, port);
    }
  }
}
