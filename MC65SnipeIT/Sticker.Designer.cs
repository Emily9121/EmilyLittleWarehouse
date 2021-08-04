namespace MC65SnipeIT
{
    partial class Sticker
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.AssetText = new System.Windows.Forms.Label();
            this.LocationText = new System.Windows.Forms.Label();
            this.NameText = new System.Windows.Forms.Label();
            this.picBarcode = new System.Windows.Forms.PictureBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.GenerateBtn = new System.Windows.Forms.Button();
            this.SaveBtn = new System.Windows.Forms.Button();
            this.ExitBtn = new System.Windows.Forms.Button();
            this.PrintBtn = new System.Windows.Forms.Button();
            this.KeyStatusInfo = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(18, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 40);
            this.label1.Text = "ID:";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.AssetText);
            this.panel1.Controls.Add(this.LocationText);
            this.panel1.Controls.Add(this.NameText);
            this.panel1.Controls.Add(this.picBarcode);
            this.panel1.Location = new System.Drawing.Point(36, 62);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(404, 472);
            // 
            // AssetText
            // 
            this.AssetText.Font = new System.Drawing.Font("Tahoma", 7F, System.Drawing.FontStyle.Bold);
            this.AssetText.Location = new System.Drawing.Point(3, 411);
            this.AssetText.Name = "AssetText";
            this.AssetText.Size = new System.Drawing.Size(398, 21);
            this.AssetText.Text = "Asset";
            this.AssetText.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // LocationText
            // 
            this.LocationText.Font = new System.Drawing.Font("Tahoma", 7F, System.Drawing.FontStyle.Bold);
            this.LocationText.Location = new System.Drawing.Point(3, 390);
            this.LocationText.Name = "LocationText";
            this.LocationText.Size = new System.Drawing.Size(398, 21);
            this.LocationText.Text = "Location";
            this.LocationText.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // NameText
            // 
            this.NameText.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.NameText.Location = new System.Drawing.Point(3, 297);
            this.NameText.Name = "NameText";
            this.NameText.Size = new System.Drawing.Size(398, 93);
            this.NameText.Text = "Name of the Item";
            this.NameText.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // picBarcode
            // 
            this.picBarcode.Location = new System.Drawing.Point(49, 2);
            this.picBarcode.Name = "picBarcode";
            this.picBarcode.Size = new System.Drawing.Size(309, 309);
            this.picBarcode.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            // 
            // textBox1
            // 
            this.textBox1.AcceptsReturn = true;
            this.textBox1.Location = new System.Drawing.Point(77, 11);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(236, 41);
            this.textBox1.TabIndex = 0;
            this.textBox1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox1_KeyDown);
            // 
            // GenerateBtn
            // 
            this.GenerateBtn.Location = new System.Drawing.Point(319, 13);
            this.GenerateBtn.Name = "GenerateBtn";
            this.GenerateBtn.Size = new System.Drawing.Size(144, 40);
            this.GenerateBtn.TabIndex = 2;
            this.GenerateBtn.Text = "Generate";
            this.GenerateBtn.Click += new System.EventHandler(this.GenerateBtn_Click);
            // 
            // SaveBtn
            // 
            this.SaveBtn.Enabled = false;
            this.SaveBtn.Location = new System.Drawing.Point(250, 540);
            this.SaveBtn.Name = "SaveBtn";
            this.SaveBtn.Size = new System.Drawing.Size(144, 30);
            this.SaveBtn.TabIndex = 5;
            this.SaveBtn.Text = "Save";
            this.SaveBtn.Click += new System.EventHandler(this.SaveBtn_Click);
            // 
            // ExitBtn
            // 
            this.ExitBtn.Location = new System.Drawing.Point(77, 540);
            this.ExitBtn.Name = "ExitBtn";
            this.ExitBtn.Size = new System.Drawing.Size(144, 30);
            this.ExitBtn.TabIndex = 6;
            this.ExitBtn.Text = "Exit";
            this.ExitBtn.Click += new System.EventHandler(this.ExitBtn_Click);
            // 
            // PrintBtn
            // 
            this.PrintBtn.Enabled = false;
            this.PrintBtn.Location = new System.Drawing.Point(319, 540);
            this.PrintBtn.Name = "PrintBtn";
            this.PrintBtn.Size = new System.Drawing.Size(144, 30);
            this.PrintBtn.TabIndex = 7;
            this.PrintBtn.TabStop = false;
            this.PrintBtn.Text = "Print";
            this.PrintBtn.Visible = false;
            this.PrintBtn.Click += new System.EventHandler(this.PrintBtn_Click);
            // 
            // KeyStatusInfo
            // 
            this.KeyStatusInfo.Font = new System.Drawing.Font("Tahoma", 6F, System.Drawing.FontStyle.Regular);
            this.KeyStatusInfo.Location = new System.Drawing.Point(6, 571);
            this.KeyStatusInfo.Name = "KeyStatusInfo";
            this.KeyStatusInfo.Size = new System.Drawing.Size(477, 20);
            this.KeyStatusInfo.Text = "Keyboard Shortcuts Disabled! - Use the keypad for data entry!";
            this.KeyStatusInfo.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // Sticker
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(192F, 192F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(187)))), ((int)(((byte)(213)))));
            this.ClientSize = new System.Drawing.Size(480, 588);
            this.Controls.Add(this.KeyStatusInfo);
            this.Controls.Add(this.PrintBtn);
            this.Controls.Add(this.ExitBtn);
            this.Controls.Add(this.SaveBtn);
            this.Controls.Add(this.GenerateBtn);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label1);
            this.Location = new System.Drawing.Point(0, 52);
            this.Name = "Sticker";
            this.Text = "ELW - Sticker Generation";
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label NameText;
        private System.Windows.Forms.PictureBox picBarcode;
        private System.Windows.Forms.Button GenerateBtn;
        private System.Windows.Forms.Label AssetText;
        private System.Windows.Forms.Label LocationText;
        private System.Windows.Forms.Button SaveBtn;
        private System.Windows.Forms.Button ExitBtn;
        private System.Windows.Forms.Button PrintBtn;
        private System.Windows.Forms.Label KeyStatusInfo;
    }
}