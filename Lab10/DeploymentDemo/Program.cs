var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

app.MapGet("/health", () => Results.Ok(new
{
    status = "healthy",
    application = "SE Lab 10 Deployment Demo",
    utcTime = DateTimeOffset.UtcNow
}));

app.MapGet("/api/info", () => Results.Ok(new
{
    name = "SE Lab 10 Deployment Demo",
    framework = ".NET 8",
    deployment = "Docker container"
}));

app.MapFallbackToFile("index.html");
app.Run();
