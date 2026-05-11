using QLPhongMay.DAL;
using QLPhongMay.DTO;

namespace QLPhongMay.BLL
{
    public class AuthService
    {
        private readonly UserRepository userRepository;

        public AuthService()
            : this(new UserRepository())
        {
        }

        public AuthService(UserRepository userRepository)
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

            return PasswordHasher.Verify(password, user.MatKhau) ? user : null;
        }
    }
}
