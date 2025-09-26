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

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

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

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
