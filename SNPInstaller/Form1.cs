using System;
using System.Configuration;
using System.IO;
using System.Security.Principal;
using System.Windows.Forms;

namespace SNPInstaller
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            bool isElevated;
            using (WindowsIdentity identity = WindowsIdentity.GetCurrent())
            {
                WindowsPrincipal principal = new WindowsPrincipal(identity);
                isElevated = principal.IsInRole(WindowsBuiltInRole.Administrator);
            }
            if (!isElevated)
                MessageBox.Show("Please Launch Me in Administrative mode! ( rightclick and hot run as admin i need this to install the service)");
        }

        private void InstallButton_Click(object sender, EventArgs e)
        {
            string strCmdText = "/K " + Directory.GetCurrentDirectory() + "\\InstallUtil.exe " + Directory.GetCurrentDirectory() + "\\Service\\" + ConfigurationManager.AppSettings["EXEname"];//change /K to /C to make the cmd prompt close after install or uninstall
            System.Diagnostics.Process.Start("CMD.exe", strCmdText);
        }

        private void UninstallButton_Click(object sender, EventArgs e)
        {
            string strCmdText = "/K " + Directory.GetCurrentDirectory() + "\\InstallUtil.exe /u " + Directory.GetCurrentDirectory() + "\\Service\\" + ConfigurationManager.AppSettings["EXEname"];
            System.Diagnostics.Process.Start("CMD.exe", strCmdText);
        }
    }
}