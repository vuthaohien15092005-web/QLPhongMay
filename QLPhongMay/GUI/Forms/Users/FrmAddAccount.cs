using System;
using System.Drawing;
using System.Windows.Forms;
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

        private readonly AccountDialogMode mode;
        private readonly AccountListItem account;
        private Label lblTitle;
        private Label lblSubtitle;
        private Label lblFullName;
        private TextBox txtFullName;
        private Label lblUsername;
        private TextBox txtUsername;
        private Label lblPassword;
        private TextBox txtPassword;
        private Label lblNewPassword;
        private TextBox txtNewPassword;
        private Label lblEmail;
        private TextBox txtEmail;
        private Label lblRole;
        private ComboBox cboRole;
        private CheckBox chkShowPassword;
        private Label lblError;
        private Button btnSave;
        private Button btnCancel;

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

        private void InitializeComponent()
        {
            this.lblTitle = new Label();
            this.lblSubtitle = new Label();
            this.lblFullName = new Label();
            this.txtFullName = new TextBox();
            this.lblUsername = new Label();
            this.txtUsername = new TextBox();
            this.lblPassword = new Label();
            this.txtPassword = new TextBox();
            this.lblNewPassword = new Label();
            this.txtNewPassword = new TextBox();
            this.lblEmail = new Label();
            this.txtEmail = new TextBox();
            this.lblRole = new Label();
            this.cboRole = new ComboBox();
            this.chkShowPassword = new CheckBox();
            this.lblError = new Label();
            this.btnSave = new Button();
            this.btnCancel = new Button();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.lblTitle.ForeColor = Color.FromArgb(15, 23, 42);
            this.lblTitle.Location = new Point(28, 24);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new Size(269, 41);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Thêm tài khoản mới";
            // 
            // lblSubtitle
            // 
            this.lblSubtitle.AutoSize = true;
            this.lblSubtitle.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.lblSubtitle.ForeColor = Color.FromArgb(100, 116, 139);
            this.lblSubtitle.Location = new Point(32, 68);
            this.lblSubtitle.Name = "lblSubtitle";
            this.lblSubtitle.Size = new Size(374, 23);
            this.lblSubtitle.TabIndex = 1;
            this.lblSubtitle.Text = "Nhập thông tin để cấp quyền truy cập hệ thống";
            // 
            // lblFullName
            // 
            this.lblFullName.AutoSize = true;
            this.lblFullName.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.lblFullName.Location = new Point(32, 116);
            this.lblFullName.Name = "lblFullName";
            this.lblFullName.Size = new Size(85, 21);
            this.lblFullName.TabIndex = 2;
            this.lblFullName.Text = "Họ và tên";
            // 
            // txtFullName
            // 
            this.txtFullName.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.txtFullName.Location = new Point(35, 142);
            this.txtFullName.Name = "txtFullName";
            this.txtFullName.Size = new Size(450, 30);
            this.txtFullName.TabIndex = 3;
            // 
            // lblUsername
            // 
            this.lblUsername.AutoSize = true;
            this.lblUsername.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.lblUsername.Location = new Point(32, 188);
            this.lblUsername.Name = "lblUsername";
            this.lblUsername.Size = new Size(122, 21);
            this.lblUsername.TabIndex = 4;
            this.lblUsername.Text = "Tên đăng nhập";
            // 
            // txtUsername
            // 
            this.txtUsername.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.txtUsername.Location = new Point(35, 214);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new Size(450, 30);
            this.txtUsername.TabIndex = 5;
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.lblPassword.Location = new Point(32, 260);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new Size(79, 21);
            this.lblPassword.TabIndex = 6;
            this.lblPassword.Text = "Mật khẩu";
            // 
            // txtPassword
            // 
            this.txtPassword.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.txtPassword.Location = new Point(35, 286);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new Size(450, 30);
            this.txtPassword.TabIndex = 7;
            this.txtPassword.UseSystemPasswordChar = true;
            // 
            // lblNewPassword
            // 
            this.lblNewPassword.AutoSize = true;
            this.lblNewPassword.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.lblNewPassword.Location = new Point(32, 332);
            this.lblNewPassword.Name = "lblNewPassword";
            this.lblNewPassword.Size = new Size(113, 21);
            this.lblNewPassword.TabIndex = 8;
            this.lblNewPassword.Text = "Mật khẩu mới";
            this.lblNewPassword.Visible = false;
            // 
            // txtNewPassword
            // 
            this.txtNewPassword.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.txtNewPassword.Location = new Point(35, 358);
            this.txtNewPassword.Name = "txtNewPassword";
            this.txtNewPassword.Size = new Size(450, 30);
            this.txtNewPassword.TabIndex = 9;
            this.txtNewPassword.UseSystemPasswordChar = true;
            this.txtNewPassword.Visible = false;
            // 
            // lblEmail
            // 
            this.lblEmail.AutoSize = true;
            this.lblEmail.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.lblEmail.Location = new Point(32, 332);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new Size(50, 21);
            this.lblEmail.TabIndex = 10;
            this.lblEmail.Text = "Email";
            // 
            // txtEmail
            // 
            this.txtEmail.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.txtEmail.Location = new Point(35, 358);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new Size(450, 30);
            this.txtEmail.TabIndex = 11;
            // 
            // lblRole
            // 
            this.lblRole.AutoSize = true;
            this.lblRole.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.lblRole.Location = new Point(32, 404);
            this.lblRole.Name = "lblRole";
            this.lblRole.Size = new Size(55, 21);
            this.lblRole.TabIndex = 12;
            this.lblRole.Text = "Vai trò";
            // 
            // cboRole
            // 
            this.cboRole.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cboRole.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.cboRole.FormattingEnabled = true;
            this.cboRole.Items.AddRange(new object[] {
            "Admin",
            "Quản lý phòng máy"});
            this.cboRole.Location = new Point(35, 430);
            this.cboRole.Name = "cboRole";
            this.cboRole.Size = new Size(450, 31);
            this.cboRole.TabIndex = 13;
            // 
            // chkShowPassword
            // 
            this.chkShowPassword.AutoSize = true;
            this.chkShowPassword.Location = new Point(35, 477);
            this.chkShowPassword.Name = "chkShowPassword";
            this.chkShowPassword.Size = new Size(130, 24);
            this.chkShowPassword.TabIndex = 14;
            this.chkShowPassword.Text = "Hiện mật khẩu";
            this.chkShowPassword.UseVisualStyleBackColor = true;
            this.chkShowPassword.CheckedChanged += new EventHandler(this.ChkShowPassword_CheckedChanged);
            // 
            // lblError
            // 
            this.lblError.AutoSize = true;
            this.lblError.ForeColor = Color.FromArgb(220, 38, 38);
            this.lblError.Location = new Point(32, 512);
            this.lblError.MaximumSize = new Size(450, 0);
            this.lblError.Name = "lblError";
            this.lblError.Size = new Size(0, 20);
            this.lblError.TabIndex = 15;
            // 
            // btnSave
            // 
            this.btnSave.BackColor = Color.FromArgb(37, 99, 235);
            this.btnSave.FlatStyle = FlatStyle.Flat;
            this.btnSave.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.btnSave.ForeColor = Color.White;
            this.btnSave.Location = new Point(355, 548);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new Size(130, 42);
            this.btnSave.TabIndex = 16;
            this.btnSave.Text = "Thêm";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new EventHandler(this.BtnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = DialogResult.Cancel;
            this.btnCancel.FlatStyle = FlatStyle.Flat;
            this.btnCancel.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.btnCancel.Location = new Point(219, 548);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(120, 42);
            this.btnCancel.TabIndex = 17;
            this.btnCancel.Text = "Hủy";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new EventHandler(this.BtnCancel_Click);
            // 
            // FrmAddAccount
            // 
            this.AcceptButton = this.btnSave;
            this.AutoScaleDimensions = new SizeF(8F, 20F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = Color.White;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new Size(520, 620);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.lblError);
            this.Controls.Add(this.chkShowPassword);
            this.Controls.Add(this.cboRole);
            this.Controls.Add(this.lblRole);
            this.Controls.Add(this.txtEmail);
            this.Controls.Add(this.lblEmail);
            this.Controls.Add(this.txtNewPassword);
            this.Controls.Add(this.lblNewPassword);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.lblPassword);
            this.Controls.Add(this.txtUsername);
            this.Controls.Add(this.lblUsername);
            this.Controls.Add(this.txtFullName);
            this.Controls.Add(this.lblFullName);
            this.Controls.Add(this.lblSubtitle);
            this.Controls.Add(this.lblTitle);
            this.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmAddAccount";
            this.ShowInTaskbar = false;
            this.StartPosition = FormStartPosition.CenterParent;
            this.Text = "Thêm tài khoản mới";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void ApplyMode()
        {
            this.cboRole.SelectedIndex = 0;

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
                this.lblPassword.Text = "Mật khẩu cũ";
                this.lblNewPassword.Visible = true;
                this.txtNewPassword.Visible = true;
                this.lblEmail.Location = new Point(32, 404);
                this.txtEmail.Location = new Point(35, 430);
                this.lblRole.Location = new Point(32, 476);
                this.cboRole.Location = new Point(35, 502);
                this.chkShowPassword.Location = new Point(35, 548);
                this.lblError.Location = new Point(32, 580);
                this.btnCancel.Location = new Point(219, 608);
                this.btnSave.Location = new Point(355, 608);
                this.ClientSize = new Size(520, 680);
                this.txtUsername.ReadOnly = true;
                this.txtUsername.BackColor = Color.FromArgb(241, 245, 249);
                this.btnSave.Text = "Lưu";
                this.Text = "Sửa tài khoản";
            }
            else if (this.mode == AccountDialogMode.View)
            {
                this.lblTitle.Text = "Chi tiết tài khoản";
                this.lblSubtitle.Text = "Thông tin tài khoản trong hệ thống";
                this.lblPassword.Visible = false;
                this.txtPassword.Visible = false;
                this.lblNewPassword.Visible = false;
                this.txtNewPassword.Visible = false;
                this.chkShowPassword.Visible = false;
                this.lblEmail.Location = new Point(32, 260);
                this.txtEmail.Location = new Point(35, 286);
                this.lblRole.Location = new Point(32, 332);
                this.cboRole.Location = new Point(35, 358);
                this.lblError.Visible = false;
                this.btnSave.Visible = false;
                this.btnCancel.Location = new Point(355, 430);
                this.btnCancel.Text = "Đóng";
                this.ClientSize = new Size(520, 500);
                SetReadOnly(this.txtFullName);
                SetReadOnly(this.txtUsername);
                SetReadOnly(this.txtEmail);
                this.cboRole.Enabled = false;
                this.AcceptButton = this.btnCancel;
                this.Text = "Chi tiết tài khoản";
            }
        }

        private static void SetReadOnly(TextBox textBox)
        {
            textBox.ReadOnly = true;
            textBox.BackColor = Color.FromArgb(241, 245, 249);
        }

        private void ChkShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            bool hidePassword = !this.chkShowPassword.Checked;
            this.txtPassword.UseSystemPasswordChar = hidePassword;
            this.txtNewPassword.UseSystemPasswordChar = hidePassword;
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
    }
}
