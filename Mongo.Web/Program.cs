using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Options;
using Mongo.Web.Services;
using Mongo.Web.Services.IServices;
using Mongo.Web.Utility;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();//for  views
builder.Services.AddHttpClient();//for code
builder.Services.AddScoped<ITokeProvider, TokeProvider>();
builder.Services.AddHttpClient<ICouponService,CouponService>();//for client
builder.Services.AddHttpClient<IAuthService, AuthService>();//for client
builder.Services.AddScoped<IBaseService, BaseService>();
builder.Services.AddScoped<ICouponService, CouponService>();    
builder.Services.AddScoped<IAuthService,AuthService>();	
SD.CouponApiBaseUrl = builder?.Configuration["ServiceUrls:CouponApiUrl"];
SD.AuthApiBaseUrl = builder?.Configuration["ServiceUrls:AuthApiUrl"];
builder.Services.AddHttpClient("MangoAPI")
	.ConfigurePrimaryHttpMessageHandler(() =>
	{
		return new HttpClientHandler
		{
			ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
		};
	});
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
.AddCookie(options =>
{
	options.ExpireTimeSpan = TimeSpan.FromHours(10);
	options.LoginPath = "/Auth/Login";
	options.LogoutPath = "/Auth/Logout";
	options.AccessDeniedPath = "/Auth/AccessDenied";
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
// Authentication and Authorization Middleware
app.UseAuthentication();
app.UseAuthorization();

//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Home}/{action=Index}/{couponId?}");
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
