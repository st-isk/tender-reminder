using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace Tender_Reminder
{
  public class Add_info : Form
  {
    private IContainer components = (IContainer) null;
    private Label label1;
    private TextBox comp_name;
    private TextBox tender_inf;
    private Label label2;
    private Button add_inf_but;

    public Add_info() => this.InitializeComponent();

    private void add_inf_but_Click(object sender, EventArgs e)
    {
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
      List<Info> infoList1 = new List<Info>();
      Info info = new Info();
      info.Com_Name = this.comp_name.Text;
      info.TenderInf = this.tender_inf.Text;
      List<Info> infoList2 = JsonConvert.DeserializeObject<List<Info>>(end);
      infoList2.Add(info);
      int num = 1;
      try
      {
        using (StreamWriter streamWriter = new StreamWriter(Application.StartupPath + "\\data.json", false, Encoding.Default))
        {
          for (int index = 0; index < infoList2.Count; ++index)
          {
            if (num == 1)
            {
              streamWriter.Write("[");
              streamWriter.WriteLine();
            }
            streamWriter.Write(JsonConvert.SerializeObject((object) infoList2[index]));
            if (num != infoList2.Count)
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
      this.label1 = new Label();
      this.comp_name = new TextBox();
      this.tender_inf = new TextBox();
      this.label2 = new Label();
      this.add_inf_but = new Button();
      this.SuspendLayout();
      this.label1.AutoSize = true;
      this.label1.Font = new Font("Microsoft Sans Serif", 10.8f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204);
      this.label1.Location = new Point(12, 23);
      this.label1.Name = "label1";
      this.label1.Size = new Size(139, 48);
      this.label1.TabIndex = 0;
      this.label1.Text = "The name \r\nof the company";
      this.comp_name.Location = new Point(159, 35);
      this.comp_name.Name = "comp_name";
      this.comp_name.Size = new Size(403, 22);
      this.comp_name.TabIndex = 1;
      this.tender_inf.Location = new Point(159, 93);
      this.tender_inf.Multiline = true;
      this.tender_inf.Name = "tender_inf";
      this.tender_inf.ScrollBars = ScrollBars.Both;
      this.tender_inf.Size = new Size(456, 226);
      this.tender_inf.TabIndex = 2;
      this.label2.AutoSize = true;
      this.label2.Font = new Font("Microsoft Sans Serif", 10.8f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204);
      this.label2.Location = new Point(12, 91);
      this.label2.Name = "label2";
      this.label2.Size = new Size(116, 48);
      this.label2.TabIndex = 3;
      this.label2.Text = "Information \r\nabout tender";
      this.add_inf_but.Location = new Point(555, 341);
      this.add_inf_but.Name = "add_inf_but";
      this.add_inf_but.Size = new Size(88, 27);
      this.add_inf_but.TabIndex = 4;
      this.add_inf_but.Text = "ОК";
      this.add_inf_but.UseVisualStyleBackColor = true;
      this.add_inf_but.Click += new EventHandler(this.add_inf_but_Click);
      this.AutoScaleDimensions = new SizeF(8f, 16f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(655, 380);
      this.Controls.Add((Control) this.add_inf_but);
      this.Controls.Add((Control) this.label2);
      this.Controls.Add((Control) this.tender_inf);
      this.Controls.Add((Control) this.comp_name);
      this.Controls.Add((Control) this.label1);
      this.MaximizeBox = false;
      this.Name = nameof (Add_info);
      this.Text = "Add new information";
      this.ResumeLayout(false);
      this.PerformLayout();
    }
  }
}
