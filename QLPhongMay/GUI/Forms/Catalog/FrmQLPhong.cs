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
    public partial class FrmQLPhong : Form
    {
        private const int PageSize = 5;
        private readonly Color primaryColor = Color.FromArgb(37, 99, 235);
        private readonly Color borderColor = Color.FromArgb(226, 232, 240);
        private readonly Color inputBackColor = Color.FromArgb(248, 250, 252);
        private readonly ToolTip actionToolTip = new ToolTip();
        private List<PhongRow> phongMays = new List<PhongRow>();
        private List<CaOption> caOptions = new List<CaOption>();
        private int currentPage = 1;

        private Panel pnlRoot;
        private Button btnBack;
        private Label lblTitle;
        private Label lblSubtitle;
        private FlowLayoutPanel pnlStats;
        private Button btnCreate;
        private Panel pnlFilter;
        private TextBox txtSearch;
        private ComboBox cboStatus;
        private CheckBox chkPhongTrong;
        private DateTimePicker dtpNgay;
        private ComboBox cboCa;
        private ComboBox cboThu;
        private DataGridView dgvPhong;
        private Panel pnlPaging;
        private Label lblPagingInfo;
        private FlowLayoutPanel pnlPageButtons;
        private DataGridViewTextBoxColumn Actions;

        public FrmQLPhong()
        {
            InitializeComponent();
            Load += FrmQLPhong_Load;
            Resize += FrmQLPhong_Resize;
        }

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            DataGridViewCellStyle headerStyle = new DataGridViewCellStyle();
            DataGridViewCellStyle rowStyle = new DataGridViewCellStyle();
            DataGridViewTextBoxColumn colMaPhong = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn colTenPhong = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn colSucChua = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn colTrangThai = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn colCreatedAt = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn colUpdatedAt = new DataGridViewTextBoxColumn();

            pnlRoot = new Panel();
            btnBack = new Button();
            lblTitle = new Label();
            lblSubtitle = new Label();
            pnlStats = new FlowLayoutPanel();
            btnCreate = new Button();
            pnlFilter = new Panel();
            txtSearch = new TextBox();
            cboStatus = new ComboBox();
            chkPhongTrong = new CheckBox();
            dtpNgay = new DateTimePicker();
            cboCa = new ComboBox();
            cboThu = new ComboBox();
            dgvPhong = new DataGridView();
            Actions = new DataGridViewTextBoxColumn();
            pnlPaging = new Panel();
            lblPagingInfo = new Label();
            pnlPageButtons = new FlowLayoutPanel();

            pnlRoot.SuspendLayout();
            pnlFilter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvPhong).BeginInit();
            pnlPaging.SuspendLayout();
            SuspendLayout();

            pnlRoot.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            pnlRoot.BackColor = Color.Transparent;
            pnlRoot.Controls.Add(btnBack);
            pnlRoot.Controls.Add(lblTitle);
            pnlRoot.Controls.Add(lblSubtitle);
            pnlRoot.Controls.Add(pnlStats);
            pnlRoot.Controls.Add(btnCreate);
            pnlRoot.Controls.Add(pnlFilter);
            pnlRoot.Controls.Add(dgvPhong);
            pnlRoot.Controls.Add(pnlPaging);
            pnlRoot.Location = new Point(28, 24);
            pnlRoot.Size = new Size(1124, 808);

            btnBack.BackColor = Color.White;
            btnBack.Cursor = Cursors.Hand;
            btnBack.FlatAppearance.BorderColor = Color.FromArgb(226, 232, 240);
            btnBack.FlatStyle = FlatStyle.Flat;
            btnBack.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnBack.ForeColor = Color.FromArgb(51, 65, 85);
            btnBack.Location = new Point(0, 4);
            btnBack.Size = new Size(128, 46);
            btnBack.Text = "<  Quay lại";
            btnBack.UseVisualStyleBackColor = false;
            btnBack.Click += BtnBack_Click;

            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 21F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTitle.ForeColor = Color.FromArgb(15, 23, 42);
            lblTitle.Location = new Point(146, 0);
            lblTitle.Text = "Quản lý phòng máy";

            lblSubtitle.AutoSize = true;
            lblSubtitle.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblSubtitle.ForeColor = Color.FromArgb(100, 116, 139);
            lblSubtitle.Location = new Point(150, 48);
            lblSubtitle.Text = "Theo dõi và cập nhật thông tin phòng máy trong hệ thống";

            pnlStats.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            pnlStats.BackColor = Color.Transparent;
            pnlStats.Location = new Point(0, 82);
            pnlStats.Size = new Size(850, 88);
            pnlStats.WrapContents = false;

            btnCreate.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnCreate.BackColor = Color.FromArgb(37, 99, 235);
            btnCreate.Cursor = Cursors.Hand;
            btnCreate.FlatAppearance.BorderSize = 0;
            btnCreate.FlatAppearance.MouseDownBackColor = Color.FromArgb(29, 78, 216);
            btnCreate.FlatAppearance.MouseOverBackColor = Color.FromArgb(29, 78, 216);
            btnCreate.FlatStyle = FlatStyle.Flat;
            btnCreate.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnCreate.ForeColor = Color.White;
            btnCreate.Location = new Point(930, 92);
            btnCreate.Size = new Size(194, 46);
            btnCreate.Text = "+  Thêm phòng";
            btnCreate.UseVisualStyleBackColor = false;
            btnCreate.Click += BtnCreate_Click;

            pnlFilter.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            pnlFilter.BackColor = Color.White;
            pnlFilter.Controls.Add(txtSearch);
            pnlFilter.Controls.Add(cboStatus);
            pnlFilter.Controls.Add(dtpNgay);
            pnlFilter.Controls.Add(cboCa);
            pnlFilter.Location = new Point(0, 188);
            pnlFilter.Size = new Size(1124, 66);
            pnlFilter.Paint += PanelBorder_Paint;

            txtSearch.BackColor = Color.FromArgb(248, 250, 252);
            txtSearch.BorderStyle = BorderStyle.FixedSingle;
            txtSearch.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtSearch.ForeColor = Color.FromArgb(100, 116, 139);
            txtSearch.Location = new Point(22, 18);
            txtSearch.Size = new Size(360, 30);
            txtSearch.Tag = "Tìm kiếm theo tên phòng";
            txtSearch.Text = "Tìm kiếm theo tên phòng";
            txtSearch.GotFocus += Placeholder_GotFocus;
            txtSearch.LostFocus += Placeholder_LostFocus;
            txtSearch.TextChanged += FilterChanged;

            cboStatus.DropDownStyle = ComboBoxStyle.DropDownList;
            cboStatus.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            cboStatus.Location = new Point(402, 18);
            cboStatus.Size = new Size(190, 31);
            cboStatus.Items.AddRange(new object[] { "Tất cả trạng thái", "Hoạt động", "Bảo trì", "Đóng" });
            cboStatus.SelectedIndex = 0;
            cboStatus.SelectedIndexChanged += FilterChanged;

            chkPhongTrong.AutoSize = true;
            chkPhongTrong.BackColor = Color.White;
            chkPhongTrong.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold, GraphicsUnit.Point, 0);
            chkPhongTrong.ForeColor = Color.FromArgb(51, 65, 85);
            chkPhongTrong.Location = new Point(22, 58);
            chkPhongTrong.Text = "Chỉ phòng trống";
            chkPhongTrong.Visible = false;
            chkPhongTrong.CheckedChanged += FilterChanged;

            dtpNgay.CustomFormat = "dd/MM/yyyy";
            dtpNgay.Format = DateTimePickerFormat.Custom;
            dtpNgay.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dtpNgay.Location = new Point(612, 18);
            dtpNgay.Size = new Size(150, 30);
            dtpNgay.ValueChanged += FilterChanged;

            cboCa.DropDownStyle = ComboBoxStyle.DropDownList;
            cboCa.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            cboCa.Location = new Point(782, 18);
            cboCa.Size = new Size(250, 31);
            cboCa.SelectedIndexChanged += FilterChanged;

            cboThu.DropDownStyle = ComboBoxStyle.DropDownList;
            cboThu.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            cboThu.Location = new Point(595, 53);
            cboThu.Size = new Size(160, 31);
            cboThu.Items.AddRange(new object[] { "Tất cả thứ", "Thứ 2", "Thứ 3", "Thứ 4", "Thứ 5", "Thứ 6", "Thứ 7", "Chủ nhật" });
            cboThu.SelectedIndex = 0;
            cboThu.Visible = false;
            cboThu.SelectedIndexChanged += FilterChanged;

            dgvPhong.AllowUserToAddRows = false;
            dgvPhong.AllowUserToDeleteRows = false;
            dgvPhong.AllowUserToResizeRows = false;
            dgvPhong.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvPhong.AutoGenerateColumns = false;
            dgvPhong.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvPhong.BackgroundColor = Color.White;
            dgvPhong.BorderStyle = BorderStyle.None;
            dgvPhong.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvPhong.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            headerStyle.BackColor = Color.FromArgb(248, 250, 252);
            headerStyle.Font = new Font("Segoe UI", 9.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            headerStyle.ForeColor = Color.FromArgb(71, 85, 105);
            headerStyle.Padding = new Padding(10, 0, 10, 0);
            headerStyle.SelectionBackColor = Color.FromArgb(248, 250, 252);
            headerStyle.SelectionForeColor = Color.FromArgb(71, 85, 105);
            dgvPhong.ColumnHeadersDefaultCellStyle = headerStyle;
            dgvPhong.ColumnHeadersHeight = 48;
            dgvPhong.Columns.AddRange(new DataGridViewColumn[] { colMaPhong, colTenPhong, colSucChua, colTrangThai, colCreatedAt, colUpdatedAt, Actions });
            colMaPhong.DataPropertyName = "MaPhong"; colMaPhong.FillWeight = 80F; colMaPhong.MinimumWidth = 90; colMaPhong.HeaderText = "Mã phòng"; colMaPhong.Name = "colMaPhong"; colMaPhong.ReadOnly = true;
            colTenPhong.DataPropertyName = "TenPhong"; colTenPhong.FillWeight = 210F; colTenPhong.MinimumWidth = 190; colTenPhong.HeaderText = "Tên phòng"; colTenPhong.Name = "colTenPhong"; colTenPhong.ReadOnly = true;
            colSucChua.DataPropertyName = "SucChua"; colSucChua.FillWeight = 90F; colSucChua.MinimumWidth = 90; colSucChua.HeaderText = "Sức chứa"; colSucChua.Name = "colSucChua"; colSucChua.ReadOnly = true;
            colTrangThai.DataPropertyName = "TrangThaiText"; colTrangThai.FillWeight = 135F; colTrangThai.MinimumWidth = 135; colTrangThai.HeaderText = "Trạng thái"; colTrangThai.Name = "colTrangThai"; colTrangThai.ReadOnly = true;
            colCreatedAt.DataPropertyName = "CreatedAtText"; colCreatedAt.FillWeight = 120F; colCreatedAt.HeaderText = "Ngày tạo"; colCreatedAt.Name = "colCreatedAt"; colCreatedAt.ReadOnly = true;
            colCreatedAt.Visible = false;
            colUpdatedAt.DataPropertyName = "LichText"; colUpdatedAt.FillWeight = 160F; colUpdatedAt.MinimumWidth = 160; colUpdatedAt.HeaderText = "Lịch theo ngày/ca"; colUpdatedAt.Name = "colLichText"; colUpdatedAt.ReadOnly = true;
            Actions.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            Actions.Width = 150;
            Actions.FillWeight = 116F;
            Actions.HeaderText = "Hành động";
            Actions.MinimumWidth = 110;
            Actions.Name = "Actions";
            Actions.ReadOnly = true;
            rowStyle.BackColor = Color.White;
            rowStyle.Font = new Font("Segoe UI", 9.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            rowStyle.ForeColor = Color.FromArgb(30, 41, 59);
            rowStyle.Padding = new Padding(10, 0, 10, 0);
            rowStyle.SelectionBackColor = Color.FromArgb(239, 246, 255);
            rowStyle.SelectionForeColor = Color.FromArgb(15, 23, 42);
            dgvPhong.DefaultCellStyle = rowStyle;
            dgvPhong.EnableHeadersVisualStyles = false;
            dgvPhong.GridColor = Color.FromArgb(241, 245, 249);
            dgvPhong.Location = new Point(0, 270);
            dgvPhong.MultiSelect = false;
            dgvPhong.ReadOnly = true;
            dgvPhong.RowHeadersVisible = false;
            dgvPhong.RowTemplate.Height = 44;
            dgvPhong.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvPhong.Size = new Size(1124, 484);
            dgvPhong.CellClick += DgvPhong_CellClick;
            dgvPhong.CellMouseLeave += DgvPhong_CellMouseLeave;
            dgvPhong.CellMouseMove += DgvPhong_CellMouseMove;
            dgvPhong.CellPainting += DgvPhong_CellPainting;

            pnlPaging.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            pnlPaging.BackColor = Color.White;
            pnlPaging.Controls.Add(lblPagingInfo);
            pnlPaging.Controls.Add(pnlPageButtons);
            pnlPaging.Location = new Point(0, 762);
            pnlPaging.Size = new Size(1124, 46);
            pnlPaging.Paint += PanelBorder_Paint;
            pnlPaging.Resize += PnlPaging_Resize;

            lblPagingInfo.AutoSize = true;
            lblPagingInfo.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblPagingInfo.ForeColor = Color.FromArgb(100, 116, 139);
            lblPagingInfo.Location = new Point(18, 14);
            lblPagingInfo.Text = "Đang tải dữ liệu...";

            pnlPageButtons.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            pnlPageButtons.BackColor = Color.Transparent;
            pnlPageButtons.Location = new Point(900, 7);
            pnlPageButtons.Size = new Size(206, 32);
            pnlPageButtons.WrapContents = false;

            AutoScaleDimensions = new SizeF(8F, 16F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(245, 247, 251);
            ClientSize = new Size(1180, 856);
            Controls.Add(pnlRoot);
            Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            MinimumSize = new Size(1060, 720);
            Name = "FrmQLPhong";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Quản lý phòng máy";

            pnlRoot.ResumeLayout(false);
            pnlRoot.PerformLayout();
            pnlFilter.ResumeLayout(false);
            pnlFilter.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvPhong).EndInit();
            pnlPaging.ResumeLayout(false);
            pnlPaging.PerformLayout();
            ResumeLayout(false);
        }

        private void FrmQLPhong_Load(object sender, EventArgs e)
        {
            LayoutResponsive();
            RefreshRooms(true);
        }

        private void FrmQLPhong_Resize(object sender, EventArgs e)
        {
            LayoutResponsive();
        }

        private void LayoutResponsive()
        {
            if (pnlRoot == null)
            {
                return;
            }

            int rootWidth = pnlRoot.ClientSize.Width;
            int rootHeight = pnlRoot.ClientSize.Height;
            if (rootWidth <= 0 || rootHeight <= 0)
            {
                return;
            }

            btnBack.Location = new Point(0, 4);
            lblTitle.Location = new Point(btnBack.Right + 18, 0);
            lblSubtitle.Location = new Point(lblTitle.Left + 4, 48);
            lblSubtitle.MaximumSize = new Size(Math.Max(320, rootWidth - lblSubtitle.Left - 24), 0);
            btnCreate.Location = new Point(rootWidth - btnCreate.Width, 92);

            pnlStats.Location = new Point(0, 82);
            pnlStats.Size = new Size(Math.Max(520, btnCreate.Left - 28), 88);
            LayoutStatCards();

            pnlFilter.Location = new Point(0, 188);
            pnlFilter.Size = new Size(rootWidth, 66);
            LayoutFilterControls();

            int gridTop = pnlFilter.Bottom + 16;
            int pagingTop = rootHeight - 46;
            dgvPhong.Location = new Point(0, gridTop);
            dgvPhong.Size = new Size(rootWidth, Math.Max(260, pagingTop - gridTop - 8));

            pnlPaging.Location = new Point(0, pagingTop);
            pnlPaging.Size = new Size(rootWidth, 46);
            pnlPageButtons.Left = pnlPaging.Width - pnlPageButtons.Width - 18;
        }

        private void LayoutStatCards()
        {
            int count = pnlStats.Controls.Count;
            if (count == 0)
            {
                return;
            }

            int gap = 12;
            int cardWidth = Math.Max(170, (pnlStats.ClientSize.Width - gap * (count - 1)) / count);
            int left = 0;
            foreach (Control card in pnlStats.Controls)
            {
                card.Location = new Point(left, 0);
                card.Size = new Size(cardWidth, 78);
                left += cardWidth + gap;
            }
        }

        private void LayoutFilterControls()
        {
            int width = pnlFilter.ClientSize.Width;
            int left = 18;
            int gap = 16;
            int y = 18;

            int statusWidth = 180;
            int dateWidth = 140;
            int shiftWidth = 160;
            int dayWidth = 140;
            int checkWidth = chkPhongTrong.Width;
            int searchWidth = Math.Max(260, width - left * 2 - statusWidth - dateWidth - shiftWidth - dayWidth - checkWidth - gap * 5);

            txtSearch.Location = new Point(left, y);
            txtSearch.Size = new Size(searchWidth, 30);
            cboStatus.Location = new Point(txtSearch.Right + gap, y);
            cboStatus.Size = new Size(statusWidth, 30);
            chkPhongTrong.Location = new Point(cboStatus.Right + gap, y + 4);
            dtpNgay.Location = new Point(chkPhongTrong.Right + gap, y);
            dtpNgay.Size = new Size(dateWidth, 30);
            cboCa.Location = new Point(dtpNgay.Right + gap, y);
            cboCa.Size = new Size(shiftWidth, 30);
            cboThu.Location = new Point(cboCa.Right + gap, y);
            cboThu.Size = new Size(Math.Max(120, width - cboCa.Right - gap - left), 30);
        }

        private void RefreshRooms(bool resetPage)
        {
            LoadLookupData();
            LoadRoomData();
            RenderStats();
            LoadRooms(resetPage);
        }

        private void LoadLookupData()
        {
            try
            {
                using (AppDbContext db = new AppDbContext())
                {
                    caOptions = db.CaHocs.AsNoTracking()
                        .OrderBy(item => item.GioBatDau)
                        .ThenBy(item => item.MaCa)
                        .ToList()
                        .Select(item => new CaOption
                        {
                            MaCa = item.MaCa,
                            Text = item.TenCa + " (" + item.GioBatDau.ToString(@"hh\:mm") + " - " + item.GioKetThuc.ToString(@"hh\:mm") + ")"
                        })
                        .ToList();
                }

                int oldIndex = cboCa.SelectedIndex;
                cboCa.Items.Clear();
                cboCa.Items.Add(new CaOption { MaCa = 0, Text = "Tất cả ca" });
                foreach (CaOption option in caOptions)
                {
                    cboCa.Items.Add(option);
                }
                cboCa.DisplayMember = "Text";
                cboCa.ValueMember = "MaCa";
                cboCa.SelectedIndex = oldIndex >= 0 && oldIndex < cboCa.Items.Count ? oldIndex : 0;
            }
            catch (Exception ex)
            {
                caOptions = new List<CaOption>();
                ShowDataError("Không thể tải danh sách ca học.", ex);
            }
        }

        private void LoadRoomData()
        {
            try
            {
                using (AppDbContext db = new AppDbContext())
                {
                    phongMays = db.PhongMays.AsNoTracking()
                        .OrderBy(item => item.MaPhong)
                        .ToList()
                        .Select(ToPhongRow)
                        .ToList();
                }
            }
            catch (Exception ex)
            {
                phongMays = new List<PhongRow>();
                ShowDataError("Không thể tải dữ liệu phòng máy.", ex);
            }
        }

        private void RenderStats()
        {
            pnlStats.Controls.Clear();
            pnlStats.Controls.Add(CreateStatCard("Σ", "Tổng phòng", phongMays.Count.ToString(CultureInfo.InvariantCulture), primaryColor));
            pnlStats.Controls.Add(CreateStatCard("H", "Hoạt động", phongMays.Count(item => NormalizeStatus(item.TrangThai) == "HoatDong").ToString(CultureInfo.InvariantCulture), Color.FromArgb(22, 163, 74)));
            pnlStats.Controls.Add(CreateStatCard("B", "Bảo trì", phongMays.Count(item => NormalizeStatus(item.TrangThai) == "BaoTri").ToString(CultureInfo.InvariantCulture), Color.FromArgb(245, 158, 11)));
            LayoutStatCards();
        }

        private Panel CreateStatCard(string iconText, string title, string value, Color accent)
        {
            Panel card = new Panel();
            Label icon = new Label();
            Label number = new Label();
            Label caption = new Label();

            card.BackColor = Color.White;
            card.Margin = new Padding(0, 0, 16, 0);
            card.Size = new Size(250, 78);
            card.Paint += PanelBorder_Paint;

            icon.BackColor = Color.FromArgb(248, 250, 252);
            icon.Font = new Font("Segoe UI", 13F, FontStyle.Bold, GraphicsUnit.Point, 0);
            icon.ForeColor = accent;
            icon.Location = new Point(18, 17);
            icon.Size = new Size(42, 42);
            icon.Text = iconText;
            icon.TextAlign = ContentAlignment.MiddleCenter;
            icon.Paint += RoundLabel_Paint;

            number.AutoSize = true;
            number.Font = new Font("Segoe UI", 16F, FontStyle.Bold, GraphicsUnit.Point, 0);
            number.ForeColor = Color.FromArgb(15, 23, 42);
            number.Location = new Point(78, 12);
            number.Text = value;

            caption.AutoSize = true;
            caption.Font = new Font("Segoe UI", 9.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            caption.ForeColor = Color.FromArgb(100, 116, 139);
            caption.Location = new Point(80, 46);
            caption.Text = title;

            card.Controls.Add(icon);
            card.Controls.Add(number);
            card.Controls.Add(caption);
            return card;
        }

        private void LoadRooms(bool resetPage)
        {
            ApplyScheduleState();
            List<PhongRow> filtered = GetFilteredRooms().ToList();
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

            List<PhongRow> pageItems = filtered
                .Skip((currentPage - 1) * PageSize)
                .Take(PageSize)
                .ToList();

            dgvPhong.DataSource = pageItems;
            int start = totalItems == 0 ? 0 : ((currentPage - 1) * PageSize) + 1;
            int end = Math.Min(currentPage * PageSize, totalItems);
            lblPagingInfo.Text = string.Format("Hiển thị {0}-{1} trong {2} kết quả", start, end, totalItems);
            RenderPaging(totalPages);
        }

        private void ApplyScheduleState()
        {
            DateTime? date = dtpNgay.Value.Date;
            CaOption selectedCaOption = cboCa.SelectedItem as CaOption;
            int selectedCa = selectedCaOption == null ? 0 : selectedCaOption.MaCa;

            try
            {
                using (AppDbContext db = new AppDbContext())
                {
                    IQueryable<LichThucHanh> query = db.LichThucHanhs.AsNoTracking()
                        .Where(item => DbFunctions.TruncateTime(item.NgayThucHanh) == date);

                    if (selectedCa > 0)
                    {
                        query = query.Where(item => item.MaCa == selectedCa);
                    }

                    HashSet<int> busyRooms = new HashSet<int>(query.Select(item => item.MaPhong).ToList());
                    foreach (PhongRow room in phongMays)
                    {
                        room.DaCoLich = busyRooms.Contains(room.MaPhong);
                        room.LichText = room.DaCoLich ? "Đã có lịch" : "Còn trống";
                    }
                }
            }
            catch
            {
                foreach (PhongRow room in phongMays)
                {
                    room.DaCoLich = false;
                    room.LichText = "Không rõ";
                }
            }
        }

        private IEnumerable<PhongRow> GetFilteredRooms()
        {
            IEnumerable<PhongRow> source = phongMays;
            string keyword = (GetInputText(txtSearch) ?? string.Empty).ToLowerInvariant();

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                source = source.Where(item =>
                    ContainsKeyword(item.MaPhong.ToString(CultureInfo.InvariantCulture), keyword) ||
                    ContainsKeyword(item.TenPhong, keyword));
            }

            string selectedStatus = GetSelectedStatusValue();
            if (!string.IsNullOrEmpty(selectedStatus))
            {
                source = source.Where(item => NormalizeStatus(item.TrangThai) == selectedStatus);
            }

            if (chkPhongTrong.Checked)
            {
                source = FilterEmptyRooms(source);
            }

            return source.OrderBy(item => item.MaPhong);
        }

        private IEnumerable<PhongRow> FilterEmptyRooms(IEnumerable<PhongRow> source)
        {
            DateTime? date = dtpNgay.Value.Date;
            CaOption selectedCaOption = cboCa.SelectedItem as CaOption;
            int selectedCa = selectedCaOption == null ? 0 : selectedCaOption.MaCa;
            int selectedThu = cboThu.SelectedIndex;

            using (AppDbContext db = new AppDbContext())
            {
                IQueryable<LichThucHanh> query = db.LichThucHanhs.AsNoTracking();
                query = query.Where(item => DbFunctions.TruncateTime(item.NgayThucHanh) == date);

                if (selectedCa > 0)
                {
                    query = query.Where(item => item.MaCa == selectedCa);
                }

                if (selectedThu > 0)
                {
                    int thuValue = selectedThu == 7 ? 1 : selectedThu + 1;
                    query = query.Where(item => item.ThuTrongTuan == thuValue);
                }

                HashSet<int> busyRooms = new HashSet<int>(query.Select(item => item.MaPhong).ToList());
                return source.Where(item => !busyRooms.Contains(item.MaPhong)).ToList();
            }
        }

        private void BtnCreate_Click(object sender, EventArgs e)
        {
            RoomDialogResult result;
            if (!ShowRoomPopup("Thêm phòng máy", null, false, out result))
            {
                return;
            }

            try
            {
                using (AppDbContext db = new AppDbContext())
                {
                    DateTime now = DateTime.Now;
                    db.PhongMays.Add(new PhongMay
                    {
                        TenPhong = result.TenPhong,
                        SucChua = result.SucChua,
                        TrangThai = result.TrangThai,
                        CreatedAt = now,
                        UpdatedAt = now
                    });
                    db.SaveChanges();
                }

                RefreshRooms(true);
                MessageBox.Show("Đã thêm phòng máy thành công.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                ShowDataError("Không thể thêm phòng máy.", ex);
            }
        }

        private void EditRoom(PhongRow row)
        {
            RoomDialogResult result;
            if (!ShowRoomPopup("Sửa phòng máy", row, false, out result))
            {
                return;
            }

            try
            {
                using (AppDbContext db = new AppDbContext())
                {
                    PhongMay room = db.PhongMays.FirstOrDefault(item => item.MaPhong == result.MaPhong);
                    if (room == null)
                    {
                        MessageBox.Show("Không tìm thấy phòng cần cập nhật.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    room.TenPhong = result.TenPhong;
                    room.SucChua = result.SucChua;
                    room.TrangThai = result.TrangThai;
                    room.UpdatedAt = DateTime.Now;
                    db.SaveChanges();
                }

                RefreshRooms(false);
                MessageBox.Show("Đã cập nhật phòng máy thành công.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                ShowDataError("Không thể cập nhật phòng máy.", ex);
            }
        }

        private void ViewRoom(PhongRow row)
        {
            RoomDialogResult ignored;
            ShowRoomPopup("Chi tiết phòng máy", row, true, out ignored);
        }

        private void DeleteRoom(int maPhong)
        {
            if (MessageBox.Show("Bạn có chắc muốn xóa phòng '" + maPhong + "'?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
            {
                return;
            }

            try
            {
                using (AppDbContext db = new AppDbContext())
                {
                    if (db.LichThucHanhs.Any(item => item.MaPhong == maPhong))
                    {
                        MessageBox.Show("Không thể xóa phòng đang có lịch thực hành.", "Không thể xóa", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    PhongMay room = db.PhongMays.FirstOrDefault(item => item.MaPhong == maPhong);
                    if (room == null)
                    {
                        MessageBox.Show("Không tìm thấy phòng cần xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    db.PhongMays.Remove(room);
                    db.SaveChanges();
                }

                RefreshRooms(false);
                MessageBox.Show("Đã xóa phòng máy thành công.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                ShowDataError("Không thể xóa phòng máy.", ex);
            }
        }

        private bool ShowRoomPopup(string title, PhongRow row, bool readOnly, out RoomDialogResult result)
        {
            result = new RoomDialogResult();
            RoomDialogResult localResult = new RoomDialogResult();

            using (Form dialog = new Form())
            using (Label lblTitleDialog = new Label())
            using (Label lblSubtitleDialog = new Label())
            using (Label lblMaPhong = new Label())
            using (Label lblTenPhong = new Label())
            using (Label lblSucChua = new Label())
            using (Label lblTrangThai = new Label())
            using (TextBox txtMaPhong = new TextBox())
            using (TextBox txtTenPhong = new TextBox())
            using (TextBox txtSucChua = new TextBox())
            using (ComboBox cboTrangThai = new ComboBox())
            using (Button btnSave = new Button())
            using (Button btnCancel = new Button())
            {
                dialog.Text = title;
                dialog.StartPosition = FormStartPosition.CenterParent;
                dialog.FormBorderStyle = FormBorderStyle.FixedDialog;
                dialog.MaximizeBox = false;
                dialog.MinimizeBox = false;
                dialog.ShowInTaskbar = false;
                dialog.ClientSize = new Size(520, 390);
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
                lblSubtitleDialog.Text = "Thông tin phòng máy trong hệ thống";

                bool isCreate = row == null;
                ConfigurePopupLabel(lblMaPhong, "Mã phòng", 34, 116);
                ConfigurePopupInput(txtMaPhong, 34, 146);
                ConfigurePopupLabel(lblTenPhong, "Tên phòng", isCreate ? 34 : 268, 116);
                ConfigurePopupInput(txtTenPhong, isCreate ? 34 : 268, 146);
                ConfigurePopupLabel(lblSucChua, "Sức chứa", 34, 216);
                ConfigurePopupInput(txtSucChua, 34, 246);
                ConfigurePopupLabel(lblTrangThai, "Trạng thái", 268, 216);
                ConfigureComboBox(cboTrangThai, 268, 246, 218);
                cboTrangThai.Items.AddRange(new object[] { "Hoạt động", "Bảo trì", "Đóng" });
                lblMaPhong.Visible = !isCreate;
                txtMaPhong.Visible = !isCreate;

                if (row != null)
                {
                    txtMaPhong.Text = row.MaPhong.ToString(CultureInfo.InvariantCulture);
                    txtTenPhong.Text = row.TenPhong;
                    txtSucChua.Text = row.SucChua.ToString(CultureInfo.InvariantCulture);
                    cboTrangThai.SelectedItem = ToStatusText(row.TrangThai);
                }
                else
                {
                    cboTrangThai.SelectedIndex = 0;
                }

                txtMaPhong.ReadOnly = true;
                txtTenPhong.ReadOnly = readOnly;
                txtSucChua.ReadOnly = readOnly;
                cboTrangThai.Enabled = !readOnly;
                txtMaPhong.BackColor = Color.FromArgb(241, 245, 249);

                btnSave.BackColor = readOnly ? Color.White : Color.FromArgb(37, 99, 235);
                btnSave.FlatAppearance.BorderColor = Color.FromArgb(226, 232, 240);
                btnSave.FlatStyle = FlatStyle.Flat;
                btnSave.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
                btnSave.ForeColor = readOnly ? Color.FromArgb(51, 65, 85) : Color.White;
                btnSave.Location = readOnly ? new Point(380, 310) : new Point(284, 310);
                btnSave.Size = new Size(106, 42);
                btnSave.Text = readOnly ? "Đóng" : "Lưu";

                btnCancel.BackColor = Color.White;
                btnCancel.FlatAppearance.BorderColor = Color.FromArgb(226, 232, 240);
                btnCancel.FlatStyle = FlatStyle.Flat;
                btnCancel.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
                btnCancel.ForeColor = Color.FromArgb(51, 65, 85);
                btnCancel.Location = new Point(400, 310);
                btnCancel.Size = new Size(86, 42);
                btnCancel.Text = "Hủy";
                btnCancel.DialogResult = DialogResult.Cancel;
                btnCancel.Visible = !readOnly;

                dialog.Controls.AddRange(new Control[] { lblTitleDialog, lblSubtitleDialog, lblMaPhong, txtMaPhong, lblTenPhong, txtTenPhong, lblSucChua, txtSucChua, lblTrangThai, cboTrangThai, btnSave, btnCancel });
                dialog.AcceptButton = btnSave;
                dialog.CancelButton = btnCancel;

                btnSave.Click += delegate
                {
                    if (readOnly)
                    {
                        dialog.DialogResult = DialogResult.Cancel;
                        return;
                    }

                    int capacity;
                    if (string.IsNullOrWhiteSpace(txtTenPhong.Text))
                    {
                        MessageBox.Show("Vui lòng nhập tên phòng.", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtTenPhong.Focus();
                        return;
                    }

                    if (!int.TryParse(txtSucChua.Text.Trim(), NumberStyles.Integer, CultureInfo.InvariantCulture, out capacity) || capacity <= 0)
                    {
                        MessageBox.Show("Sức chứa phải là số nguyên dương.", "Dữ liệu không hợp lệ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtSucChua.Focus();
                        return;
                    }

                    localResult.MaPhong = row == null ? 0 : row.MaPhong;
                    localResult.TenPhong = txtTenPhong.Text.Trim();
                    localResult.SucChua = capacity;
                    localResult.TrangThai = ToStatusValue(Convert.ToString(cboTrangThai.SelectedItem));
                    dialog.DialogResult = DialogResult.OK;
                };

                bool accepted = dialog.ShowDialog(this) == DialogResult.OK;
                if (accepted)
                {
                    result = localResult;
                }

                return accepted;
            }
        }

        private void DgvPhong_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0 || dgvPhong.Columns[e.ColumnIndex].Name != "Actions")
            {
                return;
            }

            PhongRow row = dgvPhong.Rows[e.RowIndex].DataBoundItem as PhongRow;
            if (row == null)
            {
                return;
            }

            Rectangle cellBounds = dgvPhong.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false);
            int mouseX = dgvPhong.PointToClient(Cursor.Position).X - cellBounds.Left;
            string action = GetActionFromMouseX(cellBounds.Width, mouseX);

            if (action == "View")
            {
                ViewRoom(row);
            }
            else if (action == "Edit")
            {
                EditRoom(row);
            }
            else if (action == "Delete")
            {
                DeleteRoom(row.MaPhong);
            }
        }

        private void DgvPhong_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
            {
                return;
            }

            string columnName = dgvPhong.Columns[e.ColumnIndex].Name;
            if (columnName == "colTrangThai")
            {
                string status = Convert.ToString(e.Value);
                PaintBadgeCell(e, status, GetStatusColor(status), GetStatusBackColor(status));
                return;
            }

            if (columnName == "colLichText")
            {
                string lichText = Convert.ToString(e.Value);
                Color foreColor = lichText == "Còn trống" ? Color.FromArgb(22, 163, 74) : Color.FromArgb(220, 38, 38);
                Color backColor = lichText == "Còn trống" ? Color.FromArgb(240, 253, 244) : Color.FromArgb(254, 242, 242);
                PaintBadgeCell(e, lichText, foreColor, backColor);
                return;
            }

            if (columnName == "Actions")
            {
                PaintActionsCell(e);
            }
        }

        private void PaintBadgeCell(DataGridViewCellPaintingEventArgs e, string text, Color foreColor, Color backColor)
        {
            e.Handled = true;
            e.PaintBackground(e.CellBounds, true);
            Size textSize = TextRenderer.MeasureText(text, dgvPhong.DefaultCellStyle.Font);
            Rectangle badgeBounds = new Rectangle(e.CellBounds.Left + 16, e.CellBounds.Top + 8, textSize.Width + 26, 28);
            using (GraphicsPath path = CreateRoundRectanglePath(badgeBounds, 14))
            using (SolidBrush backBrush = new SolidBrush(backColor))
            using (SolidBrush textBrush = new SolidBrush(foreColor))
            using (StringFormat format = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center })
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                e.Graphics.FillPath(backBrush, path);
                e.Graphics.DrawString(text, dgvPhong.DefaultCellStyle.Font, textBrush, badgeBounds, format);
            }
        }

        private void PaintActionsCell(DataGridViewCellPaintingEventArgs e)
        {
            e.Handled = true;
            e.PaintBackground(e.CellBounds, true);
            string[] icons = { "\uE890", "\uE70F", "\uE74D" };
            Color[] colors = { Color.FromArgb(71, 85, 105), Color.FromArgb(37, 99, 235), Color.FromArgb(220, 38, 38) };
            int iconSize = 30;
            int totalWidth = (iconSize * icons.Length) + 12;
            int startX = e.CellBounds.Left + 18;
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

        private void DgvPhong_CellMouseMove(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0 || dgvPhong.Columns[e.ColumnIndex].Name != "Actions")
            {
                dgvPhong.Cursor = Cursors.Default;
                actionToolTip.SetToolTip(dgvPhong, string.Empty);
                return;
            }

            string action = GetActionFromMouseX(dgvPhong.Columns[e.ColumnIndex].Width, e.X);
            string tip = action == "View" ? "Xem chi tiết" : action == "Edit" ? "Sửa phòng" : action == "Delete" ? "Xóa phòng" : string.Empty;
            dgvPhong.Cursor = string.IsNullOrEmpty(tip) ? Cursors.Default : Cursors.Hand;
            actionToolTip.SetToolTip(dgvPhong, tip);
        }

        private void DgvPhong_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            dgvPhong.Cursor = Cursors.Default;
            actionToolTip.SetToolTip(dgvPhong, string.Empty);
        }

        private void FilterChanged(object sender, EventArgs e)
        {
            LoadRooms(true);
        }

        private void RenderPaging(int totalPages)
        {
            pnlPageButtons.Controls.Clear();
            AddPageButton("<", currentPage > 1, currentPage - 1, false);
            int startPage = Math.Max(1, currentPage - 2);
            int endPage = Math.Min(totalPages, startPage + 4);
            startPage = Math.Max(1, endPage - 4);
            for (int page = startPage; page <= endPage; page++)
            {
                AddPageButton(page.ToString(CultureInfo.InvariantCulture), true, page, page == currentPage);
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
            button.BackColor = active ? Color.FromArgb(37, 99, 235) : enabled ? Color.White : Color.FromArgb(209, 213, 219);
            button.ForeColor = active ? Color.White : Color.FromArgb(100, 116, 139);
            if (enabled && !active)
            {
                button.Click += PageButton_Click;
            }
            pnlPageButtons.Controls.Add(button);
        }

        private void PageButton_Click(object sender, EventArgs e)
        {
            currentPage = Convert.ToInt32(((Control)sender).Tag, CultureInfo.InvariantCulture);
            LoadRooms(false);
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
            pnlPageButtons.Left = pnlPaging.Width - pnlPageButtons.Width - 18;
        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void PanelBorder_Paint(object sender, PaintEventArgs e)
        {
            Control control = (Control)sender;
            using (Pen pen = new Pen(Color.FromArgb(226, 232, 240)))
            {
                e.Graphics.DrawRectangle(pen, new Rectangle(0, 0, control.Width - 1, control.Height - 1));
            }
        }

        private void RoundLabel_Paint(object sender, PaintEventArgs e)
        {
            Label label = (Label)sender;
            using (GraphicsPath path = CreateRoundRectanglePath(label.ClientRectangle, 12))
            {
                label.Region = new Region(path);
            }
        }

        private static void ConfigureInput(TextBox textBox, string placeholder, int x, int y, int width)
        {
            textBox.BackColor = Color.FromArgb(248, 250, 252);
            textBox.BorderStyle = BorderStyle.FixedSingle;
            textBox.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            textBox.ForeColor = Color.FromArgb(100, 116, 139);
            textBox.Location = new Point(x, y);
            textBox.Size = new Size(width, 30);
            textBox.Tag = placeholder;
            textBox.Text = string.Empty;
            textBox.GotFocus += Placeholder_GotFocus;
            textBox.LostFocus += Placeholder_LostFocus;
        }

        private static void ConfigureComboBox(ComboBox comboBox, int x, int y, int width)
        {
            comboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            comboBox.Location = new Point(x, y);
            comboBox.Size = new Size(width, 31);
        }

        private static void ConfigureTextColumn(DataGridViewTextBoxColumn column, string propertyName, string headerText, float fillWeight)
        {
            column.DataPropertyName = propertyName;
            column.FillWeight = fillWeight;
            column.HeaderText = headerText;
            column.Name = "col" + propertyName;
            column.ReadOnly = true;
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

        private static void Placeholder_GotFocus(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (textBox.Text == Convert.ToString(textBox.Tag))
            {
                textBox.Text = string.Empty;
                textBox.ForeColor = Color.FromArgb(15, 23, 42);
            }
        }

        private static void Placeholder_LostFocus(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;
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

        private static PhongRow ToPhongRow(PhongMay item)
        {
            return new PhongRow
            {
                MaPhong = item.MaPhong,
                TenPhong = item.TenPhong ?? string.Empty,
                SucChua = item.SucChua,
                TrangThai = item.TrangThai ?? string.Empty,
                TrangThaiText = ToStatusText(item.TrangThai),
                CreatedAt = item.CreatedAt,
                UpdatedAt = item.UpdatedAt,
                CreatedAtText = item.CreatedAt == DateTime.MinValue ? string.Empty : item.CreatedAt.ToString("dd/MM/yyyy HH:mm"),
                UpdatedAtText = item.UpdatedAt == DateTime.MinValue ? string.Empty : item.UpdatedAt.ToString("dd/MM/yyyy HH:mm"),
                LichText = "Còn trống"
            };
        }

        private string GetSelectedStatusValue()
        {
            if (cboStatus.SelectedIndex == 1) return "HoatDong";
            if (cboStatus.SelectedIndex == 2) return "BaoTri";
            if (cboStatus.SelectedIndex == 3) return "NgungSuDung";
            return string.Empty;
        }

        private static string NormalizeStatus(string value)
        {
            string status = (value ?? string.Empty).Trim();
            if (status.Equals("HoatDong", StringComparison.OrdinalIgnoreCase) ||
                status.Equals("Hoạt động", StringComparison.OrdinalIgnoreCase) ||
                status.Equals("Hoạt Động", StringComparison.OrdinalIgnoreCase))
            {
                return "HoatDong";
            }

            if (status.Equals("BaoTri", StringComparison.OrdinalIgnoreCase) ||
                status.Equals("Bảo trì", StringComparison.OrdinalIgnoreCase) ||
                status.Equals("Bảo Trì", StringComparison.OrdinalIgnoreCase))
            {
                return "BaoTri";
            }

            if (status.Equals("NgungSuDung", StringComparison.OrdinalIgnoreCase) ||
                status.Equals("Đóng", StringComparison.OrdinalIgnoreCase) ||
                status.Equals("Dong", StringComparison.OrdinalIgnoreCase))
            {
                return "NgungSuDung";
            }

            return status;
        }

        private static string ToStatusText(string value)
        {
            string status = NormalizeStatus(value);
            if (status == "HoatDong") return "Hoạt động";
            if (status == "BaoTri") return "Bảo trì";
            if (status == "NgungSuDung") return "Đóng";
            return string.IsNullOrWhiteSpace(value) ? "Không rõ" : value;
        }

        private static string ToStatusValue(string text)
        {
            string status = NormalizeStatus(text);
            return string.IsNullOrEmpty(status) ? "HoatDong" : status;
        }

        private static Color GetStatusColor(string status)
        {
            if (status == "Hoạt động") return Color.FromArgb(22, 163, 74);
            if (status == "Bảo trì") return Color.FromArgb(245, 158, 11);
            return Color.FromArgb(220, 38, 38);
        }

        private static Color GetStatusBackColor(string status)
        {
            if (status == "Hoạt động") return Color.FromArgb(240, 253, 244);
            if (status == "Bảo trì") return Color.FromArgb(255, 251, 235);
            return Color.FromArgb(254, 242, 242);
        }

        private static bool ContainsKeyword(string value, string keyword)
        {
            return !string.IsNullOrEmpty(value) && value.ToLowerInvariant().Contains(keyword);
        }

        private static string GetActionFromMouseX(int cellWidth, int x)
        {
            int iconSize = 30;
            int totalWidth = (iconSize * 3) + 12;
            int startX = 18;
            if (x >= startX && x <= startX + iconSize) return "View";
            if (x >= startX + iconSize + 6 && x <= startX + (iconSize * 2) + 6) return "Edit";
            if (x >= startX + (iconSize * 2) + 12 && x <= startX + (iconSize * 3) + 12) return "Delete";
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

        private static void ShowDataError(string message, Exception ex)
        {
            MessageBox.Show(message + "\\n" + ex.Message, "Lỗi dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private sealed class CaOption
        {
            public int MaCa { get; set; }
            public string Text { get; set; }
            public override string ToString()
            {
                return Text;
            }
        }

        private sealed class RoomDialogResult
        {
            public int MaPhong { get; set; }
            public string TenPhong { get; set; }
            public int SucChua { get; set; }
            public string TrangThai { get; set; }
        }

        private sealed class PhongRow
        {
            public int MaPhong { get; set; }
            public string TenPhong { get; set; }
            public int SucChua { get; set; }
            public string TrangThai { get; set; }
            public string TrangThaiText { get; set; }
            public DateTime CreatedAt { get; set; }
            public DateTime UpdatedAt { get; set; }
            public string CreatedAtText { get; set; }
            public string UpdatedAtText { get; set; }
            public bool DaCoLich { get; set; }
            public string LichText { get; set; }
        }
    }
}








