-- 1. Vai trò
INSERT INTO VaiTro (tenVaiTro)
VALUES 
(N'Admin'),
(N'Quản lý phòng máy');

-- 2. Người dùng
-- Mật khẩu mẫu đã mã hóa BCrypt, dùng demo cho password: 123456
INSERT INTO NguoiDung (tenDangNhap, matKhau, hoTen, email, maVaiTro)
VALUES
(N'admin', N'$2a$10$7EqJtq98hPqEX7fNZaFWoOHiMKNxNwhBT5zANFQf8jZ9HTL0/9g6K', N'Nguyễn Văn Admin', N'admin@school.edu.vn', 1),
(N'qlpm01', N'$2a$10$7EqJtq98hPqEX7fNZaFWoOHiMKNxNwhBT5zANFQf8jZ9HTL0/9g6K', N'Trần Minh Quản Lý', N'qlpm01@school.edu.vn', 2),
(N'qlpm02', N'$2a$10$7EqJtq98hPqEX7fNZaFWoOHiMKNxNwhBT5zANFQf8jZ9HTL0/9g6K', N'Lê Thị Phòng Máy', N'qlpm02@school.edu.vn', 2);

-- 3. Ca thực hành
INSERT INTO Ca (tenCa, gioBatDau, gioKetThuc)
VALUES
(N'Ca 1', '07:00', '09:00'),
(N'Ca 2', '09:15', '11:15'),
(N'Ca 3', '13:00', '15:00'),
(N'Ca 4', '15:15', '17:15');

-- 4. Phòng máy
INSERT INTO PhongMay (tenPhong, sucChua, trangThai)
VALUES
(N'Phòng máy A101', 40, N'Hoạt động'),
(N'Phòng máy A102', 45, N'Hoạt động'),
(N'Phòng máy B201', 35, N'Hoạt động'),
(N'Phòng máy B202', 50, N'Bảo trì'),
(N'Phòng máy C301', 30, N'Hoạt động');

-- 5. Cấu hình máy
INSERT INTO CauHinh (ram, boCPU, manHinh, heDieuHanh, ghiChu)
VALUES
(N'8GB',  N'Intel Core i5 Gen 10', N'22 inch', N'Windows 10', N'Phù hợp thực hành tin học cơ bản'),
(N'16GB', N'Intel Core i7 Gen 11', N'24 inch', N'Windows 11', N'Phù hợp lập trình và thiết kế'),
(N'8GB',  N'AMD Ryzen 5',         N'22 inch', N'Windows 10', N'Phù hợp học văn phòng'),
(N'32GB', N'Intel Core i7 Gen 12', N'27 inch', N'Windows 11', N'Phù hợp đồ họa và máy ảo');

-- 6. Máy tính
INSERT INTO May (tenMay, tinhTrang, maPhong, maCauHinh)
VALUES
-- Phòng A101
(N'A101-PC01', N'Tốt', 1, 1),
(N'A101-PC02', N'Tốt', 1, 1),
(N'A101-PC03', N'Tốt', 1, 1),
(N'A101-PC04', N'Hỏng', 1, 1),
(N'A101-PC05', N'Tốt', 1, 1),

-- Phòng A102
(N'A102-PC01', N'Tốt', 2, 2),
(N'A102-PC02', N'Tốt', 2, 2),
(N'A102-PC03', N'Tốt', 2, 2),
(N'A102-PC04', N'Đang bảo trì', 2, 2),
(N'A102-PC05', N'Tốt', 2, 2),

-- Phòng B201
(N'B201-PC01', N'Tốt', 3, 3),
(N'B201-PC02', N'Tốt', 3, 3),
(N'B201-PC03', N'Hỏng', 3, 3),
(N'B201-PC04', N'Tốt', 3, 3),
(N'B201-PC05', N'Tốt', 3, 3),

-- Phòng B202
(N'B202-PC01', N'Đang bảo trì', 4, 4),
(N'B202-PC02', N'Đang bảo trì', 4, 4),
(N'B202-PC03', N'Đang bảo trì', 4, 4),

-- Phòng C301
(N'C301-PC01', N'Tốt', 5, 1),
(N'C301-PC02', N'Tốt', 5, 1),
(N'C301-PC03', N'Tốt', 5, 1),
(N'C301-PC04', N'Tốt', 5, 1);

-- 7. Lớp học
INSERT INTO Lop (tenLop, nganh, siSo)
VALUES
(N'CNTT01', N'Công nghệ thông tin', 38),
(N'CNTT02', N'Công nghệ thông tin', 42),
(N'KTPM01', N'Kỹ thuật phần mềm', 35),
(N'TKDH01', N'Thiết kế đồ họa', 30),
(N'QTM01', N'Quản trị mạng', 32);

-- 8. Lịch thực hành
INSERT INTO LichThucHanh 
(tenDangNhap, maPhong, maCa, maLop, ngayThucHanh, soLuongSV, thuTrongTuan, trangThai)
VALUES
(N'qlpm01', 1, 1, 1, '2026-05-11', 38, 2, N'Đã lên lịch'),
(N'qlpm01', 2, 2, 2, '2026-05-11', 42, 2, N'Đã lên lịch'),
(N'qlpm02', 3, 3, 3, '2026-05-12', 35, 3, N'Đã lên lịch'),
(N'qlpm02', 5, 4, 4, '2026-05-13', 30, 4, N'Hoàn thành'),
(N'admin',  1, 2, 5, '2026-05-14', 32, 5, N'Đã hủy');
GO