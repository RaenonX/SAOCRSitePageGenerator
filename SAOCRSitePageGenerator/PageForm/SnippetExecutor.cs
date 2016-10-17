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
        DataTable FieldList, ProcessedFL;
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
            ProcessedFL = GetSortedFieldList(FieldList);
            GenerateAndShowFieldSetupPanel(FieldList, SearchTest_Click);

            using (ConfigManager CM = new ConfigManager(SnippetPath + "/" + ReadOnly.SnippetStructConfig))
            {
                isRepeatedSnippet = Convert.ToBoolean(CM.GetConfig(ReadOnly.SnippetIsRepeatedSnippet));
                OutputDestination.Text = CM.GetConfig(ReadOnly.SnippetDestination);
            }

            SetGenerateButtonEnabled(FSP.FieldCollection.Count > 1);
        }
        #endregion

        #region Events
        private string SearchTest_Click(object sender, EventArgs e)
        {
            return GetDTQueryResult(((FieldSetup)sender).GetFieldName(), true);
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

        private DataTable GetSortedFieldList(DataTable FieldList)
        {
            DataView DV = FieldList.AsDataView();
            DV.Sort = ReadOnly.SnippetFieldIndex + " ASC, " + ReadOnly.SnippetFieldName + " ASC";
            return DV.ToTable();
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
                    int MaxProgressCount = 5;

                    ReportProgress(1, MaxProgressCount, "Checking export file path...");
                    if (!CheckSnippetExportFile(SnippetExportPath))
                    {
                        ReportProgress(0, 0, "Export process aborted.");
                        return;
                    }

                    ReportProgress(2, MaxProgressCount, "Inserting value to datatable...");
                    InsertValuesToProcessedDataTable();

                    ReportProgress(3, MaxProgressCount, "Replacing field with manual data...");
                    ReplaceFields(ref SB, CM.GetConfig(ReadOnly.SnippetFieldBracketL), CM.GetConfig(ReadOnly.SnippetFieldBracketR));

                    ReportProgress(4, MaxProgressCount, "Exporting snippet...");
                    WriteSnippet(SB, SnippetExportPath);

                    ReportProgress(5, MaxProgressCount, "Checking open exported snippet or not.");
                    CheckToOpenExportedSnippet(SnippetExportPath);

                    DialogResult = DialogResult.OK;
                }
            }
        }

        private string GetDTQueryResult(string FieldName, bool isLayout)
        {
            FieldSetup FS;

            try
            {
                FS = FSP.FieldCollection[FieldName];
            }
            catch (Exception e)
            {
                return isLayout ? "Error when getting control from collection. Exception: " + e.GetType().ToString() + ", Message: " + e.Message : string.Empty;
            }

            string DSNameInFS = FS.GetDataSetName();
            string DTNameInFS = FS.GetDataTableName();
            string DTReturnColumnName = FS.GetDataTableReturnColumnName();

            string DTSelectQuery = FS.GetDataTableSelectQuery();
            List<string> FieldToInputList = new List<string>();
            int IDXStart = DTSelectQuery.IndexOf(ReadOnly.FieldInQueryIdentifyCharL);
            int IDXEnd = DTSelectQuery.IndexOf(ReadOnly.FieldInQueryIdentifyCharR);

            while (IDXStart > -1 && IDXEnd > -1)
            {
                string OperatorRemovedFieldName = DTSelectQuery.Substring(IDXStart, IDXEnd - IDXStart).Replace(ReadOnly.FieldInQueryIdentifyCharL, string.Empty).Replace(ReadOnly.FieldInQueryIdentifyCharR, string.Empty);

                FieldToInputList.Add(OperatorRemovedFieldName);
                DTSelectQuery = DTSelectQuery.Substring(IDXEnd);
                IDXStart = DTSelectQuery.IndexOf(ReadOnly.FieldInQueryIdentifyCharL);
                IDXEnd = DTSelectQuery.IndexOf(ReadOnly.FieldInQueryIdentifyCharR);
            }

            foreach (string Field in FieldToInputList)
            {
                DTSelectQuery = FS.GetDataTableSelectQuery();

                string OldStr = ReadOnly.FieldInQueryIdentifyCharL + Field + ReadOnly.FieldInQueryIdentifyCharR;
                string NewStr = "'" + GetValueFromFieldListDT(Field, false) + "'";

                DTSelectQuery = DTSelectQuery.Replace(OldStr, NewStr);
            }

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
        private void InsertValuesToProcessedDataTable()
        {
            foreach (DataRow DR in ProcessedFL.Rows)
            {
                string FieldName = Convert.ToString(DR[ReadOnly.SnippetFieldName]);
                bool isAuto = GetBooleanFromDataRow(DR, ReadOnly.SnippetFieldAutoFill);
                bool isInternal = GetBooleanFromDataRow(DR, ReadOnly.SnippetFieldForInternalUse);
                DataRow DRT = ProcessedFL.Select("[" + ReadOnly.SnippetFieldName + "]='" + FieldName + "'")[0];

                string QueryResult = isAuto ? GetDTQueryResult(FieldName, false) : FSP.FieldCollection[FieldName].GetManualValue();

                DRT[ReadOnly.SnippetFieldManualValue] = QueryResult;
            }
        }

        private void ReplaceFields(ref StringBuilder SB, string BracketL, string BracketR)
        {
            foreach (DataRow DR in ProcessedFL.Rows)
            {
                bool isInternal = GetBooleanFromDataRow(DR, ReadOnly.SnippetFieldForInternalUse);
                string OldStr = BracketL + DR[ReadOnly.SnippetFieldName] + BracketR;
                string NewStr = DR[ReadOnly.SnippetFieldManualValue].ToString();
                SB.Replace(OldStr, isInternal ? string.Empty : NewStr);
            }
        }

        private void WriteSnippet(StringBuilder SB, string SnippetExportPath)
        {
            using (StreamWriter SW = new StreamWriter(SnippetExportPath))
            {
                SW.Write(SB.ToString());
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

        private void CheckToOpenExportedSnippet(string SnippetExportPath)
        {
            if (MessageBox.Show("Snippet exporting completed.\n\nExported Path: " + SnippetExportPath + "\nOpen exported snippet?", "Snippet exporting completed", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Process.Start(SnippetExportPath);
            }
        }
        #endregion

        private bool GetBooleanFromDataRow(DataRow DR, string ColumnName)
        {
            try
            {
                return Convert.ToBoolean(DR[ColumnName] == DBNull.Value ? false : DR[ColumnName]);
            }
            catch (ArgumentException)
            {
                throw new DataColumnNotExistException();
            }
        }

        private string GetValueFromFieldListDT(string FieldName, bool isOriginal)
        {
            DataRow[] Value = (isOriginal ? FieldList : ProcessedFL).Select("[" + ReadOnly.SnippetFieldName + "]='" + FieldName + "'");
            return Value.Length > 0 ? Value[0][ReadOnly.SnippetFieldManualValue].ToString() : string.Empty;
        }
        #endregion
    }
}
