using IdentityServer.IService;
using IdentityServer.Model;
using SqlSugarTool;
using System;
using System.Threading.Tasks;

namespace IdentityServer.Service
{
    public class AccountService : IAccountService
    {
        private IRepository<UserInfo> respoity;
        public AccountService(IRepository<UserInfo> _respoity)
        {
            respoity = _respoity;
        }

        public async Task<AccountResult> LoginIn(string Id, string password)
        {
            AccountResult account = new AccountResult();
            try
            {
                var user = await respoity.GetFirstAsync(x => x.UserId == Id && x.PassWord == password);
                if (user != null)
                {
                    account.Status = "登录成功";
                    account.User = user;
                    return account;
                }
                else
                {
                    account.Status = "登录失败";
                    account.Message = "账号或者密码不正确";
                    return account;
                }
            }
            catch (Exception ex)
            {
                account.Status = "登陆失败";
                account.Message = $"异常信息：{ex.Message}{System.Environment.NewLine}堆栈信息{ex.StackTrace}";
                return account;
            }
        }


    }
}
