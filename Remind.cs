using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Tender_Reminder
{
  public class Remind : Form
  {
    private Main main_f;
    private IContainer components = (IContainer) null;
    public Label label1;
    private Timer anim_timer;
    private Label label2;
    private Label label3;

    public Remind()
    {
      this.InitializeComponent();
      this.anim_timer.Interval = 40;
    }

    public Remind(Main main_f)
    {
      this.InitializeComponent();
      this.anim_timer.Interval = 40;
      this.main_f = main_f;
    }

    private void Remind_Load(object sender, EventArgs e)
    {
      this.anim_timer.Enabled = true;
      this.Left = Screen.PrimaryScreen.WorkingArea.Width - this.Width;
      this.Top = Screen.PrimaryScreen.WorkingArea.Height - this.Height;
    }

    private void anim_timer_Tick(object sender, EventArgs e)
    {
      if (this.Opacity < 1.0)
        this.Opacity += 0.08;
      else
        this.anim_timer.Enabled = false;
    }

    private void Remind_MouseEnter(object sender, EventArgs e) => this.Cursor = Cursors.Hand;

    private void Remind_DoubleClick(object sender, EventArgs e)
    {
      this.main_f.WindowState = FormWindowState.Normal;
      this.main_f.ShowInTaskbar = true;
      this.main_f.Activate();
      this.Close();
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.components = (IContainer) new Container();
      this.label1 = new Label();
      this.anim_timer = new Timer(this.components);
      this.label2 = new Label();
      this.label3 = new Label();
      this.SuspendLayout();
      this.label1.AutoSize = true;
      this.label1.Location = new Point(24, 42);
      this.label1.Name = "label1";
      this.label1.Size = new Size(0, 17);
      this.label1.TabIndex = 0;
      this.anim_timer.Tick += new EventHandler(this.anim_timer_Tick);
      this.label2.AutoSize = true;
      this.label2.Font = new Font("Microsoft Sans Serif", 10.2f, FontStyle.Bold, GraphicsUnit.Point, (byte) 204);
      this.label2.ForeColor = Color.Black;
      this.label2.Location = new Point(107, 22);
      this.label2.Name = "label2";
      this.label2.Size = new Size(179, 20);
      this.label2.TabIndex = 1;
      this.label2.Text = "Automatic reminder!";
      this.label3.AutoSize = true;
      this.label3.Font = new Font("Microsoft Sans Serif", 10.2f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204);
      this.label3.ForeColor = Color.Red;
      this.label3.Location = new Point(134, 59);
      this.label3.Name = "label3";
      this.label3.Size = new Size(117, 20);
      this.label3.TabIndex = 2;
      this.label3.Text = "Check tenders";
      this.AutoScaleDimensions = new SizeF(8f, 16f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(397, 108);
      this.Controls.Add((Control) this.label3);
      this.Controls.Add((Control) this.label2);
      this.Controls.Add((Control) this.label1);
      this.Name = nameof (Remind);
      this.Opacity = 0.0;
      this.Text = "Attention";
      this.TopMost = true;
      this.Load += new EventHandler(this.Remind_Load);
      this.DoubleClick += new EventHandler(this.Remind_DoubleClick);
      this.MouseEnter += new EventHandler(this.Remind_MouseEnter);
      this.ResumeLayout(false);
      this.PerformLayout();
    }
  }
}
