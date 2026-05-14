using QLPhongMay.DTO;
using QLPhongMay.Enums;

namespace QLPhongMay.BLL
{
    public static class Session
    {
        public static User CurrentUser { get; private set; }

        public static UserRole? CurrentRole
        {
            get { return CurrentUser == null ? (UserRole?)null : CurrentUser.Role; }
        }

        public static bool IsAuthenticated
        {
            get { return CurrentUser != null; }
        }

        public static void SignIn(User user)
        {
            CurrentUser = user;
        }

        public static void SignOut()
        {
            CurrentUser = null;
        }

        public static bool HasRole(UserRole role)
        {
            return CurrentRole.HasValue && CurrentRole.Value == role;
        }
    }
}