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
            this.components = new System.ComponentModel.Container();
            this.SNPList = new System.Windows.Forms.ListView();
            this.Empty = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SnippetName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.CreatedDate = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.LastUsedDate = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Remark = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SnippetGUID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SNPOpen = new System.Windows.Forms.Button();
            this.SNPNew = new System.Windows.Forms.Button();
            this.SNPRefresh = new System.Windows.Forms.Button();
            this.SNPDelete = new System.Windows.Forms.Button();
            this.SNPEdit = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.DSRemove = new System.Windows.Forms.Button();
            this.DSEdit = new System.Windows.Forms.Button();
            this.DSListRefresh = new System.Windows.Forms.Button();
            this.DSNew = new System.Windows.Forms.Button();
            this.DSList = new System.Windows.Forms.ListView();
            this.Empty0 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.DataSetName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.DataTableList = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.DataSetLoaded = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.DataSetGUID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.DSLoad = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.DSListMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.DSUnload = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // SNPList
            // 
            this.SNPList.Activation = System.Windows.Forms.ItemActivation.TwoClick;
            this.SNPList.AllowColumnReorder = true;
            this.SNPList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Empty,
            this.SnippetName,
            this.CreatedDate,
            this.LastUsedDate,
            this.Remark,
            this.SnippetGUID});
            this.SNPList.FullRowSelect = true;
            this.SNPList.GridLines = true;
            this.SNPList.HideSelection = false;
            this.SNPList.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.SNPList.Location = new System.Drawing.Point(6, 48);
            this.SNPList.MultiSelect = false;
            this.SNPList.Name = "SNPList";
            this.SNPList.ShowItemToolTips = true;
            this.SNPList.Size = new System.Drawing.Size(885, 286);
            this.SNPList.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.SNPList.TabIndex = 0;
            this.SNPList.UseCompatibleStateImageBehavior = false;
            this.SNPList.View = System.Windows.Forms.View.Details;
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
            // Remark
            // 
            this.Remark.Text = "Remark";
            this.Remark.Width = 150;
            // 
            // SnippetGUID
            // 
            this.SnippetGUID.Text = "Snippet GUID";
            this.SnippetGUID.Width = 270;
            // 
            // SNPOpen
            // 
            this.SNPOpen.Enabled = false;
            this.SNPOpen.Location = new System.Drawing.Point(806, 340);
            this.SNPOpen.Name = "SNPOpen";
            this.SNPOpen.Size = new System.Drawing.Size(85, 28);
            this.SNPOpen.TabIndex = 1;
            this.SNPOpen.Text = "Open";
            this.SNPOpen.UseVisualStyleBackColor = true;
            // 
            // SNPNew
            // 
            this.SNPNew.Location = new System.Drawing.Point(6, 340);
            this.SNPNew.Name = "SNPNew";
            this.SNPNew.Size = new System.Drawing.Size(100, 28);
            this.SNPNew.TabIndex = 2;
            this.SNPNew.Text = "New Snippet";
            this.SNPNew.UseVisualStyleBackColor = true;
            // 
            // SNPRefresh
            // 
            this.SNPRefresh.Font = new System.Drawing.Font("Microsoft JhengHei", 8.25F);
            this.SNPRefresh.Location = new System.Drawing.Point(837, 22);
            this.SNPRefresh.Name = "SNPRefresh";
            this.SNPRefresh.Size = new System.Drawing.Size(54, 20);
            this.SNPRefresh.TabIndex = 3;
            this.SNPRefresh.Text = "Refresh";
            this.SNPRefresh.UseVisualStyleBackColor = true;
            // 
            // SNPDelete
            // 
            this.SNPDelete.Enabled = false;
            this.SNPDelete.Location = new System.Drawing.Point(624, 340);
            this.SNPDelete.Name = "SNPDelete";
            this.SNPDelete.Size = new System.Drawing.Size(85, 28);
            this.SNPDelete.TabIndex = 4;
            this.SNPDelete.Text = "Delete";
            this.SNPDelete.UseVisualStyleBackColor = true;
            // 
            // SNPEdit
            // 
            this.SNPEdit.Enabled = false;
            this.SNPEdit.Location = new System.Drawing.Point(715, 340);
            this.SNPEdit.Name = "SNPEdit";
            this.SNPEdit.Size = new System.Drawing.Size(85, 28);
            this.SNPEdit.TabIndex = 5;
            this.SNPEdit.Text = "Edit";
            this.SNPEdit.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.DSUnload);
            this.groupBox1.Controls.Add(this.DSRemove);
            this.groupBox1.Controls.Add(this.DSEdit);
            this.groupBox1.Controls.Add(this.DSListRefresh);
            this.groupBox1.Controls.Add(this.DSNew);
            this.groupBox1.Controls.Add(this.DSList);
            this.groupBox1.Controls.Add(this.DSLoad);
            this.groupBox1.Location = new System.Drawing.Point(13, 392);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(897, 321);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Snippet Auto Source";
            // 
            // DSRemove
            // 
            this.DSRemove.Enabled = false;
            this.DSRemove.Location = new System.Drawing.Point(533, 288);
            this.DSRemove.Name = "DSRemove";
            this.DSRemove.Size = new System.Drawing.Size(85, 28);
            this.DSRemove.TabIndex = 6;
            this.DSRemove.Text = "Remove";
            this.DSRemove.UseVisualStyleBackColor = true;
            // 
            // DSEdit
            // 
            this.DSEdit.Enabled = false;
            this.DSEdit.Location = new System.Drawing.Point(624, 288);
            this.DSEdit.Name = "DSEdit";
            this.DSEdit.Size = new System.Drawing.Size(85, 28);
            this.DSEdit.TabIndex = 10;
            this.DSEdit.Text = "Edit";
            this.DSEdit.UseVisualStyleBackColor = true;
            // 
            // DSListRefresh
            // 
            this.DSListRefresh.Font = new System.Drawing.Font("Microsoft JhengHei", 8.25F);
            this.DSListRefresh.Location = new System.Drawing.Point(837, 22);
            this.DSListRefresh.Name = "DSListRefresh";
            this.DSListRefresh.Size = new System.Drawing.Size(54, 20);
            this.DSListRefresh.TabIndex = 6;
            this.DSListRefresh.Text = "Refresh";
            this.DSListRefresh.UseVisualStyleBackColor = true;
            // 
            // DSNew
            // 
            this.DSNew.Location = new System.Drawing.Point(6, 288);
            this.DSNew.Name = "DSNew";
            this.DSNew.Size = new System.Drawing.Size(100, 28);
            this.DSNew.TabIndex = 9;
            this.DSNew.Text = "New DataSet";
            this.DSNew.UseVisualStyleBackColor = true;
            // 
            // DSList
            // 
            this.DSList.Activation = System.Windows.Forms.ItemActivation.TwoClick;
            this.DSList.AllowColumnReorder = true;
            this.DSList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Empty0,
            this.DataSetName,
            this.DataTableList,
            this.DataSetLoaded,
            this.DataSetGUID});
            this.DSList.FullRowSelect = true;
            this.DSList.GridLines = true;
            this.DSList.HideSelection = false;
            this.DSList.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.DSList.Location = new System.Drawing.Point(6, 48);
            this.DSList.Name = "DSList";
            this.DSList.ShowItemToolTips = true;
            this.DSList.Size = new System.Drawing.Size(885, 234);
            this.DSList.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.DSList.TabIndex = 7;
            this.DSList.UseCompatibleStateImageBehavior = false;
            this.DSList.View = System.Windows.Forms.View.Details;
            // 
            // Empty0
            // 
            this.Empty0.Text = "Empty";
            this.Empty0.Width = 0;
            // 
            // DataSetName
            // 
            this.DataSetName.Text = "DataSet Name";
            this.DataSetName.Width = 145;
            // 
            // DataTableList
            // 
            this.DataTableList.Text = "DataTable List";
            this.DataTableList.Width = 325;
            // 
            // DataSetLoaded
            // 
            this.DataSetLoaded.Text = "DataSet Loaded";
            this.DataSetLoaded.Width = 105;
            // 
            // DataSetGUID
            // 
            this.DataSetGUID.Text = "DataSet GUID";
            this.DataSetGUID.Width = 270;
            // 
            // DSLoad
            // 
            this.DSLoad.Enabled = false;
            this.DSLoad.Location = new System.Drawing.Point(806, 288);
            this.DSLoad.Name = "DSLoad";
            this.DSLoad.Size = new System.Drawing.Size(85, 28);
            this.DSLoad.TabIndex = 7;
            this.DSLoad.Text = "Load";
            this.DSLoad.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.SNPRefresh);
            this.groupBox2.Controls.Add(this.SNPList);
            this.groupBox2.Controls.Add(this.SNPEdit);
            this.groupBox2.Controls.Add(this.SNPOpen);
            this.groupBox2.Controls.Add(this.SNPDelete);
            this.groupBox2.Controls.Add(this.SNPNew);
            this.groupBox2.Location = new System.Drawing.Point(13, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(897, 374);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Snippets";
            // 
            // DSListMenu
            // 
            this.DSListMenu.Name = "DSListMenu";
            this.DSListMenu.Size = new System.Drawing.Size(61, 4);
            // 
            // DSUnload
            // 
            this.DSUnload.Enabled = false;
            this.DSUnload.Location = new System.Drawing.Point(715, 288);
            this.DSUnload.Name = "DSUnload";
            this.DSUnload.Size = new System.Drawing.Size(85, 28);
            this.DSUnload.TabIndex = 11;
            this.DSUnload.Text = "Unload";
            this.DSUnload.UseVisualStyleBackColor = true;
            // 
            // Home
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(922, 722);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Microsoft JhengHei", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.Name = "Home";
            this.Text = "Home";
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView SNPList;
        private System.Windows.Forms.ColumnHeader SnippetName;
        private System.Windows.Forms.ColumnHeader CreatedDate;
        private System.Windows.Forms.ColumnHeader LastUsedDate;
        private System.Windows.Forms.ColumnHeader Remark;
        private System.Windows.Forms.Button SNPOpen;
        private System.Windows.Forms.Button SNPNew;
        private System.Windows.Forms.ColumnHeader Empty;
        private System.Windows.Forms.Button SNPRefresh;
        private System.Windows.Forms.Button SNPDelete;
        private System.Windows.Forms.Button SNPEdit;
        private System.Windows.Forms.ColumnHeader SnippetGUID;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button DSNew;
        private System.Windows.Forms.ListView DSList;
        private System.Windows.Forms.ColumnHeader DataSetName;
        private System.Windows.Forms.ColumnHeader DataTableList;
        private System.Windows.Forms.Button DSLoad;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button DSListRefresh;
        private System.Windows.Forms.ColumnHeader DataSetLoaded;
        private System.Windows.Forms.Button DSEdit;
        private System.Windows.Forms.Button DSRemove;
        private System.Windows.Forms.ColumnHeader DataSetGUID;
        private System.Windows.Forms.ColumnHeader Empty0;
        private System.Windows.Forms.ContextMenuStrip DSListMenu;
        private System.Windows.Forms.Button DSUnload;
    }
}

