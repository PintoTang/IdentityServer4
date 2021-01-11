using Autofac;
using IdentityServer.IService;
using IdentityServer.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SqlSugarTool;

namespace IdentityServer
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSqlSugarClient();
            services.AddIdentityServer(options => {
                options.Events.RaiseErrorEvents = true;
                options.Events.RaiseInformationEvents = true;
                options.Events.RaiseFailureEvents = true;
                options.Events.RaiseSuccessEvents = true;
                options.IssuerUri = "http://localhost:5000";
            })//ע�����
                .AddDeveloperSigningCredential()
                .AddInMemoryIdentityResources(Config.GetIdentityResources())
                .AddInMemoryApiResources(Config.GetApiResources())
                .AddInMemoryApiScopes(Config.ApiScopes())
                .AddInMemoryClients(Config.GetClients())
                .AddResourceOwnerValidator<ResourceOwnerPasswordValidator>();
            services.AddControllers();
            services.AddScoped<IAccountService, AccountService>();
        }

        /// <summary>
        /// ʹ�ù��ܷḻ��Autofac�������Ĭ������
        /// </summary>
        /// <param name="builder"></param>
        public void ConfigureContainer(ContainerBuilder builder)
        {
            //�Խӿڷ�ʽע��ִ���(ȫ��Ҫ�Խӿ���ʽע��)
            builder.RegisterGeneric(typeof(Repository<>)).As(typeof(IRepository<>)).AsImplementedInterfaces();
            var webAssemblytype = typeof(Program).Assembly;
            builder.RegisterAssemblyTypes(webAssemblytype).PropertiesAutowired();
        }
        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseIdentityServer();//����м��

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
