
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
using Store.G02.Api.Extentions;
using Store.G02.Api.Middlewares;
using System.Threading.Tasks;

namespace Store.G02.Api
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            
            builder.Services.RegisterAllServices(builder.Configuration);


            var app = builder.Build();


          await  app.ConfigureMiddlewares();




            app.Run();
        }
    }
}
