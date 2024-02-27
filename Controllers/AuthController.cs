using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic;
using ShopAccBE.Business;
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
        private readonly UserServices userServices;
        public AuthController(IConfiguration configuration, DataContext dataContext)
        {
            _configuration = configuration;
            _dataContext = dataContext;
            userServices = new UserServices(_dataContext, _configuration);
        }
        #region Register and Login
        [HttpPost("register")]
        public ActionResult<APIModel<UserInfo>> Register(UserDTO user)
        {
            var APIModel = userServices.Register(user);
            return Ok(APIModel);
        }
        [HttpPost("login")]
        public ActionResult<APIModel<UserInfo>> Login(UserDTO user)
        {
            var APIModel = userServices.Login(user);
            return Ok(APIModel);
        }
        #endregion

    }
}
