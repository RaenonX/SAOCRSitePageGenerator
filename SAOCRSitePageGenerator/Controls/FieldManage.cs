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
    public partial class FieldManage : UserControl
    {
        bool isAuto;
        DataRow DR;
        SearchTestHandler SearchTestMethod;

        #region Initialize
        public FieldManage(DataRow DR, SearchTestHandler SearchTestMethod)
        {
            this.DR = DR;
            this.SearchTestMethod = SearchTestMethod;

            InitializeComponent();
            InitializeEventHandler();
            InitializeControls();
        }

        private void InitializeControls()
        {
            FieldName.Text = Convert.ToString(DR[ReadOnly.SnippetFieldName]);

            try { isAuto = AutoFill.Checked = Convert.ToBoolean(DR[ReadOnly.SnippetFieldAutoFill] == DBNull.Value ? false : DR[ReadOnly.SnippetFieldAutoFill]); } catch(ArgumentException) { };
            try { Value.Enabled = !ReadOnlyAttribute.Checked; } catch (ArgumentException) { };
            try { ReadOnlyAttribute.Checked = Convert.ToBoolean(DR[ReadOnly.SnippetFieldReadOnly] == DBNull.Value ? false : DR[ReadOnly.SnippetFieldReadOnly]); } catch (ArgumentException) { };

            Auto.Enabled = isAuto;

            try { DataSet.Text = Convert.ToString(DR[ReadOnly.SnippetFieldDataSet]); } catch (ArgumentException) { };
            try { DataSource.Text = Convert.ToString(DR[ReadOnly.SnippetFieldDataSource]);
            } catch (ArgumentException) { };
            try { DataColumnName.Text = Convert.ToString(DR[ReadOnly.SnippetFieldDataColumnName]);
            } catch (ArgumentException) { };
            try { DataSelect.Text = Convert.ToString(DR[ReadOnly.SnippetFieldDataSelect]); } catch (ArgumentException) { };

            Value.Enabled = !isAuto;
        }

        private void InitializeEventHandler()
        {
            SizeChanged += FieldManage_SizeChanged;
            SearchTest.Click += SearchTest_Click;
        }
        #endregion

        #region Events
        private void FieldManage_SizeChanged(object sender, EventArgs e)
        {
            Panel1.Size = Size.Subtract(Size, new Size(Panel1.Margin.Top, Panel1.Margin.Left));
        }

        private void SearchTest_Click(object sender, EventArgs e)
        {
            SearchResult.Visible = !SearchResult.Visible;
            SearchTest.Text = SearchResult.Visible ? "Back" : "Search Test";

            if (!SearchResult.Visible)
            {
                if (InvokeRequired)
                    SearchTestMethod?.Invoke(this, e);
                else
                    SearchTestMethod(this, e);
            }
        }
        #endregion

        #region Methods
        public string GetValue()
        {
            return Value.Text;
        }
        #endregion
    }
}
