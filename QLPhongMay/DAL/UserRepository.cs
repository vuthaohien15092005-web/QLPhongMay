using System.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using QLPhongMay.DTO;

namespace QLPhongMay.DAL
{
    public class UserRepository
    {
        private readonly string connectionString;

        public UserRepository()
            : this(ConfigurationManager.ConnectionStrings["QLPhongMayDbContext"].ConnectionString)
        {
        }

        public UserRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public User GetByUsername(string username)
        {
            const string sql = @"
SELECT TOP 1
    [tenDangNhap] AS [TenDangNhap],
    [matKhau] AS [MatKhau],
    [hoTen] AS [HoTen],
    [email] AS [Email],
    [maVaiTro] AS [MaVaiTro]
FROM [NguoiDung]
WHERE [tenDangNhap] = @Username";

            using (IDbConnection connection = new SqlConnection(this.connectionString))
            {
                return connection.QuerySingleOrDefault<User>(sql, new { Username = username });
            }
        }

        public IEnumerable<AccountListItem> GetAccountList()
        {
            const string sql = @"
SELECT
    nd.[tenDangNhap] AS [TenDangNhap],
    nd.[hoTen] AS [HoTen],
    nd.[email] AS [Email],
    nd.[maVaiTro] AS [MaVaiTro],
    vt.[tenVaiTro] AS [TenVaiTro]
FROM [NguoiDung] nd
LEFT JOIN [VaiTro] vt ON nd.[maVaiTro] = vt.[maVaiTro]
ORDER BY nd.[tenDangNhap];";

            using (IDbConnection connection = new SqlConnection(this.connectionString))
            {
                return connection.Query<AccountListItem>(sql);
            }
        }
    }
}
