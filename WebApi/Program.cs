using AnaliticTrend.WebApi.RegisterServices;
using WebApi.Middleware;
using WebApi.RegisterServices;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
builder.AddConfigurations();
builder.AddDbContextService();
builder.Services.MapRepositories();
builder.Services.MapUseCases(builder.Configuration);
builder.Services.AddEndpoints(typeof(Program).Assembly, builder.Configuration);
builder.AddUtilities();
builder.Services.AddHelpersService();

RegisterAuthentication.AddAuthentication(builder);
builder.Services.AddAuthorization(options =>
{

    options.FallbackPolicy = new Microsoft.AspNetCore.Authorization.AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();
});

builder.AddSwagger();
builder.Services.AddCors(policy =>
    policy.AddPolicy(name: "AnaliticTrendPolicy", dp =>
    {
        string[] origins = builder.Configuration["AllowedOrigins"].Split(";");
        dp.WithOrigins(origins)
          .AllowAnyMethod()
          .AllowCredentials()
          .AllowAnyHeader();
    })
);

builder.Services.AddExceptionService();
builder.Services.AddAntiforgery();
builder.Services.AddHttpContextAccessor();

builder.Services.AddSignalR();

var app = builder.Build();

if (builder.Configuration["EnableSwagger"] == "On")
{
    app.UseSwaggerMiddleware();
}
app.UseCors("AnaliticTrendPolicy");
app.UseExceptionHandler();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();
app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<AppAuthorizeMiddleware>();
app.UseMiddleware<SecurityHeadersMiddleware>();

app.MapEndpoints();
await app.RunAsync();