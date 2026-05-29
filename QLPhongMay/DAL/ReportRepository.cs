using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using QLPhongMay.DTO;

namespace QLPhongMay.DAL
{
    public class ReportRepository
    {
        private readonly string connectionString;

        public ReportRepository()
            : this(GetConnectionString())
        {
        }

        public ReportRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        private static string GetConnectionString()
        {
            ConnectionStringSettings settings = ConfigurationManager.ConnectionStrings["QLPhongMayDbContext"];

            if (settings != null && !string.IsNullOrWhiteSpace(settings.ConnectionString))
            {
                return settings.ConnectionString;
            }

            return @"Data Source=.\SQLEXPRESS;Initial Catalog=QuanLyPhongMay;Integrated Security=True;Encrypt=False";
        }

        public List<ReportChartItem> GetScheduleStatistics(DateTime fromDate, DateTime toDate, string groupMode)
        {
            string labelSql;
            string orderSql;

            if (groupMode == "month")
            {
                labelSql = "FORMAT(ngayThucHanh, 'MM/yyyy')";
                orderSql = "YEAR(ngayThucHanh), MONTH(ngayThucHanh)";
            }
            else if (groupMode == "year")
            {
                labelSql = "CONVERT(nvarchar(4), YEAR(ngayThucHanh))";
                orderSql = "YEAR(ngayThucHanh)";
            }
            else
            {
                labelSql = "CONVERT(nvarchar(10), ngayThucHanh, 103)";
                orderSql = "CAST(ngayThucHanh AS date)";
            }

            string sql = @"
SELECT " + labelSql + @" AS Label, COUNT(*) AS Total
FROM LichThucHanh
WHERE ngayThucHanh >= @FromDate
  AND ngayThucHanh < DATEADD(day, 1, @ToDate)
GROUP BY " + labelSql + @", " + orderSql + @"
ORDER BY " + orderSql + ";";

            using (IDbConnection connection = new SqlConnection(this.connectionString))
            {
                return connection.Query<ReportChartItem>(sql, new { FromDate = fromDate.Date, ToDate = toDate.Date }).AsList();
            }
        }

        public List<ScheduleReportRow> GetScheduleReportRows(DateTime fromDate, DateTime toDate, string groupMode, int? roomId)
        {
            string orderSql;

            if (groupMode == "month")
            {
                orderSql = "YEAR(l.ngayThucHanh), MONTH(l.ngayThucHanh), l.ngayThucHanh";
            }
            else if (groupMode == "year")
            {
                orderSql = "YEAR(l.ngayThucHanh), l.ngayThucHanh";
            }
            else
            {
                orderSql = "l.ngayThucHanh";
            }

            string sql = @"
SELECT
    l.maLich AS MaLich,
    l.ngayThucHanh AS NgayThucHanh,
    p.tenPhong AS PhongMay,
    lop.tenLop AS LopHoc,
    ca.tenCa AS CaHoc,
    l.soLuongSV AS SoLuongSV,
    l.trangThai AS TrangThai
FROM LichThucHanh l
INNER JOIN PhongMay p ON l.maPhong = p.maPhong
INNER JOIN Lop lop ON l.maLop = lop.maLop
INNER JOIN Ca ca ON l.maCa = ca.maCa
WHERE l.ngayThucHanh >= @FromDate
  AND l.ngayThucHanh < DATEADD(day, 1, @ToDate)
  AND (@RoomId IS NULL OR l.maPhong = @RoomId)
ORDER BY " + orderSql + @", p.tenPhong, ca.tenCa;";

            using (IDbConnection connection = new SqlConnection(this.connectionString))
            {
                return connection.Query<ScheduleReportRow>(sql, new { FromDate = fromDate.Date, ToDate = toDate.Date, RoomId = roomId }).AsList();
            }
        }

        public List<ReportChartItem> GetRoomUsage(DateTime fromDate, DateTime toDate)
        {
            const string sql = @"
SELECT p.tenPhong AS Label, COUNT(l.maLich) AS Total
FROM PhongMay p
LEFT JOIN LichThucHanh l ON p.maPhong = l.maPhong
    AND l.ngayThucHanh >= @FromDate
    AND l.ngayThucHanh < DATEADD(day, 1, @ToDate)
GROUP BY p.maPhong, p.tenPhong
ORDER BY Total DESC, p.tenPhong;";

            using (IDbConnection connection = new SqlConnection(this.connectionString))
            {
                return connection.Query<ReportChartItem>(sql, new { FromDate = fromDate.Date, ToDate = toDate.Date }).AsList();
            }
        }

        public List<RoomUsageReportRow> GetRoomUsageReportRows()
        {
            const string sql = @"
SELECT
    p.maPhong AS MaPhong,
    p.tenPhong AS TenPhong,
    p.sucChua AS SucChua,
    p.trangThai AS TrangThai,
    ISNULL(may.SoMay, 0) AS SoMay,
    ISNULL(may.SoMayTot, 0) AS SoMayTot,
    ISNULL(lich.SoLich, 0) AS SoLich,
    lich.LanSuDung AS LanSuDung
FROM PhongMay p
LEFT JOIN (
    SELECT maPhong, COUNT(*) AS SoMay,
           SUM(CASE WHEN tinhTrang IN (N'Tốt', N'Hoạt động') THEN 1 ELSE 0 END) AS SoMayTot
    FROM May
    GROUP BY maPhong
) may ON p.maPhong = may.maPhong
LEFT JOIN (
    SELECT maPhong, COUNT(*) AS SoLich, MAX(ngayThucHanh) AS LanSuDung
    FROM LichThucHanh
    GROUP BY maPhong
) lich ON p.maPhong = lich.maPhong
ORDER BY SoLich DESC, p.tenPhong;";

            using (IDbConnection connection = new SqlConnection(this.connectionString))
            {
                return connection.Query<RoomUsageReportRow>(sql).AsList();
            }
        }

        public List<ReportChartItem> GetComputerStatus(int? roomId)
        {
            const string sql = @"
SELECT tinhTrang AS Label, COUNT(*) AS Total
FROM May
WHERE (@RoomId IS NULL OR maPhong = @RoomId)
GROUP BY tinhTrang
ORDER BY Total DESC, tinhTrang;";

            using (IDbConnection connection = new SqlConnection(this.connectionString))
            {
                return connection.Query<ReportChartItem>(sql, new { RoomId = roomId }).AsList();
            }
        }

        public List<ReportChartItem> GetRooms()
        {
            const string sql = @"
SELECT tenPhong AS Label, maPhong AS Total
FROM PhongMay
ORDER BY tenPhong;";

            using (IDbConnection connection = new SqlConnection(this.connectionString))
            {
                return connection.Query<ReportChartItem>(sql).AsList();
            }
        }

        public ReportSummary GetSummary(DateTime fromDate, DateTime toDate)
        {
            const string sql = @"
SELECT
    (SELECT COUNT(*) FROM LichThucHanh WHERE ngayThucHanh >= @FromDate AND ngayThucHanh < DATEADD(day, 1, @ToDate)) AS TotalSchedules,
    (SELECT COUNT(DISTINCT maPhong) FROM LichThucHanh WHERE ngayThucHanh >= @FromDate AND ngayThucHanh < DATEADD(day, 1, @ToDate)) AS UsedRooms,
    (SELECT COUNT(*) FROM May WHERE tinhTrang IN (N'Tốt', N'Hoạt động')) AS ActiveComputers;";

            using (IDbConnection connection = new SqlConnection(this.connectionString))
            {
                return connection.QuerySingle<ReportSummary>(sql, new { FromDate = fromDate.Date, ToDate = toDate.Date });
            }
        }
    }
}
