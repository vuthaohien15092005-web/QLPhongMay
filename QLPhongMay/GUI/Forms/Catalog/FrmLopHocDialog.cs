using System;
using System.Drawing;
using System.Windows.Forms;
using QLPhongMay.Models;

namespace QLPhongMay.GUI.Forms.Catalog
{
    public partial class FrmLopHocDialog : Form
    {
        private int maLop;

        private Panel pnlRoot;
        private Label lblTitle;
        private Label lblSubtitle;
        private Label lblTenLop;
        private TextBox txtTenLop;
        private Label lblSiSo;
        private NumericUpDown numSiSo;
        private Label lblError;
        private Button btnCancel;
        private Button btnSave;
        private System.ComponentModel.IContainer components;

        public FrmLopHocDialog()
            : this(null)
        {
        }

        public FrmLopHocDialog(LopHoc lopHoc)
        {
            this.maLop = lopHoc == null ? 0 : lopHoc.MaLop;
            InitializeComponent();

            if (lopHoc != null)
            {
                this.txtTenLop.Text = lopHoc.TenLop ?? string.Empty;
                this.numSiSo.Value = Math.Max(this.numSiSo.Minimum, Math.Min(this.numSiSo.Maximum, lopHoc.SiSo));
                this.lblTitle.Text = "Sửa lớp học";
                this.lblSubtitle.Text = "Cập nhật tên lớp và sĩ số";
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
                    MaLop = this.maLop,
                    TenLop = this.txtTenLop.Text.Trim(),
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
            this.pnlRoot = new Panel();
            this.lblTitle = new Label();
            this.lblSubtitle = new Label();
            this.lblTenLop = new Label();
            this.txtTenLop = new TextBox();
            this.lblSiSo = new Label();
            this.numSiSo = new NumericUpDown();
            this.lblError = new Label();
            this.btnCancel = new Button();
            this.btnSave = new Button();
            this.pnlRoot.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numSiSo)).BeginInit();
            this.SuspendLayout();

            this.pnlRoot.BackColor = Color.White;
            this.pnlRoot.BorderStyle = BorderStyle.FixedSingle;
            this.pnlRoot.Controls.Add(this.lblTitle);
            this.pnlRoot.Controls.Add(this.lblSubtitle);
            this.pnlRoot.Controls.Add(this.lblTenLop);
            this.pnlRoot.Controls.Add(this.txtTenLop);
            this.pnlRoot.Controls.Add(this.lblSiSo);
            this.pnlRoot.Controls.Add(this.numSiSo);
            this.pnlRoot.Controls.Add(this.lblError);
            this.pnlRoot.Controls.Add(this.btnCancel);
            this.pnlRoot.Controls.Add(this.btnSave);
            this.pnlRoot.Dock = DockStyle.Fill;

            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.lblTitle.ForeColor = Color.FromArgb(15, 23, 42);
            this.lblTitle.Location = new Point(30, 24);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Text = "Thêm lớp học";

            this.lblSubtitle.AutoSize = true;
            this.lblSubtitle.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.lblSubtitle.ForeColor = Color.FromArgb(100, 116, 139);
            this.lblSubtitle.Location = new Point(32, 62);
            this.lblSubtitle.Name = "lblSubtitle";
            this.lblSubtitle.Text = "Nhập tên lớp và sĩ số";

            this.lblTenLop.AutoSize = true;
            this.lblTenLop.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.lblTenLop.ForeColor = Color.FromArgb(51, 65, 85);
            this.lblTenLop.Location = new Point(32, 112);
            this.lblTenLop.Name = "lblTenLop";
            this.lblTenLop.Text = "Tên lớp";

            this.txtTenLop.BackColor = Color.FromArgb(248, 250, 252);
            this.txtTenLop.BorderStyle = BorderStyle.FixedSingle;
            this.txtTenLop.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.txtTenLop.ForeColor = Color.FromArgb(15, 23, 42);
            this.txtTenLop.Location = new Point(32, 140);
            this.txtTenLop.Name = "txtTenLop";
            this.txtTenLop.Size = new Size(410, 25);
            this.txtTenLop.TabIndex = 0;

            this.lblSiSo.AutoSize = true;
            this.lblSiSo.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.lblSiSo.ForeColor = Color.FromArgb(51, 65, 85);
            this.lblSiSo.Location = new Point(32, 204);
            this.lblSiSo.Name = "lblSiSo";
            this.lblSiSo.Text = "Sĩ số";

            this.numSiSo.BackColor = Color.FromArgb(248, 250, 252);
            this.numSiSo.BorderStyle = BorderStyle.FixedSingle;
            this.numSiSo.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.numSiSo.ForeColor = Color.FromArgb(15, 23, 42);
            this.numSiSo.Location = new Point(32, 232);
            this.numSiSo.Maximum = 300;
            this.numSiSo.Minimum = 1;
            this.numSiSo.Name = "numSiSo";
            this.numSiSo.Size = new Size(410, 25);
            this.numSiSo.TabIndex = 1;
            this.numSiSo.Value = 1;

            this.lblError.AutoSize = false;
            this.lblError.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.lblError.ForeColor = Color.FromArgb(220, 38, 38);
            this.lblError.Location = new Point(32, 278);
            this.lblError.Name = "lblError";
            this.lblError.Size = new Size(410, 40);
            this.lblError.Text = string.Empty;

            this.btnCancel.BackColor = Color.White;
            this.btnCancel.Cursor = Cursors.Hand;
            this.btnCancel.DialogResult = DialogResult.Cancel;
            this.btnCancel.FlatAppearance.BorderColor = Color.FromArgb(226, 232, 240);
            this.btnCancel.FlatStyle = FlatStyle.Flat;
            this.btnCancel.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.btnCancel.ForeColor = Color.FromArgb(51, 65, 85);
            this.btnCancel.Location = new Point(206, 334);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(112, 42);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Hủy";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new EventHandler(this.BtnCancel_Click);

            this.btnSave.BackColor = Color.FromArgb(37, 99, 235);
            this.btnSave.Cursor = Cursors.Hand;
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.FlatStyle = FlatStyle.Flat;
            this.btnSave.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.btnSave.ForeColor = Color.White;
            this.btnSave.Location = new Point(330, 334);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new Size(112, 42);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "Thêm";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new EventHandler(this.BtnSave_Click);

            this.AcceptButton = this.btnSave;
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = Color.White;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new Size(476, 408);
            this.Controls.Add(this.pnlRoot);
            this.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
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
            if (string.IsNullOrWhiteSpace(this.txtTenLop.Text))
            {
                return ShowValidationError("Vui lòng nhập tên lớp.", this.txtTenLop);
            }

            if (this.numSiSo.Value <= 0)
            {
                return ShowValidationError("Sĩ số phải lớn hơn 0.", this.numSiSo);
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
