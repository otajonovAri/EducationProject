using EducationApp.Application.Common;
using EducationApp.Application.Helpers.GenerateJwt;
using EducationApp.Application.Helpers.PasswordHasher;
using EducationApp.Application.Helpers.Seeder;
using EducationApp.Application.Repositories.AttendanceRepository;
using EducationApp.Application.Repositories.FileRepository;
using EducationApp.Application.Repositories.GroupRepository;
using EducationApp.Application.Repositories.GroupSubjectRepository;
using EducationApp.Application.Repositories.PaymentDocumentRepository;
using EducationApp.Application.Repositories.PaymentRepository;
using EducationApp.Application.Repositories.RoleRepository;
using EducationApp.Application.Repositories.RoomRepository;
using EducationApp.Application.Repositories.StaffRepository;
using EducationApp.Application.Repositories.StaffSubjectRepository;
using EducationApp.Application.Repositories.StudentRepository;
using EducationApp.Application.Repositories.SubjectRepository;
using EducationApp.Application.Repositories.UserRepository;
using EducationApp.Application.Service.AttendanceServices;
using EducationApp.Application.Service.EmailSendServices;
using EducationApp.Application.Service.FileStorageServices;
using EducationApp.Application.Service.GroupServices;
using EducationApp.Application.Service.GroupSubjectServices;
using EducationApp.Application.Service.IAuthServices;
using EducationApp.Application.Service.PaymentDocumentServices;
using EducationApp.Application.Service.PaymentServices;
using EducationApp.Application.Service.PermissionServices;
using EducationApp.Application.Service.RoleServices;
using EducationApp.Application.Service.RoomServices;
using EducationApp.Application.Service.StaffServices;
using EducationApp.Application.Service.StaffSubjectServices;
using EducationApp.Application.Service.StudentServices;
using EducationApp.Application.Service.SubjectServices;
using EducationApp.Application.Service.UserServices;
using EducationApp.Application.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EducationApp.Application.DIContainer;

public static class DependencyInjection
{
    public static IServiceCollection ServiceContainer(this IServiceCollection services)
    {
        // Attendance
        services.AddScoped<IAttendanceRepository, AttendanceRepo>();
        services.AddScoped<IAttendanceService, AttendanceService>();

        // Group
        services.AddScoped<IGroupRepository, GroupRepository>();
        services.AddScoped<IGroupService, GroupService>();

        // GroupSubject
        services.AddScoped<IGroupSubjectRepository, GroupSubjectRepo>();
        services.AddScoped<IGroupSubjectService, GroupSubjectService>();

        // PaymentDocument
        services.AddScoped<IPaymentDocumentRepository, PaymentDocumentRepo>();
        services.AddScoped<IPaymentDocumentService, PaymentDocumentService>();

        // Payment
        services.AddScoped<IPaymentRepository, PaymentRepo>();
        services.AddScoped<IPaymentService, PaymentService>();

        // Room
        services.AddScoped<IRoomRepository, RoomRepo>();
        services.AddScoped<IRoomService, RoomService>();

        // Staff
        services.AddScoped<IStaffRepository, StaffRepo>();
        services.AddScoped<IStaffService, StaffService>();

        // StaffSubject
        services.AddScoped<IStaffSubjectRepository, StaffSubjectRepo>();
        services.AddScoped<IStaffSubjectService, StaffSubjectService>();

        // Student
        services.AddScoped<IStudentRepository, StudentRepo>();
        services.AddScoped<IStudentService, StudentService>();

        // Subject
        services.AddScoped<ISubjectRepository, SubjectRepo>();
        services.AddScoped<ISubjectService, SubjectService>();

        // User
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUserService, UserService>();

        // Role
        services.AddScoped<IRoleRepository, RoleRepo>();
        services.AddScoped<IRoleService, RoleService>();

        // Image Upload
        services.AddScoped<IFileRepository , FileRepository>();
        services.AddScoped<IFileStorageService, MinioFileStorageService>();

        // Jwt 
        services.AddScoped<IJwtTokenHandler, JwtTokenHandler>();
        services.AddScoped<IPasswordHasher, PasswordHasher>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IPermissionService, PermissionService>();

        // Seeding 
        services.AddScoped<AppSeeder>();
        services.AddScoped<PermissionSeeder>();
        services.AddScoped<RoleSeeder>();
        services.AddScoped<AdminUserSeeder>();
        services.AddScoped<RolePermissionSeeder>();
        services.AddScoped<UserRoleSeeder>();

        // Email Sending 
        services.AddScoped<IEmailService, EmailService>();
        services.AddScoped<IOtpService, OtpService>();

        return services;
    }

    public static IServiceCollection AddJwtOption(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<JwtOption>(configuration.GetSection("JwtOption"));
        return services;
    }

    public static IServiceCollection AddEmailConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<EmailConfiguration>(configuration.GetSection("EmailConfiguration"));

        return services;

    }
}
