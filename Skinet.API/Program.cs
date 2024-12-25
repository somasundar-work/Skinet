using Skinet.Application.Extensions;
using Skinet.Application.Services;
using Skinet.Data.Context;
using Skinet.Data.Extensions;
using Skinet.Entities.Identity;
using Skinet.Infra.Extensions;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddStoreContext(builder.Configuration);
builder.Services.AddRepository();
builder.Services.AddCors();
builder.Services.AddSingleton<IConnectionMultiplexer>(config =>
{
    var connString =
        builder.Configuration.GetConnectionString("Redis")
        ?? throw new ArgumentException("Redis Connection string is required");
    ConfigurationOptions configuration = ConfigurationOptions.Parse(connString, true);
    return ConnectionMultiplexer.Connect(configuration);
});
builder.Services.AddSingleton<ICartService, CartService>();
builder.Services.AddAuthorization();
builder.Services.AddIdentityApiEndpoints<AppUser>().AddEntityFrameworkStores<StoreContext>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseExceptionMiddleware();

app.UseCors(x =>
    x.AllowAnyHeader()
        .AllowAnyMethod()
        .AllowCredentials()
        .WithOrigins("http://localhost:5119", "https://localhost:7249", "http://localhost:4200")
);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();
app.MapGroup("api/identity").MapIdentityApi<AppUser>().WithTags("Identity"); // api/identity/login

app.MigrateStore();

app.Run();
