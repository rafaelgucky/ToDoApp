using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using ToDoApp.Data;
using ToDoApp.Mapping;
using ToDoApp.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<Context>(options => 
options.UseSqlite(builder.Configuration.GetConnectionString("SqliteConnection")));

builder.Services.AddScoped<GenericServices>();
builder.Services.AddScoped<JobServices>();
builder.Services.AddScoped<CategoryServices>();
builder.Services.AddScoped<ImageServices>();
builder.Services.AddScoped<UserServices>();

builder.Services.AddScoped<JobMapping>();
builder.Services.AddScoped<CategoryMapping>();
builder.Services.AddScoped<UserMapping>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
