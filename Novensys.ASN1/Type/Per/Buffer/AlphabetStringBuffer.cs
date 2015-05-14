// Decompiled with JetBrains decompiler
// Type: Novensys.ASN1.Type.Per.Buffer.AlphabetStringBuffer
// Assembly: Novensys.eCard.SDK, Version=1.1.56.0, Culture=neutral, PublicKeyToken=null
// MVID: 59F9E456-D3DA-4066-A4A4-692B516A775E
// Assembly location: C:\Program Files (x86)\CNAS\SIUI-SPITAL\Novensys.eCard.SDK.dll

using Novensys.ASN1;
using Novensys.ASN1.IO;
using Novensys.ASN1.Type;
using System;
using System.Text;

namespace Novensys.ASN1.Type.Per.Buffer
{
  public class AlphabetStringBuffer : KnownMultiplierStringBuffer
  {
    private StringBuilder _valueHelper = new StringBuilder();
    private IntegerBuffer buffer = new IntegerBuffer();
    private string _alphabet;
    private string _value;

    public override void read(int start, int length, IAsn1BitInputStream input)
    {
      for (int index1 = 0; index1 < length; ++index1)
      {
        this.buffer.setGranularity(1);
        this.buffer.read(0, this._nbBitsPerChar, input);
        int index2 = (int) this.buffer.getLong();
        try
        {
          this._valueHelper.Append(this._alphabet[index2]);
        }
        catch (IndexOutOfRangeException ex)
        {
          throw new Asn1Exception(32, "for type <" + this._typeInstance.GetType().FullName + ">");
        }
        this.buffer.reset();
      }
    }

    protected internal override void reset()
    {
      base.reset();
      this._alphabet = (string) null;
      this._valueHelper.Length = 0;
      this._value = (string) null;
      this.buffer.reset();
    }

    public virtual void setParameters(Asn1KnownMultiplierStringType typeInstance, int nbBitsPerChar, string alphabet)
    {
      base.setParameters(typeInstance, nbBitsPerChar);
      this._alphabet = alphabet;
      this._value = typeInstance.StringValue;
      if (this._value == null)
        this._length = 0;
      else
        this._length = this._value.Length;
    }

    public override void updateTypeInstance()
    {
      this._typeInstance.SetValue(((object) this._valueHelper).ToString());
    }

    public override void write(int start, int length, IAsn1BitOutputStream output)
    {
      for (int index = 0; index < length; ++index)
      {
        long longValue = (long) this._alphabet.IndexOf(this._value[start + index]);
        if (longValue == -1L)
        {
          throw new Asn1Exception(31, "for character '" + (object) this._value[start + index] + "' in type <" + this._typeInstance.GetType().FullName + ">");
        }
        else
        {
          this.buffer.setLong(longValue);
          this.buffer.setLength(1, this._nbBitsPerChar);
          this.buffer.write(0, this._nbBitsPerChar, output);
          this.buffer.reset();
        }
      }
    }
  }
}
