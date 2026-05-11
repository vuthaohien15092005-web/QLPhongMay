using System.Data.Entity;
using QLPhongMay.Models;

namespace QLPhongMay.DAL
{
    public class AppDbContext : DbContext
    {
        public AppDbContext()
            : base("name=QLPhongMayDbContext")
        {
            Database.SetInitializer<AppDbContext>(null);
        }

        public DbSet<NguoiDung> NguoiDungs { get; set; }
        public DbSet<VaiTro> VaiTros { get; set; }
        public DbSet<PhongMay> PhongMays { get; set; }
        public DbSet<MayTinh> MayTinhs { get; set; }
        public DbSet<CauHinh> CauHinhs { get; set; }
        public DbSet<CaHoc> CaHocs { get; set; }
        public DbSet<LopHoc> LopHocs { get; set; }
        public DbSet<LichThucHanh> LichThucHanhs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
