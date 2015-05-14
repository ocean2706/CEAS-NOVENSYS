// Decompiled with JetBrains decompiler
// Type: Novensys.eCard.SDK.ISO8583.AMessage
// Assembly: Novensys.eCard.SDK, Version=1.1.56.0, Culture=neutral, PublicKeyToken=null
// MVID: 59F9E456-D3DA-4066-A4A4-692B516A775E
// Assembly location: C:\Program Files (x86)\CNAS\SIUI-SPITAL\Novensys.eCard.SDK.dll

using System;
using System.Collections.Generic;
using System.Text;

namespace Novensys.eCard.SDK.ISO8583
{
  public abstract class AMessage : IMessage
  {
    private readonly Bitmap bitmap;
    private readonly Dictionary<int, IField> fields;

    public int PackedLength
    {
      get
      {
        int packedLength = this.bitmap.PackedLength;
        for (int index = 2; index <= 128; ++index)
        {
          if (this.bitmap[index])
            packedLength += this.fields[index].PackedLength;
        }
        return packedLength;
      }
    }

    public ProcessingCode ProcessingCode
    {
      get
      {
        return new ProcessingCode(this[3]);
      }
    }

    protected Template Template { get; private set; }

    public string this[int field]
    {
      get
      {
        return this.GetFieldValue(field);
      }
      set
      {
        this.SetFieldValue(field, value);
      }
    }

    protected AMessage(Template template)
    {
      this.Template = template;
      this.fields = new Dictionary<int, IField>();
      this.bitmap = new Bitmap(template.BitmapFormatter);
    }

    public void ClearField(int field)
    {
      this.bitmap[field] = false;
      this.fields.Remove(field);
    }

    public virtual string DescribePacking()
    {
      return this.Template.DescribePacking();
    }

    public bool IsFieldSet(int field)
    {
      return this.bitmap[field];
    }

    public virtual byte[] ToMsg()
    {
      byte[] numArray = new byte[this.PackedLength];
      int destinationIndex1 = 0;
      Array.Copy((Array) this.bitmap.ToMsg(), 0, (Array) numArray, destinationIndex1, this.bitmap.PackedLength);
      int destinationIndex2 = destinationIndex1 + this.bitmap.PackedLength;
      for (int index = 2; index <= 128; ++index)
      {
        if (this.bitmap[index])
        {
          IField field = this.fields[index];
          Array.Copy((Array) field.ToMsg(), 0, (Array) numArray, destinationIndex2, field.PackedLength);
          destinationIndex2 += field.PackedLength;
        }
      }
      return numArray;
    }

    public override string ToString()
    {
      return this.ToString(string.Empty);
    }

    public virtual string ToString(string prefix)
    {
      StringBuilder stringBuilder = new StringBuilder();
      for (int field = 2; field <= 128; ++field)
      {
        if (this.bitmap[field])
          stringBuilder.Append(this.ToString(field, prefix));
      }
      return ((object) stringBuilder).ToString();
    }

    public virtual string ToString(int field, string prefix)
    {
      if (this.fields.ContainsKey(field))
        return this.fields[field].ToString(prefix + "   ");
      else
        return string.Empty;
    }

    public virtual int Unpack(byte[] msg, int startingOffset)
    {
      int offset1 = startingOffset;
      int offset2 = this.bitmap.Unpack(msg, offset1);
      for (int field = 2; field <= 128; ++field)
      {
        if (this.bitmap[field])
          offset2 = this.GetField(field).Unpack(msg, offset2);
      }
      return offset2;
    }

    protected abstract IField CreateField(int field);

    protected IField GetField(int field)
    {
      if (!this.bitmap[field] || !this.fields.ContainsKey(field))
      {
        this.fields.Add(field, this.CreateField(field));
        this.bitmap[field] = true;
      }
      return this.fields[field];
    }

    protected string GetFieldValue(int field)
    {
      return this.bitmap[field] ? this.fields[field].Value : (string) null;
    }

    protected void SetFieldValue(int field, string value)
    {
      if (value == null)
        this.ClearField(field);
      else
        this.GetField(field).Value = value;
    }

    public static class AccountType
    {
      public const string _00_DEFAULT = "00";
      public const string _10_SAVINGS = "10";
      public const string _20_CHECK = "20";
      public const string _30_CREDIT = "30";
      public const string _40_UNIVERSAL = "40";
      public const string _50_INVESTMENT = "50";
      public const string _60_ELECTRONIC_PURSE_DEFAULT = "60";
    }

    public static class AmountType
    {
      public const string _01_LEDGER_BALANCE = "01";
      public const string _02_AVAILABLE_BALANCE = "02";
      public const string _03_OWING = "03";
      public const string _04_DUE = "04";
      public const string _20_REMAINING_THIS_CYCLE = "20";
      public const string _40_CASH = "40";
      public const string _41_GOODS_SERVICES = "41";
      public const string _53_APPROVED = "53";
      public const string _56_TIP = "56";
      public const string _90_AVAILABLE_CREDIT = "90";
      public const string _91_CREDIT_LIMIT = "91";
    }
  }
}
