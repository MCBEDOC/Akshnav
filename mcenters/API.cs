// Decompiled with JetBrains decompiler
// Type: mcenters.API
// Assembly: MCenters, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 45CFC87E-86C0-4035-8A46-F8737ED6CA8B
// Assembly location: C:\Users\Misi\Downloads\akshnav_3.exe

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Media;

#nullable disable
namespace mcenters
{
  public class API
  {
    public static bool BetaEditionMode = false;
    private static Dictionary<string, Patch> SupportedVersions;
    private static bool ignoreVerification = false;
    private static int appId = 0;

    public static void SetupVersionList()
    {
      API.SupportedVersions = new Dictionary<string, Patch>(10);
      API.SupportedVersions.Add("1.19.41.01", new Patch("1.19.41.01", 40691344807608369L, 40691344824385585L, 4974556687L, 14621280L));
      API.SupportedVersions.Add("1.19.51.011", new Patch("1.19.51.011", 40691344807608369L, 40691344824385585L, 4974556687L, 14600288L));
    }

    public static void ShouldOpenBetaEdition([MarshalAs(UnmanagedType.U1)] bool val)
    {
      API.BetaEditionMode = val;
      \u003CModule\u003E.IsBetaEditionTarget(val);
    }

    public static int Test() => 1;

    public static int EnableDebug() => \u003CModule\u003E.EnableDebugPriv();

    [return: MarshalAs(UnmanagedType.U1)]
    private static unsafe bool search(Process process, FileVersionInfo fileVersion)
    {
      Logger.AddLog("Trying Pattern Search");
      int moduleMemorySize = process.MainModule.ModuleMemorySize;
      Logger.AddLog("Reading Data Source", process.MainModule.ModuleName);
      IntPtr baseAddress = process.MainModule.BaseAddress;
      int num1 = \u003CModule\u003E.SetDataSource(process.Id, baseAddress.ToPointer(), moduleMemorySize);
      Logger.AddLog("Data Source Read bytes", num1.ToString());
      if (num1 != moduleMemorySize)
        return false;
      int num2 = -1;
      int pos = (int) \u003CModule\u003E.TryFindValue(&num2);
      Logger.AddLog("Count", num2.ToString());
      if (num2 != 1)
        return false;
      Patch data = new Patch(!API.BetaEditionMode ? fileVersion.ProductVersion : fileVersion.ProductVersion + " p", (long) pos);
      Settings.Default.patch = new SerializeDeserialize<Patch>().SerializeData(data);
      Settings.Default.Save();
      API.SupportedVersions.Add(data.Version, data);
      return true;
    }

    public static unsafe PrepareResults PrepareProcess([MarshalAs(UnmanagedType.U1)] bool trial)
    {
      Process process1 = (Process) null;
      bool flag1 = false;
      Process process2;
      bool flag2;
      try
      {
        int processId = \u003CModule\u003E.LaunchApp();
        Process process3 = processId == 0 ? process1 : Process.GetProcessById(processId);
        process2 = process3;
        if (API.appId == 0)
        {
          API.appId = processId;
          process3.EnableRaisingEvents = true;
          process3.Exited += new EventHandler(API.OnExited);
        }
        flag2 = process3 != null || flag1;
      }
      catch (Exception ex)
      {
        Logger.AddError("An Error Occured while starting Minecraft");
        return PrepareResults.StartError;
      }
      if (!flag2)
      {
        Logger.AddError("Minecraft Start Failed. Is Minecraft Installed?");
        return PrepareResults.StartFailed;
      }
      FileVersionInfo fileVersionInfo = process2.MainModule.FileVersionInfo;
      string key = !API.BetaEditionMode ? fileVersionInfo.ProductVersion : fileVersionInfo.ProductVersion + " p";
      if (!API.IsValidVersion(fileVersionInfo))
      {
        Logger.AddLog("Minecraft version is not in known list", Colors.Brown);
        Logger.AddLog("Using Pattern Mode", Colors.Crimson);
        Patch patch1 = (Patch) null;
        string patch2 = Settings.Default.patch;
        if (patch2 != "nan")
          patch1 = new SerializeDeserialize<Patch>().DeserializeData(patch2);
        if (patch1 != null)
        {
          if (patch1.Version == key)
          {
            Logger.AddLog("Found from disc");
            API.SupportedVersions.Add(patch1.Version, patch1);
          }
          else if (!API.search(process2, fileVersionInfo))
          {
            Logger.AddError("Pattern Error, Version Unsupported");
            return PrepareResults.InvalidVersion;
          }
        }
        else if (!API.search(process2, fileVersionInfo))
        {
          Logger.AddError("Pattern Error, Version Unsupported");
          return PrepareResults.InvalidVersion;
        }
        Logger.AddLog("End Pattern Mode", Colors.Crimson);
      }
      Patch patch = (Patch) null;
      API.SupportedVersions.TryGetValue(key, out patch);
      \u003CModule\u003E.ChangePatchValues(patch.disableTrial, patch.enableTrial, patch.defaultvalue, patch.position);
      IntPtr baseAddress = process2.MainModule.BaseAddress;
      switch (\u003CModule\u003E.ModifyApp(process2.Id, baseAddress.ToPointer(), trial, API.ignoreVerification))
      {
        case -4:
        case -3:
          Logger.AddError("Error Occured While Writing Memory");
          return PrepareResults.MemoryWriteError;
        case -2:
          Logger.AddError("Error Occured While Reading Memory");
          return PrepareResults.MemoryReadError;
        case 0:
          Logger.AddError("Unknown Memory Patterns\nYou may be using unsupported version\nor using other mods");
          return PrepareResults.MemoryPatternError;
        case 1:
          API.ignoreVerification = true;
          Logger.AddLog("Mode Injection Successful", Colors.Green);
          return PrepareResults.Success;
        default:
          Logger.AddError("Unknown Error Occured");
          return PrepareResults.UnknownError;
      }
    }

    public static unsafe int GetProcess()
    {
      Process[] processesByName = Process.GetProcessesByName("Minecraft.Windows");
      if (processesByName == null)
        return -1;
      List<Process> processList = new List<Process>(5);
      int index = 0;
      if (0 < processesByName.Length)
      {
        do
        {
          Process process = processesByName[index];
          if (API.IsValidVersion(process.MainModule.FileVersionInfo))
            processList.Add(process);
          ++index;
        }
        while (index < processesByName.Length);
      }
      if (processList.Count == 0)
        return -1;
      int num = 0;
      List<Process>.Enumerator enumerator = processList.GetEnumerator();
      if (enumerator.MoveNext())
      {
        Process current;
        do
        {
          current = enumerator.Current;
          if (!current.HasExited)
          {
            IntPtr baseAddress = current.MainModule.BaseAddress;
            num = \u003CModule\u003E.IsValidApp(current.Id, baseAddress.ToPointer());
            switch (num)
            {
              case -2:
                goto label_14;
              case -1:
                goto label_13;
              case 1:
                goto label_12;
            }
          }
        }
        while (enumerator.MoveNext());
        goto label_15;
label_12:
        current.Kill();
        return 1;
label_13:
        return -2;
label_14:
        return -3;
label_15:
        switch (num)
        {
          case -2:
            return -3;
          case -1:
            return -2;
          case 0:
            break;
          case 1:
            return 1;
          default:
            return 0;
        }
      }
      return 0;
    }

    [return: MarshalAs(UnmanagedType.U1)]
    private static bool IsValidVersion(FileVersionInfo info)
    {
      string key = !API.BetaEditionMode ? info.ProductVersion : info.ProductVersion + " p";
      Logger.AddLog("Detected Version: " + info.ProductVersion);
      return info.ProductName == "Minecraft" && API.SupportedVersions.ContainsKey(key);
    }

    private static void OnExited(object sender, EventArgs e)
    {
      API.ignoreVerification = false;
      API.appId = 0;
    }
  }
}
