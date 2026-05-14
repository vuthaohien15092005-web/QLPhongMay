using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QLPhongMay.Models
{
    [Table("NguoiDung")]
    public class NguoiDung
    {
        public NguoiDung()
        {
            LichThucHanhs = new HashSet<LichThucHanh>();
        }

        [Key]
        [Column("tenDangNhap")]
        [StringLength(50)]
        public string TenDangNhap { get; set; }

        [Column("matKhau")]
        public string MatKhau { get; set; }

        [Column("hoTen")]
        public string HoTen { get; set; }

        [Column("email")]
        public string Email { get; set; }

        [Column("maVaiTro")]
        public int MaVaiTro { get; set; }

        [ForeignKey("MaVaiTro")]
        public virtual VaiTro VaiTro { get; set; }

        public virtual ICollection<LichThucHanh> LichThucHanhs { get; set; }
    }
}

