using EducationApp.Application.Helpers.GenerateJwt;
using EducationApp.Application.Helpers.PasswordHasher;
using EducationApp.Application.Models;
using EducationApp.Application.Models.Dtos;
using EducationApp.Application.Service.IAuthServices;
using EducationApp.Core.Entities;
using EducationApp.DataAccess.Database;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using EducationApp.Application.Service.EmailSendServices;

namespace EducationApp.Application.Services
{
    public class AuthService(
        EduDbContext context,
        IPasswordHasher passwordHasher,
        IJwtTokenHandler jwtTokenHandler,
        IOtpService otpService
        ) : IAuthService
    {
        public async Task<ApiResult<LoginResponseDto>> LoginAsync(LoginRequestDto loginDto)
        {
            var user = await context.Users
                .Include(x => x.UserRoles)
                .ThenInclude(u => u.Role)
                .ThenInclude(u => u.RolePermissions)
                .ThenInclude(u => u.Permission)
                .FirstOrDefaultAsync(r => r.Email == loginDto.Email);

            if(user == null)
            {
                return ApiResult<LoginResponseDto>.Failure(new[] { "Foydalanuvchi topilmadi" });
            }

            if (!passwordHasher.Verify(user.PasswordHash, loginDto.Password, user.PasswordSolt))
                return ApiResult<LoginResponseDto>.Failure(new[] { "Parol noto‘g‘ri" });

            //if (!user.IsVerified)
            //    return ApiResult<LoginResponseDto>.Failure(new[] { "Email tasdiqlanmagan" });

            var accessToken = jwtTokenHandler.GenerateAccessToken(user, Guid.NewGuid().ToString());
            var refreshToken = jwtTokenHandler.GenerateRefreshToken();

            return ApiResult<LoginResponseDto>.Success(new LoginResponseDto
            {
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                Roles = user.UserRoles.Select(ur => ur.Role.Name).ToList(),
                Permissions = user.UserRoles
                     .SelectMany(ur => ur.Role.RolePermissions)
                     .Select(p => p.Permission.Description)
                     .Distinct()
                     .ToList()
            });
        }

        public async Task<ApiResult<string>> RegisterAsync(RegisterRequestDto requestDto)
        {
            var existingUser = await context.Users.FirstOrDefaultAsync(u => u.Email == requestDto.Email);
            if (existingUser != null)
                return ApiResult<string>.Failure(new[] { "Email allaqachon mavjud" });

            var emailValidator = new EmailAddressAttribute();
            if (!emailValidator.IsValid(requestDto.Email))
                return ApiResult<string>.Failure(new[] { "Email formati noto‘g‘ri" });

            var salt = Guid.NewGuid().ToString();
            var hash = passwordHasher.Encrypt(requestDto.Password, salt);

            var user = new User
            {
                FirstName = requestDto.FirstName,
                LastName = requestDto.LastName,
                Email = requestDto.Email,
                PasswordHash = hash,
                PasswordSolt = salt,
                Password = requestDto.Password,
                PhoneNumber = requestDto.PhoneNumber,
                
                Gender = requestDto.Gender,
                //CreatedAt = DateTime.Now,
                //IsVerified = false // Yangi foydalanuvchilar odatda tasdiqlanmagan holda boshlanadi
            };

            await context.Users.AddAsync(user);
            await context.SaveChangesAsync();

            // --- Rolni isAdminSite ga qarab belgilash ---
            string roleName = requestDto.isadminSite ? "Admin" : "User";
            var defaultRole = await context.Roles.FirstOrDefaultAsync(r => r.Name == roleName);

            if (defaultRole == null)
            {
                // Agar kerakli rol topilmasa, xato qaytaramiz
                return ApiResult<string>.Failure(new[] { $"Tizimda '{roleName}' roli topilmadi. Admin bilan bog'laning." });
            }

            context.UserRoles.Add(new UserRole
            {
                UserId = user.Id,
                RoleId = defaultRole.Id
            });
            await context.SaveChangesAsync();
            //// --- Rolni belgilash qismi tugadi ---

            var otp = await otpService.GenerateAndSaveOtpAsync(user.Id);
            //await _emailService.SendOtpAsync(email, otp);

            return ApiResult<string>.Success("Ro'yxatdan o'tdingiz."); // Email orqali tasdiqlash qo'shish
        }
    }
}
