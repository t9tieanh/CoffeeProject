namespace CaffeeShop
{
    partial class FormChangePassWord
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormChangePassWord));
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            btnChangePass = new Guna.UI2.WinForms.Guna2Button();
            lblcheckloipasscu = new Label();
            lblcheckLoiPassmoi = new Label();
            txtMkmoi = new Guna.UI2.WinForms.Guna2TextBox();
            txtMkcu = new Guna.UI2.WinForms.Guna2TextBox();
            lblthongtintk = new Label();
            label14 = new Label();
            label11 = new Label();
            label13 = new Label();
            button18 = new Button();
            guna2TabControl1 = new Guna.UI2.WinForms.Guna2TabControl();
            tabPage1 = new TabPage();
            lblTaiKhoan = new Label();
            guna2TabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
            SuspendLayout();
            // 
            // btnChangePass
            // 
            btnChangePass.BackColor = Color.Transparent;
            btnChangePass.BackgroundImage = (Image)resources.GetObject("btnChangePass.BackgroundImage");
            btnChangePass.BorderRadius = 15;
            btnChangePass.CustomizableEdges = customizableEdges1;
            btnChangePass.DisabledState.BorderColor = Color.DarkGray;
            btnChangePass.DisabledState.CustomBorderColor = Color.DarkGray;
            btnChangePass.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btnChangePass.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btnChangePass.FillColor = SystemColors.Highlight;
            btnChangePass.Font = new Font("Consolas", 12F, FontStyle.Bold, GraphicsUnit.Point);
            btnChangePass.ForeColor = Color.White;
            btnChangePass.Location = new Point(625, 316);
            btnChangePass.Margin = new Padding(2);
            btnChangePass.Name = "btnChangePass";
            btnChangePass.PressedColor = SystemColors.AppWorkspace;
            btnChangePass.ShadowDecoration.CustomizableEdges = customizableEdges2;
            btnChangePass.Size = new Size(212, 34);
            btnChangePass.TabIndex = 36;
            btnChangePass.Text = "ChangePassWord";
            btnChangePass.Click += btnChangePass_Click;
            // 
            // lblcheckloipasscu
            // 
            lblcheckloipasscu.AutoSize = true;
            lblcheckloipasscu.Font = new Font("Consolas", 7.8F, FontStyle.Bold, GraphicsUnit.Point);
            lblcheckloipasscu.ForeColor = Color.Red;
            lblcheckloipasscu.Location = new Point(487, 178);
            lblcheckloipasscu.Margin = new Padding(2, 0, 2, 0);
            lblcheckloipasscu.Name = "lblcheckloipasscu";
            lblcheckloipasscu.Size = new Size(14, 15);
            lblcheckloipasscu.TabIndex = 35;
            lblcheckloipasscu.Text = "!";
            // 
            // lblcheckLoiPassmoi
            // 
            lblcheckLoiPassmoi.AutoSize = true;
            lblcheckLoiPassmoi.Font = new Font("Consolas", 7.8F, FontStyle.Bold, GraphicsUnit.Point);
            lblcheckLoiPassmoi.ForeColor = Color.Red;
            lblcheckLoiPassmoi.Location = new Point(487, 264);
            lblcheckLoiPassmoi.Margin = new Padding(2, 0, 2, 0);
            lblcheckLoiPassmoi.Name = "lblcheckLoiPassmoi";
            lblcheckLoiPassmoi.Size = new Size(14, 15);
            lblcheckLoiPassmoi.TabIndex = 34;
            lblcheckLoiPassmoi.Text = "!";
            // 
            // txtMkmoi
            // 
            txtMkmoi.BorderRadius = 15;
            txtMkmoi.BorderThickness = 2;
            txtMkmoi.CustomizableEdges = customizableEdges3;
            txtMkmoi.DefaultText = "";
            txtMkmoi.DisabledState.BorderColor = Color.FromArgb(208, 208, 208);
            txtMkmoi.DisabledState.FillColor = Color.FromArgb(226, 226, 226);
            txtMkmoi.DisabledState.ForeColor = Color.FromArgb(138, 138, 138);
            txtMkmoi.DisabledState.PlaceholderForeColor = Color.FromArgb(138, 138, 138);
            txtMkmoi.FocusedState.BorderColor = Color.FromArgb(94, 148, 255);
            txtMkmoi.Font = new Font("Consolas", 10.8F, FontStyle.Bold, GraphicsUnit.Point);
            txtMkmoi.ForeColor = Color.Black;
            txtMkmoi.HoverState.BorderColor = Color.FromArgb(94, 148, 255);
            txtMkmoi.Location = new Point(487, 206);
            txtMkmoi.Margin = new Padding(3, 2, 3, 2);
            txtMkmoi.Name = "txtMkmoi";
            txtMkmoi.PasswordChar = '*';
            txtMkmoi.PlaceholderText = "Nhập mật khẩu mới";
            txtMkmoi.SelectedText = "";
            txtMkmoi.ShadowDecoration.CustomizableEdges = customizableEdges4;
            txtMkmoi.Size = new Size(252, 46);
            txtMkmoi.TabIndex = 33;
            // 
            // txtMkcu
            // 
            txtMkcu.BorderRadius = 15;
            txtMkcu.BorderThickness = 2;
            txtMkcu.CustomizableEdges = customizableEdges5;
            txtMkcu.DefaultText = "";
            txtMkcu.DisabledState.BorderColor = Color.FromArgb(208, 208, 208);
            txtMkcu.DisabledState.FillColor = Color.FromArgb(226, 226, 226);
            txtMkcu.DisabledState.ForeColor = Color.FromArgb(138, 138, 138);
            txtMkcu.DisabledState.PlaceholderForeColor = Color.FromArgb(138, 138, 138);
            txtMkcu.FocusedState.BorderColor = Color.FromArgb(94, 148, 255);
            txtMkcu.Font = new Font("Consolas", 10.8F, FontStyle.Bold, GraphicsUnit.Point);
            txtMkcu.ForeColor = Color.Black;
            txtMkcu.HoverState.BorderColor = Color.FromArgb(94, 148, 255);
            txtMkcu.Location = new Point(487, 121);
            txtMkcu.Margin = new Padding(3, 2, 3, 2);
            txtMkcu.Name = "txtMkcu";
            txtMkcu.PasswordChar = '*';
            txtMkcu.PlaceholderText = "Nhập mật khẩu cũ ";
            txtMkcu.SelectedText = "";
            txtMkcu.ShadowDecoration.CustomizableEdges = customizableEdges6;
            txtMkcu.Size = new Size(252, 46);
            txtMkcu.TabIndex = 32;
            // 
            // lblthongtintk
            // 
            lblthongtintk.AutoSize = true;
            lblthongtintk.Font = new Font("Consolas", 10.2F, FontStyle.Bold, GraphicsUnit.Point);
            lblthongtintk.ForeColor = Color.FromArgb(64, 0, 0);
            lblthongtintk.Location = new Point(256, -77);
            lblthongtintk.Margin = new Padding(2, 0, 2, 0);
            lblthongtintk.Name = "lblthongtintk";
            lblthongtintk.Size = new Size(0, 20);
            lblthongtintk.TabIndex = 31;
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Font = new Font("Consolas", 10.2F, FontStyle.Bold, GraphicsUnit.Point);
            label14.ForeColor = Color.Black;
            label14.Location = new Point(54, -77);
            label14.Margin = new Padding(2, 0, 2, 0);
            label14.Name = "label14";
            label14.Size = new Size(198, 20);
            label14.TabIndex = 30;
            label14.Text = "Thông tin tài khoản :";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.BackColor = Color.Black;
            label11.Font = new Font("Lucida Calligraphy", 10.8F, FontStyle.Bold, GraphicsUnit.Point);
            label11.ForeColor = Color.WhiteSmoke;
            label11.Location = new Point(210, 206);
            label11.Margin = new Padding(2, 0, 2, 0);
            label11.Name = "label11";
            label11.Size = new Size(216, 23);
            label11.TabIndex = 28;
            label11.Text = "Nhập password mới";
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.BackColor = Color.Black;
            label13.Font = new Font("Lucida Calligraphy", 10.8F, FontStyle.Bold, GraphicsUnit.Point);
            label13.ForeColor = Color.Transparent;
            label13.Location = new Point(226, 121);
            label13.Margin = new Padding(2, 0, 2, 0);
            label13.Name = "label13";
            label13.Size = new Size(200, 23);
            label13.TabIndex = 29;
            label13.Text = "Nhập password cũ";
            // 
            // button18
            // 
            button18.BackColor = Color.SaddleBrown;
            button18.Font = new Font("Century Schoolbook", 11F, FontStyle.Bold, GraphicsUnit.Point);
            button18.ForeColor = Color.White;
            button18.Location = new Point(872, 6);
            button18.Margin = new Padding(2);
            button18.Name = "button18";
            button18.Size = new Size(34, 26);
            button18.TabIndex = 38;
            button18.Text = "X";
            button18.UseVisualStyleBackColor = false;
            button18.Click += button18_Click;
            // 
            // guna2TabControl1
            // 
            guna2TabControl1.Alignment = TabAlignment.Left;
            guna2TabControl1.Controls.Add(tabPage1);
            guna2TabControl1.ItemSize = new Size(180, 40);
            guna2TabControl1.Location = new Point(41, 71);
            guna2TabControl1.Name = "guna2TabControl1";
            guna2TabControl1.SelectedIndex = 0;
            guna2TabControl1.Size = new Size(920, 405);
            guna2TabControl1.TabButtonHoverState.BorderColor = Color.Empty;
            guna2TabControl1.TabButtonHoverState.FillColor = Color.FromArgb(40, 52, 70);
            guna2TabControl1.TabButtonHoverState.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Regular, GraphicsUnit.Point);
            guna2TabControl1.TabButtonHoverState.ForeColor = Color.White;
            guna2TabControl1.TabButtonHoverState.InnerColor = Color.FromArgb(40, 52, 70);
            guna2TabControl1.TabButtonIdleState.BorderColor = Color.Empty;
            guna2TabControl1.TabButtonIdleState.FillColor = Color.FromArgb(33, 42, 57);
            guna2TabControl1.TabButtonIdleState.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Regular, GraphicsUnit.Point);
            guna2TabControl1.TabButtonIdleState.ForeColor = Color.FromArgb(156, 160, 167);
            guna2TabControl1.TabButtonIdleState.InnerColor = Color.FromArgb(33, 42, 57);
            guna2TabControl1.TabButtonSelectedState.BorderColor = Color.Empty;
            guna2TabControl1.TabButtonSelectedState.FillColor = Color.FromArgb(29, 37, 49);
            guna2TabControl1.TabButtonSelectedState.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Regular, GraphicsUnit.Point);
            guna2TabControl1.TabButtonSelectedState.ForeColor = Color.White;
            guna2TabControl1.TabButtonSelectedState.InnerColor = Color.FromArgb(76, 132, 255);
            guna2TabControl1.TabButtonSize = new Size(180, 40);
            guna2TabControl1.TabIndex = 39;
            guna2TabControl1.TabMenuBackColor = Color.FromArgb(33, 42, 57);
            guna2TabControl1.TabMenuVisible = false;
            // 
            // tabPage1
            // 
            tabPage1.BackgroundImage = Properties.Resources._10_quan_cafe_tone_trang_cuc_trend_o_sai_gon_dang_hot_ram_ro_tren_mang_xa_hoi_202111170739483743;
            tabPage1.Controls.Add(lblTaiKhoan);
            tabPage1.Controls.Add(txtMkmoi);
            tabPage1.Controls.Add(button18);
            tabPage1.Controls.Add(label13);
            tabPage1.Controls.Add(btnChangePass);
            tabPage1.Controls.Add(label11);
            tabPage1.Controls.Add(lblcheckloipasscu);
            tabPage1.Controls.Add(txtMkcu);
            tabPage1.Controls.Add(lblcheckLoiPassmoi);
            tabPage1.Location = new Point(5, 4);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(911, 397);
            tabPage1.TabIndex = 0;
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // lblTaiKhoan
            // 
            lblTaiKhoan.AutoSize = true;
            lblTaiKhoan.BackColor = Color.Black;
            lblTaiKhoan.Font = new Font("Lucida Calligraphy", 7.8F, FontStyle.Bold, GraphicsUnit.Point);
            lblTaiKhoan.ForeColor = Color.WhiteSmoke;
            lblTaiKhoan.Location = new Point(5, 13);
            lblTaiKhoan.Margin = new Padding(2, 0, 2, 0);
            lblTaiKhoan.Name = "lblTaiKhoan";
            lblTaiKhoan.Size = new Size(169, 17);
            lblTaiKhoan.TabIndex = 39;
            lblTaiKhoan.Text = "Thông tin tài khoản ";
            // 
            // FormChangePassWord
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources._10_quan_cafe_tone_trang_cuc_trend_o_sai_gon_dang_hot_ram_ro_tren_mang_xa_hoi_202111170739483743;
            ClientSize = new Size(1066, 553);
            Controls.Add(guna2TabControl1);
            Controls.Add(lblthongtintk);
            Controls.Add(label14);
            FormBorderStyle = FormBorderStyle.None;
            Name = "FormChangePassWord";
            Text = "ChangePassWord";
            guna2TabControl1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            tabPage1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Guna.UI2.WinForms.Guna2Button btnChangePass;
        private Label lblcheckloipasscu;
        private Label lblcheckLoiPassmoi;
        private Guna.UI2.WinForms.Guna2TextBox txtMkmoi;
        private Guna.UI2.WinForms.Guna2TextBox txtMkcu;
        private Label lblthongtintk;
        private Label label14;
        private Label label11;
        private Label label13;
        private Button button18;
        private Guna.UI2.WinForms.Guna2TabControl guna2TabControl1;
        private TabPage tabPage1;
        private Label lblTaiKhoan;
    }
}