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
    public partial class SnippetEditor : Form, IFolderBrowseDialog
    {
        DataTable FieldList;
        Guid SnippetKey;
        
        bool isEditMode;
        string[] SnippetCollection;
        int RepeatedSnippetCount = 1, CurrentPage = 0;

        #region Initialize
        public SnippetEditor()
        {
            CommonInitialize();
        }

        public SnippetEditor(string SnippetPath)
        {
            CommonInitialize();
            SetToEditMode(SnippetPath);
        }

        private void InitializeEventHandler()
        {
            Snippet.KeyDown += Snippet_KeyDown;
            Snippet.KeyUp += Snippet_KeyUp;
            FieldListView.DataBindingComplete += FieldListView_DataBindingComplete;
            DestinationBrowse.Click += DestinationBrowse_Click;
            ScanFields.Click += ScanFields_Click;
            Submit.Click += Create_Click;
            SingleSnippet.CheckedChanged += SingleSnippet_CheckedChanged;
            StructSet.Click += StructSet_Click;
            NextSnippet.Click += SnippetNextPrev_Click;
            PrevSnippet.Click += SnippetNextPrev_Click;

            FormClosed += NewSnippetInitializer_FormClosed;
        }

        private void Initialize()
        {
            SnippetKey = Guid.NewGuid();
        }

        private void CommonInitialize()
        {
            InitializeComponent();
            InitializeEventHandler();
            Initialize();
        }
        #endregion

        #region Events
        private void Snippet_KeyUp(object sender, KeyEventArgs e)
        {
            SetCreateButtonEnabled(false);
            SnippetCollection[CurrentPage] = Snippet.Text;
        }

        private void Snippet_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.A && e.Modifiers == Keys.Control)
            {
                Snippet.SelectAll();
            }
        }

        private void SingleSnippet_CheckedChanged(object sender, EventArgs e)
        {
            SetMultiSnippetsEnabled(!SingleSnippet.Checked);
            SnippetCount.Value = SingleSnippet.Checked ? 1 : SnippetCount.Value;
        }

        private void StructSet_Click(object sender, EventArgs e)
        {
            SetSnippetType(!SingleSnippet.Checked);
            SnippetCollection = new string[SingleSnippet.Checked ? 1 : RepeatedSnippetCount + 2];
            SetPageDisplay(CurrentPage);
            NormalPanel.Enabled = true;
        }

        private void SnippetNextPrev_Click(object sender, EventArgs e)
        {
            Button BTN = (Button)sender;
            
            if (BTN == NextSnippet)
            {
                CurrentPage++;
            } else if (BTN == PrevSnippet)
            {
                CurrentPage--;
            }

            if (CurrentPage > SnippetCollection.Length - 1)
            {
                CurrentPage = SnippetCollection.Length - 1;
            } else if (CurrentPage < 0)
            {
                CurrentPage = 0;
            }

            SetPageDisplay(CurrentPage);
        }

        private void FieldListView_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            SetCreateButtonEnabled(true);
        }

        private void DestinationBrowse_Click(object sender, EventArgs e)
        {
            OutputDestination.Text = ShowFolderDialog("Select folder you want to output when using this snippet.");
        }

        private void ScanFields_Click(object sender, EventArgs e)
        {
            List<string> FieldNameList = ParseFieldListFromSnippet(SnippetCollection);

            FieldListView.DataSource = FieldList = FieldList == null ? GenerateFieldListDatatable(FieldNameList) : ModifyFieldListFromDatatable(FieldList, FieldNameList);
        }
        
        private void Create_Click(object sender, EventArgs e)
        {
            if (!RequiredDataInputted()) { MessageBox.Show("You need to input all red-backcolored fields."); return; }
            if (!isLegalDestination()) { MessageBox.Show("Illegal output destination."); return; }
            if (isSameBracket()) { MessageBox.Show("Field brackets at the left or right can not be same."); return; }

            string SnippetDir = ReadOnly.SnippetsPath + "/" + SnippetKey.ToString();
            string SnippetConfig = SnippetDir + "/" + ReadOnly.SnippetStructConfig;
            string SnippetFieldList = SnippetDir + "/" + ReadOnly.SnippetStructFieldList;

            SetProgress(0, "Checking snippet directory... (1/6)");
            if (!isEditMode) { SnippetDirectoryProcess(SnippetDir); }
            SetProgress(22, "Writing snippet config file... (2/6)");
            WriteConfig(SnippetConfig);
            SetProgress(45, "Writing snippet code... (3/6)");
            WriteSnippet(SnippetDir);
            SetProgress(67, "Writing snippet field list... (4/6)");
            WriteFieldList(SnippetFieldList);
            SetProgress(90, "Updating snippet list file... (5/6)");
            using (ConfigManager CM = new ConfigManager(ReadOnly.SnippetsListPath))
            {
                CM.SetConfig(SnippetKey.ToString(), SnippetName.Text);
                CM.SaveAsync();
            }
            SetProgress(100, "Snippet " + (isEditMode ? "Updating" : "Creation") + " completed. (6/6)");

            MessageBox.Show("Snippet " + (isEditMode ? "Updating" : "Creation") + " completed.\n\nSnippet Name: " + SnippetName.Text + "\nSnippet Key: " + SnippetKey.ToString());
            DialogResult = DialogResult.OK;
        }

        private void NewSnippetInitializer_FormClosed(object sender, FormClosedEventArgs e)
        {
            Dispose();
        }
        #endregion

        #region Methods
        #region Field Scanning
        private List<string> ParseFieldListFromSnippet(string[] SnippetCollection)
        {
            List<string> FieldList = new List<string>();

            foreach (string Snippet in SnippetCollection)
            {
                if (string.IsNullOrEmpty(Snippet)) { continue; }

                string[] SplittedSnippetByFBL = Snippet.Split(new string[] { FieldBracketL.Text }, StringSplitOptions.RemoveEmptyEntries);

                foreach (string Element in SplittedSnippetByFBL)
                {
                    int FBRPosition = Element.IndexOf(FieldBracketR.Text);
                    if (FBRPosition != -1)
                    {
                        FieldList.Add(Element.Substring(0, FBRPosition));
                    }
                }
            }

            return FieldList;
        }

        private DataTable GenerateFieldListDatatable(List<string> FieldList)
        {
            DataTable DT = new DataTable(ReadOnly.SnippetStructFieldList);

            foreach (KeyValuePair<string, Type> KVP in ReadOnly.SnippetFieldListDict)
            {
                DT.Columns.Add(CreateNewColumn(KVP.Key, KVP.Value));
            }

            DT.PrimaryKey = new DataColumn[] { DT.Columns[ReadOnly.SnippetFieldName] };

            foreach (string FieldName in FieldList)
            {
                try { DT.Rows.Add(CreateNewRow(DT, FieldName)); } catch (ConstraintException) { }
            }

            return DT;
        }

        private DataTable ModifyFieldListFromDatatable(DataTable DT, List<string> FieldNameList)
        {
            CheckAndAddFieldListDataColumn(ref DT);
            CheckAndRemoveFieldListDataColumn(ref DT);
            CheckAndAddFieldInfoDataRow(ref DT, FieldNameList);
            CheckAndRemoveFieldInfoDataRow(ref DT, FieldNameList);

            return DT;
        }

        private DataColumn CreateNewColumn(string ColumnName, Type ColumnDataType)
        {
            DataColumn DC = new DataColumn(ColumnName, ColumnDataType);
            DC.Unique = ColumnName == ReadOnly.SnippetFieldName;
            DC.AllowDBNull = !(ColumnName == ReadOnly.SnippetFieldName);
            return DC;
        }

        private DataRow CreateNewRow(DataTable FormatSourceDT, string FieldName)
        {
            DataRow DR = FormatSourceDT.NewRow();
            DR[ReadOnly.SnippetFieldName] = FieldName;
            return DR;
        }

        private void CheckAndAddFieldListDataColumn(ref DataTable DT)
        {
            foreach (KeyValuePair<string, Type> KVP in ReadOnly.SnippetFieldListDict)
            {
                if (!DT.Columns.Contains(KVP.Key))
                {
                    DT.Columns.Add(CreateNewColumn(KVP.Key, KVP.Value));
                }
            }
        }

        private void CheckAndRemoveFieldListDataColumn(ref DataTable DT)
        {
            List<DataColumn> DCToRemove = new List<DataColumn>();
            foreach (DataColumn DC in DT.Columns)
            {
                if (!ReadOnly.SnippetFieldListDict.ContainsKey(DC.ColumnName))
                {
                    DCToRemove.Add(DC);
                }
            }
            foreach (DataColumn DC in DCToRemove)
            {
                DT.Columns.Remove(DC);
            }
        }

        private void CheckAndAddFieldInfoDataRow(ref DataTable DT, List<string> FieldNameList)
        {
            foreach (string FieldName in FieldNameList)
            {
                if (!DT.Rows.Contains(FieldName))
                {
                    try { DT.Rows.Add(CreateNewRow(DT, FieldName)); } catch (ConstraintException) { }
                }
            }
        }

        private void CheckAndRemoveFieldInfoDataRow(ref DataTable DT, List<string> FieldNameList)
        {
            DataRow[] DRA = DT.Select();
            foreach (DataRow DR in DRA)
            {
                string FieldName = DR[ReadOnly.SnippetFieldName].ToString();
                if (DT.Rows.Contains(FieldName) && !FieldNameList.Contains(FieldName))
                {
                    DT.Rows.Remove(DR);
                }
            }
        }
        #endregion

        #region Set Control Attribute
        private void SetSnippetType(bool isRepeatedSnippet)
        {
            RepeatedSnippetCount = (int)SnippetCount.Value;
            StructGroup.Enabled = false;
        }

        private void SetCreateButtonEnabled(bool Enabled)
        {
            Submit.Enabled = Enabled;
        }

        private void SetMultiSnippetsEnabled(bool Enabled)
        {
            NextSnippet.Enabled = PrevSnippet.Enabled = SnippetCount.Enabled = Enabled;
        }

        private void SetPageDisplay(int CurrentPage)
        {
            int HeadPageIndex = 0, EndPageIndex = 1 + RepeatedSnippetCount;
            string PageText = "";

            if (SnippetCollection.Length > 1)
            {
                PageText = "HEAD ";

                for (byte i = 1; i < EndPageIndex; i++)
                {
                    PageText += i + " ";
                }
                PageText += "END";

                if (CurrentPage == HeadPageIndex)
                {
                    PageText = PageText.Replace("HEAD", "[HEAD]");
                }
                else if (CurrentPage == EndPageIndex)
                {
                    PageText = PageText.Replace("END", "[END]");
                }
                else
                {
                    PageText = PageText.Replace(CurrentPage.ToString(), "[" + CurrentPage.ToString() + "]");
                }
            }
            PageDisplay.Text = PageText;
            Snippet.Text = SnippetCollection[CurrentPage];
        }

        private void SetToEditMode(string SnippetPath)
        {
            isEditMode = true;

            LoadConfig(SnippetPath + "/" + ReadOnly.SnippetStructConfig);
            LoadSnippetToCollection(SnippetPath);
            LoadFieldList(SnippetPath + "/" + ReadOnly.SnippetStructFieldList);

            SetPageDisplay(CurrentPage);
            SetMultiSnippetsEnabled(SnippetCollection.Length > 1);
        }
        #endregion

        public string ShowFolderDialog(string Description)
        {
            FolderBrowserDialog FD = new FolderBrowserDialog();
            FD.Description = Description;
            FD.RootFolder = Environment.SpecialFolder.Desktop;
            FD.ShowNewFolderButton = true;
            if (FD.ShowDialog() == DialogResult.OK)
                return FD.SelectedPath;
            else
                return string.Empty;
        }

        #region Snippet Data Related
        public void SnippetDirectoryProcess(string SnippetDir)
        {
            if (Directory.Exists(SnippetDir))
            {
                switch (MessageBox.Show("Snippet directory exists. would you replace the snippet?", "Snippet Exists", MessageBoxButtons.YesNo))
                {
                    case DialogResult.Yes:
                        Directory.Delete(SnippetDir, true);
                        break;
                    case DialogResult.No:
                        return;
                }
            }

            Directory.CreateDirectory(SnippetDir);
        }

        #region Snippet Struct Creation
        private void WriteSnippet(string SnippetDir)
        {
            for (byte i = 0; i < SnippetCollection.Length; i++)
            {
                string SnippetFile = SnippetDir;

                if (i == 0)
                {
                    SnippetFile += "/" + (SnippetCollection.Length > 1 ? ReadOnly.SnippetStructSnippetHead : ReadOnly.SnippetStructSnippet);
                }
                else if (i == SnippetCollection.Length - 1)
                {
                    SnippetFile += "/" + ReadOnly.SnippetStructSnippetEnd;
                }
                else
                {
                    SnippetFile += "/" + ReadOnly.SnippetStructSnippetLoop + " " + i;
                }

                try
                {
                    using (StreamWriter SW = new StreamWriter(SnippetFile))
                    {
                        SW.WriteAsync(SnippetCollection[i]);
                    }
                }
                catch (IOException)
                {
                    MessageBox.Show("Snippet writing failed, ensure the file is not being used by another process and try again.");
                }

            }
        }

        private void WriteConfig(string ConfigPath)
        {
            using (ConfigManager CM = new ConfigManager(ConfigPath))
            {
                if (!isEditMode)
                {
                    CM.SetConfig(ReadOnly.SnippetCreatedDate, DateTime.Now.ToString("yyyy/MM/dd tt HH:mm"));
                }
                CM.SetConfig(ReadOnly.SnippetLastUsed, DateTime.Now.ToString("yyyy/MM/dd tt HH:mm"));
                CM.SetConfig(ReadOnly.SnippetDestination, OutputDestination.Text);
                CM.SetConfig(ReadOnly.SnippetOutputExtension, OutputExtension.Text);
                CM.SetConfig(ReadOnly.SnippetFieldBracketL, FieldBracketL.Text);
                CM.SetConfig(ReadOnly.SnippetFieldBracketR, FieldBracketR.Text);
                CM.SetConfig(ReadOnly.SnippetRemark, Remark.Text);
                CM.SetConfig(ReadOnly.SnippetName, SnippetName.Text);
                CM.SetConfig(ReadOnly.SnippetIsRepeatedSnippet, (SnippetCollection.Length > 1).ToString());
                CM.SetConfig(ReadOnly.SnippetGUID, SnippetKey.ToString());
                
                CM.SaveAsync();
            }
        }

        private void WriteFieldList(string FLPath)
        {
            FieldList.WriteXml(FLPath, XmlWriteMode.WriteSchema);
        }
        #endregion

        #region Snippet Struct Loading
        private void LoadConfig(string ConfigPath)
        {
            using (ConfigManager CM = new ConfigManager(ConfigPath))
            {
                Submit.Text = "Update";
                NormalPanel.Enabled = true;
                StructGroup.Enabled = false;

                OutputDestination.Text = CM.GetConfig(ReadOnly.SnippetDestination);
                OutputExtension.Text = CM.GetConfig(ReadOnly.SnippetOutputExtension);
                FieldBracketL.Text = CM.GetConfig(ReadOnly.SnippetFieldBracketL);
                FieldBracketR.Text = CM.GetConfig(ReadOnly.SnippetFieldBracketR);
                Remark.Text = CM.GetConfig(ReadOnly.SnippetRemark);
                SnippetName.Text = CM.GetConfig(ReadOnly.SnippetName);

                SnippetKey = new Guid(CM.GetConfig(ReadOnly.SnippetGUID));
            }
        }

        private void LoadSnippetToCollection(string SnippetDir)
        {
            using (ConfigManager CM = new ConfigManager(SnippetDir + "/"+ ReadOnly.SnippetStructConfig))
            {
                int SnippetsCount = Convert.ToBoolean(CM.GetConfig(ReadOnly.SnippetIsRepeatedSnippet)) ? RepeatedSnippetCount + 2 : 1;
                SnippetCollection = new string[SnippetsCount];

                for (byte i = 0; i < SnippetsCount; i++)
                {
                    string SnippetFile = SnippetDir + "/";

                    if (i == 0)
                    {
                        SnippetFile += (SnippetsCount == 1 ? ReadOnly.SnippetStructSnippet : ReadOnly.SnippetStructSnippetHead);
                    }
                    else if (i == SnippetCollection.Length - 1)
                    {
                        SnippetFile += ReadOnly.SnippetStructSnippetEnd;
                    }
                    else
                    {
                        SnippetFile += ReadOnly.SnippetStructSnippetLoop + " " + i;
                    }

                    using (StreamReader SR = new StreamReader(SnippetFile))
                    {
                        SnippetCollection[i] = SR.ReadToEnd();
                    }
                }
            }
        }

        private void LoadFieldList(string FLPath)
        {
            FieldList = new DataTable(ReadOnly.SnippetStructFieldList);
            FieldList.ReadXml(FLPath);
            FieldListView.DataSource = FieldList;
        }
        #endregion

        #endregion

        #region Check
        private bool RequiredDataInputted()
        {
            Color IllegalColor = Color.FromArgb(255, 170, 170);
            Color LegalColor = SystemColors.Window;

            SnippetName.BackColor = string.IsNullOrEmpty(SnippetName.Text) ? IllegalColor : LegalColor;
            OutputDestination.BackColor = string.IsNullOrEmpty(OutputDestination.Text) ? IllegalColor : LegalColor;
            FieldBracketL.BackColor = string.IsNullOrEmpty(OutputDestination.Text) ? IllegalColor : LegalColor;
            FieldBracketR.BackColor = string.IsNullOrEmpty(OutputDestination.Text) ? IllegalColor : LegalColor;
            Snippet.BackColor = string.IsNullOrEmpty(OutputDestination.Text) ? IllegalColor : LegalColor;

            bool A = SnippetName.BackColor.Equals(OutputDestination.BackColor);
            bool B = OutputDestination.BackColor.Equals(FieldBracketL.BackColor);
            bool C = FieldBracketL.BackColor.Equals(FieldBracketR.BackColor);
            bool D = FieldBracketR.BackColor.Equals(Snippet.BackColor);
            bool E = Snippet.BackColor.Equals(LegalColor);

            return A && B && C && D && E;
        }

        private bool isLegalDestination()
        {
            try { DirectoryInfo DI = new DirectoryInfo(OutputDestination.Text); }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        private bool isSameBracket()
        {
            return FieldBracketL.Text == FieldBracketR.Text;
        }
        #endregion

        private void SetProgress(int value, string Progress)
        {
            ProgressBar.Value = value;
            ProgressText.Text = Progress;
            Application.DoEvents();
        }
        #endregion
    }
}
