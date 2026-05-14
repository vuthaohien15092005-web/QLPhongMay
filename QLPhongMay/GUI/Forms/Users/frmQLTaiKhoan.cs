using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
using Guna.UI2.WinForms;
using QLPhongMay.BLL;
using QLPhongMay.DAL;
using QLPhongMay.DTO;

namespace QLPhongMay.GUI.Forms.Users
{
    public partial class frmQLTaiKhoan : Form
    {
        private const int PageSize = 5;
        private readonly UserRepository userRepository;
        private readonly ToolTip actionToolTip;
        private List<AccountRow> accounts;
        private int currentPage = 1;

        private Guna2Panel pnlRoot;
        private Guna2Button btnBack;
        private Guna2HtmlLabel lblTitle;
        private Guna2HtmlLabel lblSubtitle;
        private Guna2Button btnAdd;
        private FlowLayoutPanel pnlStats;
        private Guna2Panel pnlFilter;
        private Guna2TextBox txtSearch;
        private Guna2ComboBox cboRole;
        private DataGridView dgvAccounts;
        private Guna2Panel pnlPaging;
        private Guna2HtmlLabel lblPagingInfo;
        private FlowLayoutPanel pnlPageButtons;

        public frmQLTaiKhoan()
        {
            this.userRepository = new UserRepository();
            this.accounts = new List<AccountRow>();
            this.actionToolTip = new ToolTip();

            InitializeComponent();
            this.Load += new EventHandler(this.frmQLTaiKhoan_Load);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.actionToolTip.Dispose();
                if (this.components != null)
                {
                    this.components.Dispose();
                }
            }

            base.Dispose(disposing);
        }

        private System.ComponentModel.IContainer components = null;

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmQLTaiKhoan));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pnlRoot = new Guna.UI2.WinForms.Guna2Panel();
            this.btnBack = new Guna.UI2.WinForms.Guna2Button();
            this.lblTitle = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.lblSubtitle = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.btnAdd = new Guna.UI2.WinForms.Guna2Button();
            this.pnlStats = new System.Windows.Forms.FlowLayoutPanel();
            this.pnlFilter = new Guna.UI2.WinForms.Guna2Panel();
            this.txtSearch = new Guna.UI2.WinForms.Guna2TextBox();
            this.cboRole = new Guna.UI2.WinForms.Guna2ComboBox();
            this.dgvAccounts = new System.Windows.Forms.DataGridView();
            this.pnlPaging = new Guna.UI2.WinForms.Guna2Panel();
            this.lblPagingInfo = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.pnlPageButtons = new System.Windows.Forms.FlowLayoutPanel();
            this.pnlRoot.SuspendLayout();
            this.pnlFilter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAccounts)).BeginInit();
            this.pnlPaging.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlRoot
            // 
            this.pnlRoot.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlRoot.BackColor = System.Drawing.Color.Transparent;
            this.pnlRoot.Controls.Add(this.btnBack);
            this.pnlRoot.Controls.Add(this.lblTitle);
            this.pnlRoot.Controls.Add(this.lblSubtitle);
            this.pnlRoot.Controls.Add(this.btnAdd);
            this.pnlRoot.Controls.Add(this.pnlStats);
            this.pnlRoot.Controls.Add(this.pnlFilter);
            this.pnlRoot.Controls.Add(this.dgvAccounts);
            this.pnlRoot.Controls.Add(this.pnlPaging);
            this.pnlRoot.Location = new System.Drawing.Point(28, 24);
            this.pnlRoot.Name = "pnlRoot";
            this.pnlRoot.Size = new System.Drawing.Size(1124, 808);
            this.pnlRoot.TabIndex = 0;
            // 
            // btnBack
            // 
            this.btnBack.Animated = true;
            this.btnBack.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.btnBack.BorderRadius = 8;
            this.btnBack.BorderThickness = 1;
            this.btnBack.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBack.FillColor = System.Drawing.Color.White;
            this.btnBack.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBack.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.btnBack.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.btnBack.Location = new System.Drawing.Point(0, 4);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(128, 46);
            this.btnBack.TabIndex = 0;
            this.btnBack.Text = "<  Quay lại";
            this.btnBack.Click += new System.EventHandler(this.BtnBack_Click);
            // 
            // lblTitle
            // 
            this.lblTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 21F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblTitle.Location = new System.Drawing.Point(146, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(334, 56);
            this.lblTitle.TabIndex = 1;
            this.lblTitle.Text = "Quản lý tài khoản";
            // 
            // lblSubtitle
            // 
            this.lblSubtitle.BackColor = System.Drawing.Color.Transparent;
            this.lblSubtitle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblSubtitle.Location = new System.Drawing.Point(150, 48);
            this.lblSubtitle.Name = "lblSubtitle";
            this.lblSubtitle.Size = new System.Drawing.Size(386, 25);
            this.lblSubtitle.TabIndex = 2;
            this.lblSubtitle.Text = "Theo dõi và phân quyền tài khoản trong hệ thống";
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAdd.Animated = true;
            this.btnAdd.BorderRadius = 8;
            this.btnAdd.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAdd.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.btnAdd.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdd.ForeColor = System.Drawing.Color.White;
            this.btnAdd.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(78)))), ((int)(((byte)(216)))));
            this.btnAdd.Location = new System.Drawing.Point(930, 92);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(194, 46);
            this.btnAdd.TabIndex = 3;
            this.btnAdd.Text = "+  Thêm tài khoản";
            this.btnAdd.Click += new System.EventHandler(this.BtnAdd_Click);
            // 
            // pnlStats
            // 
            this.pnlStats.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlStats.BackColor = System.Drawing.Color.Transparent;
            this.pnlStats.Location = new System.Drawing.Point(0, 82);
            this.pnlStats.Name = "pnlStats";
            this.pnlStats.Size = new System.Drawing.Size(850, 88);
            this.pnlStats.TabIndex = 3;
            this.pnlStats.WrapContents = false;
            // 
            // pnlFilter
            // 
            this.pnlFilter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlFilter.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.pnlFilter.BorderRadius = 8;
            this.pnlFilter.BorderThickness = 1;
            this.pnlFilter.Controls.Add(this.txtSearch);
            this.pnlFilter.Controls.Add(this.cboRole);
            this.pnlFilter.FillColor = System.Drawing.Color.White;
            this.pnlFilter.Location = new System.Drawing.Point(0, 188);
            this.pnlFilter.Name = "pnlFilter";
            this.pnlFilter.Size = new System.Drawing.Size(1124, 66);
            this.pnlFilter.TabIndex = 4;
            // 
            // txtSearch
            // 
            this.txtSearch.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.txtSearch.BorderRadius = 8;
            this.txtSearch.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtSearch.DefaultText = "";
            this.txtSearch.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.txtSearch.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.txtSearch.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSearch.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.txtSearch.IconLeft = ((System.Drawing.Image)(resources.GetObject("txtSearch.IconLeft")));
            this.txtSearch.IconLeftOffset = new System.Drawing.Point(6, 0);
            this.txtSearch.IconLeftSize = new System.Drawing.Size(16, 16);
            this.txtSearch.Location = new System.Drawing.Point(22, 11);
            this.txtSearch.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(163)))), ((int)(((byte)(184)))));
            this.txtSearch.PlaceholderText = "Tìm kiếm theo họ tên, tên đăng nhập, email";
            this.txtSearch.SelectedText = "";
            this.txtSearch.Size = new System.Drawing.Size(472, 44);
            this.txtSearch.TabIndex = 0;
            this.txtSearch.TextChanged += new System.EventHandler(this.FilterChanged);
            // 
            // cboRole
            // 
            this.cboRole.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cboRole.BackColor = System.Drawing.Color.Transparent;
            this.cboRole.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.cboRole.BorderRadius = 8;
            this.cboRole.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboRole.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboRole.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.cboRole.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.cboRole.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.cboRole.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboRole.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.cboRole.ItemHeight = 36;
            this.cboRole.Items.AddRange(new object[] {
            "Tất cả vai trò",
            "Admin",
            "Quản lý phòng máy"});
            this.cboRole.Location = new System.Drawing.Point(912, 11);
            this.cboRole.Name = "cboRole";
            this.cboRole.Size = new System.Drawing.Size(190, 42);
            this.cboRole.StartIndex = 0;
            this.cboRole.TabIndex = 1;
            this.cboRole.SelectedIndexChanged += new System.EventHandler(this.FilterChanged);
            // 
            // dgvAccounts
            // 
            this.dgvAccounts.AllowUserToAddRows = false;
            this.dgvAccounts.AllowUserToDeleteRows = false;
            this.dgvAccounts.AllowUserToResizeRows = false;
            this.dgvAccounts.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvAccounts.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvAccounts.BackgroundColor = System.Drawing.Color.White;
            this.dgvAccounts.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvAccounts.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvAccounts.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 9.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            dataGridViewCellStyle1.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvAccounts.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvAccounts.ColumnHeadersHeight = 48;
            this.dgvAccounts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            dataGridViewCellStyle2.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvAccounts.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvAccounts.EnableHeadersVisualStyles = false;
            this.dgvAccounts.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.dgvAccounts.Location = new System.Drawing.Point(0, 270);
            this.dgvAccounts.MultiSelect = false;
            this.dgvAccounts.Name = "dgvAccounts";
            this.dgvAccounts.ReadOnly = true;
            this.dgvAccounts.RowHeadersVisible = false;
            this.dgvAccounts.RowHeadersWidth = 51;
            this.dgvAccounts.RowTemplate.Height = 44;
            this.dgvAccounts.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvAccounts.Size = new System.Drawing.Size(1124, 484);
            this.dgvAccounts.TabIndex = 5;
            this.dgvAccounts.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgvAccounts_CellClick);
            this.dgvAccounts.CellMouseLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgvAccounts_CellMouseLeave);
            this.dgvAccounts.CellMouseMove += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.DgvAccounts_CellMouseMove);
            this.dgvAccounts.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.DgvAccounts_CellPainting);
            // 
            // pnlPaging
            // 
            this.pnlPaging.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlPaging.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.pnlPaging.BorderRadius = 8;
            this.pnlPaging.BorderThickness = 1;
            this.pnlPaging.Controls.Add(this.lblPagingInfo);
            this.pnlPaging.Controls.Add(this.pnlPageButtons);
            this.pnlPaging.FillColor = System.Drawing.Color.White;
            this.pnlPaging.Location = new System.Drawing.Point(0, 762);
            this.pnlPaging.Name = "pnlPaging";
            this.pnlPaging.Size = new System.Drawing.Size(1124, 46);
            this.pnlPaging.TabIndex = 6;
            // 
            // lblPagingInfo
            // 
            this.lblPagingInfo.BackColor = System.Drawing.Color.Transparent;
            this.lblPagingInfo.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPagingInfo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblPagingInfo.Location = new System.Drawing.Point(18, 13);
            this.lblPagingInfo.Name = "lblPagingInfo";
            this.lblPagingInfo.Size = new System.Drawing.Size(119, 22);
            this.lblPagingInfo.TabIndex = 0;
            this.lblPagingInfo.Text = "Đang tải dữ liệu...";
            // 
            // pnlPageButtons
            // 
            this.pnlPageButtons.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlPageButtons.BackColor = System.Drawing.Color.Transparent;
            this.pnlPageButtons.Location = new System.Drawing.Point(820, 7);
            this.pnlPageButtons.Name = "pnlPageButtons";
            this.pnlPageButtons.Size = new System.Drawing.Size(286, 34);
            this.pnlPageButtons.TabIndex = 1;
            this.pnlPageButtons.WrapContents = false;
            // 
            // frmQLTaiKhoan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(248)))), ((int)(((byte)(252)))));
            this.ClientSize = new System.Drawing.Size(1180, 860);
            this.Controls.Add(this.pnlRoot);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MinimumSize = new System.Drawing.Size(1080, 780);
            this.Name = "frmQLTaiKhoan";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Quản lý tài khoản";
            this.pnlRoot.ResumeLayout(false);
            this.pnlRoot.PerformLayout();
            this.pnlFilter.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAccounts)).EndInit();
            this.pnlPaging.ResumeLayout(false);
            this.pnlPaging.PerformLayout();
            this.ResumeLayout(false);

        }

        private void frmQLTaiKhoan_Load(object sender, EventArgs e)
        {
            RefreshAccounts(true);
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

                    RefreshAccounts(true);
                    MessageBox.Show(
                        "Đã thêm tài khoản thành công.",
                        "Thành công",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
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
                    return new FrmAddAccount.AccountValidationError(
                        "Tên đăng nhập đã tồn tại.",
                        FrmAddAccount.AccountField.Username);
                }
            }
            catch (Exception ex)
            {
                return new FrmAddAccount.AccountValidationError(
                    "Không thể kiểm tra tên đăng nhập: " + ex.Message,
                    FrmAddAccount.AccountField.Username);
            }

            try
            {
                if (this.userRepository.EmailExists(dialog.Email))
                {
                    return new FrmAddAccount.AccountValidationError(
                        "Email đã tồn tại.",
                        FrmAddAccount.AccountField.Email);
                }
            }
            catch (Exception ex)
            {
                return new FrmAddAccount.AccountValidationError(
                    "Không thể kiểm tra email: " + ex.Message,
                    FrmAddAccount.AccountField.Email);
            }

            return null;
        }
        private void BtnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void RefreshAccounts(bool resetPage)
        {
            LoadAccountDataFromDatabase();
            BuildStatCards();
            LoadAccounts(resetPage);
        }

        private void LoadAccountDataFromDatabase()
        {
            try
            {
                this.accounts = this.userRepository.GetAccountList()
                    .Select(ToAccountRow)
                    .ToList();
            }
            catch (Exception ex)
            {
                this.accounts = new List<AccountRow>();
                MessageBox.Show(
                    "Không thể tải dữ liệu tài khoản từ database QuanLyPhongMay.\n" + ex.Message,
                    "Lỗi tải dữ liệu",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void BuildStatCards()
        {
            int totalAccounts = this.accounts.Count;
            int adminAccounts = this.accounts.Count(item => item.RoleId == 1);
            int staffAccounts = this.accounts.Count(item => item.RoleId == 2);

            this.pnlStats.Controls.Clear();
            AddStatCard("Tổng tài khoản", totalAccounts.ToString(), Color.FromArgb(37, 99, 235), Color.FromArgb(239, 246, 255));
            AddStatCard("Admin", adminAccounts.ToString(), Color.FromArgb(124, 58, 237), Color.FromArgb(245, 243, 255));
            AddStatCard("Quản lý phòng máy", staffAccounts.ToString(), Color.FromArgb(22, 163, 74), Color.FromArgb(240, 253, 244));
        }

        private void BuildPagination(int totalPages)
        {
            this.pnlPageButtons.Controls.Clear();
            AddPageButton("<", this.currentPage > 1, this.currentPage - 1, false);

            int startPage = Math.Max(1, this.currentPage - 2);
            int endPage = Math.Min(totalPages, startPage + 4);
            startPage = Math.Max(1, endPage - 4);

            if (startPage > 1)
            {
                AddPageButton("1", true, 1, this.currentPage == 1);
                if (startPage > 2)
                {
                    AddPageButton("...", false, 0, false);
                }
            }

            for (int page = startPage; page <= endPage; page++)
            {
                AddPageButton(page.ToString(), true, page, page == this.currentPage);
            }

            if (endPage < totalPages)
            {
                if (endPage < totalPages - 1)
                {
                    AddPageButton("...", false, 0, false);
                }

                AddPageButton(totalPages.ToString(), true, totalPages, this.currentPage == totalPages);
            }

            AddPageButton(">", this.currentPage < totalPages, this.currentPage + 1, false);
            AlignPageButtonsRight();
        }

        private void AlignPageButtonsRight()
        {
            int totalWidth = 0;
            foreach (Control control in this.pnlPageButtons.Controls)
            {
                totalWidth += control.Width + control.Margin.Left + control.Margin.Right;
            }

            this.pnlPageButtons.Width = totalWidth;
            this.pnlPageButtons.Left = this.pnlPaging.Width - this.pnlPageButtons.Width - 18;
        }

        private void AddStatCard(string title, string value, Color accentColor, Color iconBackColor)
        {
            Guna2Panel card = new Guna2Panel();
            Label icon = new Label();
            Guna2HtmlLabel lblValue = new Guna2HtmlLabel();
            Guna2HtmlLabel lblTitle = new Guna2HtmlLabel();

            card.BorderColor = Color.FromArgb(226, 232, 240);
            card.BorderRadius = 8;
            card.BorderThickness = 1;
            card.FillColor = Color.White;
            card.Margin = new Padding(0, 0, 14, 0);
            card.Size = new Size(248, 78);

            icon.BackColor = iconBackColor;
            icon.Font = new Font("Segoe UI", 13F, FontStyle.Bold, GraphicsUnit.Point, 0);
            icon.ForeColor = accentColor;
            icon.Location = new Point(18, 18);
            icon.Size = new Size(38, 38);
            icon.Text = GetStatIcon(title);
            icon.TextAlign = ContentAlignment.MiddleCenter;
            icon.Paint += new PaintEventHandler(this.RoundLabel_Paint);

            lblValue.BackColor = Color.Transparent;
            lblValue.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblValue.ForeColor = Color.FromArgb(15, 23, 42);
            lblValue.Location = new Point(74, 12);
            lblValue.Text = value;

            lblTitle.BackColor = Color.Transparent;
            lblTitle.Font = new Font("Segoe UI", 9.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblTitle.ForeColor = Color.FromArgb(100, 116, 139);
            lblTitle.Location = new Point(76, 47);
            lblTitle.Text = title;

            card.Controls.Add(icon);
            card.Controls.Add(lblValue);
            card.Controls.Add(lblTitle);
            this.pnlStats.Controls.Add(card);
        }

        private void AddPageButton(string text, bool enabled, int targetPage, bool active)
        {
            Guna2Button button = new Guna2Button();
            button.BorderColor = active ? Color.FromArgb(37, 99, 235) : Color.FromArgb(226, 232, 240);
            button.BorderRadius = 6;
            button.BorderThickness = 1;
            button.Enabled = enabled;
            button.FillColor = active ? Color.FromArgb(37, 99, 235) : Color.White;
            button.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button.ForeColor = active ? Color.White : Color.FromArgb(71, 85, 105);
            button.HoverState.FillColor = active ? Color.FromArgb(29, 78, 216) : Color.FromArgb(248, 250, 252);
            button.Margin = new Padding(0, 0, 6, 0);
            button.Size = new Size(text.Length > 1 ? 42 : 32, 32);
            button.Text = text;
            button.Tag = targetPage;
            button.Click += new EventHandler(this.PageButton_Click);
            this.pnlPageButtons.Controls.Add(button);
        }

        private void PageButton_Click(object sender, EventArgs e)
        {
            Guna2Button button = (Guna2Button)sender;
            int page = Convert.ToInt32(button.Tag);
            if (page < 1 || page == this.currentPage)
            {
                return;
            }

            this.currentPage = page;
            LoadAccounts(false);
        }

        private static string GetStatIcon(string title)
        {
            if (title.StartsWith("Tổng", StringComparison.OrdinalIgnoreCase))
            {
                return "Σ";
            }

            return title.StartsWith("Admin", StringComparison.OrdinalIgnoreCase) ? "A" : "Q";
        }

        private void LoadAccounts(bool resetPage)
        {
            if (resetPage)
            {
                this.currentPage = 1;
            }

            List<AccountRow> filteredRows = GetFilteredRows().ToList();
            int totalFiltered = filteredRows.Count;
            int totalPages = Math.Max(1, (int)Math.Ceiling(totalFiltered / (double)PageSize));
            this.currentPage = Math.Max(1, Math.Min(this.currentPage, totalPages));
            int skip = (this.currentPage - 1) * PageSize;
            List<AccountRow> rows = filteredRows
                .Skip(skip)
                .Take(PageSize)
                .Select((item, index) => item.CloneWithStt(skip + index + 1))
                .ToList();

            this.dgvAccounts.DataSource = rows;
            ConfigureGridColumns();
            BuildPagination(totalPages);
            this.lblPagingInfo.Text = totalFiltered == 0
                ? "Không có dữ liệu phù hợp"
                : string.Format("Hiển thị {0}-{1} trong {2} kết quả", skip + 1, skip + rows.Count, totalFiltered);
        }

        private IEnumerable<AccountRow> GetFilteredRows()
        {
            IEnumerable<AccountRow> source = this.accounts;
            string keyword = this.txtSearch.Text.Trim().ToLowerInvariant();

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                source = source.Where(item =>
                    ContainsKeyword(item.FullName, keyword) ||
                    ContainsKeyword(item.Username, keyword) ||
                    ContainsKeyword(item.Email, keyword));
            }

            if (this.cboRole.SelectedIndex > 0)
            {
                string selectedRole = this.cboRole.SelectedItem.ToString();
                source = source.Where(item => string.Equals(item.Role, selectedRole, StringComparison.OrdinalIgnoreCase));
            }

            return source;
        }

        private static AccountRow ToAccountRow(AccountListItem item)
        {
            string role = string.IsNullOrWhiteSpace(item.TenVaiTro) ? GetRoleName(item.MaVaiTro) : item.TenVaiTro;
            string fullName = string.IsNullOrWhiteSpace(item.HoTen) ? item.TenDangNhap : item.HoTen;

            return new AccountRow
            {
                FullName = fullName ?? string.Empty,
                Username = item.TenDangNhap ?? string.Empty,
                Email = item.Email ?? string.Empty,
                RoleId = item.MaVaiTro,
                Role = role ?? string.Empty,
                Initials = GetInitials(fullName),
                AvatarColor = GetAvatarColor(item.TenDangNhap)
            };
        }

        private static bool ContainsKeyword(string value, string keyword)
        {
            return !string.IsNullOrEmpty(value) && value.ToLowerInvariant().Contains(keyword);
        }

        private static string GetRoleName(int roleId)
        {
            return roleId == 1 ? "Admin" : "Quản lý phòng máy";
        }

        private static string GetInitials(string fullName)
        {
            if (string.IsNullOrWhiteSpace(fullName))
            {
                return "?";
            }

            string[] parts = fullName
                .Trim()
                .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            if (parts.Length == 1)
            {
                return parts[0].Substring(0, 1).ToUpperInvariant();
            }

            return (parts[0].Substring(0, 1) + parts[parts.Length - 1].Substring(0, 1)).ToUpperInvariant();
        }

        private static Color GetAvatarColor(string seed)
        {
            Color[] colors =
            {
                Color.FromArgb(37, 99, 235),
                Color.FromArgb(22, 163, 74),
                Color.FromArgb(14, 165, 233),
                Color.FromArgb(124, 58, 237),
                Color.FromArgb(245, 158, 11),
                Color.FromArgb(20, 184, 166),
                Color.FromArgb(239, 68, 68),
                Color.FromArgb(79, 70, 229)
            };

            if (string.IsNullOrEmpty(seed))
            {
                return colors[0];
            }

            int index = (int)(Math.Abs((long)seed.GetHashCode()) % colors.Length);
            return colors[index];
        }

        private void ConfigureGridColumns()
        {
            if (this.dgvAccounts.Columns.Count == 0)
            {
                return;
            }

            this.dgvAccounts.Columns[nameof(AccountRow.Stt)].HeaderText = "STT";
            this.dgvAccounts.Columns[nameof(AccountRow.FullName)].HeaderText = "Avatar + Họ tên";
            this.dgvAccounts.Columns[nameof(AccountRow.Username)].HeaderText = "Tên đăng nhập";
            this.dgvAccounts.Columns[nameof(AccountRow.Email)].HeaderText = "Email";
            this.dgvAccounts.Columns[nameof(AccountRow.Role)].HeaderText = "Vai trò";

            this.dgvAccounts.Columns[nameof(AccountRow.Initials)].Visible = false;
            this.dgvAccounts.Columns[nameof(AccountRow.AvatarColor)].Visible = false;
            this.dgvAccounts.Columns[nameof(AccountRow.RoleId)].Visible = false;

            if (!this.dgvAccounts.Columns.Contains("Actions"))
            {
                DataGridViewTextBoxColumn actionColumn = new DataGridViewTextBoxColumn();
                actionColumn.Name = "Actions";
                actionColumn.HeaderText = "Hành động";
                actionColumn.ReadOnly = true;
                this.dgvAccounts.Columns.Add(actionColumn);
            }

            this.dgvAccounts.Columns[nameof(AccountRow.Stt)].FillWeight = 48;
            this.dgvAccounts.Columns[nameof(AccountRow.FullName)].FillWeight = 220;
            this.dgvAccounts.Columns[nameof(AccountRow.Username)].FillWeight = 140;
            this.dgvAccounts.Columns[nameof(AccountRow.Email)].FillWeight = 220;
            this.dgvAccounts.Columns[nameof(AccountRow.Role)].FillWeight = 125;
            this.dgvAccounts.Columns["Actions"].FillWeight = 116;

            this.dgvAccounts.Columns[nameof(AccountRow.Stt)].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dgvAccounts.Columns["Actions"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }

        private void FilterChanged(object sender, EventArgs e)
        {
            LoadAccounts(true);
        }

        private void DgvAccounts_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            string columnName = this.dgvAccounts.Columns[e.ColumnIndex].Name;

            if (columnName == nameof(AccountRow.FullName))
            {
                PaintAvatarNameCell(e);
                return;
            }

            if (columnName == nameof(AccountRow.Role))
            {
                bool isAdmin = Convert.ToString(e.Value) == "Admin";
                PaintBadgeCell(e, Convert.ToString(e.Value), isAdmin ? Color.FromArgb(37, 99, 235) : Color.FromArgb(22, 163, 74), isAdmin ? Color.FromArgb(239, 246, 255) : Color.FromArgb(240, 253, 244));
                return;
            }

            if (columnName == "Actions")
            {
                PaintActionsCell(e);
            }
        }

        private void PaintAvatarNameCell(DataGridViewCellPaintingEventArgs e)
        {
            e.Handled = true;
            e.PaintBackground(e.CellBounds, true);

            AccountRow row = (AccountRow)this.dgvAccounts.Rows[e.RowIndex].DataBoundItem;
            Rectangle avatarBounds = new Rectangle(e.CellBounds.Left + 18, e.CellBounds.Top + 7, 30, 30);
            Rectangle textBounds = new Rectangle(e.CellBounds.Left + 60, e.CellBounds.Top, e.CellBounds.Width - 68, e.CellBounds.Height);

            using (SolidBrush avatarBrush = new SolidBrush(row.AvatarColor))
            using (SolidBrush whiteBrush = new SolidBrush(Color.White))
            using (SolidBrush textBrush = new SolidBrush(Color.FromArgb(15, 23, 42)))
            using (GraphicsPath path = new GraphicsPath())
            using (Font avatarFont = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0))
            using (StringFormat centerFormat = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center })
            using (StringFormat textFormat = new StringFormat { Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Center, Trimming = StringTrimming.EllipsisCharacter })
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                path.AddEllipse(avatarBounds);
                e.Graphics.FillPath(avatarBrush, path);
                e.Graphics.DrawString(row.Initials, avatarFont, whiteBrush, avatarBounds, centerFormat);
                e.Graphics.DrawString(row.FullName, this.dgvAccounts.DefaultCellStyle.Font, textBrush, textBounds, textFormat);
            }
        }

        private void PaintBadgeCell(DataGridViewCellPaintingEventArgs e, string text, Color foreColor, Color backColor)
        {
            e.Handled = true;
            e.PaintBackground(e.CellBounds, true);

            Size textSize = TextRenderer.MeasureText(text, this.dgvAccounts.DefaultCellStyle.Font);
            Rectangle badgeBounds = new Rectangle(e.CellBounds.Left + 16, e.CellBounds.Top + 8, textSize.Width + 26, 28);

            using (GraphicsPath path = CreateRoundRectanglePath(badgeBounds, 14))
            using (SolidBrush backBrush = new SolidBrush(backColor))
            using (SolidBrush textBrush = new SolidBrush(foreColor))
            using (StringFormat format = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center })
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                e.Graphics.FillPath(backBrush, path);
                e.Graphics.DrawString(text, this.dgvAccounts.DefaultCellStyle.Font, textBrush, badgeBounds, format);
            }
        }

        private void PaintActionsCell(DataGridViewCellPaintingEventArgs e)
        {
            e.Handled = true;
            e.PaintBackground(e.CellBounds, true);

            string[] icons = { "\uE890", "\uE70F", "\uE74D" };
            Color[] colors =
            {
                Color.FromArgb(71, 85, 105),
                Color.FromArgb(37, 99, 235),
                Color.FromArgb(220, 38, 38)
            };

            int iconSize = 30;
            int totalWidth = (iconSize * icons.Length) + 12;
            int startX = e.CellBounds.Left + (e.CellBounds.Width - totalWidth) / 2;
            int y = e.CellBounds.Top + (e.CellBounds.Height - iconSize) / 2;

            using (Font iconFont = new Font("Segoe MDL2 Assets", 10F, FontStyle.Regular, GraphicsUnit.Point, 0))
            using (StringFormat format = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center })
            {
                for (int i = 0; i < icons.Length; i++)
                {
                    Rectangle iconBounds = new Rectangle(startX + (i * (iconSize + 6)), y, iconSize, iconSize);

                    using (GraphicsPath path = CreateRoundRectanglePath(iconBounds, 6))
                    using (SolidBrush backBrush = new SolidBrush(Color.FromArgb(248, 250, 252)))
                    using (Pen borderPen = new Pen(Color.FromArgb(226, 232, 240)))
                    using (SolidBrush iconBrush = new SolidBrush(colors[i]))
                    {
                        e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                        e.Graphics.FillPath(backBrush, path);
                        e.Graphics.DrawPath(borderPen, path);
                        e.Graphics.DrawString(icons[i], iconFont, iconBrush, iconBounds, format);
                    }
                }
            }
        }

        private void DgvAccounts_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0 || this.dgvAccounts.Columns[e.ColumnIndex].Name != "Actions")
            {
                return;
            }

            AccountRow row = this.dgvAccounts.Rows[e.RowIndex].DataBoundItem as AccountRow;
            if (row == null)
            {
                return;
            }

            Rectangle cellBounds = this.dgvAccounts.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false);
            int mouseX = this.dgvAccounts.PointToClient(Cursor.Position).X - cellBounds.Left;
            string action = GetActionFromMouseX(cellBounds.Width, mouseX);

            if (action == "View")
            {
                ViewAccount(row.Username);
            }
            else if (action == "Edit")
            {
                EditAccount(row.Username);
            }
            else if (action == "Delete")
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
                        ? string.Empty
                        : PasswordHasher.HashPassword(dialog.NewPassword);
                    this.userRepository.UpdateAccount(dialog.Username, dialog.FullName, dialog.Email, dialog.RoleId, passwordHash);
                    RefreshAccounts(false);
                    MessageBox.Show(
                        "Đã cập nhật tài khoản thành công.",
                        "Thành công",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    ShowDataError("Không thể cập nhật tài khoản.", ex);
                }
            }
        }

        private void DeleteAccount(string username)
        {
            if (!ConfirmDeleteAccount(this, username))
            {
                return;
            }

            try
            {
                this.userRepository.DeleteAccount(username);
                RefreshAccounts(false);
                MessageBox.Show(
                    "Đã xóa tài khoản thành công.",
                    "Thành công",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                ShowDataError("Không thể xóa tài khoản. Tài khoản có thể đang được tham chiếu bởi dữ liệu lịch thực hành.", ex);
            }
        }

        private static bool ConfirmDeleteAccount(IWin32Window owner, string username)
        {
            using (Form dialog = new Form())
            using (PictureBox icon = new PictureBox())
            using (Label message = new Label())
            using (Button btnYes = new Button())
            using (Button btnNo = new Button())
            {
                dialog.Text = "Xác nhận xóa";
                dialog.StartPosition = FormStartPosition.CenterParent;
                dialog.FormBorderStyle = FormBorderStyle.FixedDialog;
                dialog.MaximizeBox = false;
                dialog.MinimizeBox = false;
                dialog.ClientSize = new Size(360, 150);
                dialog.ShowInTaskbar = false;

                icon.Image = SystemIcons.Warning.ToBitmap();
                icon.Location = new Point(24, 42);
                icon.Size = new Size(32, 32);
                icon.SizeMode = PictureBoxSizeMode.StretchImage;

                message.AutoSize = false;
                message.Location = new Point(72, 32);
                message.Size = new Size(260, 54);
                message.Text = "Bạn có chắc muốn xóa tài khoản '" + username + "'?";
                message.TextAlign = ContentAlignment.MiddleLeft;

                btnYes.DialogResult = DialogResult.Yes;
                btnYes.Location = new Point(152, 104);
                btnYes.Size = new Size(86, 28);
                btnYes.Text = "Có";

                btnNo.DialogResult = DialogResult.No;
                btnNo.Location = new Point(252, 104);
                btnNo.Size = new Size(86, 28);
                btnNo.Text = "Không";

                dialog.Controls.Add(icon);
                dialog.Controls.Add(message);
                dialog.Controls.Add(btnYes);
                dialog.Controls.Add(btnNo);
                dialog.AcceptButton = btnYes;
                dialog.CancelButton = btnNo;

                return dialog.ShowDialog(owner) == DialogResult.Yes;
            }
        }

        private AccountListItem GetAccount(string username)
        {
            try
            {
                AccountListItem account = this.userRepository.GetAccountByUsername(username);
                if (account == null)
                {
                    MessageBox.Show(
                        "Không tìm thấy tài khoản.",
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }

                return account;
            }
            catch (Exception ex)
            {
                ShowDataError("Không thể tải chi tiết tài khoản.", ex);
                return null;
            }
        }

        private void DgvAccounts_CellMouseMove(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0 || this.dgvAccounts.Columns[e.ColumnIndex].Name != "Actions")
            {
                this.dgvAccounts.Cursor = Cursors.Default;
                this.actionToolTip.SetToolTip(this.dgvAccounts, string.Empty);
                return;
            }

            string action = GetActionFromMouseX(this.dgvAccounts.Columns[e.ColumnIndex].Width, e.X);
            string tip = action == "View"
                ? "Xem chi tiết"
                : action == "Edit"
                    ? "Sửa tài khoản"
                    : action == "Delete"
                        ? "Xóa tài khoản"
                        : string.Empty;
            if (!string.IsNullOrEmpty(tip))
            {
                this.dgvAccounts.Cursor = Cursors.Hand;
                this.actionToolTip.SetToolTip(this.dgvAccounts, tip);
            }
            else
            {
                this.dgvAccounts.Cursor = Cursors.Default;
                this.actionToolTip.SetToolTip(this.dgvAccounts, string.Empty);
            }
        }

        private static string GetActionFromMouseX(int cellWidth, int x)
        {
            int iconSize = 30;
            int totalWidth = (iconSize * 3) + 12;
            int startX = (cellWidth - totalWidth) / 2;

            if (x >= startX && x <= startX + iconSize)
            {
                return "View";
            }

            if (x >= startX + iconSize + 6 && x <= startX + (iconSize * 2) + 6)
            {
                return "Edit";
            }

            if (x >= startX + (iconSize * 2) + 12 && x <= startX + (iconSize * 3) + 12)
            {
                return "Delete";
            }

            return string.Empty;
        }

        private void DgvAccounts_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            this.dgvAccounts.Cursor = Cursors.Default;
            this.actionToolTip.SetToolTip(this.dgvAccounts, string.Empty);
        }

        private void RoundLabel_Paint(object sender, PaintEventArgs e)
        {
            Label label = (Label)sender;
            using (GraphicsPath path = CreateRoundRectanglePath(label.ClientRectangle, 12))
            {
                label.Region = new Region(path);
            }
        }

        private static GraphicsPath CreateRoundRectanglePath(Rectangle bounds, int radius)
        {
            int diameter = radius * 2;
            GraphicsPath path = new GraphicsPath();
            path.AddArc(bounds.Left, bounds.Top, diameter, diameter, 180, 90);
            path.AddArc(bounds.Right - diameter, bounds.Top, diameter, diameter, 270, 90);
            path.AddArc(bounds.Right - diameter, bounds.Bottom - diameter, diameter, diameter, 0, 90);
            path.AddArc(bounds.Left, bounds.Bottom - diameter, diameter, diameter, 90, 90);
            path.CloseFigure();
            return path;
        }

        private class AccountRow
        {
            public int Stt { get; set; }

            public string FullName { get; set; }

            public string Username { get; set; }

            public string Email { get; set; }

            public int RoleId { get; set; }

            public string Role { get; set; }

            public string Initials { get; set; }

            public Color AvatarColor { get; set; }

            public AccountRow CloneWithStt(int stt)
            {
                return new AccountRow
                {
                    Stt = stt,
                    FullName = this.FullName,
                    Username = this.Username,
                    Email = this.Email,
                    RoleId = this.RoleId,
                    Role = this.Role,
                    Initials = this.Initials,
                    AvatarColor = this.AvatarColor
                };
            }
        }

        private static void ShowDataError(string message, Exception ex)
        {
            MessageBox.Show(
                message + "\n" + ex.Message,
                "Lỗi dữ liệu",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
    }
}
