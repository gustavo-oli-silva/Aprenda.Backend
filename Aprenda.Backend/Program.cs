using Aprenda.Backend.Services.Classroom;
using Aprenda.Backend.Data;
using Microsoft.EntityFrameworkCore;
using Aprenda.Backend.Repositories.Classroom;
using Aprenda.Backend.Repositories.User;
using Aprenda.Backend.Services.User;
using Aprenda.Backend.Repositories.Post;
using Aprenda.Backend.Services.Post;
using Aprenda.Backend.Repositories.Archive;
using Aprenda.Backend.Services;
using Aprenda.Backend.Services.ArchiveService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;
using Aprenda.Backend.Repositories.Homework;
using Aprenda.Backend.Services.Homework;
using Aprenda.Backend.Repositories.Submission;
using Aprenda.Backend.Services.Submission;
using Aprenda.Backend.Services.Jwt;
using Aprenda.Backend.Services.Hash;
using Aprenda.Backend.Services.Auth;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

// Configure JWT Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        // Valida a assinatura do token usando a chave secreta
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"])),

        // Valida quem emitiu o token
        ValidateIssuer = true,
        ValidIssuer = configuration["Jwt:Issuer"],

        // Valida para quem o token foi emitido
        ValidateAudience = true,
        ValidAudience = configuration["Jwt:Audience"],

        RoleClaimType = "role"
    };
});

builder.Services.AddAuthorization();

// Configure Entity Framework
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString)
);

builder.Services.AddScoped<IClassroomRepository, ClassroomRepository>();
builder.Services.AddScoped<IClassroomService, ClassroomService>();


builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddScoped<IPostRepository, PostRepository>();
builder.Services.AddScoped<IPostService, PostService>();

builder.Services.AddScoped<IArchiveRepository, ArchiveRepository>();
builder.Services.AddScoped<IArchiveService, ArchiveService>();

builder.Services.AddScoped<IHomeworkRepository, HomeworkRepository>();
builder.Services.AddScoped<IHomeworkService, HomeworkService>();


builder.Services.AddScoped<ISubmissionRepository, SubmissionRepository>();
builder.Services.AddScoped<ISubmissionService, SubmissionService>();

builder.Services.AddScoped<IJwtService, JwtService>();
builder.Services.AddScoped<IHashService, HashService>();
builder.Services.AddScoped<IAuthService, AuthService>();



builder.Services.AddControllers();

builder.Services.AddRouting(options =>
{
    options.LowercaseUrls = true;
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        // Descrição que aparecerá no Swagger UI
        Description = "Autorização JWT usando o esquema Bearer. Exemplo: \"Authorization: Bearer {token}\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer"
    });

     options.OperationFilter<SecurityRequirementsOperationFilter>();

});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
