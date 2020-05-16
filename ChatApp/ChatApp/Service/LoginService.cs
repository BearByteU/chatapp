using ChatApp.ISerivce;
using ChatApp.Models;
using ChatApp.SRMDbContexts.IRepository;
using ChatApp.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Service
{
    public class LoginService : ILoginService
    {
        #region DI
        private readonly IUnitOfWork _unitOfWork;
        private IConfiguration _config;
        public LoginService(IUnitOfWork unitOfWork,IConfiguration config)
        {
            _config = config;
            _unitOfWork = unitOfWork;
        }
        #endregion

        #region Fetch Login Credential
        public async Task<EntityResponseModel> FetchLoginCredential(LoginDto login)
        {
            try
            {
                var user = _unitOfWork.UserRepository.Find().Where(x => x.email == login.Email && x.password == login.Password).FirstOrDefault();

                if (user == null)
                {
                    return new EntityResponseModel
                    {
                        Status =false,
                        Message = Constants.ConstantString.EmailNotFound,
                    };
                }
                else
                {
                    var authToken = GenerateToken(user, login.Password);
                    return new EntityResponseModel
                    {
                        Message = Constants.ConstantString.Success,
                        Status = true,
                        Token = authToken
                    };
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Generate Token
        private string GenerateToken(User user, string password)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("userId", user.Id.ToString()),
                    new Claim("email", user.email)
                    
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = credentials
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
        #endregion
    }
}
