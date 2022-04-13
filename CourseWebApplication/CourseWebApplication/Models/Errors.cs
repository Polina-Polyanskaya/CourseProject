using System.Text;
using System.Text.RegularExpressions;

namespace WebApp.Models
{
    public class Errors
    {
        public static string HasAnyErrors(string login, string password)
        {
            StringBuilder error = new StringBuilder();
            if (login is null || login.Length < 4 || login.Length > 15)
            {
                error.Append("Login is null, small or long").Append(Environment.NewLine);
                return error.ToString();
            }
            if (password is null || password.Length < 8 || password.Length > 15)
            {
                error.Append("Password is null, small or long").Append(Environment.NewLine);
                return error.ToString();
            }
            if (!Regex.IsMatch(login, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{4,15}$"))
                error.Append("Login must contain at least 4 characters").Append(Environment.NewLine);
            if (!Regex.IsMatch(password, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,15}$"))
                error.Append("Password must contain at least 8 characters").Append(Environment.NewLine);
            return error.ToString();
        }

        public static string HasAnyErrors(string password)
        {
            StringBuilder error = new StringBuilder();
            if (password is null || password.Length < 8 || password.Length > 15)
            {
                error.Append("Password is null, small or long").Append(Environment.NewLine);
                return error.ToString();
            }
            if (!Regex.IsMatch(password, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,15}$"))
                error.Append("Password must contain at least 8 characters").Append(Environment.NewLine);
            return error.ToString();
        }
    }
}
