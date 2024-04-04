using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using RentVilla.Application;
using RentVilla.Application.Validators;
using RentVilla.Infrastructure;
using RentVilla.Infrastructure.Filters;
using RentVilla.Infrastructure.Services.Storage.Azure;
using RentVilla.Persistence;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddPersistenceServices();
builder.Services.AddInfrastructureServices();
builder.Services.AddApplicationServices();
//builder.Services.AddStorage(StorageType.Local);
//builder.Services.AddStorage<LocalStorage>();
builder.Services.AddStorage<AzureStorage>();
builder.Services.AddCors(options => options.AddDefaultPolicy(policy =>
    policy.WithOrigins("http://localhost:3000", "https://localhost:3000", "http://localhost:5161", "https://localhost:5161", "http://localhost:7246", "https://localhost:7246", "http://localhost:5006", "https://localhost:5006", "https://localhost:7084", "http://localhost:7084").AllowAnyHeader().AllowAnyMethod()
));

builder.Services.AddControllers(options => options.Filters.Add<ValidationFilter>())
    .AddFluentValidation(configuration => configuration.RegisterValidatorsFromAssemblyContaining<CreateProductValidator>())
    .ConfigureApiBehaviorOptions(options => options.SuppressModelStateInvalidFilter = true);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer("Admin", options =>
    {
        options.TokenValidationParameters = new()
        {
            ValidateAudience = true, //Kimin/hangi origin/sitenin kullanabileceğini belirler
            ValidateIssuer = true, //Oluşturulacak token'ı kimin dağıttığını ifade eder
            ValidateLifetime = true, //Token'ın ne kadar süre geçerli olduğunu belirler
            ValidateIssuerSigningKey = true, //Token'ın hangi key ile oluşturulduğunu belirler, security key verisinin doğrulanmasıdır

            ValidAudience = builder.Configuration["Token:Audience"],
            ValidIssuer = builder.Configuration["Token:Issuer"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Token:SigningKey"]))
        };
        
    });

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseStaticFiles();
app.UseCors();
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
