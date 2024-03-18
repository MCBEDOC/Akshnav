// Decompiled with JetBrains decompiler
// Type: M_Centers.App
// Assembly: MCenters, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 45CFC87E-86C0-4035-8A46-F8737ED6CA8B
// Assembly location: C:\Users\Misi\Downloads\akshnav_3.exe

using mcenters;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

#nullable disable
namespace M_Centers
{
  public class App : Application
  {
    public static Stream x;
    public static M_Centers.MainWindow win;

    public void InitializeComponent()
    {
      App.x = Application.ResourceAssembly.GetManifestResourceStream("mainwindow.xaml");
      this.action2();
    }

    [STAThread]
    public static void action()
    {
    }

    [STAThread]
    public void action2()
    {
      API.EnableDebug();
      Page p = (Page) XamlReader.Load(App.x);
      App.win = new M_Centers.MainWindow(p);
      p.GetType();
    }

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
      catch (InvalidOperationException ex)
      {
      }
      app.Run((Window) App.win);
    }
  }
}
