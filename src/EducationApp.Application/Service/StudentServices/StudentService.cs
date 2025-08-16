using AutoMapper;
using AutoMapper.QueryableExtensions;
using EducationApp.Application.DTOs.StudentDto;
using EducationApp.Application.Helpers.PasswordHasher;
using EducationApp.Application.Repositories.StudentRepository;
using EducationApp.Application.Responses;
using EducationApp.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace EducationApp.Application.Service.StudentServices;

public class StudentService(IStudentRepository repo, IMapper mapper, IPasswordHasher passwordHasher) : IStudentService
{
    public async Task<ApiResult<List<StudentResponseDto>>> GetAllAsync()
    {
        var students = await repo
            .GetAll()
            .ProjectTo<StudentResponseDto>(mapper.ConfigurationProvider).ToListAsync();

        return new ApiResult<List<StudentResponseDto>>("Success", true, students);
    }

    public async Task<ApiResult<StudentResponseDto>> GetByIdAsync(int id)
    {
        var student = await repo.GetByIdAsync(id);
        
        if (student is null)
            return new ApiResult<StudentResponseDto>("Student not Found", false, null!);
        
        var dto = mapper.Map<StudentResponseDto>(student);

        return new ApiResult<StudentResponseDto>("Success", true, dto);
    }

    public async Task<ApiResult<object>> CreateAsync(StudentCreateDto dto)
    {
        var entity = mapper.Map<Student>(dto);

        var salt = Guid.NewGuid().ToString();
        var hash = passwordHasher.Encrypt(dto.Password, salt);

        entity.PasswordHash = hash;
        entity.PasswordSolt = salt;

        await repo.AddAsync(entity);
        await repo.SaveChangesAsync();

        return new ApiResult<object>("Student Created", true, $"{entity.Id}");
    }

    public async Task<ApiResult<object>> UpdateAsync(int id, StudentUpdateDto dto)
    {
        var existing = await repo.GetByIdAsync(id);
        
        if (existing is null)
            return new ApiResult<object>("Student not Found", false, null!);
        
        mapper.Map(dto, existing);
        repo.Update(existing);
        await repo.SaveChangesAsync();

        return new ApiResult<object>("Student Update SuccessFully", true, $"{existing.Id}");
    }

    public async Task<ApiResult<object>> DeleteAsync(int id)
    {
        var existing = await repo.GetByIdAsync(id);
        
        if (existing is null)
            return new ApiResult<object>("Student not Found", false, null!);
        
        repo.Delete(existing);
        await repo.SaveChangesAsync();

        return new ApiResult<object>("Student Deleted SuccessFully", true, $"{existing.Id}");
    }

    public async Task<int> GetByConditionCountActiveStudents()
    {
        var studentCount = await repo.GetByCondition(s => 
            s.Status == Core.Enums.Status.Active)
            .CountAsync();

        return studentCount;
    }

    public async Task<int> GetByConditionCountUnpaidStudents()
    {
       var unpaidStudentCount = await repo.GetByCondition(s =>
            !s.Payments.Any() || s.Payments.Any(p => p.PaymentStatus == Core.Enums.PaymentStatus.Unpaid))
            .CountAsync();

        return unpaidStudentCount;
    }

    public async Task<int> GetByConditionPaidStudentsOnThisMonth()
    { 
        var paidStudentCount = await repo.GetByCondition(s =>
            s.Payments.Any(p => p.PaymentStatus == Core.Enums.PaymentStatus.Paid &&
                                p.PaymentDate.Month == DateTime.Now.Month &&
                                p.PaymentDate.Year == DateTime.Now.Year))
            .CountAsync();

        return paidStudentCount;
    }

    public async Task<int> GetByConditionCountDroppedOutStudentsActiveGroup()
    {
        var graduatedStudentCount = await repo.GetByCondition(s => 
            s.Status == Core.Enums.Status.DroppedOut && 
            s.Attendances.Any(k => k.GroupSubject.Group.GroupStatus == Core.Enums.GroupStatus.Active))
            .CountAsync();
        return graduatedStudentCount;
    }

    public async Task<ApiResult<List<StudentGetAllResponseDto>>> GetByConditionAllActiveStudents()
    {
        var activeStudents = await repo.GetByCondition(s => s.Status == Core.Enums.Status.Active)
            .Select(k => new StudentGetAllResponseDto
            {
                Id = k.Id,
                FullName = $"{k.FirstName} {k.LastName}",
                PhoneNumber = k.PhoneNumber,
                GroupName = k.Attendances.Select(r => r.GroupSubject.Group.Name).FirstOrDefault() ?? "No Group"
            })
            .ToListAsync();

        return new ApiResult<List<StudentGetAllResponseDto>>("Success", true, activeStudents);
    }

    public async Task<ApiResult<List<StudentGetAllResponseDto>>> GetByConditionAllInActiveStudents()
    {
        var inActiveStudents = await repo.GetByCondition(s => s.Status == Core.Enums.Status.Inactive)
             .Select(k => new StudentGetAllResponseDto
             {
                 Id = k.Id,
                 FullName = $"{k.FirstName} {k.LastName}",
                 PhoneNumber = k.PhoneNumber,
                 GroupName = k.Attendances.Select(r => r.GroupSubject.Group.Name).FirstOrDefault() ?? "No Group"
             })
             .ToListAsync();

        return new ApiResult<List<StudentGetAllResponseDto>>("Success", true, inActiveStudents);
    }

    public async Task<ApiResult<bool>> GetByConditionReturnLastPayment(int studentId)
    {
        var student = await repo.GetByCondition(s => s.Id == studentId)
            .Include(k => k.Payments)
            .FirstOrDefaultAsync();

        if(student is null)
            return new ApiResult<bool>("Student not found", false, false);

        var lastPayment =  student.Payments
            .Where(p => !p.IsDeleted)
            .OrderByDescending(p => p.Id)
            .FirstOrDefault();

        if (lastPayment is null)
            return new ApiResult<bool>("No payments found for this student", false, false);

        lastPayment.IsDeleted = true;

        repo.Update(student);
        await repo.SaveChangesAsync();

        return new ApiResult<bool>("Last payment returned successfully", true, true);
    }
}