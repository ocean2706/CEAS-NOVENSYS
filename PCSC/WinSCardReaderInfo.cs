// Decompiled with JetBrains decompiler
// Type: Novensys.eCard.SDK.PCSC.WinSCardReaderInfo
// Assembly: Novensys.eCard.SDK, Version=1.1.56.0, Culture=neutral, PublicKeyToken=null
// MVID: 59F9E456-D3DA-4066-A4A4-692B516A775E
// Assembly location: C:\Program Files (x86)\CNAS\SIUI-SPITAL\Novensys.eCard.SDK.dll

using Novensys.eCard.SDK.PCSC.ACR;
using Novensys.eCard.SDK.PCSC.HID;
using Novensys.eCard.SDK.PCSC.Reiner;
using System;
using System.Collections.Generic;

namespace Novensys.eCard.SDK.PCSC
{
  public class WinSCardReaderInfo : ICloneable
  {
    private static WinSCardReaderInfo[] instances = new WinSCardReaderInfo[20]
    {
      new WinSCardReaderInfo()
      {
        Name = "OMNIKEY CardMan 3821",
        CardTextReaderType = typeof (HIDCardTextReader)
      },
      new WinSCardReaderInfo()
      {
        Name = "OMNIKEY CardMan 3x21"
      },
      new WinSCardReaderInfo()
      {
        Name = "Generic EMV Smartcard Reader"
      },
      new WinSCardReaderInfo()
      {
        Name = "ACS ACR83U",
        CardTextReaderType = typeof (ACRCardTextReader)
      },
      new WinSCardReaderInfo()
      {
        Name = "ACS CCID USB Reader"
      },
      new WinSCardReaderInfo()
      {
        Name = "Athena ASEDrive IIIe USB"
      },
      new WinSCardReaderInfo()
      {
        Name = "Athena ASEDrive V3CR"
      },
      new WinSCardReaderInfo()
      {
        Name = "REINER SCT cyberJack pinpad/e-com USB"
      },
      new WinSCardReaderInfo()
      {
        Name = "REINER SCT cyberJack go"
      },
      new WinSCardReaderInfo()
      {
        Name = "Gemalto USB Smart Card Reader"
      },
      new WinSCardReaderInfo()
      {
        Name = "Gemalto USB SmartCard Reader"
      },
      new WinSCardReaderInfo()
      {
        Name = "Gemalto Ezio Shield"
      },
      new WinSCardReaderInfo()
      {
        Name = "SCM Microsystems Inc. SCR35xx USB Smart Card Reader"
      },
      new WinSCardReaderInfo()
      {
        Name = "SCM Microsystems Inc. SCR35xx v2.0 USB SC Reader"
      },
      new WinSCardReaderInfo()
      {
        Name = "ACS ACR39U ICC Reader"
      },
      new WinSCardReaderInfo()
      {
        Name = "Feitian SCR301"
      },
      new WinSCardReaderInfo()
      {
        Name = "XIRING USB Smart Card Reader"
      },
      new WinSCardReaderInfo()
      {
        Name = "XIRING CCID Smart Card Reader"
      },
      new WinSCardReaderInfo()
      {
        Name = "REINER SCT cyberJack syonic",
        CardTextReaderType = typeof (ReinerCardTextReader)
      },
      new WinSCardReaderInfo()
      {
        Name = "Gemalto IDBridge CT7xx"
      }
    };

    public static IEnumerable<WinSCardReaderInfo> Instances
    {
      get
      {
        foreach (WinSCardReaderInfo winScardReaderInfo in WinSCardReaderInfo.instances)
          yield return (WinSCardReaderInfo) winScardReaderInfo.Clone();
      }
    }

    public string Name { get; private set; }

    public string FullName { get; private set; }

    internal Type CardTextReaderType { get; private set; }

    private WinSCardReaderInfo()
    {
    }

    public object Clone()
    {
      return this.MemberwiseClone();
    }

    public override bool Equals(object obj)
    {
      WinSCardReaderInfo winScardReaderInfo = obj as WinSCardReaderInfo;
      return winScardReaderInfo != null && string.Equals(this.Name, winScardReaderInfo.Name) && string.Equals(this.FullName, winScardReaderInfo.FullName);
    }

    public override int GetHashCode()
    {
      return (this.Name == null ? 0 : this.Name.GetHashCode()) ^ (this.FullName == null ? 0 : this.FullName.GetHashCode());
    }

    public override string ToString()
    {
      return this.FullName;
    }

    public static WinSCardReaderInfo FindReader(string fullName)
    {
      if (string.IsNullOrEmpty(fullName))
        return (WinSCardReaderInfo) null;
      foreach (WinSCardReaderInfo winScardReaderInfo1 in WinSCardReaderInfo.instances)
      {
        if (fullName.StartsWith(winScardReaderInfo1.Name))
        {
          WinSCardReaderInfo winScardReaderInfo2 = (WinSCardReaderInfo) winScardReaderInfo1.Clone();
          winScardReaderInfo2.FullName = fullName;
          return winScardReaderInfo2;
        }
      }
      return (WinSCardReaderInfo) null;
    }
  }
}
