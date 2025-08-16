using EducationApp.Application.Repositories.Repository;
using EducationApp.Core.Entities;
using EducationApp.DataAccess.Database;

namespace EducationApp.Application.Repositories.PaymentRepository;

public class PaymentRepo(EduDbContext context) : Repository<Payment>(context), IPaymentRepository
{
}