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
    public partial class StringProcessTester : Form
    {
        public StringProcessTester()
        {
            InitializeComponent();
            InitializeEventHandler();
        }

        public StringProcessTester(string CMDText)
        {
            InitializeComponent();
            InitializeEventHandler();

            CMD.Text = CMDText;
        }

        private void InitializeEventHandler()
        {
            Execute.Click += Execute_Click;
        }

        private void Execute_Click(object sender, EventArgs e)
        {
            StringProcessor SP = new StringProcessor(Input.Text);

            string[] CommandLine = CMD.Text.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string CMDLine in CommandLine)
            {
                if (!string.IsNullOrEmpty(CMDLine))
                {
                    Output.Text = SP.Process(CMDLine, true);
                }
            }
        }
    }
}
