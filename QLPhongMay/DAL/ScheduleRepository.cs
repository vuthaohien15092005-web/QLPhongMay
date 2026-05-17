using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using QLPhongMay.DTO;

namespace QLPhongMay.DAL
{
    public class ScheduleRepository
    {
        private readonly string connectionString;

        public ScheduleRepository()
            : this(ConfigurationManager.ConnectionStrings["QLPhongMayDbContext"].ConnectionString)
        {
        }

        public ScheduleRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<ScheduleListItem> GetByFilter(DateTime? fromDate, DateTime? toDate, int? dayOfWeek, string roomId, string shiftId, string classId, string status)
        {
            const string sql = @"
SELECT
    CONVERT(nvarchar(50), l.maLich) AS MaLich,
    l.tenDangNhap AS TenDangNhap,
    CONVERT(nvarchar(50), l.maPhong) AS MaPhong,
    p.tenPhong AS TenPhong,
    CONVERT(nvarchar(50), l.maCa) AS MaCa,
    c.tenCa AS TenCa,
    CONVERT(nvarchar(50), l.maLop) AS MaLop,
    lop.tenLop AS TenLop,
    l.ngayThucHanh AS NgayThucHanh,
    l.soLuongSV AS SoLuongSV,
    l.thuTrongTuan AS ThuTrongTuan,
    l.trangThai AS TrangThai
FROM LichThucHanh l
INNER JOIN PhongMay p ON l.maPhong = p.maPhong
INNER JOIN Ca c ON l.maCa = c.maCa
INNER JOIN Lop lop ON l.maLop = lop.maLop
WHERE (@FromDate IS NULL OR l.ngayThucHanh >= @FromDate)
  AND (@ToDate IS NULL OR l.ngayThucHanh < DATEADD(day, 1, @ToDate))
  AND (@DayOfWeek IS NULL OR l.thuTrongTuan = @DayOfWeek)
  AND (@RoomId IS NULL OR l.maPhong = @RoomId)
  AND (@ShiftId IS NULL OR l.maCa = @ShiftId)
  AND (@ClassId IS NULL OR l.maLop = @ClassId)
  AND (@Status IS NULL OR l.trangThai = @Status)
ORDER BY l.ngayThucHanh DESC, l.maCa, p.tenPhong, lop.tenLop;";

            using (IDbConnection connection = new SqlConnection(this.connectionString))
            {
                return connection.Query<ScheduleListItem>(sql, new
                {
                    FromDate = fromDate,
                    ToDate = toDate,
                    DayOfWeek = dayOfWeek,
                    RoomId = ToNullableInt(roomId),
                    ShiftId = ToNullableInt(shiftId),
                    ClassId = ToNullableInt(classId),
                    Status = EmptyToNull(status)
                }).AsList();
            }
        }

        public ScheduleListItem GetById(string scheduleId)
        {
            const string sql = @"
SELECT TOP 1
    CONVERT(nvarchar(50), maLich) AS MaLich,
    tenDangNhap AS TenDangNhap,
    CONVERT(nvarchar(50), maPhong) AS MaPhong,
    CONVERT(nvarchar(50), maCa) AS MaCa,
    CONVERT(nvarchar(50), maLop) AS MaLop,
    ngayThucHanh AS NgayThucHanh,
    soLuongSV AS SoLuongSV,
    thuTrongTuan AS ThuTrongTuan,
    trangThai AS TrangThai
FROM LichThucHanh
WHERE maLich = @ScheduleId;";

            using (IDbConnection connection = new SqlConnection(this.connectionString))
            {
                return connection.QuerySingleOrDefault<ScheduleListItem>(sql, new { ScheduleId = ToNullableInt(scheduleId) });
            }
        }

        public bool Exists(string scheduleId)
        {
            if (!ToNullableInt(scheduleId).HasValue)
            {
                return false;
            }

            const string sql = "SELECT COUNT(1) FROM LichThucHanh WHERE maLich = @ScheduleId;";
            using (IDbConnection connection = new SqlConnection(this.connectionString))
            {
                return connection.ExecuteScalar<int>(sql, new { ScheduleId = ToNullableInt(scheduleId) }) > 0;
            }
        }

        public bool HasScheduleConflict(string scheduleId, string roomId, string shiftId, DateTime date)
        {
            const string sql = @"
SELECT COUNT(1)
FROM LichThucHanh
WHERE maPhong = @MaPhong
  AND maCa = @MaCa
  AND ngayThucHanh = @NgayThucHanh
  AND (@MaLich IS NULL OR maLich <> @MaLich);";

            using (IDbConnection connection = new SqlConnection(this.connectionString))
            {
                return connection.ExecuteScalar<int>(sql, new
                {
                    MaLich = ToNullableInt(scheduleId),
                    MaPhong = ToRequiredInt(roomId, "phòng"),
                    MaCa = ToRequiredInt(shiftId, "ca"),
                    NgayThucHanh = date.Date
                }) > 0;
            }
        }

        public void Add(ScheduleListItem item)
        {
            const string sql = @"
INSERT INTO LichThucHanh (tenDangNhap, maPhong, maCa, maLop, ngayThucHanh, soLuongSV, thuTrongTuan, trangThai)
VALUES (@TenDangNhap, @MaPhong, @MaCa, @MaLop, @NgayThucHanh, @SoLuongSV, @ThuTrongTuan, @TrangThai);";

            using (IDbConnection connection = new SqlConnection(this.connectionString))
            {
                connection.Execute(sql, new
                {
                    item.TenDangNhap,
                    MaPhong = ToRequiredInt(item.MaPhong, "phòng"),
                    MaCa = ToRequiredInt(item.MaCa, "ca"),
                    MaLop = ToRequiredInt(item.MaLop, "lớp"),
                    item.NgayThucHanh,
                    item.SoLuongSV,
                    item.ThuTrongTuan,
                    item.TrangThai
                });
            }
        }

        public void Update(ScheduleListItem item)
        {
            const string sql = @"
UPDATE LichThucHanh
SET tenDangNhap = @TenDangNhap,
    maPhong = @MaPhong,
    maCa = @MaCa,
    maLop = @MaLop,
    ngayThucHanh = @NgayThucHanh,
    soLuongSV = @SoLuongSV,
    thuTrongTuan = @ThuTrongTuan,
    trangThai = @TrangThai
WHERE maLich = @MaLich;";

            using (IDbConnection connection = new SqlConnection(this.connectionString))
            {
                connection.Execute(sql, new
                {
                    MaLich = ToRequiredInt(item.MaLich, "lịch"),
                    item.TenDangNhap,
                    MaPhong = ToRequiredInt(item.MaPhong, "phòng"),
                    MaCa = ToRequiredInt(item.MaCa, "ca"),
                    MaLop = ToRequiredInt(item.MaLop, "lớp"),
                    item.NgayThucHanh,
                    item.SoLuongSV,
                    item.ThuTrongTuan,
                    item.TrangThai
                });
            }
        }

        public void Delete(string scheduleId)
        {
            const string sql = "DELETE FROM LichThucHanh WHERE maLich = @ScheduleId;";
            using (IDbConnection connection = new SqlConnection(this.connectionString))
            {
                connection.Execute(sql, new { ScheduleId = ToRequiredInt(scheduleId, "lịch") });
            }
        }

        public List<LookupItem> GetRooms()
        {
            return QueryLookup("SELECT CONVERT(nvarchar(50), maPhong) AS Id, tenPhong AS Name FROM PhongMay ORDER BY tenPhong, maPhong;");
        }

        public List<LookupItem> GetShifts()
        {
            return QueryLookup("SELECT CONVERT(nvarchar(50), maCa) AS Id, tenCa AS Name FROM Ca ORDER BY maCa;");
        }

        public List<LookupItem> GetClasses()
        {
            return QueryLookup("SELECT CONVERT(nvarchar(50), maLop) AS Id, tenLop AS Name FROM Lop ORDER BY tenLop, maLop;");
        }

        public List<LookupItem> GetUsers()
        {
            return QueryLookup("SELECT tenDangNhap AS Id, hoTen AS Name FROM NguoiDung ORDER BY hoTen, tenDangNhap;");
        }

        private List<LookupItem> QueryLookup(string sql)
        {
            using (IDbConnection connection = new SqlConnection(this.connectionString))
            {
                return connection.Query<LookupItem>(sql).AsList();
            }
        }

        private static int ToRequiredInt(string value, string fieldName)
        {
            int result;
            if (!int.TryParse(value, out result))
            {
                throw new InvalidOperationException("Giá trị " + fieldName + " không hợp lệ.");
            }

            return result;
        }

        private static int? ToNullableInt(string value)
        {
            int result;
            return int.TryParse(value, out result) ? result : (int?)null;
        }

        private static string EmptyToNull(string value)
        {
            return string.IsNullOrWhiteSpace(value) ? null : value;
        }
    }

    public class LookupItem
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public override string ToString()
        {
            return this.Name;
        }
    }
}
