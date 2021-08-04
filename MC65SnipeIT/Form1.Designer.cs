namespace MC65SnipeIT
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.Cat = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.WiFiSetup = new System.Windows.Forms.LinkLabel();
            this.GSMSetup = new System.Windows.Forms.LinkLabel();
            this.ExitToOS = new System.Windows.Forms.LinkLabel();
            this.ServSetup = new System.Windows.Forms.LinkLabel();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.KeyStatusInfo = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // Cat
            // 
            this.Cat.Image = ((System.Drawing.Image)(resources.GetObject("Cat.Image")));
            this.Cat.Location = new System.Drawing.Point(119, 63);
            this.Cat.Name = "Cat";
            this.Cat.Size = new System.Drawing.Size(230, 206);
            this.Cat.Click += new System.EventHandler(this.Cat_Click);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(108, 272);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(263, 40);
            this.label1.Text = "1 - Press Cat To Login";
            // 
            // WiFiSetup
            // 
            this.WiFiSetup.Location = new System.Drawing.Point(17, 365);
            this.WiFiSetup.Name = "WiFiSetup";
            this.WiFiSetup.Size = new System.Drawing.Size(200, 40);
            this.WiFiSetup.TabIndex = 2;
            this.WiFiSetup.Text = "2 - WiFi Setup";
            this.WiFiSetup.Click += new System.EventHandler(this.WiFiSetup_Click);
            // 
            // GSMSetup
            // 
            this.GSMSetup.Location = new System.Drawing.Point(17, 409);
            this.GSMSetup.Name = "GSMSetup";
            this.GSMSetup.Size = new System.Drawing.Size(200, 40);
            this.GSMSetup.TabIndex = 3;
            this.GSMSetup.Text = "3 - GSM Setup";
            this.GSMSetup.Click += new System.EventHandler(this.GSMSetup_Click);
            // 
            // ExitToOS
            // 
            this.ExitToOS.Location = new System.Drawing.Point(17, 489);
            this.ExitToOS.Name = "ExitToOS";
            this.ExitToOS.Size = new System.Drawing.Size(200, 40);
            this.ExitToOS.TabIndex = 5;
            this.ExitToOS.Text = "5 - Exit to the OS";
            this.ExitToOS.Click += new System.EventHandler(this.ExitToOS_Click);
            // 
            // ServSetup
            // 
            this.ServSetup.Location = new System.Drawing.Point(17, 449);
            this.ServSetup.Name = "ServSetup";
            this.ServSetup.Size = new System.Drawing.Size(200, 40);
            this.ServSetup.TabIndex = 4;
            this.ServSetup.Text = "4 - Server Setup";
            this.ServSetup.Click += new System.EventHandler(this.ServSetup_Click);
            // 
            // linkLabel1
            // 
            this.linkLabel1.Font = new System.Drawing.Font("Tahoma", 6F, System.Drawing.FontStyle.Underline);
            this.linkLabel1.Location = new System.Drawing.Point(0, 535);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(477, 22);
            this.linkLabel1.TabIndex = 6;
            this.linkLabel1.Text = "6 - About Emily\'s Little Warehouse";
            this.linkLabel1.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.linkLabel1.Click += new System.EventHandler(this.linkLabel1_Click);
            // 
            // KeyStatusInfo
            // 
            this.KeyStatusInfo.Font = new System.Drawing.Font("Tahoma", 6F, System.Drawing.FontStyle.Regular);
            this.KeyStatusInfo.Location = new System.Drawing.Point(3, 567);
            this.KeyStatusInfo.Name = "KeyStatusInfo";
            this.KeyStatusInfo.Size = new System.Drawing.Size(477, 20);
            this.KeyStatusInfo.Text = "Keyboard Shortcuts Enabled! - Use the keypad for navigation!";
            this.KeyStatusInfo.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(192F, 192F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(187)))), ((int)(((byte)(213)))));
            this.ClientSize = new System.Drawing.Size(480, 588);
            this.ControlBox = false;
            this.Controls.Add(this.KeyStatusInfo);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.ServSetup);
            this.Controls.Add(this.ExitToOS);
            this.Controls.Add(this.GSMSetup);
            this.Controls.Add(this.WiFiSetup);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Cat);
            this.KeyPreview = true;
            this.Location = new System.Drawing.Point(0, 52);
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.Text = "Emily\'s Little Warehouse";
            this.TopMost = true;
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox Cat;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.LinkLabel WiFiSetup;
        private System.Windows.Forms.LinkLabel GSMSetup;
        private System.Windows.Forms.LinkLabel ExitToOS;
        private System.Windows.Forms.LinkLabel ServSetup;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.Label KeyStatusInfo;
    }
}

