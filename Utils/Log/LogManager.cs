// Decompiled with JetBrains decompiler
// Type: Novensys.eCard.SDK.Utils.Log.LogManager
// Assembly: Novensys.eCard.SDK, Version=1.1.56.0, Culture=neutral, PublicKeyToken=null
// MVID: 59F9E456-D3DA-4066-A4A4-692B516A775E
// Assembly location: C:\Program Files (x86)\CNAS\SIUI-SPITAL\Novensys.eCard.SDK.dll

using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Reflection;

namespace Novensys.eCard.SDK.Utils.Log
{
  public static class LogManager
  {
    private static string LogFolderPath { get; set; }

    public static void FileLog(string message)
    {
      LogManager.WriteText(message);
    }

    public static void AddSeparatorLine()
    {
      LogManager.WriteText(new string('-', 100), false);
    }

    public static void FileLog(Exception exception)
    {
      LogManager.WriteText(string.Format("{0}\r\n{1}", (object) exception.Message, (object) exception.StackTrace));
    }

    public static void FileLog(string customMessage, Exception exception)
    {
      LogManager.WriteText(string.Format("{0}\r\n{1}\r\n{2}", (object) customMessage, (object) exception.Message, (object) exception.StackTrace));
    }

    private static void WriteText(string message)
    {
      LogManager.WriteText(message, true);
    }

    private static void WriteText(string message, bool addTimestamp)
    {
      try
      {
        string str = message;
        if (addTimestamp)
          str = string.Format((IFormatProvider) CultureInfo.InvariantCulture, "{0:HH:mm:ss:fff} : {1}", new object[2]
          {
            (object) DateTime.Now,
            (object) message
          });
        using (StreamWriter streamWriter = LogManager.CreateStreamWriter())
          streamWriter.WriteLine(str);
      }
      catch (Exception ex)
      {
      }
    }

    private static StreamWriter CreateStreamWriter()
    {
      string str1 = string.Empty;
      string path1 = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\Novensys.eCard.SDK\\" + Process.GetCurrentProcess().ProcessName + "\\Logs\\";
      if (string.IsNullOrEmpty(LogManager.LogFolderPath))
        LogManager.LogFolderPath = path1;
      if (!Directory.Exists(LogManager.LogFolderPath))
        Directory.CreateDirectory(path1);
      string str2 = Assembly.GetEntryAssembly() == null ? "GenericCardApp" : Assembly.GetEntryAssembly().GetName().Name;
      string str3 = Assembly.GetEntryAssembly() == null ? "1.0" : ((object) Assembly.GetEntryAssembly().GetName().Version).ToString();
      string path2 = Path.Combine(LogManager.LogFolderPath, str2 + "_" + LogManager.GetLogFileName());
      StreamWriter streamWriter = File.Exists(path2) ? File.AppendText(path2) : File.CreateText(path2);
      streamWriter.AutoFlush = true;
      return streamWriter;
    }

    private static string GetLogFileName()
    {
      DateTime today = DateTime.Today;
      return today.Year.ToString() + (today.Month < 10 ? "0" : "") + today.Month.ToString() + (today.Day < 10 ? "0" : "") + today.Day.ToString() + ".log";
    }

    public static void CloseLogger()
    {
    }
  }
}
