using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Guna.UI2.WinForms;
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
            this.dtpFrom.Value = DateTime.Today.AddMonths(-1);
            this.dtpTo.Value = DateTime.Today.AddMonths(1);
            LoadLookups();
            LoadSchedules(true);
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

        private static void BindLookup(Guna2ComboBox comboBox, List<LookupItem> items)
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

        private static string GetSelectedValue(Guna2ComboBox comboBox)
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
            using (FrmScheduleEditor dialog = new FrmScheduleEditor(this.repository, null))
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
            using (FrmScheduleEditor dialog = new FrmScheduleEditor(this.repository, item))
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
