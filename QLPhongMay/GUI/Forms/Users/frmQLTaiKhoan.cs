using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using QLPhongMay.BLL;
using QLPhongMay.DAL;
using QLPhongMay.DTO;

namespace QLPhongMay.GUI.Forms.Users
{
    public partial class frmQLTaiKhoan : Form
    {
        private readonly UserRepository userRepository;
        private List<AccountListItem> accounts;
        private Panel pnlRoot;
        private Button btnBack;
        private Label lblTitle;
        private Label lblSubtitle;
        private Button btnAdd;
        private Panel pnlStats;
        private Label lblTotalAccounts;
        private Label lblAdminAccounts;
        private Label lblStaffAccounts;
        private Panel pnlFilter;
        private TextBox txtSearch;
        private ComboBox cboRole;
        private DataGridView dgvAccounts;
        private Label lblSummary;

        public frmQLTaiKhoan()
        {
            this.userRepository = new UserRepository();
            this.accounts = new List<AccountListItem>();
            InitializeComponent();
            this.Load += new EventHandler(this.frmQLTaiKhoan_Load);
        }

        private void InitializeComponent()
        {
            this.pnlRoot = new Panel();
            this.btnBack = new Button();
            this.lblTitle = new Label();
            this.lblSubtitle = new Label();
            this.btnAdd = new Button();
            this.pnlStats = new Panel();
            this.lblTotalAccounts = new Label();
            this.lblAdminAccounts = new Label();
            this.lblStaffAccounts = new Label();
            this.pnlFilter = new Panel();
            this.txtSearch = new TextBox();
            this.cboRole = new ComboBox();
            this.dgvAccounts = new DataGridView();
            this.lblSummary = new Label();
            this.pnlRoot.SuspendLayout();
            this.pnlStats.SuspendLayout();
            this.pnlFilter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAccounts)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlRoot
            // 
            this.pnlRoot.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            this.pnlRoot.Controls.Add(this.btnBack);
            this.pnlRoot.Controls.Add(this.lblTitle);
            this.pnlRoot.Controls.Add(this.lblSubtitle);
            this.pnlRoot.Controls.Add(this.btnAdd);
            this.pnlRoot.Controls.Add(this.pnlStats);
            this.pnlRoot.Controls.Add(this.pnlFilter);
            this.pnlRoot.Controls.Add(this.dgvAccounts);
            this.pnlRoot.Controls.Add(this.lblSummary);
            this.pnlRoot.Location = new Point(24, 22);
            this.pnlRoot.Name = "pnlRoot";
            this.pnlRoot.Size = new Size(1132, 816);
            this.pnlRoot.TabIndex = 0;
            // 
            // btnBack
            // 
            this.btnBack.FlatStyle = FlatStyle.Flat;
            this.btnBack.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.btnBack.Location = new Point(0, 8);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new Size(124, 42);
            this.btnBack.TabIndex = 0;
            this.btnBack.Text = "< Quay lại";
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new EventHandler(this.BtnBack_Click);
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new Font("Segoe UI", 22F, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.lblTitle.ForeColor = Color.FromArgb(15, 23, 42);
            this.lblTitle.Location = new Point(148, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new Size(330, 50);
            this.lblTitle.TabIndex = 1;
            this.lblTitle.Text = "Quản lý tài khoản";
            // 
            // lblSubtitle
            // 
            this.lblSubtitle.AutoSize = true;
            this.lblSubtitle.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.lblSubtitle.ForeColor = Color.FromArgb(100, 116, 139);
            this.lblSubtitle.Location = new Point(152, 52);
            this.lblSubtitle.Name = "lblSubtitle";
            this.lblSubtitle.Size = new Size(402, 23);
            this.lblSubtitle.TabIndex = 2;
            this.lblSubtitle.Text = "Theo dõi và phân quyền tài khoản trong hệ thống";
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            this.btnAdd.BackColor = Color.FromArgb(37, 99, 235);
            this.btnAdd.FlatStyle = FlatStyle.Flat;
            this.btnAdd.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.btnAdd.ForeColor = Color.White;
            this.btnAdd.Location = new Point(932, 92);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new Size(200, 42);
            this.btnAdd.TabIndex = 3;
            this.btnAdd.Text = "+ Thêm tài khoản";
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Click += new EventHandler(this.BtnAdd_Click);
            // 
            // pnlStats
            // 
            this.pnlStats.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.pnlStats.BackColor = Color.White;
            this.pnlStats.BorderStyle = BorderStyle.FixedSingle;
            this.pnlStats.Controls.Add(this.lblTotalAccounts);
            this.pnlStats.Controls.Add(this.lblAdminAccounts);
            this.pnlStats.Controls.Add(this.lblStaffAccounts);
            this.pnlStats.Location = new Point(0, 92);
            this.pnlStats.Name = "pnlStats";
            this.pnlStats.Size = new Size(900, 84);
            this.pnlStats.TabIndex = 4;
            // 
            // lblTotalAccounts
            // 
            this.lblTotalAccounts.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.lblTotalAccounts.ForeColor = Color.FromArgb(37, 99, 235);
            this.lblTotalAccounts.Location = new Point(22, 14);
            this.lblTotalAccounts.Name = "lblTotalAccounts";
            this.lblTotalAccounts.Size = new Size(250, 54);
            this.lblTotalAccounts.TabIndex = 0;
            this.lblTotalAccounts.Text = "Tổng tài khoản: 0";
            this.lblTotalAccounts.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblAdminAccounts
            // 
            this.lblAdminAccounts.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.lblAdminAccounts.ForeColor = Color.FromArgb(124, 58, 237);
            this.lblAdminAccounts.Location = new Point(314, 14);
            this.lblAdminAccounts.Name = "lblAdminAccounts";
            this.lblAdminAccounts.Size = new Size(220, 54);
            this.lblAdminAccounts.TabIndex = 1;
            this.lblAdminAccounts.Text = "Admin: 0";
            this.lblAdminAccounts.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblStaffAccounts
            // 
            this.lblStaffAccounts.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.lblStaffAccounts.ForeColor = Color.FromArgb(22, 163, 74);
            this.lblStaffAccounts.Location = new Point(586, 14);
            this.lblStaffAccounts.Name = "lblStaffAccounts";
            this.lblStaffAccounts.Size = new Size(250, 54);
            this.lblStaffAccounts.TabIndex = 2;
            this.lblStaffAccounts.Text = "Quản lý phòng máy: 0";
            this.lblStaffAccounts.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // pnlFilter
            // 
            this.pnlFilter.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.pnlFilter.BackColor = Color.White;
            this.pnlFilter.BorderStyle = BorderStyle.FixedSingle;
            this.pnlFilter.Controls.Add(this.txtSearch);
            this.pnlFilter.Controls.Add(this.cboRole);
            this.pnlFilter.Location = new Point(0, 194);
            this.pnlFilter.Name = "pnlFilter";
            this.pnlFilter.Size = new Size(1132, 66);
            this.pnlFilter.TabIndex = 5;
            // 
            // txtSearch
            // 
            this.txtSearch.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.txtSearch.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.txtSearch.Location = new Point(18, 17);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new Size(850, 30);
            this.txtSearch.TabIndex = 0;
            this.txtSearch.TextChanged += new EventHandler(this.FilterChanged);
            // 
            // cboRole
            // 
            this.cboRole.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            this.cboRole.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cboRole.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.cboRole.FormattingEnabled = true;
            this.cboRole.Items.AddRange(new object[] {
            "Tất cả vai trò",
            "Admin",
            "Quản lý phòng máy"});
            this.cboRole.Location = new Point(898, 17);
            this.cboRole.Name = "cboRole";
            this.cboRole.Size = new Size(210, 31);
            this.cboRole.TabIndex = 1;
            this.cboRole.SelectedIndexChanged += new EventHandler(this.FilterChanged);
            // 
            // dgvAccounts
            // 
            this.dgvAccounts.AllowUserToAddRows = false;
            this.dgvAccounts.AllowUserToDeleteRows = false;
            this.dgvAccounts.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            this.dgvAccounts.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvAccounts.BackgroundColor = Color.White;
            this.dgvAccounts.ColumnHeadersHeight = 36;
            this.dgvAccounts.Location = new Point(0, 282);
            this.dgvAccounts.MultiSelect = false;
            this.dgvAccounts.Name = "dgvAccounts";
            this.dgvAccounts.ReadOnly = true;
            this.dgvAccounts.RowHeadersWidth = 36;
            this.dgvAccounts.RowTemplate.Height = 34;
            this.dgvAccounts.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgvAccounts.Size = new Size(1132, 476);
            this.dgvAccounts.TabIndex = 6;
            this.dgvAccounts.CellContentClick += new DataGridViewCellEventHandler(this.DgvAccounts_CellContentClick);
            // 
            // lblSummary
            // 
            this.lblSummary.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            this.lblSummary.AutoSize = true;
            this.lblSummary.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.lblSummary.Location = new Point(4, 778);
            this.lblSummary.Name = "lblSummary";
            this.lblSummary.Size = new Size(146, 23);
            this.lblSummary.TabIndex = 7;
            this.lblSummary.Text = "Tổng tài khoản: 0";
            // 
            // frmQLTaiKhoan
            // 
            this.AutoScaleDimensions = new SizeF(8F, 20F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = Color.FromArgb(246, 248, 252);
            this.ClientSize = new Size(1180, 860);
            this.Controls.Add(this.pnlRoot);
            this.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.MinimumSize = new Size(1080, 760);
            this.Name = "frmQLTaiKhoan";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Quản lý tài khoản";
            this.pnlRoot.ResumeLayout(false);
            this.pnlRoot.PerformLayout();
            this.pnlStats.ResumeLayout(false);
            this.pnlFilter.ResumeLayout(false);
            this.pnlFilter.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAccounts)).EndInit();
            this.ResumeLayout(false);
        }

        private void frmQLTaiKhoan_Load(object sender, EventArgs e)
        {
            this.cboRole.SelectedIndex = 0;
            RefreshAccounts();
        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            using (FrmAddAccount dialog = new FrmAddAccount())
            {
                dialog.ExternalValidator = ValidateCreateAccount;
                if (dialog.ShowDialog(this) != DialogResult.OK)
                {
                    return;
                }

                try
                {
                    this.userRepository.CreateAccount(
                        dialog.Username,
                        PasswordHasher.HashPassword(dialog.Password),
                        dialog.FullName,
                        dialog.Email,
                        dialog.RoleId);

                    RefreshAccounts();
                    MessageBox.Show("Đã thêm tài khoản thành công.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    ShowDataError("Không thể thêm tài khoản.", ex);
                }
            }
        }

        private FrmAddAccount.AccountValidationError ValidateCreateAccount(FrmAddAccount dialog)
        {
            try
            {
                if (this.userRepository.UsernameExists(dialog.Username))
                {
                    return new FrmAddAccount.AccountValidationError("Tên đăng nhập đã tồn tại.", FrmAddAccount.AccountField.Username);
                }

                if (this.userRepository.EmailExists(dialog.Email))
                {
                    return new FrmAddAccount.AccountValidationError("Email đã tồn tại.", FrmAddAccount.AccountField.Email);
                }
            }
            catch (Exception ex)
            {
                return new FrmAddAccount.AccountValidationError("Không thể kiểm tra dữ liệu: " + ex.Message, FrmAddAccount.AccountField.Username);
            }

            return null;
        }

        private void RefreshAccounts()
        {
            try
            {
                this.accounts = this.userRepository.GetAccountList().ToList();
            }
            catch (Exception ex)
            {
                this.accounts = new List<AccountListItem>();
                ShowDataError("Không thể tải dữ liệu tài khoản từ database QuanLyPhongMay.", ex);
            }

            UpdateStats();
            LoadGrid();
        }

        private void UpdateStats()
        {
            int total = this.accounts.Count;
            int admin = this.accounts.Count(item => item.MaVaiTro == 1);
            int staff = this.accounts.Count(item => item.MaVaiTro == 2);

            this.lblTotalAccounts.Text = "Tổng tài khoản: " + total;
            this.lblAdminAccounts.Text = "Admin: " + admin;
            this.lblStaffAccounts.Text = "Quản lý phòng máy: " + staff;
            this.lblSummary.Text = "Tổng tài khoản: " + total;
        }

        private void FilterChanged(object sender, EventArgs e)
        {
            LoadGrid();
        }

        private void LoadGrid()
        {
            string keyword = this.txtSearch.Text.Trim().ToLowerInvariant();
            string roleFilter = Convert.ToString(this.cboRole.SelectedItem);
            IEnumerable<AccountListItem> source = this.accounts;

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                source = source.Where(item =>
                    ContainsText(item.HoTen, keyword) ||
                    ContainsText(item.TenDangNhap, keyword) ||
                    ContainsText(item.Email, keyword));
            }

            if (!string.IsNullOrWhiteSpace(roleFilter) && roleFilter != "Tất cả vai trò")
            {
                source = source.Where(item => GetRoleName(item) == roleFilter);
            }

            var rows = source
                .Select((item, index) => new AccountRow
                {
                    Stt = index + 1,
                    Username = item.TenDangNhap,
                    FullName = item.HoTen,
                    Email = item.Email,
                    Role = GetRoleName(item)
                })
                .ToList();

            this.dgvAccounts.Columns.Clear();
            this.dgvAccounts.DataSource = rows;
            ConfigureGridColumns();
            this.lblSummary.Text = "Đang hiển thị: " + rows.Count + " / " + this.accounts.Count + " tài khoản";
        }

        private static bool ContainsText(string text, string keyword)
        {
            return !string.IsNullOrEmpty(text) && text.ToLowerInvariant().Contains(keyword);
        }

        private static string GetRoleName(AccountListItem item)
        {
            if (!string.IsNullOrWhiteSpace(item.TenVaiTro))
            {
                return item.TenVaiTro;
            }

            return item.MaVaiTro == 1 ? "Admin" : "Quản lý phòng máy";
        }

        private void ConfigureGridColumns()
        {
            if (this.dgvAccounts.Columns.Count == 0)
            {
                return;
            }

            this.dgvAccounts.Columns[nameof(AccountRow.Stt)].HeaderText = "STT";
            this.dgvAccounts.Columns[nameof(AccountRow.Username)].HeaderText = "Tên đăng nhập";
            this.dgvAccounts.Columns[nameof(AccountRow.FullName)].HeaderText = "Họ tên";
            this.dgvAccounts.Columns[nameof(AccountRow.Email)].HeaderText = "Email";
            this.dgvAccounts.Columns[nameof(AccountRow.Role)].HeaderText = "Vai trò";

            this.dgvAccounts.Columns[nameof(AccountRow.Stt)].FillWeight = 45;
            this.dgvAccounts.Columns[nameof(AccountRow.Username)].FillWeight = 130;
            this.dgvAccounts.Columns[nameof(AccountRow.FullName)].FillWeight = 180;
            this.dgvAccounts.Columns[nameof(AccountRow.Email)].FillWeight = 220;
            this.dgvAccounts.Columns[nameof(AccountRow.Role)].FillWeight = 130;

            AddButtonColumn("View", "Xem");
            AddButtonColumn("Edit", "Sửa");
            AddButtonColumn("Delete", "Xóa");
        }

        private void AddButtonColumn(string name, string text)
        {
            DataGridViewButtonColumn column = new DataGridViewButtonColumn();
            column.Name = name;
            column.HeaderText = text;
            column.Text = text;
            column.UseColumnTextForButtonValue = true;
            column.FillWeight = 65;
            this.dgvAccounts.Columns.Add(column);
        }

        private void DgvAccounts_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
            {
                return;
            }

            string columnName = this.dgvAccounts.Columns[e.ColumnIndex].Name;
            AccountRow row = this.dgvAccounts.Rows[e.RowIndex].DataBoundItem as AccountRow;
            if (row == null)
            {
                return;
            }

            if (columnName == "View")
            {
                ViewAccount(row.Username);
            }
            else if (columnName == "Edit")
            {
                EditAccount(row.Username);
            }
            else if (columnName == "Delete")
            {
                DeleteAccount(row.Username);
            }
        }

        private void ViewAccount(string username)
        {
            AccountListItem account = GetAccount(username);
            if (account == null)
            {
                return;
            }

            using (FrmAddAccount dialog = new FrmAddAccount(FrmAddAccount.AccountDialogMode.View, account))
            {
                dialog.ShowDialog(this);
            }
        }

        private void EditAccount(string username)
        {
            AccountListItem account = GetAccount(username);
            if (account == null)
            {
                return;
            }

            using (FrmAddAccount dialog = new FrmAddAccount(FrmAddAccount.AccountDialogMode.Edit, account))
            {
                if (dialog.ShowDialog(this) != DialogResult.OK)
                {
                    return;
                }

                try
                {
                    string passwordHash = string.IsNullOrWhiteSpace(dialog.NewPassword)
                        ? null
                        : PasswordHasher.HashPassword(dialog.NewPassword);
                    this.userRepository.UpdateAccount(dialog.Username, dialog.FullName, dialog.Email, dialog.RoleId, passwordHash);
                    RefreshAccounts();
                    MessageBox.Show("Đã cập nhật tài khoản thành công.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    ShowDataError("Không thể cập nhật tài khoản.", ex);
                }
            }
        }

        private void DeleteAccount(string username)
        {
            DialogResult result = MessageBox.Show(
                "Bạn có chắc muốn xóa tài khoản '" + username + "' không?",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (result != DialogResult.Yes)
            {
                return;
            }

            try
            {
                this.userRepository.DeleteAccount(username);
                RefreshAccounts();
                MessageBox.Show("Đã xóa tài khoản thành công.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                ShowDataError("Không thể xóa tài khoản.", ex);
            }
        }

        private AccountListItem GetAccount(string username)
        {
            try
            {
                AccountListItem account = this.userRepository.GetAccountByUsername(username);
                if (account == null)
                {
                    MessageBox.Show("Không tìm thấy tài khoản.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                return account;
            }
            catch (Exception ex)
            {
                ShowDataError("Không thể lấy thông tin tài khoản.", ex);
                return null;
            }
        }

        private static void ShowDataError(string title, Exception ex)
        {
            MessageBox.Show(title + "\n" + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private class AccountRow
        {
            public int Stt { get; set; }

            public string Username { get; set; }

            public string FullName { get; set; }

            public string Email { get; set; }

            public string Role { get; set; }
        }
    }
}
