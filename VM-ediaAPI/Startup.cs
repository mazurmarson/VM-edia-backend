using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using VM_ediaAPI.Data;
using VM_ediaAPI.Dtos;
using VM_ediaAPI.Middleware;
using VM_ediaAPI.Models;
using VM_ediaAPI.Validators;

namespace VM_ediaAPI
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
            var authenticationSettings = new AuthenticationSettings();
            Configuration.GetSection("Authentication").Bind(authenticationSettings);

            services.AddSingleton(authenticationSettings);

            services.AddAuthentication(opt => {
                opt.DefaultAuthenticateScheme = "Bearer";
                opt.DefaultScheme = "Bearer";
                opt.DefaultChallengeScheme = "Bearer";
            }).AddJwtBearer(cfg => {
                cfg.RequireHttpsMetadata = false;
                cfg.SaveToken = true;
                cfg.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = authenticationSettings.JwtIssuer, //Kto generuje
                    ValidAudience = authenticationSettings.JwtIssuer, //Kto jest odbiorcą
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authenticationSettings.JwtKey))
                };
            });

            services.AddControllers().AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
).AddFluentValidation();
            services.AddDbContext<DataContext>(opt => opt.UseNpgsql(
                Configuration.GetConnectionString("connectionString")
            ));
            services.AddScoped<IGenRepo, GenRepo>();
            services.AddScoped<IUserRepo, UserRepo>();
            services.AddScoped<IFollowRepo, FollowRepo>();
            services.AddScoped<IPhotoRepo, PhotoRepo>();
            services.AddScoped<IPostRepo, PostRepo>();
            services.AddScoped<IReactionRepo, ReactionRepo>();
            services.AddScoped<ICommentRepo, CommentRepo>();
            services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
            services.AddScoped<ErrorHandlingMiddleware>();
            services.AddTransient<Seed>();
            services.AddScoped<IValidator<RegisterUserDto>, RegisterUserDtoValidator>();
            services.AddScoped<IValidator<AddPostDto>, AddPostDtoValidator>();
            services.AddScoped<IValidator<AddCommentDto>, AddCommentDtoValidator>();
            services.AddScoped<IValidator<AddReactionDto>, AddReactionDtoValidator>();
            services.AddScoped<IValidator<UpdateCommentDto>, UpdateCommentDtoValidator>();
            services.AddScoped<IValidator<UpdatePostDto>, UpdatePostDtoValidator>();
            services.AddScoped<IValidator<UpdateUserDto>, UpdateUserDtoValidator>();
            services.AddScoped<IValidator<Reaction>, ReactionValidator>();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddSwaggerGen();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, Seed seeder)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseMiddleware<ErrorHandlingMiddleware>();
            seeder.SeedUsers();
            seeder.SeedPosts();
            seeder.SeedComments();
            seeder.SeedReactions();
            seeder.SeedFollow();
            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseSwagger();
            app.UseSwaggerUI(x => {
                x.SwaggerEndpoint("/swagger/v1/swagger.json", "VM-edia API");
            });
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
