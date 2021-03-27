namespace mqttservice
{
    partial class Form1
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
            this.lstNotifier = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // lstNotifier
            // 
            this.lstNotifier.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.lstNotifier.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstNotifier.GridLines = true;
            this.lstNotifier.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lstNotifier.HideSelection = false;
            this.lstNotifier.Location = new System.Drawing.Point(0, 0);
            this.lstNotifier.MultiSelect = false;
            this.lstNotifier.Name = "lstNotifier";
            this.lstNotifier.Size = new System.Drawing.Size(544, 335);
            this.lstNotifier.TabIndex = 0;
            this.lstNotifier.UseCompatibleStateImageBehavior = false;
            this.lstNotifier.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Details";
            this.columnHeader1.Width = 540;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(544, 335);
            this.Controls.Add(this.lstNotifier);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lstNotifier;
        private System.Windows.Forms.ColumnHeader columnHeader1;
    }
}

