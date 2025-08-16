using AutoMapper;
using AutoMapper.QueryableExtensions;
using EducationApp.Application.DTOs.UserDto;
using EducationApp.Application.Helpers.PasswordHasher;
using EducationApp.Application.Repositories.UserRepository;
using EducationApp.Application.Responses;
using EducationApp.Application.Service.EmailSendServices;
using EducationApp.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace EducationApp.Application.Service.UserServices;

public class UserService(
    IUserRepository repo, 
    IMapper mapper,
    IPasswordHasher passwordHasher,
    IOtpService otpService) : IUserService
{
    public async Task<ApiResult<List<UserResponseDto>>> GetAllAsync()
    {
       var users = await repo.GetAll()
           .ProjectTo<UserResponseDto>(mapper.ConfigurationProvider).ToListAsync();

       return new ApiResult<List<UserResponseDto>>("Success", true, users);
    }


    public async Task<ApiResult<string>> VerifyOtpAsync(OtpVerificationModel model)
    {
        var user = await repo.EmailFindAsync(model.Email);
        if (user is null)
            return new ApiResult<string>
            {
                Message = "Foydalanuvchi topilmadi",
                Success = false,
                Data = null!
            };

        var otp = await otpService.GetLatestOtpAsync(user.Id, model.Code);
        if (otp is null || otp.ExpiredAt > DateTime.Now)
            return new ApiResult<string> { Message = "Kod notogri yoki muddati tugagan.", Success = false };

        user.IsVerified = true;
        await repo.SaveChangesAsync();

        return new ApiResult<string>()
        {
            Message = "Email tasdiqlandi",
            Success = true,
        };

    }



    public async Task<ApiResult<UserResponseDto>> GetByIdAsync(int id)
    {
       var user = await repo.GetByIdAsync(id);

       if (user is null)
           return new ApiResult<UserResponseDto>("User not Found", false, null!);

       var dto = mapper.Map<UserResponseDto>(user);

       return new ApiResult<UserResponseDto>("Success", true, dto);
    }

    public async Task<ApiResult<object>> CreateAsync(UserCreateDto dto)
    {
        var entity = mapper.Map<User>(dto);

        var exisstingUser = await repo.GetByCondition(u => u.Email == dto.Email).FirstOrDefaultAsync();
        if(exisstingUser is not null)
        {
            return new ApiResult<object>("Email already exists", false, null!);
        }

        var salt = Guid.NewGuid().ToString();
        var hash = passwordHasher.Encrypt(dto.Password, salt);

        entity.PasswordHash = hash;
        entity.PasswordSolt = salt;

        await repo.AddAsync(entity);
        await repo.SaveChangesAsync();

        return new ApiResult<object>("User Created", true, $"{entity.Id}");
    }

    public async Task<ApiResult<object>> UpdateAsync(int id, UserUpdateDto dto)
    {
        var existing = await repo.GetByIdAsync(id);

        if (existing is null)
            return new ApiResult<object>("User not Found", false, null!);

        mapper.Map(dto, existing);
        repo.Update(existing);
        await repo.SaveChangesAsync();

        return new ApiResult<object>("User Update SuccessFully", true, $"{existing.Id}");
    }

    public async Task<ApiResult<object>> DeleteAsync(int id)
    {
        var existing = await repo.GetByIdAsync(id);

        if (existing is null)
            return new ApiResult<object>("User not Found", false, null!);

        repo.Delete(existing);
        await repo.SaveChangesAsync();

        return new ApiResult<object>("User Deleted Successfully", true, existing);
    }
}