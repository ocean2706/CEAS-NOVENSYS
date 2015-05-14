// Decompiled with JetBrains decompiler
// Type: Novensys.eCard.SDK.Remoting.RemotingHelper
// Assembly: Novensys.eCard.SDK, Version=1.1.56.0, Culture=neutral, PublicKeyToken=null
// MVID: 59F9E456-D3DA-4066-A4A4-692B516A775E
// Assembly location: C:\Program Files (x86)\CNAS\SIUI-SPITAL\Novensys.eCard.SDK.dll

using System;
using System.Collections;
using System.IO;
using System.Reflection;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Ipc;
using System.Runtime.Serialization.Formatters;

namespace Novensys.eCard.SDK.Remoting
{
  public class RemotingHelper
  {
    public static IpcClientChannel RegisterClientChannel(string portName, string channelName)
    {
      IDictionary properties = (IDictionary) new Hashtable();
      properties[(object) "name"] = (object) channelName;
      properties[(object) "portName"] = (object) portName;
      properties[(object) "authorizedGroup"] = (object) "Everyone";
      IpcClientChannel ipcClientChannel = new IpcClientChannel(properties, (IClientChannelSinkProvider) null);
      ChannelServices.RegisterChannel((IChannel) ipcClientChannel, false);
      return ipcClientChannel;
    }

    public static IpcServerChannel RegisterServerChannel(string portName, string channelName)
    {
      BinaryServerFormatterSinkProvider formatterSinkProvider = new BinaryServerFormatterSinkProvider();
      formatterSinkProvider.TypeFilterLevel = TypeFilterLevel.Full;
      IDictionary properties = (IDictionary) new Hashtable();
      properties[(object) "name"] = (object) channelName;
      properties[(object) "portName"] = (object) portName;
      properties[(object) "authorizedGroup"] = (object) "Everyone";
      properties[(object) "exclusiveAddressUse"] = (object) false;
      IpcServerChannel ipcServerChannel = new IpcServerChannel(properties, (IServerChannelSinkProvider) formatterSinkProvider);
      ChannelServices.RegisterChannel((IChannel) ipcServerChannel, false);
      return ipcServerChannel;
    }

    public static void UnregisterChannel(IChannel channel)
    {
      ChannelServices.UnregisterChannel(channel);
    }

    private static void CheckConfigurationFile(string configurationFile)
    {
      RemotingHelper.ExtractEmbeddedBinaryFile(Assembly.GetExecutingAssembly(), string.Format("Novensys.eCard.SDK.Remoting.Configs.{0}", (object) configurationFile), Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), configurationFile));
    }

    private static void ExtractEmbeddedBinaryFile(Assembly assembly, string resourceName, string targetFile)
    {
      FileInfo fileInfo = new FileInfo(assembly.Location);
      if (File.Exists(targetFile) && new FileInfo(targetFile).CreationTime < fileInfo.LastWriteTime)
        File.Delete(targetFile);
      if (File.Exists(targetFile))
        return;
      Stream manifestResourceStream = assembly.GetManifestResourceStream(resourceName);
      string str = string.Empty;
      if (manifestResourceStream == null)
        throw new Exception(string.Format("Fisierul resursa '{0}' nu a fost gasit in '{1}'.", (object) resourceName, (object) assembly.Location));
      Stream output = (Stream) File.Create(targetFile);
      BinaryReader binaryReader = new BinaryReader(manifestResourceStream);
      BinaryWriter binaryWriter = new BinaryWriter(output);
      if (manifestResourceStream.Length > (long) int.MaxValue)
        throw new Exception(string.Format("Fisierul resursa '{0}' din '{1}' este prea mare.", (object) resourceName, (object) assembly.Location));
      binaryWriter.Write(binaryReader.ReadBytes((int) manifestResourceStream.Length));
      binaryWriter.Flush();
      binaryWriter.Close();
      binaryReader.Close();
      new FileInfo(targetFile).CreationTime = fileInfo.LastWriteTime;
    }
  }
}
