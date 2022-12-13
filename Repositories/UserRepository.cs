using AuthProject.Models;

namespace AuthProject.Repositories
{
    public class UserRepository
    {
        public static User Get(string username, string password)
        {
            var users = new List<User>
            {
                new User { Id = 1, Username = "Batman", Password = "marta123", Role = "manager" },
                new User { Id = 2, Username = "Robin", Password = "coringa123", Role = "employee" }
            };

            return users.Where(x => x.Username.ToLower() == username.ToLower() && x.Password.ToLower() == password.ToLower()).FirstOrDefault();
        }
    }
}
