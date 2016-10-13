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
    public partial class SnippetGenerator : Form
    {
        bool isRepeatedSnippet;
        KeyValuePair<string, string> SnippetInfo;
        DataTable FieldList;

        #region Initialize
        public SnippetGenerator(KeyValuePair<string, string> SnippetInfo)
        {
            this.SnippetInfo = SnippetInfo;

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
            FieldList = LoadFieldList(SnippetInfo.Key);
            ShowFieldManageControls(ref FieldList);
            using (ConfigManager CM = new ConfigManager(ReadOnly.SnippetsPath + "/" + SnippetInfo.Key + "/" + ReadOnly.SnippetStructConfig))
            {
                isRepeatedSnippet = Convert.ToBoolean(CM.GetConfig(ReadOnly.SnippetIsRepeatedSnippet));
            }
        }
        #endregion

        #region Events
        private void SearchTest_Click(object sender, EventArgs e)
        {
            FieldManage FM = (FieldManage)sender;

        }

        private void Generate_Click(object sender, EventArgs e)
        {
            GenerateSnippet(isRepeatedSnippet);
        }
        #endregion

        #region Methods
        private DataTable LoadFieldList(string SnippetKey)
        {
            DataTable DT = new DataTable(ReadOnly.SnippetStructFieldList);
            string FieldListPath = ReadOnly.SnippetsPath + "/" + SnippetKey + "/" + ReadOnly.SnippetStructFieldList;

            DT.ReadXml(FieldListPath);

            return DT;
        }

        private void ShowFieldManageControls(ref DataTable DT)
        {
            for (int i = 0; i < DT.Rows.Count; i++)
            {
                FieldManage FM = new FieldManage(DT.Rows[i], SearchTest_Click);
                FM.Name = Convert.ToString(DT.Rows[i][ReadOnly.SnippetFieldName]);
                FM.Top = i * FM.Height + (i + 1) * FM.Margin.Top;
                FM.Left = FM.Margin.Left;
                FieldPanel.Controls.Add(FM);
            }
        }

        private StringBuilder LoadSnippet(string SnippetKey)
        {
            string FieldListPath = ReadOnly.SnippetsPath + "/" + SnippetKey + "/" + ReadOnly.SnippetStructSnippet;

            StringBuilder SB = new StringBuilder(new StreamReader(FieldListPath).ReadToEnd());

            return SB;
        }

        private void GenerateSnippet(bool isRepeatedSnippet)
        {
            using (ConfigManager CM = new ConfigManager(ReadOnly.SnippetsPath + "/" + SnippetInfo.Key + "/" + ReadOnly.SnippetStructConfig))
            {
                if (isRepeatedSnippet)
                {

                }
                else
                {
                    string SnippetExportPath = CM.GetConfig(ReadOnly.SnippetDestination) + "/" + SnippetInfo.Key + CM.GetConfig(ReadOnly.SnippetOutputExtension);
                    StringBuilder SB = LoadSnippet(SnippetInfo.Key);

                    foreach (DataRow DR in FieldList.Rows)
                    {
                        string FieldName = Convert.ToString(DR[ReadOnly.SnippetFieldName]);

                        SB = SB.Replace(CM.GetConfig(ReadOnly.SnippetFieldBracketL) + FieldName + CM.GetConfig(ReadOnly.SnippetFieldBracketR),
                            ((FieldManage)FieldPanel.Controls[FieldName]).GetValue());
                    }

                    if (!File.Exists(SnippetExportPath))
                    {
                        using (FileStream FM = File.Create(SnippetExportPath)) { }
                    }
                    else
                    {
                        switch (MessageBox.Show("Old exported snippet detected. Replace with new file?", "Old exported snippet detected", MessageBoxButtons.YesNo))
                        {
                            case DialogResult.Yes:
                                File.Delete(SnippetExportPath);
                                using (FileStream FM = File.Create(SnippetExportPath)) { }
                                break;
                            case DialogResult.No:
                                MessageBox.Show("Snippet output process aborted.");
                                return;
                        }
                    }

                    using (StreamWriter SW = new StreamWriter(SnippetExportPath))
                    {
                        SW.Write(SB.ToString());
                    }

                    if (MessageBox.Show("Snippet exporting completed.\n\nExported Path: " + SnippetExportPath + "\nOpen exported snippet?", "Snippet exporting completed", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        Process.Start(SnippetExportPath);
                    }
                    DialogResult = DialogResult.OK;
                }
            }
        }
        #endregion
    }
}
