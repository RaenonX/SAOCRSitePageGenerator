namespace SAOCRSitePageGenerator
{
    partial class DataSetEditor
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
            this.DTView = new System.Windows.Forms.DataGridView();
            this.Save = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.DSName = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.DCManager = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.DCExpr = new System.Windows.Forms.TextBox();
            this.DCRemove = new System.Windows.Forms.Button();
            this.DCAdd = new System.Windows.Forms.Button();
            this.DCPrimaryKey = new System.Windows.Forms.CheckBox();
            this.DCAllowNull = new System.Windows.Forms.CheckBox();
            this.DCUnique = new System.Windows.Forms.CheckBox();
            this.DCType = new System.Windows.Forms.DomainUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.DCName = new System.Windows.Forms.TextBox();
            this.DSMemo = new System.Windows.Forms.TextBox();
            this.DTList = new System.Windows.Forms.ListView();
            this.Empty = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.DTName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.DTRemark = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.DTColumnCount = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.DTRowCount = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.DTGuid = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.DTRemove = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.DTNameToAdd = new System.Windows.Forms.TextBox();
            this.DTCurrent = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.DTNew = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.DTRemarkToAdd = new System.Windows.Forms.TextBox();
            this.DTLoad = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.DTView)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.DCManager.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // DataTableView
            // 
            this.DTView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DTView.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.DTView.Location = new System.Drawing.Point(334, 245);
            this.DTView.Name = "DataTableView";
            this.DTView.Size = new System.Drawing.Size(863, 412);
            this.DTView.TabIndex = 0;
            // 
            // Save
            // 
            this.Save.Font = new System.Drawing.Font("Microsoft JhengHei", 11F);
            this.Save.Location = new System.Drawing.Point(1108, 663);
            this.Save.Name = "Save";
            this.Save.Size = new System.Drawing.Size(89, 35);
            this.Save.TabIndex = 21;
            this.Save.Text = "Save";
            this.Save.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(6, 22);
            this.label2.Margin = new System.Windows.Forms.Padding(3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(98, 23);
            this.label2.TabIndex = 25;
            this.label2.Text = "Data Set Name";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // DSName
            // 
            this.DSName.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.DSName.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.DSName.Location = new System.Drawing.Point(110, 22);
            this.DSName.Name = "DSName";
            this.DSName.Size = new System.Drawing.Size(200, 23);
            this.DSName.TabIndex = 24;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.DSName);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(316, 50);
            this.groupBox1.TabIndex = 28;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "DataSet Basic Information";
            // 
            // DCManager
            // 
            this.DCManager.Controls.Add(this.label5);
            this.DCManager.Controls.Add(this.DCExpr);
            this.DCManager.Controls.Add(this.DCRemove);
            this.DCManager.Controls.Add(this.DCAdd);
            this.DCManager.Controls.Add(this.DCPrimaryKey);
            this.DCManager.Controls.Add(this.DCAllowNull);
            this.DCManager.Controls.Add(this.DCUnique);
            this.DCManager.Controls.Add(this.DCType);
            this.DCManager.Controls.Add(this.label4);
            this.DCManager.Controls.Add(this.label3);
            this.DCManager.Controls.Add(this.DCName);
            this.DCManager.Location = new System.Drawing.Point(334, 186);
            this.DCManager.Name = "DCManager";
            this.DCManager.Size = new System.Drawing.Size(863, 53);
            this.DCManager.TabIndex = 29;
            this.DCManager.TabStop = false;
            this.DCManager.Text = "DataColumn Manager";
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(591, 22);
            this.label5.Margin = new System.Windows.Forms.Padding(3);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(33, 23);
            this.label5.TabIndex = 33;
            this.label5.Text = "Expr";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // DCExpr
            // 
            this.DCExpr.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.DCExpr.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.DCExpr.Location = new System.Drawing.Point(630, 22);
            this.DCExpr.Name = "DCExpr";
            this.DCExpr.Size = new System.Drawing.Size(109, 23);
            this.DCExpr.TabIndex = 32;
            // 
            // DCRemove
            // 
            this.DCRemove.Enabled = false;
            this.DCRemove.Font = new System.Drawing.Font("Microsoft JhengHei", 9F);
            this.DCRemove.Location = new System.Drawing.Point(794, 22);
            this.DCRemove.Name = "DCRemove";
            this.DCRemove.Size = new System.Drawing.Size(63, 23);
            this.DCRemove.TabIndex = 31;
            this.DCRemove.Text = "Remove";
            this.DCRemove.UseVisualStyleBackColor = true;
            // 
            // DCAdd
            // 
            this.DCAdd.Font = new System.Drawing.Font("Microsoft JhengHei", 9F);
            this.DCAdd.Location = new System.Drawing.Point(745, 22);
            this.DCAdd.Name = "DCAdd";
            this.DCAdd.Size = new System.Drawing.Size(43, 23);
            this.DCAdd.TabIndex = 30;
            this.DCAdd.Text = "Add";
            this.DCAdd.UseVisualStyleBackColor = true;
            // 
            // DCPrimaryKey
            // 
            this.DCPrimaryKey.Location = new System.Drawing.Point(334, 22);
            this.DCPrimaryKey.Name = "DCPrimaryKey";
            this.DCPrimaryKey.Size = new System.Drawing.Size(90, 23);
            this.DCPrimaryKey.TabIndex = 28;
            this.DCPrimaryKey.Text = "PrimaryKey";
            this.DCPrimaryKey.UseVisualStyleBackColor = true;
            // 
            // DCAllowNull
            // 
            this.DCAllowNull.Location = new System.Drawing.Point(430, 22);
            this.DCAllowNull.Name = "DCAllowNull";
            this.DCAllowNull.Size = new System.Drawing.Size(81, 23);
            this.DCAllowNull.TabIndex = 27;
            this.DCAllowNull.Text = "AllowNull";
            this.DCAllowNull.UseVisualStyleBackColor = true;
            // 
            // DCUnique
            // 
            this.DCUnique.Location = new System.Drawing.Point(517, 22);
            this.DCUnique.Name = "DCUnique";
            this.DCUnique.Size = new System.Drawing.Size(68, 23);
            this.DCUnique.TabIndex = 26;
            this.DCUnique.Text = "Unique";
            this.DCUnique.UseVisualStyleBackColor = true;
            // 
            // DCType
            // 
            this.DCType.Items.Add("System.String");
            this.DCType.Items.Add("System.Int32");
            this.DCType.Items.Add("System.Boolean");
            this.DCType.Location = new System.Drawing.Point(216, 22);
            this.DCType.Name = "DCType";
            this.DCType.ReadOnly = true;
            this.DCType.Size = new System.Drawing.Size(112, 23);
            this.DCType.TabIndex = 25;
            this.DCType.Text = "System.String";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(165, 22);
            this.label4.Margin = new System.Windows.Forms.Padding(3);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(45, 23);
            this.label4.TabIndex = 24;
            this.label4.Text = "Type";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(6, 22);
            this.label3.Margin = new System.Windows.Forms.Padding(3);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(45, 23);
            this.label3.TabIndex = 23;
            this.label3.Text = "Name";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // DCName
            // 
            this.DCName.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.DCName.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.DCName.Location = new System.Drawing.Point(57, 22);
            this.DCName.Name = "DCName";
            this.DCName.Size = new System.Drawing.Size(102, 23);
            this.DCName.TabIndex = 22;
            // 
            // DataTableMemo
            // 
            this.DSMemo.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.DSMemo.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.HistoryList;
            this.DSMemo.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.DSMemo.Location = new System.Drawing.Point(12, 89);
            this.DSMemo.Multiline = true;
            this.DSMemo.Name = "DataTableMemo";
            this.DSMemo.Size = new System.Drawing.Size(316, 568);
            this.DSMemo.TabIndex = 32;
            // 
            // DTList
            // 
            this.DTList.Activation = System.Windows.Forms.ItemActivation.TwoClick;
            this.DTList.AllowColumnReorder = true;
            this.DTList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Empty,
            this.DTName,
            this.DTRemark,
            this.DTColumnCount,
            this.DTRowCount,
            this.DTGuid});
            this.DTList.FullRowSelect = true;
            this.DTList.GridLines = true;
            this.DTList.HideSelection = false;
            this.DTList.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.DTList.Location = new System.Drawing.Point(6, 22);
            this.DTList.MultiSelect = false;
            this.DTList.Name = "DTList";
            this.DTList.ShowItemToolTips = true;
            this.DTList.Size = new System.Drawing.Size(782, 111);
            this.DTList.TabIndex = 33;
            this.DTList.UseCompatibleStateImageBehavior = false;
            this.DTList.View = System.Windows.Forms.View.Details;
            // 
            // Empty
            // 
            this.Empty.Text = "Empty";
            this.Empty.Width = 0;
            // 
            // DTName
            // 
            this.DTName.Text = "DataTable Name";
            this.DTName.Width = 130;
            // 
            // DTRemark
            // 
            this.DTRemark.Text = "DataTable Remark";
            this.DTRemark.Width = 165;
            // 
            // DTColumnCount
            // 
            this.DTColumnCount.Text = "Column Count";
            this.DTColumnCount.Width = 93;
            // 
            // DTRowCount
            // 
            this.DTRowCount.Text = "Row Count";
            this.DTRowCount.Width = 93;
            // 
            // DTGuid
            // 
            this.DTGuid.Text = "DataTable GUID";
            this.DTGuid.Width = 270;
            // 
            // DTRemove
            // 
            this.DTRemove.Enabled = false;
            this.DTRemove.Font = new System.Drawing.Font("Microsoft JhengHei", 9F);
            this.DTRemove.Location = new System.Drawing.Point(794, 56);
            this.DTRemove.Name = "DTRemove";
            this.DTRemove.Size = new System.Drawing.Size(63, 28);
            this.DTRemove.TabIndex = 34;
            this.DTRemove.Text = "Remove";
            this.DTRemove.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.DTNameToAdd);
            this.groupBox3.Controls.Add(this.DTCurrent);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.DTNew);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.DTRemarkToAdd);
            this.groupBox3.Controls.Add(this.DTLoad);
            this.groupBox3.Controls.Add(this.DTList);
            this.groupBox3.Controls.Add(this.DTRemove);
            this.groupBox3.Location = new System.Drawing.Point(334, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(863, 168);
            this.groupBox3.TabIndex = 35;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "DataTable Manager";
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(6, 139);
            this.label8.Margin = new System.Windows.Forms.Padding(3);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(54, 23);
            this.label8.TabIndex = 39;
            this.label8.Text = "Name";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // DTNameToAdd
            // 
            this.DTNameToAdd.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.DTNameToAdd.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.DTNameToAdd.Location = new System.Drawing.Point(66, 139);
            this.DTNameToAdd.Name = "DTNameToAdd";
            this.DTNameToAdd.Size = new System.Drawing.Size(224, 23);
            this.DTNameToAdd.TabIndex = 38;
            // 
            // DTCurrent
            // 
            this.DTCurrent.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.DTCurrent.Font = new System.Drawing.Font("Microsoft JhengHei", 9.5F);
            this.DTCurrent.Location = new System.Drawing.Point(671, 139);
            this.DTCurrent.Margin = new System.Windows.Forms.Padding(3);
            this.DTCurrent.Name = "DTCurrent";
            this.DTCurrent.Size = new System.Drawing.Size(186, 23);
            this.DTCurrent.TabIndex = 37;
            this.DTCurrent.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(564, 139);
            this.label7.Margin = new System.Windows.Forms.Padding(3);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(101, 23);
            this.label7.TabIndex = 34;
            this.label7.Text = "Current Loaded";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // DTNew
            // 
            this.DTNew.Font = new System.Drawing.Font("Microsoft JhengHei", 9F);
            this.DTNew.Location = new System.Drawing.Point(794, 22);
            this.DTNew.Name = "DTNew";
            this.DTNew.Size = new System.Drawing.Size(63, 28);
            this.DTNew.TabIndex = 36;
            this.DTNew.Text = "New";
            this.DTNew.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(296, 139);
            this.label1.Margin = new System.Windows.Forms.Padding(3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 23);
            this.label1.TabIndex = 29;
            this.label1.Text = "Remark";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // DTRemarkToAdd
            // 
            this.DTRemarkToAdd.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.DTRemarkToAdd.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.DTRemarkToAdd.Location = new System.Drawing.Point(356, 139);
            this.DTRemarkToAdd.Name = "DTRemarkToAdd";
            this.DTRemarkToAdd.Size = new System.Drawing.Size(202, 23);
            this.DTRemarkToAdd.TabIndex = 28;
            // 
            // DTLoad
            // 
            this.DTLoad.Enabled = false;
            this.DTLoad.Font = new System.Drawing.Font("Microsoft JhengHei", 9F);
            this.DTLoad.Location = new System.Drawing.Point(794, 90);
            this.DTLoad.Name = "DTLoad";
            this.DTLoad.Size = new System.Drawing.Size(63, 43);
            this.DTLoad.TabIndex = 35;
            this.DTLoad.Text = "Load";
            this.DTLoad.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(12, 68);
            this.label6.Margin = new System.Windows.Forms.Padding(3);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(316, 15);
            this.label6.TabIndex = 37;
            this.label6.Text = "DataSet Document";
            this.label6.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // DataSetEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1209, 710);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.DSMemo);
            this.Controls.Add(this.DCManager);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.Save);
            this.Controls.Add(this.DTView);
            this.Font = new System.Drawing.Font("Microsoft JhengHei", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.Name = "DataSetEditor";
            this.Text = "DataTable Editor";
            ((System.ComponentModel.ISupportInitialize)(this.DTView)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.DCManager.ResumeLayout(false);
            this.DCManager.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView DTView;
        private System.Windows.Forms.Button Save;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox DSName;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox DCManager;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox DCName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DomainUpDown DCType;
        private System.Windows.Forms.Button DCAdd;
        private System.Windows.Forms.CheckBox DCPrimaryKey;
        private System.Windows.Forms.CheckBox DCAllowNull;
        private System.Windows.Forms.CheckBox DCUnique;
        private System.Windows.Forms.Button DCRemove;
        private System.Windows.Forms.TextBox DSMemo;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox DCExpr;
        private System.Windows.Forms.ListView DTList;
        private System.Windows.Forms.ColumnHeader DTName;
        private System.Windows.Forms.ColumnHeader DTRemark;
        private System.Windows.Forms.ColumnHeader DTColumnCount;
        private System.Windows.Forms.ColumnHeader DTRowCount;
        private System.Windows.Forms.Button DTRemove;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button DTLoad;
        private System.Windows.Forms.Button DTNew;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox DTRemarkToAdd;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label DTCurrent;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox DTNameToAdd;
        private System.Windows.Forms.ColumnHeader DTGuid;
        private System.Windows.Forms.ColumnHeader Empty;
    }
}