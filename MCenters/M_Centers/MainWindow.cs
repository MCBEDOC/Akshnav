using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using mcenters;
using Microsoft.Win32;

namespace M_Centers
{
	// Token: 0x02000007 RID: 7
	public class MainWindow : Window
	{
		// Token: 0x06000083 RID: 131 RVA: 0x00002E68 File Offset: 0x00002268
		public void InitializeComponent()
		{
			if (!this._contentLoaded)
			{
				this._contentLoaded = true;
				Uri uri = new Uri("/MCenters;component/mainwindow.xaml", UriKind.Relative);
				Application.LoadComponent(this, uri);
			}
		}

		// Token: 0x06000084 RID: 132 RVA: 0x000034A0 File Offset: 0x000028A0
		public MainWindow(Page p)
		{
			base.ResizeMode = ResizeMode.CanMinimize;
			base.WindowStartupLocation = WindowStartupLocation.CenterScreen;
			base.Background = null;
			base.WindowStyle = WindowStyle.None;
			base.AllowsTransparency = true;
			this.Title = "Akshnav";
			this.Height = 584.0;
			this.Width = 757.0;
			this.Content = p;
			this.MinecraftReleaseRadioButton = (RadioButton)p.FindName("minecraftReleaseRadioButton");
			this.MinecraftReleaseRadioButton.Checked += this.ReleaseModeSelected;
			this.MinecraftBetaRadioButton = (RadioButton)p.FindName("minecraftBetaRadioButton");
			this.MinecraftBetaRadioButton.Checked += this.BetaModeSelected;
			((Grid)p.FindName("rootGrid")).MouseDown += this.Grid_MouseDown;
			Button button = (Button)p.FindName("InjectButton");
			button.Click += this.Button_Click;
			this.InjectButton = button;
			TextBlock textBlock = (TextBlock)p.FindName("closeButton");
			textBlock.MouseDown += this.TextBlock_MouseDown;
			textBlock.MouseEnter += this.TextBlock_MouseEnter;
			textBlock.MouseLeave += this.TextBlock_MouseLeave;
			this.trialBox = (CheckBox)p.FindName("trialBox");
			this.trialBox.Checked += this.CheckBox_Checked;
			this.trialBox.Unchecked += this.CheckBox_Unchecked;
			this.outputBox = (RichTextBox)p.FindName("outputBox");
			Settings @default = Settings.Default;
			((Button)p.FindName("ClipSVCButton")).Click += this.ClipSvcButton_Click;
			((Button)p.FindName("ContactButton")).Click += this.ContactButton_Click;
			((TextBlock)p.FindName("VersionBox")).Text = "Version " + Process.GetCurrentProcess().MainModule.FileVersionInfo.ProductVersion;
			API.SetupVersionList();
			Logger.SetLogControl(this.outputBox);
			string[] commandLineArgs = Environment.GetCommandLineArgs();
			if (commandLineArgs != null)
			{
				string text = null;
				if (commandLineArgs.Length > 1)
				{
					text = commandLineArgs[1];
				}
				if (!string.IsNullOrEmpty(text))
				{
					if (text == "trial-enable")
					{
						this.trial = true;
						bool? flag = true;
						this.trialBox.IsChecked = flag;
						this.PerformHack();
					}
					if (text == "trial-disable")
					{
						this.trial = false;
						bool? flag2 = false;
						this.trialBox.IsChecked = flag2;
						this.PerformHack();
					}
				}
			}
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00002E98 File Offset: 0x00002298
		private void ReleaseModeSelected(object sender, RoutedEventArgs e)
		{
			API.ShouldOpenBetaEdition(false);
			GC.KeepAlive(this);
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00002EB4 File Offset: 0x000022B4
		private void BetaModeSelected(object sender, RoutedEventArgs e)
		{
			API.ShouldOpenBetaEdition(true);
			GC.KeepAlive(this);
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00002ED0 File Offset: 0x000022D0
		private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
		{
			Environment.Exit(0);
			GC.KeepAlive(this);
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00002EEC File Offset: 0x000022EC
		private void TextBlock_MouseEnter(object sender, MouseEventArgs e)
		{
			TextBlock textBlock = (TextBlock)sender;
			textBlock.Background = null;
			Color red = Colors.Red;
			textBlock.Foreground = new SolidColorBrush(red);
			FontWeight extraBold = FontWeights.ExtraBold;
			textBlock.FontWeight = extraBold;
			GC.KeepAlive(this);
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00002F2C File Offset: 0x0000232C
		private void TextBlock_MouseLeave(object sender, MouseEventArgs e)
		{
			TextBlock textBlock = (TextBlock)sender;
			Color black = Colors.Black;
			textBlock.Foreground = new SolidColorBrush(black);
			Color red = Colors.Red;
			textBlock.Background = new SolidColorBrush(red);
			FontWeight bold = FontWeights.Bold;
			textBlock.FontWeight = bold;
			GC.KeepAlive(this);
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00003448 File Offset: 0x00002848
		private void PerformHack()
		{
			Logger.Clear();
			if (this.trial)
			{
				Logger.AddLog("Selected Minecraft Mode = Enable Trial");
			}
			else
			{
				Logger.AddLog("Selected Minecraft Mode = Enable Full Game");
			}
			API.PrepareProcess(this.trial);
			GC.KeepAlive(this);
		}

		// Token: 0x0600008B RID: 139 RVA: 0x0000348C File Offset: 0x0000288C
		private void Button_Click(object sender, RoutedEventArgs e)
		{
			this.PerformHack();
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00002F78 File Offset: 0x00002378
		private void ContactButton_Click(object sender, RoutedEventArgs e)
		{
			Color goldenrod = Colors.Goldenrod;
			Logger.AddLog("Opening Link", goldenrod);
			if (Process.Start("https://x.com/tinedpakgamer") == null)
			{
				Color red = Colors.Red;
				Logger.AddLog("Failed to Open", red);
			}
			else
			{
				Color green = Colors.Green;
				Logger.AddLog("Link Opened", green);
			}
			GC.KeepAlive(this);
		}

		// Token: 0x0600008D RID: 141 RVA: 0x00002FCC File Offset: 0x000023CC
		private void ClipSvcButton_Click(object sender, RoutedEventArgs e)
		{
			Color goldenrod = Colors.Goldenrod;
			Logger.AddLog("Fixing ClipSvc", goldenrod);
			try
			{
				RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SYSTEM\\CurrentControlSet\\Services\\ClipSVC\\Parameters", true);
				registryKey.SetValue("ServiceDll", "%SystemRoot%\\System32\\ClipSVC.dll", RegistryValueKind.ExpandString);
				registryKey.Close();
				Color green = Colors.Green;
				Logger.AddLog("ClipSvc fixed successfully", green);
			}
			catch (Exception ex)
			{
				Color red = Colors.Red;
				int hresult = ex.HResult;
				Logger.AddLog(ex.Message + "\nThe error code is " + hresult.ToString() + "\nThe source is " + ex.Source, red);
			}
			GC.KeepAlive(this);
		}

		// Token: 0x0600008E RID: 142 RVA: 0x00003098 File Offset: 0x00002498
		private void CheckBox_Checked(object sender, RoutedEventArgs e)
		{
			this.trial = true;
		}

		// Token: 0x0600008F RID: 143 RVA: 0x000030AC File Offset: 0x000024AC
		private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
		{
			this.trial = false;
		}

		// Token: 0x06000090 RID: 144 RVA: 0x000030C0 File Offset: 0x000024C0
		private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
		{
			if (e.LeftButton == MouseButtonState.Pressed)
			{
				base.DragMove();
			}
		}

		// Token: 0x0400004C RID: 76
		internal CheckBox trialBox;

		// Token: 0x0400004D RID: 77
		internal RichTextBox outputBox;

		// Token: 0x0400004E RID: 78
		private bool _contentLoaded;

		// Token: 0x0400004F RID: 79
		public Button InjectButton;

		// Token: 0x04000050 RID: 80
		public RadioButton MinecraftReleaseRadioButton;

		// Token: 0x04000051 RID: 81
		public RadioButton MinecraftBetaRadioButton;

		// Token: 0x04000052 RID: 82
		private bool trial = false;
	}
}
