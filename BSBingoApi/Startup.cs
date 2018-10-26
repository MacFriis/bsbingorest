using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNet.Security.OpenIdConnect.Primitives;
using AutoMapper;
using BSBingoApi.Filters;
using BSBingoApi.Infrastructure;
using BSBingoApi.Model;
using BSBingoApi.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NSwag.AspNetCore;
using OpenIddict.Validation;

namespace BSBingoApi
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
            services.Configure<ApiInfo>(Configuration.GetSection("Info"));
            services.Configure<PagingOptions>(Configuration.GetSection("DefaultPagingOptions"));

            services.AddScoped<IBSWordService, DefaultBSWordService>();
            services.AddScoped<IUsersService, DefaultUserService>();

            services.AddDbContextPool<BSBingoApiDbContext>(options =>
            {
                options.UseInMemoryDatabase("bsworddb");
            });

            services.AddDbContextPool<UserDabaseContext>(options =>
            {
                options.UseInMemoryDatabase("identity");
                options.UseOpenIddict<Guid>();
            });

            services.AddOpenIddict()
                    .AddCore(options =>
            {
                options.UseEntityFrameworkCore()
                       .UseDbContext<UserDabaseContext>()
                       .ReplaceDefaultEntities<Guid>();
            })
                    .AddServer(options =>
                    {
                        options.UseMvc();
                        options.EnableTokenEndpoint("/token");
                        options.AllowPasswordFlow();
                        options.AcceptAnonymousClients();
                    })
                    .AddValidation();

            services.Configure<IdentityOptions>(options =>
            {
                options.ClaimsIdentity.UserNameClaimType = OpenIdConnectConstants.Claims.Name;
                options.ClaimsIdentity.UserIdClaimType = OpenIdConnectConstants.Claims.Subject;
                options.ClaimsIdentity.RoleClaimType = OpenIdConnectConstants.Claims.Role;
            });

            services.AddAuthentication(options =>
            {
                options.DefaultScheme = OpenIddictValidationDefaults.AuthenticationScheme;
            });


            services.AddAuthorization(options =>
            {
                options.AddPolicy("ViewAllUsersPolicy",
                                  p => p.RequireAuthenticatedUser().RequireRole("Admin"));
            });



            services.AddMvc(options =>
            {
                options.Filters.Add<JsonExceptionFilter>();
                options.Filters.Add<RequireHttpsOrCloseAttribute>();
                options.Filters.Add<LinkRewritingFilter>();
            })
                    .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddRouting(options => options.LowercaseUrls = true);

            services.AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.ApiVersionReader = new MediaTypeApiVersionReader();
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ReportApiVersions = true;
                options.ApiVersionSelector = new CurrentImplementationApiVersionSelector(options);
            });

            services.AddCors(options =>
            {
                options.AddPolicy("AllowMyApp",
                                  policy => policy.AllowAnyOrigin());
            });

            // Add ASP.NET Core identity
            AddIdentityCoreServices(services);


            services.AddAutoMapper(options => options.AddProfile<MappingProfile>());

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = context =>
                {
                    var errorResponse = new ApiError(context.ModelState);
                    return new BadRequestObjectResult(errorResponse);
                };
            });




        }



        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                app.UseSwaggerUi3WithApiExplorer(options =>
                {
                    options.GeneratorSettings.DefaultPropertyNameHandling = NJsonSchema.PropertyNameHandling
                        .CamelCase;
                });
            }
            else
            {
                app.UseHsts();
            }

            app.UseAuthentication();


            app.UseCors("AllowMyApp");
            app.UseMvc();
        }

        private void AddIdentityCoreServices(IServiceCollection services)
        {
            var builder = services.AddIdentityCore<UserEntity>();
            builder = new IdentityBuilder(builder.UserType, typeof(UserRoleEntity), builder.Services);

            builder.AddRoles<UserRoleEntity>()
                   .AddEntityFrameworkStores<UserDabaseContext>()
                   .AddDefaultTokenProviders()
                   .AddSignInManager<SignInManager<UserEntity>>();

        }
    }
}
