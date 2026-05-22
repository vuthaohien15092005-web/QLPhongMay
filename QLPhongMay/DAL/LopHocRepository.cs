using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using QLPhongMay.Models;

namespace QLPhongMay.DAL
{
    public class LopHocRepository
    {
        private readonly string connectionString;

        public LopHocRepository()
            : this(GetConfiguredConnectionString())
        {
        }

        public LopHocRepository(string connectionString)
        {
            this.connectionString = NormalizeConnectionString(connectionString);
        }

        private static string GetConfiguredConnectionString()
        {
            ConnectionStringSettings settings = ConfigurationManager.ConnectionStrings["QLPhongMayDbContext"];
            if (settings == null || string.IsNullOrWhiteSpace(settings.ConnectionString))
            {
                throw new ConfigurationErrorsException("KhÃ´ng tÃ¬m tháº¥y connection string 'QLPhongMayDbContext' trong App.config.");
            }

            return settings.ConnectionString;
        }

        private static string NormalizeConnectionString(string connectionString)
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(connectionString);
            if (builder.ConnectTimeout == 0 || builder.ConnectTimeout > 5)
            {
                builder.ConnectTimeout = 5;
            }

            return builder.ConnectionString;
        }

        public IEnumerable<LopHoc> GetAll()
        {
            const string sql = @"
SELECT
    [maLop] AS [MaLop],
    [tenLop] AS [TenLop],
    [siSo] AS [SiSo]
FROM [Lop]
ORDER BY [maLop];";

            using (IDbConnection connection = new SqlConnection(this.connectionString))
            {
                return connection.Query<LopHoc>(sql);
            }
        }

        public LopHoc GetById(int maLop)
        {
            const string sql = @"
SELECT TOP 1
    [maLop] AS [MaLop],
    [tenLop] AS [TenLop],
    [siSo] AS [SiSo]
FROM [Lop]
WHERE [maLop] = @MaLop;";

            using (IDbConnection connection = new SqlConnection(this.connectionString))
            {
                return connection.QuerySingleOrDefault<LopHoc>(sql, new { MaLop = maLop });
            }
        }

        public bool Exists(int maLop)
        {
            const string sql = @"
SELECT COUNT(1)
FROM [Lop]
WHERE [maLop] = @MaLop;";

            using (IDbConnection connection = new SqlConnection(this.connectionString))
            {
                return connection.ExecuteScalar<int>(sql, new { MaLop = maLop }) > 0;
            }
        }

        public bool ExistsByTenLop(string tenLop, int? excludedMaLop = null)
        {
            const string sql = @"
SELECT COUNT(1)
FROM [Lop]
WHERE LOWER(LTRIM(RTRIM([tenLop]))) = LOWER(LTRIM(RTRIM(@TenLop)))
  AND (@ExcludedMaLop IS NULL OR [maLop] <> @ExcludedMaLop);";

            using (IDbConnection connection = new SqlConnection(this.connectionString))
            {
                return connection.ExecuteScalar<int>(sql, new { TenLop = tenLop, ExcludedMaLop = excludedMaLop }) > 0;
            }
        }

        public void Create(LopHoc lopHoc)
        {
            const string sql = @"
INSERT INTO [Lop] ([tenLop], [siSo])
VALUES (@TenLop, @SiSo);";

            using (IDbConnection connection = new SqlConnection(this.connectionString))
            {
                connection.Execute(sql, lopHoc);
            }
        }

        public void Update(LopHoc lopHoc)
        {
            const string sql = @"
UPDATE [Lop]
SET [tenLop] = @TenLop,
    [siSo] = @SiSo
WHERE [maLop] = @MaLop;";

            using (IDbConnection connection = new SqlConnection(this.connectionString))
            {
                connection.Execute(sql, lopHoc);
            }
        }

        public void Delete(int maLop)
        {
            const string sql = @"
DELETE FROM [Lop]
WHERE [maLop] = @MaLop;";

            using (IDbConnection connection = new SqlConnection(this.connectionString))
            {
                connection.Execute(sql, new { MaLop = maLop });
            }
        }
    }
}
