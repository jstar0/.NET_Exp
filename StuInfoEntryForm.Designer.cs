namespace StudentInformationEntrySystem
{
    partial class StuInfoEntryForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            menuStrip1 = new MenuStrip();
            menuItem_Opt = new ToolStripMenuItem();
            menuItem_Opt_Read = new ToolStripMenuItem();
            menuItem_Opt_Repo = new ToolStripMenuItem();
            menuItem_Course = new ToolStripMenuItem();
            menuItem_SignOut = new ToolStripMenuItem();
            关闭系统ToolStripMenuItem = new ToolStripMenuItem();
            menuItem_Close_Save = new ToolStripMenuItem();
            menuItem_Close_NoSave = new ToolStripMenuItem();
            splitContainer1 = new SplitContainer();
            tabControl = new TabControl();
            tabPageBasic = new TabPage();
            btnUploadPhoto = new Button();
            txtBoxProfile = new TextBox();
            lblProfile = new Label();
            txtBoxContact = new TextBox();
            txtBoxMajor = new TextBox();
            lblContact = new Label();
            lblMajor = new Label();
            comboBoxQualification = new ComboBox();
            lblQualification = new Label();
            comboBoxPolitical = new ComboBox();
            lblPolitical = new Label();
            lblAncestry = new Label();
            comboBoxGender = new ComboBox();
            lblGender = new Label();
            txtBoxAncestry = new TextBox();
            txtBoxAge = new TextBox();
            txtBoxName = new TextBox();
            lblAge = new Label();
            lblName = new Label();
            picBoxPhoto = new PictureBox();
            tabPageCourse = new TabPage();
            lblCourseTips = new Label();
            btnCourseFromServer = new Button();
            lblIsSelect = new Label();
            lblCanSelect = new Label();
            lblCourseDesc = new Label();
            lsBoxIsSelect = new ListBox();
            lsBoxCanSelect = new ListBox();
            tabPageFamily = new TabPage();
            lblFamilyType = new Label();
            radioButtonFamilyType2 = new RadioButton();
            radioButtonFamilyType1 = new RadioButton();
            txtBoxAdress = new TextBox();
            lblAddress = new Label();
            txtBoxHomePhone = new TextBox();
            lblLiving = new Label();
            lblHomePhone = new Label();
            txtBoxLiving = new TextBox();
            txtBoxZipCode = new TextBox();
            txtBoxBirth = new TextBox();
            lblZipCode = new Label();
            lblBirth = new Label();
            tabPageConfirm = new TabPage();
            lblFamily = new Label();
            lblCourse = new Label();
            lblBasic = new Label();
            txtBoxSumFamily = new TextBox();
            txtBoxSumCourse = new TextBox();
            txtBoxSumBasic = new TextBox();
            btnNextPage = new Button();
            btnLastPage = new Button();
            menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            tabControl.SuspendLayout();
            tabPageBasic.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picBoxPhoto).BeginInit();
            tabPageCourse.SuspendLayout();
            tabPageFamily.SuspendLayout();
            tabPageConfirm.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { menuItem_Opt, menuItem_Course, menuItem_SignOut, 关闭系统ToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(844, 25);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // menuItem_Opt
            // 
            menuItem_Opt.DropDownItems.AddRange(new ToolStripItem[] { menuItem_Opt_Read, menuItem_Opt_Repo });
            menuItem_Opt.Name = "menuItem_Opt";
            menuItem_Opt.Size = new Size(44, 21);
            menuItem_Opt.Text = "选项";
            // 
            // menuItem_Opt_Read
            // 
            menuItem_Opt_Read.Name = "menuItem_Opt_Read";
            menuItem_Opt_Read.Size = new Size(172, 22);
            menuItem_Opt_Read.Text = "阅读告知信息";
            menuItem_Opt_Read.Click += menuItem_Opt_Read_Click;
            // 
            // menuItem_Opt_Repo
            // 
            menuItem_Opt_Repo.Name = "menuItem_Opt_Repo";
            menuItem_Opt_Repo.Size = new Size(172, 22);
            menuItem_Opt_Repo.Text = "打开项目源码仓库";
            menuItem_Opt_Repo.Click += menuItem_Opt_Repo_Click;
            // 
            // menuItem_Course
            // 
            menuItem_Course.Name = "menuItem_Course";
            menuItem_Course.Size = new Size(116, 21);
            menuItem_Course.Text = "重新获取选课信息";
            menuItem_Course.Click += menuItem_Course_Click;
            // 
            // menuItem_SignOut
            // 
            menuItem_SignOut.Name = "menuItem_SignOut";
            menuItem_SignOut.Size = new Size(44, 21);
            menuItem_SignOut.Text = "登出";
            menuItem_SignOut.Click += menuItem_SignOut_Click;
            // 
            // 关闭系统ToolStripMenuItem
            // 
            关闭系统ToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { menuItem_Close_Save, menuItem_Close_NoSave });
            关闭系统ToolStripMenuItem.Name = "关闭系统ToolStripMenuItem";
            关闭系统ToolStripMenuItem.Size = new Size(68, 21);
            关闭系统ToolStripMenuItem.Text = "关闭系统";
            // 
            // menuItem_Close_Save
            // 
            menuItem_Close_Save.Name = "menuItem_Close_Save";
            menuItem_Close_Save.Size = new Size(160, 22);
            menuItem_Close_Save.Text = "提交并关闭";
            menuItem_Close_Save.Click += menuItem_Close_Save_Click;
            // 
            // menuItem_Close_NoSave
            // 
            menuItem_Close_NoSave.Name = "menuItem_Close_NoSave";
            menuItem_Close_NoSave.Size = new Size(160, 22);
            menuItem_Close_NoSave.Text = "放弃提交并关闭";
            menuItem_Close_NoSave.Click += menuItem_Close_NoSave_Click;
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.Location = new Point(0, 25);
            splitContainer1.Name = "splitContainer1";
            splitContainer1.Orientation = Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(tabControl);
            splitContainer1.Panel1.RightToLeft = RightToLeft.No;
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(btnNextPage);
            splitContainer1.Panel2.Controls.Add(btnLastPage);
            splitContainer1.Panel2.RightToLeft = RightToLeft.No;
            splitContainer1.RightToLeft = RightToLeft.No;
            splitContainer1.Size = new Size(844, 499);
            splitContainer1.SplitterDistance = 438;
            splitContainer1.TabIndex = 1;
            // 
            // tabControl
            // 
            tabControl.Controls.Add(tabPageBasic);
            tabControl.Controls.Add(tabPageCourse);
            tabControl.Controls.Add(tabPageFamily);
            tabControl.Controls.Add(tabPageConfirm);
            tabControl.Dock = DockStyle.Fill;
            tabControl.Font = new Font("Microsoft YaHei UI", 12F);
            tabControl.Location = new Point(0, 0);
            tabControl.Name = "tabControl";
            tabControl.SelectedIndex = 0;
            tabControl.Size = new Size(844, 438);
            tabControl.TabIndex = 0;
            tabControl.SelectedIndexChanged += tabControl_SelectedIndexChanged;
            // 
            // tabPageBasic
            // 
            tabPageBasic.Controls.Add(btnUploadPhoto);
            tabPageBasic.Controls.Add(txtBoxProfile);
            tabPageBasic.Controls.Add(lblProfile);
            tabPageBasic.Controls.Add(txtBoxContact);
            tabPageBasic.Controls.Add(txtBoxMajor);
            tabPageBasic.Controls.Add(lblContact);
            tabPageBasic.Controls.Add(lblMajor);
            tabPageBasic.Controls.Add(comboBoxQualification);
            tabPageBasic.Controls.Add(lblQualification);
            tabPageBasic.Controls.Add(comboBoxPolitical);
            tabPageBasic.Controls.Add(lblPolitical);
            tabPageBasic.Controls.Add(lblAncestry);
            tabPageBasic.Controls.Add(comboBoxGender);
            tabPageBasic.Controls.Add(lblGender);
            tabPageBasic.Controls.Add(txtBoxAncestry);
            tabPageBasic.Controls.Add(txtBoxAge);
            tabPageBasic.Controls.Add(txtBoxName);
            tabPageBasic.Controls.Add(lblAge);
            tabPageBasic.Controls.Add(lblName);
            tabPageBasic.Controls.Add(picBoxPhoto);
            tabPageBasic.Location = new Point(4, 30);
            tabPageBasic.Name = "tabPageBasic";
            tabPageBasic.Padding = new Padding(3);
            tabPageBasic.Size = new Size(836, 404);
            tabPageBasic.TabIndex = 0;
            tabPageBasic.Text = "基本信息";
            tabPageBasic.UseVisualStyleBackColor = true;
            // 
            // btnUploadPhoto
            // 
            btnUploadPhoto.Font = new Font("Microsoft YaHei UI", 9F);
            btnUploadPhoto.Location = new Point(691, 224);
            btnUploadPhoto.Name = "btnUploadPhoto";
            btnUploadPhoto.Size = new Size(104, 26);
            btnUploadPhoto.TabIndex = 16;
            btnUploadPhoto.Text = "上传4:3照片";
            btnUploadPhoto.UseVisualStyleBackColor = true;
            btnUploadPhoto.Click += btnUploadPhoto_Click;
            // 
            // txtBoxProfile
            // 
            txtBoxProfile.Location = new Point(22, 277);
            txtBoxProfile.Multiline = true;
            txtBoxProfile.Name = "txtBoxProfile";
            txtBoxProfile.PlaceholderText = "简述，不少于10字，不大于300字";
            txtBoxProfile.ScrollBars = ScrollBars.Vertical;
            txtBoxProfile.Size = new Size(793, 110);
            txtBoxProfile.TabIndex = 18;
            // 
            // lblProfile
            // 
            lblProfile.AutoSize = true;
            lblProfile.Location = new Point(23, 244);
            lblProfile.Name = "lblProfile";
            lblProfile.Size = new Size(74, 21);
            lblProfile.TabIndex = 17;
            lblProfile.Text = "个人经历";
            // 
            // txtBoxContact
            // 
            txtBoxContact.Location = new Point(442, 193);
            txtBoxContact.Name = "txtBoxContact";
            txtBoxContact.PlaceholderText = "手机号或邮箱";
            txtBoxContact.Size = new Size(182, 28);
            txtBoxContact.TabIndex = 15;
            txtBoxContact.TextAlign = HorizontalAlignment.Center;
            // 
            // txtBoxMajor
            // 
            txtBoxMajor.Location = new Point(128, 193);
            txtBoxMajor.Name = "txtBoxMajor";
            txtBoxMajor.PlaceholderText = "填在读专业全名";
            txtBoxMajor.Size = new Size(157, 28);
            txtBoxMajor.TabIndex = 13;
            txtBoxMajor.TextAlign = HorizontalAlignment.Center;
            // 
            // lblContact
            // 
            lblContact.AutoSize = true;
            lblContact.Location = new Point(349, 197);
            lblContact.Name = "lblContact";
            lblContact.Size = new Size(74, 21);
            lblContact.TabIndex = 14;
            lblContact.Text = "联系方式";
            // 
            // lblMajor
            // 
            lblMajor.AutoSize = true;
            lblMajor.Location = new Point(23, 196);
            lblMajor.Name = "lblMajor";
            lblMajor.Size = new Size(74, 21);
            lblMajor.TabIndex = 12;
            lblMajor.Text = "在读专业";
            // 
            // comboBoxQualification
            // 
            comboBoxQualification.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxQualification.FormattingEnabled = true;
            comboBoxQualification.Items.AddRange(new object[] { "小学", "初中", "中专", "高中", "大专", "本科及以上" });
            comboBoxQualification.Location = new Point(503, 139);
            comboBoxQualification.Name = "comboBoxQualification";
            comboBoxQualification.Size = new Size(121, 29);
            comboBoxQualification.TabIndex = 11;
            // 
            // lblQualification
            // 
            lblQualification.AutoSize = true;
            lblQualification.Location = new Point(349, 142);
            lblQualification.Name = "lblQualification";
            lblQualification.Size = new Size(74, 21);
            lblQualification.TabIndex = 10;
            lblQualification.Text = "最高学历";
            // 
            // comboBoxPolitical
            // 
            comboBoxPolitical.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxPolitical.FormattingEnabled = true;
            comboBoxPolitical.Items.AddRange(new object[] { "中共党员", "中共预备党员", "共青团员", "民主党派", "无党派人士", "群众" });
            comboBoxPolitical.Location = new Point(164, 139);
            comboBoxPolitical.Name = "comboBoxPolitical";
            comboBoxPolitical.Size = new Size(121, 29);
            comboBoxPolitical.TabIndex = 9;
            // 
            // lblPolitical
            // 
            lblPolitical.AutoSize = true;
            lblPolitical.Location = new Point(23, 142);
            lblPolitical.Name = "lblPolitical";
            lblPolitical.Size = new Size(74, 21);
            lblPolitical.TabIndex = 8;
            lblPolitical.Text = "政治面貌";
            // 
            // lblAncestry
            // 
            lblAncestry.AutoSize = true;
            lblAncestry.Location = new Point(381, 88);
            lblAncestry.Name = "lblAncestry";
            lblAncestry.Size = new Size(42, 21);
            lblAncestry.TabIndex = 6;
            lblAncestry.Text = "籍贯";
            // 
            // comboBoxGender
            // 
            comboBoxGender.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxGender.FormattingEnabled = true;
            comboBoxGender.Items.AddRange(new object[] { "男", "女", "不愿透露" });
            comboBoxGender.Location = new Point(503, 31);
            comboBoxGender.Name = "comboBoxGender";
            comboBoxGender.Size = new Size(121, 29);
            comboBoxGender.TabIndex = 3;
            // 
            // lblGender
            // 
            lblGender.AutoSize = true;
            lblGender.Location = new Point(381, 34);
            lblGender.Name = "lblGender";
            lblGender.Size = new Size(42, 21);
            lblGender.TabIndex = 2;
            lblGender.Text = "性别";
            // 
            // txtBoxAncestry
            // 
            txtBoxAncestry.Location = new Point(503, 85);
            txtBoxAncestry.Name = "txtBoxAncestry";
            txtBoxAncestry.PlaceholderText = "省市（县）";
            txtBoxAncestry.Size = new Size(121, 28);
            txtBoxAncestry.TabIndex = 7;
            txtBoxAncestry.TextAlign = HorizontalAlignment.Center;
            // 
            // txtBoxAge
            // 
            txtBoxAge.Location = new Point(164, 85);
            txtBoxAge.Name = "txtBoxAge";
            txtBoxAge.PlaceholderText = "填阿拉伯数字";
            txtBoxAge.Size = new Size(121, 28);
            txtBoxAge.TabIndex = 5;
            txtBoxAge.TextAlign = HorizontalAlignment.Center;
            // 
            // txtBoxName
            // 
            txtBoxName.Location = new Point(128, 31);
            txtBoxName.Name = "txtBoxName";
            txtBoxName.PlaceholderText = "汉字，分隔符用“·”";
            txtBoxName.Size = new Size(157, 28);
            txtBoxName.TabIndex = 1;
            txtBoxName.TextAlign = HorizontalAlignment.Center;
            // 
            // lblAge
            // 
            lblAge.AutoSize = true;
            lblAge.Location = new Point(55, 88);
            lblAge.Name = "lblAge";
            lblAge.Size = new Size(42, 21);
            lblAge.TabIndex = 4;
            lblAge.Text = "年龄";
            // 
            // lblName
            // 
            lblName.AutoSize = true;
            lblName.Location = new Point(55, 34);
            lblName.Name = "lblName";
            lblName.Size = new Size(42, 21);
            lblName.TabIndex = 0;
            lblName.Text = "姓名";
            // 
            // picBoxPhoto
            // 
            picBoxPhoto.BackColor = Color.AliceBlue;
            picBoxPhoto.Location = new Point(668, 18);
            picBoxPhoto.Name = "picBoxPhoto";
            picBoxPhoto.Size = new Size(150, 200);
            picBoxPhoto.SizeMode = PictureBoxSizeMode.Zoom;
            picBoxPhoto.TabIndex = 0;
            picBoxPhoto.TabStop = false;
            // 
            // tabPageCourse
            // 
            tabPageCourse.Controls.Add(lblCourseTips);
            tabPageCourse.Controls.Add(btnCourseFromServer);
            tabPageCourse.Controls.Add(lblIsSelect);
            tabPageCourse.Controls.Add(lblCanSelect);
            tabPageCourse.Controls.Add(lblCourseDesc);
            tabPageCourse.Controls.Add(lsBoxIsSelect);
            tabPageCourse.Controls.Add(lsBoxCanSelect);
            tabPageCourse.Location = new Point(4, 30);
            tabPageCourse.Name = "tabPageCourse";
            tabPageCourse.Padding = new Padding(3);
            tabPageCourse.Size = new Size(836, 404);
            tabPageCourse.TabIndex = 1;
            tabPageCourse.Text = "选课信息";
            tabPageCourse.UseVisualStyleBackColor = true;
            // 
            // lblCourseTips
            // 
            lblCourseTips.AutoSize = true;
            lblCourseTips.Font = new Font("Microsoft YaHei UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 134);
            lblCourseTips.Location = new Point(233, 373);
            lblCourseTips.Name = "lblCourseTips";
            lblCourseTips.Size = new Size(371, 20);
            lblCourseTips.TabIndex = 6;
            lblCourseTips.Text = "向右拖拽可以选课，双击查看详细信息，选中按DEL以退选";
            lblCourseTips.Visible = false;
            // 
            // btnCourseFromServer
            // 
            btnCourseFromServer.Location = new Point(342, 172);
            btnCourseFromServer.Name = "btnCourseFromServer";
            btnCourseFromServer.Size = new Size(152, 63);
            btnCourseFromServer.TabIndex = 5;
            btnCourseFromServer.Text = "根据所填专业\r\n从服务器获取";
            btnCourseFromServer.UseVisualStyleBackColor = true;
            btnCourseFromServer.Click += btnCourseFromServer_Click;
            // 
            // lblIsSelect
            // 
            lblIsSelect.AutoSize = true;
            lblIsSelect.Font = new Font("Microsoft YaHei UI", 14F);
            lblIsSelect.Location = new Point(555, 25);
            lblIsSelect.Name = "lblIsSelect";
            lblIsSelect.Size = new Size(88, 25);
            lblIsSelect.TabIndex = 4;
            lblIsSelect.Text = "已选课程";
            lblIsSelect.Visible = false;
            // 
            // lblCanSelect
            // 
            lblCanSelect.AutoSize = true;
            lblCanSelect.Font = new Font("Microsoft YaHei UI", 14F);
            lblCanSelect.Location = new Point(192, 25);
            lblCanSelect.Name = "lblCanSelect";
            lblCanSelect.Size = new Size(88, 25);
            lblCanSelect.TabIndex = 3;
            lblCanSelect.Text = "可选课程";
            lblCanSelect.Visible = false;
            // 
            // lblCourseDesc
            // 
            lblCourseDesc.AutoSize = true;
            lblCourseDesc.Location = new Point(386, 207);
            lblCourseDesc.Name = "lblCourseDesc";
            lblCourseDesc.Size = new Size(64, 42);
            lblCourseDesc.TabIndex = 2;
            lblCourseDesc.Text = "拖拽到\r\n------>";
            lblCourseDesc.TextAlign = ContentAlignment.MiddleCenter;
            lblCourseDesc.Visible = false;
            // 
            // lsBoxIsSelect
            // 
            lsBoxIsSelect.AllowDrop = true;
            lsBoxIsSelect.FormattingEnabled = true;
            lsBoxIsSelect.ItemHeight = 21;
            lsBoxIsSelect.Location = new Point(459, 66);
            lsBoxIsSelect.Name = "lsBoxIsSelect";
            lsBoxIsSelect.Size = new Size(281, 298);
            lsBoxIsSelect.TabIndex = 1;
            lsBoxIsSelect.Visible = false;
            lsBoxIsSelect.DragDrop += lsBoxIsSelect_DragDrop;
            lsBoxIsSelect.DragEnter += lsBoxIsSelect_DragEnter;
            lsBoxIsSelect.DoubleClick += IsBoxIs_DoubleClick;
            lsBoxIsSelect.KeyDown += IsBoxIs_KeyDown;
            // 
            // lsBoxCanSelect
            // 
            lsBoxCanSelect.FormattingEnabled = true;
            lsBoxCanSelect.ItemHeight = 21;
            lsBoxCanSelect.Location = new Point(96, 67);
            lsBoxCanSelect.Name = "lsBoxCanSelect";
            lsBoxCanSelect.Size = new Size(281, 298);
            lsBoxCanSelect.TabIndex = 0;
            lsBoxCanSelect.Visible = false;
            lsBoxCanSelect.DoubleClick += IsBoxCan_DoubleClick;
            lsBoxCanSelect.MouseDown += lsBoxCanSelect_MouseDown;
            // 
            // tabPageFamily
            // 
            tabPageFamily.Controls.Add(lblFamilyType);
            tabPageFamily.Controls.Add(radioButtonFamilyType2);
            tabPageFamily.Controls.Add(radioButtonFamilyType1);
            tabPageFamily.Controls.Add(txtBoxAdress);
            tabPageFamily.Controls.Add(lblAddress);
            tabPageFamily.Controls.Add(txtBoxHomePhone);
            tabPageFamily.Controls.Add(lblLiving);
            tabPageFamily.Controls.Add(lblHomePhone);
            tabPageFamily.Controls.Add(txtBoxLiving);
            tabPageFamily.Controls.Add(txtBoxZipCode);
            tabPageFamily.Controls.Add(txtBoxBirth);
            tabPageFamily.Controls.Add(lblZipCode);
            tabPageFamily.Controls.Add(lblBirth);
            tabPageFamily.Location = new Point(4, 30);
            tabPageFamily.Name = "tabPageFamily";
            tabPageFamily.Size = new Size(836, 404);
            tabPageFamily.TabIndex = 2;
            tabPageFamily.Text = "家庭信息";
            tabPageFamily.UseVisualStyleBackColor = true;
            // 
            // lblFamilyType
            // 
            lblFamilyType.AutoSize = true;
            lblFamilyType.Location = new Point(102, 54);
            lblFamilyType.Name = "lblFamilyType";
            lblFamilyType.Size = new Size(74, 21);
            lblFamilyType.TabIndex = 23;
            lblFamilyType.Text = "家庭类型";
            // 
            // radioButtonFamilyType2
            // 
            radioButtonFamilyType2.AutoSize = true;
            radioButtonFamilyType2.Location = new Point(430, 52);
            radioButtonFamilyType2.Name = "radioButtonFamilyType2";
            radioButtonFamilyType2.Size = new Size(60, 25);
            radioButtonFamilyType2.TabIndex = 22;
            radioButtonFamilyType2.TabStop = true;
            radioButtonFamilyType2.Text = "乡村";
            radioButtonFamilyType2.UseVisualStyleBackColor = true;
            // 
            // radioButtonFamilyType1
            // 
            radioButtonFamilyType1.AutoSize = true;
            radioButtonFamilyType1.Location = new Point(243, 52);
            radioButtonFamilyType1.Name = "radioButtonFamilyType1";
            radioButtonFamilyType1.Size = new Size(60, 25);
            radioButtonFamilyType1.TabIndex = 21;
            radioButtonFamilyType1.TabStop = true;
            radioButtonFamilyType1.Text = "城镇";
            radioButtonFamilyType1.UseVisualStyleBackColor = true;
            // 
            // txtBoxAdress
            // 
            txtBoxAdress.Location = new Point(207, 218);
            txtBoxAdress.Multiline = true;
            txtBoxAdress.Name = "txtBoxAdress";
            txtBoxAdress.PlaceholderText = "具体到门牌号";
            txtBoxAdress.Size = new Size(496, 98);
            txtBoxAdress.TabIndex = 20;
            // 
            // lblAddress
            // 
            lblAddress.AutoSize = true;
            lblAddress.Location = new Point(102, 221);
            lblAddress.Name = "lblAddress";
            lblAddress.Size = new Size(74, 21);
            lblAddress.TabIndex = 19;
            lblAddress.Text = "详细住址";
            // 
            // txtBoxHomePhone
            // 
            txtBoxHomePhone.Location = new Point(582, 158);
            txtBoxHomePhone.Name = "txtBoxHomePhone";
            txtBoxHomePhone.PlaceholderText = "填阿拉伯数字";
            txtBoxHomePhone.Size = new Size(121, 28);
            txtBoxHomePhone.TabIndex = 18;
            txtBoxHomePhone.TextAlign = HorizontalAlignment.Center;
            // 
            // lblLiving
            // 
            lblLiving.AutoSize = true;
            lblLiving.Location = new Point(430, 107);
            lblLiving.Name = "lblLiving";
            lblLiving.Size = new Size(74, 21);
            lblLiving.TabIndex = 17;
            lblLiving.Text = "现居省市";
            // 
            // lblHomePhone
            // 
            lblHomePhone.AutoSize = true;
            lblHomePhone.Location = new Point(430, 161);
            lblHomePhone.Name = "lblHomePhone";
            lblHomePhone.Size = new Size(74, 21);
            lblHomePhone.TabIndex = 15;
            lblHomePhone.Text = "家庭电话";
            // 
            // txtBoxLiving
            // 
            txtBoxLiving.Location = new Point(582, 104);
            txtBoxLiving.Name = "txtBoxLiving";
            txtBoxLiving.PlaceholderText = "省市（县）";
            txtBoxLiving.Size = new Size(121, 28);
            txtBoxLiving.TabIndex = 14;
            txtBoxLiving.TextAlign = HorizontalAlignment.Center;
            // 
            // txtBoxZipCode
            // 
            txtBoxZipCode.Location = new Point(243, 158);
            txtBoxZipCode.Name = "txtBoxZipCode";
            txtBoxZipCode.PlaceholderText = "填阿拉伯数字";
            txtBoxZipCode.Size = new Size(121, 28);
            txtBoxZipCode.TabIndex = 13;
            txtBoxZipCode.TextAlign = HorizontalAlignment.Center;
            // 
            // txtBoxBirth
            // 
            txtBoxBirth.Location = new Point(243, 104);
            txtBoxBirth.Name = "txtBoxBirth";
            txtBoxBirth.PlaceholderText = "省市（县）";
            txtBoxBirth.Size = new Size(121, 28);
            txtBoxBirth.TabIndex = 12;
            txtBoxBirth.TextAlign = HorizontalAlignment.Center;
            // 
            // lblZipCode
            // 
            lblZipCode.AutoSize = true;
            lblZipCode.Location = new Point(86, 161);
            lblZipCode.Name = "lblZipCode";
            lblZipCode.Size = new Size(90, 21);
            lblZipCode.TabIndex = 11;
            lblZipCode.Text = "现居地邮编";
            // 
            // lblBirth
            // 
            lblBirth.AutoSize = true;
            lblBirth.Location = new Point(118, 107);
            lblBirth.Name = "lblBirth";
            lblBirth.Size = new Size(58, 21);
            lblBirth.TabIndex = 10;
            lblBirth.Text = "出生地";
            // 
            // tabPageConfirm
            // 
            tabPageConfirm.Controls.Add(lblFamily);
            tabPageConfirm.Controls.Add(lblCourse);
            tabPageConfirm.Controls.Add(lblBasic);
            tabPageConfirm.Controls.Add(txtBoxSumFamily);
            tabPageConfirm.Controls.Add(txtBoxSumCourse);
            tabPageConfirm.Controls.Add(txtBoxSumBasic);
            tabPageConfirm.Location = new Point(4, 30);
            tabPageConfirm.Name = "tabPageConfirm";
            tabPageConfirm.Size = new Size(836, 404);
            tabPageConfirm.TabIndex = 3;
            tabPageConfirm.Text = "信息确认与上传";
            tabPageConfirm.UseVisualStyleBackColor = true;
            // 
            // lblFamily
            // 
            lblFamily.AutoSize = true;
            lblFamily.Location = new Point(65, 252);
            lblFamily.Name = "lblFamily";
            lblFamily.Size = new Size(74, 21);
            lblFamily.TabIndex = 13;
            lblFamily.Text = "家庭信息";
            // 
            // lblCourse
            // 
            lblCourse.AutoSize = true;
            lblCourse.Location = new Point(65, 141);
            lblCourse.Name = "lblCourse";
            lblCourse.Size = new Size(74, 21);
            lblCourse.TabIndex = 12;
            lblCourse.Text = "选课信息";
            // 
            // lblBasic
            // 
            lblBasic.AutoSize = true;
            lblBasic.Location = new Point(65, 30);
            lblBasic.Name = "lblBasic";
            lblBasic.Size = new Size(74, 21);
            lblBasic.TabIndex = 11;
            lblBasic.Text = "基本信息";
            // 
            // txtBoxSumFamily
            // 
            txtBoxSumFamily.Enabled = false;
            txtBoxSumFamily.Location = new Point(65, 276);
            txtBoxSumFamily.Multiline = true;
            txtBoxSumFamily.Name = "txtBoxSumFamily";
            txtBoxSumFamily.ScrollBars = ScrollBars.Both;
            txtBoxSumFamily.Size = new Size(707, 77);
            txtBoxSumFamily.TabIndex = 2;
            // 
            // txtBoxSumCourse
            // 
            txtBoxSumCourse.Enabled = false;
            txtBoxSumCourse.Location = new Point(65, 165);
            txtBoxSumCourse.Multiline = true;
            txtBoxSumCourse.Name = "txtBoxSumCourse";
            txtBoxSumCourse.ScrollBars = ScrollBars.Both;
            txtBoxSumCourse.Size = new Size(707, 77);
            txtBoxSumCourse.TabIndex = 1;
            // 
            // txtBoxSumBasic
            // 
            txtBoxSumBasic.Enabled = false;
            txtBoxSumBasic.Location = new Point(65, 54);
            txtBoxSumBasic.Multiline = true;
            txtBoxSumBasic.Name = "txtBoxSumBasic";
            txtBoxSumBasic.ScrollBars = ScrollBars.Both;
            txtBoxSumBasic.Size = new Size(707, 77);
            txtBoxSumBasic.TabIndex = 0;
            txtBoxSumBasic.KeyDown += txtBoxSum_KeyDown;
            // 
            // btnNextPage
            // 
            btnNextPage.Anchor = AnchorStyles.Bottom;
            btnNextPage.Font = new Font("Microsoft YaHei UI", 12F);
            btnNextPage.Location = new Point(434, 11);
            btnNextPage.Name = "btnNextPage";
            btnNextPage.Size = new Size(102, 40);
            btnNextPage.TabIndex = 1;
            btnNextPage.Text = "下一页";
            btnNextPage.UseVisualStyleBackColor = true;
            btnNextPage.Click += pageChange_onClick;
            // 
            // btnLastPage
            // 
            btnLastPage.Anchor = AnchorStyles.Bottom;
            btnLastPage.Enabled = false;
            btnLastPage.Font = new Font("Microsoft YaHei UI", 12F);
            btnLastPage.Location = new Point(309, 11);
            btnLastPage.Name = "btnLastPage";
            btnLastPage.Size = new Size(102, 40);
            btnLastPage.TabIndex = 0;
            btnLastPage.Text = "上一页";
            btnLastPage.UseVisualStyleBackColor = true;
            btnLastPage.Click += pageChange_onClick;
            // 
            // StuInfoEntryForm
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(844, 524);
            Controls.Add(splitContainer1);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            MaximumSize = new Size(860, 563);
            MinimumSize = new Size(860, 563);
            Name = "StuInfoEntryForm";
            Text = "录入 | 学生信息管理系统";
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            tabControl.ResumeLayout(false);
            tabPageBasic.ResumeLayout(false);
            tabPageBasic.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)picBoxPhoto).EndInit();
            tabPageCourse.ResumeLayout(false);
            tabPageCourse.PerformLayout();
            tabPageFamily.ResumeLayout(false);
            tabPageFamily.PerformLayout();
            tabPageConfirm.ResumeLayout(false);
            tabPageConfirm.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStrip1;
        private ToolStripMenuItem menuItem_Opt;
        private ToolStripMenuItem menuItem_SignOut;
        private ToolStripMenuItem 关闭系统ToolStripMenuItem;
        private ToolStripMenuItem menuItem_Close_Save;
        private ToolStripMenuItem menuItem_Close_NoSave;
        private SplitContainer splitContainer1;
        private TabControl tabControl;
        private TabPage tabPageBasic;
        private TabPage tabPageCourse;
        private Button btnNextPage;
        private Button btnLastPage;
        private TabPage tabPageFamily;
        private TabPage tabPageConfirm;
        private TextBox txtBoxAncestry;
        private TextBox txtBoxAge;
        private TextBox txtBoxName;
        private Label lblAge;
        private Label lblName;
        private PictureBox picBoxPhoto;
        private TextBox txtBoxProfile;
        private Label lblProfile;
        private TextBox txtBoxContact;
        private TextBox txtBoxMajor;
        private Label lblContact;
        private Label lblMajor;
        private ComboBox comboBoxQualification;
        private Label lblQualification;
        private ComboBox comboBoxPolitical;
        private Label lblPolitical;
        private Label lblAncestry;
        private ComboBox comboBoxGender;
        private Label lblGender;
        private Button btnUploadPhoto;
        private ListBox lsBoxIsSelect;
        private ListBox lsBoxCanSelect;
        private Label lblCourseDesc;
        private Button btnCourseFromServer;
        private Label lblIsSelect;
        private Label lblCanSelect;
        private TextBox txtBoxAdress;
        private Label lblAddress;
        private TextBox txtBoxHomePhone;
        private Label lblLiving;
        private Label lblHomePhone;
        private TextBox txtBoxLiving;
        private TextBox txtBoxZipCode;
        private TextBox txtBoxBirth;
        private Label lblZipCode;
        private Label lblBirth;
        private TextBox txtBoxSumFamily;
        private TextBox txtBoxSumCourse;
        private TextBox txtBoxSumBasic;
        private Label lblFamily;
        private Label lblCourse;
        private Label lblBasic;
        private Label lblFamilyType;
        private RadioButton radioButtonFamilyType2;
        private RadioButton radioButtonFamilyType1;
        private ToolStripMenuItem menuItem_Course;
        private ToolStripMenuItem menuItem_Opt_Repo;
        private ToolStripMenuItem menuItem_Opt_Read;
        private Label lblCourseTips;
    }
}