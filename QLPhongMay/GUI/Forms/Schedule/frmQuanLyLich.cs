using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using QLPhongMay.BLL;
using QLPhongMay.Enums;
using QLPhongMay.GUI.Forms.Dashboard;
using QLPhongMay.DAL;
using QLPhongMay.DTO;

namespace QLPhongMay.GUI.Forms.Schedule
{
    public partial class frmQuanLyLich : Form
    {
        private const int PageSize = 8;
        private readonly ScheduleRepository repository;
        private List<ScheduleRow> filteredRows = new List<ScheduleRow>();
        private int currentPage = 1;

        public frmQuanLyLich()
        {
            this.repository = new ScheduleRepository();
            InitializeComponent();
            this.Load += frmQuanLyLich_Load;
            this.Resize += frmQuanLyLich_Resize;
        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            if (this.Owner != null)
            {
                this.Close();
                return;
            }

            Form mainForm = null;
            if (Session.HasRole(UserRole.Admin))
            {
                mainForm = new frmMain_Admin();
            }
            else if (Session.HasRole(UserRole.QuanLyPhongMay))
            {
                mainForm = new frmMain_QLPM();
            }
            else
            {
                MessageBox.Show("Không xác định được vai trò người dùng.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Close();
                return;
            }

            using (mainForm)
            {
                this.Hide();
                mainForm.ShowDialog(this);
            }

            this.Close();
        }

        private void frmQuanLyLich_Load(object sender, EventArgs e)
        {
            LayoutResponsive();
            this.dtpFrom.Value = DateTime.Today.AddMonths(-1);
            this.dtpTo.Value = DateTime.Today.AddMonths(1);
            LoadLookups();
            LoadSchedules(true);
        }

        private void frmQuanLyLich_Resize(object sender, EventArgs e)
        {
            LayoutResponsive();
        }

        private void LayoutResponsive()
        {
            int margin = 30;
            int contentWidth = Math.Max(760, this.ClientSize.Width - margin * 2);
            int contentHeight = Math.Max(560, this.ClientSize.Height);

            this.btnBack.Location = new Point(margin, 32);
            this.lblTitle.Location = new Point(this.btnBack.Right + 16, 22);
            this.lblSubtitle.Location = new Point(this.lblTitle.Left + 4, 72);
            this.lblSubtitle.MaximumSize = new Size(Math.Max(320, contentWidth - this.lblTitle.Left - 24), 0);

            int actionTop = 32;
            this.btnDelete.Location = new Point(this.ClientSize.Width - margin - this.btnDelete.Width, actionTop);
            this.btnEdit.Location = new Point(this.btnDelete.Left - this.btnEdit.Width - 18, actionTop);
            this.btnAdd.Location = new Point(this.btnEdit.Left - this.btnAdd.Width - 18, actionTop);

            this.pnlFilter.Location = new Point(margin, 128);
            this.pnlFilter.Size = new Size(contentWidth, 152);
            LayoutFilterControls();

            int gridTop = this.pnlFilter.Bottom + 24;
            int pagingTop = contentHeight - 50;
            int gridHeight = Math.Max(260, pagingTop - gridTop - 18);
            this.dgvSchedules.Location = new Point(margin, gridTop);
            this.dgvSchedules.Size = new Size(contentWidth, gridHeight);

            this.lblCount.Location = new Point(margin + 4, pagingTop + 8);
            this.lblCount.MaximumSize = new Size(Math.Max(320, contentWidth - 360), 0);

            this.btnNextPage.Location = new Point(margin + contentWidth - this.btnNextPage.Width, pagingTop);
            this.lblPageInfo.Location = new Point(this.btnNextPage.Left - this.lblPageInfo.Width - 10, pagingTop + 10);
            this.btnPreviousPage.Location = new Point(this.lblPageInfo.Left - this.btnPreviousPage.Width - 10, pagingTop);
        }

        private void LayoutFilterControls()
        {
            int left = 24;
            int gap = 18;
            int topLabel = 18;
            int topInput = 42;
            int row2Label = 88;
            int row2Input = 112;
            int width = this.pnlFilter.ClientSize.Width;

            int dateWidth = 150;
            int dayWidth = 130;
            int roomWidth = Math.Max(150, (width - 48 - dateWidth * 2 - dayWidth - gap * 5) / 3);
            int comboWidth = Math.Min(180, roomWidth);

            this.lblFrom.Location = new Point(left, topLabel);
            this.dtpFrom.Location = new Point(left, topInput);
            this.dtpFrom.Size = new Size(dateWidth, 27);

            int x = this.dtpFrom.Right + gap;
            this.lblTo.Location = new Point(x, topLabel);
            this.dtpTo.Location = new Point(x, topInput);
            this.dtpTo.Size = new Size(dateWidth, 27);

            x = this.dtpTo.Right + gap;
            this.lblDayOfWeek.Location = new Point(x, topLabel);
            this.cboDayOfWeek.Location = new Point(x, topInput);
            this.cboDayOfWeek.Size = new Size(dayWidth, 28);

            x = this.cboDayOfWeek.Right + gap;
            this.lblRoom.Location = new Point(x, topLabel);
            this.cboRoom.Location = new Point(x, topInput);
            this.cboRoom.Size = new Size(comboWidth, 28);

            x = this.cboRoom.Right + gap;
            this.lblShift.Location = new Point(x, topLabel);
            this.cboShift.Location = new Point(x, topInput);
            this.cboShift.Size = new Size(comboWidth, 28);

            x = this.cboShift.Right + gap;
            this.lblClass.Location = new Point(x, topLabel);
            this.cboClass.Location = new Point(x, topInput);
            this.cboClass.Size = new Size(Math.Max(130, width - x - left), 28);

            this.lblStatus.Location = new Point(left, row2Label);
            this.cboStatus.Location = new Point(left, row2Input);
            this.cboStatus.Size = new Size(170, 28);

            this.btnClear.Location = new Point(width - left - this.btnClear.Width, 106);
            this.btnFilter.Location = new Point(this.btnClear.Left - this.btnFilter.Width - 14, 106);
        }

        private void LoadLookups()
        {
            BindLookup(this.cboRoom, this.repository.GetRooms());
            BindLookup(this.cboShift, this.repository.GetShifts());
            BindLookup(this.cboClass, this.repository.GetClasses());
            this.cboDayOfWeek.Items.Clear();
            this.cboDayOfWeek.Items.AddRange(new object[] { "Tất cả", "Thứ 2", "Thứ 3", "Thứ 4", "Thứ 5", "Thứ 6", "Thứ 7", "Chủ nhật" });
            this.cboDayOfWeek.SelectedIndex = 0;
            this.cboStatus.Items.Clear();
            this.cboStatus.Items.AddRange(new object[] { "Tất cả", "Đã lên lịch", "Hoàn thành", "Đã hủy" });
            this.cboStatus.SelectedIndex = 0;
        }

        private static void BindLookup(ComboBox comboBox, List<LookupItem> items)
        {
            List<LookupItem> source = new List<LookupItem> { new LookupItem { Id = string.Empty, Name = "Tất cả" } };
            source.AddRange(items);
            comboBox.DataSource = source;
            comboBox.DisplayMember = "Name";
            comboBox.ValueMember = "Id";
        }

        private void LoadSchedules(bool resetPage)
        {
            try
            {
                if (resetPage)
                {
                    this.currentPage = 1;
                }

                this.filteredRows = this.repository.GetByFilter(
                        this.dtpFrom.Value.Date,
                        this.dtpTo.Value.Date,
                        GetSelectedDayOfWeek(),
                        GetSelectedValue(this.cboRoom),
                        GetSelectedValue(this.cboShift),
                        GetSelectedValue(this.cboClass),
                        GetSelectedStatus())
                    .Select((item, index) => new ScheduleRow(item, index + 1))
                    .ToList();
                LoadCurrentPage();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể tải dữ liệu lịch.\n" + ex.Message, "Lỗi dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadCurrentPage()
        {
            int totalRows = this.filteredRows.Count;
            int totalPages = Math.Max(1, (int)Math.Ceiling(totalRows / (double)PageSize));
            this.currentPage = Math.Max(1, Math.Min(this.currentPage, totalPages));
            int skip = (this.currentPage - 1) * PageSize;
            List<ScheduleRow> pageRows = this.filteredRows
                .Skip(skip)
                .Take(PageSize)
                .Select((item, index) => item.CloneWithStt(skip + index + 1))
                .ToList();

            this.dgvSchedules.DataSource = pageRows;
            ConfigureColumns();
            this.lblCount.Text = totalRows == 0
                ? "Không có lịch phù hợp"
                : string.Format("Hiển thị {0}-{1} trong {2} lịch", skip + 1, skip + pageRows.Count, totalRows);
            this.lblPageInfo.Text = string.Format("Trang {0}/{1}", this.currentPage, totalPages);
            this.btnPreviousPage.Enabled = this.currentPage > 1;
            this.btnNextPage.Enabled = this.currentPage < totalPages;
        }

        private void ConfigureColumns()
        {
            if (this.dgvSchedules.Columns.Count == 0)
            {
                return;
            }

            this.dgvSchedules.Columns[nameof(ScheduleRow.MaLich)].Visible = false;
            this.dgvSchedules.Columns[nameof(ScheduleRow.Stt)].HeaderText = "STT";
            this.dgvSchedules.Columns[nameof(ScheduleRow.Class)].HeaderText = "Lớp";
            this.dgvSchedules.Columns[nameof(ScheduleRow.Room)].HeaderText = "Phòng";
            this.dgvSchedules.Columns[nameof(ScheduleRow.Date)].HeaderText = "Ngày";
            this.dgvSchedules.Columns[nameof(ScheduleRow.DayOfWeek)].HeaderText = "Thứ";
            this.dgvSchedules.Columns[nameof(ScheduleRow.Shift)].HeaderText = "Ca";
            this.dgvSchedules.Columns[nameof(ScheduleRow.StudentCount)].HeaderText = "Số SV";
            this.dgvSchedules.Columns[nameof(ScheduleRow.Status)].HeaderText = "Trạng thái";
            this.dgvSchedules.Columns[nameof(ScheduleRow.Stt)].FillWeight = 45;
            this.dgvSchedules.Columns[nameof(ScheduleRow.StudentCount)].FillWeight = 80;
        }

        private int? GetSelectedDayOfWeek()
        {
            return this.cboDayOfWeek.SelectedIndex <= 0 ? (int?)null : this.cboDayOfWeek.SelectedIndex + 1;
        }

        private static string GetSelectedValue(ComboBox comboBox)
        {
            return Convert.ToString(comboBox.SelectedValue);
        }

        private string GetSelectedStatus()
        {
            return this.cboStatus.SelectedIndex <= 0 ? null : Convert.ToString(this.cboStatus.SelectedItem);
        }

        private void BtnFilter_Click(object sender, EventArgs e)
        {
            LoadSchedules(true);
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            this.dtpFrom.Value = DateTime.Today.AddMonths(-1);
            this.dtpTo.Value = DateTime.Today.AddMonths(1);
            this.cboDayOfWeek.SelectedIndex = 0;
            this.cboRoom.SelectedIndex = 0;
            this.cboShift.SelectedIndex = 0;
            this.cboClass.SelectedIndex = 0;
            this.cboStatus.SelectedIndex = 0;
            LoadSchedules(true);
        }

        private void BtnPreviousPage_Click(object sender, EventArgs e)
        {
            if (this.currentPage <= 1)
            {
                return;
            }

            this.currentPage--;
            LoadCurrentPage();
        }

        private void BtnNextPage_Click(object sender, EventArgs e)
        {
            int totalPages = Math.Max(1, (int)Math.Ceiling(this.filteredRows.Count / (double)PageSize));
            if (this.currentPage >= totalPages)
            {
                return;
            }

            this.currentPage++;
            LoadCurrentPage();
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            using (FrmScheduleEditor2 dialog = new FrmScheduleEditor2(this.repository, null))
            {
                if (dialog.ShowDialog(this) != DialogResult.OK)
                {
                    return;
                }

                this.repository.Add(dialog.ScheduleItem);
                LoadSchedules(true);
            }
        }

        private void BtnEdit_Click(object sender, EventArgs e)
        {
            ScheduleRow row = GetSelectedRow();
            if (row == null)
            {
                return;
            }

            ScheduleListItem item = this.repository.GetById(row.MaLich);
            using (FrmScheduleEditor2 dialog = new FrmScheduleEditor2(this.repository, item))
            {
                if (dialog.ShowDialog(this) != DialogResult.OK)
                {
                    return;
                }

                this.repository.Update(dialog.ScheduleItem);
                LoadSchedules(false);
            }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            ScheduleRow row = GetSelectedRow();
            if (row == null)
            {
                return;
            }

            DialogResult confirm = MessageBox.Show("Bạn có chắc muốn xóa lịch '" + row.MaLich + "'?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (confirm != DialogResult.Yes)
            {
                return;
            }

            this.repository.Delete(row.MaLich);
            LoadSchedules(false);
        }

        private ScheduleRow GetSelectedRow()
        {
            if (this.dgvSchedules.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn một lịch.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return null;
            }

            return this.dgvSchedules.CurrentRow.DataBoundItem as ScheduleRow;
        }

        private static string GetDayOfWeekText(int dayOfWeek)
        {
            return dayOfWeek == 8 ? "Chủ nhật" : "Thứ " + dayOfWeek;
        }

        private class ScheduleRow
        {
            public ScheduleRow(ScheduleListItem item, int stt)
            {
                this.Stt = stt;
                this.MaLich = item.MaLich;
                this.Class = string.IsNullOrWhiteSpace(item.TenLop) ? item.MaLop : item.TenLop;
                this.Room = string.IsNullOrWhiteSpace(item.TenPhong) ? item.MaPhong : item.TenPhong;
                this.Date = item.NgayThucHanh.ToString("dd/MM/yyyy");
                this.DayOfWeek = GetDayOfWeekText(item.ThuTrongTuan);
                this.Shift = string.IsNullOrWhiteSpace(item.TenCa) ? item.MaCa : item.TenCa;
                this.StudentCount = item.SoLuongSV;
                this.Status = item.TrangThai;
            }

            public int Stt { get; set; }
            public string MaLich { get; set; }
            public string Class { get; set; }
            public string Room { get; set; }
            public string Date { get; set; }
            public string DayOfWeek { get; set; }
            public string Shift { get; set; }
            public int StudentCount { get; set; }
            public string Status { get; set; }

            public ScheduleRow CloneWithStt(int stt)
            {
                return new ScheduleRow
                {
                    Stt = stt,
                    MaLich = this.MaLich,
                    Class = this.Class,
                    Room = this.Room,
                    Date = this.Date,
                    DayOfWeek = this.DayOfWeek,
                    Shift = this.Shift,
                    StudentCount = this.StudentCount,
                    Status = this.Status
                };
            }

            private ScheduleRow()
            {
            }
        }
    }
}
