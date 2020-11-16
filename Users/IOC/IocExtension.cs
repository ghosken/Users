using Domain.Interfaces.Repos;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace IOC
{
    public class IocExtension
    {
        public static void Configure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<PrincipalContext>(opt =>
            opt.UseInMemoryDatabase("Cadastro"));
            //opt.UseSqlServer(Configuration.GetConnectionString("CnSqlServer")));

            //services.AddScoped<IUserRepo, UserDapperRepo>();
            services.AddScoped<IUserRepo, UserRepo>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IUserService, UserService>();

        }
    }
}
