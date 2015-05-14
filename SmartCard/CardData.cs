// Decompiled with JetBrains decompiler
// Type: Novensys.eCard.SDK.Entities.SmartCard.CardData
// Assembly: Novensys.eCard.SDK, Version=1.1.56.0, Culture=neutral, PublicKeyToken=null
// MVID: 59F9E456-D3DA-4066-A4A4-692B516A775E
// Assembly location: C:\Program Files (x86)\CNAS\SIUI-SPITAL\Novensys.eCard.SDK.dll

using System;

namespace Novensys.eCard.SDK.Entities.SmartCard
{
  [Serializable]
  public class CardData
  {
    public DeviceData DeviceData { get; private set; }

    public DateIdentificare DateIdentificare { get; private set; }

    public DateAdministrative DateAdministrative { get; private set; }

    public DateClinicePrimare DateClinicePrimare { get; private set; }

    public ExtensiiClinice1 ExtensiiClinice1 { get; private set; }

    public ExtensiiClinice2 ExtensiiClinice2 { get; private set; }

    public SecurityData SecurityData { get; private set; }

    public DezvoltariUlterioare1 DezvoltariUlterioare1 { get; private set; }

    public DezvoltariUlterioare2 DezvoltariUlterioare2 { get; private set; }

    public StareCard StareCard { get; set; }

    public CardData()
    {
      this.DeviceData = new DeviceData();
      this.DateIdentificare = new DateIdentificare();
      this.DateAdministrative = new DateAdministrative();
      this.DateClinicePrimare = new DateClinicePrimare();
      this.ExtensiiClinice1 = new ExtensiiClinice1();
      this.ExtensiiClinice2 = new ExtensiiClinice2();
      this.DezvoltariUlterioare1 = new DezvoltariUlterioare1();
      this.SecurityData = new SecurityData();
      this.DezvoltariUlterioare2 = new DezvoltariUlterioare2();
    }
  }
}
