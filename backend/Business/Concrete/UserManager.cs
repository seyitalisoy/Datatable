
using Business.Abstract;
using Core.Entities.Identity;
using Core.Utilities.Results;
using DataAccess.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class UserManager : IUserService
    {
        private IUserDal _userDal;

        public UserManager(IUserDal userDal)
        {
            _userDal = userDal;
        }

        public IResult Add(User user)
        {
            _userDal.Add(user);
            return new SuccessResult("Kullanıcı eklendi");
        }

        public IDataResult<User> GetByEmail(string email)
        {
            var user = _userDal.Get(u=>u.Email == email);
            if (user!=null)
            {
                return new SuccessDataResult<User>(user);
            }
            return new ErrorDataResult<User>("Kullanıcı bulunamadı");
        }

        public IDataResult<List<OperationClaim>> GetClaims(User user)
        {
            var claims = _userDal.GetClaims(user);
            if (claims.Any())
            {
                return new SuccessDataResult<List<OperationClaim>>(claims);
            }
            return new ErrorDataResult<List<OperationClaim>>("Yetki bulunamadı");
        }
    }
}
