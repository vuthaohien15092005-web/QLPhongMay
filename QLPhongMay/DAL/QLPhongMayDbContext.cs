using System.Data.Entity;

namespace QLPhongMay.DAL
{
    public class QLPhongMayDbContext : DbContext
    {
        public QLPhongMayDbContext()
            : base("name=QLPhongMayDbContext")
        {
            Database.SetInitializer<QLPhongMayDbContext>(null);
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
