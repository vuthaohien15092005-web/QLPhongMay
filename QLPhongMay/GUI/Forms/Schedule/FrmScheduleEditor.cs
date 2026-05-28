using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Guna.UI2.WinForms;
using QLPhongMay.DAL;
using QLPhongMay.DTO;

namespace QLPhongMay.GUI.Forms.Schedule
{
    public partial class FrmScheduleEditor : Form
    {
        private readonly ScheduleRepository repository;
        private readonly bool editMode;
        private TextBox txtId;
        private ComboBox cboUser;
        private ComboBox cboClass;
        private ComboBox cboRoom;
        private ComboBox cboShift;
        private ComboBox cboStatus;
        private Label lblStatus;
        private DateTimePicker dtpDate;
        private NumericUpDown nudStudentCount;
        private Guna2Panel pnlRoomSuggestion;
        private Guna2HtmlLabel lblSuggestionTitle;
        private Guna2HtmlLabel lblSuggestionSubtitle;
        private Guna2HtmlLabel lblSuggestionState;
        private Guna2Button btnRefreshSuggestions;
        private FlowLayoutPanel pnlSuggestionRooms;
        private Button btnSave;
        private Button btnCancel;

        public ScheduleListItem ScheduleItem { get; private set; }

        public FrmScheduleEditor(ScheduleRepository repository, ScheduleListItem item)
        {
            this.repository = repository;
            this.editMode = item != null;
            this.ScheduleItem = item ?? new ScheduleListItem { NgayThucHanh = DateTime.Today, TrangThai = "Đã lên lịch" };
            InitializeComponent();
            LoadLookups();
            BindSchedule();
            RefreshRoomSuggestions();
        }

        private void InitializeComponent()
        {
            this.Text = this.editMode ? "Chỉnh sửa lịch" : "Tạo lịch";
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.ClientSize = new Size(720, this.editMode ? 620 : 576);
            this.Font = new Font("Segoe UI", 9F);

            TableLayoutPanel layout = new TableLayoutPanel();
            layout.Dock = DockStyle.Fill;
            layout.Padding = new Padding(22);
            layout.ColumnCount = 2;
            layout.RowCount = 10;
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 140));
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            this.Controls.Add(layout);

            this.txtId = new TextBox();
            this.cboUser = CreateComboBox();
            this.cboClass = CreateComboBox();
            this.cboRoom = CreateComboBox();
            this.cboShift = CreateComboBox();
            this.cboStatus = CreateComboBox();
            this.dtpDate = new DateTimePicker { Dock = DockStyle.Fill, Format = DateTimePickerFormat.Short };
            this.nudStudentCount = new NumericUpDown { Dock = DockStyle.Fill, Minimum = 1, Maximum = 500 };
            this.pnlRoomSuggestion = new Guna2Panel();
            this.lblSuggestionTitle = new Guna2HtmlLabel();
            this.lblSuggestionSubtitle = new Guna2HtmlLabel();
            this.lblSuggestionState = new Guna2HtmlLabel();
            this.btnRefreshSuggestions = new Guna2Button();
            this.pnlSuggestionRooms = new FlowLayoutPanel();
            this.btnSave = new Button { Text = "Lưu", Width = 100, Height = 34, BackColor = Color.FromArgb(37, 99, 235), ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 9F, FontStyle.Bold), DialogResult = DialogResult.None };
            this.btnCancel = new Button { Text = "Hủy", Width = 100, Height = 34, BackColor = Color.White, ForeColor = Color.FromArgb(71, 85, 105), FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 9F, FontStyle.Bold), DialogResult = DialogResult.Cancel };

            this.txtId.Dock = DockStyle.Fill;
            this.txtId.Enabled = false;
            this.cboStatus.Items.AddRange(new object[] { "Đã lên lịch", "Hoàn thành", "Đã hủy" });
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnCancel.FlatAppearance.BorderColor = Color.FromArgb(203, 213, 225);

            AddField(layout, 0, "Mã lịch", this.txtId);
            AddField(layout, 1, "Người tạo", this.cboUser);
            AddField(layout, 2, "Lớp", this.cboClass);
            AddField(layout, 3, "Phòng", this.cboRoom);
            AddField(layout, 4, "Ca", this.cboShift);
            AddField(layout, 5, "Ngày", this.dtpDate);
            AddField(layout, 6, "Số sinh viên", this.nudStudentCount);
            this.lblStatus = AddField(layout, 7, "Trạng thái", this.cboStatus);
            if (!this.editMode)
            {
                this.lblStatus.Visible = false;
                this.cboStatus.Visible = false;
                layout.RowStyles[7].Height = 0;
            }
            ConfigureSuggestionPalette();
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 190));
            layout.Controls.Add(this.pnlRoomSuggestion, 0, 8);
            layout.SetColumnSpan(this.pnlRoomSuggestion, 2);

            FlowLayoutPanel actions = new FlowLayoutPanel { Dock = DockStyle.Right, FlowDirection = FlowDirection.RightToLeft, AutoSize = true };
            actions.Controls.Add(this.btnSave);
            actions.Controls.Add(this.btnCancel);
            layout.Controls.Add(actions, 1, 9);

            this.AcceptButton = this.btnSave;
            this.CancelButton = this.btnCancel;
            this.btnSave.Click += BtnSave_Click;
            this.cboShift.SelectedIndexChanged += ScheduleInputChanged;
            this.dtpDate.ValueChanged += ScheduleInputChanged;
            this.nudStudentCount.ValueChanged += ScheduleInputChanged;
            this.btnRefreshSuggestions.Click += BtnRefreshSuggestions_Click;
        }

        private static ComboBox CreateComboBox()
        {
            return new ComboBox { Dock = DockStyle.Fill, DropDownStyle = ComboBoxStyle.DropDownList };
        }

        private static Label AddField(TableLayoutPanel layout, int row, string label, Control control)
        {
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 44));
            Label lbl = new Label { Text = label, Dock = DockStyle.Fill, TextAlign = ContentAlignment.MiddleLeft };
            layout.Controls.Add(lbl, 0, row);
            layout.Controls.Add(control, 1, row);
            return lbl;
        }

        private void ConfigureSuggestionPalette()
        {
            this.pnlRoomSuggestion.BorderColor = Color.FromArgb(226, 232, 240);
            this.pnlRoomSuggestion.BorderRadius = 8;
            this.pnlRoomSuggestion.BorderThickness = 1;
            this.pnlRoomSuggestion.Dock = DockStyle.Fill;
            this.pnlRoomSuggestion.FillColor = Color.White;
            this.pnlRoomSuggestion.Margin = new Padding(0, 8, 0, 8);

            this.lblSuggestionTitle.BackColor = Color.Transparent;
            this.lblSuggestionTitle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.lblSuggestionTitle.ForeColor = Color.FromArgb(15, 23, 42);
            this.lblSuggestionTitle.Location = new Point(16, 14);
            this.lblSuggestionTitle.Text = "Palette đề xuất phòng trống";

            this.lblSuggestionSubtitle.BackColor = Color.Transparent;
            this.lblSuggestionSubtitle.Font = new Font("Segoe UI", 8.5F);
            this.lblSuggestionSubtitle.ForeColor = Color.FromArgb(100, 116, 139);
            this.lblSuggestionSubtitle.Location = new Point(16, 40);
            this.lblSuggestionSubtitle.Text = "Dựa trên ngày, ca học, sĩ số và phòng chưa bị trùng lịch";

            this.btnRefreshSuggestions.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            this.btnRefreshSuggestions.Animated = true;
            this.btnRefreshSuggestions.BorderColor = Color.FromArgb(226, 232, 240);
            this.btnRefreshSuggestions.BorderRadius = 8;
            this.btnRefreshSuggestions.BorderThickness = 1;
            this.btnRefreshSuggestions.Cursor = Cursors.Hand;
            this.btnRefreshSuggestions.FillColor = Color.FromArgb(248, 250, 252);
            this.btnRefreshSuggestions.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            this.btnRefreshSuggestions.ForeColor = Color.FromArgb(37, 99, 235);
            this.btnRefreshSuggestions.HoverState.FillColor = Color.FromArgb(239, 246, 255);
            this.btnRefreshSuggestions.Location = new Point(548, 16);
            this.btnRefreshSuggestions.Size = new Size(116, 32);
            this.btnRefreshSuggestions.Text = "Làm mới";

            this.pnlSuggestionRooms.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.pnlSuggestionRooms.AutoScroll = true;
            this.pnlSuggestionRooms.BackColor = Color.Transparent;
            this.pnlSuggestionRooms.Location = new Point(16, 70);
            this.pnlSuggestionRooms.Name = "pnlSuggestionRooms";
            this.pnlSuggestionRooms.Size = new Size(648, 100);
            this.pnlSuggestionRooms.WrapContents = true;

            this.lblSuggestionState.BackColor = Color.Transparent;
            this.lblSuggestionState.Font = new Font("Segoe UI", 9F);
            this.lblSuggestionState.ForeColor = Color.FromArgb(100, 116, 139);
            this.lblSuggestionState.Location = new Point(18, 94);
            this.lblSuggestionState.Text = string.Empty;
            this.lblSuggestionState.Visible = false;

            this.pnlRoomSuggestion.Controls.Add(this.lblSuggestionTitle);
            this.pnlRoomSuggestion.Controls.Add(this.lblSuggestionSubtitle);
            this.pnlRoomSuggestion.Controls.Add(this.btnRefreshSuggestions);
            this.pnlRoomSuggestion.Controls.Add(this.pnlSuggestionRooms);
            this.pnlRoomSuggestion.Controls.Add(this.lblSuggestionState);
            this.pnlRoomSuggestion.Resize += PnlRoomSuggestion_Resize;
        }

        private void LoadLookups()
        {
            BindLookup(this.cboUser, this.repository.GetUsers());
            BindLookup(this.cboClass, this.repository.GetClasses());
            BindLookup(this.cboRoom, this.repository.GetRooms());
            BindLookup(this.cboShift, this.repository.GetShifts());
        }

        private static void BindLookup(ComboBox comboBox, List<LookupItem> items)
        {
            comboBox.DataSource = items;
            comboBox.DisplayMember = "Name";
            comboBox.ValueMember = "Id";
        }

        private void BindSchedule()
        {
            this.txtId.Text = this.editMode ? this.ScheduleItem.MaLich : "Tự động";
            SelectValue(this.cboUser, this.ScheduleItem.TenDangNhap);
            SelectValue(this.cboClass, this.ScheduleItem.MaLop);
            SelectValue(this.cboRoom, this.ScheduleItem.MaPhong);
            SelectValue(this.cboShift, this.ScheduleItem.MaCa);
            this.dtpDate.Value = this.ScheduleItem.NgayThucHanh == DateTime.MinValue ? DateTime.Today : this.ScheduleItem.NgayThucHanh;
            this.nudStudentCount.Value = Math.Max(this.nudStudentCount.Minimum, Math.Min(this.nudStudentCount.Maximum, this.ScheduleItem.SoLuongSV <= 0 ? 1 : this.ScheduleItem.SoLuongSV));
            this.cboStatus.SelectedItem = NormalizeStatus(this.ScheduleItem.TrangThai);
            if (this.cboStatus.SelectedIndex < 0)
            {
                this.cboStatus.SelectedIndex = 0;
            }
        }

        private static void SelectValue(ComboBox comboBox, string value)
        {
            if (!string.IsNullOrWhiteSpace(value))
            {
                comboBox.SelectedValue = value;
            }
        }

        private void ScheduleInputChanged(object sender, EventArgs e)
        {
            RefreshRoomSuggestions();
        }

        private void BtnRefreshSuggestions_Click(object sender, EventArgs e)
        {
            RefreshRoomSuggestions();
        }

        private void PnlRoomSuggestion_Resize(object sender, EventArgs e)
        {
            this.btnRefreshSuggestions.Left = this.pnlRoomSuggestion.Width - this.btnRefreshSuggestions.Width - 16;
            this.pnlSuggestionRooms.Width = this.pnlRoomSuggestion.Width - 32;
        }

        private void RefreshRoomSuggestions()
        {
            if (this.cboShift.SelectedValue == null || this.pnlSuggestionRooms == null)
            {
                return;
            }

            string shiftId = Convert.ToString(this.cboShift.SelectedValue);
            if (string.IsNullOrWhiteSpace(shiftId))
            {
                ShowSuggestionState("Chọn ca học để xem phòng trống.");
                return;
            }

            try
            {
                List<RoomSuggestionItem> rooms = this.repository.GetAvailableRooms(
                    this.dtpDate.Value.Date,
                    shiftId,
                    Convert.ToInt32(this.nudStudentCount.Value),
                    this.editMode ? this.ScheduleItem.MaLich : null);

                RenderRoomSuggestions(rooms);
            }
            catch (Exception ex)
            {
                ShowSuggestionState("Không thể tải phòng trống: " + ex.Message);
            }
        }

        private void RenderRoomSuggestions(List<RoomSuggestionItem> rooms)
        {
            this.pnlSuggestionRooms.SuspendLayout();
            this.pnlSuggestionRooms.Controls.Clear();

            if (rooms == null || rooms.Count == 0)
            {
                this.pnlSuggestionRooms.ResumeLayout();
                ShowSuggestionState("Không có phòng phù hợp với ca, ngày và sĩ số đã chọn.");
                return;
            }

            this.lblSuggestionState.Visible = false;
            foreach (RoomSuggestionItem room in rooms)
            {
                this.pnlSuggestionRooms.Controls.Add(CreateRoomSuggestionCard(room));
            }

            this.pnlSuggestionRooms.ResumeLayout();
        }

        private Guna2Button CreateRoomSuggestionCard(RoomSuggestionItem room)
        {
            bool selected = Convert.ToString(this.cboRoom.SelectedValue) == room.MaPhong;
            Guna2Button card = new Guna2Button();
            card.Animated = true;
            card.BorderColor = selected ? Color.FromArgb(37, 99, 235) : Color.FromArgb(226, 232, 240);
            card.BorderRadius = 8;
            card.BorderThickness = selected ? 2 : 1;
            card.ButtonMode = Guna.UI2.WinForms.Enums.ButtonMode.RadioButton;
            card.Checked = selected;
            card.Cursor = Cursors.Hand;
            card.FillColor = selected ? Color.FromArgb(239, 246, 255) : Color.FromArgb(248, 250, 252);
            card.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            card.ForeColor = Color.FromArgb(15, 23, 42);
            card.HoverState.BorderColor = Color.FromArgb(37, 99, 235);
            card.HoverState.FillColor = Color.FromArgb(239, 246, 255);
            card.Margin = new Padding(0, 0, 10, 0);
            card.Size = new Size(148, 66);
            card.Tag = room;
            card.Text = string.Format("{0}\nSức chứa {1} | {2} máy", room.TenPhong, room.SucChua, room.SoMay);
            card.TextAlign = HorizontalAlignment.Left;
            card.TextOffset = new Point(8, 0);
            card.Click += RoomSuggestionCard_Click;
            return card;
        }

        private void RoomSuggestionCard_Click(object sender, EventArgs e)
        {
            Guna2Button card = sender as Guna2Button;
            RoomSuggestionItem room = card == null ? null : card.Tag as RoomSuggestionItem;
            if (room == null)
            {
                return;
            }

            this.cboRoom.SelectedValue = room.MaPhong;
            RefreshRoomSuggestions();
        }

        private void ShowSuggestionState(string message)
        {
            this.pnlSuggestionRooms.Controls.Clear();
            this.lblSuggestionState.Text = message;
            this.lblSuggestionState.Visible = true;
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateSelections())
            {
                return;
            }

            if (this.editMode)
            {
                this.ScheduleItem.MaLich = this.txtId.Text.Trim();
            }
            this.ScheduleItem.TenDangNhap = Convert.ToString(this.cboUser.SelectedValue);
            this.ScheduleItem.MaLop = Convert.ToString(this.cboClass.SelectedValue);
            this.ScheduleItem.MaPhong = Convert.ToString(this.cboRoom.SelectedValue);
            this.ScheduleItem.MaCa = Convert.ToString(this.cboShift.SelectedValue);
            this.ScheduleItem.NgayThucHanh = this.dtpDate.Value.Date;
            this.ScheduleItem.ThuTrongTuan = ToVietnameseDayOfWeek(this.dtpDate.Value.DayOfWeek);
            this.ScheduleItem.SoLuongSV = Convert.ToInt32(this.nudStudentCount.Value);
            this.ScheduleItem.TrangThai = this.editMode ? Convert.ToString(this.cboStatus.SelectedItem) : "Đã lên lịch";

            if (this.repository.HasScheduleConflict(this.ScheduleItem.MaLich, this.ScheduleItem.MaPhong, this.ScheduleItem.MaCa, this.ScheduleItem.NgayThucHanh))
            {
                MessageBox.Show("Phòng này đã có lịch trong ca và ngày đã chọn. Vui lòng chọn phòng, ca hoặc ngày khác.", "Trùng lịch", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private bool ValidateSelections()
        {
            if (!HasSelectedValue(this.cboUser))
            {
                return ShowValidationWarning("Vui lòng chọn người tạo.", this.cboUser);
            }

            if (!HasSelectedValue(this.cboClass))
            {
                return ShowValidationWarning("Vui lòng chọn lớp.", this.cboClass);
            }

            if (!HasSelectedValue(this.cboRoom))
            {
                return ShowValidationWarning("Vui lòng chọn phòng hoặc chọn một phòng trong palette đề xuất.", this.cboRoom);
            }

            if (!HasSelectedValue(this.cboShift))
            {
                return ShowValidationWarning("Vui lòng chọn ca học.", this.cboShift);
            }

            return true;
        }

        private static bool HasSelectedValue(ComboBox comboBox)
        {
            return comboBox.SelectedValue != null && !string.IsNullOrWhiteSpace(Convert.ToString(comboBox.SelectedValue));
        }

        private static bool ShowValidationWarning(string message, Control target)
        {
            MessageBox.Show(message, "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            target.Focus();
            return false;
        }

        private static string NormalizeStatus(string status)
        {
            if (string.IsNullOrWhiteSpace(status) || status == "DaLenLich")
            {
                return "Đã lên lịch";
            }

            if (status == "HoanThanh")
            {
                return "Hoàn thành";
            }

            if (status == "DaHuy")
            {
                return "Đã hủy";
            }

            return status;
        }

        private static int ToVietnameseDayOfWeek(DayOfWeek dayOfWeek)
        {
            return dayOfWeek == DayOfWeek.Sunday ? 8 : ((int)dayOfWeek + 1);
        }
    }
}


