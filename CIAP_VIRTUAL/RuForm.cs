using RuFramework.Config;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CIAP_VIRTUAL
{
    public partial class CIAP_VIRTUAL : Form
    {
        #region Init
        // Settings
        public AppSettings appSettings = new AppSettings();

        String MyFileName = null;
        public CIAP_VIRTUAL()
        {
            InitializeComponent();
            #region AssemblyInfo
            // ######################################################################################################
            // Requires entries in AssemblyInfo.cs for RuSplash
            // [assembly: AssemblyTitle("CIAP_VIRTUAL")]
            // [assembly: AssemblyCompany("MyCompany")]
            // [assembly: AssemblyProduct("CIAP_VIRTUAL")]
            // [assembly: AssemblyCopyright("Copyright © MyCompany 2018")]
            // [assembly: AssemblyMetadata("url", "https://marketplace.visualstudio.com/")]
            // [assembly: AssemblyMetadata("urlText", "Extensions for the Visual Studio family of products")]
            // ######################################################################################################
            #endregion
            #region Init RuSplash
            // ######################################################################################################
            // Initialize SplashScreen

            // Do not show the AboutBox as splash screen
            // ruSplash1.Visible = false;

            // GetAssemblyAttribute
            // Title, Description, Configuration, Company, Product
            // Copyright, Trademark, Metadata ("url")( "urlText"), Version, FileVersion

            ruSplash1.ApplicationName = GetAssemblyAttribute("Title");
            ruSplash1.CopyRight = GetAssemblyAttribute("Copyright");
            ruSplash1.ApplicationVersion = GetAssemblyAttribute("Version");
            ruSplash1.Url = GetAssemblyAttribute("Metadata", "url");
            ruSplash1.UrlText = GetAssemblyAttribute("Metadata", "urlText");
            ruSplash1.FontSize = 9;
            ruSplash1.CompanyImage = ((System.Drawing.Bitmap)GetResoureObject("CIAP_VIRTUAL.Resources.Menu", "KR_neu"));
            // ######################################################################################################
            #endregion
            #region Lingual
            // ######################################################################################################
            // Initialize menus in the local language 
            // Test multi-lingual
            // CultureInfo ci;
            // ci = CultureInfo.CreateSpecificCulture("en-US");
            // ci = CultureInfo.CreateSpecificCulture("de-DE");
            // System.Threading.Thread.CurrentThread.CurrentCulture = ci;
            InitMenuText();
            // ######################################################################################################
            #endregion
            #region Appsettings
            // Select the config data path
            //                                          ConfigManager.GetAppDataPath(AppDataPath.Local);
            //                                          ConfigManager.GetAppDataPath(AppDataPath.Common);
            //                                          ConfigManager.GetAppDataPath(AppDataPath.ExePath);
            //                                          ConfigManager.GetAppDataPath(AppDataPath.Roaming);
            // Default read data = ConfigManager.Read(ConfigManager.GetAppDataPath(AppDataPath.Roaming))
            appSettings = ConfigManager.Read(); // default Roaming

            appSettings.MyString = "Property MyString changed";
            // ConfigManager.Save(appSettings, AppDataPath.Roaming);
            // or
            ConfigManager.Save(appSettings); // default Roaming
            #endregion
        }
        #region Helpers
        private string GetAssemblyAttribute(string Attribute, string Key = null)
        {
            // Title, Description, Configuration, Company, Product
            // Copyright, Trademark, Metadata ("url"), Version, FileVersion

            string returnValue = null;
            Assembly asm = Assembly.GetExecutingAssembly();

            switch (Attribute.ToUpper())
            {
                case "TITLE":
                    returnValue = (asm.GetCustomAttributes(typeof(AssemblyTitleAttribute), false)[0] as AssemblyTitleAttribute).Title;
                    break;
                case "DESCRIPTION":
                    returnValue = (asm.GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false)[0] as AssemblyDescriptionAttribute).Description;
                    break;
                case "CONFIGURATION":
                    returnValue = (asm.GetCustomAttributes(typeof(AssemblyConfigurationAttribute), false)[0] as AssemblyConfigurationAttribute).Configuration;
                    break;
                case "COMPANY":
                    returnValue = (asm.GetCustomAttributes(typeof(AssemblyCompanyAttribute), false)[0] as AssemblyCompanyAttribute).Company;
                    break;
                case "PRODUCT":
                    returnValue = (asm.GetCustomAttributes(typeof(AssemblyProductAttribute), false)[0] as AssemblyProductAttribute).Product;
                    break;
                case "COPYRIGHT":
                    returnValue = (asm.GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false)[0] as AssemblyCopyrightAttribute).Copyright;
                    break;
                case "TRADEMARK":
                    returnValue = (asm.GetCustomAttributes(typeof(AssemblyTrademarkAttribute), false)[0] as AssemblyTrademarkAttribute).Trademark;
                    break;
                case "METADATA":
                    object[] metadata = asm.GetCustomAttributes(typeof(AssemblyMetadataAttribute), false);
                    foreach (AssemblyMetadataAttribute item in metadata)
                    {
                        if (item.Key.ToUpper() == Key.ToUpper()) returnValue = item.Value;
                    }
                    break;
                case "VERSION":
                    returnValue = Assembly.GetExecutingAssembly().GetName().Version.ToString();
                    break;
                case "FILEVERSION":
                    returnValue = (asm.GetCustomAttributes(typeof(AssemblyFileVersionAttribute), false)[0] as AssemblyFileVersionAttribute).Version;
                    break;
                default:
                    break;
            }
            return returnValue;
        }
        /// <summary>
        /// Init menus with localized Text
        /// </summary>
        private void InitMenuText()
        {

            string Location = "CIAP_VIRTUAL.Resources.Menu";
            fileToolStripMenuItem.Text = GetResoureString(Location, "fileToolStripMenuItem");
            editToolStripMenuItem.Text = GetResoureString(Location, "editToolStripMenuItem");
            newToolStripMenuItem.Text = GetResoureString(Location, "newToolStripMenuItem");
            openToolStripMenuItem.Text = GetResoureString(Location, "openToolStripMenuItem");
            closeToolStripMenuItem.Text = GetResoureString(Location, "closeToolStripMenuItem");
            saveToolStripMenuItem.Text = GetResoureString(Location, "saveToolStripMenuItem");
            saveAsToolStripMenuItem.Text = GetResoureString(Location, "saveAsToolStripMenuItem");
            printToolStripMenuItem.Text = GetResoureString(Location, "printToolStripMenuItem");
            exitToolStripMenuItem.Text = GetResoureString(Location, "exitToolStripMenuItem");
            addToolStripMenuItem.Text = GetResoureString(Location, "addToolStripMenuItem");
            removeToolStripMenuItem.Text = GetResoureString(Location, "removeToolStripMenuItem");
            settingsToolStripMenuItem.Text = GetResoureString(Location, "settingsToolStripMenuItem");
            helpToolStripMenuItem.Text = GetResoureString(Location, "helpToolStripMenuItem");
            aboutToolStripMenuItem.Text = GetResoureString(Location, "aboutToolStripMenuItem");
        }
        // Get localized Message and toolStripStatusLabel-text 
        private string GetMenuMsg(string Name)
        {
            string Location = "CIAP_VIRTUAL.Resources.Menu";
            return GetResoureString(Location, Name);
        }
        #endregion
        #endregion
        #region Menu File
        // New file
        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                MyFileName = "DummyFile.txt";
                // ToDo
                this.toolStripStatusLabel1.Text = MyFileName + " " + GetMenuMsg("msgNew");
                HandlingMenuWorking();
            }
            catch (Exception)
            {
                MessageBox.Show(GetMenuMsg("msgNewError"), "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Exclamation,
                                MessageBoxDefaultButton.Button1);
                HandlingMenuClose();
            }
        }
        // Open file
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                openFileDialog1.InitialDirectory = @"C:\";
                openFileDialog1.DefaultExt = "txt";
                openFileDialog1.FileName = MyFileName;

                openFileDialog1.Title = "Browse Text Files";

                openFileDialog1.CheckFileExists = true;
                openFileDialog1.CheckPathExists = true;

                openFileDialog1.DefaultExt = "txt";
                openFileDialog1.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
                openFileDialog1.FilterIndex = 1;
                openFileDialog1.RestoreDirectory = true;

                openFileDialog1.ReadOnlyChecked = true;
                openFileDialog1.ShowReadOnly = true;

                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    MyFileName = openFileDialog1.FileName;

                    // ToDo
                    this.toolStripStatusLabel1.Text = MyFileName + " " + GetMenuMsg("msgOpen");
                    HandlingMenuWorking();
                }
            }
            catch (Exception)
            {
                MessageBox.Show(GetMenuMsg("msgOpenError"), "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Exclamation,
                                MessageBoxDefaultButton.Button1);
                HandlingMenuClose();
            }
        }
        // Close file
        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                // ToDo
                this.toolStripStatusLabel1.Text = MyFileName + " " + GetMenuMsg("msgClose");
                HandlingMenuClose();
            }
            catch (Exception)
            {
                MessageBox.Show(GetMenuMsg("msgCloseError"), "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Exclamation,
                                MessageBoxDefaultButton.Button1);
                HandlingMenuClose();
            }
        }
        // Save file
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                // ToDo
                this.toolStripStatusLabel1.Text = MyFileName + " " + GetMenuMsg("msgSave") + " " + saveFileDialog1.FileName;
                HandlingMenuClose();
            }
            catch (Exception)
            {
                MessageBox.Show(GetMenuMsg("msgSaveError"), "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Exclamation,
                                MessageBoxDefaultButton.Button1);
                HandlingMenuClose();
            }
        }
        // Save file as ...
        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                saveFileDialog1.InitialDirectory = @"C:\";
                saveFileDialog1.FileName = Path.GetFileName(MyFileName);
                saveFileDialog1.Title = "Save text Files";
                saveFileDialog1.CheckFileExists = false;
                saveFileDialog1.CheckPathExists = true;
                saveFileDialog1.DefaultExt = "txt";
                saveFileDialog1.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
                saveFileDialog1.FilterIndex = 2;
                saveFileDialog1.RestoreDirectory = true;

                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    this.toolStripStatusLabel1.Text = GetMenuMsg("msgSaveAs") + " " + saveFileDialog1.FileName;
                    // ToDo
                    // Save File
                    HandlingMenuClose();
                }
            }
            catch (Exception)
            {
                MessageBox.Show(GetMenuMsg("msgSaveAsError"), "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Exclamation,
                                MessageBoxDefaultButton.Button1);
                HandlingMenuClose();
            }
        }
        // Print file
        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                // ToDo
                this.toolStripStatusLabel1.Text = MyFileName + " " + GetMenuMsg("msgPrint");
            }
            catch (Exception)
            {
                MessageBox.Show(GetMenuMsg("msgPrintError") + " ", "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Exclamation,
                                MessageBoxDefaultButton.Button1);
            }
        }
        // Exit application
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Exit application
            Application.Exit();
        }
        #endregion
        #region Menu Edit
        // Add Item
        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                // ToDo
                this.toolStripStatusLabel1.Text = GetMenuMsg("msgAdd");
            }
            catch (Exception)
            {
                MessageBox.Show(GetMenuMsg("msgAddError"), "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Exclamation,
                                MessageBoxDefaultButton.Button1);
                HandlingMenuClose();
            }
        }
        // Remove Item
        private void removeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                // ToDo
                this.toolStripStatusLabel1.Text = GetMenuMsg("msgRemove");
            }
            catch (Exception)
            {
                MessageBox.Show(GetMenuMsg("msgRemoveError"), "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Exclamation,
                                MessageBoxDefaultButton.Button1);
                HandlingMenuClose();
            }
        }
        // Open Settings
        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                //AppSettingsDialog appSettingsDialog = new AppSettingsDialog(appSettings, AppDataPath.Roaming);
                AppSettingsDialog appSettingsDialog = new AppSettingsDialog(appSettings); // default Roaming
                appSettingsDialog.propertyGrid.SelectedObject = appSettings;
                appSettingsDialog.ShowDialog();
                appSettings = appSettingsDialog.AppSettingsOk;
            }
            catch (Exception)
            {
                MessageBox.Show(GetMenuMsg("msgSettingsError") + " ", "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Exclamation,
                                MessageBoxDefaultButton.Button1);
                HandlingMenuClose();
            }
        }
        #endregion
        #region Menu Info
        // Open HelpFile
        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                this.toolStripStatusLabel1.Text = null;
                string helpFileName = System.IO.Path.Combine(Application.StartupPath, "CIAP_VIRTUAL.chm");
                if (System.IO.File.Exists(helpFileName)) Help.ShowHelp(this, helpFileName);
            }
            catch (Exception)
            {
                MessageBox.Show(GetMenuMsg("msgHelpError"), "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Exclamation,
                                MessageBoxDefaultButton.Button1);
            }
        }
        // Info RuSplash
        private void infoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.toolStripStatusLabel1.Text = null;
            // Enable About-Control permanently
            ruSplash1.AsSplash = false;
            ruSplash1.Visible = true;
        }
        #endregion
        #region Enable/Disable Menu
        // Enable Menu by working
        private void HandlingMenuWorking()
        {
            newToolStripMenuItem.Enabled = false;
            openToolStripMenuItem.Enabled = false;
            closeToolStripMenuItem.Enabled = true;
            saveToolStripMenuItem.Enabled = true;
            saveAsToolStripMenuItem.Enabled = true;
            printToolStripMenuItem.Enabled = true;
            addToolStripMenuItem.Enabled = true;
            removeToolStripMenuItem.Enabled = true;
        }
        // Disanable Menu by working
        private void HandlingMenuClose()
        {
            newToolStripMenuItem.Enabled = true;
            openToolStripMenuItem.Enabled = true;
            closeToolStripMenuItem.Enabled = false;
            saveToolStripMenuItem.Enabled = false;
            saveAsToolStripMenuItem.Enabled = false;
            printToolStripMenuItem.Enabled = false;
            addToolStripMenuItem.Enabled = false;
            removeToolStripMenuItem.Enabled = false;

        }
        #endregion
        #region GetResources
        public String GetResoureString(String Location, String Name)
        {
            System.Reflection.Assembly resourceAssembly = this.GetType().Assembly;
            System.Resources.ResourceManager rmStrings;
            rmStrings = new System.Resources.ResourceManager(Location, resourceAssembly);
            return rmStrings.GetString(Name, System.Threading.Thread.CurrentThread.CurrentCulture);
        }
        public Object GetResoureObject(String Location, String Name)
        {
            System.Reflection.Assembly resourceAssembly = this.GetType().Assembly;
            System.Resources.ResourceManager rmObject;
            rmObject = new System.Resources.ResourceManager(Location, resourceAssembly);
            return rmObject.GetObject(Name, System.Threading.Thread.CurrentThread.CurrentCulture);
        }
        #endregion

        private void CIAP_VIRTUAL_Load(object sender, EventArgs e)
        {

        }

        private void ruSplash1_Load(object sender, EventArgs e)
        {

        }
    }
}
