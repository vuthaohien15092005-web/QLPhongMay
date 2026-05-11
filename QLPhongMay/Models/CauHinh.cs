using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QLPhongMay.Models
{
    [Table("CauHinh")]
    public class CauHinh
    {
        public CauHinh()
        {
            MayTinhs = new HashSet<MayTinh>();
        }

        [Key]
        [Column("maCauHinh")]
        [StringLength(50)]
        public string MaCauHinh { get; set; }

        [Column("ram")]
        public string Ram { get; set; }

        [Column("boCPU")]
        public string BoCPU { get; set; }

        [Column("manHinh")]
        public string ManHinh { get; set; }

        [Column("heDieuHanh")]
        public string HeDieuHanh { get; set; }

        [Column("ghiChu")]
        public string GhiChu { get; set; }

        public virtual ICollection<MayTinh> MayTinhs { get; set; }
    }
}
