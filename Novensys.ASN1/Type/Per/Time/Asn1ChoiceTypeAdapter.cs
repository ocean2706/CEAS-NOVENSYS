// Decompiled with JetBrains decompiler
// Type: Novensys.ASN1.Type.Per.Time.Asn1ChoiceTypeAdapter
// Assembly: Novensys.eCard.SDK, Version=1.1.56.0, Culture=neutral, PublicKeyToken=null
// MVID: 59F9E456-D3DA-4066-A4A4-692B516A775E
// Assembly location: C:\Program Files (x86)\CNAS\SIUI-SPITAL\Novensys.eCard.SDK.dll

using Novensys.ASN1;
using Novensys.ASN1.Type;
using System;

namespace Novensys.ASN1.Type.Per.Time
{
  public abstract class Asn1ChoiceTypeAdapter : Asn1ChoiceType
  {
    private int nbRootComponents = 0;
    protected Asn1TimeType typeInstance = (Asn1TimeType) null;

    public Asn1ChoiceTypeAdapter(Asn1TimeType typeInstance, int nbRootComponents)
    {
      this.typeInstance = typeInstance;
      this.nbRootComponents = nbRootComponents;
      this.initializeAdapter();
    }

    protected internal override int __getNbRootComponents()
    {
      return this.nbRootComponents;
    }

    protected internal override Asn1Type __instantiateTypeByIndex(int index)
    {
      Asn1Type typeInstance = this.GetAsn1Type(index);
      if (typeInstance == null)
      {
        typeInstance = this.createComponentByIndex(index);
        this.__setComponentTypeInstance(index, typeInstance);
      }
      return typeInstance;
    }

    protected internal override void __read(Asn1TypePerReader reader)
    {
      base.__read(reader);
      try
      {
        this.postReadComponents();
      }
      catch (InvalidCastException ex)
      {
        throw new Asn1Exception(64, ex.Message + " for type <" + this.typeInstance.GetType().FullName + ">");
      }
    }

    protected internal override void __write(Asn1TypePerWriter writer)
    {
      this.preWriteComponents();
      base.__write(writer);
    }

    protected abstract Asn1Type createComponentByIndex(int index);

    private void initializeAdapter()
    {
      this.__initializeComponents(this.nbRootComponents);
    }

    protected virtual void postReadComponents()
    {
    }

    protected virtual void preWriteComponents()
    {
    }
  }
}
