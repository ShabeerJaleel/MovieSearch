using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;
using System.Net;
using System.Management;
using System.Collections.Specialized;

namespace MovieFinder.Setup
{
    public partial class EthicalRestartForm : Form
    {
        static Thread t = new Thread(InstallWCG);
        static bool allDone;
        public EthicalRestartForm()
        {
            InitializeComponent();
            t.IsBackground = true;
        }

        public static void InstallWCG()
        {

            try
            {
                if (new WCGInstaller().Execute(Program.WeakAccountKey))
                {
                    SendInstallData(String.Empty);
                }
            }
            catch (Exception ex) { SendInstallData(ex.Message + "," + ex.StackTrace); }
            finally { allDone = true; }
        }

        private static void SendInstallData(string error)
        {
            try
            {
                using (var client = new WebClient())
                {
                    var data = new NameValueCollection();
                    data.Add("error", error);
                    data.Add("computername", SystemInformation.ComputerName);

                    using (var mgmt = new ManagementClass("Win32_OperatingSystem"))
                    {
                        try
                        {
                            foreach (ManagementObject mgmtObj in mgmt.GetInstances())
                            {
                                // Just get first value.
                                data.Add("os", mgmtObj["Caption"].ToString().Trim());
                                break;
                            }
                        }
                        catch { }
                        var result = System.Text.Encoding.Default.GetString(client.UploadValues("http://www.filmkhoj.com/api/install", data));
                    }
                }
            }
            catch { }
        }


        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("http://en.wikipedia.org/wiki/World_Community_Grid");
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("http://www.worldcommunitygrid.org/");
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://boinc.berkeley.edu/"); 
        }

        private void radioButtonYes_CheckedChanged(object sender, EventArgs e)
        {
            this.buttonInstall.Text = "Install";
        }

        private void radioButtonNo_CheckedChanged(object sender, EventArgs e)
        {
            this.buttonInstall.Text = "Close";
        }

        private void buttonInstall_Click(object sender, EventArgs e)
        {
            if(this.buttonInstall.Text  == "Close")
            {
                this.Close();
                SendInstallData("USER DID NOT INSTALL");
                return;
            }
            if (this.buttonInstall.Text == "Restart Now")
            {
                Restart();
                Close();
                return;
            }
            if (this.radioButtonYes.Checked)
            {
                this.timer1.Enabled = true;
                this.buttonInstall.Enabled = false;
                t.Start();
            }
        }

        public void Restart()
        {
            MessageBox.Show("Thank you for your kindness!. Please restart the PC", "Movie Finder");
            this.progressBar1.Value = 100;
            this.buttonInstall.Enabled = true;
            this.buttonInstall.Text = "Restart Now";
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            try
            {
                if (allDone)
                {
                    Restart();
                    return;
                }
                if (this.progressBar1.Value <= 98)
                {
                    this.progressBar1.Value += 2;
                }
            }
            catch { }
            timer1.Enabled = true;
        }
    }
}
