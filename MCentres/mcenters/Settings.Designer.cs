using System;
using System.Collections.Generic;
using System.Configuration;
using System.Reflection;

namespace mcenters
{
	// Token: 0x02000003 RID: 3
	[DefaultMember("Item")]
	internal partial class Settings : ApplicationSettingsBase
	{
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600005A RID: 90 RVA: 0x0000271C File Offset: 0x00001B1C
		public static Settings Default
		{
			get
			{
				return Settings.defaultInstance;
			}
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600005B RID: 91 RVA: 0x00002730 File Offset: 0x00001B30
		// (set) Token: 0x0600005C RID: 92 RVA: 0x00002750 File Offset: 0x00001B50
		[DefaultSettingValue("nan")]
		[UserScopedSetting]
		public string patch
		{
			get
			{
				return (string)this["patch"];
			}
			set
			{
				this["Patch"] = value;
			}
		}

		// Token: 0x0400003D RID: 61
		private static Settings defaultInstance = (Settings)SettingsBase.Synchronized(new Settings());
	}
}
