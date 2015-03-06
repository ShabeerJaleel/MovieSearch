using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using System.Diagnostics;
using MovieTube.Viewer.Properties;
using MovieTube.Viewer.Data;
using System.Threading;

namespace MovieTube.Viewer
{
    partial class AboutBox : Form
    {
        private Thread versionCheckThread;
        public AboutBox()
        {
            this.versionCheckThread = new Thread(CheckVersion);
            this.versionCheckThread.Start();
            InitializeComponent();
            this.Text = String.Format("About {0}", Constants.AppTitle);
            this.labelProductName.Text = Constants.AppTitle;
            this.labelLatestVersion.Text = this.labelInstalledVersion.Text = AssemblyVersion;
            this.labelCopyright.Text = AssemblyCopyright;
            this.labelCompanyName.Text = AssemblyCompany;
            this.labelDBVersion.Text = ClientDataService.Single.MovieDBVersion.ToString();
            this.labelLastChecked.Text = ClientDataService.Single.Settings.DBVersionCheckedTime.ToString();
            
        }

        #region Assembly Attribute Accessors

        public string AssemblyTitle
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
                if (attributes.Length > 0)
                {
                    AssemblyTitleAttribute titleAttribute = (AssemblyTitleAttribute)attributes[0];
                    if (titleAttribute.Title != "")
                    {
                        return titleAttribute.Title;
                    }
                }
                return System.IO.Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
            }
        }

        public string AssemblyVersion
        {
            get
            {
                return Assembly.GetExecutingAssembly().GetName().Version.ToString();
            }
        }

        public string AssemblyDescription
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyDescriptionAttribute)attributes[0]).Description;
            }
        }

        public string AssemblyProduct
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyProductAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyProductAttribute)attributes[0]).Product;
            }
        }

        public string AssemblyCopyright
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
            }
        }

        public string AssemblyCompany
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyCompanyAttribute)attributes[0]).Company;
            }
        }
        #endregion

        private void linkLabel1_Click(object sender, EventArgs e)
        {
            Process.Start(UrlConstants.WebsiteUrl);
        }

        private void CheckVersion()
        {
            var version = new UpdaterService().GetLatestAppVersion(true);
            if (!this.IsDisposed && version != Constants.AppVersion)
            {
                this.InvokeEx(() =>
                {
                    this.labelLatestVersion.Text = version.ToString();
                    this.button1.Visible = true;
                });
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Process.Start(UrlConstants.AppDownloadUrl);
        }
    }
}
