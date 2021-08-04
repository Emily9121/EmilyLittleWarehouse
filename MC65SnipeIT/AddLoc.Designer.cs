namespace MC65SnipeIT
{
    partial class AddLoc
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
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.ParentComboBox = new System.Windows.Forms.ComboBox();
            this.ManagerComboBox = new System.Windows.Forms.ComboBox();
            this.ParentCheck = new System.Windows.Forms.CheckBox();
            this.CountryBox = new System.Windows.Forms.TextBox();
            this.CurrencyBox = new System.Windows.Forms.TextBox();
            this.CityBox = new System.Windows.Forms.TextBox();
            this.ZIPBox = new System.Windows.Forms.TextBox();
            this.AddressBox = new System.Windows.Forms.TextBox();
            this.NameBox = new System.Windows.Forms.TextBox();
            this.labelmanager = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.Address2Box = new System.Windows.Forms.TextBox();
            this.statusText = new System.Windows.Forms.Label();
            this.KeyStatusInfo = new System.Windows.Forms.Label();
            this.LabelBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ParentComboBox
            // 
            this.ParentComboBox.Enabled = false;
            this.ParentComboBox.Location = new System.Drawing.Point(135, 69);
            this.ParentComboBox.Name = "ParentComboBox";
            this.ParentComboBox.Size = new System.Drawing.Size(341, 41);
            this.ParentComboBox.TabIndex = 9;
            // 
            // ManagerComboBox
            // 
            this.ManagerComboBox.Location = new System.Drawing.Point(135, 131);
            this.ManagerComboBox.Name = "ManagerComboBox";
            this.ManagerComboBox.Size = new System.Drawing.Size(342, 41);
            this.ManagerComboBox.TabIndex = 8;
            // 
            // ParentCheck
            // 
            this.ParentCheck.Location = new System.Drawing.Point(19, 69);
            this.ParentCheck.Name = "ParentCheck";
            this.ParentCheck.Size = new System.Drawing.Size(110, 40);
            this.ParentCheck.TabIndex = 10;
            this.ParentCheck.Text = "Child";
            this.ParentCheck.CheckStateChanged += new System.EventHandler(this.ParentCheck_CheckStateChanged);
            // 
            // CountryBox
            // 
            this.CountryBox.Location = new System.Drawing.Point(135, 334);
            this.CountryBox.Name = "CountryBox";
            this.CountryBox.Size = new System.Drawing.Size(341, 41);
            this.CountryBox.TabIndex = 5;
            // 
            // CurrencyBox
            // 
            this.CurrencyBox.Location = new System.Drawing.Point(135, 395);
            this.CurrencyBox.Name = "CurrencyBox";
            this.CurrencyBox.Size = new System.Drawing.Size(341, 41);
            this.CurrencyBox.TabIndex = 6;
            // 
            // CityBox
            // 
            this.CityBox.Location = new System.Drawing.Point(227, 287);
            this.CityBox.Name = "CityBox";
            this.CityBox.Size = new System.Drawing.Size(249, 41);
            this.CityBox.TabIndex = 4;
            // 
            // ZIPBox
            // 
            this.ZIPBox.Location = new System.Drawing.Point(135, 287);
            this.ZIPBox.Name = "ZIPBox";
            this.ZIPBox.Size = new System.Drawing.Size(86, 41);
            this.ZIPBox.TabIndex = 3;
            // 
            // AddressBox
            // 
            this.AddressBox.Location = new System.Drawing.Point(135, 193);
            this.AddressBox.Name = "AddressBox";
            this.AddressBox.Size = new System.Drawing.Size(341, 41);
            this.AddressBox.TabIndex = 1;
            // 
            // NameBox
            // 
            this.NameBox.Location = new System.Drawing.Point(135, 3);
            this.NameBox.Name = "NameBox";
            this.NameBox.Size = new System.Drawing.Size(341, 41);
            this.NameBox.TabIndex = 0;
            // 
            // labelmanager
            // 
            this.labelmanager.Location = new System.Drawing.Point(3, 131);
            this.labelmanager.Name = "labelmanager";
            this.labelmanager.Size = new System.Drawing.Size(126, 40);
            this.labelmanager.Text = "Manager:";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(3, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(126, 40);
            this.label1.Text = "Name:";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(3, 194);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(126, 40);
            this.label2.Text = "Address:";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(3, 395);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(129, 40);
            this.label3.Text = "Currency:";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(328, 467);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(144, 40);
            this.button1.TabIndex = 7;
            this.button1.Text = "Create";
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(9, 467);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(144, 40);
            this.button2.TabIndex = 17;
            this.button2.Text = "Exit";
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(3, 288);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(126, 40);
            this.label4.Text = "Zip/City:";
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(3, 336);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(129, 40);
            this.label5.Text = "Country:";
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(3, 241);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(126, 40);
            this.label6.Text = "Address 2:";
            // 
            // Address2Box
            // 
            this.Address2Box.Location = new System.Drawing.Point(135, 240);
            this.Address2Box.Name = "Address2Box";
            this.Address2Box.Size = new System.Drawing.Size(341, 41);
            this.Address2Box.TabIndex = 2;
            // 
            // statusText
            // 
            this.statusText.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.statusText.Location = new System.Drawing.Point(3, 439);
            this.statusText.Name = "statusText";
            this.statusText.Size = new System.Drawing.Size(477, 25);
            this.statusText.Text = "Status: Waiting for User...";
            // 
            // KeyStatusInfo
            // 
            this.KeyStatusInfo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(187)))), ((int)(((byte)(213)))));
            this.KeyStatusInfo.Font = new System.Drawing.Font("Tahoma", 6F, System.Drawing.FontStyle.Regular);
            this.KeyStatusInfo.Location = new System.Drawing.Point(3, 516);
            this.KeyStatusInfo.Name = "KeyStatusInfo";
            this.KeyStatusInfo.Size = new System.Drawing.Size(477, 20);
            this.KeyStatusInfo.Text = "Keyboard Shortcuts Disabled! - Use the keypad for data entry!";
            this.KeyStatusInfo.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // LabelBtn
            // 
            this.LabelBtn.Location = new System.Drawing.Point(159, 467);
            this.LabelBtn.Name = "LabelBtn";
            this.LabelBtn.Size = new System.Drawing.Size(163, 40);
            this.LabelBtn.TabIndex = 32;
            this.LabelBtn.Text = "Gen Stickers";
            this.LabelBtn.Click += new System.EventHandler(this.LabelBtn_Click);
            // 
            // AddLoc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(192F, 192F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(187)))), ((int)(((byte)(213)))));
            this.ClientSize = new System.Drawing.Size(480, 536);
            this.Controls.Add(this.LabelBtn);
            this.Controls.Add(this.KeyStatusInfo);
            this.Controls.Add(this.statusText);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.Address2Box);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.labelmanager);
            this.Controls.Add(this.NameBox);
            this.Controls.Add(this.AddressBox);
            this.Controls.Add(this.ZIPBox);
            this.Controls.Add(this.CityBox);
            this.Controls.Add(this.CurrencyBox);
            this.Controls.Add(this.CountryBox);
            this.Controls.Add(this.ParentCheck);
            this.Controls.Add(this.ManagerComboBox);
            this.Controls.Add(this.ParentComboBox);
            this.Location = new System.Drawing.Point(0, 52);
            this.Menu = this.mainMenu1;
            this.Name = "AddLoc";
            this.Text = "ELW - New Location";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox ParentComboBox;
        private System.Windows.Forms.ComboBox ManagerComboBox;
        private System.Windows.Forms.CheckBox ParentCheck;
        private System.Windows.Forms.TextBox CountryBox;
        private System.Windows.Forms.TextBox CurrencyBox;
        private System.Windows.Forms.TextBox CityBox;
        private System.Windows.Forms.TextBox ZIPBox;
        private System.Windows.Forms.TextBox AddressBox;
        private System.Windows.Forms.TextBox NameBox;
        private System.Windows.Forms.Label labelmanager;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox Address2Box;
        private System.Windows.Forms.Label statusText;
        private System.Windows.Forms.Label KeyStatusInfo;
        private System.Windows.Forms.Button LabelBtn;
    }
}