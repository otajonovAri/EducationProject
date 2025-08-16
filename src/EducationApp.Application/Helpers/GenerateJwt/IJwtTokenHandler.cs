using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EducationApp.Core.Entities;

namespace EducationApp.Application.Helpers.GenerateJwt
{
    public interface IJwtTokenHandler
    {
        string GenerateAccessToken(User user, string sessionToken);
        string GenerateRefreshToken();
    }
}
