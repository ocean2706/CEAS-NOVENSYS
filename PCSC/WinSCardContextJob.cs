// Decompiled with JetBrains decompiler
// Type: Novensys.eCard.SDK.PCSC.WinSCardContextJob
// Assembly: Novensys.eCard.SDK, Version=1.1.56.0, Culture=neutral, PublicKeyToken=null
// MVID: 59F9E456-D3DA-4066-A4A4-692B516A775E
// Assembly location: C:\Program Files (x86)\CNAS\SIUI-SPITAL\Novensys.eCard.SDK.dll

using Novensys.eCard.SDK.PCSC.Apdu;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

namespace Novensys.eCard.SDK.PCSC
{
  public class WinSCardContextJob
  {
    private static WinSCardContextJob instance = new WinSCardContextJob();
    private string traceName = "WinSCardContextJob";
    private bool traceEnabled = true;
    private TimeSpan startDelay = new TimeSpan(0, 0, 10);
    private TimeSpan shortDelay = new TimeSpan(0, 0, 1);
    private TimeSpan longDelay = new TimeSpan(0, 2, 0);
    private ManualResetEvent stopHandle = new ManualResetEvent(false);
    private ManualResetEvent startHandle = new ManualResetEvent(false);
    private object syncRoot = new object();
    private IntPtr phContext = IntPtr.Zero;
    private List<WinSCardCardJob> cardJobs = new List<WinSCardCardJob>();
    private Thread thread;

    public static WinSCardContextJob Instance
    {
      get
      {
        return WinSCardContextJob.instance;
      }
    }

    public bool ContextEstablished
    {
      get
      {
        return this.phContext != IntPtr.Zero;
      }
    }

    public event EventHandler ContextCreated;

    public event EventHandler ContextDestroyed;

    public event EventHandler<CardEventArgs> ReaderConnected;

    public event EventHandler<CardEventArgs> ReaderDisconnected;

    public event EventHandler<CardEventArgs> CardInserted;

    public event EventHandler<CardEventArgs> CardRemoved;

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
          this.EstablishContext();
        }
        catch (ThreadAbortException ex)
        {
        }
        catch (Exception ex)
        {
          if (this.stopHandle.WaitOne(this.longDelay))
            break;
        }
      }
      while (!this.stopHandle.WaitOne(this.shortDelay));
    }

    private void EstablishContext()
    {
      try
      {
        if ((int) WinSCardContextJob.SCardEstablishContext(2U, IntPtr.Zero, IntPtr.Zero, out this.phContext) != 0)
          return;
        if (this.ContextCreated != null)
          this.ContextCreated((object) this, EventArgs.Empty);
        this.CreateCardJobs();
      }
      finally
      {
        if (this.phContext != IntPtr.Zero)
        {
          int num = (int) WinSCardContextJob.SCardReleaseContext(this.phContext);
          this.phContext = IntPtr.Zero;
          if (this.ContextDestroyed != null)
            this.ContextDestroyed((object) this, EventArgs.Empty);
        }
      }
    }

    private void CreateCardJobs()
    {
      try
      {
        do
        {
          List<WinSCardReaderInfo> list = new List<WinSCardReaderInfo>(this.GetAvailableReaders(this.phContext));
          foreach (WinSCardReaderInfo readerInfo in list)
          {
            bool flag = false;
            foreach (WinSCardCardJob winScardCardJob in this.cardJobs)
            {
              if (readerInfo.Equals((object) winScardCardJob.ReaderInfo))
              {
                flag = true;
                break;
              }
            }
            if (!flag)
              this.AddJob(new WinSCardCardJob(this.phContext, readerInfo));
          }
          foreach (WinSCardCardJob job in this.cardJobs.ToArray())
          {
            bool flag = false;
            foreach (object obj in list)
            {
              if (obj.Equals((object) job.ReaderInfo))
              {
                flag = true;
                break;
              }
            }
            if (!flag)
              this.RemoveJob(job);
          }
          this.startHandle.Set();
        }
        while (!this.stopHandle.WaitOne(this.shortDelay));
      }
      finally
      {
        foreach (WinSCardCardJob job in this.cardJobs.ToArray())
          this.RemoveJob(job);
      }
    }

    private IEnumerable<WinSCardReaderInfo> GetAvailableReaders(IntPtr phContext)
    {
      int count = 0;
      uint result = WinSCardContextJob.SCardListReaders(phContext, (string) null, (byte[]) null, ref count);
      if ((int) result == 0)
      {
        byte[] mszReaders = new byte[count];
        result = WinSCardContextJob.SCardListReaders(phContext, (string) null, mszReaders, ref count);
        if ((int) result == 0)
        {
          string[] readerFullNames = Encoding.ASCII.GetString(mszReaders).Split(new char[1], StringSplitOptions.RemoveEmptyEntries);
          foreach (string fullName in readerFullNames)
          {
            WinSCardReaderInfo readerInfo = WinSCardReaderInfo.FindReader(fullName);
            if (readerInfo != null)
              yield return readerInfo;
          }
        }
      }
    }

    private void AddJob(WinSCardCardJob job)
    {
      this.cardJobs.Add(job);
      job.CardInserted += new EventHandler<CardEventArgs>(this.JobCardInserted);
      job.CardRemoved += new EventHandler<CardEventArgs>(this.JobCardRemoved);
      job.Start();
      if (this.ReaderConnected == null)
        return;
      this.ReaderConnected((object) this, new CardEventArgs()
      {
        ReaderFullName = job.ReaderInfo.FullName
      });
    }

    private void RemoveJob(WinSCardCardJob job)
    {
      this.cardJobs.Remove(job);
      job.Stop();
      job.CardInserted -= new EventHandler<CardEventArgs>(this.JobCardInserted);
      job.CardRemoved -= new EventHandler<CardEventArgs>(this.JobCardRemoved);
      if (this.ReaderDisconnected == null)
        return;
      this.ReaderDisconnected((object) this, new CardEventArgs()
      {
        ReaderFullName = job.ReaderInfo.FullName
      });
    }

    private void JobCardInserted(object sender, CardEventArgs e)
    {
      if (this.CardInserted == null)
        return;
      this.CardInserted(sender, e);
    }

    private void JobCardRemoved(object sender, CardEventArgs e)
    {
      if (this.CardRemoved == null)
        return;
      this.CardRemoved(sender, e);
    }

    public string FindAvailableReader(string name)
    {
      foreach (WinSCardCardJob winScardCardJob in this.cardJobs.ToArray())
      {
        if (string.IsNullOrEmpty(name) || string.Equals(winScardCardJob.ReaderInfo.Name, name) || string.Equals(winScardCardJob.ReaderInfo.FullName, name))
          return winScardCardJob.ReaderInfo.FullName;
      }
      return (string) null;
    }

    public IApduExchange GetApduExchanger(string fullName)
    {
      return (IApduExchange) this.GetCardJob(fullName);
    }

    public ICardTextReader GetCardTextReader(string fullName)
    {
      return (ICardTextReader) this.GetCardJob(fullName);
    }

    private WinSCardCardJob GetCardJob(string fullName)
    {
      if (string.IsNullOrEmpty(fullName))
        throw new ArgumentException("Argument cannot be null or empty.", "fullName");
      foreach (WinSCardCardJob winScardCardJob in this.cardJobs.ToArray())
      {
        if (string.Equals(winScardCardJob.ReaderInfo.FullName, fullName) && !winScardCardJob.Faulted)
          return winScardCardJob;
      }
      return (WinSCardCardJob) null;
    }

    [DllImport("WinScard.dll")]
    private static extern uint SCardEstablishContext(uint dwScope, IntPtr pvReserved1, IntPtr pvReserved2, out IntPtr phContext);

    [DllImport("WinScard.dll")]
    private static extern uint SCardReleaseContext(IntPtr hContext);

    [DllImport("WinScard.dll")]
    private static extern uint SCardListReaders(IntPtr hContext, string mszGroups, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPWStr)] byte[] mszReaders, ref int pcchReaders);
  }
}
