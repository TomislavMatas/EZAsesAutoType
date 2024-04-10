//
// File: "FormMain.Designer.cs"
//
// Revision History:
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
            textBoxPunchIn = new TextBox();
            textBoxPunchOut = new TextBox();
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
            textBox1 = new TextBox();
            textBox2 = new TextBox();
            SuspendLayout();
            // 
            // btnRun
            // 
            btnRun.BackColor = Color.Gray;
            btnRun.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnRun.ForeColor = Color.White;
            btnRun.Location = new Point(331, 205);
            btnRun.Margin = new Padding(2, 3, 2, 3);
            btnRun.Name = "btnRun";
            btnRun.Size = new Size(144, 43);
            btnRun.TabIndex = 30;
            btnRun.Text = "Run";
            btnRun.UseVisualStyleBackColor = false;
            btnRun.Click += btnRun_Click;
            // 
            // textBoxUrl
            // 
            textBoxUrl.Location = new Point(97, 17);
            textBoxUrl.Margin = new Padding(2, 3, 2, 3);
            textBoxUrl.Name = "textBoxUrl";
            textBoxUrl.Size = new Size(373, 27);
            textBoxUrl.TabIndex = 40;
            textBoxUrl.Text = "https://ases.noncd.rz.db.de/SES/html?ClientNo=06";
            // 
            // textBoxUid
            // 
            textBoxUid.Location = new Point(97, 56);
            textBoxUid.Margin = new Padding(2, 3, 2, 3);
            textBoxUid.Name = "textBoxUid";
            textBoxUid.Size = new Size(140, 27);
            textBoxUid.TabIndex = 50;
            textBoxUid.Text = "ChangeIt!";
            // 
            // textBoxPwd
            // 
            textBoxPwd.Location = new Point(330, 56);
            textBoxPwd.Margin = new Padding(2, 3, 2, 3);
            textBoxPwd.Name = "textBoxPwd";
            textBoxPwd.PasswordChar = '*';
            textBoxPwd.Size = new Size(140, 27);
            textBoxPwd.TabIndex = 60;
            textBoxPwd.Text = "ChangeIt!";
            textBoxPwd.UseSystemPasswordChar = true;
            // 
            // textBoxPunchIn
            // 
            textBoxPunchIn.Location = new Point(330, 137);
            textBoxPunchIn.Margin = new Padding(2, 3, 2, 3);
            textBoxPunchIn.MaxLength = 5;
            textBoxPunchIn.Name = "textBoxPunchIn";
            textBoxPunchIn.Size = new Size(70, 27);
            textBoxPunchIn.TabIndex = 10;
            textBoxPunchIn.Text = "09:00";
            textBoxPunchIn.TextAlign = HorizontalAlignment.Center;
            textBoxPunchIn.TextChanged += textBoxPunchIn_TextChanged;
            textBoxPunchIn.Validated += textBoxPunchIn_Validated;
            // 
            // textBoxPunchOut
            // 
            textBoxPunchOut.Location = new Point(403, 137);
            textBoxPunchOut.Margin = new Padding(2, 3, 2, 3);
            textBoxPunchOut.MaxLength = 5;
            textBoxPunchOut.Name = "textBoxPunchOut";
            textBoxPunchOut.Size = new Size(67, 27);
            textBoxPunchOut.TabIndex = 20;
            textBoxPunchOut.Text = "12:00";
            textBoxPunchOut.TextAlign = HorizontalAlignment.Center;
            textBoxPunchOut.TextChanged += textBoxPunchOut_TextChanged;
            textBoxPunchOut.Validated += textBoxPunchOut_Validated;
            // 
            // labelUrl
            // 
            labelUrl.AutoSize = true;
            labelUrl.BackColor = Color.Transparent;
            labelUrl.ForeColor = Color.White;
            labelUrl.Location = new Point(10, 21);
            labelUrl.Margin = new Padding(2, 0, 2, 0);
            labelUrl.Name = "labelUrl";
            labelUrl.Size = new Size(38, 20);
            labelUrl.TabIndex = 7;
            labelUrl.Text = "URL:";
            // 
            // labelUid
            // 
            labelUid.AutoSize = true;
            labelUid.BackColor = Color.Transparent;
            labelUid.ForeColor = Color.White;
            labelUid.Location = new Point(10, 61);
            labelUid.Margin = new Padding(2, 0, 2, 0);
            labelUid.Name = "labelUid";
            labelUid.Size = new Size(56, 20);
            labelUid.TabIndex = 8;
            labelUid.Text = "UserID:";
            // 
            // labelPwd
            // 
            labelPwd.AutoSize = true;
            labelPwd.BackColor = Color.Transparent;
            labelPwd.ForeColor = Color.White;
            labelPwd.Location = new Point(249, 61);
            labelPwd.Margin = new Padding(2, 0, 2, 0);
            labelPwd.Name = "labelPwd";
            labelPwd.Size = new Size(73, 20);
            labelPwd.TabIndex = 9;
            labelPwd.Text = "Password:";
            // 
            // labelPunchIn
            // 
            labelPunchIn.AutoSize = true;
            labelPunchIn.BackColor = Color.Transparent;
            labelPunchIn.ForeColor = Color.White;
            labelPunchIn.Location = new Point(249, 140);
            labelPunchIn.Margin = new Padding(2, 0, 2, 0);
            labelPunchIn.Name = "labelPunchIn";
            labelPunchIn.Size = new Size(62, 20);
            labelPunchIn.TabIndex = 10;
            labelPunchIn.Text = "In / Out:";
            // 
            // comboBoxClientNo
            // 
            comboBoxClientNo.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            comboBoxClientNo.AutoCompleteSource = AutoCompleteSource.ListItems;
            comboBoxClientNo.FormattingEnabled = true;
            comboBoxClientNo.Location = new Point(97, 95);
            comboBoxClientNo.Margin = new Padding(2, 3, 2, 3);
            comboBoxClientNo.Name = "comboBoxClientNo";
            comboBoxClientNo.Size = new Size(140, 28);
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
            labelClientNo.Location = new Point(10, 100);
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
            comboBoxWebDriver.Location = new Point(97, 137);
            comboBoxWebDriver.Margin = new Padding(2, 3, 2, 3);
            comboBoxWebDriver.Name = "comboBoxWebDriver";
            comboBoxWebDriver.Size = new Size(140, 28);
            comboBoxWebDriver.TabIndex = 90;
            comboBoxWebDriver.SelectedIndexChanged += comboBoxWebDriver_SelectedIndexChanged;
            // 
            // labelWebDriver
            // 
            labelWebDriver.AutoSize = true;
            labelWebDriver.BackColor = Color.Transparent;
            labelWebDriver.ForeColor = Color.White;
            labelWebDriver.Location = new Point(10, 141);
            labelWebDriver.Margin = new Padding(2, 0, 2, 0);
            labelWebDriver.Name = "labelWebDriver";
            labelWebDriver.Size = new Size(82, 20);
            labelWebDriver.TabIndex = 15;
            labelWebDriver.Text = "WebDriver:";
            // 
            // labelWebDriverVersion
            // 
            labelWebDriverVersion.AutoSize = true;
            labelWebDriverVersion.BackColor = Color.Transparent;
            labelWebDriverVersion.ForeColor = Color.White;
            labelWebDriverVersion.Location = new Point(97, 168);
            labelWebDriverVersion.Margin = new Padding(2, 0, 2, 0);
            labelWebDriverVersion.Name = "labelWebDriverVersion";
            labelWebDriverVersion.Size = new Size(135, 20);
            labelWebDriverVersion.TabIndex = 16;
            labelWebDriverVersion.Text = "xxxxxxxxxxxxxxxxxx";
            // 
            // comboBoxLanguage
            // 
            comboBoxLanguage.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxLanguage.FormattingEnabled = true;
            comboBoxLanguage.Location = new Point(330, 95);
            comboBoxLanguage.Name = "comboBoxLanguage";
            comboBoxLanguage.Size = new Size(140, 28);
            comboBoxLanguage.TabIndex = 80;
            // 
            // labelLanguage
            // 
            labelLanguage.AutoSize = true;
            labelLanguage.BackColor = Color.Transparent;
            labelLanguage.ForeColor = Color.White;
            labelLanguage.Location = new Point(249, 100);
            labelLanguage.Margin = new Padding(2, 0, 2, 0);
            labelLanguage.Name = "labelLanguage";
            labelLanguage.Size = new Size(77, 20);
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
            btnCancel.Location = new Point(10, 205);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(144, 43);
            btnCancel.TabIndex = 30;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = false;
            btnCancel.Visible = false;
            btnCancel.Click += btnCancel_Click;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(330, 165);
            textBox1.Margin = new Padding(2, 3, 2, 3);
            textBox1.MaxLength = 5;
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(70, 27);
            textBox1.TabIndex = 91;
            textBox1.Text = "12:00";
            textBox1.TextAlign = HorizontalAlignment.Center;
            // 
            // textBox2
            // 
            textBox2.Location = new Point(403, 165);
            textBox2.Margin = new Padding(2, 3, 2, 3);
            textBox2.MaxLength = 5;
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(67, 27);
            textBox2.TabIndex = 92;
            textBox2.Text = "17:00";
            textBox2.TextAlign = HorizontalAlignment.Center;
            // 
            // FormMain
            // 
            AcceptButton = btnRun;
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources.atoss_504x306;
            BackgroundImageLayout = ImageLayout.Stretch;
            CancelButton = btnCancel;
            ClientSize = new Size(486, 256);
            Controls.Add(textBox2);
            Controls.Add(textBox1);
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
            Controls.Add(textBoxPunchOut);
            Controls.Add(textBoxPunchIn);
            Controls.Add(textBoxPwd);
            Controls.Add(textBoxUid);
            Controls.Add(textBoxUrl);
            Controls.Add(btnRun);
            Controls.Add(btnCancel);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MaximumSize = new Size(504, 303);
            MinimizeBox = false;
            MinimumSize = new Size(504, 303);
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
        private TextBox textBoxPunchIn;
        private TextBox textBoxPunchOut;
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
        private TextBox textBox1;
        private TextBox textBox2;
    }
}
