// Decompiled with JetBrains decompiler
// Type: Novensys.eCard.SDK.ISO8583.Formatter.Formatters
// Assembly: Novensys.eCard.SDK, Version=1.1.56.0, Culture=neutral, PublicKeyToken=null
// MVID: 59F9E456-D3DA-4066-A4A4-692B516A775E
// Assembly location: C:\Program Files (x86)\CNAS\SIUI-SPITAL\Novensys.eCard.SDK.dll

namespace Novensys.eCard.SDK.ISO8583.Formatter
{
  public static class Formatters
  {
    public static IFormatter Ascii
    {
      get
      {
        return (IFormatter) new AsciiFormatter();
      }
    }

    public static IFormatter Bcd
    {
      get
      {
        return (IFormatter) new BcdFormatter();
      }
    }

    public static IFormatter Binary
    {
      get
      {
        return (IFormatter) new BinaryFormatter();
      }
    }
  }
}
