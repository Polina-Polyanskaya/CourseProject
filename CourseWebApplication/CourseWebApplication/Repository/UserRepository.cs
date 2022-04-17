using Microsoft.EntityFrameworkCore;
using WebApp.Models;
using WebApp.Models.Requests;

namespace WebApp.Jwt
{
    public class UserRepository : IUserRepository
    {
        private readonly WebAppContext _context;
    

        public UserRepository(WebAppContext context)
        {
            _context = context;
        }

        public async Task<string> AddUser(User user)
        {
            if (await _context.Users.AnyAsync(x => x.Login == user.Login))
                return "User with such login already exists.";
            if (await _context.Users.AnyAsync(x => x.Email == user.Email))
                return "User with such email already exists.";
            user.Password=BcryptPasswordHasher.HashPassword(user.Password);
            await _context.Users.AddAsync(user);

            if (!await SaveChanges())
                return "Failed to add user.";

            return null;
        }

        public async Task<bool> SaveChanges()
        {
            return await _context.SaveChangesAsync() >= 0;
        }

        public async Task<User> GetUserById(Guid id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<User> GetUser(LoginRequest user)
        {
            var newUser= await _context.Users.SingleOrDefaultAsync(x => x.Login == user.Login);
            if (newUser == null)
                return null;

            if(BcryptPasswordHasher.VerifyPassword(user.Password, newUser.Password))
                return newUser;

            return null;
        }

        public async Task<User> GetUser(CheckUserRequest user)
        {
            return await _context.Users.SingleOrDefaultAsync(x => x.Login == user.Login && x.Email == user.Email);
        }
    }
}
