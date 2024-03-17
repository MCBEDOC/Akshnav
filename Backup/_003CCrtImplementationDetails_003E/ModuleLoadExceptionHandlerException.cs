// Decompiled with JetBrains decompiler
// Type: <CrtImplementationDetails>.ModuleLoadExceptionHandlerException
// Assembly: MCenters, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 45CFC87E-86C0-4035-8A46-F8737ED6CA8B
// Assembly location: C:\Users\Misi\Downloads\akshnav_3.exe

using System;
using System.Runtime.Serialization;
using System.Security;

#nullable disable
namespace \u003CCrtImplementationDetails\u003E
{
  [Serializable]
  internal class ModuleLoadExceptionHandlerException : ModuleLoadException
  {
    private const string formatString = "\n{0}: {1}\n--- Start of primary exception ---\n{2}\n--- End of primary exception ---\n\n--- Start of nested exception ---\n{3}\n--- End of nested exception ---\n";
    private Exception \u003Cbacking_store\u003ENestedException;

    protected ModuleLoadExceptionHandlerException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
      Type type = typeof (Exception);
      string name = nameof (NestedException);
      this.NestedException = (Exception) info.GetValue(name, type);
      GC.KeepAlive((object) this);
    }

    public ModuleLoadExceptionHandlerException(
      string message,
      Exception innerException,
      Exception nestedException)
      : base(message, innerException)
    {
      this.NestedException = nestedException;
    }

    public Exception NestedException
    {
      get => this.\u003Cbacking_store\u003ENestedException;
      set => this.\u003Cbacking_store\u003ENestedException = value;
    }

    public override string ToString()
    {
      string str1 = this.InnerException == null ? string.Empty : this.InnerException.ToString();
      string str2 = this.NestedException == null ? string.Empty : this.NestedException.ToString();
      object[] objArray = new object[4];
      Type type = this.GetType();
      objArray[0] = (object) type;
      string str3 = this.Message == null ? string.Empty : this.Message;
      objArray[1] = (object) str3;
      string str4 = str1 == null ? string.Empty : str1;
      objArray[2] = (object) str4;
      string str5 = str2 == null ? string.Empty : str2;
      objArray[3] = (object) str5;
      string str6 = string.Format("\n{0}: {1}\n--- Start of primary exception ---\n{2}\n--- End of primary exception ---\n\n--- Start of nested exception ---\n{3}\n--- End of nested exception ---\n", objArray);
      GC.KeepAlive((object) this);
      return str6;
    }

    [SecurityCritical]
    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
      base.GetObjectData(info, context);
      Type type = typeof (Exception);
      Exception nestedException = this.NestedException;
      info.AddValue("NestedException", (object) nestedException, type);
      GC.KeepAlive((object) this);
    }
  }
}
