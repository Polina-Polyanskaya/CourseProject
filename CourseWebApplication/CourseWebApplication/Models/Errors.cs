using System.Text;
using System.Text.RegularExpressions;

namespace WebApp.Models
{
    public class Errors
    {
        public static string HasAnyErrors(string login, string password)
        {
            StringBuilder error = new StringBuilder();
            if (login is null)
                error.Append("Login is null").Append(Environment.NewLine);
            if (password is null)
                error.Append("Password is null").Append(Environment.NewLine);
            if (error.Length != 0)
                return error.ToString();

            if (login.Length < 4 || login.Length > 15)
                error.Append("Login length must be in [4;15]").Append(Environment.NewLine);
            foreach (var character in login)
            {
                if (!Char.IsLetterOrDigit(character))
                {
                    error.Append("Login contains symbol that is not letter or digit").Append(Environment.NewLine); ;
                    break;
                }
            }
            if (password.Length < 8 || password.Length > 15)
                error.Append("Password length must be in [8;15]").Append(Environment.NewLine);
            foreach (var character in password)
            {
                if (!Char.IsLetterOrDigit(character))
                {
                    error.Append("Password contains symbol that is not letter or digit").Append(Environment.NewLine); ;
                    break;
                }
            }
            return error.ToString();
        }

        public static string HasAnyErrors(string password)
        {
            StringBuilder error = new StringBuilder();
            if (password is null)
                error.Append("Password is null").Append(Environment.NewLine);
            if (error.Length != 0)
                return error.ToString();
            if (password.Length < 8 || password.Length > 15)
                error.Append("Password length must be in [8;15]").Append(Environment.NewLine);
            foreach (var character in password)
            {
                if (!Char.IsLetterOrDigit(character))
                {
                    error.Append("Password contains symbol that is not letter or digit").Append(Environment.NewLine); ;
                    break;
                }
            }
            return error.ToString();
        }
    }
}
