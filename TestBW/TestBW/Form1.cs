using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestBW
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void bwDoOp_DoWork(object sender, DoWorkEventArgs e)
        {
            for (int i = 0;i < 100;i += 10)
            {
                bwDoOp.ReportProgress(i);

                if(bwDoOp.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }

                Thread.Sleep(1000);
            }
        }

        private void bwDoOp_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar.Value = e.ProgressPercentage;
        }

        private void bwDoOp_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                lblStatus.Text = "Stopped";
            }
            else
            {
                lblStatus.Text = "Done";
            }
            btnStart.Enabled = true;
            btnStop.Enabled = false;
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            btnStart.Enabled = false;
            btnStop.Enabled = true;
            lblStatus.Text = "Started";

            bwDoOp.RunWorkerAsync();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            if(bwDoOp.IsBusy)
            {
                bwDoOp.CancelAsync();
            }
        }
    }
}
