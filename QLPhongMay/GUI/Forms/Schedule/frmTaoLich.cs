using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using QLPhongMay.BLL;
using QLPhongMay.DAL;

namespace QLPhongMay.GUI.Forms.Schedule
{
    public partial class frmTaoLich : Form
    {
        private readonly ScheduleRepository scheduleRepository;
        private List<ScheduleRepository.ShiftOption> shifts;
        private List<ScheduleRepository.ClassOption> classes;
        private List<ScheduleRepository.AvailableRoom> availableRooms;
        private int? selectedRoomId;

        private Panel pnlRoot;
        private Button btnBack;
        private Label lblTitle;
        private Label lblSubtitle;
        private Panel pnlFilter;
        private Label lblDate;
        private DateTimePicker dtpDate;
        private Label lblShift;
        private ComboBox cboShift;
        private Label lblClass;
        private ComboBox cboClass;
        private Button btnFindRooms;
        private Label lblPaletteTitle;
        private FlowLayoutPanel pnlRooms;
        private Panel pnlSummary;
        private Label lblSummaryTitle;
        private Label lblSummaryText;
        private Button btnCreateSchedule;
        private Label lblStatus;

        public frmTaoLich()
        {
            this.scheduleRepository = new ScheduleRepository();
            this.shifts = new List<ScheduleRepository.ShiftOption>();
            this.classes = new List<ScheduleRepository.ClassOption>();
            this.availableRooms = new List<ScheduleRepository.AvailableRoom>();

            InitializeComponent();
            this.Load += new EventHandler(this.frmTaoLich_Load);
        }

        private void InitializeComponent()
        {
            this.pnlRoot = new System.Windows.Forms.Panel();
            this.btnBack = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblSubtitle = new System.Windows.Forms.Label();
            this.pnlFilter = new System.Windows.Forms.Panel();
            this.lblDate = new System.Windows.Forms.Label();
            this.dtpDate = new System.Windows.Forms.DateTimePicker();
            this.lblShift = new System.Windows.Forms.Label();
            this.cboShift = new System.Windows.Forms.ComboBox();
            this.lblClass = new System.Windows.Forms.Label();
            this.cboClass = new System.Windows.Forms.ComboBox();
            this.btnFindRooms = new System.Windows.Forms.Button();
            this.lblPaletteTitle = new System.Windows.Forms.Label();
            this.pnlRooms = new System.Windows.Forms.FlowLayoutPanel();
            this.pnlSummary = new System.Windows.Forms.Panel();
            this.lblSummaryTitle = new System.Windows.Forms.Label();
            this.lblSummaryText = new System.Windows.Forms.Label();
            this.btnCreateSchedule = new System.Windows.Forms.Button();
            this.lblStatus = new System.Windows.Forms.Label();
            this.pnlRoot.SuspendLayout();
            this.pnlFilter.SuspendLayout();
            this.pnlSummary.SuspendLayout();
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
            this.pnlRoot.Controls.Add(this.pnlFilter);
            this.pnlRoot.Controls.Add(this.lblPaletteTitle);
            this.pnlRoot.Controls.Add(this.pnlRooms);
            this.pnlRoot.Controls.Add(this.pnlSummary);
            this.pnlRoot.Controls.Add(this.lblStatus);
            this.pnlRoot.Location = new System.Drawing.Point(28, 24);
            this.pnlRoot.Name = "pnlRoot";
            this.pnlRoot.Size = new System.Drawing.Size(1124, 708);
            this.pnlRoot.TabIndex = 0;
            // 
            // btnBack
            // 
            this.btnBack.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.btnBack.Location = new System.Drawing.Point(60, 21);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(128, 46);
            this.btnBack.TabIndex = 0;
            this.btnBack.Click += new System.EventHandler(this.BtnBack_Click);
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 21F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblTitle.Location = new System.Drawing.Point(383, 10);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(261, 38);
            this.lblTitle.TabIndex = 1;
            this.lblTitle.Text = "Tạo lịch thực hành";
            // 
            // lblSubtitle
            // 
            this.lblSubtitle.AutoSize = true;
            this.lblSubtitle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblSubtitle.Location = new System.Drawing.Point(338, 48);
            this.lblSubtitle.Name = "lblSubtitle";
            this.lblSubtitle.Size = new System.Drawing.Size(330, 19);
            this.lblSubtitle.TabIndex = 2;
            this.lblSubtitle.Text = "Chọn ngày, ca học, lớp học và phòng máy còn trống";
            // 
            // pnlFilter
            // 
            this.pnlFilter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlFilter.BackColor = System.Drawing.Color.White;
            this.pnlFilter.Controls.Add(this.lblDate);
            this.pnlFilter.Controls.Add(this.dtpDate);
            this.pnlFilter.Controls.Add(this.lblShift);
            this.pnlFilter.Controls.Add(this.cboShift);
            this.pnlFilter.Controls.Add(this.lblClass);
            this.pnlFilter.Controls.Add(this.cboClass);
            this.pnlFilter.Controls.Add(this.btnFindRooms);
            this.pnlFilter.Location = new System.Drawing.Point(0, 96);
            this.pnlFilter.Name = "pnlFilter";
            this.pnlFilter.Size = new System.Drawing.Size(1124, 104);
            this.pnlFilter.TabIndex = 3;
            this.pnlFilter.Paint += new System.Windows.Forms.PaintEventHandler(this.PanelBorder_Paint);
            // 
            // lblDate
            // 
            this.lblDate.Location = new System.Drawing.Point(0, 0);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(100, 23);
            this.lblDate.TabIndex = 0;
            // 
            // dtpDate
            // 
            this.dtpDate.CustomFormat = "dd/MM/yyyy";
            this.dtpDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDate.Location = new System.Drawing.Point(24, 48);
            this.dtpDate.MinDate = new System.DateTime(2026, 5, 23, 0, 0, 0, 0);
            this.dtpDate.Name = "dtpDate";
            this.dtpDate.Size = new System.Drawing.Size(180, 23);
            this.dtpDate.TabIndex = 1;
            this.dtpDate.ValueChanged += new System.EventHandler(this.FilterChanged);
            // 
            // lblShift
            // 
            this.lblShift.Location = new System.Drawing.Point(0, 0);
            this.lblShift.Name = "lblShift";
            this.lblShift.Size = new System.Drawing.Size(100, 23);
            this.lblShift.TabIndex = 2;
            // 
            // cboShift
            // 
            this.cboShift.Location = new System.Drawing.Point(0, -3);
            this.cboShift.Name = "cboShift";
            this.cboShift.Size = new System.Drawing.Size(121, 23);
            this.cboShift.TabIndex = 3;
            this.cboShift.SelectedIndexChanged += new System.EventHandler(this.FilterChanged);
            // 
            // lblClass
            // 
            this.lblClass.Location = new System.Drawing.Point(0, 0);
            this.lblClass.Name = "lblClass";
            this.lblClass.Size = new System.Drawing.Size(100, 23);
            this.lblClass.TabIndex = 4;
            // 
            // cboClass
            // 
            this.cboClass.Location = new System.Drawing.Point(0, 0);
            this.cboClass.Name = "cboClass";
            this.cboClass.Size = new System.Drawing.Size(121, 23);
            this.cboClass.TabIndex = 5;
            this.cboClass.SelectedIndexChanged += new System.EventHandler(this.FilterChanged);
            // 
            // btnFindRooms
            // 
            this.btnFindRooms.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFindRooms.Location = new System.Drawing.Point(902, 44);
            this.btnFindRooms.Name = "btnFindRooms";
            this.btnFindRooms.Size = new System.Drawing.Size(198, 38);
            this.btnFindRooms.TabIndex = 6;
            this.btnFindRooms.Click += new System.EventHandler(this.BtnFindRooms_Click);
            // 
            // lblPaletteTitle
            // 
            this.lblPaletteTitle.AutoSize = true;
            this.lblPaletteTitle.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPaletteTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblPaletteTitle.Location = new System.Drawing.Point(0, 226);
            this.lblPaletteTitle.Name = "lblPaletteTitle";
            this.lblPaletteTitle.Size = new System.Drawing.Size(188, 25);
            this.lblPaletteTitle.TabIndex = 4;
            this.lblPaletteTitle.Text = "Phòng trống đề xuất";
            // 
            // pnlRooms
            // 
            this.pnlRooms.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlRooms.AutoScroll = true;
            this.pnlRooms.BackColor = System.Drawing.Color.White;
            this.pnlRooms.Location = new System.Drawing.Point(0, 266);
            this.pnlRooms.Name = "pnlRooms";
            this.pnlRooms.Padding = new System.Windows.Forms.Padding(16);
            this.pnlRooms.Size = new System.Drawing.Size(760, 376);
            this.pnlRooms.TabIndex = 5;
            this.pnlRooms.Paint += new System.Windows.Forms.PaintEventHandler(this.PanelBorder_Paint);
            // 
            // pnlSummary
            // 
            this.pnlSummary.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlSummary.BackColor = System.Drawing.Color.White;
            this.pnlSummary.Controls.Add(this.lblSummaryTitle);
            this.pnlSummary.Controls.Add(this.lblSummaryText);
            this.pnlSummary.Controls.Add(this.btnCreateSchedule);
            this.pnlSummary.Location = new System.Drawing.Point(788, 266);
            this.pnlSummary.Name = "pnlSummary";
            this.pnlSummary.Size = new System.Drawing.Size(336, 376);
            this.pnlSummary.TabIndex = 6;
            this.pnlSummary.Paint += new System.Windows.Forms.PaintEventHandler(this.PanelBorder_Paint);
            // 
            // lblSummaryTitle
            // 
            this.lblSummaryTitle.AutoSize = true;
            this.lblSummaryTitle.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSummaryTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblSummaryTitle.Location = new System.Drawing.Point(22, 22);
            this.lblSummaryTitle.Name = "lblSummaryTitle";
            this.lblSummaryTitle.Size = new System.Drawing.Size(130, 25);
            this.lblSummaryTitle.TabIndex = 0;
            this.lblSummaryTitle.Text = "Thông tin lịch";
            // 
            // lblSummaryText
            // 
            this.lblSummaryText.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSummaryText.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.lblSummaryText.Location = new System.Drawing.Point(24, 68);
            this.lblSummaryText.Name = "lblSummaryText";
            this.lblSummaryText.Size = new System.Drawing.Size(286, 190);
            this.lblSummaryText.TabIndex = 1;
            this.lblSummaryText.Text = "Chọn đầy đủ thông tin và chọn một phòng trống để tạo lịch.";
            // 
            // btnCreateSchedule
            // 
            this.btnCreateSchedule.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCreateSchedule.Enabled = false;
            this.btnCreateSchedule.Location = new System.Drawing.Point(24, 304);
            this.btnCreateSchedule.Name = "btnCreateSchedule";
            this.btnCreateSchedule.Size = new System.Drawing.Size(286, 44);
            this.btnCreateSchedule.TabIndex = 2;
            this.btnCreateSchedule.Click += new System.EventHandler(this.BtnCreateSchedule_Click);
            // 
            // lblStatus
            // 
            this.lblStatus.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblStatus.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblStatus.Location = new System.Drawing.Point(21, 657);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(1124, 28);
            this.lblStatus.TabIndex = 7;
            this.lblStatus.Text = "Sẵn sàng";
            // 
            // frmTaoLich
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(247)))), ((int)(((byte)(251)))));
            this.ClientSize = new System.Drawing.Size(1180, 760);
            this.Controls.Add(this.pnlRoot);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MinimumSize = new System.Drawing.Size(1040, 680);
            this.Name = "frmTaoLich";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Tạo lịch thực hành";
            this.pnlRoot.ResumeLayout(false);
            this.pnlRoot.PerformLayout();
            this.pnlFilter.ResumeLayout(false);
            this.pnlSummary.ResumeLayout(false);
            this.pnlSummary.PerformLayout();
            this.ResumeLayout(false);

        }

        private void frmTaoLich_Load(object sender, EventArgs e)
        {
            LoadOptions();
            LoadAvailableRooms();
        }

        private void LoadOptions()
        {
            try
            {
                this.shifts = this.scheduleRepository.GetShifts().ToList();
                this.classes = this.scheduleRepository.GetClasses().ToList();

                this.cboShift.DataSource = this.shifts;
                this.cboShift.DisplayMember = "DisplayName";
                this.cboShift.ValueMember = "MaCa";

                this.cboClass.DataSource = this.classes;
                this.cboClass.DisplayMember = "DisplayName";
                this.cboClass.ValueMember = "MaLop";
            }
            catch (Exception ex)
            {
                ShowDataError("Không thể tải dữ liệu ca học và lớp học.", ex);
            }
        }

        private void LoadAvailableRooms()
        {
            ScheduleRepository.ShiftOption shift = this.cboShift.SelectedItem as ScheduleRepository.ShiftOption;
            ScheduleRepository.ClassOption classOption = this.cboClass.SelectedItem as ScheduleRepository.ClassOption;

            this.selectedRoomId = null;
            this.btnCreateSchedule.Enabled = false;
            this.pnlRooms.Controls.Clear();

            if (shift == null || classOption == null)
            {
                this.lblStatus.Text = "Chưa có đủ dữ liệu ca học hoặc lớp học.";
                UpdateSummary(null);
                return;
            }

            try
            {
                this.availableRooms = this.scheduleRepository
                    .GetAvailableRooms(this.dtpDate.Value.Date, shift.MaCa, classOption.SiSo)
                    .ToList();

                foreach (ScheduleRepository.AvailableRoom room in this.availableRooms)
                {
                    this.pnlRooms.Controls.Add(CreateRoomCard(room));
                }

                if (this.availableRooms.Count == 0)
                {
                    Label emptyLabel = new Label();
                    emptyLabel.AutoSize = false;
                    emptyLabel.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
                    emptyLabel.ForeColor = Color.FromArgb(100, 116, 139);
                    emptyLabel.Size = new Size(680, 40);
                    emptyLabel.Text = "Không có phòng trống phù hợp với ca học, ngày và sĩ số đã chọn.";
                    this.pnlRooms.Controls.Add(emptyLabel);
                }

                this.lblStatus.Text = string.Format("Tìm thấy {0} phòng trống phù hợp.", this.availableRooms.Count);
                UpdateSummary(null);
            }
            catch (Exception ex)
            {
                ShowDataError("Không thể tải danh sách phòng trống.", ex);
            }
        }

        private Control CreateRoomCard(ScheduleRepository.AvailableRoom room)
        {
            Panel card = new Panel();
            Label title = new Label();
            Label detail = new Label();
            Label badge = new Label();

            card.BackColor = Color.White;
            card.BorderStyle = BorderStyle.FixedSingle;
            card.Cursor = Cursors.Hand;
            card.Margin = new Padding(8);
            card.Name = "room_" + room.MaPhong;
            card.Size = new Size(218, 118);
            card.Tag = room;
            card.Click += new EventHandler(this.RoomCard_Click);

            title.AutoSize = true;
            title.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            title.ForeColor = Color.FromArgb(15, 23, 42);
            title.Location = new Point(14, 12);
            title.Text = room.TenPhong;
            title.Click += new EventHandler(this.RoomCardChild_Click);

            detail.AutoSize = false;
            detail.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            detail.ForeColor = Color.FromArgb(71, 85, 105);
            detail.Location = new Point(16, 46);
            detail.Size = new Size(188, 42);
            detail.Text = string.Format("Sức chứa: {0} SV\nMáy tốt: {1}/{2}", room.SucChua, room.SoMayTot, room.SoMay);
            detail.Click += new EventHandler(this.RoomCardChild_Click);

            badge.AutoSize = false;
            badge.BackColor = Color.FromArgb(240, 253, 244);
            badge.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold, GraphicsUnit.Point, 0);
            badge.ForeColor = Color.FromArgb(22, 101, 52);
            badge.Location = new Point(14, 88);
            badge.Size = new Size(90, 22);
            badge.Text = "Còn trống";
            badge.TextAlign = ContentAlignment.MiddleCenter;
            badge.Click += new EventHandler(this.RoomCardChild_Click);

            card.Controls.Add(title);
            card.Controls.Add(detail);
            card.Controls.Add(badge);
            return card;
        }

        private void RoomCard_Click(object sender, EventArgs e)
        {
            Panel card = sender as Panel;
            if (card == null)
            {
                return;
            }

            SelectRoomCard(card);
        }

        private void RoomCardChild_Click(object sender, EventArgs e)
        {
            Control child = sender as Control;
            if (child != null && child.Parent is Panel)
            {
                SelectRoomCard((Panel)child.Parent);
            }
        }

        private void SelectRoomCard(Panel selectedCard)
        {
            foreach (Control control in this.pnlRooms.Controls)
            {
                Panel card = control as Panel;
                if (card != null)
                {
                    card.BackColor = Color.White;
                }
            }

            ScheduleRepository.AvailableRoom room = selectedCard.Tag as ScheduleRepository.AvailableRoom;
            if (room == null)
            {
                return;
            }

            selectedCard.BackColor = Color.FromArgb(239, 246, 255);
            this.selectedRoomId = room.MaPhong;
            this.btnCreateSchedule.Enabled = true;
            UpdateSummary(room);
        }

        private void UpdateSummary(ScheduleRepository.AvailableRoom room)
        {
            ScheduleRepository.ShiftOption shift = this.cboShift.SelectedItem as ScheduleRepository.ShiftOption;
            ScheduleRepository.ClassOption classOption = this.cboClass.SelectedItem as ScheduleRepository.ClassOption;

            string roomText = room == null
                ? "Phòng: Chưa chọn"
                : string.Format("Phòng: {0} ({1} SV)", room.TenPhong, room.SucChua);

            string classText = classOption == null
                ? "Lớp: Chưa chọn"
                : string.Format("Lớp: {0} - {1} SV", classOption.TenLop, classOption.SiSo);

            string shiftText = shift == null
                ? "Ca: Chưa chọn"
                : "Ca: " + shift.DisplayName;

            this.lblSummaryText.Text = string.Format(
                "Ngày: {0:dd/MM/yyyy}\n{1}\n{2}\n{3}\n\nTrạng thái: Đã lên lịch",
                this.dtpDate.Value.Date,
                shiftText,
                classText,
                roomText);
        }

        private void BtnFindRooms_Click(object sender, EventArgs e)
        {
            LoadAvailableRooms();
        }

        private void FilterChanged(object sender, EventArgs e)
        {
            this.selectedRoomId = null;
            this.btnCreateSchedule.Enabled = false;
            UpdateSummary(null);
        }

        private void BtnCreateSchedule_Click(object sender, EventArgs e)
        {
            ScheduleRepository.ShiftOption shift = this.cboShift.SelectedItem as ScheduleRepository.ShiftOption;
            ScheduleRepository.ClassOption classOption = this.cboClass.SelectedItem as ScheduleRepository.ClassOption;

            if (shift == null || classOption == null || !this.selectedRoomId.HasValue)
            {
                MessageBox.Show("Vui lòng chọn đầy đủ ngày, ca học, lớp học và phòng trống.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!Session.IsAuthenticated || Session.CurrentUser == null)
            {
                MessageBox.Show("Phiên đăng nhập không hợp lệ. Vui lòng đăng nhập lại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                if (this.scheduleRepository.ScheduleExists(this.selectedRoomId.Value, shift.MaCa, this.dtpDate.Value.Date))
                {
                    MessageBox.Show("Phòng này vừa được xếp lịch cho ca đã chọn. Vui lòng chọn phòng khác.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    LoadAvailableRooms();
                    return;
                }

                this.scheduleRepository.CreateSchedule(
                    Session.CurrentUser.TenDangNhap,
                    this.selectedRoomId.Value,
                    shift.MaCa,
                    classOption.MaLop,
                    this.dtpDate.Value.Date,
                    classOption.SiSo);

                MessageBox.Show("Đã tạo lịch thực hành thành công.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadAvailableRooms();
            }
            catch (Exception ex)
            {
                ShowDataError("Không thể tạo lịch thực hành.", ex);
            }
        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private static void ConfigureLabel(Label label, string text, int x, int y)
        {
            label.AutoSize = true;
            label.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label.ForeColor = Color.FromArgb(51, 65, 85);
            label.Location = new Point(x, y);
            label.Text = text;
        }

        private static void ConfigureComboBox(ComboBox comboBox, int x, int y, int width)
        {
            comboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            comboBox.Location = new Point(x, y);
            comboBox.Size = new Size(width, 28);
        }

        private static void ConfigureButton(Button button, string text, Color backColor, Color foreColor)
        {
            button.BackColor = backColor;
            button.Cursor = Cursors.Hand;
            button.FlatStyle = FlatStyle.Flat;
            button.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button.ForeColor = foreColor;
            button.Text = text;
            button.UseVisualStyleBackColor = false;
            button.FlatAppearance.BorderSize = 0;
        }

        private void PanelBorder_Paint(object sender, PaintEventArgs e)
        {
            Control panel = (Control)sender;
            using (Pen pen = new Pen(Color.FromArgb(226, 232, 240)))
            {
                e.Graphics.DrawRectangle(pen, 0, 0, panel.Width - 1, panel.Height - 1);
            }
        }

        private static void ShowDataError(string message, Exception ex)
        {
            MessageBox.Show(message + "\n" + ex.Message, "Lỗi dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
