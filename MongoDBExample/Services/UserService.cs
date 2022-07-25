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
        public UserService(IMongoDBSettings settings)
        {
            _client = new MongoClient(settings.ConnectionString);
            _database = _client.GetDatabase(settings.DatabaseName);
            _users= _database.GetCollection<User>("Users");
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

        public User Create(User user)
        {
            _users.InsertOne(user);
            return user;
        }

        public string Login(LoginInput loginInput)
        {

            var user = _users.Find(x => x.Email == loginInput.email).FirstOrDefault();
            if (user == null)
            {
                return "kullanıcı bulunamadı.";
            }


            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.UTF8.GetBytes("MySuperSecretKey");
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Email, loginInput.email),
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(tokenKey),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);


            //var securtityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("MySuperSecretKey"));
            //var credentials = new SigningCredentials(securtityKey, SecurityAlgorithms.HmacSha256);

            //var claims = new List<Claim>
            //{
            //    new Claim(ClaimTypes.Name, user.Name),
            //    new Claim(ClaimTypes.Email, user.Email)
            //};

            //        claims.Add(new Claim(ClaimTypes.Role, "User"));


            //var jwtSecurityToken = new JwtSecurityToken(
            //    issuer: "https://auth.chillicream.com",
            //    audience: "https://graphql.chillicream.com",
            //    //expires: DateTime.Now.AddMinutes(_jwtSettings.AccessTokenExpMinute),
            //    signingCredentials: credentials,
            //    claims: claims
            //);
            //return new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);


            ////var roles = _userRoleRepository.GetRoleById(user.Id);

        }
    }
}
