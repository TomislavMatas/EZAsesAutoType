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
            SuspendLayout();
            // 
            // btnRun
            // 
            btnRun.Location = new Point(702, 100);
            btnRun.Name = "btnRun";
            btnRun.Size = new Size(176, 40);
            btnRun.TabIndex = 0;
            btnRun.Text = "Run";
            btnRun.UseVisualStyleBackColor = true;
            btnRun.Click += btnRun_Click;
            // 
            // textBoxUrl
            // 
            textBoxUrl.Location = new Point(110, 12);
            textBoxUrl.Name = "textBoxUrl";
            textBoxUrl.Size = new Size(564, 31);
            textBoxUrl.TabIndex = 1;
            textBoxUrl.TextChanged += textBox1_BaseUrl_TextChanged;
            // 
            // textBoxUid
            // 
            textBoxUid.Location = new Point(110, 102);
            textBoxUid.Name = "textBoxUid";
            textBoxUid.Size = new Size(219, 31);
            textBoxUid.TabIndex = 3;
            textBoxUid.TextChanged += textBox1_Uid_TextChanged;
            // 
            // textBoxPwd
            // 
            textBoxPwd.Location = new Point(455, 105);
            textBoxPwd.Name = "textBoxPwd";
            textBoxPwd.Size = new Size(219, 31);
            textBoxPwd.TabIndex = 4;
            // 
            // textBoxPunchIn
            // 
            textBoxPunchIn.Location = new Point(800, 12);
            textBoxPunchIn.Name = "textBoxPunchIn";
            textBoxPunchIn.Size = new Size(78, 31);
            textBoxPunchIn.TabIndex = 5;
            textBoxPunchIn.Text = "09:00";
            textBoxPunchIn.TextAlign = HorizontalAlignment.Center;
            // 
            // textBoxPunchOut
            // 
            textBoxPunchOut.Location = new Point(800, 49);
            textBoxPunchOut.Name = "textBoxPunchOut";
            textBoxPunchOut.Size = new Size(78, 31);
            textBoxPunchOut.TabIndex = 6;
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
            labelUid.Location = new Point(12, 105);
            labelUid.Name = "labelUid";
            labelUid.Size = new Size(69, 25);
            labelUid.TabIndex = 8;
            labelUid.Text = "UserID:";
            // 
            // labelPwd
            // 
            labelPwd.AutoSize = true;
            labelPwd.Location = new Point(357, 108);
            labelPwd.Name = "labelPwd";
            labelPwd.Size = new Size(91, 25);
            labelPwd.TabIndex = 9;
            labelPwd.Text = "Password:";
            // 
            // labelPunchIn
            // 
            labelPunchIn.AutoSize = true;
            labelPunchIn.Location = new Point(702, 15);
            labelPunchIn.Name = "labelPunchIn";
            labelPunchIn.Size = new Size(86, 25);
            labelPunchIn.TabIndex = 10;
            labelPunchIn.Text = "Punch-In:";
            // 
            // labelPunchOut
            // 
            labelPunchOut.AutoSize = true;
            labelPunchOut.Location = new Point(692, 52);
            labelPunchOut.Name = "labelPunchOut";
            labelPunchOut.Size = new Size(101, 25);
            labelPunchOut.TabIndex = 11;
            labelPunchOut.Text = "Punch-Out:";
            // 
            // comboBoxClientNo
            // 
            comboBoxClientNo.FormattingEnabled = true;
            comboBoxClientNo.Location = new Point(110, 49);
            comboBoxClientNo.Name = "comboBoxClientNo";
            comboBoxClientNo.Size = new Size(564, 33);
            comboBoxClientNo.TabIndex = 2;
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
            // FormMain
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(899, 157);
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
            MinimizeBox = false;
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
    }
}
