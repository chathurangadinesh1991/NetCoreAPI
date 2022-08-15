using Microsoft.Extensions.Options;
using NetCoreAPI.Domain.Helper;
using NetCoreAPI.Domain.Models;
using NetCoreAPI.Domain.Services;
using NetCoreAPI.Persistence.Context;
using NetCoreAPI.Persistence.Models;
using System;
using System.Linq;

namespace NetCoreAPI.Application.Helper
{
    public class UserService : IUserService
    {
        private SSNDBContext _context;
        private readonly AppSettings _appSettings;

        public UserService(SSNDBContext context, IOptions<AppSettings> appSettings)
        {
            _context = context;
            _appSettings = appSettings.Value;
        }

        public AuthenticateResponse Register(RegisterRequest model, string ipAddress)
        {
            using (var dbContextTransaction = _context.Database.BeginTransaction())
            {
                try
                {
                    var refreshToken = TokenHelper.GenerateRefreshToken(ipAddress);

                    _context.Add<UserInfo>(new UserInfo
                    {
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        EmailAddress = model.Email,
                        PasswordHash = model.Password,
                        UserId = model.Email,
                        UserRole = "User"
                    });
                    _context.Add<RefreshToken>(new RefreshToken
                    {
                        UserId = model.Email,
                        IsActive = true,
                        Token = refreshToken.Token
                    });

                    int result = _context.SaveChanges();
                    if (result != 2)
                    {
                        dbContextTransaction.Rollback();
                        return null;
                    }

                    var user = new User
                    {
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        UserRole = "User",
                        Password = model.Password,
                        RefreshToken = refreshToken,
                        UserId = model.Email,
                    }; 
                    var jwtToken = TokenHelper.GenerateJwtToken(user);                    

                    dbContextTransaction.Commit();
                    return new AuthenticateResponse(user, jwtToken, refreshToken.Token);
                }
                catch (Exception)
                {
                    dbContextTransaction.Rollback();
                    return null;
                }
            }
        }

        public AuthenticateResponse Login(LoginRequest model, string ipAddress)
        {
            using (var dbContextTransaction = _context.Database.BeginTransaction())
            {
                try
                {
                    var result = _context.UserInfos.SingleOrDefault(x => x.UserId == model.UserId && x.PasswordHash == model.Password);
                    if (result == null)
                    {
                        dbContextTransaction.Commit();
                        return null;
                    }

                    var refreshToken = TokenHelper.GenerateRefreshToken(ipAddress);
                    var user = new User
                    {
                        FirstName = result.FirstName,
                        LastName = result.LastName,
                        UserRole = result.UserRole,
                        Password = result.PasswordHash,
                        RefreshToken = refreshToken,
                        UserId = result.UserId,
                    };

                    _context.Add<RefreshToken>(new RefreshToken
                    {
                        UserId = user.UserId,
                        IsActive = true,
                        Token = refreshToken.Token
                    });
                    _context.SaveChanges();

                    var jwtToken = TokenHelper.GenerateJwtToken(user);
                    dbContextTransaction.Commit();
                    return new AuthenticateResponse(user, jwtToken, refreshToken.Token);
                }
                catch (Exception)
                {
                    dbContextTransaction.Rollback();
                    return null;
                }
            }
        }

        public AuthenticateResponse RefreshToken(string token, string ipAddress)
        {
            using (var dbContextTransaction = _context.Database.BeginTransaction())
            {
                try
                {
                    var newRefreshToken = TokenHelper.GenerateRefreshToken(ipAddress);

                    var result = _context.UserInfos.SingleOrDefault(u => u.RefreshTokens.Any(t => t.Token == token));
                    if (result == null)
                    {
                        dbContextTransaction.Commit();
                        return null;
                    }

                    var refreshToken = _context.RefreshTokens.Single(x => x.Token == token && x.IsActive == true);
                    if (refreshToken == null)
                    {
                        dbContextTransaction.Commit();
                        return null;
                    }

                    result.RefreshTokens.Add(new RefreshToken
                    {
                        UserId = result.UserId,
                        Token = newRefreshToken.Token,
                        IsActive = true
                    });
                    _context.Update(result);
                    _context.SaveChanges();

                    var user = new User
                    {
                        FirstName = result.FirstName,
                        LastName = result.LastName,
                        UserRole = "User",
                        Password = result.PasswordHash,
                        RefreshToken = newRefreshToken,
                        UserId = result.UserId,
                    };

                    var jwtToken = TokenHelper.GenerateJwtToken(user);
                    dbContextTransaction.Commit();
                    return new AuthenticateResponse(user, jwtToken, newRefreshToken.Token);
                }
                catch (Exception)
                {
                    dbContextTransaction.Rollback();
                    return null;
                }
            }
        }
    }
}
