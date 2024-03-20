using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace mcenters
{
	// Token: 0x02000004 RID: 4
	public class Logger
	{
		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600005E RID: 94 RVA: 0x00002790 File Offset: 0x00001B90
		// (set) Token: 0x0600005F RID: 95 RVA: 0x000027A4 File Offset: 0x00001BA4
		private static Paragraph Text
		{
			get
			{
				return Logger.<backing_store>Text;
			}
			set
			{
				Logger.<backing_store>Text = value;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000060 RID: 96 RVA: 0x000027B8 File Offset: 0x00001BB8
		// (set) Token: 0x06000061 RID: 97 RVA: 0x000027CC File Offset: 0x00001BCC
		private static RichTextBox logger
		{
			get
			{
				return Logger.<backing_store>logger;
			}
			set
			{
				Logger.<backing_store>logger = value;
			}
		}

		// Token: 0x06000062 RID: 98 RVA: 0x000027E0 File Offset: 0x00001BE0
		public static void SetLogControl(RichTextBox box)
		{
			Logger.<backing_store>logger = box;
			Logger.<backing_store>logger.Document.Blocks.Clear();
			if (Logger.<backing_store>Text == null)
			{
				Paragraph paragraph = new Paragraph();
				Logger.<backing_store>Text = paragraph;
			}
			Paragraph paragraph2 = Logger.<backing_store>Text;
			Logger.<backing_store>logger.Document.Blocks.Add(paragraph2);
		}

		// Token: 0x06000063 RID: 99 RVA: 0x0000283C File Offset: 0x00001C3C
		public static void Clear()
		{
			Logger.<backing_store>Text.Inlines.Clear();
			Logger.cleared = true;
		}

		// Token: 0x06000064 RID: 100 RVA: 0x0000296C File Offset: 0x00001D6C
		public static void AddLog(string text, Color color)
		{
			if (Logger.<backing_store>logger != null)
			{
				Run run;
				if (!Logger.cleared)
				{
					run = new Run("\n" + text);
					run.Foreground = new SolidColorBrush(color);
				}
				else
				{
					run = new Run(text);
					run.Foreground = new SolidColorBrush(color);
					Logger.cleared = false;
				}
				Logger.<backing_store>Text.Inlines.Add(run);
			}
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00002900 File Offset: 0x00001D00
		public static void AddLog(string text)
		{
			Color blue = Colors.Blue;
			if (Logger.<backing_store>logger != null)
			{
				Run run;
				if (!Logger.cleared)
				{
					run = new Run("\n" + text);
					run.Foreground = new SolidColorBrush(blue);
				}
				else
				{
					run = new Run(text);
					run.Foreground = new SolidColorBrush(blue);
					Logger.cleared = false;
				}
				Logger.<backing_store>Text.Inlines.Add(run);
			}
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00002860 File Offset: 0x00001C60
		public static void AddLog(string field, string value)
		{
			Color blue = Colors.Blue;
			Color black = Colors.Black;
			Run run = new Run("\t\t" + value);
			run.Foreground = new SolidColorBrush(black);
			if (Logger.<backing_store>logger != null)
			{
				Run run2;
				if (!Logger.cleared)
				{
					run2 = new Run("\n" + field);
				}
				else
				{
					run2 = new Run(field);
					Logger.cleared = false;
				}
				FontWeight bold = FontWeights.Bold;
				run2.FontWeight = bold;
				run2.Foreground = new SolidColorBrush(blue);
				Logger.<backing_store>Text.Inlines.Add(run2);
				Logger.<backing_store>Text.Inlines.Add(run);
			}
		}

		// Token: 0x06000067 RID: 103 RVA: 0x000029D0 File Offset: 0x00001DD0
		public static void AddError(string text)
		{
			if (Logger.<backing_store>logger != null)
			{
				Run run;
				if (!Logger.cleared)
				{
					run = new Run("\n" + text);
					Color red = Colors.Red;
					run.Foreground = new SolidColorBrush(red);
				}
				else
				{
					run = new Run(text);
					Color red2 = Colors.Red;
					run.Foreground = new SolidColorBrush(red2);
					Logger.cleared = false;
				}
				Logger.<backing_store>Text.Inlines.Add(run);
			}
		}

		// Token: 0x0400003F RID: 63
		private static bool cleared = true;

		// Token: 0x04000040 RID: 64
		private static Paragraph <backing_store>Text;

		// Token: 0x04000041 RID: 65
		private static RichTextBox <backing_store>logger;
	}
}
