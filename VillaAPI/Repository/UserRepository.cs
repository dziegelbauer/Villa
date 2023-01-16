using System.IdentityModel.Tokens.Jwt;
using System.Runtime.InteropServices.JavaScript;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using VillaAPI.Data;
using VillaAPI.Models;
using VillaAPI.Models.DTO;
using VillaAPI.Repository.IRepository;

namespace VillaAPI.Repository;

public class UserRepository : Repository<LocalUser>, IUserRepository
{
    private readonly ApplicationDbContext _db;
    private readonly string _secretKey;

    public UserRepository(ApplicationDbContext db, IConfiguration config) : base(db)
    {
        _db = db;
        _secretKey = config.GetValue<string>("ApiSettings:Secret")!;
    }

    public bool IsUniqueUser(string username)
    {
        return _db.LocalUsers.FirstOrDefault(u => u.UserName == username) is null;
    }

    public async Task<LoginResponseDTO?> LoginAsync(LoginRequestDTO loginRequestDTO)
    {
        var user = await GetAsync(u => 
            u.UserName == loginRequestDTO.UserName 
            && u.Password == loginRequestDTO.Password);

        if (user is null)
        {
            return new LoginResponseDTO()
            {
                Token = String.Empty,
                LocalUser = null,
            };
        }

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_secretKey);

        var tokenDescriptor = new SecurityTokenDescriptor()
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, user.Id.ToString()),
                new Claim(ClaimTypes.Role, user.Role)
            }),
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);

        var loginResponseDTO = new LoginResponseDTO()
        {
            Token = tokenHandler.WriteToken(token),
            LocalUser = user,
        };

        return loginResponseDTO;
    }

    public async Task<LocalUser?> RegisterAsync(RegistrationRequestDTO registrationRequestDTO)
    {
        LocalUser user = new()
        {
            UserName = registrationRequestDTO.UserName,
            Password = registrationRequestDTO.Password,
            Name = registrationRequestDTO.Name,
            Role = registrationRequestDTO.Role,
        };

        await CreateAsync(user);
        
        user.Password = String.Empty;

        return user;
    }

    public async Task<LocalUser> UpdateAsync(LocalUser entity)
    {
        _db.LocalUsers.Update(entity);
        await _db.SaveChangesAsync();
        return entity;
    }
}