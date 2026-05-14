using System;
using System.Windows.Forms;
using QLPhongMay.Auth;
using QLPhongMay.BLL;
using QLPhongMay.Enums;
using QLPhongMay.GUI.Forms.Dashboard;

namespace QLPhongMay
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

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