
using Business.Abstract;
using Core.Entities.Identity;
using Core.Utilities.Results;
using Entities.DTOs;
using Core.Utilities.Security.Hashing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Utilities.Security.Jwt;

namespace Business.Concrete
{
    public class AuthManager : IAuthService
    {
        private IUserService _userService;
        private ITokenHelper _tokenHelper;

        public AuthManager(IUserService userService, ITokenHelper tokenHelper)
        {
            _userService = userService;
            _tokenHelper = tokenHelper;
        }

        public IDataResult<AccessToken> CreateAccessToken(User user)
        {
            var claims = _userService.GetClaims(user);
            var accessToken = _tokenHelper.CreateToken(user,claims.Data);
            return new SuccessDataResult<AccessToken>(accessToken, "Access Token Oluşturuldu");
        }

        public IDataResult<User> Login(UserLoginDto userLoginDto)
        {
            var userToCheck = _userService.GetByEmail(userLoginDto.Email);

            if (!userToCheck.Success)
            {
                return new ErrorDataResult<User>(userToCheck.Message);
            }
            if (!HashingHelper.VerifyPasswordHash(userLoginDto.Password,userToCheck.Data.PasswordHash,userToCheck.Data.PasswordSalt))
            {
                return new ErrorDataResult<User>("Email veya şifre yanlış");
            }
            return new SuccessDataResult<User>(userToCheck.Data,"Giriş yapıldı");
        }

        public IDataResult<User> Register(UserRegisterDto userRegisterDto)
        {
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(userRegisterDto.Password, out passwordHash, out passwordSalt);
            var user = new User
            {
                Email = userRegisterDto.Email,
                FirstName = userRegisterDto.FirstName,
                LastName = userRegisterDto.LastName,
                Status = true,
                PasswordSalt = passwordSalt,
                PasswordHash = passwordHash
            };
            _userService.Add(user);
            return new SuccessDataResult<User>(user, "Kullanıcı eklendi");
        }
        public IResult UserExists(string email)
        {
            var user = _userService.GetByEmail(email);
            if (user.Success)
            {
                return new SuccessResult();
            }
            return new ErrorResult();
        }

    }
}
