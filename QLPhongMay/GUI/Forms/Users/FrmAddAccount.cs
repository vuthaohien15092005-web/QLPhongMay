using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using Guna.UI2.WinForms;
using QLPhongMay.BLL;
using QLPhongMay.DTO;

namespace QLPhongMay.GUI.Forms.Users
{
    public partial class FrmAddAccount : Form
    {
        public enum AccountDialogMode
        {
            Create,
            Edit,
            View
        }

        public enum AccountField
        {
            FullName,
            Username,
            Password,
            NewPassword,
            Email
        }

        public class AccountValidationError
        {
            public AccountValidationError(string message, AccountField field)
            {
                this.Message = message;
                this.Field = field;
            }

            public string Message { get; private set; }

            public AccountField Field { get; private set; }
        }

        private readonly Color primaryColor = Color.FromArgb(37, 99, 235);
        private readonly Color mutedTextColor = Color.FromArgb(100, 116, 139);
        private readonly Color borderColor = Color.FromArgb(226, 232, 240);
        private readonly Color inputBackColor = Color.FromArgb(248, 250, 252);
        private readonly Color dangerColor = Color.FromArgb(220, 38, 38);
        private readonly AccountDialogMode mode;
        private readonly AccountListItem account;

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
        private Guna2HtmlLabel lblNewPassword;
        private Guna2TextBox txtNewPassword;
        private Guna2HtmlLabel lblEmail;
        private Guna2TextBox txtEmail;
        private Guna2HtmlLabel lblRole;
        private Guna2ComboBox cboRole;
        private Guna2HtmlLabel lblError;
        private Guna2Button btnCancel;
        private Guna2Button btnSave;
        private bool passwordVisible;

        public Func<FrmAddAccount, AccountValidationError> ExternalValidator { get; set; }

        public FrmAddAccount()
            : this(AccountDialogMode.Create, null)
        {
        }

        public FrmAddAccount(AccountDialogMode mode, AccountListItem account)
        {
            this.mode = mode;
            this.account = account;
            InitializeComponent();
            ApplyMode();
            ApplyPasswordVisibilityForRole();
            UpdatePasswordIcon();
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

        public string OldPassword
        {
            get { return this.txtPassword.Text; }
        }

        public string NewPassword
        {
            get { return this.txtNewPassword.Text; }
        }

        public string Email
        {
            get { return this.txtEmail.Text.Trim(); }
        }

        public string RoleName
        {
            get { return Convert.ToString(this.cboRole.SelectedItem); }
        }

        public int RoleId
        {
            get { return this.RoleName == "Admin" ? 1 : 2; }
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
            Color primaryColor = Color.FromArgb(37, 99, 235);
            Color mutedTextColor = Color.FromArgb(100, 116, 139);
            Color borderColor = Color.FromArgb(226, 232, 240);
            Color inputBackColor = Color.FromArgb(248, 250, 252);
            Color dangerColor = Color.FromArgb(220, 38, 38);

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
            this.lblNewPassword = new Guna2HtmlLabel();
            this.txtNewPassword = new Guna2TextBox();
            this.lblEmail = new Guna2HtmlLabel();
            this.txtEmail = new Guna2TextBox();
            this.lblRole = new Guna2HtmlLabel();
            this.cboRole = new Guna2ComboBox();
            this.lblError = new Guna2HtmlLabel();
            this.btnCancel = new Guna2Button();
            this.btnSave = new Guna2Button();
            this.pnlRoot.SuspendLayout();
            this.SuspendLayout();

            this.borderlessForm.BorderRadius = 18;
            this.borderlessForm.ContainerControl = this;
            this.borderlessForm.DockIndicatorTransparencyValue = 0.6D;
            this.borderlessForm.ResizeForm = false;
            this.borderlessForm.TransparentWhileDrag = true;

            this.shadowForm.BorderRadius = 18;
            this.shadowForm.TargetForm = this;

            this.pnlRoot.BorderColor = borderColor;
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
            this.pnlRoot.Controls.Add(this.lblNewPassword);
            this.pnlRoot.Controls.Add(this.txtNewPassword);
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

            this.btnClose.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            this.btnClose.BorderRadius = 8;
            this.btnClose.FillColor = Color.Transparent;
            this.btnClose.HoverState.FillColor = Color.FromArgb(241, 245, 249);
            this.btnClose.IconColor = Color.FromArgb(71, 85, 105);
            this.btnClose.Location = new Point(464, 24);
            this.btnClose.Size = new Size(32, 32);

            this.lblTitle.BackColor = Color.Transparent;
            this.lblTitle.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.lblTitle.ForeColor = Color.FromArgb(51, 65, 85);
            this.lblTitle.Location = new Point(28, 24);
            this.lblTitle.Text = "Thêm tài khoản mới";

            this.lblSubtitle.BackColor = Color.Transparent;
            this.lblSubtitle.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.lblSubtitle.ForeColor = mutedTextColor;
            this.lblSubtitle.Location = new Point(30, 69);
            this.lblSubtitle.Text = "Nhập thông tin để cấp quyền truy cập hệ thống";

            this.lblFullName.BackColor = Color.Transparent;
            this.lblFullName.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.lblFullName.ForeColor = Color.FromArgb(51, 65, 85);
            this.lblFullName.Location = new Point(31, 118);
            this.lblFullName.Text = "Họ và tên";
            this.txtFullName.BorderColor = borderColor;
            this.txtFullName.BorderRadius = 8;
            this.txtFullName.Cursor = Cursors.IBeam;
            this.txtFullName.DefaultText = string.Empty;
            this.txtFullName.FillColor = inputBackColor;
            this.txtFullName.FocusedState.BorderColor = primaryColor;
            this.txtFullName.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.txtFullName.ForeColor = Color.FromArgb(15, 23, 42);
            this.txtFullName.HoverState.BorderColor = primaryColor;
            this.txtFullName.Location = new Point(30, 148);
            this.txtFullName.Margin = new Padding(3, 5, 3, 5);
            this.txtFullName.PlaceholderText = "Nhập họ và tên";
            this.txtFullName.Size = new Size(460, 46);
            this.txtFullName.TabIndex = 1;

            this.lblUsername.BackColor = Color.Transparent;
            this.lblUsername.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.lblUsername.ForeColor = Color.FromArgb(51, 65, 85);
            this.lblUsername.Location = new Point(31, 216);
            this.lblUsername.Text = "Tên đăng nhập";
            this.txtUsername.BorderColor = borderColor;
            this.txtUsername.BorderRadius = 8;
            this.txtUsername.Cursor = Cursors.IBeam;
            this.txtUsername.DefaultText = string.Empty;
            this.txtUsername.FillColor = inputBackColor;
            this.txtUsername.FocusedState.BorderColor = primaryColor;
            this.txtUsername.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.txtUsername.ForeColor = Color.FromArgb(15, 23, 42);
            this.txtUsername.HoverState.BorderColor = primaryColor;
            this.txtUsername.Location = new Point(30, 246);
            this.txtUsername.Margin = new Padding(3, 5, 3, 5);
            this.txtUsername.PlaceholderText = "Nhập tên đăng nhập";
            this.txtUsername.Size = new Size(460, 46);
            this.txtUsername.TabIndex = 2;

            this.lblPassword.BackColor = Color.Transparent;
            this.lblPassword.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.lblPassword.ForeColor = Color.FromArgb(51, 65, 85);
            this.lblPassword.Location = new Point(31, 314);
            this.lblPassword.Text = "Mật khẩu";
            this.txtPassword.BorderColor = borderColor;
            this.txtPassword.BorderRadius = 8;
            this.txtPassword.Cursor = Cursors.IBeam;
            this.txtPassword.DefaultText = string.Empty;
            this.txtPassword.FillColor = inputBackColor;
            this.txtPassword.FocusedState.BorderColor = primaryColor;
            this.txtPassword.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.txtPassword.ForeColor = Color.FromArgb(15, 23, 42);
            this.txtPassword.HoverState.BorderColor = primaryColor;
            this.txtPassword.Location = new Point(30, 344);
            this.txtPassword.Margin = new Padding(3, 5, 3, 5);
            this.txtPassword.PlaceholderText = "Nhập mật khẩu";
            this.txtPassword.Size = new Size(460, 46);
            this.txtPassword.TabIndex = 3;
            this.txtPassword.UseSystemPasswordChar = true;
            this.txtPassword.IconRightOffset = new Point(8, 0);
            this.txtPassword.IconRightSize = new Size(18, 18);
            this.txtPassword.IconRightClick += new EventHandler(this.TxtPassword_IconRightClick);

            this.lblNewPassword.BackColor = Color.Transparent;
            this.lblNewPassword.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.lblNewPassword.ForeColor = Color.FromArgb(51, 65, 85);
            this.lblNewPassword.Location = new Point(31, 412);
            this.lblNewPassword.Text = "Mật khẩu mới";
            this.txtNewPassword.BorderColor = borderColor;
            this.txtNewPassword.BorderRadius = 8;
            this.txtNewPassword.Cursor = Cursors.IBeam;
            this.txtNewPassword.DefaultText = string.Empty;
            this.txtNewPassword.FillColor = inputBackColor;
            this.txtNewPassword.FocusedState.BorderColor = primaryColor;
            this.txtNewPassword.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.txtNewPassword.ForeColor = Color.FromArgb(15, 23, 42);
            this.txtNewPassword.HoverState.BorderColor = primaryColor;
            this.txtNewPassword.Location = new Point(30, 442);
            this.txtNewPassword.Margin = new Padding(3, 5, 3, 5);
            this.txtNewPassword.PlaceholderText = "Để trống nếu không đổi mật khẩu";
            this.txtNewPassword.Size = new Size(460, 46);
            this.txtNewPassword.TabIndex = 4;
            this.lblNewPassword.Visible = false;
            this.txtNewPassword.Visible = false;
            this.txtNewPassword.UseSystemPasswordChar = true;

            this.lblEmail.BackColor = Color.Transparent;
            this.lblEmail.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.lblEmail.ForeColor = Color.FromArgb(51, 65, 85);
            this.lblEmail.Location = new Point(31, 412);
            this.lblEmail.Text = "Email";
            this.txtEmail.BorderColor = borderColor;
            this.txtEmail.BorderRadius = 8;
            this.txtEmail.Cursor = Cursors.IBeam;
            this.txtEmail.DefaultText = string.Empty;
            this.txtEmail.FillColor = inputBackColor;
            this.txtEmail.FocusedState.BorderColor = primaryColor;
            this.txtEmail.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.txtEmail.ForeColor = Color.FromArgb(15, 23, 42);
            this.txtEmail.HoverState.BorderColor = primaryColor;
            this.txtEmail.Location = new Point(30, 442);
            this.txtEmail.Margin = new Padding(3, 5, 3, 5);
            this.txtEmail.PlaceholderText = "Nhập email";
            this.txtEmail.Size = new Size(460, 46);
            this.txtEmail.TabIndex = 4;

            this.lblRole.BackColor = Color.Transparent;
            this.lblRole.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.lblRole.ForeColor = Color.FromArgb(51, 65, 85);
            this.lblRole.Location = new Point(31, 510);
            this.lblRole.Text = "Vai trò";

            this.cboRole.BackColor = Color.Transparent;
            this.cboRole.BorderColor = borderColor;
            this.cboRole.BorderRadius = 8;
            this.cboRole.Cursor = Cursors.Hand;
            this.cboRole.DrawMode = DrawMode.OwnerDrawFixed;
            this.cboRole.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cboRole.FillColor = inputBackColor;
            this.cboRole.FocusedColor = primaryColor;
            this.cboRole.FocusedState.BorderColor = primaryColor;
            this.cboRole.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.cboRole.ForeColor = Color.FromArgb(51, 65, 85);
            this.cboRole.HoverState.BorderColor = primaryColor;
            this.cboRole.ItemHeight = 40;
            this.cboRole.Items.AddRange(new object[] { "Admin", "Quản lý phòng máy" });
            this.cboRole.Location = new Point(30, 540);
            this.cboRole.Name = "cboRole";
            this.cboRole.Size = new Size(460, 46);
            this.cboRole.StartIndex = 0;
            this.cboRole.TabIndex = 5;
            this.cboRole.SelectedIndexChanged += new EventHandler(this.CboRole_SelectedIndexChanged);

            this.lblError.BackColor = Color.Transparent;
            this.lblError.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.lblError.ForeColor = dangerColor;
            this.lblError.Location = new Point(31, 596);
            this.lblError.Text = string.Empty;

            this.btnCancel.Animated = true;
            this.btnCancel.BorderColor = borderColor;
            this.btnCancel.BorderRadius = 8;
            this.btnCancel.BorderThickness = 1;
            this.btnCancel.Cursor = Cursors.Hand;
            this.btnCancel.DialogResult = DialogResult.Cancel;
            this.btnCancel.FillColor = Color.White;
            this.btnCancel.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.btnCancel.ForeColor = Color.FromArgb(51, 65, 85);
            this.btnCancel.HoverState.FillColor = Color.FromArgb(248, 250, 252);
            this.btnCancel.Location = new Point(250, 598);
            this.btnCancel.Size = new Size(112, 42);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "Hủy";
            this.btnCancel.Click += new EventHandler(this.BtnCancel_Click);

            this.btnSave.Animated = true;
            this.btnSave.BorderRadius = 8;
            this.btnSave.Cursor = Cursors.Hand;
            this.btnSave.FillColor = primaryColor;
            this.btnSave.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.btnSave.ForeColor = Color.White;
            this.btnSave.HoverState.FillColor = Color.FromArgb(29, 78, 216);
            this.btnSave.Location = new Point(374, 598);
            this.btnSave.Size = new Size(116, 42);
            this.btnSave.TabIndex = 6;
            this.btnSave.Text = "Thêm";
            this.btnSave.Click += new EventHandler(this.BtnSave_Click);

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

        private void ApplyMode()
        {
            if (this.account != null)
            {
                this.txtFullName.Text = this.account.HoTen ?? string.Empty;
                this.txtUsername.Text = this.account.TenDangNhap ?? string.Empty;
                this.txtEmail.Text = this.account.Email ?? string.Empty;
                this.cboRole.SelectedItem = this.account.MaVaiTro == 1 ? "Admin" : "Quản lý phòng máy";
            }

            if (this.mode == AccountDialogMode.Edit)
            {
                this.lblTitle.Text = "Sửa tài khoản";
                this.lblSubtitle.Text = "Cập nhật thông tin và quyền truy cập";
                this.lblFullName.Location = new Point(31, 108);
                this.txtFullName.Location = new Point(30, 132);
                this.lblUsername.Location = new Point(31, 188);
                this.txtUsername.Location = new Point(30, 212);
                this.lblPassword.Location = new Point(31, 268);
                this.txtPassword.Location = new Point(30, 292);
                this.lblPassword.Text = "Mật khẩu cũ";
                this.txtPassword.PlaceholderText = "Nhập mật khẩu hiện tại";
                this.lblNewPassword.Visible = true;
                this.txtNewPassword.Visible = true;
                this.lblNewPassword.Location = new Point(31, 348);
                this.txtNewPassword.Location = new Point(30, 372);
                this.lblEmail.Location = new Point(31, 428);
                this.txtEmail.Location = new Point(30, 452);
                this.lblRole.Location = new Point(31, 508);
                this.cboRole.Location = new Point(30, 532);
                this.lblError.Location = new Point(31, 596);
                this.btnCancel.Location = new Point(250, 598);
                this.btnSave.Location = new Point(374, 598);
                this.ClientSize = new Size(520, 650);
                this.txtUsername.ReadOnly = true;
                this.txtUsername.FillColor = Color.FromArgb(241, 245, 249);
                this.btnSave.Text = "Lưu";
                this.Text = "Sửa tài khoản";
            }
            else if (this.mode == AccountDialogMode.View)
            {
                this.lblTitle.Text = "Chi tiết tài khoản";
                this.lblSubtitle.Text = "Thông tin tài khoản trong hệ thống";
                this.lblNewPassword.Visible = false;
                this.txtNewPassword.Visible = false;
                this.lblPassword.Text = "Mật khẩu";
                this.txtPassword.Text = this.account == null ? string.Empty : this.account.MatKhau ?? string.Empty;
                this.txtPassword.UseSystemPasswordChar = false;
                this.txtPassword.IconRight = null;
                this.lblEmail.Location = new Point(31, 412);
                this.txtEmail.Location = new Point(30, 442);
                this.lblRole.Location = new Point(31, 510);
                this.cboRole.Location = new Point(30, 540);
                this.lblError.Visible = false;
                this.btnCancel.Location = new Point(374, 598);
                this.ClientSize = new Size(520, 650);
                SetReadOnly(this.txtFullName);
                SetReadOnly(this.txtUsername);
                SetReadOnly(this.txtPassword);
                SetReadOnly(this.txtEmail);
                this.cboRole.Enabled = false;
                this.btnSave.Visible = false;
                this.btnCancel.Text = "Đóng";
                this.AcceptButton = this.btnCancel;
                this.Text = "Chi tiết tài khoản";
            }
        }

        private void ConfigureLabel(Guna2HtmlLabel label, string text, int x, int y, float size, bool bold)
        {
            label.BackColor = Color.Transparent;
            label.Font = new Font("Segoe UI", size, bold ? FontStyle.Bold : FontStyle.Regular, GraphicsUnit.Point, 0);
            label.ForeColor = bold ? Color.FromArgb(51, 65, 85) : this.mutedTextColor;
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
            textBox.ForeColor = Color.FromArgb(15, 23, 42);
            textBox.HoverState.BorderColor = this.primaryColor;
            textBox.Location = new Point(x, y);
            textBox.Margin = new Padding(3, 5, 3, 5);
            textBox.PlaceholderForeColor = Color.FromArgb(148, 163, 184);
            textBox.PlaceholderText = placeholder;
            textBox.SelectedText = string.Empty;
            textBox.Size = new Size(460, 46);
            textBox.TabIndex = tabIndex;
        }

        private static void SetReadOnly(Guna2TextBox textBox)
        {
            textBox.ReadOnly = true;
            textBox.FillColor = Color.FromArgb(241, 245, 249);
            textBox.Cursor = Cursors.Default;
        }

        private void TxtPassword_IconRightClick(object sender, EventArgs e)
        {
            this.passwordVisible = !this.passwordVisible;
            this.txtPassword.UseSystemPasswordChar = !this.passwordVisible;
            UpdatePasswordIcon();
        }

        private void CboRole_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.DesignMode || (this.Site != null && this.Site.DesignMode))
            {
                return;
            }

            ApplyPasswordVisibilityForRole();
            UpdatePasswordIcon();
        }

        private void ApplyPasswordVisibilityForRole()
        {
            if (this.mode != AccountDialogMode.Create)
            {
                return;
            }

            this.passwordVisible = this.RoleId == 1;
            this.txtPassword.UseSystemPasswordChar = !this.passwordVisible;
        }

        private void UpdatePasswordIcon()
        {
            if (this.mode == AccountDialogMode.View || this.txtPassword == null)
            {
                return;
            }

            this.txtPassword.IconRight = CreateEyeIcon(this.passwordVisible ? this.primaryColor : this.mutedTextColor, this.passwordVisible);
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateInput())
            {
                return;
            }

            if (this.ExternalValidator != null)
            {
                AccountValidationError validationError = this.ExternalValidator(this);
                if (validationError != null)
                {
                    ShowValidationError(validationError.Message, GetFieldControl(validationError.Field));
                    return;
                }
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
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

            if (this.mode == AccountDialogMode.Create && string.IsNullOrWhiteSpace(this.Password))
            {
                return ShowValidationError("Vui lòng nhập mật khẩu.", this.txtPassword);
            }

            if (this.mode == AccountDialogMode.Edit)
            {
                if (string.IsNullOrWhiteSpace(this.OldPassword))
                {
                    return ShowValidationError("Vui lòng nhập mật khẩu cũ.", this.txtPassword);
                }

                if (this.account == null || !PasswordHasher.Verify(this.OldPassword, this.account.MatKhau))
                {
                    return ShowValidationError("Mật khẩu cũ không đúng.", this.txtPassword);
                }

                if (!string.IsNullOrWhiteSpace(this.NewPassword) && PasswordHasher.Verify(this.NewPassword, this.account.MatKhau))
                {
                    return ShowValidationError("Mật khẩu mới phải khác mật khẩu cũ.", this.txtNewPassword);
                }
            }

            if (string.IsNullOrWhiteSpace(this.Email))
            {
                return ShowValidationError("Vui lòng nhập email.", this.txtEmail);
            }

            if (!this.Email.Contains("@") || !this.Email.Contains("."))
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

        private Control GetFieldControl(AccountField field)
        {
            switch (field)
            {
                case AccountField.FullName:
                    return this.txtFullName;
                case AccountField.Username:
                    return this.txtUsername;
                case AccountField.Password:
                    return this.txtPassword;
                case AccountField.NewPassword:
                    return this.txtNewPassword;
                case AccountField.Email:
                    return this.txtEmail;
                default:
                    return this.txtUsername;
            }
        }

        private static Image CreateEyeIcon(Color color, bool active)
        {
            Bitmap bitmap = new Bitmap(24, 24, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
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
    }
}
