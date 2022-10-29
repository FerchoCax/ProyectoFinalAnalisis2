using AccesoDatos;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Servicios.Interfaces;
using Servicios.Servicios;
using System.Text;
using Microsoft.AspNetCore.Mvc.NewtonsoftJson;
using Microsoft.AspNetCore.DataProtection;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddNewtonsoftJson(x => x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration["JWTKey"]))
    };
    x.Events = new JwtBearerEvents
    {
        OnMessageReceived = context =>
        {
            var accesToken = context.Request.Query["acces_token"];
            var path = context.HttpContext.Request.Path;
            if (!string.IsNullOrEmpty(accesToken))
            {
                context.Token = accesToken;
            }
            return Task.CompletedTask;
        }

    };
});
builder.Services.AddDbContext<DataBaseContext>((options) =>
{
 });
builder.Services.AddDataProtection();
builder.Services.AddScoped<IJtAuth, Auth>();
builder.Services.AddScoped<IUsuarios, Usuarios>();
builder.Services.AddScoped<IClientes, Clientes>();
builder.Services.AddScoped<IRoles, Roles>();
builder.Services.AddScoped<IRolesUsuario, Servicios.Servicios.RolesUsuario>();
builder.Services.AddScoped<IDepartamentosMunicipios, DepartamentosMunicipios>();
builder.Services.AddScoped<ISucursal, Sucursales>();
builder.Services.AddScoped<ISalas, Salas>();
builder.Services.AddScoped<IImagenesPelicula, ImagenesPeliculas>();
builder.Services.AddScoped<IFunciones, Funciones>();
builder.Services.AddScoped<ICompras, Compras>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors(x => x
             .AllowAnyMethod()
             .AllowAnyHeader()
             //.WithHeaders("Access-Control-Allow-Headers: Origin, X-Requested-With, Content-Type, Accept")
             .SetIsOriginAllowed(origin => true) // allow any origin
             .AllowCredentials());
app.UseAuthorization();
app.MapControllers();
app.UseRouting();
 app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/hola", async context =>
                {
                    await context.Response.WriteAsync("Version 0.1");
                });
                endpoints.MapControllers();//.RequireAuthorization();
                // endpoints.MapHub<OrdenesMedicasHub>("/hub/OrdenesMedicas");
            });
app.Run();
