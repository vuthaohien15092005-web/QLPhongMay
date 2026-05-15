using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using QLPhongMay.DAL;
using QLPhongMay.Models;

namespace QLPhongMay.GUI.Forms.Catalog
{
    public partial class frmQLCaHoc : Form
    {
        private const int PageSize = 5;
        private readonly Color primaryColor = Color.FromArgb(37, 99, 235);
        private readonly Color borderColor = Color.FromArgb(226, 232, 240);
        private readonly Color pageBackColor = Color.FromArgb(245, 247, 251);
        private readonly Color inputBackColor = Color.FromArgb(248, 250, 252);
        private readonly ToolTip actionToolTip = new ToolTip();
        private List<CaHocRow> caHocs = new List<CaHocRow>();
        private int currentPage = 1;

        private Panel pnlRoot;
        private FlowLayoutPanel pnlStats;
        private TextBox txtMaCa;
        private TextBox txtTenCa;
        private TextBox txtGioBatDau;
        private TextBox txtGioKetThuc;
        private Button btnCreate;
        private Button btnUpdate;
        private Button btnDelete;
        private Button btnClear;
        private TextBox txtSearch;
        private ComboBox cboDuration;
        private DataGridView dgvCaHoc;
        private Label lblPagingInfo;
        private FlowLayoutPanel pnlPageButtons;

        public frmQLCaHoc()
        {
            InitializeComponent();
            Load += frmQLCaHoc_Load;
        }

        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pnlRoot = new System.Windows.Forms.Panel();
            this.btnBack = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblSubtitle = new System.Windows.Forms.Label();
            this.pnlStats = new System.Windows.Forms.FlowLayoutPanel();
            this.btnCreate = new System.Windows.Forms.Button();
            this.pnlEditor = new System.Windows.Forms.Panel();
            this.txtMaCa = new System.Windows.Forms.TextBox();
            this.txtTenCa = new System.Windows.Forms.TextBox();
            this.txtGioBatDau = new System.Windows.Forms.TextBox();
            this.txtGioKetThuc = new System.Windows.Forms.TextBox();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.pnlFilter = new System.Windows.Forms.Panel();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.cboDuration = new System.Windows.Forms.ComboBox();
            this.dgvCaHoc = new System.Windows.Forms.DataGridView();
            System.Windows.Forms.DataGridViewTextBoxColumn colMaCa = new System.Windows.Forms.DataGridViewTextBoxColumn();
            System.Windows.Forms.DataGridViewTextBoxColumn colTenCa = new System.Windows.Forms.DataGridViewTextBoxColumn();
            System.Windows.Forms.DataGridViewTextBoxColumn colGioBatDau = new System.Windows.Forms.DataGridViewTextBoxColumn();
            System.Windows.Forms.DataGridViewTextBoxColumn colGioKetThuc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Actions = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlPaging = new System.Windows.Forms.Panel();
            this.lblPagingInfo = new System.Windows.Forms.Label();
            this.pnlPageButtons = new System.Windows.Forms.FlowLayoutPanel();
            this.ViewAction = new System.Windows.Forms.DataGridViewButtonColumn();
            this.EditAction = new System.Windows.Forms.DataGridViewButtonColumn();
            this.DeleteAction = new System.Windows.Forms.DataGridViewButtonColumn();
            this.pnlRoot.SuspendLayout();
            this.pnlEditor.SuspendLayout();
            this.pnlFilter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCaHoc)).BeginInit();
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
            this.pnlRoot.Controls.Add(this.pnlStats);
            this.pnlRoot.Controls.Add(this.btnCreate);
            this.pnlRoot.Controls.Add(this.dgvCaHoc);
            this.pnlRoot.Controls.Add(this.pnlPaging);
            this.pnlRoot.Location = new System.Drawing.Point(28, 24);
            this.pnlRoot.Name = "pnlRoot";
            this.pnlRoot.Size = new System.Drawing.Size(984, 625);
            this.pnlRoot.TabIndex = 0;
            // 
            // btnBack
            // 
            this.btnBack.BackColor = System.Drawing.Color.White;
            this.btnBack.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBack.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.btnBack.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBack.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBack.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.btnBack.Location = new System.Drawing.Point(3, 3);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(128, 46);
            this.btnBack.TabIndex = 0;
            this.btnBack.Text = "<  Quay lại";
            this.btnBack.UseVisualStyleBackColor = false;
            this.btnBack.Click += new System.EventHandler(this.BtnBack_Click);
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 21F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblTitle.Location = new System.Drawing.Point(146, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(262, 47);
            this.lblTitle.TabIndex = 1;
            this.lblTitle.Text = "Quản lý ca học";
            // 
            // lblSubtitle
            // 
            this.lblSubtitle.AutoSize = true;
            this.lblSubtitle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblSubtitle.Location = new System.Drawing.Point(150, 48);
            this.lblSubtitle.Name = "lblSubtitle";
            this.lblSubtitle.Size = new System.Drawing.Size(438, 23);
            this.lblSubtitle.TabIndex = 2;
            this.lblSubtitle.Text = "Theo dõi và cập nhật các khung giờ học trong hệ thống";
            // 
            // pnlStats
            // 
            this.pnlStats.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlStats.BackColor = System.Drawing.Color.Transparent;
            this.pnlStats.Location = new System.Drawing.Point(0, 82);
            this.pnlStats.Name = "pnlStats";
            this.pnlStats.Size = new System.Drawing.Size(710, 88);
            this.pnlStats.TabIndex = 3;
            this.pnlStats.WrapContents = false;
            // 
            // btnCreate
            // 
            this.btnCreate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.btnCreate.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCreate.FlatAppearance.BorderSize = 0;
            this.btnCreate.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(78)))), ((int)(((byte)(216)))));
            this.btnCreate.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(78)))), ((int)(((byte)(216)))));
            this.btnCreate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCreate.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCreate.ForeColor = System.Drawing.Color.White;
            this.btnCreate.Location = new System.Drawing.Point(790, 92);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(194, 46);
            this.btnCreate.TabIndex = 4;
            this.btnCreate.Text = "+  Thêm ca học";
            this.btnCreate.UseVisualStyleBackColor = false;
            this.btnCreate.Click += new System.EventHandler(this.BtnCreate_Click);
            // 
            // pnlEditor
            // 
            this.pnlEditor.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlEditor.BackColor = System.Drawing.Color.White;
            this.pnlEditor.Controls.Add(this.txtMaCa);
            this.pnlEditor.Controls.Add(this.txtTenCa);
            this.pnlEditor.Controls.Add(this.txtGioBatDau);
            this.pnlEditor.Controls.Add(this.txtGioKetThuc);
            this.pnlEditor.Controls.Add(this.btnUpdate);
            this.pnlEditor.Controls.Add(this.btnDelete);
            this.pnlEditor.Controls.Add(this.btnClear);
            this.pnlEditor.Location = new System.Drawing.Point(0, 188);
            this.pnlEditor.Name = "pnlEditor";
            this.pnlEditor.Size = new System.Drawing.Size(984, 110);
            this.pnlEditor.TabIndex = 4;
            this.pnlEditor.Visible = false;
            this.pnlEditor.Paint += new System.Windows.Forms.PaintEventHandler(this.PanelBorder_Paint);
            // 
            // txtMaCa
            // 
            this.txtMaCa.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.txtMaCa.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtMaCa.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMaCa.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.txtMaCa.Location = new System.Drawing.Point(22, 16);
            this.txtMaCa.Name = "txtMaCa";
            this.txtMaCa.Size = new System.Drawing.Size(120, 30);
            this.txtMaCa.TabIndex = 0;
            this.txtMaCa.Tag = "Mã ca";
            this.txtMaCa.Text = "Mã ca";
            this.txtMaCa.GotFocus += new System.EventHandler(this.Placeholder_GotFocus);
            this.txtMaCa.LostFocus += new System.EventHandler(this.Placeholder_LostFocus);
            // 
            // txtTenCa
            // 
            this.txtTenCa.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.txtTenCa.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTenCa.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTenCa.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.txtTenCa.Location = new System.Drawing.Point(156, 16);
            this.txtTenCa.Name = "txtTenCa";
            this.txtTenCa.Size = new System.Drawing.Size(240, 30);
            this.txtTenCa.TabIndex = 1;
            this.txtTenCa.Tag = "Tên ca";
            this.txtTenCa.Text = "Tên ca";
            this.txtTenCa.GotFocus += new System.EventHandler(this.Placeholder_GotFocus);
            this.txtTenCa.LostFocus += new System.EventHandler(this.Placeholder_LostFocus);
            // 
            // txtGioBatDau
            // 
            this.txtGioBatDau.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.txtGioBatDau.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtGioBatDau.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtGioBatDau.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.txtGioBatDau.Location = new System.Drawing.Point(410, 16);
            this.txtGioBatDau.Name = "txtGioBatDau";
            this.txtGioBatDau.Size = new System.Drawing.Size(170, 30);
            this.txtGioBatDau.TabIndex = 2;
            this.txtGioBatDau.Tag = "Giờ bắt đầu HH:mm";
            this.txtGioBatDau.Text = "Giờ bắt đầu HH:mm";
            this.txtGioBatDau.GotFocus += new System.EventHandler(this.Placeholder_GotFocus);
            this.txtGioBatDau.LostFocus += new System.EventHandler(this.Placeholder_LostFocus);
            // 
            // txtGioKetThuc
            // 
            this.txtGioKetThuc.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.txtGioKetThuc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtGioKetThuc.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtGioKetThuc.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.txtGioKetThuc.Location = new System.Drawing.Point(594, 16);
            this.txtGioKetThuc.Name = "txtGioKetThuc";
            this.txtGioKetThuc.Size = new System.Drawing.Size(170, 30);
            this.txtGioKetThuc.TabIndex = 3;
            this.txtGioKetThuc.Tag = "Giờ kết thúc HH:mm";
            this.txtGioKetThuc.Text = "Giờ kết thúc HH:mm";
            this.txtGioKetThuc.GotFocus += new System.EventHandler(this.Placeholder_GotFocus);
            this.txtGioKetThuc.LostFocus += new System.EventHandler(this.Placeholder_LostFocus);
            // 
            // btnUpdate
            // 
            this.btnUpdate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(165)))), ((int)(((byte)(233)))));
            this.btnUpdate.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUpdate.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUpdate.ForeColor = System.Drawing.Color.White;
            this.btnUpdate.Location = new System.Drawing.Point(144, 64);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(96, 34);
            this.btnUpdate.TabIndex = 5;
            this.btnUpdate.Text = "Sửa";
            this.btnUpdate.UseVisualStyleBackColor = false;
            this.btnUpdate.Click += new System.EventHandler(this.BtnUpdate_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this.btnDelete.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelete.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDelete.ForeColor = System.Drawing.Color.White;
            this.btnDelete.Location = new System.Drawing.Point(252, 64);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(96, 34);
            this.btnDelete.TabIndex = 6;
            this.btnDelete.Text = "Xóa";
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new System.EventHandler(this.BtnDelete_Click);
            // 
            // btnClear
            // 
            this.btnClear.BackColor = System.Drawing.Color.White;
            this.btnClear.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClear.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClear.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClear.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.btnClear.Location = new System.Drawing.Point(360, 64);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(116, 34);
            this.btnClear.TabIndex = 7;
            this.btnClear.Text = "Làm mới";
            this.btnClear.UseVisualStyleBackColor = false;
            this.btnClear.Click += new System.EventHandler(this.BtnClear_Click);
            // 
            // pnlFilter
            // 
            this.pnlFilter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlFilter.BackColor = System.Drawing.Color.White;
            this.pnlFilter.Controls.Add(this.txtSearch);
            this.pnlFilter.Controls.Add(this.cboDuration);
            this.pnlFilter.Location = new System.Drawing.Point(0, 188);
            this.pnlFilter.Name = "pnlFilter";
            this.pnlFilter.Size = new System.Drawing.Size(984, 66);
            this.pnlFilter.TabIndex = 5;
            this.pnlFilter.Visible = false;
            this.pnlFilter.Paint += new System.Windows.Forms.PaintEventHandler(this.PanelBorder_Paint);
            // 
            // txtSearch
            // 
            this.txtSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSearch.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSearch.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.txtSearch.Location = new System.Drawing.Point(22, 16);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(560, 30);
            this.txtSearch.TabIndex = 0;
            this.txtSearch.Tag = "Tìm kiếm theo mã ca, tên ca, giờ bắt đầu, giờ kết thúc";
            this.txtSearch.Text = "Tìm kiếm theo mã ca, tên ca, giờ bắt đầu, giờ kết thúc";
            this.txtSearch.TextChanged += new System.EventHandler(this.FilterChanged);
            this.txtSearch.GotFocus += new System.EventHandler(this.Placeholder_GotFocus);
            this.txtSearch.LostFocus += new System.EventHandler(this.Placeholder_LostFocus);
            // 
            // cboDuration
            // 
            this.cboDuration.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cboDuration.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDuration.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboDuration.FormattingEnabled = true;
            this.cboDuration.Items.AddRange(new object[] {
            "Tất cả thời lượng",
            "Dưới 2 giờ",
            "2 - 4 giờ",
            "Trên 4 giờ"});
            this.cboDuration.Location = new System.Drawing.Point(772, 16);
            this.cboDuration.Name = "cboDuration";
            this.cboDuration.Size = new System.Drawing.Size(190, 31);
            this.cboDuration.TabIndex = 1;
            this.cboDuration.SelectedIndexChanged += new System.EventHandler(this.FilterChanged);
            // 
            // dgvCaHoc
            // 
            this.dgvCaHoc.AllowUserToAddRows = false;
            this.dgvCaHoc.AllowUserToDeleteRows = false;
            this.dgvCaHoc.AllowUserToResizeRows = false;
            this.dgvCaHoc.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvCaHoc.AutoGenerateColumns = false;
            this.dgvCaHoc.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvCaHoc.BackgroundColor = System.Drawing.Color.White;
            this.dgvCaHoc.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvCaHoc.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvCaHoc.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.dgvCaHoc.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvCaHoc.ColumnHeadersHeight = 48;
            this.dgvCaHoc.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            colMaCa,
            colTenCa,
            colGioBatDau,
            colGioKetThuc,
            this.Actions});
            colMaCa.DataPropertyName = "MaCa";
            colMaCa.FillWeight = 90F;
            colMaCa.HeaderText = "Mã ca";
            colMaCa.Name = "colMaCa";
            colMaCa.ReadOnly = true;
            colTenCa.DataPropertyName = "TenCa";
            colTenCa.FillWeight = 190F;
            colTenCa.HeaderText = "Tên ca";
            colTenCa.Name = "colTenCa";
            colTenCa.ReadOnly = true;
            colGioBatDau.DataPropertyName = "GioBatDauText";
            colGioBatDau.FillWeight = 120F;
            colGioBatDau.HeaderText = "Giờ bắt đầu";
            colGioBatDau.Name = "colGioBatDau";
            colGioBatDau.ReadOnly = true;
            colGioKetThuc.DataPropertyName = "GioKetThucText";
            colGioKetThuc.FillWeight = 120F;
            colGioKetThuc.HeaderText = "Giờ kết thúc";
            colGioKetThuc.Name = "colGioKetThuc";
            colGioKetThuc.ReadOnly = true;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(246)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvCaHoc.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvCaHoc.EnableHeadersVisualStyles = false;
            this.dgvCaHoc.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.dgvCaHoc.Location = new System.Drawing.Point(3, 188);
            this.dgvCaHoc.MultiSelect = false;
            this.dgvCaHoc.Name = "dgvCaHoc";
            this.dgvCaHoc.ReadOnly = true;
            this.dgvCaHoc.RowHeadersVisible = false;
            this.dgvCaHoc.RowHeadersWidth = 51;
            this.dgvCaHoc.RowTemplate.Height = 44;
            this.dgvCaHoc.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvCaHoc.Size = new System.Drawing.Size(984, 383);
            this.dgvCaHoc.TabIndex = 6;
            this.dgvCaHoc.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgvCaHoc_CellClick);
            this.dgvCaHoc.CellMouseLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgvCaHoc_CellMouseLeave);
            this.dgvCaHoc.CellMouseMove += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.DgvCaHoc_CellMouseMove);
            this.dgvCaHoc.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.DgvCaHoc_CellPainting);
            // 
            // Actions
            // 
            this.Actions.FillWeight = 120F;
            this.Actions.HeaderText = "Hành động";
            this.Actions.MinimumWidth = 110;
            this.Actions.Name = "Actions";
            this.Actions.ReadOnly = true;
            // 
            // pnlPaging
            // 
            this.pnlPaging.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlPaging.BackColor = System.Drawing.Color.White;
            this.pnlPaging.Controls.Add(this.lblPagingInfo);
            this.pnlPaging.Controls.Add(this.pnlPageButtons);
            this.pnlPaging.Location = new System.Drawing.Point(0, 579);
            this.pnlPaging.Name = "pnlPaging";
            this.pnlPaging.Size = new System.Drawing.Size(984, 46);
            this.pnlPaging.TabIndex = 7;
            this.pnlPaging.Paint += new System.Windows.Forms.PaintEventHandler(this.PanelBorder_Paint);
            this.pnlPaging.Resize += new System.EventHandler(this.PnlPaging_Resize);
            // 
            // lblPagingInfo
            // 
            this.lblPagingInfo.AutoSize = true;
            this.lblPagingInfo.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPagingInfo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblPagingInfo.Location = new System.Drawing.Point(18, 13);
            this.lblPagingInfo.Name = "lblPagingInfo";
            this.lblPagingInfo.Size = new System.Drawing.Size(192, 20);
            this.lblPagingInfo.TabIndex = 0;
            this.lblPagingInfo.Text = "Hiển thị 0-0 trong 0 kết quả";
            // 
            // pnlPageButtons
            // 
            this.pnlPageButtons.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlPageButtons.Location = new System.Drawing.Point(760, 7);
            this.pnlPageButtons.Name = "pnlPageButtons";
            this.pnlPageButtons.Size = new System.Drawing.Size(206, 32);
            this.pnlPageButtons.TabIndex = 1;
            this.pnlPageButtons.WrapContents = false;
            // 
            // ViewAction
            // 
            this.ViewAction.FillWeight = 70F;
            this.ViewAction.HeaderText = "Xem";
            this.ViewAction.MinimumWidth = 6;
            this.ViewAction.Name = "ViewAction";
            this.ViewAction.ReadOnly = true;
            this.ViewAction.Text = "Xem";
            this.ViewAction.UseColumnTextForButtonValue = true;
            this.ViewAction.Width = 125;
            // 
            // EditAction
            // 
            this.EditAction.FillWeight = 70F;
            this.EditAction.HeaderText = "Sửa";
            this.EditAction.MinimumWidth = 6;
            this.EditAction.Name = "EditAction";
            this.EditAction.ReadOnly = true;
            this.EditAction.Text = "Sửa";
            this.EditAction.UseColumnTextForButtonValue = true;
            this.EditAction.Width = 125;
            // 
            // DeleteAction
            // 
            this.DeleteAction.FillWeight = 70F;
            this.DeleteAction.HeaderText = "Xóa";
            this.DeleteAction.MinimumWidth = 6;
            this.DeleteAction.Name = "DeleteAction";
            this.DeleteAction.ReadOnly = true;
            this.DeleteAction.Text = "Xóa";
            this.DeleteAction.UseColumnTextForButtonValue = true;
            this.DeleteAction.Width = 125;
            // 
            // frmQLCaHoc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(247)))), ((int)(((byte)(251)))));
            this.ClientSize = new System.Drawing.Size(1042, 673);
            this.Controls.Add(this.pnlRoot);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MinimumSize = new System.Drawing.Size(1060, 720);
            this.Name = "frmQLCaHoc";
            this.Text = "Quản lý ca học";
            this.pnlRoot.ResumeLayout(false);
            this.pnlRoot.PerformLayout();
            this.pnlEditor.ResumeLayout(false);
            this.pnlEditor.PerformLayout();
            this.pnlFilter.ResumeLayout(false);
            this.pnlFilter.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCaHoc)).EndInit();
            this.pnlPaging.ResumeLayout(false);
            this.pnlPaging.PerformLayout();
            this.ResumeLayout(false);

        }
        private void frmQLCaHoc_Load(object sender, EventArgs e)
        {
            RefreshCaHoc(true);
        }

        private void ConfigureTextBox(TextBox textBox, string placeholder, int x, int y, int width)
        {
            textBox.BackColor = inputBackColor;
            textBox.BorderStyle = BorderStyle.FixedSingle;
            textBox.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            textBox.ForeColor = Color.FromArgb(15, 23, 42);
            textBox.Location = new Point(x, y);
            textBox.Size = new Size(width, 30);
            textBox.Tag = placeholder;
            textBox.Text = placeholder;
            textBox.GotFocus += Placeholder_GotFocus;
            textBox.LostFocus += Placeholder_LostFocus;
        }

        private void ConfigureButton(Button button, string text, int x, int y, int width, int height, Color backColor, Color foreColor)
        {
            button.BackColor = backColor;
            button.Cursor = Cursors.Hand;
            button.FlatStyle = FlatStyle.Flat;
            button.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button.ForeColor = foreColor;
            button.Location = new Point(x, y);
            button.Size = new Size(width, height);
            button.Text = text;
            button.UseVisualStyleBackColor = false;
        }

        private void AddTextColumn(string propertyName, string headerText, float fillWeight)
        {
            DataGridViewTextBoxColumn column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = propertyName;
            column.Name = propertyName;
            column.HeaderText = headerText;
            column.FillWeight = fillWeight;
            dgvCaHoc.Columns.Add(column);
        }

        private void AddButtonColumn(string name, string headerText, string text, float fillWeight)
        {
            DataGridViewButtonColumn column = new DataGridViewButtonColumn();
            column.Name = name;
            column.HeaderText = headerText;
            column.Text = text;
            column.UseColumnTextForButtonValue = true;
            column.FillWeight = fillWeight;
            dgvCaHoc.Columns.Add(column);
        }

        private void RefreshCaHoc(bool resetPage)
        {
            LoadCaHocDataFromDatabase();
            BuildStatCards();
            LoadCaHoc(resetPage);
        }

        private void LoadCaHocDataFromDatabase()
        {
            try
            {
                using (AppDbContext db = new AppDbContext())
                {
                    caHocs = db.CaHocs.AsNoTracking()
                        .OrderBy(item => item.GioBatDau)
                        .ThenBy(item => item.MaCa)
                        .ToList()
                        .Select(ToCaHocRow)
                        .ToList();
                }
            }
            catch (Exception ex)
            {
                caHocs = new List<CaHocRow>();
                ShowDataError("Không thể tải dữ liệu ca học từ database QuanLyPhongMay.", ex);
            }
        }

        private void BuildStatCards()
        {
            pnlStats.Controls.Clear();
            AddStatCard("Tổng ca học", caHocs.Count.ToString(), Color.FromArgb(37, 99, 235));
            AddStatCard("Ca buổi sáng", caHocs.Count(item => item.GioBatDau < new TimeSpan(12, 0, 0)).ToString(), Color.FromArgb(245, 158, 11));
            AddStatCard("Ca buổi chiều", caHocs.Count(item => item.GioBatDau >= new TimeSpan(12, 0, 0)).ToString(), Color.FromArgb(22, 163, 74));
        }

        private void AddStatCard(string title, string value, Color accent)
        {
            Panel card = new Panel();
            Label number = new Label();
            Label caption = new Label();
            card.BackColor = Color.White;
            card.Paint += PanelBorder_Paint;
            card.Margin = new Padding(0, 0, 16, 0);
            card.Size = new Size(250, 78);
            number.AutoSize = true;
            number.Font = new Font("Segoe UI", 16F, FontStyle.Bold, GraphicsUnit.Point, 0);
            number.ForeColor = accent;
            number.Location = new Point(24, 10);
            number.Text = value;
            caption.AutoSize = true;
            caption.Font = new Font("Segoe UI", 9.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            caption.ForeColor = Color.FromArgb(100, 116, 139);
            caption.Location = new Point(26, 46);
            caption.Text = title;
            card.Controls.Add(number);
            card.Controls.Add(caption);
            pnlStats.Controls.Add(card);
        }

        private void LoadCaHoc(bool resetPage)
        {
            List<CaHocRow> filtered = caHocs.OrderBy(item => item.GioBatDau).ThenBy(item => item.MaCa).ToList();
            int totalItems = filtered.Count;
            int totalPages = Math.Max(1, (int)Math.Ceiling(totalItems / (double)PageSize));

            if (resetPage)
            {
                currentPage = 1;
            }

            if (currentPage > totalPages)
            {
                currentPage = totalPages;
            }

            List<CaHocRow> pageItems = filtered
                .Skip((currentPage - 1) * PageSize)
                .Take(PageSize)
                .Select((item, index) => item.CloneWithStt(((currentPage - 1) * PageSize) + index + 1))
                .ToList();

            dgvCaHoc.DataSource = pageItems;
            int start = totalItems == 0 ? 0 : ((currentPage - 1) * PageSize) + 1;
            int end = Math.Min(currentPage * PageSize, totalItems);
            lblPagingInfo.Text = string.Format("Hiển thị {0}-{1} trong {2} kết quả", start, end, totalItems);
            BuildPagination(totalPages);
        }

        private IEnumerable<CaHocRow> GetFilteredCaHoc()
        {
            IEnumerable<CaHocRow> source = caHocs;
            string keyword = GetInputText(txtSearch).ToLowerInvariant();

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                source = source.Where(item =>
                    ContainsKeyword(item.MaCa, keyword) ||
                    ContainsKeyword(item.TenCa, keyword) ||
                    ContainsKeyword(item.GioBatDauText, keyword) ||
                    ContainsKeyword(item.GioKetThucText, keyword));
            }

            if (cboDuration.SelectedIndex > 0)
            {
                source = source.Where(item => MatchesDurationFilter(item.ThoiLuong, cboDuration.SelectedIndex));
            }

            return source.OrderBy(item => item.GioBatDau).ThenBy(item => item.MaCa);
        }

        private static bool MatchesDurationFilter(double hours, int selectedIndex)
        {
            if (selectedIndex == 1)
            {
                return hours < 2D;
            }

            if (selectedIndex == 2)
            {
                return hours >= 2D && hours <= 4D;
            }

            if (selectedIndex == 3)
            {
                return hours > 4D;
            }

            return true;
        }

        private void BtnCreate_Click(object sender, EventArgs e)
        {
            string tenCa;
            TimeSpan start;
            TimeSpan end;
            int unusedMaCa;
            if (!ShowCaHocPopup("Thêm ca học", null, false, true, out unusedMaCa, out tenCa, out start, out end))
            {
                return;
            }

            try
            {
                using (AppDbContext db = new AppDbContext())
                {
                    db.CaHocs.Add(new CaHoc
                    {
                        TenCa = tenCa,
                        GioBatDau = start,
                        GioKetThuc = end
                    });
                    db.SaveChanges();
                }

                RefreshCaHoc(true);
                MessageBox.Show("Đã thêm ca học thành công.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                ShowDataError("Không thể thêm ca học.", ex);
            }
        }

        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            TimeSpan start;
            TimeSpan end;
            if (!ValidateEditor(out start, out end))
            {
                return;
            }

            try
            {
                using (AppDbContext db = new AppDbContext())
                {
                    int maCa = int.Parse(GetInputText(txtMaCa), CultureInfo.InvariantCulture);
                    CaHoc caHoc = db.CaHocs.FirstOrDefault(item => item.MaCa == maCa);
                    if (caHoc == null)
                    {
                        MessageBox.Show("Không tìm thấy ca học cần cập nhật.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    caHoc.TenCa = GetInputText(txtTenCa);
                    caHoc.GioBatDau = start;
                    caHoc.GioKetThuc = end;
                    db.SaveChanges();
                }

                RefreshCaHoc(false);
                MessageBox.Show("Đã cập nhật ca học thành công.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                ShowDataError("Không thể cập nhật ca học.", ex);
            }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            DeleteCaHoc(GetInputText(txtMaCa));
        }

        private void DeleteCaHoc(string maCa)
        {
            if (string.IsNullOrWhiteSpace(maCa))
            {
                MessageBox.Show("Vui lòng chọn ca học cần xóa.", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int maCaValue;
            if (!int.TryParse(maCa, NumberStyles.Integer, CultureInfo.InvariantCulture, out maCaValue))
            {
                MessageBox.Show("Mã ca phải là số nguyên.", "Dữ liệu không hợp lệ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("Bạn có chắc muốn xóa ca học '" + maCa + "'?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
            {
                return;
            }

            try
            {
                using (AppDbContext db = new AppDbContext())
                {
                    if (db.LichThucHanhs.Any(item => item.MaCa == maCaValue))
                    {
                        MessageBox.Show("Không thể xóa ca học đang được sử dụng trong lịch thực hành.", "Không thể xóa", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    CaHoc caHoc = db.CaHocs.FirstOrDefault(item => item.MaCa == maCaValue);
                    if (caHoc == null)
                    {
                        MessageBox.Show("Không tìm thấy ca học cần xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    db.CaHocs.Remove(caHoc);
                    db.SaveChanges();
                }

                ClearEditor();
                RefreshCaHoc(false);
                MessageBox.Show("Đã xóa ca học thành công.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                ShowDataError("Không thể xóa ca học.", ex);
            }
        }

        private bool ValidateEditor(out TimeSpan start, out TimeSpan end)
        {
            start = TimeSpan.Zero;
            end = TimeSpan.Zero;

            if (string.IsNullOrWhiteSpace(GetInputText(txtMaCa)))
            {
                MessageBox.Show("Vui lòng nhập mã ca.", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaCa.Focus();
                return false;
            }

            int maCa;
            if (!int.TryParse(GetInputText(txtMaCa), NumberStyles.Integer, CultureInfo.InvariantCulture, out maCa))
            {
                MessageBox.Show("Mã ca phải là số nguyên vì cột maCa trong SQL đang là kiểu int.", "Dữ liệu không hợp lệ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaCa.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(GetInputText(txtTenCa)))
            {
                MessageBox.Show("Vui lòng nhập tên ca.", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenCa.Focus();
                return false;
            }

            if (!TimeSpan.TryParseExact(GetInputText(txtGioBatDau), @"hh\:mm", CultureInfo.InvariantCulture, out start))
            {
                MessageBox.Show("Giờ bắt đầu phải đúng định dạng HH:mm.", "Dữ liệu không hợp lệ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtGioBatDau.Focus();
                return false;
            }

            if (!TimeSpan.TryParseExact(GetInputText(txtGioKetThuc), @"hh\:mm", CultureInfo.InvariantCulture, out end))
            {
                MessageBox.Show("Giờ kết thúc phải đúng định dạng HH:mm.", "Dữ liệu không hợp lệ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtGioKetThuc.Focus();
                return false;
            }

            if (start == end)
            {
                MessageBox.Show("Giờ kết thúc phải khác giờ bắt đầu.", "Dữ liệu không hợp lệ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtGioKetThuc.Focus();
                return false;
            }

            return true;
        }

        private void ClearEditor()
        {
            txtMaCa.ReadOnly = false;
            txtMaCa.BackColor = inputBackColor;
            ResetPlaceholder(txtMaCa);
            ResetPlaceholder(txtTenCa);
            ResetPlaceholder(txtGioBatDau);
            ResetPlaceholder(txtGioKetThuc);
            dgvCaHoc.ClearSelection();
        }

        private void FillEditor(CaHocRow row)
        {
            SetInputText(txtMaCa, row.MaCa);
            SetInputText(txtTenCa, row.TenCa);
            SetInputText(txtGioBatDau, row.GioBatDauText);
            SetInputText(txtGioKetThuc, row.GioKetThucText);
            txtMaCa.ReadOnly = true;
            txtMaCa.BackColor = Color.FromArgb(241, 245, 249);
        }

        private void EditCaHoc(CaHocRow row)
        {
            int maCa;
            string tenCa;
            TimeSpan start;
            TimeSpan end;
            if (!ShowCaHocPopup("Sửa ca học", row, false, true, out maCa, out tenCa, out start, out end))
            {
                return;
            }

            try
            {
                using (AppDbContext db = new AppDbContext())
                {
                    CaHoc caHoc = db.CaHocs.FirstOrDefault(item => item.MaCa == maCa);
                    if (caHoc == null)
                    {
                        MessageBox.Show("Không tìm thấy ca học cần cập nhật.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    caHoc.TenCa = tenCa;
                    caHoc.GioBatDau = start;
                    caHoc.GioKetThuc = end;
                    db.SaveChanges();
                }

                RefreshCaHoc(false);
                MessageBox.Show("Đã cập nhật ca học thành công.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                ShowDataError("Không thể cập nhật ca học.", ex);
            }
        }

        private bool ShowCaHocPopup(string title, CaHocRow row, bool readOnly, bool lockCode, out int maCa, out string tenCa, out TimeSpan start, out TimeSpan end)
        {
            maCa = 0;
            tenCa = string.Empty;
            start = TimeSpan.Zero;
            end = TimeSpan.Zero;
            int resultMaCa = 0;
            string resultTenCa = string.Empty;
            TimeSpan resultStart = TimeSpan.Zero;
            TimeSpan resultEnd = TimeSpan.Zero;

            using (Form dialog = new Form())
            using (Label lblTitleDialog = new Label())
            using (Label lblSubtitleDialog = new Label())
            using (Label lblMaCa = new Label())
            using (Label lblTenCa = new Label())
            using (Label lblStart = new Label())
            using (Label lblEnd = new Label())
            using (TextBox txtDialogMaCa = new TextBox())
            using (TextBox txtDialogTenCa = new TextBox())
            using (TextBox txtDialogStart = new TextBox())
            using (TextBox txtDialogEnd = new TextBox())
            using (Button btnSave = new Button())
            using (Button btnCancel = new Button())
            {
                dialog.Text = title;
                dialog.StartPosition = FormStartPosition.CenterParent;
                dialog.FormBorderStyle = FormBorderStyle.FixedDialog;
                dialog.MaximizeBox = false;
                dialog.MinimizeBox = false;
                dialog.ShowInTaskbar = false;
                dialog.ClientSize = new Size(520, 430);
                dialog.BackColor = Color.White;
                dialog.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);

                lblTitleDialog.AutoSize = true;
                lblTitleDialog.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
                lblTitleDialog.ForeColor = Color.FromArgb(15, 23, 42);
                lblTitleDialog.Location = new Point(30, 24);
                lblTitleDialog.Text = title;

                lblSubtitleDialog.AutoSize = true;
                lblSubtitleDialog.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
                lblSubtitleDialog.ForeColor = Color.FromArgb(100, 116, 139);
                lblSubtitleDialog.Location = new Point(32, 68);
                lblSubtitleDialog.Text = "Thông tin ca học trong hệ thống";

                bool createMode = row == null && !readOnly;
                ConfigurePopupLabel(lblMaCa, "Mã ca", 34, 116);
                ConfigurePopupInput(txtDialogMaCa, 34, 146);
                ConfigurePopupLabel(lblTenCa, "Tên ca", createMode ? 34 : 268, 116);
                ConfigurePopupInput(txtDialogTenCa, createMode ? 34 : 268, 146);
                ConfigurePopupLabel(lblStart, "Giờ bắt đầu", 34, 216);
                ConfigurePopupInput(txtDialogStart, 34, 246);
                ConfigurePopupLabel(lblEnd, "Giờ kết thúc", 268, 216);
                ConfigurePopupInput(txtDialogEnd, 268, 246);

                if (row != null)
                {
                    txtDialogMaCa.Text = row.MaCa;
                    txtDialogTenCa.Text = row.TenCa;
                    txtDialogStart.Text = row.GioBatDauText;
                    txtDialogEnd.Text = row.GioKetThucText;
                }

                txtDialogMaCa.ReadOnly = true;
                txtDialogTenCa.ReadOnly = readOnly;
                txtDialogStart.ReadOnly = readOnly;
                txtDialogEnd.ReadOnly = readOnly;
                lblMaCa.Visible = !createMode;
                txtDialogMaCa.Visible = !createMode;

                if (txtDialogMaCa.ReadOnly)
                {
                    txtDialogMaCa.BackColor = Color.FromArgb(241, 245, 249);
                }

                btnSave.BackColor = readOnly ? Color.White : primaryColor;
                btnSave.FlatAppearance.BorderColor = borderColor;
                btnSave.FlatStyle = FlatStyle.Flat;
                btnSave.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
                btnSave.ForeColor = readOnly ? Color.FromArgb(51, 65, 85) : Color.White;
                btnSave.Location = readOnly ? new Point(380, 350) : new Point(284, 350);
                btnSave.Size = new Size(106, 42);
                btnSave.Text = readOnly ? "Đóng" : "Lưu";
                btnSave.DialogResult = readOnly ? DialogResult.Cancel : DialogResult.None;

                btnCancel.BackColor = Color.White;
                btnCancel.FlatAppearance.BorderColor = borderColor;
                btnCancel.FlatStyle = FlatStyle.Flat;
                btnCancel.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
                btnCancel.ForeColor = Color.FromArgb(51, 65, 85);
                btnCancel.Location = new Point(400, 350);
                btnCancel.Size = new Size(86, 42);
                btnCancel.Text = "Hủy";
                btnCancel.DialogResult = DialogResult.Cancel;
                btnCancel.Visible = !readOnly;

                dialog.Controls.Add(lblTitleDialog);
                dialog.Controls.Add(lblSubtitleDialog);
                dialog.Controls.Add(lblMaCa);
                dialog.Controls.Add(txtDialogMaCa);
                dialog.Controls.Add(lblTenCa);
                dialog.Controls.Add(txtDialogTenCa);
                dialog.Controls.Add(lblStart);
                dialog.Controls.Add(txtDialogStart);
                dialog.Controls.Add(lblEnd);
                dialog.Controls.Add(txtDialogEnd);
                dialog.Controls.Add(btnSave);
                dialog.Controls.Add(btnCancel);
                dialog.AcceptButton = btnSave;
                dialog.CancelButton = btnCancel;

                btnSave.Click += delegate
                {
                    if (readOnly)
                    {
                        dialog.DialogResult = DialogResult.Cancel;
                        return;
                    }

                    int parsedMaCa = row == null ? 0 : int.Parse(row.MaCa, CultureInfo.InvariantCulture);
                    TimeSpan parsedStart;
                    TimeSpan parsedEnd;

                    if (!createMode && !int.TryParse(txtDialogMaCa.Text.Trim(), NumberStyles.Integer, CultureInfo.InvariantCulture, out parsedMaCa))
                    {
                MessageBox.Show("Mã ca phải là số nguyên.", "Dữ liệu không hợp lệ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtDialogMaCa.Focus();
                        return;
                    }

                    if (string.IsNullOrWhiteSpace(txtDialogTenCa.Text))
                    {
                MessageBox.Show("Vui lòng nhập tên ca.", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtDialogTenCa.Focus();
                        return;
                    }

                    if (!TimeSpan.TryParseExact(txtDialogStart.Text.Trim(), @"hh\:mm", CultureInfo.InvariantCulture, out parsedStart))
                    {
                MessageBox.Show("Giờ bắt đầu phải đúng định dạng HH:mm.", "Dữ liệu không hợp lệ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtDialogStart.Focus();
                        return;
                    }

                    if (!TimeSpan.TryParseExact(txtDialogEnd.Text.Trim(), @"hh\:mm", CultureInfo.InvariantCulture, out parsedEnd))
                    {
                MessageBox.Show("Giờ bắt đầu phải đúng định dạng HH:mm.", "Dữ liệu không hợp lệ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtDialogEnd.Focus();
                        return;
                    }

                    if (parsedStart == parsedEnd)
                    {
                MessageBox.Show("Giờ kết thúc phải khác giờ bắt đầu.", "Dữ liệu không hợp lệ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtDialogEnd.Focus();
                        return;
                    }

                    resultMaCa = parsedMaCa;
                    resultTenCa = txtDialogTenCa.Text.Trim();
                    resultStart = parsedStart;
                    resultEnd = parsedEnd;
                    dialog.DialogResult = DialogResult.OK;
                };

                bool accepted = dialog.ShowDialog(this) == DialogResult.OK;
                if (accepted)
                {
                    maCa = resultMaCa;
                    tenCa = resultTenCa;
                    start = resultStart;
                    end = resultEnd;
                }

                return accepted;
            }
        }

        private static void ConfigurePopupLabel(Label label, string text, int x, int y)
        {
            label.AutoSize = true;
            label.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label.ForeColor = Color.FromArgb(30, 41, 59);
            label.Location = new Point(x, y);
            label.Text = text;
        }

        private static void ConfigurePopupInput(TextBox textBox, int x, int y)
        {
            textBox.BackColor = Color.FromArgb(248, 250, 252);
            textBox.BorderStyle = BorderStyle.FixedSingle;
            textBox.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            textBox.ForeColor = Color.FromArgb(15, 23, 42);
            textBox.Location = new Point(x, y);
            textBox.Size = new Size(218, 30);
        }

        private void DgvCaHoc_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            CaHocRow row = dgvCaHoc.Rows[e.RowIndex].DataBoundItem as CaHocRow;
            if (row == null)
            {
                return;
            }

            string columnName = dgvCaHoc.Columns[e.ColumnIndex].Name;
            if (columnName == "Actions")
            {
                Rectangle cellBounds = dgvCaHoc.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false);
                int mouseX = dgvCaHoc.PointToClient(Cursor.Position).X - cellBounds.Left;
                string action = GetActionFromMouseX(cellBounds.Width, mouseX);

                if (action == "Delete")
                {
                    DeleteCaHoc(row.MaCa);
                    return;
                }

                if (action == "Edit")
                {
                    EditCaHoc(row);
                    return;
                }
            }

            if (columnName == "DeleteAction")
            {
                DeleteCaHoc(row.MaCa);
                return;
            }

            FillEditor(row);
        }

        private void DgvCaHoc_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0 || dgvCaHoc.Columns[e.ColumnIndex].Name != "Actions")
            {
                return;
            }

            e.Handled = true;
            e.PaintBackground(e.CellBounds, true);

            string[] icons = { "\uE70F", "\uE74D" };
            Color[] colors =
            {
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

        private void DgvCaHoc_CellMouseMove(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0 || dgvCaHoc.Columns[e.ColumnIndex].Name != "Actions")
            {
                dgvCaHoc.Cursor = Cursors.Default;
                actionToolTip.SetToolTip(dgvCaHoc, string.Empty);
                return;
            }

            string action = GetActionFromMouseX(dgvCaHoc.Columns[e.ColumnIndex].Width, e.X);
            string tip = action == "Edit" ? "Sửa ca học" : action == "Delete" ? "Xóa ca học" : string.Empty;
            dgvCaHoc.Cursor = string.IsNullOrEmpty(tip) ? Cursors.Default : Cursors.Hand;
            actionToolTip.SetToolTip(dgvCaHoc, tip);
        }

        private void DgvCaHoc_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            dgvCaHoc.Cursor = Cursors.Default;
            actionToolTip.SetToolTip(dgvCaHoc, string.Empty);
        }

        private static string GetActionFromMouseX(int cellWidth, int x)
        {
            int iconSize = 30;
            int totalWidth = (iconSize * 2) + 6;
            int startX = (cellWidth - totalWidth) / 2;

            if (x >= startX && x <= startX + iconSize)
            {
                return "Edit";
            }

            if (x >= startX + iconSize + 6 && x <= startX + (iconSize * 2) + 6)
            {
                return "Delete";
            }

            return string.Empty;
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

        private void FilterChanged(object sender, EventArgs e)
        {
            LoadCaHoc(true);
        }

        private void BuildPagination(int totalPages)
        {
            pnlPageButtons.Controls.Clear();
            AddPageButton("<", currentPage > 1, currentPage - 1, false);

            int startPage = Math.Max(1, currentPage - 2);
            int endPage = Math.Min(totalPages, startPage + 4);
            startPage = Math.Max(1, endPage - 4);

            for (int page = startPage; page <= endPage; page++)
            {
                AddPageButton(page.ToString(), true, page, page == currentPage);
            }

            AddPageButton(">", currentPage < totalPages, currentPage + 1, false);
            AlignPageButtonsRight();
        }

        private void AddPageButton(string text, bool enabled, int page, bool active)
        {
            Button button = new Button();
            button.Enabled = enabled;
            button.FlatStyle = FlatStyle.Flat;
            button.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button.Margin = new Padding(3, 0, 3, 0);
            button.Size = new Size(32, 32);
            button.Text = text;
            button.Tag = page;
            button.BackColor = active ? primaryColor : enabled ? Color.White : Color.FromArgb(209, 213, 219);
            button.ForeColor = active ? Color.White : Color.FromArgb(100, 116, 139);

            if (enabled && !active)
            {
                button.Click += PageButton_Click;
            }

            pnlPageButtons.Controls.Add(button);
        }

        private void PageButton_Click(object sender, EventArgs e)
        {
            currentPage = Convert.ToInt32(((Control)sender).Tag);
            LoadCaHoc(false);
        }

        private void PnlPaging_Resize(object sender, EventArgs e)
        {
            AlignPageButtonsRight();
        }

        private void AlignPageButtonsRight()
        {
            int totalWidth = 0;
            foreach (Control control in pnlPageButtons.Controls)
            {
                totalWidth += control.Width + control.Margin.Left + control.Margin.Right;
            }

            pnlPageButtons.Width = totalWidth;
            pnlPageButtons.Left = Math.Max(220, pnlRoot.Width - pnlPageButtons.Width - 18);
        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            ClearEditor();
        }

        private void PanelBorder_Paint(object sender, PaintEventArgs e)
        {
            Control control = (Control)sender;
            using (Pen pen = new Pen(Color.FromArgb(226, 232, 240)))
            {
                Rectangle bounds = new Rectangle(0, 0, control.Width - 1, control.Height - 1);
                e.Graphics.DrawRectangle(pen, bounds);
            }
        }

        private void Placeholder_GotFocus(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (textBox.Text == Convert.ToString(textBox.Tag))
            {
                textBox.Text = string.Empty;
                textBox.ForeColor = Color.FromArgb(15, 23, 42);
            }
        }

        private void Placeholder_LostFocus(object sender, EventArgs e)
        {
            ResetPlaceholder((TextBox)sender);
        }

        private static void ResetPlaceholder(TextBox textBox)
        {
            if (string.IsNullOrWhiteSpace(textBox.Text))
            {
                textBox.Text = Convert.ToString(textBox.Tag);
                textBox.ForeColor = Color.FromArgb(100, 116, 139);
            }
        }

        private static string GetInputText(TextBox textBox)
        {
            string placeholder = Convert.ToString(textBox.Tag);
            return textBox.Text == placeholder ? string.Empty : textBox.Text.Trim();
        }

        private static void SetInputText(TextBox textBox, string value)
        {
            textBox.Text = value ?? string.Empty;
            textBox.ForeColor = Color.FromArgb(15, 23, 42);
        }

        private static CaHocRow ToCaHocRow(CaHoc item)
        {
            double totalHours = Math.Round((item.GioKetThuc - item.GioBatDau).TotalHours, 2);
            if (totalHours < 0)
            {
                totalHours += 24D;
            }

            return new CaHocRow
            {
                MaCa = item.MaCa.ToString(CultureInfo.InvariantCulture),
                TenCa = item.TenCa ?? string.Empty,
                GioBatDau = item.GioBatDau,
                GioKetThuc = item.GioKetThuc,
                GioBatDauText = item.GioBatDau.ToString(@"hh\:mm"),
                GioKetThucText = item.GioKetThuc.ToString(@"hh\:mm"),
                ThoiLuong = totalHours,
                ThoiLuongText = totalHours.ToString("0.##") + " giờ"
            };
        }

        private static bool ContainsKeyword(string value, string keyword)
        {
            return !string.IsNullOrEmpty(value) && value.ToLowerInvariant().Contains(keyword);
        }

        private static void ShowDataError(string message, Exception ex)
        {
            MessageBox.Show(message + "\n" + GetFullErrorMessage(ex), "Lỗi dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private static string GetFullErrorMessage(Exception ex)
        {
            List<string> messages = new List<string>();
            while (ex != null)
            {
                messages.Add(ex.Message);
                ex = ex.InnerException;
            }

            return string.Join("\n", messages);
        }

        private class CaHocRow
        {
            public int Stt { get; set; }
            public string MaCa { get; set; }
            public string TenCa { get; set; }
            public TimeSpan GioBatDau { get; set; }
            public TimeSpan GioKetThuc { get; set; }
            public string GioBatDauText { get; set; }
            public string GioKetThucText { get; set; }
            public double ThoiLuong { get; set; }
            public string ThoiLuongText { get; set; }

            public CaHocRow CloneWithStt(int stt)
            {
                return new CaHocRow
                {
                    Stt = stt,
                    MaCa = MaCa,
                    TenCa = TenCa,
                    GioBatDau = GioBatDau,
                    GioKetThuc = GioKetThuc,
                    GioBatDauText = GioBatDauText,
                    GioKetThucText = GioKetThucText,
                    ThoiLuong = ThoiLuong,
                    ThoiLuongText = ThoiLuongText
                };
            }
        }

    }
}

