using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using WAFcc.Areas.Identity;
using WAFcc.Data;
using WAFcc.DataServices;
using WAFcc.Interfaces.DataServices;

var builder = WebApplication.CreateBuilder(args);



// Add services to the container.
//var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
var fccConnectionString = builder.Configuration.GetConnectionString("FccConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(fccConnectionString));
builder.Services.AddDbContext<PersonDbContext>(options =>
    options.UseSqlServer(fccConnectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();
//builder.Services.AddLocalization(opts => opts.ResourcesPath = "Resources");
//builder.Services.Configure<RequestLocalizationOptions>(
//    opts =>
//    {
//        var supportedCultures = new List<CultureInfo>
//        {
//                new CultureInfo("en-US"),
//                new CultureInfo("en"),
//                new CultureInfo("de-DE"),
//                new CultureInfo("de"),
//                new CultureInfo("ru-RU"),
//                new CultureInfo("ru"),
//        };

//        opts.DefaultRequestCulture = new RequestCulture("ru-RU");
//            // Formatting numbers, dates, etc.
//            opts.SupportedCultures = supportedCultures;
//            // UI strings that we have localized.
//            opts.SupportedUICultures = supportedCultures;
//    });
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<IdentityUser>>();

// Supply HttpClient instances that include access tokens when making requests to the server project
builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("WebAppFcc.ServerAPI"));

builder.Services.AddScoped<IPersonDataService, PersonDataService>((sp) =>
    new PersonDataService(sp.GetRequiredService<IHttpClientFactory>().CreateClient("WebAppFcc.ServerAPI")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
