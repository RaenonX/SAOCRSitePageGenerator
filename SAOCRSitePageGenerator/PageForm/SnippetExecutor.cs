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

            SetGenerateButtonEnabled(FSP.Controls.Count > 1);
        }
        #endregion

        #region Events
        private string SearchTest_Click(object sender, EventArgs e)
        {
            return GetDTQueryResult((FieldSetup)sender, true);
        }

        private void Generate_Click(object sender, EventArgs e)
        {
            GenerateSnippet(isRepeatedSnippet);
        }
        #endregion

        #region Methods

        #region Control Modifying
        private void SetGenerateButtonEnabled(bool Enabled)
        {
            Generate.Enabled = Enabled;
        }

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
                    ReplaceFields(ref SB, CM.GetConfig(ReadOnly.SnippetFieldBracketL), CM.GetConfig(ReadOnly.SnippetFieldBracketR));

                    ReportProgress(2, MaxProgressCount, "Checking export file path...");
                    if (!CheckSnippetExportFile(SnippetExportPath))
                    {
                        ReportProgress(0, 0, "Export process aborted.");
                        return;
                    }

                    ReportProgress(3, MaxProgressCount, "Exporting snippet...");
                    WriteSnippet(SB, SnippetExportPath);

                    ReportProgress(4, MaxProgressCount, "Checking open exported snippet or not.");
                    CheckToOpenExportedSnippet(SnippetExportPath);

                    DialogResult = DialogResult.OK;
                }
            }
        }

        private string GetDTQueryResult(FieldSetup FS, bool isLayout)
        {
            string DSNameInFS = FS.GetDataSetName();
            string DTNameInFS = FS.GetDataTableName();
            string DTSelectQuery = FS.GetDataTableSelectQuery();
            string DTReturnColumnName = FS.GetDataTableReturnColumnName();

            if (!Static.SnippetExternalSourceName.ContainsValue(DSNameInFS))
            {
                return isLayout ? "Error: Specified DataSet not loaded or not exists." : string.Empty ;
            } else if (string.IsNullOrEmpty(DSNameInFS))
            {
                return isLayout ? "Error: Specified DataSet name empty." : string.Empty;
            } else if (string.IsNullOrEmpty(DTNameInFS))
            {
                return isLayout ? "Error: Specified DataTable name empty." : string.Empty;
            } else if (string.IsNullOrEmpty(DTReturnColumnName))
            {
                return isLayout ? "Error: Specified return DT column name empty." : string.Empty;
            } else
            {
                Guid DSGuid = Static.SnippetExternalSourceName.FirstOrDefault(x => x.Value == DSNameInFS).Key;
                DataSet DSToUse = Static.SnippetExternalSourceDataSet[DSGuid];
                DataRow[] DTQueried;
                Guid DTGuid = new Guid(DSToUse.Tables[ReadOnly.SourceDataTableInfoTableName].Select("[" + ReadOnly.SourceDataTableName + "]='" + DTNameInFS + "'")[0][ReadOnly.SourceDataTableGUID].ToString());

                try
                {
                    DTQueried = DSToUse.Tables[DTGuid.ToString()].Select(DTSelectQuery);
                }
                catch (Exception e)
                {
                    return "Error when querying data. Exception: " + e.GetType().ToString() + ", Message: " + e.Message;
                }

                return DTQueried.Length > 0 ? DTQueried[0][DTReturnColumnName].ToString() : (isLayout ? "Data not found" : string.Empty);
            }
        }

        #region Snippet Generating Process

        private void ReplaceFields(ref StringBuilder SB, string BracketL, string BracketR)
        {
            using (DataTable TempDTForInternalUse = FieldList.Copy())
            {
                foreach (DataRow DR in FieldList.Rows)
                {
                    string FieldName = Convert.ToString(DR[ReadOnly.SnippetFieldName]);
                    string FieldSectionToReplace = BracketL + FieldName + BracketR;
                    bool isAuto = Convert.ToBoolean(DR[ReadOnly.SnippetFieldAutoFill] == DBNull.Value ? false : DR[ReadOnly.SnippetFieldAutoFill]);
                    bool isInternal = Convert.ToBoolean(DR[ReadOnly.SnippetFieldForInternalUse] == DBNull.Value ? false : DR[ReadOnly.SnippetFieldForInternalUse]);

                    if (isInternal)
                    {
                        SB = SB.Replace(FieldSectionToReplace, FSP.FieldCollection[FieldName].GetManualValue());

                        DataRow[] DRA = TempDTForInternalUse.Select("[" + ReadOnly.SnippetFieldName + "]='" + FieldName + "'");
                        DataRow DRT = DRA[0];

                        if (isAuto)
                        {
                            DRT[ReadOnly.SnippetFieldManualValue] = GetDTQueryResult(FSP.FieldCollection[FieldName], false);
                        }
                        else
                        {
                            DRT[ReadOnly.SnippetFieldManualValue] = FSP.FieldCollection[FieldName].GetManualValue();
                        }
                    }
                    else
                    {
                        if (isAuto)
                        {
                            SB = SB.Replace(FieldSectionToReplace, GetDTQueryResult(FSP.FieldCollection[FieldName], false));
                        }
                        else
                        {
                            SB = SB.Replace(FieldSectionToReplace, FSP.FieldCollection[FieldName].GetManualValue());
                        }
                    }
                }
            }
        }

        private bool CheckSnippetExportFile(string ExportPath)
        {
            if (!File.Exists(ExportPath))
            {
                using (FileStream FM = File.Create(ExportPath)) { }
                return true;
            }
            else
            {
                switch (MessageBox.Show("Old exported snippet detected. Replace with new file?", "Old exported snippet detected", MessageBoxButtons.YesNo))
                {
                    case DialogResult.Yes:
                        File.Delete(ExportPath);
                        using (FileStream FM = File.Create(ExportPath)) { }
                        return true;
                    default:
                        MessageBox.Show("Snippet output process aborted.");
                        return false;
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
