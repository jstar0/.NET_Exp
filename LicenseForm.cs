using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StudentInformationEntrySystem
{
    public partial class LicenseForm : Form
    {
        public LicenseForm()
        {
            InitializeComponent();
            this.FormClosing += (sender, e) =>
            {
                if (Program.WillExit)
                {
                    MessageBox.Show("您必须先阅读并知悉提示信息，方可使用系统。\n程序即将退出。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            };
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void btnLicenseAgree_Click(object sender, EventArgs e)
        {
            AppSettings.UpdateAppSettings("hasReadLicense", "true");
            Program.WillExit = false;
            this.Close();
        }

        private void btnLicenseDisagree_Click(object sender, EventArgs e)
        {
            Program.WillExit = true;
            Application.Exit();
        }

        private void linkLblFrontEnd_Click(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // 打开指定的URL
            string url = "https://github.com/jstar0/.NET_Exp/tree/exp3_dotnet";
            Process.Start(new ProcessStartInfo
            {
                FileName = url,
                UseShellExecute = true
            });
        }

        private void linkLbl_BackEnd(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // 打开指定的URL
            string url = "https://github.com/jstar0/.NET_Exp/tree/exp3_nestjs";
            Process.Start(new ProcessStartInfo
            {
                FileName = url,
                UseShellExecute = true
            });
        }
    }
}
