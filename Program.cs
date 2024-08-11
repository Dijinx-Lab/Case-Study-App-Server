using System.Security.Claims;
using CaseStudyAppServer.Constants;
using CaseStudyAppServer.Data;
using CaseStudyAppServer.Dtos.Base;
using CaseStudyAppServer.Helpers;
using CaseStudyAppServer.Interfaces;
using CaseStudyAppServer.Middlewares;
using CaseStudyAppServer.Models;
using CaseStudyAppServer.Repositories;
using CaseStudyAppServer.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers()
   .ConfigureApiBehaviorOptions(options =>
   {
       options.InvalidModelStateResponseFactory = context =>
       {
           // Get the first error message from the model state dictionary
           var firstError = context.ModelState.Values
               .SelectMany(v => v.Errors)
               .Select(e => e.ErrorMessage)
               .FirstOrDefault() ?? MessageConstants.BadRequest;

           var response = new BaseDto
           {
               Status = false,
               Message = firstError
           };

           return new BadRequestObjectResult(response);
       };
   });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
});

builder.Services.AddDbContext<ApplicationDBContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddIdentity<AppUser, IdentityRole>(options =>
{
    options.Password.RequiredLength = 8;
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireDigit = false;
    options.Password.RequireNonAlphanumeric = false;
    options.User.RequireUniqueEmail = true;
    options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+ ";
})
.AddEntityFrameworkStores<ApplicationDBContext>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme =
    options.DefaultChallengeScheme =
    options.DefaultForbidScheme =
    options.DefaultScheme =
    options.DefaultSignInScheme =
    options.DefaultSignOutScheme = JwtBearerDefaults.AuthenticationScheme;

}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateAudience = false,
        ValidateIssuer = true,
        ValidIssuer = builder.Configuration["JWT:Issuer"],
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(builder.Configuration["JWT:SigningKey"]!)),
        RoleClaimType = ClaimTypes.Role
    };
});

builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<ITokenRepository, TokenRepository>();
builder.Services.AddScoped<ITeamService, TeamService>();
builder.Services.AddScoped<ITeamRepository, TeamRepository>();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseMiddleware<UnauthorizedMiddleware>();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();