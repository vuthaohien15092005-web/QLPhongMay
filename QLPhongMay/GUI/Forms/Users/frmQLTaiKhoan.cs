using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
using Guna.UI2.WinForms;
using QLPhongMay.DAL;
using QLPhongMay.DTO;

namespace QLPhongMay.GUI.Forms.Users
{
    public partial class frmQLTaiKhoan : Form
    {
        private readonly UserRepository userRepository;
        private readonly ToolTip actionToolTip;
        private List<AccountRow> accounts;

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
            BuildPagination();
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
            this.btnBack.Location = new System.Drawing.Point(0, 9);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(128, 46);
            this.btnBack.TabIndex = 0;
            this.btnBack.Text = "<  Quay lại";
            this.btnBack.Click += new System.EventHandler(this.BtnBack_Click);
            // 
            // lblTitle
            // 
            this.lblTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
            this.lblSubtitle.Location = new System.Drawing.Point(150, 55);
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
            this.btnAdd.Location = new System.Drawing.Point(930, 9);
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
            this.pnlStats.Location = new System.Drawing.Point(0, 104);
            this.pnlStats.Name = "pnlStats";
            this.pnlStats.Size = new System.Drawing.Size(1124, 118);
            this.pnlStats.TabIndex = 3;
            this.pnlStats.WrapContents = false;
            this.pnlStats.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlStats_Paint);
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
            this.pnlFilter.Location = new System.Drawing.Point(0, 246);
            this.pnlFilter.Name = "pnlFilter";
            this.pnlFilter.Size = new System.Drawing.Size(1124, 78);
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
            this.txtSearch.Location = new System.Drawing.Point(22, 17);
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
            "NV phòng máy"});
            this.cboRole.Location = new System.Drawing.Point(912, 17);
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
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvAccounts.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvAccounts.ColumnHeadersHeight = 48;
            this.dgvAccounts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            dataGridViewCellStyle2.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(246)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvAccounts.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvAccounts.EnableHeadersVisualStyles = false;
            this.dgvAccounts.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.dgvAccounts.Location = new System.Drawing.Point(0, 346);
            this.dgvAccounts.MultiSelect = false;
            this.dgvAccounts.Name = "dgvAccounts";
            this.dgvAccounts.ReadOnly = true;
            this.dgvAccounts.RowHeadersVisible = false;
            this.dgvAccounts.RowHeadersWidth = 51;
            this.dgvAccounts.RowTemplate.Height = 44;
            this.dgvAccounts.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvAccounts.Size = new System.Drawing.Size(1124, 408);
            this.dgvAccounts.TabIndex = 5;
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
            LoadAccountDataFromDatabase();
            BuildStatCards();
            LoadAccounts();
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            using (FrmAddAccount dialog = new FrmAddAccount())
            {
                dialog.ShowDialog(this);
            }
        }
        private void BtnBack_Click(object sender, EventArgs e)
        {
            this.Close();
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
            int adminAccounts = this.accounts.Count(item => IsAdminRole(item.Role));

            this.pnlStats.Controls.Clear();
            AddStatCard("Tổng tài khoản", totalAccounts.ToString(), Color.FromArgb(37, 99, 235), Color.FromArgb(239, 246, 255));
            AddStatCard("Admin", adminAccounts.ToString(), Color.FromArgb(124, 58, 237), Color.FromArgb(245, 243, 255));
        }

        private void BuildPagination()
        {
            this.pnlPageButtons.Controls.Clear();
            AddPageButton("<", false);
            AddPageButton("1", true);
            AddPageButton("2", false);
            AddPageButton("3", false);
            AddPageButton("...", false);
            AddPageButton("16", false);
            AddPageButton(">", false);
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
            card.Margin = new Padding(0, 0, 18, 0);
            card.Size = new Size(263, 106);

            icon.BackColor = iconBackColor;
            icon.Font = new Font("Segoe UI", 13F, FontStyle.Bold, GraphicsUnit.Point, 0);
            icon.ForeColor = accentColor;
            icon.Location = new Point(20, 22);
            icon.Size = new Size(42, 42);
            icon.Text = GetStatIcon(title);
            icon.TextAlign = ContentAlignment.MiddleCenter;
            icon.Paint += new PaintEventHandler(this.RoundLabel_Paint);

            lblValue.BackColor = Color.Transparent;
            lblValue.Font = new Font("Segoe UI", 22F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblValue.ForeColor = Color.FromArgb(15, 23, 42);
            lblValue.Location = new Point(82, 18);
            lblValue.Text = value;

            lblTitle.BackColor = Color.Transparent;
            lblTitle.Font = new Font("Segoe UI", 9.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblTitle.ForeColor = Color.FromArgb(100, 116, 139);
            lblTitle.Location = new Point(84, 62);
            lblTitle.Text = title;

            card.Controls.Add(icon);
            card.Controls.Add(lblValue);
            card.Controls.Add(lblTitle);
            this.pnlStats.Controls.Add(card);
        }

        private void AddPageButton(string text, bool active)
        {
            Guna2Button button = new Guna2Button();
            button.BorderColor = active ? Color.FromArgb(37, 99, 235) : Color.FromArgb(226, 232, 240);
            button.BorderRadius = 6;
            button.BorderThickness = 1;
            button.FillColor = active ? Color.FromArgb(37, 99, 235) : Color.White;
            button.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button.ForeColor = active ? Color.White : Color.FromArgb(71, 85, 105);
            button.HoverState.FillColor = active ? Color.FromArgb(29, 78, 216) : Color.FromArgb(248, 250, 252);
            button.Margin = new Padding(0, 0, 6, 0);
            button.Size = new Size(text.Length > 1 ? 42 : 32, 32);
            button.Text = text;
            this.pnlPageButtons.Controls.Add(button);
        }

        private static string GetStatIcon(string title)
        {
            if (title.StartsWith("Tổng", StringComparison.OrdinalIgnoreCase))
            {
                return "Î£";
            }

            return "A";
        }

        private void LoadAccounts()
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

            List<AccountRow> rows = source.Select((item, index) => new AccountRow
            {
                Stt = index + 1,
                FullName = item.FullName,
                Username = item.Username,
                Email = item.Email,
                Role = item.Role,
                Initials = item.Initials,
                AvatarColor = item.AvatarColor
            }).ToList();

            this.dgvAccounts.DataSource = rows;
            ConfigureGridColumns();
            this.lblPagingInfo.Text = rows.Count == 0
                ? "Không có dữ liệu phù hợp"
                : string.Format("Hiển thị 1-{0} trong {1}", rows.Count, this.accounts.Count);
        }

        private static AccountRow ToAccountRow(AccountListItem item)
        {
            string role = string.IsNullOrWhiteSpace(item.TenVaiTro) ? item.MaVaiTro.ToString() : item.TenVaiTro;
            string fullName = string.IsNullOrWhiteSpace(item.HoTen) ? item.TenDangNhap : item.HoTen;

            return new AccountRow
            {
                FullName = fullName ?? string.Empty,
                Username = item.TenDangNhap ?? string.Empty,
                Email = item.Email ?? string.Empty,
                Role = role ?? string.Empty,
                Initials = GetInitials(fullName),
                AvatarColor = GetAvatarColor(item.TenDangNhap)
            };
        }

        private static bool ContainsKeyword(string value, string keyword)
        {
            return !string.IsNullOrEmpty(value) && value.ToLowerInvariant().Contains(keyword);
        }

        private static bool IsAdminRole(string role)
        {
            return string.Equals(role, "Admin", StringComparison.OrdinalIgnoreCase);
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
            LoadAccounts();
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

            string[] icons = { "\uE890", "\uE70F" };
            Color[] colors =
            {
                Color.FromArgb(71, 85, 105),
                Color.FromArgb(37, 99, 235)
            };

            int iconSize = 30;
            int totalWidth = (iconSize * icons.Length) + 6;
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

        private void DgvAccounts_CellMouseMove(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0 || this.dgvAccounts.Columns[e.ColumnIndex].Name != "Actions")
            {
                return;
            }

            int iconSize = 30;
            int totalWidth = (iconSize * 2) + 6;
            int startX = (this.dgvAccounts.Columns[e.ColumnIndex].Width - totalWidth) / 2;
            string tip = string.Empty;

            if (e.X >= startX && e.X <= startX + iconSize)
            {
                tip = "Xem chi tiết";
            }
            else if (e.X >= startX + iconSize + 6 && e.X <= startX + (iconSize * 2) + 6)
            {
                tip = "Sửa tài khoản";
            }
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

            public string Role { get; set; }

            public string Initials { get; set; }

            public Color AvatarColor { get; set; }
        }

        private void pnlStats_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
