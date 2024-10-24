using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic.Devices;

namespace StudentInformationEntrySystem
{
    public partial class StuInfoEntryForm : Form
    {
        public StuInfoEntryForm()
        {
            InitializeComponent();
            this.Text += " | " + Program.CurrentUser.username;

            ImportUserProfile();
            // Download Pic
            GetUserPhoto();

            this.FormClosing += (sender, e) =>
            {
                this.Text = "录入 | 学生信息管理系统";
            };
        }

        private void ImportUserProfile()
        {
            if (Program.CurrentUser.basic != null)
            {
                txtBoxName.Text = Program.CurrentUser.basic.name;
                txtBoxAge.Text = Program.CurrentUser.basic.age.ToString();

                if (int.TryParse(Program.CurrentUser.basic.gender, out var gender))
                {
                    comboBoxGender.SelectedIndex = gender;
                }
                txtBoxAncestry.Text = Program.CurrentUser.basic.ancestry;
                if (int.TryParse(Program.CurrentUser.basic.political, out var political))
                {
                    comboBoxPolitical.SelectedIndex = political;
                }
                if (int.TryParse(Program.CurrentUser.basic.qualification, out var qualification))
                {
                    comboBoxQualification.SelectedIndex = qualification;
                }
                txtBoxMajor.Text = Program.CurrentUser.basic.major;
                txtBoxContact.Text = Program.CurrentUser.basic.contact;
                txtBoxProfile.Text = Program.CurrentUser.basic.profile;
            }

            // 载入已选课程
            // 从 Program.CurrentUser.selectedCourses 到 SelectedCoursesList

            // Load Family Info
            if (Program.CurrentUser.family != null)
            {
                if (Program.CurrentUser.family.livingInCity)
                {
                    radioButtonFamilyType1.Checked = true;
                    radioButtonFamilyType2.Checked = false;
                }
                else
                {
                    radioButtonFamilyType2.Checked = true;
                    radioButtonFamilyType1.Checked = false;
                }

                txtBoxBirth.Text = Program.CurrentUser.family.birth;
                txtBoxLiving.Text = Program.CurrentUser.family.living;
                txtBoxZipCode.Text = Program.CurrentUser.family.zipCode;
                txtBoxHomePhone.Text = Program.CurrentUser.family.homePhone;
                txtBoxAdress.Text = Program.CurrentUser.family.address;
            }
        }

        private void ExportUserProfile()
        {
            // Export Basic Info
            Program.CurrentUser.basic = new BasicInfo(
                txtBoxName.Text,
                int.Parse(txtBoxAge.Text),
                comboBoxGender.SelectedIndex.ToString(),
                txtBoxAncestry.Text,
                comboBoxPolitical.SelectedIndex.ToString(),
                comboBoxQualification.SelectedIndex.ToString(),
                txtBoxMajor.Text,
                txtBoxContact.Text,
                txtBoxProfile.Text
            );

            // Export Selected Courses
            Program.CurrentUser.selectedCourses = [];
            foreach (var courseId in Program.SelectedCoursesList.Keys)
            {
                Program.CurrentUser.selectedCourses.Add(courseId);
            }

            // Export Family Info
            Program.CurrentUser.family = new FamilyInfo(
                txtBoxBirth.Text,
                txtBoxLiving.Text,
                txtBoxZipCode.Text,
                txtBoxHomePhone.Text,
                txtBoxAdress.Text,
                radioButtonFamilyType1.Checked
            );
        }

        private readonly BindingSource _bindingSourceCan = new BindingSource();
        private readonly BindingSource _bindingSourceIs = new BindingSource();

        private void BindCoursesToListBox()
        {
            _bindingSourceCan.DataSource = Program.CanSelectCoursesList.Values.ToList();

            lsBoxCanSelect.DataSource = _bindingSourceCan;
            lsBoxCanSelect.DisplayMember = "name";
            lsBoxCanSelect.ValueMember = "id";

            _bindingSourceIs.DataSource = Program.SelectedCoursesList.Values.ToList();

            lsBoxIsSelect.DataSource = _bindingSourceIs;
            lsBoxIsSelect.DisplayMember = "name";
            lsBoxIsSelect.ValueMember = "id";
        }


        private async void GetUserPhoto()
        {
            btnUploadPhoto.Enabled = false;
            btnUploadPhoto.Text = "下载中...";
            picBoxPhoto.Image = await NetworkStack.GetUserPhoto();
            btnUploadPhoto.Enabled = true;
            btnUploadPhoto.Text = "上传4:3照片";
        }

        private async void pageChange_onClick(object sender, EventArgs e)
        {
            Button btn = (Button)sender;

            // 这部分的职能在 tabControl_SelectedIndexChanged 也实现了一部分
            if (btn.Name == "btnLastPage")
            {
                tabControl.SelectedIndex = tabControl.SelectedIndex - 1;
            }
            else if (btn.Name == "btnNextPage")
            {
                if (tabControl.SelectedIndex == tabControl.TabCount - 1)
                {
                    await HandleSubmit();
                }
                else
                {
                    tabControl.SelectedIndex = tabControl.SelectedIndex + 1;
                }
            }
        }

        // 在鼠标按下时开始拖动
        private void lsBoxCanSelect_MouseDown(object sender, MouseEventArgs e)
        {
            // 检查点击的是否是有效项
            if (e.Clicks > 1 || lsBoxCanSelect.SelectedItem == null) return;

            // 获取选中的课程并开始拖动
            var selectedCourse = (Courses)lsBoxCanSelect.SelectedItem;
            DoDragDrop(selectedCourse, DragDropEffects.Move);
        }

        // 在目标 ListBox 上检测拖动进入
        private void lsBoxIsSelect_DragEnter(object sender, DragEventArgs e)
        {
            // 检查是否是课程对象
            if (e.Data.GetDataPresent(typeof(Courses)))
            {
                e.Effect = DragDropEffects.Move;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        // 在拖放结束时执行的操作
        private void lsBoxIsSelect_DragDrop(object sender, DragEventArgs e)
        {
            // 检查数据是否是课程对象
            if (e.Data.GetDataPresent(typeof(Courses)))
            {
                var droppedCourse = (Courses)e.Data.GetData(typeof(Courses));

                // 将课程从可选列表移到已选列表
                Program.CanSelectCoursesList.Remove(droppedCourse.id);
                Program.SelectedCoursesList.Add(droppedCourse.id, droppedCourse);

                // 更新 ListBox 数据源
                _bindingSourceCan.DataSource = Program.CanSelectCoursesList.Values.ToList();
                _bindingSourceIs.DataSource = Program.SelectedCoursesList.Values.ToList();
            }
        }

        private void tabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl.SelectedIndex == 0)
            {
                btnLastPage.Enabled = false;
            }
            else
            {
                btnLastPage.Enabled = true;
            }
            if (tabControl.SelectedIndex == tabControl.TabCount - 1)
            {
                btnNextPage.Text = "提交";
                LoadFromPrevTxtBox();
            }
            else
            {
                btnNextPage.Text = "下一页";
            }
        }

        private void LoadFromPrevTxtBox()
        {
            txtBoxSumBasic.Text = $"用户ID：{Program.CurrentUser.username}, 学号：{Program.CurrentUser.schoolId}";
            txtBoxSumBasic.Text += $"\r\n姓名：{txtBoxName.Text}, 年龄：{txtBoxAge.Text}, 性别：{comboBoxGender.Text}";
            txtBoxSumBasic.Text += $"\r\n民族：{txtBoxAncestry.Text}, 政治面貌：{comboBoxPolitical.Text}, 学历：{comboBoxQualification.Text}";
            txtBoxSumBasic.Text += $"\r\n专业：{txtBoxMajor.Text}, 联系方式：{txtBoxContact.Text}";
            txtBoxSumBasic.Text += $"\r\n个人简介：{txtBoxProfile.Text}";
            txtBoxSumBasic.Enabled = true;

            if (!Program.HasGotCourseInfo)
            {
                txtBoxSumCourse.Text = "请您先在第二页获取课程信息，否则不能提交表格。";
            }
            else
            {
                txtBoxSumCourse.Text = $"已选课程ID：\r\n";
                foreach (var courseId in Program.SelectedCoursesList.Keys)
                {
                    txtBoxSumCourse.Text += $"{courseId}, ";
                }
                txtBoxSumCourse.Enabled = true;
            }

            txtBoxSumFamily.Text = $"家庭类型：{(radioButtonFamilyType1.Checked ? "城市" : "农村")}户口";
            txtBoxSumFamily.Text += $"\r\n出生地：{txtBoxBirth.Text}, 现居地：{txtBoxLiving.Text}";
            txtBoxSumFamily.Text += $"\r\n现居地邮编：{txtBoxZipCode.Text}, 家庭电话：{txtBoxHomePhone.Text}";
            txtBoxSumFamily.Text += $"\r\n家庭住址：{txtBoxAdress.Text}";
            txtBoxSumFamily.Enabled = true;
        }

        private async Task HandleSubmit()
        {
            if (!Program.HasGotCourseInfo)
            {
                MessageBox.Show("您必须先去第二页获取课程信息，方能提交请求。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            btnNextPage.Enabled = false;
            btnLastPage.Enabled = false;
            tabControl.Enabled = false;

            ExportUserProfile();

            if (await NetworkStack.UpdateUserProfile())
            {
                MessageBox.Show("提交成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("提交失败，请重试。", "错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            btnNextPage.Enabled = true;
            btnLastPage.Enabled = true;
            tabControl.Enabled = true;
        }

        private async void btnUploadPhoto_Click(object sender, EventArgs e)
        {
            btnUploadPhoto.Enabled = false;
            btnUploadPhoto.Text = "设置中...";
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files(*.BMP;*.JPG;*.GIF;*.PNG)|*.BMP;*.JPG;*.GIF;*.PNG";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string compressedFilePath = Path.Combine(Path.GetTempPath(), Path.GetFileName(openFileDialog.FileName));
                    // 先移除冲突的文件（如有）
                    if (File.Exists(compressedFilePath))
                    {
                        File.Delete(compressedFilePath);
                    }
                    if (CompressImg.CompressImage(openFileDialog.FileName, compressedFilePath, 90, 300))
                    {
                        Image img = Image.FromFile(compressedFilePath);
                        picBoxPhoto.Image = img;
                        byte[] imgBytes = File.ReadAllBytes(compressedFilePath);
                        string base64Image = Convert.ToBase64String(imgBytes);
                        Program.CurrentUser.photo = base64Image;
                        btnUploadPhoto.Text = "上传中...";
                        await UploadImgAsync();

                        // 解除文件占用
                        img.Dispose();

                        // 清理缓存文件
                        File.Delete(compressedFilePath);
                    }
                    else
                    {
                        MessageBox.Show("图片压缩失败。", "错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("图片上传失败。请重试，或更换其他图片。\n" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    Program.CurrentUser.photo = null;
                    picBoxPhoto.Image = null;
                    btnUploadPhoto.Enabled = true;
                }
                btnUploadPhoto.Text = "上传4:3照片";
                btnUploadPhoto.Enabled = true;
            }
            else
            {
                btnUploadPhoto.Text = "上传4:3照片";
                btnUploadPhoto.Enabled = true;
            }
        }

        private ImageCodecInfo GetEncoder(ImageFormat jpeg)
        {
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageDecoders();
            foreach (ImageCodecInfo codec in codecs)
            {
                if (codec.FormatID == jpeg.Guid)
                {
                    return codec;
                }
            }
            return codecs[1];
        }

        private async Task UploadImgAsync()
        {
            if (!await NetworkStack.UpdateUserPhoto(Program.CurrentUser.photo))
            {
                MessageBox.Show("图片上传失败，请重试。", "错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                picBoxPhoto.Image = null;
                Program.CurrentUser.photo = null;
            }
        }

        private void btnCourseFromServer_Click(object sender, EventArgs e)
        {
            UpdateCourseList();
        }


        // menuItems
        private void menuItem_Opt_Read_Click(object sender, EventArgs e)
        {
            // 重新打开LicenseForm
            Form licenseForm = new LicenseForm();
            licenseForm.ShowDialog();
        }

        private void menuItem_Opt_Repo_Click(object sender, EventArgs e)
        {
            // 打开指定的URL
            string url = "https://github.com/jstar0/.NET_Exp/";
            Process.Start(new ProcessStartInfo
            {
                FileName = url,
                UseShellExecute = true
            });
        }

        private async void menuItem_Course_Click(object sender, EventArgs e)
        {
            menuItem_Course.Enabled = false;
            await UpdateCourseList();
            menuItem_Course.Enabled = true;
        }

        private void menuItem_SignOut_Click(object sender, EventArgs e)
        {
            var msgBoxMessage = MessageBox.Show("您确定要登出吗？所有未作保存的信息将丢失。", "提醒", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (msgBoxMessage == DialogResult.OK)
            {
                Program.HasGotCourseInfo = false;
                DataInterface.ResetCurrentData();
                this.Hide();
                var loginForm = new LoginForm();
                loginForm.FormClosed += (s, args) => this.Close();
                loginForm.Show();
            }
        }

        private async void menuItem_Close_Save_Click(object sender, EventArgs e)
        {
            tabControl.SelectedIndex = 3;
            await HandleSubmit();
            if (Program.HasGotCourseInfo)
            {
                this.Close();
            }
        }

        private void menuItem_Close_NoSave_Click(object sender, EventArgs e)
        {
            var msgBoxMessage = MessageBox.Show("您确定要直接退出吗？所有未作保存的信息将丢失。", "提醒", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (msgBoxMessage == DialogResult.OK)
            {
                Application.Exit();
            }
        }

        private async Task UpdateCourseList()
        {
            if (txtBoxMajor.Text == "")
            {
                MessageBox.Show("请先在表格第一页添加专业信息。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            btnCourseFromServer.Text = "正在获取信息...";
            btnCourseFromServer.Enabled = false;
            await NetworkStack.GetCoursesList(txtBoxMajor.Text);
            btnCourseFromServer.Visible = false;
            lblCanSelect.Visible = true;
            lblIsSelect.Visible = true;
            lsBoxCanSelect.Visible = true;
            lsBoxIsSelect.Visible = true;
            lblCourseDesc.Visible = true;
            lblCourseTips.Visible = true;

            if (Program.AvailableCoursesList.Count == 0)
            {
                MessageBox.Show($"您的专业{txtBoxMajor.Text}，在系统中无对应课程。\n您可以尝试将其修改为“cs”，然后从菜单栏“重新获取选课信息”。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                Program.HasGotCourseInfo = true;
            }

            BindCoursesToListBox();
        }

        private void IsBoxCan_DoubleClick(object sender, EventArgs e)
        {
            if (lsBoxCanSelect.SelectedItem != null)
            {
                // MessageBox输出该Course的详细信息
                var selectedId = (int)lsBoxCanSelect.SelectedValue;
                if (selectedId != 0)
                {
                    // 从字典查询 selectedId 并输出
                    if (Program.CanSelectCoursesList.TryGetValue(selectedId, out var selectedCourse))
                    {
                        MessageBox.Show($"课程ID: {selectedCourse.id}\n课程名: {selectedCourse.name}\n限选专业：{selectedCourse.major}\n课程简介: {selectedCourse.description}", "课程信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        private void IsBoxIs_DoubleClick(object sender, EventArgs e)
        {
            if (lsBoxIsSelect.SelectedItem != null)
            {
                // MessageBox输出该Course的详细信息
                var selectedId = (int)lsBoxIsSelect.SelectedValue;
                if (selectedId != 0)
                {
                    // 从字典查询 selectedId 并输出
                    if (Program.SelectedCoursesList.TryGetValue(selectedId, out var selectedCourse))
                    {
                        MessageBox.Show($"课程ID: {selectedCourse.id}\n课程名: {selectedCourse.name}\n限选专业：{selectedCourse.major}\n课程简介: {selectedCourse.description}", "课程信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        private void txtBoxSum_KeyDown(object sender, KeyEventArgs e)
        {
            // 取消所有按键
            e.SuppressKeyPress = true;
        }

        private void IsBoxIs_KeyDown(object sender, KeyEventArgs e)
        {
            // 只监听DEL按键
            if (e.KeyValue == (char)Keys.Delete)
            {
                MessageBox.Show("Raised");
                if (lsBoxIsSelect.SelectedItem != null)
                {
                    var selectedId = (int)lsBoxIsSelect.SelectedValue;
                    if (selectedId != 0)
                    {
                        if (Program.SelectedCoursesList.ContainsKey(selectedId))
                        {
                            Program.SelectedCoursesList.Remove(selectedId);
                            Program.CanSelectCoursesList.Add(selectedId, Program.AvailableCoursesList[selectedId]);
                            BindCoursesToListBox();
                        }
                    }
                }
            }
        }
    }
}
