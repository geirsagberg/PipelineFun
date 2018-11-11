using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PipelineFun.Web.Data;
using PipelineFun.Web.MvcUtils;
using Swashbuckle.AspNetCore.Swagger;

namespace PipelineFun.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options => {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddDefaultIdentity<IdentityUser>()
                .AddEntityFrameworkStores<ApplicationDbContext>();
            services.Configure<IdentityOptions>(options => {
                options.Password = new PasswordOptions {
                    RequireDigit = false,
                    RequireLowercase = false,
                    RequireNonAlphanumeric = false,
                    RequireUppercase = false,
                    RequiredLength = 8
                };
            });

            services.AddTransient<IApplicationModelProvider, ActionDependencyModelProvider>();

            services.AddMvc(o => {
                    o.Filters.Add<PipelineActionFilter>();
                    o.Filters.Add<HttpExceptionFilter>();
                })
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new Info {Title = "My API", Version = "v1"}); });

            services.Scan(classes => { classes.FromCallingAssembly().AddClasses().AsImplementedInterfaces(); });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseWhen(context => context.Request.Headers["accept"].Any(h => h.ToLowerInvariant().Contains("json")),
                apiApp => { apiApp.UseMiddleware<ApiExceptionHandlerMiddleware>(); });
            app.UseWhen(context => !context.Request.Headers["accept"].Any(h => h.ToLowerInvariant().Contains("json")),
                mvcApp => {
                    if (env.IsDevelopment()) {
                        mvcApp.UseDeveloperExceptionPage();
                        mvcApp.UseDatabaseErrorPage();
                    } else {
                        mvcApp.UseExceptionHandler("/Error");
                    }
                });
            if (!env.IsDevelopment()) {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseAuthentication();

            app.UseMvc();

            app.UseSwagger();

            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1"); });
        }
    }
}