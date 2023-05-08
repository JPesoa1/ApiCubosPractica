using ApiCubosPractica.Data;
using ApiCubosPractica.Helpers;
using ApiCubosPractica.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

string connectionString = builder.Configuration.GetConnectionString("SqlAzure");
builder.Services.AddSingleton<HelperOAuthToken>();
HelperOAuthToken helper = new HelperOAuthToken(builder.Configuration);
builder.Services.AddAuthentication(helper.GetAuthenticationOptions())
    .AddJwtBearer(helper.GetJwtOptions());

builder.Services.AddTransient<RepositoryCubos>();
builder.Services.AddDbContext<CubosContext>
    (option => option.UseSqlServer(connectionString));




builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(x =>
{
    x.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Api Crud Cubos",
        Description = "Api Cubos",
        Version = "v1"
    });
});

var app = builder.Build();


app.UseSwagger();
app.UseSwaggerUI(x =>
{
    x.SwaggerEndpoint(url: "/swagger/v1/swagger.json", name: "Api crud Cubos");
    x.RoutePrefix = "";
});
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
