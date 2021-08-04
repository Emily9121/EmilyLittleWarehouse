namespace MC65SnipeIT
{
    partial class search
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.MainMenu mainMenu1;

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(search));
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.label1 = new System.Windows.Forms.Label();
            this.searchBox = new System.Windows.Forms.TextBox();
            this.searchBtn = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.ResultBox = new System.Windows.Forms.ListBox();
            this.button1 = new System.Windows.Forms.Button();
            this.KeyStatusInfo = new System.Windows.Forms.Label();
            this.ItemID = new System.Windows.Forms.Label();
            this.GetInfoBtn = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(14, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 40);
            this.label1.Text = "Search:";
            // 
            // searchBox
            // 
            this.searchBox.AcceptsReturn = true;
            this.searchBox.Location = new System.Drawing.Point(116, 15);
            this.searchBox.Name = "searchBox";
            this.searchBox.Size = new System.Drawing.Size(253, 41);
            this.searchBox.TabIndex = 1;
            this.searchBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.searchBox_KeyDown);
            // 
            // searchBtn
            // 
            this.searchBtn.Location = new System.Drawing.Point(375, 15);
            this.searchBtn.Name = "searchBtn";
            this.searchBtn.Size = new System.Drawing.Size(88, 40);
            this.searchBtn.TabIndex = 2;
            this.searchBtn.Text = "Go";
            this.searchBtn.Click += new System.EventHandler(this.searchBtn_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(0, 107);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(497, 478);
            // 
            // ResultBox
            // 
            this.ResultBox.Location = new System.Drawing.Point(14, 85);
            this.ResultBox.Name = "ResultBox";
            this.ResultBox.Size = new System.Drawing.Size(449, 147);
            this.ResultBox.TabIndex = 5;
            this.ResultBox.SelectedIndexChanged += new System.EventHandler(this.ResultBox_SelectedIndexChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(14, 472);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(449, 40);
            this.button1.TabIndex = 6;
            this.button1.Text = "Back To Main Menu";
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // KeyStatusInfo
            // 
            this.KeyStatusInfo.BackColor = System.Drawing.Color.White;
            this.KeyStatusInfo.Font = new System.Drawing.Font("Tahoma", 6F, System.Drawing.FontStyle.Regular);
            this.KeyStatusInfo.Location = new System.Drawing.Point(3, 515);
            this.KeyStatusInfo.Name = "KeyStatusInfo";
            this.KeyStatusInfo.Size = new System.Drawing.Size(477, 20);
            this.KeyStatusInfo.Text = "Keyboard Shortcuts Disabled! - Use the keypad for data entry!";
            this.KeyStatusInfo.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // ItemID
            // 
            this.ItemID.BackColor = System.Drawing.Color.White;
            this.ItemID.Location = new System.Drawing.Point(14, 321);
            this.ItemID.Name = "ItemID";
            this.ItemID.Size = new System.Drawing.Size(449, 40);
            this.ItemID.Text = "Selected Item ID";
            this.ItemID.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // GetInfoBtn
            // 
            this.GetInfoBtn.Location = new System.Drawing.Point(14, 426);
            this.GetInfoBtn.Name = "GetInfoBtn";
            this.GetInfoBtn.Size = new System.Drawing.Size(163, 40);
            this.GetInfoBtn.TabIndex = 9;
            this.GetInfoBtn.Text = "Get Info";
            this.GetInfoBtn.Click += new System.EventHandler(this.GetInfoBtn_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(319, 426);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(144, 40);
            this.button3.TabIndex = 10;
            this.button3.Text = "Add to list";
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // search
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(192F, 192F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(187)))), ((int)(((byte)(213)))));
            this.ClientSize = new System.Drawing.Size(480, 536);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.GetInfoBtn);
            this.Controls.Add(this.ItemID);
            this.Controls.Add(this.KeyStatusInfo);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.ResultBox);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.searchBtn);
            this.Controls.Add(this.searchBox);
            this.Controls.Add(this.label1);
            this.Location = new System.Drawing.Point(0, 52);
            this.Menu = this.mainMenu1;
            this.Name = "search";
            this.Text = "ELW - Search";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox searchBox;
        private System.Windows.Forms.Button searchBtn;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ListBox ResultBox;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label KeyStatusInfo;
        private System.Windows.Forms.Label ItemID;
        private System.Windows.Forms.Button GetInfoBtn;
        private System.Windows.Forms.Button button3;
    }
}