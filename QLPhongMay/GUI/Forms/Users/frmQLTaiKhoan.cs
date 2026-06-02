using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using QLPhongMay.BLL;
using QLPhongMay.DAL;
using QLPhongMay.DTO;

namespace QLPhongMay.GUI.Forms.Users
{
    public partial class frmQLTaiKhoan : Form
    {
        private readonly UserRepository userRepository;
        private const int PageSize = 5;
        private List<AccountListItem> accounts;
        private List<AccountListItem> filteredAccounts;
        private int currentPage;
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
        private Button btnPreviousPage;
        private Label lblPageInfo;
        private Button btnNextPage;

        public frmQLTaiKhoan()
        {
            this.userRepository = new UserRepository();
            this.accounts = new List<AccountListItem>();
            this.filteredAccounts = new List<AccountListItem>();
            this.currentPage = 1;
            InitializeComponent();
            this.Load += new EventHandler(this.frmQLTaiKhoan_Load);
            this.Resize += new EventHandler(this.frmQLTaiKhoan_Resize);
        }

        private void InitializeComponent()
        {
            this.pnlRoot = new System.Windows.Forms.Panel();
            this.btnBack = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblSubtitle = new System.Windows.Forms.Label();
            this.btnAdd = new System.Windows.Forms.Button();
            this.pnlStats = new System.Windows.Forms.Panel();
            this.lblTotalAccounts = new System.Windows.Forms.Label();
            this.lblAdminAccounts = new System.Windows.Forms.Label();
            this.lblStaffAccounts = new System.Windows.Forms.Label();
            this.pnlFilter = new System.Windows.Forms.Panel();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.cboRole = new System.Windows.Forms.ComboBox();
            this.dgvAccounts = new System.Windows.Forms.DataGridView();
            this.lblSummary = new System.Windows.Forms.Label();
            this.btnPreviousPage = new System.Windows.Forms.Button();
            this.lblPageInfo = new System.Windows.Forms.Label();
            this.btnNextPage = new System.Windows.Forms.Button();
            this.pnlRoot.SuspendLayout();
            this.pnlStats.SuspendLayout();
            this.pnlFilter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAccounts)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlRoot
            // 
            this.pnlRoot.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlRoot.Controls.Add(this.btnBack);
            this.pnlRoot.Controls.Add(this.lblTitle);
            this.pnlRoot.Controls.Add(this.lblSubtitle);
            this.pnlRoot.Controls.Add(this.btnAdd);
            this.pnlRoot.Controls.Add(this.pnlStats);
            this.pnlRoot.Controls.Add(this.pnlFilter);
            this.pnlRoot.Controls.Add(this.dgvAccounts);
            this.pnlRoot.Controls.Add(this.lblSummary);
            this.pnlRoot.Controls.Add(this.btnPreviousPage);
            this.pnlRoot.Controls.Add(this.lblPageInfo);
            this.pnlRoot.Controls.Add(this.btnNextPage);
            this.pnlRoot.Location = new System.Drawing.Point(24, 22);
            this.pnlRoot.Name = "pnlRoot";
            this.pnlRoot.Size = new System.Drawing.Size(1132, 816);
            this.pnlRoot.TabIndex = 0;
            // 
            // btnBack
            // 
            this.btnBack.BackColor = System.Drawing.Color.White;
            this.btnBack.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.btnBack.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBack.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBack.Location = new System.Drawing.Point(0, 8);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(124, 42);
            this.btnBack.TabIndex = 0;
            this.btnBack.Text = "< Quay lại";
            this.btnBack.UseVisualStyleBackColor = false;
            this.btnBack.Click += new System.EventHandler(this.BtnBack_Click);
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 22F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblTitle.Location = new System.Drawing.Point(148, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(392, 60);
            this.lblTitle.TabIndex = 1;
            this.lblTitle.Text = "Quản lý tài khoản";
            // 
            // lblSubtitle
            // 
            this.lblSubtitle.AutoSize = true;
            this.lblSubtitle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblSubtitle.Location = new System.Drawing.Point(152, 52);
            this.lblSubtitle.Name = "lblSubtitle";
            this.lblSubtitle.Size = new System.Drawing.Size(446, 28);
            this.lblSubtitle.TabIndex = 2;
            this.lblSubtitle.Text = "Theo dõi và phân quyền tài khoản trong hệ thống";
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAdd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.btnAdd.FlatAppearance.BorderSize = 0;
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdd.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdd.ForeColor = System.Drawing.Color.White;
            this.btnAdd.Location = new System.Drawing.Point(932, 92);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(200, 42);
            this.btnAdd.TabIndex = 3;
            this.btnAdd.Text = "+ Thêm tài khoản";
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Click += new System.EventHandler(this.BtnAdd_Click);
            // 
            // pnlStats
            // 
            this.pnlStats.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlStats.BackColor = System.Drawing.Color.White;
            this.pnlStats.Controls.Add(this.lblTotalAccounts);
            this.pnlStats.Controls.Add(this.lblAdminAccounts);
            this.pnlStats.Controls.Add(this.lblStaffAccounts);
            this.pnlStats.Location = new System.Drawing.Point(0, 92);
            this.pnlStats.Name = "pnlStats";
            this.pnlStats.Size = new System.Drawing.Size(868, 84);
            this.pnlStats.TabIndex = 4;
            // 
            // lblTotalAccounts
            // 
            this.lblTotalAccounts.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalAccounts.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.lblTotalAccounts.Location = new System.Drawing.Point(22, 14);
            this.lblTotalAccounts.Name = "lblTotalAccounts";
            this.lblTotalAccounts.Size = new System.Drawing.Size(250, 54);
            this.lblTotalAccounts.TabIndex = 0;
            this.lblTotalAccounts.Text = "Tổng tài khoản: 0";
            this.lblTotalAccounts.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblAdminAccounts
            // 
            this.lblAdminAccounts.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAdminAccounts.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(124)))), ((int)(((byte)(58)))), ((int)(((byte)(237)))));
            this.lblAdminAccounts.Location = new System.Drawing.Point(314, 14);
            this.lblAdminAccounts.Name = "lblAdminAccounts";
            this.lblAdminAccounts.Size = new System.Drawing.Size(220, 54);
            this.lblAdminAccounts.TabIndex = 1;
            this.lblAdminAccounts.Text = "Admin: 0";
            this.lblAdminAccounts.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblStaffAccounts
            // 
            this.lblStaffAccounts.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStaffAccounts.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(163)))), ((int)(((byte)(74)))));
            this.lblStaffAccounts.Location = new System.Drawing.Point(586, 14);
            this.lblStaffAccounts.Name = "lblStaffAccounts";
            this.lblStaffAccounts.Size = new System.Drawing.Size(250, 54);
            this.lblStaffAccounts.TabIndex = 2;
            this.lblStaffAccounts.Text = "Quản lý phòng máy: 0";
            this.lblStaffAccounts.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlFilter
            // 
            this.pnlFilter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlFilter.BackColor = System.Drawing.Color.White;
            this.pnlFilter.Controls.Add(this.txtSearch);
            this.pnlFilter.Controls.Add(this.cboRole);
            this.pnlFilter.Location = new System.Drawing.Point(0, 194);
            this.pnlFilter.Name = "pnlFilter";
            this.pnlFilter.Size = new System.Drawing.Size(1132, 66);
            this.pnlFilter.TabIndex = 5;
            // 
            // txtSearch
            // 
            this.txtSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSearch.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtSearch.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSearch.Location = new System.Drawing.Point(18, 17);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(850, 27);
            this.txtSearch.TabIndex = 0;
            this.txtSearch.TextChanged += new System.EventHandler(this.FilterChanged);
            // 
            // cboRole
            // 
            this.cboRole.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cboRole.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.cboRole.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboRole.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboRole.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboRole.FormattingEnabled = true;
            this.cboRole.Items.AddRange(new object[] {
            "Tất cả vai trò",
            "Admin",
            "Quản lý phòng máy"});
            this.cboRole.Location = new System.Drawing.Point(898, 17);
            this.cboRole.Name = "cboRole";
            this.cboRole.Size = new System.Drawing.Size(210, 36);
            this.cboRole.TabIndex = 1;
            this.cboRole.SelectedIndexChanged += new System.EventHandler(this.FilterChanged);
            // 
            // dgvAccounts
            // 
            this.dgvAccounts.AllowUserToAddRows = false;
            this.dgvAccounts.AllowUserToDeleteRows = false;
            this.dgvAccounts.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvAccounts.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvAccounts.BackgroundColor = System.Drawing.Color.White;
            this.dgvAccounts.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvAccounts.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvAccounts.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgvAccounts.ColumnHeadersHeight = 36;
            this.dgvAccounts.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(239)))), ((int)(((byte)(239)))));
            this.dgvAccounts.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.Black;
            this.dgvAccounts.ColumnHeadersDefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(239)))), ((int)(((byte)(239)))));
            this.dgvAccounts.ColumnHeadersDefaultCellStyle.SelectionForeColor = System.Drawing.Color.Black;
            this.dgvAccounts.EnableHeadersVisualStyles = false;
            this.dgvAccounts.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.dgvAccounts.Location = new System.Drawing.Point(0, 282);
            this.dgvAccounts.MultiSelect = false;
            this.dgvAccounts.Name = "dgvAccounts";
            this.dgvAccounts.ReadOnly = true;
            this.dgvAccounts.RowHeadersWidth = 36;
            this.dgvAccounts.RowTemplate.Height = 34;
            this.dgvAccounts.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvAccounts.Size = new System.Drawing.Size(1132, 476);
            this.dgvAccounts.TabIndex = 6;
            this.dgvAccounts.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgvAccounts_CellContentClick);
            // 
            // lblSummary
            // 
            this.lblSummary.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblSummary.AutoSize = true;
            this.lblSummary.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSummary.Location = new System.Drawing.Point(4, 778);
            this.lblSummary.Name = "lblSummary";
            this.lblSummary.Size = new System.Drawing.Size(178, 28);
            this.lblSummary.TabIndex = 7;
            this.lblSummary.Text = "Tổng tài khoản: 0";
            // 
            // btnPreviousPage
            // 
            this.btnPreviousPage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPreviousPage.BackColor = System.Drawing.Color.White;
            this.btnPreviousPage.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.btnPreviousPage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPreviousPage.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPreviousPage.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.btnPreviousPage.Location = new System.Drawing.Point(940, 770);
            this.btnPreviousPage.Name = "btnPreviousPage";
            this.btnPreviousPage.Size = new System.Drawing.Size(46, 36);
            this.btnPreviousPage.TabIndex = 8;
            this.btnPreviousPage.Text = "<";
            this.btnPreviousPage.UseVisualStyleBackColor = false;
            this.btnPreviousPage.Click += new System.EventHandler(this.BtnPreviousPage_Click);
            // 
            // lblPageInfo
            // 
            this.lblPageInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPageInfo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.lblPageInfo.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPageInfo.ForeColor = System.Drawing.Color.White;
            this.lblPageInfo.Location = new System.Drawing.Point(994, 770);
            this.lblPageInfo.Name = "lblPageInfo";
            this.lblPageInfo.Size = new System.Drawing.Size(84, 36);
            this.lblPageInfo.TabIndex = 9;
            this.lblPageInfo.Text = "1 / 1";
            this.lblPageInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnNextPage
            // 
            this.btnNextPage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNextPage.BackColor = System.Drawing.Color.White;
            this.btnNextPage.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.btnNextPage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNextPage.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNextPage.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.btnNextPage.Location = new System.Drawing.Point(1086, 770);
            this.btnNextPage.Name = "btnNextPage";
            this.btnNextPage.Size = new System.Drawing.Size(46, 36);
            this.btnNextPage.TabIndex = 10;
            this.btnNextPage.Text = ">";
            this.btnNextPage.UseVisualStyleBackColor = false;
            this.btnNextPage.Click += new System.EventHandler(this.BtnNextPage_Click);
            // 
            // frmQLTaiKhoan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(248)))), ((int)(((byte)(252)))));
            this.ClientSize = new System.Drawing.Size(1180, 860);
            this.Controls.Add(this.pnlRoot);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MinimumSize = new System.Drawing.Size(1080, 760);
            this.Name = "frmQLTaiKhoan";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
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
            LayoutResponsive();
            ApplySearchPlaceholder();
            this.cboRole.SelectedIndex = 0;
            RefreshAccounts();
        }

        private void frmQLTaiKhoan_Resize(object sender, EventArgs e)
        {
            LayoutResponsive();
        }

        private void LayoutResponsive()
        {
            if (this.pnlRoot == null)
            {
                return;
            }

            int rootWidth = this.pnlRoot.ClientSize.Width;
            int rootHeight = this.pnlRoot.ClientSize.Height;
            if (rootWidth <= 0 || rootHeight <= 0)
            {
                return;
            }

            this.btnBack.Location = new Point(0, 8);
            this.lblTitle.Location = new Point(this.btnBack.Right + 24, 0);
            this.lblSubtitle.Location = new Point(this.lblTitle.Left + 4, 52);
            this.lblSubtitle.MaximumSize = new Size(Math.Max(360, rootWidth - this.lblSubtitle.Left - 24), 0);

            this.btnAdd.Location = new Point(rootWidth - this.btnAdd.Width, 92);

            int statsWidth = Math.Max(560, this.btnAdd.Left - 32);
            this.pnlStats.Location = new Point(0, 92);
            this.pnlStats.Size = new Size(statsWidth, 84);

            int statsInnerWidth = Math.Max(360, this.pnlStats.ClientSize.Width - 44);
            int statsColumnWidth = statsInnerWidth / 3;
            this.lblTotalAccounts.Location = new Point(22, 14);
            this.lblTotalAccounts.Size = new Size(statsColumnWidth, 54);
            this.lblAdminAccounts.Location = new Point(22 + statsColumnWidth, 14);
            this.lblAdminAccounts.Size = new Size(statsColumnWidth, 54);
            this.lblStaffAccounts.Location = new Point(22 + statsColumnWidth * 2, 14);
            this.lblStaffAccounts.Size = new Size(statsInnerWidth - statsColumnWidth * 2, 54);

            this.pnlFilter.Location = new Point(0, 194);
            this.pnlFilter.Size = new Size(rootWidth, 66);
            this.cboRole.Location = new Point(rootWidth - this.cboRole.Width - 22, 17);
            this.txtSearch.Location = new Point(18, 17);
            this.txtSearch.Size = new Size(Math.Max(320, this.cboRole.Left - 36), 30);

            int gridTop = 282;
            int pagingTop = rootHeight - 46;
            int gridHeight = Math.Max(260, pagingTop - gridTop - 16);
            this.dgvAccounts.Location = new Point(0, gridTop);
            this.dgvAccounts.Size = new Size(rootWidth, gridHeight);

            this.lblSummary.Location = new Point(4, pagingTop + 6);
            this.lblSummary.MaximumSize = new Size(Math.Max(360, rootWidth - 260), 0);

            this.btnNextPage.Location = new Point(rootWidth - this.btnNextPage.Width, pagingTop);
            this.lblPageInfo.Location = new Point(this.btnNextPage.Left - this.lblPageInfo.Width - 8, pagingTop);
            this.btnPreviousPage.Location = new Point(this.lblPageInfo.Left - this.btnPreviousPage.Width - 8, pagingTop);
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
            this.currentPage = 1;
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

            this.filteredAccounts = source.ToList();
            int totalFiltered = this.filteredAccounts.Count;
            int totalPages = Math.Max(1, (int)Math.Ceiling(totalFiltered / (double)PageSize));
            if (this.currentPage > totalPages)
            {
                this.currentPage = totalPages;
            }

            var pageItems = this.filteredAccounts
                .Skip((this.currentPage - 1) * PageSize)
                .Take(PageSize)
                .ToList();

            var rows = pageItems
                .Select((item, index) => new AccountRow
                {
                    Stt = ((this.currentPage - 1) * PageSize) + index + 1,
                    Username = item.TenDangNhap,
                    FullName = item.HoTen,
                    Email = item.Email,
                    Role = GetRoleName(item)
                })
                .ToList();

            this.dgvAccounts.Columns.Clear();
            this.dgvAccounts.DataSource = rows;
            ConfigureGridColumns();
            UpdatePaging(totalFiltered, totalPages, rows.Count);
        }

        private void UpdatePaging(int totalFiltered, int totalPages, int visibleCount)
        {
            if (totalFiltered == 0)
            {
                this.lblSummary.Text = "Không có tài khoản phù hợp";
                this.lblPageInfo.Text = "0 / 0";
                this.btnPreviousPage.Enabled = false;
                this.btnNextPage.Enabled = false;
                return;
            }

            int from = ((this.currentPage - 1) * PageSize) + 1;
            int to = from + visibleCount - 1;
            this.lblSummary.Text = "Hiển thị " + from + "-" + to + " / " + totalFiltered + " tài khoản";
            this.lblPageInfo.Text = this.currentPage + " / " + totalPages;
            this.btnPreviousPage.Enabled = this.currentPage > 1;
            this.btnNextPage.Enabled = this.currentPage < totalPages;
        }

        private void BtnPreviousPage_Click(object sender, EventArgs e)
        {
            if (this.currentPage <= 1)
            {
                return;
            }

            this.currentPage--;
            LoadGrid();
        }

        private void BtnNextPage_Click(object sender, EventArgs e)
        {
            int totalPages = Math.Max(1, (int)Math.Ceiling(this.filteredAccounts.Count / (double)PageSize));
            if (this.currentPage >= totalPages)
            {
                return;
            }

            this.currentPage++;
            LoadGrid();
        }

        private void ApplySearchPlaceholder()
        {
            SendMessage(this.txtSearch.Handle, 0x1501, (IntPtr)1, "Tìm kiếm theo họ tên, tên đăng nhập, email");
        }

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wParam, string lParam);

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
            column.DefaultCellStyle.BackColor = Color.White;
            column.DefaultCellStyle.ForeColor = Color.FromArgb(37, 99, 235);
            column.DefaultCellStyle.SelectionBackColor = Color.FromArgb(239, 246, 255);
            column.DefaultCellStyle.SelectionForeColor = Color.FromArgb(37, 99, 235);
            column.FlatStyle = FlatStyle.Flat;
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
