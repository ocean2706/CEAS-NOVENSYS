// Decompiled with JetBrains decompiler
// Type: Novensys.ASN1.Type.Per.Time.DynAsn1SequenceTypeAdapter
// Assembly: Novensys.eCard.SDK, Version=1.1.56.0, Culture=neutral, PublicKeyToken=null
// MVID: 59F9E456-D3DA-4066-A4A4-692B516A775E
// Assembly location: C:\Program Files (x86)\CNAS\SIUI-SPITAL\Novensys.eCard.SDK.dll

using Novensys.ASN1.Type;

namespace Novensys.ASN1.Type.Per.Time
{
  public class DynAsn1SequenceTypeAdapter : Asn1SequenceTypeAdapter
  {
    private int addComponentIndex;
    private int[] componentIDList;
    private bool mixedSetting;
    private int[] optionalList;
    private int[] selectorList;

    public DynAsn1SequenceTypeAdapter(Asn1TimeType typeInstance, int nbRootComponents, int nbRootOptComponents)
      : base(typeInstance, nbRootComponents, nbRootOptComponents)
    {
      this.addComponentIndex = 0;
      this.mixedSetting = false;
    }

    public DynAsn1SequenceTypeAdapter(Asn1TimeType typeInstance, int nbRootComponents, int nbRootOptComponents, bool mixedSetting)
      : base(typeInstance, nbRootComponents, nbRootOptComponents)
    {
      this.addComponentIndex = 0;
      this.mixedSetting = false;
      this.mixedSetting = mixedSetting;
    }

    public void addComponentAdapter(int componentID)
    {
      this.addComponentAdapter(componentID, -1, -1);
    }

    public void addComponentAdapter(int componentID, int fieldID)
    {
      this.addComponentAdapter(componentID, fieldID, -1);
    }

    private void addComponentAdapter(int componentID, int fieldID, int testerID)
    {
      if (testerID != -1)
        this.__setComponentIsOptional(this.addComponentIndex);
      this.componentIDList[this.addComponentIndex] = componentID;
      this.selectorList[this.addComponentIndex] = fieldID;
      this.optionalList[this.addComponentIndex] = testerID;
      ++this.addComponentIndex;
    }

    public void addOptionalComponentAdapter(int componentID, int testerID)
    {
      this.addComponentAdapter(componentID, -1, testerID);
    }

    protected override Asn1Type createComponentByIndex(int index)
    {
      Asn1Type asn1Type = (Asn1Type) null;
      if (index < this.__getNbRootComponents())
      {
        int fieldID = this.selectorList[index];
        asn1Type = Asn1TimeTypeAdapterFactory.createAsn1TimeTypeAdapter(fieldID == -1 ? this.typeInstance : Asn1TimeTypeAdapterFactory.getAsn1TimeTypeComponent(this.typeInstance, fieldID), this.componentIDList[index], this.mixedSetting);
      }
      return asn1Type;
    }

    protected override void initializeAdapter_impl()
    {
      int nbRootComponents = this.__getNbRootComponents();
      this.componentIDList = new int[nbRootComponents];
      this.selectorList = new int[nbRootComponents];
      this.optionalList = new int[nbRootComponents];
    }

    protected override void postReadComponents()
    {
      for (int index = 0; index < this.__getNbRootComponents(); ++index)
      {
        if (this.IsComponentOptional(index) && !this.__isComponentDefined(index))
          Asn1TimeTypeAdapterFactory.asn1TimeTypeComponentNotDefined(this.typeInstance, this.optionalList[index]);
      }
    }

    protected override void preWriteComponents()
    {
      for (int index = 0; index < this.__getNbRootComponents(); ++index)
      {
        if (this.IsComponentOptional(index))
        {
          if (Asn1TimeTypeAdapterFactory.isAsn1TimeTypeComponentDefined(this.typeInstance, this.optionalList[index]))
            this.NewAsn1Type(index);
        }
        else
          this.NewAsn1Type(index);
      }
    }
  }
}
