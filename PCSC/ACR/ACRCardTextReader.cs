// Decompiled with JetBrains decompiler
// Type: Novensys.eCard.SDK.PCSC.ACR.ACRCardTextReader
// Assembly: Novensys.eCard.SDK, Version=1.1.56.0, Culture=neutral, PublicKeyToken=null
// MVID: 59F9E456-D3DA-4066-A4A4-692B516A775E
// Assembly location: C:\Program Files (x86)\CNAS\SIUI-SPITAL\Novensys.eCard.SDK.dll

using Novensys.eCard.SDK.PCSC;
using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Novensys.eCard.SDK.PCSC.ACR
{
  internal class ACRCardTextReader : IWinSCardTextReader, IDisposable
  {
    private string traceName = "ACRCardTextReader";
    private bool traceEnabled = true;
    private byte[] sendBuff = new byte[263];
    private byte[] recvBuff = new byte[263];
    private object syncRoot = new object();
    private const uint FILE_DEVICE_SMARTCARD = 3211264U;
    private const uint IOCTL_SMARTCARD_ENABLE_PIN_VERIFICATION = 3219564U;
    private const uint IOCTL_SMARTCARD_ENABLE_PIN_MODIFICATION = 3219568U;
    private const uint IOCTL_SMARTCARD_DISABLE_SECURE_PIN_ENTRY = 3219572U;
    private const uint IOCTL_SMARTCARD_GET_FIRMWARE_VERSION = 3219576U;
    private const uint IOCTL_SMARTCARD_DISPLAY_LCD_MESSAGE = 3219580U;
    private const uint IOCTL_SMARTCARD_READ_KEY = 3219584U;
    private const uint CM_IOCTL_GET_FEATURE_REQUEST = 3224864U;
    private bool disposed;

    public IntPtr PHContext { get; set; }

    public IntPtr PHCard { get; set; }

    public string ReaderName { get; set; }

    ~ACRCardTextReader()
    {
      this.Dispose(false);
    }

    public string ReadText(string prompt, bool password)
    {
      lock (this.syncRoot)
      {
        try
        {
          this.WriteText(prompt);
          return this.ReadText(password);
        }
        finally
        {
          this.ClearScreen();
        }
      }
    }

    public void Dispose()
    {
      this.Dispose(true);
      GC.SuppressFinalize((object) this);
    }

    protected virtual void Dispose(bool disposing)
    {
      if (this.disposed)
        return;
      if (!disposing)
        ;
      this.disposed = true;
    }

    private void ClearScreen()
    {
      this.WriteText(new string(' ', 32));
    }

    private void WriteText(string text)
    {
      if (string.IsNullOrEmpty(text))
        return;
      if (text.Length > 32)
        text = text.Substring(0, 32);
      byte[] bytes = Encoding.ASCII.GetBytes(text);
      this.ClearBuffers();
      int pcbBytesReturned = 0;
      if (ACRCardTextReader.SCardControl(this.PHCard, 3219580U, bytes, Math.Min(bytes.Length, 32), this.recvBuff, 32, ref pcbBytesReturned) != 0)
        ;
    }

    private string ReadText(bool password)
    {
      READ_KEY_OPTION readKeyOption = new READ_KEY_OPTION();
      readKeyOption.bEchoLCDMode = password ? (byte) 1 : (byte) 0;
      readKeyOption.bKeyReturnCondition = (byte) 0;
      readKeyOption.bKeyReturnCondition |= Convert.ToByte((object) KeyReturnCondition.ACR83_KeyEPress);
      readKeyOption.bKeyReturnCondition |= Convert.ToByte((object) KeyReturnCondition.ACR83_KeyCPress);
      readKeyOption.bKeyReturnCondition |= Convert.ToByte((object) KeyReturnCondition.ACR83_TimeOut);
      readKeyOption.bTimeOut = (byte) 60;
      readKeyOption.wPINMinsize = (byte) 0;
      readKeyOption.wPINMaxsize = (byte) 16;
      readKeyOption.bEchoLCDStartPosition = (byte) 16;
      this.ClearBuffers();
      this.sendBuff[0] = readKeyOption.bTimeOut;
      this.sendBuff[1] = readKeyOption.wPINMaxsize;
      this.sendBuff[2] = readKeyOption.wPINMinsize;
      this.sendBuff[3] = readKeyOption.bKeyReturnCondition;
      this.sendBuff[4] = readKeyOption.bEchoLCDStartPosition;
      this.sendBuff[5] = readKeyOption.bEchoLCDMode;
      int SendBuffLen = 6;
      int RecvBuffLen = 3 + (int) readKeyOption.wPINMaxsize;
      int pcbBytesReturned = 0;
      int num = ACRCardTextReader.SCardControl(this.PHCard, 3219584U, this.sendBuff, SendBuffLen, this.recvBuff, RecvBuffLen, ref pcbBytesReturned);
      if (num != 0)
        throw new WinSCardException(true, "ReadText", (uint) num);
      if ((int) this.recvBuff[0] != 0 || (int) this.recvBuff[1] != 0)
        throw new Exception("Terminalul nu a intors textul introdus de utilizator");
      switch ((char) this.recvBuff[2])
      {
        case '3':
          throw new WinSCardException(true, "ReadText", 2148532234U);
        case '4':
          throw new WinSCardException(true, "ReadText", 2148532334U);
        default:
          StringBuilder stringBuilder = new StringBuilder();
          for (int index = 3; index < RecvBuffLen && (int) this.recvBuff[index] != 0; ++index)
            stringBuilder.Append(Convert.ToChar(this.recvBuff[index]));
          return ((object) stringBuilder).ToString();
      }
    }

    private void ClearBuffers()
    {
      Array.Clear((Array) this.sendBuff, 0, this.sendBuff.Length);
      Array.Clear((Array) this.recvBuff, 0, this.recvBuff.Length);
    }

    [DllImport("winscard.dll")]
    private static extern int SCardControl(IntPtr hCard, uint dwControlCode, byte[] SendBuff, int SendBuffLen, byte[] RecvBuff, int RecvBuffLen, ref int pcbBytesReturned);
  }
}
