USE [Quanlyphongmay];
GO

-- 1. VaiTro
CREATE TABLE VaiTro (
    maVaiTro    INT PRIMARY KEY IDENTITY(1,1),
    tenVaiTro   NVARCHAR(50) NOT NULL
);

-- 2. NguoiDung
CREATE TABLE NguoiDung (
    tenDangNhap NVARCHAR(50)  PRIMARY KEY,
    matKhau     NVARCHAR(255) NOT NULL,
    hoTen       NVARCHAR(100) NOT NULL,
    email       NVARCHAR(100) NOT NULL UNIQUE,
    maVaiTro    INT           NOT NULL,
    CONSTRAINT FK_NguoiDung_VaiTro FOREIGN KEY (maVaiTro) REFERENCES VaiTro(maVaiTro)
);

-- 3. Ca
CREATE TABLE Ca (
    maCa        INT PRIMARY KEY IDENTITY(1,1),
    tenCa       NVARCHAR(50)  NOT NULL,
    gioBatDau   TIME          NOT NULL,
    gioKetThuc  TIME          NOT NULL
);

-- 4. PhongMay
CREATE TABLE PhongMay (
    maPhong     INT PRIMARY KEY IDENTITY(1,1),
    tenPhong    NVARCHAR(50)  NOT NULL,
    sucChua     INT           NOT NULL,
    trangThai   NVARCHAR(50)  NOT NULL DEFAULT N'Hoạt động',
    createdAt   DATETIME      NOT NULL DEFAULT GETDATE(),
    updatedAt   DATETIME      NOT NULL DEFAULT GETDATE()
);

-- 5. CauHinh
CREATE TABLE CauHinh (
    maCauHinh   INT PRIMARY KEY IDENTITY(1,1),
    ram         NVARCHAR(50)  NOT NULL,
    boCPU       NVARCHAR(100) NOT NULL,
    manHinh     NVARCHAR(50)  NULL,
    heDieuHanh  NVARCHAR(100) NOT NULL,
    ghiChu      NVARCHAR(255) NULL
);

-- 6. May
CREATE TABLE May (
    maMay       INT PRIMARY KEY IDENTITY(1,1),
    tenMay      NVARCHAR(100) NOT NULL,
    tinhTrang   NVARCHAR(50)  NOT NULL DEFAULT N'Tốt',
    maPhong     INT           NOT NULL,
    maCauHinh   INT           NOT NULL,
    createdAt   DATETIME      NOT NULL DEFAULT GETDATE(),
    updatedAt   DATETIME      NOT NULL DEFAULT GETDATE(),
    CONSTRAINT FK_May_PhongMay   FOREIGN KEY (maPhong)    REFERENCES PhongMay(maPhong),
    CONSTRAINT FK_May_CauHinh    FOREIGN KEY (maCauHinh)  REFERENCES CauHinh(maCauHinh)
);

-- 7. Lop
CREATE TABLE Lop (
    maLop       INT PRIMARY KEY IDENTITY(1,1),
    tenLop      NVARCHAR(50)  NOT NULL,
    nganh       NVARCHAR(100) NOT NULL,
    siSo        INT           NOT NULL
);

-- 8. LichThucHanh
CREATE TABLE LichThucHanh (
    maLich          INT PRIMARY KEY IDENTITY(1,1),
    tenDangNhap     NVARCHAR(50)  NOT NULL,
    maPhong         INT           NOT NULL,
    maCa            INT           NOT NULL,
    maLop           INT           NOT NULL,
    ngayThucHanh    DATE          NOT NULL,
    soLuongSV       INT           NOT NULL,
    thuTrongTuan    TINYINT       NOT NULL,   -- 2-8 (Thứ 2 → Chủ nhật)
    trangThai       NVARCHAR(50)  NOT NULL,
    CONSTRAINT FK_Lich_NguoiDung FOREIGN KEY (tenDangNhap) REFERENCES NguoiDung(tenDangNhap),
    CONSTRAINT FK_Lich_PhongMay  FOREIGN KEY (maPhong)     REFERENCES PhongMay(maPhong),
    CONSTRAINT FK_Lich_Ca        FOREIGN KEY (maCa)        REFERENCES Ca(maCa),
    CONSTRAINT FK_Lich_Lop       FOREIGN KEY (maLop)       REFERENCES Lop(maLop),
    CONSTRAINT UQ_Lich_Phong_Ca_Ngay UNIQUE (maPhong, maCa, ngayThucHanh)
);
GO


