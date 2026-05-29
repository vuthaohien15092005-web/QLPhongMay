using System;
using System.Drawing;
using System.Windows.Forms;
using QLPhongMay.Auth;
using QLPhongMay.BLL;
using QLPhongMay.DTO;
using QLPhongMay.Enums;
using QLPhongMay.GUI.Forms.Catalog;
using QLPhongMay.GUI.Forms.Computer;
using QLPhongMay.GUI.Forms.Reports;
using QLPhongMay.GUI.Forms.Schedule;

namespace QLPhongMay.GUI.Forms.Dashboard
{
    public partial class frmMain_QLPM : Form
    {
        private Panel pnlSidebar;
        private Panel pnlHeader;
        private Panel pnlContent;
        private Panel pnlAccount;
        private Label lblBrand;
        private Label lblRole;
        private Label lblTitle;
        private Label lblWelcome;
        private Label lblAccountTitle;
        private Label lblUsername;
        private Label lblFullName;
        private Label lblEmail;
        private Label lblAccountRole;
        private Button btnLogout;

        public frmMain_QLPM()
        {
            InitializeComponent();
            BuildMenu();
            LoadAccountInfo();
        }

        private void InitializeComponent()
        {
            this.pnlSidebar = new Panel();
            this.pnlHeader = new Panel();
            this.pnlContent = new Panel();
            this.pnlAccount = new Panel();
            this.lblBrand = new Label();
            this.lblRole = new Label();
            this.lblTitle = new Label();
            this.lblWelcome = new Label();
            this.lblAccountTitle = new Label();
            this.lblUsername = new Label();
            this.lblFullName = new Label();
            this.lblEmail = new Label();
            this.lblAccountRole = new Label();
            this.btnLogout = new Button();
            this.pnlSidebar.SuspendLayout();
            this.pnlHeader.SuspendLayout();
            this.pnlContent.SuspendLayout();
            this.pnlAccount.SuspendLayout();
            this.SuspendLayout();

            this.pnlSidebar.BackColor = Color.FromArgb(15, 76, 129);
            this.pnlSidebar.Controls.Add(this.lblBrand);
            this.pnlSidebar.Controls.Add(this.lblRole);
            this.pnlSidebar.Dock = DockStyle.Left;
            this.pnlSidebar.Location = new Point(0, 0);
            this.pnlSidebar.Name = "pnlSidebar";
            this.pnlSidebar.Size = new Size(240, 760);

            this.lblBrand.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            this.lblBrand.ForeColor = Color.White;
            this.lblBrand.Location = new Point(22, 28);
            this.lblBrand.Name = "lblBrand";
            this.lblBrand.Size = new Size(196, 78);
            this.lblBrand.Text = "QL Phòng Máy";
            this.lblBrand.TextAlign = ContentAlignment.MiddleLeft;

            this.lblRole.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.lblRole.ForeColor = Color.FromArgb(191, 219, 254);
            this.lblRole.Location = new Point(24, 102);
            this.lblRole.Name = "lblRole";
            this.lblRole.Size = new Size(190, 24);
            this.lblRole.Text = "QUẢN LÝ PHÒNG MÁY";

            this.pnlHeader.BackColor = Color.White;
            this.pnlHeader.Controls.Add(this.lblTitle);
            this.pnlHeader.Controls.Add(this.lblWelcome);
            this.pnlHeader.Controls.Add(this.btnLogout);
            this.pnlHeader.Dock = DockStyle.Top;
            this.pnlHeader.Location = new Point(240, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new Size(940, 96);
            this.pnlHeader.Paint += BorderBottom_Paint;

            this.lblTitle.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            this.lblTitle.ForeColor = Color.FromArgb(15, 23, 42);
            this.lblTitle.Location = new Point(34, 22);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new Size(460, 38);
            this.lblTitle.Text = "Bảng điều khiển phòng máy";

            this.lblWelcome.Font = new Font("Segoe UI", 9.5F);
            this.lblWelcome.ForeColor = Color.FromArgb(100, 116, 139);
            this.lblWelcome.Location = new Point(38, 61);
            this.lblWelcome.Name = "lblWelcome";
            this.lblWelcome.Size = new Size(620, 22);
            this.lblWelcome.Text = "Quản lý lịch, phòng máy, máy tính, cấu hình, lớp học và ca học";

            this.btnLogout.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            this.btnLogout.BackColor = Color.White;
            this.btnLogout.Cursor = Cursors.Hand;
            this.btnLogout.FlatAppearance.BorderColor = Color.FromArgb(37, 99, 235);
            this.btnLogout.FlatStyle = FlatStyle.Flat;
            this.btnLogout.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.btnLogout.ForeColor = Color.FromArgb(37, 99, 235);
            this.btnLogout.Location = new Point(790, 28);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new Size(116, 38);
            this.btnLogout.Text = "Đăng xuất";
            this.btnLogout.UseVisualStyleBackColor = false;
            this.btnLogout.Click += BtnLogout_Click;

            this.pnlContent.BackColor = Color.FromArgb(241, 245, 249);
            this.pnlContent.Controls.Add(this.pnlAccount);
            this.pnlContent.Dock = DockStyle.Fill;
            this.pnlContent.Location = new Point(240, 96);
            this.pnlContent.Name = "pnlContent";
            this.pnlContent.Size = new Size(940, 664);

            this.pnlAccount.Anchor = AnchorStyles.None;
            this.pnlAccount.BackColor = Color.White;
            this.pnlAccount.Controls.Add(this.lblAccountTitle);
            this.pnlAccount.Controls.Add(this.lblUsername);
            this.pnlAccount.Controls.Add(this.lblFullName);
            this.pnlAccount.Controls.Add(this.lblEmail);
            this.pnlAccount.Controls.Add(this.lblAccountRole);
            this.pnlAccount.Location = new Point(190, 120);
            this.pnlAccount.Name = "pnlAccount";
            this.pnlAccount.Size = new Size(560, 330);
            this.pnlAccount.Paint += AccountPanel_Paint;

            this.lblAccountTitle.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            this.lblAccountTitle.ForeColor = Color.FromArgb(15, 76, 129);
            this.lblAccountTitle.Location = new Point(40, 30);
            this.lblAccountTitle.Name = "lblAccountTitle";
            this.lblAccountTitle.Size = new Size(480, 44);
            this.lblAccountTitle.Text = "Thông tin tài khoản";
            this.lblAccountTitle.TextAlign = ContentAlignment.MiddleCenter;

            this.lblUsername.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            this.lblUsername.ForeColor = Color.FromArgb(30, 41, 59);
            this.lblUsername.Location = new Point(78, 112);
            this.lblUsername.Name = "lblUsername";
            this.lblUsername.Size = new Size(420, 30);

            this.lblFullName.Font = new Font("Segoe UI", 12F);
            this.lblFullName.ForeColor = Color.FromArgb(51, 65, 85);
            this.lblFullName.Location = new Point(78, 158);
            this.lblFullName.Name = "lblFullName";
            this.lblFullName.Size = new Size(420, 30);

            this.lblEmail.Font = new Font("Segoe UI", 12F);
            this.lblEmail.ForeColor = Color.FromArgb(51, 65, 85);
            this.lblEmail.Location = new Point(78, 204);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new Size(420, 30);

            this.lblAccountRole.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            this.lblAccountRole.ForeColor = Color.FromArgb(37, 99, 235);
            this.lblAccountRole.Location = new Point(78, 250);
            this.lblAccountRole.Name = "lblAccountRole";
            this.lblAccountRole.Size = new Size(420, 30);

            this.AutoScaleDimensions = new SizeF(8F, 20F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = Color.FromArgb(241, 245, 249);
            this.ClientSize = new Size(1180, 760);
            this.Controls.Add(this.pnlContent);
            this.Controls.Add(this.pnlHeader);
            this.Controls.Add(this.pnlSidebar);
            this.Font = new Font("Segoe UI", 9F);
            this.MinimumSize = new Size(1040, 680);
            this.Name = "frmMain_QLPM";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Quản lý phòng máy";
            this.pnlSidebar.ResumeLayout(false);
            this.pnlHeader.ResumeLayout(false);
            this.pnlContent.ResumeLayout(false);
            this.pnlAccount.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        private void BuildMenu()
        {
            int top = 152;
            AddMenuButton("Quản lý lịch", top, this.OpenScheduleForm);
            AddMenuButton("Quản lý phòng máy", top += 56, this.OpenRoomForm);
            AddMenuButton("Quản lý máy tính", top += 56, this.OpenComputerForm);
            AddMenuButton("Quản lý cấu hình", top += 56, this.OpenConfigForm);
            AddMenuButton("Quản lý lớp học", top += 56, this.OpenClassForm);
            AddMenuButton("Quản lý ca học", top += 56, this.OpenShiftForm);
            AddMenuButton("Báo cáo và Thống kê", top += 56, this.OpenReportForm);
        }

        private void AddMenuButton(string text, int top, EventHandler clickHandler)
        {
            Button button = new Button();
            button.BackColor = Color.FromArgb(29, 105, 174);
            button.Cursor = Cursors.Hand;
            button.FlatAppearance.BorderColor = Color.FromArgb(147, 197, 253);
            button.FlatStyle = FlatStyle.Flat;
            button.Font = new Font("Segoe UI", text.Length > 18 ? 8.5F : 10F, FontStyle.Bold);
            button.ForeColor = Color.White;
            button.Location = new Point(18, top);
            button.Name = "btn" + text.Replace(" ", string.Empty);
            button.Size = new Size(204, 44);
            button.Text = text;
            button.TextAlign = ContentAlignment.MiddleCenter;
            button.UseVisualStyleBackColor = false;
            button.Click += clickHandler;
            button.MouseEnter += delegate { button.BackColor = Color.FromArgb(37, 99, 235); };
            button.MouseLeave += delegate { button.BackColor = Color.FromArgb(29, 105, 174); };
            this.pnlSidebar.Controls.Add(button);
        }

        private void LoadAccountInfo()
        {
            User user = Session.CurrentUser;
            string username = user == null ? "Chưa xác định" : user.TenDangNhap;
            string fullName = user == null ? "Chưa xác định" : user.HoTen;
            string email = user == null ? "Chưa xác định" : user.Email;
            string role = user == null ? "Quản lý phòng máy" : (string.IsNullOrWhiteSpace(user.TenVaiTro) ? "Quản lý phòng máy" : user.TenVaiTro);

            this.lblUsername.Text = "Tên đăng nhập: " + username;
            this.lblFullName.Text = "Họ tên: " + fullName;
            this.lblEmail.Text = "Email: " + email;
            this.lblAccountRole.Text = "Vai trò: " + role;
        }

        private void AccountPanel_Paint(object sender, PaintEventArgs e)
        {
            Panel panel = (Panel)sender;
            using (Pen pen = new Pen(Color.FromArgb(191, 219, 254), 2))
            {
                e.Graphics.DrawRectangle(pen, 0, 0, panel.Width - 1, panel.Height - 1);
            }
        }

        private void BorderBottom_Paint(object sender, PaintEventArgs e)
        {
            using (Pen pen = new Pen(Color.FromArgb(226, 232, 240)))
            {
                e.Graphics.DrawLine(pen, 0, this.pnlHeader.Height - 1, this.pnlHeader.Width, this.pnlHeader.Height - 1);
            }
        }

        private void OpenScheduleForm(object sender, EventArgs e)
        {
            using (frmQuanLyLich form = new frmQuanLyLich())
            {
                form.ShowDialog(this);
            }
        }

        private void OpenRoomForm(object sender, EventArgs e)
        {
            using (FrmQLPhong form = new FrmQLPhong())
            {
                form.ShowDialog(this);
            }
        }

        private void OpenComputerForm(object sender, EventArgs e)
        {
            using (frmQuanLyMay form = new frmQuanLyMay())
            {
                form.ShowDialog(this);
            }
        }

        private void OpenConfigForm(object sender, EventArgs e)
        {
            using (frmCauHinh form = new frmCauHinh())
            {
                form.ShowDialog(this);
            }
        }

        private void OpenClassForm(object sender, EventArgs e)
        {
            using (frmQLLopHoc form = new frmQLLopHoc())
            {
                form.ShowDialog(this);
            }
        }

        private void OpenShiftForm(object sender, EventArgs e)
        {
            using (frmQLCaHoc form = new frmQLCaHoc())
            {
                form.ShowDialog(this);
            }
        }

        private void OpenReportForm(object sender, EventArgs e)
        {
            using (frmBaoCaoThongKe form = new frmBaoCaoThongKe())
            {
                form.ShowDialog(this);
            }
        }

        private void OpenPlaceholderForm(string title)
        {
            using (Form form = new Form())
            {
                form.StartPosition = FormStartPosition.CenterParent;
                form.Size = new Size(900, 560);
                form.Text = title;
                form.BackColor = Color.FromArgb(241, 245, 249);
                form.ShowDialog(this);
            }
        }

        private void BtnLogout_Click(object sender, EventArgs e)
        {
            Session.SignOut();
            this.Hide();

            using (FrmLogin login = new FrmLogin())
            {
                if (login.ShowDialog() != DialogResult.OK)
                {
                    this.Close();
                    return;
                }
            }

            Form nextMain = null;
            if (Session.HasRole(UserRole.Admin))
            {
                nextMain = new frmMain_Admin();
            }
            else if (Session.HasRole(UserRole.QuanLyPhongMay))
            {
                nextMain = new frmMain_QLPM();
            }

            if (nextMain != null)
            {
                using (nextMain)
                {
                    nextMain.ShowDialog();
                }
            }

            this.Close();
        }
    }
}
