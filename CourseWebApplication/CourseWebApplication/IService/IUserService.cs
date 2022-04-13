using WebApp.Models;
using WebApp.Models.Requests;

namespace WebApp.Jwt
{
    public interface IUserService
    {
        Task<string> AddUser(User user);

        Task<User> GetUserById(Guid id);

        Task<User> GetUser(LoginRequest user);

        Task<string> UpdateUser(User user);

        Task<bool> CheckUser(CheckUserRequest user);
    }
}
