// Decompiled with JetBrains decompiler
// Type: Novensys.eCard.SDK.PCSC.WinSCardCardJob
// Assembly: Novensys.eCard.SDK, Version=1.1.56.0, Culture=neutral, PublicKeyToken=null
// MVID: 59F9E456-D3DA-4066-A4A4-692B516A775E
// Assembly location: C:\Program Files (x86)\CNAS\SIUI-SPITAL\Novensys.eCard.SDK.dll

using Novensys.eCard.SDK.PCSC.Apdu;
using Novensys.eCard.SDK.Utils.Hex;
using Novensys.eCard.SDK.Utils.Log;
using System;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

namespace Novensys.eCard.SDK.PCSC
{
  internal class WinSCardCardJob : IApduExchange, ICardTextReader
  {
    private bool traceEnabled = true;
    private TimeSpan startDelay = new TimeSpan(0, 0, 10);
    private TimeSpan shortDelay = new TimeSpan(0, 0, 1);
    private TimeSpan longDelay = new TimeSpan(0, 2, 0);
    private ManualResetEvent stopHandle = new ManualResetEvent(false);
    private ManualResetEvent startHandle = new ManualResetEvent(false);
    private object syncRoot = new object();
    private IntPtr phContext = IntPtr.Zero;
    private IntPtr phCard = IntPtr.Zero;
    private string traceName;
    private Thread thread;
    private uint activeProtocol;
    private WinSCardReaderInfo readerInfo;
    private IWinSCardTextReader cardTextReader;

    public bool ReadTextSupported
    {
      get
      {
        return this.readerInfo.CardTextReaderType != null;
      }
    }

    public bool CardPresent
    {
      get
      {
        return this.phCard != IntPtr.Zero;
      }
    }

    public bool Faulted { get; private set; }

    public WinSCardReaderInfo ReaderInfo
    {
      get
      {
        return this.readerInfo;
      }
    }

    public event EventHandler<CardEventArgs> CardInserted;

    public event EventHandler<CardEventArgs> CardRemoved;

    internal WinSCardCardJob(IntPtr phContext, WinSCardReaderInfo readerInfo)
    {
      this.phContext = phContext;
      this.readerInfo = readerInfo;
      this.traceName = string.Format((IFormatProvider) CultureInfo.InvariantCulture, "WinSCardCardJob - {0}", new object[1]
      {
        (object) this.readerInfo.FullName
      });
    }

    public void Start()
    {
      lock (this.syncRoot)
      {
        if (this.thread != null)
          return;
        this.stopHandle.Reset();
        this.thread = new Thread(new ThreadStart(this.Job));
        this.thread.Name = this.traceName;
        this.thread.IsBackground = true;
        this.thread.Start();
        this.startHandle.WaitOne(this.startDelay);
      }
    }

    public void Stop()
    {
      lock (this.syncRoot)
      {
        if (this.thread == null)
          return;
        this.stopHandle.Set();
        this.thread.Join();
        this.thread = (Thread) null;
      }
    }

    private void Job()
    {
      do
      {
        try
        {
          if (this.Faulted)
            this.stopHandle.WaitOne(this.longDelay);
          else
            this.CreateCard();
        }
        catch (ThreadAbortException ex)
        {
        }
        catch (DllNotFoundException ex)
        {
          LogManager.FileLog((Exception) ex);
          this.Faulted = true;
        }
        catch (Exception ex)
        {
          if (this.stopHandle.WaitOne(this.longDelay))
            break;
        }
      }
      while (!this.stopHandle.WaitOne(this.shortDelay));
    }

    private void CreateCard()
    {
      SCARD_SHARE_MODE scardShareMode = SCARD_SHARE_MODE.Shared;
      SCARD_PROTOCOL scardProtocol = SCARD_PROTOCOL.Tx;
      try
      {
        while (this.phCard == IntPtr.Zero)
        {
          switch (WinSCardCardJob.SCardConnect(this.phContext, this.readerInfo.FullName, (uint) scardShareMode, (uint) scardProtocol, out this.phCard, out this.activeProtocol))
          {
            case 2148532329U:
              this.startHandle.Set();
              if (this.stopHandle.WaitOne(this.shortDelay))
                return;
              else
                goto case 0U;
            case 0U:
              continue;
            default:
              this.startHandle.Set();
              return;
          }
        }
        if (this.ReadTextSupported)
        {
          this.cardTextReader = (IWinSCardTextReader) Activator.CreateInstance(this.readerInfo.CardTextReaderType);
          this.cardTextReader.PHContext = this.phContext;
          this.cardTextReader.PHCard = this.phCard;
          this.cardTextReader.ReaderName = this.ReaderInfo.FullName;
        }
        if (this.CardInserted != null)
          this.CardInserted((object) this, new CardEventArgs()
          {
            ReaderFullName = this.ReaderInfo.FullName
          });
        this.startHandle.Set();
        this.CheckCardState();
      }
      finally
      {
        if (this.phCard != IntPtr.Zero)
        {
          if (this.cardTextReader != null)
          {
            this.cardTextReader.Dispose();
            this.cardTextReader = (IWinSCardTextReader) null;
          }
          int num = (int) WinSCardCardJob.SCardDisconnect(this.phCard, 2U);
          this.phCard = IntPtr.Zero;
          if (this.CardRemoved != null)
            this.CardRemoved((object) this, new CardEventArgs()
            {
              ReaderFullName = this.ReaderInfo.FullName
            });
        }
      }
    }

    private void CheckCardState()
    {
      while (!this.stopHandle.WaitOne(0))
      {
        SCARD_READERSTATE[] rgReaderStates = new SCARD_READERSTATE[1];
        rgReaderStates[0].m_szReader = this.readerInfo.FullName;
        rgReaderStates[0].m_dwEventState = 0U;
        rgReaderStates[0].m_dwCurrentState = 0U;
        if ((int) WinSCardCardJob.SCardGetStatusChange(this.phContext, (uint) this.shortDelay.TotalMilliseconds, rgReaderStates, 1U) != 0 || ((int) rgReaderStates[0].m_dwEventState & 16) == 16)
          break;
      }
    }

    public override string ToString()
    {
      return this.readerInfo.ToString();
    }

    public void Transmit(byte[] sendBuffer, int sendLength, byte[] responseBuffer, ref int responseLength)
    {
      if (this.phCard == IntPtr.Zero)
        throw new WinSCardException(this.traceEnabled, "SCard.Transmit", 2148532329U);
      SCARD_IO_REQUEST pioSendPci;
      pioSendPci.dwProtocol = this.activeProtocol;
      pioSendPci.cbPciLength = 8U;
      uint status = WinSCardCardJob.SCardTransmit(this.phCard, ref pioSendPci, sendBuffer, sendLength, IntPtr.Zero, responseBuffer, ref responseLength);
      if ((int) status != 0)
        throw new WinSCardException(this.traceEnabled, "SCard.Transmit", status);
      RespApdu respApdu = new RespApdu(responseBuffer, responseLength);
      WinSCardCardJob.LogTransmitResult(sendBuffer, sendLength, respApdu);
    }

    private static void LogTransmitResult(byte[] sendBuffer, int sendLength, RespApdu respApdu)
    {
      try
      {
        ushort? sw1Sw2 = respApdu.SW1SW2;
        if (((int) sw1Sw2.GetValueOrDefault() != 36864 ? 1 : (!sw1Sw2.HasValue ? 1 : 0)) == 0)
          return;
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.AppendLine("APDU Command data:");
        stringBuilder.AppendLine(HexFormatting.Dump("--> C-APDU: 0x", sendBuffer, sendLength, 16, ValueFormat.HexASCII));
        stringBuilder.AppendFormat("APDU Command returned an unexpected result: 0x{0:X04}", (object) respApdu.SW1SW2);
        stringBuilder.AppendLine();
        stringBuilder.Append("SDK Calling methods:");
        StackTrace stackTrace = new StackTrace();
        for (int index = 0; index < stackTrace.FrameCount; ++index)
        {
          if (index % 5 == 0)
          {
            stringBuilder.AppendLine();
            stringBuilder.Append("\t");
          }
          StackFrame frame = stackTrace.GetFrame(stackTrace.FrameCount - 1 - index);
          stringBuilder.AppendFormat("{0} -> ", (object) frame.GetMethod().Name);
        }
        stringBuilder.AppendLine();
        LogManager.FileLog(((object) stringBuilder).ToString());
      }
      catch (Exception ex)
      {
        LogManager.FileLog(ex);
      }
    }

    public void Exchange(byte[] sendBuffer, int sendLength, byte[] responseBuffer, ref int responseLength, ushort? expectedSW1SW2)
    {
      if (sendBuffer == null)
        throw new ArgumentNullException("snd_buf");
      if (responseBuffer == null)
        throw new ArgumentNullException("rec_buf");
      this.Transmit(sendBuffer, sendLength, responseBuffer, ref responseLength);
      RespApdu respApdu = new RespApdu(responseBuffer, responseLength);
      ushort? nullable = expectedSW1SW2;
      if (!(nullable.HasValue ? new int?((int) nullable.GetValueOrDefault()) : new int?()).HasValue)
        return;
      nullable = expectedSW1SW2;
      ushort? sw1Sw2 = respApdu.SW1SW2;
      if (((int) nullable.GetValueOrDefault() != (int) sw1Sw2.GetValueOrDefault() ? 1 : (nullable.HasValue != sw1Sw2.HasValue ? 1 : 0)) != 0)
        throw new ApduException(this.traceEnabled, 0, expectedSW1SW2.Value, respApdu.SW1SW2);
    }

    public void Exchange(byte[] sendBuffer, int sendLength, out byte[] responseBuffer, out int responseLength, ushort? expectedSW1SW2)
    {
      responseBuffer = (byte[]) null;
      CmdApdu cmdApdu;
      if (sendBuffer.Length == sendLength)
      {
        cmdApdu = new CmdApdu(sendBuffer);
      }
      else
      {
        byte[] cmdApduBuffer = new byte[sendLength];
        Array.Copy((Array) sendBuffer, (Array) cmdApduBuffer, sendLength);
        cmdApdu = new CmdApdu(cmdApduBuffer);
      }
      int length = 2;
      if (cmdApdu.Le.HasValue)
        length = cmdApdu.Le.Value + 2;
      byte[] responseBuffer1 = new byte[length];
      responseLength = responseBuffer1.Length;
      this.Exchange(sendBuffer, sendLength, responseBuffer1, ref responseLength, expectedSW1SW2);
      responseBuffer = new byte[responseLength];
      Array.Copy((Array) responseBuffer1, (Array) responseBuffer, responseLength);
    }

    public RespApdu Exchange(string cmdApdu)
    {
      return this.Exchange(cmdApdu, new ushort?());
    }

    public RespApdu Exchange(string cmdApdu, ushort? expectedSW1SW2)
    {
      return new RespApdu(this.Exchange(HexEncoding.GetBytes(cmdApdu), expectedSW1SW2));
    }

    public RespApdu Exchange(CmdApdu cmdApdu)
    {
      return this.Exchange(cmdApdu, new ushort?());
    }

    public RespApdu Exchange(CmdApdu cmdApdu, ushort? expectedSW1SW2)
    {
      return new RespApdu(this.Exchange(cmdApdu.GetBytes(), expectedSW1SW2));
    }

    public byte[] Exchange(byte[] sendBuffer)
    {
      return this.Exchange(sendBuffer, new ushort?());
    }

    public byte[] Exchange(byte[] sendBuffer, ushort? expectedSW1SW2)
    {
      byte[] responseBuffer = (byte[]) null;
      this.Exchange(sendBuffer, out responseBuffer, expectedSW1SW2);
      return responseBuffer;
    }

    public void Exchange(byte[] sendBuffer, out byte[] responseBuffer)
    {
      this.Exchange(sendBuffer, out responseBuffer, new ushort?());
    }

    public void Exchange(byte[] sendBuffer, out byte[] responseBuffer, ushort? expectedSW1SW2)
    {
      int responseLength = 0;
      this.Exchange(sendBuffer, sendBuffer.Length, out responseBuffer, out responseLength, expectedSW1SW2);
    }

    public void Exchange(byte[] sendBuffer, int sendLength, out byte[] responseBuffer, out int responseLength)
    {
      this.Exchange(sendBuffer, sendLength, out responseBuffer, out responseLength, new ushort?());
    }

    public void Exchange(byte[] sendBuffer, int sendLength, byte[] responseBuffer, ref int responseLength)
    {
      this.Exchange(sendBuffer, sendLength, responseBuffer, ref responseLength, new ushort?());
    }

    public string ReadText(string prompt, bool password)
    {
      if (!this.ReadTextSupported)
        throw new NotSupportedException("Actiunea de citire text de la dispozitiv nu este suportata.");
      IWinSCardTextReader winScardTextReader = this.cardTextReader;
      if (winScardTextReader == null)
        throw new WinSCardException(this.traceEnabled, "SCard.Connect", 2148532247U);
      else
        return winScardTextReader.ReadText(prompt, password);
    }

    [DllImport("WinScard.dll")]
    private static extern uint SCardConnect(IntPtr hContext, string cReaderName, uint dwShareMode, uint dwPrefProtocol, out IntPtr phCard, out uint pdwActiveProtocol);

    [DllImport("WinScard.dll")]
    private static extern uint SCardDisconnect(IntPtr hCard, uint dwDisposition);

    [DllImport("WinScard.DLL")]
    private static extern uint SCardGetStatusChange(IntPtr hContext, uint dwTimeout, [In, Out] SCARD_READERSTATE[] rgReaderStates, uint cReaders);

    [DllImport("WinScard.dll")]
    public static extern uint SCardTransmit(IntPtr hCard, ref SCARD_IO_REQUEST pioSendPci, byte[] pbSendBuffer, int cbSendLength, IntPtr pioRecvPci, byte[] pbRecvBuffer, ref int pcbRecvLength);
  }
}
