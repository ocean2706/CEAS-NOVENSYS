// Decompiled with JetBrains decompiler
// Type: Novensys.eCard.SDK.Remoting.ObtineTokenEventHandler
// Assembly: Novensys.eCard.SDK, Version=1.1.56.0, Culture=neutral, PublicKeyToken=null
// MVID: 59F9E456-D3DA-4066-A4A4-692B516A775E
// Assembly location: C:\Program Files (x86)\CNAS\SIUI-SPITAL\Novensys.eCard.SDK.dll

using Novensys.eCard.SDK;
using Novensys.eCard.SDK.Entities;
using System;

namespace Novensys.eCard.SDK.Remoting
{
  public delegate string ObtineTokenEventHandler(string cif, IdentificatorDrepturi identificatorDrepturi, string terminalId, ref DateTime serverDateTime, ref CoduriRaspunsOperatieCard raspunsOperatie);
}
