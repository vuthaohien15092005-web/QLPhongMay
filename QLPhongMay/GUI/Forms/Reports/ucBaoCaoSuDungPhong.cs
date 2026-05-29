using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using QLPhongMay.DTO;

namespace QLPhongMay.GUI.Forms.Reports
{
    public partial class ucBaoCaoSuDungPhong : UserControl
    {
        private Label lblTitle;
        private DataGridView dgvRoomUsage;
        private Label lblTotalText;
        private Label lblTotalValue;

        public ucBaoCaoSuDungPhong()
        {
            InitializeComponent();
            Resize += delegate { LayoutControls(); };
        }

        private void InitializeComponent()
        {
            lblTitle = new Label();
            dgvRoomUsage = new DataGridView();
            lblTotalText = new Label();
            lblTotalValue = new Label();
            ((System.ComponentModel.ISupportInitialize)(dgvRoomUsage)).BeginInit();
            SuspendLayout();

            BackColor = Color.White;
            Controls.Add(lblTitle);
            Controls.Add(dgvRoomUsage);
            Controls.Add(lblTotalText);
            Controls.Add(lblTotalValue);
            Name = "ucBaoCaoSuDungPhong";
            Size = new Size(1230, 526);

            lblTitle.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(15, 23, 42);
            lblTitle.Location = new Point(18, 14);
            lblTitle.Size = new Size(520, 30);
            lblTitle.Text = "Tỷ lệ sử dụng phòng máy";

            dgvRoomUsage.AllowUserToAddRows = false;
            dgvRoomUsage.AllowUserToDeleteRows = false;
            dgvRoomUsage.AllowUserToResizeRows = false;
            dgvRoomUsage.BackgroundColor = Color.White;
            dgvRoomUsage.BorderStyle = BorderStyle.FixedSingle;
            dgvRoomUsage.ColumnHeadersHeight = 34;
            dgvRoomUsage.EnableHeadersVisualStyles = false;
            dgvRoomUsage.Location = new Point(18, 56);
            dgvRoomUsage.ReadOnly = true;
            dgvRoomUsage.RowHeadersVisible = false;
            dgvRoomUsage.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvRoomUsage.Size = new Size(1194, 394);

            lblTotalText.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblTotalText.ForeColor = Color.FromArgb(15, 23, 42);
            lblTotalText.Location = new Point(18, 466);
            lblTotalText.Size = new Size(120, 24);
            lblTotalText.Text = "Tổng lịch:";

            lblTotalValue.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblTotalValue.ForeColor = Color.FromArgb(34, 139, 34);
            lblTotalValue.Location = new Point(138, 466);
            lblTotalValue.Size = new Size(500, 24);
            lblTotalValue.Text = "0";

            ((System.ComponentModel.ISupportInitialize)(dgvRoomUsage)).EndInit();
            ResumeLayout(false);
        }

        public void LoadData(List<RoomUsageReportRow> rows)
        {
            dgvRoomUsage.DataSource = rows.Select(x => new
            {
                MaPhong = x.MaPhong,
                TenPhong = x.TenPhong,
                SucChua = x.SucChua,
                TrangThai = x.TrangThai,
                SoMay = x.SoMay,
                SoMayTot = x.SoMayTot,
                SoLich = x.SoLich,
                LanSuDung = x.LanSuDungText
            }).ToList();

            dgvRoomUsage.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            SetHeader(dgvRoomUsage, "MaPhong", "Mã phòng", 90);
            SetHeader(dgvRoomUsage, "TenPhong", "Tên phòng", 180);
            SetHeader(dgvRoomUsage, "SucChua", "Sức chứa", 90);
            SetHeader(dgvRoomUsage, "TrangThai", "Trạng thái", 120);
            SetHeader(dgvRoomUsage, "SoMay", "Số máy", 90);
            SetHeader(dgvRoomUsage, "SoMayTot", "Máy tốt", 90);
            SetHeader(dgvRoomUsage, "SoLich", "Số lịch", 90);
            SetHeader(dgvRoomUsage, "LanSuDung", "Lần sử dụng gần nhất", 170);
            lblTotalValue.Text = rows.Sum(x => x.SoLich).ToString();
        }

        private void LayoutControls()
        {
            int gridHeight = Math.Max(280, Height - 132);
            dgvRoomUsage.Location = new Point(18, 56);
            dgvRoomUsage.Size = new Size(Math.Max(300, Width - 36), gridHeight);
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
