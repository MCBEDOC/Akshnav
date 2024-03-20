using System;
using System.Runtime.Serialization;

namespace <CrtImplementationDetails>
{
	// Token: 0x0200000A RID: 10
	[Serializable]
	internal class ModuleLoadException : Exception
	{
		// Token: 0x06000097 RID: 151 RVA: 0x000045AC File Offset: 0x000039AC
		protected ModuleLoadException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00004594 File Offset: 0x00003994
		public ModuleLoadException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00004580 File Offset: 0x00003980
		public ModuleLoadException(string message)
			: base(message)
		{
		}

		// Token: 0x0400005E RID: 94
		public const string Nested = "A nested exception occurred after the primary exception that caused the C++ module to fail to load.\n";
	}
}
