using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ocelot.JWTAuthorizePolicy;

namespace AuthenticationAPI.Controllers
{
    public class PermissionController : Controller
    {
        /// <summary>
        /// 自定义参数策略
        /// </summary>
        private PermissionRequirement _requirement;

        public PermissionController(PermissionRequirement requirement)
        {
            _requirement = requirement;
        }

        [AllowAnonymous]
        [HttpGet("/authapi/login")]
        public IActionResult Login(string username, string password)
        {
            var isValidated = (username == "yp" && password == "111111") || (username == "yepeng" && password == "222222");
            var role = username == "yp" ? "admin" : "system";
            if (!isValidated)
            {
                return new JsonResult(new
                {
                    Status = false,
                    Message = "认证失败"
                });
            }
            else
            {
                //如果是基于用户的授权策略，这里要添加用户；如果是基于角色的授权策略，这里要添加角色
                var claims = new Claim[]
                {
                    new Claim(ClaimTypes.Name, username), new Claim(ClaimTypes.Role, role),
                    new Claim(ClaimTypes.Expiration,
                        DateTime.Now.AddSeconds(_requirement.Expiration.TotalSeconds).ToString())
                };
                //用户标识
                var identity = new ClaimsIdentity(JwtBearerDefaults.AuthenticationScheme);
                identity.AddClaims(claims);

                var token = JwtToken.BuildJwtToken(claims, _requirement);
                return new JsonResult(token);
            }
        }
    }
}