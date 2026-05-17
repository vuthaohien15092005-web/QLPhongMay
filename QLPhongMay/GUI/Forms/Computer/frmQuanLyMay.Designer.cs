using System.Drawing;
using System.Windows.Forms;

namespace QLPhongMay.GUI.Forms.Computer
{
    public partial class frmQuanLyMay : Form
    {
        private Panel pnlRoot;
        private Button btnBack;
        private Button btnAdd;
        private Label lblTitle;
        private Label lblSubtitle;
        private FlowLayoutPanel pnlStats;
        private Panel pnlFilter;
        private TextBox txtSearch;
        private ComboBox cboRoom;
        private ComboBox cboRam;
        private ComboBox cboCpu;
        private ComboBox cboMonitor;
        private ComboBox cboOs;
        private Button btnResetConfigFilters;
        private ComboBox cboStatus;
        private DataGridView dgvComputers;
        private Panel pnlPaging;
        private Label lblPagingInfo;
        private FlowLayoutPanel pnlPageButtons;
        private System.ComponentModel.IContainer components = null;

        private void InitializeComponent()
        {
            DataGridViewCellStyle headerStyle = new DataGridViewCellStyle();
            DataGridViewCellStyle cellStyle = new DataGridViewCellStyle();
            this.pnlRoot = new Panel();
            this.btnBack = new Button();
            this.btnAdd = new Button();
            this.lblTitle = new Label();
            this.lblSubtitle = new Label();
            this.pnlStats = new FlowLayoutPanel();
            this.pnlFilter = new Panel();
            this.txtSearch = new TextBox();
            this.cboRoom = new ComboBox();
            this.cboRam = new ComboBox();
            this.cboCpu = new ComboBox();
            this.cboMonitor = new ComboBox();
            this.cboOs = new ComboBox();
            this.btnResetConfigFilters = new Button();
            this.cboStatus = new ComboBox();
            this.dgvComputers = new DataGridView();
            this.pnlPaging = new Panel();
            this.lblPagingInfo = new Label();
            this.pnlPageButtons = new FlowLayoutPanel();
            this.pnlRoot.SuspendLayout();
            this.pnlFilter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvComputers)).BeginInit();
            this.pnlPaging.SuspendLayout();
            this.SuspendLayout();

            this.pnlRoot.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            this.pnlRoot.BackColor = Color.Transparent;
            this.pnlRoot.Controls.Add(this.btnBack);
            this.pnlRoot.Controls.Add(this.lblTitle);
            this.pnlRoot.Controls.Add(this.lblSubtitle);
            this.pnlRoot.Controls.Add(this.btnAdd);
            this.pnlRoot.Controls.Add(this.pnlStats);
            this.pnlRoot.Controls.Add(this.pnlFilter);
            this.pnlRoot.Controls.Add(this.dgvComputers);
            this.pnlRoot.Controls.Add(this.pnlPaging);
            this.pnlRoot.Location = new Point(28, 24);
            this.pnlRoot.Size = new Size(1124, 808);

            this.btnBack.BackColor = Color.White;
            this.btnBack.Cursor = Cursors.Hand;
            this.btnBack.FlatAppearance.BorderColor = Color.FromArgb(226, 232, 240);
            this.btnBack.FlatAppearance.BorderSize = 1;
            this.btnBack.FlatStyle = FlatStyle.Flat;
            this.btnBack.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.btnBack.ForeColor = Color.FromArgb(51, 65, 85);
            this.btnBack.Location = new Point(0, 4);
            this.btnBack.Size = new Size(128, 46);
            this.btnBack.Text = "<  Quay lại";
            this.btnBack.UseVisualStyleBackColor = false;
            this.btnBack.Click += BtnBack_Click;

            this.lblTitle.AutoSize = true;
            this.lblTitle.BackColor = Color.Transparent;
            this.lblTitle.Font = new Font("Segoe UI", 21F, FontStyle.Bold);
            this.lblTitle.ForeColor = Color.FromArgb(15, 23, 42);
            this.lblTitle.Location = new Point(146, 0);
            this.lblTitle.Text = "Quản lý máy tính";

            this.lblSubtitle.AutoSize = true;
            this.lblSubtitle.BackColor = Color.Transparent;
            this.lblSubtitle.Font = new Font("Segoe UI", 10F);
            this.lblSubtitle.ForeColor = Color.FromArgb(100, 116, 139);
            this.lblSubtitle.Location = new Point(150, 48);
            this.lblSubtitle.Text = "Theo dõi danh sách máy tính theo phòng máy và cấu hình";

            this.btnAdd.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            this.btnAdd.BackColor = Color.FromArgb(37, 99, 235);
            this.btnAdd.Cursor = Cursors.Hand;
            this.btnAdd.FlatAppearance.BorderColor = Color.FromArgb(37, 99, 235);
            this.btnAdd.FlatAppearance.BorderSize = 1;
            this.btnAdd.FlatStyle = FlatStyle.Flat;
            this.btnAdd.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.btnAdd.ForeColor = Color.White;
            this.btnAdd.Location = new Point(940, 92);
            this.btnAdd.Size = new Size(184, 46);
            this.btnAdd.Text = "+  Thêm máy";
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Click += BtnAdd_Click;

            this.pnlStats.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.pnlStats.BackColor = Color.Transparent;
            this.pnlStats.Location = new Point(0, 82);
            this.pnlStats.Size = new Size(900, 88);
            this.pnlStats.WrapContents = false;

            this.pnlFilter.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.pnlFilter.BackColor = Color.White;
            this.pnlFilter.BorderStyle = BorderStyle.None;
            this.pnlFilter.Location = new Point(0, 188);
            this.pnlFilter.Size = new Size(1124, 122);
            this.pnlFilter.Controls.AddRange(new Control[] { this.txtSearch, this.cboRoom, this.cboStatus, this.cboRam, this.cboCpu, this.cboMonitor, this.cboOs, this.btnResetConfigFilters });

            this.txtSearch.BorderStyle = BorderStyle.FixedSingle;
            this.txtSearch.Font = new Font("Segoe UI", 10F);
            this.txtSearch.ForeColor = Color.FromArgb(15, 23, 42);
            this.txtSearch.Location = new Point(22, 18);
            this.txtSearch.Size = new Size(420, 30);            this.txtSearch.TextChanged += FilterChanged;

            this.cboRoom.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cboRoom.BackColor = Color.FromArgb(239, 246, 255);
            this.cboRoom.FlatStyle = FlatStyle.Flat;
            this.cboRoom.Font = new Font("Segoe UI", 10F);
            this.cboRoom.ForeColor = Color.FromArgb(30, 64, 175);
            this.cboRoom.Location = new Point(462, 18);
            this.cboRoom.Size = new Size(250, 30);
            this.cboRoom.SelectedIndexChanged += FilterChanged;

            this.cboStatus.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cboStatus.BackColor = Color.FromArgb(239, 246, 255);
            this.cboStatus.FlatStyle = FlatStyle.Flat;
            this.cboStatus.Font = new Font("Segoe UI", 10F);
            this.cboStatus.ForeColor = Color.FromArgb(30, 64, 175);
            this.cboStatus.Location = new Point(732, 18);
            this.cboStatus.Size = new Size(190, 30);
            this.cboStatus.Items.AddRange(new object[] { "Tất cả trạng thái", "Đang hoạt động", "Đang bảo trì", "Hỏng" });
            this.cboStatus.SelectedIndex = 0;
            this.cboStatus.SelectedIndexChanged += FilterChanged;

            this.cboRam.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cboRam.BackColor = Color.FromArgb(239, 246, 255);
            this.cboRam.FlatStyle = FlatStyle.Flat;
            this.cboRam.Font = new Font("Segoe UI", 10F);
            this.cboRam.ForeColor = Color.FromArgb(30, 64, 175);
            this.cboRam.Location = new Point(22, 72);
            this.cboRam.Size = new Size(150, 30);
            this.cboRam.SelectedIndexChanged += FilterChanged;

            this.cboCpu.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cboCpu.BackColor = Color.FromArgb(239, 246, 255);
            this.cboCpu.FlatStyle = FlatStyle.Flat;
            this.cboCpu.Font = new Font("Segoe UI", 10F);
            this.cboCpu.ForeColor = Color.FromArgb(30, 64, 175);
            this.cboCpu.Location = new Point(192, 72);
            this.cboCpu.Size = new Size(286, 30);
            this.cboCpu.SelectedIndexChanged += FilterChanged;

            this.cboMonitor.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cboMonitor.BackColor = Color.FromArgb(239, 246, 255);
            this.cboMonitor.FlatStyle = FlatStyle.Flat;
            this.cboMonitor.Font = new Font("Segoe UI", 10F);
            this.cboMonitor.ForeColor = Color.FromArgb(30, 64, 175);
            this.cboMonitor.Location = new Point(498, 72);
            this.cboMonitor.Size = new Size(180, 30);
            this.cboMonitor.SelectedIndexChanged += FilterChanged;

            this.cboOs.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cboOs.BackColor = Color.FromArgb(239, 246, 255);
            this.cboOs.FlatStyle = FlatStyle.Flat;
            this.cboOs.Font = new Font("Segoe UI", 10F);
            this.cboOs.ForeColor = Color.FromArgb(30, 64, 175);
            this.cboOs.Location = new Point(698, 72);
            this.cboOs.Size = new Size(190, 30);
            this.cboOs.SelectedIndexChanged += FilterChanged;

            this.btnResetConfigFilters.BackColor = Color.FromArgb(255, 237, 213);
            this.btnResetConfigFilters.Cursor = Cursors.Hand;
            this.btnResetConfigFilters.FlatAppearance.BorderColor = Color.FromArgb(254, 215, 170);
            this.btnResetConfigFilters.FlatAppearance.BorderSize = 1;
            this.btnResetConfigFilters.FlatStyle = FlatStyle.Flat;
            this.btnResetConfigFilters.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.btnResetConfigFilters.ForeColor = Color.FromArgb(234, 88, 12);
            this.btnResetConfigFilters.Location = new Point(908, 70);
            this.btnResetConfigFilters.Size = new Size(120, 32);
            this.btnResetConfigFilters.Text = "Làm mới";
            this.btnResetConfigFilters.UseVisualStyleBackColor = false;
            this.btnResetConfigFilters.Click += BtnResetConfigFilters_Click;

            this.dgvComputers.AllowUserToAddRows = false;
            this.dgvComputers.AllowUserToDeleteRows = false;
            this.dgvComputers.AllowUserToResizeRows = false;
            this.dgvComputers.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            this.dgvComputers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvComputers.BackgroundColor = Color.White;
            this.dgvComputers.BorderStyle = BorderStyle.None;
            this.dgvComputers.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvComputers.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            headerStyle.BackColor = Color.FromArgb(248, 250, 252);
            headerStyle.Font = new Font("Segoe UI", 9.2F, FontStyle.Bold);
            headerStyle.ForeColor = Color.FromArgb(71, 85, 105);
            headerStyle.Padding = new Padding(10, 0, 10, 0);
            headerStyle.SelectionBackColor = Color.FromArgb(248, 250, 252);
            headerStyle.SelectionForeColor = Color.FromArgb(71, 85, 105);
            this.dgvComputers.ColumnHeadersDefaultCellStyle = headerStyle;
            this.dgvComputers.ColumnHeadersHeight = 48;
            this.dgvComputers.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            cellStyle.BackColor = Color.White;
            cellStyle.Font = new Font("Segoe UI", 9.5F);
            cellStyle.ForeColor = Color.FromArgb(30, 41, 59);
            cellStyle.Padding = new Padding(10, 0, 10, 0);
            cellStyle.SelectionBackColor = Color.White;
            cellStyle.SelectionForeColor = Color.FromArgb(15, 23, 42);
            this.dgvComputers.DefaultCellStyle = cellStyle;
            this.dgvComputers.EnableHeadersVisualStyles = false;
            this.dgvComputers.GridColor = Color.FromArgb(241, 245, 249);
            this.dgvComputers.Location = new Point(0, 328);
            this.dgvComputers.MultiSelect = false;
            this.dgvComputers.ReadOnly = true;
            this.dgvComputers.RowHeadersVisible = false;
            this.dgvComputers.RowTemplate.Height = 48;
            this.dgvComputers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgvComputers.Size = new Size(1124, 426);
            this.dgvComputers.CellClick += DgvComputers_CellClick;
            this.dgvComputers.CellMouseLeave += DgvComputers_CellMouseLeave;
            this.dgvComputers.CellMouseMove += DgvComputers_CellMouseMove;
            this.dgvComputers.CellPainting += DgvComputers_CellPainting;

            this.pnlPaging.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            this.pnlPaging.BackColor = Color.White;
            this.pnlPaging.BorderStyle = BorderStyle.None;
            this.pnlPaging.Location = new Point(0, 762);
            this.pnlPaging.Size = new Size(1124, 46);
            this.pnlPaging.Controls.Add(this.lblPagingInfo);
            this.pnlPaging.Controls.Add(this.pnlPageButtons);

            this.lblPagingInfo.AutoSize = true;
            this.lblPagingInfo.BackColor = Color.Transparent;
            this.lblPagingInfo.Font = new Font("Segoe UI", 9F);
            this.lblPagingInfo.ForeColor = Color.FromArgb(100, 116, 139);
            this.lblPagingInfo.Location = new Point(18, 13);
            this.lblPagingInfo.Text = "Đang tải dữ liệu...";

            this.pnlPageButtons.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            this.pnlPageButtons.BackColor = Color.Transparent;
            this.pnlPageButtons.Location = new Point(820, 7);
            this.pnlPageButtons.Size = new Size(286, 34);
            this.pnlPageButtons.WrapContents = false;

            this.AutoScaleDimensions = new SizeF(8F, 20F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = Color.FromArgb(246, 248, 252);
            this.ClientSize = new Size(1180, 860);
            this.Controls.Add(this.pnlRoot);
            this.Font = new Font("Segoe UI", 9F);
            this.MinimumSize = new Size(1080, 780);
            this.Name = "frmQuanLyMay";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Quản lý máy tính";

            this.pnlRoot.ResumeLayout(false);
            this.pnlRoot.PerformLayout();
            this.pnlFilter.ResumeLayout(false);
            this.pnlFilter.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvComputers)).EndInit();
            this.pnlPaging.ResumeLayout(false);
            this.pnlPaging.PerformLayout();
            this.ResumeLayout(false);
        }
    }
}




