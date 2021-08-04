namespace MC65SnipeIT
{
    partial class ServerSettings
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
            this.URLText = new System.Windows.Forms.Label();
            this.ServAddress = new System.Windows.Forms.TextBox();
            this.TokenText = new System.Windows.Forms.Label();
            this.Token = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.KeyStatusInfo = new System.Windows.Forms.Label();
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // URLText
            // 
            this.URLText.Location = new System.Drawing.Point(3, 24);
            this.URLText.Name = "URLText";
            this.URLText.Size = new System.Drawing.Size(200, 40);
            this.URLText.Text = "Server Address:";
            this.URLText.Visible = false;
            // 
            // ServAddress
            // 
            this.ServAddress.Location = new System.Drawing.Point(3, 67);
            this.ServAddress.Name = "ServAddress";
            this.ServAddress.Size = new System.Drawing.Size(474, 41);
            this.ServAddress.TabIndex = 1;
            this.ServAddress.Visible = false;
            // 
            // TokenText
            // 
            this.TokenText.Location = new System.Drawing.Point(4, 191);
            this.TokenText.Name = "TokenText";
            this.TokenText.Size = new System.Drawing.Size(200, 40);
            this.TokenText.Text = "Token:";
            this.TokenText.Visible = false;
            // 
            // Token
            // 
            this.Token.Location = new System.Drawing.Point(3, 234);
            this.Token.Name = "Token";
            this.Token.Size = new System.Drawing.Size(474, 41);
            this.Token.TabIndex = 3;
            this.Token.Visible = false;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(237, 281);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(240, 40);
            this.button1.TabIndex = 4;
            this.button1.Text = "Import From File";
            this.button1.Visible = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // checkBox1
            // 
            this.checkBox1.Location = new System.Drawing.Point(4, 365);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(200, 40);
            this.checkBox1.TabIndex = 5;
            this.checkBox1.Text = "Debug Mode";
            this.checkBox1.Visible = false;
            this.checkBox1.CheckStateChanged += new System.EventHandler(this.checkBox1_CheckStateChanged);
            // 
            // checkBox2
            // 
            this.checkBox2.Location = new System.Drawing.Point(4, 411);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(200, 40);
            this.checkBox2.TabIndex = 6;
            this.checkBox2.Text = "Read Only";
            this.checkBox2.Visible = false;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(333, 465);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(144, 40);
            this.button2.TabIndex = 7;
            this.button2.Text = "Nya!";
            this.button2.Visible = false;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(4, 465);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(144, 40);
            this.button3.TabIndex = 8;
            this.button3.Text = "Cancel";
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // KeyStatusInfo
            // 
            this.KeyStatusInfo.Font = new System.Drawing.Font("Tahoma", 6F, System.Drawing.FontStyle.Regular);
            this.KeyStatusInfo.Location = new System.Drawing.Point(3, 508);
            this.KeyStatusInfo.Name = "KeyStatusInfo";
            this.KeyStatusInfo.Size = new System.Drawing.Size(477, 20);
            this.KeyStatusInfo.Text = "Keyboard Shortcuts Disabled! - Use the keypad for data entry!";
            this.KeyStatusInfo.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(0, 111);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(450, 80);
            this.label1.Text = "WARNING: BROKEN - Edit config by editing app.config!";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // ServerSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(192F, 192F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(187)))), ((int)(((byte)(213)))));
            this.ClientSize = new System.Drawing.Size(480, 536);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.KeyStatusInfo);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.checkBox2);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.Token);
            this.Controls.Add(this.TokenText);
            this.Controls.Add(this.ServAddress);
            this.Controls.Add(this.URLText);
            this.Location = new System.Drawing.Point(0, 52);
            this.Menu = this.mainMenu1;
            this.MinimizeBox = false;
            this.Name = "ServerSettings";
            this.Text = "ELW Server Settings";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label URLText;
        private System.Windows.Forms.TextBox ServAddress;
        private System.Windows.Forms.Label TokenText;
        private System.Windows.Forms.TextBox Token;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label KeyStatusInfo;
        private System.Windows.Forms.MainMenu mainMenu1;
        private System.Windows.Forms.Label label1;
    }
}