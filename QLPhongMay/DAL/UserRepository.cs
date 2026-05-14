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
    nd.[tenDangNhap] AS [TenDangNhap],
    nd.[matKhau] AS [MatKhau],
    nd.[hoTen] AS [HoTen],
    nd.[email] AS [Email],
    CONVERT(nvarchar(50), nd.[maVaiTro]) AS [MaVaiTro],
    vt.[tenVaiTro] AS [TenVaiTro]
FROM [NguoiDung] nd
LEFT JOIN [VaiTro] vt ON nd.[maVaiTro] = vt.[maVaiTro]
WHERE nd.[tenDangNhap] = @Username";

            using (IDbConnection connection = new SqlConnection(this.connectionString))
            {
                return connection.QuerySingleOrDefault<User>(sql, new { Username = username });
            }
        }

        public AccountListItem GetAccountByUsername(string username)
        {
            const string sql = @"
SELECT TOP 1
    nd.[tenDangNhap] AS [TenDangNhap],
    nd.[hoTen] AS [HoTen],
    nd.[email] AS [Email],
    nd.[matKhau] AS [MatKhau],
    nd.[maVaiTro] AS [MaVaiTro],
    vt.[tenVaiTro] AS [TenVaiTro]
FROM [NguoiDung] nd
LEFT JOIN [VaiTro] vt ON nd.[maVaiTro] = vt.[maVaiTro]
WHERE nd.[tenDangNhap] = @Username;";

            using (IDbConnection connection = new SqlConnection(this.connectionString))
            {
                return connection.QuerySingleOrDefault<AccountListItem>(sql, new { Username = username });
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

        public bool UsernameExists(string username)
        {
            const string sql = @"
SELECT COUNT(1)
FROM [NguoiDung]
WHERE [tenDangNhap] = @Username;";

            using (IDbConnection connection = new SqlConnection(this.connectionString))
            {
                return connection.ExecuteScalar<int>(sql, new { Username = username }) > 0;
            }
        }

        public bool EmailExists(string email)
        {
            const string sql = @"
SELECT COUNT(1)
FROM [NguoiDung]
WHERE [email] = @Email;";

            using (IDbConnection connection = new SqlConnection(this.connectionString))
            {
                return connection.ExecuteScalar<int>(sql, new { Email = email }) > 0;
            }
        }

        public void CreateAccount(string username, string passwordHash, string fullName, string email, int roleId)
        {
            const string sql = @"
INSERT INTO [NguoiDung] ([tenDangNhap], [matKhau], [hoTen], [email], [maVaiTro])
VALUES (@Username, @PasswordHash, @FullName, @Email, @RoleId);";

            using (IDbConnection connection = new SqlConnection(this.connectionString))
            {
                connection.Execute(sql, new
                {
                    Username = username,
                    PasswordHash = passwordHash,
                    FullName = fullName,
                    Email = email,
                    RoleId = roleId
                });
            }
        }

        public void UpdateAccount(string username, string fullName, string email, int roleId, string passwordHash)
        {
            const string sqlWithoutPassword = @"
UPDATE [NguoiDung]
SET [hoTen] = @FullName,
    [email] = @Email,
    [maVaiTro] = @RoleId
WHERE [tenDangNhap] = @Username;";

            const string sqlWithPassword = @"
UPDATE [NguoiDung]
SET [hoTen] = @FullName,
    [email] = @Email,
    [maVaiTro] = @RoleId,
    [matKhau] = @PasswordHash
WHERE [tenDangNhap] = @Username;";

            using (IDbConnection connection = new SqlConnection(this.connectionString))
            {
                connection.Execute(
                    string.IsNullOrEmpty(passwordHash) ? sqlWithoutPassword : sqlWithPassword,
                    new
                    {
                        Username = username,
                        FullName = fullName,
                        Email = email,
                        RoleId = roleId,
                        PasswordHash = passwordHash
                    });
            }
        }

        public void DeleteAccount(string username)
        {
            const string sql = @"
DELETE FROM [NguoiDung]
WHERE [tenDangNhap] = @Username;";

            using (IDbConnection connection = new SqlConnection(this.connectionString))
            {
                connection.Execute(sql, new { Username = username });
            }
        }
    }
}
