namespace SAOCRSitePageGenerator
{
    partial class ListviewSelector
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.DatatableList = new System.Windows.Forms.ListView();
            this.Empty = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Select = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // DatatableList
            // 
            this.DatatableList.Activation = System.Windows.Forms.ItemActivation.TwoClick;
            this.DatatableList.AllowColumnReorder = true;
            this.DatatableList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Empty});
            this.DatatableList.FullRowSelect = true;
            this.DatatableList.GridLines = true;
            this.DatatableList.HideSelection = false;
            this.DatatableList.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.DatatableList.Location = new System.Drawing.Point(12, 12);
            this.DatatableList.Name = "DatatableList";
            this.DatatableList.ShowItemToolTips = true;
            this.DatatableList.Size = new System.Drawing.Size(1018, 549);
            this.DatatableList.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.DatatableList.TabIndex = 8;
            this.DatatableList.UseCompatibleStateImageBehavior = false;
            this.DatatableList.View = System.Windows.Forms.View.Details;
            // 
            // Empty
            // 
            this.Empty.Text = "Empty";
            this.Empty.Width = 0;
            // 
            // Select
            // 
            this.Select.Location = new System.Drawing.Point(945, 567);
            this.Select.Name = "Select";
            this.Select.Size = new System.Drawing.Size(85, 32);
            this.Select.TabIndex = 9;
            this.Select.Text = "Select";
            this.Select.UseVisualStyleBackColor = true;
            // 
            // ListviewSelector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1042, 608);
            this.Controls.Add(this.Select);
            this.Controls.Add(this.DatatableList);
            this.Font = new System.Drawing.Font("Microsoft JhengHei", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.Name = "ListviewSelector";
            this.Text = "ListviewSelector";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView DatatableList;
        private System.Windows.Forms.ColumnHeader Empty;
        private System.Windows.Forms.Button Select;
    }
}