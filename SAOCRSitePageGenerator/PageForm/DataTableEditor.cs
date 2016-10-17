using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SAOCRSitePageGenerator
{
    public partial class DataSetEditor : Form
    {
        bool isEdit;

        DataColumn DCinDCManager;
        DataSet DS;
        Guid DSGuid, CurrentDTGuid;

        BackgroundWorker CsvImporterWorker = new BackgroundWorker();

        #region Initialize
        public DataSetEditor()
        {
            InitializeNewDS();
            CommonInitialize();
        }

        public DataSetEditor(Guid DSGuid)
        {
            InitializeEditDS(DSGuid);
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
            Save.Click += Save_Click;
            DCAdd.Click += DCAdd_Click;
            DCRemove.Click += DCRemove_Click;
            DCLoad.Click += DCLoad_Click;

            foreach (DataTable DT in DS.Tables)
            {
                DT.ColumnChanged += DT_ColumnChanged;
                DT.RowChanged += DT_RowChanged;
            }
            DTNew.Click += DTNew_Click;
            DTRemove.Click += DTRemove_Click;
            DTLoad.Click += DTLoad_Click;
            DTList.KeyDown += DTList_KeyDown;
            DTList.ItemSelectionChanged += DTList_ItemSelectionChanged;
            DTList.MouseDoubleClick += DTList_MouseDoubleClick;
            DTNameToAdd.KeyDown += DTNameToAdd_KeyDown;
            DTRemarkToAdd.KeyDown += DTRemarkToAdd_KeyDown;

            DSMemo.KeyDown += DSMemo_KeyDown;

            CsvImport.Click += CsvImport_Click;
            CsvPathBrowse.Click += CsvPathBrowse_Click;
            CsvImporterWorker.DoWork += CsvImporterWorker_DoWork;
            CsvImporterWorker.ProgressChanged += CsvImporterWorker_ProgressChanged;
            CsvImporterWorker.RunWorkerCompleted += CsvImporterWorker_RunWorkerCompleted;
        }

        private void Initialize()
        {
            UpdateDataTableView();
            RefreshDataTableList();

            if (isEdit)
            {
                SetBasicInfoToDSBasicInfoArea(DSGuid);
                LoadDataSetDocument(DSGuid);
            }
            
            CsvImporterWorker.WorkerReportsProgress = true;
            CsvImporterWorker.WorkerSupportsCancellation = true;
        }

        private void InitializeNewDS()
        {
            isEdit = false;

            DS = new DataSet();
            DS.Tables.Add(NewDataTableInfoTable());
            
            DSGuid = Guid.NewGuid();
        }

        private void InitializeEditDS(Guid DSGuid)
        {
            isEdit = true;

            DS = ReadDataSetXML(ReadOnly.SourcePath + "/" + DSGuid.ToString());

            this.DSGuid = DSGuid;
        }
        #endregion

        #region Events
        private void Save_Click(object sender, EventArgs e)
        {
            using (LoadingBlock LB = new LoadingBlock((isEdit ? "Updating" : "Saving") + " DataSet..."))
            {
                LB.StartPosition = FormStartPosition.CenterScreen;

                LB.Show(this);
                Application.DoEvents();

                WriteDataSetBasicInfo();
                WriteDataDataSetXML();
                WriteDSMemo();

                LB.Close();
                Application.DoEvents();
            }

            using (ConfigManager CM = new ConfigManager(ReadOnly.SourceDataSetDict))
            {
                MessageBox.Show("DataSet " + (isEdit ? "modifying" : "creating") + "completed.\n\nDataSet Name: " + CM.GetConfig(DSGuid.ToString()) + "\nDataSet GUID: " + DSGuid.ToString());
            }
            DialogResult = DialogResult.OK;
        }

        private void DCRemove_Click(object sender, EventArgs e)
        {
            if (DCinDCManager != null)
            {
                RemoveDataColumnLoadedInDCManager(DCinDCManager);
                SetEnableByLoadedStatusForDCManager(false);
            }
        }

        private void DCAdd_Click(object sender, EventArgs e)
        {
            DataColumn DC = WrtieDCManagerToDataColumn();
            if (DC != null && !DS.Tables[CurrentDTGuid.ToString()].Columns.Contains(DC.ColumnName)) { DS.Tables[CurrentDTGuid.ToString()].Columns.Add(DC); } 
        }

        private void DCLoad_Click(object sender, EventArgs e)
        {
            if (DCinDCManager == null)
            {
                int ColumnIndexInDTView = -1;

                try
                {
                    ColumnIndexInDTView = DTView.SelectedCells[0].ColumnIndex;
                }
                catch (Exception) { }

                if (ColumnIndexInDTView >= 0)
                {
                    DataColumn DC = DS.Tables[CurrentDTGuid.ToString()].Columns[ColumnIndexInDTView];
                    LoadDataColumnToDCManager(DC);
                }
                SetEnableByLoadedStatusForDCManager(ColumnIndexInDTView >= 0);
            } else
            {
                UnloadDataColumnFromDCManager();
                SetEnableByLoadedStatusForDCManager(false);
            }
        }

        private void DSMemo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.A && e.Modifiers == Keys.Control)
            {
                DSMemo.SelectAll();
            }
        }

        #region DT Manager Control
        private void DT_RowChanged(object sender, DataRowChangeEventArgs e)
        {
            UpdateDataTableView();
        }

        private void DT_ColumnChanged(object sender, DataColumnChangeEventArgs e)
        {
            UpdateDataTableView();
        }

        private void DTLoad_Click(object sender, EventArgs e)
        {
            DTNameToAdd.Text = DTList.SelectedItems[0].SubItems[ReadOnly.SourceDataTableName].Text;
            DTRemarkToAdd.Text = DTList.SelectedItems[0].SubItems[ReadOnly.SourceDataTableRemark].Text;
            CurrentDTGuid = new Guid(DTList.SelectedItems[0].SubItems[ReadOnly.SourceDataTableGUID].Text);
            UpdateDataTableView();
        }

        private void DTRemove_Click(object sender, EventArgs e)
        {
            RemoveDT();
        }

        private void DTNew_Click(object sender, EventArgs e)
        {
            CreateNewDT();
        }

        private void DTList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.F5)
            {
                RefreshDataTableList();
            }
        }

        private void DTList_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            SetEnabledForDTManager(DTList.SelectedItems.Count > 0);
        }

        private void DTList_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (DTList.SelectedItems.Count > 0)
            {
                DTLoad_Click(sender, e);
            }
        }

        private void DTRemarkToAdd_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                UpdateDataTableInfo();
                RefreshDataTableList();
            }
        }

        private void DTNameToAdd_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                UpdateDataTableInfo();
                RefreshDataTableList();
            }
        }
        #endregion

        #region CSV Importer
        private void CsvPathBrowse_Click(object sender, EventArgs e)
        {
            CsvPath.Text = BrowseAndGetFilePath();
        }

        private void CsvImport_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(CsvDelim.Text))
            {
                MessageBox.Show("Please insert a delimeter, then try again.");
            } else if (string.IsNullOrEmpty(CsvPath.Text)) {
                MessageBox.Show("Please choose csv file path to import.");
                CsvPathBrowse_Click(sender, e);
            } else
            {
                SetCSVImporterButtonEnabled(false);
                SetDTBasicInfoSyncFromCSVImporter();
                CsvImporterWorker.RunWorkerAsync();
            }
        }

        private void CsvImporterWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            SetCSVImporterButtonEnabled(true);
            CreateNewDT((DataTable)e.Result);
        }

        private void CsvImporterWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            SetCsvImportProgressText(e.UserState.ToString());
        }

        private void CsvImporterWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            e.Result = ImportCSV();
        }
        #endregion

        #endregion

        #region Methods

        #region Control Modifying
        private void SetEnableByLoadedStatusForDCManager(bool isLoaded)
        {
            DCAdd.Enabled = !isLoaded;
            DCRemove.Enabled = isLoaded;
            DCName.ReadOnly = isLoaded;
            DCExpr.ReadOnly = isLoaded;
            DCType.Enabled = !isLoaded;
            DCAllowNull.Enabled = DCUnique.Enabled = DCPrimaryKey.Enabled = !isLoaded;
            DCLoad.Text = isLoaded ? "Unload" : "Load";
        }

        private void SetEnabledForDTManager(bool Enabled)
        {
            DTRemove.Enabled = Enabled;
            DTLoad.Enabled = Enabled;
        }

        private void SetBasicInfoToDSBasicInfoArea(Guid DSGuid)
        {
            using (ConfigManager CM = new ConfigManager(ReadOnly.SourceDataSetDict))
            {
                DSName.Text = CM.GetConfig(DSGuid.ToString());
            }
        }

        private void SetDTBasicInfoSyncFromCSVImporter()
        {
            DTNameToAdd.Text = CsvDTName.Text;
            DTRemarkToAdd.Text = CsvDTRemark.Text;
        }

        private void SetCSVImporterButtonEnabled(bool Enabled)
        {
            CsvImport.Enabled = Enabled;
            CsvPathBrowse.Enabled = Enabled;
        }

        private void SetCsvImportProgressText(string Text)
        {
            CsvImportProgress.Text = Text;
        }
             
        private void LoadDataSetDocument(Guid DSGuid)
        {
            using (StreamReader SR = new StreamReader(ReadOnly.SourcePath + "/" + DSGuid.ToString() + ReadOnly.SourceMemoExtension))
            {
                DSMemo.Text = SR.ReadToEnd();
            }
        }
        #endregion

        #region DataColumn Manager
        private void LoadDataColumnToDCManager(DataColumn DC)
        {
            DCinDCManager = DC;
            DCName.Text = DC.ColumnName;
            DCType.Text = DC.DataType.ToString();
            DCUnique.Checked = DC.Unique;
            DCAllowNull.Checked = DC.AllowDBNull;
            DCPrimaryKey.Checked = DS.Tables[CurrentDTGuid.ToString()].PrimaryKey.Contains(DC);
        }

        private void UnloadDataColumnFromDCManager()
        {
            DCinDCManager = null;
            DCName.Text = "";
            DCType.Text = "System.String";
            DCUnique.Checked = false;
            DCAllowNull.Checked = false;
            DCPrimaryKey.Checked = false;
        }

        private void RemoveDataColumnLoadedInDCManager(DataColumn DC)
        {
            if (DS.Tables[CurrentDTGuid.ToString()].Columns.Contains(DC.ColumnName))
            {
                try
                {
                    DS.Tables[CurrentDTGuid.ToString()].Columns.Remove(DC);
                }
                catch (ArgumentException)
                {
                    if (MessageBox.Show("The data column you are trying to remove is a primary key of the data table you loaded. Are you sure want to delete this data column?", "DataColumn Deletion Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        List<DataColumn> DCL = DS.Tables[CurrentDTGuid.ToString()].PrimaryKey.ToList();
                        DCL.Remove(DC);
                        DS.Tables[CurrentDTGuid.ToString()].PrimaryKey = DCL.ToArray();
                        DS.Tables[CurrentDTGuid.ToString()].Columns.Remove(DC);
                    }
                }
            }
            UnloadDataColumnFromDCManager();
        }

        private DataColumn WrtieDCManagerToDataColumn()
        {
            DataColumn DC = new DataColumn();
            DC.ColumnName = DCName.Text;
            DC.DataType = Type.GetType(DCType.Text);
            DC.Expression = DCExpr.Text;
            DC.Unique = DCUnique.Checked;
            DC.AllowDBNull = DCAllowNull.Checked;

            try
            {
                DS.Tables[CurrentDTGuid.ToString()].Columns.Add(DC);
            }
            catch (DuplicateNameException)
            {
                return null;
            }

            if (DCPrimaryKey.Checked)
            {
                List<DataColumn> PKeyList = DS.Tables[CurrentDTGuid.ToString()].PrimaryKey.ToList();
                PKeyList.Add(DC);
                DS.Tables[CurrentDTGuid.ToString()].PrimaryKey = PKeyList.ToArray();
            }
            return DC;
        }
        #endregion

        #region Write DataSet
        private void WriteDataSetBasicInfo()
        {
            DS.DataSetName = DSGuid.ToString();

            using (ConfigManager CM = new ConfigManager(ReadOnly.SourceDataSetDict))
            {
                CM.SetConfig(DSGuid.ToString(), DSName.Text);
                CM.SaveAsync();
            }
        }

        private void WriteDataDataSetXML()
        {
            DS.WriteXml(ReadOnly.SourcePath + "/" + DSGuid.ToString(), XmlWriteMode.WriteSchema);
        }

        private void WriteDSMemo()
        {
            string DSMemoPath = ReadOnly.SourcePath + "/" + DSGuid.ToString() + ReadOnly.SourceMemoExtension;
            if (!File.Exists(DSMemoPath))
            {
                using (StreamWriter SW = File.CreateText(DSMemoPath)) { }
            }
            using (StreamWriter SW = File.CreateText(DSMemoPath))
            {
                SW.Write(DSMemo.Text);
            }
        }
        #endregion

        #region DataTable Manager
        private void CreateNewDT()
        {
            Guid DTGuid = Guid.NewGuid();

            DataTable DT = new DataTable(DTGuid.ToString());
            RegisterDTToDataSet(DTGuid);
            DS.Tables.Add(DT);

            RefreshDataTableList();
        }

        private void CreateNewDT(DataTable DT)
        {
            Guid DTGuid = Guid.NewGuid();

            DT.TableName = DTGuid.ToString();
            RegisterDTToDataSet(DTGuid);
            DS.Tables.Add(DT);

            RefreshDataTableList();
        }

        private void RemoveDT()
        {
            Guid DTGuid = new Guid(DTList.SelectedItems[0].SubItems[ReadOnly.SourceDataTableGUID].Text);

            if (MessageBox.Show("Are you sure want to delete selected DataTable?\n\nDataTable Name: " + DTList.SelectedItems[0].SubItems[ReadOnly.SourceDataTableName].Text + "\n\nDataTable GUID: " + DTGuid.ToString(), "DataSet Deletion Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                RemoveDTFromDataSet(DTGuid);
                RefreshDataTableList();
            }

            SetEnabledForDTManager(DTList.SelectedItems.Count > 0);
        }

        private DataTable NewDataTableInfoTable()
        {
            DataTable DT = new DataTable(ReadOnly.SourceDataTableInfoTableName);

            foreach (KeyValuePair<string, Type> InfoTColInfo in ReadOnly.SourceDataTableInfoTableDict)
            {
                DataColumn DC = new DataColumn(InfoTColInfo.Key, InfoTColInfo.Value);
                DT.Columns.Add(DC);
            }

            return DT;
        }

        private void RegisterDTToDataSet(Guid TableID)
        {
            DataRow DR = DS.Tables[ReadOnly.SourceDataTableInfoTableName].NewRow();
            DR[ReadOnly.SourceDataTableName] = string.IsNullOrEmpty(DTNameToAdd.Text) ? "(No Name)" : DTNameToAdd.Text;
            DR[ReadOnly.SourceDataTableRemark] = DTRemarkToAdd.Text;
            DR[ReadOnly.SourceDataTableGUID] = TableID.ToString();
            DS.Tables[ReadOnly.SourceDataTableInfoTableName].Rows.Add(DR);
        }

        private void RemoveDTFromDataSet(Guid TableID)
        {
            DataRow[] DataRowInInfoTableToRemove = DS.Tables[ReadOnly.SourceDataTableInfoTableName].Select("[" + ReadOnly.SourceDataTableGUID + "] = '" + TableID.ToString() + "'");
            foreach (DataRow DR in DataRowInInfoTableToRemove)
            {
                DS.Tables[ReadOnly.SourceDataTableInfoTableName].Rows.Remove(DR);
            }

            DS.Tables.Remove(TableID.ToString());

            CurrentDTGuid = Guid.Empty;
            DTView.DataSource = null;
            UpdateDataTableView();
        }

        private void UpdateDataTableView()
        {
            bool CurrentDTIsNull = CurrentDTGuid == Guid.Empty || CurrentDTGuid == null;
            DTView.DataSource = CurrentDTIsNull ? null : DS.Tables[CurrentDTGuid.ToString()];

            DataRow[] SearchTableInfoByGuid = DS.Tables[ReadOnly.SourceDataTableInfoTableName].Select("[" + ReadOnly.SourceDataTableGUID + "] = '" + CurrentDTGuid.ToString() + "'");
            DTCurrent.Text = SearchTableInfoByGuid.Length > 0 ? SearchTableInfoByGuid[0][ReadOnly.SourceDataTableName].ToString() : "";

            DCManager.Enabled = !CurrentDTIsNull;
        }

        private void UpdateDataTableInfo()
        {
            using (ConfigManager CM = new ConfigManager(ReadOnly.SourceDataSetDict))
            {
                CM.SetConfig(CurrentDTGuid.ToString(), DTNameToAdd.Text);

                DataRow[] DataRowInInfoTableToRemove = DS.Tables[ReadOnly.SourceDataTableInfoTableName].Select("[" + ReadOnly.SourceDataTableGUID + "] = '" + CurrentDTGuid.ToString() + "'");
                foreach (DataRow DR in DataRowInInfoTableToRemove)
                {
                    DR[ReadOnly.SourceDataTableName] = DTNameToAdd.Text;
                    DR[ReadOnly.SourceDataTableRemark] = DTRemarkToAdd.Text;
                }
            }
        }

        private void RefreshDataTableList()
        {
            DTList.Items.Clear();

            foreach (DataRow DR in DS.Tables[ReadOnly.SourceDataTableInfoTableName].Rows)
            {
                ListViewItem LVI = CreateListViewItemFitToListView(DTList);
                LVI.SubItems.RemoveByKey("Empty");
                foreach (string Key in ReadOnly.SourceDataTableInfoKeys)
                {
                    LVI.SubItems[Key].Text = DR[Key].ToString();
                }
                DataTable DT = DS.Tables[DR[ReadOnly.SourceDataTableGUID].ToString()];
                LVI.SubItems[ReadOnly.SourceDataTableColumnCount].Text = DT == null ? "DT is Null" : DT.Columns.Count.ToString();
                LVI.SubItems[ReadOnly.SourceDataTableRowCount].Text = DT == null ? "DT is Null" : DT.Rows.Count.ToString();
                LVI.Text = DR[ReadOnly.SourceDataTableGUID].ToString();
                DT.Dispose();
                DTList.Items.Add(LVI);
            }
        }
        #endregion

        #region CSV Importer
        private string BrowseAndGetFilePath()
        {
            if (CSVSelector.ShowDialog() == DialogResult.OK)
            {
                return CSVSelector.FileName;
            } else
            {
                return string.Empty;
            }
        }

        private DataTable ImportCSV()
        {
            using (TextFieldParser TFP = new TextFieldParser(CsvPath.Text))
            {
                using (DataTable DT = new DataTable())
                {
                    CsvImporterWorker.ReportProgress(0, "Importing...");
                    TFP.Delimiters = new string[] { CsvDelim.Text };

                    while (!TFP.EndOfData)
                    {
                        try
                        {
                            string[] ReadData = TFP.ReadFields();

                            while (DT.Columns.Count < ReadData.Length)
                            {
                                DT.Columns.Add(CreateNewDataColumn());
                            }
                            
                            DT.Rows.Add(GetStringArrayImportedDataRow(DT, ReadData));
                        } catch (MalformedLineException) { }
                    }
                    CsvImporterWorker.ReportProgress(0, "Importing Completed.");
                    return DT;
                }
            }
        }

        private DataColumn CreateNewDataColumn()
        {
            DataColumn DC = new DataColumn(string.Empty, typeof(string));
            return DC;
        }

        private DataRow GetStringArrayImportedDataRow(DataTable DT, string[] Array)
        {
            DataRow DR = DT.NewRow();
            DR.ItemArray = Array;
            return DR;
        }
        #endregion

        private DataSet ReadDataSetXML(string Path)
        {
            DataSet DS = new DataSet();
            DS.ReadXml(Path);
            return DS;
        }


        private ListViewItem CreateListViewItemFitToListView(ListView LV)
        {
            ListViewItem LVI = new ListViewItem();
            foreach (ColumnHeader CH in LV.Columns)
            {
                ListViewItem.ListViewSubItem SUB = new ListViewItem.ListViewSubItem();
                SUB.Name = CH.Text;
                LVI.SubItems.Add(SUB);
            }
            return LVI;
        }
        #endregion
    }
}
