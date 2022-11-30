using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using FluentValidation.AspNetCore;
using CostAccounting.Validation;
using AutoMapper;
using WebApi;

namespace CostAccounting
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        [System.Obsolete]
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddFluentValidation( fv =>
            {
                fv.RegisterValidatorsFromAssemblyContaining<CategoryValidator>(lifetime: ServiceLifetime.Singleton);
                fv.RegisterValidatorsFromAssemblyContaining<RecordValidator>(lifetime: ServiceLifetime.Singleton);
                fv.RegisterValidatorsFromAssemblyContaining<UserValidator>(lifetime: ServiceLifetime.Singleton);
            });

            services.AddDbContext<CostAccountingDbContext>(options =>
                options.UseSqlite(Configuration.GetConnectionString("DefaultConnection")));

            var mapperConfig = new MapperConfiguration(mc =>
                mc.AddProfile(new AuthomapperProfile()));
            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
