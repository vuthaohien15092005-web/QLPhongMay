using QLPhongMay.Enums;

namespace QLPhongMay.DTO
{
    public class User
    {
        public string TenDangNhap { get; set; }
        public string MatKhau { get; set; }
        public string HoTen { get; set; }
        public string Email { get; set; }
        public string MaVaiTro { get; set; }
        public string TenVaiTro { get; set; }
        public UserRole Role { get; set; }
    }
}