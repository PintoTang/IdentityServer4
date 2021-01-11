using IdentityModel;
using IdentityServer.IService;
using IdentityServer.Model;
using IdentityServer4.Models;
using IdentityServer4.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace IdentityServer
{
    public class ResourceOwnerPasswordValidator: IResourceOwnerPasswordValidator
    {
        private readonly IAccountService accountService;

        public ResourceOwnerPasswordValidator(IAccountService _accountService)
        {
            accountService = _accountService;
        }

        public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            var accountResult = await accountService.LoginIn(context.UserName, context.Password);
            if (accountResult.Status == "登录成功")
            {
                context.Result = new GrantValidationResult
                    (accountResult.User.UserId, "admin", GetUserClaims(accountResult.User));
            }
            else
            {
                //验证失败
                context.Result = new GrantValidationResult
                    (TokenRequestErrors.InvalidGrant, accountResult.Message);
            }
        }

        public Claim[] GetUserClaims(UserInfo userInfo)
        {
            var claims = new Claim[] 
            { 
                new Claim("USERID", userInfo.UserId), 
                new Claim("USERNAME", userInfo.UserName),
                new Claim("USERROLES", userInfo.UserRoles)
            };
            return claims;
        }

    }
}
