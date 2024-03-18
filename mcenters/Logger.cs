// Decompiled with JetBrains decompiler
// Type: mcenters.Logger
// Assembly: MCenters, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 45CFC87E-86C0-4035-8A46-F8737ED6CA8B
// Assembly location: C:\Users\Misi\Downloads\akshnav_3.exe

using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

#nullable disable
namespace mcenters
{
  public class Logger
  {
    private static bool cleared = true;
    private static Paragraph \u003Cbacking_store\u003EText;
    private static RichTextBox \u003Cbacking_store\u003Elogger;

    private static Paragraph Text
    {
      get => Logger.\u003Cbacking_store\u003EText;
      set => Logger.\u003Cbacking_store\u003EText = value;
    }

    private static RichTextBox logger
    {
      get => Logger.\u003Cbacking_store\u003Elogger;
      set => Logger.\u003Cbacking_store\u003Elogger = value;
    }

    public static void SetLogControl(RichTextBox box)
    {
      Logger.\u003Cbacking_store\u003Elogger = box;
      Logger.\u003Cbacking_store\u003Elogger.Document.Blocks.Clear();
      if (Logger.\u003Cbacking_store\u003EText == null)
        Logger.\u003Cbacking_store\u003EText = new Paragraph();
      Paragraph backingStoreText = Logger.\u003Cbacking_store\u003EText;
      Logger.\u003Cbacking_store\u003Elogger.Document.Blocks.Add((Block) backingStoreText);
    }

    public static void Clear()
    {
      Logger.\u003Cbacking_store\u003EText.Inlines.Clear();
      Logger.cleared = true;
    }

    public static void AddLog(string text, Color color)
    {
      if (Logger.\u003Cbacking_store\u003Elogger == null)
        return;
      Run run;
      if (!Logger.cleared)
      {
        run = new Run("\n" + text);
        run.Foreground = (Brush) new SolidColorBrush(color);
      }
      else
      {
        run = new Run(text);
        run.Foreground = (Brush) new SolidColorBrush(color);
        Logger.cleared = false;
      }
      Logger.\u003Cbacking_store\u003EText.Inlines.Add((Inline) run);
    }

    public static void AddLog(string text)
    {
      Color blue = Colors.Blue;
      if (Logger.\u003Cbacking_store\u003Elogger == null)
        return;
      Run run;
      if (!Logger.cleared)
      {
        run = new Run("\n" + text);
        run.Foreground = (Brush) new SolidColorBrush(blue);
      }
      else
      {
        run = new Run(text);
        run.Foreground = (Brush) new SolidColorBrush(blue);
        Logger.cleared = false;
      }
      Logger.\u003Cbacking_store\u003EText.Inlines.Add((Inline) run);
    }

    public static void AddLog(string field, string value)
    {
      Color blue = Colors.Blue;
      Color black = Colors.Black;
      Run run1 = new Run("\t\t" + value);
      run1.Foreground = (Brush) new SolidColorBrush(black);
      if (Logger.\u003Cbacking_store\u003Elogger == null)
        return;
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
      run2.Foreground = (Brush) new SolidColorBrush(blue);
      Logger.\u003Cbacking_store\u003EText.Inlines.Add((Inline) run2);
      Logger.\u003Cbacking_store\u003EText.Inlines.Add((Inline) run1);
    }

    public static void AddError(string text)
    {
      if (Logger.\u003Cbacking_store\u003Elogger == null)
        return;
      Run run;
      if (!Logger.cleared)
      {
        run = new Run("\n" + text);
        Color red = Colors.Red;
        run.Foreground = (Brush) new SolidColorBrush(red);
      }
      else
      {
        run = new Run(text);
        Color red = Colors.Red;
        run.Foreground = (Brush) new SolidColorBrush(red);
        Logger.cleared = false;
      }
      Logger.\u003Cbacking_store\u003EText.Inlines.Add((Inline) run);
    }
  }
}
