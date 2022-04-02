using System;
using DecaBlog.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DecaBlog.Extensions
{
    public static class DbContextExtension
    {
        private static string GetHerokuConnectionString(IConfiguration Configuration)
        {
            // Get the Database URL from the ENV variables in Heroku
            string connectionUrl = Environment.GetEnvironmentVariable("DATABASE_URL");

            // parse the connection string
            if (string.IsNullOrWhiteSpace(connectionUrl))
            {
                //Use this for connection string for simulated production environment
                return Configuration.GetConnectionString("ConNpg");
            }

            var databaseUri = new Uri(connectionUrl);
            string db = databaseUri.LocalPath.TrimStart('/');
            string[] userInfo = databaseUri.UserInfo.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);

            return $"User ID={userInfo[0]};Password={userInfo[1]};Host={databaseUri.Host};Port={databaseUri.Port};" +
                   $"Database={db};Pooling=true;SSL Mode=Require;Trust Server Certificate=True;";
        }

        public static void AddDbContextExtension(this IServiceCollection services, IConfiguration config, IWebHostEnvironment _env)
        {
            //if (_env.IsDevelopment())
            //{
            services.AddDbContextPool<DecaBlogDbContext>(options =>
            options.UseSqlite(config.GetConnectionString("Default")));

            //}
            //else
            //{
            //services.AddDbContext<DecaBlogDbContext>(option => option.UseNpgsql(GetHerokuConnectionString(config)));
            //}
        }
    }
}
