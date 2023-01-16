using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using VillaAPI.Models;
using VillaAPI.Models.DTO;
using VillaAPI.Repository.IRepository;

namespace VillaAPI.Controllers;

[Route("api/UsersAuth")]
[ApiController]
public class UsersController : Controller
{
    private readonly IUserRepository _userRepository;

    public UsersController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    
    [HttpPost("login")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Login([FromBody] LoginRequestDTO loginRequestDTO)
    {
        var loginResponse = await _userRepository.LoginAsync(loginRequestDTO);

        if (loginResponse?.LocalUser is null || loginResponse.Token.IsNullOrEmpty())
        {
            return BadRequest(new APIResponse()
            {
                IsSuccessful = false,
                StatusCode = HttpStatusCode.BadRequest,
                ErrorMessages = new()
                {
                    "Invalid login"
                },
            });
        }
        
        return Ok(new APIResponse()
        {
            IsSuccessful = true,
            StatusCode = HttpStatusCode.OK,
            Result = loginResponse,
        });
    }
    
    [HttpPost("register")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Register([FromBody] RegistrationRequestDTO registrationRequestDTO)
    {
        if (!_userRepository.IsUniqueUser(registrationRequestDTO.UserName))
        {
            return BadRequest(new APIResponse()
            {
                IsSuccessful = false,
                StatusCode = HttpStatusCode.BadRequest,
                ErrorMessages = new()
                {
                    "Username unavailable."
                }
            });
        }

        var user = await _userRepository.RegisterAsync(registrationRequestDTO);

        if (user is null)
        {
            return BadRequest(new APIResponse()
            {
                IsSuccessful = false,
                StatusCode = HttpStatusCode.BadRequest,
                ErrorMessages = new()
                {
                    "Error while registering."
                }
            });
        }
        
        return Ok(new APIResponse()
        {
            IsSuccessful = true,
            StatusCode = HttpStatusCode.OK,
            Result = user,
        });
    }
}