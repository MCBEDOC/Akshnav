// Decompiled with JetBrains decompiler
// Type: mcenters.SerializeDeserialize`1
// Assembly: MCenters, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 45CFC87E-86C0-4035-8A46-F8737ED6CA8B
// Assembly location: C:\Users\Misi\Downloads\akshnav_3.exe

using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

#nullable disable
namespace mcenters
{
  internal class SerializeDeserialize<T>
  {
    private StringBuilder sbData;
    private StringWriter swWriter;
    private XmlDocument xDoc;
    private XmlNodeReader xNodeReader;
    private XmlSerializer xmlSerializer;

    public SerializeDeserialize() => this.sbData = new StringBuilder(500);

    public string SerializeData(T data)
    {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof (T));
      StringWriter stringWriter1 = new StringWriter(this.sbData);
      this.swWriter = stringWriter1;
      StringWriter stringWriter2 = stringWriter1;
      // ISSUE: variable of a boxed type
      __Boxed<T> o = (object) data;
      xmlSerializer.Serialize((TextWriter) stringWriter2, (object) o);
      string str = this.sbData.ToString();
      GC.KeepAlive((object) this);
      return str;
    }

    public T DeserializeData(string dataXML)
    {
      XmlDocument xmlDocument = new XmlDocument();
      this.xDoc = xmlDocument;
      xmlDocument.LoadXml(dataXML);
      SerializeDeserialize<T> serializeDeserialize = this;
      serializeDeserialize.xNodeReader = new XmlNodeReader((XmlNode) serializeDeserialize.xDoc.DocumentElement);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof (T));
      this.xmlSerializer = xmlSerializer;
      T obj = (T) xmlSerializer.Deserialize((XmlReader) this.xNodeReader);
      GC.KeepAlive((object) this);
      return obj;
    }
  }
}
