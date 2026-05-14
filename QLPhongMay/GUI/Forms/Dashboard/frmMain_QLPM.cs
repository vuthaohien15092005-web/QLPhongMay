using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using Guna.UI2.WinForms;

namespace QLPhongMay.GUI.Forms.Dashboard
{
    public partial class frmMain_QLPM : Form
    {
        private Guna2Panel pnlHeader;
        private Guna2Panel pnlLogoMark;
        private Label lblLogoIcon;
        private Guna2HtmlLabel lblAppName;
        private Guna2HtmlLabel lblAppSub;
        private Guna2Panel pnlUser;
        private Label lblAvatar;
        private Guna2HtmlLabel lblUserName;
        private Guna2Button btnLogout;
        private Guna2Panel pnlContent;
        private TableLayoutPanel tblMenu;

        public frmMain_QLPM()
        {
            InitializeComponent();
            BuildMenuCards();
        }

        private void InitializeComponent()
        {
            this.pnlHeader = new Guna.UI2.WinForms.Guna2Panel();
            this.pnlLogoMark = new Guna.UI2.WinForms.Guna2Panel();
            this.lblLogoIcon = new System.Windows.Forms.Label();
            this.lblAppName = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.lblAppSub = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.pnlUser = new Guna.UI2.WinForms.Guna2Panel();
            this.lblAvatar = new System.Windows.Forms.Label();
            this.lblUserName = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.btnLogout = new Guna.UI2.WinForms.Guna2Button();
            this.pnlContent = new Guna.UI2.WinForms.Guna2Panel();
            this.tblMenu = new System.Windows.Forms.TableLayoutPanel();
            this.pnlHeader.SuspendLayout();
            this.pnlLogoMark.SuspendLayout();
            this.pnlUser.SuspendLayout();
            this.pnlContent.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.Transparent;
            this.pnlHeader.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.pnlHeader.Controls.Add(this.pnlLogoMark);
            this.pnlHeader.Controls.Add(this.lblAppName);
            this.pnlHeader.Controls.Add(this.lblAppSub);
            this.pnlHeader.Controls.Add(this.pnlUser);
            this.pnlHeader.CustomBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.pnlHeader.CustomBorderThickness = new System.Windows.Forms.Padding(0, 0, 0, 1);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.FillColor = System.Drawing.Color.White;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(1180, 84);
            this.pnlHeader.TabIndex = 0;
            // 
            // pnlLogoMark
            // 
            this.pnlLogoMark.BorderRadius = 12;
            this.pnlLogoMark.Controls.Add(this.lblLogoIcon);
            this.pnlLogoMark.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.pnlLogoMark.Location = new System.Drawing.Point(32, 20);
            this.pnlLogoMark.Name = "pnlLogoMark";
            this.pnlLogoMark.Size = new System.Drawing.Size(44, 44);
            this.pnlLogoMark.TabIndex = 0;
            // 
            // lblLogoIcon
            // 
            this.lblLogoIcon.BackColor = System.Drawing.Color.Transparent;
            this.lblLogoIcon.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblLogoIcon.Font = new System.Drawing.Font("Segoe MDL2 Assets", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLogoIcon.ForeColor = System.Drawing.Color.White;
            this.lblLogoIcon.Location = new System.Drawing.Point(0, 0);
            this.lblLogoIcon.Name = "lblLogoIcon";
            this.lblLogoIcon.Size = new System.Drawing.Size(44, 44);
            this.lblLogoIcon.TabIndex = 0;
            this.lblLogoIcon.Text = "";
            this.lblLogoIcon.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblAppName
            // 
            this.lblAppName.BackColor = System.Drawing.Color.Transparent;
            this.lblAppName.Font = new System.Drawing.Font("Segoe UI", 17F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAppName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblAppName.Location = new System.Drawing.Point(88, 14);
            this.lblAppName.Name = "lblAppName";
            this.lblAppName.Size = new System.Drawing.Size(261, 40);
            this.lblAppName.TabIndex = 1;
            this.lblAppName.Text = "Quản lý phòng máy";
            // 
            // lblAppSub
            // 
            this.lblAppSub.BackColor = System.Drawing.Color.Transparent;
            this.lblAppSub.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAppSub.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblAppSub.Location = new System.Drawing.Point(90, 52);
            this.lblAppSub.Name = "lblAppSub";
            this.lblAppSub.Size = new System.Drawing.Size(168, 22);
            this.lblAppSub.TabIndex = 2;
            this.lblAppSub.Text = "Bảng điều khiển quản lý phòng máy";
            // 
            // pnlUser
            // 
            this.pnlUser.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlUser.BackColor = System.Drawing.Color.Transparent;
            this.pnlUser.Controls.Add(this.lblAvatar);
            this.pnlUser.Controls.Add(this.lblUserName);
            this.pnlUser.Controls.Add(this.btnLogout);
            this.pnlUser.FillColor = System.Drawing.Color.Transparent;
            this.pnlUser.Location = new System.Drawing.Point(836, 14);
            this.pnlUser.Name = "pnlUser";
            this.pnlUser.Size = new System.Drawing.Size(312, 56);
            this.pnlUser.TabIndex = 3;
            // 
            // lblAvatar
            // 
            this.lblAvatar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.lblAvatar.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAvatar.ForeColor = System.Drawing.Color.White;
            this.lblAvatar.Location = new System.Drawing.Point(0, 8);
            this.lblAvatar.Name = "lblAvatar";
            this.lblAvatar.Size = new System.Drawing.Size(40, 40);
            this.lblAvatar.TabIndex = 0;
            this.lblAvatar.Text = "QL";
            this.lblAvatar.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblAvatar.Paint += new System.Windows.Forms.PaintEventHandler(this.RoundAvatar_Paint);
            // 
            // lblUserName
            // 
            this.lblUserName.BackColor = System.Drawing.Color.Transparent;
            this.lblUserName.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUserName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblUserName.Location = new System.Drawing.Point(52, 17);
            this.lblUserName.Name = "lblUserName";
            this.lblUserName.Size = new System.Drawing.Size(140, 25);
            this.lblUserName.TabIndex = 1;
            this.lblUserName.Text = "Quản lý phòng máy";
            // 
            // btnLogout
            // 
            this.btnLogout.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLogout.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.btnLogout.BorderRadius = 9;
            this.btnLogout.BorderThickness = 1;
            this.btnLogout.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLogout.FillColor = System.Drawing.Color.White;
            this.btnLogout.Font = new System.Drawing.Font("Segoe MDL2 Assets", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLogout.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.btnLogout.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.btnLogout.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.btnLogout.Location = new System.Drawing.Point(260, 8);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(42, 40);
            this.btnLogout.TabIndex = 2;
            this.btnLogout.Text = "";
            this.btnLogout.Click += new System.EventHandler(this.BtnLogout_Click);
            // 
            // pnlContent
            // 
            this.pnlContent.BackColor = System.Drawing.Color.Transparent;
            this.pnlContent.Controls.Add(this.tblMenu);
            this.pnlContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContent.FillColor = System.Drawing.Color.Transparent;
            this.pnlContent.Location = new System.Drawing.Point(0, 84);
            this.pnlContent.Name = "pnlContent";
            this.pnlContent.Padding = new System.Windows.Forms.Padding(38, 42, 38, 42);
            this.pnlContent.Size = new System.Drawing.Size(1180, 676);
            this.pnlContent.TabIndex = 1;
            // 
            // tblMenu
            // 
            this.tblMenu.BackColor = System.Drawing.Color.Transparent;
            this.tblMenu.ColumnCount = 3;
            this.tblMenu.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tblMenu.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tblMenu.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tblMenu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblMenu.Location = new System.Drawing.Point(38, 42);
            this.tblMenu.Name = "tblMenu";
            this.tblMenu.RowCount = 2;
            this.tblMenu.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblMenu.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblMenu.Size = new System.Drawing.Size(1104, 592);
            this.tblMenu.TabIndex = 0;
            // 
            // frmMain_QLPM
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(247)))), ((int)(((byte)(251)))));
            this.ClientSize = new System.Drawing.Size(1180, 760);
            this.Controls.Add(this.pnlContent);
            this.Controls.Add(this.pnlHeader);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MinimumSize = new System.Drawing.Size(1040, 680);
            this.Name = "frmMain_QLPM";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Quản lý phòng máy";
            this.Load += new System.EventHandler(this.frmMain_QLPM_Load);
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.pnlLogoMark.ResumeLayout(false);
            this.pnlUser.ResumeLayout(false);
            this.pnlUser.PerformLayout();
            this.pnlContent.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        private void BuildMenuCards()
        {
            AddMenuCard(0, 0, "\uE787", "Tạo lịch thực hành", "Xếp lịch và kiểm tra phòng trống", Color.FromArgb(37, 99, 235), false, this.OpenScheduleForm);
            AddMenuCard(0, 1, "\uE80F", "Quản lý phòng máy", "Thêm sửa xóa thông tin phòng", Color.FromArgb(14, 165, 233), false, this.OpenRoomForm);
            AddMenuCard(0, 2, "\uE950", "Quản lý máy tính", "Theo dõi máy, cấu hình và trạng thái", Color.FromArgb(20, 184, 166), false, this.OpenComputerForm);
            AddMenuCard(1, 0, "\uE7BE", "Quản lý lớp học", "Danh sách lớp tín chỉ", Color.FromArgb(22, 163, 74), false, this.OpenClassForm);
            AddMenuCard(1, 1, "\uE823", "Quản lý ca học", "Khung giờ các ca thực hành", Color.FromArgb(245, 158, 11), false, this.OpenShiftForm);
            AddMenuCard(1, 2, "\uE9D2", "Báo cáo & Thống kê", "Thống kê sử dụng phòng máy", Color.FromArgb(239, 68, 68), false, this.OpenReportForm);
        }

        private void AddMenuCard(int row, int column, string icon, string title, string description, Color accentColor, bool adminBadge, EventHandler clickHandler)
        {
            Guna2Panel card = new Guna2Panel();
            Label iconLabel = new Label();
            Guna2HtmlLabel titleLabel = new Guna2HtmlLabel();
            Guna2HtmlLabel descLabel = new Guna2HtmlLabel();

            card.BorderColor = Color.FromArgb(226, 232, 240);
            card.BorderRadius = 14;
            card.BorderThickness = 1;
            card.Cursor = Cursors.Hand;
            card.Dock = DockStyle.Fill;
            card.FillColor = Color.White;
            card.Margin = new Padding(14);
            card.Name = "card" + row + column;
            card.ShadowDecoration.Color = Color.FromArgb(28, 15, 23, 42);
            card.ShadowDecoration.Depth = 8;
            card.ShadowDecoration.Enabled = false;
            card.Click += clickHandler;
            card.MouseEnter += new EventHandler(this.Card_MouseEnter);
            card.MouseLeave += new EventHandler(this.Card_MouseLeave);

            iconLabel.BackColor = Color.Transparent;
            iconLabel.Cursor = Cursors.Hand;
            iconLabel.Font = new Font("Segoe MDL2 Assets", 36F, FontStyle.Regular, GraphicsUnit.Point, 0);
            iconLabel.ForeColor = accentColor;
            iconLabel.Location = new Point(0, 54);
            iconLabel.Name = "icon" + row + column;
            iconLabel.Size = new Size(100, 70);
            iconLabel.Text = icon;
            iconLabel.TextAlign = ContentAlignment.MiddleCenter;
            iconLabel.Click += clickHandler;
            iconLabel.MouseEnter += new EventHandler(this.CardChild_MouseEnter);
            iconLabel.MouseLeave += new EventHandler(this.CardChild_MouseLeave);

            titleLabel.BackColor = Color.Transparent;
            titleLabel.Cursor = Cursors.Hand;
            titleLabel.Font = new Font("Segoe UI", 14F, FontStyle.Bold, GraphicsUnit.Point, 0);
            titleLabel.ForeColor = Color.FromArgb(15, 23, 42);
            titleLabel.Location = new Point(0, 134);
            titleLabel.Name = "title" + row + column;
            titleLabel.Text = title;
            titleLabel.TextAlignment = ContentAlignment.MiddleCenter;
            titleLabel.Click += clickHandler;
            titleLabel.MouseEnter += new EventHandler(this.CardChild_MouseEnter);
            titleLabel.MouseLeave += new EventHandler(this.CardChild_MouseLeave);

            descLabel.BackColor = Color.Transparent;
            descLabel.Cursor = Cursors.Hand;
            descLabel.Font = new Font("Segoe UI", 9.4F, FontStyle.Regular, GraphicsUnit.Point, 0);
            descLabel.ForeColor = Color.FromArgb(100, 116, 139);
            descLabel.Location = new Point(0, 174);
            descLabel.Name = "desc" + row + column;
            descLabel.Text = description;
            descLabel.TextAlignment = ContentAlignment.MiddleCenter;
            descLabel.Click += clickHandler;
            descLabel.MouseEnter += new EventHandler(this.CardChild_MouseEnter);
            descLabel.MouseLeave += new EventHandler(this.CardChild_MouseLeave);

            card.Controls.Add(iconLabel);
            card.Controls.Add(titleLabel);
            card.Controls.Add(descLabel);

            if (adminBadge)
            {
                Guna2HtmlLabel badge = new Guna2HtmlLabel();
                badge.Anchor = AnchorStyles.Top | AnchorStyles.Right;
                badge.BackColor = Color.Transparent;
                badge.Font = new Font("Segoe UI", 8F, FontStyle.Bold, GraphicsUnit.Point, 0);
                badge.ForeColor = Color.FromArgb(37, 99, 235);
                badge.Location = new Point(0, 0);
                badge.Name = "badgeAdmin";
                badge.Text = "Admin";

                Guna2Panel badgeBox = new Guna2Panel();
                badgeBox.Anchor = AnchorStyles.Top | AnchorStyles.Right;
                badgeBox.BorderRadius = 8;
                badgeBox.Controls.Add(badge);
                badgeBox.Cursor = Cursors.Hand;
                badgeBox.FillColor = Color.FromArgb(239, 246, 255);
                badgeBox.Location = new Point(246, 18);
                badgeBox.Name = "badgeBox";
                badgeBox.Size = new Size(70, 28);
                badgeBox.Click += clickHandler;
                badgeBox.MouseEnter += new EventHandler(this.CardChild_MouseEnter);
                badgeBox.MouseLeave += new EventHandler(this.CardChild_MouseLeave);
                badge.Location = new Point(16, 4);

                card.Controls.Add(badgeBox);
            }

            card.Resize += delegate
            {
                CenterCardContent(card, iconLabel, titleLabel, descLabel);
                Control badgeBox = card.Controls["badgeBox"];
                if (badgeBox != null)
                {
                    badgeBox.Left = card.Width - badgeBox.Width - 18;
                    badgeBox.Top = 18;
                }
            };

            CenterCardContent(card, iconLabel, titleLabel, descLabel);
            Control adminBadgeBox = card.Controls["badgeBox"];
            if (adminBadgeBox != null)
            {
                adminBadgeBox.Left = card.Width - adminBadgeBox.Width - 18;
                adminBadgeBox.Top = 18;
            }
            this.tblMenu.Controls.Add(card, column, row);
        }

        private static void CenterCardContent(Control card, Control iconLabel, Guna2HtmlLabel titleLabel, Guna2HtmlLabel descLabel)
        {
            iconLabel.Left = (card.Width - iconLabel.Width) / 2;
            titleLabel.Left = (card.Width - titleLabel.Width) / 2;
            descLabel.Left = (card.Width - descLabel.Width) / 2;
        }

        private void Card_MouseEnter(object sender, EventArgs e)
        {
            Guna2Panel card = sender as Guna2Panel;
            if (card == null)
            {
                return;
            }

            card.BorderColor = Color.FromArgb(37, 99, 235);
            card.ShadowDecoration.Enabled = true;
        }

        private void Card_MouseLeave(object sender, EventArgs e)
        {
            Guna2Panel card = sender as Guna2Panel;
            if (card == null)
            {
                return;
            }

            card.BorderColor = Color.FromArgb(226, 232, 240);
            card.ShadowDecoration.Enabled = false;
        }

        private void CardChild_MouseEnter(object sender, EventArgs e)
        {
            Control child = sender as Control;
            Guna2Panel card = GetMenuCard(child);
            if (card != null)
            {
                Card_MouseEnter(card, EventArgs.Empty);
            }
        }

        private void CardChild_MouseLeave(object sender, EventArgs e)
        {
            Control child = sender as Control;
            Guna2Panel card = GetMenuCard(child);
            if (card != null)
            {
                Card_MouseLeave(card, EventArgs.Empty);
            }
        }

        private Guna2Panel GetMenuCard(Control control)
        {
            Control current = control;
            while (current != null)
            {
                if (current is Guna2Panel && current.Parent == this.tblMenu)
                {
                    return (Guna2Panel)current;
                }

                current = current.Parent;
            }

            return null;
        }

        private void OpenScheduleForm(object sender, EventArgs e)
        {
            OpenPlaceholderForm("Tạo lịch thực hành");
        }

        private void OpenRoomForm(object sender, EventArgs e)
        {
            OpenPlaceholderForm("Quản lý phòng máy");
        }

        private void OpenComputerForm(object sender, EventArgs e)
        {
            OpenPlaceholderForm("Quản lý máy tính");
        }
        private void OpenClassForm(object sender, EventArgs e)
        {
            OpenPlaceholderForm("Quản lý lớp học");
        }

        private void OpenShiftForm(object sender, EventArgs e)
        {
            OpenPlaceholderForm("Quản lý ca học");
        }

        private void OpenReportForm(object sender, EventArgs e)
        {
            OpenPlaceholderForm("Báo cáo & Thống kê");
        }

        private void OpenPlaceholderForm(string title)
        {
            using (Form form = new Form())
            {
                form.StartPosition = FormStartPosition.CenterParent;
                form.Size = new Size(900, 560);
                form.Text = title;
                form.BackColor = Color.FromArgb(245, 247, 251);
                form.ShowDialog(this);
            }
        }

        private void BtnLogout_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void RoundAvatar_Paint(object sender, PaintEventArgs e)
        {
            Label label = (Label)sender;
            using (GraphicsPath path = new GraphicsPath())
            {
                path.AddEllipse(label.ClientRectangle);
                label.Region = new Region(path);
            }
        }

        private void frmMain_QLPM_Load(object sender, EventArgs e)
        {

        }
    }
}
