using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QLPhongMay.Models
{
    [Table("VaiTro")]
    public class VaiTro
    {
        public VaiTro()
        {
            NguoiDungs = new HashSet<NguoiDung>();
        }

        [Key]
        [Column("maVaiTro")]
        public int MaVaiTro { get; set; }

        [Column("tenVaiTro")]
        public string TenVaiTro { get; set; }

        public virtual ICollection<NguoiDung> NguoiDungs { get; set; }
    }
}

