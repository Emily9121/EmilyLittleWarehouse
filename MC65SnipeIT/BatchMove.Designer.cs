namespace MC65SnipeIT
{
    partial class BatchMove
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BatchMove));
            this.label3 = new System.Windows.Forms.Label();
            this.MoveBtn = new System.Windows.Forms.Button();
            this.history = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.UrlBox = new System.Windows.Forms.TextBox();
            this.KeyStatusInfo = new System.Windows.Forms.Label();
            this.ExitBnt = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.locationbox = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(187)))), ((int)(((byte)(213)))));
            this.label3.Font = new System.Drawing.Font("Tahoma", 6F, System.Drawing.FontStyle.Regular);
            this.label3.Location = new System.Drawing.Point(2, 96);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 20);
            this.label3.Text = "History:";
            // 
            // MoveBtn
            // 
            this.MoveBtn.Location = new System.Drawing.Point(166, 261);
            this.MoveBtn.Name = "MoveBtn";
            this.MoveBtn.Size = new System.Drawing.Size(144, 40);
            this.MoveBtn.TabIndex = 10;
            this.MoveBtn.Text = "Move";
            this.MoveBtn.Click += new System.EventHandler(this.MoveBtn_Click);
            // 
            // history
            // 
            this.history.Location = new System.Drawing.Point(2, 116);
            this.history.Name = "history";
            this.history.Size = new System.Drawing.Size(471, 118);
            this.history.TabIndex = 9;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(2, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(170, 40);
            this.label1.Text = "Item URL:";
            // 
            // UrlBox
            // 
            this.UrlBox.AcceptsReturn = true;
            this.UrlBox.Location = new System.Drawing.Point(178, 4);
            this.UrlBox.Name = "UrlBox";
            this.UrlBox.Size = new System.Drawing.Size(295, 41);
            this.UrlBox.TabIndex = 7;
            this.UrlBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.UrlBox_KeyDown);
            // 
            // KeyStatusInfo
            // 
            this.KeyStatusInfo.BackColor = System.Drawing.Color.White;
            this.KeyStatusInfo.Font = new System.Drawing.Font("Tahoma", 6F, System.Drawing.FontStyle.Regular);
            this.KeyStatusInfo.Location = new System.Drawing.Point(-2, 569);
            this.KeyStatusInfo.Name = "KeyStatusInfo";
            this.KeyStatusInfo.Size = new System.Drawing.Size(477, 20);
            this.KeyStatusInfo.Text = "Keyboard Shortcuts Disabled! - Use the keypad for data entry!";
            this.KeyStatusInfo.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // ExitBnt
            // 
            this.ExitBnt.Location = new System.Drawing.Point(2, 518);
            this.ExitBnt.Name = "ExitBnt";
            this.ExitBnt.Size = new System.Drawing.Size(471, 48);
            this.ExitBnt.TabIndex = 11;
            this.ExitBnt.Text = "Back To Main Menu";
            this.ExitBnt.Click += new System.EventHandler(this.ExitBnt_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(-1, 157);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(497, 478);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(3, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(169, 40);
            this.label2.Text = "Location:";
            // 
            // locationbox
            // 
            this.locationbox.Location = new System.Drawing.Point(178, 51);
            this.locationbox.Name = "locationbox";
            this.locationbox.Size = new System.Drawing.Size(295, 41);
            this.locationbox.TabIndex = 15;
            this.locationbox.SelectedIndexChanged += new System.EventHandler(this.locationbox_SelectedIndexChanged);
            // 
            // BatchMove
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(192F, 192F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(187)))), ((int)(((byte)(213)))));
            this.ClientSize = new System.Drawing.Size(480, 588);
            this.Controls.Add(this.locationbox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.MoveBtn);
            this.Controls.Add(this.history);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.UrlBox);
            this.Controls.Add(this.KeyStatusInfo);
            this.Controls.Add(this.ExitBnt);
            this.Controls.Add(this.pictureBox1);
            this.Location = new System.Drawing.Point(0, 52);
            this.Name = "BatchMove";
            this.Text = "ELW - Batch Move";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button MoveBtn;
        private System.Windows.Forms.ListBox history;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox UrlBox;
        private System.Windows.Forms.Label KeyStatusInfo;
        private System.Windows.Forms.Button ExitBnt;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox locationbox;
    }
}