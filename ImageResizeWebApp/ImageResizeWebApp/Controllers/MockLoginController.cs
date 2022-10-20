using Microsoft.AspNetCore.Mvc;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;

namespace ImageResizeWebApp.Controllers
{
    [Route("api/[controller]")]
    public class MockLoginController : Controller
    {
        [HttpPost("[action]")]
        public async Task<IActionResult> LoginMock([FromHeader]string email)
        {
            return Ok(new JwtSecurityToken(expires: DateTime.Now.AddHours(2), audience:"dreamAudience", issuer: email));
        }
    }
}