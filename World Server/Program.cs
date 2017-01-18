using System;
using System.Drawing;
using System.Windows.Forms;

namespace World_Server
{
    public static class RichTextBoxExtensions
    {
        public static void AppendLine(this RichTextBox box, string text, Color? color = null)
        {
            box.SelectionStart = box.TextLength;
            box.SelectionColor = color ?? Color.Black;
            box.AppendText($"[ {DateTime.Now} ] {text} \r\n");
            box.SelectionColor = box.ForeColor;
            box.ScrollToCaret();
        }
    }

    class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Main());
        }
    }
}
