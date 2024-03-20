using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using mcenters;

namespace M_Centers
{
	// Token: 0x02000008 RID: 8
	public class App : Application
	{
		// Token: 0x06000091 RID: 145 RVA: 0x00003790 File Offset: 0x00002B90
		public void InitializeComponent()
		{
			App.x = Application.ResourceAssembly.GetManifestResourceStream("mainwindow.xaml");
			this.action2();
		}

		// Token: 0x06000092 RID: 146 RVA: 0x000030DC File Offset: 0x000024DC
		[STAThread]
		public static void action()
		{
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00003758 File Offset: 0x00002B58
		[STAThread]
		public void action2()
		{
			API.EnableDebug();
			Page page = (Page)XamlReader.Load(App.x);
			App.win = new MainWindow(page);
			page.GetType();
		}

		// Token: 0x06000094 RID: 148 RVA: 0x000037B8 File Offset: 0x00002BB8
		public static void mainlogic()
		{
			App app = new App();
			app.InitializeComponent();
			if (App.win == null)
			{
				do
				{
					Task.Delay(500).Wait();
				}
				while (App.win == null);
			}
			try
			{
				App.x.Close();
			}
			catch (InvalidOperationException)
			{
			}
			app.Run(App.win);
		}

		// Token: 0x04000053 RID: 83
		public static Stream x;

		// Token: 0x04000054 RID: 84
		public static MainWindow win;
	}
}
