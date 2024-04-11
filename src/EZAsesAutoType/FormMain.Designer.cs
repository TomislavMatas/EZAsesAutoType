//
// File: "FormMain.Designer.cs"
//
// Revision History:
// 2024/04/14:TomislavMatas: Version "1.123.4.0"
// * Rename "textBoxPunchIn"  to "textBoxPunchInAM".
// * Rename "textBoxPunchOut" to "textBoxPunchOutAM".
// * Add "textBoxPunchInPM" and "textBoxPunchOutPM".
// 2024/04/04:TomislavMatas: Version "1.123.1"
// * Set WorkerSupportsCancellation=true.
// 2024/04/04:TomislavMatas: Version "1.0.0"
// * Initial version.
//

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
            textBoxPunchInAM = new TextBox();
            textBoxPunchOutAM = new TextBox();
            labelUrl = new Label();
            labelUid = new Label();
            labelPwd = new Label();
            labelPunchIn = new Label();
            comboBoxClientNo = new ComboBox();
            labelClientNo = new Label();
            comboBoxWebDriver = new ComboBox();
            labelWebDriver = new Label();
            labelWebDriverVersion = new Label();
            comboBoxLanguage = new ComboBox();
            labelLanguage = new Label();
            backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            btnCancel = new Button();
            textBoxPunchInPM = new TextBox();
            textBoxPunchOutPM = new TextBox();
            SuspendLayout();
            // 
            // btnRun
            // 
            btnRun.BackColor = Color.Gray;
            btnRun.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnRun.ForeColor = Color.White;
            btnRun.Location = new Point(414, 256);
            btnRun.Margin = new Padding(2, 4, 2, 4);
            btnRun.Name = "btnRun";
            btnRun.Size = new Size(180, 54);
            btnRun.TabIndex = 30;
            btnRun.Text = "Run";
            btnRun.UseVisualStyleBackColor = false;
            btnRun.Click += btnRun_Click;
            // 
            // textBoxUrl
            // 
            textBoxUrl.Location = new Point(121, 21);
            textBoxUrl.Margin = new Padding(2, 4, 2, 4);
            textBoxUrl.Name = "textBoxUrl";
            textBoxUrl.Size = new Size(465, 31);
            textBoxUrl.TabIndex = 40;
            textBoxUrl.Text = "https://ases.noncd.rz.db.de/SES/html?ClientNo=06";
            // 
            // textBoxUid
            // 
            textBoxUid.Location = new Point(121, 70);
            textBoxUid.Margin = new Padding(2, 4, 2, 4);
            textBoxUid.Name = "textBoxUid";
            textBoxUid.Size = new Size(174, 31);
            textBoxUid.TabIndex = 50;
            textBoxUid.Text = "ChangeIt!";
            // 
            // textBoxPwd
            // 
            textBoxPwd.Location = new Point(412, 70);
            textBoxPwd.Margin = new Padding(2, 4, 2, 4);
            textBoxPwd.Name = "textBoxPwd";
            textBoxPwd.PasswordChar = '*';
            textBoxPwd.Size = new Size(174, 31);
            textBoxPwd.TabIndex = 60;
            textBoxPwd.Text = "ChangeIt!";
            textBoxPwd.UseSystemPasswordChar = true;
            // 
            // textBoxPunchInAM
            // 
            textBoxPunchInAM.Location = new Point(412, 171);
            textBoxPunchInAM.Margin = new Padding(2, 4, 2, 4);
            textBoxPunchInAM.MaxLength = 5;
            textBoxPunchInAM.Name = "textBoxPunchInAM";
            textBoxPunchInAM.Size = new Size(84, 31);
            textBoxPunchInAM.TabIndex = 10;
            textBoxPunchInAM.Text = "09:00";
            textBoxPunchInAM.TextAlign = HorizontalAlignment.Center;
            textBoxPunchInAM.TextChanged += textBoxPunchInAM_TextChanged;
            textBoxPunchInAM.Validated += textBoxPunchInAM_Validated;
            // 
            // textBoxPunchOutAM
            // 
            textBoxPunchOutAM.Location = new Point(502, 171);
            textBoxPunchOutAM.Margin = new Padding(2, 4, 2, 4);
            textBoxPunchOutAM.MaxLength = 5;
            textBoxPunchOutAM.Name = "textBoxPunchOutAM";
            textBoxPunchOutAM.Size = new Size(84, 31);
            textBoxPunchOutAM.TabIndex = 20;
            textBoxPunchOutAM.Text = "12:00";
            textBoxPunchOutAM.TextAlign = HorizontalAlignment.Center;
            textBoxPunchOutAM.TextChanged += textBoxPunchOutAM_TextChanged;
            textBoxPunchOutAM.Validated += textBoxPunchOutAM_Validated;
            // 
            // labelUrl
            // 
            labelUrl.AutoSize = true;
            labelUrl.BackColor = Color.Transparent;
            labelUrl.ForeColor = Color.White;
            labelUrl.Location = new Point(12, 26);
            labelUrl.Margin = new Padding(2, 0, 2, 0);
            labelUrl.Name = "labelUrl";
            labelUrl.Size = new Size(47, 25);
            labelUrl.TabIndex = 7;
            labelUrl.Text = "URL:";
            // 
            // labelUid
            // 
            labelUid.AutoSize = true;
            labelUid.BackColor = Color.Transparent;
            labelUid.ForeColor = Color.White;
            labelUid.Location = new Point(12, 76);
            labelUid.Margin = new Padding(2, 0, 2, 0);
            labelUid.Name = "labelUid";
            labelUid.Size = new Size(69, 25);
            labelUid.TabIndex = 8;
            labelUid.Text = "UserID:";
            // 
            // labelPwd
            // 
            labelPwd.AutoSize = true;
            labelPwd.BackColor = Color.Transparent;
            labelPwd.ForeColor = Color.White;
            labelPwd.Location = new Point(311, 76);
            labelPwd.Margin = new Padding(2, 0, 2, 0);
            labelPwd.Name = "labelPwd";
            labelPwd.Size = new Size(91, 25);
            labelPwd.TabIndex = 9;
            labelPwd.Text = "Password:";
            // 
            // labelPunchIn
            // 
            labelPunchIn.AutoSize = true;
            labelPunchIn.BackColor = Color.Transparent;
            labelPunchIn.ForeColor = Color.White;
            labelPunchIn.Location = new Point(311, 174);
            labelPunchIn.Margin = new Padding(2, 0, 2, 0);
            labelPunchIn.Name = "labelPunchIn";
            labelPunchIn.Size = new Size(78, 25);
            labelPunchIn.TabIndex = 10;
            labelPunchIn.Text = "In / Out:";
            // 
            // comboBoxClientNo
            // 
            comboBoxClientNo.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            comboBoxClientNo.AutoCompleteSource = AutoCompleteSource.ListItems;
            comboBoxClientNo.FormattingEnabled = true;
            comboBoxClientNo.Location = new Point(121, 119);
            comboBoxClientNo.Margin = new Padding(2, 4, 2, 4);
            comboBoxClientNo.Name = "comboBoxClientNo";
            comboBoxClientNo.Size = new Size(174, 33);
            comboBoxClientNo.TabIndex = 70;
            comboBoxClientNo.Text = "06-DB-Systel";
            comboBoxClientNo.SelectedIndexChanged += comboBoxClientNo_SelectedIndexChanged;
            comboBoxClientNo.SelectedValueChanged += comboBoxClientNo_SelectedValueChanged;
            comboBoxClientNo.TextChanged += comboBoxClientNo_TextChanged;
            // 
            // labelClientNo
            // 
            labelClientNo.AutoSize = true;
            labelClientNo.BackColor = Color.Transparent;
            labelClientNo.ForeColor = Color.White;
            labelClientNo.Location = new Point(12, 125);
            labelClientNo.Margin = new Padding(2, 0, 2, 0);
            labelClientNo.Name = "labelClientNo";
            labelClientNo.Size = new Size(60, 25);
            labelClientNo.TabIndex = 13;
            labelClientNo.Text = "Client:";
            // 
            // comboBoxWebDriver
            // 
            comboBoxWebDriver.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxWebDriver.FormattingEnabled = true;
            comboBoxWebDriver.Location = new Point(121, 171);
            comboBoxWebDriver.Margin = new Padding(2, 4, 2, 4);
            comboBoxWebDriver.Name = "comboBoxWebDriver";
            comboBoxWebDriver.Size = new Size(174, 33);
            comboBoxWebDriver.TabIndex = 90;
            comboBoxWebDriver.SelectedIndexChanged += comboBoxWebDriver_SelectedIndexChanged;
            // 
            // labelWebDriver
            // 
            labelWebDriver.AutoSize = true;
            labelWebDriver.BackColor = Color.Transparent;
            labelWebDriver.ForeColor = Color.White;
            labelWebDriver.Location = new Point(12, 176);
            labelWebDriver.Margin = new Padding(2, 0, 2, 0);
            labelWebDriver.Name = "labelWebDriver";
            labelWebDriver.Size = new Size(99, 25);
            labelWebDriver.TabIndex = 15;
            labelWebDriver.Text = "WebDriver:";
            // 
            // labelWebDriverVersion
            // 
            labelWebDriverVersion.AutoSize = true;
            labelWebDriverVersion.BackColor = Color.Transparent;
            labelWebDriverVersion.ForeColor = Color.White;
            labelWebDriverVersion.Location = new Point(121, 210);
            labelWebDriverVersion.Margin = new Padding(2, 0, 2, 0);
            labelWebDriverVersion.Name = "labelWebDriverVersion";
            labelWebDriverVersion.Size = new Size(156, 25);
            labelWebDriverVersion.TabIndex = 16;
            labelWebDriverVersion.Text = "xxxxxxxxxxxxxxxxxx";
            // 
            // comboBoxLanguage
            // 
            comboBoxLanguage.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxLanguage.FormattingEnabled = true;
            comboBoxLanguage.Location = new Point(412, 119);
            comboBoxLanguage.Margin = new Padding(4);
            comboBoxLanguage.Name = "comboBoxLanguage";
            comboBoxLanguage.Size = new Size(174, 33);
            comboBoxLanguage.TabIndex = 80;
            // 
            // labelLanguage
            // 
            labelLanguage.AutoSize = true;
            labelLanguage.BackColor = Color.Transparent;
            labelLanguage.ForeColor = Color.White;
            labelLanguage.Location = new Point(311, 125);
            labelLanguage.Margin = new Padding(2, 0, 2, 0);
            labelLanguage.Name = "labelLanguage";
            labelLanguage.Size = new Size(93, 25);
            labelLanguage.TabIndex = 18;
            labelLanguage.Text = "Language:";
            // 
            // backgroundWorker1
            // 
            backgroundWorker1.WorkerSupportsCancellation = true;
            backgroundWorker1.DoWork += backgroundWorker1_DoWork;
            // 
            // btnCancel
            // 
            btnCancel.BackColor = Color.Gray;
            btnCancel.Enabled = false;
            btnCancel.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnCancel.ForeColor = Color.White;
            btnCancel.Location = new Point(12, 256);
            btnCancel.Margin = new Padding(4);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(180, 54);
            btnCancel.TabIndex = 30;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = false;
            btnCancel.Visible = false;
            btnCancel.Click += btnCancel_Click;
            // 
            // textBoxPunchInPM
            // 
            textBoxPunchInPM.Location = new Point(412, 206);
            textBoxPunchInPM.Margin = new Padding(2, 4, 2, 4);
            textBoxPunchInPM.MaxLength = 5;
            textBoxPunchInPM.Name = "textBoxPunchInPM";
            textBoxPunchInPM.Size = new Size(84, 31);
            textBoxPunchInPM.TabIndex = 91;
            textBoxPunchInPM.Text = "12:00";
            textBoxPunchInPM.TextAlign = HorizontalAlignment.Center;
            // 
            // textBoxPunchOutPM
            // 
            textBoxPunchOutPM.Location = new Point(502, 206);
            textBoxPunchOutPM.Margin = new Padding(2, 4, 2, 4);
            textBoxPunchOutPM.MaxLength = 5;
            textBoxPunchOutPM.Name = "textBoxPunchOutPM";
            textBoxPunchOutPM.Size = new Size(84, 31);
            textBoxPunchOutPM.TabIndex = 92;
            textBoxPunchOutPM.Text = "17:00";
            textBoxPunchOutPM.TextAlign = HorizontalAlignment.Center;
            // 
            // FormMain
            // 
            AcceptButton = btnRun;
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources.atoss_504x306;
            BackgroundImageLayout = ImageLayout.Stretch;
            CancelButton = btnCancel;
            ClientSize = new Size(602, 324);
            Controls.Add(textBoxPunchOutPM);
            Controls.Add(textBoxPunchInPM);
            Controls.Add(labelLanguage);
            Controls.Add(comboBoxLanguage);
            Controls.Add(labelWebDriverVersion);
            Controls.Add(labelWebDriver);
            Controls.Add(comboBoxWebDriver);
            Controls.Add(labelClientNo);
            Controls.Add(comboBoxClientNo);
            Controls.Add(labelPunchIn);
            Controls.Add(labelPwd);
            Controls.Add(labelUid);
            Controls.Add(labelUrl);
            Controls.Add(textBoxPunchOutAM);
            Controls.Add(textBoxPunchInAM);
            Controls.Add(textBoxPwd);
            Controls.Add(textBoxUid);
            Controls.Add(textBoxUrl);
            Controls.Add(btnRun);
            Controls.Add(btnCancel);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(4);
            MaximizeBox = false;
            MaximumSize = new Size(624, 380);
            MinimizeBox = false;
            MinimumSize = new Size(624, 380);
            Name = "FormMain";
            Text = "EZAsesAutoType";
            FormClosing += onClosing;
            Load += onLoad;
            Shown += onShown;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnRun;
        private TextBox textBoxUrl;
        private TextBox textBoxUid;
        private TextBox textBoxPwd;
        private TextBox textBoxPunchInAM;
        private TextBox textBoxPunchOutAM;
        private Label labelUrl;
        private Label labelUid;
        private Label labelPwd;
        private Label labelPunchIn;
        private ComboBox comboBoxClientNo;
        private Label labelClientNo;
        private ComboBox comboBoxWebDriver;
        private Label labelWebDriver;
        private Label labelWebDriverVersion;
        private ComboBox comboBoxLanguage;
        private Label labelLanguage;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private Button btnCancel;
        private TextBox textBoxPunchInPM;
        private TextBox textBoxPunchOutPM;
    }
}
