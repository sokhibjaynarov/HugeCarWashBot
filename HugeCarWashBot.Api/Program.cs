using Bot.MiddlewareExtensions;
using HugeCarService.Api.Extensions;
using HugeCarWashBot.API.Configurations;
using HugeCarWashBot.Data.Contexts;
using HugeCarWashBot.Domain.Entities.Configurations;
using HugeCarWashBot.Service.Mappers;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

// Add services to the container.

builder.Services.AddAppConfigurations(builder.Configuration);
builder.Services.AddAppServices(builder.Configuration);

builder.Services.AddControllers().AddNewtonsoftJson();

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy
                            .WithOrigins("http://localhost:5001")
                            .WithMethods("GET", "POST", "PUT", "DELETE")
                            .AllowAnyHeader()
                            .AllowCredentials();
                      });
});

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("Default"));
});

builder.Services.ConfigureSwagger(builder.Configuration);

builder.Services.AddHttpContextAccessor();

builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddCustomServices();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//app.UseMiddleware<CreateSession>();

app.UseRouting();

app.UseCors(MyAllowSpecificOrigins);

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    var token = app.Configuration.GetSection(BotConfigurations.Position).Get<BotConfigurations>().AuthToken;
    endpoints.MapControllerRoute
    (
        name: "TelegramWebHookRoute",
        pattern: $"bot/{token}",
        new { controller = "WebHookController", action = "Post" }
    );
    endpoints.MapControllers();
});

app.Run();
