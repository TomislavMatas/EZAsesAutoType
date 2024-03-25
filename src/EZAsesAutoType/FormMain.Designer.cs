namespace EZAsesAutoType
{
    partial class FormMain
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            btnRun = new Button();
            textBoxUrl = new TextBox();
            textBoxUid = new TextBox();
            textBoxPwd = new TextBox();
            textBoxPunchIn = new TextBox();
            textBoxPunchOut = new TextBox();
            labelUrl = new Label();
            labelUid = new Label();
            labelPwd = new Label();
            labelPunchIn = new Label();
            labelPunchOut = new Label();
            comboBoxClientNo = new ComboBox();
            labelClientNo = new Label();
            comboBoxWebDriver = new ComboBox();
            labelWebDriver = new Label();
            labelWebDriverVersion = new Label();
            textBoxWebDriverVersion = new TextBox();
            SuspendLayout();
            // 
            // btnRun
            // 
            btnRun.Location = new Point(613, 88);
            btnRun.Name = "btnRun";
            btnRun.Size = new Size(191, 70);
            btnRun.TabIndex = 3;
            btnRun.Text = "Run";
            btnRun.UseVisualStyleBackColor = true;
            btnRun.Click += btnRun_Click;
            // 
            // textBoxUrl
            // 
            textBoxUrl.Location = new Point(121, 12);
            textBoxUrl.Name = "textBoxUrl";
            textBoxUrl.Size = new Size(465, 31);
            textBoxUrl.TabIndex = 4;
            textBoxUrl.Text = "https://ases.noncd.rz.db.de/SES/html?ClientNo=06";
            // 
            // textBoxUid
            // 
            textBoxUid.Location = new Point(121, 88);
            textBoxUid.Name = "textBoxUid";
            textBoxUid.Size = new Size(174, 31);
            textBoxUid.TabIndex = 6;
            textBoxUid.Text = "ChangeIt!";
            // 
            // textBoxPwd
            // 
            textBoxPwd.Location = new Point(412, 88);
            textBoxPwd.Name = "textBoxPwd";
            textBoxPwd.PasswordChar = '*';
            textBoxPwd.Size = new Size(174, 31);
            textBoxPwd.TabIndex = 7;
            textBoxPwd.Text = "ChangeIt!";
            textBoxPwd.UseSystemPasswordChar = true;
            // 
            // textBoxPunchIn
            // 
            textBoxPunchIn.Location = new Point(726, 12);
            textBoxPunchIn.Name = "textBoxPunchIn";
            textBoxPunchIn.Size = new Size(78, 31);
            textBoxPunchIn.TabIndex = 1;
            textBoxPunchIn.Text = "09:00";
            textBoxPunchIn.TextAlign = HorizontalAlignment.Center;
            // 
            // textBoxPunchOut
            // 
            textBoxPunchOut.Location = new Point(726, 49);
            textBoxPunchOut.Name = "textBoxPunchOut";
            textBoxPunchOut.Size = new Size(78, 31);
            textBoxPunchOut.TabIndex = 2;
            textBoxPunchOut.Text = "17:00";
            textBoxPunchOut.TextAlign = HorizontalAlignment.Center;
            // 
            // labelUrl
            // 
            labelUrl.AutoSize = true;
            labelUrl.Location = new Point(12, 15);
            labelUrl.Name = "labelUrl";
            labelUrl.Size = new Size(47, 25);
            labelUrl.TabIndex = 7;
            labelUrl.Text = "URL:";
            // 
            // labelUid
            // 
            labelUid.AutoSize = true;
            labelUid.Location = new Point(12, 91);
            labelUid.Name = "labelUid";
            labelUid.Size = new Size(69, 25);
            labelUid.TabIndex = 8;
            labelUid.Text = "UserID:";
            // 
            // labelPwd
            // 
            labelPwd.AutoSize = true;
            labelPwd.Location = new Point(311, 91);
            labelPwd.Name = "labelPwd";
            labelPwd.Size = new Size(91, 25);
            labelPwd.TabIndex = 9;
            labelPwd.Text = "Password:";
            // 
            // labelPunchIn
            // 
            labelPunchIn.AutoSize = true;
            labelPunchIn.Location = new Point(613, 15);
            labelPunchIn.Name = "labelPunchIn";
            labelPunchIn.Size = new Size(86, 25);
            labelPunchIn.TabIndex = 10;
            labelPunchIn.Text = "Punch-In:";
            // 
            // labelPunchOut
            // 
            labelPunchOut.AutoSize = true;
            labelPunchOut.Location = new Point(613, 52);
            labelPunchOut.Name = "labelPunchOut";
            labelPunchOut.Size = new Size(101, 25);
            labelPunchOut.TabIndex = 11;
            labelPunchOut.Text = "Punch-Out:";
            // 
            // comboBoxClientNo
            // 
            comboBoxClientNo.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            comboBoxClientNo.AutoCompleteSource = AutoCompleteSource.ListItems;
            comboBoxClientNo.FormattingEnabled = true;
            comboBoxClientNo.Location = new Point(121, 49);
            comboBoxClientNo.Name = "comboBoxClientNo";
            comboBoxClientNo.Size = new Size(465, 33);
            comboBoxClientNo.TabIndex = 5;
            comboBoxClientNo.Text = "06-DB-Systel";
            comboBoxClientNo.SelectedIndexChanged += comboBoxClientNo_SelectedIndexChanged;
            comboBoxClientNo.SelectedValueChanged += comboBoxClientNo_SelectedValueChanged;
            // 
            // labelClientNo
            // 
            labelClientNo.AutoSize = true;
            labelClientNo.Location = new Point(12, 52);
            labelClientNo.Name = "labelClientNo";
            labelClientNo.Size = new Size(60, 25);
            labelClientNo.TabIndex = 13;
            labelClientNo.Text = "Client:";
            // 
            // comboBoxWebDriver
            // 
            comboBoxWebDriver.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxWebDriver.FormattingEnabled = true;
            comboBoxWebDriver.Location = new Point(121, 125);
            comboBoxWebDriver.Name = "comboBoxWebDriver";
            comboBoxWebDriver.Size = new Size(174, 33);
            comboBoxWebDriver.TabIndex = 8;
            comboBoxWebDriver.SelectedIndexChanged += comboBoxWebDriver_SelectedIndexChanged;
            // 
            // labelWebDriver
            // 
            labelWebDriver.AutoSize = true;
            labelWebDriver.Location = new Point(12, 128);
            labelWebDriver.Name = "labelWebDriver";
            labelWebDriver.Size = new Size(99, 25);
            labelWebDriver.TabIndex = 15;
            labelWebDriver.Text = "WebDriver:";
            // 
            // labelWebDriverVersion
            // 
            labelWebDriverVersion.AutoSize = true;
            labelWebDriverVersion.Location = new Point(311, 128);
            labelWebDriverVersion.Name = "labelWebDriverVersion";
            labelWebDriverVersion.Size = new Size(74, 25);
            labelWebDriverVersion.TabIndex = 16;
            labelWebDriverVersion.Text = "Version:";
            // 
            // textBoxWebDriverVersion
            // 
            textBoxWebDriverVersion.Location = new Point(412, 125);
            textBoxWebDriverVersion.Name = "textBoxWebDriverVersion";
            textBoxWebDriverVersion.ReadOnly = true;
            textBoxWebDriverVersion.Size = new Size(174, 31);
            textBoxWebDriverVersion.TabIndex = 9;
            textBoxWebDriverVersion.Text = "123.0.6312.58";
            // 
            // FormMain
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(833, 175);
            Controls.Add(textBoxWebDriverVersion);
            Controls.Add(labelWebDriverVersion);
            Controls.Add(labelWebDriver);
            Controls.Add(comboBoxWebDriver);
            Controls.Add(labelClientNo);
            Controls.Add(comboBoxClientNo);
            Controls.Add(labelPunchOut);
            Controls.Add(labelPunchIn);
            Controls.Add(labelPwd);
            Controls.Add(labelUid);
            Controls.Add(labelUrl);
            Controls.Add(textBoxPunchOut);
            Controls.Add(textBoxPunchIn);
            Controls.Add(textBoxPwd);
            Controls.Add(textBoxUid);
            Controls.Add(textBoxUrl);
            Controls.Add(btnRun);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(4);
            MaximizeBox = false;
            MaximumSize = new Size(855, 231);
            MinimizeBox = false;
            MinimumSize = new Size(855, 231);
            Name = "FormMain";
            Text = "EZAsesAutoType";
            FormClosing += onClosing;
            Load += onLoad;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnRun;
        private TextBox textBoxUrl;
        private TextBox textBoxUid;
        private TextBox textBoxPwd;
        private TextBox textBoxPunchIn;
        private TextBox textBoxPunchOut;
        private Label labelUrl;
        private Label labelUid;
        private Label labelPwd;
        private Label labelPunchIn;
        private Label labelPunchOut;
        private ComboBox comboBoxClientNo;
        private Label labelClientNo;
        private ComboBox comboBoxWebDriver;
        private Label labelWebDriver;
        private Label labelWebDriverVersion;
        private TextBox textBoxWebDriverVersion;
    }
}
