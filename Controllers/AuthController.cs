using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic;
using ShopAccBE.Data;
using ShopAccBE.Model;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using static ShopAccBE.Data.EnumConstant;

namespace ShopAccBE.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly DataContext _dataContext;
        public AuthController(IConfiguration configuration, DataContext dataContext)
        {
            _configuration = configuration;
            _dataContext = dataContext;

        }
    
        [HttpPost("register")]
        public async Task<ActionResult<APIModel<UserInfo>>> Register(UserDTO user)
        {
            var APIModel = new APIModel<UserInfo>();
            APIModel.Status = StatusApi.E_FAILED.ToString();
            try
            {
                var userDB = await _dataContext.UserInfo.Where(s => s.UserLogin == user.UserLogin && s.IsDelete == null || s.IsDelete == false).FirstOrDefaultAsync();
                if (userDB != null)
                {
                    //Có user trong DB
                    APIModel.Data = new List<UserInfo> { userDB };
                    APIModel.Status = StatusApi.E_FAILED.ToString();
                    return Ok(APIModel);
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
                    await _dataContext.SaveChangesAsync();

                    APIModel.Status = StatusApi.E_SUCCESSED.ToString();
                    APIModel.Data = new List<UserInfo> { userInfo };
                    return Ok(APIModel);
                }
                
            }
            catch (Exception ex)
            {
                APIModel.Message = ex.Message.ToString();
                return Ok(APIModel);
            }
        }
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
                    , expires:DateTime.Now.AddDays(1)
                    , signingCredentials: cred
                );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }
        [HttpPost("login")]
        public async Task<ActionResult<APIModel<UserInfo>>> Login(UserDTO user)
        {
            var APIModel = new APIModel<UserInfo>();
            APIModel.Status = StatusApi.E_FAILED.ToString();
            var userDB = await _dataContext.UserInfo.Where(s => s.UserLogin == user.UserLogin && s.IsDelete == null || s.IsDelete == false).FirstOrDefaultAsync();
            if (userDB == null)
            {
                APIModel.Message = "LoginNotUser";
                return Ok(APIModel);
            }

            if (user.UserLogin != userDB.UserLogin)
            {
                APIModel.Message = "LoginFailedUserName";
                return Ok(APIModel);
            }

            if (!VerifyPasswordHash(user.Password, userDB.PasssWordHash, userDB.PasssWordSalth))
            {
                APIModel.Message = "LoginFailedPassword";
                return Ok(APIModel);
            }
            APIModel.Status = StatusApi.E_SUCCESSED.ToString();
            string token = CreateToken(userDB);
            APIModel.Message = token;
            APIModel.Data = new List<UserInfo> { userDB };
            return Ok(APIModel);
        }
    }
}
