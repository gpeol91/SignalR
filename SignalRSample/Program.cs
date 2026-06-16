using SignalRSample.Hubs;
using SignalRSample.Middelware;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSignalR();

// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("cors", policy =>
    {
        policy.WithOrigins("http://localhost:3000","https://HumayaDigital.com")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});

var app = builder.Build();
app.UseRouting();
app.UseCors("cors");
//app.UseCors(builder =>
//{
//    builder.WithOrigins("http://localhost:3000")
//           .AllowAnyHeader()
//           .AllowAnyMethod()
//           .AllowCredentials();
//});


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//app.UseMiddleware<ApiKeyMiddleware>();
//API KEY middleware para controllers
app.Use(async (context, next) =>
{
    var path = context.Request.Path.Value;

// excluir SignalR
if (path.StartsWith("/hubs"))
{
    await next();
    return;
}

if (!context.Request.Headers.TryGetValue("X-API-KEY", out var apiKey))
{
    context.Response.StatusCode = 401;
    await context.Response.WriteAsync("API Key requerida");
    return;
}

if (apiKey != "SDDEW_rwerew_423545_323423")
{
    context.Response.StatusCode = 403;
    await context.Response.WriteAsync("API Key inválida");
    return;
}

await next();
});


app.UseAuthorization();

app.MapControllers();

app.MapHub<UserHub>("/hubs/userCount");

app.Run();
