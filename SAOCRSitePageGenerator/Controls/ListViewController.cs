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
    public partial class ListViewController : UserControl
    {
        [Category("Misc")]
        public Orientation ButtonOrientation { get; set; }
        [Category("Misc")]
        public bool DisplayRefreshButton { get; set; }

        new Rectangle DefaultMargin = new Rectangle(3, 3, 3, 3);
        Size DefaultActionButtonSize = new Size(85, 28);
        Size DefaultNewItemButtonSize = new Size(100, 28);
        Size DefaultRefreshButtonSize = new Size(85, 20);

        bool DefaultShowRefreshButton = false;
        Orientation DefaultButtonOrientation = Orientation.Horizontal;

        #region Initialize
        public ListViewController()
        {
            DisplayRefreshButton = DefaultShowRefreshButton;
            ButtonOrientation = DefaultButtonOrientation;
            CommonInitiaize();
        }

        public ListViewController(bool DisplayRefreshButton)
        {
            this.DisplayRefreshButton = DisplayRefreshButton;
            ButtonOrientation = DefaultButtonOrientation;
            CommonInitiaize();
        }

        public ListViewController(Orientation ButtonOrientation)
        {
            DisplayRefreshButton = DefaultShowRefreshButton;
            this.ButtonOrientation = ButtonOrientation;
            CommonInitiaize();
        }

        public ListViewController(bool DisplayRefreshButton, Orientation ButtonOrientation)
        {
            this.DisplayRefreshButton = DisplayRefreshButton;
            this.ButtonOrientation = ButtonOrientation;
            CommonInitiaize();
        }

        public void CommonInitiaize()
        {
            InitializeComponent();
            InitializeControls();
            InitializeEventHandler();
            Initialize();
        }

        private void InitializeControls()
        {
            SetSizeAndLocation(this, EventArgs.Empty);
        }

        private void InitializeEventHandler()
        {
            SizeChanged += SetSizeAndLocation;

            switch (ButtonOrientation)
            {
                case Orientation.Horizontal:
                    NewButton.SizeChanged += SetSizeAndLocation;
                    ExecuteButton.SizeChanged += SetSizeAndLocation;
                    EditButton.SizeChanged += SetSizeAndLocation;
                    RemoveButton.SizeChanged += SetSizeAndLocation;
                    break;
                case Orientation.Vertical:
                    NewButton.SizeChanged += SetSizeAndLocation;
                    ExecuteButton.SizeChanged += SetSizeAndLocation;
                    EditButton.SizeChanged += SetSizeAndLocation;
                    RemoveButton.SizeChanged += SetSizeAndLocation;
                    break;
            }
        }

        private void Initialize()
        {
            
        }
        #endregion

        #region Events

        #endregion

        #region Methods
        private void SetSizeAndLocation(object sender, EventArgs e)
        {
            switch (ButtonOrientation)
            {
                case Orientation.Horizontal:
                    RemoveButton.Size = EditButton.Size = ExecuteButton.Size;
                    NewButton.Size = new Size(NewButton.Size.Width, RemoveButton.Size.Height);

                    NewButton.Location = new Point(0, Size.Height - NewButton.Size.Height);
                    ExecuteButton.Location = new Point(Size.Subtract(Size, ExecuteButton.Size));
                    EditButton.Location = new Point(ExecuteButton.Location.X - ExecuteButton.Margin.Left - EditButton.Margin.Right - EditButton.Size.Width, ExecuteButton.Location.Y);
                    RemoveButton.Location = new Point(EditButton.Location.X - EditButton.Margin.Left - RemoveButton.Margin.Right - RemoveButton.Size.Width, EditButton.Location.Y);
                    if (DisplayRefreshButton)
                    { 
                        RefreshButton.Location = new Point(Size.Width - RefreshButton.Size.Width, 0);
                        List.Size = new Size(Size.Width, Size.Height - RefreshButton.Size.Height - RefreshButton.Margin.Bottom - List.Margin.Top - List.Margin.Bottom - Math.Max(NewButton.Margin.Top, Math.Max(RemoveButton.Margin.Top, Math.Max(EditButton.Margin.Top, ExecuteButton.Margin.Top))) - NewButton.Size.Height);

                        List.Location = new Point(0, RefreshButton.Size.Height + RefreshButton.Margin.Bottom + List.Margin.Top);
                    }
                    else
                    {
                        RefreshButton.Location = new Point(Size.Width - RefreshButton.Size.Width, -(RefreshButton.Size.Width));
                        List.Size = new Size(Size.Width, Size.Height - List.Margin.Bottom - Math.Max(NewButton.Margin.Top, Math.Max(RemoveButton.Margin.Top, Math.Max(EditButton.Margin.Top, ExecuteButton.Margin.Top))) - NewButton.Size.Height);
                        List.Location = new Point(0, 0);
                    }
                    break;
                case Orientation.Vertical:
                    List.Location = new Point(0, 0);
                    List.Size = new Size(Size.Width - List.Margin.Right - ExecuteButton.Margin.Left - ExecuteButton.Size.Width, Size.Height);

                    if (DisplayRefreshButton)
                    {
                        RefreshButton.Location = new Point(Size.Width - RefreshButton.Size.Width, 0);
                        NewButton.Location = new Point(List.Width + List.Margin.Right + ExecuteButton.Margin.Left, RefreshButton.Size.Height + RefreshButton.Margin.Bottom + NewButton.Margin.Top);
                    }
                    else
                    {
                        RefreshButton.Location = new Point(Size.Width - RefreshButton.Size.Width, -(RefreshButton.Size.Width));
                    }
                    break;
            }
        }
        #endregion
    }
}
