using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using QLPhongMay.DAL;
using QLPhongMay.DTO;

namespace QLPhongMay.GUI.Forms.Schedule
{
    public class FrmScheduleEditor2 : Form
    {
        private readonly ScheduleRepository repository;
        private readonly bool editMode;

        private Label lblUser;
        private Label lblClass;
        private Label lblRoom;
        private Label lblShift;
        private Label lblDate;
        private Label lblStudentCount;
        private Label lblStatus;
        private ComboBox cboUser;
        private ComboBox cboClass;
        private ComboBox cboRoom;
        private ComboBox cboShift;
        private ComboBox cboStatus;
        private DateTimePicker dtpDate;
        private NumericUpDown nudStudentCount;
        private Panel pnlSuggestion;
        private Label lblSuggestionTitle;
        private Label lblSuggestionSubtitle;
        private Label lblSuggestionState;
        private FlowLayoutPanel pnlSuggestionRooms;
        private Button btnRefreshSuggestions;
        private Button btnSave;
        private Button btnCancel;

        public ScheduleListItem ScheduleItem { get; private set; }

        public FrmScheduleEditor2()
        {
            this.repository = null;
            this.editMode = false;
            this.ScheduleItem = new ScheduleListItem { NgayThucHanh = DateTime.Today, TrangThai = "Đã lên lịch" };
            InitializeComponent();
        }

        public FrmScheduleEditor2(ScheduleRepository repository, ScheduleListItem item)
        {
            this.repository = repository;
            this.editMode = item != null;
            this.ScheduleItem = item ?? new ScheduleListItem { NgayThucHanh = DateTime.Today, TrangThai = "Đã lên lịch" };
            InitializeComponent();
            ApplyModeLayout();
            LoadLookups();
            BindSchedule();
            WireRuntimeEvents();
            RefreshRoomSuggestions();
        }

        private void InitializeComponent()
        {
            this.lblUser = new System.Windows.Forms.Label();
            this.lblClass = new System.Windows.Forms.Label();
            this.lblRoom = new System.Windows.Forms.Label();
            this.lblShift = new System.Windows.Forms.Label();
            this.lblDate = new System.Windows.Forms.Label();
            this.lblStudentCount = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.cboUser = new System.Windows.Forms.ComboBox();
            this.cboClass = new System.Windows.Forms.ComboBox();
            this.cboRoom = new System.Windows.Forms.ComboBox();
            this.cboShift = new System.Windows.Forms.ComboBox();
            this.cboStatus = new System.Windows.Forms.ComboBox();
            this.dtpDate = new System.Windows.Forms.DateTimePicker();
            this.nudStudentCount = new System.Windows.Forms.NumericUpDown();
            this.pnlSuggestion = new System.Windows.Forms.Panel();
            this.lblSuggestionTitle = new System.Windows.Forms.Label();
            this.lblSuggestionSubtitle = new System.Windows.Forms.Label();
            this.btnRefreshSuggestions = new System.Windows.Forms.Button();
            this.pnlSuggestionRooms = new System.Windows.Forms.FlowLayoutPanel();
            this.lblSuggestionState = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.nudStudentCount)).BeginInit();
            this.pnlSuggestion.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblUser
            // 
            this.lblUser.AutoSize = true;
            this.lblUser.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblUser.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.lblUser.Location = new System.Drawing.Point(32, 28);
            this.lblUser.Name = "lblUser";
            this.lblUser.Size = new System.Drawing.Size(63, 15);
            this.lblUser.TabIndex = 0;
            this.lblUser.Text = "Người tạo";
            // 
            // lblClass
            // 
            this.lblClass.AutoSize = true;
            this.lblClass.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblClass.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.lblClass.Location = new System.Drawing.Point(29, 72);
            this.lblClass.Name = "lblClass";
            this.lblClass.Size = new System.Drawing.Size(28, 15);
            this.lblClass.TabIndex = 1;
            this.lblClass.Text = "Lớp";
            // 
            // lblRoom
            // 
            this.lblRoom.AutoSize = true;
            this.lblRoom.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblRoom.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.lblRoom.Location = new System.Drawing.Point(29, 116);
            this.lblRoom.Name = "lblRoom";
            this.lblRoom.Size = new System.Drawing.Size(42, 15);
            this.lblRoom.TabIndex = 2;
            this.lblRoom.Text = "Phòng";
            // 
            // lblShift
            // 
            this.lblShift.AutoSize = true;
            this.lblShift.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblShift.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.lblShift.Location = new System.Drawing.Point(29, 160);
            this.lblShift.Name = "lblShift";
            this.lblShift.Size = new System.Drawing.Size(20, 15);
            this.lblShift.TabIndex = 3;
            this.lblShift.Text = "Ca";
            // 
            // lblDate
            // 
            this.lblDate.AutoSize = true;
            this.lblDate.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblDate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.lblDate.Location = new System.Drawing.Point(29, 204);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(35, 15);
            this.lblDate.TabIndex = 4;
            this.lblDate.Text = "Ngày";
            // 
            // lblStudentCount
            // 
            this.lblStudentCount.AutoSize = true;
            this.lblStudentCount.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblStudentCount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.lblStudentCount.Location = new System.Drawing.Point(29, 248);
            this.lblStudentCount.Name = "lblStudentCount";
            this.lblStudentCount.Size = new System.Drawing.Size(73, 15);
            this.lblStudentCount.TabIndex = 5;
            this.lblStudentCount.Text = "Số sinh viên";
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.lblStatus.Location = new System.Drawing.Point(29, 292);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(62, 15);
            this.lblStatus.TabIndex = 6;
            this.lblStatus.Text = "Trạng thái";
            // 
            // cboUser
            // 
            this.cboUser.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboUser.Location = new System.Drawing.Point(177, 20);
            this.cboUser.Name = "cboUser";
            this.cboUser.Size = new System.Drawing.Size(500, 23);
            this.cboUser.TabIndex = 7;
            // 
            // cboClass
            // 
            this.cboClass.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboClass.Location = new System.Drawing.Point(177, 64);
            this.cboClass.Name = "cboClass";
            this.cboClass.Size = new System.Drawing.Size(500, 23);
            this.cboClass.TabIndex = 8;
            // 
            // cboRoom
            // 
            this.cboRoom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboRoom.Location = new System.Drawing.Point(177, 108);
            this.cboRoom.Name = "cboRoom";
            this.cboRoom.Size = new System.Drawing.Size(500, 23);
            this.cboRoom.TabIndex = 9;
            // 
            // cboShift
            // 
            this.cboShift.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboShift.Location = new System.Drawing.Point(177, 152);
            this.cboShift.Name = "cboShift";
            this.cboShift.Size = new System.Drawing.Size(500, 23);
            this.cboShift.TabIndex = 10;
            // 
            // cboStatus
            // 
            this.cboStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboStatus.Items.AddRange(new object[] {
            "Đã lên lịch",
            "Hoàn thành",
            "Đã hủy"});
            this.cboStatus.Location = new System.Drawing.Point(177, 284);
            this.cboStatus.Name = "cboStatus";
            this.cboStatus.Size = new System.Drawing.Size(500, 23);
            this.cboStatus.TabIndex = 11;
            // 
            // dtpDate
            // 
            this.dtpDate.CustomFormat = "dd/MM/yyyy";
            this.dtpDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDate.Location = new System.Drawing.Point(177, 198);
            this.dtpDate.Name = "dtpDate";
            this.dtpDate.Size = new System.Drawing.Size(500, 23);
            this.dtpDate.TabIndex = 12;
            // 
            // nudStudentCount
            // 
            this.nudStudentCount.Location = new System.Drawing.Point(177, 240);
            this.nudStudentCount.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.nudStudentCount.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudStudentCount.Name = "nudStudentCount";
            this.nudStudentCount.Size = new System.Drawing.Size(500, 23);
            this.nudStudentCount.TabIndex = 13;
            this.nudStudentCount.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // pnlSuggestion
            // 
            this.pnlSuggestion.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlSuggestion.BackColor = System.Drawing.Color.White;
            this.pnlSuggestion.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlSuggestion.Controls.Add(this.lblSuggestionTitle);
            this.pnlSuggestion.Controls.Add(this.lblSuggestionSubtitle);
            this.pnlSuggestion.Controls.Add(this.btnRefreshSuggestions);
            this.pnlSuggestion.Controls.Add(this.pnlSuggestionRooms);
            this.pnlSuggestion.Controls.Add(this.lblSuggestionState);
            this.pnlSuggestion.Location = new System.Drawing.Point(37, 338);
            this.pnlSuggestion.Name = "pnlSuggestion";
            this.pnlSuggestion.Size = new System.Drawing.Size(640, 190);
            this.pnlSuggestion.TabIndex = 14;
            this.pnlSuggestion.Resize += new System.EventHandler(this.PnlSuggestion_Resize);
            // 
            // lblSuggestionTitle
            // 
            this.lblSuggestionTitle.AutoSize = true;
            this.lblSuggestionTitle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblSuggestionTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblSuggestionTitle.Location = new System.Drawing.Point(16, 12);
            this.lblSuggestionTitle.Name = "lblSuggestionTitle";
            this.lblSuggestionTitle.Size = new System.Drawing.Size(198, 19);
            this.lblSuggestionTitle.TabIndex = 0;
            this.lblSuggestionTitle.Text = "Palette đề xuất phòng trống";
            // 
            // lblSuggestionSubtitle
            // 
            this.lblSuggestionSubtitle.AutoSize = true;
            this.lblSuggestionSubtitle.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblSuggestionSubtitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblSuggestionSubtitle.Location = new System.Drawing.Point(16, 38);
            this.lblSuggestionSubtitle.Name = "lblSuggestionSubtitle";
            this.lblSuggestionSubtitle.Size = new System.Drawing.Size(300, 15);
            this.lblSuggestionSubtitle.TabIndex = 1;
            this.lblSuggestionSubtitle.Text = "Dựa trên ngày, ca học, sĩ số và phòng chưa bị trùng lịch";
            // 
            // btnRefreshSuggestions
            // 
            this.btnRefreshSuggestions.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRefreshSuggestions.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.btnRefreshSuggestions.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.btnRefreshSuggestions.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefreshSuggestions.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.btnRefreshSuggestions.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.btnRefreshSuggestions.Location = new System.Drawing.Point(506, 14);
            this.btnRefreshSuggestions.Name = "btnRefreshSuggestions";
            this.btnRefreshSuggestions.Size = new System.Drawing.Size(116, 30);
            this.btnRefreshSuggestions.TabIndex = 2;
            this.btnRefreshSuggestions.Text = "Làm mới";
            this.btnRefreshSuggestions.UseVisualStyleBackColor = false;
            // 
            // pnlSuggestionRooms
            // 
            this.pnlSuggestionRooms.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlSuggestionRooms.AutoScroll = true;
            this.pnlSuggestionRooms.BackColor = System.Drawing.Color.White;
            this.pnlSuggestionRooms.Location = new System.Drawing.Point(16, 66);
            this.pnlSuggestionRooms.Name = "pnlSuggestionRooms";
            this.pnlSuggestionRooms.Size = new System.Drawing.Size(606, 109);
            this.pnlSuggestionRooms.TabIndex = 3;
            // 
            // lblSuggestionState
            // 
            this.lblSuggestionState.AutoSize = true;
            this.lblSuggestionState.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblSuggestionState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblSuggestionState.Location = new System.Drawing.Point(18, 88);
            this.lblSuggestionState.Name = "lblSuggestionState";
            this.lblSuggestionState.Size = new System.Drawing.Size(0, 15);
            this.lblSuggestionState.TabIndex = 4;
            this.lblSuggestionState.Visible = false;
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(577, 550);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(100, 34);
            this.btnSave.TabIndex = 15;
            this.btnSave.Text = "Lưu";
            this.btnSave.UseVisualStyleBackColor = false;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.BackColor = System.Drawing.Color.White;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(213)))), ((int)(((byte)(225)))));
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnCancel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.btnCancel.Location = new System.Drawing.Point(454, 550);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 34);
            this.btnCancel.TabIndex = 16;
            this.btnCancel.Text = "Hủy";
            this.btnCancel.UseVisualStyleBackColor = false;
            // 
            // FrmScheduleEditor2
            // 
            this.AcceptButton = this.btnSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(248)))), ((int)(((byte)(252)))));
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(720, 596);
            this.Controls.Add(this.lblUser);
            this.Controls.Add(this.lblClass);
            this.Controls.Add(this.lblRoom);
            this.Controls.Add(this.lblShift);
            this.Controls.Add(this.lblDate);
            this.Controls.Add(this.lblStudentCount);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.cboUser);
            this.Controls.Add(this.cboClass);
            this.Controls.Add(this.cboRoom);
            this.Controls.Add(this.cboShift);
            this.Controls.Add(this.cboStatus);
            this.Controls.Add(this.dtpDate);
            this.Controls.Add(this.nudStudentCount);
            this.Controls.Add(this.pnlSuggestion);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnCancel);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmScheduleEditor2";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Tạo lịch";
            ((System.ComponentModel.ISupportInitialize)(this.nudStudentCount)).EndInit();
            this.pnlSuggestion.ResumeLayout(false);
            this.pnlSuggestion.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void ApplyModeLayout()
        {
            this.Text = this.editMode ? "Chỉnh sửa lịch" : "Tạo lịch";
            this.ClientSize = new Size(720, this.editMode ? 620 : 576);
            this.lblStatus.Visible = this.editMode;
            this.cboStatus.Visible = this.editMode;
            this.pnlSuggestion.Top = this.editMode ? 336 : 292;
            this.pnlSuggestion.Height = 190;
            this.btnCancel.Top = this.ClientSize.Height - 62;
            this.btnSave.Top = this.ClientSize.Height - 62;
            PnlSuggestion_Resize(this.pnlSuggestion, EventArgs.Empty);
        }

        private void PnlSuggestion_Resize(object sender, EventArgs e)
        {
            this.btnRefreshSuggestions.Left = this.pnlSuggestion.Width - this.btnRefreshSuggestions.Width - 16;
            this.pnlSuggestionRooms.Width = this.pnlSuggestion.Width - 32;
            this.pnlSuggestionRooms.Height = Math.Max(70, this.pnlSuggestion.Height - this.pnlSuggestionRooms.Top - 14);
            UpdateSuggestionScrollSize();
        }

        private void LoadLookups()
        {
            BindLookup(this.cboUser, this.repository.GetUsers());
            BindLookup(this.cboClass, this.repository.GetClasses());
            BindLookup(this.cboRoom, this.repository.GetRooms());
            BindLookup(this.cboShift, this.repository.GetShifts());
        }

        private void WireRuntimeEvents()
        {
            this.cboShift.SelectedIndexChanged += this.ScheduleInputChanged;
            this.dtpDate.ValueChanged += this.ScheduleInputChanged;
            this.nudStudentCount.ValueChanged += this.ScheduleInputChanged;
            this.btnRefreshSuggestions.Click += this.BtnRefreshSuggestions_Click;
            this.btnSave.Click += this.BtnSave_Click;
        }

        private static void BindLookup(ComboBox comboBox, List<LookupItem> items)
        {
            comboBox.DataSource = items;
            comboBox.DisplayMember = "Name";
            comboBox.ValueMember = "Id";
        }

        private void BindSchedule()
        {
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

        private void RefreshRoomSuggestions()
        {
            if (this.repository == null || this.cboShift.SelectedValue == null)
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

            UpdateSuggestionScrollSize();
            this.pnlSuggestionRooms.ResumeLayout();
        }

        private void UpdateSuggestionScrollSize()
        {
            if (this.pnlSuggestionRooms == null || this.pnlSuggestionRooms.Controls.Count == 0)
            {
                return;
            }

            int cardWidthWithMargin = 144;
            int cardHeightWithMargin = 76;
            int visibleWidth = Math.Max(1, this.pnlSuggestionRooms.ClientSize.Width - SystemInformation.VerticalScrollBarWidth);
            int cardsPerRow = Math.Max(1, visibleWidth / cardWidthWithMargin);
            int rowCount = (int)Math.Ceiling(this.pnlSuggestionRooms.Controls.Count / (double)cardsPerRow);
            int contentHeight = rowCount * cardHeightWithMargin;
            this.pnlSuggestionRooms.AutoScrollMinSize = new Size(0, Math.Max(0, contentHeight));
        }

        private Button CreateRoomSuggestionCard(RoomSuggestionItem room)
        {
            bool selected = Convert.ToString(this.cboRoom.SelectedValue) == room.MaPhong;
            Button card = new Button();
            card.BackColor = selected ? Color.FromArgb(239, 246, 255) : Color.FromArgb(248, 250, 252);
            card.FlatAppearance.BorderColor = selected ? Color.FromArgb(37, 99, 235) : Color.FromArgb(226, 232, 240);
            card.FlatStyle = FlatStyle.Flat;
            card.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            card.ForeColor = Color.FromArgb(15, 23, 42);
            card.Margin = new Padding(0, 0, 8, 10);
            card.Size = new Size(136, 66);
            card.Tag = room;
            card.Text = string.Format("{0}\nSức chứa {1} | {2} máy", room.TenPhong, room.SucChua, room.SoMay);
            card.TextAlign = ContentAlignment.MiddleLeft;
            card.Click += RoomSuggestionCard_Click;
            return card;
        }

        private void RoomSuggestionCard_Click(object sender, EventArgs e)
        {
            Button card = sender as Button;
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
