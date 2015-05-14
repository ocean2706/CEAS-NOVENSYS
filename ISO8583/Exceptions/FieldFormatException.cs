// Decompiled with JetBrains decompiler
// Type: Novensys.eCard.SDK.ISO8583.Exceptions.FieldFormatException
// Assembly: Novensys.eCard.SDK, Version=1.1.56.0, Culture=neutral, PublicKeyToken=null
// MVID: 59F9E456-D3DA-4066-A4A4-692B516A775E
// Assembly location: C:\Program Files (x86)\CNAS\SIUI-SPITAL\Novensys.eCard.SDK.dll

using System;

namespace Novensys.eCard.SDK.ISO8583.Exceptions
{
  public class FieldFormatException : FormatException
  {
    public int FieldNumber { get; private set; }

    public FieldFormatException(int fieldNumber, string message)
      : base(string.Concat(new object[4]
      {
        (object) "Field Number : ",
        (object) fieldNumber,
        (object) Environment.NewLine,
        (object) message
      }))
    {
      this.FieldNumber = fieldNumber;
    }
  }
}
