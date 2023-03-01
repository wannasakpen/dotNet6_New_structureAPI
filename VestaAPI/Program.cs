using VestaAPI.ActionFilters;
using VestaAPI.Utilities.IHeadersUtil;
using VestaAPI.Utilities;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMvc(config =>
    config.Filters.Add(typeof(ValidateModelAttribute))
);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddTransient<IHeadersUtils, HeadersUtil>();

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.Cookie.IsEssential = true;
    options.IdleTimeout = TimeSpan.FromHours(2);
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
