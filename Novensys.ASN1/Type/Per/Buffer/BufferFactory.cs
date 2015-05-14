// Decompiled with JetBrains decompiler
// Type: Novensys.ASN1.Type.Per.Buffer.BufferFactory
// Assembly: Novensys.eCard.SDK, Version=1.1.56.0, Culture=neutral, PublicKeyToken=null
// MVID: 59F9E456-D3DA-4066-A4A4-692B516A775E
// Assembly location: C:\Program Files (x86)\CNAS\SIUI-SPITAL\Novensys.eCard.SDK.dll

namespace Novensys.ASN1.Type.Per.Buffer
{
  public class BufferFactory
  {
    public static AlphabetStringBuffer getAlphabetStringBuffer()
    {
      return new AlphabetStringBuffer();
    }

    public static BitFieldWrapper getBitFieldWrapper()
    {
      return new BitFieldWrapper();
    }

    public static BoundStringBuffer getBoundStringBuffer()
    {
      return new BoundStringBuffer();
    }

    public static DirectStringBuffer getDirectStringBuffer()
    {
      return new DirectStringBuffer();
    }

    public static IntegerBuffer getIntegerBuffer()
    {
      return new IntegerBuffer();
    }

    public static TypeVectorBuffer getTypeVectorBuffer()
    {
      return new TypeVectorBuffer();
    }

    public static void putBuffer(AbstractBuffer buffer)
    {
      buffer.reset();
    }
  }
}
