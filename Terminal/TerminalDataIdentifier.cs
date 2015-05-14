// Decompiled with JetBrains decompiler
// Type: Novensys.eCard.SDK.Terminal.TerminalDataIdentifier
// Assembly: Novensys.eCard.SDK, Version=1.1.56.0, Culture=neutral, PublicKeyToken=null
// MVID: 59F9E456-D3DA-4066-A4A4-692B516A775E
// Assembly location: C:\Program Files (x86)\CNAS\SIUI-SPITAL\Novensys.eCard.SDK.dll

using System;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;

namespace Novensys.eCard.SDK.Terminal
{
  internal class TerminalDataIdentifier
  {
    public string NumarContract { get; set; }

    public DateTime DataContract { get; set; }

    public string CasaAsigurare { get; set; }

    public string TipFurnizor { get; set; }

    public string CUI { get; set; }

    public string CIF { get; set; }

    public string NumeStatie { get; set; }

    public string AdresaUM { get; set; }

    public int Versiune { get; private set; }

    public string Hash
    {
      get
      {
        byte[] hash = new SHA1Managed().ComputeHash(Encoding.UTF8.GetBytes(this.HashText));
        StringBuilder stringBuilder = new StringBuilder();
        foreach (byte num in hash)
          stringBuilder.AppendFormat("{0:X2}", (object) num);
        return ((object) stringBuilder).ToString();
      }
    }

    internal string HashText
    {
      get
      {
        return string.Format((IFormatProvider) CultureInfo.InvariantCulture, "{0}\r\n{1:yyyyMMdd}\r\n{2}\r\n{3}\r\n{4}\r\n{5}\r\n{6}\r\n{7}\r\n{8}", (object) this.NumarContract, (object) this.DataContract, (object) this.CasaAsigurare, (object) this.TipFurnizor, (object) this.CUI, (object) this.CIF, (object) this.NumeStatie, (object) this.AdresaUM, (object) this.Versiune);
      }
    }

    public TerminalDataIdentifier()
    {
      this.Versiune = 1;
    }

    public override bool Equals(object obj)
    {
      StringComparison comparisonType = StringComparison.Ordinal;
      TerminalDataIdentifier terminalDataIdentifier = obj as TerminalDataIdentifier;
      return terminalDataIdentifier != null && string.Equals(this.NumarContract, terminalDataIdentifier.NumarContract, comparisonType) && (DateTime.Equals(this.DataContract.Date, terminalDataIdentifier.DataContract.Date) && string.Equals(this.CasaAsigurare, terminalDataIdentifier.CasaAsigurare, comparisonType) && (string.Equals(this.TipFurnizor, terminalDataIdentifier.TipFurnizor, comparisonType) && string.Equals(this.CUI, terminalDataIdentifier.CUI, comparisonType)) && (string.Equals(this.CIF, terminalDataIdentifier.CIF, comparisonType) && string.Equals(this.NumeStatie, terminalDataIdentifier.NumeStatie, comparisonType) && string.Equals(this.AdresaUM, terminalDataIdentifier.AdresaUM, comparisonType))) && object.Equals((object) this.Versiune, (object) terminalDataIdentifier.Versiune);
    }

    public override int GetHashCode()
    {
      return (this.NumarContract == null ? 0 : this.NumarContract.GetHashCode()) ^ this.DataContract.GetHashCode() ^ (this.CasaAsigurare == null ? 0 : this.CasaAsigurare.GetHashCode()) ^ (this.TipFurnizor == null ? 0 : this.TipFurnizor.GetHashCode()) ^ (this.CUI == null ? 0 : this.CUI.GetHashCode()) ^ (this.CIF == null ? 0 : this.CIF.GetHashCode()) ^ (this.NumeStatie == null ? 0 : this.NumeStatie.GetHashCode()) ^ (this.AdresaUM == null ? 0 : this.AdresaUM.GetHashCode()) ^ this.Versiune.GetHashCode();
    }
  }
}
