using System;

namespace QLPhongMay.DTO
{
    public class RoomUsageReportRow
    {
        public int MaPhong { get; set; }

        public string TenPhong { get; set; }

        public int SucChua { get; set; }

        public string TrangThai { get; set; }

        public int SoMay { get; set; }

        public int SoMayTot { get; set; }

        public int SoLich { get; set; }

        public DateTime? LanSuDung { get; set; }

        public string LanSuDungText
        {
            get
            {
                return LanSuDung.HasValue ? LanSuDung.Value.ToString("dd/MM/yyyy") : "Chưa sử dụng";
            }
        }
    }
}
