using System;

namespace QLPhongMay.DTO
{
    public class ScheduleReportRow
    {
        public int MaLich { get; set; }

        public DateTime NgayThucHanh { get; set; }

        public string PhongMay { get; set; }

        public string LopHoc { get; set; }

        public string CaHoc { get; set; }

        public int SoLuongSV { get; set; }

        public string TrangThai { get; set; }
    }
}
