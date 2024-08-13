using CaseStudyAppServer.Constants;
using CaseStudyAppServer.Dtos.Admins;
using CaseStudyAppServer.Extenstions;
using CaseStudyAppServer.Helpers;
using CaseStudyAppServer.Interfaces;
using CaseStudyAppServer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CaseStudyAppServer.Controllers.Admin
{
    [Route(RouteConstants.AdminRoute)]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signinManager;
        private readonly ITokenService _tokenService;
        private readonly ITokenRepository _tokenRepo;
        public AdminController(UserManager<AppUser> userManager, ITokenService tokenService, SignInManager<AppUser> signinManager, ITokenRepository tokenRepo)
        {
            _tokenRepo = tokenRepo;
            _userManager = userManager;
            _tokenService = tokenService;
            _signinManager = signinManager;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] AdminRegisterDto registerDto)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(registerDto.Email!);

                if (user != null) return ResponseUtility.ReturnBadRequest(
                    MessageConstants.EmailTaken
                );

                if (registerDto.Password != registerDto.ConfirmPassword) return ResponseUtility.ReturnBadRequest(
                    MessageConstants.PasswordsDontMatch
                );

                var validUserName = registerDto.Email.Replace("@", string.Empty).Replace(".", string.Empty);

                var appUser = new AppUser
                {
                    UserName = validUserName,
                    DisplayName = registerDto.UserName ?? "",
                    Email = registerDto.Email,
                };

                var createdUser = await _userManager.CreateAsync(appUser, registerDto.Password!);

                if (!createdUser.Succeeded) return ResponseUtility.ReturnBadRequest(createdUser.Errors.FirstOrDefault()?.Description);

                var roleResult = await _userManager.AddToRoleAsync(appUser, "Admin");

                if (!roleResult.Succeeded) return ResponseUtility.ReturnServerError(roleResult.Errors.FirstOrDefault()?.Description);

                var token = await _tokenService.CreateToken(appUser);

                if (token == null) return ResponseUtility.ReturnServerError(null);

                var adminUser = new AdminUserDto
                {
                    UserName = appUser.DisplayName,
                    Email = appUser.Email,
                    AuthToken = token.Value,
                    RefreshToken = token.RefreshValue,
                };

                return ResponseUtility.ReturnOk(new { user = adminUser });
            }
            catch (Exception e)
            {
                return ResponseUtility.ReturnServerError(e.ToString());
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] AdminLoginDto loginDto)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(loginDto.Email!);

                if (user == null) return ResponseUtility.ReturnUnauthorized(MessageConstants.EmailOrPassIncorrent);

                var signinResult = await _signinManager.CheckPasswordSignInAsync(user, loginDto.Password ?? "", false);

                if (!signinResult.Succeeded) return ResponseUtility.ReturnUnauthorized(MessageConstants.EmailOrPassIncorrent);

                var token = await _tokenService.CreateToken(user);

                if (token == null) return ResponseUtility.ReturnServerError(null);

                var adminUser = new AdminUserDto
                {
                    UserName = user.DisplayName,
                    Email = user.Email,
                    AuthToken = token.Value,
                    RefreshToken = token.RefreshValue,
                };

                return ResponseUtility.ReturnOk(new { user = adminUser });

            }
            catch (Exception e)
            {
                return ResponseUtility.ReturnServerError(e.ToString());
            }
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Get()
        {
            try
            {
                var userEmail = User.GetUserEmail();

                if (userEmail == null) return ResponseUtility.ReturnBadRequest(MessageConstants.UnknownToken);

                var user = await _userManager.FindByEmailAsync(userEmail);

                if (user == null) return ResponseUtility.ReturnBadRequest(MessageConstants.UnknownToken);

                return ResponseUtility.ReturnOk(
                    new AdminUserGetDto
                    {
                        UserName = user.DisplayName,
                        Email = user.Email,
                    }
                );

            }
            catch (Exception e)
            {
                return ResponseUtility.ReturnServerError(e.ToString());
            }
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh()
        {
            try
            {
                if (!Request.Headers.TryGetValue("Authorization", out var tokenHeader) || tokenHeader.Count == 0 || string.IsNullOrWhiteSpace(tokenHeader.ToString()))
                    return ResponseUtility.ReturnUnauthorized(null);


                if (!Request.Headers.TryGetValue("Refresh", out var refreshTokenHeader) || refreshTokenHeader.Count == 0 || string.IsNullOrWhiteSpace(refreshTokenHeader.ToString()))
                    return ResponseUtility.ReturnUnauthorized(MessageConstants.UnknownRefreshToken);


                var token = tokenHeader.ToString().Replace("Bearer ", string.Empty);
                var refreshToken = refreshTokenHeader.ToString();



                if (token == null || token == string.Empty)
                    return ResponseUtility.ReturnUnauthorized(null);
                if (refreshToken == null || refreshToken == string.Empty)
                    return ResponseUtility.ReturnUnauthorized(MessageConstants.UnknownRefreshToken);



                var existingToken = await _tokenRepo.ValidateRefreshTokenAsync(token!, refreshToken!);
                if (existingToken == null) return ResponseUtility.ReturnBadRequest(MessageConstants.UnknownToken);

                var userEmail = User.GetUserEmail();
                if (userEmail == null) return ResponseUtility.ReturnBadRequest(MessageConstants.UnknownToken);

                var user = await _userManager.FindByEmailAsync(userEmail);
                if (user == null) return ResponseUtility.ReturnBadRequest(MessageConstants.UnknownToken);

                var newToken = await _tokenService.CreateToken(user);
                if (newToken == null) return ResponseUtility.ReturnServerError(null);

                var adminUser = new AdminUserDto
                {
                    UserName = user.DisplayName,
                    Email = user.Email,
                    AuthToken = newToken.Value,
                    RefreshToken = newToken.RefreshValue,
                };

                return ResponseUtility.ReturnOk(new { user = adminUser });

            }
            catch (Exception e)
            {
                return ResponseUtility.ReturnServerError(e.ToString());
            }
        }

        [HttpDelete("logout")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Logout()
        {
            try
            {
                var userEmail = User.GetUserEmail();

                if (userEmail == null) return ResponseUtility.ReturnBadRequest(MessageConstants.UnknownToken);

                var user = await _userManager.FindByEmailAsync(userEmail);

                if (user == null) return ResponseUtility.ReturnBadRequest(MessageConstants.UnknownToken);

                await _tokenRepo.DeleteTokensByUserIdAsync(user.Id);

                return ResponseUtility.ReturnOk(null, MessageConstants.SignedOut);

            }
            catch (Exception e)
            {
                return ResponseUtility.ReturnServerError(e.ToString());
            }
        }

        [HttpDelete()]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete()
        {
            try
            {
                var userEmail = User.GetUserEmail();

                if (userEmail == null) return ResponseUtility.ReturnBadRequest(MessageConstants.UnknownToken);

                var user = await _userManager.FindByEmailAsync(userEmail);

                if (user == null) return ResponseUtility.ReturnBadRequest(MessageConstants.UnknownToken);

                var result = await _userManager.DeleteAsync(user);
                if (!result.Succeeded) return ResponseUtility.ReturnBadRequest(result.Errors.FirstOrDefault()?.Description);

                await _tokenRepo.DeleteTokensByUserIdAsync(user.Id);

                return ResponseUtility.ReturnOk(null, MessageConstants.ItemDeleted);

            }
            catch (Exception e)
            {
                return ResponseUtility.ReturnServerError(e.ToString());
            }
        }
    }
}