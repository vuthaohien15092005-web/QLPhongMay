using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using QLPhongMay.DTO;

namespace QLPhongMay.GUI.Forms.Reports
{
    public partial class ucBaoCaoLichThucHanh : UserControl
    {
        private Label lblTitle;
        private DataGridView dgvSchedules;
        private Label lblTotalText;
        private Label lblTotalValue;

        public ucBaoCaoLichThucHanh()
        {
            InitializeComponent();
            Resize += delegate { LayoutControls(); };
        }

        private void InitializeComponent()
        {
            lblTitle = new Label();
            dgvSchedules = new DataGridView();
            lblTotalText = new Label();
            lblTotalValue = new Label();
            ((System.ComponentModel.ISupportInitialize)(dgvSchedules)).BeginInit();
            SuspendLayout();

            BackColor = Color.White;
            Controls.Add(lblTitle);
            Controls.Add(dgvSchedules);
            Controls.Add(lblTotalText);
            Controls.Add(lblTotalValue);
            Name = "ucBaoCaoLichThucHanh";
            Size = new Size(1230, 420);

            lblTitle.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(15, 23, 42);
            lblTitle.Location = new Point(18, 14);
            lblTitle.Size = new Size(520, 30);
            lblTitle.Text = "Lịch thực hành theo thời gian";

            dgvSchedules.AllowUserToAddRows = false;
            dgvSchedules.AllowUserToDeleteRows = false;
            dgvSchedules.AllowUserToResizeRows = false;
            dgvSchedules.BackgroundColor = Color.White;
            dgvSchedules.BorderStyle = BorderStyle.FixedSingle;
            dgvSchedules.ColumnHeadersHeight = 34;
            dgvSchedules.EnableHeadersVisualStyles = false;
            dgvSchedules.Location = new Point(18, 56);
            dgvSchedules.ReadOnly = true;
            dgvSchedules.RowHeadersVisible = false;
            dgvSchedules.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvSchedules.Size = new Size(1194, 288);

            lblTotalText.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblTotalText.ForeColor = Color.FromArgb(15, 23, 42);
            lblTotalText.Location = new Point(18, 360);
            lblTotalText.Size = new Size(120, 24);
            lblTotalText.Text = "Tổng lịch:";

            lblTotalValue.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblTotalValue.ForeColor = Color.FromArgb(34, 139, 34);
            lblTotalValue.Location = new Point(138, 360);
            lblTotalValue.Size = new Size(500, 24);
            lblTotalValue.Text = "0";

            ((System.ComponentModel.ISupportInitialize)(dgvSchedules)).EndInit();
            ResumeLayout(false);
        }

        public void LoadData(List<ScheduleReportRow> rows)
        {
            dgvSchedules.DataSource = rows.Select(x => new
            {
                MaLich = x.MaLich,
                NgayThucHanh = x.NgayThucHanh.ToString("dd/MM/yyyy"),
                PhongMay = x.PhongMay,
                LopHoc = x.LopHoc,
                CaHoc = x.CaHoc,
                SoLuongSV = x.SoLuongSV,
                TrangThai = x.TrangThai
            }).ToList();

            dgvSchedules.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            SetHeader(dgvSchedules, "MaLich", "Mã lịch", 80);
            SetHeader(dgvSchedules, "NgayThucHanh", "Ngày thực hành", 120);
            SetHeader(dgvSchedules, "PhongMay", "Phòng máy", 150);
            SetHeader(dgvSchedules, "LopHoc", "Lớp học", 140);
            SetHeader(dgvSchedules, "CaHoc", "Ca học", 120);
            SetHeader(dgvSchedules, "SoLuongSV", "Số SV", 80);
            SetHeader(dgvSchedules, "TrangThai", "Trạng thái", 140);
            lblTotalValue.Text = rows.Count.ToString();
        }

        private void LayoutControls()
        {
            int gridHeight = Math.Max(280, Height - 132);
            dgvSchedules.Location = new Point(18, 56);
            dgvSchedules.Size = new Size(Math.Max(300, Width - 36), gridHeight);
            lblTotalText.Location = new Point(18, gridHeight + 72);
            lblTotalValue.Location = new Point(138, gridHeight + 72);
        }

        private static void SetHeader(DataGridView grid, string columnName, string header, int weight)
        {
            if (!grid.Columns.Contains(columnName))
            {
                return;
            }

            grid.Columns[columnName].HeaderText = header;
            grid.Columns[columnName].MinimumWidth = Math.Min(weight, 80);
            grid.Columns[columnName].FillWeight = weight;
            grid.Columns[columnName].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }
    }
}
