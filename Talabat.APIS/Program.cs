using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using Talabat.APIS.Errors;
using Talabat.APIS.Extensions;
using Talabat.APIS.Helper;
using Talabat.APIS.MiddleWares;
using Talabat.Core.Eintites;
using Talabat.Core.Repositories;
using Talabat.Repository;
using Talabat.Repository.Data;

namespace Talabat.APIS
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);//Create Host
            #region Configure Services
            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<TalabatDbContext>(option =>
            {
                option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            builder.Services.AddAppServices();
            builder.Services.AddSingleton<IConnectionMultiplexer>(Options =>
            {
                var Connection = builder.Configuration.GetConnectionString("RedisConnection");
                return ConnectionMultiplexer.Connect(Connection);
            });
            #endregion
            #region Build Project
            var app = builder.Build(); //Build the project
            #endregion
            #region Update-Database
            //TalabatDbContext dbContext = new TalabatDbContext();
            //await dbContext.Database.MigrateAsync();
            //group of services scoped
            using var Scope = app.Services.CreateScope();
            //Service it self
            var Services = Scope.ServiceProvider;
            //Ask CLR for creating object from Dbcontext Explicitly
            var LoggerFactory = Services.GetRequiredService<ILoggerFactory>();
            try
            {
                var Dbcontext = Services.GetRequiredService<TalabatDbContext>();
                await Dbcontext.Database.MigrateAsync();
                await TalabatDbContextSeed.SeedAsync(Dbcontext);
            }
            catch (Exception ex)
            {
                var Logger = LoggerFactory.CreateLogger<Program>();
                Logger.LogError(ex, "An Error Occured During Aplling the Migration");
            }
            #endregion
            #region kestral Pipline MiddleWare
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMiddleware<ExceptionMiddleWare>();
                app.UseSwaggerMiddleWares(); 
            }
            app.UseStatusCodePagesWithRedirects("/errors/{0}");
            app.UseStaticFiles();
            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();


            #endregion
            app.Run();
        }
    }
}
