using Microsoft.EntityFrameworkCore;
using UserDirectory.Data;

var builder = WebApplication.CreateBuilder(args);

// Add CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp",
        policy => policy.WithOrigins("http://localhost:5173") // React dev server
                        .AllowAnyHeader()
                      
                        .AllowAnyMethod());
});


//builder.Services.AddAuthentication("Bearer")
//    .AddJwtBearer("Bearer", options =>
//    {
//        options.Authority = "https://login.microsoftonline.com/{tenantId}/v2.0"; // or Auth0 domain
//        options.Audience = "api://your-api-client-id";
//    });

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=data/app.db"));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


// Use CORS before authentication/authorization
app.UseCors("AllowReactApp");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();


