namespace MC65SnipeIT
{
    partial class StickerLocation
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
            this.locationBox = new System.Windows.Forms.ComboBox();
            this.KeyStatusInfo = new System.Windows.Forms.Label();
            this.PrintBtn = new System.Windows.Forms.Button();
            this.ExitBtn = new System.Windows.Forms.Button();
            this.SaveBtn = new System.Windows.Forms.Button();
            this.GenerateBtn = new System.Windows.Forms.Button();
            this.IDText = new System.Windows.Forms.Label();
            this.picBarcode = new System.Windows.Forms.PictureBox();
            this.NameText = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // locationBox
            // 
            this.locationBox.Location = new System.Drawing.Point(39, 12);
            this.locationBox.Name = "locationBox";
            this.locationBox.Size = new System.Drawing.Size(274, 41);
            this.locationBox.TabIndex = 16;
            // 
            // KeyStatusInfo
            // 
            this.KeyStatusInfo.Font = new System.Drawing.Font("Tahoma", 6F, System.Drawing.FontStyle.Regular);
            this.KeyStatusInfo.Location = new System.Drawing.Point(10, 570);
            this.KeyStatusInfo.Name = "KeyStatusInfo";
            this.KeyStatusInfo.Size = new System.Drawing.Size(477, 20);
            this.KeyStatusInfo.Text = "Keyboard Shortcuts Disabled! - Use the keypad for data entry!";
            this.KeyStatusInfo.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // PrintBtn
            // 
            this.PrintBtn.Enabled = false;
            this.PrintBtn.Location = new System.Drawing.Point(319, 540);
            this.PrintBtn.Name = "PrintBtn";
            this.PrintBtn.Size = new System.Drawing.Size(144, 30);
            this.PrintBtn.TabIndex = 15;
            this.PrintBtn.TabStop = false;
            this.PrintBtn.Text = "Print";
            this.PrintBtn.Visible = false;
            this.PrintBtn.Click += new System.EventHandler(this.PrintBtn_Click);
            // 
            // ExitBtn
            // 
            this.ExitBtn.Location = new System.Drawing.Point(77, 540);
            this.ExitBtn.Name = "ExitBtn";
            this.ExitBtn.Size = new System.Drawing.Size(144, 30);
            this.ExitBtn.TabIndex = 14;
            this.ExitBtn.Text = "Exit";
            this.ExitBtn.Click += new System.EventHandler(this.ExitBtn_Click);
            // 
            // SaveBtn
            // 
            this.SaveBtn.Enabled = false;
            this.SaveBtn.Location = new System.Drawing.Point(250, 540);
            this.SaveBtn.Name = "SaveBtn";
            this.SaveBtn.Size = new System.Drawing.Size(144, 30);
            this.SaveBtn.TabIndex = 13;
            this.SaveBtn.Text = "Save";
            this.SaveBtn.Click += new System.EventHandler(this.SaveBtn_Click);
            // 
            // GenerateBtn
            // 
            this.GenerateBtn.Location = new System.Drawing.Point(319, 13);
            this.GenerateBtn.Name = "GenerateBtn";
            this.GenerateBtn.Size = new System.Drawing.Size(144, 40);
            this.GenerateBtn.TabIndex = 12;
            this.GenerateBtn.Text = "Generate";
            this.GenerateBtn.Click += new System.EventHandler(this.GenerateBtn_Click);
            // 
            // IDText
            // 
            this.IDText.Location = new System.Drawing.Point(7, 417);
            this.IDText.Name = "IDText";
            this.IDText.Size = new System.Drawing.Size(398, 40);
            this.IDText.Text = "ID";
            this.IDText.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // picBarcode
            // 
            this.picBarcode.Location = new System.Drawing.Point(53, 9);
            this.picBarcode.Name = "picBarcode";
            this.picBarcode.Size = new System.Drawing.Size(309, 309);
            this.picBarcode.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            // 
            // NameText
            // 
            this.NameText.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.NameText.Location = new System.Drawing.Point(7, 304);
            this.NameText.Name = "NameText";
            this.NameText.Size = new System.Drawing.Size(398, 38);
            this.NameText.Text = "Name of the Location";
            this.NameText.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.IDText);
            this.panel1.Controls.Add(this.NameText);
            this.panel1.Controls.Add(this.picBarcode);
            this.panel1.Location = new System.Drawing.Point(36, 62);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(404, 472);
            // 
            // StickerLocation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(192F, 192F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(187)))), ((int)(((byte)(213)))));
            this.ClientSize = new System.Drawing.Size(480, 588);
            this.Controls.Add(this.locationBox);
            this.Controls.Add(this.KeyStatusInfo);
            this.Controls.Add(this.PrintBtn);
            this.Controls.Add(this.ExitBtn);
            this.Controls.Add(this.SaveBtn);
            this.Controls.Add(this.GenerateBtn);
            this.Controls.Add(this.panel1);
            this.Location = new System.Drawing.Point(0, 52);
            this.Name = "StickerLocation";
            this.Text = "ELW - Loc Sticker Gen";
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox locationBox;
        private System.Windows.Forms.Label KeyStatusInfo;
        private System.Windows.Forms.Button PrintBtn;
        private System.Windows.Forms.Button ExitBtn;
        private System.Windows.Forms.Button SaveBtn;
        private System.Windows.Forms.Button GenerateBtn;
        private System.Windows.Forms.Label IDText;
        private System.Windows.Forms.PictureBox picBarcode;
        private System.Windows.Forms.Label NameText;
        private System.Windows.Forms.Panel panel1;

    }
}