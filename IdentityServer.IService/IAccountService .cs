using IdentityServer.Model;
using System.Threading.Tasks;

namespace IdentityServer.IService
{
    public interface IAccountService
    {
        Task<AccountResult> LoginIn(string Id, string password);
    }
}
