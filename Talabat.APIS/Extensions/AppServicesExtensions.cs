using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;
using Talabat.APIS.Errors;
using Talabat.APIS.Helper;
using Talabat.Core.Repositories;
using Talabat.Repository;

namespace Talabat.APIS.Extensions
{
    public static class AppServicesExtensions
    {
        public static IServiceCollection AddAppServices(this IServiceCollection Services)
        {
            //builder.Services.AddScoped<IGenaricRepository<Product>,GenaricRepository<Product>>();
            Services.AddScoped(typeof(IGenaricRepository<>), typeof(GenaricRepository<>));//more dinamic عشان معدش اكرر لل بروداكت براد و البروداكت تايب 
            Services.AddAutoMapper(typeof(MappingProfiles));
            Services.AddScoped(typeof(IBasketRepository),typeof(BasketRepository));
            Services.Configure<ApiBehaviorOptions>(options =>
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
            return Services;
        }
    }
}
