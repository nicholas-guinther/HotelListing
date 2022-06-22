using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using HotelListing.API.Contracts;
using HotelListing.API.Data;
using HotelListing.API.Models.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Npgsql.EntityFrameworkCore.PostgreSQL.Query.ExpressionTranslators.Internal;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace HotelListing.API.Repository;

public class AuthManager : IAuthManager
{
    private readonly IConfiguration _configuration;
    private readonly IMapper _mapper;
    private readonly UserManager<User> _userManager;
    private User _user;
    private const string _loginProvider = "HotelListingAPi";
    private const string _refreshToken = "RefreshToken";
    
    public AuthManager(IMapper mapper, UserManager<User> userManager, IConfiguration configuration)
    {
        _mapper = mapper;
        _userManager = userManager;
        _configuration = configuration;
    }

    public async Task<IEnumerable<IdentityError>> Register(UserDto userDto)
    {
        _user = _mapper.Map<User>(userDto);

        _user.UserName = userDto.Email;

        var result = await _userManager.CreateAsync(_user, userDto.Password);

        if (result.Succeeded)
        {
            await _userManager.AddToRoleAsync(_user, "User");
        }

        return result.Errors;
    }

    public async Task<IEnumerable<IdentityError>> RegisterAdmin(UserDto userDto)
    {
        _user = _mapper.Map<User>(userDto);

        _user.UserName = userDto.Email;

        var result = await _userManager.CreateAsync(_user, userDto.Password);

        if (result.Succeeded)
        {
            await _userManager.AddToRoleAsync(_user, "Administrator");
        }

        return result.Errors;
    }

    public async Task<AuthResponseDto> Login(LoginDto loginDto)
    { 
        _user = await _userManager.FindByEmailAsync(loginDto.Email);
        bool isValidUser = await _userManager.CheckPasswordAsync(_user, loginDto.Password);
            
        if (_user ==  null | isValidUser == false)
        {
            return null;
        }
        var token = await GenerateToken();
        return new AuthResponseDto
        {
            Token = token,
            UserId = _user.Id,
            RefreshToken = await CreateRefreshToken()
        };
    }

    public async Task<string> CreateRefreshToken()
    {
        await _userManager.RemoveAuthenticationTokenAsync(_user, _loginProvider,
            _refreshToken);
        var newRefreshToken = await _userManager.GenerateUserTokenAsync(_user, _loginProvider,
            _refreshToken);

        var result = await _userManager.SetAuthenticationTokenAsync(_user, _loginProvider,
            _refreshToken, newRefreshToken);
        
        return newRefreshToken;

    }

    public async Task<AuthResponseDto> VerifyRefreshToken(AuthResponseDto request)
    {
        var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();

        var tokenContent = jwtSecurityTokenHandler.ReadJwtToken(request.Token);

        var userName = tokenContent.Claims.ToList().FirstOrDefault(q => q.Type == 
                                                                        JwtRegisteredClaimNames.Sub)?.Value;
        _user = await _userManager.FindByNameAsync(userName);

        if (_user == null || _user.Id != request.UserId)
        {
            return null;
        }

        var isValidRefreshToken =
            await _userManager.VerifyUserTokenAsync(_user, _loginProvider,
                _refreshToken, request.RefreshToken);
        if (isValidRefreshToken)
        {
            var token = await GenerateToken();
            return new AuthResponseDto
            {
                Token = token,
                UserId = _user.Id,
                RefreshToken = await CreateRefreshToken()
            };
        }

        await _userManager.UpdateSecurityStampAsync(_user);
        
        return null;
    }

    private async Task<string> GenerateToken()
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration
            ["JwtSettings:Key"]));

        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var roles = await _userManager.GetRolesAsync(_user);

        var roleClaims = roles.Select(x => new Claim(ClaimTypes.Role, x)).ToList();

        var userClaims = await _userManager.GetClaimsAsync(_user);

        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, _user.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Email, _user.Email)
        }.Union(userClaims).Union(roleClaims);

        var token = new JwtSecurityToken(
            issuer: _configuration["JwtSettings:Issuer"],
            audience: _configuration["JwtSettings:Audience"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(Convert.ToInt32(_configuration["JwtSettings:DurationInMinutes"])),
            signingCredentials: credentials
            );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}