using System;
using System.Drawing;
using System.Windows.Forms;
using Guna.UI2.WinForms;
using QLPhongMay.Models;

namespace QLPhongMay.GUI.Forms.Catalog
{
    public partial class FrmLopHocDialog : Form
    {
        private readonly bool editMode;

        private Guna2BorderlessForm borderlessForm;
        private Guna2ShadowForm shadowForm;
        private Guna2Panel pnlRoot;
        private Guna2ControlBox btnClose;
        private Guna2HtmlLabel lblTitle;
        private Guna2HtmlLabel lblSubtitle;
        private Guna2HtmlLabel lblMaLop;
        private Guna2TextBox txtMaLop;
        private Guna2HtmlLabel lblTenLop;
        private Guna2TextBox txtTenLop;
        private Guna2HtmlLabel lblNganh;
        private Guna2TextBox txtNganh;
        private Guna2HtmlLabel lblSiSo;
        private Guna2NumericUpDown numSiSo;
        private Guna2HtmlLabel lblError;
        private Guna2Button btnCancel;
        private Guna2Button btnSave;
        private System.ComponentModel.IContainer components;

        public FrmLopHocDialog()
            : this(null)
        {
        }

        public FrmLopHocDialog(LopHoc lopHoc)
        {
            this.editMode = lopHoc != null;
            InitializeComponent();

            if (lopHoc != null)
            {
                this.txtMaLop.Text = lopHoc.MaLop ?? string.Empty;
                this.txtTenLop.Text = lopHoc.TenLop ?? string.Empty;
                this.txtNganh.Text = lopHoc.Nganh ?? string.Empty;
                this.numSiSo.Value = Math.Max(this.numSiSo.Minimum, Math.Min(this.numSiSo.Maximum, lopHoc.SiSo));
                this.txtMaLop.ReadOnly = true;
                this.txtMaLop.FillColor = Color.FromArgb(241, 245, 249);
                this.lblTitle.Text = "Sửa lớp học";
                this.lblSubtitle.Text = "Cập nhật thông tin lớp học";
                this.btnSave.Text = "Lưu";
                this.Text = "Sửa lớp học";
            }
        }

        public LopHoc LopHoc
        {
            get
            {
                return new LopHoc
                {
                    MaLop = this.txtMaLop.Text.Trim(),
                    TenLop = this.txtTenLop.Text.Trim(),
                    Nganh = this.txtNganh.Text.Trim(),
                    SiSo = Convert.ToInt32(this.numSiSo.Value)
                };
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && this.components != null)
            {
                this.components.Dispose();
            }

            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.borderlessForm = new Guna2BorderlessForm(this.components);
            this.shadowForm = new Guna2ShadowForm(this.components);
            this.pnlRoot = new Guna2Panel();
            this.btnClose = new Guna2ControlBox();
            this.lblTitle = new Guna2HtmlLabel();
            this.lblSubtitle = new Guna2HtmlLabel();
            this.lblMaLop = new Guna2HtmlLabel();
            this.txtMaLop = new Guna2TextBox();
            this.lblTenLop = new Guna2HtmlLabel();
            this.txtTenLop = new Guna2TextBox();
            this.lblNganh = new Guna2HtmlLabel();
            this.txtNganh = new Guna2TextBox();
            this.lblSiSo = new Guna2HtmlLabel();
            this.numSiSo = new Guna2NumericUpDown();
            this.lblError = new Guna2HtmlLabel();
            this.btnCancel = new Guna2Button();
            this.btnSave = new Guna2Button();
            this.pnlRoot.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numSiSo)).BeginInit();
            this.SuspendLayout();

            this.borderlessForm.BorderRadius = 18;
            this.borderlessForm.ContainerControl = this;
            this.borderlessForm.ResizeForm = false;

            this.shadowForm.BorderRadius = 18;
            this.shadowForm.TargetForm = this;

            this.pnlRoot.BorderColor = Color.FromArgb(226, 232, 240);
            this.pnlRoot.BorderRadius = 18;
            this.pnlRoot.BorderThickness = 1;
            this.pnlRoot.Controls.Add(this.btnClose);
            this.pnlRoot.Controls.Add(this.lblTitle);
            this.pnlRoot.Controls.Add(this.lblSubtitle);
            this.pnlRoot.Controls.Add(this.lblMaLop);
            this.pnlRoot.Controls.Add(this.txtMaLop);
            this.pnlRoot.Controls.Add(this.lblTenLop);
            this.pnlRoot.Controls.Add(this.txtTenLop);
            this.pnlRoot.Controls.Add(this.lblNganh);
            this.pnlRoot.Controls.Add(this.txtNganh);
            this.pnlRoot.Controls.Add(this.lblSiSo);
            this.pnlRoot.Controls.Add(this.numSiSo);
            this.pnlRoot.Controls.Add(this.lblError);
            this.pnlRoot.Controls.Add(this.btnCancel);
            this.pnlRoot.Controls.Add(this.btnSave);
            this.pnlRoot.Dock = DockStyle.Fill;
            this.pnlRoot.FillColor = Color.White;

            this.btnClose.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            this.btnClose.BorderRadius = 8;
            this.btnClose.FillColor = Color.Transparent;
            this.btnClose.HoverState.FillColor = Color.FromArgb(241, 245, 249);
            this.btnClose.IconColor = Color.FromArgb(71, 85, 105);
            this.btnClose.Location = new Point(430, 22);
            this.btnClose.Size = new Size(32, 32);

            ConfigureLabel(this.lblTitle, "Thêm lớp học", 28, 24, 18F, true);
            ConfigureLabel(this.lblSubtitle, "Nhập thông tin lớp thực hành", 30, 68, 10F, false);
            ConfigureLabel(this.lblMaLop, "Mã lớp", 30, 116, 9.5F, true);
            ConfigureTextBox(this.txtMaLop, "Ví dụ: CNTT01", 30, 144, 0);
            ConfigureLabel(this.lblTenLop, "Tên lớp", 30, 210, 9.5F, true);
            ConfigureTextBox(this.txtTenLop, "Nhập tên lớp", 30, 238, 1);
            ConfigureLabel(this.lblNganh, "Ngành", 30, 304, 9.5F, true);
            ConfigureTextBox(this.txtNganh, "Nhập ngành", 30, 332, 2);
            ConfigureLabel(this.lblSiSo, "Sĩ số", 30, 398, 9.5F, true);

            this.numSiSo.BackColor = Color.Transparent;
            this.numSiSo.BorderColor = Color.FromArgb(226, 232, 240);
            this.numSiSo.BorderRadius = 8;
            this.numSiSo.Cursor = Cursors.IBeam;
            this.numSiSo.FillColor = Color.FromArgb(248, 250, 252);
            this.numSiSo.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.numSiSo.ForeColor = Color.FromArgb(15, 23, 42);
            this.numSiSo.Location = new Point(30, 426);
            this.numSiSo.Maximum = 300;
            this.numSiSo.Minimum = 1;
            this.numSiSo.Name = "numSiSo";
            this.numSiSo.Size = new Size(430, 44);
            this.numSiSo.TabIndex = 3;
            this.numSiSo.UpDownButtonFillColor = Color.FromArgb(37, 99, 235);
            this.numSiSo.Value = 1;

            this.lblError.BackColor = Color.Transparent;
            this.lblError.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.lblError.ForeColor = Color.FromArgb(220, 38, 38);
            this.lblError.Location = new Point(31, 488);
            this.lblError.Text = string.Empty;

            this.btnCancel.Animated = true;
            this.btnCancel.BorderColor = Color.FromArgb(226, 232, 240);
            this.btnCancel.BorderRadius = 8;
            this.btnCancel.BorderThickness = 1;
            this.btnCancel.Cursor = Cursors.Hand;
            this.btnCancel.DialogResult = DialogResult.Cancel;
            this.btnCancel.FillColor = Color.White;
            this.btnCancel.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.btnCancel.ForeColor = Color.FromArgb(51, 65, 85);
            this.btnCancel.Location = new Point(224, 516);
            this.btnCancel.Size = new Size(112, 42);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "Hủy";
            this.btnCancel.Click += new EventHandler(this.BtnCancel_Click);

            this.btnSave.Animated = true;
            this.btnSave.BorderRadius = 8;
            this.btnSave.Cursor = Cursors.Hand;
            this.btnSave.FillColor = Color.FromArgb(37, 99, 235);
            this.btnSave.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.btnSave.ForeColor = Color.White;
            this.btnSave.Location = new Point(348, 516);
            this.btnSave.Size = new Size(112, 42);
            this.btnSave.TabIndex = 4;
            this.btnSave.Text = "Thêm";
            this.btnSave.Click += new EventHandler(this.BtnSave_Click);

            this.AcceptButton = this.btnSave;
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = Color.White;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new Size(490, 586);
            this.Controls.Add(this.pnlRoot);
            this.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.FormBorderStyle = FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmLopHocDialog";
            this.ShowInTaskbar = false;
            this.StartPosition = FormStartPosition.CenterParent;
            this.Text = "Thêm lớp học";
            this.pnlRoot.ResumeLayout(false);
            this.pnlRoot.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numSiSo)).EndInit();
            this.ResumeLayout(false);
        }

        private static void ConfigureLabel(Guna2HtmlLabel label, string text, int x, int y, float size, bool bold)
        {
            label.BackColor = Color.Transparent;
            label.Font = new Font("Segoe UI", size, bold ? FontStyle.Bold : FontStyle.Regular, GraphicsUnit.Point, 0);
            label.ForeColor = bold ? Color.FromArgb(51, 65, 85) : Color.FromArgb(100, 116, 139);
            label.Location = new Point(x, y);
            label.Text = text;
        }

        private static void ConfigureTextBox(Guna2TextBox textBox, string placeholder, int x, int y, int tabIndex)
        {
            textBox.BorderColor = Color.FromArgb(226, 232, 240);
            textBox.BorderRadius = 8;
            textBox.Cursor = Cursors.IBeam;
            textBox.DefaultText = string.Empty;
            textBox.FillColor = Color.FromArgb(248, 250, 252);
            textBox.FocusedState.BorderColor = Color.FromArgb(37, 99, 235);
            textBox.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            textBox.ForeColor = Color.FromArgb(15, 23, 42);
            textBox.HoverState.BorderColor = Color.FromArgb(37, 99, 235);
            textBox.Location = new Point(x, y);
            textBox.Margin = new Padding(3, 5, 3, 5);
            textBox.PlaceholderForeColor = Color.FromArgb(148, 163, 184);
            textBox.PlaceholderText = placeholder;
            textBox.SelectedText = string.Empty;
            textBox.Size = new Size(430, 44);
            textBox.TabIndex = tabIndex;
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateInput())
            {
                return;
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(this.txtMaLop.Text))
            {
                return ShowValidationError("Vui lòng nhập mã lớp.", this.txtMaLop);
            }

            if (string.IsNullOrWhiteSpace(this.txtTenLop.Text))
            {
                return ShowValidationError("Vui lòng nhập tên lớp.", this.txtTenLop);
            }

            if (string.IsNullOrWhiteSpace(this.txtNganh.Text))
            {
                return ShowValidationError("Vui lòng nhập ngành.", this.txtNganh);
            }

            this.lblError.Text = string.Empty;
            return true;
        }

        private bool ShowValidationError(string message, Control target)
        {
            this.lblError.Text = message;
            target.Focus();
            return false;
        }
    }
}
