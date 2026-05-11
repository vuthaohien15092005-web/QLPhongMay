using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using Guna.UI2.WinForms;

namespace QLPhongMay.GUI.Forms.Users
{
    public partial class FrmAddAccount : Form
    {
        private readonly Color primaryColor = Color.FromArgb(37, 99, 235);
        private readonly Color primaryHoverColor = Color.FromArgb(29, 78, 216);
        private readonly Color textColor = Color.FromArgb(15, 23, 42);
        private readonly Color mutedTextColor = Color.FromArgb(100, 116, 139);
        private readonly Color borderColor = Color.FromArgb(226, 232, 240);
        private readonly Color inputBackColor = Color.FromArgb(248, 250, 252);
        private readonly Color dangerColor = Color.FromArgb(220, 38, 38);

        private System.ComponentModel.IContainer components;
        private Guna2BorderlessForm borderlessForm;
        private Guna2ShadowForm shadowForm;
        private Guna2Panel pnlRoot;
        private Guna2ControlBox btnClose;
        private Guna2HtmlLabel lblTitle;
        private Guna2HtmlLabel lblSubtitle;
        private Guna2HtmlLabel lblFullName;
        private Guna2TextBox txtFullName;
        private Guna2HtmlLabel lblUsername;
        private Guna2TextBox txtUsername;
        private Guna2HtmlLabel lblPassword;
        private Guna2TextBox txtPassword;
        private Guna2HtmlLabel lblEmail;
        private Guna2TextBox txtEmail;
        private Guna2HtmlLabel lblRole;
        private Guna2ComboBox cboRole;
        private Guna2HtmlLabel lblError;
        private Guna2Button btnCancel;
        private Guna2Button btnSave;
        private bool passwordVisible;

        public FrmAddAccount()
        {
            InitializeComponent();
        }

        public string FullName
        {
            get { return this.txtFullName.Text.Trim(); }
        }

        public string Username
        {
            get { return this.txtUsername.Text.Trim(); }
        }

        public string Password
        {
            get { return this.txtPassword.Text; }
        }

        public string Email
        {
            get { return this.txtEmail.Text.Trim(); }
        }

        public string RoleName
        {
            get { return Convert.ToString(this.cboRole.SelectedItem); }
        }

        public string RoleCode
        {
            get { return this.RoleName == "Admin" ? "Admin" : "QuanLyPhongMay"; }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this.components != null)
                {
                    this.components.Dispose();
                }
            }

            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.borderlessForm = new Guna2BorderlessForm(this.components);
            this.shadowForm = new Guna2ShadowForm(this.components);
            this.pnlRoot = new Guna2Panel();
            this.btnClose = new Guna2ControlBox();
            this.lblTitle = new Guna2HtmlLabel();
            this.lblSubtitle = new Guna2HtmlLabel();
            this.lblFullName = new Guna2HtmlLabel();
            this.txtFullName = new Guna2TextBox();
            this.lblUsername = new Guna2HtmlLabel();
            this.txtUsername = new Guna2TextBox();
            this.lblPassword = new Guna2HtmlLabel();
            this.txtPassword = new Guna2TextBox();
            this.lblEmail = new Guna2HtmlLabel();
            this.txtEmail = new Guna2TextBox();
            this.lblRole = new Guna2HtmlLabel();
            this.cboRole = new Guna2ComboBox();
            this.lblError = new Guna2HtmlLabel();
            this.btnCancel = new Guna2Button();
            this.btnSave = new Guna2Button();
            this.pnlRoot.SuspendLayout();
            this.SuspendLayout();
            // 
            // borderlessForm
            // 
            this.borderlessForm.BorderRadius = 18;
            this.borderlessForm.ContainerControl = this;
            this.borderlessForm.DockIndicatorTransparencyValue = 0.6D;
            this.borderlessForm.ResizeForm = false;
            this.borderlessForm.TransparentWhileDrag = true;
            // 
            // shadowForm
            // 
            this.shadowForm.BorderRadius = 18;
            this.shadowForm.TargetForm = this;
            // 
            // pnlRoot
            // 
            this.pnlRoot.BorderColor = Color.FromArgb(226, 232, 240);
            this.pnlRoot.BorderRadius = 18;
            this.pnlRoot.BorderThickness = 1;
            this.pnlRoot.Controls.Add(this.btnClose);
            this.pnlRoot.Controls.Add(this.lblTitle);
            this.pnlRoot.Controls.Add(this.lblSubtitle);
            this.pnlRoot.Controls.Add(this.lblFullName);
            this.pnlRoot.Controls.Add(this.txtFullName);
            this.pnlRoot.Controls.Add(this.lblUsername);
            this.pnlRoot.Controls.Add(this.txtUsername);
            this.pnlRoot.Controls.Add(this.lblPassword);
            this.pnlRoot.Controls.Add(this.txtPassword);
            this.pnlRoot.Controls.Add(this.lblEmail);
            this.pnlRoot.Controls.Add(this.txtEmail);
            this.pnlRoot.Controls.Add(this.lblRole);
            this.pnlRoot.Controls.Add(this.cboRole);
            this.pnlRoot.Controls.Add(this.lblError);
            this.pnlRoot.Controls.Add(this.btnCancel);
            this.pnlRoot.Controls.Add(this.btnSave);
            this.pnlRoot.Dock = DockStyle.Fill;
            this.pnlRoot.FillColor = Color.White;
            this.pnlRoot.Location = new Point(0, 0);
            this.pnlRoot.Name = "pnlRoot";
            this.pnlRoot.Padding = new Padding(28);
            this.pnlRoot.Size = new Size(520, 650);
            this.pnlRoot.TabIndex = 0;
            // 
            // btnClose
            // 
            this.btnClose.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            this.btnClose.BorderRadius = 8;
            this.btnClose.FillColor = Color.Transparent;
            this.btnClose.HoverState.FillColor = Color.FromArgb(241, 245, 249);
            this.btnClose.IconColor = Color.FromArgb(71, 85, 105);
            this.btnClose.Location = new Point(464, 24);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new Size(32, 32);
            this.btnClose.TabIndex = 13;
            // 
            // lblTitle
            // 
            this.lblTitle.BackColor = Color.Transparent;
            this.lblTitle.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.lblTitle.ForeColor = Color.FromArgb(15, 23, 42);
            this.lblTitle.Location = new Point(28, 24);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new Size(272, 43);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Thêm tài khoản mới";
            // 
            // lblSubtitle
            // 
            this.lblSubtitle.BackColor = Color.Transparent;
            this.lblSubtitle.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.lblSubtitle.ForeColor = Color.FromArgb(100, 116, 139);
            this.lblSubtitle.Location = new Point(30, 69);
            this.lblSubtitle.Name = "lblSubtitle";
            this.lblSubtitle.Size = new Size(355, 25);
            this.lblSubtitle.TabIndex = 1;
            this.lblSubtitle.Text = "Nhập thông tin để cấp quyền truy cập hệ thống";
            // 
            // lblFullName
            // 
            this.lblFullName.BackColor = Color.Transparent;
            this.lblFullName.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.lblFullName.ForeColor = Color.FromArgb(51, 65, 85);
            this.lblFullName.Location = new Point(31, 118);
            this.lblFullName.Name = "lblFullName";
            this.lblFullName.Size = new Size(72, 23);
            this.lblFullName.TabIndex = 2;
            this.lblFullName.Text = "Họ và tên";
            // 
            // txtFullName
            // 
            this.txtFullName.BorderColor = Color.FromArgb(226, 232, 240);
            this.txtFullName.BorderRadius = 8;
            this.txtFullName.Cursor = Cursors.IBeam;
            this.txtFullName.DefaultText = string.Empty;
            this.txtFullName.FillColor = Color.FromArgb(248, 250, 252);
            this.txtFullName.FocusedState.BorderColor = Color.FromArgb(37, 99, 235);
            this.txtFullName.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.txtFullName.ForeColor = Color.FromArgb(15, 23, 42);
            this.txtFullName.HoverState.BorderColor = Color.FromArgb(37, 99, 235);
            this.txtFullName.Location = new Point(30, 148);
            this.txtFullName.Margin = new Padding(3, 5, 3, 5);
            this.txtFullName.Name = "txtFullName";
            this.txtFullName.PlaceholderForeColor = Color.FromArgb(148, 163, 184);
            this.txtFullName.PlaceholderText = "Nhập họ và tên";
            this.txtFullName.SelectedText = string.Empty;
            this.txtFullName.Size = new Size(460, 46);
            this.txtFullName.TabIndex = 1;
            // 
            // lblUsername
            // 
            this.lblUsername.BackColor = Color.Transparent;
            this.lblUsername.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.lblUsername.ForeColor = Color.FromArgb(51, 65, 85);
            this.lblUsername.Location = new Point(31, 216);
            this.lblUsername.Name = "lblUsername";
            this.lblUsername.Size = new Size(115, 23);
            this.lblUsername.TabIndex = 3;
            this.lblUsername.Text = "Tên đăng nhập";
            // 
            // txtUsername
            // 
            this.txtUsername.BorderColor = Color.FromArgb(226, 232, 240);
            this.txtUsername.BorderRadius = 8;
            this.txtUsername.Cursor = Cursors.IBeam;
            this.txtUsername.DefaultText = string.Empty;
            this.txtUsername.FillColor = Color.FromArgb(248, 250, 252);
            this.txtUsername.FocusedState.BorderColor = Color.FromArgb(37, 99, 235);
            this.txtUsername.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.txtUsername.ForeColor = Color.FromArgb(15, 23, 42);
            this.txtUsername.HoverState.BorderColor = Color.FromArgb(37, 99, 235);
            this.txtUsername.Location = new Point(30, 246);
            this.txtUsername.Margin = new Padding(3, 5, 3, 5);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.PlaceholderForeColor = Color.FromArgb(148, 163, 184);
            this.txtUsername.PlaceholderText = "Nhập tên đăng nhập";
            this.txtUsername.SelectedText = string.Empty;
            this.txtUsername.Size = new Size(460, 46);
            this.txtUsername.TabIndex = 2;
            this.txtUsername.IconLeftOffset = new Point(8, 0);
            this.txtUsername.IconLeftSize = new Size(18, 18);
            // 
            // lblPassword
            // 
            this.lblPassword.BackColor = Color.Transparent;
            this.lblPassword.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.lblPassword.ForeColor = Color.FromArgb(51, 65, 85);
            this.lblPassword.Location = new Point(31, 314);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new Size(74, 23);
            this.lblPassword.TabIndex = 4;
            this.lblPassword.Text = "Mật khẩu";
            // 
            // txtPassword
            // 
            this.txtPassword.BorderColor = Color.FromArgb(226, 232, 240);
            this.txtPassword.BorderRadius = 8;
            this.txtPassword.Cursor = Cursors.IBeam;
            this.txtPassword.DefaultText = string.Empty;
            this.txtPassword.FillColor = Color.FromArgb(248, 250, 252);
            this.txtPassword.FocusedState.BorderColor = Color.FromArgb(37, 99, 235);
            this.txtPassword.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.txtPassword.ForeColor = Color.FromArgb(15, 23, 42);
            this.txtPassword.HoverState.BorderColor = Color.FromArgb(37, 99, 235);
            this.txtPassword.Location = new Point(30, 344);
            this.txtPassword.Margin = new Padding(3, 5, 3, 5);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PlaceholderForeColor = Color.FromArgb(148, 163, 184);
            this.txtPassword.PlaceholderText = "Nhập mật khẩu";
            this.txtPassword.SelectedText = string.Empty;
            this.txtPassword.Size = new Size(460, 46);
            this.txtPassword.TabIndex = 3;
            this.txtPassword.UseSystemPasswordChar = true;
            this.txtPassword.IconRightOffset = new Point(8, 0);
            this.txtPassword.IconRightSize = new Size(18, 18);
            this.txtPassword.IconRightClick += new EventHandler(this.TxtPassword_IconRightClick);
            // 
            // lblEmail
            // 
            this.lblEmail.BackColor = Color.Transparent;
            this.lblEmail.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.lblEmail.ForeColor = Color.FromArgb(51, 65, 85);
            this.lblEmail.Location = new Point(31, 412);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new Size(44, 23);
            this.lblEmail.TabIndex = 5;
            this.lblEmail.Text = "Email";
            // 
            // txtEmail
            // 
            this.txtEmail.BorderColor = Color.FromArgb(226, 232, 240);
            this.txtEmail.BorderRadius = 8;
            this.txtEmail.Cursor = Cursors.IBeam;
            this.txtEmail.DefaultText = string.Empty;
            this.txtEmail.FillColor = Color.FromArgb(248, 250, 252);
            this.txtEmail.FocusedState.BorderColor = Color.FromArgb(37, 99, 235);
            this.txtEmail.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.txtEmail.ForeColor = Color.FromArgb(15, 23, 42);
            this.txtEmail.HoverState.BorderColor = Color.FromArgb(37, 99, 235);
            this.txtEmail.Location = new Point(30, 442);
            this.txtEmail.Margin = new Padding(3, 5, 3, 5);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.PlaceholderForeColor = Color.FromArgb(148, 163, 184);
            this.txtEmail.PlaceholderText = "Nhập email";
            this.txtEmail.SelectedText = string.Empty;
            this.txtEmail.Size = new Size(460, 46);
            this.txtEmail.TabIndex = 4;
            this.txtEmail.IconLeftOffset = new Point(8, 0);
            this.txtEmail.IconLeftSize = new Size(18, 18);
            // 
            // lblRole
            // 
            this.lblRole.BackColor = Color.Transparent;
            this.lblRole.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.lblRole.ForeColor = Color.FromArgb(51, 65, 85);
            this.lblRole.Location = new Point(31, 510);
            this.lblRole.Name = "lblRole";
            this.lblRole.Size = new Size(55, 23);
            this.lblRole.TabIndex = 6;
            this.lblRole.Text = "Vai trò";
            // 
            // cboRole
            // 
            this.cboRole.BackColor = Color.Transparent;
            this.cboRole.BorderColor = Color.FromArgb(226, 232, 240);
            this.cboRole.BorderRadius = 8;
            this.cboRole.Cursor = Cursors.Hand;
            this.cboRole.DrawMode = DrawMode.OwnerDrawFixed;
            this.cboRole.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cboRole.FillColor = Color.FromArgb(248, 250, 252);
            this.cboRole.FocusedColor = Color.FromArgb(37, 99, 235);
            this.cboRole.FocusedState.BorderColor = Color.FromArgb(37, 99, 235);
            this.cboRole.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.cboRole.ForeColor = Color.FromArgb(51, 65, 85);
            this.cboRole.HoverState.BorderColor = Color.FromArgb(37, 99, 235);
            this.cboRole.ItemHeight = 40;
            this.cboRole.Items.AddRange(new object[] {
            "Admin",
            "Quản lý phòng máy"});
            this.cboRole.Location = new Point(30, 540);
            this.cboRole.Name = "cboRole";
            this.cboRole.Size = new Size(460, 46);
            this.cboRole.StartIndex = 0;
            this.cboRole.TabIndex = 5;
            // 
            // lblError
            // 
            this.lblError.BackColor = Color.Transparent;
            this.lblError.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.lblError.ForeColor = Color.FromArgb(220, 38, 38);
            this.lblError.Location = new Point(31, 596);
            this.lblError.Name = "lblError";
            this.lblError.Size = new Size(3, 2);
            this.lblError.TabIndex = 11;
            this.lblError.Text = string.Empty;
            // 
            // btnCancel
            // 
            this.btnCancel.Animated = true;
            this.btnCancel.BorderColor = Color.FromArgb(226, 232, 240);
            this.btnCancel.BorderRadius = 8;
            this.btnCancel.BorderThickness = 1;
            this.btnCancel.Cursor = Cursors.Hand;
            this.btnCancel.DialogResult = DialogResult.Cancel;
            this.btnCancel.FillColor = Color.White;
            this.btnCancel.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.btnCancel.ForeColor = Color.FromArgb(51, 65, 85);
            this.btnCancel.HoverState.FillColor = Color.FromArgb(248, 250, 252);
            this.btnCancel.Location = new Point(250, 598);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(112, 42);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "Hủy";
            // 
            // btnSave
            // 
            this.btnSave.Animated = true;
            this.btnSave.BorderRadius = 8;
            this.btnSave.Cursor = Cursors.Hand;
            this.btnSave.FillColor = Color.FromArgb(37, 99, 235);
            this.btnSave.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.btnSave.ForeColor = Color.White;
            this.btnSave.HoverState.FillColor = Color.FromArgb(29, 78, 216);
            this.btnSave.Location = new Point(374, 598);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new Size(116, 42);
            this.btnSave.TabIndex = 6;
            this.btnSave.Text = "Thêm";
            this.btnSave.Click += new EventHandler(this.BtnSave_Click);
            // 
            // FrmAddAccount
            // 
            this.AcceptButton = this.btnSave;
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = Color.White;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new Size(520, 650);
            this.Controls.Add(this.pnlRoot);
            this.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.FormBorderStyle = FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmAddAccount";
            this.ShowInTaskbar = false;
            this.StartPosition = FormStartPosition.CenterParent;
            this.Text = "Thêm tài khoản mới";
            this.pnlRoot.ResumeLayout(false);
            this.pnlRoot.PerformLayout();
            this.ResumeLayout(false);
        }

        private void ApplyRuntimeIcons()
        {
            this.txtUsername.IconLeft = CreateUserIcon(this.mutedTextColor);
            this.txtPassword.IconRight = CreateEyeIcon(this.mutedTextColor, false);
            this.txtEmail.IconLeft = CreateMailIcon(this.mutedTextColor);
        }
        private void ConfigureLabel(Guna2HtmlLabel label, string text, int x, int y)
        {
            label.BackColor = Color.Transparent;
            label.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label.ForeColor = Color.FromArgb(51, 65, 85);
            label.Location = new Point(x, y);
            label.Text = text;
        }

        private void ConfigureTextBox(Guna2TextBox textBox, string placeholder, int x, int y, int tabIndex)
        {
            textBox.BorderColor = this.borderColor;
            textBox.BorderRadius = 8;
            textBox.Cursor = Cursors.IBeam;
            textBox.DefaultText = string.Empty;
            textBox.FillColor = this.inputBackColor;
            textBox.FocusedState.BorderColor = this.primaryColor;
            textBox.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            textBox.ForeColor = this.textColor;
            textBox.HoverState.BorderColor = this.primaryColor;
            textBox.Location = new Point(x, y);
            textBox.Margin = new Padding(3, 5, 3, 5);
            textBox.Name = textBox == this.txtFullName ? "txtFullName" : textBox == this.txtUsername ? "txtUsername" : textBox == this.txtPassword ? "txtPassword" : "txtEmail";
            textBox.PlaceholderForeColor = Color.FromArgb(148, 163, 184);
            textBox.PlaceholderText = placeholder;
            textBox.SelectedText = string.Empty;
            textBox.Size = new Size(460, 46);
            textBox.TabIndex = tabIndex;
        }

        private void TxtPassword_IconRightClick(object sender, EventArgs e)
        {
            this.passwordVisible = !this.passwordVisible;
            this.txtPassword.UseSystemPasswordChar = !this.passwordVisible;
            this.txtPassword.IconRight = CreateEyeIcon(this.passwordVisible ? this.primaryColor : this.mutedTextColor, this.passwordVisible);
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateInput())
            {
                return;
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(this.FullName))
            {
                return ShowValidationError("Vui lòng nhập họ và tên.", this.txtFullName);
            }

            if (string.IsNullOrWhiteSpace(this.Username))
            {
                return ShowValidationError("Vui lòng nhập tên đăng nhập.", this.txtUsername);
            }

            if (string.IsNullOrWhiteSpace(this.Password))
            {
                return ShowValidationError("Vui lòng nhập mật khẩu.", this.txtPassword);
            }

            if (string.IsNullOrWhiteSpace(this.Email))
            {
                return ShowValidationError("Vui lòng nhập email.", this.txtEmail);
            }

            if (!this.Email.Contains("@"))
            {
                return ShowValidationError("Email chưa đúng định dạng.", this.txtEmail);
            }

            this.lblError.Text = string.Empty;
            return true;
        }

        private bool ShowValidationError(string message, Control target)
        {
            this.lblError.Text = message;
            target.Focus();
            return false;
        }

        private static Image CreateUserIcon(Color color)
        {
            Bitmap bitmap = CreateIconBitmap();
            using (Graphics graphics = Graphics.FromImage(bitmap))
            using (Pen pen = new Pen(color, 2F))
            {
                graphics.SmoothingMode = SmoothingMode.AntiAlias;
                graphics.DrawEllipse(pen, 7, 4, 10, 10);
                graphics.DrawArc(pen, 4, 14, 16, 10, 200, 140);
            }

            return bitmap;
        }

        private static Image CreateMailIcon(Color color)
        {
            Bitmap bitmap = CreateIconBitmap();
            using (Graphics graphics = Graphics.FromImage(bitmap))
            using (Pen pen = new Pen(color, 2F))
            {
                graphics.SmoothingMode = SmoothingMode.AntiAlias;
                graphics.DrawRectangle(pen, 4, 6, 16, 12);
                graphics.DrawLine(pen, 5, 7, 12, 13);
                graphics.DrawLine(pen, 19, 7, 12, 13);
            }

            return bitmap;
        }

        private static Image CreateEyeIcon(Color color, bool active)
        {
            Bitmap bitmap = CreateIconBitmap();
            using (Graphics graphics = Graphics.FromImage(bitmap))
            using (Pen pen = new Pen(color, 2F))
            using (SolidBrush brush = new SolidBrush(color))
            {
                graphics.SmoothingMode = SmoothingMode.AntiAlias;
                graphics.DrawArc(pen, 3, 7, 18, 10, 180, 180);
                graphics.DrawArc(pen, 3, 7, 18, 10, 0, 180);
                graphics.FillEllipse(brush, 10, 10, 4, 4);

                if (!active)
                {
                    graphics.DrawLine(pen, 5, 20, 20, 4);
                }
            }

            return bitmap;
        }

        private static Bitmap CreateIconBitmap()
        {
            return new Bitmap(24, 24, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
        }
    }
}


