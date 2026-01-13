namespace ReserK
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            groupBox1 = new GroupBox();
            knt = new TextBox();
            label7 = new Label();
            label4 = new Label();
            label3 = new Label();
            Fdt = new TextBox();
            Ldt = new TextBox();
            label2 = new Label();
            label1 = new Label();
            toolStrip1 = new ToolStrip();
            SaveData_button = new ToolStripButton();
            OpenData_button = new ToolStripButton();
            Execute_button = new ToolStripButton();
            Help_button = new ToolStripButton();
            SaveResults_button = new ToolStripButton();
            SaveData = new SaveFileDialog();
            SaveResults = new SaveFileDialog();
            OpenData = new OpenFileDialog();
            groupBox2 = new GroupBox();
            groupBox4 = new GroupBox();
            label19 = new Label();
            Fnkt = new TextBox();
            label14 = new Label();
            label15 = new Label();
            Znnkt = new TextBox();
            label16 = new Label();
            label17 = new Label();
            Zvnkt = new TextBox();
            label18 = new Label();
            groupBox3 = new GroupBox();
            mvodt = new TextBox();
            label13 = new Label();
            label11 = new Label();
            Bvodt = new TextBox();
            Bvod = new Label();
            label9 = new Label();
            Zvodt = new TextBox();
            label10 = new Label();
            krt = new TextBox();
            label5 = new Label();
            label6 = new Label();
            Frt = new TextBox();
            label8 = new Label();
            groupBox5 = new GroupBox();
            label20 = new Label();
            label21 = new Label();
            Trast = new TextBox();
            dtt = new TextBox();
            label22 = new Label();
            label23 = new Label();
            groupBox6 = new GroupBox();
            dataGridView_discharge = new DataGridView();
            label26 = new Label();
            label27 = new Label();
            formsPlot_L = new ScottPlot.WinForms.FormsPlot();
            formsPlot_Q = new ScottPlot.WinForms.FormsPlot();
            groupBox7 = new GroupBox();
            Volume = new Label();
            Elevation = new Label();
            groupBox1.SuspendLayout();
            toolStrip1.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox4.SuspendLayout();
            groupBox3.SuspendLayout();
            groupBox5.SuspendLayout();
            groupBox6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView_discharge).BeginInit();
            groupBox7.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(knt);
            groupBox1.Controls.Add(label7);
            groupBox1.Controls.Add(label4);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(Fdt);
            groupBox1.Controls.Add(Ldt);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(label1);
            groupBox1.Location = new Point(10, 21);
            groupBox1.Margin = new Padding(3, 2, 3, 2);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new Padding(3, 2, 3, 2);
            groupBox1.Size = new Size(253, 112);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "Деривация";
            // 
            // knt
            // 
            knt.Location = new Point(170, 83);
            knt.Margin = new Padding(3, 2, 3, 2);
            knt.Name = "knt";
            knt.Size = new Size(50, 23);
            knt.TabIndex = 3;
            knt.TextAlign = HorizontalAlignment.Right;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(5, 74);
            label7.Name = "label7";
            label7.Size = new Size(103, 30);
            label7.TabIndex = 6;
            label7.Text = "Коэффициент\r\nшероховатости n";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(224, 53);
            label4.Name = "label4";
            label4.Size = new Size(20, 15);
            label4.TabIndex = 5;
            label4.Text = "м²";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(224, 28);
            label3.Name = "label3";
            label3.Size = new Size(16, 15);
            label3.TabIndex = 4;
            label3.Text = "м";
            // 
            // Fdt
            // 
            Fdt.Location = new Point(170, 51);
            Fdt.Margin = new Padding(3, 2, 3, 2);
            Fdt.Name = "Fdt";
            Fdt.Size = new Size(50, 23);
            Fdt.TabIndex = 2;
            Fdt.TextAlign = HorizontalAlignment.Right;
            Fdt.TextChanged += Fdt_TextChanged;
            // 
            // Ldt
            // 
            Ldt.Location = new Point(170, 26);
            Ldt.Margin = new Padding(3, 2, 3, 2);
            Ldt.Name = "Ldt";
            Ldt.Size = new Size(50, 23);
            Ldt.TabIndex = 1;
            Ldt.TextAlign = HorizontalAlignment.Right;
            Ldt.TextChanged += Ldt_TextChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(5, 53);
            label2.Name = "label2";
            label2.Size = new Size(74, 15);
            label2.TabIndex = 1;
            label2.Text = "Площадь Fд";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(5, 28);
            label1.Name = "label1";
            label1.Size = new Size(57, 15);
            label1.TabIndex = 0;
            label1.Text = "Длина Lд";
            // 
            // toolStrip1
            // 
            toolStrip1.ImageScalingSize = new Size(20, 20);
            toolStrip1.Items.AddRange(new ToolStripItem[] { SaveData_button, OpenData_button, Execute_button, Help_button, SaveResults_button });
            toolStrip1.Location = new Point(0, 0);
            toolStrip1.Name = "toolStrip1";
            toolStrip1.Size = new Size(700, 27);
            toolStrip1.TabIndex = 1;
            toolStrip1.Text = "toolStrip1";
            // 
            // SaveData_button
            // 
            SaveData_button.DisplayStyle = ToolStripItemDisplayStyle.Image;
            SaveData_button.Image = Properties.Resources.save;
            SaveData_button.ImageTransparentColor = Color.Magenta;
            SaveData_button.Name = "SaveData_button";
            SaveData_button.Size = new Size(24, 24);
            SaveData_button.Text = "Сохранить исходные данные";
            SaveData_button.Click += SaveData_button_Click;
            // 
            // OpenData_button
            // 
            OpenData_button.DisplayStyle = ToolStripItemDisplayStyle.Image;
            OpenData_button.Image = Properties.Resources.open;
            OpenData_button.ImageTransparentColor = Color.Magenta;
            OpenData_button.Name = "OpenData_button";
            OpenData_button.Size = new Size(24, 24);
            OpenData_button.Text = "Открыть исходные данные";
            OpenData_button.Click += OpenData_button_Click;
            // 
            // Execute_button
            // 
            Execute_button.DisplayStyle = ToolStripItemDisplayStyle.Image;
            Execute_button.Image = Properties.Resources.execute;
            Execute_button.ImageTransparentColor = Color.Magenta;
            Execute_button.Name = "Execute_button";
            Execute_button.Size = new Size(24, 24);
            Execute_button.Text = "Выполнить расчет";
            Execute_button.Click += Execute_button_Click;
            // 
            // Help_button
            // 
            Help_button.Alignment = ToolStripItemAlignment.Right;
            Help_button.DisplayStyle = ToolStripItemDisplayStyle.Image;
            Help_button.Image = Properties.Resources.Help;
            Help_button.ImageTransparentColor = Color.Magenta;
            Help_button.Name = "Help_button";
            Help_button.Size = new Size(24, 24);
            Help_button.Text = "ПАМАГИТЕ!!!";
            Help_button.Click += Help_button_Click;
            // 
            // SaveResults_button
            // 
            SaveResults_button.DisplayStyle = ToolStripItemDisplayStyle.Image;
            SaveResults_button.Image = Properties.Resources.SaveResults;
            SaveResults_button.ImageTransparentColor = Color.Magenta;
            SaveResults_button.Name = "SaveResults_button";
            SaveResults_button.Size = new Size(24, 24);
            SaveResults_button.Text = "Сохранить результаты расчета";
            SaveResults_button.Click += SaveResults_button_Click;
            // 
            // OpenData
            // 
            OpenData.FileName = "openFileDialog1";
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(groupBox4);
            groupBox2.Controls.Add(groupBox3);
            groupBox2.Controls.Add(krt);
            groupBox2.Controls.Add(label5);
            groupBox2.Controls.Add(label6);
            groupBox2.Controls.Add(Frt);
            groupBox2.Controls.Add(label8);
            groupBox2.Location = new Point(10, 137);
            groupBox2.Margin = new Padding(3, 2, 3, 2);
            groupBox2.Name = "groupBox2";
            groupBox2.Padding = new Padding(3, 2, 3, 2);
            groupBox2.Size = new Size(253, 321);
            groupBox2.TabIndex = 2;
            groupBox2.TabStop = false;
            groupBox2.Text = "Резервуар";
            // 
            // groupBox4
            // 
            groupBox4.Controls.Add(label19);
            groupBox4.Controls.Add(Fnkt);
            groupBox4.Controls.Add(label14);
            groupBox4.Controls.Add(label15);
            groupBox4.Controls.Add(Znnkt);
            groupBox4.Controls.Add(label16);
            groupBox4.Controls.Add(label17);
            groupBox4.Controls.Add(Zvnkt);
            groupBox4.Controls.Add(label18);
            groupBox4.Location = new Point(5, 220);
            groupBox4.Margin = new Padding(3, 2, 3, 2);
            groupBox4.Name = "groupBox4";
            groupBox4.Padding = new Padding(3, 2, 3, 2);
            groupBox4.Size = new Size(242, 95);
            groupBox4.TabIndex = 8;
            groupBox4.TabStop = false;
            groupBox4.Text = "Нижняя камера";
            // 
            // label19
            // 
            label19.AutoSize = true;
            label19.Location = new Point(219, 70);
            label19.Name = "label19";
            label19.Size = new Size(20, 15);
            label19.TabIndex = 13;
            label19.Text = "м²";
            // 
            // Fnkt
            // 
            Fnkt.Location = new Point(164, 68);
            Fnkt.Margin = new Padding(3, 2, 3, 2);
            Fnkt.Name = "Fnkt";
            Fnkt.Size = new Size(50, 23);
            Fnkt.TabIndex = 11;
            Fnkt.TextAlign = HorizontalAlignment.Right;
            Fnkt.TextChanged += Fnkt_TextChanged;
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Location = new Point(3, 70);
            label14.Name = "label14";
            label14.Size = new Size(81, 15);
            label14.TabIndex = 11;
            label14.Text = "Площадь Fнк";
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.Location = new Point(219, 46);
            label15.Name = "label15";
            label15.Size = new Size(16, 15);
            label15.TabIndex = 10;
            label15.Text = "м";
            // 
            // Znnkt
            // 
            Znnkt.Location = new Point(164, 44);
            Znnkt.Margin = new Padding(3, 2, 3, 2);
            Znnkt.Name = "Znnkt";
            Znnkt.Size = new Size(50, 23);
            Znnkt.TabIndex = 10;
            Znnkt.TextAlign = HorizontalAlignment.Right;
            Znnkt.TextChanged += Znnkt_TextChanged;
            // 
            // label16
            // 
            label16.AutoSize = true;
            label16.Location = new Point(3, 46);
            label16.Name = "label16";
            label16.Size = new Size(111, 15);
            label16.TabIndex = 8;
            label16.Text = "Отметка низа Zннк";
            // 
            // label17
            // 
            label17.AutoSize = true;
            label17.Location = new Point(219, 21);
            label17.Name = "label17";
            label17.Size = new Size(16, 15);
            label17.TabIndex = 7;
            label17.Text = "м";
            // 
            // Zvnkt
            // 
            Zvnkt.Location = new Point(164, 19);
            Zvnkt.Margin = new Padding(3, 2, 3, 2);
            Zvnkt.Name = "Zvnkt";
            Zvnkt.Size = new Size(50, 23);
            Zvnkt.TabIndex = 9;
            Zvnkt.TextAlign = HorizontalAlignment.Right;
            Zvnkt.TextChanged += Zvnkt_TextChanged;
            // 
            // label18
            // 
            label18.AutoSize = true;
            label18.Location = new Point(3, 21);
            label18.Name = "label18";
            label18.Size = new Size(116, 15);
            label18.TabIndex = 5;
            label18.Text = "Отметка верха Zвнк";
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(mvodt);
            groupBox3.Controls.Add(label13);
            groupBox3.Controls.Add(label11);
            groupBox3.Controls.Add(Bvodt);
            groupBox3.Controls.Add(Bvod);
            groupBox3.Controls.Add(label9);
            groupBox3.Controls.Add(Zvodt);
            groupBox3.Controls.Add(label10);
            groupBox3.Location = new Point(5, 107);
            groupBox3.Margin = new Padding(3, 2, 3, 2);
            groupBox3.Name = "groupBox3";
            groupBox3.Padding = new Padding(3, 2, 3, 2);
            groupBox3.Size = new Size(242, 108);
            groupBox3.TabIndex = 7;
            groupBox3.TabStop = false;
            groupBox3.Text = "Верхняя камера";
            // 
            // mvodt
            // 
            mvodt.Location = new Point(164, 80);
            mvodt.Margin = new Padding(3, 2, 3, 2);
            mvodt.Name = "mvodt";
            mvodt.Size = new Size(50, 23);
            mvodt.TabIndex = 8;
            mvodt.TextAlign = HorizontalAlignment.Right;
            mvodt.TextChanged += mvodt_TextChanged;
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Location = new Point(3, 70);
            label13.Name = "label13";
            label13.Size = new Size(131, 30);
            label13.TabIndex = 11;
            label13.Text = "Коэффициент расхода\r\nводослива m";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(219, 46);
            label11.Name = "label11";
            label11.Size = new Size(16, 15);
            label11.TabIndex = 10;
            label11.Text = "м";
            // 
            // Bvodt
            // 
            Bvodt.Location = new Point(164, 44);
            Bvodt.Margin = new Padding(3, 2, 3, 2);
            Bvodt.Name = "Bvodt";
            Bvodt.Size = new Size(50, 23);
            Bvodt.TabIndex = 7;
            Bvodt.TextAlign = HorizontalAlignment.Right;
            Bvodt.TextChanged += Bvodt_TextChanged;
            // 
            // Bvod
            // 
            Bvod.AutoSize = true;
            Bvod.Location = new Point(3, 46);
            Bvod.Name = "Bvod";
            Bvod.Size = new Size(132, 15);
            Bvod.TabIndex = 8;
            Bvod.Text = "Длина водослива Bвод";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(219, 21);
            label9.Name = "label9";
            label9.Size = new Size(16, 15);
            label9.TabIndex = 7;
            label9.Text = "м";
            // 
            // Zvodt
            // 
            Zvodt.Location = new Point(164, 19);
            Zvodt.Margin = new Padding(3, 2, 3, 2);
            Zvodt.Name = "Zvodt";
            Zvodt.Size = new Size(50, 23);
            Zvodt.TabIndex = 6;
            Zvodt.TextAlign = HorizontalAlignment.Right;
            Zvodt.TextChanged += Zvodt_TextChanged;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(3, 21);
            label10.Name = "label10";
            label10.Size = new Size(143, 15);
            label10.TabIndex = 5;
            label10.Text = "Отметка водослива Zвод";
            // 
            // krt
            // 
            krt.Location = new Point(170, 82);
            krt.Margin = new Padding(3, 2, 3, 2);
            krt.Name = "krt";
            krt.Size = new Size(50, 23);
            krt.TabIndex = 5;
            krt.TextAlign = HorizontalAlignment.Right;
            krt.TextChanged += krt_TextChanged;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(5, 54);
            label5.Name = "label5";
            label5.Size = new Size(105, 60);
            label5.TabIndex = 5;
            label5.Text = "Коэффициент\r\nдополнительного\r\nсопротивления ζ\r\n\r\n";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(224, 28);
            label6.Name = "label6";
            label6.Size = new Size(20, 15);
            label6.TabIndex = 4;
            label6.Text = "м²";
            // 
            // Frt
            // 
            Frt.Location = new Point(170, 26);
            Frt.Margin = new Padding(3, 2, 3, 2);
            Frt.Name = "Frt";
            Frt.Size = new Size(50, 23);
            Frt.TabIndex = 4;
            Frt.TextAlign = HorizontalAlignment.Right;
            Frt.TextChanged += Frt_TextChanged;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(5, 28);
            label8.Name = "label8";
            label8.Size = new Size(75, 15);
            label8.TabIndex = 0;
            label8.Text = "Площадь Fр";
            // 
            // groupBox5
            // 
            groupBox5.Controls.Add(label20);
            groupBox5.Controls.Add(label21);
            groupBox5.Controls.Add(Trast);
            groupBox5.Controls.Add(dtt);
            groupBox5.Controls.Add(label22);
            groupBox5.Controls.Add(label23);
            groupBox5.Location = new Point(10, 463);
            groupBox5.Margin = new Padding(3, 2, 3, 2);
            groupBox5.Name = "groupBox5";
            groupBox5.Padding = new Padding(3, 2, 3, 2);
            groupBox5.Size = new Size(253, 80);
            groupBox5.TabIndex = 3;
            groupBox5.TabStop = false;
            groupBox5.Text = "Параметры расчета";
            // 
            // label20
            // 
            label20.AutoSize = true;
            label20.Location = new Point(224, 53);
            label20.Name = "label20";
            label20.Size = new Size(13, 15);
            label20.TabIndex = 5;
            label20.Text = "с";
            // 
            // label21
            // 
            label21.AutoSize = true;
            label21.Location = new Point(224, 28);
            label21.Name = "label21";
            label21.Size = new Size(13, 15);
            label21.TabIndex = 4;
            label21.Text = "с";
            // 
            // Trast
            // 
            Trast.Location = new Point(170, 51);
            Trast.Margin = new Padding(3, 2, 3, 2);
            Trast.Name = "Trast";
            Trast.Size = new Size(50, 23);
            Trast.TabIndex = 13;
            Trast.TextAlign = HorizontalAlignment.Right;
            Trast.TextChanged += Trast_TextChanged;
            // 
            // dtt
            // 
            dtt.Location = new Point(170, 26);
            dtt.Margin = new Padding(3, 2, 3, 2);
            dtt.Name = "dtt";
            dtt.Size = new Size(50, 23);
            dtt.TabIndex = 12;
            dtt.TextAlign = HorizontalAlignment.Right;
            dtt.TextChanged += dtt_TextChanged;
            // 
            // label22
            // 
            label22.AutoSize = true;
            label22.Location = new Point(5, 53);
            label22.Name = "label22";
            label22.Size = new Size(97, 15);
            label22.TabIndex = 1;
            label22.Text = "Время расчета T";
            // 
            // label23
            // 
            label23.AutoSize = true;
            label23.Location = new Point(5, 28);
            label23.Name = "label23";
            label23.Size = new Size(89, 15);
            label23.TabIndex = 0;
            label23.Text = "Шаг расчета dt";
            // 
            // groupBox6
            // 
            groupBox6.Controls.Add(dataGridView_discharge);
            groupBox6.Controls.Add(label26);
            groupBox6.Controls.Add(label27);
            groupBox6.Location = new Point(269, 463);
            groupBox6.Margin = new Padding(3, 2, 3, 2);
            groupBox6.Name = "groupBox6";
            groupBox6.Padding = new Padding(3, 2, 3, 2);
            groupBox6.Size = new Size(262, 80);
            groupBox6.TabIndex = 4;
            groupBox6.TabStop = false;
            groupBox6.Text = "Закон изменения расхода";
            // 
            // dataGridView_discharge
            // 
            dataGridView_discharge.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView_discharge.Location = new Point(86, 20);
            dataGridView_discharge.Margin = new Padding(3, 2, 3, 2);
            dataGridView_discharge.Name = "dataGridView_discharge";
            dataGridView_discharge.RowHeadersWidth = 51;
            dataGridView_discharge.Size = new Size(168, 56);
            dataGridView_discharge.TabIndex = 14;
            // 
            // label26
            // 
            label26.AutoSize = true;
            label26.Location = new Point(5, 53);
            label26.Name = "label26";
            label26.Size = new Size(75, 15);
            label26.TabIndex = 1;
            label26.Text = "Расход, м³/с";
            // 
            // label27
            // 
            label27.AutoSize = true;
            label27.Location = new Point(5, 28);
            label27.Name = "label27";
            label27.Size = new Size(54, 15);
            label27.TabIndex = 0;
            label27.Text = "Время, с";
            // 
            // formsPlot_L
            // 
            formsPlot_L.DisplayScale = 1.25F;
            formsPlot_L.Location = new Point(269, 22);
            formsPlot_L.Margin = new Padding(3, 2, 3, 2);
            formsPlot_L.Name = "formsPlot_L";
            formsPlot_L.Size = new Size(421, 219);
            formsPlot_L.TabIndex = 5;
            // 
            // formsPlot_Q
            // 
            formsPlot_Q.DisplayScale = 1.25F;
            formsPlot_Q.Location = new Point(269, 246);
            formsPlot_Q.Margin = new Padding(3, 2, 3, 2);
            formsPlot_Q.Name = "formsPlot_Q";
            formsPlot_Q.Size = new Size(421, 212);
            formsPlot_Q.TabIndex = 6;
            // 
            // groupBox7
            // 
            groupBox7.Controls.Add(Volume);
            groupBox7.Controls.Add(Elevation);
            groupBox7.Location = new Point(536, 463);
            groupBox7.Margin = new Padding(3, 2, 3, 2);
            groupBox7.Name = "groupBox7";
            groupBox7.Padding = new Padding(3, 2, 3, 2);
            groupBox7.Size = new Size(153, 80);
            groupBox7.TabIndex = 7;
            groupBox7.TabStop = false;
            groupBox7.Text = "Экстремумы";
            // 
            // Volume
            // 
            Volume.AutoSize = true;
            Volume.Location = new Point(5, 53);
            Volume.Name = "Volume";
            Volume.Size = new Size(73, 15);
            Volume.TabIndex = 1;
            Volume.Text = "Объем ВК: -";
            // 
            // Elevation
            // 
            Elevation.AutoSize = true;
            Elevation.Location = new Point(5, 28);
            Elevation.Name = "Elevation";
            Elevation.Size = new Size(64, 15);
            Elevation.TabIndex = 0;
            Elevation.Text = "Уровень: -";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(700, 550);
            Controls.Add(groupBox7);
            Controls.Add(formsPlot_Q);
            Controls.Add(formsPlot_L);
            Controls.Add(groupBox6);
            Controls.Add(groupBox5);
            Controls.Add(groupBox2);
            Controls.Add(toolStrip1);
            Controls.Add(groupBox1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(3, 2, 3, 2);
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Камерный уравнительный резервуар";
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            groupBox4.ResumeLayout(false);
            groupBox4.PerformLayout();
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            groupBox5.ResumeLayout(false);
            groupBox5.PerformLayout();
            groupBox6.ResumeLayout(false);
            groupBox6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView_discharge).EndInit();
            groupBox7.ResumeLayout(false);
            groupBox7.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private GroupBox groupBox1;
        private ToolStrip toolStrip1;
        private SaveFileDialog SaveData;
        private SaveFileDialog SaveResults;
        private OpenFileDialog OpenData;
        private Label label4;
        private Label label3;
        private TextBox Fdt;
        private TextBox Ldt;
        private Label label2;
        private Label label1;
        private GroupBox groupBox2;
        private TextBox krt;
        private Label label5;
        private Label label6;
        private TextBox Frt;
        private Label label8;
        private GroupBox groupBox3;
        private Label label9;
        private TextBox Zvodt;
        private Label label10;
        private GroupBox groupBox4;
        private TextBox Fnkt;
        private Label label14;
        private Label label15;
        private TextBox Znnkt;
        private Label label16;
        private Label label17;
        private TextBox Zvnkt;
        private Label label18;
        private TextBox mvodt;
        private Label label13;
        private Label label11;
        private TextBox Bvodt;
        private Label Bvod;
        private Label label19;
        private ToolStripButton SaveData_button;
        private ToolStripButton OpenData_button;
        private ToolStripButton Execute_button;
        private ToolStripButton Help_button;
        private ToolStripButton SaveResults_button;
        private GroupBox groupBox5;
        private Label label20;
        private Label label21;
        private TextBox Trast;
        private TextBox dtt;
        private Label label22;
        private Label label23;
        private GroupBox groupBox6;
        private DataGridView dataGridView_discharge;
        private Label label26;
        private Label label27;
        private ScottPlot.WinForms.FormsPlot formsPlot_L;
        private ScottPlot.WinForms.FormsPlot formsPlot_Q;
        private GroupBox groupBox7;
        private Label Volume;
        private Label Elevation;
        private TextBox knt;
        private Label label7;
    }
}
