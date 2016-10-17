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
    public partial class LoadingBlock : Form
    {
        public LoadingBlock()
        {
            InitializeComponent();
        }

        public LoadingBlock(string Text)
        {
            InitializeComponent();
            label1.Text = Text;
        }
    }
}
