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
            this.ItemList = new System.Windows.Forms.ListView();
            this.Empty = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Submit = new System.Windows.Forms.Button();
            this.Tips = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // ItemList
            // 
            this.ItemList.Activation = System.Windows.Forms.ItemActivation.TwoClick;
            this.ItemList.AllowColumnReorder = true;
            this.ItemList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Empty});
            this.ItemList.FullRowSelect = true;
            this.ItemList.GridLines = true;
            this.ItemList.HideSelection = false;
            this.ItemList.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.ItemList.Location = new System.Drawing.Point(12, 12);
            this.ItemList.Name = "ItemList";
            this.ItemList.ShowItemToolTips = true;
            this.ItemList.Size = new System.Drawing.Size(1018, 549);
            this.ItemList.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.ItemList.TabIndex = 8;
            this.ItemList.UseCompatibleStateImageBehavior = false;
            this.ItemList.View = System.Windows.Forms.View.Details;
            // 
            // Empty
            // 
            this.Empty.Text = "Empty";
            this.Empty.Width = 0;
            // 
            // Select
            // 
            this.Submit.Enabled = false;
            this.Submit.Location = new System.Drawing.Point(945, 567);
            this.Submit.Name = "Select";
            this.Submit.Size = new System.Drawing.Size(85, 32);
            this.Submit.TabIndex = 9;
            this.Submit.Text = "Select";
            this.Submit.UseVisualStyleBackColor = true;
            // 
            // Tips
            // 
            this.Tips.Location = new System.Drawing.Point(12, 567);
            this.Tips.Margin = new System.Windows.Forms.Padding(3);
            this.Tips.Name = "Tips";
            this.Tips.Size = new System.Drawing.Size(927, 32);
            this.Tips.TabIndex = 10;
            this.Tips.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ListviewSelector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1042, 608);
            this.Controls.Add(this.Tips);
            this.Controls.Add(this.Submit);
            this.Controls.Add(this.ItemList);
            this.Font = new System.Drawing.Font("Microsoft JhengHei", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.Name = "ListviewSelector";
            this.Text = "ListviewSelector";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView ItemList;
        private System.Windows.Forms.ColumnHeader Empty;
        private System.Windows.Forms.Button Submit;
        private System.Windows.Forms.Label Tips;
    }
}