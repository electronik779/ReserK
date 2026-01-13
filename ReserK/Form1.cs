using ReserK.Properties;
using System.Data;
using System.Diagnostics;
using System.Globalization;

namespace ReserK
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            dataGridView_discharge.ColumnHeadersVisible = false;
            dataGridView_discharge.RowHeadersVisible = false;
            dataGridView_discharge.AllowUserToAddRows = false;
            dataGridView_discharge.AllowUserToDeleteRows = false;
            dataGridView_discharge.AllowUserToOrderColumns = false;
            dataGridView_discharge.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            DataTable discharge = new DataTable();

            List<string> colNames = new List<string>() { "0", "1", "2" };

            foreach (string str in colNames)
            {
                discharge.Columns.Add(str);
            }
            for (int i = 0; i < 2; i++)
            {
                DataRow dr = discharge.NewRow();
                discharge.Rows.Add(dr);
            }

            dataGridView_discharge.DataSource = discharge;

            for (int i = 0; i < dataGridView_discharge.ColumnCount; i++)
            {
                dataGridView_discharge.Columns[i].Width = 55;
            }

            OpenData.Filter = "CSV файлы (*.csv)|*.csv";

            SaveData.Filter = "CSV файлы (*.csv)|*.csv";
            SaveData.DefaultExt = "csv";
            SaveData.AddExtension = true;

            SaveResults.Filter = "CSV файлы (*.csv)|*.csv";
            SaveResults.DefaultExt = "csv";
            SaveResults.AddExtension = true;

            this.FormClosing += (sender, e) => { TryDeleteFile(tempFilePath); };

        }

        // Генерация уникального имени файла
        string tempFilePath = Path.Combine(Path.GetTempPath(),
                Guid.NewGuid().ToString() + ".pdf");


        double[,] Table = new double[32768, 9];

        double t2 = 0;

        int count;

        private void Execute_button_Click(object sender, EventArgs e)
        {
            Elevation.Text = "Уровень: -";
            Volume.Text = "Объем ВК: -";


            count = 0;

            int IK = 6;

            double[] UT = new double[6];
            double[] UQST = new double[6];

            double Ld = 0;
            double Fd = 0;
            double Fr = 0;
            double kn = 0;
            double kr = 0;
            double dt = 0;
            double Zvod = 0;
            double Bvod = 0;
            double mvod = 0;
            double Zvnk = 0;
            double Znnk = 0;
            double Fnk = 0;
            double Tras = 0;
            double kwd;
            double kwr;
            double Dd;
            double R;
            double Qst;
            double Qd;
            double Qr;
            double Hwd;
            double Hwr;
            double Z;
            double Hd;
            double Shesi;
            double Wwk = 0;
            double LevelMax = 0;
            double LevelMin = 0;
            double W = 0;
            double T = 0;

            try { Ld = GetDouble(Ldt.Text, 0); }
            catch { ErrorMsg(Ldt, "длину деривации"); }
            try { Fd = GetDouble(Fdt.Text, 0); }
            catch { ErrorMsg(Fdt, "площадь деривации"); }
            try { Fr = GetDouble(Frt.Text, 0); }
            catch { ErrorMsg(Frt, "площадь резервуара"); }
            try { kn = GetDouble(knt.Text, 0); }
            catch { ErrorMsg(knt, "коэффициент шероховатости"); }
            try { kr = GetDouble(krt.Text, 0); }
            catch { ErrorMsg(krt, "коэффициент дополнительного сопротивления"); }
            try { Zvod = GetDouble(Zvodt.Text, 0); }
            catch { ErrorMsg(Zvodt, "отметку водослива"); }
            try { Bvod = GetDouble(Bvodt.Text, 0); }
            catch { ErrorMsg(Bvodt, "длину водослива"); }
            try { mvod = GetDouble(mvodt.Text, 0); }
            catch { ErrorMsg(mvodt, "коэффициент расхода водослива"); }
            try { Zvnk = GetDouble(Zvnkt.Text, 0); }
            catch { ErrorMsg(Zvnkt, "отметку верха нижней камеры"); }
            try { Znnk = GetDouble(Znnkt.Text, 0); }
            catch { ErrorMsg(Znnkt, "отметку низа нижней камеры"); }
            try { Fnk = GetDouble(Fnkt.Text, 0); }
            catch { ErrorMsg(Fnkt, "площадь нижней камеры"); }
            try { dt = GetDouble(dtt.Text, 0); }
            catch { ErrorMsg(dtt, "шаг расчета"); }
            try { Tras = GetDouble(Trast.Text, 0); }
            catch { ErrorMsg(Trast, "время расчета"); }

            for (int i = 0; i < 3; i++)
            {
                try
                {
                    UT[i] = GetDouble((string)dataGridView_discharge.Rows[0].Cells[i].Value, 0);
                    UQST[i] = GetDouble((string)dataGridView_discharge.Rows[1].Cells[i].Value, 0);
                }
                catch
                {
                    MessageBox.Show("Ошибка в законе изменения расхода.", "Внимание!",
                            MessageBoxButtons.OK, icon: MessageBoxIcon.Error);
                    return;
                }
            }

            t2 = UT[1];

            if (kn < 0) kn = 0;

            if (dt > 10)
            {
                dtt.BackColor = Color.Red;
                MessageBox.Show("Шаг расчета не должен превышать 10 с", "Внимание!",
                    MessageBoxButtons.OK, icon: MessageBoxIcon.Information);
                return;
            }

            try
            {
                Dd = Math.Pow(4 * Fd / 3.1415, 0.5);
                R = Dd / 4;
                if (kn > 0)
                {
                    Shesi = 1 / kn * Math.Pow(R, 1 / 6);
                    kwd = Ld / (Math.Pow(Shesi, 2) * R * Math.Pow(Fd, 2));
                }
                else { kwd = 0; }

                kwr = kr / (19.62 * Math.Pow(Fd, 2));

                Qst = Int11(T, IK, UT, UQST);
                Qd = Qst;
                Qr = 0;
                Hwd = kwd * Qd * Math.Abs(Qd) + Math.Pow(Qd, 2) / (19.62 * Math.Pow(Fd, 2));
                Hwr = 0;
                Z = -Hwd;
                Hd = Z;

                Table[0, 0] = T;
                Table[0, 1] = Qst;
                Table[0, 2] = Qd;
                Table[0, 3] = Qr;
                Table[0, 4] = Hwd;
                Table[0, 5] = Hwr;
                Table[0, 6] = Z;
                Table[0, 7] = Hd;
                Table[0, 8] = Wwk;

                double[] UZ = new double[4] { Z, Zvnk, Znnk, -100 };
                double[] UW = new double[4] { 0, Fr * (Z - Zvnk),
                    Fr * (Z - Zvnk) + Fnk * (Zvnk - Znnk),
                    Fr * (Z - Zvnk) + Fnk * (Zvnk - Znnk) + Fr * (Znnk + 100) };

                while (T <= Tras)
                {
                    if (Z > LevelMax) LevelMax = Z;
                    if (Z < LevelMin) LevelMin = Z;

                    T += dt;
                    count++;
                    if (count >= 32768)
                    {
                        MessageBox.Show("Слишком много значений. Увеличьте шаг расчета", "Внимание!",
                        MessageBoxButtons.OK, icon: MessageBoxIcon.Information);
                        break;
                    }
                    Qst = Int11(T, IK, UT, UQST);
                    Qr = Qd - Qst;
                    Hwd = kwd * Qd * Math.Abs(Qd) + Math.Pow(Qd, 2) / (19.62 * Math.Pow(Fd, 2));
                    Hwr = kwr * Qr * Math.Abs(Qr);
                    //Debug.WriteLine("Common: {0} {1} {2} {3} {4}", T, Z, Qd, Qst, Qr);

                    if (Qr <= 0)
                    {
                        W += Math.Abs(Qr) * dt;
                        Z = Int11(W, 4, UW, UZ);
                        Qd += (-(Z + Hwd + Hwr) * 9.81 * Fd / Ld) * dt;
                        Qr = Qd - Qst;
                        Hd = Z + Hwr;
                        //Debug.WriteLine("Qr < 0: {0} {1} {2} {3} {4}",T, Z, Qd, Qst, Qr);
                        if (Qr >= 0) { break; }
                    }
                    if (Z <= Zvod && Qr > 0)
                    {
                        Z += Qr / Fr * dt;
                        Qd += (-(Z + Hwd + Hwr) * 9.81 * Fd / Ld) * dt;
                        Qr = Qd - Qst;
                        Hd = Z + Hwr;
                        //Debug.WriteLine("Z <= Zwod: {0} {1} {2} {3} {4}", T, Z, Qd, Qst, Qr);
                        if (Qr <= 0) { break; }
                    }
                    else if (Z > Zvod && Qr > 0)
                    {
                        Z = Zvod + Math.Pow((Qr / (mvod * Bvod * 4.43)), 2 / 3);
                        Qd += (-(Z + Hwd + Hwr) * 9.81 * Fd / Ld) * dt;
                        Qr = Qd - Qst;
                        Hd = Z + Hwr;
                        Wwk += Qr * dt;
                        //Debug.WriteLine("Z > Zwod: {0} {1} {2} {3} {4}", T, Z, Qd, Qst, Qr);
                        if (Qr <= 0) { break; }
                    }
                    Table[count, 0] = T;
                    Table[count, 1] = Qst;
                    Table[count, 2] = Qd;
                    Table[count, 3] = Qr;
                    Table[count, 4] = Hwd;
                    Table[count, 5] = Hwr;
                    Table[count, 6] = Z;
                    Table[count, 7] = Hd;
                    Table[count, 8] = Wwk;
                }

                Elevation.Text = "Уровень: мин. " + Math.Round(LevelMin, 2) + " м";

                if (UQST[0] > UQST[1])
                {
                    Volume.Text = "Объем ВК: " + Math.Round(Wwk, 0) + " м³";
                    Elevation.Text = "Уровень: макс. " + Math.Round(LevelMax, 2) + " м";
                }


                double Ttmp = Math.Ceiling(count * dt);
                //Debug.WriteLine("Ttmp= {0}", Ttmp);
                if (Ttmp < Tras)
                {
                    Ttmp = Math.Ceiling(Ttmp / 10) * 10;
                    //Debug.WriteLine("Ttmp= {0}", Ttmp);
                }

                // Строим графики
                int Lmax = -1000000;
                int Lmin = 1000000;
                int Qmax = Lmax;
                int Qmin = Lmin;

                for (int i = 0; i < count; i++)
                {
                    if (Table[i, 6] < Lmin) { Lmin = (int)Table[i, 6]; }
                    if (Table[i, 6] > Lmax) { Lmax = (int)Table[i, 6]; }
                    if (Table[i, 1] < Qmin) { Qmin = (int)Table[i, 1]; }
                    if (Table[i, 1] > Qmax) { Qmax = (int)Table[i, 1]; }
                }
                for (int i = 0; i < count; i++)
                {
                    if (Table[i, 7] < Lmin) { Lmin = (int)Table[i, 7]; }
                    if (Table[i, 7] > Lmax) { Lmax = (int)Table[i, 7]; }
                    if (Table[i, 2] < Qmin) { Qmin = (int)Table[i, 2]; }
                    if (Table[i, 2] > Qmax) { Qmax = (int)Table[i, 2]; }
                }
                Lmin -= 1;
                Lmax += 1;
                Qmin -= 1;
                Qmax += 1;

                double[] DataX = new double[count];
                double[] DataY1 = new double[count];
                double[] DataY2 = new double[count];

                formsPlot_L.Plot.Clear();
                formsPlot_Q.Plot.Clear();

                for (int i = 0; i < count; i++)
                {
                    DataX[i] = Table[i, 0];
                    DataY1[i] = Table[i, 6];
                    DataY2[i] = Table[i, 7];
                }
                BuildChart_2lines(formsPlot_L, DataX, DataY1, DataY2,
                    "Уровень в УР, Z", "Давление в деривации, Нд", 2, 0,
                    "Время, с", 0, Ttmp,
                    "м", Lmin, Lmax);

                for (int i = 0; i < count; i++)
                {
                    DataY1[i] = Table[i, 2];
                    DataY2[i] = Table[i, 1];
                }
                BuildChart_2lines(formsPlot_Q, DataX, DataY1, DataY2,
                    "Расход деривации, Qд", "Расход турбинных водоводов, Qт", 2, 0,
                    "Время, с", 0, Ttmp,
                    "м³/c", Qmin, Qmax);
            }
            catch
            {
                MessageBox.Show("Ошибка расчета. Проверьте корректность введенных данных",
                    "Внимание!",
                    MessageBoxButtons.OK, icon: MessageBoxIcon.Error);
            }
        }

        private void BuildChart_2lines(ScottPlot.WinForms.FormsPlot chartName,
            double[] dataX, double[] dataY1, double[] dataY2,
            string line1_Name, string line2_Name,
            float lineWidth, float markerSize,
            string axisX_Name, double minX, double maxX,
            string axisY_Name, double minY, double maxY)
        {
            ScottPlot.Color semitransparent = ScottPlot.Colors.White.WithAlpha(0.3);
            chartName.Plot.Axes.SetLimitsX(minX, maxX);
            chartName.Plot.Axes.SetLimitsY(minY, maxY);
            chartName.Plot.Axes.Left.Label.Text = axisY_Name;
            chartName.Plot.Axes.Left.Label.Bold = false;
            chartName.Plot.Axes.Left.Label.FontSize = 14;
            chartName.Plot.Axes.Bottom.Label.Text = axisX_Name;
            chartName.Plot.Axes.Bottom.Label.Bold = false;
            chartName.Plot.Axes.Bottom.Label.FontSize = 14;
            chartName.Plot.Legend.BackgroundColor = semitransparent;
            chartName.Plot.Legend.FontName = "Segoe UI";
            chartName.Plot.Legend.FontSize = 12;
            chartName.Plot.ShowLegend(ScottPlot.Alignment.LowerRight, ScottPlot.Orientation.Vertical);

            var line1 = chartName.Plot.Add.Scatter(dataX, dataY1);
            var line2 = chartName.Plot.Add.Scatter(dataX, dataY2);
            line1.LegendText = line1_Name;
            line2.LegendText = line2_Name;
            line1.MarkerSize = markerSize;
            line2.MarkerSize = markerSize;
            line1.LineWidth = lineWidth;
            line2.LineWidth = lineWidth;
            chartName.Refresh();
        }

        private double GetDouble(string str, double defaultValue)
        {
            double result;
            //Try parsing in the current culture
            if (!double.TryParse(str, NumberStyles.Any, CultureInfo.CurrentCulture, out result) &&
                //Then try in US english
                !double.TryParse(str, NumberStyles.Any, CultureInfo.GetCultureInfo("en-US"), out result) &&
                //Then in neutral language
                !double.TryParse(str, NumberStyles.Any, CultureInfo.InvariantCulture, out result))
            {
                result = defaultValue;
                throw new ArgumentNullException();
            }

            return result;
        }

        private void ErrorMsg(TextBox textBox, string str)
        {
            textBox.BackColor = Color.Red;
            MessageBox.Show("Необходимо ввести " + str + ".", "Внимание!",
                    MessageBoxButtons.OK, icon: MessageBoxIcon.Error);
            return;
        }

        private double Int11(double D, int N, double[] X, double[] Y)
        {
            double V = -1;
            int i;
            for (i = 1; i < N; i++)
            {
                if (D - X[i] <= 0)
                {
                    int i1 = i - 1;
                    V = (Y[i] * (D - X[i1]) - Y[i1] * (D - X[i])) / (X[i] - X[i1]);
                    break;
                }
            }
            if (V == -1)
            {
                V = (Y[2] * (D - X[1]) - Y[1] * (D - X[2])) / (X[2] - X[1]);
            }
            return V;
        }

        private void SaveData_button_Click(object sender, EventArgs e)
        {
            if (SaveData.ShowDialog() == DialogResult.OK)
            {
                // получаем выбранный файл
                string filename = SaveData.FileName;

                //если существует - удаляем
                if (File.Exists(filename))
                {
                    File.Delete(filename);
                }

                List<string> block1 = new List<string>();
                List<string> block2 = new List<string>();
                List<string> block3 = new List<string>();
                List<string> block4 = new List<string>();
                List<string> block5 = new List<string>();
                List<string> block6 = new List<string>();

                block1.Add(Ldt.Text);
                block1.Add(Fdt.Text);
                block1.Add(knt.Text);
                block2.Add(Frt.Text);
                block2.Add(krt.Text);
                block3.Add(Zvodt.Text);
                block3.Add(Bvodt.Text);
                block3.Add(mvodt.Text);
                block4.Add(Zvnkt.Text);
                block4.Add(Znnkt.Text);
                block4.Add(Fnkt.Text);

                for (int i = 0; i < 3; i++)
                {
                    block5.Add((string)dataGridView_discharge.Rows[0].Cells[i].Value);
                }
                for (int i = 0; i < 3; i++)
                {
                    block5.Add((string)dataGridView_discharge.Rows[1].Cells[i].Value);
                }

                block6.Add(dtt.Text);
                block6.Add(Trast.Text);

                using (StreamWriter writer = new StreamWriter(filename, true, System.Text.Encoding.UTF8))
                {
                    writer.WriteLine(string.Join(";", block1));
                    writer.WriteLine(string.Join(";", block2));
                    writer.WriteLine(string.Join(";", block3));
                    writer.WriteLine(string.Join(";", block4));
                    writer.WriteLine(string.Join(";", block5));
                    writer.WriteLine(string.Join(";", block6));
                }
            }
        }

        private void OpenData_button_Click(object sender, EventArgs e)
        {
            if (OpenData.ShowDialog() == DialogResult.OK)
            {
                // получаем выбранный файл
                string filename = OpenData.FileName;

                List<List<string>> blocks = new List<List<string>>();

                using (StreamReader reader = new StreamReader(filename))
                {
                    string? line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        List<string> row = line.Split(';').ToList();
                        blocks.Add(row);
                    }
                }

                List<string>? block1 = blocks.ElementAtOrDefault(0);
                List<string>? block2 = blocks.ElementAtOrDefault(1);
                List<string>? block3 = blocks.ElementAtOrDefault(2);
                List<string>? block4 = blocks.ElementAtOrDefault(3);
                List<string>? block5 = blocks.ElementAtOrDefault(4);
                List<string>? block6 = blocks.ElementAtOrDefault(5);

                try
                {
                    Ldt.Text = block1?.ElementAtOrDefault(0) ?? string.Empty;
                    Fdt.Text = block1?.ElementAtOrDefault(1) ?? string.Empty;
                    knt.Text = block1?.ElementAtOrDefault(2) ?? string.Empty;
                    Frt.Text = block2?.ElementAtOrDefault(0) ?? string.Empty;
                    krt.Text = block2?.ElementAtOrDefault(1) ?? string.Empty;
                    Zvodt.Text = block3?.ElementAtOrDefault(0) ?? string.Empty;
                    Bvodt.Text = block3?.ElementAtOrDefault(1) ?? string.Empty;
                    mvodt.Text = block3?.ElementAtOrDefault(2) ?? string.Empty;
                    Zvnkt.Text = block4?.ElementAtOrDefault(0) ?? string.Empty;
                    Znnkt.Text = block4?.ElementAtOrDefault(1) ?? string.Empty;
                    Fnkt.Text = block4?.ElementAtOrDefault(2) ?? string.Empty;


                    for (int i = 0; i < 3; i++)
                    {
                        dataGridView_discharge.Rows[0].Cells[i].Value =
                            block5?.ElementAtOrDefault(i) ?? string.Empty;
                    }
                    for (int i = 3; i < 6; i++)
                    {
                        dataGridView_discharge.Rows[1].Cells[i - 3].Value =
                            block5?.ElementAtOrDefault(i) ?? string.Empty;
                    }

                    dtt.Text = block6?.ElementAtOrDefault(0) ?? string.Empty;
                    Trast.Text = block6?.ElementAtOrDefault(1) ?? string.Empty;
                }
                catch
                {
                    MessageBox.Show("Неверный формат файла исходных данных " +
                        "/ файл исходных данных повреждён", "Внимание!",
                    MessageBoxButtons.OK, icon: MessageBoxIcon.Error);
                }
            }
        }

        private void SaveResults_button_Click(object sender, EventArgs e)
        {
            if (SaveResults.ShowDialog() == DialogResult.OK)
            {
                // получаем выбранный файл
                string filename = SaveResults.FileName;

                //если существует - удаляем
                if (File.Exists(filename))
                {
                    File.Delete(filename);
                }

                using (StreamWriter writer = new StreamWriter(filename, true,
                    System.Text.Encoding.UTF8))
                {
                    List<string> columnsNames = new List<string>()
                   { "Время, с", "Расход турбинного водовода, м3/с", "Расход деривации, м3/с",
                        "Расход резервуара, м3/с", "Потери в деривации, м",
                        "Потери в резервуаре, м", "Уровень в резервуаре, м",
                        "Давление в деривации, м", "Объем воды в верхней камере, м3"};
                    writer.WriteLine(string.Join(";", columnsNames));

                    for (int j = 0; j < count; j++)
                    {
                        List<string> list = new List<string>();
                        for (int i = 0; i < 9; i++)
                        {
                            double tmp;
                            tmp = Table[j, i];
                            //Debug.WriteLine("{0}, {1}, {2}", j, i, tmp);
                            list.Add(tmp.ToString());
                        }
                        writer.WriteLine(string.Join(";", list));
                    }
                }
            }

        }

        private void Ldt_TextChanged(object sender, EventArgs e)
        {
            if (Ldt.BackColor == Color.Red) { Ldt.BackColor = SystemColors.Window; }
        }

        private void Fdt_TextChanged(object sender, EventArgs e)
        {
            if (Fdt.BackColor == Color.Red) { Fdt.BackColor = SystemColors.Window; }
        }

        private void Frt_TextChanged(object sender, EventArgs e)
        {
            if (Frt.BackColor == Color.Red) { Frt.BackColor = SystemColors.Window; }
        }

        private void knt_TextChanged(object sender, EventArgs e)
        {
            if (knt.BackColor == Color.Red) { knt.BackColor = SystemColors.Window; }
        }

        private void krt_TextChanged(object sender, EventArgs e)
        {
            if (krt.BackColor == Color.Red) { krt.BackColor = SystemColors.Window; }
        }

        private void Zvodt_TextChanged(object sender, EventArgs e)
        {
            if (Zvodt.BackColor == Color.Red) { Zvodt.BackColor = SystemColors.Window; }
        }

        private void Bvodt_TextChanged(object sender, EventArgs e)
        {
            if (Bvodt.BackColor == Color.Red) { Bvodt.BackColor = SystemColors.Window; }
        }

        private void mvodt_TextChanged(object sender, EventArgs e)
        {
            if (mvodt.BackColor == Color.Red) { mvodt.BackColor = SystemColors.Window; }
        }

        private void Zvnkt_TextChanged(object sender, EventArgs e)
        {
            if (Zvnkt.BackColor == Color.Red) { Zvnkt.BackColor = SystemColors.Window; }
        }

        private void Znnkt_TextChanged(object sender, EventArgs e)
        {
            if (Znnkt.BackColor == Color.Red) { Znnkt.BackColor = SystemColors.Window; }
        }

        private void Fnkt_TextChanged(object sender, EventArgs e)
        {
            if (Fnkt.BackColor == Color.Red) { Fnkt.BackColor = SystemColors.Window; }
        }

        private void dtt_TextChanged(object sender, EventArgs e)
        {
            if (dtt.BackColor == Color.Red) { dtt.BackColor = SystemColors.Window; }
        }

        private void Trast_TextChanged(object sender, EventArgs e)
        {
            if (Trast.BackColor == Color.Red) { Trast.BackColor = SystemColors.Window; }
        }

        private static void TryDeleteFile(string path)
        {
            try
            {
                if (File.Exists(path))
                    File.Delete(path);
            }
            catch { /* Игнорируем ошибки удаления */ }
        }

        private void Help_button_Click(object sender, EventArgs e)
        {
            byte[] fileData = Resources.ReserK_help;

            try
            {
                // Сохранение ресурса во временный файл
                File.WriteAllBytes(tempFilePath, fileData);

                // Запуск приложения по умолчанию
                ProcessStartInfo startInfo = new ProcessStartInfo()
                {
                    FileName = tempFilePath,
                    UseShellExecute = true // Ключевой параметр для использования ассоциаций ОС
                };
                Process.Start(startInfo);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка открытия справки: {ex.Message}");
                // Удаление файла в случае ошибки
                TryDeleteFile(tempFilePath);
            }
        }
    }
}
