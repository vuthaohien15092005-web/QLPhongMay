using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using QLPhongMay.Enums;

namespace QLPhongMay.Models
{
    [Table("LichThucHanh")]
    public class LichThucHanh
    {
        [Key]
        [Column("maLich")]
        [StringLength(50)]
        public string MaLich { get; set; }

        [Column("tenDangNhap")]
        [StringLength(50)]
        public string TenDangNhap { get; set; }

        [Column("maPhong")]
        [StringLength(50)]
        public string MaPhong { get; set; }

        [Column("maCa")]
        public int MaCa { get; set; }

        [Column("maLop")]
        [StringLength(50)]
        public string MaLop { get; set; }

        [Column("ngayThucHanh")]
        public DateTime NgayThucHanh { get; set; }

        [Column("soLuongSV")]
        public int SoLuongSV { get; set; }

        [Column("thuTrongTuan")]
        public int ThuTrongTuan { get; set; }

        [Column("trangThai")]
        public string TrangThai { get; set; }

        [NotMapped]
        public ScheduleStatus? TrangThaiEnum
        {
            get
            {
                ScheduleStatus value;
                return Enum.TryParse(TrangThai, true, out value) ? value : (ScheduleStatus?)null;
            }
            set { TrangThai = value.HasValue ? value.Value.ToString() : null; }
        }

        [ForeignKey("TenDangNhap")]
        public virtual NguoiDung NguoiDung { get; set; }

        [ForeignKey("MaPhong")]
        public virtual PhongMay PhongMay { get; set; }

        [ForeignKey("MaCa")]
        public virtual CaHoc CaHoc { get; set; }

        [ForeignKey("MaLop")]
        public virtual LopHoc LopHoc { get; set; }
    }
}
