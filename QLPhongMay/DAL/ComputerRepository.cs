using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using Dapper;
using QLPhongMay.DTO;

namespace QLPhongMay.DAL
{
    public class ComputerRepository
    {
        private readonly string connectionString;

        public ComputerRepository()
            : this(ConfigurationManager.ConnectionStrings["QLPhongMayDbContext"].ConnectionString)
        {
        }

        public ComputerRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<ComputerListItem> GetComputerList()
        {
            const string sql = @"
SELECT
    m.maMay AS MaMay,
    m.tenMay AS TenMay,
    m.tinhTrang AS TinhTrang,
    m.maPhong AS MaPhong,
    p.tenPhong AS TenPhong,
    m.maRam AS MaRam,
    r.tenRam AS TenRam,
    m.maCPU AS MaCPU,
    cpu.tenCPU AS TenCPU,
    m.maManHinh AS MaManHinh,
    mh.tenManHinh AS TenManHinh,
    m.maHDH AS MaHDH,
    hdh.tenHDH AS TenHDH
FROM May m
INNER JOIN PhongMay p ON m.maPhong = p.maPhong
LEFT JOIN Ram r ON m.maRam = r.maRam
LEFT JOIN BoCPU cpu ON m.maCPU = cpu.maCPU
LEFT JOIN ManHinh mh ON m.maManHinh = mh.maManHinh
LEFT JOIN HeDieuHanh hdh ON m.maHDH = hdh.maHDH
ORDER BY m.maMay;";

            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                return connection.Query<ComputerListItem>(sql).AsList();
            }
        }

        public List<ComputerLookupItem> GetRoomLookup()
        {
            const string sql = "SELECT CONVERT(nvarchar(50), maPhong) AS Id, tenPhong AS Name FROM PhongMay ORDER BY tenPhong, maPhong;";
            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                return connection.Query<ComputerLookupItem>(sql).AsList();
            }
        }

        public List<ComputerLookupItem> GetRamLookup()
        {
            const string sql = "SELECT CONVERT(nvarchar(50), maRam) AS Id, tenRam AS Name FROM Ram ORDER BY tenRam, maRam;";
            return QueryLookup(sql);
        }

        public List<ComputerLookupItem> GetCpuLookup()
        {
            const string sql = "SELECT CONVERT(nvarchar(50), maCPU) AS Id, tenCPU AS Name FROM BoCPU ORDER BY tenCPU, maCPU;";
            return QueryLookup(sql);
        }

        public List<ComputerLookupItem> GetMonitorLookup()
        {
            const string sql = "SELECT CONVERT(nvarchar(50), maManHinh) AS Id, tenManHinh AS Name FROM ManHinh ORDER BY tenManHinh, maManHinh;";
            return QueryLookup(sql);
        }

        public List<ComputerLookupItem> GetOperatingSystemLookup()
        {
            const string sql = "SELECT CONVERT(nvarchar(50), maHDH) AS Id, tenHDH AS Name FROM HeDieuHanh ORDER BY tenHDH, maHDH;";
            return QueryLookup(sql);
        }

        private List<ComputerLookupItem> QueryLookup(string sql)
        {
            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                return connection.Query<ComputerLookupItem>(sql).AsList();
            }
        }


        public void CreateComputer(ComputerEditItem item)
        {
            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                connection.Open();
                using (SqlTransaction transaction = connection.BeginTransaction())
                {
                    int maRam = GetOrCreateRam(connection, transaction, item.TenRam);
                    int maCPU = GetOrCreateCpu(connection, transaction, item.TenCPU);
                    int maManHinh = GetOrCreateMonitor(connection, transaction, item.TenManHinh);
                    int maHDH = GetOrCreateOperatingSystem(connection, transaction, item.TenHDH);

                    const string sql = @"
INSERT INTO May (tenMay, tinhTrang, maPhong, maRam, maCPU, maManHinh, maHDH, createdAt, updatedAt)
VALUES (@TenMay, @TinhTrang, @MaPhong, @MaRam, @MaCPU, @MaManHinh, @MaHDH, GETDATE(), GETDATE());";

                    connection.Execute(sql, new
                    {
                        TenMay = NormalizeRequired(item.TenMay, "Tên máy"),
                        TinhTrang = NormalizeRequired(item.TinhTrang, "Tình trạng"),
                        item.MaPhong,
                        MaRam = maRam,
                        MaCPU = maCPU,
                        MaManHinh = maManHinh,
                        MaHDH = maHDH
                    }, transaction);

                    transaction.Commit();
                }
            }
        }

        public void UpdateComputer(ComputerEditItem item)
        {
            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                connection.Open();
                using (SqlTransaction transaction = connection.BeginTransaction())
                {
                    int maRam = GetOrCreateRam(connection, transaction, item.TenRam);
                    int maCPU = GetOrCreateCpu(connection, transaction, item.TenCPU);
                    int maManHinh = GetOrCreateMonitor(connection, transaction, item.TenManHinh);
                    int maHDH = GetOrCreateOperatingSystem(connection, transaction, item.TenHDH);

                    const string sql = @"
UPDATE May
SET tenMay = @TenMay,
    tinhTrang = @TinhTrang,
    maPhong = @MaPhong,
    maRam = @MaRam,
    maCPU = @MaCPU,
    maManHinh = @MaManHinh,
    maHDH = @MaHDH,
    updatedAt = GETDATE()
WHERE maMay = @MaMay;";

                    connection.Execute(sql, new
                    {
                        item.MaMay,
                        TenMay = NormalizeRequired(item.TenMay, "Tên máy"),
                        TinhTrang = NormalizeRequired(item.TinhTrang, "Tình trạng"),
                        item.MaPhong,
                        MaRam = maRam,
                        MaCPU = maCPU,
                        MaManHinh = maManHinh,
                        MaHDH = maHDH
                    }, transaction);

                    transaction.Commit();
                }
            }
        }

        private static int GetOrCreateRam(SqlConnection connection, SqlTransaction transaction, string value)
        {
            return GetOrCreateLookup(connection, transaction, "Ram", "maRam", "tenRam", value, "RAM");
        }

        private static int GetOrCreateCpu(SqlConnection connection, SqlTransaction transaction, string value)
        {
            return GetOrCreateLookup(connection, transaction, "BoCPU", "maCPU", "tenCPU", value, "Bộ CPU");
        }

        private static int GetOrCreateMonitor(SqlConnection connection, SqlTransaction transaction, string value)
        {
            return GetOrCreateLookup(connection, transaction, "ManHinh", "maManHinh", "tenManHinh", value, "Màn hình");
        }

        private static int GetOrCreateOperatingSystem(SqlConnection connection, SqlTransaction transaction, string value)
        {
            return GetOrCreateLookup(connection, transaction, "HeDieuHanh", "maHDH", "tenHDH", value, "Hệ điều hành");
        }

        private static int GetOrCreateLookup(SqlConnection connection, SqlTransaction transaction, string tableName, string idColumn, string nameColumn, string value, string fieldName)
        {
            string normalized = NormalizeRequired(value, fieldName);
            string findSql = string.Format("SELECT TOP 1 {0} FROM {1} WHERE LTRIM(RTRIM({2})) = @Name;", idColumn, tableName, nameColumn);
            int? existingId = connection.QueryFirstOrDefault<int?>(findSql, new { Name = normalized }, transaction);
            if (existingId.HasValue)
            {
                return existingId.Value;
            }

            string insertSql = string.Format("INSERT INTO {0} ({1}) VALUES (@Name); SELECT CAST(SCOPE_IDENTITY() AS int);", tableName, nameColumn);
            return connection.QuerySingle<int>(insertSql, new { Name = normalized }, transaction);
        }

        private static string NormalizeRequired(string value, string fieldName)
        {
            string normalized = (value ?? string.Empty).Trim();
            if (string.IsNullOrWhiteSpace(normalized))
            {
                throw new System.ArgumentException(fieldName + " không được để trống.");
            }

            return normalized;
        }
        public void DeleteComputer(int maMay)
        {
            const string sql = "DELETE FROM May WHERE maMay = @maMay;";
            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                connection.Execute(sql, new { maMay });
            }
        }
    }
}

