using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SAOCRSitePageGenerator
{
    public partial class SnippetExecutor : Form
    {
        bool isRepeatedSnippet;
        DataTable FieldList;
        KeyValuePair<string, string> SnippetInfo;
        FieldSetupPanel FSP;

        #region Initialize
        public SnippetExecutor(KeyValuePair<string, string> SnippetInfo)
        {
            this.SnippetInfo = SnippetInfo;

            CommonInitialize();
        }

        private void CommonInitialize()
        {
            InitializeComponent();
            InitializeEventHandler();
            Initialize();
        }

        private void InitializeEventHandler()
        {
            Generate.Click += Generate_Click;
        }

        private void Initialize()
        {
            string SnippetPath = ReadOnly.SnippetsPath + "/" + SnippetInfo.Key;

            FieldList = LoadFieldList(SnippetInfo.Key);
            GenerateAndShowFieldSetupPanel(FieldList, SearchTest_Click);
            using (ConfigManager CM = new ConfigManager(SnippetPath + "/" + ReadOnly.SnippetStructConfig))
            {
                isRepeatedSnippet = Convert.ToBoolean(CM.GetConfig(ReadOnly.SnippetIsRepeatedSnippet));
                OutputDestination.Text = CM.GetConfig(ReadOnly.SnippetDestination);
            }
        }
        #endregion

        #region Events
        private void SearchTest_Click(object sender, EventArgs e)
        {
            FieldSetup FM = (FieldSetup)sender;

        }

        private void Generate_Click(object sender, EventArgs e)
        {
            GenerateSnippet(isRepeatedSnippet);
        }
        #endregion

        #region Methods

        #region Control Modifying
        private void GenerateAndShowFieldSetupPanel(DataTable FieldList, SearchTestHandler SearchTestMethod)
        {
            FSP = new FieldSetupPanel(FieldList, SearchTestMethod);
            FSP.Location = new Point(0, 0);
            FSP.Size = Panel.Size;
            Panel.Controls.Add(FSP);
        }

        private void ReportProgress(int ProgressValue, int MaxValue, string Text)
        {
            ProgressText.Text = Text + " (" + ProgressValue + "/" + MaxValue + ")";
            Application.DoEvents();
        }
        #endregion

        #region Resources Loading Process
        private DataTable LoadFieldList(string SnippetKey)
        {
            DataTable DT = new DataTable(ReadOnly.SnippetStructFieldList);
            string FieldListPath = ReadOnly.SnippetsPath + "/" + SnippetKey + "/" + ReadOnly.SnippetStructFieldList;

            DT.ReadXml(FieldListPath);

            return DT;
        }

        private StringBuilder LoadSnippet(string SnippetKey)
        {
            string FieldListPath = ReadOnly.SnippetsPath + "/" + SnippetKey + "/" + ReadOnly.SnippetStructSnippet;

            StringBuilder SB = new StringBuilder(new StreamReader(FieldListPath).ReadToEnd());

            return SB;
        }

        #endregion

        private void GenerateSnippet(bool isRepeatedSnippet)
        {
            using (ConfigManager CM = new ConfigManager(ReadOnly.SnippetsPath + "/" + SnippetInfo.Key + "/" + ReadOnly.SnippetStructConfig))
            {
                string SnippetExportPath = CM.GetConfig(ReadOnly.SnippetDestination) + "/" + SnippetInfo.Key + CM.GetConfig(ReadOnly.SnippetOutputExtension);
                StringBuilder SB = LoadSnippet(SnippetInfo.Key);

                if (isRepeatedSnippet)
                {

                }
                else
                {
                    int MaxProgressCount = 4;

                    ReportProgress(1, MaxProgressCount, "Replacing field with manual data...");
                    ReplaceManualField(ref SB, CM.GetConfig(ReadOnly.SnippetFieldBracketL), CM.GetConfig(ReadOnly.SnippetFieldBracketR));

                    ReportProgress(2, MaxProgressCount, "Checking export file path...");
                    CheckSnippetExportFile(SnippetExportPath);

                    ReportProgress(3, MaxProgressCount, "Exporting snippet...");
                    WriteSnippet(SB, SnippetExportPath);

                    ReportProgress(4, MaxProgressCount, "Checking open exported snippet or not.");
                    CheckToOpenExportedSnippet(SnippetExportPath);

                    DialogResult = DialogResult.OK;
                }
            }
        }

        #region Snippet Generating Process
        private StringBuilder ReplaceManualField(ref StringBuilder SB, string BracketL, string BracketR)
        {
            foreach (DataRow DR in FieldList.Rows)
            {
                string FieldName = Convert.ToString(DR[ReadOnly.SnippetFieldName]);
                bool isAuto = Convert.ToBoolean(DR[ReadOnly.SnippetFieldAutoFill] == DBNull.Value ? false : DR[ReadOnly.SnippetFieldAutoFill]);
                bool isInternal = Convert.ToBoolean(DR[ReadOnly.SnippetFieldForInternalUse] == DBNull.Value ? false : DR[ReadOnly.SnippetFieldForInternalUse]);
                SB = !isAuto && !isInternal ? SB.Replace(BracketL + FieldName + BracketR, FSP.FieldCollection[FieldName].GetManualValue()) : SB;
            }
            return SB;
        }

        private void CheckSnippetExportFile(string ExportPath)
        {
            if (!File.Exists(ExportPath))
                using (FileStream FM = File.Create(ExportPath)) { }
            else
            {
                switch (MessageBox.Show("Old exported snippet detected. Replace with new file?", "Old exported snippet detected", MessageBoxButtons.YesNo))
                {
                    case DialogResult.Yes:
                        File.Delete(ExportPath);
                        using (FileStream FM = File.Create(ExportPath)) { }
                        break;
                    case DialogResult.No:
                        MessageBox.Show("Snippet output process aborted.");
                        return;
                }
            }
        }

        private void WriteSnippet(StringBuilder SB, string SnippetExportPath)
        {
            using (StreamWriter SW = new StreamWriter(SnippetExportPath))
            {
                SW.Write(SB.ToString());
            }
        }

        private void CheckToOpenExportedSnippet(string SnippetExportPath)
        {
            if (MessageBox.Show("Snippet exporting completed.\n\nExported Path: " + SnippetExportPath + "\nOpen exported snippet?", "Snippet exporting completed", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Process.Start(SnippetExportPath);
            }
        }

        #endregion

        #endregion
    }
}
