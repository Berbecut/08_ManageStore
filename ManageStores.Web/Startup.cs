using FluentValidation;
using FluentValidation.AspNetCore;
using ManageStores.Infrastructure.Repositories.Implementations.AppImplementations;
using ManageStores.Infrastructure.Repositories.Interfaces.AppInterfaces;
using ManageStores.Core.Models;
using ManageStores.Infrastructure.DataAccess;
using ManageStores.Infrastructure.Repositories.Interfaces.Generic;
using ManageStores.Infrastructure.Repositories.Implementations.Generic;
using ManageStores.Infrastructure.Repositories.Implementations.Company;
using ManageStores.Infrastructure.Repositories.Implementations.Store;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using static ManageStores.Core.Models.Company;
using static ManageStores.Core.Models.Store;
using ManageStores.Infrastructure.Repositories.Interfaces.Company;
using ManageStores.Infrastructure.Repositories.Interfaces.Store;
using ManageStores.Infrastructure.Services.Interfaces;
using ManageStores.Infrastructure.Services.Implementations;

namespace ManageStores.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc()

         .AddFluentValidation(o =>
         {
             o.RegisterValidatorsFromAssemblyContaining<CompanyValidator>();

             o.RegisterValidatorsFromAssemblyContaining<StoreValidator>();
         });

            var conn = Configuration.GetConnectionString("ManageStoresContextConnection");
            services.AddDbContext<ManageStoresDatabaseContext>(options => options.UseSqlServer(conn));

            services.AddIdentity<IdentityUser, IdentityRole>().
            AddEntityFrameworkStores<ManageStoresDatabaseContext>().
            AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 3;
            });

            services.AddTransient<IIdentitySeeder, IdentitySeeder>();

            services.AddTransient<ICompanyService, CompanyService>();
            services.AddTransient<IStoreService, StoreService>();

            services.AddSingleton(typeof(IGeneric<>), typeof(GenericRepository<>));

            services.AddSingleton<ICompany, CompanyRepository>();
            services.AddSingleton<ICompanyApp, CompanyApp>();

            services.AddSingleton<IStore, StoreRepository>();
            services.AddSingleton<IStoreApp, StoreApp>();

            services.AddTransient<IValidator<Company>, CompanyValidator>();
            services.AddTransient<IValidator<Store>, StoreValidator>();

            services.AddMvc().AddNewtonsoftJson();

            services.AddMvc().AddRazorRuntimeCompilation();

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IIdentitySeeder identitySeeder)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseStatusCodePages(); 
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseStatusCodePagesWithReExecute("/Error/Error", "?statusCode={0}");
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Account}/{action=Login}/{id?}");
            });

            identitySeeder.CreateAdminAccountIFEmpty();
        }
    }
}
