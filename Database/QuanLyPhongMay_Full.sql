USE master;
GO

IF DB_ID(N'QuanLyPhongMay') IS NOT NULL
BEGIN
    ALTER DATABASE QuanLyPhongMay SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE QuanLyPhongMay;
END
GO

CREATE DATABASE QuanLyPhongMay;
GO

USE QuanLyPhongMay;
GO

CREATE TABLE VaiTro
(
    maVaiTro  INT IDENTITY(1,1) PRIMARY KEY,
    tenVaiTro NVARCHAR(50) NOT NULL
);
GO

CREATE TABLE NguoiDung
(
    tenDangNhap NVARCHAR(50) PRIMARY KEY,
    matKhau     NVARCHAR(255) NOT NULL,
    hoTen       NVARCHAR(100) NOT NULL,
    email       NVARCHAR(100) NOT NULL UNIQUE,
    maVaiTro    INT NOT NULL,
    CONSTRAINT FK_NguoiDung_VaiTro
        FOREIGN KEY (maVaiTro) REFERENCES VaiTro(maVaiTro)
);
GO

CREATE TABLE Ca
(
    maCa       INT IDENTITY(1,1) PRIMARY KEY,
    tenCa      NVARCHAR(50) NOT NULL,
    gioBatDau  TIME NOT NULL,
    gioKetThuc TIME NOT NULL
);
GO

CREATE TABLE PhongMay
(
    maPhong   INT IDENTITY(1,1) PRIMARY KEY,
    tenPhong  NVARCHAR(50) NOT NULL,
    sucChua   INT NOT NULL,
    trangThai NVARCHAR(50) NOT NULL CONSTRAINT DF_PhongMay_trangThai DEFAULT N'Hoạt động',
    createdAt DATETIME NOT NULL CONSTRAINT DF_PhongMay_createdAt DEFAULT GETDATE(),
    updatedAt DATETIME NOT NULL CONSTRAINT DF_PhongMay_updatedAt DEFAULT GETDATE()
);
GO

CREATE TABLE CauHinh
(
    maCauHinh  INT IDENTITY(1,1) PRIMARY KEY,
    ram        NVARCHAR(50) NOT NULL,
    boCPU      NVARCHAR(100) NOT NULL,
    manHinh    NVARCHAR(50) NULL,
    heDieuHanh NVARCHAR(100) NOT NULL,
    ghiChu     NVARCHAR(255) NULL
);
GO

CREATE TABLE May
(
    maMay     INT IDENTITY(1,1) PRIMARY KEY,
    tenMay    NVARCHAR(100) NOT NULL,
    tinhTrang NVARCHAR(50) NOT NULL CONSTRAINT DF_May_tinhTrang DEFAULT N'Tốt',
    maPhong   INT NOT NULL,
    maCauHinh INT NOT NULL,
    createdAt DATETIME NOT NULL CONSTRAINT DF_May_createdAt DEFAULT GETDATE(),
    updatedAt DATETIME NOT NULL CONSTRAINT DF_May_updatedAt DEFAULT GETDATE(),
    CONSTRAINT FK_May_PhongMay
        FOREIGN KEY (maPhong) REFERENCES PhongMay(maPhong),
    CONSTRAINT FK_May_CauHinh
        FOREIGN KEY (maCauHinh) REFERENCES CauHinh(maCauHinh)
);
GO

CREATE TABLE Lop
(
    maLop  INT IDENTITY(1,1) PRIMARY KEY,
    tenLop NVARCHAR(50) NOT NULL,
    siSo   INT NOT NULL
);
GO

CREATE TABLE LichThucHanh
(
    maLich       INT IDENTITY(1,1) PRIMARY KEY,
    tenDangNhap  NVARCHAR(50) NOT NULL,
    maPhong      INT NOT NULL,
    maCa         INT NOT NULL,
    maLop        INT NOT NULL,
    ngayThucHanh DATE NOT NULL,
    soLuongSV    INT NOT NULL,
    thuTrongTuan TINYINT NOT NULL,
    trangThai    NVARCHAR(50) NOT NULL,
    CONSTRAINT FK_Lich_NguoiDung
        FOREIGN KEY (tenDangNhap) REFERENCES NguoiDung(tenDangNhap),
    CONSTRAINT FK_Lich_PhongMay
        FOREIGN KEY (maPhong) REFERENCES PhongMay(maPhong),
    CONSTRAINT FK_Lich_Ca
        FOREIGN KEY (maCa) REFERENCES Ca(maCa),
    CONSTRAINT FK_Lich_Lop
        FOREIGN KEY (maLop) REFERENCES Lop(maLop),
    CONSTRAINT UQ_Lich_Phong_Ca_Ngay
        UNIQUE (maPhong, maCa, ngayThucHanh)
);
GO

INSERT INTO VaiTro (tenVaiTro)
VALUES
(N'Admin'),
(N'Quản lý phòng máy');
GO

-- Mật khẩu mẫu: 123456
-- App hiện tại vẫn hỗ trợ so sánh mật khẩu dạng thường để tiện demo.
INSERT INTO NguoiDung (tenDangNhap, matKhau, hoTen, email, maVaiTro)
VALUES
(N'admin',  N'123456', N'Nguyễn Văn Admin',   N'admin@school.edu.vn', 1),
(N'qlpm01', N'123456', N'Trần Minh Quản Lý',  N'qlpm01@school.edu.vn', 2),
(N'qlpm02', N'123456', N'Lê Thị Phòng Máy',   N'qlpm02@school.edu.vn', 2);
GO

INSERT INTO Ca (tenCa, gioBatDau, gioKetThuc)
VALUES
(N'Ca 1', '07:00', '09:00'),
(N'Ca 2', '09:15', '11:15'),
(N'Ca 3', '13:00', '15:00'),
(N'Ca 4', '15:15', '17:15');
GO

INSERT INTO PhongMay (tenPhong, sucChua, trangThai)
VALUES
(N'Phòng máy A101', 40, N'Hoạt động'),
(N'Phòng máy A102', 45, N'Hoạt động'),
(N'Phòng máy B201', 35, N'Hoạt động'),
(N'Phòng máy B202', 50, N'Bảo trì'),
(N'Phòng máy C301', 30, N'Hoạt động');
GO

INSERT INTO CauHinh (ram, boCPU, manHinh, heDieuHanh, ghiChu)
VALUES
(N'8GB',  N'Intel Core i5 Gen 10', N'22 inch', N'Windows 10', N'Phù hợp thực hành tin học cơ bản'),
(N'16GB', N'Intel Core i7 Gen 11', N'24 inch', N'Windows 11', N'Phù hợp lập trình và thiết kế'),
(N'8GB',  N'AMD Ryzen 5',          N'22 inch', N'Windows 10', N'Phù hợp học văn phòng'),
(N'32GB', N'Intel Core i7 Gen 12', N'27 inch', N'Windows 11', N'Phù hợp đồ họa và máy ảo');
GO

INSERT INTO May (tenMay, tinhTrang, maPhong, maCauHinh)
VALUES
(N'A101-PC01', N'Tốt', 1, 1),
(N'A101-PC02', N'Tốt', 1, 1),
(N'A101-PC03', N'Tốt', 1, 1),
(N'A101-PC04', N'Hỏng', 1, 1),
(N'A101-PC05', N'Tốt', 1, 1),
(N'A102-PC01', N'Tốt', 2, 2),
(N'A102-PC02', N'Tốt', 2, 2),
(N'A102-PC03', N'Tốt', 2, 2),
(N'A102-PC04', N'Đang bảo trì', 2, 2),
(N'A102-PC05', N'Tốt', 2, 2),
(N'B201-PC01', N'Tốt', 3, 3),
(N'B201-PC02', N'Tốt', 3, 3),
(N'B201-PC03', N'Hỏng', 3, 3),
(N'B201-PC04', N'Tốt', 3, 3),
(N'B201-PC05', N'Tốt', 3, 3),
(N'B202-PC01', N'Đang bảo trì', 4, 4),
(N'B202-PC02', N'Đang bảo trì', 4, 4),
(N'B202-PC03', N'Đang bảo trì', 4, 4),
(N'C301-PC01', N'Tốt', 5, 1),
(N'C301-PC02', N'Tốt', 5, 1),
(N'C301-PC03', N'Tốt', 5, 1),
(N'C301-PC04', N'Tốt', 5, 1);
GO

-- Form quản lý lớp học hiện chỉ dùng tenLop và siSo, không dùng cột ngành.
INSERT INTO Lop (tenLop, siSo)
VALUES
(N'CNTT01', 38),
(N'CNTT02', 42),
(N'KTPM01', 35),
(N'TKDH01', 30),
(N'QTM01', 32);
GO

INSERT INTO LichThucHanh
(tenDangNhap, maPhong, maCa, maLop, ngayThucHanh, soLuongSV, thuTrongTuan, trangThai)
VALUES
(N'qlpm01', 1, 1, 1, '2026-05-11', 38, 2, N'Đã lên lịch'),
(N'qlpm01', 2, 2, 2, '2026-05-11', 42, 2, N'Đã lên lịch'),
(N'qlpm02', 3, 3, 3, '2026-05-12', 35, 3, N'Đã lên lịch'),
(N'qlpm02', 5, 4, 4, '2026-05-13', 30, 4, N'Hoàn thành'),
(N'admin',  1, 2, 5, '2026-05-14', 32, 5, N'Đã hủy');
GO

SELECT N'Tạo database QuanLyPhongMay và dữ liệu mẫu thành công.' AS ThongBao;
GO
