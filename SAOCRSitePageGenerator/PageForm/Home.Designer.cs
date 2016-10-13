namespace SAOCRSitePageGenerator
{
    partial class Home
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
            this.SnippetList = new System.Windows.Forms.ListView();
            this.Empty = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SnippetName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.CreatedDate = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.LastUsedDate = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Notes = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.FolderKey = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Open = new System.Windows.Forms.Button();
            this.New = new System.Windows.Forms.Button();
            this.RefreshList = new System.Windows.Forms.Button();
            this.Delete = new System.Windows.Forms.Button();
            this.Edit = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.LoadDT = new System.Windows.Forms.Button();
            this.SelectDT = new System.Windows.Forms.Button();
            this.DatatableList = new System.Windows.Forms.ListView();
            this.DataSet = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.DataSource = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.NewDT = new System.Windows.Forms.Button();
            this.Empty0 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // SnippetList
            // 
            this.SnippetList.Activation = System.Windows.Forms.ItemActivation.TwoClick;
            this.SnippetList.AllowColumnReorder = true;
            this.SnippetList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Empty,
            this.SnippetName,
            this.CreatedDate,
            this.LastUsedDate,
            this.Notes,
            this.FolderKey});
            this.SnippetList.FullRowSelect = true;
            this.SnippetList.GridLines = true;
            this.SnippetList.HideSelection = false;
            this.SnippetList.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.SnippetList.Location = new System.Drawing.Point(12, 38);
            this.SnippetList.MultiSelect = false;
            this.SnippetList.Name = "SnippetList";
            this.SnippetList.ShowItemToolTips = true;
            this.SnippetList.Size = new System.Drawing.Size(898, 520);
            this.SnippetList.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.SnippetList.TabIndex = 0;
            this.SnippetList.UseCompatibleStateImageBehavior = false;
            this.SnippetList.View = System.Windows.Forms.View.Details;
            // 
            // Empty
            // 
            this.Empty.Text = "Empty";
            this.Empty.Width = 0;
            // 
            // SnippetName
            // 
            this.SnippetName.Text = "Snippet Name";
            this.SnippetName.Width = 150;
            // 
            // CreatedDate
            // 
            this.CreatedDate.Text = "Created Date";
            this.CreatedDate.Width = 135;
            // 
            // LastUsedDate
            // 
            this.LastUsedDate.Text = "Last Used";
            this.LastUsedDate.Width = 135;
            // 
            // Notes
            // 
            this.Notes.Text = "Notes";
            this.Notes.Width = 150;
            // 
            // FolderKey
            // 
            this.FolderKey.Text = "Folder Key";
            this.FolderKey.Width = 270;
            // 
            // Open
            // 
            this.Open.Enabled = false;
            this.Open.Location = new System.Drawing.Point(825, 564);
            this.Open.Name = "Open";
            this.Open.Size = new System.Drawing.Size(85, 28);
            this.Open.TabIndex = 1;
            this.Open.Text = "Open";
            this.Open.UseVisualStyleBackColor = true;
            // 
            // New
            // 
            this.New.Location = new System.Drawing.Point(12, 564);
            this.New.Name = "New";
            this.New.Size = new System.Drawing.Size(100, 28);
            this.New.TabIndex = 2;
            this.New.Text = "New Snippet";
            this.New.UseVisualStyleBackColor = true;
            // 
            // RefreshList
            // 
            this.RefreshList.Font = new System.Drawing.Font("Microsoft JhengHei", 8.25F);
            this.RefreshList.Location = new System.Drawing.Point(856, 12);
            this.RefreshList.Name = "RefreshList";
            this.RefreshList.Size = new System.Drawing.Size(54, 20);
            this.RefreshList.TabIndex = 3;
            this.RefreshList.Text = "Refresh";
            this.RefreshList.UseVisualStyleBackColor = true;
            // 
            // Delete
            // 
            this.Delete.Enabled = false;
            this.Delete.Location = new System.Drawing.Point(643, 564);
            this.Delete.Name = "Delete";
            this.Delete.Size = new System.Drawing.Size(85, 28);
            this.Delete.TabIndex = 4;
            this.Delete.Text = "Delete";
            this.Delete.UseVisualStyleBackColor = true;
            // 
            // Edit
            // 
            this.Edit.Enabled = false;
            this.Edit.Location = new System.Drawing.Point(734, 564);
            this.Edit.Name = "Edit";
            this.Edit.Size = new System.Drawing.Size(85, 28);
            this.Edit.TabIndex = 5;
            this.Edit.Text = "Edit";
            this.Edit.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.NewDT);
            this.groupBox1.Controls.Add(this.DatatableList);
            this.groupBox1.Controls.Add(this.SelectDT);
            this.groupBox1.Controls.Add(this.LoadDT);
            this.groupBox1.Location = new System.Drawing.Point(13, 599);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(897, 175);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Source for auto insert";
            // 
            // LoadDT
            // 
            this.LoadDT.Enabled = false;
            this.LoadDT.Location = new System.Drawing.Point(806, 136);
            this.LoadDT.Name = "LoadDT";
            this.LoadDT.Size = new System.Drawing.Size(85, 33);
            this.LoadDT.TabIndex = 7;
            this.LoadDT.Text = "Load";
            this.LoadDT.UseVisualStyleBackColor = true;
            // 
            // SelectDT
            // 
            this.SelectDT.Location = new System.Drawing.Point(806, 61);
            this.SelectDT.Name = "SelectDT";
            this.SelectDT.Size = new System.Drawing.Size(85, 33);
            this.SelectDT.TabIndex = 8;
            this.SelectDT.Text = "Select";
            this.SelectDT.UseVisualStyleBackColor = true;
            // 
            // DatatableList
            // 
            this.DatatableList.Activation = System.Windows.Forms.ItemActivation.TwoClick;
            this.DatatableList.AllowColumnReorder = true;
            this.DatatableList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Empty0,
            this.DataSet,
            this.DataSource});
            this.DatatableList.FullRowSelect = true;
            this.DatatableList.GridLines = true;
            this.DatatableList.HideSelection = false;
            this.DatatableList.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.DatatableList.Location = new System.Drawing.Point(6, 22);
            this.DatatableList.MultiSelect = false;
            this.DatatableList.Name = "DatatableList";
            this.DatatableList.ShowItemToolTips = true;
            this.DatatableList.Size = new System.Drawing.Size(794, 147);
            this.DatatableList.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.DatatableList.TabIndex = 7;
            this.DatatableList.UseCompatibleStateImageBehavior = false;
            this.DatatableList.View = System.Windows.Forms.View.Details;
            // 
            // DataSet
            // 
            this.DataSet.Text = "Data Set";
            this.DataSet.Width = 250;
            // 
            // DataSource
            // 
            this.DataSource.Text = "DataSource";
            this.DataSource.Width = 250;
            // 
            // NewDT
            // 
            this.NewDT.Location = new System.Drawing.Point(806, 22);
            this.NewDT.Name = "NewDT";
            this.NewDT.Size = new System.Drawing.Size(85, 33);
            this.NewDT.TabIndex = 9;
            this.NewDT.Text = "New";
            this.NewDT.UseVisualStyleBackColor = true;
            // 
            // Empty0
            // 
            this.Empty0.Text = "Empty";
            this.Empty0.Width = 0;
            // 
            // Home
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(922, 786);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.Edit);
            this.Controls.Add(this.Delete);
            this.Controls.Add(this.RefreshList);
            this.Controls.Add(this.New);
            this.Controls.Add(this.Open);
            this.Controls.Add(this.SnippetList);
            this.Font = new System.Drawing.Font("Microsoft JhengHei", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.Name = "Home";
            this.Text = "Home";
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView SnippetList;
        private System.Windows.Forms.ColumnHeader SnippetName;
        private System.Windows.Forms.ColumnHeader CreatedDate;
        private System.Windows.Forms.ColumnHeader LastUsedDate;
        private System.Windows.Forms.ColumnHeader Notes;
        private System.Windows.Forms.Button Open;
        private System.Windows.Forms.Button New;
        private System.Windows.Forms.ColumnHeader Empty;
        private System.Windows.Forms.Button RefreshList;
        private System.Windows.Forms.Button Delete;
        private System.Windows.Forms.Button Edit;
        private System.Windows.Forms.ColumnHeader FolderKey;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button NewDT;
        private System.Windows.Forms.ListView DatatableList;
        private System.Windows.Forms.ColumnHeader DataSet;
        private System.Windows.Forms.ColumnHeader DataSource;
        private System.Windows.Forms.Button SelectDT;
        private System.Windows.Forms.Button LoadDT;
        private System.Windows.Forms.ColumnHeader Empty0;
    }
}

