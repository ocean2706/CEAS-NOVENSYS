// Decompiled with JetBrains decompiler
// Type: Novensys.eCard.SDK.ManagerSesiuniCard
// Assembly: Novensys.eCard.SDK, Version=1.1.56.0, Culture=neutral, PublicKeyToken=null
// MVID: 59F9E456-D3DA-4066-A4A4-692B516A775E
// Assembly location: C:\Program Files (x86)\CNAS\SIUI-SPITAL\Novensys.eCard.SDK.dll

using Novensys.eCard.SDK.PCSC;
using Novensys.eCard.SDK.TCPCommunication;
using System;
using System.Collections.Generic;

namespace Novensys.eCard.SDK
{
  public sealed class ManagerSesiuniCard
  {
    private static ISesiuneCard ultimaSesiune;

    private ManagerSesiuniCard()
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

    public static ISesiuneCard StartSesiuneNoua()
    {
      return ManagerSesiuniCard.StartSesiuneNoua((string) null);
    }

    public static ISesiuneCard StartSesiuneNoua(string desiredTerminalName)
    {
      WinSCardContextJob.Instance.Start();
      TerminalManager terminalManager = new TerminalManager();
      terminalManager.DesiredTerminalName = desiredTerminalName;
      SesiuneCard newInstance = SesiuneCard.GetNewInstance(terminalManager);
      newInstance.Card = terminalManager.Card;
      ManagerSesiuniCard.ultimaSesiune = (ISesiuneCard) newInstance;
      return (ISesiuneCard) newInstance;
    }

    [Obsolete("Folositi metoda Stop() a interfetei ISesiuneCard")]
    public static void StopSesiuneCurenta()
    {
      if (ManagerSesiuniCard.ultimaSesiune == null)
        return;
      ManagerSesiuniCard.ultimaSesiune.Stop();
      ManagerSesiuniCard.ultimaSesiune = (ISesiuneCard) null;
    }

    public static void SetAdresaUnitateManagement(string ipAddress, int port)
    {
      IPAddressUM.SetAddress(ipAddress, port);
    }
  }
}
