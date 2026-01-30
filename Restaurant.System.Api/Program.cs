using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Restaurant.System.Api.HealthChecks;
using Restaurant.System.Data;
using Restaurant.System.Data.Extensions;
using Restaurant.System.Services.Extensions;
using Supabase;

var builder = WebApplication.CreateBuilder(args);

var url = builder.Configuration["Supabase:Url"];
var key = builder.Configuration["Supabase:ServiceRoleKey"];

if (string.IsNullOrEmpty(url) || string.IsNullOrEmpty(key))
{
    throw new InvalidOperationException("Supabase URL or Key is missing from Configuration.");
}

var options = new SupabaseOptions
{
    AutoRefreshToken = true,
    AutoConnectRealtime = true
};

builder.Services.AddSingleton(provider => new Supabase.Client(url, key, options));

Console.WriteLine($"Key loaded: {!string.IsNullOrEmpty(key)}");

builder.Services.AddHealthChecks().AddCheck<SupabaseHealthCheck>("Supabase");

builder.Services.AddCors(options =>
{
    options.AddPolicy("DevelopmentPolicy", policy =>
    {
        policy
            .SetIsOriginAllowed(_ => true) // <-- Allows Codespaces dynamic URL
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });

    options.AddPolicy("ProductionPolicy", policy =>
    {
        policy
            .SetIsOriginAllowed(_ => true) // <-- Allows Codespaces dynamic URL
            .WithOrigins("https://vigilant-fiesta-q7644xj6wqgwh657j-4200.app.github.dev")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
options.TokenValidationParameters = new TokenValidationParameters
{
    ValidateIssuer = true,
    ValidateAudience = true,
    ValidateLifetime = true,
    ValidateIssuerSigningKey = true,
    ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
    ValidAudience = builder.Configuration["JwtSettings:Audience"],
    IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:Key"]))
});

// .AddGoogle(options =>
// {
//     // Google configuration options
// })
// .AddFacebook(options =>
// {
//     // Facebook configuration options
// })
// .AddMicrosoftAccount(options =>
// {
//     // Microsoft Account configuration options
// })
// .AddTwitter(options =>
// {
//     // Twitter configuration options
// });



// builder.Services.AddAuthorization(options =>
// {
//     options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
// });

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddDataDependencies();
builder.Services.AddServiceDependencies();

var app = builder.Build();

app.MapHealthChecks("/api/ishealthy", new HealthCheckOptions
{
    ResponseWriter = async (context, report) =>
    {
        context.Response.ContentType = "application/json";

        // This picks up the "Supabase is reachable via API" message
        var result = System.Text.Json.JsonSerializer.Serialize(new
        {
            status = report.Status.ToString(),
            details = report.Entries.Select(e => new
            {
                service = e.Key,
                state = e.Value.Status.ToString(),
                message = e.Value.Description // <--- This is where your message lives!
            })
        });

        await context.Response.WriteAsync(result);
    }
});

// if(!app.Environment.IsDevelopment())
// {
//     app.UseHttpsRedirection();
// }

app.UseRouting();
app.UseCors(app.Environment.IsDevelopment() ? "DevelopmentPolicy" : "ProductionPolicy");

app.Use(async (ctx, next) =>
{
    if (ctx.Request.Method == "OPTIONS")
    {
        ctx.Response.Headers["Access-Control-Allow-Origin"] = ctx.Request.Headers["Origin"];
        ctx.Response.Headers["Access-Control-Allow-Methods"] = "GET, POST, PUT, DELETE, OPTIONS";
        ctx.Response.Headers["Access-Control-Allow-Headers"] =
            ctx.Request.Headers["Access-Control-Request-Headers"];

        ctx.Response.StatusCode = 200;
        return;
    }

    await next();
});

app.UseAuthentication();
app.UseAuthorization();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseDefaultFiles();
app.UseStaticFiles();


app.MapControllers();
app.MapFallbackToFile("index.html"); // <-- For Angular routes

app.MapGet("/", () => Results.Text("API running"));

app.Run();