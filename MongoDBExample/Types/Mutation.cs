using MongoDBExample.Models;
using MongoDBExample.Services;

namespace MongoDBExample.Types
{
    public class Mutation
    {
        public async Task<User> create([Service] UserService userService, string name, string email, string password)
        {
            User user = new User();
            user.Name = name;
            user.Email = email;
            user.Password = password;
            await userService.Create(user);
            return user;
        } 

        public async Task<String> createRole([Service] UserService userService, string name)
        {
            return await userService.CreateRole(name);
        }

        public async Task<string> login([Service] UserService userService, LoginInput loginInput)
        {
            return await userService.Login(loginInput);
        }
    }
}
