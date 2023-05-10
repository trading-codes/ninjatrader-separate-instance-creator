// https://Puvox.Software
// License:  MIT

using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security;
using System.Security.Permissions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NinjaTrader_Starter
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public bool is64BitOS = Environment.Is64BitOperatingSystem;
        private string docsDir = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
        string tmpVersionPath = Environment.GetEnvironmentVariable("tmp") + "nt_mi_last_chosen_verion";
        string tmpClickStartPath = Environment.GetEnvironmentVariable("tmp") + "nt_mi_enable_clickstart";

        private string defaultNinjaDirPath { get { return Environment.GetFolderPath(Environment.SpecialFolder.Personal) + @"\NinjaTrader "+getChosenVersion(); } }
        private string targetNinjaBaseFolder { get { return Environment.GetFolderPath(Environment.SpecialFolder.Personal) + @"\NinjaTrader "+getChosenVersion()+@"\bin\Custom"; } } //Main NT doesnt work for some reason, Access denied
        private string defaultNinjaInstallExe { get {
                var installDir = InstallDir + @"\bin64";
                if (!Directory.Exists(installDir))
                {
                    installDir = InstallDir + @"\bin";
                }
                return installDir + @"\NinjaTrader.exe"; 
            } }

        public string InstallDir
        {
            get
            {
                try
                {
                    // Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) + 
                    RegistryKey registryKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, Environment.Is64BitOperatingSystem ? RegistryView.Registry64 : RegistryView.Registry32);
                    registryKey = registryKey.OpenSubKey("SOFTWARE");
                    if (Environment.Is64BitOperatingSystem)
                    {
                        registryKey = registryKey.OpenSubKey("Wow6432Node");
                    }
                    registryKey = registryKey.OpenSubKey("NinjaTrader, LLC");
                    registryKey = registryKey.OpenSubKey("NinjaTrader "+ getChosenVersion());
                    return (string)registryKey.GetValue("InstallDir");
                }
                catch (Exception)
                {
                    return (Environment.Is64BitOperatingSystem ? Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86) : Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles)) + "\\NinjaTrader 8\\";
                }
            }
        }



        private string specFileName = "z_NT_instance_name_";
        private string defaultInstanceName = "Default instance";
        Dictionary<string, string> allDirs = new Dictionary<string, string>();
        private string nl = Environment.NewLine;

        private void Form1_Load(object sender, EventArgs e)
        {
            initialSetup();
        }

        private void initialSetup()
        {
            // If process running, show error
            if (Process.GetProcessesByName("NinjaTrader").Length != 0)
            {
                m("This can't work while NinjaTrader is running. Please close NinjaTrader and run this app again.");
                Application.Exit();
                return;
            }

            setChosenVersion();

            if (!File.Exists(defaultNinjaInstallExe))
            {
                m("NinjaTrader installation at " + defaultNinjaInstallExe + " wasn't detected.");
                return;
            }
            else if (!Directory.Exists(targetNinjaBaseFolder))
            {
                m("NinjaTrader documents folder wasn't detected.");
                return;
            }
            else if (!File.Exists(tagfilePath(targetNinjaBaseFolder)))
            {
                File.WriteAllText(tagfilePath(targetNinjaBaseFolder), defaultInstanceName);

                if ( !File.Exists(tmpVersionPath) )
                {
                    m("     First time message:  " + nl +
                        "Using this app, you can create separate instances for working/creating/importing your Indicators or strategies, and have different files in each instance. Note: This app is experimental, we don't support the usage. If you meet any problem, the solution provided in HELP should solve the problem."
                    );
                }
            }

            // Fill Form
            createToolTips();
            foreach (DirectoryInfo foundDir in getAllNtFolders())
            {
                string dirPath = foundDir.FullName;

                // If not backup folder
                if(!dirPath.Contains( Path.GetFileName(targetNinjaBaseFolder )+ " Backup"))
                {
                    string targetFile = dirPath + "\\"+ specFileName;
                    if (File.Exists(targetFile))
                    {
                        string name = File.ReadAllText(targetFile);
                        //allDirs[name] = dirPath;
                        insertNewButton(name);
                    }
                }
            }
        }

        private DirectoryInfo[] getAllNtFolders()
        {
            DirectoryInfo hdDirectoryInWhichToSearch = new DirectoryInfo(Path.GetDirectoryName(targetNinjaBaseFolder)   );
            DirectoryInfo[] dirsInDir = hdDirectoryInWhichToSearch.GetDirectories(  Path.GetFileName(targetNinjaBaseFolder) + "*.*");
            return dirsInDir;
        }


        int lastY = 35;
        private void insertNewButton(string buttonText)
        {
            // main button
            Button button = new System.Windows.Forms.Button();
            button.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            button.Size = new System.Drawing.Size( startButtonsPanel.Width-50, 40);
            button.Location = new System.Drawing.Point(3, lastY);
            button.Cursor = Cursors.Hand;
            lastY += button.Size.Height + 4;
            button.Name = buttonText;
            button.Text = buttonText;
            // Set up the ToolTip text for the Button and Checkbox.
            toolTip1.SetToolTip(button, "Switch to this instance");

            //if ACTIVE one, make it green
            if (isActiveInstance(buttonText))
            {
                button.BackColor = Color.Green;
            }


            // add "OpenFolder"
            bool ShowOpenFolder = true;
            if (ShowOpenFolder)
            {
                Button button1 = new Button();
                button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 5.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                button1.Size = new System.Drawing.Size(45, 17);
                button1.Location = new System.Drawing.Point(startButtonsPanel.Width - 50 + 5, lastY - 4- 34);
                button1.Text = "Open folder";
                toolTip1.SetToolTip(button1, "Open folder");
                button1.Tag = buttonText;
                button1.Click += new System.EventHandler(openFolder_click);
                startButtonsPanel.Controls.Add(button1);
            }

            // Add "DELETE" button //btw, it shouldnt be default, we should protect it
            if (!isActiveInstance(buttonText) && defaultInstanceName != buttonText)
            {
                Button button1 = new Button();
                button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 5.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                button1.Size = new System.Drawing.Size(45, 17);
                button1.Location = new System.Drawing.Point(startButtonsPanel.Width - 50 + 5, lastY - 4 - 19);
                button1.Text = "DELETE";
                toolTip1.SetToolTip(button1, "Delete this instance");
                button1.Tag = buttonText;
                button1.ForeColor = Color.Red;
                button1.Click += new System.EventHandler(delete_click);
                startButtonsPanel.Controls.Add(button1);
            }

            button.Click += new System.EventHandler(startNt_Click);
            startButtonsPanel.Controls.Add(button);
        }

        ToolTip toolTip1;
        private void createToolTips()
        {
            // Create the ToolTip and associate with the Form container.
            toolTip1 = new ToolTip();

            // Set up the delays for the ToolTip.
            toolTip1.AutoPopDelay = 5000;
            toolTip1.InitialDelay = 100;
            toolTip1.ReshowDelay = 500;
            // Force the ToolTip text to be displayed whether or not the form is active.
            toolTip1.ShowAlways = true;
        }



        private void startNt_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;
            string dir = targetNinjaBaseFolder;
            System.IO.Directory.Move(dir, dir + prefixedSlug(readDirTagname(dir)));
            Directory.Move(dir + prefixedSlug(button.Text), dir);
            if (startOnClick.Checked)
            {
                System.Diagnostics.Process.Start(defaultNinjaInstallExe);
            }
            this.Close();
        }
        private void delete_click(object sender, EventArgs e)
        {
            Button button = sender as Button;
            string instanceName = button.Tag.ToString();

            if (  DialogResult.OK != MessageBox.Show("Delete  instance?", instanceName, MessageBoxButtons.OKCancel))
                return;
            Library_Puvox.Methods.DeleteDirectory(targetNinjaBaseFolder + prefixedSlug(instanceName) );

            //delete this and start button
            button.Parent.Controls.Remove(button);

            Control[] theMainButt = startButtonsPanel.Controls.Find(instanceName, true);
            theMainButt[0].Parent.Controls.Remove(theMainButt[0]);
        }
        private void openFolder_click(object sender, EventArgs e)
        {
            Button button = sender as Button;
            string instanceName = button.Tag.ToString();
            Process.Start(targetNinjaBaseFolder + prefixedSlug(instanceName));
        }

        /*  var permissionSet = new PermissionSet(PermissionState.None);
            var writePermission = new FileIOPermission(FileIOPermissionAccess.Write, defaultNinjaDirPath);
            permissionSet.AddPermission(writePermission);

            if (permissionSet.IsSubsetOf(AppDomain.CurrentDomain.PermissionSet))
            {
                // do your stuff
                MakeFolderWritable(defaultNinjaDirPath);
                File.SetAttributes(defaultNinjaDirPath, FileAttributes.Normal);
                //new DirectoryInfo(Path.Combine(defaultNinjaDirPath)).MoveTo(Path.Combine( defaultNinjaDirPath, "asd" )  );
                System.IO.Directory.Move(defaultNinjaDirPath, defaultNinjaDirPath +  "asd");
            } 
            */


        string newPathName;
        private void addNew_Click(object sender, EventArgs e)
        {
            string newNameSanitized = sanitizePathName(newInstanceName.Text);
            newPathName = targetNinjaBaseFolder + prefixedSlug(newNameSanitized);  
            if (Directory.Exists(newPathName))
            {
                m("That already exists.");
                return;
            }
            ProgrHideShow(true);
            new Library_Puvox.Methods.CopyFolderProgressBar(ref progressBar1, getWhereverIsCleanInstancePath(), newPathName);
            File.WriteAllText(newPathName + "\\" + specFileName, newNameSanitized);
            ProgrHideShow(false);
            insertNewButton(newNameSanitized);
        }

        public string prefixedSlug(string buttonText)
        {
            return "_" + sanitizePathName(buttonText.Trim());
        }

        private bool isActiveInstance(string instanceName) { return readDirTagnameInStartDir() == instanceName; }

        private string readDirTagnameInStartDir()      {       return readDirTagname(targetNinjaBaseFolder);      }

        private string readDirTagname(string dir)
        {
            string filePath = tagfilePath(dir);
            if( !File.Exists(filePath))
            {
                m("Error. " + filePath + " doesn't exist.");
                return "";
            }
            return File.ReadAllText(filePath);
        }

        private string tagfilePath(string dir)
        {
            return dir + "\\" + specFileName;
        }

        private string getWhereverIsCleanInstancePath()
        {
            string path = "";
            foreach (DirectoryInfo foundDir in getAllNtFolders())
            {
                string targetFile = foundDir.FullName + "\\" + specFileName;
                if (File.Exists(targetFile))
                {
                    string name = File.ReadAllText(targetFile);
                    if (name == defaultInstanceName)
                    {
                        path = foundDir.FullName;
                    }
                }
            }
            return path;
        }


        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://puvox.software/blog/ninjatrader-multi-separated-instances/");
        }

        bool isProgrmamaticChange=false;
        public void setChosenVersion()
        {
            isProgrmamaticChange = true;
            comboBox1.SelectedText = getChosenVersion();
            startOnClick.Checked = getClickStartState() == "1";
            isProgrmamaticChange = false;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isProgrmamaticChange) File.WriteAllText(tmpVersionPath, (sender as ComboBox).Text);
        }
        private string getChosenVersion()
        {
            return File.Exists(tmpVersionPath) ? File.ReadAllText(tmpVersionPath) : "8";  
        }

        private void ProgrHideShow(bool show)
        {
            if (show)
            {
                progressBar1.Value = 2;
                progressPanel.Visible = true; 
                fullPanel1.Visible = false;
                
            }
            else
            {
                progressPanel.Visible = false;
                fullPanel1.Visible = true;
            }

        }


        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            m(@"If any unexpected event happens in this app and NinjaTrader doesnt load correctly, go to 'Documents / NinjaTrader X / bin /' and rename one of the folders to 'Custom' and restart NT.");
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (!isProgrmamaticChange) File.WriteAllText(tmpClickStartPath, (startOnClick.Checked ? "1" : "0"));
        }
        private string getClickStartState()
        {
            return File.Exists(tmpClickStartPath) ? File.ReadAllText(tmpClickStartPath) : "1";
        }


        // ======================  helpers  ======================= //
        private void m(dynamic x) { MessageBox.Show(x.ToString()); }

        public string sanitizePathName(string path)
        {
            return String.Join("_", path.Split(Path.GetInvalidFileNameChars(), StringSplitOptions.RemoveEmptyEntries)).TrimEnd('.');
        }

        private void MakeFolderWritable(string Folder)
        {
            if (IsFolderReadOnly(Folder))
            {
                System.IO.DirectoryInfo oDir = new System.IO.DirectoryInfo(Folder);
                oDir.Attributes = oDir.Attributes & ~System.IO.FileAttributes.ReadOnly;
            }
        }
        private bool IsFolderReadOnly(string Folder)
        {
            System.IO.DirectoryInfo oDir = new System.IO.DirectoryInfo(Folder);
            return ((oDir.Attributes & System.IO.FileAttributes.ReadOnly) > 0);
        }

    }
}









namespace Library_Puvox
{
    public partial class Methods
    {

        public static string homepage = "https://puvox.software";

        public static void DeleteDirectory(string target_dir)
        {
            string[] files = Directory.GetFiles(target_dir);
            string[] dirs = Directory.GetDirectories(target_dir);

            foreach (string file in files)
            {
                File.SetAttributes(file, FileAttributes.Normal);
                File.Delete(file);
            }

            foreach (string dir in dirs)
            {
                DeleteDirectory(dir);
            }

            Directory.Delete(target_dir, false);
        }




        public class CopyFolderProgressBar
        {

            private void m(dynamic x) { MessageBox.Show(x.ToString()); }

            private ProgressBar progressBar;
            private string sourcePath;
            private string targetPath;
            private long sourceSize, targetSize;

            public CopyFolderProgressBar(ref ProgressBar progressBar_, string sourcePath_, string targetPath_)
            {
                ProgressBarStart(ref progressBar_, sourcePath_, targetPath_);
            }

            BackgroundWorker worker_copy = new BackgroundWorker();
            public void ProgressBarStart(ref ProgressBar progressBar_, string sourcePath_, string targetPath_)
            {
                sourcePath = sourcePath_;
                targetPath = targetPath_;
                sourceSize = GetDirectorySize(sourcePath);
                //new System.Threading.Thread(delegate () {    .....      }).Start();
                copyFolder(ref progressBar_, sourcePath, targetPath);
            }


            public void updateProgrBar(ref ProgressBar progressBar_)
            {
                if (Directory.Exists(targetPath))
                {
                    int pctn = (int) ( ((double)GetDirectorySize(targetPath) / (double)sourceSize) * 100);
                    progressBar_.Value = pctn;
                } 
            }

            public long GetDirectorySize(object dirPathOrInfo)
            {
                DirectoryInfo d = dirPathOrInfo is string ? new DirectoryInfo(dirPathOrInfo as string) : (dirPathOrInfo as DirectoryInfo);

                long size = 0;
                // Add file sizes.
                FileInfo[] fis = d.GetFiles();
                foreach (FileInfo fi in fis)
                {
                    size += fi.Length;
                }
                // Add subdirectory sizes.
                DirectoryInfo[] dis = d.GetDirectories();
                foreach (DirectoryInfo di in dis)
                {
                    size += GetDirectorySize(di);
                }
                return size;
            }


            // copy folder
            public void copyFolder(ref ProgressBar progressBar_, string source, string target)
            {
                copyFolder(ref progressBar_, new DirectoryInfo(source), new DirectoryInfo(target));
            }

            public void copyFolder(ref ProgressBar progressBar_, DirectoryInfo source, DirectoryInfo target)
            {
                updateProgrBar(ref progressBar_);
                foreach (DirectoryInfo dir in source.GetDirectories())
                    copyFolder(ref progressBar_, dir, target.CreateSubdirectory(dir.Name));
                foreach (FileInfo file in source.GetFiles())
                    file.CopyTo(Path.Combine(target.FullName, file.Name));
            }

        }





    }
}