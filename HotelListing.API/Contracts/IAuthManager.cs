using HotelListing.API.Models.Users;
using Microsoft.AspNetCore.Identity;

namespace HotelListing.API.Contracts;

public interface IAuthManager
{
    Task<IEnumerable<IdentityError>> Register(UserDto user);
    Task<IEnumerable<IdentityError>> RegisterAdmin(UserDto user);
    Task<AuthResponseDto> Login(LoginDto loginDto);
    
    Task<string> CreateRefreshToken();
    
    Task<AuthResponseDto> VerifyRefreshToken(AuthResponseDto request);
}