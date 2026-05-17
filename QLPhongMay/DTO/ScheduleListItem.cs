using System;

namespace QLPhongMay.DTO
{
    public class ScheduleListItem
    {
        public string MaLich { get; set; }
        public string TenDangNhap { get; set; }
        public string MaPhong { get; set; }
        public string TenPhong { get; set; }
        public string MaCa { get; set; }
        public string TenCa { get; set; }
        public string MaLop { get; set; }
        public string TenLop { get; set; }
        public DateTime NgayThucHanh { get; set; }
        public int SoLuongSV { get; set; }
        public int ThuTrongTuan { get; set; }
        public string TrangThai { get; set; }
    }
}
