using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;
using QLPhongMay.DAL;
using QLPhongMay.Models;

namespace QLPhongMay.GUI.Forms.Catalog
{
    public partial class frmQLLopHoc : Form
    {
        private const int PageSize = 5;
        private const int ECM_FIRST = 0x1500;
        private const int EM_SETCUEBANNER = ECM_FIRST + 1;

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wParam, string lParam);


        private readonly LopHocRepository lopHocRepository;
        private readonly ToolTip actionToolTip;
        private List<LopHoc> lopHocs;
        private int currentPage = 1;
        private bool isLoading;

        private Panel pnlRoot;
        private Button btnBack;
        private Label lblTitle;
        private Label lblSubtitle;
        private Button btnAdd;
        private Panel pnlTotalClasses;
        private Panel pnlTotalStudents;
        private Panel pnlAverageStudents;
        private Label lblTotalClassesValue;
        private Label lblTotalStudentsValue;
        private Label lblAverageStudentsValue;
        private Label lblTotalClassesText;
        private Label lblTotalStudentsText;
        private Label lblAverageStudentsText;
        private Label lblTotalClassesIcon;
        private Label lblTotalStudentsIcon;
        private Label lblAverageStudentsIcon;
        private Panel pnlFilter;
        private Label lblSearch;
        private Label lblSiSoFilter;
        private Label lblSort;
        private TextBox txtSearch;
        private ComboBox cboSiSo;
        private ComboBox cboSort;
        private DataGridView dgvLopHoc;
        private Panel pnlPaging;
        private Label lblPagingInfo;
        private FlowLayoutPanel pnlPageButtons;
        private System.ComponentModel.IContainer components;

        public frmQLLopHoc()
        {
            this.lopHocRepository = new LopHocRepository();
            this.actionToolTip = new ToolTip();
            this.lopHocs = new List<LopHoc>();

            InitializeComponent();
            this.Shown += new EventHandler(this.frmQLLopHoc_Shown);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.actionToolTip.Dispose();
                if (this.components != null)
                {
                    this.components.Dispose();
                }
            }

            base.Dispose(disposing);
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            SendMessage(this.txtSearch.Handle, EM_SETCUEBANNER, IntPtr.Zero, "Tìm kiếm theo tên lớp");
        }
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pnlRoot = new System.Windows.Forms.Panel();
            this.btnBack = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblSubtitle = new System.Windows.Forms.Label();
            this.btnAdd = new System.Windows.Forms.Button();
            this.pnlTotalClasses = new System.Windows.Forms.Panel();
            this.lblTotalClassesIcon = new System.Windows.Forms.Label();
            this.lblTotalClassesValue = new System.Windows.Forms.Label();
            this.lblTotalClassesText = new System.Windows.Forms.Label();
            this.pnlTotalStudents = new System.Windows.Forms.Panel();
            this.lblTotalStudentsIcon = new System.Windows.Forms.Label();
            this.lblTotalStudentsValue = new System.Windows.Forms.Label();
            this.lblTotalStudentsText = new System.Windows.Forms.Label();
            this.pnlAverageStudents = new System.Windows.Forms.Panel();
            this.lblAverageStudentsIcon = new System.Windows.Forms.Label();
            this.lblAverageStudentsValue = new System.Windows.Forms.Label();
            this.lblAverageStudentsText = new System.Windows.Forms.Label();
            this.pnlFilter = new System.Windows.Forms.Panel();
            this.lblSearch = new System.Windows.Forms.Label();
            this.lblSiSoFilter = new System.Windows.Forms.Label();
            this.lblSort = new System.Windows.Forms.Label();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.cboSiSo = new System.Windows.Forms.ComboBox();
            this.cboSort = new System.Windows.Forms.ComboBox();
            this.dgvLopHoc = new System.Windows.Forms.DataGridView();
            this.pnlPaging = new System.Windows.Forms.Panel();
            this.lblPagingInfo = new System.Windows.Forms.Label();
            this.pnlPageButtons = new System.Windows.Forms.FlowLayoutPanel();
            this.pnlRoot.SuspendLayout();
            this.pnlTotalClasses.SuspendLayout();
            this.pnlTotalStudents.SuspendLayout();
            this.pnlAverageStudents.SuspendLayout();
            this.pnlFilter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLopHoc)).BeginInit();
            this.pnlPaging.SuspendLayout();
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
            this.pnlRoot.Controls.Add(this.btnAdd);
            this.pnlRoot.Controls.Add(this.pnlTotalClasses);
            this.pnlRoot.Controls.Add(this.pnlTotalStudents);
            this.pnlRoot.Controls.Add(this.pnlAverageStudents);
            this.pnlRoot.Controls.Add(this.pnlFilter);
            this.pnlRoot.Controls.Add(this.dgvLopHoc);
            this.pnlRoot.Controls.Add(this.pnlPaging);
            this.pnlRoot.Location = new System.Drawing.Point(28, 24);
            this.pnlRoot.Name = "pnlRoot";
            this.pnlRoot.Size = new System.Drawing.Size(1124, 808);
            this.pnlRoot.TabIndex = 0;
            // 
            // btnBack
            // 
            this.btnBack.BackColor = System.Drawing.Color.White;
            this.btnBack.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBack.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.btnBack.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBack.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBack.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.btnBack.Location = new System.Drawing.Point(0, 4);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(128, 46);
            this.btnBack.TabIndex = 0;
            this.btnBack.Text = "<  Quay lại";
            this.btnBack.UseVisualStyleBackColor = false;
            this.btnBack.Click += new System.EventHandler(this.BtnBack_Click);
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 21F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblTitle.Location = new System.Drawing.Point(146, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(336, 57);
            this.lblTitle.TabIndex = 1;
            this.lblTitle.Text = "Quản lý lớp học";
            // 
            // lblSubtitle
            // 
            this.lblSubtitle.AutoSize = true;
            this.lblSubtitle.BackColor = System.Drawing.Color.Transparent;
            this.lblSubtitle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblSubtitle.Location = new System.Drawing.Point(150, 48);
            this.lblSubtitle.Name = "lblSubtitle";
            this.lblSubtitle.Size = new System.Drawing.Size(320, 28);
            this.lblSubtitle.TabIndex = 2;
            this.lblSubtitle.Text = "Theo dõi danh sách lớp học và sĩ số";
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAdd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.btnAdd.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAdd.FlatAppearance.BorderSize = 0;
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdd.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdd.ForeColor = System.Drawing.Color.White;
            this.btnAdd.Location = new System.Drawing.Point(930, 92);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(194, 46);
            this.btnAdd.TabIndex = 3;
            this.btnAdd.Text = "+  Thêm lớp học";
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Click += new System.EventHandler(this.BtnAdd_Click);
            // 
            // pnlTotalClasses
            // 
            this.pnlTotalClasses.BackColor = System.Drawing.Color.White;
            this.pnlTotalClasses.Controls.Add(this.lblTotalClassesIcon);
            this.pnlTotalClasses.Controls.Add(this.lblTotalClassesValue);
            this.pnlTotalClasses.Controls.Add(this.lblTotalClassesText);
            this.pnlTotalClasses.Location = new System.Drawing.Point(0, 82);
            this.pnlTotalClasses.Name = "pnlTotalClasses";
            this.pnlTotalClasses.Size = new System.Drawing.Size(248, 78);
            this.pnlTotalClasses.TabIndex = 4;
            // 
            // lblTotalClassesIcon
            // 
            this.lblTotalClassesIcon.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(246)))), ((int)(((byte)(255)))));
            this.lblTotalClassesIcon.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalClassesIcon.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.lblTotalClassesIcon.Location = new System.Drawing.Point(24, 20);
            this.lblTotalClassesIcon.Name = "lblTotalClassesIcon";
            this.lblTotalClassesIcon.Size = new System.Drawing.Size(38, 38);
            this.lblTotalClassesIcon.TabIndex = 0;
            this.lblTotalClassesIcon.Text = "T";
            this.lblTotalClassesIcon.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTotalClassesValue
            // 
            this.lblTotalClassesValue.AutoSize = true;
            this.lblTotalClassesValue.Font = new System.Drawing.Font("Segoe UI", 19F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalClassesValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblTotalClassesValue.Location = new System.Drawing.Point(82, 12);
            this.lblTotalClassesValue.Name = "lblTotalClassesValue";
            this.lblTotalClassesValue.Size = new System.Drawing.Size(44, 51);
            this.lblTotalClassesValue.TabIndex = 1;
            this.lblTotalClassesValue.Text = "0";
            // 
            // lblTotalClassesText
            // 
            this.lblTotalClassesText.AutoSize = true;
            this.lblTotalClassesText.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalClassesText.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblTotalClassesText.Location = new System.Drawing.Point(84, 48);
            this.lblTotalClassesText.Name = "lblTotalClassesText";
            this.lblTotalClassesText.Size = new System.Drawing.Size(123, 25);
            this.lblTotalClassesText.TabIndex = 2;
            this.lblTotalClassesText.Text = "Tổng lớp học";
            // 
            // pnlTotalStudents
            // 
            this.pnlTotalStudents.BackColor = System.Drawing.Color.White;
            this.pnlTotalStudents.Controls.Add(this.lblTotalStudentsIcon);
            this.pnlTotalStudents.Controls.Add(this.lblTotalStudentsValue);
            this.pnlTotalStudents.Controls.Add(this.lblTotalStudentsText);
            this.pnlTotalStudents.Location = new System.Drawing.Point(262, 82);
            this.pnlTotalStudents.Name = "pnlTotalStudents";
            this.pnlTotalStudents.Size = new System.Drawing.Size(248, 78);
            this.pnlTotalStudents.TabIndex = 5;
            // 
            // lblTotalStudentsIcon
            // 
            this.lblTotalStudentsIcon.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(253)))), ((int)(((byte)(244)))));
            this.lblTotalStudentsIcon.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalStudentsIcon.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(163)))), ((int)(((byte)(74)))));
            this.lblTotalStudentsIcon.Location = new System.Drawing.Point(24, 20);
            this.lblTotalStudentsIcon.Name = "lblTotalStudentsIcon";
            this.lblTotalStudentsIcon.Size = new System.Drawing.Size(38, 38);
            this.lblTotalStudentsIcon.TabIndex = 0;
            this.lblTotalStudentsIcon.Text = "S";
            this.lblTotalStudentsIcon.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTotalStudentsValue
            // 
            this.lblTotalStudentsValue.AutoSize = true;
            this.lblTotalStudentsValue.Font = new System.Drawing.Font("Segoe UI", 19F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalStudentsValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblTotalStudentsValue.Location = new System.Drawing.Point(82, 12);
            this.lblTotalStudentsValue.Name = "lblTotalStudentsValue";
            this.lblTotalStudentsValue.Size = new System.Drawing.Size(44, 51);
            this.lblTotalStudentsValue.TabIndex = 1;
            this.lblTotalStudentsValue.Text = "0";
            // 
            // lblTotalStudentsText
            // 
            this.lblTotalStudentsText.AutoSize = true;
            this.lblTotalStudentsText.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalStudentsText.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblTotalStudentsText.Location = new System.Drawing.Point(84, 48);
            this.lblTotalStudentsText.Name = "lblTotalStudentsText";
            this.lblTotalStudentsText.Size = new System.Drawing.Size(97, 25);
            this.lblTotalStudentsText.TabIndex = 2;
            this.lblTotalStudentsText.Text = "Tổng sĩ số";
            // 
            // pnlAverageStudents
            // 
            this.pnlAverageStudents.BackColor = System.Drawing.Color.White;
            this.pnlAverageStudents.Controls.Add(this.lblAverageStudentsIcon);
            this.pnlAverageStudents.Controls.Add(this.lblAverageStudentsValue);
            this.pnlAverageStudents.Controls.Add(this.lblAverageStudentsText);
            this.pnlAverageStudents.Location = new System.Drawing.Point(524, 82);
            this.pnlAverageStudents.Name = "pnlAverageStudents";
            this.pnlAverageStudents.Size = new System.Drawing.Size(248, 78);
            this.pnlAverageStudents.TabIndex = 6;
            this.pnlAverageStudents.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlAverageStudents_Paint);
            // 
            // lblAverageStudentsIcon
            // 
            this.lblAverageStudentsIcon.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            this.lblAverageStudentsIcon.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAverageStudentsIcon.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(124)))), ((int)(((byte)(58)))), ((int)(((byte)(237)))));
            this.lblAverageStudentsIcon.Location = new System.Drawing.Point(24, 20);
            this.lblAverageStudentsIcon.Name = "lblAverageStudentsIcon";
            this.lblAverageStudentsIcon.Size = new System.Drawing.Size(38, 38);
            this.lblAverageStudentsIcon.TabIndex = 0;
            this.lblAverageStudentsIcon.Text = "TB";
            this.lblAverageStudentsIcon.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblAverageStudentsValue
            // 
            this.lblAverageStudentsValue.AutoSize = true;
            this.lblAverageStudentsValue.Font = new System.Drawing.Font("Segoe UI", 19F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAverageStudentsValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblAverageStudentsValue.Location = new System.Drawing.Point(82, 12);
            this.lblAverageStudentsValue.Name = "lblAverageStudentsValue";
            this.lblAverageStudentsValue.Size = new System.Drawing.Size(44, 51);
            this.lblAverageStudentsValue.TabIndex = 1;
            this.lblAverageStudentsValue.Text = "0";
            // 
            // lblAverageStudentsText
            // 
            this.lblAverageStudentsText.AutoSize = true;
            this.lblAverageStudentsText.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAverageStudentsText.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblAverageStudentsText.Location = new System.Drawing.Point(84, 48);
            this.lblAverageStudentsText.Name = "lblAverageStudentsText";
            this.lblAverageStudentsText.Size = new System.Drawing.Size(145, 25);
            this.lblAverageStudentsText.TabIndex = 2;
            this.lblAverageStudentsText.Text = "Sĩ só trung bình";
            // 
            // pnlFilter
            // 
            this.pnlFilter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlFilter.BackColor = System.Drawing.Color.White;
            this.pnlFilter.Controls.Add(this.lblSearch);
            this.pnlFilter.Controls.Add(this.lblSiSoFilter);
            this.pnlFilter.Controls.Add(this.lblSort);
            this.pnlFilter.Controls.Add(this.txtSearch);
            this.pnlFilter.Controls.Add(this.cboSiSo);
            this.pnlFilter.Controls.Add(this.cboSort);
            this.pnlFilter.Location = new System.Drawing.Point(0, 188);
            this.pnlFilter.Name = "pnlFilter";
            this.pnlFilter.Size = new System.Drawing.Size(1124, 66);
            this.pnlFilter.TabIndex = 7;
            // 
            // lblSearch
            // 
            this.lblSearch.AutoSize = true;
            this.lblSearch.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSearch.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblSearch.Location = new System.Drawing.Point(22, 12);
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.Size = new System.Drawing.Size(90, 25);
            this.lblSearch.TabIndex = 0;
            this.lblSearch.Text = "Tìm kiếm";
            this.lblSearch.Visible = false;
            // 
            // lblSiSoFilter
            // 
            this.lblSiSoFilter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSiSoFilter.AutoSize = true;
            this.lblSiSoFilter.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSiSoFilter.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblSiSoFilter.Location = new System.Drawing.Point(600, 12);
            this.lblSiSoFilter.Name = "lblSiSoFilter";
            this.lblSiSoFilter.Size = new System.Drawing.Size(127, 25);
            this.lblSiSoFilter.TabIndex = 1;
            this.lblSiSoFilter.Text = "Lọc theo sĩ số";
            this.lblSiSoFilter.Visible = false;
            // 
            // lblSort
            // 
            this.lblSort.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSort.AutoSize = true;
            this.lblSort.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSort.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblSort.Location = new System.Drawing.Point(840, 12);
            this.lblSort.Name = "lblSort";
            this.lblSort.Size = new System.Drawing.Size(79, 25);
            this.lblSort.TabIndex = 2;
            this.lblSort.Text = "Sắp xếp";
            this.lblSort.Visible = false;
            // 
            // txtSearch
            // 
            this.txtSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSearch.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSearch.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.txtSearch.Location = new System.Drawing.Point(22, 17);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(520, 34);
            this.txtSearch.TabIndex = 0;
            this.txtSearch.TextChanged += new System.EventHandler(this.FilterChanged);
            // 
            // cboSiSo
            // 
            this.cboSiSo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cboSiSo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.cboSiSo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSiSo.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboSiSo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.cboSiSo.Location = new System.Drawing.Point(704, 17);
            this.cboSiSo.Name = "cboSiSo";
            this.cboSiSo.Size = new System.Drawing.Size(178, 36);
            this.cboSiSo.TabIndex = 1;
            this.cboSiSo.SelectedIndexChanged += new System.EventHandler(this.FilterChanged);
            // 
            // cboSort
            // 
            this.cboSort.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cboSort.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.cboSort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSort.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboSort.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.cboSort.Location = new System.Drawing.Point(900, 17);
            this.cboSort.Name = "cboSort";
            this.cboSort.Size = new System.Drawing.Size(202, 36);
            this.cboSort.TabIndex = 2;
            this.cboSort.SelectedIndexChanged += new System.EventHandler(this.FilterChanged);
            // 
            // dgvLopHoc
            // 
            this.dgvLopHoc.AllowUserToAddRows = false;
            this.dgvLopHoc.AllowUserToDeleteRows = false;
            this.dgvLopHoc.AllowUserToResizeRows = false;
            this.dgvLopHoc.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvLopHoc.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvLopHoc.BackgroundColor = System.Drawing.Color.White;
            this.dgvLopHoc.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvLopHoc.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvLopHoc.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            dataGridViewCellStyle1.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.dgvLopHoc.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvLopHoc.ColumnHeadersHeight = 48;
            this.dgvLopHoc.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            dataGridViewCellStyle2.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(246)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvLopHoc.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvLopHoc.EnableHeadersVisualStyles = false;
            this.dgvLopHoc.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.dgvLopHoc.Location = new System.Drawing.Point(0, 260);
            this.dgvLopHoc.MultiSelect = false;
            this.dgvLopHoc.Name = "dgvLopHoc";
            this.dgvLopHoc.ReadOnly = true;
            this.dgvLopHoc.RowHeadersVisible = false;
            this.dgvLopHoc.RowHeadersWidth = 51;
            this.dgvLopHoc.RowTemplate.Height = 44;
            this.dgvLopHoc.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvLopHoc.Size = new System.Drawing.Size(1124, 484);
            this.dgvLopHoc.TabIndex = 5;
            this.dgvLopHoc.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgvLopHoc_CellClick);
            this.dgvLopHoc.CellMouseLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgvLopHoc_CellMouseLeave);
            this.dgvLopHoc.CellMouseMove += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.DgvLopHoc_CellMouseMove);
            this.dgvLopHoc.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.DgvLopHoc_CellPainting);
            // 
            // pnlPaging
            // 
            this.pnlPaging.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlPaging.BackColor = System.Drawing.Color.White;
            this.pnlPaging.Controls.Add(this.lblPagingInfo);
            this.pnlPaging.Controls.Add(this.pnlPageButtons);
            this.pnlPaging.Location = new System.Drawing.Point(0, 762);
            this.pnlPaging.Name = "pnlPaging";
            this.pnlPaging.Size = new System.Drawing.Size(1124, 46);
            this.pnlPaging.TabIndex = 8;
            // 
            // lblPagingInfo
            // 
            this.lblPagingInfo.AutoSize = true;
            this.lblPagingInfo.BackColor = System.Drawing.Color.Transparent;
            this.lblPagingInfo.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPagingInfo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblPagingInfo.Location = new System.Drawing.Point(18, 13);
            this.lblPagingInfo.Name = "lblPagingInfo";
            this.lblPagingInfo.Size = new System.Drawing.Size(147, 25);
            this.lblPagingInfo.TabIndex = 0;
            this.lblPagingInfo.Text = "Không có dữ liệu";
            // 
            // pnlPageButtons
            // 
            this.pnlPageButtons.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlPageButtons.BackColor = System.Drawing.Color.Transparent;
            this.pnlPageButtons.Location = new System.Drawing.Point(838, 9);
            this.pnlPageButtons.Name = "pnlPageButtons";
            this.pnlPageButtons.Size = new System.Drawing.Size(286, 34);
            this.pnlPageButtons.TabIndex = 1;
            this.pnlPageButtons.WrapContents = false;
            // 
            // frmQLLopHoc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(247)))), ((int)(((byte)(251)))));
            this.ClientSize = new System.Drawing.Size(1180, 860);
            this.Controls.Add(this.pnlRoot);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MinimumSize = new System.Drawing.Size(1080, 780);
            this.Name = "frmQLLopHoc";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Quản lý lớp học";
            this.pnlRoot.ResumeLayout(false);
            this.pnlRoot.PerformLayout();
            this.pnlTotalClasses.ResumeLayout(false);
            this.pnlTotalClasses.PerformLayout();
            this.pnlTotalStudents.ResumeLayout(false);
            this.pnlTotalStudents.PerformLayout();
            this.pnlAverageStudents.ResumeLayout(false);
            this.pnlAverageStudents.PerformLayout();
            this.pnlFilter.ResumeLayout(false);
            this.pnlFilter.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLopHoc)).EndInit();
            this.pnlPaging.ResumeLayout(false);
            this.pnlPaging.PerformLayout();
            this.ResumeLayout(false);

        }

        private async void frmQLLopHoc_Shown(object sender, EventArgs e)
        {
            await RefreshDataAsync(true);
        }

        private async Task RefreshDataAsync(bool resetPage)
        {
            SetLoadingState(true, "Dang tai danh sach lop hoc...");

            try
            {
                List<LopHoc> data = await Task.Run(() => this.lopHocRepository.GetAll().ToList());
                if (this.IsDisposed)
                {
                    return;
                }

                this.lopHocs = data;
                UpdateSummaryCards();
                LoadSiSoFilter();
                LoadSortOptions();
                ApplyFilter(resetPage);
            }
            catch (Exception ex)
            {
                this.lopHocs = new List<LopHoc>();
                UpdateSummaryCards();
                LoadSiSoFilter();
                LoadSortOptions();
                BindPage(new List<LopHocRow>());
                ShowDataError("Khong the tai danh sach lop hoc.", ex);
                this.lblPagingInfo.Text = "Khong tai duoc du lieu. Kiem tra SQL Server va App.config.";
            }
            finally
            {
                SetLoadingState(false, null);
            }
        }

        private void RefreshData(bool resetPage)
        {
            SetLoadingState(true, "Dang tai danh sach lop hoc...");

            try
            {
                this.lopHocs = this.lopHocRepository.GetAll().ToList();
                UpdateSummaryCards();
                LoadSiSoFilter();
                LoadSortOptions();
                ApplyFilter(resetPage);
            }
            catch (Exception ex)
            {
                ShowDataError("Khong the tai danh sach lop hoc.", ex);
            }
            finally
            {
                SetLoadingState(false, null);
            }
        }

        private void SetLoadingState(bool loading, string message)
        {
            this.isLoading = loading;
            this.txtSearch.Enabled = !loading;
            this.cboSiSo.Enabled = !loading;
            this.cboSort.Enabled = !loading;
            this.btnAdd.Enabled = !loading;
            this.dgvLopHoc.Enabled = !loading;

            if (loading)
            {
                this.pnlPageButtons.Controls.Clear();
                this.lblPagingInfo.Text = message ?? "Dang tai...";
            }
        }

        private void UpdateSummaryCards()
        {
            int totalClasses = this.lopHocs.Count;
            int totalStudents = this.lopHocs.Sum(l => l.SiSo);
            int averageStudents = totalClasses == 0 ? 0 : (int)Math.Round(totalStudents / (double)totalClasses);

            this.lblTotalClassesValue.Text = totalClasses.ToString();
            this.lblTotalStudentsValue.Text = totalStudents.ToString();
            this.lblAverageStudentsValue.Text = averageStudents.ToString();
        }

        private void LoadSiSoFilter()
        {
            string selected = this.cboSiSo.SelectedItem == null ? "Tat ca si so" : Convert.ToString(this.cboSiSo.SelectedItem);
            this.cboSiSo.SelectedIndexChanged -= new EventHandler(this.FilterChanged);
            this.cboSiSo.Items.Clear();
            this.cboSiSo.Items.Add("Tat ca si so");
            this.cboSiSo.Items.Add("Duoi 30");
            this.cboSiSo.Items.Add("30 - 40");
            this.cboSiSo.Items.Add("Tren 40");

            int selectedIndex = this.cboSiSo.Items.IndexOf(selected);
            this.cboSiSo.SelectedIndex = selectedIndex >= 0 ? selectedIndex : 0;
            this.cboSiSo.SelectedIndexChanged += new EventHandler(this.FilterChanged);
        }

        private void LoadSortOptions()
        {
            string selected = this.cboSort.SelectedItem == null ? "Ten lop A-Z" : Convert.ToString(this.cboSort.SelectedItem);
            this.cboSort.SelectedIndexChanged -= new EventHandler(this.FilterChanged);
            this.cboSort.Items.Clear();
            this.cboSort.Items.Add("Ten lop A-Z");
            this.cboSort.Items.Add("Ten lop Z-A");
            this.cboSort.Items.Add("Si so tang dan");
            this.cboSort.Items.Add("Si so giam dan");

            int selectedIndex = this.cboSort.Items.IndexOf(selected);
            this.cboSort.SelectedIndex = selectedIndex >= 0 ? selectedIndex : 0;
            this.cboSort.SelectedIndexChanged += new EventHandler(this.FilterChanged);
        }

        private void FilterChanged(object sender, EventArgs e)
        {
            ApplyFilter(true);
        }

        private void ApplyFilter(bool resetPage)
        {
            if (resetPage)
            {
                this.currentPage = 1;
            }

            string keyword = (this.txtSearch.Text ?? string.Empty).Trim();
            string selectedSiSo = this.cboSiSo.SelectedItem == null ? "Tat ca si so" : Convert.ToString(this.cboSiSo.SelectedItem);
            string selectedSort = this.cboSort.SelectedItem == null ? "Ten lop A-Z" : Convert.ToString(this.cboSort.SelectedItem);
            IEnumerable<LopHoc> query = this.lopHocs;

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                query = query.Where(l => Contains(l.TenLop, keyword));
            }

            if (string.Equals(selectedSiSo, "Duoi 30", StringComparison.OrdinalIgnoreCase))
            {
                query = query.Where(l => l.SiSo < 30);
            }
            else if (string.Equals(selectedSiSo, "30 - 40", StringComparison.OrdinalIgnoreCase))
            {
                query = query.Where(l => l.SiSo >= 30 && l.SiSo <= 40);
            }
            else if (string.Equals(selectedSiSo, "Tren 40", StringComparison.OrdinalIgnoreCase))
            {
                query = query.Where(l => l.SiSo > 40);
            }

            if (string.Equals(selectedSort, "Ten lop Z-A", StringComparison.OrdinalIgnoreCase))
            {
                query = query.OrderByDescending(l => l.TenLop);
            }
            else if (string.Equals(selectedSort, "Si so tang dan", StringComparison.OrdinalIgnoreCase))
            {
                query = query.OrderBy(l => l.SiSo).ThenBy(l => l.TenLop);
            }
            else if (string.Equals(selectedSort, "Si so giam dan", StringComparison.OrdinalIgnoreCase))
            {
                query = query.OrderByDescending(l => l.SiSo).ThenBy(l => l.TenLop);
            }
            else
            {
                query = query.OrderBy(l => l.TenLop);
            }

            List<LopHocRow> rows = query
                .Select((l, index) => new LopHocRow
                {
                    Stt = index + 1,
                    MaLop = l.MaLop,
                    TenLop = l.TenLop,
                    SiSo = l.SiSo
                })
                .ToList();

            BindPage(rows);
        }

        private void BindPage(List<LopHocRow> rows)
        {
            int totalRows = rows.Count;
            int totalPages = Math.Max(1, (int)Math.Ceiling(totalRows / (double)PageSize));

            if (this.currentPage > totalPages)
            {
                this.currentPage = totalPages;
            }

            List<LopHocRow> pageRows = rows
                .Skip((this.currentPage - 1) * PageSize)
                .Take(PageSize)
                .ToList();

            this.dgvLopHoc.DataSource = pageRows;
            ConfigureGridColumns();
            UpdatePaging(totalRows, totalPages);
        }

        private void ConfigureGridColumns()
        {
            if (this.dgvLopHoc.Columns.Count == 0)
            {
                return;
            }

            this.dgvLopHoc.Columns[nameof(LopHocRow.Stt)].HeaderText = "STT";
            this.dgvLopHoc.Columns[nameof(LopHocRow.MaLop)].Visible = false;
            this.dgvLopHoc.Columns[nameof(LopHocRow.TenLop)].HeaderText = "Tên lớp";
            this.dgvLopHoc.Columns[nameof(LopHocRow.SiSo)].HeaderText = "Sĩ số";

            if (!this.dgvLopHoc.Columns.Contains("Actions"))
            {
                DataGridViewTextBoxColumn actionColumn = new DataGridViewTextBoxColumn();
                actionColumn.Name = "Actions";
                actionColumn.HeaderText = "Hành động";
                this.dgvLopHoc.Columns.Add(actionColumn);
            }

            this.dgvLopHoc.Columns[nameof(LopHocRow.Stt)].FillWeight = 55;
            this.dgvLopHoc.Columns[nameof(LopHocRow.TenLop)].FillWeight = 260;
            this.dgvLopHoc.Columns[nameof(LopHocRow.SiSo)].FillWeight = 80;
            this.dgvLopHoc.Columns["Actions"].FillWeight = 130;
            this.dgvLopHoc.Columns[nameof(LopHocRow.Stt)].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dgvLopHoc.Columns[nameof(LopHocRow.SiSo)].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dgvLopHoc.Columns["Actions"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dgvLopHoc.Columns[nameof(LopHocRow.SiSo)].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dgvLopHoc.Columns["Actions"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dgvLopHoc.Columns[nameof(LopHocRow.SiSo)].HeaderCell.Style.Padding = Padding.Empty;
            this.dgvLopHoc.Columns["Actions"].HeaderCell.Style.Padding = Padding.Empty;
            this.dgvLopHoc.Columns[nameof(LopHocRow.SiSo)].DefaultCellStyle.Padding = Padding.Empty;
            this.dgvLopHoc.Columns["Actions"].DefaultCellStyle.Padding = Padding.Empty;
        }

        private void UpdatePaging(int totalRows, int totalPages)
        {
            int from = totalRows == 0 ? 0 : ((this.currentPage - 1) * PageSize) + 1;
            int to = Math.Min(this.currentPage * PageSize, totalRows);
            this.lblPagingInfo.Text = string.Format("Hien thi {0}-{1} / {2} lop", from, to, totalRows);

            this.pnlPageButtons.Controls.Clear();
            AddPageButton("<", Math.Max(1, this.currentPage - 1), this.currentPage > 1);

            for (int page = 1; page <= totalPages; page++)
            {
                AddPageButton(page.ToString(), page, true);
            }

            AddPageButton(">", Math.Min(totalPages, this.currentPage + 1), this.currentPage < totalPages);
        }

        private void AddPageButton(string text, int page, bool enabled)
        {
            Button button = new Button();
            bool active = text == this.currentPage.ToString();
            button.BackColor = active ? Color.FromArgb(37, 99, 235) : Color.White;
            button.Cursor = enabled ? Cursors.Hand : Cursors.Default;
            button.Enabled = enabled;
            button.FlatAppearance.BorderColor = active ? Color.FromArgb(37, 99, 235) : Color.FromArgb(226, 232, 240);
            button.FlatStyle = FlatStyle.Flat;
            button.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button.ForeColor = active ? Color.White : Color.FromArgb(51, 65, 85);
            button.Margin = new Padding(3, 0, 3, 0);
            button.Size = new Size(34, 32);
            button.Text = text;
            button.Tag = page;
            button.Click += new EventHandler(this.PageButton_Click);
            this.pnlPageButtons.Controls.Add(button);
        }

        private void PageButton_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;
            if (button == null)
            {
                return;
            }

            this.currentPage = Convert.ToInt32(button.Tag);
            ApplyFilter(false);
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            if (this.isLoading)
            {
                return;
            }

            using (FrmLopHocDialog dialog = new FrmLopHocDialog())
            {
                if (dialog.ShowDialog(this) != DialogResult.OK)
                {
                    return;
                }

                try
                {
                    LopHoc lopHoc = dialog.LopHoc;
                    if (this.lopHocRepository.ExistsByTenLop(lopHoc.TenLop))
                    {
                        MessageBox.Show("Ten lop da ton tai.", "Thong bao", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    this.lopHocRepository.Create(lopHoc);
                    RefreshData(false);
                    MessageBox.Show("Da them lop hoc thanh cong.", "Thanh cong", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    ShowDataError("Khong the them lop hoc.", ex);
                }
            }
        }

        private void EditLopHoc(int maLop)
        {
            LopHoc lopHoc = this.lopHocRepository.GetById(maLop);
            if (lopHoc == null)
            {
                MessageBox.Show("Khong tim thay lop hoc.", "Thong bao", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using (FrmLopHocDialog dialog = new FrmLopHocDialog(lopHoc))
            {
                if (dialog.ShowDialog(this) != DialogResult.OK)
                {
                    return;
                }

                try
                {
                    LopHoc updated = dialog.LopHoc;
                    if (this.lopHocRepository.ExistsByTenLop(updated.TenLop, updated.MaLop))
                    {
                        MessageBox.Show("Ten lop da ton tai.", "Thong bao", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    this.lopHocRepository.Update(updated);
                    RefreshData(false);
                    MessageBox.Show("Da cap nhat lop hoc thanh cong.", "Thanh cong", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    ShowDataError("Khong the cap nhat lop hoc.", ex);
                }
            }
        }

        private void DeleteLopHoc(int maLop)
        {
            DialogResult result = MessageBox.Show("Ban co chac muon xoa lop nay?", "Xac nhan xoa", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result != DialogResult.Yes)
            {
                return;
            }

            try
            {
                this.lopHocRepository.Delete(maLop);
                RefreshData(false);
                MessageBox.Show("Da xoa lop hoc thanh cong.", "Thanh cong", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                ShowDataError("Khong the xoa lop hoc. Lop co the dang duoc tham chieu boi lich thuc hanh.", ex);
            }
        }

        private void DgvLopHoc_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0 || this.dgvLopHoc.Columns[e.ColumnIndex].Name != "Actions")
            {
                return;
            }

            LopHocRow row = this.dgvLopHoc.Rows[e.RowIndex].DataBoundItem as LopHocRow;
            if (row == null)
            {
                return;
            }

            Rectangle cellBounds = this.dgvLopHoc.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true);
            Point cursorPoint = this.dgvLopHoc.PointToClient(Cursor.Position);

            if (GetActionIconBounds(cellBounds, 0).Contains(cursorPoint))
            {
                EditLopHoc(row.MaLop);
            }
            else if (GetActionIconBounds(cellBounds, 1).Contains(cursorPoint))
            {
                DeleteLopHoc(row.MaLop);
            }
        }

        private void DgvLopHoc_CellMouseMove(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0 && this.dgvLopHoc.Columns[e.ColumnIndex].Name == "Actions")
            {
                this.dgvLopHoc.Cursor = Cursors.Hand;
                this.actionToolTip.SetToolTip(this.dgvLopHoc, "Sua hoac xoa lop hoc");
                return;
            }

            this.dgvLopHoc.Cursor = Cursors.Default;
            this.actionToolTip.SetToolTip(this.dgvLopHoc, string.Empty);
        }

        private void DgvLopHoc_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            this.dgvLopHoc.Cursor = Cursors.Default;
            this.actionToolTip.SetToolTip(this.dgvLopHoc, string.Empty);
        }

        private void DgvLopHoc_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0 || this.dgvLopHoc.Columns[e.ColumnIndex].Name != "Actions")
            {
                return;
            }

            e.Handled = true;
            e.PaintBackground(e.CellBounds, true);
            e.Paint(e.CellBounds, DataGridViewPaintParts.Border);

            string[] icons = { "\uE70F", "\uE74D" };
            Color[] iconColors = { Color.FromArgb(37, 99, 235), Color.FromArgb(239, 68, 68) };

            using (Font iconFont = new Font("Segoe MDL2 Assets", 9.5F, FontStyle.Regular, GraphicsUnit.Point, 0))
            using (StringFormat format = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center })
            {
                for (int i = 0; i < icons.Length; i++)
                {
                    Rectangle iconBounds = GetActionIconBounds(e.CellBounds, i);
                    using (GraphicsPath path = RoundedRectangle(iconBounds, 6))
                    using (SolidBrush backBrush = new SolidBrush(Color.FromArgb(248, 250, 252)))
                    using (Pen borderPen = new Pen(Color.FromArgb(226, 232, 240)))
                    using (SolidBrush iconBrush = new SolidBrush(iconColors[i]))
                    {
                        e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                        e.Graphics.FillPath(backBrush, path);
                        e.Graphics.DrawPath(borderPen, path);
                        e.Graphics.DrawString(icons[i], iconFont, iconBrush, iconBounds, format);
                    }
                }
            }
        }

        private static Rectangle GetActionIconBounds(Rectangle cellBounds, int index)
        {
            const int iconSize = 28;
            const int gap = 8;
            int totalWidth = iconSize * 2 + gap;
            int left = cellBounds.Left + (cellBounds.Width - totalWidth) / 2;
            int top = cellBounds.Top + (cellBounds.Height - iconSize) / 2;
            return new Rectangle(left + index * (iconSize + gap), top, iconSize, iconSize);
        }

        private static GraphicsPath RoundedRectangle(Rectangle bounds, int radius)
        {
            int diameter = radius * 2;
            GraphicsPath path = new GraphicsPath();
            path.AddArc(bounds.Left, bounds.Top, diameter, diameter, 180, 90);
            path.AddArc(bounds.Right - diameter, bounds.Top, diameter, diameter, 270, 90);
            path.AddArc(bounds.Right - diameter, bounds.Bottom - diameter, diameter, diameter, 0, 90);
            path.AddArc(bounds.Left, bounds.Bottom - diameter, diameter, diameter, 90, 90);
            path.CloseFigure();
            return path;
        }
        private void BtnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private static bool Contains(string source, string value)
        {
            return (source ?? string.Empty).IndexOf(value, StringComparison.OrdinalIgnoreCase) >= 0;
        }

        private static void ShowDataError(string message, Exception ex)
        {
            MessageBox.Show(message + "\n" + ex.Message, "Loi du lieu", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private class LopHocRow
        {
            public int Stt { get; set; }

            public int MaLop { get; set; }

            public string TenLop { get; set; }

            public int SiSo { get; set; }
        }

        private void pnlAverageStudents_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
