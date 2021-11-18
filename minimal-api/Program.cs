using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add CORS policy to service builder.
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowOrigins", builder => builder.WithOrigins("http://localhost:3000").AllowAnyHeader());
});

// Add swagger doc generation to service builder.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "Minimal Api", Version = "v1" });
});

// Add entity framework to service builder.
var connectionstring = builder.Configuration.GetSection("ConnectionStrings")["Postgres"].ToString();
builder.Services.AddDbContext<EmployeeDBContext>(options => options.UseNpgsql(connectionstring));

var app = builder.Build();

// Use CORS policy in the app.
app.UseCors("AllowOrigins");
// Use swagger ui to navigate the api.
app.UseSwagger();
app.UseSwaggerUI(ui =>
{
    ui.SwaggerEndpoint("/swagger/v1/swagger.json", "Minimal Api v1");
});

app.MapGet("/", () => "Hello World!");

app.Run();
