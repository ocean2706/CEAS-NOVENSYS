// Decompiled with JetBrains decompiler
// Type: Novensys.eCard.SDK.PCSC.HID.HIDCardTextReader
// Assembly: Novensys.eCard.SDK, Version=1.1.56.0, Culture=neutral, PublicKeyToken=null
// MVID: 59F9E456-D3DA-4066-A4A4-692B516A775E
// Assembly location: C:\Program Files (x86)\CNAS\SIUI-SPITAL\Novensys.eCard.SDK.dll

using Novensys.eCard.SDK.PCSC;
using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Novensys.eCard.SDK.PCSC.HID
{
  internal class HIDCardTextReader : IWinSCardTextReader, IDisposable
  {
    private string traceName = "HIDCardTextReader";
    private bool traceEnabled = true;
    private object syncRoot = new object();
    private TimeSpan shortDelay = new TimeSpan(0, 0, 0, 0, 500);
    private TimeSpan timeoutWriteText = new TimeSpan(0, 1, 0);
    private TimeSpan timeoutReadText = new TimeSpan(0, 1, 0);
    private uint displayWidth = 16U;
    private const string AdvanceKeys = "0123456789*.";
    private const char BackspaceKey = '\b';
    private const char OKKey = '\r';
    private const char CancelKey = '\x001B';
    private const char FunctionKey = 'F';
    private bool disposed;
    private IntPtr phCard;

    public IntPtr PHContext { get; set; }

    public IntPtr PHCard
    {
      get
      {
        return this.phCard;
      }
      set
      {
        if (!(this.phCard != value))
          return;
        if (this.phCard != IntPtr.Zero)
        {
          int num = (int) HIDCardTextReader.SCardReleaseDisplay(this.phCard);
        }
        this.phCard = value;
        if ((int) HIDCardTextReader.SCardAcquireDisplay(this.phCard) == 0)
          ;
      }
    }

    public string ReaderName { get; set; }

    ~HIDCardTextReader()
    {
      this.Dispose(false);
    }

    public string ReadText(string prompt, bool password)
    {
      lock (this.syncRoot)
      {
        try
        {
          this.WriteText(prompt, 0U);
          this.WriteText(string.Empty, 1U);
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
      if (this.phCard != IntPtr.Zero)
      {
        try
        {
          int num = (int) HIDCardTextReader.SCardReleaseDisplay(this.phCard);
        }
        catch (Exception ex)
        {
        }
        finally
        {
          this.phCard = IntPtr.Zero;
        }
      }
      this.disposed = true;
    }

    private void WriteText(string text, uint y)
    {
      this.WriteText(text, y, this.timeoutWriteText);
    }

    private void WriteText(string text, uint y, TimeSpan displayTime)
    {
      this.WriteText(this.PadText(text), 0U, y, displayTime);
    }

    private void WriteText(string text, uint x, uint y, TimeSpan displayTime)
    {
      if (text == null || this.phCard == IntPtr.Zero)
        return;
      byte[] bytes = Encoding.ASCII.GetBytes(text);
      if ((int) HIDCardTextReader.SCardWriteDisplay(this.phCard, (uint) displayTime.TotalMilliseconds, x, y, bytes, bytes.Length) != 0)
        ;
    }

    public void WriteTextNoCard(string text, uint y)
    {
      this.WriteTextNoCard(this.PadText(text), 0U, y);
    }

    public void WriteTextNoCard(string text, uint x, uint y)
    {
      if (text == null || this.PHContext == IntPtr.Zero)
        return;
      byte[] bytes = Encoding.ASCII.GetBytes(text);
      if ((int) HIDCardTextReader.SCardWriteDisplayNoCard(this.PHContext, this.ReaderName, x, y, bytes, bytes.Length) != 0)
        ;
    }

    private void ClearScreen()
    {
      this.WriteText(string.Empty, 0U);
      this.WriteText(string.Empty, 1U);
      this.WriteTextNoCard(string.Empty, 0U);
      this.WriteTextNoCard(string.Empty, 1U);
    }

    private string PadText(string text)
    {
      if (text == null)
        return (string) null;
      int num = (int) this.displayWidth;
      if (text.Length > num)
        text = text.Substring(0, num);
      else if ((long) text.Length < (long) this.displayWidth)
        text = text.PadRight(num);
      return text;
    }

    private string ReadText(bool password)
    {
      uint bPositionX = 0U;
      uint num = 1U;
      string str = string.Empty;
      DateTime now = DateTime.Now;
      uint key;
      do
      {
        uint nDisplayMode = password ? 1U : 0U;
        if (bPositionX >= this.displayWidth)
          nDisplayMode = 2U;
        uint pbKeyPressed = 0U;
        key = HIDCardTextReader.SCardGetKey(this.phCard, (uint) this.shortDelay.TotalMilliseconds, nDisplayMode, bPositionX, num, ref pbKeyPressed);
        if ((int) key == 0)
        {
          if ((int) pbKeyPressed != 0)
          {
            char c = (char) pbKeyPressed;
            if ("0123456789*.".IndexOf(c) >= 0 && bPositionX < this.displayWidth)
            {
              ++bPositionX;
              str = str + new string(c, 1);
            }
            else if ((int) c == 8 && bPositionX > 0U)
            {
              --bPositionX;
              str = str.Substring(0, (int) bPositionX);
              this.WriteText(new string('*', (int) bPositionX), num);
            }
            else if ((int) c != 13)
            {
              if ((int) c == 27)
                goto label_12;
            }
            else
              goto label_10;
          }
        }
        else
          goto label_3;
      }
      while (!(DateTime.Now.Subtract(now) >= this.timeoutReadText));
      goto label_15;
label_3:
      throw new WinSCardException(true, "ReadText", key);
label_10:
      return str;
label_12:
      throw new WinSCardException(true, "ReadText", 2148532334U);
label_15:
      throw new WinSCardException(true, "ReadText", 2148532234U);
    }

    [DllImport("scardspen.dll")]
    private static extern uint SCardAcquireDisplay(IntPtr hCard);

    [DllImport("scardspen.dll")]
    private static extern uint SCardReleaseDisplay(IntPtr hCard);

    [DllImport("scardspen.dll")]
    private static extern uint SCardWriteDisplay(IntPtr hCard, uint usDisplayTime, uint bPositionX, uint bPositionY, byte[] pbString, int cbStringLength);

    [DllImport("scardspen.dll")]
    private static extern uint SCardWriteDisplayNoCard(IntPtr hContext, string szReader, uint bPositionX, uint bPositionY, byte[] pbString, int cbStringLength);

    [DllImport("scardspen.dll")]
    private static extern uint SCardGetKey(IntPtr hCard, uint usWaitTime, uint nDisplayMode, uint bPositionX, uint bPositionY, ref uint pbKeyPressed);
  }
}
