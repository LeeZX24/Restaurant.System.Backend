using System.Text;
using LogDashboard;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Restaurant.System.Data;
using Restaurant.System.Data.Extensions;
using Restaurant.System.Services.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Decide which connection to use
string connStr;

if (builder.Environment.IsDevelopment())
{
    // Codespaces / local dev → use SharedPooler
    connStr = builder.Configuration.GetConnectionString("DevelopmentConnection");
}
else
{
    // Production → use DirectIPv6
    connStr = builder.Configuration.GetConnectionString("DefaultConnection");
}

if (string.IsNullOrWhiteSpace(connStr))
{
    throw new InvalidOperationException("PostgreSQL connection string not found in environment variables.");
}

if (!connStr.Contains("Ssl Mode", StringComparison.OrdinalIgnoreCase))
{
    throw new InvalidOperationException("Connection string must explicitly include SSL Mode.");
}

builder.Services.AddHealthChecks()
    .AddNpgSql(
        connStr,
        name: "PostgreSQL",
        failureStatus: Microsoft.Extensions.Diagnostics.HealthChecks.HealthStatus.Unhealthy,
        tags: ["db", "postgres"])
    .AddCheck("self", () => Microsoft.Extensions.Diagnostics.HealthChecks.HealthCheckResult.Healthy());

// var url = builder.Configuration["Supabase:Url"];
// var key = builder.Configuration["Supabase:ServiceRoleKey"];

// if (string.IsNullOrEmpty(url) || string.IsNullOrEmpty(key))
// {
//     throw new InvalidOperationException("Supabase URL or Key is missing from Configuration.");
// }

// var options = new SupabaseOptions
// {
//     AutoRefreshToken = true,
//     AutoConnectRealtime = true
// };

// builder.Services.AddSingleton(provider => new Supabase.Client(url, key, options));

// Console.WriteLine($"Key loaded: {!string.IsNullOrEmpty(key)}");

// builder.Services.AddHealthChecks().AddCheck<SupabaseHealthCheck>("Supabase");

builder.Services.AddCors(options =>
{
    options.AddPolicy("DevelopmentPolicy", policy =>
    {
        policy.SetIsOriginAllowed(_ => true)
            .AllowAnyHeader()
            .AllowAnyMethod();
    });

    options.AddPolicy("ProductionPolicy", policy =>
    {
        policy.WithOrigins(
            [
                "https://stunning-chainsaw-97w55xrwvv7whj67-4200.app.github.dev",
                "https://leezx24.github.io",
                "https://restaurant-system-web-beta.onrender.com",
                "https://restaurant-system-web-4ji2.onrender.com"
            ])
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var jwtSettings = builder.Configuration.GetSection("JwtSettings");
var key = Encoding.UTF8.GetBytes(jwtSettings["Key"] ?? throw new Exception("JWT key missing"));

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
    IssuerSigningKey = new SymmetricSecurityKey(key),
    ClockSkew = TimeSpan.Zero
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
    options.UseNpgsql(connStr,
        b => b.MigrationsAssembly("Restaurant.System.Data")
        .EnableRetryOnFailure()
    )
);

builder.Services.AddDataDependencies();
builder.Services.AddServiceDependencies();

builder.Services.AddDataProtection()
        .PersistKeysToFileSystem(
            new DirectoryInfo(Path.Combine(builder.Environment.ContentRootPath, "DataProtection-Keys")))
        .SetApplicationName("Restaurant.System");

builder.Services.AddSignalR();

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

if (!app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

app.UseRouting();
app.UseCors("ProductionPolicy");

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

app.MapHub<LogHub>("/logsHub");
var hubContext = app.Services.GetRequiredService<IHubContext<LogHub>>();
var loggerFactory = app.Services.GetRequiredService<ILoggerFactory>();

loggerFactory.AddProvider(new SignalRLoggerProvider(hubContext));

if (app.Environment.IsDevelopment())
{
    app.MapGet("/", async context =>
    {
        var html = """
            <!DOCTYPE html>
            <html>
            <head>
                <title>Live Logs</title>
                <script src="https://cdnjs.cloudflare.com/ajax/libs/microsoft-signalr/7.0.5/signalr.min.js"></script>
            </head>
            <body style="background:black;color:lime;font-family:monospace;">
                <h3>Live Logs</h3>
                <pre id="logs"></pre>

                <script>
                    const connection = new signalR.HubConnectionBuilder()
                        .withUrl("/logsHub")
                        .build();

                    connection.on("ReceiveLog", message => {
                        const logs = document.getElementById("logs");
                        logs.textContent += message + "\\n";
                        window.scrollTo(0, document.body.scrollHeight);
                    });

                    connection.start();
                </script>
            </body>
            </html>
            """;

        context.Response.ContentType = "text/html";
        await context.Response.WriteAsync(html);
    });
}
else
{
    app.MapGet("/", () => Results.Text("API running"));
}


app.Run();