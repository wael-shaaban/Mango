using Mongo.Web.Services;
using Mongo.Web.Services.IServices;
using Mongo.Web.Utility;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();//for  views
builder.Services.AddHttpClient();//for code
builder.Services.AddHttpClient<ICouponService,CouponService>();//for client
builder.Services.AddScoped<IBaseService, BaseService>();
builder.Services.AddScoped<ICouponService, CouponService>();    
SD.CouponApiBaseUrl = builder?.Configuration["ServiceUrls:CouponApiUrl"];
builder.Services.AddHttpClient("MangoAPI")
	.ConfigurePrimaryHttpMessageHandler(() =>
	{
		return new HttpClientHandler
		{
			ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
		};
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

app.UseAuthorization();

//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Home}/{action=Index}/{couponId?}");
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
