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
		// Token: 0x06000058 RID: 88 RVA: 0x000026F4 File Offset: 0x00001AF4
		public Settings()
			: base("Data")
		{
		}

		// Token: 0x06000059 RID: 89 RVA: 0x0000270C File Offset: 0x00001B0C
		public void LoadDic()
		{
		}

		// Token: 0x0400003E RID: 62
		private Dictionary<string, SettingsPropertyValue> Dic;
	}
}
