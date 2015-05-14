// Decompiled with JetBrains decompiler
// Type: Novensys.eCard.SDK.Properties.Settings
// Assembly: Novensys.eCard.SDK, Version=1.1.56.0, Culture=neutral, PublicKeyToken=null
// MVID: 59F9E456-D3DA-4066-A4A4-692B516A775E
// Assembly location: C:\Program Files (x86)\CNAS\SIUI-SPITAL\Novensys.eCard.SDK.dll

using System.CodeDom.Compiler;
using System.Configuration;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Novensys.eCard.SDK.Properties
{
  [CompilerGenerated]
  [GeneratedCode("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "11.0.0.0")]
  internal sealed class Settings : ApplicationSettingsBase
  {
    private static Settings defaultInstance = (Settings) SettingsBase.Synchronized((SettingsBase) new Settings());

    public static Settings Default
    {
      get
      {
        Settings settings = Settings.defaultInstance;
        return settings;
      }
    }

    [DefaultSettingValue("127.0.0.1")]
    [ApplicationScopedSetting]
    [DebuggerNonUserCode]
    public string IPUnitateManagement
    {
      get
      {
        return (string) this["IPUnitateManagement"];
      }
    }

    [DebuggerNonUserCode]
    [DefaultSettingValue("3002")]
    [ApplicationScopedSetting]
    public int PortUnitateManagement
    {
      get
      {
        return (int) this["PortUnitateManagement"];
      }
    }
  }
}
