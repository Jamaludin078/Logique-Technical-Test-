using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddControllersWithViews();
builder.Services.AddRouting(options => options.LowercaseUrls = true);

builder.Services.AddDbContext<Web.Data.DataContext>(options =>
				options.UseSqlServer(Web.Data.Extensions.DataConfigurationManager.GetConnectionString()),
				ServiceLifetime.Singleton
			);

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
.AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
{
	options.Cookie.HttpOnly = false;
	options.LoginPath = new PathString("/account/login");
	options.LogoutPath = new PathString("/account/logout");
	options.ExpireTimeSpan = TimeSpan.FromSeconds(10);
	options.ReturnUrlParameter = "/";
});
//var baseurl = "http://localhost";
//var policyName = "defaultCorsPolicy";

builder.Services.AddCors(options =>
	options.AddDefaultPolicy(builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));

builder.Services.AddSignalR();

builder.Services.AddSingleton<ITempDataProvider, CookieTempDataProvider>();

builder.Services.AddHttpContextAccessor();

builder.Services.AddMemoryCache();

builder.Services.AddHttpClient();

builder.Services.AddSession(options =>
{
	options.IdleTimeout = TimeSpan.FromSeconds(10);
	options.Cookie.HttpOnly = true;
	options.Cookie.IsEssential = true;
});

builder.Services.AddMvc(config =>
{
	config.CacheProfiles.Add("DefaultNoCacheProfile", new CacheProfile
	{
		NoStore = true,
		Location = ResponseCacheLocation.None
	});
	config.Filters.Add(new ResponseCacheAttribute
	{
		CacheProfileName = "DefaultNoCacheProfile"
	});

	var schemas = new List<string>
				{
					CookieAuthenticationDefaults.AuthenticationScheme
				}.ToArray();

	config.Filters.Add(new AuthorizeFilter(new AuthorizationPolicyBuilder(schemas).RequireAuthenticatedUser().Build()));
});

builder.Services.Configure<RequestLocalizationOptions>(options =>
{
	options.DefaultRequestCulture = new RequestCulture("en-US");
});

builder.Services.RegisterDbService(builder.Configuration);

builder.WebHost.UseIISIntegration();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
}

app.UseSession();
app.UseCors();
app.UseAuthorization();
app.UseAuthentication();

app.UseStaticFiles();

app.UseRouting();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
