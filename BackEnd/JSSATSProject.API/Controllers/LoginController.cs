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

//[Authorize]
[Route("api/[controller]")]
[ApiController]
public class LoginController : ControllerBase
{
    private readonly IAccountService _accountService;
    private readonly IConfiguration _config;
    private readonly IMapper _mapper;


    public LoginController(IAccountService accountService, IConfiguration config, IMapper mapper)
    {
        _accountService = accountService;
        _config = config;
        _mapper = mapper;
    }

    [AllowAnonymous]
    [HttpPost]
    public ActionResult Login([FromBody] RequestSignIn userLogin)
    {
        var user = Authenticate(userLogin);
        var tokenResponse = _mapper.Map<ResponseToken>(user);
        if (user is not null)
        {
            var token = GenerateToken(user);
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
            expires: DateTime.Now.AddMinutes(30),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
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