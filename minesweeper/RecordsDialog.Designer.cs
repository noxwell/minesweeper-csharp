namespace minesweeper
{
    partial class RecordsDialog
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.newbieTimeLabel = new System.Windows.Forms.Label();
            this.advancedTimeLabel = new System.Windows.Forms.Label();
            this.profiTimeLabel = new System.Windows.Forms.Label();
            this.profiNameLabel = new System.Windows.Forms.Label();
            this.advancedNameLabel = new System.Windows.Forms.Label();
            this.newbieNameLabel = new System.Windows.Forms.Label();
            this.resetRecordsButton = new System.Windows.Forms.Button();
            this.closeButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Новичок:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Любитель:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 57);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(86, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Профессионал:";
            // 
            // newbieTimeLabel
            // 
            this.newbieTimeLabel.AutoSize = true;
            this.newbieTimeLabel.Location = new System.Drawing.Point(116, 13);
            this.newbieTimeLabel.Name = "newbieTimeLabel";
            this.newbieTimeLabel.Size = new System.Drawing.Size(49, 13);
            this.newbieTimeLabel.TabIndex = 3;
            this.newbieTimeLabel.Text = "999 сек.";
            // 
            // advancedTimeLabel
            // 
            this.advancedTimeLabel.AutoSize = true;
            this.advancedTimeLabel.Location = new System.Drawing.Point(116, 35);
            this.advancedTimeLabel.Name = "advancedTimeLabel";
            this.advancedTimeLabel.Size = new System.Drawing.Size(49, 13);
            this.advancedTimeLabel.TabIndex = 4;
            this.advancedTimeLabel.Text = "999 сек.";
            // 
            // profiTimeLabel
            // 
            this.profiTimeLabel.AutoSize = true;
            this.profiTimeLabel.Location = new System.Drawing.Point(116, 57);
            this.profiTimeLabel.Name = "profiTimeLabel";
            this.profiTimeLabel.Size = new System.Drawing.Size(49, 13);
            this.profiTimeLabel.TabIndex = 5;
            this.profiTimeLabel.Text = "999 сек.";
            // 
            // profiNameLabel
            // 
            this.profiNameLabel.AutoSize = true;
            this.profiNameLabel.Location = new System.Drawing.Point(187, 57);
            this.profiNameLabel.Name = "profiNameLabel";
            this.profiNameLabel.Size = new System.Drawing.Size(46, 13);
            this.profiNameLabel.TabIndex = 8;
            this.profiNameLabel.Text = "Аноним";
            // 
            // advancedNameLabel
            // 
            this.advancedNameLabel.AutoSize = true;
            this.advancedNameLabel.Location = new System.Drawing.Point(187, 35);
            this.advancedNameLabel.Name = "advancedNameLabel";
            this.advancedNameLabel.Size = new System.Drawing.Size(46, 13);
            this.advancedNameLabel.TabIndex = 7;
            this.advancedNameLabel.Text = "Аноним";
            // 
            // newbieNameLabel
            // 
            this.newbieNameLabel.AutoSize = true;
            this.newbieNameLabel.Location = new System.Drawing.Point(187, 13);
            this.newbieNameLabel.Name = "newbieNameLabel";
            this.newbieNameLabel.Size = new System.Drawing.Size(46, 13);
            this.newbieNameLabel.TabIndex = 6;
            this.newbieNameLabel.Text = "Аноним";
            // 
            // resetRecordsButton
            // 
            this.resetRecordsButton.Location = new System.Drawing.Point(34, 92);
            this.resetRecordsButton.Name = "resetRecordsButton";
            this.resetRecordsButton.Size = new System.Drawing.Size(119, 23);
            this.resetRecordsButton.TabIndex = 9;
            this.resetRecordsButton.Text = "Сброс результатов";
            this.resetRecordsButton.UseVisualStyleBackColor = true;
            this.resetRecordsButton.Click += new System.EventHandler(this.resetRecordsButton_Click);
            // 
            // closeButton
            // 
            this.closeButton.Location = new System.Drawing.Point(159, 92);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(75, 23);
            this.closeButton.TabIndex = 10;
            this.closeButton.Text = "OK";
            this.closeButton.UseVisualStyleBackColor = true;
            this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
            // 
            // RecordsDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 124);
            this.Controls.Add(this.closeButton);
            this.Controls.Add(this.resetRecordsButton);
            this.Controls.Add(this.profiNameLabel);
            this.Controls.Add(this.advancedNameLabel);
            this.Controls.Add(this.newbieNameLabel);
            this.Controls.Add(this.profiTimeLabel);
            this.Controls.Add(this.advancedTimeLabel);
            this.Controls.Add(this.newbieTimeLabel);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "RecordsDialog";
            this.Text = "Таблица рекордов";
            this.Load += new System.EventHandler(this.RecordsDialog_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label newbieTimeLabel;
        private System.Windows.Forms.Label advancedTimeLabel;
        private System.Windows.Forms.Label profiTimeLabel;
        private System.Windows.Forms.Label profiNameLabel;
        private System.Windows.Forms.Label advancedNameLabel;
        private System.Windows.Forms.Label newbieNameLabel;
        private System.Windows.Forms.Button resetRecordsButton;
        private System.Windows.Forms.Button closeButton;
    }
}