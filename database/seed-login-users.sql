IF NOT EXISTS (SELECT 1 FROM NguoiDung WHERE tenDangNhap = N'qlpm1')
BEGIN
    INSERT INTO NguoiDung (tenDangNhap, matKhau, hoTen, email, maVaiTro)
    VALUES (N'qlpm1', N'vuthaohien', N'Nguyễn Văn Admin', N'qlpm4@school.edu.vn', 2);
END;

IF NOT EXISTS (SELECT 1 FROM NguoiDung WHERE tenDangNhap = N'admin1')
BEGIN
    INSERT INTO NguoiDung (tenDangNhap, matKhau, hoTen, email, maVaiTro)
    VALUES (N'admin1', N'123456', N'Vũ Thảo Hiền', N'vuthaohien4@school.edu.vn', 1);
END;
