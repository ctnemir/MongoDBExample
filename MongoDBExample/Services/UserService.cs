using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using MongoDBExample.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MongoDBExample.Services
{
    public class UserService
    {
        private MongoClient _client;
        private IMongoDatabase _database;
        private readonly IMongoCollection<User> _users;
        private UserManager<ApplicationUser> userManager;
        private RoleManager<ApplicationRole> roleManager;
        private SignInManager<ApplicationUser> signInManager;
        public UserService(IMongoDBSettings settings, 
            UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _client = new MongoClient(settings.ConnectionString);
            _database = _client.GetDatabase(settings.DatabaseName);
            _users = _database.GetCollection<User>("Users");
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.signInManager = signInManager;
        }

        public List<User> GetUsers()
        {
            List<User> users;
            users = _users.Find<User>(us => true).ToList();
            return users;
        }

        public User GetUsers(string id)
        {
            User user;
            user = _users.Find<User>(us => us.Id == id).FirstOrDefault();
            return user;
        }

        public async Task<User> Create(User user)
        {

            ApplicationUser appUser = new ApplicationUser
            {
                UserName = user.Name,
                Email = user.Email
            };

            IdentityResult result = await userManager.CreateAsync(appUser, user.Password);

            return user;
        }

        public async Task<String> CreateRole(string name)
        {
            IdentityResult result = await roleManager.CreateAsync(new ApplicationRole() { Name = name });
            if (result.Succeeded)
                return "Ok";
            else
            {
                return "Hata";
            }
        }

        //Burası Identity bazlı denemem
        public async Task<String> Login(LoginInput loginInput)
        {
            ApplicationUser appUser = await userManager.FindByEmailAsync(loginInput.email);
            if (appUser != null)
            {
                Microsoft.AspNetCore.Identity.SignInResult result = await signInManager.PasswordSignInAsync(appUser, loginInput.password, false, false);
                if (result.Succeeded)
                {
                    return "Succeed";
                }
            }
            return "Failed";
        }


        // Burası JWT Token Dönüyor

        //public string Login(LoginInput loginInput)
        //{

        //    var user = _users.Find(x => x.Email == loginInput.email).FirstOrDefault();
        //    if (user == null)
        //    {
        //        return "kullanıcı bulunamadı.";
        //    }


        //    var tokenHandler = new JwtSecurityTokenHandler();
        //    var tokenKey = Encoding.UTF8.GetBytes("MySuperSecretKey");
        //    var tokenDescriptor = new SecurityTokenDescriptor()
        //    {
        //        Subject = new ClaimsIdentity(new Claim[]
        //        {
        //            new Claim(ClaimTypes.Email, loginInput.email),
        //        }),
        //        Expires = DateTime.UtcNow.AddHours(1),
        //        SigningCredentials = new SigningCredentials(
        //            new SymmetricSecurityKey(tokenKey),
        //            SecurityAlgorithms.HmacSha256Signature)
        //    };

        //    var token = tokenHandler.CreateToken(tokenDescriptor);
        //    return tokenHandler.WriteToken(token);

        //}
    }
}
