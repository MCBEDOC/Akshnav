// Decompiled with JetBrains decompiler
// Type: M_Centers.MainWindow
// Assembly: MCenters, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 45CFC87E-86C0-4035-8A46-F8737ED6CA8B
// Assembly location: C:\Users\Misi\Downloads\akshnav_3.exe

using mcenters;
using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;

#nullable disable
namespace M_Centers
{
  public class MainWindow : Window
  {
    internal CheckBox trialBox;
    internal RichTextBox outputBox;
    private bool _contentLoaded;
    public Button InjectButton;
    public RadioButton MinecraftReleaseRadioButton;
    public RadioButton MinecraftBetaRadioButton;
    private bool trial = false;

    public void InitializeComponent()
    {
      if (this._contentLoaded)
        return;
      this._contentLoaded = true;
      Application.LoadComponent((object) this, new Uri("/MCenters;component/mainwindow.xaml", UriKind.Relative));
    }

    public MainWindow(Page p)
    {
      this.ResizeMode = ResizeMode.CanMinimize;
      this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
      this.Background = (Brush) null;
      this.WindowStyle = WindowStyle.None;
      this.AllowsTransparency = true;
      this.Title = "Akshnav";
      this.Height = 584.0;
      this.Width = 757.0;
      this.Content = (object) p;
      this.MinecraftReleaseRadioButton = (RadioButton) p.FindName("minecraftReleaseRadioButton");
      this.MinecraftReleaseRadioButton.Checked += new RoutedEventHandler(this.ReleaseModeSelected);
      this.MinecraftBetaRadioButton = (RadioButton) p.FindName("minecraftBetaRadioButton");
      this.MinecraftBetaRadioButton.Checked += new RoutedEventHandler(this.BetaModeSelected);
      ((UIElement) p.FindName("rootGrid")).MouseDown += new MouseButtonEventHandler(this.Grid_MouseDown);
      Button name1 = (Button) p.FindName(nameof (InjectButton));
      name1.Click += new RoutedEventHandler(this.Button_Click);
      this.InjectButton = name1;
      TextBlock name2 = (TextBlock) p.FindName("closeButton");
      name2.MouseDown += new MouseButtonEventHandler(this.TextBlock_MouseDown);
      name2.MouseEnter += new MouseEventHandler(this.TextBlock_MouseEnter);
      name2.MouseLeave += new MouseEventHandler(this.TextBlock_MouseLeave);
      this.trialBox = (CheckBox) p.FindName(nameof (trialBox));
      this.trialBox.Checked += new RoutedEventHandler(this.CheckBox_Checked);
      this.trialBox.Unchecked += new RoutedEventHandler(this.CheckBox_Unchecked);
      this.outputBox = (RichTextBox) p.FindName(nameof (outputBox));
      Settings settings = Settings.Default;
      ((ButtonBase) p.FindName("ClipSVCButton")).Click += new RoutedEventHandler(this.ClipSvcButton_Click);
      ((ButtonBase) p.FindName("ContactButton")).Click += new RoutedEventHandler(this.ContactButton_Click);
      ((TextBlock) p.FindName("VersionBox")).Text = "Version " + Process.GetCurrentProcess().MainModule.FileVersionInfo.ProductVersion;
      API.SetupVersionList();
      Logger.SetLogControl(this.outputBox);
      string[] commandLineArgs = Environment.GetCommandLineArgs();
      if (commandLineArgs == null)
        return;
      string str = (string) null;
      if (commandLineArgs.Length > 1)
        str = commandLineArgs[1];
      if (string.IsNullOrEmpty(str))
        return;
      if (str == "trial-enable")
      {
        this.trial = true;
        this.trialBox.IsChecked = (bool?) true;
        this.PerformHack();
      }
      if (!(str == "trial-disable"))
        return;
      this.trial = false;
      this.trialBox.IsChecked = (bool?) false;
      this.PerformHack();
    }

    private void ReleaseModeSelected(object sender, RoutedEventArgs e)
    {
      API.ShouldOpenBetaEdition(false);
      GC.KeepAlive((object) this);
    }

    private void BetaModeSelected(object sender, RoutedEventArgs e)
    {
      API.ShouldOpenBetaEdition(true);
      GC.KeepAlive((object) this);
    }

    private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
    {
      Environment.Exit(0);
      GC.KeepAlive((object) this);
    }

    private void TextBlock_MouseEnter(object sender, MouseEventArgs e)
    {
      TextBlock textBlock = (TextBlock) sender;
      textBlock.Background = (Brush) null;
      Color red = Colors.Red;
      textBlock.Foreground = (Brush) new SolidColorBrush(red);
      FontWeight extraBold = FontWeights.ExtraBold;
      textBlock.FontWeight = extraBold;
      GC.KeepAlive((object) this);
    }

    private void TextBlock_MouseLeave(object sender, MouseEventArgs e)
    {
      TextBlock textBlock = (TextBlock) sender;
      Color black = Colors.Black;
      textBlock.Foreground = (Brush) new SolidColorBrush(black);
      Color red = Colors.Red;
      textBlock.Background = (Brush) new SolidColorBrush(red);
      FontWeight bold = FontWeights.Bold;
      textBlock.FontWeight = bold;
      GC.KeepAlive((object) this);
    }

    private void PerformHack()
    {
      Logger.Clear();
      if (this.trial)
        Logger.AddLog("Selected Minecraft Mode = Enable Trial");
      else
        Logger.AddLog("Selected Minecraft Mode = Enable Full Game");
      int num = (int) API.PrepareProcess(this.trial);
      GC.KeepAlive((object) this);
    }

    private void Button_Click(object sender, RoutedEventArgs e) => this.PerformHack();

    private void ContactButton_Click(object sender, RoutedEventArgs e)
    {
      Logger.AddLog("Opening Link", Colors.Goldenrod);
      if (Process.Start("https://x.com/tinedpakgamer") == null)
        Logger.AddLog("Failed to Open", Colors.Red);
      else
        Logger.AddLog("Link Opened", Colors.Green);
      GC.KeepAlive((object) this);
    }

    private void ClipSvcButton_Click(object sender, RoutedEventArgs e)
    {
      Logger.AddLog("Fixing ClipSvc", Colors.Goldenrod);
      try
      {
        RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SYSTEM\\CurrentControlSet\\Services\\ClipSVC\\Parameters", true);
        registryKey.SetValue("ServiceDll", (object) "%SystemRoot%\\System32\\ClipSVC.dll", RegistryValueKind.ExpandString);
        registryKey.Close();
        Logger.AddLog("ClipSvc fixed successfully", Colors.Green);
      }
      catch (Exception ex)
      {
        Color red = Colors.Red;
        int hresult = ex.HResult;
        Logger.AddLog(ex.Message + "\nThe error code is " + hresult.ToString() + "\nThe source is " + ex.Source, red);
      }
      GC.KeepAlive((object) this);
    }

    private void CheckBox_Checked(object sender, RoutedEventArgs e) => this.trial = true;

    private void CheckBox_Unchecked(object sender, RoutedEventArgs e) => this.trial = false;

    private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
    {
      if (e.LeftButton != MouseButtonState.Pressed)
        return;
      this.DragMove();
    }
  }
}
