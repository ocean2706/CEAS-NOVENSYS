// Decompiled with JetBrains decompiler
// Type: Novensys.eCard.SDK.ISO8583.LambdaAdjuster
// Assembly: Novensys.eCard.SDK, Version=1.1.56.0, Culture=neutral, PublicKeyToken=null
// MVID: 59F9E456-D3DA-4066-A4A4-692B516A775E
// Assembly location: C:\Program Files (x86)\CNAS\SIUI-SPITAL\Novensys.eCard.SDK.dll

namespace Novensys.eCard.SDK.ISO8583
{
  public class LambdaAdjuster : Adjuster
  {
    private readonly FuncStringString _getLambda;
    private readonly FuncStringString _setLambda;

    public LambdaAdjuster(FuncStringString getLambda = null, FuncStringString setLambda = null)
    {
      this._getLambda = getLambda;
      this._setLambda = setLambda;
    }

    public override string Get(string value)
    {
      return this._getLambda == null ? value : this._getLambda(value);
    }

    public override string Set(string value)
    {
      return this._setLambda == null ? value : this._setLambda(value);
    }
  }
}
