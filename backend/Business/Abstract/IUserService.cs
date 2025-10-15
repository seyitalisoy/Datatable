using Core.Entities.Identity;
using Core.Utilities.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IUserService
    {
        IResult Add(User user);

        IDataResult<User> GetByEmail(string email);

        IDataResult<List<OperationClaim>> GetClaims(User user);
    }
}
