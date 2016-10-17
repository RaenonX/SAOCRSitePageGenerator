namespace SAOCRSitePageGenerator
{
    partial class StringProcessTester
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
            this.Input = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.CMD = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.Output = new System.Windows.Forms.TextBox();
            this.Execute = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Input
            // 
            this.Input.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.Input.Location = new System.Drawing.Point(68, 12);
            this.Input.Name = "Input";
            this.Input.Size = new System.Drawing.Size(251, 23);
            this.Input.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(12, 12);
            this.label1.Margin = new System.Windows.Forms.Padding(3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 23);
            this.label1.TabIndex = 1;
            this.label1.Text = "Input";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // CMD
            // 
            this.CMD.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.CMD.Location = new System.Drawing.Point(12, 41);
            this.CMD.Multiline = true;
            this.CMD.Name = "CMD";
            this.CMD.Size = new System.Drawing.Size(307, 210);
            this.CMD.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(12, 287);
            this.label2.Margin = new System.Windows.Forms.Padding(3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 23);
            this.label2.TabIndex = 4;
            this.label2.Text = "Output";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Output
            // 
            this.Output.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.Output.Location = new System.Drawing.Point(68, 287);
            this.Output.Name = "Output";
            this.Output.ReadOnly = true;
            this.Output.Size = new System.Drawing.Size(251, 23);
            this.Output.TabIndex = 3;
            // 
            // Execute
            // 
            this.Execute.Location = new System.Drawing.Point(242, 257);
            this.Execute.Name = "Execute";
            this.Execute.Size = new System.Drawing.Size(76, 24);
            this.Execute.TabIndex = 5;
            this.Execute.Text = "Execute";
            this.Execute.UseVisualStyleBackColor = true;
            // 
            // StringProcessTester
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(331, 322);
            this.Controls.Add(this.Execute);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.Output);
            this.Controls.Add(this.CMD);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Input);
            this.Font = new System.Drawing.Font("Microsoft JhengHei", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "StringProcessTester";
            this.Text = "String Process Tester";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox Input;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox CMD;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox Output;
        private System.Windows.Forms.Button Execute;
    }
}