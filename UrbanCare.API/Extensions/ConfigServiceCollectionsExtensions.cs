using System.Text;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using UrbanCare.Application.Features.UserOperations.Commands;
using UrbanCare.Application.Interfaces;
using UrbanCare.Application.Services;
using UrbanCare.Domain.Enums;
using UrbanCare.Domain.Interfaces.Repositories;
using UrbanCare.Domain.Interfaces.Security;
using UrbanCare.Infrastructure.Options;
using UrbanCare.Infrastructure.Persistance;
using UrbanCare.Infrastructure.Persistance.Repositories;
using UrbanCare.Infrastructure.Security;

namespace UrbanCare.API.Extensions
{
    public static class ConfigServiceCollectionsExtensions
    {
        public static IServiceCollection AddDependencies(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseMySql(configuration.GetConnectionString("Test"),
                ServerVersion.Parse("8.0.36-mysql")));

            services.Configure<JwtOptions>(configuration.GetSection(nameof(JwtOptions)));

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserPersonalDataRepository, UserPersonalDataRepository>();
            services.AddScoped<IPassportDataRepository, PassportDataRepository>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IManagementCompanyRepository, ManagementCompanyRepository>();
            services.AddScoped<IEmployeePositionRepository, EmployeePositionRepository>();
            services.AddScoped<IQualificationCategoryRepository, QualificationCategoryRepository>();
            services.AddScoped<IEmployeeStatusRepository, EmployeeStatusRepository>();
            services.AddScoped<IRegionRepository, RegionRepository>();
            services.AddScoped<IBuildingRepository, BuildingRepository>();
            services.AddScoped<IApartmentRepository, ApartmentRepository>();
            services.AddScoped<IBuildingTypeRepository, BuildingTypeRepository>();
            services.AddScoped<IFloorMaterialRepository, FloorMaterialRepository>();
            services.AddScoped<IWallMaterialRepository, WallMaterialRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IResidentRepository, ResidentRepository>();
            services.AddScoped<IPriorityRepository, PriorityRepository>();
            services.AddScoped<IOrderTypeRepository, OrderTypeRepository>();
            services.AddScoped<IOrderCategoryRepository, OrderCategoryRepository>();
            services.AddScoped<IOrderStatusRepository, OrderStatusRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();

            services.AddScoped<GettingDataService>();

            services.AddScoped<IHasher, SHA512Hasher>();
            services.AddScoped<IJwtProvider, JwtProvider>();

            services.AddMediatR(cfg =>
                cfg.RegisterServicesFromAssembly(typeof(RegistrationCommand).Assembly));

            services.AddValidatorsFromAssembly(typeof(RegistrationCommand).Assembly);

            return services;
        }

        public static void AddApiAuthentification(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var jwtOptions = configuration.GetSection(nameof(JwtOptions)).Get<JwtOptions>();
            if (jwtOptions is null)
                throw new ArgumentNullException(nameof(jwtOptions));

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
                {
                    options.TokenValidationParameters = new()
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SecretKey))
                    };
                });

            services.AddAuthorization(option =>
            {
                option.AddPolicy("AdminPolicy", policy =>
                {
                    policy.RequireClaim("roleId", ((int)RolesEnum.Admin).ToString());
                });
                option.AddPolicy("EmployeePolicy", policy =>
                {
                    policy.RequireAssertion(context =>
                    {
                        var roleClaim = context.User.FindFirst("roleId");
                        if (roleClaim != null && int.TryParse(roleClaim.Value, out int roleId))
                        {
                            return roleId < (int)RolesEnum.Resident;
                        }
                        return false;
                    });
                });
                option.AddPolicy("ResidentPolicy", policy =>
                {
                    policy.RequireClaim("roleId", ((int)RolesEnum.Resident).ToString());
                });
                option.AddPolicy("DispatcherPolicy", policy =>
                {
                    policy.RequireClaim("roleId", ((int)RolesEnum.Dispatcher).ToString());
                });
            });
        }
    }
}
