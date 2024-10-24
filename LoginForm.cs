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
                // ��ʾ����
                txtBoxLoginPasswd.UseSystemPasswordChar = false;
            }
            else
            {
                // ��������
                txtBoxLoginPasswd.UseSystemPasswordChar = true;
            }
        }

        private void checkBoxShowPasswordRegister_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxShowPasswordRegister.Checked)
            {
                // ��ʾ����
                txtBoxRegPasswd.UseSystemPasswordChar = false;
            }
            else
            {
                // ��������
                txtBoxRegPasswd.UseSystemPasswordChar = true;
            }
        }

        private async void btnLogin_Click(object sender, EventArgs e)
        {
            if (txtBoxLoginUsername.Text != "" && txtBoxLoginPasswd.Text != "")
            {
                btnLogin.Text = "��½��...";
                btnLogin.Enabled = false;
                tabControlLoginForm.Enabled = false;
                progressBarLogin.Style = ProgressBarStyle.Marquee;

                var responseMessage = await NetworkStack.Login(txtBoxLoginUsername.Text, txtBoxLoginPasswd.Text);
                if (responseMessage == "OK")
                {
                    btnLogin.Text = "������Ϣ...";
                    await NetworkStack.GetUserProfile();
                    this.Hide();
                    CloseAndOpenStuInfoForm();

                }
                else
                {
                    MessageBox.Show("��½ʧ��\n" + responseMessage, "����", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                btnLogin.Text = "��¼ϵͳ";
                btnLogin.Enabled = true;
                tabControlLoginForm.Enabled = true;
                progressBarLogin.Style = ProgressBarStyle.Blocks;
            }
            else
            {
                MessageBox.Show("�û��������벻��Ϊ�ա�", "����", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private async void btnRegister_Click(object sender, EventArgs e)
        {
            if (txtBoxRegUsername.Text != "" && txtBoxRegPasswd.Text != "" && txtBoxRegSchoolId.Text != "")
            {
                btnRegister.Text = "ע����...";
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
                    MessageBox.Show("ע��ʧ��\n" + responseMessage, "����", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                btnRegister.Text = "ע��ϵͳ";
                btnRegister.Enabled = true;
                tabControlLoginForm.Enabled = true;
                progressBarRegister.Style = ProgressBarStyle.Blocks;
            }
            else
            {
                MessageBox.Show("�û����������ѧУID������Ϊ�ա�", "����", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private async void btnForget_Click(object sender, EventArgs e)
        {
            if (txtBoxForgetSchoolId.Text != "")
            {
                btnForget.Text = "��ѯ��...";
                btnForget.Enabled = false;
                tabControlLoginForm.Enabled = false;
                progressBarForget.Style = ProgressBarStyle.Marquee;

                var responseMessage = await NetworkStack.Forget(txtBoxForgetSchoolId.Text);
                btnForget.Text = "��ѯ�û���";
                btnForget.Enabled = true;
                tabControlLoginForm.Enabled = true;
                progressBarForget.Style = ProgressBarStyle.Blocks;
                if (responseMessage != "No user found" && !String.IsNullOrEmpty(responseMessage))
                {
                    MessageBox.Show("�û���: " + responseMessage, "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("���޴�ѧУID���û�����������", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
