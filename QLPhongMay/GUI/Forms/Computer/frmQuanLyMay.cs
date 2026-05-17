using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using QLPhongMay.DAL;
using QLPhongMay.DTO;

namespace QLPhongMay.GUI.Forms.Computer
{
    public partial class frmQuanLyMay : Form
    {
        private const int PageSize = 5;
        private const int EmSetCueBanner = 0x1501;
        private readonly ComputerRepository repository = new ComputerRepository();
        private readonly ToolTip actionToolTip = new ToolTip();
        private readonly Color blue = Color.FromArgb(37, 99, 235);
        private readonly Color text = Color.FromArgb(15, 23, 42);
        private readonly Color muted = Color.FromArgb(100, 116, 139);
        private readonly Color border = Color.FromArgb(226, 232, 240);
        private List<ComputerRow> rows = new List<ComputerRow>();
        private int currentPage = 1;

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wParam, string lParam);

        public frmQuanLyMay()
        {
            InitializeComponent();
            SetSearchPlaceholder();
            this.Load += FrmQuanLyMay_Load;
        }


        private void SetSearchPlaceholder()
        {
            if (this.txtSearch.IsHandleCreated)
            {
                SendMessage(this.txtSearch.Handle, EmSetCueBanner, (IntPtr)1, "Tìm máy theo tên");
            }
            else
            {
                this.txtSearch.HandleCreated += TxtSearch_HandleCreated;
            }
        }

        private void TxtSearch_HandleCreated(object sender, EventArgs e)
        {
            SendMessage(this.txtSearch.Handle, EmSetCueBanner, (IntPtr)1, "Tìm máy theo tên");
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

        private void FrmQuanLyMay_Load(object sender, EventArgs e)
        {
            try
            {
                LoadFilterLookups();
                RefreshComputers(true);
            }
            catch (Exception ex)
            {
                ShowError("Không thể tải dữ liệu máy tính từ database QuanLyPhongMay.", ex);
            }
        }

        private void LoadFilterLookups()
        {
            LoadCombo(this.cboRoom, "Tất cả phòng máy", this.repository.GetRoomLookup());
            LoadCombo(this.cboRam, "Tất cả RAM", this.repository.GetRamLookup());
            LoadCombo(this.cboCpu, "Tất cả CPU", this.repository.GetCpuLookup());
            LoadCombo(this.cboMonitor, "Tất cả màn hình", this.repository.GetMonitorLookup());
            LoadCombo(this.cboOs, "Tất cả HĐH", this.repository.GetOperatingSystemLookup());
        }

        private void RefreshComputers(bool resetPage)
        {
            this.rows = this.repository.GetComputerList().Select(ToRow).ToList();
            BuildStatCards();
            LoadComputers(resetPage);
        }

        private void BuildStatCards()
        {
            this.pnlStats.Controls.Clear();
            AddStatCard("Tổng số máy", this.rows.Count.ToString(), this.blue, Color.FromArgb(239, 246, 255), "T");
            AddStatCard("Máy đang hoạt động", this.rows.Count(x => IsActive(x.RawStatus)).ToString(), Color.FromArgb(22, 163, 74), Color.FromArgb(240, 253, 244), "OK");
            AddStatCard("Máy bảo trì / hỏng", this.rows.Count(x => IsMaintenance(x.RawStatus) || IsBroken(x.RawStatus)).ToString(), Color.FromArgb(245, 158, 11), Color.FromArgb(255, 251, 235), "!");
            AddStatCard("Tổng số phòng máy", this.rows.Select(x => x.MaPhong).Distinct().Count().ToString(), Color.FromArgb(20, 184, 166), Color.FromArgb(240, 253, 250), "P");
        }

        private void LoadComputers(bool resetPage)
        {
            if (resetPage)
            {
                this.currentPage = 1;
            }

            List<ComputerRow> filtered = GetFilteredRows().ToList();
            int total = filtered.Count;
            int totalPages = Math.Max(1, (int)Math.Ceiling(total / (double)PageSize));
            this.currentPage = Math.Max(1, Math.Min(this.currentPage, totalPages));
            int skip = (this.currentPage - 1) * PageSize;
            List<ComputerRow> pageRows = filtered.Skip(skip).Take(PageSize).Select((x, i) => x.CloneWithStt(skip + i + 1)).ToList();
            this.dgvComputers.DataSource = pageRows;
            ConfigureGridColumns();
            BuildPagination(totalPages);
            this.lblPagingInfo.Text = total == 0 ? "Không có dữ liệu phù hợp" : string.Format("Hiển thị {0}-{1} trong {2} máy", skip + 1, skip + pageRows.Count, total);
        }

        private IEnumerable<ComputerRow> GetFilteredRows()
        {
            IEnumerable<ComputerRow> source = this.rows;
            string keyword = this.txtSearch.Text.Trim().ToLowerInvariant();
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                source = source.Where(x => Contains(x.TenMay, keyword));
            }

            string roomId = Convert.ToString(this.cboRoom.SelectedValue);
            if (!string.IsNullOrWhiteSpace(roomId))
            {
                source = source.Where(x => x.MaPhong.ToString() == roomId);
            }

            string selectedRam = Convert.ToString(this.cboRam.SelectedValue);
            if (!string.IsNullOrWhiteSpace(selectedRam))
            {
                source = source.Where(x => x.MaRam.HasValue && x.MaRam.Value.ToString() == selectedRam);
            }

            string selectedCpu = Convert.ToString(this.cboCpu.SelectedValue);
            if (!string.IsNullOrWhiteSpace(selectedCpu))
            {
                source = source.Where(x => x.MaCPU.HasValue && x.MaCPU.Value.ToString() == selectedCpu);
            }

            string selectedMonitor = Convert.ToString(this.cboMonitor.SelectedValue);
            if (!string.IsNullOrWhiteSpace(selectedMonitor))
            {
                source = source.Where(x => x.MaManHinh.HasValue && x.MaManHinh.Value.ToString() == selectedMonitor);
            }

            string selectedOs = Convert.ToString(this.cboOs.SelectedValue);
            if (!string.IsNullOrWhiteSpace(selectedOs))
            {
                source = source.Where(x => x.MaHDH.HasValue && x.MaHDH.Value.ToString() == selectedOs);
            }

            if (this.cboStatus.SelectedIndex > 0)
            {
                source = source.Where(x => x.TrangThai == Convert.ToString(this.cboStatus.SelectedItem));
            }

            return source;
        }

        private void ConfigureGridColumns()
        {
            if (this.dgvComputers.Columns.Count == 0)
            {
                return;
            }

            this.dgvComputers.Columns[nameof(ComputerRow.Stt)].HeaderText = "STT";
            this.dgvComputers.Columns[nameof(ComputerRow.MaMay)].Visible = false;
            this.dgvComputers.Columns[nameof(ComputerRow.TenMay)].HeaderText = "Tên máy";
            this.dgvComputers.Columns[nameof(ComputerRow.TenPhong)].HeaderText = "Phòng máy";
            this.dgvComputers.Columns[nameof(ComputerRow.Ram)].HeaderText = "RAM";
            this.dgvComputers.Columns[nameof(ComputerRow.BoCPU)].HeaderText = "Bộ CPU";
            this.dgvComputers.Columns[nameof(ComputerRow.ManHinh)].HeaderText = "Màn hình";
            this.dgvComputers.Columns[nameof(ComputerRow.HeDieuHanh)].HeaderText = "Hệ điều hành";
            this.dgvComputers.Columns[nameof(ComputerRow.TrangThai)].HeaderText = "Trạng thái";
            this.dgvComputers.Columns[nameof(ComputerRow.RawStatus)].Visible = false;
            this.dgvComputers.Columns[nameof(ComputerRow.MaPhong)].Visible = false;
            this.dgvComputers.Columns[nameof(ComputerRow.MaRam)].Visible = false;
            this.dgvComputers.Columns[nameof(ComputerRow.MaCPU)].Visible = false;
            this.dgvComputers.Columns[nameof(ComputerRow.MaManHinh)].Visible = false;
            this.dgvComputers.Columns[nameof(ComputerRow.MaHDH)].Visible = false;

            if (!this.dgvComputers.Columns.Contains("Actions"))
            {
                this.dgvComputers.Columns.Add(new DataGridViewTextBoxColumn { Name = "Actions", HeaderText = "Hành động", ReadOnly = true });
            }

            this.dgvComputers.Columns[nameof(ComputerRow.Stt)].FillWeight = 48;
            this.dgvComputers.Columns[nameof(ComputerRow.TenMay)].FillWeight = 130;
            this.dgvComputers.Columns[nameof(ComputerRow.TenPhong)].FillWeight = 120;
            this.dgvComputers.Columns[nameof(ComputerRow.Ram)].FillWeight = 78;
            this.dgvComputers.Columns[nameof(ComputerRow.BoCPU)].FillWeight = 190;
            this.dgvComputers.Columns[nameof(ComputerRow.ManHinh)].FillWeight = 95;
            this.dgvComputers.Columns[nameof(ComputerRow.HeDieuHanh)].FillWeight = 125;
            this.dgvComputers.Columns[nameof(ComputerRow.TrangThai)].FillWeight = 125;
            this.dgvComputers.Columns["Actions"].FillWeight = 95;
            this.dgvComputers.Columns[nameof(ComputerRow.Stt)].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dgvComputers.Columns[nameof(ComputerRow.Ram)].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dgvComputers.Columns[nameof(ComputerRow.ManHinh)].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dgvComputers.Columns[nameof(ComputerRow.TrangThai)].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dgvComputers.Columns["Actions"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }

        private void DgvComputers_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            string column = this.dgvComputers.Columns[e.ColumnIndex].Name;
            if (column == nameof(ComputerRow.TrangThai))
            {
                Color fore;
                Color back;
                GetStatusColors(Convert.ToString(e.Value), out fore, out back);
                PaintBadge(e, Convert.ToString(e.Value), fore, back);
            }
            else if (column == "Actions")
            {
                PaintActions(e);
            }
        }

        private void PaintBadge(DataGridViewCellPaintingEventArgs e, string value, Color fore, Color back)
        {
            e.Handled = true;
            e.PaintBackground(e.CellBounds, true);
            Size textSize = TextRenderer.MeasureText(value, this.dgvComputers.DefaultCellStyle.Font);
            Rectangle rect = new Rectangle(e.CellBounds.Left + (e.CellBounds.Width - textSize.Width - 30) / 2, e.CellBounds.Top + 9, textSize.Width + 30, 28);
            using (GraphicsPath path = RoundRect(rect, 14))
            using (SolidBrush backBrush = new SolidBrush(back))
            using (SolidBrush textBrush = new SolidBrush(fore))
            using (StringFormat format = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center })
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                e.Graphics.FillPath(backBrush, path);
                e.Graphics.DrawString(value, this.dgvComputers.DefaultCellStyle.Font, textBrush, rect, format);
            }
        }

        private void PaintActions(DataGridViewCellPaintingEventArgs e)
        {
            e.Handled = true;
            e.PaintBackground(e.CellBounds, true);
            string[] icons = { "\uE70F", "\uE74D" };
            Color[] colors = { this.blue, Color.FromArgb(220, 38, 38) };
            int size = 30;
            int startX = e.CellBounds.Left + (e.CellBounds.Width - (size * 2 + 6)) / 2;
            int y = e.CellBounds.Top + (e.CellBounds.Height - size) / 2;

            using (Font iconFont = new Font("Segoe MDL2 Assets", 10F))
            using (StringFormat format = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center })
            {
                for (int i = 0; i < icons.Length; i++)
                {
                    Rectangle rect = new Rectangle(startX + i * (size + 6), y, size, size);
                    using (GraphicsPath path = RoundRect(rect, 6))
                    using (SolidBrush backBrush = new SolidBrush(Color.FromArgb(248, 250, 252)))
                    using (Pen pen = new Pen(this.border))
                    using (SolidBrush iconBrush = new SolidBrush(colors[i]))
                    {
                        e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                        e.Graphics.FillPath(backBrush, path);
                        e.Graphics.DrawPath(pen, path);
                        e.Graphics.DrawString(icons[i], iconFont, iconBrush, rect, format);
                    }
                }
            }
        }

        private void DgvComputers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0 || this.dgvComputers.Columns[e.ColumnIndex].Name != "Actions")
            {
                return;
            }

            ComputerRow row = this.dgvComputers.Rows[e.RowIndex].DataBoundItem as ComputerRow;
            if (row == null)
            {
                return;
            }

            Rectangle cell = this.dgvComputers.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false);
            string action = GetAction(cell.Width, this.dgvComputers.PointToClient(Cursor.Position).X - cell.Left);
            if (action == "Edit")
            {
                EditComputer(row);
            }
            else if (action == "Delete")
            {
                DeleteComputer(row);
            }
        }

        private void AddComputer()
        {
            using (FrmMayDialog dialog = new FrmMayDialog(this.repository))
            {
                if (dialog.ShowDialog(this) != DialogResult.OK)
                {
                    return;
                }

                try
                {
                    this.repository.CreateComputer(dialog.ComputerData);
                    LoadFilterLookups();
                    RefreshComputers(true);
                    MessageBox.Show("Đã thêm máy thành công.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    ShowError("Không thể thêm máy.", ex);
                }
            }
        }

        private void EditComputer(ComputerRow row)
        {
            using (FrmMayDialog dialog = new FrmMayDialog(this.repository, ToEditItem(row)))
            {
                if (dialog.ShowDialog(this) != DialogResult.OK)
                {
                    return;
                }

                try
                {
                    this.repository.UpdateComputer(dialog.ComputerData);
                    LoadFilterLookups();
                    RefreshComputers(false);
                    MessageBox.Show("Đã cập nhật máy thành công.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    ShowError("Không thể cập nhật máy.", ex);
                }
            }
        }

        private static ComputerEditItem ToEditItem(ComputerRow row)
        {
            return new ComputerEditItem
            {
                MaMay = row.MaMay,
                TenMay = row.TenMay,
                MaPhong = row.MaPhong,
                TinhTrang = GetStatusDatabaseText(row.RawStatus),
                MaRam = row.MaRam,
                TenRam = row.Ram,
                MaCPU = row.MaCPU,
                TenCPU = row.BoCPU,
                MaManHinh = row.MaManHinh,
                TenManHinh = row.ManHinh,
                MaHDH = row.MaHDH,
                TenHDH = row.HeDieuHanh
            };
        }

        private static string GetStatusDatabaseText(string status)
        {
            if (IsActive(status))
            {
                return "Tốt";
            }

            if (IsMaintenance(status))
            {
                return "Đang bảo trì";
            }

            if (IsBroken(status))
            {
                return "Hỏng";
            }

            return status;
        }

        private void DeleteComputer(ComputerRow row)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc muốn xóa máy '" + row.TenMay + "'?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result != DialogResult.Yes)
            {
                return;
            }

            try
            {
                this.repository.DeleteComputer(row.MaMay);
                RefreshComputers(false);
                MessageBox.Show("Đã xóa máy thành công.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                ShowError("Không thể xóa máy. Máy có thể đang được tham chiếu bởi dữ liệu khác.", ex);
            }
        }

        private void DgvComputers_CellMouseMove(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0 || this.dgvComputers.Columns[e.ColumnIndex].Name != "Actions")
            {
                this.dgvComputers.Cursor = Cursors.Default;
                this.actionToolTip.SetToolTip(this.dgvComputers, string.Empty);
                return;
            }

            string action = GetAction(this.dgvComputers.Columns[e.ColumnIndex].Width, e.X);
            string tip = action == "Edit" ? "Sửa máy" : action == "Delete" ? "Xóa máy" : string.Empty;
            this.dgvComputers.Cursor = string.IsNullOrEmpty(tip) ? Cursors.Default : Cursors.Hand;
            this.actionToolTip.SetToolTip(this.dgvComputers, tip);
        }

        private void DgvComputers_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            this.dgvComputers.Cursor = Cursors.Default;
            this.actionToolTip.SetToolTip(this.dgvComputers, string.Empty);
        }

        private void BuildPagination(int totalPages)
        {
            this.pnlPageButtons.Controls.Clear();
            AddPageButton("<", this.currentPage > 1, this.currentPage - 1, false);
            for (int page = 1; page <= totalPages; page++)
            {
                AddPageButton(page.ToString(), true, page, page == this.currentPage);
            }
            AddPageButton(">", this.currentPage < totalPages, this.currentPage + 1, false);

            int width = this.pnlPageButtons.Controls.Cast<Control>().Sum(c => c.Width + c.Margin.Left + c.Margin.Right);
            this.pnlPageButtons.Width = width;
            this.pnlPageButtons.Left = this.pnlPaging.Width - width - 18;
        }

        private void AddPageButton(string label, bool enabled, int page, bool active)
        {
            Button button = new Button();
            StyleButton(button, label, active ? this.blue : Color.White, active ? Color.White : Color.FromArgb(71, 85, 105), new Size(32, 32));
            button.Enabled = enabled;
            button.Margin = new Padding(0, 0, 6, 0);
            button.Tag = page;
            button.Click += PageButton_Click;
            this.pnlPageButtons.Controls.Add(button);
        }

        private void AddStatCard(string title, string value, Color accent, Color iconBack, string iconText)
        {
            Panel card = new Panel();
            Label icon = new Label();
            Label lblValue = new Label();
            Label lblTitle = new Label();
            card.BackColor = Color.White;
            card.Margin = new Padding(0, 0, 12, 0);
            card.Size = new Size(208, 78);
            card.Paint += PanelBorder_Paint;
            icon.BackColor = iconBack;
            icon.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            icon.ForeColor = accent;
            icon.Location = new Point(16, 18);
            icon.Size = new Size(38, 38);
            icon.Text = iconText;
            icon.TextAlign = ContentAlignment.MiddleCenter;
            icon.Paint += RoundLabel_Paint;
            lblValue.BackColor = Color.White;
            lblValue.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            lblValue.ForeColor = this.text;
            lblValue.Location = new Point(70, 12);
            lblValue.Size = new Size(120, 32);
            lblValue.Text = value;
            lblTitle.BackColor = Color.White;
            lblTitle.Font = new Font("Segoe UI", 9F);
            lblTitle.ForeColor = this.muted;
            lblTitle.Location = new Point(72, 48);
            lblTitle.Size = new Size(130, 22);
            lblTitle.Text = title;
            card.Controls.Add(icon);
            card.Controls.Add(lblValue);
            card.Controls.Add(lblTitle);
            this.pnlStats.Controls.Add(card);
        }

        private static void LoadCombo(ComboBox combo, string firstText, List<ComputerLookupItem> items)
        {
            List<ComputerLookupItem> source = new List<ComputerLookupItem> { new ComputerLookupItem { Id = string.Empty, Name = firstText } };
            source.AddRange(items);
            combo.DisplayMember = "Name";
            combo.ValueMember = "Id";
            combo.DataSource = source;
        }

        private static ComputerRow ToRow(ComputerListItem item)
        {
            return new ComputerRow
            {
                MaMay = item.MaMay,
                TenMay = item.TenMay ?? string.Empty,
                RawStatus = item.TinhTrang ?? string.Empty,
                TrangThai = StatusText(item.TinhTrang),
                MaPhong = item.MaPhong,
                TenPhong = item.TenPhong ?? string.Empty,
                MaRam = item.MaRam,
                Ram = item.TenRam ?? string.Empty,
                MaCPU = item.MaCPU,
                BoCPU = item.TenCPU ?? string.Empty,
                MaManHinh = item.MaManHinh,
                ManHinh = item.TenManHinh ?? string.Empty,
                MaHDH = item.MaHDH,
                HeDieuHanh = item.TenHDH ?? string.Empty
            };
        }

        private static void StyleButton(Button button, string label, Color fill, Color fore, Size size)
        {
            button.BackColor = fill;
            button.Cursor = Cursors.Hand;
            button.FlatAppearance.BorderColor = fill == Color.White ? Color.FromArgb(226, 232, 240) : fill;
            button.FlatAppearance.BorderSize = 1;
            button.FlatStyle = FlatStyle.Flat;
            button.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            button.ForeColor = fore;
            button.Size = size;
            button.Text = label;
            button.UseVisualStyleBackColor = false;
        }

        private void StyleTextBox(TextBox box, string placeholder, Point location, Size size)
        {
            box.BorderStyle = BorderStyle.FixedSingle;
            box.Font = new Font("Segoe UI", 10F);
            box.ForeColor = this.text;
            box.Location = location;
            box.Size = size;
            box.TextChanged += FilterChanged;
            box.HandleCreated += (sender, e) => SendMessage(box.Handle, EmSetCueBanner, (IntPtr)1, placeholder);
        }

        private void StyleCombo(ComboBox combo, Point location, Size size)
        {
            combo.DropDownStyle = ComboBoxStyle.DropDownList;
            combo.FlatStyle = FlatStyle.Flat;
            combo.Font = new Font("Segoe UI", 10F);
            combo.ForeColor = Color.FromArgb(51, 65, 85);
            combo.Location = location;
            combo.Size = size;
            combo.SelectedIndexChanged += FilterChanged;
        }

        private void FilterChanged(object sender, EventArgs e)
        {
            if (this.dgvComputers != null && this.rows != null)
            {
                LoadComputers(true);
            }
        }

        private void PageButton_Click(object sender, EventArgs e)
        {
            int page = Convert.ToInt32(((Button)sender).Tag);
            if (page > 0 && page != this.currentPage)
            {
                this.currentPage = page;
                LoadComputers(false);
            }
        }

        private void BtnResetConfigFilters_Click(object sender, EventArgs e)
        {
            this.txtSearch.Text = string.Empty;
            ResetCombo(this.cboRoom);
            ResetCombo(this.cboStatus);
            ResetCombo(this.cboRam);
            ResetCombo(this.cboCpu);
            ResetCombo(this.cboMonitor);
            ResetCombo(this.cboOs);
            RefreshComputers(true);
        }

        private static void ResetCombo(ComboBox combo)
        {
            if (combo.Items.Count > 0)
            {
                combo.SelectedIndex = 0;
            }
        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            AddComputer();
        }

        private static string GetAction(int cellWidth, int x)
        {
            int size = 30;
            int start = (cellWidth - (size * 2 + 6)) / 2;
            if (x >= start && x <= start + size)
            {
                return "Edit";
            }

            if (x >= start + size + 6 && x <= start + size * 2 + 6)
            {
                return "Delete";
            }

            return string.Empty;
        }

        private static string StatusText(string status)
        {
            if (IsActive(status))
            {
                return "Đang hoạt động";
            }

            if (IsMaintenance(status))
            {
                return "Đang bảo trì";
            }

            if (IsBroken(status))
            {
                return "Hỏng";
            }

            return string.IsNullOrWhiteSpace(status) ? "Không xác định" : status;
        }

        private static void GetStatusColors(string status, out Color fore, out Color back)
        {
            if (status == "Đang hoạt động")
            {
                fore = Color.FromArgb(22, 163, 74);
                back = Color.FromArgb(240, 253, 244);
            }
            else if (status == "Đang bảo trì")
            {
                fore = Color.FromArgb(217, 119, 6);
                back = Color.FromArgb(255, 251, 235);
            }
            else
            {
                fore = Color.FromArgb(220, 38, 38);
                back = Color.FromArgb(254, 242, 242);
            }
        }

        private static bool IsActive(string status)
        {
            return string.Equals(status, "Tốt", StringComparison.OrdinalIgnoreCase) ||
                   string.Equals(status, "Đang hoạt động", StringComparison.OrdinalIgnoreCase);
        }

        private static bool IsMaintenance(string status)
        {
            return string.Equals(status, "Đang bảo trì", StringComparison.OrdinalIgnoreCase) ||
                   string.Equals(status, "DangBaoTri", StringComparison.OrdinalIgnoreCase);
        }

        private static bool IsBroken(string status)
        {
            return string.Equals(status, "Hỏng", StringComparison.OrdinalIgnoreCase) ||
                   string.Equals(status, "Hong", StringComparison.OrdinalIgnoreCase);
        }

        private static bool Contains(string value, string keyword)
        {
            return !string.IsNullOrEmpty(value) && value.ToLowerInvariant().Contains(keyword);
        }

        private void RoundLabel_Paint(object sender, PaintEventArgs e)
        {
            Label label = (Label)sender;
            using (GraphicsPath path = RoundRect(label.ClientRectangle, 12))
            {
                label.Region = new Region(path);
            }
        }

        private void PanelBorder_Paint(object sender, PaintEventArgs e)
        {
            Control control = (Control)sender;
            Rectangle rect = new Rectangle(0, 0, control.Width - 1, control.Height - 1);
            using (Pen pen = new Pen(this.border))
            {
                e.Graphics.DrawRectangle(pen, rect);
            }
        }

        private static GraphicsPath RoundRect(Rectangle rect, int radius)
        {
            int d = radius * 2;
            GraphicsPath path = new GraphicsPath();
            path.AddArc(rect.Left, rect.Top, d, d, 180, 90);
            path.AddArc(rect.Right - d, rect.Top, d, d, 270, 90);
            path.AddArc(rect.Right - d, rect.Bottom - d, d, d, 0, 90);
            path.AddArc(rect.Left, rect.Bottom - d, d, d, 90, 90);
            path.CloseFigure();
            return path;
        }

        private static void ShowError(string message, Exception ex)
        {
            MessageBox.Show(message + "\n" + ex.Message, "Lỗi dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private class ComputerRow
        {
            public int Stt { get; set; }
            public int MaMay { get; set; }
            public string TenMay { get; set; }
            public int MaPhong { get; set; }
            public string TenPhong { get; set; }
            public int? MaRam { get; set; }
            public string Ram { get; set; }
            public int? MaCPU { get; set; }
            public string BoCPU { get; set; }
            public int? MaManHinh { get; set; }
            public string ManHinh { get; set; }
            public int? MaHDH { get; set; }
            public string HeDieuHanh { get; set; }
            public string RawStatus { get; set; }
            public string TrangThai { get; set; }

            public ComputerRow CloneWithStt(int stt)
            {
                return new ComputerRow
                {
                    Stt = stt,
                    MaMay = this.MaMay,
                    TenMay = this.TenMay,
                    MaPhong = this.MaPhong,
                    TenPhong = this.TenPhong,
                    MaRam = this.MaRam,
                    Ram = this.Ram,
                    MaCPU = this.MaCPU,
                    BoCPU = this.BoCPU,
                    MaManHinh = this.MaManHinh,
                    ManHinh = this.ManHinh,
                    MaHDH = this.MaHDH,
                    HeDieuHanh = this.HeDieuHanh,
                    RawStatus = this.RawStatus,
                    TrangThai = this.TrangThai
                };
            }
        }

        private void pnlStats_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}


