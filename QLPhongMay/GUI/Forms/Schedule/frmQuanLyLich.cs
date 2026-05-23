using System;
using System.Collections.Generic;
using System.Drawing;
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
        private Guna2Panel pnlRoomPalette;
        private Guna2HtmlLabel lblPaletteTitle;
        private Guna2HtmlLabel lblPaletteSubtitle;
        private Guna2HtmlLabel lblPaletteDate;
        private Guna2HtmlLabel lblPaletteShift;
        private Guna2HtmlLabel lblPaletteStudents;
        private DateTimePicker dtpPaletteDate;
        private ComboBox cboPaletteShift;
        private NumericUpDown nudPaletteStudents;
        private Guna2Button btnRefreshPalette;
        private Guna2HtmlLabel lblPaletteState;
        private FlowLayoutPanel pnlPaletteRooms;

        public frmQuanLyLich()
        {
            this.repository = new ScheduleRepository();
            InitializeComponent();
            InitializeRoomPalette();
            this.Load += frmQuanLyLich_Load;
        }

        private void InitializeRoomPalette()
        {
            this.pnlRoomPalette = new Guna2Panel();
            this.lblPaletteTitle = new Guna2HtmlLabel();
            this.lblPaletteSubtitle = new Guna2HtmlLabel();
            this.lblPaletteDate = new Guna2HtmlLabel();
            this.lblPaletteShift = new Guna2HtmlLabel();
            this.lblPaletteStudents = new Guna2HtmlLabel();
            this.dtpPaletteDate = new DateTimePicker();
            this.cboPaletteShift = new ComboBox();
            this.nudPaletteStudents = new NumericUpDown();
            this.btnRefreshPalette = new Guna2Button();
            this.lblPaletteState = new Guna2HtmlLabel();
            this.pnlPaletteRooms = new FlowLayoutPanel();

            this.pnlRoomPalette.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.pnlRoomPalette.BorderColor = Color.FromArgb(226, 232, 240);
            this.pnlRoomPalette.BorderRadius = 8;
            this.pnlRoomPalette.BorderThickness = 1;
            this.pnlRoomPalette.FillColor = Color.White;
            this.pnlRoomPalette.Location = new Point(30, 292);
            this.pnlRoomPalette.Size = new Size(1120, 112);

            ConfigurePaletteLabel(this.lblPaletteTitle, "Palette đề xuất phòng trống", 24, 14, 10.5F, true);
            ConfigurePaletteLabel(this.lblPaletteSubtitle, "Chọn ngày, ca và sĩ số để xem phòng còn trống, đủ sức chứa", 24, 40, 8.8F, false);
            ConfigurePaletteLabel(this.lblPaletteDate, "Ngày", 390, 15, 8.8F, true);
            ConfigurePaletteLabel(this.lblPaletteShift, "Ca", 540, 15, 8.8F, true);
            ConfigurePaletteLabel(this.lblPaletteStudents, "Sĩ số", 700, 15, 8.8F, true);

            this.dtpPaletteDate.Format = DateTimePickerFormat.Short;
            this.dtpPaletteDate.Location = new Point(390, 38);
            this.dtpPaletteDate.Size = new Size(130, 23);
            this.dtpPaletteDate.ValueChanged += PaletteInputChanged;

            this.cboPaletteShift.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cboPaletteShift.Location = new Point(540, 38);
            this.cboPaletteShift.Size = new Size(140, 23);
            this.cboPaletteShift.SelectedIndexChanged += PaletteInputChanged;

            this.nudPaletteStudents.Location = new Point(700, 38);
            this.nudPaletteStudents.Maximum = 500;
            this.nudPaletteStudents.Minimum = 1;
            this.nudPaletteStudents.Size = new Size(76, 23);
            this.nudPaletteStudents.Value = 30;
            this.nudPaletteStudents.ValueChanged += PaletteInputChanged;

            this.btnRefreshPalette.Animated = true;
            this.btnRefreshPalette.BorderRadius = 8;
            this.btnRefreshPalette.Cursor = Cursors.Hand;
            this.btnRefreshPalette.FillColor = Color.FromArgb(37, 99, 235);
            this.btnRefreshPalette.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.btnRefreshPalette.ForeColor = Color.White;
            this.btnRefreshPalette.HoverState.FillColor = Color.FromArgb(29, 78, 216);
            this.btnRefreshPalette.Location = new Point(796, 32);
            this.btnRefreshPalette.Size = new Size(104, 34);
            this.btnRefreshPalette.Text = "Làm mới";
            this.btnRefreshPalette.Click += BtnRefreshPalette_Click;

            this.pnlPaletteRooms.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.pnlPaletteRooms.AutoScroll = true;
            this.pnlPaletteRooms.BackColor = Color.Transparent;
            this.pnlPaletteRooms.Location = new Point(24, 72);
            this.pnlPaletteRooms.Size = new Size(1072, 32);
            this.pnlPaletteRooms.WrapContents = false;

            this.lblPaletteState.BackColor = Color.Transparent;
            this.lblPaletteState.Font = new Font("Segoe UI", 9F);
            this.lblPaletteState.ForeColor = Color.FromArgb(100, 116, 139);
            this.lblPaletteState.Location = new Point(24, 78);
            this.lblPaletteState.Text = "Đang chờ dữ liệu phòng và ca học.";

            this.pnlRoomPalette.Controls.Add(this.lblPaletteTitle);
            this.pnlRoomPalette.Controls.Add(this.lblPaletteSubtitle);
            this.pnlRoomPalette.Controls.Add(this.lblPaletteDate);
            this.pnlRoomPalette.Controls.Add(this.lblPaletteShift);
            this.pnlRoomPalette.Controls.Add(this.lblPaletteStudents);
            this.pnlRoomPalette.Controls.Add(this.dtpPaletteDate);
            this.pnlRoomPalette.Controls.Add(this.cboPaletteShift);
            this.pnlRoomPalette.Controls.Add(this.nudPaletteStudents);
            this.pnlRoomPalette.Controls.Add(this.btnRefreshPalette);
            this.pnlRoomPalette.Controls.Add(this.pnlPaletteRooms);
            this.pnlRoomPalette.Controls.Add(this.lblPaletteState);
            this.Controls.Add(this.pnlRoomPalette);
            this.pnlRoomPalette.BringToFront();

            this.dgvSchedules.Location = new Point(this.dgvSchedules.Left, 424);
            this.dgvSchedules.Size = new Size(this.dgvSchedules.Width, 266);
        }

        private static void ConfigurePaletteLabel(Guna2HtmlLabel label, string text, int x, int y, float size, bool bold)
        {
            label.BackColor = Color.Transparent;
            label.Font = new Font("Segoe UI", size, bold ? FontStyle.Bold : FontStyle.Regular);
            label.ForeColor = bold ? Color.FromArgb(51, 65, 85) : Color.FromArgb(100, 116, 139);
            label.Location = new Point(x, y);
            label.Text = text;
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
            this.dtpPaletteDate.Value = DateTime.Today;
            LoadLookups();
            RefreshRoomPalette();
            LoadSchedules(true);
        }

        private void LoadLookups()
        {
            BindLookup(this.cboRoom, this.repository.GetRooms());
            BindLookup(this.cboShift, this.repository.GetShifts());
            BindRequiredLookup(this.cboPaletteShift, this.repository.GetShifts());
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

        private static void BindRequiredLookup(ComboBox comboBox, List<LookupItem> items)
        {
            comboBox.DataSource = items;
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

        private void PaletteInputChanged(object sender, EventArgs e)
        {
            RefreshRoomPalette();
        }

        private void BtnRefreshPalette_Click(object sender, EventArgs e)
        {
            RefreshRoomPalette();
        }

        private void RefreshRoomPalette()
        {
            if (this.pnlPaletteRooms == null || this.cboPaletteShift == null)
            {
                return;
            }

            string shiftId = Convert.ToString(this.cboPaletteShift.SelectedValue);
            if (string.IsNullOrWhiteSpace(shiftId))
            {
                ShowPaletteState("Chưa có ca học để đề xuất phòng trống.");
                return;
            }

            try
            {
                List<RoomSuggestionItem> rooms = this.repository.GetAvailableRooms(
                    this.dtpPaletteDate.Value.Date,
                    shiftId,
                    Convert.ToInt32(this.nudPaletteStudents.Value),
                    null);

                RenderRoomPalette(rooms);
            }
            catch (Exception ex)
            {
                ShowPaletteState("Không thể tải phòng trống: " + ex.Message);
            }
        }

        private void RenderRoomPalette(List<RoomSuggestionItem> rooms)
        {
            this.pnlPaletteRooms.SuspendLayout();
            this.pnlPaletteRooms.Controls.Clear();

            if (rooms == null || rooms.Count == 0)
            {
                this.pnlPaletteRooms.ResumeLayout();
                ShowPaletteState("Không có phòng trống phù hợp với ngày, ca và sĩ số đã chọn.");
                return;
            }

            this.lblPaletteState.Visible = false;
            foreach (RoomSuggestionItem room in rooms)
            {
                this.pnlPaletteRooms.Controls.Add(CreateRoomPaletteCard(room));
            }

            this.pnlPaletteRooms.ResumeLayout();
        }

        private Guna2Button CreateRoomPaletteCard(RoomSuggestionItem room)
        {
            Guna2Button card = new Guna2Button();
            card.Animated = true;
            card.BorderColor = Color.FromArgb(226, 232, 240);
            card.BorderRadius = 8;
            card.BorderThickness = 1;
            card.Cursor = Cursors.Hand;
            card.FillColor = Color.FromArgb(248, 250, 252);
            card.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            card.ForeColor = Color.FromArgb(15, 23, 42);
            card.HoverState.BorderColor = Color.FromArgb(37, 99, 235);
            card.HoverState.FillColor = Color.FromArgb(239, 246, 255);
            card.Margin = new Padding(0, 0, 8, 0);
            card.Size = new Size(150, 30);
            card.Tag = room;
            card.Text = string.Format("{0}  |  {1} SV", room.TenPhong, room.SucChua);
            card.TextAlign = HorizontalAlignment.Left;
            card.TextOffset = new Point(8, 0);
            card.Click += RoomPaletteCard_Click;
            return card;
        }

        private void RoomPaletteCard_Click(object sender, EventArgs e)
        {
            Guna2Button card = sender as Guna2Button;
            RoomSuggestionItem room = card == null ? null : card.Tag as RoomSuggestionItem;
            if (room == null)
            {
                return;
            }

            this.cboRoom.SelectedValue = room.MaPhong;
            LoadSchedules(true);
        }

        private void ShowPaletteState(string message)
        {
            this.pnlPaletteRooms.Controls.Clear();
            this.lblPaletteState.Text = message;
            this.lblPaletteState.Visible = true;
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
