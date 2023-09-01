using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ShopAccBE.Data;
using ShopAccBE.Model;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using static ShopAccBE.Data.EnumConstant;

namespace ShopAccBE.Business
{
    public class UserServices
    {
        private readonly DataContext _dataContext;
        private readonly IConfiguration _configuration;
        public UserServices(DataContext dataContext, IConfiguration configuration)
        {
            _dataContext = dataContext;
            _configuration = configuration;
        }

        #region HasPass and Token
        private void CreatePassWordHash(string passWord, out byte[] passWordHash, out byte[] passWordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passWordSalt = hmac.Key;
                passWordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(passWord));
            }
        }
        private bool VerifyPasswordHash(string passWord, byte[] passWordHash, byte[] passWordSalt)
        {
            using (var hmac = new HMACSHA512(passWordSalt))
            {
                var computeHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(passWord));
                return computeHash.SequenceEqual(passWordHash);
            }
        }
        private string CreateToken(UserInfo userInfo)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name,userInfo.UserLogin)
            };
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));
            //var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("Secret phase"));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            var token = new JwtSecurityToken(
                    claims: claims
                    , expires: DateTime.Now.AddDays(1)
                    , signingCredentials: cred
                );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }
        #endregion

        #region Register and Login
        public APIModel<UserInfo> Register(UserDTO user)
        {
            var APIModel = new APIModel<UserInfo>();
            APIModel.Status = StatusApi.E_FAILED.ToString();
            try
            {
                var userDB = _dataContext.UserInfo.Where(s => s.UserLogin == user.UserLogin && s.IsDelete == null || s.IsDelete == false).FirstOrDefault();
                if (userDB != null)
                {
                    //Có user trong DB
                    APIModel.Data = new List<UserInfo> { userDB };
                    APIModel.Status = StatusApi.E_FAILED.ToString();
                    return APIModel;
                }
                else
                {
                    CreatePassWordHash(user.Password, out byte[] passWordHash, out byte[] passWordSalt);
                    var userInfo = new UserInfo();
                    userInfo.ID = Guid.NewGuid();
                    userInfo.DateUpdate = DateTime.Now;
                    userInfo.UserUpdate = user.UserLogin;
                    userInfo.IsDelete = null;
                    userInfo.UserName = user.UserLogin;
                    userInfo.PasssWordHash = passWordHash;
                    userInfo.PasssWordSalth = passWordSalt;
                    userInfo.UserLogin = user.UserLogin;
                    _dataContext.Add(userInfo);
                    _dataContext.SaveChanges();

                    APIModel.Status = StatusApi.E_SUCCESSED.ToString();
                    APIModel.Data = new List<UserInfo> { userInfo };
                    return APIModel;
                }

            }
            catch (Exception ex)
            {
                APIModel.Message = ex.Message.ToString();
                return APIModel;
            }
        }
        public APIModel<UserInfo> Login(UserDTO user)
        {
            var APIModel = new APIModel<UserInfo>();
            APIModel.Status = StatusApi.E_FAILED.ToString();
            var userDB = _dataContext.UserInfo.Where(s => s.UserLogin == user.UserLogin && s.IsDelete == null || s.IsDelete == false).FirstOrDefault();
            if (userDB == null)
            {
                APIModel.Message = "LoginNotUser";
                return APIModel;
            }

            if (user.UserLogin != userDB.UserLogin)
            {
                APIModel.Message = "LoginFailedUserName";
                return APIModel;
            }

            if (!VerifyPasswordHash(user.Password, userDB.PasssWordHash, userDB.PasssWordSalth))
            {
                APIModel.Message = "LoginFailedPassword";
                return APIModel;
            }
            APIModel.Status = StatusApi.E_SUCCESSED.ToString();
            string token = CreateToken(userDB);
            APIModel.Message = token;
            APIModel.Data = new List<UserInfo> { userDB };
            return APIModel;
        }
        #endregion
    }
}
