using WebApp.Models;
using WebApp.Models.Requests;

namespace WebApp.Jwt
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<string> AddUser(User user)
        {
            string error = Errors.HasAnyErrors(user.Login, user.Password);

            if (string.IsNullOrEmpty(error))
                error = await _userRepository.AddUser(user);

            return error;
        }

        public async Task<string> UpdateUser(User user)
        {
            user.Password = BcryptPasswordHasher.HashPassword(user.Password);
            string? error = null;
            try
            {
                if (!(await _userRepository.SaveChanges()))
                    error = "Failed to save changes.";
            }
            catch (Exception e)
            {
                error = e.Message;
            }
            return error;
        }

        public async Task<User> GetUserById(Guid id)
        {
            return await _userRepository.GetUserById(id);
        }

        public async Task<User> GetUser(LoginRequest user)
        {
            return await _userRepository.GetUser(user);
        }

        public async Task<bool> CheckUser(CheckUserRequest user)
        {
            return await _userRepository.GetUser(user) !=null;
        }

    }
}
