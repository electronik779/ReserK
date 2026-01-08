using System.Data;

namespace ReserK
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            //this.FormClosed += new FormClosedEventHandler(Form1_FormClosed);

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

            for (int i = 0; i < dataGridView_discharge.Columns.Count; i++)
            {
                dataGridView_discharge.Columns[i].Width = 60;
            }

            OpenData.Filter = "CSV פאיכû (*.csv)|*.csv";

            SaveData.Filter = "CSV פאיכû (*.csv)|*.csv";
            SaveData.DefaultExt = "csv";
            SaveData.AddExtension = true;

            SaveResults.Filter = "CSV פאיכû (*.csv)|*.csv";
            SaveResults.DefaultExt = "csv";
            SaveResults.AddExtension = true;

            //this.FormClosing += (sender, e) => { TryDeleteFile(tempFilePath); };

        }
    }
}
