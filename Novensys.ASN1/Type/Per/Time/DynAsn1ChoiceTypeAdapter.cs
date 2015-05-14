// Decompiled with JetBrains decompiler
// Type: Novensys.ASN1.Type.Per.Time.DynAsn1ChoiceTypeAdapter
// Assembly: Novensys.eCard.SDK, Version=1.1.56.0, Culture=neutral, PublicKeyToken=null
// MVID: 59F9E456-D3DA-4066-A4A4-692B516A775E
// Assembly location: C:\Program Files (x86)\CNAS\SIUI-SPITAL\Novensys.eCard.SDK.dll

using Novensys.ASN1.Type;
using System;

namespace Novensys.ASN1.Type.Per.Time
{
  public class DynAsn1ChoiceTypeAdapter : Asn1ChoiceTypeAdapter
  {
    private int choiceID;
    private int[] componentIDList;
    private bool mixedSetting;

    public DynAsn1ChoiceTypeAdapter(Asn1TimeType typeInstance, int[] componentIDList, int choiceID)
      : base(typeInstance, componentIDList.Length)
    {
      this.mixedSetting = false;
      this.componentIDList = componentIDList;
      this.choiceID = choiceID;
    }

    public DynAsn1ChoiceTypeAdapter(Asn1TimeType typeInstance, int[] componentIDList, int choiceID, bool mixedSetting)
      : base(typeInstance, componentIDList.Length)
    {
      this.mixedSetting = false;
      this.componentIDList = componentIDList;
      this.choiceID = choiceID;
      this.mixedSetting = mixedSetting;
    }

    protected override Asn1Type createComponentByIndex(int index)
    {
      Asn1Type asn1Type = (Asn1Type) null;
      if (index < this.__getNbRootComponents())
        asn1Type = Asn1TimeTypeAdapterFactory.createAsn1TimeTypeAdapter(Asn1TimeTypeAdapterFactory.getAsn1TimeTypeComponent(this.typeInstance, this.choiceID, this.componentIDList[index]), this.componentIDList[index], this.mixedSetting);
      return asn1Type;
    }

    private int findComponentIndexFromType(int componentID)
    {
      int index = 0;
      while (index < this.__getNbRootComponents() && this.componentIDList[index] != componentID)
        ++index;
      if (index == this.__getNbRootComponents())
        throw new ArgumentException("PER Time Type : can not find choice component index - bad component ID argument");
      else
        return index;
    }

    protected override void preWriteComponents()
    {
      this.NewAsn1Type(this.findComponentIndexFromType(Asn1TimeTypeAdapterFactory.chooseComponentAdapter(this.typeInstance, this.choiceID)));
    }
  }
}
