using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System.Collections.Generic;

namespace IdentityServer
{
    public class Config
    {
        /// <summary>
        /// 定义资源范围
        /// </summary>
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
              {
                new ApiResource("api1", "我的第一个API")
              };
        }

        /// <summary>
        /// 定义访问的资源客户端
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
              {
                new Client
                {
                  ClientId="client",//定义客户端ID
                  ClientSecrets={ new Secret("secret".Sha256()) },//定义客户端秘钥
                  AllowedGrantTypes=GrantTypes.ResourceOwnerPassword,//授权方式为用户密码模式授权
                  AllowedScopes={ "api1" }//AllowedScopes必须是ApiScopes中的值，否则报"invalid_scope"错误 
                }
              };
        }

        public static IEnumerable<ApiScope> ApiScopes()
        {
            return new ApiScope[]
            {
                new ApiScope("api1"),
                new ApiScope("api2"),
            };
        }

        /// <summary>
        /// 这个方法是来规范tooken生成的规则和方法的。一般直接采用默认的即可。
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile()
            };
        }

        public static List<TestUser> GetUsers()
        {
            return new List<TestUser>
             {
                 new TestUser
                 {
                     Username = "test",
                     Password = "123456",
                     SubjectId = "1"
                 }
             };
        }
    }
}
