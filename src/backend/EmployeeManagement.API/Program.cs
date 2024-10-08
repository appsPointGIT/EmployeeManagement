using EmployeeManagement.API.Authentication;
using EmployeeManagementApp.Core.Interfaces;
using EmployeeManagementApp.DataAccess;
using EmployeeManagementApp.DataAccess.Repositories;
using EmployeeManagementApp.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.OpenApi.Models;


namespace EmployeeManagement.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add controllers
            builder.Services.AddControllers();

            // Reg database connection
            builder.Services.AddScoped<IDataHelper>(provider => new SQLDataHelper(builder.Configuration.GetConnectionString("DefaultConnection")));

            // Reg services and repo
            builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            builder.Services.AddScoped<IDepartmentService, DepartmentService>();

            builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            builder.Services.AddScoped<IEmployeeService, EmployeeService>();


            //Add Auth
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = "Bearer";
                options.DefaultChallengeScheme = "Bearer";
            })
           .AddScheme<AuthenticationSchemeOptions, BearerTokenAuthHandler>("Bearer", null);


            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Description = "Please enter your Bearer token",
                    Name = "Authorization",
                    Scheme = "bearer",
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] {}
                    }
                });
            });

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                               .AllowAnyHeader()
                               .AllowAnyMethod();
                    });
            });

            var app = builder.Build();


            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseCors("AllowAll");

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
