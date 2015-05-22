using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace minesweeper
{
    public partial class RecordsDialog : Form
    {
        RecordTable table;
        public RecordsDialog()
        {
            InitializeComponent();
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void resetRecordsButton_Click(object sender, EventArgs e)
        {
            table.InitFile();
            UpdateLabels();
        }

        private void UpdateLabels()
        {
            table = new RecordTable();
            var records = table.GetRecords();
            newbieNameLabel.Text = records[0].name;
            newbieTimeLabel.Text = records[0].time.ToString();
            advancedNameLabel.Text = records[1].name;
            advancedTimeLabel.Text = records[1].time.ToString();
            profiNameLabel.Text = records[2].name;
            profiTimeLabel.Text = records[2].time.ToString();
        }

        private void RecordsDialog_Load(object sender, EventArgs e)
        {
            UpdateLabels();
        }
    }
}
