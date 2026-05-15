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
                throw new ConfigurationErrorsException("Không tìm thấy connection string 'QLPhongMayDbContext' trong App.config.");
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
    [nganh] AS [Nganh],
    [siSo] AS [SiSo]
FROM [Lop]
ORDER BY [maLop];";

            using (IDbConnection connection = new SqlConnection(this.connectionString))
            {
                return connection.Query<LopHoc>(sql);
            }
        }

        public LopHoc GetById(string maLop)
        {
            const string sql = @"
SELECT TOP 1
    [maLop] AS [MaLop],
    [tenLop] AS [TenLop],
    [nganh] AS [Nganh],
    [siSo] AS [SiSo]
FROM [Lop]
WHERE [maLop] = @MaLop;";

            using (IDbConnection connection = new SqlConnection(this.connectionString))
            {
                return connection.QuerySingleOrDefault<LopHoc>(sql, new { MaLop = maLop });
            }
        }

        public bool Exists(string maLop)
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

        public void Create(LopHoc lopHoc)
        {
            const string sql = @"
INSERT INTO [Lop] ([maLop], [tenLop], [nganh], [siSo])
VALUES (@MaLop, @TenLop, @Nganh, @SiSo);";

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
    [nganh] = @Nganh,
    [siSo] = @SiSo
WHERE [maLop] = @MaLop;";

            using (IDbConnection connection = new SqlConnection(this.connectionString))
            {
                connection.Execute(sql, lopHoc);
            }
        }

        public void Delete(string maLop)
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
