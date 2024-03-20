using System;

namespace mcenters
{
	// Token: 0x02000005 RID: 5
	[Serializable]
	public class Patch
	{
		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600006A RID: 106 RVA: 0x00002A68 File Offset: 0x00001E68
		// (set) Token: 0x0600006B RID: 107 RVA: 0x00002A7C File Offset: 0x00001E7C
		public string Version
		{
			get
			{
				return this.<backing_store>Version;
			}
			set
			{
				this.<backing_store>Version = value;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600006C RID: 108 RVA: 0x00002A90 File Offset: 0x00001E90
		// (set) Token: 0x0600006D RID: 109 RVA: 0x00002AA4 File Offset: 0x00001EA4
		public long disableTrial
		{
			get
			{
				return this.<backing_store>disableTrial;
			}
			set
			{
				this.<backing_store>disableTrial = value;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600006E RID: 110 RVA: 0x00002AB8 File Offset: 0x00001EB8
		// (set) Token: 0x0600006F RID: 111 RVA: 0x00002ACC File Offset: 0x00001ECC
		public long enableTrial
		{
			get
			{
				return this.<backing_store>enableTrial;
			}
			set
			{
				this.<backing_store>enableTrial = value;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000070 RID: 112 RVA: 0x00002AE0 File Offset: 0x00001EE0
		// (set) Token: 0x06000071 RID: 113 RVA: 0x00002AF4 File Offset: 0x00001EF4
		public long defaultvalue
		{
			get
			{
				return this.<backing_store>defaultvalue;
			}
			set
			{
				this.<backing_store>defaultvalue = value;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000072 RID: 114 RVA: 0x00002B08 File Offset: 0x00001F08
		// (set) Token: 0x06000073 RID: 115 RVA: 0x00002B1C File Offset: 0x00001F1C
		public long position
		{
			get
			{
				return this.<backing_store>position;
			}
			set
			{
				this.<backing_store>position = value;
			}
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00002B7C File Offset: 0x00001F7C
		public Patch(string version, long pos)
		{
			this.<backing_store>Version = version;
			this.<backing_store>position = pos;
			this.<backing_store>disableTrial = 40691344807608369L;
			this.<backing_store>enableTrial = 40691344824385585L;
			long defaultPattern = Patch.DefaultPattern;
			this.<backing_store>defaultvalue = defaultPattern;
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00002B44 File Offset: 0x00001F44
		public Patch(string version, long disable, long enable, long defaultval, long pos)
		{
			this.<backing_store>position = pos;
			this.<backing_store>Version = version;
			this.<backing_store>disableTrial = disable;
			this.<backing_store>enableTrial = enable;
			this.<backing_store>defaultvalue = defaultval;
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00002B30 File Offset: 0x00001F30
		public Patch()
		{
		}

		// Token: 0x04000042 RID: 66
		private string <backing_store>Version;

		// Token: 0x04000043 RID: 67
		private long <backing_store>disableTrial;

		// Token: 0x04000044 RID: 68
		public static long DefaultPattern = 4974556687L;

		// Token: 0x04000045 RID: 69
		private long <backing_store>enableTrial;

		// Token: 0x04000046 RID: 70
		private long <backing_store>defaultvalue;

		// Token: 0x04000047 RID: 71
		private long <backing_store>position;
	}
}
