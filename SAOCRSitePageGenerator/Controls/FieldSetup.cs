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
            FieldName.Text = Convert.ToString(DR[ReadOnly.SnippetFieldName]);

            try { AutoFill.Checked = isAuto = Convert.ToBoolean(DR[ReadOnly.SnippetFieldAutoFill] == DBNull.Value ? false : DR[ReadOnly.SnippetFieldAutoFill]); } catch(ArgumentException) { };
            try { ReadOnlyAttribute.Checked = Convert.ToBoolean(DR[ReadOnly.SnippetFieldReadOnly] == DBNull.Value ? false : DR[ReadOnly.SnippetFieldReadOnly]); } catch (ArgumentException) { };
            try { Internal.Checked = Convert.ToBoolean(DR[ReadOnly.SnippetFieldForInternalUse] == DBNull.Value ? false : DR[ReadOnly.SnippetFieldForInternalUse]); } catch (ArgumentException) { };
            Value.Enabled = !ReadOnlyAttribute.Checked;
            
            try { DataSet.Text = Convert.ToString(DR[ReadOnly.SnippetFieldDataSet]); } catch (ArgumentException) { };
            try { DataSource.Text = Convert.ToString(DR[ReadOnly.SnippetFieldDataSource]); } catch (ArgumentException) { };
            try { DataSelect.Text = Convert.ToString(DR[ReadOnly.SnippetFieldDataSelect]); } catch (ArgumentException) { };
            try { ProcessCMD.Text = Convert.ToString(DR[ReadOnly.SnippetFieldProcessCMD]).Replace("\\n", "\r\n") + "\r\n"; } catch (ArgumentException) { };

            Auto.Enabled = isAuto && !ReadOnlyAttribute.Checked;
            Value.Enabled = !isAuto && !ReadOnlyAttribute.Checked;
            Process.Enabled = !ReadOnlyAttribute.Checked;

            ProcessCMDCatalog.Items.AddRange(Enum.GetNames(typeof(StringProcessCMDType)));
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

            if (!SearchResult.Visible)
            {
                if (InvokeRequired)
                    SearchTestMethod?.Invoke(this, e);
                else
                    SearchTestMethod(this, e);
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

        private void ShowStringProcessTester()
        {
            StringProcessTester SPT = new StringProcessTester(ProcessCMD.Text);
            SPT.Show();
        }
        #endregion

        public string GetManualValue()
        {
            return Value.Text;
        }

        public DataRow GetDataRowSetToControl()
        {
            return DR;
        }
        #endregion
    }
}
