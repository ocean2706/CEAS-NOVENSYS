// Decompiled with JetBrains decompiler
// Type: Novensys.eCard.SDK.ISO8583.MessageTypes
// Assembly: Novensys.eCard.SDK, Version=1.1.56.0, Culture=neutral, PublicKeyToken=null
// MVID: 59F9E456-D3DA-4066-A4A4-692B516A775E
// Assembly location: C:\Program Files (x86)\CNAS\SIUI-SPITAL\Novensys.eCard.SDK.dll

namespace Novensys.eCard.SDK.ISO8583
{
  public enum MessageTypes
  {
    MTI_0000_InvalidMessage = 0,
    MTI_0100_AuthRequest = 256,
    MTI_0110_AuthResponse = 272,
    MTI_0120_AuthAdvice = 288,
    MTI_0130_AuthAdviceResponse = 304,
    MTI_0200_TransactionRequest = 512,
    MTI_0201_TransactionRequestRepeat = 513,
    MTI_0202_TransactionCompletion = 514,
    MTI_0203TransactionCompletionRepeat = 515,
    MTI_0210_TransactionResponse = 528,
    MTI_0212_TransactionCompletionResponse = 530,
    MTI_0220_TransactionAdvice = 544,
    MTI_0221_TransactionAdviceRepeat = 545,
    MTI_0230_TransactionAdviceResponse = 560,
    MTI_0300_AcquirerFileUpdateRequest = 768,
    MTI_0310_AcquirerFileUpdateResponse = 784,
    MTI_0320_AcquirerFileUpdateAdvice = 800,
    MTI_0322_IssuerFileUpdateAdvice = 802,
    MTI_0330_AcquirerFileUpdateAdviceResponse = 816,
    MTI_0332_IssuerFileUpdateAdviceResponse = 818,
    MTI_0400_AcquirerReversalRequest = 1024,
    MTI_0410_AcquirerReversalRequestResponse = 1040,
    MTI_0420_AcquirerReversalAdvice = 1056,
    MTI_0421_AcquirerReversalAdviceRepeat = 1057,
    MTI_0430_AcquirerReversalAdviceResponse = 1072,
    MTI_0500_AcquirerReconRequest = 1280,
    MTI_0510_AcquirerReconRequestResponse = 1296,
    MTI_0520_AcquirerReconAdvice = 1312,
    MTI_0521_AcquirerReconAdviceRepeat = 1313,
    MTI_0530_AcquirerReconAdviceResponse = 1328,
    MTI_0600_AdministrativeRequest = 1536,
    MTI_0601_AdministrativeRequest = 1537,
    MTI_0610_AdministrativeRequestResponse = 1552,
    MTI_0800_NetworkManagementRequest = 2048,
    MTI_0801_NetworkManagementRequestRepeat = 2049,
    MTI_0810_NetworkManagementResponse = 2064,
  }
}
