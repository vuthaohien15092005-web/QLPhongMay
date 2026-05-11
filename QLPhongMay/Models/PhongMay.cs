using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using QLPhongMay.Enums;

namespace QLPhongMay.Models
{
    [Table("PhongMay")]
    public class PhongMay
    {
        public PhongMay()
        {
            MayTinhs = new HashSet<MayTinh>();
            LichThucHanhs = new HashSet<LichThucHanh>();
        }

        [Key]
        [Column("maPhong")]
        [StringLength(50)]
        public string MaPhong { get; set; }

        [Column("tenPhong")]
        public string TenPhong { get; set; }

        [Column("sucChua")]
        public int SucChua { get; set; }

        [Column("trangThai")]
        public string TrangThai { get; set; }

        [NotMapped]
        public RoomStatus? TrangThaiEnum
        {
            get
            {
                RoomStatus value;
                return Enum.TryParse(TrangThai, true, out value) ? value : (RoomStatus?)null;
            }
            set { TrangThai = value.HasValue ? value.Value.ToString() : null; }
        }

        [Column("createdAt")]
        public DateTime CreatedAt { get; set; }

        [Column("updatedAt")]
        public DateTime UpdatedAt { get; set; }

        public virtual ICollection<MayTinh> MayTinhs { get; set; }
        public virtual ICollection<LichThucHanh> LichThucHanhs { get; set; }
    }
}
