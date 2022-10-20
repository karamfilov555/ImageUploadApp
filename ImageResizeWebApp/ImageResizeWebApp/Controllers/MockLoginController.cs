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
        public async Task<IActionResult> LoginMock([FromHeader] string email)
        {
            var token = new JwtSecurityToken(expires: DateTime.Now.AddHours(2), audience: "dreamAudience", issuer: email);

            return Ok(token);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> LoginMock2([FromHeader] string email)
        {
            if (email.Equals("batman@abv.bg", StringComparison.InvariantCultureIgnoreCase) || email.Equals("daffy@abv.bg", StringComparison.InvariantCultureIgnoreCase))
            {
                var token = new JwtSecurityToken(expires: DateTime.Now.AddHours(2), audience: "dreamAudience", issuer: email);
                return Ok(token);
            }


            return BadRequest("Invalid request");
        }
    }
}