using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using Dapper;
using QLPhongMay.DTO;

namespace QLPhongMay.DAL
{
    public class ConfigurationRepository
    {
        private readonly string connectionString;

        public ConfigurationRepository()
            : this(ConfigurationManager.ConnectionStrings["QLPhongMayDbContext"].ConnectionString)
        {
        }

        public ConfigurationRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<ConfigLookupItem> GetItems(ConfigCategory category)
        {
            EnsureLookupTables();
            ConfigTable table = GetTable(category);
            string sql = string.Format(
                "SELECT {0} AS Id, {1} AS Name FROM {2} ORDER BY {1}, {0};",
                table.IdColumn,
                table.NameColumn,
                table.TableName);

            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                return connection.Query<ConfigLookupItem>(sql).AsList();
            }
        }

        public void AddItem(ConfigCategory category, string name)
        {
            EnsureLookupTables();
            ConfigTable table = GetTable(category);
            string normalized = NormalizeName(name, table.DisplayName);
            string sql = string.Format(
                "INSERT INTO {0} ({1}) VALUES (@Name);",
                table.TableName,
                table.NameColumn);

            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                connection.Execute(sql, new { Name = normalized });
            }
        }

        public void UpdateItem(ConfigCategory category, int id, string name)
        {
            EnsureLookupTables();
            ConfigTable table = GetTable(category);
            string normalized = NormalizeName(name, table.DisplayName);
            string sql = string.Format(
                "UPDATE {0} SET {1} = @Name WHERE {2} = @Id;",
                table.TableName,
                table.NameColumn,
                table.IdColumn);

            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                connection.Execute(sql, new { Id = id, Name = normalized });
            }
        }

        public void DeleteItem(ConfigCategory category, int id)
        {
            EnsureLookupTables();
            ConfigTable table = GetTable(category);
            string sql = string.Format(
                "DELETE FROM {0} WHERE {1} = @Id;",
                table.TableName,
                table.IdColumn);

            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                connection.Execute(sql, new { Id = id });
            }
        }

        private static string NormalizeName(string name, string displayName)
        {
            string normalized = (name ?? string.Empty).Trim();
            if (string.IsNullOrWhiteSpace(normalized))
            {
                throw new ArgumentException(displayName + " không được để trống.");
            }

            return normalized;
        }

        private void EnsureLookupTables()
        {
            const string sql = @"
IF OBJECT_ID(N'Ram', N'U') IS NULL
BEGIN
    CREATE TABLE Ram (
        maRam INT IDENTITY(1,1) PRIMARY KEY,
        tenRam NVARCHAR(50) NOT NULL
    );
END;

IF OBJECT_ID(N'BoCPU', N'U') IS NULL
BEGIN
    CREATE TABLE BoCPU (
        maCPU INT IDENTITY(1,1) PRIMARY KEY,
        tenCPU NVARCHAR(100) NOT NULL
    );
END;

IF OBJECT_ID(N'ManHinh', N'U') IS NULL
BEGIN
    CREATE TABLE ManHinh (
        maManHinh INT IDENTITY(1,1) PRIMARY KEY,
        tenManHinh NVARCHAR(50) NOT NULL
    );
END;

IF OBJECT_ID(N'HeDieuHanh', N'U') IS NULL
BEGIN
    CREATE TABLE HeDieuHanh (
        maHDH INT IDENTITY(1,1) PRIMARY KEY,
        tenHDH NVARCHAR(50) NOT NULL
    );
END;";

            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                connection.Execute(sql);
            }
        }

        public static ConfigTable GetTable(ConfigCategory category)
        {
            switch (category)
            {
                case ConfigCategory.Ram:
                    return new ConfigTable("Ram", "maRam", "tenRam", "RAM");
                case ConfigCategory.Monitor:
                    return new ConfigTable("ManHinh", "maManHinh", "tenManHinh", "Màn hình");
                case ConfigCategory.OperatingSystem:
                    return new ConfigTable("HeDieuHanh", "maHDH", "tenHDH", "Hệ điều hành");
                case ConfigCategory.Cpu:
                    return new ConfigTable("BoCPU", "maCPU", "tenCPU", "CPU");
                default:
                    throw new ArgumentOutOfRangeException("category");
            }
        }
    }

    public enum ConfigCategory
    {
        Ram,
        Monitor,
        OperatingSystem,
        Cpu
    }

    public class ConfigTable
    {
        public ConfigTable(string tableName, string idColumn, string nameColumn, string displayName)
        {
            this.TableName = tableName;
            this.IdColumn = idColumn;
            this.NameColumn = nameColumn;
            this.DisplayName = displayName;
        }

        public string TableName { get; private set; }

        public string IdColumn { get; private set; }

        public string NameColumn { get; private set; }

        public string DisplayName { get; private set; }
    }
}
