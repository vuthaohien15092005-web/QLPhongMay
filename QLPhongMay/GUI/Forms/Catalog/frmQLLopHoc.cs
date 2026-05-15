using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using QLPhongMay.DAL;
using QLPhongMay.Models;

namespace QLPhongMay.GUI.Forms.Catalog
{
    public partial class frmQLLopHoc : Form
    {
        private const int PageSize = 8;
        private readonly LopHocRepository lopHocRepository;
        private readonly ToolTip actionToolTip;
        private List<LopHoc> lopHocs;
        private int currentPage = 1;
        private bool isLoading;

        private Panel pnlRoot;
        private Button btnBack;
        private Label lblTitle;
        private Label lblSubtitle;
        private Button btnAdd;
        private Panel pnlFilter;
        private TextBox txtSearch;
        private ComboBox cboNganh;
        private DataGridView dgvLopHoc;
        private Panel pnlPaging;
        private Label lblPagingInfo;
        private FlowLayoutPanel pnlPageButtons;
        private System.ComponentModel.IContainer components;

        public frmQLLopHoc()
        {
            this.lopHocRepository = new LopHocRepository();
            this.actionToolTip = new ToolTip();
            this.lopHocs = new List<LopHoc>();

            InitializeComponent();
            this.Shown += new EventHandler(this.frmQLLopHoc_Shown);
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

        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pnlRoot = new System.Windows.Forms.Panel();
            this.btnBack = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblSubtitle = new System.Windows.Forms.Label();
            this.btnAdd = new System.Windows.Forms.Button();
            this.pnlFilter = new System.Windows.Forms.Panel();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.cboNganh = new System.Windows.Forms.ComboBox();
            this.dgvLopHoc = new System.Windows.Forms.DataGridView();
            this.pnlPaging = new System.Windows.Forms.Panel();
            this.lblPagingInfo = new System.Windows.Forms.Label();
            this.pnlPageButtons = new System.Windows.Forms.FlowLayoutPanel();
            this.pnlRoot.SuspendLayout();
            this.pnlFilter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLopHoc)).BeginInit();
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
            this.pnlRoot.Controls.Add(this.pnlFilter);
            this.pnlRoot.Controls.Add(this.dgvLopHoc);
            this.pnlRoot.Controls.Add(this.pnlPaging);
            this.pnlRoot.Location = new System.Drawing.Point(28, 24);
            this.pnlRoot.Name = "pnlRoot";
            this.pnlRoot.Size = new System.Drawing.Size(1124, 708);
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
            this.btnBack.Location = new System.Drawing.Point(0, 4);
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
            this.lblTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 21F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblTitle.Location = new System.Drawing.Point(402, 12);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(224, 38);
            this.lblTitle.TabIndex = 1;
            this.lblTitle.Text = "Quản lý lớp học";
            this.lblTitle.Click += new System.EventHandler(this.lblTitle_Click);
            // 
            // lblSubtitle
            // 
            this.lblSubtitle.AutoSize = true;
            this.lblSubtitle.BackColor = System.Drawing.Color.Transparent;
            this.lblSubtitle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblSubtitle.Location = new System.Drawing.Point(380, 50);
            this.lblSubtitle.Name = "lblSubtitle";
            this.lblSubtitle.Size = new System.Drawing.Size(246, 19);
            this.lblSubtitle.TabIndex = 2;
            this.lblSubtitle.Text = "Theo dõi lớp thực hành, ngành và sĩ số";
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAdd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.btnAdd.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAdd.FlatAppearance.BorderSize = 0;
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdd.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdd.ForeColor = System.Drawing.Color.White;
            this.btnAdd.Location = new System.Drawing.Point(930, 28);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(194, 46);
            this.btnAdd.TabIndex = 3;
            this.btnAdd.Text = "+  Thêm lớp";
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Click += new System.EventHandler(this.BtnAdd_Click);
            // 
            // pnlFilter
            // 
            this.pnlFilter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlFilter.BackColor = System.Drawing.Color.White;
            this.pnlFilter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlFilter.Controls.Add(this.txtSearch);
            this.pnlFilter.Controls.Add(this.cboNganh);
            this.pnlFilter.Location = new System.Drawing.Point(0, 104);
            this.pnlFilter.Name = "pnlFilter";
            this.pnlFilter.Size = new System.Drawing.Size(1124, 66);
            this.pnlFilter.TabIndex = 4;
            // 
            // txtSearch
            // 
            this.txtSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSearch.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtSearch.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSearch.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.txtSearch.Location = new System.Drawing.Point(22, 11);
            this.txtSearch.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(520, 25);
            this.txtSearch.TabIndex = 0;
            this.txtSearch.TextChanged += new System.EventHandler(this.FilterChanged);
            // 
            // cboNganh
            // 
            this.cboNganh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cboNganh.BackColor = System.Drawing.Color.White;
            this.cboNganh.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboNganh.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboNganh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboNganh.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboNganh.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.cboNganh.ItemHeight = 36;
            this.cboNganh.Location = new System.Drawing.Point(852, 11);
            this.cboNganh.Name = "cboNganh";
            this.cboNganh.Size = new System.Drawing.Size(250, 42);
            this.cboNganh.TabIndex = 1;
            this.cboNganh.SelectedIndexChanged += new System.EventHandler(this.FilterChanged);
            // 
            // dgvLopHoc
            // 
            this.dgvLopHoc.AllowUserToAddRows = false;
            this.dgvLopHoc.AllowUserToDeleteRows = false;
            this.dgvLopHoc.AllowUserToResizeRows = false;
            this.dgvLopHoc.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvLopHoc.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvLopHoc.BackgroundColor = System.Drawing.Color.White;
            this.dgvLopHoc.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvLopHoc.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvLopHoc.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            dataGridViewCellStyle1.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.dgvLopHoc.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvLopHoc.ColumnHeadersHeight = 48;
            this.dgvLopHoc.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            dataGridViewCellStyle2.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(246)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvLopHoc.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvLopHoc.EnableHeadersVisualStyles = false;
            this.dgvLopHoc.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.dgvLopHoc.Location = new System.Drawing.Point(0, 188);
            this.dgvLopHoc.MultiSelect = false;
            this.dgvLopHoc.Name = "dgvLopHoc";
            this.dgvLopHoc.ReadOnly = true;
            this.dgvLopHoc.RowHeadersVisible = false;
            this.dgvLopHoc.RowTemplate.Height = 44;
            this.dgvLopHoc.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvLopHoc.Size = new System.Drawing.Size(1124, 464);
            this.dgvLopHoc.TabIndex = 5;
            this.dgvLopHoc.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgvLopHoc_CellClick);
            this.dgvLopHoc.CellMouseLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgvLopHoc_CellMouseLeave);
            this.dgvLopHoc.CellMouseMove += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.DgvLopHoc_CellMouseMove);
            // 
            // pnlPaging
            // 
            this.pnlPaging.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlPaging.BackColor = System.Drawing.Color.White;
            this.pnlPaging.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlPaging.Controls.Add(this.lblPagingInfo);
            this.pnlPaging.Controls.Add(this.pnlPageButtons);
            this.pnlPaging.Location = new System.Drawing.Point(0, 662);
            this.pnlPaging.Name = "pnlPaging";
            this.pnlPaging.Size = new System.Drawing.Size(1124, 46);
            this.pnlPaging.TabIndex = 6;
            // 
            // lblPagingInfo
            // 
            this.lblPagingInfo.AutoSize = true;
            this.lblPagingInfo.BackColor = System.Drawing.Color.Transparent;
            this.lblPagingInfo.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPagingInfo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblPagingInfo.Location = new System.Drawing.Point(18, 12);
            this.lblPagingInfo.Name = "lblPagingInfo";
            this.lblPagingInfo.Size = new System.Drawing.Size(97, 15);
            this.lblPagingInfo.TabIndex = 0;
            this.lblPagingInfo.Text = "Không có dữ liệu";
            // 
            // pnlPageButtons
            // 
            this.pnlPageButtons.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlPageButtons.BackColor = System.Drawing.Color.Transparent;
            this.pnlPageButtons.Location = new System.Drawing.Point(820, 6);
            this.pnlPageButtons.Name = "pnlPageButtons";
            this.pnlPageButtons.Size = new System.Drawing.Size(286, 34);
            this.pnlPageButtons.TabIndex = 1;
            this.pnlPageButtons.WrapContents = false;
            // 
            // frmQLLopHoc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(247)))), ((int)(((byte)(251)))));
            this.ClientSize = new System.Drawing.Size(1180, 760);
            this.Controls.Add(this.pnlRoot);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MinimumSize = new System.Drawing.Size(980, 620);
            this.Name = "frmQLLopHoc";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Quản lý lớp học";
            this.pnlRoot.ResumeLayout(false);
            this.pnlRoot.PerformLayout();
            this.pnlFilter.ResumeLayout(false);
            this.pnlFilter.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLopHoc)).EndInit();
            this.pnlPaging.ResumeLayout(false);
            this.pnlPaging.PerformLayout();
            this.ResumeLayout(false);

        }

        private async void frmQLLopHoc_Shown(object sender, EventArgs e)
        {
            await RefreshDataAsync(true);
        }

        private async Task RefreshDataAsync(bool resetPage)
        {
            SetLoadingState(true, "Đang tải danh sách lớp học...");

            try
            {
                List<LopHoc> data = await Task.Run(() => this.lopHocRepository.GetAll().ToList());
                if (this.IsDisposed)
                {
                    return;
                }

                this.lopHocs = data;
                LoadNganhFilter();
                ApplyFilter(resetPage);
            }
            catch (Exception ex)
            {
                this.lopHocs = new List<LopHoc>();
                LoadNganhFilter();
                BindPage(new List<LopHocRow>());
                ShowDataError("Không thể tải danh sách lớp học.", ex);
                this.lblPagingInfo.Text = "Không tải được dữ liệu. Kiểm tra SQL Server và App.config.";
            }
            finally
            {
                SetLoadingState(false, null);
            }
        }

        private void RefreshData(bool resetPage)
        {
            SetLoadingState(true, "Đang tải danh sách lớp học...");

            try
            {
                this.lopHocs = this.lopHocRepository.GetAll().ToList();
                LoadNganhFilter();
                ApplyFilter(resetPage);
            }
            catch (Exception ex)
            {
                ShowDataError("Không thể tải danh sách lớp học.", ex);
            }
            finally
            {
                SetLoadingState(false, null);
            }
        }

        private void SetLoadingState(bool loading, string message)
        {
            this.isLoading = loading;
            this.txtSearch.Enabled = !loading;
            this.cboNganh.Enabled = !loading;
            this.btnAdd.Enabled = !loading;
            this.dgvLopHoc.Enabled = !loading;

            if (loading)
            {
                this.pnlPageButtons.Controls.Clear();
                this.lblPagingInfo.Text = message ?? "Đang tải...";
            }
        }

        private void LoadNganhFilter()
        {
            string selected = this.cboNganh.SelectedItem == null ? "Tất cả ngành" : Convert.ToString(this.cboNganh.SelectedItem);
            this.cboNganh.SelectedIndexChanged -= new EventHandler(this.FilterChanged);
            this.cboNganh.Items.Clear();
            this.cboNganh.Items.Add("Tất cả ngành");

            foreach (string nganh in this.lopHocs
                .Select(l => l.Nganh)
                .Where(n => !string.IsNullOrWhiteSpace(n))
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .OrderBy(n => n))
            {
                this.cboNganh.Items.Add(nganh);
            }

            int selectedIndex = this.cboNganh.Items.IndexOf(selected);
            this.cboNganh.SelectedIndex = selectedIndex >= 0 ? selectedIndex : 0;
            this.cboNganh.SelectedIndexChanged += new EventHandler(this.FilterChanged);
        }

        private void FilterChanged(object sender, EventArgs e)
        {
            ApplyFilter(true);
        }

        private void ApplyFilter(bool resetPage)
        {
            if (resetPage)
            {
                this.currentPage = 1;
            }

            string keyword = (this.txtSearch.Text ?? string.Empty).Trim();
            string nganh = this.cboNganh.SelectedItem == null ? "Tất cả ngành" : Convert.ToString(this.cboNganh.SelectedItem);

            IEnumerable<LopHoc> query = this.lopHocs;

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                query = query.Where(l =>
                    Contains(l.MaLop, keyword) ||
                    Contains(l.TenLop, keyword) ||
                    Contains(l.Nganh, keyword));
            }

            if (!string.Equals(nganh, "Tất cả ngành", StringComparison.OrdinalIgnoreCase))
            {
                query = query.Where(l => string.Equals(l.Nganh, nganh, StringComparison.OrdinalIgnoreCase));
            }

            List<LopHocRow> rows = query
                .OrderBy(l => l.MaLop)
                .Select((l, index) => new LopHocRow
                {
                    Stt = index + 1,
                    MaLop = l.MaLop,
                    TenLop = l.TenLop,
                    Nganh = l.Nganh,
                    SiSo = l.SiSo
                })
                .ToList();

            BindPage(rows);
        }

        private void BindPage(List<LopHocRow> rows)
        {
            int totalRows = rows.Count;
            int totalPages = Math.Max(1, (int)Math.Ceiling(totalRows / (double)PageSize));

            if (this.currentPage > totalPages)
            {
                this.currentPage = totalPages;
            }

            List<LopHocRow> pageRows = rows
                .Skip((this.currentPage - 1) * PageSize)
                .Take(PageSize)
                .ToList();

            this.dgvLopHoc.DataSource = pageRows;
            ConfigureGridColumns();
            UpdatePaging(totalRows, totalPages);
        }

        private void ConfigureGridColumns()
        {
            if (this.dgvLopHoc.Columns.Count == 0)
            {
                return;
            }

            this.dgvLopHoc.Columns[nameof(LopHocRow.Stt)].HeaderText = "STT";
            this.dgvLopHoc.Columns[nameof(LopHocRow.MaLop)].HeaderText = "Mã lớp";
            this.dgvLopHoc.Columns[nameof(LopHocRow.TenLop)].HeaderText = "Tên lớp";
            this.dgvLopHoc.Columns[nameof(LopHocRow.Nganh)].HeaderText = "Ngành";
            this.dgvLopHoc.Columns[nameof(LopHocRow.SiSo)].HeaderText = "Sĩ số";

            if (!this.dgvLopHoc.Columns.Contains("Actions"))
            {
                DataGridViewButtonColumn actionColumn = new DataGridViewButtonColumn();
                actionColumn.Name = "Actions";
                actionColumn.HeaderText = "Hành động";
                actionColumn.Text = "Sửa / Xóa";
                actionColumn.UseColumnTextForButtonValue = true;
                this.dgvLopHoc.Columns.Add(actionColumn);
            }

            this.dgvLopHoc.Columns[nameof(LopHocRow.Stt)].FillWeight = 55;
            this.dgvLopHoc.Columns[nameof(LopHocRow.MaLop)].FillWeight = 130;
            this.dgvLopHoc.Columns[nameof(LopHocRow.TenLop)].FillWeight = 220;
            this.dgvLopHoc.Columns[nameof(LopHocRow.Nganh)].FillWeight = 180;
            this.dgvLopHoc.Columns[nameof(LopHocRow.SiSo)].FillWeight = 80;
            this.dgvLopHoc.Columns["Actions"].FillWeight = 130;

            this.dgvLopHoc.Columns[nameof(LopHocRow.Stt)].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dgvLopHoc.Columns[nameof(LopHocRow.SiSo)].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dgvLopHoc.Columns["Actions"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }

        private void UpdatePaging(int totalRows, int totalPages)
        {
            int from = totalRows == 0 ? 0 : ((this.currentPage - 1) * PageSize) + 1;
            int to = Math.Min(this.currentPage * PageSize, totalRows);
            this.lblPagingInfo.Text = string.Format("Hiển thị {0}-{1} / {2} lớp", from, to, totalRows);

            this.pnlPageButtons.Controls.Clear();
            AddPageButton("<", Math.Max(1, this.currentPage - 1), this.currentPage > 1);

            for (int page = 1; page <= totalPages; page++)
            {
                if (totalPages > 5 && page != 1 && page != totalPages && Math.Abs(page - this.currentPage) > 1)
                {
                    if (page == 2 || page == totalPages - 1)
                    {
                        AddEllipsis();
                    }

                    continue;
                }

                AddPageButton(page.ToString(), page, true);
            }

            AddPageButton(">", Math.Min(totalPages, this.currentPage + 1), this.currentPage < totalPages);
        }

        private void AddPageButton(string text, int page, bool enabled)
        {
            Button button = new Button();
            bool active = text == this.currentPage.ToString();
            button.BackColor = active ? Color.FromArgb(37, 99, 235) : Color.White;
            button.Cursor = enabled ? Cursors.Hand : Cursors.Default;
            button.Enabled = enabled;
            button.FlatAppearance.BorderColor = active ? Color.FromArgb(37, 99, 235) : Color.FromArgb(226, 232, 240);
            button.FlatStyle = FlatStyle.Flat;
            button.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button.ForeColor = active ? Color.White : Color.FromArgb(51, 65, 85);
            button.Margin = new Padding(3, 0, 3, 0);
            button.Size = new Size(34, 32);
            button.Text = text;
            button.Tag = page;
            button.Click += new EventHandler(this.PageButton_Click);
            this.pnlPageButtons.Controls.Add(button);
        }

        private void AddEllipsis()
        {
            Label label = new Label();
            label.ForeColor = Color.FromArgb(100, 116, 139);
            label.Margin = new Padding(3, 7, 3, 0);
            label.Size = new Size(20, 20);
            label.Text = "...";
            label.TextAlign = ContentAlignment.MiddleCenter;
            this.pnlPageButtons.Controls.Add(label);
        }

        private void PageButton_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;
            if (button == null)
            {
                return;
            }

            this.currentPage = Convert.ToInt32(button.Tag);
            ApplyFilter(false);
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            if (this.isLoading)
            {
                return;
            }

            using (FrmLopHocDialog dialog = new FrmLopHocDialog())
            {
                if (dialog.ShowDialog(this) != DialogResult.OK)
                {
                    return;
                }

                try
                {
                    LopHoc lopHoc = dialog.LopHoc;
                    if (this.lopHocRepository.Exists(lopHoc.MaLop))
                    {
                        MessageBox.Show("Mã lớp đã tồn tại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    this.lopHocRepository.Create(lopHoc);
                    RefreshData(false);
                    MessageBox.Show("Đã thêm lớp học thành công.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    ShowDataError("Không thể thêm lớp học.", ex);
                }
            }
        }

        private void EditLopHoc(string maLop)
        {
            LopHoc lopHoc = this.lopHocRepository.GetById(maLop);
            if (lopHoc == null)
            {
                MessageBox.Show("Không tìm thấy lớp học.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using (FrmLopHocDialog dialog = new FrmLopHocDialog(lopHoc))
            {
                if (dialog.ShowDialog(this) != DialogResult.OK)
                {
                    return;
                }

                try
                {
                    this.lopHocRepository.Update(dialog.LopHoc);
                    RefreshData(false);
                    MessageBox.Show("Đã cập nhật lớp học thành công.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    ShowDataError("Không thể cập nhật lớp học.", ex);
                }
            }
        }

        private void DeleteLopHoc(string maLop)
        {
            DialogResult result = MessageBox.Show(
                "Bạn có chắc muốn xóa lớp '" + maLop + "'?",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (result != DialogResult.Yes)
            {
                return;
            }

            try
            {
                this.lopHocRepository.Delete(maLop);
                RefreshData(false);
                MessageBox.Show("Đã xóa lớp học thành công.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                ShowDataError("Không thể xóa lớp học. Lớp có thể đang được tham chiếu bởi lịch thực hành.", ex);
            }
        }

        private void DgvLopHoc_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0 || this.dgvLopHoc.Columns[e.ColumnIndex].Name != "Actions")
            {
                return;
            }

            LopHocRow row = this.dgvLopHoc.Rows[e.RowIndex].DataBoundItem as LopHocRow;
            if (row == null)
            {
                return;
            }

            ContextMenuStrip menu = new ContextMenuStrip();
            ToolStripMenuItem editItem = new ToolStripMenuItem("Sửa");
            ToolStripMenuItem deleteItem = new ToolStripMenuItem("Xóa");
            editItem.Click += delegate { EditLopHoc(row.MaLop); };
            deleteItem.Click += delegate { DeleteLopHoc(row.MaLop); };
            menu.Items.Add(editItem);
            menu.Items.Add(deleteItem);

            Rectangle cellBounds = this.dgvLopHoc.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true);
            menu.Show(this.dgvLopHoc, cellBounds.Left, cellBounds.Bottom);
        }

        private void DgvLopHoc_CellMouseMove(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0 && this.dgvLopHoc.Columns[e.ColumnIndex].Name == "Actions")
            {
                this.dgvLopHoc.Cursor = Cursors.Hand;
                this.actionToolTip.SetToolTip(this.dgvLopHoc, "Sửa hoặc xóa lớp học");
                return;
            }

            this.dgvLopHoc.Cursor = Cursors.Default;
            this.actionToolTip.SetToolTip(this.dgvLopHoc, string.Empty);
        }

        private void DgvLopHoc_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            this.dgvLopHoc.Cursor = Cursors.Default;
            this.actionToolTip.SetToolTip(this.dgvLopHoc, string.Empty);
        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private static bool Contains(string source, string value)
        {
            return (source ?? string.Empty).IndexOf(value, StringComparison.OrdinalIgnoreCase) >= 0;
        }

        private static void ShowDataError(string message, Exception ex)
        {
            MessageBox.Show(message + "\n" + ex.Message, "Lỗi dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private class LopHocRow
        {
            public int Stt { get; set; }

            public string MaLop { get; set; }

            public string TenLop { get; set; }

            public string Nganh { get; set; }

            public int SiSo { get; set; }
        }

        private void lblTitle_Click(object sender, EventArgs e)
        {

        }
    }
}
