using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using JSSATSProject.Repository.Entities;
using JSSATSProject.Service.Models.AccountModel;
using JSSATSProject.Service.Service.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace JSSATSProject.API.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class LoginController : ControllerBase
{
    private readonly IActiveJWTService _activeJwtService;
    private readonly IAccountService _accountService;
    private readonly IConfiguration _config;
    private readonly IMapper _mapper;


    public LoginController(IAccountService accountService, IConfiguration config, IMapper mapper,
        IActiveJWTService activeJwtService)
    {
        _accountService = accountService;
        _config = config;
        _mapper = mapper;
        _activeJwtService = activeJwtService;
    }

    [AllowAnonymous]
    [HttpPost]
    public async Task<ActionResult> Login([FromBody] RequestSignIn userLogin)
    {
        var user = Authenticate(userLogin);
        var username = userLogin.Username;
        var tokenResponse = _mapper.Map<ResponseToken>(user);
        if (user is not null)
        {
            var lastJwtToken = await _activeJwtService.GetByUsernameAsync(username);
            if (lastJwtToken is not null)
            {
                await _activeJwtService.DeleteAsync(lastJwtToken.Username);
            }

            if (user.Status != "active")
                return Problem($"Account {userLogin.Username} has been deactivated.",
                    statusCode: Convert.ToInt32(HttpStatusCode.Unauthorized), title: "Login Failed");
            var token = GenerateToken(user);
            await _activeJwtService.SaveTokenAsync(username, token);
            tokenResponse.Token = token;
            return Ok(tokenResponse);
        }

        return Problem($"User {userLogin.Username} not found.",
            statusCode: Convert.ToInt32(HttpStatusCode.Unauthorized), title: "Login Failed");
    }

    // To generate token
    private string GenerateToken(Account user)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Username),
            new Claim(ClaimTypes.Role, user.Role.Name)
        };
        var token = new JwtSecurityToken(_config["Jwt:Issuer"],
            _config["Jwt:Audience"],
            claims,
            expires: DateTime.Now.AddMinutes(120),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private async Task InvalidateUserTokenAsync(string username)
    {
        await _activeJwtService.DeleteAsync(username);
    }

    //To authenticate user
    private Account? Authenticate(RequestSignIn userLogin)
    {
        if (_accountService.GetByUsernameAndPassword(userLogin.Username, userLogin.Password)
                .Result.Data is Account currentUser)
            return currentUser;

        return null;
    }
}