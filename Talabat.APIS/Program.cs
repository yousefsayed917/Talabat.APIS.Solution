using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Talabat.APIS.Errors;
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
            //builder.Services.AddScoped<IGenaricRepository<Product>,GenaricRepository<Product>>();
            builder.Services.AddScoped(typeof(IGenaricRepository<>), typeof(GenaricRepository<>));//more dinamic ⁄‘«‰ „⁄œ‘ «ﬂ—— ·· »—Êœ«ﬂ  »—«œ Ê «·»—Êœ«ﬂ   «Ì» 
            builder.Services.AddAutoMapper(typeof(MappingProfiles));
            builder.Services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = (actionContext) =>
                {
                    var error = actionContext.ModelState.Where(p => p.Value.Errors.Count > 0).
                    SelectMany(p => p.Value.Errors).Select(p => p.ErrorMessage).ToArray();
                    var ValidationErrorResponse = new ApiValidationErrorResponse()
                    {
                        Errors = error
                    };
                    return new BadRequestObjectResult(ValidationErrorResponse);
                };
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
            #region kestral Pipline Medelware
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMiddleware<ExceptionMiddleWare>();
                app.UseSwagger();
                app.UseSwaggerUI();
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
