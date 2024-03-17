// Decompiled with JetBrains decompiler
// Type: mcenters.Settings
// Assembly: MCenters, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 45CFC87E-86C0-4035-8A46-F8737ED6CA8B
// Assembly location: C:\Users\Misi\Downloads\akshnav_3.exe

using System.Collections.Generic;
using System.Configuration;
using System.Reflection;

#nullable disable
namespace mcenters
{
  [DefaultMember("Item")]
  internal class Settings : ApplicationSettingsBase
  {
    private static Settings defaultInstance = (Settings) SettingsBase.Synchronized((SettingsBase) new Settings());
    private Dictionary<string, SettingsPropertyValue> Dic;

    public Settings()
      : base("Data")
    {
    }

    public void LoadDic()
    {
    }

    public static Settings Default => Settings.defaultInstance;

    [DefaultSettingValue("nan")]
    [UserScopedSetting]
    public string patch
    {
      get => (string) this[nameof (patch)];
      set => this["Patch"] = (object) value;
    }
  }
}
