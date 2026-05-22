using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using QLPhongMay.DAL;
using QLPhongMay.DTO;

namespace QLPhongMay.GUI.Forms.Computer
{
    public class FrmMayDialog : Form
    {
        private readonly ComputerRepository repository;
        private readonly bool editMode;
        private readonly ComputerEditItem originalItem;

        private Label lblTitle;
        private Label lblSubtitle;
        private Label lblComputerName;
        private Label lblRoom;
        private Label lblStatus;
        private Label lblRam;
        private Label lblCpu;
        private Label lblMonitor;
        private Label lblOs;
        private TextBox txtComputerName;
        private ComboBox cboRoom;
        private ComboBox cboStatus;
        private ComboBox cboRam;
        private ComboBox cboCpu;
        private ComboBox cboMonitor;
        private ComboBox cboOs;
        private Button btnCancel;
        private Button btnSave;

        public ComputerEditItem ComputerData { get; private set; }

        public FrmMayDialog(ComputerRepository repository, ComputerEditItem item = null)
        {
            this.repository = repository;
            this.originalItem = item;
            this.editMode = item != null;
            InitializeComponent();
            ApplyDialogModeText();
            Load += FrmMayDialog_Load;
        }

        private void InitializeComponent()
        {
            this.lblTitle = new Label();
            this.lblSubtitle = new Label();
            this.lblComputerName = new Label();
            this.lblRoom = new Label();
            this.lblStatus = new Label();
            this.lblRam = new Label();
            this.lblCpu = new Label();
            this.lblMonitor = new Label();
            this.lblOs = new Label();
            this.txtComputerName = new TextBox();
            this.cboRoom = new ComboBox();
            this.cboStatus = new ComboBox();
            this.cboRam = new ComboBox();
            this.cboCpu = new ComboBox();
            this.cboMonitor = new ComboBox();
            this.cboOs = new ComboBox();
            this.btnCancel = new Button();
            this.btnSave = new Button();
            this.SuspendLayout();

            this.BackColor = Color.White;
            this.ClientSize = new Size(620, 520);
            this.Font = new Font("Segoe UI", 9F);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.StartPosition = FormStartPosition.CenterParent;
            this.Text = "Thêm máy tính";

            this.lblTitle.AutoSize = true;
            this.lblTitle.BackColor = Color.Transparent;
            this.lblTitle.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            this.lblTitle.ForeColor = Color.FromArgb(15, 23, 42);
            this.lblTitle.Location = new Point(32, 24);
            this.lblTitle.Text = "Thêm máy tính";

            this.lblSubtitle.AutoSize = true;
            this.lblSubtitle.BackColor = Color.Transparent;
            this.lblSubtitle.Font = new Font("Segoe UI", 10F);
            this.lblSubtitle.ForeColor = Color.FromArgb(100, 116, 139);
            this.lblSubtitle.Location = new Point(36, 74);
            this.lblSubtitle.Text = "Nhập thông tin máy tính và cấu hình sử dụng";

            this.lblComputerName.AutoSize = true;
            this.lblComputerName.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            this.lblComputerName.ForeColor = Color.FromArgb(51, 65, 85);
            this.lblComputerName.Location = new Point(34, 116);
            this.lblComputerName.Text = "Tên máy";

            this.txtComputerName.BorderStyle = BorderStyle.FixedSingle;
            this.txtComputerName.Font = new Font("Segoe UI", 10F);
            this.txtComputerName.ForeColor = Color.FromArgb(15, 23, 42);
            this.txtComputerName.Location = new Point(34, 146);
            this.txtComputerName.Size = new Size(552, 30);

            this.lblRoom.AutoSize = true;
            this.lblRoom.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            this.lblRoom.ForeColor = Color.FromArgb(51, 65, 85);
            this.lblRoom.Location = new Point(34, 204);
            this.lblRoom.Text = "Phòng máy";

            this.cboRoom.BackColor = Color.FromArgb(248, 250, 252);
            this.cboRoom.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cboRoom.FlatStyle = FlatStyle.Flat;
            this.cboRoom.Font = new Font("Segoe UI", 10F);
            this.cboRoom.Location = new Point(34, 234);
            this.cboRoom.Size = new Size(260, 30);

            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            this.lblStatus.ForeColor = Color.FromArgb(51, 65, 85);
            this.lblStatus.Location = new Point(326, 204);
            this.lblStatus.Text = "Tình trạng";

            this.cboStatus.BackColor = Color.FromArgb(248, 250, 252);
            this.cboStatus.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cboStatus.FlatStyle = FlatStyle.Flat;
            this.cboStatus.Font = new Font("Segoe UI", 10F);
            this.cboStatus.Location = new Point(326, 234);
            this.cboStatus.Size = new Size(260, 30);
            this.cboStatus.Items.AddRange(new object[] { "Tốt", "Đang bảo trì", "Hỏng" });
            this.cboStatus.SelectedIndex = 0;

            this.lblRam.AutoSize = true;
            this.lblRam.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            this.lblRam.ForeColor = Color.FromArgb(51, 65, 85);
            this.lblRam.Location = new Point(34, 292);
            this.lblRam.Text = "RAM";

            this.cboRam.BackColor = Color.FromArgb(248, 250, 252);
            this.cboRam.DropDownStyle = ComboBoxStyle.DropDown;
            this.cboRam.FlatStyle = FlatStyle.Flat;
            this.cboRam.Font = new Font("Segoe UI", 10F);
            this.cboRam.Location = new Point(34, 322);
            this.cboRam.Size = new Size(160, 30);

            this.lblCpu.AutoSize = true;
            this.lblCpu.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            this.lblCpu.ForeColor = Color.FromArgb(51, 65, 85);
            this.lblCpu.Location = new Point(214, 292);
            this.lblCpu.Text = "Bộ CPU";

            this.cboCpu.BackColor = Color.FromArgb(248, 250, 252);
            this.cboCpu.DropDownStyle = ComboBoxStyle.DropDown;
            this.cboCpu.FlatStyle = FlatStyle.Flat;
            this.cboCpu.Font = new Font("Segoe UI", 10F);
            this.cboCpu.Location = new Point(214, 322);
            this.cboCpu.Size = new Size(372, 30);

            this.lblMonitor.AutoSize = true;
            this.lblMonitor.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            this.lblMonitor.ForeColor = Color.FromArgb(51, 65, 85);
            this.lblMonitor.Location = new Point(34, 380);
            this.lblMonitor.Text = "Màn hình";

            this.cboMonitor.BackColor = Color.FromArgb(248, 250, 252);
            this.cboMonitor.DropDownStyle = ComboBoxStyle.DropDown;
            this.cboMonitor.FlatStyle = FlatStyle.Flat;
            this.cboMonitor.Font = new Font("Segoe UI", 10F);
            this.cboMonitor.Location = new Point(34, 410);
            this.cboMonitor.Size = new Size(260, 30);

            this.lblOs.AutoSize = true;
            this.lblOs.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            this.lblOs.ForeColor = Color.FromArgb(51, 65, 85);
            this.lblOs.Location = new Point(326, 380);
            this.lblOs.Text = "Hệ điều hành";

            this.cboOs.BackColor = Color.FromArgb(248, 250, 252);
            this.cboOs.DropDownStyle = ComboBoxStyle.DropDown;
            this.cboOs.FlatStyle = FlatStyle.Flat;
            this.cboOs.Font = new Font("Segoe UI", 10F);
            this.cboOs.Location = new Point(326, 410);
            this.cboOs.Size = new Size(260, 30);

            this.btnCancel.BackColor = Color.White;
            this.btnCancel.Cursor = Cursors.Hand;
            this.btnCancel.FlatAppearance.BorderColor = Color.FromArgb(226, 232, 240);
            this.btnCancel.FlatAppearance.BorderSize = 1;
            this.btnCancel.FlatStyle = FlatStyle.Flat;
            this.btnCancel.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.btnCancel.ForeColor = Color.FromArgb(71, 85, 105);
            this.btnCancel.Location = new Point(350, 464);
            this.btnCancel.Size = new Size(112, 42);
            this.btnCancel.Text = "Hủy";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += BtnCancel_Click;

            this.btnSave.BackColor = Color.FromArgb(37, 99, 235);
            this.btnSave.Cursor = Cursors.Hand;
            this.btnSave.FlatAppearance.BorderColor = Color.FromArgb(37, 99, 235);
            this.btnSave.FlatAppearance.BorderSize = 1;
            this.btnSave.FlatStyle = FlatStyle.Flat;
            this.btnSave.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.btnSave.ForeColor = Color.White;
            this.btnSave.Location = new Point(462, 464);
            this.btnSave.Size = new Size(124, 42);
            this.btnSave.Text = "Lưu máy";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += BtnSave_Click;

            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.lblSubtitle);
            this.Controls.Add(this.lblComputerName);
            this.Controls.Add(this.txtComputerName);
            this.Controls.Add(this.lblRoom);
            this.Controls.Add(this.cboRoom);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.cboStatus);
            this.Controls.Add(this.lblRam);
            this.Controls.Add(this.cboRam);
            this.Controls.Add(this.lblCpu);
            this.Controls.Add(this.cboCpu);
            this.Controls.Add(this.lblMonitor);
            this.Controls.Add(this.cboMonitor);
            this.Controls.Add(this.lblOs);
            this.Controls.Add(this.cboOs);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void ApplyDialogModeText()
        {
            if (!editMode)
            {
                return;
            }

            Text = "Sửa máy tính";
            lblTitle.Text = "Sửa máy tính";
            btnSave.Text = "Lưu thay đổi";
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void FrmMayDialog_Load(object sender, EventArgs e)
        {
            LoadLookupCombo(cboRoom, repository.GetRoomLookup());
            LoadLookupCombo(cboRam, repository.GetRamLookup());
            LoadLookupCombo(cboCpu, repository.GetCpuLookup());
            LoadLookupCombo(cboMonitor, repository.GetMonitorLookup());
            LoadLookupCombo(cboOs, repository.GetOperatingSystemLookup());

            if (editMode)
            {
                txtComputerName.Text = originalItem.TenMay ?? string.Empty;
                SelectLookupValue(cboRoom, originalItem.MaPhong.ToString(), null);
                cboStatus.SelectedItem = string.IsNullOrWhiteSpace(originalItem.TinhTrang) ? "Tốt" : originalItem.TinhTrang;
                SelectLookupValue(cboRam, ToId(originalItem.MaRam), originalItem.TenRam);
                SelectLookupValue(cboCpu, ToId(originalItem.MaCPU), originalItem.TenCPU);
                SelectLookupValue(cboMonitor, ToId(originalItem.MaManHinh), originalItem.TenManHinh);
                SelectLookupValue(cboOs, ToId(originalItem.MaHDH), originalItem.TenHDH);
            }
        }

        private static string ToId(int? value)
        {
            return value.HasValue ? value.Value.ToString() : string.Empty;
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                ComputerData = new ComputerEditItem
                {
                    MaMay = editMode ? originalItem.MaMay : 0,
                    TenMay = Clean(txtComputerName.Text),
                    MaPhong = GetSelectedRoomId(),
                    TinhTrang = Clean(Convert.ToString(cboStatus.SelectedItem)),
                    TenRam = Clean(cboRam.Text),
                    TenCPU = Clean(cboCpu.Text),
                    TenManHinh = Clean(cboMonitor.Text),
                    TenHDH = Clean(cboOs.Text)
                };

                ValidateData(ComputerData);
                DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Dữ liệu chưa hợp lệ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private int GetSelectedRoomId()
        {
            string value = Convert.ToString(cboRoom.SelectedValue);
            int roomId;
            if (!int.TryParse(value, out roomId))
            {
                throw new ArgumentException("Vui lòng chọn phòng máy.");
            }

            return roomId;
        }

        private static void ValidateData(ComputerEditItem item)
        {
            Require(item.TenMay, "Tên máy");
            Require(item.TinhTrang, "Tình trạng");
            Require(item.TenRam, "RAM");
            Require(item.TenCPU, "Bộ CPU");
            Require(item.TenManHinh, "Màn hình");
            Require(item.TenHDH, "Hệ điều hành");
        }

        private static void Require(string value, string fieldName)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException(fieldName + " không được để trống.");
            }
        }

        private static string Clean(string value)
        {
            return (value ?? string.Empty).Trim();
        }

        private static void LoadLookupCombo(ComboBox combo, List<ComputerLookupItem> items)
        {
            combo.DisplayMember = "Name";
            combo.ValueMember = "Id";
            combo.DataSource = items;
        }

        private static void SelectLookupValue(ComboBox combo, string id, string fallbackText)
        {
            if (!string.IsNullOrWhiteSpace(id))
            {
                combo.SelectedValue = id;
                if (Convert.ToString(combo.SelectedValue) == id)
                {
                    return;
                }
            }

            combo.Text = fallbackText ?? string.Empty;
        }
    }
}

