using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CoWGenerator
{
    public partial class frmMain : Form
    {
        string csvPath;
        string savePath;
        CSV2PNML CSV2PNML;
        public frmMain()
        {
            InitializeComponent();
        }

        private void btnPickCSV_Click(object sender, EventArgs e)
        {
            ofdCSVPicker.Filter = "CSV Files (.csv)|*.csv";
            ofdCSVPicker.ShowDialog();
            csvPath = ofdCSVPicker.FileName;

            Console.WriteLine(ofdCSVPicker.FileName);
        }

        private void btnGO_Click(object sender, EventArgs e)
        {
            CSV2PNML = new CSV2PNML(csvPath, savePath);
        }

        private void btnLocation_Click(object sender, EventArgs e)
        {
            fbdFolderBrowser.ShowDialog();
            savePath = fbdFolderBrowser.SelectedPath;
            Console.WriteLine(savePath);
        }
    }
}
