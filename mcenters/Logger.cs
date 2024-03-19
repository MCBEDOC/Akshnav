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
    private static Paragraph <backing_store>Text;
    private static RichTextBox <backing_store>logger;

    private static Paragraph Text
    {
      get => Logger.<backing_store>Text;
      set => Logger.<backing_store>Text = value;
    }

    private static RichTextBox logger
    {
      get => Logger.<backing_store>logger;
      set => Logger.<backing_store>logger = value;
    }

    public static void SetLogControl(RichTextBox box)
    {
      Logger.<backing_store>logger = box;
      Logger.<backing_store>logger.Document.Blocks.Clear();
      if (Logger.<backing_store>Text == null)
        Logger.<backing_store>Text = new Paragraph();
      Paragraph backingStoreText = Logger.<backing_store>Text;
      Logger.<backing_store>logger.Document.Blocks.Add((Block) backingStoreText);
    }

    public static void Clear()
    {
      Logger.<backing_store>Text.Inlines.Clear();
      Logger.cleared = true;
    }

    public static void AddLog(string text, Color color)
    {
      if (Logger.<backing_store>logger == null)
        return;
      Run run;
      if (!Logger.cleared)
      {
        run = new Run("%n" + text);
        run.Foreground = (Brush) new SolidColorBrush(color);
      }
      else
      {
        run = new Run(text);
        run.Foreground = (Brush) new SolidColorBrush(color);
        Logger.cleared = false;
      }
      Logger.<backing_store>Text.Inlines.Add((Inline) run);
    }

    public static void AddLog(string text)
    {
      Color blue = Colors.Blue;
      if (Logger.<backing_store>logger == null)
        return;
      Run run;
      if (!Logger.cleared)
      {
        run = new Run("%n" + text);
        run.Foreground = (Brush) new SolidColorBrush(blue);
      }
      else
      {
        run = new Run(text);
        run.Foreground = (Brush) new SolidColorBrush(blue);
        Logger.cleared = false;
      }
      Logger.<backing_store>Text.Inlines.Add((Inline) run);
    }

    public static void AddLog(string field, string value)
    {
      Color blue = Colors.Blue;
      Color black = Colors.Black;
      Run run1 = new Run("%t%t" + value);
      run1.Foreground = (Brush) new SolidColorBrush(black);
      if (Logger.<backing_store>logger == null)
        return;
      Run run2;
      if (!Logger.cleared)
      {
        run2 = new Run("%n" + field);
      }
      else
      {
        run2 = new Run(field);
        Logger.cleared = false;
      }
      FontWeight bold = FontWeights.Bold;
      run2.FontWeight = bold;
      run2.Foreground = (Brush) new SolidColorBrush(blue);
      Logger.<backing_store>Text.Inlines.Add((Inline) run2);
      Logger.<backing_store>Text.Inlines.Add((Inline) run1);
    }

    public static void AddError(string text)
    {
      if (Logger.<backing_store>logger == null)
        return;
      Run run;
      if (!Logger.cleared)
      {
        run = new Run("%n" + text);
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
      Logger.<backing_store>Text.Inlines.Add((Inline) run);
    }
  }
}
