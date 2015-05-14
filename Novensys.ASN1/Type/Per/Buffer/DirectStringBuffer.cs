// Decompiled with JetBrains decompiler
// Type: Novensys.ASN1.Type.Per.Buffer.DirectStringBuffer
// Assembly: Novensys.eCard.SDK, Version=1.1.56.0, Culture=neutral, PublicKeyToken=null
// MVID: 59F9E456-D3DA-4066-A4A4-692B516A775E
// Assembly location: C:\Program Files (x86)\CNAS\SIUI-SPITAL\Novensys.eCard.SDK.dll

using Novensys.ASN1;
using Novensys.ASN1.IO;
using Novensys.ASN1.Type;
using System;
using System.IO;

namespace Novensys.ASN1.Type.Per.Buffer
{
  public class DirectStringBuffer : KnownMultiplierStringBuffer
  {
    private byte[] _data;

    private void ensureAddedByteCapacity(int addedCapacity)
    {
      if (this._data == null)
        this._data = new byte[0];
      int length = this._data.Length;
      if (this._length + addedCapacity <= length)
        return;
      byte[] numArray = this._data;
      this._data = new byte[length + addedCapacity];
      Array.Copy((Array) numArray, 0, (Array) this._data, 0, this._length);
    }

    public override void read(int start, int length, IAsn1BitInputStream input)
    {
      if (length <= 0)
        return;
      this.ensureAddedByteCapacity(length * this._nbBytesPerChar);
      for (int index1 = 0; index1 < length; ++index1)
      {
        int index2 = this._length + index1 * this._nbBytesPerChar;
        this._data[index2] = input.read(this._firstBitIndex, 8 - this._firstBitIndex);
        for (int index3 = 1; index3 < this._nbBytesPerChar; ++index3)
          this._data[index2 + index3] = input.read(0, 8);
      }
      this._length += length * this._nbBytesPerChar;
    }

    protected internal override void reset()
    {
      base.reset();
      this._data = (byte[]) null;
    }

    public override void setParameters(Asn1KnownMultiplierStringType typeInstance, int nbBitsPerChar)
    {
      base.setParameters(typeInstance, nbBitsPerChar);
      try
      {
        this._data = typeInstance.GetByteArrayValue();
        string stringValue = typeInstance.GetStringValue();
        if (stringValue == null)
          this._length = 0;
        else
          this._length = stringValue.Length;
      }
      catch (Exception ex)
      {
        throw new Asn1Exception(21, "exception " + ex.Message + " for type <" + typeInstance.GetType().FullName + ">");
      }
    }

    public override void updateTypeInstance()
    {
      try
      {
        this._typeInstance.SetValue(this._data);
      }
      catch (IOException ex)
      {
        throw new Asn1Exception(21, "exception " + ex.Message + " for type <" + this._typeInstance.GetType().FullName + ">");
      }
    }

    public override void write(int start, int length, IAsn1BitOutputStream output)
    {
      for (int index1 = 0; index1 < length; ++index1)
      {
        int index2 = (start + index1) * this._nbBytesPerChar;
        output.write(this._data[index2], this._firstBitIndex, 8 - this._firstBitIndex);
        for (int index3 = 1; index3 < this._nbBytesPerChar; ++index3)
          output.write(this._data[index2 + index3], 0, 8);
      }
    }
  }
}
