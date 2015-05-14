// Decompiled with JetBrains decompiler
// Type: Novensys.eCard.SDK.PCSC.Reiner.ReinerCardTextReader
// Assembly: Novensys.eCard.SDK, Version=1.1.56.0, Culture=neutral, PublicKeyToken=null
// MVID: 59F9E456-D3DA-4066-A4A4-692B516A775E
// Assembly location: C:\Program Files (x86)\CNAS\SIUI-SPITAL\Novensys.eCard.SDK.dll

using Novensys.eCard.SDK.PCSC;
using Novensys.eCard.SDK.PCSC.Apdu;
using System;
using System.Text;

namespace Novensys.eCard.SDK.PCSC.Reiner
{
  internal class ReinerCardTextReader : IWinSCardTextReader, IDisposable
  {
    private string traceName = "ReinerCardTextReader";
    private bool traceEnabled = true;
    private byte[] sendBuff = new byte[256];
    private byte[] recvBuff = new byte[256];
    private object syncRoot = new object();
    private TimeSpan timeoutReadText = new TimeSpan(0, 1, 0);
    private int displayWidth = 16;
    private const string AdvanceKeys = "0123456789";
    private const char KEY_C = '\x001B';
    private const char KEY_CLR = '\b';
    private const char KEY_AT = 'M';
    private const char KEY_UP = '*';
    private const char KEY_DOWN = '.';
    private const char KEY_OK = '\r';
    private bool disposed;

    public IntPtr PHContext { get; set; }

    public IntPtr PHCard { get; set; }

    public string ReaderName { get; set; }

    ~ReinerCardTextReader()
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
          return this.ReadTextInternal(prompt, password);
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
      byte[] numArray1 = new byte[32];
      byte[] numArray2 = new byte[7];
      numArray2[1] = (byte) 1;
      byte[] numArray3 = numArray2;
      byte[] numArray4 = new byte[5]
      {
        byte.MaxValue,
        (byte) 194,
        (byte) 1,
        (byte) 15,
        (byte) 0
      };
      numArray3[6] = (byte) numArray1.Length;
      numArray4[4] = (byte) (numArray1.Length + numArray3.Length);
      this.ClearBuffers();
      Array.Copy((Array) numArray4, 0, (Array) this.sendBuff, 0, numArray4.Length);
      Array.Copy((Array) numArray3, 0, (Array) this.sendBuff, numArray4.Length, numArray3.Length);
      Array.Copy((Array) numArray1, 0, (Array) this.sendBuff, numArray4.Length + numArray3.Length, numArray1.Length);
      int sendLength = numArray4.Length + numArray3.Length + numArray1.Length;
      IApduExchange apduExchanger = WinSCardContextJob.Instance.GetApduExchanger(this.ReaderName);
      if (apduExchanger == null)
        return;
      int length = this.recvBuff.Length;
      apduExchanger.Transmit(this.sendBuff, sendLength, this.recvBuff, ref length);
    }

    private void WriteText(string text)
    {
      if (string.IsNullOrEmpty(text))
        return;
      if (text.Length > 2 * this.displayWidth)
        text = text.Substring(0, 2 * this.displayWidth);
      byte[] bytes = Encoding.ASCII.GetBytes(text);
      byte[] numArray1 = new byte[7];
      byte[] numArray2 = new byte[5]
      {
        byte.MaxValue,
        (byte) 194,
        (byte) 1,
        (byte) 15,
        (byte) 0
      };
      numArray1[6] = (byte) bytes.Length;
      numArray2[4] = (byte) (bytes.Length + numArray1.Length);
      this.ClearBuffers();
      Array.Copy((Array) numArray2, 0, (Array) this.sendBuff, 0, numArray2.Length);
      Array.Copy((Array) numArray1, 0, (Array) this.sendBuff, numArray2.Length, numArray1.Length);
      Array.Copy((Array) bytes, 0, (Array) this.sendBuff, numArray2.Length + numArray1.Length, bytes.Length);
      int sendLength = numArray2.Length + numArray1.Length + bytes.Length;
      IApduExchange apduExchanger = WinSCardContextJob.Instance.GetApduExchanger(this.ReaderName);
      if (apduExchanger == null)
        return;
      int length = this.recvBuff.Length;
      apduExchanger.Transmit(this.sendBuff, sendLength, this.recvBuff, ref length);
    }

    private string ReadTextInternal(string prompt, bool password)
    {
      int num1 = 0;
      int num2 = 1;
      string str = string.Empty;
      DateTime now = DateTime.Now;
      byte[] sendBuffer = new byte[10]
      {
        byte.MaxValue,
        (byte) 194,
        (byte) 1,
        (byte) 16,
        (byte) 5,
        (byte) 16,
        (byte) 39,
        (byte) 0,
        (byte) 0,
        (byte) 0
      };
      prompt = prompt ?? string.Empty;
      prompt = prompt.Length <= this.displayWidth ? prompt.PadRight(this.displayWidth, ' ') : prompt.Substring(0, this.displayWidth);
      while (true)
      {
        do
        {
          sendBuffer[7] = password ? (byte) 1 : (byte) 0;
          if (num1 + 1 >= this.displayWidth)
            sendBuffer[7] = (byte) 2;
          sendBuffer[8] = (byte) num1;
          sendBuffer[9] = (byte) num2;
          IApduExchange apduExchanger = WinSCardContextJob.Instance.GetApduExchanger(this.ReaderName);
          if (apduExchanger != null)
          {
            this.ClearBuffers();
            int length = this.recvBuff.Length;
            apduExchanger.Transmit(sendBuffer, sendBuffer.Length, this.recvBuff, ref length);
            switch (length)
            {
              case 2:
                if ((int) this.recvBuff[0] == 100 && (int) this.recvBuff[1] == 0)
                  continue;
                else
                  goto label_11;
              case 3:
                goto label_5;
              default:
                goto label_10;
            }
          }
          else
            goto label_3;
        }
        while (!(DateTime.Now.Subtract(now) >= this.timeoutReadText));
        goto label_9;
label_5:
        if ((int) this.recvBuff[1] != 144 || (int) this.recvBuff[3] != 0)
          goto label_6;
label_11:
        char c = (char) this.recvBuff[0];
        int num3;
        switch (c)
        {
          case '\r':
            goto label_12;
          case '\x001B':
            goto label_13;
          case 'M':
          case '*':
          case '.':
            continue;
          case '\b':
            num3 = num1 <= 0 ? 1 : 0;
            break;
          default:
            num3 = 1;
            break;
        }
        if (num3 == 0)
        {
          --num1;
          str = str.Substring(0, num1);
          this.WriteText(prompt + new string('*', num1));
          goto case 'M';
        }
        else if ("0123456789".IndexOf(c) >= 0 && num1 + 1 < this.displayWidth)
        {
          ++num1;
          str = str + new string(c, 1);
          goto case 'M';
        }
        else
          goto case 'M';
      }
label_3:
      throw new Exception("Failed to get APDU Exchanger.");
label_6:
      throw new Exception("Failed to read text from device.");
label_9:
      throw new WinSCardException(true, "ReadText", 2148532234U);
label_10:
      throw new Exception("Received invalid response from device.");
label_12:
      return str;
label_13:
      throw new WinSCardException(true, "ReadText", 2148532334U);
    }

    private void ClearBuffers()
    {
      Array.Clear((Array) this.sendBuff, 0, this.sendBuff.Length);
      Array.Clear((Array) this.recvBuff, 0, this.recvBuff.Length);
    }
  }
}
