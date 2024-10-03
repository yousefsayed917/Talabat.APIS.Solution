namespace Talabat.APIS.Extensions
{
    public  static class AddSwaggerExtentios
    {
        public static WebApplication UseSwaggerMiddleWares(this WebApplication app)
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            return app;
        }
    }
}
