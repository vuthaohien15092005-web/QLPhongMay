using System;
using QLPhongMay.DAL;
using QLPhongMay.DTO;
using QLPhongMay.Enums;

namespace QLPhongMay.BLL
{
    public class UserService
    {
        private readonly UserRepository userRepository;

        public UserService()
            : this(new UserRepository())
        {
        }

        public UserService(UserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public User Login(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrEmpty(password))
            {
                return null;
            }

            User user = this.userRepository.GetByUsername(username.Trim());
            if (user == null)
            {
                return null;
            }

            if (!PasswordHasher.Verify(password, user.MatKhau))
            {
                return null;
            }

            user.Role = ResolveRole(user.MaVaiTro, user.TenVaiTro);
            return user;
        }

        private static UserRole ResolveRole(string roleCode, string roleName)
        {
            string value = ((roleCode ?? string.Empty) + " " + (roleName ?? string.Empty)).Trim();

            if (Contains(value, "admin") || Contains(value, "quantri") || Contains(value, "quản trị"))
            {
                return UserRole.Admin;
            }

            if (Contains(value, "quanlyphongmay") ||
                Contains(value, "qlpm") ||
                Contains(value, "quản lý phòng máy") ||
                Contains(value, "nhân viên") ||
                Contains(value, "nhan vien"))
            {
                return UserRole.QuanLyPhongMay;
            }

            throw new UnauthorizedAccessException("Tài khoản chưa được gán vai trò hợp lệ.");
        }

        private static bool Contains(string source, string value)
        {
            return source.IndexOf(value, StringComparison.OrdinalIgnoreCase) >= 0;
        }
    }
}