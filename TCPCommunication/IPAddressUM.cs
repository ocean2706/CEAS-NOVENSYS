// Decompiled with JetBrains decompiler
// Type: Novensys.eCard.SDK.TCPCommunication.IPAddressUM
// Assembly: Novensys.eCard.SDK, Version=1.1.56.0, Culture=neutral, PublicKeyToken=null
// MVID: 59F9E456-D3DA-4066-A4A4-692B516A775E
// Assembly location: C:\Program Files (x86)\CNAS\SIUI-SPITAL\Novensys.eCard.SDK.dll

using Novensys.eCard.SDK.Properties;
using Novensys.eCard.SDK.Utils.Log;
using System;
using System.Net;
using System.Net.Sockets;

namespace Novensys.eCard.SDK.TCPCommunication
{
  internal static class IPAddressUM
  {
    private static bool addressSet;
    private static string address;
    private static int port;

    public static IPEndPoint Address
    {
      get
      {
        if (IPAddressUM.addressSet)
          return new IPEndPoint(IPAddressUM.ResolveAddress(IPAddressUM.address), IPAddressUM.port);
        string unitateManagement = Settings.Default.IPUnitateManagement;
        return new IPEndPoint(string.IsNullOrEmpty(unitateManagement) ? IPAddressUM.GetLocalIPAddress() : IPAddressUM.ResolveAddress(unitateManagement), Settings.Default.PortUnitateManagement);
      }
    }

    public static string HostName
    {
      get
      {
        return Dns.GetHostName();
      }
    }

    public static void SetAddress(string address, int port)
    {
      LogManager.FileLog(string.Format("UM Address set to {0}:{1}", (object) address, (object) port));
      IPAddressUM.addressSet = true;
      IPAddressUM.address = address;
      IPAddressUM.port = port;
    }

    private static IPAddress GetLocalIPAddress()
    {
      foreach (IPAddress ipAddress in Dns.GetHostEntry(Dns.GetHostName()).AddressList)
      {
        if (ipAddress.AddressFamily == AddressFamily.InterNetwork)
          return ipAddress;
      }
      return (IPAddress) null;
    }

    private static IPAddress ResolveAddress(string address)
    {
      IPAddress address1;
      if (IPAddress.TryParse(address, out address1))
        return address1;
      IPAddress[] hostAddresses = Dns.GetHostAddresses(address);
      if (hostAddresses.Length <= 0)
        throw new Exception(string.Format("Cannot resolve host name {0}", (object) address));
      IPAddress ipAddress = hostAddresses[0];
      LogManager.FileLog(string.Format("Resolved {0} to {1}", (object) address, (object) ipAddress));
      return ipAddress;
    }
  }
}
