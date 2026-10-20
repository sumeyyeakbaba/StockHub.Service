var builder = WebApplication.CreateBuilder(args);

// MVC Controller ve View desteði
builder.Services.AddControllersWithViews();

// API çaðrýlarý için HttpClient kaydý
var apiBaseUrl = builder.Configuration["ApiSettings:BaseUrl"];
builder.Services.AddHttpClient("StockHubApi", client =>
{
    client.BaseAddress = new Uri(apiBaseUrl);
});

// Oturum ve Token yönetimi için Session desteði
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(60);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
// HttpContextAccessor ve Kendi Servisimiz (MUTLAKA BUILD'DEN ÖNCE OLMALI)
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<StockHub.Web.Services.IApiService, StockHub.Web.Services.ApiService>();
var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Session Middleware (Routing ile Authorization arasýnda olmalý)
app.UseSession();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();