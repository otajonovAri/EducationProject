using EducationApp.Application.Repositories.GroupSubjectRepository;
using EducationApp.Application.Repositories.PaymentRepository;
using EducationApp.Application.Repositories.Repository;
using EducationApp.Core.Entities;
using EducationApp.DataAccess.Database;

namespace EducationApp.Application.Repositories.PaymentDocumentRepository;

public class PaymentDocumentRepo(EduDbContext context) 
    : Repository<PaymentDocument>(context), IPaymentDocumentRepository
{
}