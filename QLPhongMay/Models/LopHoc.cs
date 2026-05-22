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
        public int MaLop { get; set; }

        [Column("tenLop")]
        public string TenLop { get; set; }

        [Column("siSo")]
        public int SiSo { get; set; }

        public virtual ICollection<LichThucHanh> LichThucHanhs { get; set; }
    }
}
