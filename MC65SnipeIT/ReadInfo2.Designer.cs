namespace MC65SnipeIT
{
    partial class ReadInfo2
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReadInfo2));
            this.UrlBox = new System.Windows.Forms.TextBox();
            this.GetBtn = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.ResultBox = new System.Windows.Forms.ListBox();
            this.CategoryText = new System.Windows.Forms.Label();
            this.ItemLocation = new System.Windows.Forms.Label();
            this.idtag = new System.Windows.Forms.Label();
            this.ItemName = new System.Windows.Forms.Label();
            this.KeyStatusInfo = new System.Windows.Forms.Label();
            this.exit = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.AddToLstBtn = new System.Windows.Forms.Button();
            this.LstStatus = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // UrlBox
            // 
            this.UrlBox.Location = new System.Drawing.Point(3, 3);
            this.UrlBox.Name = "UrlBox";
            this.UrlBox.Size = new System.Drawing.Size(348, 41);
            this.UrlBox.TabIndex = 0;
            this.UrlBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.UrlBox_KeyDown);
            // 
            // GetBtn
            // 
            this.GetBtn.Location = new System.Drawing.Point(357, 4);
            this.GetBtn.Name = "GetBtn";
            this.GetBtn.Size = new System.Drawing.Size(119, 40);
            this.GetBtn.TabIndex = 1;
            this.GetBtn.Text = "GET";
            this.GetBtn.Click += new System.EventHandler(this.GetBtn_Click);
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(3, 176);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(117, 40);
            this.label4.Text = "Category:";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(3, 136);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(117, 40);
            this.label3.Text = "Location:";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(3, 96);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(103, 40);
            this.label2.Text = "ID:";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(3, 56);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(103, 40);
            this.label1.Text = "Name:";
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Tahoma", 7F, System.Drawing.FontStyle.Regular);
            this.label5.Location = new System.Drawing.Point(4, 212);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(202, 26);
            this.label5.Text = "Custom Fields:";
            // 
            // ResultBox
            // 
            this.ResultBox.Location = new System.Drawing.Point(4, 235);
            this.ResultBox.Name = "ResultBox";
            this.ResultBox.Size = new System.Drawing.Size(473, 147);
            this.ResultBox.TabIndex = 11;
            this.ResultBox.SelectedIndexChanged += new System.EventHandler(this.ResultBox_SelectedIndexChanged);
            // 
            // CategoryText
            // 
            this.CategoryText.Location = new System.Drawing.Point(136, 176);
            this.CategoryText.Name = "CategoryText";
            this.CategoryText.Size = new System.Drawing.Size(337, 40);
            // 
            // ItemLocation
            // 
            this.ItemLocation.Location = new System.Drawing.Point(136, 136);
            this.ItemLocation.Name = "ItemLocation";
            this.ItemLocation.Size = new System.Drawing.Size(337, 37);
            // 
            // idtag
            // 
            this.idtag.Location = new System.Drawing.Point(136, 96);
            this.idtag.Name = "idtag";
            this.idtag.Size = new System.Drawing.Size(340, 40);
            // 
            // ItemName
            // 
            this.ItemName.Location = new System.Drawing.Point(136, 56);
            this.ItemName.Name = "ItemName";
            this.ItemName.Size = new System.Drawing.Size(340, 40);
            // 
            // KeyStatusInfo
            // 
            this.KeyStatusInfo.Font = new System.Drawing.Font("Tahoma", 6F, System.Drawing.FontStyle.Regular);
            this.KeyStatusInfo.Location = new System.Drawing.Point(3, 569);
            this.KeyStatusInfo.Name = "KeyStatusInfo";
            this.KeyStatusInfo.Size = new System.Drawing.Size(477, 20);
            this.KeyStatusInfo.Text = "Keyboard Shortcuts Disabled! - Use the keypad for data entry!";
            this.KeyStatusInfo.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // exit
            // 
            this.exit.Location = new System.Drawing.Point(3, 521);
            this.exit.Name = "exit";
            this.exit.Size = new System.Drawing.Size(471, 48);
            this.exit.TabIndex = 19;
            this.exit.Text = "Back To Main Menu";
            this.exit.Click += new System.EventHandler(this.exit_Click_1);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(4, 371);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(155, 165);
            // 
            // AddToLstBtn
            // 
            this.AddToLstBtn.Location = new System.Drawing.Point(165, 467);
            this.AddToLstBtn.Name = "AddToLstBtn";
            this.AddToLstBtn.Size = new System.Drawing.Size(308, 48);
            this.AddToLstBtn.TabIndex = 30;
            this.AddToLstBtn.Text = "Add to List";
            this.AddToLstBtn.Click += new System.EventHandler(this.AddToLstBtn_Click);
            // 
            // LstStatus
            // 
            this.LstStatus.Font = new System.Drawing.Font("Tahoma", 7F, System.Drawing.FontStyle.Regular);
            this.LstStatus.Location = new System.Drawing.Point(165, 439);
            this.LstStatus.Name = "LstStatus";
            this.LstStatus.Size = new System.Drawing.Size(308, 25);
            this.LstStatus.Text = "Waiting for user...";
            // 
            // ReadInfo2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(192F, 192F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(187)))), ((int)(((byte)(213)))));
            this.ClientSize = new System.Drawing.Size(480, 588);
            this.Controls.Add(this.LstStatus);
            this.Controls.Add(this.AddToLstBtn);
            this.Controls.Add(this.KeyStatusInfo);
            this.Controls.Add(this.exit);
            this.Controls.Add(this.CategoryText);
            this.Controls.Add(this.ItemLocation);
            this.Controls.Add(this.idtag);
            this.Controls.Add(this.ItemName);
            this.Controls.Add(this.ResultBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.GetBtn);
            this.Controls.Add(this.UrlBox);
            this.Controls.Add(this.pictureBox1);
            this.Location = new System.Drawing.Point(0, 52);
            this.Name = "ReadInfo2";
            this.Text = "ELW - Informations";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox UrlBox;
        private System.Windows.Forms.Button GetBtn;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ListBox ResultBox;
        private System.Windows.Forms.Label CategoryText;
        private System.Windows.Forms.Label ItemLocation;
        private System.Windows.Forms.Label idtag;
        private System.Windows.Forms.Label ItemName;
        private System.Windows.Forms.Label KeyStatusInfo;
        private System.Windows.Forms.Button exit;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button AddToLstBtn;
        private System.Windows.Forms.Label LstStatus;

    }
}