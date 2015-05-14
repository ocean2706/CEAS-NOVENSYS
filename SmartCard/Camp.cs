// Decompiled with JetBrains decompiler
// Type: Novensys.eCard.SDK.Entities.SmartCard.Camp
// Assembly: Novensys.eCard.SDK, Version=1.1.56.0, Culture=neutral, PublicKeyToken=null
// MVID: 59F9E456-D3DA-4066-A4A4-692B516A775E
// Assembly location: C:\Program Files (x86)\CNAS\SIUI-SPITAL\Novensys.eCard.SDK.dll

using System;
using System.ComponentModel;

namespace Novensys.eCard.SDK.Entities.SmartCard
{
  [Serializable]
  public class Camp : INotifyPropertyChanged
  {
    private object valoare = (object) null;

    public string Cod { get; private set; }

    public string Denumire { get; private set; }

    public int Partitie { get; private set; }

    public object Valoare
    {
      get
      {
        return this.valoare;
      }
      set
      {
        if (value == this.valoare)
          return;
        this.valoare = value;
        this.OnPropertyChanged("Valoare");
      }
    }

    public bool IsChanged { get; set; }

    public event PropertyChangedEventHandler PropertyChanged;

    public Camp(CoduriCampuriCard cod, string denumire, int partitie)
    {
      this.Cod = ((object) cod).ToString();
      this.Denumire = denumire;
      this.Partitie = partitie;
      this.PropertyChanged += new PropertyChangedEventHandler(this.Camp_PropertyChanged);
    }

    private void Camp_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
      if (!(e.PropertyName == "Valoare"))
        return;
      this.IsChanged = true;
    }

    protected void OnPropertyChanged(string propertyName)
    {
      if (this.PropertyChanged == null)
        return;
      this.PropertyChanged((object) this, new PropertyChangedEventArgs(propertyName));
    }
  }
}
