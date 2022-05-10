using System;
using System.Windows.Forms;

namespace Tender_Reminder
{
  internal static class Program
  {
    [STAThread]
    private static void Main()
    {
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);
      Application.Run((Form) new Tender_Reminder.Main(true));
    }
  }
}
