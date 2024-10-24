namespace StudentInformationEntrySystem
{
    partial class LoginForm
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
            splitContainerLoginForm = new SplitContainer();
            lblFormTitle = new Label();
            tabControlLoginForm = new TabControl();
            tabPageLogin = new TabPage();
            btnLogin = new Button();
            checkBoxShowPasswordLogin = new CheckBox();
            txtBoxLoginPasswd = new TextBox();
            txtBoxLoginUsername = new TextBox();
            progressBarLogin = new ProgressBar();
            lblLoginPasswd = new Label();
            lblLoginUsername = new Label();
            tabPageRegister = new TabPage();
            txtBoxRegSchoolId = new TextBox();
            lblRegSchoolId = new Label();
            btnRegister = new Button();
            checkBoxShowPasswordRegister = new CheckBox();
            txtBoxRegPasswd = new TextBox();
            txtBoxRegUsername = new TextBox();
            lblRegPasswd = new Label();
            lblRegUsername = new Label();
            progressBarRegister = new ProgressBar();
            tabPageForget = new TabPage();
            txtBoxForgetSchoolId = new TextBox();
            lblForgetSchoolId = new Label();
            btnForget = new Button();
            progressBarForget = new ProgressBar();
            ((System.ComponentModel.ISupportInitialize)splitContainerLoginForm).BeginInit();
            splitContainerLoginForm.Panel1.SuspendLayout();
            splitContainerLoginForm.Panel2.SuspendLayout();
            splitContainerLoginForm.SuspendLayout();
            tabControlLoginForm.SuspendLayout();
            tabPageLogin.SuspendLayout();
            tabPageRegister.SuspendLayout();
            tabPageForget.SuspendLayout();
            SuspendLayout();
            // 
            // splitContainerLoginForm
            // 
            splitContainerLoginForm.Dock = DockStyle.Fill;
            splitContainerLoginForm.IsSplitterFixed = true;
            splitContainerLoginForm.Location = new Point(0, 0);
            splitContainerLoginForm.Name = "splitContainerLoginForm";
            splitContainerLoginForm.Orientation = Orientation.Horizontal;
            // 
            // splitContainerLoginForm.Panel1
            // 
            splitContainerLoginForm.Panel1.Controls.Add(lblFormTitle);
            splitContainerLoginForm.Panel1.Paint += splitContainer1_Panel1_Paint;
            // 
            // splitContainerLoginForm.Panel2
            // 
            splitContainerLoginForm.Panel2.Controls.Add(tabControlLoginForm);
            splitContainerLoginForm.Size = new Size(542, 534);
            splitContainerLoginForm.SplitterDistance = 63;
            splitContainerLoginForm.SplitterWidth = 1;
            splitContainerLoginForm.TabIndex = 0;
            // 
            // lblFormTitle
            // 
            lblFormTitle.AutoSize = true;
            lblFormTitle.Font = new Font("Microsoft YaHei UI", 22F, FontStyle.Bold, GraphicsUnit.Point, 134);
            lblFormTitle.Location = new Point(143, 13);
            lblFormTitle.Name = "lblFormTitle";
            lblFormTitle.Size = new Size(257, 40);
            lblFormTitle.TabIndex = 0;
            lblFormTitle.Text = "学生信息管理系统";
            // 
            // tabControlLoginForm
            // 
            tabControlLoginForm.Controls.Add(tabPageLogin);
            tabControlLoginForm.Controls.Add(tabPageRegister);
            tabControlLoginForm.Controls.Add(tabPageForget);
            tabControlLoginForm.Dock = DockStyle.Fill;
            tabControlLoginForm.Font = new Font("Microsoft YaHei UI", 13F);
            tabControlLoginForm.Location = new Point(0, 0);
            tabControlLoginForm.Name = "tabControlLoginForm";
            tabControlLoginForm.SelectedIndex = 0;
            tabControlLoginForm.Size = new Size(542, 470);
            tabControlLoginForm.TabIndex = 0;
            // 
            // tabPageLogin
            // 
            tabPageLogin.Controls.Add(btnLogin);
            tabPageLogin.Controls.Add(checkBoxShowPasswordLogin);
            tabPageLogin.Controls.Add(txtBoxLoginPasswd);
            tabPageLogin.Controls.Add(txtBoxLoginUsername);
            tabPageLogin.Controls.Add(progressBarLogin);
            tabPageLogin.Controls.Add(lblLoginPasswd);
            tabPageLogin.Controls.Add(lblLoginUsername);
            tabPageLogin.Location = new Point(4, 32);
            tabPageLogin.Name = "tabPageLogin";
            tabPageLogin.Padding = new Padding(3);
            tabPageLogin.Size = new Size(534, 434);
            tabPageLogin.TabIndex = 1;
            tabPageLogin.Text = "登入系统";
            tabPageLogin.UseVisualStyleBackColor = true;
            // 
            // btnLogin
            // 
            btnLogin.Location = new Point(196, 289);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new Size(143, 53);
            btnLogin.TabIndex = 5;
            btnLogin.Text = "登录系统";
            btnLogin.UseVisualStyleBackColor = true;
            btnLogin.Click += btnLogin_Click;
            // 
            // checkBoxShowPasswordLogin
            // 
            checkBoxShowPasswordLogin.AutoSize = true;
            checkBoxShowPasswordLogin.Font = new Font("Microsoft YaHei UI", 10F);
            checkBoxShowPasswordLogin.Location = new Point(378, 197);
            checkBoxShowPasswordLogin.Name = "checkBoxShowPasswordLogin";
            checkBoxShowPasswordLogin.Size = new Size(84, 24);
            checkBoxShowPasswordLogin.TabIndex = 4;
            checkBoxShowPasswordLogin.Text = "显示密码";
            checkBoxShowPasswordLogin.UseVisualStyleBackColor = true;
            checkBoxShowPasswordLogin.CheckedChanged += checkBoxShowPasswordLogin_CheckedChanged;
            // 
            // txtBoxLoginPasswd
            // 
            txtBoxLoginPasswd.Location = new Point(163, 149);
            txtBoxLoginPasswd.Name = "txtBoxLoginPasswd";
            txtBoxLoginPasswd.Size = new Size(299, 30);
            txtBoxLoginPasswd.TabIndex = 3;
            txtBoxLoginPasswd.UseSystemPasswordChar = true;
            // 
            // txtBoxLoginUsername
            // 
            txtBoxLoginUsername.Location = new Point(163, 90);
            txtBoxLoginUsername.Name = "txtBoxLoginUsername";
            txtBoxLoginUsername.Size = new Size(299, 30);
            txtBoxLoginUsername.TabIndex = 1;
            // 
            // progressBarLogin
            // 
            progressBarLogin.Dock = DockStyle.Bottom;
            progressBarLogin.Location = new Point(3, 400);
            progressBarLogin.Name = "progressBarLogin";
            progressBarLogin.Size = new Size(528, 31);
            progressBarLogin.TabIndex = 6;
            // 
            // lblLoginPasswd
            // 
            lblLoginPasswd.AutoSize = true;
            lblLoginPasswd.Location = new Point(80, 152);
            lblLoginPasswd.Name = "lblLoginPasswd";
            lblLoginPasswd.Size = new Size(64, 24);
            lblLoginPasswd.TabIndex = 2;
            lblLoginPasswd.Text = "密码：";
            // 
            // lblLoginUsername
            // 
            lblLoginUsername.AutoSize = true;
            lblLoginUsername.Location = new Point(62, 92);
            lblLoginUsername.Name = "lblLoginUsername";
            lblLoginUsername.Size = new Size(82, 24);
            lblLoginUsername.TabIndex = 0;
            lblLoginUsername.Text = "用户名：";
            // 
            // tabPageRegister
            // 
            tabPageRegister.Controls.Add(txtBoxRegSchoolId);
            tabPageRegister.Controls.Add(lblRegSchoolId);
            tabPageRegister.Controls.Add(btnRegister);
            tabPageRegister.Controls.Add(checkBoxShowPasswordRegister);
            tabPageRegister.Controls.Add(txtBoxRegPasswd);
            tabPageRegister.Controls.Add(txtBoxRegUsername);
            tabPageRegister.Controls.Add(lblRegPasswd);
            tabPageRegister.Controls.Add(lblRegUsername);
            tabPageRegister.Controls.Add(progressBarRegister);
            tabPageRegister.Location = new Point(4, 32);
            tabPageRegister.Name = "tabPageRegister";
            tabPageRegister.Padding = new Padding(3);
            tabPageRegister.Size = new Size(534, 434);
            tabPageRegister.TabIndex = 2;
            tabPageRegister.Text = "注册系统";
            tabPageRegister.UseVisualStyleBackColor = true;
            // 
            // txtBoxRegSchoolId
            // 
            txtBoxRegSchoolId.Location = new Point(168, 60);
            txtBoxRegSchoolId.Name = "txtBoxRegSchoolId";
            txtBoxRegSchoolId.Size = new Size(299, 30);
            txtBoxRegSchoolId.TabIndex = 1;
            // 
            // lblRegSchoolId
            // 
            lblRegSchoolId.AutoSize = true;
            lblRegSchoolId.Location = new Point(67, 62);
            lblRegSchoolId.Name = "lblRegSchoolId";
            lblRegSchoolId.Size = new Size(83, 24);
            lblRegSchoolId.TabIndex = 0;
            lblRegSchoolId.Text = "学校ID：";
            // 
            // btnRegister
            // 
            btnRegister.Location = new Point(196, 289);
            btnRegister.Name = "btnRegister";
            btnRegister.Size = new Size(143, 53);
            btnRegister.TabIndex = 7;
            btnRegister.Text = "注册系统";
            btnRegister.UseVisualStyleBackColor = true;
            btnRegister.Click += btnRegister_Click;
            // 
            // checkBoxShowPasswordRegister
            // 
            checkBoxShowPasswordRegister.AutoSize = true;
            checkBoxShowPasswordRegister.Font = new Font("Microsoft YaHei UI", 10F);
            checkBoxShowPasswordRegister.Location = new Point(383, 225);
            checkBoxShowPasswordRegister.Name = "checkBoxShowPasswordRegister";
            checkBoxShowPasswordRegister.Size = new Size(84, 24);
            checkBoxShowPasswordRegister.TabIndex = 6;
            checkBoxShowPasswordRegister.Text = "显示密码";
            checkBoxShowPasswordRegister.UseVisualStyleBackColor = true;
            checkBoxShowPasswordRegister.CheckedChanged += checkBoxShowPasswordRegister_CheckedChanged;
            // 
            // txtBoxRegPasswd
            // 
            txtBoxRegPasswd.Location = new Point(168, 177);
            txtBoxRegPasswd.Name = "txtBoxRegPasswd";
            txtBoxRegPasswd.Size = new Size(299, 30);
            txtBoxRegPasswd.TabIndex = 5;
            txtBoxRegPasswd.UseSystemPasswordChar = true;
            // 
            // txtBoxRegUsername
            // 
            txtBoxRegUsername.Location = new Point(168, 118);
            txtBoxRegUsername.Name = "txtBoxRegUsername";
            txtBoxRegUsername.Size = new Size(299, 30);
            txtBoxRegUsername.TabIndex = 3;
            // 
            // lblRegPasswd
            // 
            lblRegPasswd.AutoSize = true;
            lblRegPasswd.Location = new Point(86, 180);
            lblRegPasswd.Name = "lblRegPasswd";
            lblRegPasswd.Size = new Size(64, 24);
            lblRegPasswd.TabIndex = 4;
            lblRegPasswd.Text = "密码：";
            // 
            // lblRegUsername
            // 
            lblRegUsername.AutoSize = true;
            lblRegUsername.Location = new Point(68, 120);
            lblRegUsername.Name = "lblRegUsername";
            lblRegUsername.Size = new Size(82, 24);
            lblRegUsername.TabIndex = 2;
            lblRegUsername.Text = "用户名：";
            // 
            // progressBarRegister
            // 
            progressBarRegister.Dock = DockStyle.Bottom;
            progressBarRegister.Location = new Point(3, 400);
            progressBarRegister.Name = "progressBarRegister";
            progressBarRegister.Size = new Size(528, 31);
            progressBarRegister.TabIndex = 8;
            // 
            // tabPageForget
            // 
            tabPageForget.Controls.Add(txtBoxForgetSchoolId);
            tabPageForget.Controls.Add(lblForgetSchoolId);
            tabPageForget.Controls.Add(btnForget);
            tabPageForget.Controls.Add(progressBarForget);
            tabPageForget.Location = new Point(4, 32);
            tabPageForget.Name = "tabPageForget";
            tabPageForget.Padding = new Padding(3);
            tabPageForget.Size = new Size(534, 434);
            tabPageForget.TabIndex = 3;
            tabPageForget.Text = "查询用户名";
            tabPageForget.UseVisualStyleBackColor = true;
            // 
            // txtBoxForgetSchoolId
            // 
            txtBoxForgetSchoolId.Location = new Point(168, 128);
            txtBoxForgetSchoolId.Name = "txtBoxForgetSchoolId";
            txtBoxForgetSchoolId.Size = new Size(299, 30);
            txtBoxForgetSchoolId.TabIndex = 1;
            // 
            // lblForgetSchoolId
            // 
            lblForgetSchoolId.AutoSize = true;
            lblForgetSchoolId.Location = new Point(67, 130);
            lblForgetSchoolId.Name = "lblForgetSchoolId";
            lblForgetSchoolId.Size = new Size(83, 24);
            lblForgetSchoolId.TabIndex = 0;
            lblForgetSchoolId.Text = "学校ID：";
            // 
            // btnForget
            // 
            btnForget.Location = new Point(196, 289);
            btnForget.Name = "btnForget";
            btnForget.Size = new Size(143, 53);
            btnForget.TabIndex = 2;
            btnForget.Text = "查询用户名";
            btnForget.UseVisualStyleBackColor = true;
            btnForget.Click += btnForget_Click;
            // 
            // progressBarForget
            // 
            progressBarForget.Dock = DockStyle.Bottom;
            progressBarForget.Location = new Point(3, 400);
            progressBarForget.Name = "progressBarForget";
            progressBarForget.Size = new Size(528, 31);
            progressBarForget.TabIndex = 3;
            // 
            // LoginForm
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(542, 534);
            Controls.Add(splitContainerLoginForm);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            MaximumSize = new Size(558, 573);
            MinimumSize = new Size(558, 573);
            Name = "LoginForm";
            Text = "登录 | 学生信息管理系统 | 未登录";
            splitContainerLoginForm.Panel1.ResumeLayout(false);
            splitContainerLoginForm.Panel1.PerformLayout();
            splitContainerLoginForm.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainerLoginForm).EndInit();
            splitContainerLoginForm.ResumeLayout(false);
            tabControlLoginForm.ResumeLayout(false);
            tabPageLogin.ResumeLayout(false);
            tabPageLogin.PerformLayout();
            tabPageRegister.ResumeLayout(false);
            tabPageRegister.PerformLayout();
            tabPageForget.ResumeLayout(false);
            tabPageForget.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private SplitContainer splitContainerLoginForm;
        private Label lblFormTitle;
        private TabControl tabControlLoginForm;
        private TabPage tabPageLogin;
        private TabPage tabPageRegister;
        private Label lblLoginUsername;
        private ProgressBar progressBarLogin;
        private Label lblLoginPasswd;
        private ProgressBar progressBarRegister;
        private TabPage tabPageForget;
        private ProgressBar progressBarForget;
        private TextBox txtBoxLoginPasswd;
        private TextBox txtBoxLoginUsername;
        private CheckBox checkBoxShowPasswordLogin;
        private Button btnLogin;
        private TextBox txtBoxRegSchoolId;
        private Label lblRegSchoolId;
        private Button btnRegister;
        private CheckBox checkBoxShowPasswordRegister;
        private TextBox txtBoxRegPasswd;
        private TextBox txtBoxRegUsername;
        private Label lblRegPasswd;
        private Label lblRegUsername;
        private Button btnForget;
        private TextBox txtBoxForgetSchoolId;
        private Label lblForgetSchoolId;
    }
}
