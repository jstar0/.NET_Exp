namespace StudentInformationEntrySystem
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
            this.FormClosing += (sender, e) =>
            {
                if (!Program.IsSignedIn)
                {
                    Program.WillExit = true;
                    Application.Exit();
                }
            };
        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void checkBoxShowPasswordLogin_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxShowPasswordLogin.Checked)
            {
                // 显示密码
                txtBoxLoginPasswd.UseSystemPasswordChar = false;
            }
            else
            {
                // 隐藏密码
                txtBoxLoginPasswd.UseSystemPasswordChar = true;
            }
        }

        private void checkBoxShowPasswordRegister_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxShowPasswordRegister.Checked)
            {
                // 显示密码
                txtBoxRegPasswd.UseSystemPasswordChar = false;
            }
            else
            {
                // 隐藏密码
                txtBoxRegPasswd.UseSystemPasswordChar = true;
            }
        }

        private async void btnLogin_Click(object sender, EventArgs e)
        {
            if (txtBoxLoginUsername.Text != "" && txtBoxLoginPasswd.Text != "")
            {
                btnLogin.Text = "登陆中...";
                btnLogin.Enabled = false;
                tabControlLoginForm.Enabled = false;
                progressBarLogin.Style = ProgressBarStyle.Marquee;

                var responseMessage = await NetworkStack.Login(txtBoxLoginUsername.Text, txtBoxLoginPasswd.Text);
                if (responseMessage == "OK")
                {
                    btnLogin.Text = "下载信息...";
                    await NetworkStack.GetUserProfile();
                    this.Hide();
                    CloseAndOpenStuInfoForm();

                }
                else
                {
                    MessageBox.Show("登陆失败\n" + responseMessage, "错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                btnLogin.Text = "登录系统";
                btnLogin.Enabled = true;
                tabControlLoginForm.Enabled = true;
                progressBarLogin.Style = ProgressBarStyle.Blocks;
            }
            else
            {
                MessageBox.Show("用户名和密码不能为空。", "错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private async void btnRegister_Click(object sender, EventArgs e)
        {
            if (txtBoxRegUsername.Text != "" && txtBoxRegPasswd.Text != "" && txtBoxRegSchoolId.Text != "")
            {
                btnRegister.Text = "注册中...";
                btnRegister.Enabled = false;
                tabControlLoginForm.Enabled = false;
                progressBarRegister.Style = ProgressBarStyle.Marquee;

                var responseMessage = await NetworkStack.Register(txtBoxRegUsername.Text, txtBoxRegPasswd.Text, txtBoxRegSchoolId.Text);
                if (responseMessage == "OK")
                {
                    var getUserProfile = NetworkStack.GetUserProfile();
                    CloseAndOpenStuInfoForm();
                }
                else
                {
                    MessageBox.Show("注册失败\n" + responseMessage, "错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                btnRegister.Text = "注册系统";
                btnRegister.Enabled = true;
                tabControlLoginForm.Enabled = true;
                progressBarRegister.Style = ProgressBarStyle.Blocks;
            }
            else
            {
                MessageBox.Show("用户名、密码和学校ID都不能为空。", "错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private async void btnForget_Click(object sender, EventArgs e)
        {
            if (txtBoxForgetSchoolId.Text != "")
            {
                btnForget.Text = "查询中...";
                btnForget.Enabled = false;
                tabControlLoginForm.Enabled = false;
                progressBarForget.Style = ProgressBarStyle.Marquee;

                var responseMessage = await NetworkStack.Forget(txtBoxForgetSchoolId.Text);
                btnForget.Text = "查询用户名";
                btnForget.Enabled = true;
                tabControlLoginForm.Enabled = true;
                progressBarForget.Style = ProgressBarStyle.Blocks;
                if (responseMessage != "No user found" && !String.IsNullOrEmpty(responseMessage))
                {
                    MessageBox.Show("用户名: " + responseMessage, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("查无此学校ID的用户，或发生错误。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void CloseAndOpenStuInfoForm()
        {
            this.Hide();
            var stuInfoEntryForm = new StuInfoEntryForm();
            stuInfoEntryForm.FormClosed += (s, args) => this.Close();
            stuInfoEntryForm.Show();
        }
    }
}
