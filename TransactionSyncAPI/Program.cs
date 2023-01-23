using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TransactionSyncAPI.DataAccess;
using TransactionSyncAPI.Interfases;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = builder.Configuration["Jwt:Audience"],
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});

//Coonection string to database
string connectionString = builder.Configuration.GetConnectionString("SqlServerConnectionStrings") ?? "";

//Add to dependency injection database context for entity framework core
builder.Services.AddDbContext<TransactionDbContext>(option =>
{
    option.UseSqlServer(connectionString);
});

//Add database context for Dapper
builder.Services.AddScoped<ITransactionDbContext>(provider => provider.GetService<TransactionDbContext>());
builder.Services.AddScoped<ITransactionWriteDbConnection, TransactionWriteDbConnection>();
builder.Services.AddScoped<ITransactionReadDbConnection, TransactionReadDbConnection>();

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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
