using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using LiveCharts;
using LiveCharts.Wpf;
using QLPhongMay.DTO;

namespace QLPhongMay.GUI.Forms.Reports
{
    public partial class ucBaoCaoTrangThaiMay : UserControl
    {
        private Label lblTitle;
        private LiveCharts.WinForms.PieChart chartComputerStatus;
        private DataGridView dgvComputerStatus;
        private Label lblTotalText;
        private Label lblTotalValue;

        public ucBaoCaoTrangThaiMay()
        {
            InitializeComponent();
            Resize += delegate { LayoutControls(); };
        }

        private void InitializeComponent()
        {
            lblTitle = new Label();
            chartComputerStatus = new LiveCharts.WinForms.PieChart();
            dgvComputerStatus = new DataGridView();
            lblTotalText = new Label();
            lblTotalValue = new Label();
            ((System.ComponentModel.ISupportInitialize)(dgvComputerStatus)).BeginInit();
            SuspendLayout();

            BackColor = Color.White;
            Controls.Add(lblTitle);
            Controls.Add(chartComputerStatus);
            Controls.Add(dgvComputerStatus);
            Controls.Add(lblTotalText);
            Controls.Add(lblTotalValue);
            Name = "ucBaoCaoTrangThaiMay";
            Size = new Size(1230, 526);

            lblTitle.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(15, 23, 42);
            lblTitle.Location = new Point(18, 14);
            lblTitle.Size = new Size(520, 30);
            lblTitle.Text = "Thống kê trạng thái máy tính";

            chartComputerStatus.Location = new Point(18, 56);
            chartComputerStatus.Size = new Size(760, 390);
            chartComputerStatus.LegendLocation = LegendLocation.Right;

            dgvComputerStatus.AllowUserToAddRows = false;
            dgvComputerStatus.AllowUserToDeleteRows = false;
            dgvComputerStatus.AllowUserToResizeRows = false;
            dgvComputerStatus.BackgroundColor = Color.White;
            dgvComputerStatus.BorderStyle = BorderStyle.FixedSingle;
            dgvComputerStatus.ColumnHeadersHeight = 34;
            dgvComputerStatus.EnableHeadersVisualStyles = false;
            dgvComputerStatus.ReadOnly = true;
            dgvComputerStatus.RowHeadersVisible = false;
            dgvComputerStatus.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvComputerStatus.Location = new Point(810, 56);
            dgvComputerStatus.Size = new Size(380, 390);

            lblTotalText.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblTotalText.ForeColor = Color.FromArgb(15, 23, 42);
            lblTotalText.Location = new Point(18, 466);
            lblTotalText.Size = new Size(120, 24);
            lblTotalText.Text = "Tổng máy:";

            lblTotalValue.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblTotalValue.ForeColor = Color.FromArgb(34, 139, 34);
            lblTotalValue.Location = new Point(138, 466);
            lblTotalValue.Size = new Size(500, 24);
            lblTotalValue.Text = "0";

            ((System.ComponentModel.ISupportInitialize)(dgvComputerStatus)).EndInit();
            ResumeLayout(false);
        }

        public void LoadData(List<ReportChartItem> rows)
        {
            SeriesCollection series = new SeriesCollection();
            foreach (ReportChartItem item in rows)
            {
                series.Add(new PieSeries
                {
                    Title = item.Label,
                    Values = new ChartValues<int> { item.Total },
                    DataLabels = true
                });
            }

            chartComputerStatus.Series = series;
            dgvComputerStatus.DataSource = rows.Select(x => new { TrangThai = x.Label, SoMay = x.Total }).ToList();
            dgvComputerStatus.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            SetHeader(dgvComputerStatus, "TrangThai", "Trạng thái", 180);
            SetHeader(dgvComputerStatus, "SoMay", "Số máy", 90);
            lblTotalValue.Text = rows.Sum(x => x.Total).ToString();
        }

        private void LayoutControls()
        {
            int gridHeight = Math.Max(280, Height - 132);
            int statusGridWidth = Math.Min(430, Math.Max(360, Width / 4));
            dgvComputerStatus.Location = new Point(Width - statusGridWidth - 18, 56);
            dgvComputerStatus.Size = new Size(statusGridWidth, gridHeight);
            chartComputerStatus.Location = new Point(18, 56);
            chartComputerStatus.Size = new Size(Math.Max(300, dgvComputerStatus.Left - 36), gridHeight);
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
