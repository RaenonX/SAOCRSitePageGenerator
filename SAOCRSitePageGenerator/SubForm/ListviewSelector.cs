using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SAOCRSitePageGenerator
{
    public partial class ListviewSelector : Form
    {
        DataTable DT;

        #region Initialize
        public ListviewSelector(DataTable DT, string TipsText)
        {
            this.DT = DT;
            Tips.Text = TipsText;

            CommonInitialize();
        }

        public ListviewSelector(DataTable DT)
        {
            this.DT = DT;

            CommonInitialize();
        }

        private void CommonInitialize()
        {
            InitializeComponent();
            Initialize();
            InitializeEventHandler();
        }

        private void InitializeEventHandler()
        {
            ItemList.ItemSelectionChanged += ItemList_ItemSelectionChanged;
            Submit.Click += Select_Click;
            FormClosed += ListviewSelector_FormClosed;
        }

        private void Initialize()
        {
            ItemList.Columns.Clear();
            ItemList.Columns.Add("Empty", 0);
            foreach (DataColumn DC in DT.Columns)
            {
                ColumnHeader CH = new ColumnHeader();
                CH.Text = CH.Name = DC.ColumnName;
                CH.Width = 100;
                ItemList.Columns.Add(CH);
            }

            foreach (DataRow DR in DT.Rows)
            {
                ListViewItem LSV = new ListViewItem();
                foreach (object item in DR.ItemArray)
                {
                    LSV.SubItems.Add(item.ToString());
                }
                ItemList.Items.Add(LSV);
            }
        }
        #endregion

        #region Events
        private void Select_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void ItemList_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            Submit.Enabled = ItemList.SelectedItems.Count > 0;
        }

        private void ListviewSelector_FormClosed(object sender, FormClosedEventArgs e)
        {
            Dispose();
        }
        #endregion

        #region Methods
        public new ListView.SelectedListViewItemCollection ShowDialog()
        {
            return base.ShowDialog() == DialogResult.OK ? ItemList.SelectedItems : null;
        }
        #endregion
    }
}
