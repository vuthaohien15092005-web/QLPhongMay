using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
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
        private DateTimePicker dtpDate;
        private NumericUpDown nudStudentCount;
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
        }

        private void InitializeComponent()
        {
            this.Text = this.editMode ? "Chỉnh sửa lịch" : "Tạo lịch";
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.ClientSize = new Size(560, 430);
            this.Font = new Font("Segoe UI", 9F);

            TableLayoutPanel layout = new TableLayoutPanel();
            layout.Dock = DockStyle.Fill;
            layout.Padding = new Padding(22);
            layout.ColumnCount = 2;
            layout.RowCount = 9;
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
            AddField(layout, 7, "Trạng thái", this.cboStatus);

            FlowLayoutPanel actions = new FlowLayoutPanel { Dock = DockStyle.Right, FlowDirection = FlowDirection.RightToLeft, AutoSize = true };
            actions.Controls.Add(this.btnSave);
            actions.Controls.Add(this.btnCancel);
            layout.Controls.Add(actions, 1, 8);

            this.AcceptButton = this.btnSave;
            this.CancelButton = this.btnCancel;
            this.btnSave.Click += BtnSave_Click;
        }

        private static ComboBox CreateComboBox()
        {
            return new ComboBox { Dock = DockStyle.Fill, DropDownStyle = ComboBoxStyle.DropDownList };
        }

        private static void AddField(TableLayoutPanel layout, int row, string label, Control control)
        {
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 44));
            Label lbl = new Label { Text = label, Dock = DockStyle.Fill, TextAlign = ContentAlignment.MiddleLeft };
            layout.Controls.Add(lbl, 0, row);
            layout.Controls.Add(control, 1, row);
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

        private void BtnSave_Click(object sender, EventArgs e)
        {
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
            this.ScheduleItem.TrangThai = Convert.ToString(this.cboStatus.SelectedItem);

            if (this.repository.HasScheduleConflict(this.ScheduleItem.MaLich, this.ScheduleItem.MaPhong, this.ScheduleItem.MaCa, this.ScheduleItem.NgayThucHanh))
            {
                MessageBox.Show("Phòng này đã có lịch trong ca và ngày đã chọn. Vui lòng chọn phòng, ca hoặc ngày khác.", "Trùng lịch", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
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


