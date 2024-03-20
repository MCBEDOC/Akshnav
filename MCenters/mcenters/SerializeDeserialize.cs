using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace mcenters
{
	// Token: 0x02000002 RID: 2
	internal class SerializeDeserialize<T>
	{
		// Token: 0x06000055 RID: 85 RVA: 0x0000261C File Offset: 0x00001A1C
		public SerializeDeserialize()
		{
			this.sbData = new StringBuilder(500);
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00002640 File Offset: 0x00001A40
		public string SerializeData(T data)
		{
			XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
			StringWriter stringWriter = new StringWriter(this.sbData);
			this.swWriter = stringWriter;
			xmlSerializer.Serialize(stringWriter, data);
			string text = this.sbData.ToString();
			GC.KeepAlive(this);
			return text;
		}

		// Token: 0x06000057 RID: 87 RVA: 0x0000268C File Offset: 0x00001A8C
		public T DeserializeData(string dataXML)
		{
			XmlDocument xmlDocument = new XmlDocument();
			this.xDoc = xmlDocument;
			xmlDocument.LoadXml(dataXML);
			this.xNodeReader = new XmlNodeReader(this.xDoc.DocumentElement);
			XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
			this.xmlSerializer = xmlSerializer;
			T t = (T)((object)xmlSerializer.Deserialize(this.xNodeReader));
			GC.KeepAlive(this);
			return t;
		}

		// Token: 0x04000038 RID: 56
		private StringBuilder sbData;

		// Token: 0x04000039 RID: 57
		private StringWriter swWriter;

		// Token: 0x0400003A RID: 58
		private XmlDocument xDoc;

		// Token: 0x0400003B RID: 59
		private XmlNodeReader xNodeReader;

		// Token: 0x0400003C RID: 60
		private XmlSerializer xmlSerializer;
	}
}
