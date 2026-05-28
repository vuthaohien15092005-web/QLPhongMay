using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using QLPhongMay.DAL;
using QLPhongMay.DTO;

namespace QLPhongMay.GUI.Forms.Catalog
{
    public partial class frmCauHinh : Form
    {
        private readonly ConfigurationRepository repository;
        private readonly Dictionary<TabPage, ConfigCategory> tabCategories;
        private readonly Dictionary<ConfigCategory, DataGridView> grids;
        private readonly Dictionary<ConfigCategory, string> sortColumns;
        private readonly Dictionary<ConfigCategory, bool> sortAscending;

        private Panel pnlRoot;
        private Button btnBack;
        private Label lblTitle;
        private Label lblSubtitle;
        private Button btnAdd;
        private Button btnEdit;
        private Button btnDelete;
        private TabControl tabControl;

        public frmCauHinh()
        {
            this.repository = new ConfigurationRepository();
            this.tabCategories = new Dictionary<TabPage, ConfigCategory>();
            this.grids = new Dictionary<ConfigCategory, DataGridView>();
            this.sortColumns = new Dictionary<ConfigCategory, string>();
            this.sortAscending = new Dictionary<ConfigCategory, bool>();

            InitializeComponent();
            BuildInterface();
            this.Load += FrmCauHinh_Load;
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = Color.FromArgb(246, 248, 252);
            this.ClientSize = new Size(1180, 760);
            this.Font = new Font("Segoe UI", 9F);
            this.MinimumSize = new Size(1080, 680);
            this.Name = "frmCauHinh";
            this.StartPosition = FormStartPosition.CenterParent;
            this.Text = "Quản lý cấu hình";
            this.ResumeLayout(false);
        }

        private void BuildInterface()
        {
            this.pnlRoot = new Panel();
            this.btnBack = new Button();
            this.lblTitle = new Label();
            this.lblSubtitle = new Label();
            this.btnAdd = new Button();
            this.btnEdit = new Button();
            this.btnDelete = new Button();
            this.tabControl = new TabControl();

            this.pnlRoot.SuspendLayout();

            this.pnlRoot.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            this.pnlRoot.BackColor = Color.Transparent;
            this.pnlRoot.Controls.Add(this.btnBack);
            this.pnlRoot.Controls.Add(this.lblTitle);
            this.pnlRoot.Controls.Add(this.lblSubtitle);
            this.pnlRoot.Controls.Add(this.btnAdd);
            this.pnlRoot.Controls.Add(this.btnEdit);
            this.pnlRoot.Controls.Add(this.btnDelete);
            this.pnlRoot.Controls.Add(this.tabControl);
            this.pnlRoot.Location = new Point(28, 24);
            this.pnlRoot.Name = "pnlRoot";
            this.pnlRoot.Size = new Size(1124, 708);
            this.pnlRoot.Resize += PnlRoot_Resize;

            ConfigureButton(this.btnBack, "<  Quay lại", 0, 4, 128, 46, Color.White, Color.FromArgb(51, 65, 85), Color.FromArgb(226, 232, 240));
            this.btnBack.Click += BtnBack_Click;

            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new Font("Segoe UI", 21F, FontStyle.Bold);
            this.lblTitle.ForeColor = Color.FromArgb(15, 23, 42);
            this.lblTitle.Location = new Point(146, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Text = "Quản lý cấu hình";

            this.lblSubtitle.AutoSize = true;
            this.lblSubtitle.Font = new Font("Segoe UI", 10F);
            this.lblSubtitle.ForeColor = Color.FromArgb(100, 116, 139);
            this.lblSubtitle.Location = new Point(150, 48);
            this.lblSubtitle.Name = "lblSubtitle";
            this.lblSubtitle.Text = "Quản lý danh mục RAM, màn hình, hệ điều hành và CPU";

            ConfigureButton(this.btnAdd, "+  Thêm", 600, 24, 194, 44, Color.FromArgb(37, 99, 235), Color.White, Color.FromArgb(37, 99, 235));
            ConfigureButton(this.btnEdit, "Chỉnh sửa", 812, 24, 128, 44, Color.White, Color.FromArgb(37, 99, 235), Color.FromArgb(37, 99, 235));
            ConfigureButton(this.btnDelete, "Xóa", 958, 24, 128, 44, Color.White, Color.FromArgb(220, 38, 38), Color.FromArgb(220, 38, 38));
            this.btnAdd.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            this.btnEdit.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            this.btnDelete.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            this.btnAdd.Click += BtnAdd_Click;
            this.btnEdit.Click += BtnEdit_Click;
            this.btnDelete.Click += BtnDelete_Click;

            this.tabControl.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.tabControl.Controls.Add(CreateTab("RAM", ConfigCategory.Ram));
            this.tabControl.Controls.Add(CreateTab("Màn hình", ConfigCategory.Monitor));
            this.tabControl.Controls.Add(CreateTab("Hệ điều hành", ConfigCategory.OperatingSystem));
            this.tabControl.Controls.Add(CreateTab("CPU", ConfigCategory.Cpu));
            this.tabControl.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.tabControl.Location = new Point(0, 96);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new Size(1124, 612);
            this.tabControl.SelectedIndexChanged += TabControl_SelectedIndexChanged;

            this.Controls.Add(this.pnlRoot);
            UpdateAddButtonText();
            LayoutActionButtons();

            this.pnlRoot.ResumeLayout(false);
            this.pnlRoot.PerformLayout();
        }

        private static void ConfigureButton(Button button, string text, int left, int top, int width, int height, Color backColor, Color foreColor, Color borderColor)
        {
            button.BackColor = backColor;
            button.Cursor = Cursors.Hand;
            button.FlatAppearance.BorderColor = borderColor;
            button.FlatAppearance.BorderSize = 1;
            button.FlatStyle = FlatStyle.Flat;
            button.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            button.ForeColor = foreColor;
            button.Location = new Point(left, top);
            button.Size = new Size(width, height);
            button.Text = text;
            button.TextAlign = ContentAlignment.MiddleCenter;
            button.UseVisualStyleBackColor = false;
        }

        private void LayoutActionButtons()
        {
            if (this.pnlRoot == null || this.btnAdd == null || this.btnEdit == null || this.btnDelete == null)
            {
                return;
            }

            const int rightMargin = 38;
            const int gap = 18;
            this.btnDelete.Left = this.pnlRoot.Width - rightMargin - this.btnDelete.Width;
            this.btnEdit.Left = this.btnDelete.Left - gap - this.btnEdit.Width;
            this.btnAdd.Left = this.btnEdit.Left - gap - this.btnAdd.Width;
        }

        private void PnlRoot_Resize(object sender, EventArgs e)
        {
            LayoutActionButtons();
        }

        private TabPage CreateTab(string title, ConfigCategory category)
        {
            TabPage tab = new TabPage(title);
            tab.BackColor = Color.White;
            tab.Padding = new Padding(16);

            Panel panel = new Panel();
            panel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            panel.BackColor = Color.White;
            panel.BorderStyle = BorderStyle.FixedSingle;
            panel.Location = new Point(16, 16);
            panel.Padding = new Padding(16);
            panel.Size = new Size(1060, 172);

            DataGridView grid = CreateGrid();
            grid.Tag = category;
            grid.ColumnHeaderMouseClick += Grid_ColumnHeaderMouseClick;
            panel.Controls.Add(grid);
            tab.Controls.Add(panel);

            this.tabCategories.Add(tab, category);
            this.grids.Add(category, grid);
            return tab;
        }

        private static DataGridView CreateGrid()
        {
            DataGridView grid = new DataGridView();
            DataGridViewCellStyle headerStyle = new DataGridViewCellStyle();
            DataGridViewCellStyle rowStyle = new DataGridViewCellStyle();

            grid.AllowUserToAddRows = false;
            grid.AllowUserToDeleteRows = false;
            grid.AllowUserToResizeRows = false;
            grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            grid.BackgroundColor = Color.White;
            grid.BorderStyle = BorderStyle.None;
            grid.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            grid.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            grid.ColumnHeadersHeight = 48;
            grid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            grid.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            grid.Location = new Point(16, 16);
            grid.EnableHeadersVisualStyles = false;
            grid.GridColor = Color.FromArgb(241, 245, 249);
            grid.MultiSelect = false;
            grid.ReadOnly = true;
            grid.RowHeadersVisible = false;
            grid.RowTemplate.Height = 44;
            grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grid.Size = new Size(1028, 140);

            headerStyle.BackColor = Color.FromArgb(248, 250, 252);
            headerStyle.Font = new Font("Segoe UI", 9.2F, FontStyle.Bold);
            headerStyle.ForeColor = Color.FromArgb(71, 85, 105);
            headerStyle.Padding = new Padding(10, 0, 10, 0);
            headerStyle.SelectionBackColor = Color.FromArgb(248, 250, 252);
            headerStyle.SelectionForeColor = Color.FromArgb(71, 85, 105);
            grid.ColumnHeadersDefaultCellStyle = headerStyle;

            rowStyle.BackColor = Color.White;
            rowStyle.Font = new Font("Segoe UI", 9.5F);
            rowStyle.ForeColor = Color.FromArgb(30, 41, 59);
            rowStyle.Padding = new Padding(10, 0, 10, 0);
            rowStyle.SelectionBackColor = Color.FromArgb(219, 234, 254);
            rowStyle.SelectionForeColor = Color.FromArgb(15, 23, 42);
            grid.DefaultCellStyle = rowStyle;

            return grid;
        }

        private void FrmCauHinh_Load(object sender, EventArgs e)
        {
            LoadAllTabs();
        }

        private void LoadAllTabs()
        {
            foreach (ConfigCategory category in this.grids.Keys)
            {
                LoadTab(category);
            }
        }

        private void LoadTab(ConfigCategory category)
        {
            List<ConfigLookupItem> items = this.repository.GetItems(category);
            items = ApplySort(category, items);
            DataGridView grid = this.grids[category];
            grid.DataSource = items;
            ConfigureGridColumns(grid, category);
            UpdateGridHeight(grid, items.Count);
        }

        private void ConfigureGridColumns(DataGridView grid, ConfigCategory category)
        {
            if (grid.Columns.Count == 0)
            {
                return;
            }

            ConfigTable table = ConfigurationRepository.GetTable(category);
            grid.Columns[nameof(ConfigLookupItem.Id)].HeaderText = GetHeaderText(category, nameof(ConfigLookupItem.Id), "Mã");
            grid.Columns[nameof(ConfigLookupItem.Name)].HeaderText = GetHeaderText(category, nameof(ConfigLookupItem.Name), table.DisplayName);
            grid.Columns[nameof(ConfigLookupItem.Id)].FillWeight = 80;
            grid.Columns[nameof(ConfigLookupItem.Name)].FillWeight = 420;
            grid.Columns[nameof(ConfigLookupItem.Id)].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            grid.Columns[nameof(ConfigLookupItem.Id)].SortMode = DataGridViewColumnSortMode.Programmatic;
            grid.Columns[nameof(ConfigLookupItem.Name)].SortMode = DataGridViewColumnSortMode.Programmatic;
        }

        private List<ConfigLookupItem> ApplySort(ConfigCategory category, List<ConfigLookupItem> items)
        {
            string column;
            bool ascending;
            if (!this.sortColumns.TryGetValue(category, out column))
            {
                column = nameof(ConfigLookupItem.Id);
                this.sortColumns[category] = column;
            }

            if (!this.sortAscending.TryGetValue(category, out ascending))
            {
                ascending = true;
                this.sortAscending[category] = ascending;
            }

            IEnumerable<ConfigLookupItem> sorted = column == nameof(ConfigLookupItem.Name)
                ? ascending
                    ? items.OrderBy(item => item.Name, StringComparer.CurrentCultureIgnoreCase)
                    : items.OrderByDescending(item => item.Name, StringComparer.CurrentCultureIgnoreCase)
                : ascending
                    ? items.OrderBy(item => item.Id)
                    : items.OrderByDescending(item => item.Id);

            return sorted.ToList();
        }

        private string GetHeaderText(ConfigCategory category, string columnName, string text)
        {
            string activeColumn;
            bool ascending;
            if (!this.sortColumns.TryGetValue(category, out activeColumn) || activeColumn != columnName)
            {
                return text;
            }

            if (!this.sortAscending.TryGetValue(category, out ascending))
            {
                ascending = true;
            }

            return text + (ascending ? " ↑" : " ↓");
        }

        private static void UpdateGridHeight(DataGridView grid, int rowCount)
        {
            int rowsToShow = Math.Max(2, Math.Min(rowCount, 8));
            grid.Height = grid.ColumnHeadersHeight + (rowsToShow * grid.RowTemplate.Height) + 3;
            if (grid.Parent != null)
            {
                grid.Parent.Height = grid.Height + 32;
            }

            TabPage tab = grid.Parent == null ? null : grid.Parent.Parent as TabPage;
            if (tab != null && tab.Parent != null)
            {
                tab.Parent.Height = grid.Parent.Height + 58;
            }
        }

        private void Grid_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridView grid = sender as DataGridView;
            if (grid == null || e.ColumnIndex < 0 || e.ColumnIndex >= grid.Columns.Count)
            {
                return;
            }

            ConfigCategory category = (ConfigCategory)grid.Tag;
            string column = grid.Columns[e.ColumnIndex].DataPropertyName;
            if (string.IsNullOrEmpty(column))
            {
                column = grid.Columns[e.ColumnIndex].Name;
            }

            string currentColumn;
            bool ascending;
            if (this.sortColumns.TryGetValue(category, out currentColumn) && currentColumn == column)
            {
                this.sortAscending.TryGetValue(category, out ascending);
                this.sortAscending[category] = !ascending;
            }
            else
            {
                this.sortColumns[category] = column;
                this.sortAscending[category] = true;
            }

            LoadTab(category);
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            ConfigCategory category = GetCurrentCategory();
            ConfigTable table = ConfigurationRepository.GetTable(category);
            using (ConfigValueDialog dialog = new ConfigValueDialog("Thêm " + table.DisplayName, table.DisplayName, string.Empty))
            {
                if (dialog.ShowDialog(this) != DialogResult.OK)
                {
                    return;
                }

                TryExecute(delegate
                {
                    this.repository.AddItem(category, dialog.ValueText);
                    LoadTab(category);
                }, "Đã thêm " + table.DisplayName + " thành công.", "Không thể thêm " + table.DisplayName + ".");
            }
        }

        private void BtnEdit_Click(object sender, EventArgs e)
        {
            ConfigCategory category = GetCurrentCategory();
            ConfigLookupItem item = GetSelectedItem(category);
            if (item == null)
            {
                return;
            }

            ConfigTable table = ConfigurationRepository.GetTable(category);
            using (ConfigValueDialog dialog = new ConfigValueDialog("Sửa " + table.DisplayName, table.DisplayName, item.Name))
            {
                if (dialog.ShowDialog(this) != DialogResult.OK)
                {
                    return;
                }

                TryExecute(delegate
                {
                    this.repository.UpdateItem(category, item.Id, dialog.ValueText);
                    LoadTab(category);
                }, "Đã cập nhật " + table.DisplayName + " thành công.", "Không thể cập nhật " + table.DisplayName + ".");
            }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            ConfigCategory category = GetCurrentCategory();
            ConfigLookupItem item = GetSelectedItem(category);
            if (item == null)
            {
                return;
            }

            ConfigTable table = ConfigurationRepository.GetTable(category);
            DialogResult confirm = MessageBox.Show(
                "Bạn có chắc muốn xóa '" + item.Name + "'?",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);
            if (confirm != DialogResult.Yes)
            {
                return;
            }

            TryExecute(delegate
            {
                this.repository.DeleteItem(category, item.Id);
                LoadTab(category);
            }, "Đã xóa " + table.DisplayName + " thành công.", "Không thể xóa " + table.DisplayName + ". Giá trị này có thể đang được máy tính sử dụng.");
        }

        private ConfigLookupItem GetSelectedItem(ConfigCategory category)
        {
            DataGridView grid = this.grids[category];
            if (grid.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn một dòng.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return null;
            }

            return grid.CurrentRow.DataBoundItem as ConfigLookupItem;
        }

        private ConfigCategory GetCurrentCategory()
        {
            return this.tabCategories[this.tabControl.SelectedTab];
        }

        private void TabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateAddButtonText();
        }

        private void UpdateAddButtonText()
        {
            ConfigTable table = ConfigurationRepository.GetTable(GetCurrentCategory());
            this.btnAdd.Text = "+  Thêm " + table.DisplayName;
            int textWidth = TextRenderer.MeasureText(this.btnAdd.Text, this.btnAdd.Font).Width;
            this.btnAdd.Width = Math.Max(194, textWidth + 44);
            LayoutActionButtons();
        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private static void TryExecute(Action action, string successMessage, string errorMessage)
        {
            try
            {
                action();
                MessageBox.Show(successMessage, "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(errorMessage + "\n" + ex.Message, "Lỗi dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private class ConfigValueDialog : Form
        {
            private readonly string fieldName;
            private Panel pnlRoot;
            private Label lblTitle;
            private Label lblField;
            private TextBox txtValue;
            private Button btnCancel;
            private Button btnSave;

            public ConfigValueDialog(string title, string fieldName, string value)
            {
                this.fieldName = fieldName;
                InitializeComponent(title, fieldName, value);
            }

            public string ValueText
            {
                get { return this.txtValue.Text.Trim(); }
            }

            private void InitializeComponent(string title, string label, string value)
            {
                this.pnlRoot = new Panel();
                this.lblTitle = new Label();
                this.lblField = new Label();
                this.txtValue = new TextBox();
                this.btnCancel = new Button();
                this.btnSave = new Button();

                this.pnlRoot.BackColor = Color.White;
                this.pnlRoot.BorderStyle = BorderStyle.FixedSingle;
                this.pnlRoot.Dock = DockStyle.Fill;

                this.lblTitle.AutoSize = true;
                this.lblTitle.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
                this.lblTitle.ForeColor = Color.FromArgb(15, 23, 42);
                this.lblTitle.Location = new Point(28, 24);
                this.lblTitle.Text = title;

                this.lblField.AutoSize = true;
                this.lblField.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
                this.lblField.ForeColor = Color.FromArgb(51, 65, 85);
                this.lblField.Location = new Point(30, 86);
                this.lblField.Text = label;

                this.txtValue.BackColor = Color.FromArgb(248, 250, 252);
                this.txtValue.BorderStyle = BorderStyle.FixedSingle;
                this.txtValue.Font = new Font("Segoe UI", 10F);
                this.txtValue.ForeColor = Color.FromArgb(15, 23, 42);
                this.txtValue.Location = new Point(30, 118);
                this.txtValue.Size = new Size(420, 30);
                this.txtValue.Text = value ?? string.Empty;

                ConfigureButton(this.btnCancel, "Hủy", 214, 190, 112, 40, Color.White, Color.FromArgb(51, 65, 85), Color.FromArgb(226, 232, 240));
                this.btnCancel.DialogResult = DialogResult.Cancel;

                ConfigureButton(this.btnSave, "Lưu", 338, 190, 112, 40, Color.FromArgb(37, 99, 235), Color.White, Color.FromArgb(37, 99, 235));
                this.btnSave.Click += BtnSave_Click;

                this.pnlRoot.Controls.Add(this.lblTitle);
                this.pnlRoot.Controls.Add(this.lblField);
                this.pnlRoot.Controls.Add(this.txtValue);
                this.pnlRoot.Controls.Add(this.btnCancel);
                this.pnlRoot.Controls.Add(this.btnSave);

                this.AcceptButton = this.btnSave;
                this.CancelButton = this.btnCancel;
                this.BackColor = Color.White;
                this.ClientSize = new Size(480, 258);
                this.Controls.Add(this.pnlRoot);
                this.Font = new Font("Segoe UI", 9F);
                this.FormBorderStyle = FormBorderStyle.FixedDialog;
                this.MaximizeBox = false;
                this.MinimizeBox = false;
                this.ShowInTaskbar = false;
                this.StartPosition = FormStartPosition.CenterParent;
                this.Text = title;
            }

            private void BtnSave_Click(object sender, EventArgs e)
            {
                if (string.IsNullOrWhiteSpace(this.ValueText))
                {
                    MessageBox.Show(this.fieldName + " không được để trống.", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.txtValue.Focus();
                    return;
                }

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }
    }
}
