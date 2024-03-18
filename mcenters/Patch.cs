// Decompiled with JetBrains decompiler
// Type: mcenters.Patch
// Assembly: MCenters, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 45CFC87E-86C0-4035-8A46-F8737ED6CA8B
// Assembly location: C:\Users\Misi\Downloads\akshnav_3.exe

using System;

#nullable disable
namespace mcenters
{
  [Serializable]
  public class Patch
  {
    private string \u003Cbacking_store\u003EVersion;
    private long \u003Cbacking_store\u003EdisableTrial;
    public static long DefaultPattern = 4974556687;
    private long \u003Cbacking_store\u003EenableTrial;
    private long \u003Cbacking_store\u003Edefaultvalue;
    private long \u003Cbacking_store\u003Eposition;

    public string Version
    {
      get => this.\u003Cbacking_store\u003EVersion;
      set => this.\u003Cbacking_store\u003EVersion = value;
    }

    public long disableTrial
    {
      get => this.\u003Cbacking_store\u003EdisableTrial;
      set => this.\u003Cbacking_store\u003EdisableTrial = value;
    }

    public long enableTrial
    {
      get => this.\u003Cbacking_store\u003EenableTrial;
      set => this.\u003Cbacking_store\u003EenableTrial = value;
    }

    public long defaultvalue
    {
      get => this.\u003Cbacking_store\u003Edefaultvalue;
      set => this.\u003Cbacking_store\u003Edefaultvalue = value;
    }

    public long position
    {
      get => this.\u003Cbacking_store\u003Eposition;
      set => this.\u003Cbacking_store\u003Eposition = value;
    }

    public Patch(string version, long pos)
    {
      this.\u003Cbacking_store\u003EVersion = version;
      this.\u003Cbacking_store\u003Eposition = pos;
      this.\u003Cbacking_store\u003EdisableTrial = 40691344807608369L;
      this.\u003Cbacking_store\u003EenableTrial = 40691344824385585L;
      this.\u003Cbacking_store\u003Edefaultvalue = Patch.DefaultPattern;
    }

    public Patch(string version, long disable, long enable, long defaultval, long pos)
    {
      this.\u003Cbacking_store\u003Eposition = pos;
      this.\u003Cbacking_store\u003EVersion = version;
      this.\u003Cbacking_store\u003EdisableTrial = disable;
      this.\u003Cbacking_store\u003EenableTrial = enable;
      this.\u003Cbacking_store\u003Edefaultvalue = defaultval;
    }

    public Patch()
    {
    }
  }
}
