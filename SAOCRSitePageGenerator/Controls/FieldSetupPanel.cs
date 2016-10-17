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
    public partial class FieldSetupPanel : UserControl
    {
        public DataTable FieldList { get; set; }
        public SearchTestHandler SearchTestMethod { get; set; }
        public Dictionary<string, FieldSetup> FieldCollection { get; set; }
        public Dictionary<string, DataRow> FieldDataCollection { get; set; }

        #region Initialize
        public FieldSetupPanel(DataTable FieldList, SearchTestHandler SearchTestMethod)
        {
            this.FieldList = FieldList;
            this.SearchTestMethod = SearchTestMethod;

            CommonInitialize();
        }

        private void CommonInitialize()
        {
            InitializeComponent();
            InitializeEventHandler();
            Initialize();
        }

        private void Initialize()
        {
            FieldCollection = new Dictionary<string, FieldSetup>();
            FieldDataCollection = new Dictionary<string, DataRow>();

            GenerateFieldSetups(FieldList);
        }

        private void InitializeEventHandler()
        {
            SizeChanged += FieldSetupPanel_SizeChanged;
            MouseEnter += FieldSetupPanel_MouseEnter;
        }
        #endregion

        #region Events
        private void FieldSetupPanel_SizeChanged(object sender, EventArgs e)
        {
            Panel1.Size = Size.Subtract(Size, new Size(Panel1.Margin.Top, Panel1.Margin.Left));
        }

        private void FieldSetupPanel_MouseEnter(object sender, EventArgs e)
        {
            SetActiveControl();
        }
        #endregion

        #region Methods
        private void SetActiveControl()
        {
            ActiveControl = Panel1;
        }

        private void GenerateFieldSetups(DataTable DT)
        {
            for (int i = 0; i < DT.Rows.Count; i++)
            {
                DataRow DR = DT.Rows[i];
                string FieldName = Convert.ToString(DR[ReadOnly.SnippetFieldName]);

                FieldSetup FM = new FieldSetup(DR, SearchTestMethod);
                FM.Name = FieldName;
                FM.Top = i * FM.Height + ((i + 1) * 5);
                FM.Left = FM.Margin.Left;

                Panel1.Controls.Add(FM);
                FieldCollection.Add(FieldName, FM);
                FieldDataCollection.Add(FieldName, DR);
            }
        }
        #endregion
    }
}
