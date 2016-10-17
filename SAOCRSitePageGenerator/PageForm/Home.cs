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
    public partial class Home : Form
    {
        #region Initialize
        public Home()
        {
            InitializeComponent();
            InitializeEventHandler();
            Initialize();
        }

        private void Initialize()
        {
            RefreshSnippetList();
            CheckSourceDirectory();
            RefreshDSList();
        }
        
        private void InitializeEventHandler()
        {
            SNPList.ItemSelectionChanged += SNPList_ItemSelectionChanged;
            SNPList.KeyDown += SNPList_KeyDown;
            SNPList.MouseDoubleClick += SNPOpen_Click;
            SNPRefresh.Click += SNPRefresh_Click;
            SNPOpen.Click += SNPOpen_Click;
            SNPDelete.Click += SNPDelete_Click;
            SNPEdit.Click += SNPEdit_Click;
            SNPNew.Click += SNPNew_Click;

            DSList.KeyDown += DSList_KeyDown;
            DSList.ItemSelectionChanged += DSList_ItemSelectionChanged;
            DSList.MouseDoubleClick += DSList_MouseDoubleClick;
            DSListRefresh.Click += DSListRefresh_Click;
            DSNew.Click += DSNew_Click;
            DSEdit.Click += DSEdit_Click;
            DSRemove.Click += DSRemove_Click;
            DSLoad.Click += DSLoad_Click;
            DSUnload.Click += DSUnload_Click;
        }
        #endregion

        #region Events

        #region Snippet Processing
        private void SNPList_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            SetSnippetControlButtonsEnabled(SNPList.SelectedItems.Count > 0);
        }

        private void SNPOpen_Click(object sender, EventArgs e)
        {
            string SNPName = SNPList.SelectedItems[0].SubItems[ReadOnly.SnippetName].Text;
            string SNPKey = SNPList.SelectedItems[0].SubItems[ReadOnly.SnippetGUID].Text;

            KeyValuePair<string, string> SnippetInfo = new KeyValuePair<string, string>(SNPKey, SNPName);

            if (ExecuteSnippet(SnippetInfo) == DialogResult.OK)
            {
                using (ConfigManager CMS = new ConfigManager(ReadOnly.SnippetsPath + "/" + SnippetInfo.Key + "/" + ReadOnly.SnippetStructConfig))
                {
                    CMS.SetConfig(ReadOnly.SnippetLastUsed, DateTime.Now.ToString("yyyy/MM/dd tt HH:mm"));
                    CMS.SaveAsync();
                }
            }
            RefreshSnippetList();

            SetSnippetControlButtonsEnabled(SNPList.SelectedItems.Count > 0);
        }

        private void SNPEdit_Click(object sender, EventArgs e)
        {
            string SNPPath = ReadOnly.SnippetsPath + "/" + SNPList.SelectedItems[0].SubItems[ReadOnly.SnippetGUID].Text;
            
            if (EditSnippet(SNPPath) == DialogResult.OK)
            {
                RefreshSnippetList();
            }

            SetSnippetControlButtonsEnabled(SNPList.SelectedItems.Count > 0);
        }

        private void SNPDelete_Click(object sender, EventArgs e)
        {
            string SNPKey = SNPList.SelectedItems[0].SubItems[ReadOnly.SnippetGUID].Text;

            if (MessageBox.Show("Are you sure want to delete the snippet?\n\nSnippet Name: " + SNPKey, "Snippet deleting confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                DeleteSnippet(SNPKey);
            }
            RefreshSnippetList();

            SetSnippetControlButtonsEnabled(SNPList.SelectedItems.Count > 0);
        }

        private void SNPNew_Click(object sender, EventArgs e)
        {
            if (CreateNewSnippet() == DialogResult.OK)
            {
                RefreshSnippetList();
            }

            SetSnippetControlButtonsEnabled(SNPList.SelectedItems.Count > 0);
        }

        private void SNPRefresh_Click(object sender, EventArgs e)
        {
            RefreshSnippetList();
        }

        private void SNPList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.F5)
            {
                RefreshSnippetList();
            }
        }
        #endregion

        #region External Source Processing
        private void DSNew_Click(object sender, EventArgs e)
        {
            if (CreateNewDataSet() == DialogResult.OK)
            {
                RefreshDSList();
            }
        }

        private void DSEdit_Click(object sender, EventArgs e)
        {
            if (EditDataSet(new Guid(DSList.SelectedItems[0].SubItems[ReadOnly.SourceDataSetGUID].Text)) == DialogResult.OK)
            {
                RefreshDSList();
            }
        }

        private void DSRemove_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure want to remove the selected DataSet?", "DataSet Deletion Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                foreach (ListViewItem DSVIewItem in DSList.SelectedItems)
                {
                    Guid DSGuid = new Guid(DSVIewItem.SubItems[ReadOnly.SourceDataSetGUID].Text);
                    string DSName = DSVIewItem.SubItems[ReadOnly.SourceDataSetName].Text;

                    RemoveDS(DSGuid);
                }

                RefreshDSList();
            }
        }

        private void DSListRefresh_Click(object sender, EventArgs e)
        {
            RefreshDSList();
        }

        private void DSList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.F5)
            {
                RefreshDSList();
            }
        }

        private void DSList_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            SetDataSetControlButtonEnabled(DSList.SelectedItems.Count > 0);
        }

        private void DSLoad_Click(object sender, EventArgs e)
        {
            LoadDataSet();
        }

        private void DSList_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LoadDataSet();
        }

        private void DSUnload_Click(object sender, EventArgs e)
        {
            unLoadDataSet();
        }
        #endregion

        #endregion

        #region Methods

        #region Snippet Processing
        private void SetSnippetControlButtonsEnabled(bool Enabled)
        {
            SNPEdit.Enabled = SNPDelete.Enabled = SNPOpen.Enabled = Enabled;
        }

        private void RefreshSnippetList()
        {
            SNPList.Items.Clear();

            if (!Directory.Exists(ReadOnly.SnippetsPath))
            {
                Directory.CreateDirectory(ReadOnly.SnippetsPath);
            }

            using (ConfigManager CM = new ConfigManager(ReadOnly.SnippetsListPath))
            {
                foreach (KeyValuePair<string, string> SnippetInfo in CM.GetConfigAll())
                {
                    string SnippetFolderPath = ReadOnly.SnippetsPath + "/" + SnippetInfo.Key;
                    bool isSingle = true, isRepeatedSnippet = true;

                    isSingle = ScanIsSingleSnippet(SnippetFolderPath);
                    isRepeatedSnippet = ScanIsRepeatedSnippet(SnippetFolderPath);

                    if (isSingle || isRepeatedSnippet)
                    {
                        using (ConfigManager CMS = new ConfigManager(SnippetFolderPath + "/" + ReadOnly.SnippetStructConfig))
                        {
                            ListViewItem LVI = CreateListViewItemFitToListView(SNPList);
                            LVI.SubItems.RemoveByKey("Empty");

                            foreach (string Keys in ReadOnly.SnippetsListKeys)
                            {
                                LVI.SubItems[Keys].Text = CMS.GetConfig(Keys);
                            }

                            SNPList.Items.Add(LVI);
                        }
                    }
                    else
                    {
                        if (MessageBox.Show("Snippet folder file struct illegal. Delete folder?\n\nFolder Path: " + SnippetFolderPath + "\nFolder Name: " + SnippetInfo.Value, "Illegal Snippet Folder", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            Directory.Delete(SnippetFolderPath, true);
                            CM.RemoveConfig(SnippetFolderPath);
                        }
                    }
                }
            }
        }

        private bool ScanIsSingleSnippet(string folder)
        {
            bool isLegalSnippet = true;
            foreach (string FileName in ReadOnly.SnippetStructSingle)
            {
                if (!File.Exists(folder + "/" + FileName))
                {
                    isLegalSnippet = false;
                    break;
                }
            }
            return isLegalSnippet;
        }

        private bool ScanIsRepeatedSnippet(string Folder)
        {
            bool isLegalSnippet = true;
            foreach (string FileName in ReadOnly.SnippetStructMulti)
            {
                if (FileName == ReadOnly.SnippetStructSnippetLoop)
                {
                    isLegalSnippet = Directory.GetFiles(Folder, "*" + ReadOnly.SnippetStructSnippetLoop + "*").Length > 0;
                }
                else
                {
                    if (!File.Exists(Folder + "/" + FileName))
                    {
                        isLegalSnippet = false;
                        break;
                    }
                }
            }
            return isLegalSnippet;
        }

        private void CheckSourceDirectory()
        {
            if (!Directory.Exists(ReadOnly.SourcePath))
                Directory.CreateDirectory(ReadOnly.SourcePath);
        }

        private void DeleteSnippet(string SnippetKey)
        {
            using (ConfigManager CM = new ConfigManager(ReadOnly.SnippetsListPath))
            {
                if (!string.IsNullOrEmpty(SnippetKey))
                {
                    Directory.Delete(ReadOnly.SnippetsPath + "/" + SnippetKey, true);
                    CM.RemoveConfig(SnippetKey);
                }
            }
        }

        private DialogResult ExecuteSnippet(KeyValuePair<string, string> SnippetInfo)
        {
            try
            {
                using (LoadingBlock LB = new LoadingBlock())
                {
                    LB.StartPosition = FormStartPosition.CenterScreen;

                    LB.Show(this);
                    Application.DoEvents();

                    SnippetExecutor SG = new SnippetExecutor(SnippetInfo);

                    LB.Close();
                    Application.DoEvents();

                    return SG.ShowDialog(this);
                }
            }
            catch (DataColumnNotExistException)
            {
                MessageBox.Show("Field List DataColumn is out of date. Please open snippet editor, rescan fields, and try again.");
                return DialogResult.Abort;
            }
        }

        private DialogResult CreateNewSnippet()
        {
            SnippetEditor NSI = new SnippetEditor();
            return NSI.ShowDialog();
        }

        private DialogResult EditSnippet(string SnippetPath)
        {
            SnippetEditor NSI = new SnippetEditor(SnippetPath);
            return NSI.ShowDialog();
        }
        #endregion

        #region External Source Processing
        private void SetDataSetControlButtonEnabled(bool Enabled)
        {
            DSEdit.Enabled = Enabled;
            DSLoad.Enabled = Enabled;
            DSRemove.Enabled = Enabled;
            DSUnload.Enabled = Enabled;
        }

        private void RefreshDSList()
        {
            DSList.Items.Clear();
            using (ConfigManager CM = new ConfigManager(ReadOnly.SourceDataSetDict))
            {
                foreach (KeyValuePair<string, string> Pair in CM.GetConfigAll())
                {
                    using (DataSet DS = ReadDataSetXML(ReadOnly.SourcePath + "/" + Pair.Key))
                    {
                        ListViewItem LVI = CreateListViewItemFitToListView(DSList);
                        LVI.SubItems.RemoveByKey("Empty");
                        LVI.SubItems[ReadOnly.SourceDataTableList].Text = GetDTListString(DS);
                        LVI.SubItems[ReadOnly.SourceDataSetName].Text = string.IsNullOrEmpty(Pair.Value) ? "(No Name)" : Pair.Value;
                        LVI.SubItems[ReadOnly.SourceDataSetLoaded].Text = Static.SnippetExternalSourceDataSet.ContainsKey(new Guid(Pair.Key)).ToString();
                        LVI.SubItems[ReadOnly.SourceDataSetGUID].Text = Pair.Key;

                        DSList.Items.Add(LVI);
                    }
                }
            }

            SetDataSetControlButtonEnabled(DSList.SelectedItems.Count > 0);
        }

        private string GetDTListString(DataSet DS)
        {
            string ListString = "";

            foreach (DataRow DR in DS.Tables[ReadOnly.SourceDataTableInfoTableName].Rows)
            {
                ListString += DR[ReadOnly.SourceDataTableName] + ", ";
            }

            ListString = ListString.Length > 2 ? ListString.Substring(0, ListString.Length - 2) : string.Empty;
            return ListString;
        }

        private DataSet ReadDataSetXML(string Path)
        {
            DataSet DS = new DataSet();
            DS.ReadXml(Path);
            return DS;
        }

        private void RemoveDS(Guid DSGuid)
        {
            if (Static.SnippetExternalSourceName.ContainsKey(DSGuid))
            {
                Static.SnippetExternalSourceDataSet.Remove(DSGuid);
                Static.SnippetExternalSourceName.Remove(DSGuid);
            }
            using (ConfigManager CM = new ConfigManager(ReadOnly.SourceDataSetDict))
            {
                CM.RemoveConfig(DSGuid.ToString());
                CM.Save();
            }
            File.Delete(ReadOnly.SourcePath + "/" + DSGuid.ToString());
            File.Delete(ReadOnly.SourcePath + "/" + DSGuid.ToString() + ReadOnly.SourceMemoExtension);
        }

        private DialogResult CreateNewDataSet()
        {
            DataSetEditor DTE = new DataSetEditor();
            return DTE.ShowDialog();
        }

        private DialogResult EditDataSet(Guid DSGuid)
        {
            DataSetEditor DTE = new DataSetEditor(DSGuid);
            return DTE.ShowDialog();
        }

        private void LoadDataSet()
        {
            foreach (ListViewItem LVI in DSList.SelectedItems)
            {
                Guid DSGuid = new Guid(LVI.SubItems[ReadOnly.SourceDataSetGUID].Text);
                DataSet DSToAdd = ReadDataSetXML(ReadOnly.SourcePath + "/" + LVI.SubItems[ReadOnly.SourceDataSetGUID].Text);
                string DSName = LVI.SubItems[ReadOnly.SourceDataSetName].Text;
                if (!Static.SnippetExternalSourceName.ContainsKey(DSGuid))
                {
                    Static.SnippetExternalSourceDataSet.Add(DSGuid, DSToAdd);
                    Static.SnippetExternalSourceName.Add(DSGuid, DSName);
                }
            }
            RefreshDSList();
        }

        private void unLoadDataSet()
        {
            foreach (ListViewItem LVI in DSList.SelectedItems)
            {
                Guid DSGuid = new Guid(LVI.SubItems[ReadOnly.SourceDataSetGUID].Text);
                if (Static.SnippetExternalSourceName.ContainsKey(DSGuid))
                {
                    Static.SnippetExternalSourceDataSet.Remove(DSGuid);
                    Static.SnippetExternalSourceName.Remove(DSGuid);
                }
            }
            RefreshDSList();
        }

        private DataSet GetDataSetByGuid(Dictionary<Guid, DataSet> Dict, Guid Guid)
        {
            DataSet DSRet = null;
            Dict.TryGetValue(Guid, out DSRet);
            return DSRet;
        }
        #endregion

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
