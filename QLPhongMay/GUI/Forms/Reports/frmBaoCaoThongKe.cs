using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using QLPhongMay.DAL;
using QLPhongMay.DTO;

namespace QLPhongMay.GUI.Forms.Reports
{
    public partial class frmBaoCaoThongKe : Form
    {
        private ReportRepository repository;
        private int activeReportTab;

        private Panel rootPanel;
        private Label lblTitle;
        private Label lblSubtitle;
        private Panel tabPanel;
        private Button btnScheduleTab;
        private Button btnRoomTab;
        private Button btnStatusTab;
        private Panel filterPanel;
        private Label lblFrom;
        private Label lblTo;
        private Label lblGroup;
        private Label lblRoom;
        private DateTimePicker dtpFrom;
        private DateTimePicker dtpTo;
        private ComboBox cboGroupMode;
        private ComboBox cboRoom;
        private Button btnFilter;
        private Button btnClear;
        private Panel contentPanel;

        private ucBaoCaoLichThucHanh ucSchedule;
        private ucBaoCaoSuDungPhong ucRoom;
        private ucBaoCaoTrangThaiMay ucStatus;

        public frmBaoCaoThongKe()
        {
            InitializeComponent();

            if (IsDesignerMode())
            {
                return;
            }

            repository = new ReportRepository();
            SetupRuntimeControls();
            Load += FrmBaoCaoThongKe_Load;
            Resize += FrmBaoCaoThongKe_Resize;
        }

        private void InitializeComponent()
        {
            rootPanel = new Panel();
            lblTitle = new Label();
            lblSubtitle = new Label();
            tabPanel = new Panel();
            btnScheduleTab = new Button();
            btnRoomTab = new Button();
            btnStatusTab = new Button();
            filterPanel = new Panel();
            lblFrom = new Label();
            lblTo = new Label();
            lblGroup = new Label();
            lblRoom = new Label();
            dtpFrom = new DateTimePicker();
            dtpTo = new DateTimePicker();
            cboGroupMode = new ComboBox();
            cboRoom = new ComboBox();
            btnFilter = new Button();
            btnClear = new Button();
            contentPanel = new Panel();
            rootPanel.SuspendLayout();
            tabPanel.SuspendLayout();
            filterPanel.SuspendLayout();
            SuspendLayout();

            rootPanel.AutoScroll = true;
            rootPanel.BackColor = Color.FromArgb(239, 246, 255);
            rootPanel.Controls.Add(lblTitle);
            rootPanel.Controls.Add(lblSubtitle);
            rootPanel.Controls.Add(tabPanel);
            rootPanel.Controls.Add(filterPanel);
            rootPanel.Controls.Add(contentPanel);
            rootPanel.Dock = DockStyle.Fill;
            rootPanel.Location = new Point(0, 0);
            rootPanel.Name = "rootPanel";
            rootPanel.Size = new Size(1280, 760);

            lblTitle.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(15, 23, 42);
            lblTitle.Location = new Point(28, 22);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(560, 38);
            lblTitle.Text = "Quản lý Báo cáo & Thống kê";
            lblTitle.UseMnemonic = false;

            lblSubtitle.Font = new Font("Segoe UI", 10F);
            lblSubtitle.ForeColor = Color.FromArgb(100, 116, 139);
            lblSubtitle.Location = new Point(30, 62);
            lblSubtitle.Name = "lblSubtitle";
            lblSubtitle.Size = new Size(650, 24);
            lblSubtitle.Text = "Theo dõi lịch thực hành, mức sử dụng phòng và trạng thái máy tính";

            tabPanel.BackColor = Color.White;
            tabPanel.Controls.Add(btnScheduleTab);
            tabPanel.Controls.Add(btnRoomTab);
            tabPanel.Controls.Add(btnStatusTab);
            tabPanel.Location = new Point(24, 106);
            tabPanel.Name = "tabPanel";
            tabPanel.Size = new Size(1230, 46);

            btnScheduleTab.BackColor = Color.White;
            btnScheduleTab.Cursor = Cursors.Hand;
            btnScheduleTab.FlatAppearance.BorderColor = Color.FromArgb(203, 213, 225);
            btnScheduleTab.FlatStyle = FlatStyle.Flat;
            btnScheduleTab.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnScheduleTab.ForeColor = Color.FromArgb(37, 99, 235);
            btnScheduleTab.Location = new Point(0, 0);
            btnScheduleTab.Size = new Size(410, 46);
            btnScheduleTab.Text = "Lịch thực hành theo thời gian";
            btnScheduleTab.UseMnemonic = false;
            btnScheduleTab.UseVisualStyleBackColor = false;

            btnRoomTab.BackColor = Color.White;
            btnRoomTab.Cursor = Cursors.Hand;
            btnRoomTab.FlatAppearance.BorderColor = Color.FromArgb(203, 213, 225);
            btnRoomTab.FlatStyle = FlatStyle.Flat;
            btnRoomTab.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnRoomTab.ForeColor = Color.FromArgb(37, 99, 235);
            btnRoomTab.Location = new Point(410, 0);
            btnRoomTab.Size = new Size(410, 46);
            btnRoomTab.Text = "Tỷ lệ sử dụng phòng máy";
            btnRoomTab.UseMnemonic = false;
            btnRoomTab.UseVisualStyleBackColor = false;

            btnStatusTab.BackColor = Color.White;
            btnStatusTab.Cursor = Cursors.Hand;
            btnStatusTab.FlatAppearance.BorderColor = Color.FromArgb(203, 213, 225);
            btnStatusTab.FlatStyle = FlatStyle.Flat;
            btnStatusTab.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnStatusTab.ForeColor = Color.FromArgb(37, 99, 235);
            btnStatusTab.Location = new Point(820, 0);
            btnStatusTab.Size = new Size(410, 46);
            btnStatusTab.Text = "Thống kê trạng thái máy tính";
            btnStatusTab.UseMnemonic = false;
            btnStatusTab.UseVisualStyleBackColor = false;
            btnScheduleTab.BackColor = Color.FromArgb(37, 99, 235);
            btnScheduleTab.ForeColor = Color.White;

            filterPanel.BackColor = Color.White;
            filterPanel.Controls.Add(lblFrom);
            filterPanel.Controls.Add(lblTo);
            filterPanel.Controls.Add(lblGroup);
            filterPanel.Controls.Add(lblRoom);
            filterPanel.Controls.Add(dtpFrom);
            filterPanel.Controls.Add(dtpTo);
            filterPanel.Controls.Add(cboGroupMode);
            filterPanel.Controls.Add(cboRoom);
            filterPanel.Controls.Add(btnFilter);
            filterPanel.Controls.Add(btnClear);
            filterPanel.Location = new Point(24, 166);
            filterPanel.Name = "filterPanel";
            filterPanel.Size = new Size(1230, 86);

            lblFrom.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            lblFrom.ForeColor = Color.FromArgb(71, 85, 105);
            lblFrom.Location = new Point(18, 12);
            lblFrom.Size = new Size(120, 20);
            lblFrom.Text = "Từ ngày";

            lblTo.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            lblTo.ForeColor = Color.FromArgb(71, 85, 105);
            lblTo.Location = new Point(244, 12);
            lblTo.Size = new Size(120, 20);
            lblTo.Text = "Đến ngày";

            lblGroup.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            lblGroup.ForeColor = Color.FromArgb(71, 85, 105);
            lblGroup.Location = new Point(477, 12);
            lblGroup.Size = new Size(120, 20);
            lblGroup.Text = "Nhóm lịch";

            lblRoom.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            lblRoom.ForeColor = Color.FromArgb(71, 85, 105);
            lblRoom.Location = new Point(693, 12);
            lblRoom.Size = new Size(120, 20);
            lblRoom.Text = "Phòng máy";

            dtpFrom.CustomFormat = "dd/MM/yyyy";
            dtpFrom.Font = new Font("Segoe UI", 10F);
            dtpFrom.Format = DateTimePickerFormat.Custom;
            dtpFrom.Location = new Point(18, 36);
            dtpFrom.Size = new Size(210, 30);

            dtpTo.CustomFormat = "dd/MM/yyyy";
            dtpTo.Font = new Font("Segoe UI", 10F);
            dtpTo.Format = DateTimePickerFormat.Custom;
            dtpTo.Location = new Point(244, 36);
            dtpTo.Size = new Size(210, 30);

            cboGroupMode.DropDownStyle = ComboBoxStyle.DropDownList;
            cboGroupMode.Font = new Font("Segoe UI", 10F);
            cboGroupMode.Items.AddRange(new object[] { "Theo ngày", "Theo tháng", "Theo năm" });
            cboGroupMode.Location = new Point(477, 35);
            cboGroupMode.Name = "cboGroupMode";
            cboGroupMode.Size = new Size(190, 31);

            cboRoom.DropDownStyle = ComboBoxStyle.DropDownList;
            cboRoom.Font = new Font("Segoe UI", 10F);
            cboRoom.Items.AddRange(new object[] { "Tất cả phòng" });
            cboRoom.Location = new Point(693, 35);
            cboRoom.Name = "cboRoom";
            cboRoom.Size = new Size(198, 31);

            btnFilter.BackColor = Color.FromArgb(34, 139, 34);
            btnFilter.Cursor = Cursors.Hand;
            btnFilter.FlatAppearance.BorderColor = Color.FromArgb(148, 163, 184);
            btnFilter.FlatStyle = FlatStyle.Flat;
            btnFilter.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnFilter.ForeColor = Color.White;
            btnFilter.Location = new Point(940, 32);
            btnFilter.Size = new Size(115, 34);
            btnFilter.Text = "Hiển thị";
            btnFilter.UseVisualStyleBackColor = false;

            btnClear.BackColor = Color.White;
            btnClear.Cursor = Cursors.Hand;
            btnClear.FlatAppearance.BorderColor = Color.FromArgb(148, 163, 184);
            btnClear.FlatStyle = FlatStyle.Flat;
            btnClear.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnClear.ForeColor = Color.FromArgb(37, 99, 235);
            btnClear.Location = new Point(1093, 32);
            btnClear.Size = new Size(115, 34);
            btnClear.Text = "Làm mới";
            btnClear.UseVisualStyleBackColor = false;

            contentPanel.BackColor = Color.White;
            contentPanel.Location = new Point(24, 272);
            contentPanel.Name = "contentPanel";
            contentPanel.Size = new Size(1230, 420);

            BackColor = Color.FromArgb(239, 246, 255);
            ClientSize = new Size(1280, 760);
            Controls.Add(rootPanel);
            Font = new Font("Segoe UI", 9F);
            MinimumSize = new Size(1100, 700);
            Name = "frmBaoCaoThongKe";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Quản lý Báo cáo & Thống kê";

            rootPanel.ResumeLayout(false);
            tabPanel.ResumeLayout(false);
            filterPanel.ResumeLayout(false);
            ResumeLayout(false);
        }

        private static bool IsDesignerMode()
        {
            return LicenseManager.UsageMode == LicenseUsageMode.Designtime
                || System.Diagnostics.Process.GetCurrentProcess().ProcessName.Equals("devenv", StringComparison.OrdinalIgnoreCase);
        }

        private void SetupRuntimeControls()
        {
            ucSchedule = new ucBaoCaoLichThucHanh();
            ucRoom = new ucBaoCaoSuDungPhong();
            ucStatus = new ucBaoCaoTrangThaiMay();
            AddReportControl(ucSchedule);
            AddReportControl(ucRoom);
            AddReportControl(ucStatus);

            cboGroupMode.Items.Clear();
            cboGroupMode.Items.Add(new ComboOption("Theo ngày", "day"));
            cboGroupMode.Items.Add(new ComboOption("Theo tháng", "month"));
            cboGroupMode.Items.Add(new ComboOption("Theo năm", "year"));
            cboGroupMode.SelectedIndex = 0;

            btnScheduleTab.Click += delegate { SelectReportTab(0); };
            btnRoomTab.Click += delegate { SelectReportTab(1); };
            btnStatusTab.Click += delegate { SelectReportTab(2); };
            btnFilter.Click += delegate { LoadReportData(); };
            btnClear.Click += BtnClear_Click;
        }

        private void AddReportControl(Control control)
        {
            control.Dock = DockStyle.Fill;
            control.Visible = false;
            contentPanel.Controls.Add(control);
        }

        private void FrmBaoCaoThongKe_Load(object sender, EventArgs e)
        {
            dtpFrom.Value = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            dtpTo.Value = DateTime.Today;
            LoadRoomFilter();
            LoadReportData();
            SelectReportTab(0);
        }

        private void FrmBaoCaoThongKe_Resize(object sender, EventArgs e)
        {
            LayoutControls();
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            dtpFrom.Value = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            dtpTo.Value = DateTime.Today;
            cboGroupMode.SelectedIndex = 0;
            cboRoom.SelectedIndex = 0;
            LoadReportData();
        }

        private void SelectReportTab(int tabIndex)
        {
            activeReportTab = tabIndex;
            filterPanel.Visible = tabIndex == 0;
            ucSchedule.Visible = tabIndex == 0;
            ucRoom.Visible = tabIndex == 1;
            ucStatus.Visible = tabIndex == 2;
            ucSchedule.BringToFront();
            ucRoom.BringToFront();
            ucStatus.BringToFront();

            ApplyTabStyle(btnScheduleTab, tabIndex == 0);
            ApplyTabStyle(btnRoomTab, tabIndex == 1);
            ApplyTabStyle(btnStatusTab, tabIndex == 2);
            LayoutControls();
        }

        private void LayoutControls()
        {
            int contentWidth = Math.Max(1040, ClientSize.Width - 48);
            int contentTop = activeReportTab == 0 ? 272 : 166;
            int contentHeight = Math.Max(420, ClientSize.Height - contentTop - 36);

            tabPanel.Size = new Size(contentWidth, 46);
            int tabWidth = contentWidth / 3;
            btnScheduleTab.Location = new Point(0, 0);
            btnRoomTab.Location = new Point(tabWidth, 0);
            btnStatusTab.Location = new Point(tabWidth * 2, 0);
            btnScheduleTab.Size = new Size(tabWidth, 46);
            btnRoomTab.Size = new Size(tabWidth, 46);
            btnStatusTab.Size = new Size(contentWidth - tabWidth * 2, 46);

            filterPanel.Size = new Size(contentWidth, 86);
            contentPanel.Location = new Point(24, contentTop);
            contentPanel.Size = new Size(contentWidth, contentHeight);
            rootPanel.AutoScrollMinSize = new Size(contentWidth + 48, contentTop + contentHeight + 24);
        }

        private void LoadRoomFilter()
        {
            cboRoom.Items.Clear();
            cboRoom.Items.Add(new RoomOption("Tất cả phòng", null));

            foreach (ReportChartItem room in repository.GetRooms())
            {
                cboRoom.Items.Add(new RoomOption(room.Label, room.Total));
            }

            cboRoom.SelectedIndex = 0;
        }

        private void LoadReportData()
        {
            DateTime fromDate = dtpFrom.Value.Date;
            DateTime toDate = dtpTo.Value.Date;

            if (fromDate > toDate)
            {
                MessageBox.Show("Từ ngày không được lớn hơn đến ngày.", "Dữ liệu lọc chưa hợp lệ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string groupMode = ((ComboOption)cboGroupMode.SelectedItem).Value;
            int? roomId = ((RoomOption)cboRoom.SelectedItem).RoomId;

            try
            {
                ucSchedule.LoadData(repository.GetScheduleReportRows(fromDate, toDate, groupMode, roomId));
                ucRoom.LoadData(repository.GetRoomUsageReportRows());
                ucStatus.LoadData(repository.GetComputerStatus(null));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể tải dữ liệu báo cáo.\n" + ex.Message, "Lỗi dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static void ApplyTabStyle(Button button, bool active)
        {
            button.BackColor = active ? Color.FromArgb(37, 99, 235) : Color.White;
            button.ForeColor = active ? Color.White : Color.FromArgb(37, 99, 235);
        }

        private class ComboOption
        {
            public ComboOption(string text, string value)
            {
                Text = text;
                Value = value;
            }

            public string Text { get; private set; }

            public string Value { get; private set; }

            public override string ToString()
            {
                return Text;
            }
        }

        private class RoomOption
        {
            public RoomOption(string text, int? roomId)
            {
                Text = text;
                RoomId = roomId;
            }

            public string Text { get; private set; }

            public int? RoomId { get; private set; }

            public override string ToString()
            {
                return Text;
            }
        }
    }
}
