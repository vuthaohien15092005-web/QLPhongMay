using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Dapper;

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

        public IEnumerable<ShiftOption> GetShifts()
        {
            const string sql = @"
SELECT
    [maCa] AS [MaCa],
    [tenCa] AS [TenCa],
    [gioBatDau] AS [GioBatDau],
    [gioKetThuc] AS [GioKetThuc]
FROM [Ca]
ORDER BY [gioBatDau];";

            using (IDbConnection connection = new SqlConnection(this.connectionString))
            {
                return connection.Query<ShiftOption>(sql);
            }
        }

        public IEnumerable<ClassOption> GetClasses()
        {
            const string sql = @"
SELECT
    [maLop] AS [MaLop],
    [tenLop] AS [TenLop],
    [siSo] AS [SiSo]
FROM [Lop]
ORDER BY [tenLop];";

            using (IDbConnection connection = new SqlConnection(this.connectionString))
            {
                return connection.Query<ClassOption>(sql);
            }
        }

        public IEnumerable<AvailableRoom> GetAvailableRooms(DateTime date, int shiftId, int studentCount)
        {
            const string sql = @"
SELECT
    p.[maPhong] AS [MaPhong],
    p.[tenPhong] AS [TenPhong],
    p.[sucChua] AS [SucChua],
    p.[trangThai] AS [TrangThai],
    COUNT(m.[maMay]) AS [SoMay],
    SUM(CASE WHEN m.[tinhTrang] = N'Tốt' THEN 1 ELSE 0 END) AS [SoMayTot]
FROM [PhongMay] p
LEFT JOIN [May] m ON p.[maPhong] = m.[maPhong]
WHERE p.[sucChua] >= @StudentCount
  AND p.[trangThai] = N'Hoạt động'
  AND NOT EXISTS (
      SELECT 1
      FROM [LichThucHanh] l
      WHERE l.[maPhong] = p.[maPhong]
        AND l.[maCa] = @ShiftId
        AND l.[ngayThucHanh] = @Date
        AND l.[trangThai] <> N'Đã hủy'
  )
GROUP BY p.[maPhong], p.[tenPhong], p.[sucChua], p.[trangThai]
ORDER BY p.[sucChua], p.[tenPhong];";

            using (IDbConnection connection = new SqlConnection(this.connectionString))
            {
                return connection.Query<AvailableRoom>(
                    sql,
                    new
                    {
                        Date = date.Date,
                        ShiftId = shiftId,
                        StudentCount = studentCount
                    });
            }
        }

        public bool ScheduleExists(int roomId, int shiftId, DateTime date)
        {
            const string sql = @"
SELECT COUNT(1)
FROM [LichThucHanh]
WHERE [maPhong] = @RoomId
  AND [maCa] = @ShiftId
  AND [ngayThucHanh] = @Date
  AND [trangThai] <> N'Đã hủy';";

            using (IDbConnection connection = new SqlConnection(this.connectionString))
            {
                return connection.ExecuteScalar<int>(
                    sql,
                    new { RoomId = roomId, ShiftId = shiftId, Date = date.Date }) > 0;
            }
        }

        public void CreateSchedule(string username, int roomId, int shiftId, int classId, DateTime date, int studentCount)
        {
            const string sql = @"
INSERT INTO [LichThucHanh]
    ([tenDangNhap], [maPhong], [maCa], [maLop], [ngayThucHanh], [soLuongSV], [thuTrongTuan], [trangThai])
VALUES
    (@Username, @RoomId, @ShiftId, @ClassId, @Date, @StudentCount, @DayOfWeek, N'Đã lên lịch');";

            using (IDbConnection connection = new SqlConnection(this.connectionString))
            {
                connection.Execute(
                    sql,
                    new
                    {
                        Username = username,
                        RoomId = roomId,
                        ShiftId = shiftId,
                        ClassId = classId,
                        Date = date.Date,
                        StudentCount = studentCount,
                        DayOfWeek = GetVietnameseDayOfWeek(date)
                    });
            }
        }

        private static int GetVietnameseDayOfWeek(DateTime date)
        {
            return date.DayOfWeek == DayOfWeek.Sunday ? 8 : ((int)date.DayOfWeek + 1);
        }

        public class ShiftOption
        {
            public int MaCa { get; set; }

            public string TenCa { get; set; }

            public TimeSpan GioBatDau { get; set; }

            public TimeSpan GioKetThuc { get; set; }

            public string DisplayName
            {
                get
                {
                    return string.Format("{0} ({1:hh\\:mm} - {2:hh\\:mm})", this.TenCa, this.GioBatDau, this.GioKetThuc);
                }
            }
        }

        public class ClassOption
        {
            public int MaLop { get; set; }

            public string TenLop { get; set; }

            public int SiSo { get; set; }

            public string DisplayName
            {
                get { return string.Format("{0} - {1} SV", this.TenLop, this.SiSo); }
            }
        }

        public class AvailableRoom
        {
            public int MaPhong { get; set; }

            public string TenPhong { get; set; }

            public int SucChua { get; set; }

            public string TrangThai { get; set; }

            public int SoMay { get; set; }

            public int SoMayTot { get; set; }
        }
    }
}
