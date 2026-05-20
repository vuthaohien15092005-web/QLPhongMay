DELETE FROM May
WHERE maMay BETWEEN 23 AND 44;

DELETE FROM PhongMay
WHERE maPhong BETWEEN 6 AND 10;

DELETE FROM Ca
WHERE maCa BETWEEN 5 AND 12;
GO

CREATE TABLE Ram (
    maRam INT IDENTITY(1,1) PRIMARY KEY,
    tenRam NVARCHAR(50) NOT NULL
);

CREATE TABLE BoCPU (
    maCPU INT IDENTITY(1,1) PRIMARY KEY,
    tenCPU NVARCHAR(100) NOT NULL
);

CREATE TABLE ManHinh (
    maManHinh INT IDENTITY(1,1) PRIMARY KEY,
    tenManHinh NVARCHAR(50) NOT NULL
);

CREATE TABLE HeDieuHanh (
    maHDH INT IDENTITY(1,1) PRIMARY KEY,
    tenHDH NVARCHAR(50) NOT NULL
);
GO

INSERT INTO Ram (tenRam)
VALUES
(N'8GB'),
(N'16GB'),
(N'32GB');

INSERT INTO BoCPU (tenCPU)
VALUES
(N'Intel Core i5 Gen 10'),
(N'Intel Core i7 Gen 11'),
(N'AMD Ryzen 5'),
(N'Intel Core i7 Gen 12');

INSERT INTO ManHinh (tenManHinh)
VALUES
(N'22 inch'),
(N'24 inch'),
(N'27 inch');

INSERT INTO HeDieuHanh (tenHDH)
VALUES
(N'Windows 10'),
(N'Windows 11');
GO

ALTER TABLE May
ADD maRam INT,
    maCPU INT,
    maManHinh INT,
    maHDH INT;
GO

UPDATE May
SET maRam = 1,
    maCPU = 1,
    maManHinh = 1,
    maHDH = 1
WHERE maCauHinh = 1;

UPDATE May
SET maRam = 2,
    maCPU = 2,
    maManHinh = 2,
    maHDH = 2
WHERE maCauHinh = 2;

UPDATE May
SET maRam = 1,
    maCPU = 3,
    maManHinh = 1,
    maHDH = 1
WHERE maCauHinh = 3;

UPDATE May
SET maRam = 3,
    maCPU = 4,
    maManHinh = 3,
    maHDH = 2
WHERE maCauHinh = 4;

UPDATE May
SET maRam = 1,
    maCPU = 1,
    maManHinh = 1,
    maHDH = 1
WHERE maCauHinh = 5;
GO

ALTER TABLE May
ADD CONSTRAINT FK_May_Ram
FOREIGN KEY (maRam) REFERENCES Ram(maRam);

ALTER TABLE May
ADD CONSTRAINT FK_May_CPU
FOREIGN KEY (maCPU) REFERENCES BoCPU(maCPU);

ALTER TABLE May
ADD CONSTRAINT FK_May_ManHinh
FOREIGN KEY (maManHinh) REFERENCES ManHinh(maManHinh);

ALTER TABLE May
ADD CONSTRAINT FK_May_HDH
FOREIGN KEY (maHDH) REFERENCES HeDieuHanh(maHDH);
GO

ALTER TABLE May
DROP CONSTRAINT FK_May_CauHinh;
GO

ALTER TABLE May
DROP COLUMN maCauHinh;
GO

DROP TABLE CauHinh;
GO
