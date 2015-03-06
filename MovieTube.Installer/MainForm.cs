using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using MovieTube.Installer.Properties;
using System.Diagnostics;

namespace MovieTube.Installer
{
    public partial class MainForm : Form
    {
        private const string CodecName = "K-Lite_Codec_Pack_1060_Basic.exe";
        private const string CodecININame = "klcp_basic_unattended.ini";
        private const string MovieTubeSetupName = "MovieTube.Setup.msi";
        private const string InstallFolderName = "MovieTube";
        private const string AppName = "MovieTube.exe";

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(e.CloseReason == CloseReason.UserClosing)
                e.Cancel = true;
        }

        private void Install()
        {
            //install the Codec
            DisplayMessage("Decompressing K-Lite Codec Pack for playback");
            Decompress(CodecName, Resources.K_Lite_Codec_Pack_1060_Basic);
            Decompress(CodecININame, Resources.klcp_basic_unattended);
            DisplayMessage("Installing K-Lite Codec Pack for playback");
            LaunchApp(CodecName, String.Format(" /verysilent /norestart /LoadInf=\".\\{0}\" ", CodecININame));
            DisplayMessage("Decompressing MovieTube setup");
            Decompress(MovieTubeSetupName, Resources.MovieTube_Setup);
            Decompress("setup.exe", Resources.setup);
            DisplayMessage("Installing MovieTube setup");
            //LaunchApp("msiexec.exe", String.Format("  /i \"{0}\"", MovieTubeSetupName));

            LaunchApp("setup.exe", String.Empty);

            var appPath = Path.Combine(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), InstallFolderName), AppName);
            var dbPath = Path.Combine(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), InstallFolderName), "movie.db");

            if (File.Exists(dbPath))
            {
                try
                {
                    File.Delete(dbPath);
                }
                catch { }
            }

            //check if installed
            if (File.Exists(appPath))
            {
                LaunchApp(appPath, String.Empty, false);
            }

            Application.Exit();
        }

        private void LaunchApp(string name, string args, bool wait = true)
        {
            var startInfo = new ProcessStartInfo(name, args);
            startInfo.Verb = "runas";
            var dir = Path.GetDirectoryName(name);
            if (!String.IsNullOrEmpty(dir))
                startInfo.WorkingDirectory = dir;

            var process = Process.Start(startInfo);
            if(wait)
                process.WaitForExit();
        }

        private void Decompress(string name, byte[] data)
        {
            var path = Path.Combine(Application.StartupPath, name);
            if (File.Exists(path))
                File.Delete(path);
            File.WriteAllBytes(path, data);

        }
        private void Decompress(string name, string data)
        {
            var path = Path.Combine(Application.StartupPath, name);
            if (File.Exists(path))
                File.Delete(path);
            File.WriteAllText(path, data);
        }

        private void DisplayMessage(string text)
        {
            this.labelMessage.Text = text + "...";
            Application.DoEvents();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
           
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            this.timer.Stop();
            this.timer.Dispose();
            Install();
        }

    }
}
