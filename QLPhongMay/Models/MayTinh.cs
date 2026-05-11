using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using QLPhongMay.Enums;

namespace QLPhongMay.Models
{
    [Table("May")]
    public class MayTinh
    {
        [Key]
        [Column("maMay")]
        [StringLength(50)]
        public string MaMay { get; set; }

        [Column("tenMay")]
        public string TenMay { get; set; }

        [Column("tinhTrang")]
        public string TinhTrang { get; set; }

        [NotMapped]
        public ComputerStatus? TinhTrangEnum
        {
            get
            {
                ComputerStatus value;
                return Enum.TryParse(TinhTrang, true, out value) ? value : (ComputerStatus?)null;
            }
            set { TinhTrang = value.HasValue ? value.Value.ToString() : null; }
        }

        [Column("maPhong")]
        [StringLength(50)]
        public string MaPhong { get; set; }

        [Column("maCauHinh")]
        [StringLength(50)]
        public string MaCauHinh { get; set; }

        [Column("createdAt")]
        public DateTime CreatedAt { get; set; }

        [Column("updatedAt")]
        public DateTime UpdatedAt { get; set; }

        [ForeignKey("MaPhong")]
        public virtual PhongMay PhongMay { get; set; }

        [ForeignKey("MaCauHinh")]
        public virtual CauHinh CauHinh { get; set; }
    }
}
