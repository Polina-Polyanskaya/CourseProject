using WebApp.Models;
using WebApp.Models.Requests;

namespace WebApp.Jwt
{
    public interface IUserRepository
    {
        Task<string> AddUser(User user);

        Task<User> GetUserById(Guid id);

        Task<User> GetUser(LoginRequest user);

        Task<bool> SaveChanges();

        Task<User> GetUser(CheckUserRequest user);
    }
}
