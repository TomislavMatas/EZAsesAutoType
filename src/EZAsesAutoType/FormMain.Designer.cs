//
// File: "FormMain.Designer.cs"
//
// Revision History:
// 2024/04/13:TomislavMatas: Version "1.123.4"
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
            labelPunchIn1 = new Label();
            comboBoxClientNo = new ComboBox();
            labelClientNo = new Label();
            comboBoxWebDriver = new ComboBox();
            labelWebDriverVersion = new Label();
            comboBoxLanguage = new ComboBox();
            labelLanguage = new Label();
            backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            btnCancel = new Button();
            textBoxPunchInPM = new TextBox();
            textBoxPunchOutPM = new TextBox();
            checkBox_DoLogin = new CheckBox();
            checkBox_DoPunch = new CheckBox();
            labelPunchIn2 = new Label();
            label1 = new Label();
            label2 = new Label();
            checkBox_DoLogout = new CheckBox();
            textBox_Deviation = new TextBox();
            label_Deviation = new Label();
            label3 = new Label();
            checkBox_Sso = new CheckBox();
            SuspendLayout();
            // 
            // btnRun
            // 
            btnRun.BackColor = Color.Gray;
            btnRun.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnRun.ForeColor = Color.White;
            btnRun.Location = new Point(251, 188);
            btnRun.Margin = new Padding(1, 2, 1, 2);
            btnRun.Name = "btnRun";
            btnRun.Size = new Size(94, 39);
            btnRun.TabIndex = 30;
            btnRun.Text = "Run";
            btnRun.UseVisualStyleBackColor = false;
            btnRun.Click += btnRun_Click;
            // 
            // textBoxUrl
            // 
            textBoxUrl.Location = new Point(74, 13);
            textBoxUrl.Margin = new Padding(1, 2, 1, 2);
            textBoxUrl.Name = "textBoxUrl";
            textBoxUrl.Size = new Size(335, 23);
            textBoxUrl.TabIndex = 40;
            textBoxUrl.Text = "https://ases.noncd.rz.db.de/SES/html?ClientNo=06";
            // 
            // textBoxUid
            // 
            textBoxUid.Location = new Point(74, 46);
            textBoxUid.Margin = new Padding(1, 2, 1, 2);
            textBoxUid.Name = "textBoxUid";
            textBoxUid.Size = new Size(123, 23);
            textBoxUid.TabIndex = 50;
            textBoxUid.Text = "ChangeIt!";
            // 
            // textBoxPwd
            // 
            textBoxPwd.Location = new Point(287, 46);
            textBoxPwd.Margin = new Padding(1, 2, 1, 2);
            textBoxPwd.Name = "textBoxPwd";
            textBoxPwd.PasswordChar = '*';
            textBoxPwd.Size = new Size(123, 23);
            textBoxPwd.TabIndex = 60;
            textBoxPwd.Text = "ChangeIt!";
            textBoxPwd.UseSystemPasswordChar = true;
            // 
            // textBoxPunchInAM
            // 
            textBoxPunchInAM.Location = new Point(287, 107);
            textBoxPunchInAM.Margin = new Padding(1, 2, 1, 2);
            textBoxPunchInAM.MaxLength = 5;
            textBoxPunchInAM.Name = "textBoxPunchInAM";
            textBoxPunchInAM.Size = new Size(60, 23);
            textBoxPunchInAM.TabIndex = 10;
            textBoxPunchInAM.Text = "08:00";
            textBoxPunchInAM.TextAlign = HorizontalAlignment.Center;
            textBoxPunchInAM.Enter += textBoxPunchInAM_Enter;
            textBoxPunchInAM.Validated += textBoxPunchInAM_Validated;
            // 
            // textBoxPunchOutAM
            // 
            textBoxPunchOutAM.Location = new Point(349, 107);
            textBoxPunchOutAM.Margin = new Padding(1, 2, 1, 2);
            textBoxPunchOutAM.MaxLength = 5;
            textBoxPunchOutAM.Name = "textBoxPunchOutAM";
            textBoxPunchOutAM.Size = new Size(60, 23);
            textBoxPunchOutAM.TabIndex = 20;
            textBoxPunchOutAM.Text = "12:00";
            textBoxPunchOutAM.TextAlign = HorizontalAlignment.Center;
            textBoxPunchOutAM.Enter += textBoxPunchOutAM_Enter;
            textBoxPunchOutAM.Validated += textBoxPunchOutAM_Validated;
            // 
            // labelUrl
            // 
            labelUrl.AutoSize = true;
            labelUrl.BackColor = Color.Transparent;
            labelUrl.ForeColor = Color.White;
            labelUrl.Location = new Point(8, 16);
            labelUrl.Margin = new Padding(1, 0, 1, 0);
            labelUrl.Name = "labelUrl";
            labelUrl.Size = new Size(31, 15);
            labelUrl.TabIndex = 0;
            labelUrl.Text = "URL:";
            // 
            // labelUid
            // 
            labelUid.AutoSize = true;
            labelUid.BackColor = Color.Transparent;
            labelUid.ForeColor = Color.White;
            labelUid.Location = new Point(8, 50);
            labelUid.Margin = new Padding(1, 0, 1, 0);
            labelUid.Name = "labelUid";
            labelUid.Size = new Size(44, 15);
            labelUid.TabIndex = 0;
            labelUid.Text = "UserID:";
            // 
            // labelPwd
            // 
            labelPwd.AutoSize = true;
            labelPwd.BackColor = Color.Transparent;
            labelPwd.ForeColor = Color.White;
            labelPwd.Location = new Point(216, 50);
            labelPwd.Margin = new Padding(1, 0, 1, 0);
            labelPwd.Name = "labelPwd";
            labelPwd.Size = new Size(60, 15);
            labelPwd.TabIndex = 0;
            labelPwd.Text = "Password:";
            // 
            // labelPunchIn1
            // 
            labelPunchIn1.AutoSize = true;
            labelPunchIn1.BackColor = Color.Transparent;
            labelPunchIn1.ForeColor = Color.White;
            labelPunchIn1.Location = new Point(215, 110);
            labelPunchIn1.Margin = new Padding(1, 0, 1, 0);
            labelPunchIn1.Name = "labelPunchIn1";
            labelPunchIn1.Size = new Size(61, 15);
            labelPunchIn1.TabIndex = 0;
            labelPunchIn1.Text = "1. In | Out:";
            // 
            // comboBoxClientNo
            // 
            comboBoxClientNo.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            comboBoxClientNo.AutoCompleteSource = AutoCompleteSource.ListItems;
            comboBoxClientNo.FormattingEnabled = true;
            comboBoxClientNo.Location = new Point(74, 75);
            comboBoxClientNo.Margin = new Padding(1, 2, 1, 2);
            comboBoxClientNo.Name = "comboBoxClientNo";
            comboBoxClientNo.Size = new Size(123, 23);
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
            labelClientNo.Location = new Point(8, 79);
            labelClientNo.Margin = new Padding(1, 0, 1, 0);
            labelClientNo.Name = "labelClientNo";
            labelClientNo.Size = new Size(41, 15);
            labelClientNo.TabIndex = 0;
            labelClientNo.Text = "Client:";
            // 
            // comboBoxWebDriver
            // 
            comboBoxWebDriver.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxWebDriver.FormattingEnabled = true;
            comboBoxWebDriver.Location = new Point(74, 191);
            comboBoxWebDriver.Margin = new Padding(1, 2, 1, 2);
            comboBoxWebDriver.Name = "comboBoxWebDriver";
            comboBoxWebDriver.Size = new Size(123, 23);
            comboBoxWebDriver.TabIndex = 90;
            comboBoxWebDriver.SelectedIndexChanged += comboBoxWebDriver_SelectedIndexChanged;
            // 
            // labelWebDriverVersion
            // 
            labelWebDriverVersion.AutoSize = true;
            labelWebDriverVersion.BackColor = Color.Transparent;
            labelWebDriverVersion.Font = new Font("Segoe UI", 9F);
            labelWebDriverVersion.ForeColor = Color.White;
            labelWebDriverVersion.Location = new Point(73, 216);
            labelWebDriverVersion.Margin = new Padding(1, 0, 1, 0);
            labelWebDriverVersion.Name = "labelWebDriverVersion";
            labelWebDriverVersion.Size = new Size(115, 15);
            labelWebDriverVersion.TabIndex = 0;
            labelWebDriverVersion.Text = "xxxxxxxxxxxxxxxxxx";
            // 
            // comboBoxLanguage
            // 
            comboBoxLanguage.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxLanguage.FormattingEnabled = true;
            comboBoxLanguage.Location = new Point(287, 75);
            comboBoxLanguage.Margin = new Padding(3, 2, 3, 2);
            comboBoxLanguage.Name = "comboBoxLanguage";
            comboBoxLanguage.Size = new Size(123, 23);
            comboBoxLanguage.TabIndex = 80;
            // 
            // labelLanguage
            // 
            labelLanguage.AutoSize = true;
            labelLanguage.BackColor = Color.Transparent;
            labelLanguage.ForeColor = Color.White;
            labelLanguage.Location = new Point(216, 79);
            labelLanguage.Margin = new Padding(1, 0, 1, 0);
            labelLanguage.Name = "labelLanguage";
            labelLanguage.Size = new Size(62, 15);
            labelLanguage.TabIndex = 0;
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
            btnCancel.Location = new Point(352, 188);
            btnCancel.Margin = new Padding(3, 2, 3, 2);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(94, 39);
            btnCancel.TabIndex = 35;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = false;
            btnCancel.Visible = false;
            btnCancel.Click += btnCancel_Click;
            // 
            // textBoxPunchInPM
            // 
            textBoxPunchInPM.Location = new Point(287, 131);
            textBoxPunchInPM.Margin = new Padding(1, 2, 1, 2);
            textBoxPunchInPM.MaxLength = 21;
            textBoxPunchInPM.Name = "textBoxPunchInPM";
            textBoxPunchInPM.Size = new Size(60, 23);
            textBoxPunchInPM.TabIndex = 21;
            textBoxPunchInPM.Text = "13:00";
            textBoxPunchInPM.TextAlign = HorizontalAlignment.Center;
            textBoxPunchInPM.Enter += textBoxPunchInPM_Enter;
            textBoxPunchInPM.Validated += textBoxPunchInPM_Validated;
            // 
            // textBoxPunchOutPM
            // 
            textBoxPunchOutPM.Location = new Point(349, 131);
            textBoxPunchOutPM.Margin = new Padding(1, 2, 1, 2);
            textBoxPunchOutPM.MaxLength = 22;
            textBoxPunchOutPM.Name = "textBoxPunchOutPM";
            textBoxPunchOutPM.Size = new Size(60, 23);
            textBoxPunchOutPM.TabIndex = 22;
            textBoxPunchOutPM.Text = "17:00";
            textBoxPunchOutPM.TextAlign = HorizontalAlignment.Center;
            textBoxPunchOutPM.Enter += textBoxPunchOutPM_Enter;
            textBoxPunchOutPM.Validated += textBoxPunchOutPM_Validated;
            // 
            // checkBox_DoLogin
            // 
            checkBox_DoLogin.AutoSize = true;
            checkBox_DoLogin.BackColor = Color.Transparent;
            checkBox_DoLogin.FlatAppearance.BorderSize = 2;
            checkBox_DoLogin.ForeColor = Color.White;
            checkBox_DoLogin.Location = new Point(74, 107);
            checkBox_DoLogin.MinimumSize = new Size(123, 23);
            checkBox_DoLogin.Name = "checkBox_DoLogin";
            checkBox_DoLogin.Size = new Size(123, 23);
            checkBox_DoLogin.TabIndex = 85;
            checkBox_DoLogin.Text = "Do Login";
            checkBox_DoLogin.UseVisualStyleBackColor = false;
            checkBox_DoLogin.CheckedChanged += checkBox_DoLogin_CheckedChanged;
            // 
            // checkBox_DoPunch
            // 
            checkBox_DoPunch.AutoSize = true;
            checkBox_DoPunch.BackColor = Color.Transparent;
            checkBox_DoPunch.ForeColor = Color.White;
            checkBox_DoPunch.Location = new Point(74, 131);
            checkBox_DoPunch.MinimumSize = new Size(123, 23);
            checkBox_DoPunch.Name = "checkBox_DoPunch";
            checkBox_DoPunch.Size = new Size(123, 23);
            checkBox_DoPunch.TabIndex = 86;
            checkBox_DoPunch.Text = "Do Punch";
            checkBox_DoPunch.UseVisualStyleBackColor = false;
            checkBox_DoPunch.CheckedChanged += checkBox_DoPunch_CheckedChanged;
            // 
            // labelPunchIn2
            // 
            labelPunchIn2.AutoSize = true;
            labelPunchIn2.BackColor = Color.Transparent;
            labelPunchIn2.ForeColor = Color.White;
            labelPunchIn2.Location = new Point(215, 134);
            labelPunchIn2.Margin = new Padding(1, 0, 1, 0);
            labelPunchIn2.Name = "labelPunchIn2";
            labelPunchIn2.Size = new Size(61, 15);
            labelPunchIn2.TabIndex = 95;
            labelPunchIn2.Text = "2. In | Out:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.Transparent;
            label1.ForeColor = Color.White;
            label1.Location = new Point(8, 194);
            label1.Margin = new Padding(1, 0, 1, 0);
            label1.Name = "label1";
            label1.Size = new Size(52, 15);
            label1.TabIndex = 96;
            label1.Text = "Browser:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.Transparent;
            label2.Font = new Font("Segoe UI", 9F);
            label2.ForeColor = Color.White;
            label2.Location = new Point(8, 216);
            label2.Margin = new Padding(1, 0, 1, 0);
            label2.Name = "label2";
            label2.Size = new Size(41, 15);
            label2.TabIndex = 97;
            label2.Text = "Driver:";
            // 
            // checkBox_DoLogout
            // 
            checkBox_DoLogout.AutoSize = true;
            checkBox_DoLogout.BackColor = Color.Transparent;
            checkBox_DoLogout.ForeColor = Color.White;
            checkBox_DoLogout.Location = new Point(74, 155);
            checkBox_DoLogout.MinimumSize = new Size(123, 23);
            checkBox_DoLogout.Name = "checkBox_DoLogout";
            checkBox_DoLogout.Size = new Size(123, 23);
            checkBox_DoLogout.TabIndex = 87;
            checkBox_DoLogout.Text = "Do Logout";
            checkBox_DoLogout.UseVisualStyleBackColor = false;
            checkBox_DoLogout.CheckedChanged += checkBox_DoLogout_CheckedChanged;
            // 
            // textBox_Deviation
            // 
            textBox_Deviation.Location = new Point(287, 155);
            textBox_Deviation.Margin = new Padding(1, 2, 1, 2);
            textBox_Deviation.MaxLength = 2;
            textBox_Deviation.Name = "textBox_Deviation";
            textBox_Deviation.Size = new Size(60, 23);
            textBox_Deviation.TabIndex = 23;
            textBox_Deviation.Text = "60";
            textBox_Deviation.TextAlign = HorizontalAlignment.Center;
            textBox_Deviation.TextChanged += textBox_Deviation_TextChanged;
            textBox_Deviation.Validated += textBox_Deviation_Validated;
            // 
            // label_Deviation
            // 
            label_Deviation.AutoSize = true;
            label_Deviation.BackColor = Color.Transparent;
            label_Deviation.ForeColor = Color.White;
            label_Deviation.Location = new Point(215, 158);
            label_Deviation.Margin = new Padding(1, 0, 1, 0);
            label_Deviation.Name = "label_Deviation";
            label_Deviation.Size = new Size(60, 15);
            label_Deviation.TabIndex = 99;
            label_Deviation.Text = "Deviation:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = Color.Transparent;
            label3.ForeColor = Color.White;
            label3.Location = new Point(349, 158);
            label3.Margin = new Padding(1, 0, 1, 0);
            label3.Name = "label3";
            label3.Size = new Size(50, 15);
            label3.TabIndex = 100;
            label3.Text = "minutes";
            // 
            // checkBox_Sso
            // 
            checkBox_Sso.AutoSize = true;
            checkBox_Sso.BackColor = Color.Transparent;
            checkBox_Sso.FlatAppearance.BorderSize = 2;
            checkBox_Sso.ForeColor = Color.White;
            checkBox_Sso.Location = new Point(413, 13);
            checkBox_Sso.MinimumSize = new Size(50, 23);
            checkBox_Sso.Name = "checkBox_Sso";
            checkBox_Sso.Size = new Size(50, 23);
            checkBox_Sso.TabIndex = 41;
            checkBox_Sso.Text = "SSO";
            checkBox_Sso.UseVisualStyleBackColor = false;
            checkBox_Sso.CheckedChanged += checkBox_Sso_CheckedChanged;
            // 
            // FormMain
            // 
            AcceptButton = btnRun;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources.atoss_504x306;
            BackgroundImageLayout = ImageLayout.Stretch;
            CancelButton = btnCancel;
            ClientSize = new Size(467, 244);
            Controls.Add(checkBox_Sso);
            Controls.Add(label3);
            Controls.Add(label_Deviation);
            Controls.Add(textBox_Deviation);
            Controls.Add(checkBox_DoLogout);
            Controls.Add(label2);
            Controls.Add(comboBoxWebDriver);
            Controls.Add(label1);
            Controls.Add(labelPunchIn2);
            Controls.Add(checkBox_DoPunch);
            Controls.Add(checkBox_DoLogin);
            Controls.Add(textBoxPunchOutPM);
            Controls.Add(textBoxPunchInPM);
            Controls.Add(labelLanguage);
            Controls.Add(comboBoxLanguage);
            Controls.Add(labelClientNo);
            Controls.Add(comboBoxClientNo);
            Controls.Add(labelPunchIn1);
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
            Controls.Add(labelWebDriverVersion);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(3, 2, 3, 2);
            MaximizeBox = false;
            MinimizeBox = false;
            MinimumSize = new Size(440, 272);
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
        private Label labelPunchIn1;
        private ComboBox comboBoxClientNo;
        private Label labelClientNo;
        private ComboBox comboBoxWebDriver;
        private Label labelWebDriverVersion;
        private ComboBox comboBoxLanguage;
        private Label labelLanguage;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private Button btnCancel;
        private TextBox textBoxPunchInPM;
        private TextBox textBoxPunchOutPM;
        private CheckBox checkBox_DoLogin;
        private CheckBox checkBox_DoPunch;
        private Label labelPunchIn2;
        private Label label1;
        private Label label2;
        private CheckBox checkBox_DoLogout;
        private TextBox textBox_Deviation;
        private Label label_Deviation;
        private Label label3;
        private CheckBox checkBox_Sso;
    }
}
