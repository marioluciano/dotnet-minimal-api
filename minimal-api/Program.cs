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

// Add authentication and authorization to service builder.
builder.Services.AddAuthorization();
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
        ValidAudience = builder.Configuration["JWT:ValidAudience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:SigningKey"]))
    };
});

var app = builder.Build();

// Use CORS policy in the app.
app.UseCors("AllowOrigins");

// Use swagger ui to navigate the api.
app.UseSwagger();
app.UseSwaggerUI(ui =>
{
    ui.SwaggerEndpoint("/swagger/v1/swagger.json", "Minimal Api v1");
});

// Use authorization
app.UseAuthorization();

// Use authentication
app.UseAuthentication();

app.MapPost("/login",
[AllowAnonymous]
([FromBody] Login login) =>
{
    AuthenticationHelper authHelper = new AuthenticationHelper(builder.Configuration);
    var token = authHelper.Login(login);

    if (!string.IsNullOrEmpty(token))
        return token;
    else
        return "Unauthorized";
});

app.MapGet("/employees",
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
async ([FromServices] EmployeeDBContext dbContext) =>
{
    var employees = await dbContext.Employees.ToListAsync();
    return employees;
});

app.MapGet("/employees/{id}",
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
async ([FromServices] EmployeeDBContext dbContext, int id) =>
{
    var employee = await dbContext.Employees.Where(t => t.Id == id).FirstOrDefaultAsync();
    return employee;
});

app.MapPost("/employees",
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
async ([FromServices] EmployeeDBContext dbContext, Employee employee) =>
{
    dbContext.Employees.Add(employee);
    await dbContext.SaveChangesAsync();
    return employee;
});

app.MapPut("/employees",
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
async ([FromServices] EmployeeDBContext dbContext, Employee employee) =>
{
    dbContext.Entry(employee).State = EntityState.Modified;
    await dbContext.SaveChangesAsync();
    return employee;
});

await app.RunAsync();
