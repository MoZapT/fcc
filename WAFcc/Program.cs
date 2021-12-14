using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WAFcc.Areas.Identity;
using WAFcc.Data;
using WAFcc.DataServices;
using WAFcc.Interfaces.DataServices;
using WAFcc.Interfaces.Managers;
using WAFcc.Managers;

var builder = WebApplication.CreateBuilder(args);



// Add services to the container.
var fccConnectionString = builder.Configuration.GetConnectionString("FccConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(fccConnectionString));
builder.Services.AddDbContext<PersonDbContext>(options =>
    options.UseSqlServer(fccConnectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<IdentityUser>>();

builder.Services.AddScoped<IFccManager, FccManager>();
builder.Services.AddScoped<IPersonDataService, PersonDataService>();

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
