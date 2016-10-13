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
        }
        
        private void InitializeEventHandler()
        {
            SnippetList.ItemSelectionChanged += SnippetList_ItemSelectionChanged;
            SnippetList.KeyDown += SnippetList_KeyDown;
            SnippetList.MouseDoubleClick += Open_Click;
            DatatableList.KeyDown += DatatableList_KeyDown;
            RefreshList.Click += Refresh_Click;
            Open.Click += Open_Click;
            Delete.Click += Delete_Click;
            Edit.Click += Edit_Click;
            New.Click += New_Click;
        }
        #endregion

        #region Events
        private void SnippetList_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            SetSnippetControlButtonsEnabled(SnippetList.SelectedItems.Count > 0);
        }

        private void Open_Click(object sender, EventArgs e)
        {
            string SNPName = SnippetList.SelectedItems[0].SubItems[ReadOnly.SnippetName].Text;
            string SNPKey = SnippetList.SelectedItems[0].SubItems[ReadOnly.SnippetFolderKey].Text;

            KeyValuePair<string, string> SnippetInfo = new KeyValuePair<string, string>(SNPKey, SNPName);

            ExecuteSnippet(SnippetInfo);
        }

        private void Edit_Click(object sender, EventArgs e)
        {
            string SNPPath = ReadOnly.SnippetsPath + "/" + SnippetList.SelectedItems[0].SubItems[ReadOnly.SnippetFolderKey].Text;

            EditSnippet(SNPPath);
        }

        private void Delete_Click(object sender, EventArgs e)
        {
            string SNPKey = SnippetList.SelectedItems[0].SubItems[ReadOnly.SnippetFolderKey].Text;

            if (MessageBox.Show("Are you sure want to delete the snippet?\n\nSnippet Name: " + SNPKey, "Snippet deleting confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                DeleteSnippet(SNPKey);
            }
            RefreshSnippetList();
        }

        private void New_Click(object sender, EventArgs e)
        {
            if (CreateNewSnippet() == DialogResult.OK)
            {
                RefreshSnippetList();
            }
        }

        private void Refresh_Click(object sender, EventArgs e)
        {
            RefreshSnippetList();
        }

        private void SnippetList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.F5)
            {
                RefreshSnippetList();
            }
        }
        
        private void DatatableList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.F5)
            {
                RefreshSnippetList();
            }
        }
        #endregion

        #region Methods
        private void SetSnippetControlButtonsEnabled(bool Enabled)
        {
            Edit.Enabled = Delete.Enabled = Open.Enabled = Enabled;
        }

        private void RefreshSnippetList()
        {
            SnippetList.Items.Clear();

            if (!Directory.Exists(ReadOnly.SnippetsPath))
            {
                Directory.CreateDirectory(ReadOnly.SnippetsPath);
            }

            using (ConfigManager CM = new ConfigManager(ReadOnly.SnippetsPath + "/" + ReadOnly.SnippetsList))
            {
                foreach (KeyValuePair<string, string> Config in CM.GetConfigAll())
                {
                    string SnippetFolderPath = ReadOnly.SnippetsPath + "/" + Config.Key;
                    bool isSingle = true, isRepeatedSnippet = true;

                    foreach (string FileName in ReadOnly.SnippetStructSingle)
                    {
                        if (!File.Exists(SnippetFolderPath + "/" + FileName))
                        {
                            isSingle = false;
                            break;
                        }
                    }

                    foreach (string FileName in ReadOnly.SnippetStructMulti)
                    {
                        if (FileName == ReadOnly.SnippetStructSnippetLoop)
                        {
                            isRepeatedSnippet = Directory.GetFiles(SnippetFolderPath, "*" + ReadOnly.SnippetStructSnippetLoop + "*").Length > 0;
                        }
                        else
                        {
                            if (!File.Exists(SnippetFolderPath + "/" + FileName))
                            {
                                isRepeatedSnippet = false;
                                break;
                            }
                        }
                    }

                    if (isSingle || isRepeatedSnippet)
                    {
                        using (ConfigManager CMS = new ConfigManager(SnippetFolderPath + "/" + ReadOnly.SnippetStructConfig))
                        {
                            ListViewItem LVI = new ListViewItem();
                            foreach (ColumnHeader CH in SnippetList.Columns)
                            {
                                ListViewItem.ListViewSubItem SUB = new ListViewItem.ListViewSubItem();
                                SUB.Name = CH.Text;
                                LVI.SubItems.Add(SUB);
                            }
                            LVI.SubItems.RemoveByKey("Empty");
                            foreach (string Keys in ReadOnly.SnippetsListKeys)
                            {
                                LVI.SubItems[Keys].Text = CMS.GetConfig(Keys);
                            }
                            SnippetList.Items.Add(LVI);
                        }
                    }
                    else
                    {
                        if (MessageBox.Show("Snippet folder file struct illegal. Delete folder?\n\nFolder Path: " + SnippetFolderPath + "\nFolder Name: " + Config.Value, "Illegal Snippet Folder", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            Directory.Delete(SnippetFolderPath, true);
                            CM.RemoveConfig(SnippetFolderPath);
                        }
                    }
                }
            }
        }

        private void CheckSourceDirectory()
        {
            if (!Directory.Exists(ReadOnly.SourcePath))
                Directory.CreateDirectory(ReadOnly.SourcePath);

            foreach (string Path in ReadOnly.SourceDataFolders)
            {
                Directory.CreateDirectory(Path);
            }
        }

        private void DeleteSnippet(string SnippetKey)
        {
            using (ConfigManager CM = new ConfigManager(ReadOnly.SnippetsPath + "/" + ReadOnly.SnippetsList))
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
            SnippetGenerator SG = new SnippetGenerator(SnippetInfo);
            return SG.ShowDialog();
        }

        private DialogResult CreateNewSnippet()
        {
            NewSnippetInitializer NSI = new NewSnippetInitializer();
            return NSI.ShowDialog();
        }

        private DialogResult EditSnippet(string SnippetPath)
        {
            NewSnippetInitializer NSI = new NewSnippetInitializer(SnippetPath);
            return NSI.ShowDialog();
        }
        #endregion
    }
}
