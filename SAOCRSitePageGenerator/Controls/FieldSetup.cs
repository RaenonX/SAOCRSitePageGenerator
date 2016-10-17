using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SAOCRSitePageGenerator
{
    public partial class FieldSetup : UserControl
    {
        bool isAuto;
        DataRow DR;
        SearchTestHandler SearchTestMethod;

        #region Initialize
        public FieldSetup(DataRow DR, SearchTestHandler SearchTestMethod)
        {
            this.DR = DR;
            this.SearchTestMethod = SearchTestMethod;

            CommonInitialize();
        }

        private void CommonInitialize()
        {
            InitializeComponent();
            InitializeEventHandler();
            InitializeControls();
        }

        private void InitializeControls()
        {
            LoadDataAndSetToControlByDataRow(DR);
            SetControlByReadOnlyAttribute(ReadOnlyAttribute.Checked);
            AddStringProcessCMDType();
        }

        private void InitializeEventHandler()
        {
            SizeChanged += FieldManage_SizeChanged;

            SearchTest.Click += SearchTest_Click;

            ProcessCMDView.Click += ProcessCMDView_Click;
            ProcessCMDAdd.Click += ProcessCMDAdd_Click;
            ProcessCMDTest.Click += ProcessCMDTest_Click;
            ProcessCMD.KeyDown += ProcessCMD_KeyDown;
            ProcessCMDCatalog.MouseDoubleClick += ProcessCMDCatalog_MouseDoubleClick;
            ProcessParam1.KeyPress += ProcessParam1_KeyPress;
            ProcessParam2.KeyPress += ProcessParam2_KeyPress;
        }
        #endregion

        #region Events
        private void FieldManage_SizeChanged(object sender, EventArgs e)
        {
            Panel1.Size = Size.Subtract(Size, new Size(Panel1.Margin.Top, Panel1.Margin.Left));
        }

        private void SearchTest_Click(object sender, EventArgs e)
        {
            SetSearchTestResultView(!SearchResult.Visible);

            if (SearchResult.Visible)
            {
                if (InvokeRequired)
                    SetReturnSearchResultToResultLabel(SearchTestMethod?.Invoke(this, e));
                else
                    SetReturnSearchResultToResultLabel(SearchTestMethod(this, e));
            }
        }

        private void ProcessCMDView_Click(object sender, EventArgs e)
        {
            SetTextProcessingAreaVisibility(!ProcessCMD.Visible);
        }

        private void ProcessCMDAdd_Click(object sender, EventArgs e)
        {
            AddCommandLineToProcess();
        }

        private void ProcessCMDTest_Click(object sender, EventArgs e)
        {
            ShowStringProcessTester();
        }

        private void ProcessCMD_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.A && e.Modifiers == Keys.Control)
            {
                ProcessCMD.SelectAll();
            }
        }
        
        private void ProcessCMDCatalog_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            AddCommandLineToProcess();
        }

        private void ProcessParam2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                AddCommandLineToProcess();
            }
        }

        private void ProcessParam1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                AddCommandLineToProcess();
            }
        }
        #endregion

        #region Methods

        #region Control Modifying
        private void SetTextProcessingAreaVisibility(bool isShowingCommandLine)
        {
            ProcessCMDCatalog.Visible = !isShowingCommandLine;
            ProcessParam1.Visible = !isShowingCommandLine;
            ProcessParam2.Visible = !isShowingCommandLine;
            ProcessRelation.Visible = !isShowingCommandLine;
            ProcessCMD.Visible = isShowingCommandLine;
            ProcessCMDTest.Visible = isShowingCommandLine;
        }

        private void SetSearchTestResultView(bool isAtTestView)
        {
            SearchResult.Visible = isAtTestView;
            SearchTest.Text = isAtTestView ? "Back" : "Search Test";
        }

        private void SetControlByReadOnlyAttribute(bool isReadOnly)
        {
            Auto.Enabled = isAuto && !isReadOnly;
            ManualValue.Enabled = !isAuto && !isReadOnly;
            Process.Enabled = !isReadOnly;
        }

        private void AddCommandLineToProcess()
        {
            StringProcessor CMDProc = new StringProcessor();

            try
            {
                if (ProcessCMDCatalog.SelectedItem != null)
                {
                    ProcessCMD.AppendText(CMDProc.GetStringProcessCMD((StringProcessCMDType)Enum.Parse(typeof(StringProcessCMDType), ProcessCMDCatalog.SelectedItem.ToString()), ProcessParam1.Text, ProcessParam2.Text) + "\n");
                }
                ProcessParam1.Text = ProcessParam2.Text = "";
            } catch (IllegalParameterException ex) {
                MessageBox.Show("Illegal parameter recognized. Recheck the parameter. \n\nCommand type: " + ex.CMDType.ToString());
            }
        }

        private void AddStringProcessCMDType()
        {
            ProcessCMDCatalog.Items.AddRange(Enum.GetNames(typeof(StringProcessCMDType)));
        }

        private void ShowStringProcessTester()
        {
            StringProcessTester SPT = new StringProcessTester(ProcessCMD.Text);
            SPT.Show();
        }

        private void SetReturnSearchResultToResultLabel(string Result)
        {
            SearchResult.Text = Result;
        }

        private void LoadDataAndSetToControlByDataRow(DataRow DR)
        {
            OrderIndex.Text = Convert.ToString(DR[ReadOnly.SnippetFieldIndex]);
            FieldName.Text = Convert.ToString(DR[ReadOnly.SnippetFieldName]);

            AutoFill.Checked = isAuto = GetBooleanFromDataRow(DR, ReadOnly.SnippetFieldAutoFill);
            ReadOnlyAttribute.Checked = GetBooleanFromDataRow(DR, ReadOnly.SnippetFieldReadOnly);
            Internal.Checked = GetBooleanFromDataRow(DR, ReadOnly.SnippetFieldForInternalUse);
            LoopUse.Checked = GetBooleanFromDataRow(DR, ReadOnly.SnippetFieldDataForLoopUse);

            ManualValue.Enabled = !ReadOnlyAttribute.Checked;

            DS.Text = Convert.ToString(DR[ReadOnly.SnippetFieldDataSet]);
            DT.Text = Convert.ToString(DR[ReadOnly.SnippetFieldDataTable]);
            DTSelect.Text = Convert.ToString(DR[ReadOnly.SnippetFieldDataQuery]);
            DTReturnColumnName.Text = Convert.ToString(DR[ReadOnly.SnippetFieldDataQueryReturnColumnName]);
            ProcessCMD.Text = Convert.ToString(DR[ReadOnly.SnippetFieldProcessCMD]).Replace("\\n", "\r\n") + "\r\n";
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

        public string GetManualValue()
        {
            return ManualValue.Text;
        }

        public string GetDataTableName()
        {
            return DT.Text;
        }

        public string GetDataSetName()
        {
            return DS.Text;
        }

        public string GetDataTableSelectQuery()
        {
            return DTSelect.Text;
        }

        public string GetDataTableReturnColumnName()
        {
            return DTReturnColumnName.Text;
        }
        #endregion
    }
}
