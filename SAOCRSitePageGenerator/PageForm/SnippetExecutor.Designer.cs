namespace SAOCRSitePageGenerator
{
    partial class SnippetExecutor
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
            this.Generate = new System.Windows.Forms.Button();
            this.Panel = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.OutputDestination = new System.Windows.Forms.Label();
            this.ProgressText = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // Generate
            // 
            this.Generate.Location = new System.Drawing.Point(1174, 681);
            this.Generate.Name = "Generate";
            this.Generate.Size = new System.Drawing.Size(85, 24);
            this.Generate.TabIndex = 2;
            this.Generate.Text = "Generate";
            this.Generate.UseVisualStyleBackColor = true;
            // 
            // Panel
            // 
            this.Panel.Location = new System.Drawing.Point(12, 12);
            this.Panel.Name = "Panel";
            this.Panel.Size = new System.Drawing.Size(1247, 663);
            this.Panel.TabIndex = 3;
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(12, 681);
            this.label7.Margin = new System.Windows.Forms.Padding(3);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(122, 24);
            this.label7.TabIndex = 17;
            this.label7.Text = "Output Destination";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // OutputDestination
            // 
            this.OutputDestination.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.OutputDestination.Location = new System.Drawing.Point(140, 681);
            this.OutputDestination.Margin = new System.Windows.Forms.Padding(3);
            this.OutputDestination.Name = "OutputDestination";
            this.OutputDestination.Size = new System.Drawing.Size(335, 24);
            this.OutputDestination.TabIndex = 18;
            this.OutputDestination.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ProgressText
            // 
            this.ProgressText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ProgressText.Location = new System.Drawing.Point(882, 681);
            this.ProgressText.Margin = new System.Windows.Forms.Padding(3);
            this.ProgressText.Name = "ProgressText";
            this.ProgressText.Size = new System.Drawing.Size(286, 24);
            this.ProgressText.TabIndex = 19;
            this.ProgressText.Text = "Standby.";
            this.ProgressText.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(825, 681);
            this.label2.Margin = new System.Windows.Forms.Padding(3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 24);
            this.label2.TabIndex = 20;
            this.label2.Text = "Process";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // SnippetExecutor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1272, 713);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.ProgressText);
            this.Controls.Add(this.OutputDestination);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.Panel);
            this.Controls.Add(this.Generate);
            this.Font = new System.Drawing.Font("Microsoft JhengHei", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.Name = "SnippetExecutor";
            this.Text = "Snippet Generator";
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button Generate;
        private System.Windows.Forms.Panel Panel;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label OutputDestination;
        private System.Windows.Forms.Label ProgressText;
        private System.Windows.Forms.Label label2;
    }
}