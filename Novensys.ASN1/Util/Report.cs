// Decompiled with JetBrains decompiler
// Type: Novensys.ASN1.Util.Report
// Assembly: Novensys.eCard.SDK, Version=1.1.56.0, Culture=neutral, PublicKeyToken=null
// MVID: 59F9E456-D3DA-4066-A4A4-692B516A775E
// Assembly location: C:\Program Files (x86)\CNAS\SIUI-SPITAL\Novensys.eCard.SDK.dll

using System;
using System.IO;
using System.Reflection;

namespace Novensys.ASN1.Util
{
  public class Report
  {
    public static void logException(string dir, Exception ex)
    {
      TextWriter textWriter = Report.makeReportWriter(dir);
      textWriter.WriteLine(ex.ToString());
      textWriter.WriteLine(ex.StackTrace);
      textWriter.Close();
    }

    private static TextWriter makeReportWriter(string dir)
    {
      TextWriter textWriter;
      if ("stdout".Equals(dir))
        textWriter = Console.Out;
      else if ("stderr".Equals(dir))
      {
        textWriter = Console.Error;
      }
      else
      {
        string path = "Asn1rt_" + DateTime.Now.Ticks.ToString() + ".log";
        try
        {
          if (dir != null && !"".Equals(dir))
          {
            if (!Directory.Exists(dir))
              Directory.CreateDirectory(dir);
            path = dir + Path.DirectorySeparatorChar.ToString() + path;
          }
        }
        catch (Exception ex)
        {
        }
        try
        {
          textWriter = (TextWriter) new StreamWriter((Stream) new FileStream(path, FileMode.Create, FileAccess.Write));
        }
        catch (Exception ex)
        {
          Console.Out.WriteLine("!! can't create file '" + path + "'.");
          textWriter = Console.Out;
        }
      }
      textWriter.WriteLine(Assembly.GetCallingAssembly().FullName);
      return textWriter;
    }
  }
}
