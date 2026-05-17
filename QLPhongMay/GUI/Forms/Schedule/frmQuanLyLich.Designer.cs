namespace QLPhongMay.GUI.Forms.Schedule
{
    partial class frmQuanLyLich
    {
        private System.ComponentModel.IContainer components = null;
        private Guna.UI2.WinForms.Guna2Button btnBack;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblTitle;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblSubtitle;
        private Guna.UI2.WinForms.Guna2Button btnAdd;
        private Guna.UI2.WinForms.Guna2Button btnEdit;
        private Guna.UI2.WinForms.Guna2Button btnDelete;
        private Guna.UI2.WinForms.Guna2Panel pnlFilter;
        private System.Windows.Forms.Label lblFrom;
        private System.Windows.Forms.Label lblTo;
        private System.Windows.Forms.Label lblDayOfWeek;
        private System.Windows.Forms.Label lblRoom;
        private System.Windows.Forms.Label lblShift;
        private System.Windows.Forms.Label lblClass;
        private System.Windows.Forms.Label lblStatus;
        private Guna.UI2.WinForms.Guna2DateTimePicker dtpFrom;
        private Guna.UI2.WinForms.Guna2DateTimePicker dtpTo;
        private Guna.UI2.WinForms.Guna2ComboBox cboDayOfWeek;
        private Guna.UI2.WinForms.Guna2ComboBox cboRoom;
        private Guna.UI2.WinForms.Guna2ComboBox cboShift;
        private Guna.UI2.WinForms.Guna2ComboBox cboClass;
        private Guna.UI2.WinForms.Guna2ComboBox cboStatus;
        private Guna.UI2.WinForms.Guna2Button btnFilter;
        private Guna.UI2.WinForms.Guna2Button btnClear;
        private Guna.UI2.WinForms.Guna2DataGridView dgvSchedules;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblCount;
        private Guna.UI2.WinForms.Guna2Button btnPreviousPage;
        private Guna.UI2.WinForms.Guna2Button btnNextPage;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblPageInfo;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle headerStyle = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle rowStyle = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnBack = new Guna.UI2.WinForms.Guna2Button();
            this.lblTitle = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.lblSubtitle = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.btnAdd = new Guna.UI2.WinForms.Guna2Button();
            this.btnEdit = new Guna.UI2.WinForms.Guna2Button();
            this.btnDelete = new Guna.UI2.WinForms.Guna2Button();
            this.pnlFilter = new Guna.UI2.WinForms.Guna2Panel();
            this.lblFrom = new System.Windows.Forms.Label();
            this.lblTo = new System.Windows.Forms.Label();
            this.lblDayOfWeek = new System.Windows.Forms.Label();
            this.lblRoom = new System.Windows.Forms.Label();
            this.lblShift = new System.Windows.Forms.Label();
            this.lblClass = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.dtpFrom = new Guna.UI2.WinForms.Guna2DateTimePicker();
            this.dtpTo = new Guna.UI2.WinForms.Guna2DateTimePicker();
            this.cboDayOfWeek = new Guna.UI2.WinForms.Guna2ComboBox();
            this.cboRoom = new Guna.UI2.WinForms.Guna2ComboBox();
            this.cboShift = new Guna.UI2.WinForms.Guna2ComboBox();
            this.cboClass = new Guna.UI2.WinForms.Guna2ComboBox();
            this.cboStatus = new Guna.UI2.WinForms.Guna2ComboBox();
            this.btnFilter = new Guna.UI2.WinForms.Guna2Button();
            this.btnClear = new Guna.UI2.WinForms.Guna2Button();
            this.dgvSchedules = new Guna.UI2.WinForms.Guna2DataGridView();
            this.lblCount = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.btnPreviousPage = new Guna.UI2.WinForms.Guna2Button();
            this.btnNextPage = new Guna.UI2.WinForms.Guna2Button();
            this.lblPageInfo = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.pnlFilter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSchedules)).BeginInit();
            this.SuspendLayout();
            // 
            // btnBack
            // 
            this.btnBack.BorderColor = System.Drawing.Color.FromArgb(226, 232, 240);
            this.btnBack.BorderRadius = 8;
            this.btnBack.BorderThickness = 1;
            this.btnBack.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBack.FillColor = System.Drawing.Color.White;
            this.btnBack.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnBack.ForeColor = System.Drawing.Color.FromArgb(51, 65, 85);
            this.btnBack.Location = new System.Drawing.Point(30, 32);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(104, 38);
            this.btnBack.TabIndex = 0;
            this.btnBack.Text = "<  Quay lại";
            this.btnBack.Click += new System.EventHandler(this.BtnBack_Click);
            // 
            // lblTitle
            // 
            this.lblTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 21F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            this.lblTitle.Location = new System.Drawing.Point(150, 24);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(195, 49);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Quản lý lịch";
            // 
            // lblSubtitle
            // 
            this.lblSubtitle.BackColor = System.Drawing.Color.Transparent;
            this.lblSubtitle.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            this.lblSubtitle.Location = new System.Drawing.Point(154, 78);
            this.lblSubtitle.Name = "lblSubtitle";
            this.lblSubtitle.Size = new System.Drawing.Size(594, 22);
            this.lblSubtitle.TabIndex = 1;
            this.lblSubtitle.Text = "Theo dõi lịch thực hành theo lớp, phòng, ngày, thứ, ca, số sinh viên và trạng thái";
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAdd.BorderRadius = 8;
            this.btnAdd.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAdd.FillColor = System.Drawing.Color.FromArgb(37, 99, 235);
            this.btnAdd.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnAdd.ForeColor = System.Drawing.Color.White;
            this.btnAdd.Location = new System.Drawing.Point(790, 32);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(104, 38);
            this.btnAdd.TabIndex = 2;
            this.btnAdd.Text = "Tạo mới";
            this.btnAdd.Click += new System.EventHandler(this.BtnAdd_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEdit.BorderColor = System.Drawing.Color.FromArgb(37, 99, 235);
            this.btnEdit.BorderRadius = 8;
            this.btnEdit.BorderThickness = 1;
            this.btnEdit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEdit.FillColor = System.Drawing.Color.White;
            this.btnEdit.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnEdit.ForeColor = System.Drawing.Color.FromArgb(37, 99, 235);
            this.btnEdit.Location = new System.Drawing.Point(912, 32);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(104, 38);
            this.btnEdit.TabIndex = 3;
            this.btnEdit.Text = "Chỉnh sửa";
            this.btnEdit.Click += new System.EventHandler(this.BtnEdit_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDelete.BorderColor = System.Drawing.Color.FromArgb(220, 38, 38);
            this.btnDelete.BorderRadius = 8;
            this.btnDelete.BorderThickness = 1;
            this.btnDelete.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDelete.FillColor = System.Drawing.Color.White;
            this.btnDelete.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnDelete.ForeColor = System.Drawing.Color.FromArgb(220, 38, 38);
            this.btnDelete.Location = new System.Drawing.Point(1034, 32);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(104, 38);
            this.btnDelete.TabIndex = 4;
            this.btnDelete.Text = "Xóa";
            this.btnDelete.Click += new System.EventHandler(this.BtnDelete_Click);
            // 
            // pnlFilter
            // 
            this.pnlFilter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlFilter.BorderColor = System.Drawing.Color.FromArgb(226, 232, 240);
            this.pnlFilter.BorderRadius = 8;
            this.pnlFilter.BorderThickness = 1;
            this.pnlFilter.Controls.Add(this.lblFrom);
            this.pnlFilter.Controls.Add(this.lblTo);
            this.pnlFilter.Controls.Add(this.lblDayOfWeek);
            this.pnlFilter.Controls.Add(this.lblRoom);
            this.pnlFilter.Controls.Add(this.lblShift);
            this.pnlFilter.Controls.Add(this.lblClass);
            this.pnlFilter.Controls.Add(this.lblStatus);
            this.pnlFilter.Controls.Add(this.dtpFrom);
            this.pnlFilter.Controls.Add(this.dtpTo);
            this.pnlFilter.Controls.Add(this.cboDayOfWeek);
            this.pnlFilter.Controls.Add(this.cboRoom);
            this.pnlFilter.Controls.Add(this.cboShift);
            this.pnlFilter.Controls.Add(this.cboClass);
            this.pnlFilter.Controls.Add(this.cboStatus);
            this.pnlFilter.Controls.Add(this.btnFilter);
            this.pnlFilter.Controls.Add(this.btnClear);
            this.pnlFilter.FillColor = System.Drawing.Color.White;
            this.pnlFilter.Location = new System.Drawing.Point(30, 132);
            this.pnlFilter.Name = "pnlFilter";
            this.pnlFilter.Size = new System.Drawing.Size(1120, 150);
            this.pnlFilter.TabIndex = 5;
            // 
            // labels and filters
            // 
            this.lblFrom.Location = new System.Drawing.Point(24, 18);
            this.lblFrom.Size = new System.Drawing.Size(120, 20);
            this.lblFrom.Text = "Từ ngày";
            this.dtpFrom.BorderRadius = 8;
            this.dtpFrom.Checked = true;
            this.dtpFrom.FillColor = System.Drawing.Color.White;
            this.dtpFrom.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFrom.Location = new System.Drawing.Point(24, 42);
            this.dtpFrom.Size = new System.Drawing.Size(150, 36);
            this.lblTo.Location = new System.Drawing.Point(194, 18);
            this.lblTo.Size = new System.Drawing.Size(120, 20);
            this.lblTo.Text = "Đến ngày";
            this.dtpTo.BorderRadius = 8;
            this.dtpTo.Checked = true;
            this.dtpTo.FillColor = System.Drawing.Color.White;
            this.dtpTo.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dtpTo.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpTo.Location = new System.Drawing.Point(194, 42);
            this.dtpTo.Size = new System.Drawing.Size(150, 36);
            this.lblDayOfWeek.Location = new System.Drawing.Point(364, 18);
            this.lblDayOfWeek.Size = new System.Drawing.Size(120, 20);
            this.lblDayOfWeek.Text = "Thứ";
            this.cboDayOfWeek.BackColor = System.Drawing.Color.Transparent;
            this.cboDayOfWeek.BorderRadius = 8;
            this.cboDayOfWeek.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboDayOfWeek.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDayOfWeek.ItemHeight = 30;
            this.cboDayOfWeek.Location = new System.Drawing.Point(364, 42);
            this.cboDayOfWeek.Size = new System.Drawing.Size(130, 36);
            this.lblRoom.Location = new System.Drawing.Point(514, 18);
            this.lblRoom.Size = new System.Drawing.Size(120, 20);
            this.lblRoom.Text = "Phòng";
            this.cboRoom.BackColor = System.Drawing.Color.Transparent;
            this.cboRoom.BorderRadius = 8;
            this.cboRoom.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboRoom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboRoom.ItemHeight = 30;
            this.cboRoom.Location = new System.Drawing.Point(514, 42);
            this.cboRoom.Size = new System.Drawing.Size(160, 36);
            this.lblShift.Location = new System.Drawing.Point(694, 18);
            this.lblShift.Size = new System.Drawing.Size(120, 20);
            this.lblShift.Text = "Ca";
            this.cboShift.BackColor = System.Drawing.Color.Transparent;
            this.cboShift.BorderRadius = 8;
            this.cboShift.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboShift.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboShift.ItemHeight = 30;
            this.cboShift.Location = new System.Drawing.Point(694, 42);
            this.cboShift.Size = new System.Drawing.Size(140, 36);
            this.lblClass.Location = new System.Drawing.Point(854, 18);
            this.lblClass.Size = new System.Drawing.Size(120, 20);
            this.lblClass.Text = "Lớp";
            this.cboClass.BackColor = System.Drawing.Color.Transparent;
            this.cboClass.BorderRadius = 8;
            this.cboClass.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboClass.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboClass.ItemHeight = 30;
            this.cboClass.Location = new System.Drawing.Point(854, 42);
            this.cboClass.Size = new System.Drawing.Size(160, 36);
            this.lblStatus.Location = new System.Drawing.Point(24, 88);
            this.lblStatus.Size = new System.Drawing.Size(120, 20);
            this.lblStatus.Text = "Trạng thái";
            this.cboStatus.BackColor = System.Drawing.Color.Transparent;
            this.cboStatus.BorderRadius = 8;
            this.cboStatus.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboStatus.ItemHeight = 30;
            this.cboStatus.Location = new System.Drawing.Point(24, 112);
            this.cboStatus.Size = new System.Drawing.Size(170, 36);
            this.btnFilter.BorderRadius = 8;
            this.btnFilter.FillColor = System.Drawing.Color.FromArgb(37, 99, 235);
            this.btnFilter.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnFilter.ForeColor = System.Drawing.Color.White;
            this.btnFilter.Location = new System.Drawing.Point(694, 106);
            this.btnFilter.Size = new System.Drawing.Size(104, 38);
            this.btnFilter.Text = "Lọc";
            this.btnFilter.Click += new System.EventHandler(this.BtnFilter_Click);
            this.btnClear.BorderColor = System.Drawing.Color.FromArgb(226, 232, 240);
            this.btnClear.BorderRadius = 8;
            this.btnClear.BorderThickness = 1;
            this.btnClear.FillColor = System.Drawing.Color.White;
            this.btnClear.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnClear.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            this.btnClear.Location = new System.Drawing.Point(816, 106);
            this.btnClear.Size = new System.Drawing.Size(104, 38);
            this.btnClear.Text = "Xóa lọc";
            this.btnClear.Click += new System.EventHandler(this.BtnClear_Click);
            // 
            // dgvSchedules
            // 
            this.dgvSchedules.AllowUserToAddRows = false;
            this.dgvSchedules.AllowUserToDeleteRows = false;
            this.dgvSchedules.AllowUserToResizeRows = false;
            this.dgvSchedules.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvSchedules.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvSchedules.BackgroundColor = System.Drawing.Color.White;
            this.dgvSchedules.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvSchedules.ColumnHeadersHeight = 44;
            headerStyle.BackColor = System.Drawing.Color.FromArgb(248, 250, 252);
            headerStyle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            headerStyle.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            this.dgvSchedules.ColumnHeadersDefaultCellStyle = headerStyle;
            rowStyle.BackColor = System.Drawing.Color.White;
            rowStyle.Font = new System.Drawing.Font("Segoe UI", 9F);
            rowStyle.ForeColor = System.Drawing.Color.FromArgb(30, 41, 59);
            rowStyle.SelectionBackColor = System.Drawing.Color.FromArgb(219, 234, 254);
            rowStyle.SelectionForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            this.dgvSchedules.DefaultCellStyle = rowStyle;
            this.dgvSchedules.EnableHeadersVisualStyles = false;
            this.dgvSchedules.GridColor = System.Drawing.Color.FromArgb(241, 245, 249);
            this.dgvSchedules.Location = new System.Drawing.Point(30, 304);
            this.dgvSchedules.MultiSelect = false;
            this.dgvSchedules.Name = "dgvSchedules";
            this.dgvSchedules.ReadOnly = true;
            this.dgvSchedules.RowHeadersVisible = false;
            this.dgvSchedules.RowTemplate.Height = 38;
            this.dgvSchedules.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvSchedules.Size = new System.Drawing.Size(1120, 386);
            this.dgvSchedules.TabIndex = 6;
            this.dgvSchedules.ThemeStyle.HeaderStyle.BackColor = System.Drawing.Color.FromArgb(248, 250, 252);
            this.dgvSchedules.ThemeStyle.RowsStyle.BackColor = System.Drawing.Color.White;
            // 
            // lblCount
            // 
            this.lblCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblCount.BackColor = System.Drawing.Color.Transparent;
            this.lblCount.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            this.lblCount.Location = new System.Drawing.Point(34, 718);
            this.lblCount.Name = "lblCount";
            this.lblCount.Size = new System.Drawing.Size(37, 22);
            this.lblCount.TabIndex = 7;
            this.lblCount.Text = "0 lịch";
            // 
            // btnPreviousPage
            // 
            this.btnPreviousPage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPreviousPage.BorderColor = System.Drawing.Color.FromArgb(226, 232, 240);
            this.btnPreviousPage.BorderRadius = 8;
            this.btnPreviousPage.BorderThickness = 1;
            this.btnPreviousPage.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPreviousPage.FillColor = System.Drawing.Color.White;
            this.btnPreviousPage.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnPreviousPage.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            this.btnPreviousPage.Location = new System.Drawing.Point(828, 710);
            this.btnPreviousPage.Name = "btnPreviousPage";
            this.btnPreviousPage.Size = new System.Drawing.Size(112, 38);
            this.btnPreviousPage.TabIndex = 8;
            this.btnPreviousPage.Text = "Trang trước";
            this.btnPreviousPage.Click += new System.EventHandler(this.BtnPreviousPage_Click);
            // 
            // lblPageInfo
            // 
            this.lblPageInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPageInfo.BackColor = System.Drawing.Color.Transparent;
            this.lblPageInfo.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblPageInfo.ForeColor = System.Drawing.Color.FromArgb(51, 65, 85);
            this.lblPageInfo.Location = new System.Drawing.Point(956, 718);
            this.lblPageInfo.Name = "lblPageInfo";
            this.lblPageInfo.Size = new System.Drawing.Size(73, 22);
            this.lblPageInfo.TabIndex = 9;
            this.lblPageInfo.Text = "Trang 1/1";
            // 
            // btnNextPage
            // 
            this.btnNextPage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNextPage.BorderRadius = 8;
            this.btnNextPage.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNextPage.FillColor = System.Drawing.Color.FromArgb(37, 99, 235);
            this.btnNextPage.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnNextPage.ForeColor = System.Drawing.Color.White;
            this.btnNextPage.Location = new System.Drawing.Point(1038, 710);
            this.btnNextPage.Name = "btnNextPage";
            this.btnNextPage.Size = new System.Drawing.Size(112, 38);
            this.btnNextPage.TabIndex = 10;
            this.btnNextPage.Text = "Trang sau";
            this.btnNextPage.Click += new System.EventHandler(this.BtnNextPage_Click);
            // 
            // frmQuanLyLich
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(246, 248, 252);
            this.ClientSize = new System.Drawing.Size(1180, 760);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.lblSubtitle);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.pnlFilter);
            this.Controls.Add(this.dgvSchedules);
            this.Controls.Add(this.lblCount);
            this.Controls.Add(this.btnPreviousPage);
            this.Controls.Add(this.lblPageInfo);
            this.Controls.Add(this.btnNextPage);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.MinimumSize = new System.Drawing.Size(1080, 680);
            this.Name = "frmQuanLyLich";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Quản lý lịch";
            this.pnlFilter.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSchedules)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}


