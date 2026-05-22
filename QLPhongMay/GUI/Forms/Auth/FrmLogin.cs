using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using QLPhongMay.BLL;
using QLPhongMay.DTO;

namespace QLPhongMay.Auth
{
    public partial class FrmLogin : Form
    {
        private readonly UserService userService;
        private GradientPanel pnlBackground;
        private Panel pnlBrand;
        private Panel pnlLogin;
        private Label lblBrand;
        private Label lblBrandSub;
        private Label lblTitle;
        private Label lblSubtitle;
        private Label lblUsername;
        private Label lblPassword;
        private TextBox txtUsername;
        private TextBox txtPassword;
        private Button btnLogin;
        private Button btnClose;
        private System.ComponentModel.IContainer components;

        public FrmLogin()
        {
            this.userService = new UserService();
            InitializeComponent();
        }

        public User CurrentUser { get; private set; }

        public string Username
        {
            get { return this.txtUsername.Text.Trim(); }
        }

        public string Password
        {
            get { return this.txtPassword.Text; }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && this.components != null)
            {
                this.components.Dispose();
            }

            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.pnlBackground = new GradientPanel();
            this.pnlBrand = new Panel();
            this.pnlLogin = new Panel();
            this.lblBrand = new Label();
            this.lblBrandSub = new Label();
            this.lblTitle = new Label();
            this.lblSubtitle = new Label();
            this.lblUsername = new Label();
            this.lblPassword = new Label();
            this.txtUsername = new TextBox();
            this.txtPassword = new TextBox();
            this.btnLogin = new Button();
            this.btnClose = new Button();
            this.pnlBackground.SuspendLayout();
            this.pnlBrand.SuspendLayout();
            this.pnlLogin.SuspendLayout();
            this.SuspendLayout();

            this.pnlBackground.Dock = DockStyle.Fill;
            this.pnlBackground.StartColor = Color.FromArgb(17, 44, 75);
            this.pnlBackground.EndColor = Color.FromArgb(39, 115, 99);
            this.pnlBackground.Controls.Add(this.btnClose);
            this.pnlBackground.Controls.Add(this.pnlBrand);
            this.pnlBackground.Controls.Add(this.pnlLogin);

            this.btnClose.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            this.btnClose.BackColor = Color.FromArgb(232, 80, 91);
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.FlatStyle = FlatStyle.Flat;
            this.btnClose.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.btnClose.ForeColor = Color.White;
            this.btnClose.Location = new Point(764, 18);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new Size(38, 32);
            this.btnClose.TabIndex = 8;
            this.btnClose.Text = "X";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new EventHandler(this.BtnClose_Click);

            this.pnlBrand.BackColor = Color.Transparent;
            this.pnlBrand.Controls.Add(this.lblBrand);
            this.pnlBrand.Controls.Add(this.lblBrandSub);
            this.pnlBrand.Location = new Point(54, 66);
            this.pnlBrand.Name = "pnlBrand";
            this.pnlBrand.Size = new Size(340, 380);

            this.lblBrand.AutoSize = true;
            this.lblBrand.BackColor = Color.Transparent;
            this.lblBrand.Font = new Font("Segoe UI", 25F, FontStyle.Bold);
            this.lblBrand.ForeColor = Color.White;
            this.lblBrand.Location = new Point(0, 92);
            this.lblBrand.Name = "lblBrand";
            this.lblBrand.Size = new Size(301, 57);
            this.lblBrand.Text = "QL Phòng Máy";

            this.lblBrandSub.BackColor = Color.Transparent;
            this.lblBrandSub.Font = new Font("Segoe UI", 11F);
            this.lblBrandSub.ForeColor = Color.FromArgb(219, 246, 241);
            this.lblBrandSub.Location = new Point(4, 166);
            this.lblBrandSub.Name = "lblBrandSub";
            this.lblBrandSub.Size = new Size(314, 62);
            this.lblBrandSub.Text = "Quản lý phòng máy nhanh gọn,\r\nbảo mật và dễ sử dụng cho nhân viên.";

            this.pnlLogin.BackColor = Color.White;
            this.pnlLogin.Controls.Add(this.lblTitle);
            this.pnlLogin.Controls.Add(this.lblSubtitle);
            this.pnlLogin.Controls.Add(this.lblUsername);
            this.pnlLogin.Controls.Add(this.txtUsername);
            this.pnlLogin.Controls.Add(this.lblPassword);
            this.pnlLogin.Controls.Add(this.txtPassword);
            this.pnlLogin.Controls.Add(this.btnLogin);
            this.pnlLogin.Location = new Point(444, 58);
            this.pnlLogin.Name = "pnlLogin";
            this.pnlLogin.Size = new Size(326, 404);
            this.pnlLogin.Paint += new PaintEventHandler(this.PnlLogin_Paint);

            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new Font("Segoe UI", 22F, FontStyle.Bold);
            this.lblTitle.ForeColor = Color.FromArgb(23, 37, 84);
            this.lblTitle.Location = new Point(34, 36);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new Size(213, 50);
            this.lblTitle.Text = "Đăng nhập";

            this.lblSubtitle.AutoSize = true;
            this.lblSubtitle.Font = new Font("Segoe UI", 9.5F);
            this.lblSubtitle.ForeColor = Color.FromArgb(107, 114, 128);
            this.lblSubtitle.Location = new Point(36, 90);
            this.lblSubtitle.Name = "lblSubtitle";
            this.lblSubtitle.Size = new Size(206, 21);
            this.lblSubtitle.Text = "Nhập tài khoản để tiếp tục";

            this.lblUsername.AutoSize = true;
            this.lblUsername.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            this.lblUsername.ForeColor = Color.FromArgb(55, 65, 81);
            this.lblUsername.Location = new Point(36, 138);
            this.lblUsername.Name = "lblUsername";
            this.lblUsername.Size = new Size(124, 21);
            this.lblUsername.Text = "Tên đăng nhập";

            this.txtUsername.BackColor = Color.FromArgb(248, 250, 252);
            this.txtUsername.BorderStyle = BorderStyle.FixedSingle;
            this.txtUsername.Font = new Font("Segoe UI", 11F);
            this.txtUsername.Location = new Point(36, 166);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new Size(254, 32);
            this.txtUsername.TabIndex = 0;

            this.lblPassword.AutoSize = true;
            this.lblPassword.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            this.lblPassword.ForeColor = Color.FromArgb(55, 65, 81);
            this.lblPassword.Location = new Point(36, 224);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new Size(83, 21);
            this.lblPassword.Text = "Mật khẩu";

            this.txtPassword.BackColor = Color.FromArgb(248, 250, 252);
            this.txtPassword.BorderStyle = BorderStyle.FixedSingle;
            this.txtPassword.Font = new Font("Segoe UI", 11F);
            this.txtPassword.Location = new Point(36, 252);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new Size(254, 32);
            this.txtPassword.TabIndex = 1;
            this.txtPassword.UseSystemPasswordChar = true;

            this.btnLogin.BackColor = Color.FromArgb(37, 99, 235);
            this.btnLogin.Cursor = Cursors.Hand;
            this.btnLogin.FlatAppearance.BorderSize = 0;
            this.btnLogin.FlatStyle = FlatStyle.Flat;
            this.btnLogin.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.btnLogin.ForeColor = Color.White;
            this.btnLogin.Location = new Point(36, 328);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new Size(254, 44);
            this.btnLogin.TabIndex = 2;
            this.btnLogin.Text = "Đăng nhập";
            this.btnLogin.UseVisualStyleBackColor = false;
            this.btnLogin.Click += new EventHandler(this.BtnLogin_Click);

            this.AcceptButton = this.btnLogin;
            this.AutoScaleDimensions = new SizeF(8F, 20F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = Color.White;
            this.ClientSize = new Size(820, 520);
            this.Controls.Add(this.pnlBackground);
            this.Font = new Font("Segoe UI", 9F);
            this.FormBorderStyle = FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmLogin";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Đăng nhập";
            this.pnlBackground.ResumeLayout(false);
            this.pnlBrand.ResumeLayout(false);
            this.pnlBrand.PerformLayout();
            this.pnlLogin.ResumeLayout(false);
            this.pnlLogin.PerformLayout();
            this.ResumeLayout(false);
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void PnlLogin_Paint(object sender, PaintEventArgs e)
        {
            using (Pen pen = new Pen(Color.FromArgb(226, 232, 240)))
            {
                Rectangle rect = new Rectangle(0, 0, this.pnlLogin.Width - 1, this.pnlLogin.Height - 1);
                e.Graphics.DrawRectangle(pen, rect);
            }
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(this.Username))
            {
                MessageBox.Show("Vui lòng nhập tên đăng nhập.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.txtUsername.Focus();
                return;
            }

            if (string.IsNullOrEmpty(this.Password))
            {
                MessageBox.Show("Vui lòng nhập mật khẩu.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.txtPassword.Focus();
                return;
            }

            User user = this.userService.Login(this.Username, this.Password);
            if (user == null)
            {
                MessageBox.Show("Tên đăng nhập hoặc mật khẩu không đúng.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.txtPassword.Clear();
                this.txtPassword.Focus();
                return;
            }

            this.CurrentUser = user;
            Session.SignIn(user);
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private class GradientPanel : Panel
        {
            public Color StartColor { get; set; }
            public Color EndColor { get; set; }

            protected override void OnPaint(PaintEventArgs e)
            {
                using (LinearGradientBrush brush = new LinearGradientBrush(this.ClientRectangle, this.StartColor, this.EndColor, LinearGradientMode.ForwardDiagonal))
                {
                    e.Graphics.FillRectangle(brush, this.ClientRectangle);
                }

                base.OnPaint(e);
            }
        }
    }
}
