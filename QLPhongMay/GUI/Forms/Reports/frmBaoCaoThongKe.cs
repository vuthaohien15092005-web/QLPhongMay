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
        private Button btnBack;
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
            FormClosed += FrmBaoCaoThongKe_FormClosed;
        }

        private void InitializeComponent()
        {
            this.rootPanel = new System.Windows.Forms.Panel();
            this.btnBack = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblSubtitle = new System.Windows.Forms.Label();
            this.tabPanel = new System.Windows.Forms.Panel();
            this.btnScheduleTab = new System.Windows.Forms.Button();
            this.btnRoomTab = new System.Windows.Forms.Button();
            this.btnStatusTab = new System.Windows.Forms.Button();
            this.filterPanel = new System.Windows.Forms.Panel();
            this.lblFrom = new System.Windows.Forms.Label();
            this.lblTo = new System.Windows.Forms.Label();
            this.lblGroup = new System.Windows.Forms.Label();
            this.lblRoom = new System.Windows.Forms.Label();
            this.dtpFrom = new System.Windows.Forms.DateTimePicker();
            this.dtpTo = new System.Windows.Forms.DateTimePicker();
            this.cboGroupMode = new System.Windows.Forms.ComboBox();
            this.cboRoom = new System.Windows.Forms.ComboBox();
            this.btnFilter = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.contentPanel = new System.Windows.Forms.Panel();
            this.rootPanel.SuspendLayout();
            this.tabPanel.SuspendLayout();
            this.filterPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // rootPanel
            // 
            this.rootPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(246)))), ((int)(((byte)(255)))));
            this.rootPanel.Controls.Add(this.btnBack);
            this.rootPanel.Controls.Add(this.lblTitle);
            this.rootPanel.Controls.Add(this.lblSubtitle);
            this.rootPanel.Controls.Add(this.tabPanel);
            this.rootPanel.Controls.Add(this.filterPanel);
            this.rootPanel.Controls.Add(this.contentPanel);
            this.rootPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rootPanel.Location = new System.Drawing.Point(0, 0);
            this.rootPanel.Name = "rootPanel";
            this.rootPanel.Size = new System.Drawing.Size(1280, 760);
            this.rootPanel.TabIndex = 0;
            // 
            // btnBack
            // 
            this.btnBack.BackColor = System.Drawing.Color.White;
            this.btnBack.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBack.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.btnBack.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBack.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnBack.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.btnBack.Location = new System.Drawing.Point(24, 24);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(118, 38);
            this.btnBack.TabIndex = 0;
            this.btnBack.Text = "< Quay lại";
            this.btnBack.UseVisualStyleBackColor = false;
            this.btnBack.Click += new System.EventHandler(this.BtnBack_Click);
            // 
            // lblTitle
            // 
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 19F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblTitle.Location = new System.Drawing.Point(166, 30);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(620, 48);
            this.lblTitle.TabIndex = 1;
            this.lblTitle.Text = "Quản lý Báo cáo & Thống kê";
            this.lblTitle.UseMnemonic = false;
            // 
            // lblSubtitle
            // 
            this.lblSubtitle.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblSubtitle.Location = new System.Drawing.Point(168, 82);
            this.lblSubtitle.Name = "lblSubtitle";
            this.lblSubtitle.Size = new System.Drawing.Size(651, 32);
            this.lblSubtitle.TabIndex = 2;
            this.lblSubtitle.Text = "Theo dõi lịch thực hành, mức sử dụng phòng và trạng thái máy tính";
            // 
            // tabPanel
            // 
            this.tabPanel.BackColor = System.Drawing.Color.White;
            this.tabPanel.Controls.Add(this.btnScheduleTab);
            this.tabPanel.Controls.Add(this.btnRoomTab);
            this.tabPanel.Controls.Add(this.btnStatusTab);
            this.tabPanel.Location = new System.Drawing.Point(24, 130);
            this.tabPanel.Name = "tabPanel";
            this.tabPanel.Size = new System.Drawing.Size(1230, 46);
            this.tabPanel.TabIndex = 3;
            // 
            // btnScheduleTab
            // 
            this.btnScheduleTab.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.btnScheduleTab.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnScheduleTab.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(213)))), ((int)(((byte)(225)))));
            this.btnScheduleTab.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnScheduleTab.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnScheduleTab.ForeColor = System.Drawing.Color.White;
            this.btnScheduleTab.Location = new System.Drawing.Point(0, 0);
            this.btnScheduleTab.Name = "btnScheduleTab";
            this.btnScheduleTab.Size = new System.Drawing.Size(410, 46);
            this.btnScheduleTab.TabIndex = 0;
            this.btnScheduleTab.Text = "Lịch thực hành theo thời gian";
            this.btnScheduleTab.UseMnemonic = false;
            this.btnScheduleTab.UseVisualStyleBackColor = false;
            // 
            // btnRoomTab
            // 
            this.btnRoomTab.BackColor = System.Drawing.Color.White;
            this.btnRoomTab.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRoomTab.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(213)))), ((int)(((byte)(225)))));
            this.btnRoomTab.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRoomTab.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnRoomTab.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.btnRoomTab.Location = new System.Drawing.Point(410, 0);
            this.btnRoomTab.Name = "btnRoomTab";
            this.btnRoomTab.Size = new System.Drawing.Size(410, 46);
            this.btnRoomTab.TabIndex = 1;
            this.btnRoomTab.Text = "Tỷ lệ sử dụng phòng máy";
            this.btnRoomTab.UseMnemonic = false;
            this.btnRoomTab.UseVisualStyleBackColor = false;
            // 
            // btnStatusTab
            // 
            this.btnStatusTab.BackColor = System.Drawing.Color.White;
            this.btnStatusTab.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnStatusTab.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(213)))), ((int)(((byte)(225)))));
            this.btnStatusTab.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStatusTab.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnStatusTab.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.btnStatusTab.Location = new System.Drawing.Point(820, 0);
            this.btnStatusTab.Name = "btnStatusTab";
            this.btnStatusTab.Size = new System.Drawing.Size(410, 46);
            this.btnStatusTab.TabIndex = 2;
            this.btnStatusTab.Text = "Thống kê trạng thái máy tính";
            this.btnStatusTab.UseMnemonic = false;
            this.btnStatusTab.UseVisualStyleBackColor = false;
            // 
            // filterPanel
            // 
            this.filterPanel.BackColor = System.Drawing.Color.White;
            this.filterPanel.Controls.Add(this.lblFrom);
            this.filterPanel.Controls.Add(this.lblTo);
            this.filterPanel.Controls.Add(this.lblGroup);
            this.filterPanel.Controls.Add(this.lblRoom);
            this.filterPanel.Controls.Add(this.dtpFrom);
            this.filterPanel.Controls.Add(this.dtpTo);
            this.filterPanel.Controls.Add(this.cboGroupMode);
            this.filterPanel.Controls.Add(this.cboRoom);
            this.filterPanel.Controls.Add(this.btnFilter);
            this.filterPanel.Controls.Add(this.btnClear);
            this.filterPanel.Location = new System.Drawing.Point(24, 190);
            this.filterPanel.Name = "filterPanel";
            this.filterPanel.Size = new System.Drawing.Size(1230, 86);
            this.filterPanel.TabIndex = 4;
            // 
            // lblFrom
            // 
            this.lblFrom.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.lblFrom.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblFrom.Location = new System.Drawing.Point(18, 12);
            this.lblFrom.Name = "lblFrom";
            this.lblFrom.Size = new System.Drawing.Size(120, 20);
            this.lblFrom.TabIndex = 0;
            this.lblFrom.Text = "Từ ngày";
            // 
            // lblTo
            // 
            this.lblTo.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.lblTo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblTo.Location = new System.Drawing.Point(244, 12);
            this.lblTo.Name = "lblTo";
            this.lblTo.Size = new System.Drawing.Size(120, 20);
            this.lblTo.TabIndex = 1;
            this.lblTo.Text = "Đến ngày";
            // 
            // lblGroup
            // 
            this.lblGroup.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.lblGroup.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblGroup.Location = new System.Drawing.Point(477, 12);
            this.lblGroup.Name = "lblGroup";
            this.lblGroup.Size = new System.Drawing.Size(120, 20);
            this.lblGroup.TabIndex = 2;
            this.lblGroup.Text = "Nhóm lịch";
            // 
            // lblRoom
            // 
            this.lblRoom.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.lblRoom.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblRoom.Location = new System.Drawing.Point(693, 12);
            this.lblRoom.Name = "lblRoom";
            this.lblRoom.Size = new System.Drawing.Size(120, 20);
            this.lblRoom.TabIndex = 3;
            this.lblRoom.Text = "Phòng máy";
            // 
            // dtpFrom
            // 
            this.dtpFrom.CustomFormat = "dd/MM/yyyy";
            this.dtpFrom.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.dtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFrom.Location = new System.Drawing.Point(18, 36);
            this.dtpFrom.Name = "dtpFrom";
            this.dtpFrom.Size = new System.Drawing.Size(210, 34);
            this.dtpFrom.TabIndex = 4;
            // 
            // dtpTo
            // 
            this.dtpTo.CustomFormat = "dd/MM/yyyy";
            this.dtpTo.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.dtpTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpTo.Location = new System.Drawing.Point(244, 36);
            this.dtpTo.Name = "dtpTo";
            this.dtpTo.Size = new System.Drawing.Size(210, 34);
            this.dtpTo.TabIndex = 5;
            // 
            // cboGroupMode
            // 
            this.cboGroupMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboGroupMode.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cboGroupMode.Items.AddRange(new object[] {
            "Theo ngày",
            "Theo tháng",
            "Theo năm"});
            this.cboGroupMode.Location = new System.Drawing.Point(477, 35);
            this.cboGroupMode.Name = "cboGroupMode";
            this.cboGroupMode.Size = new System.Drawing.Size(190, 36);
            this.cboGroupMode.TabIndex = 6;
            // 
            // cboRoom
            // 
            this.cboRoom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboRoom.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cboRoom.Items.AddRange(new object[] {
            "Tất cả phòng"});
            this.cboRoom.Location = new System.Drawing.Point(693, 35);
            this.cboRoom.Name = "cboRoom";
            this.cboRoom.Size = new System.Drawing.Size(198, 36);
            this.cboRoom.TabIndex = 7;
            // 
            // btnFilter
            // 
            this.btnFilter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(139)))), ((int)(((byte)(34)))));
            this.btnFilter.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnFilter.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(163)))), ((int)(((byte)(184)))));
            this.btnFilter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFilter.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnFilter.ForeColor = System.Drawing.Color.White;
            this.btnFilter.Location = new System.Drawing.Point(940, 32);
            this.btnFilter.Name = "btnFilter";
            this.btnFilter.Size = new System.Drawing.Size(115, 34);
            this.btnFilter.TabIndex = 8;
            this.btnFilter.Text = "Hiển thị";
            this.btnFilter.UseVisualStyleBackColor = false;
            // 
            // btnClear
            // 
            this.btnClear.BackColor = System.Drawing.Color.White;
            this.btnClear.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClear.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(163)))), ((int)(((byte)(184)))));
            this.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClear.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnClear.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.btnClear.Location = new System.Drawing.Point(1093, 32);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(115, 34);
            this.btnClear.TabIndex = 9;
            this.btnClear.Text = "Làm mới";
            this.btnClear.UseVisualStyleBackColor = false;
            // 
            // contentPanel
            // 
            this.contentPanel.BackColor = System.Drawing.Color.White;
            this.contentPanel.Location = new System.Drawing.Point(24, 296);
            this.contentPanel.Name = "contentPanel";
            this.contentPanel.Size = new System.Drawing.Size(1230, 420);
            this.contentPanel.TabIndex = 5;
            // 
            // frmBaoCaoThongKe
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(246)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(1280, 760);
            this.Controls.Add(this.rootPanel);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.MinimumSize = new System.Drawing.Size(1100, 700);
            this.Name = "frmBaoCaoThongKe";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Quản lý Báo cáo & Thống kê";
            this.rootPanel.ResumeLayout(false);
            this.tabPanel.ResumeLayout(false);
            this.filterPanel.ResumeLayout(false);
            this.ResumeLayout(false);

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

        private void FrmBaoCaoThongKe_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (Owner != null && !Owner.IsDisposed)
            {
                Owner.PerformLayout();
                Owner.Invalidate(true);
                Owner.Update();
            }
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            dtpFrom.Value = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            dtpTo.Value = DateTime.Today;
            cboGroupMode.SelectedIndex = 0;
            cboRoom.SelectedIndex = 0;
            LoadReportData();
        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            Close();
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
            int contentTop = activeReportTab == 0 ? 296 : 190;
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
