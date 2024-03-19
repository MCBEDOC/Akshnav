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
