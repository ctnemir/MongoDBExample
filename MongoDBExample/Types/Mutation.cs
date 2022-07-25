using MongoDBExample.Models;
using MongoDBExample.Services;

namespace MongoDBExample.Types
{
    public class Mutation
    {
        public User create([Service] UserService userService, string name, string email, string password)
        {
            User user = new User();
            user.Name = name;
            user.Email = email;
            user.Password = password;
            userService.Create(user);
            return user;
        } 

        public string login([Service] UserService userService, LoginInput loginInput)
        {
            return userService.Login(loginInput);
        }
    }
}
