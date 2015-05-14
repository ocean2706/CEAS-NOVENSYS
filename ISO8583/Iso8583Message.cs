// Decompiled with JetBrains decompiler
// Type: Novensys.eCard.SDK.ISO8583.Iso8583Message
// Assembly: Novensys.eCard.SDK, Version=1.1.56.0, Culture=neutral, PublicKeyToken=null
// MVID: 59F9E456-D3DA-4066-A4A4-692B516A775E
// Assembly location: C:\Program Files (x86)\CNAS\SIUI-SPITAL\Novensys.eCard.SDK.dll

using Novensys.eCard.SDK.ISO8583.Exceptions;
using Novensys.eCard.SDK.ISO8583.Formatter;
using System;
using System.Text;

namespace Novensys.eCard.SDK.ISO8583
{
  public class Iso8583Message : AMessage
  {
    private static readonly Template DefaultTemplate;

    public int MessageType { get; set; }

    public new int PackedLength
    {
      get
      {
        return base.PackedLength + this.Template.MsgTypeFormatter.GetBytes(this.MessageType.ToString().PadLeft(4, '0')).Length;
      }
    }

    public TransmissionDateTime TransmissionDateTime
    {
      get
      {
        return new TransmissionDateTime((AMessage) this);
      }
    }

    static Iso8583Message()
    {
      Template template = new Template();
      template.Add(2, FieldDescriptor.PanMask(FieldDescriptor.AsciiLlNumeric(19)));
      template.Add(3, FieldDescriptor.AsciiNumeric(6));
      template.Add(4, FieldDescriptor.AsciiNumeric(12));
      template.Add(5, FieldDescriptor.AsciiNumeric(12));
      template.Add(6, FieldDescriptor.AsciiNumeric(12));
      template.Add(7, FieldDescriptor.AsciiNumeric(10));
      template.Add(8, FieldDescriptor.AsciiNumeric(8));
      template.Add(9, FieldDescriptor.AsciiNumeric(8));
      template.Add(10, FieldDescriptor.AsciiNumeric(8));
      template.Add(11, FieldDescriptor.AsciiNumeric(6));
      template.Add(12, FieldDescriptor.AsciiNumeric(6));
      template.Add(13, FieldDescriptor.AsciiNumeric(4));
      template.Add(14, FieldDescriptor.AsciiNumeric(4));
      template.Add(15, FieldDescriptor.AsciiNumeric(4));
      template.Add(16, FieldDescriptor.AsciiNumeric(4));
      template.Add(17, FieldDescriptor.AsciiNumeric(4));
      template.Add(18, FieldDescriptor.AsciiNumeric(4));
      template.Add(19, FieldDescriptor.AsciiNumeric(3));
      template.Add(20, FieldDescriptor.AsciiNumeric(3));
      template.Add(21, FieldDescriptor.AsciiNumeric(3));
      template.Add(22, FieldDescriptor.AsciiNumeric(3));
      template.Add(23, FieldDescriptor.AsciiNumeric(3));
      template.Add(24, FieldDescriptor.AsciiNumeric(3));
      template.Add(25, FieldDescriptor.AsciiNumeric(2));
      template.Add(26, FieldDescriptor.AsciiNumeric(2));
      template.Add(27, FieldDescriptor.AsciiNumeric(1));
      template.Add(28, FieldDescriptor.AsciiAmount(9));
      template.Add(29, FieldDescriptor.AsciiAmount(9));
      template.Add(30, FieldDescriptor.AsciiAmount(9));
      template.Add(31, FieldDescriptor.AsciiAmount(9));
      template.Add(32, FieldDescriptor.AsciiLlNumeric(11));
      template.Add(33, FieldDescriptor.AsciiLlNumeric(11));
      template.Add(34, FieldDescriptor.AsciiLlCharacter(28));
      template.Add(35, FieldDescriptor.AsciiLlNumeric(37));
      template.Add(36, FieldDescriptor.AsciiLllCharacter(104));
      template.Add(37, FieldDescriptor.AsciiAlphaNumeric(12));
      template.Add(38, FieldDescriptor.AsciiAlphaNumeric(6));
      template.Add(39, FieldDescriptor.AsciiAlphaNumeric(2));
      template.Add(40, FieldDescriptor.AsciiAlphaNumeric(3));
      template.Add(41, FieldDescriptor.AsciiAlphaNumeric(8));
      template.Add(42, FieldDescriptor.AsciiAlphaNumeric(15));
      template.Add(43, FieldDescriptor.AsciiAlphaNumeric(40));
      template.Add(44, FieldDescriptor.AsciiLlCharacter(25));
      template.Add(45, FieldDescriptor.AsciiLlCharacter(76));
      template.Add(46, FieldDescriptor.AsciiLllCharacter(999));
      template.Add(47, FieldDescriptor.AsciiLllCharacter(999));
      template.Add(48, FieldDescriptor.AsciiLllCharacter(999));
      template.Add(49, FieldDescriptor.AsciiAlphaNumeric(3));
      template.Add(50, FieldDescriptor.AsciiAlphaNumeric(3));
      template.Add(51, FieldDescriptor.AsciiAlphaNumeric(3));
      template.Add(52, FieldDescriptor.AsciiAlphaNumeric(16));
      template.Add(53, FieldDescriptor.AsciiNumeric(16));
      template.Add(54, FieldDescriptor.AsciiLllCharacter(120));
      template.Add(55, FieldDescriptor.AsciiLllCharacter(999));
      template.Add(56, FieldDescriptor.AsciiLllCharacter(999));
      template.Add(57, FieldDescriptor.AsciiLllCharacter(999));
      template.Add(58, FieldDescriptor.AsciiLllCharacter(999));
      template.Add(59, FieldDescriptor.AsciiLllCharacter(999));
      template.Add(60, FieldDescriptor.AsciiLllCharacter(999));
      template.Add(61, FieldDescriptor.AsciiLllCharacter(999));
      template.Add(62, FieldDescriptor.AsciiLllCharacter(999));
      template.Add(63, FieldDescriptor.AsciiLllCharacter(999));
      template.Add(64, FieldDescriptor.BinaryFixed(8));
      template.Add(65, FieldDescriptor.AsciiAlphaNumeric(16));
      template.Add(66, FieldDescriptor.AsciiNumeric(1));
      template.Add(67, FieldDescriptor.AsciiNumeric(2));
      template.Add(68, FieldDescriptor.AsciiNumeric(3));
      template.Add(69, FieldDescriptor.AsciiNumeric(3));
      template.Add(70, FieldDescriptor.AsciiNumeric(3));
      template.Add(71, FieldDescriptor.AsciiNumeric(4));
      template.Add(72, FieldDescriptor.AsciiNumeric(4));
      template.Add(73, FieldDescriptor.AsciiNumeric(6));
      template.Add(74, FieldDescriptor.AsciiNumeric(10));
      template.Add(75, FieldDescriptor.AsciiNumeric(10));
      template.Add(76, FieldDescriptor.AsciiNumeric(10));
      template.Add(77, FieldDescriptor.AsciiNumeric(10));
      template.Add(78, FieldDescriptor.AsciiNumeric(10));
      template.Add(79, FieldDescriptor.AsciiNumeric(10));
      template.Add(80, FieldDescriptor.AsciiNumeric(10));
      template.Add(81, FieldDescriptor.AsciiNumeric(10));
      template.Add(82, FieldDescriptor.AsciiNumeric(12));
      template.Add(83, FieldDescriptor.AsciiNumeric(12));
      template.Add(84, FieldDescriptor.AsciiNumeric(12));
      template.Add(85, FieldDescriptor.AsciiNumeric(12));
      template.Add(86, FieldDescriptor.AsciiNumeric(15));
      template.Add(87, FieldDescriptor.AsciiNumeric(15));
      template.Add(88, FieldDescriptor.AsciiNumeric(15));
      template.Add(89, FieldDescriptor.AsciiNumeric(15));
      template.Add(90, FieldDescriptor.AsciiNumeric(42));
      template.Add(91, FieldDescriptor.AsciiAlphaNumeric(1));
      template.Add(92, FieldDescriptor.AsciiAlphaNumeric(2));
      template.Add(93, FieldDescriptor.AsciiAlphaNumeric(5));
      template.Add(94, FieldDescriptor.AsciiAlphaNumeric(7));
      template.Add(95, FieldDescriptor.AsciiAlphaNumeric(42));
      template.Add(96, FieldDescriptor.BinaryFixed(8));
      template.Add(97, FieldDescriptor.AsciiAmount(16));
      template.Add(98, FieldDescriptor.AsciiAlphaNumeric(25));
      template.Add(99, FieldDescriptor.AsciiLlNumeric(11));
      template.Add(100, FieldDescriptor.AsciiLlNumeric(11));
      template.Add(101, FieldDescriptor.AsciiLlCharacter(17));
      template.Add(102, FieldDescriptor.AsciiLlCharacter(28));
      template.Add(103, FieldDescriptor.AsciiLlCharacter(28));
      template.Add(104, FieldDescriptor.AsciiLllCharacter(100));
      template.Add(105, FieldDescriptor.AsciiLllCharacter(999));
      template.Add(106, FieldDescriptor.AsciiLllCharacter(999));
      template.Add(107, FieldDescriptor.AsciiLllCharacter(999));
      template.Add(108, FieldDescriptor.AsciiLllCharacter(999));
      template.Add(109, FieldDescriptor.AsciiLllCharacter(999));
      template.Add(110, FieldDescriptor.AsciiLllCharacter(999));
      template.Add(111, FieldDescriptor.AsciiLllCharacter(999));
      template.Add(112, FieldDescriptor.AsciiLllCharacter(999));
      template.Add(113, FieldDescriptor.AsciiLlNumeric(11));
      template.Add(114, FieldDescriptor.AsciiLllCharacter(999));
      template.Add(115, FieldDescriptor.AsciiLllCharacter(999));
      template.Add(116, FieldDescriptor.AsciiLllCharacter(999));
      template.Add(117, FieldDescriptor.AsciiLllCharacter(999));
      template.Add(118, FieldDescriptor.AsciiLllCharacter(999));
      template.Add(119, FieldDescriptor.AsciiLllCharacter(999));
      template.Add(120, FieldDescriptor.AsciiLllCharacter(999));
      template.Add(121, FieldDescriptor.AsciiLllCharacter(999));
      template.Add(122, FieldDescriptor.AsciiLllCharacter(999));
      template.Add(123, FieldDescriptor.AsciiLllCharacter(999));
      template.Add(124, FieldDescriptor.AsciiLllCharacter((int) byte.MaxValue));
      template.Add(125, FieldDescriptor.AsciiLllCharacter(50));
      template.Add(126, FieldDescriptor.AsciiLllCharacter(999));
      template.Add((int) sbyte.MaxValue, FieldDescriptor.AsciiLllCharacter(999));
      template.Add(128, FieldDescriptor.BinaryFixed(16));
      Iso8583Message.DefaultTemplate = template;
      Iso8583Message.DefaultTemplate.BitmapFormatter = Formatters.Ascii;
      Iso8583Message.DefaultTemplate.MsgTypeFormatter = Formatters.Ascii;
    }

    public Iso8583Message()
      : this(Iso8583Message.DefaultTemplate)
    {
    }

    public Iso8583Message(Template template)
      : base(template)
    {
      this.MessageType = 0;
    }

    public override byte[] ToMsg()
    {
      byte[] numArray1 = new byte[this.PackedLength];
      byte[] bytes = this.Template.MsgTypeFormatter.GetBytes(IsoConvert.FromIntToMsgType(this.MessageType));
      int length = bytes.Length;
      Array.Copy((Array) bytes, 0, (Array) numArray1, 0, bytes.Length);
      byte[] numArray2 = base.ToMsg();
      Array.Copy((Array) numArray2, 0, (Array) numArray1, length, numArray2.Length);
      return numArray1;
    }

    public override string ToString(string prefix)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("{" + prefix + IsoConvert.FromIntToMsgType(this.MessageType) + "}");
      stringBuilder.Append(base.ToString(prefix));
      return ((object) stringBuilder).ToString();
    }

    public override int Unpack(byte[] msg, int startingOffset)
    {
      int packedLength = this.Template.MsgTypeFormatter.GetPackedLength(4);
      byte[] data = new byte[packedLength];
      int sourceIndex = startingOffset;
      Array.Copy((Array) msg, sourceIndex, (Array) data, 0, packedLength);
      this.MessageType = IsoConvert.FromMsgTypeToInt(this.Template.MsgTypeFormatter.GetString(data));
      int startingOffset1 = sourceIndex + packedLength;
      return base.Unpack(msg, startingOffset1);
    }

    protected override IField CreateField(int field)
    {
      if (this.Template.ContainsKey(field))
        return (IField) new Field(field, this.Template[field]);
      else
        throw new UnknownFieldException(field.ToString());
    }

    public class MsgType
    {
      public static int GetResponse(int msgType)
      {
        return msgType - msgType % 2 + 16;
      }
    }
  }
}
