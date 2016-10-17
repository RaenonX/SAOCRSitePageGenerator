namespace SAOCRSitePageGenerator
{
    partial class ListViewController
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.RemoveButton = new System.Windows.Forms.Button();
            this.EditButton = new System.Windows.Forms.Button();
            this.RefreshButton = new System.Windows.Forms.Button();
            this.NewButton = new System.Windows.Forms.Button();
            this.List = new System.Windows.Forms.ListView();
            this.ExecuteButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // RemoveButton
            // 
            this.RemoveButton.Enabled = false;
            this.RemoveButton.Location = new System.Drawing.Point(791, 122);
            this.RemoveButton.Name = "RemoveButton";
            this.RemoveButton.Size = new System.Drawing.Size(100, 28);
            this.RemoveButton.TabIndex = 11;
            this.RemoveButton.Text = "Remove";
            this.RemoveButton.UseVisualStyleBackColor = true;
            // 
            // EditButton
            // 
            this.EditButton.Enabled = false;
            this.EditButton.Location = new System.Drawing.Point(791, 156);
            this.EditButton.Name = "EditButton";
            this.EditButton.Size = new System.Drawing.Size(100, 28);
            this.EditButton.TabIndex = 16;
            this.EditButton.Text = "Edit";
            this.EditButton.UseVisualStyleBackColor = true;
            // 
            // RefreshButton
            // 
            this.RefreshButton.Font = new System.Drawing.Font("Microsoft JhengHei", 8.25F);
            this.RefreshButton.Location = new System.Drawing.Point(837, 26);
            this.RefreshButton.Name = "RefreshButton";
            this.RefreshButton.Size = new System.Drawing.Size(54, 20);
            this.RefreshButton.TabIndex = 12;
            this.RefreshButton.Text = "Refresh";
            this.RefreshButton.UseVisualStyleBackColor = true;
            // 
            // NewButton
            // 
            this.NewButton.Location = new System.Drawing.Point(791, 59);
            this.NewButton.Name = "NewButton";
            this.NewButton.Size = new System.Drawing.Size(100, 57);
            this.NewButton.TabIndex = 15;
            this.NewButton.Text = "New";
            this.NewButton.UseVisualStyleBackColor = true;
            // 
            // List
            // 
            this.List.Activation = System.Windows.Forms.ItemActivation.TwoClick;
            this.List.AllowColumnReorder = true;
            this.List.FullRowSelect = true;
            this.List.GridLines = true;
            this.List.HideSelection = false;
            this.List.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.List.Location = new System.Drawing.Point(0, 26);
            this.List.Name = "List";
            this.List.ShowItemToolTips = true;
            this.List.Size = new System.Drawing.Size(785, 240);
            this.List.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.List.TabIndex = 13;
            this.List.UseCompatibleStateImageBehavior = false;
            this.List.View = System.Windows.Forms.View.Details;
            // 
            // ExecuteButton
            // 
            this.ExecuteButton.Enabled = false;
            this.ExecuteButton.Location = new System.Drawing.Point(791, 190);
            this.ExecuteButton.Name = "ExecuteButton";
            this.ExecuteButton.Size = new System.Drawing.Size(100, 76);
            this.ExecuteButton.TabIndex = 14;
            this.ExecuteButton.Text = "Execute";
            this.ExecuteButton.UseVisualStyleBackColor = true;
            // 
            // ListViewController
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.RemoveButton);
            this.Controls.Add(this.EditButton);
            this.Controls.Add(this.RefreshButton);
            this.Controls.Add(this.NewButton);
            this.Controls.Add(this.List);
            this.Controls.Add(this.ExecuteButton);
            this.Font = new System.Drawing.Font("Microsoft JhengHei", 9F);
            this.Name = "ListViewController";
            this.Size = new System.Drawing.Size(891, 300);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Button RemoveButton;
        public System.Windows.Forms.Button EditButton;
        public System.Windows.Forms.Button RefreshButton;
        public System.Windows.Forms.Button NewButton;
        public System.Windows.Forms.ListView List;
        public System.Windows.Forms.Button ExecuteButton;
    }
}
