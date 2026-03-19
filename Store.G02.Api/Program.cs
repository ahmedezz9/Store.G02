
using Domain.Contracts;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
//using Microsoft.IdentityModel.Tokens.Experimental;
using Persistence;
using Persistence.Data;
using Services;
using Services.Abstractions;
using Services.MappingProfiles;
using Shared.ErrorsModels;
using Store.G02.Api.Middlewares;
using System.Threading.Tasks;

namespace Store.G02.Api
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<StoreDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            builder.Services.AddScoped<IDbInitializer, DbInitializer>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            //builder.Services.AddAutoMapper(typeof(AsssemblyRefrence).Assembly);
            builder.Services.AddAutoMapper(cfg => { }, typeof(AsssemblyRefrence).Assembly);
            builder.Services.AddScoped<IServiceManger, ServiceManger>();

            builder.Services.Configure<ApiBehaviorOptions>
                (
                config =>
                {
                    config.InvalidModelStateResponseFactory = (actionContext) =>
                    {
                        var errors = actionContext.ModelState.Where(m => m.Value.Errors.Any())
                                                              .Select(m => new ValidationError()
                                                              {
                                                                  Field = m.Key,
                                                                  Errors = m.Value.Errors.Select(errors => errors.ErrorMessage)
                                                              });
                        var response = new ValidationErrorResponse() { Errors = errors};
                        return new BadRequestObjectResult(response);
                    };



                });

            var app = builder.Build();

            using var scope = app.Services.CreateScope();
            var dbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
            await dbInitializer.InitializeAsync();

            app.UseMiddleware<GlobalErrorHandlingMiddleware>();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseStaticFiles();
            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
