using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QLPhongMay.Models
{
    [Table("Ca")]
    public class CaHoc
    {
        public CaHoc()
        {
            LichThucHanhs = new HashSet<LichThucHanh>();
        }

        [Key]
        [Column("maCa")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MaCa { get; set; }

        [Column("tenCa")]
        public string TenCa { get; set; }

        [Column("gioBatDau")]
        public TimeSpan GioBatDau { get; set; }

        [Column("gioKetThuc")]
        public TimeSpan GioKetThuc { get; set; }

        public virtual ICollection<LichThucHanh> LichThucHanhs { get; set; }
    }
}
