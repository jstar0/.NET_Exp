namespace StudentInformationEntrySystem
{
    partial class LicenseForm
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
            lblLicenseTitle = new Label();
            label1 = new Label();
            label2 = new Label();
            linkLblFrontEnd = new LinkLabel();
            linkLblBackEnd = new LinkLabel();
            btnLicenseAgree = new Button();
            btnLicenseDisagree = new Button();
            SuspendLayout();
            // 
            // lblLicenseTitle
            // 
            lblLicenseTitle.AutoSize = true;
            lblLicenseTitle.Font = new Font("Microsoft YaHei UI", 20F, FontStyle.Bold, GraphicsUnit.Point, 134);
            lblLicenseTitle.Location = new Point(187, 25);
            lblLicenseTitle.Name = "lblLicenseTitle";
            lblLicenseTitle.Size = new Size(339, 36);
            lblLicenseTitle.TabIndex = 0;
            lblLicenseTitle.Text = "欢迎使用学生信息管理系统";
            lblLicenseTitle.Click += label1_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Microsoft YaHei UI", 14F);
            label1.Location = new Point(25, 94);
            label1.Name = "label1";
            label1.Size = new Size(666, 50);
            label1.TabIndex = 1;
            label1.Text = "如需调整服务器地址，请转至本目录下唯一的 config 文件: baseUrl 字段。\r\n客户端和后端代码开源在 GitHub，基于 GPL-3.0 license，请勿滥用该项目。";
            label1.Click += label1_Click_1;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Microsoft YaHei UI", 14F);
            label2.Location = new Point(25, 264);
            label2.Name = "label2";
            label2.Size = new Size(582, 50);
            label2.TabIndex = 2;
            label2.Text = "当您点击确定，表示已知悉上述提示信息，以后运行程序不再显示。\r\n取消将退出程序。";
            label2.Click += label2_Click;
            // 
            // linkLblFrontEnd
            // 
            linkLblFrontEnd.AutoSize = true;
            linkLblFrontEnd.Font = new Font("Microsoft YaHei UI", 14F);
            linkLblFrontEnd.Location = new Point(147, 193);
            linkLblFrontEnd.Name = "linkLblFrontEnd";
            linkLblFrontEnd.Size = new Size(147, 25);
            linkLblFrontEnd.TabIndex = 3;
            linkLblFrontEnd.TabStop = true;
            linkLblFrontEnd.Text = "前端仓库 (.NET)";
            linkLblFrontEnd.LinkClicked += linkLblFrontEnd_Click;
            // 
            // linkLblBackEnd
            // 
            linkLblBackEnd.AutoSize = true;
            linkLblBackEnd.Font = new Font("Microsoft YaHei UI", 14F);
            linkLblBackEnd.Location = new Point(398, 193);
            linkLblBackEnd.Name = "linkLblBackEnd";
            linkLblBackEnd.Size = new Size(167, 25);
            linkLblBackEnd.TabIndex = 4;
            linkLblBackEnd.TabStop = true;
            linkLblBackEnd.Text = "后端仓库 (NestJS)";
            linkLblBackEnd.LinkClicked += linkLbl_BackEnd;
            // 
            // btnLicenseAgree
            // 
            btnLicenseAgree.Font = new Font("Microsoft YaHei UI", 14F, FontStyle.Regular, GraphicsUnit.Point, 134);
            btnLicenseAgree.Location = new Point(170, 358);
            btnLicenseAgree.Name = "btnLicenseAgree";
            btnLicenseAgree.Size = new Size(161, 48);
            btnLicenseAgree.TabIndex = 5;
            btnLicenseAgree.Text = "确定";
            btnLicenseAgree.UseVisualStyleBackColor = true;
            btnLicenseAgree.Click += btnLicenseAgree_Click;
            // 
            // btnLicenseDisagree
            // 
            btnLicenseDisagree.Font = new Font("Microsoft YaHei UI", 14F, FontStyle.Regular, GraphicsUnit.Point, 134);
            btnLicenseDisagree.Location = new Point(381, 357);
            btnLicenseDisagree.Name = "btnLicenseDisagree";
            btnLicenseDisagree.Size = new Size(161, 48);
            btnLicenseDisagree.TabIndex = 6;
            btnLicenseDisagree.Text = "取消";
            btnLicenseDisagree.UseVisualStyleBackColor = true;
            btnLicenseDisagree.Click += btnLicenseDisagree_Click;
            // 
            // LicenseForm
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(713, 450);
            Controls.Add(btnLicenseDisagree);
            Controls.Add(btnLicenseAgree);
            Controls.Add(linkLblBackEnd);
            Controls.Add(linkLblFrontEnd);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(lblLicenseTitle);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Name = "LicenseForm";
            Text = "协议 | 学生信息管理系统";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblLicenseTitle;
        private Label label1;
        private Label label2;
        private LinkLabel linkLblFrontEnd;
        private LinkLabel linkLblBackEnd;
        private Button btnLicenseAgree;
        private Button btnLicenseDisagree;
    }
}