namespace SAOCRSitePageGenerator
{
    partial class SnippetGenerator
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
            this.FieldPanel = new System.Windows.Forms.Panel();
            this.Generate = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // FieldPanel
            // 
            this.FieldPanel.AutoScroll = true;
            this.FieldPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.FieldPanel.Location = new System.Drawing.Point(13, 13);
            this.FieldPanel.Name = "FieldPanel";
            this.FieldPanel.Size = new System.Drawing.Size(1224, 662);
            this.FieldPanel.TabIndex = 0;
            // 
            // Generate
            // 
            this.Generate.Location = new System.Drawing.Point(1152, 681);
            this.Generate.Name = "Generate";
            this.Generate.Size = new System.Drawing.Size(85, 30);
            this.Generate.TabIndex = 2;
            this.Generate.Text = "Generate";
            this.Generate.UseVisualStyleBackColor = true;
            // 
            // SnippetGenerator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1250, 723);
            this.Controls.Add(this.Generate);
            this.Controls.Add(this.FieldPanel);
            this.Font = new System.Drawing.Font("Microsoft JhengHei", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.Name = "SnippetGenerator";
            this.Text = "Snippet Generator";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel FieldPanel;
        private System.Windows.Forms.Button Generate;
    }
}