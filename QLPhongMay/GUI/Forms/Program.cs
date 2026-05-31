using System;
using System.Windows.Forms;
using QLPhongMay.Auth;
using QLPhongMay.BLL;
using QLPhongMay.Enums;
using QLPhongMay.GUI.Forms.Dashboard;
using QLPhongMay.GUI.Forms.Reports;

namespace QLPhongMay
{
    internal static class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            if (args != null && args.Length > 0 && string.Equals(args[0], "--report", StringComparison.OrdinalIgnoreCase))
            {
                Application.Run(new frmBaoCaoThongKe());
                return;
            }

            using (FrmLogin login = new FrmLogin())
            {
                if (login.ShowDialog() != DialogResult.OK)
                {
                    return;
                }
            }

            if (!Session.IsAuthenticated)
            {
                MessageBox.Show("Phiên đăng nhập không hợp lệ.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (Session.HasRole(UserRole.Admin))
            {
                Application.Run(new frmMain_Admin());
                return;
            }

            if (Session.HasRole(UserRole.QuanLyPhongMay))
            {
                Application.Run(new frmMain_QLPM());
                return;
            }

            MessageBox.Show("Tài khoản không có quyền truy cập hệ thống.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }
}
