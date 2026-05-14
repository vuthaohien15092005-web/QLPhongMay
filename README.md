# QLPhongMay

Ứng dụng WinForms quản lý phòng máy, viết bằng C# trên .NET Framework 4.7.2. Giao diện dùng Guna.UI2.WinForms, dữ liệu dùng SQL Server, Entity Framework 6 và Dapper.

## Cấu trúc chính

```text
QLPhongMay/
├── BLL/        # Xử lý nghiệp vụ
├── DAL/        # Truy cập database
├── DTO/        # Object truyền dữ liệu giữa các tầng
├── Enums/      # Enum trạng thái, vai trò
├── Models/     # Entity ánh xạ bảng database
├── GUI/Forms/  # Giao diện WinForms
├── Properties/
├── App.config
└── QLPhongMay.csproj
Các thư mục quan trọng
BLL
Chứa logic nghiệp vụ.

AuthService.cs
PasswordHasher.cs
Dùng cho đăng nhập, xác thực mật khẩu và các xử lý nghiệp vụ liên quan.

DAL
Chứa code làm việc với database.

AppDbContext.cs
QLPhongMayDbContext.cs
UserRepository.cs
Form không nên viết SQL trực tiếp, mà nên gọi qua Repository hoặc Service.

DTO
Chứa class dữ liệu phục vụ trao đổi giữa các tầng.

User.cs
AccountListItem.cs
Models
Chứa entity ánh xạ database.

CaHoc.cs
CauHinh.cs
LichThucHanh.cs
LopHoc.cs
MayTinh.cs
NguoiDung.cs
PhongMay.cs
VaiTro.cs
GUI/Forms
Chứa các màn hình WinForms.

Auth/
  FrmLogin.cs

Dashboard/
  frmMain_Admin.cs

Users/
  frmQLTaiKhoan.cs
  FrmAddAccount.cs
Màn hình hiện có
FrmLogin
Màn hình đăng nhập. Gọi AuthService để kiểm tra tài khoản và mật khẩu.

frmMain_Admin
Dashboard chính của Admin. Hiện có các thẻ chức năng:

Tạo lịch thực hành
Quản lý phòng máy
Quản lý máy tính
Quản lý lớp học
Quản lý ca học
Quản lý tài khoản
Báo cáo & Thống kê
frmQLTaiKhoan
Màn hình quản lý tài khoản, gồm danh sách tài khoản, tìm kiếm, lọc vai trò và nút thêm tài khoản.

FrmAddAccount
Modal thêm tài khoản mới, gồm:

Họ và tên
Tên đăng nhập
Mật khẩu
Email
Vai trò
Hiện form mới trả dữ liệu nhập, chưa lưu database.

Quy ước làm việc
UI chỉ xử lý giao diện, không viết SQL trực tiếp.
Logic nghiệp vụ đặt trong BLL.
Truy cập dữ liệu đặt trong DAL.
Entity database đặt trong Models.
Dữ liệu hiển thị/truyền tầng đặt trong DTO.
