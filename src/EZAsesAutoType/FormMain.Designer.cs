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
            btnRun.Location = new Point(490, 70);
            btnRun.Margin = new Padding(2, 2, 2, 2);
            btnRun.Name = "btnRun";
            btnRun.Size = new Size(153, 56);
            btnRun.TabIndex = 3;
            btnRun.Text = "Run";
            btnRun.UseVisualStyleBackColor = true;
            btnRun.Click += btnRun_Click;
            // 
            // textBoxUrl
            // 
            textBoxUrl.Location = new Point(97, 10);
            textBoxUrl.Margin = new Padding(2, 2, 2, 2);
            textBoxUrl.Name = "textBoxUrl";
            textBoxUrl.Size = new Size(373, 27);
            textBoxUrl.TabIndex = 4;
            textBoxUrl.Text = "https://ases.noncd.rz.db.de/SES/html?ClientNo=06";
            // 
            // textBoxUid
            // 
            textBoxUid.Location = new Point(97, 70);
            textBoxUid.Margin = new Padding(2, 2, 2, 2);
            textBoxUid.Name = "textBoxUid";
            textBoxUid.Size = new Size(140, 27);
            textBoxUid.TabIndex = 6;
            textBoxUid.Text = "ChangeIt!";
            // 
            // textBoxPwd
            // 
            textBoxPwd.Location = new Point(330, 70);
            textBoxPwd.Margin = new Padding(2, 2, 2, 2);
            textBoxPwd.Name = "textBoxPwd";
            textBoxPwd.PasswordChar = '*';
            textBoxPwd.Size = new Size(140, 27);
            textBoxPwd.TabIndex = 7;
            textBoxPwd.Text = "ChangeIt!";
            textBoxPwd.UseSystemPasswordChar = true;
            // 
            // textBoxPunchIn
            // 
            textBoxPunchIn.Location = new Point(581, 10);
            textBoxPunchIn.Margin = new Padding(2, 2, 2, 2);
            textBoxPunchIn.Name = "textBoxPunchIn";
            textBoxPunchIn.Size = new Size(63, 27);
            textBoxPunchIn.TabIndex = 1;
            textBoxPunchIn.Text = "09:00";
            textBoxPunchIn.TextAlign = HorizontalAlignment.Center;
            // 
            // textBoxPunchOut
            // 
            textBoxPunchOut.Location = new Point(581, 39);
            textBoxPunchOut.Margin = new Padding(2, 2, 2, 2);
            textBoxPunchOut.Name = "textBoxPunchOut";
            textBoxPunchOut.Size = new Size(63, 27);
            textBoxPunchOut.TabIndex = 2;
            textBoxPunchOut.Text = "17:00";
            textBoxPunchOut.TextAlign = HorizontalAlignment.Center;
            // 
            // labelUrl
            // 
            labelUrl.AutoSize = true;
            labelUrl.Location = new Point(10, 12);
            labelUrl.Margin = new Padding(2, 0, 2, 0);
            labelUrl.Name = "labelUrl";
            labelUrl.Size = new Size(38, 20);
            labelUrl.TabIndex = 7;
            labelUrl.Text = "URL:";
            // 
            // labelUid
            // 
            labelUid.AutoSize = true;
            labelUid.Location = new Point(10, 73);
            labelUid.Margin = new Padding(2, 0, 2, 0);
            labelUid.Name = "labelUid";
            labelUid.Size = new Size(56, 20);
            labelUid.TabIndex = 8;
            labelUid.Text = "UserID:";
            // 
            // labelPwd
            // 
            labelPwd.AutoSize = true;
            labelPwd.Location = new Point(249, 73);
            labelPwd.Margin = new Padding(2, 0, 2, 0);
            labelPwd.Name = "labelPwd";
            labelPwd.Size = new Size(73, 20);
            labelPwd.TabIndex = 9;
            labelPwd.Text = "Password:";
            // 
            // labelPunchIn
            // 
            labelPunchIn.AutoSize = true;
            labelPunchIn.Location = new Point(490, 12);
            labelPunchIn.Margin = new Padding(2, 0, 2, 0);
            labelPunchIn.Name = "labelPunchIn";
            labelPunchIn.Size = new Size(69, 20);
            labelPunchIn.TabIndex = 10;
            labelPunchIn.Text = "Punch-In:";
            // 
            // labelPunchOut
            // 
            labelPunchOut.AutoSize = true;
            labelPunchOut.Location = new Point(490, 42);
            labelPunchOut.Margin = new Padding(2, 0, 2, 0);
            labelPunchOut.Name = "labelPunchOut";
            labelPunchOut.Size = new Size(81, 20);
            labelPunchOut.TabIndex = 11;
            labelPunchOut.Text = "Punch-Out:";
            // 
            // comboBoxClientNo
            // 
            comboBoxClientNo.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            comboBoxClientNo.AutoCompleteSource = AutoCompleteSource.ListItems;
            comboBoxClientNo.FormattingEnabled = true;
            comboBoxClientNo.Location = new Point(97, 39);
            comboBoxClientNo.Margin = new Padding(2, 2, 2, 2);
            comboBoxClientNo.Name = "comboBoxClientNo";
            comboBoxClientNo.Size = new Size(373, 28);
            comboBoxClientNo.TabIndex = 5;
            comboBoxClientNo.Text = "06-DB-Systel";
            comboBoxClientNo.SelectedIndexChanged += comboBoxClientNo_SelectedIndexChanged;
            comboBoxClientNo.SelectedValueChanged += comboBoxClientNo_SelectedValueChanged;
            comboBoxClientNo.TextChanged += comboBoxClientNo_TextChanged;
            // 
            // labelClientNo
            // 
            labelClientNo.AutoSize = true;
            labelClientNo.Location = new Point(10, 42);
            labelClientNo.Margin = new Padding(2, 0, 2, 0);
            labelClientNo.Name = "labelClientNo";
            labelClientNo.Size = new Size(50, 20);
            labelClientNo.TabIndex = 13;
            labelClientNo.Text = "Client:";
            // 
            // comboBoxWebDriver
            // 
            comboBoxWebDriver.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxWebDriver.FormattingEnabled = true;
            comboBoxWebDriver.Location = new Point(97, 100);
            comboBoxWebDriver.Margin = new Padding(2, 2, 2, 2);
            comboBoxWebDriver.Name = "comboBoxWebDriver";
            comboBoxWebDriver.Size = new Size(140, 28);
            comboBoxWebDriver.TabIndex = 8;
            comboBoxWebDriver.SelectedIndexChanged += comboBoxWebDriver_SelectedIndexChanged;
            // 
            // labelWebDriver
            // 
            labelWebDriver.AutoSize = true;
            labelWebDriver.Location = new Point(10, 102);
            labelWebDriver.Margin = new Padding(2, 0, 2, 0);
            labelWebDriver.Name = "labelWebDriver";
            labelWebDriver.Size = new Size(82, 20);
            labelWebDriver.TabIndex = 15;
            labelWebDriver.Text = "WebDriver:";
            // 
            // labelWebDriverVersion
            // 
            labelWebDriverVersion.AutoSize = true;
            labelWebDriverVersion.Location = new Point(249, 102);
            labelWebDriverVersion.Margin = new Padding(2, 0, 2, 0);
            labelWebDriverVersion.Name = "labelWebDriverVersion";
            labelWebDriverVersion.Size = new Size(60, 20);
            labelWebDriverVersion.TabIndex = 16;
            labelWebDriverVersion.Text = "Version:";
            // 
            // textBoxWebDriverVersion
            // 
            textBoxWebDriverVersion.Location = new Point(330, 100);
            textBoxWebDriverVersion.Margin = new Padding(2, 2, 2, 2);
            textBoxWebDriverVersion.Name = "textBoxWebDriverVersion";
            textBoxWebDriverVersion.ReadOnly = true;
            textBoxWebDriverVersion.Size = new Size(140, 27);
            textBoxWebDriverVersion.TabIndex = 9;
            textBoxWebDriverVersion.Text = "123.0.6312.58";
            // 
            // FormMain
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(670, 147);
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
            MaximizeBox = false;
            MaximumSize = new Size(688, 194);
            MinimizeBox = false;
            MinimumSize = new Size(688, 194);
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
