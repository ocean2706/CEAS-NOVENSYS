// Decompiled with JetBrains decompiler
// Type: Novensys.eCard.SDK.Entities.SmartCard.SecurityData
// Assembly: Novensys.eCard.SDK, Version=1.1.56.0, Culture=neutral, PublicKeyToken=null
// MVID: 59F9E456-D3DA-4066-A4A4-692B516A775E
// Assembly location: C:\Program Files (x86)\CNAS\SIUI-SPITAL\Novensys.eCard.SDK.dll

using System;

namespace Novensys.eCard.SDK.Entities.SmartCard
{
  [Serializable]
  public class SecurityData
  {
    public Camp CertificateDigitale { get; private set; }

    public Camp CheiPrivate { get; private set; }

    public Camp PINTransport { get; private set; }

    public Camp PINCardHolder { get; private set; }

    public Camp CertificatMAI { get; private set; }

    public SecurityData()
    {
      this.CertificateDigitale = new Camp(CoduriCampuriCard.G1, "Certificate digitale", 6);
      this.CheiPrivate = new Camp(CoduriCampuriCard.G2, "Chei private", 6);
      this.PINTransport = new Camp(CoduriCampuriCard.G3, "PIN Transport", 6);
      this.PINCardHolder = new Camp(CoduriCampuriCard.G4, "PIN CardHolder", 6);
      this.CertificatMAI = new Camp(CoduriCampuriCard.G5, "Certificat MAI", 7);
    }
  }
}
