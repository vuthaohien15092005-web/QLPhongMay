using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QLPhongMay.Models
{
    [Table("Lop")]
    public class LopHoc
    {
        public LopHoc()
        {
            LichThucHanhs = new HashSet<LichThucHanh>();
        }

        [Key]
        [Column("maLop")]
        [StringLength(50)]
        public string MaLop { get; set; }

        [Column("tenLop")]
        public string TenLop { get; set; }

        [Column("nganh")]
        public string Nganh { get; set; }

        [Column("siSo")]
        public int SiSo { get; set; }

        public virtual ICollection<LichThucHanh> LichThucHanhs { get; set; }
    }
}
