using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.OpenApi.Models;
using System.Reflection.Metadata;
using Microsoft.IdentityModel.Tokens;
using ToDoApp.Data;
using ToDoApp.Mapping;
using ToDoApp.Services;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "ToDoApp", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter: \"Bearer [yourToken]\"",
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                          new OpenApiSecurityScheme
                          {
                              Reference = new OpenApiReference
                              {
                                  Type = ReferenceType.SecurityScheme,
                                  Id = "Bearer"
                              }
                          },
                         new string[] {}
                    }
                });
});

builder.Services.AddDbContext<Context>(options => 
options.UseSqlite(builder.Configuration.GetConnectionString("SqliteConnection")));

builder.Services.AddScoped<GenericServices>();
builder.Services.AddScoped<JobServices>();
builder.Services.AddScoped<CategoryServices>();
builder.Services.AddScoped<ImageServices>();
builder.Services.AddScoped<UserServices>();
builder.Services.AddScoped<RoleServices>();
builder.Services.AddScoped<TokenServices>();

builder.Services.AddScoped<JobMapping>();
builder.Services.AddScoped<CategoryMapping>();
builder.Services.AddScoped<UserMapping>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true, // Habilita validação do emissor
        ValidateAudience = true, // Habilita validação do público
        ValidateLifetime = true, // Verifica se o token expirou
        ValidateIssuerSigningKey = true, // Verifica a chave de assinatura

        ValidIssuer = "https://seudominio.com", // Ex: https://seudominio.com
        ValidAudience = "https://seusistema.com", // Ex: https://seusistema.com
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.ASCII.GetBytes(builder.Configuration
            .GetValue<string>("SecretKey")!)),
    };
});


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
