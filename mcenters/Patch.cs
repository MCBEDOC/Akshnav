using System;

#nullable disable
namespace mcenters
{
  [Serializable]
  public class Patch
  {
    private string <backing_store>Version;
    private long <backing_store>disableTrial;
    public static long DefaultPattern = 4974556687;
    private long <backing_store>enableTrial;
    private long <backing_store>defaultvalue;
    private long <backing_store>position;

    public string Version
    {
      get => this.<backing_store>Version;
      set => this.<backing_store>Version = value;
    }

    public long disableTrial
    {
      get => this.<backing_store>disableTrial;
      set => this.<backing_store>disableTrial = value;
    }

    public long enableTrial
    {
      get => this.<backing_store>enableTrial;
      set => this.<backing_store>enableTrial = value;
    }

    public long defaultvalue
    {
      get => this.<backing_store>defaultvalue;
      set => this.<backing_store>defaultvalue = value;
    }

    public long position
    {
      get => this.<backing_store>position;
      set => this.<backing_store>position = value;
    }

    public Patch(string version, long pos)
    {
      this.<backing_store>Version = version;
      this.<backing_store>position = pos;
      this.<backing_store>disableTrial = 40691344807608369L;
      this.<backing_store>enableTrial = 40691344824385585L;
      this.<backing_store>defaultvalue = Patch.DefaultPattern;
    }

    public Patch(string version, long disable, long enable, long defaultval, long pos)
    {
      this.<backing_store>position = pos;
      this.<backing_store>Version = version;
      this.<backing_store>disableTrial = disable;
      this.<backing_store>enableTrial = enable;
      this.<backing_store>defaultvalue = defaultval;
    }

    public Patch()
    {
    }
  }
}
