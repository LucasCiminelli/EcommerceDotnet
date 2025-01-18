using System.Text;
using Ecommerce.Domain;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.IdentityModel.Tokens;
using Infrastructure;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddInfrastructureServices(builder.Configuration); //agregando los servicios de infrastructure y pasandole el Configuration que tiene los datos de JwtSettings


builder.Services.AddDbContext<EcommerceDbContext>(options =>

options.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionString"),

    b => b.MigrationsAssembly(typeof(EcommerceDbContext).Assembly.FullName)  //imprimir en la consola las tareas que se realicen respecto a la DB

    )
);

// Add services to the container.

builder.Services.AddControllers(opt =>
{

    var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
    opt.Filters.Add(new AuthorizeFilter(policy));

});

IdentityBuilder identityBuilder = builder.Services.AddIdentityCore<Usuario>();
identityBuilder = new IdentityBuilder(identityBuilder.UserType, identityBuilder.Services);

identityBuilder.AddRoles<IdentityRole>().AddDefaultTokenProviders(); //la información de los roles pueda ser imprimida en los tokens.

identityBuilder.AddClaimsPrincipalFactory<UserClaimsPrincipalFactory<Usuario, IdentityRole>>(); //propiedades que se van a agregar dentro ed los tokens de seguridad.

identityBuilder.AddEntityFrameworkStores<EcommerceDbContext>(); //soporte dentro de la base de datos para que se puedan generar el esquema de clases en aspnetcore identity como tablas en SQL Server

identityBuilder.AddSignInManager<SignInManager<Usuario>>(); //soporte de tareas de login.

builder.Services.TryAddSingleton<ISystemClock, SystemClock>(); //soporte para la creación de DateTimes cuando genere un nuevo record en la base de datos



var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:Key"]!));
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
{
    opt.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true, //proceso de validación del token se tiene que usar el key
        IssuerSigningKey = key, //objecto clave para desencriptar el token = el key que declaramos arriba
        ValidateAudience = false, //no validar la audiencia
        ValidateIssuer = false, //no validar el issuer.
    };
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
    builder =>
    builder
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());
});


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.UseCors("CorsPolicy");

app.MapControllers();

app.Run();
