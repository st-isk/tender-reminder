using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace Tender_Reminder
{
  public class Main : Form
  {
    private Info[] infoarr;
    private IContainer components = (IContainer) null;
    private ComboBox inflist;
    private NotifyIcon notifyIcon1;
    private ContextMenuStrip contextMenuStrip1;
    private ToolStripMenuItem exit_but;
    private ToolStripSeparator toolStripSeparator1;
    private Timer rem_timer;
    private ToolStripMenuItem open_win_but;
    private Button plus_inf;
    private MenuStrip menuStrip1;
    private MenuStrip menuStrip2;
    private TextBox ten_inf_box;
    private Label comp_label;

    public Main() => this.InitializeComponent();

    public Main(bool state_flag)
    {
      this.InitializeComponent();
      if (state_flag)
      {
        this.ShowInTaskbar = false;
        this.notifyIcon1.Visible = true;
        this.WindowState = FormWindowState.Minimized;
      }
      this.rem_timer.Interval = 30000;
    }

    private void file_sel_Click(object sender, EventArgs e)
    {
      string path = "";
      OpenFileDialog openFileDialog = new OpenFileDialog();
      if (openFileDialog.ShowDialog() == DialogResult.OK)
        path = openFileDialog.FileName;
      string[] strArray1 = new string[File.ReadAllLines(path).Length];
      Info[] infoArray = new Info[strArray1.Length];
      try
      {
        using (StreamReader streamReader = new StreamReader(path, Encoding.Default))
        {
          for (int index = 0; index < strArray1.Length; ++index)
            strArray1[index] = streamReader.ReadLine();
        }
      }
      catch (Exception ex)
      {
        throw new Exception(ex.Message);
      }
      for (int index = 0; index < strArray1.Length; ++index)
      {
        string[] strArray2 = new string[3];
        string[] strArray3 = strArray1[index].Split(new char[1]
        {
          '/'
        }, StringSplitOptions.RemoveEmptyEntries);
        infoArray[index] = new Info()
        {
          Com_Name = strArray3[0],
          TenderInf = strArray3[1]
        };
        this.inflist.Items.AddRange((object[]) new string[1]
        {
          infoArray[index].Com_Name + " " + infoArray[index].TenderInf
        });
      }
      int num = 1;
      try
      {
        using (StreamWriter streamWriter = new StreamWriter(Application.StartupPath + "\\data.json", false, Encoding.Default))
        {
          for (int index = 0; index < infoArray.Length; ++index)
          {
            if (num == 1)
            {
              streamWriter.Write("[");
              streamWriter.WriteLine();
            }
            streamWriter.Write(JsonConvert.SerializeObject((object) infoArray[index]));
            if (num != infoArray.Length)
            {
              streamWriter.Write(",");
              streamWriter.WriteLine();
            }
            else
            {
              streamWriter.WriteLine();
              streamWriter.Write("]");
            }
            ++num;
          }
        }
      }
      catch (Exception ex)
      {
        throw new Exception(ex.Message);
      }
    }

    private void plus_inf_Click(object sender, EventArgs e)
    {
      int num = (int) new Add_info().ShowDialog();
    }

    private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
    {
      this.WindowState = FormWindowState.Normal;
      this.ShowInTaskbar = true;
      this.Activate();
    }

    private void Main_FormClosing(object sender, FormClosingEventArgs e)
    {
      e.Cancel = true;
      if (!this.notifyIcon1.Visible)
        this.notifyIcon1.Visible = true;
      this.ShowInTaskbar = false;
      this.WindowState = FormWindowState.Minimized;
    }

    private void exit_but_Click(object sender, EventArgs e) => Environment.Exit(0);

    private void Main_Load(object sender, EventArgs e) => this.rem_timer.Enabled = true;

    private void rem_timer_Tick(object sender, EventArgs e) => new Remind(this).Show();

    private void open_win_but_Click(object sender, EventArgs e)
    {
      this.WindowState = FormWindowState.Normal;
      this.ShowInTaskbar = true;
      this.Activate();
    }

    private void inflist_sv(object sender, EventArgs e)
    {
      ComboBox comboBox = (ComboBox) sender;
      for (int index = 0; index < this.infoarr.Length; ++index)
      {
        if (comboBox.SelectedItem.ToString() == this.infoarr[index].Com_Name)
          this.ten_inf_box.Text = this.infoarr[index].TenderInf;
      }
    }

    private void inflist_DropDown(object sender, EventArgs e)
    {
      this.inflist.Items.Clear();
      string end;
      try
      {
        using (StreamReader streamReader = new StreamReader(Application.StartupPath + "\\data.json", Encoding.Default))
          end = streamReader.ReadToEnd();
      }
      catch (Exception ex)
      {
        throw new Exception(ex.Message);
      }
      this.infoarr = JsonConvert.DeserializeObject<Info[]>(end);
      for (int index = 0; index < this.infoarr.Length; ++index)
        this.inflist.Items.AddRange((object[]) new string[1]
        {
          this.infoarr[index].Com_Name
        });
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
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (Main));
      this.inflist = new ComboBox();
      this.notifyIcon1 = new NotifyIcon(this.components);
      this.contextMenuStrip1 = new ContextMenuStrip(this.components);
      this.exit_but = new ToolStripMenuItem();
      this.toolStripSeparator1 = new ToolStripSeparator();
      this.open_win_but = new ToolStripMenuItem();
      this.rem_timer = new Timer(this.components);
      this.plus_inf = new Button();
      this.menuStrip1 = new MenuStrip();
      this.menuStrip2 = new MenuStrip();
      this.ten_inf_box = new TextBox();
      this.comp_label = new Label();
      this.contextMenuStrip1.SuspendLayout();
      this.SuspendLayout();
      this.inflist.DropDownHeight = 300;
      this.inflist.DropDownStyle = ComboBoxStyle.DropDownList;
      this.inflist.FormattingEnabled = true;
      this.inflist.IntegralHeight = false;
      this.inflist.Location = new Point(9, 34);
      this.inflist.Margin = new Padding(2);
      this.inflist.MaxDropDownItems = 20;
      this.inflist.Name = "inflist";
      this.inflist.Size = new Size(414, 21);
      this.inflist.TabIndex = 2;
      this.inflist.DropDown += new EventHandler(this.inflist_DropDown);
      this.inflist.SelectionChangeCommitted += new EventHandler(this.inflist_sv);
      this.notifyIcon1.ContextMenuStrip = this.contextMenuStrip1;
      this.notifyIcon1.Icon = (Icon) componentResourceManager.GetObject("notifyIcon1.Icon");
      this.notifyIcon1.Text = "Tender Reminder";
      this.notifyIcon1.MouseDoubleClick += new MouseEventHandler(this.notifyIcon1_MouseDoubleClick);
      this.contextMenuStrip1.ImageScalingSize = new Size(20, 20);
      this.contextMenuStrip1.Items.AddRange(new ToolStripItem[3]
      {
        (ToolStripItem) this.exit_but,
        (ToolStripItem) this.toolStripSeparator1,
        (ToolStripItem) this.open_win_but
      });
      this.contextMenuStrip1.Name = "contextMenuStrip1";
      this.contextMenuStrip1.Size = new Size(166, 54);
      this.exit_but.Name = "exit_but";
      this.exit_but.Size = new Size(165, 22);
      this.exit_but.Text = "Выход";
      this.exit_but.Click += new EventHandler(this.exit_but_Click);
      this.toolStripSeparator1.Name = "toolStripSeparator1";
      this.toolStripSeparator1.Size = new Size(162, 6);
      this.open_win_but.Name = "open_win_but";
      this.open_win_but.Size = new Size(165, 22);
      this.open_win_but.Text = "Развернуть окно";
      this.open_win_but.Click += new EventHandler(this.open_win_but_Click);
      this.rem_timer.Tick += new EventHandler(this.rem_timer_Tick);
      this.plus_inf.Font = new Font("Microsoft Sans Serif", 10.8f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204);
      this.plus_inf.Location = new Point(426, 28);
      this.plus_inf.Margin = new Padding(2);
      this.plus_inf.Name = "plus_inf";
      this.plus_inf.Size = new Size(35, 32);
      this.plus_inf.TabIndex = 6;
      this.plus_inf.Text = "+";
      this.plus_inf.UseVisualStyleBackColor = true;
      this.plus_inf.Click += new EventHandler(this.plus_inf_Click);
      this.menuStrip1.BackColor = SystemColors.Control;
      this.menuStrip1.ImageScalingSize = new Size(20, 20);
      this.menuStrip1.Location = new Point(0, 24);
      this.menuStrip1.Name = "menuStrip1";
      this.menuStrip1.Padding = new Padding(4, 2, 0, 2);
      this.menuStrip1.Size = new Size(475, 24);
      this.menuStrip1.TabIndex = 0;
      this.menuStrip1.Text = "menuStrip1";
      this.menuStrip2.BackColor = SystemColors.Control;
      this.menuStrip2.ImageScalingSize = new Size(20, 20);
      this.menuStrip2.Location = new Point(0, 0);
      this.menuStrip2.Name = "menuStrip2";
      this.menuStrip2.Padding = new Padding(4, 2, 0, 2);
      this.menuStrip2.Size = new Size(475, 24);
      this.menuStrip2.TabIndex = 1;
      this.menuStrip2.Text = "menuStrip2";
      this.ten_inf_box.BackColor = SystemColors.Window;
      this.ten_inf_box.ForeColor = SystemColors.WindowText;
      this.ten_inf_box.Location = new Point(9, 98);
      this.ten_inf_box.Margin = new Padding(2);
      this.ten_inf_box.Multiline = true;
      this.ten_inf_box.Name = "ten_inf_box";
      this.ten_inf_box.ReadOnly = true;
      this.ten_inf_box.Size = new Size(453, 251);
      this.ten_inf_box.TabIndex = 7;
      this.comp_label.AutoSize = true;
      this.comp_label.Location = new Point(9, 70);
      this.comp_label.Margin = new Padding(2, 0, 2, 0);
      this.comp_label.Name = "comp_label";
      this.comp_label.Size = new Size(0, 13);
      this.comp_label.TabIndex = 8;
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(475, 366);
      this.Controls.Add((Control) this.comp_label);
      this.Controls.Add((Control) this.ten_inf_box);
      this.Controls.Add((Control) this.plus_inf);
      this.Controls.Add((Control) this.inflist);
      this.Controls.Add((Control) this.menuStrip1);
      this.Controls.Add((Control) this.menuStrip2);
      this.MainMenuStrip = this.menuStrip1;
      this.Margin = new Padding(2);
      this.Name = nameof (Main);
      this.Text = "Workspace";
      this.FormClosing += new FormClosingEventHandler(this.Main_FormClosing);
      this.Load += new EventHandler(this.Main_Load);
      this.contextMenuStrip1.ResumeLayout(false);
      this.ResumeLayout(false);
      this.PerformLayout();
    }
  }
}
