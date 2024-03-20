using System;
using System.Collections;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Security;
using System.Threading;

namespace <CrtImplementationDetails>
{
	// Token: 0x0200000C RID: 12
	internal class ModuleUninitializer : Stack
	{
		// Token: 0x060000A0 RID: 160 RVA: 0x00004744 File Offset: 0x00003B44
		[SecuritySafeCritical]
		internal void AddHandler(EventHandler handler)
		{
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				RuntimeHelpers.PrepareConstrainedRegions();
				Monitor.Enter(ModuleUninitializer.@lock, ref flag);
				RuntimeHelpers.PrepareDelegate(handler);
				this.Push(handler);
			}
			finally
			{
				if (flag)
				{
					Monitor.Exit(ModuleUninitializer.@lock);
				}
			}
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x00004D5C File Offset: 0x0000415C
		[SecuritySafeCritical]
		private ModuleUninitializer()
		{
			EventHandler eventHandler = new EventHandler(this.SingletonDomainUnload);
			AppDomain.CurrentDomain.DomainUnload += eventHandler;
			AppDomain.CurrentDomain.ProcessExit += eventHandler;
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x000047A4 File Offset: 0x00003BA4
		[SecurityCritical]
		[PrePrepareMethod]
		private void SingletonDomainUnload(object source, EventArgs arguments)
		{
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				RuntimeHelpers.PrepareConstrainedRegions();
				Monitor.Enter(ModuleUninitializer.@lock, ref flag);
				using (IEnumerator enumerator = this.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						((EventHandler)enumerator.Current)(source, arguments);
					}
				}
			}
			finally
			{
				if (flag)
				{
					Monitor.Exit(ModuleUninitializer.@lock);
				}
			}
		}

		// Token: 0x04000061 RID: 97
		private static object @lock = new object();

		// Token: 0x04000062 RID: 98
		internal static ModuleUninitializer _ModuleUninitializer = new ModuleUninitializer();
	}
}
