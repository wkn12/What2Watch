var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.SameSite = SameSiteMode.None;

});

builder.Services.AddHttpClient("w2wApi", client =>
{
    client.BaseAddress = new Uri(builder.Configuration["ApiUrl"]);
});

builder.Services.AddSingleton<IConfiguration>(builder.Configuration);
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.ConfigureSerilog();
builder.Services.RegisterLoggingInterfaces();
builder.Services.AddControllersWithViews(config =>
{
    config.Filters.Add<CustomExceptionFilterAttribute>();
}
);
builder.Services.AddDataServices(builder.Configuration);
builder.Services.AddHttpContextAccessor();

var app = builder.Build();
app.UseMiddleware<RefreshTokenMiddleware>();
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseHsts();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.Run();
