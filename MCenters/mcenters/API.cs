using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Media;

namespace mcenters
{
	// Token: 0x02000006 RID: 6
	public class API
	{
		// Token: 0x06000078 RID: 120 RVA: 0x00002BE4 File Offset: 0x00001FE4
		public static void SetupVersionList()
		{
			API.SupportedVersions = new Dictionary<string, Patch>(10);
			API.SupportedVersions.Add("1.19.41.01", new Patch("1.19.41.01", 40691344807608369L, 40691344824385585L, 4974556687L, 14621280L));
			API.SupportedVersions.Add("1.19.51.011", new Patch("1.19.51.011", 40691344807608369L, 40691344824385585L, 4974556687L, 14600288L));
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00002C74 File Offset: 0x00002074
		public static void ShouldOpenBetaEdition([MarshalAs(UnmanagedType.U1)] bool val)
		{
			API.BetaEditionMode = val;
			<Module>.IsBetaEditionTarget(val);
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00002C90 File Offset: 0x00002090
		public static int Test()
		{
			return 1;
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00002CA0 File Offset: 0x000020A0
		public static int EnableDebug()
		{
			return <Module>.EnableDebugPriv();
		}

		// Token: 0x0600007C RID: 124 RVA: 0x00002CB4 File Offset: 0x000020B4
		[return: MarshalAs(UnmanagedType.U1)]
		private unsafe static bool search(Process process, FileVersionInfo fileVersion)
		{
			Logger.AddLog("Trying Pattern Search");
			int moduleMemorySize = process.MainModule.ModuleMemorySize;
			Logger.AddLog("Reading Data Source", process.MainModule.ModuleName);
			IntPtr baseAddress = process.MainModule.BaseAddress;
			int num = <Module>.SetDataSource(process.Id, baseAddress.ToPointer(), moduleMemorySize);
			int num2 = num;
			Logger.AddLog("Data Source Read bytes", num2.ToString());
			if (num != moduleMemorySize)
			{
				return false;
			}
			int num3 = -1;
			int num4 = <Module>.TryFindValue(&num3);
			int num5 = num3;
			Logger.AddLog("Count", num5.ToString());
			if (num3 != 1)
			{
				return false;
			}
			Patch patch = new Patch((!API.BetaEditionMode) ? fileVersion.ProductVersion : (fileVersion.ProductVersion + " p"), (long)num4);
			string text = new SerializeDeserialize<Patch>().SerializeData(patch);
			Settings.Default.patch = text;
			Settings.Default.Save();
			API.SupportedVersions.Add(patch.Version, patch);
			return true;
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00003100 File Offset: 0x00002500
		public static PrepareResults PrepareProcess([MarshalAs(UnmanagedType.U1)] bool trial)
		{
			Patch patch = null;
			Process process = null;
			bool flag = false;
			try
			{
				int num = <Module>.LaunchApp();
				Process process2;
				if (num != 0)
				{
					process2 = Process.GetProcessById(num);
				}
				else
				{
					process2 = process;
				}
				process = process2;
				if (API.appId == 0)
				{
					API.appId = num;
					process2.EnableRaisingEvents = true;
					process2.Exited += API.OnExited;
				}
				flag = process2 != null || flag;
			}
			catch (Exception ex)
			{
				Logger.AddError("An Error Occured while starting Minecraft");
				return PrepareResults.StartError;
			}
			if (!flag)
			{
				Logger.AddError("Minecraft Start Failed. Is Minecraft Installed?");
				return PrepareResults.StartFailed;
			}
			FileVersionInfo fileVersionInfo = process.MainModule.FileVersionInfo;
			string text;
			if (API.BetaEditionMode)
			{
				text = fileVersionInfo.ProductVersion + " p";
			}
			else
			{
				text = fileVersionInfo.ProductVersion;
			}
			if (!API.IsValidVersion(fileVersionInfo))
			{
				Color brown = Colors.Brown;
				Logger.AddLog("Minecraft version is not in known list", brown);
				Color crimson = Colors.Crimson;
				Logger.AddLog("Using Pattern Mode", crimson);
				Patch patch2 = null;
				string patch3 = Settings.Default.patch;
				if (patch3 != "nan")
				{
					patch2 = new SerializeDeserialize<Patch>().DeserializeData(patch3);
				}
				if (patch2 != null)
				{
					if (patch2.Version == text)
					{
						Logger.AddLog("Found from disc");
						API.SupportedVersions.Add(patch2.Version, patch2);
					}
					else if (!API.search(process, fileVersionInfo))
					{
						Logger.AddError("Pattern Error, Version Unsupported");
						return PrepareResults.InvalidVersion;
					}
				}
				else if (!API.search(process, fileVersionInfo))
				{
					Logger.AddError("Pattern Error, Version Unsupported");
					return PrepareResults.InvalidVersion;
				}
				Color crimson2 = Colors.Crimson;
				Logger.AddLog("End Pattern Mode", crimson2);
			}
			patch = null;
			API.SupportedVersions.TryGetValue(text, out patch);
			<Module>.ChangePatchValues(patch.disableTrial, patch.enableTrial, patch.defaultvalue, patch.position);
			IntPtr baseAddress = process.MainModule.BaseAddress;
			int num2 = <Module>.ModifyApp(process.Id, baseAddress.ToPointer(), trial, API.ignoreVerification);
			if (num2 == -4 || num2 == -3)
			{
				Logger.AddError("Error Occured While Writing Memory");
				return PrepareResults.MemoryWriteError;
			}
			if (num2 == -2)
			{
				Logger.AddError("Error Occured While Reading Memory");
				return PrepareResults.MemoryReadError;
			}
			if (num2 == 0)
			{
				Logger.AddError("Unknown Memory Patterns\nYou may be using unsupported version\nor using other mods");
				return PrepareResults.MemoryPatternError;
			}
			if (num2 != 1)
			{
				Logger.AddError("Unknown Error Occured");
				return PrepareResults.UnknownError;
			}
			API.ignoreVerification = true;
			Color green = Colors.Green;
			Logger.AddLog("Mode Injection Successful", green);
			return PrepareResults.Success;
		}

		// Token: 0x0600007E RID: 126 RVA: 0x0000335C File Offset: 0x0000275C
		public static int GetProcess()
		{
			Process[] processesByName = Process.GetProcessesByName("Minecraft.Windows");
			if (processesByName == null)
			{
				return -1;
			}
			List<Process> list = new List<Process>(5);
			int num = 0;
			if (0 < processesByName.Length)
			{
				do
				{
					Process process = processesByName[num];
					if (API.IsValidVersion(process.MainModule.FileVersionInfo))
					{
						list.Add(process);
					}
					num++;
				}
				while (num < processesByName.Length);
			}
			if (list.Count == 0)
			{
				return -1;
			}
			int num2 = 0;
			List<Process>.Enumerator enumerator = list.GetEnumerator();
			if (enumerator.MoveNext())
			{
				Process process2;
				for (;;)
				{
					process2 = enumerator.Current;
					if (!process2.HasExited)
					{
						IntPtr baseAddress = process2.MainModule.BaseAddress;
						num2 = <Module>.IsValidApp(process2.Id, baseAddress.ToPointer());
						if (num2 != 0)
						{
							if (num2 == -2)
							{
								return -3;
							}
							if (num2 == -1)
							{
								return -2;
							}
							if (num2 == 1)
							{
								break;
							}
						}
					}
					if (!enumerator.MoveNext())
					{
						goto IL_C1;
					}
				}
				process2.Kill();
				return 1;
				IL_C1:
				if (num2 == -2)
				{
					return -3;
				}
				if (num2 == -1)
				{
					return -2;
				}
				if (num2 != 0)
				{
					if (num2 == 1)
					{
						return 1;
					}
					return 0;
				}
			}
			return 0;
		}

		// Token: 0x0600007F RID: 127 RVA: 0x00002DAC File Offset: 0x000021AC
		[return: MarshalAs(UnmanagedType.U1)]
		private static bool IsValidVersion(FileVersionInfo info)
		{
			string text;
			if (API.BetaEditionMode)
			{
				text = info.ProductVersion + " p";
			}
			else
			{
				text = info.ProductVersion;
			}
			Logger.AddLog("Detected Version: " + info.ProductVersion);
			int num;
			if (info.ProductName == "Minecraft" && API.SupportedVersions.ContainsKey(text))
			{
				num = 1;
			}
			else
			{
				num = 0;
			}
			return (byte)num != 0;
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00002E18 File Offset: 0x00002218
		private static void OnExited(object sender, EventArgs e)
		{
			API.ignoreVerification = false;
			API.appId = 0;
		}

		// Token: 0x04000048 RID: 72
		public static bool BetaEditionMode = false;

		// Token: 0x04000049 RID: 73
		private static Dictionary<string, Patch> SupportedVersions;

		// Token: 0x0400004A RID: 74
		private static bool ignoreVerification = false;

		// Token: 0x0400004B RID: 75
		private static int appId = 0;
	}
}
