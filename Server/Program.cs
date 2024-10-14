using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Radzen;
using Microsoft.EntityFrameworkCore;
using Microsoft.OData.ModelBuilder;
using Microsoft.AspNetCore.OData;
using VersaControl.Server.Data;
using Microsoft.AspNetCore.Identity;
using VersaControl.Server.Models;
using Microsoft.AspNetCore.Components.Authorization;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddRadzenComponents();
builder.Services.AddRadzenCookieThemeService(options =>
{
    options.Name = "VersaControlTheme";
    options.Duration = TimeSpan.FromDays(365);
});
builder.Services.AddSingleton(sp =>
{
    // Get the address that the app is currently running at
    var server = sp.GetRequiredService<IServer>();
    var addressFeature = server.Features.Get<IServerAddressesFeature>();
    string baseAddress = addressFeature.Addresses.First();
    return new HttpClient
    {
        BaseAddress = new Uri(baseAddress)
    };
});
builder.Services.AddScoped<VersaControl.Server.versacontrolService>();
builder.Services.AddDbContext<VersaControl.Server.Data.versacontrolContext>(options =>
{
    options.UseMySql(builder.Configuration.GetConnectionString("versacontrolConnection"), ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("versacontrolConnection")));
});
builder.Services.AddControllers().AddOData(opt =>
{
    var oDataBuilderversacontrol = new ODataConventionModelBuilder();
    oDataBuilderversacontrol.EntitySet<VersaControl.Server.Models.versacontrol.Efmigrationshistory>("Efmigrationshistories");
    oDataBuilderversacontrol.EntitySet<VersaControl.Server.Models.versacontrol.AdminSetting>("AdminSettings");
    oDataBuilderversacontrol.EntitySet<VersaControl.Server.Models.versacontrol.Anexa>("Anexas");
    oDataBuilderversacontrol.EntitySet<VersaControl.Server.Models.versacontrol.Aspnetroleclaim>("Aspnetroleclaims");
    oDataBuilderversacontrol.EntitySet<VersaControl.Server.Models.versacontrol.Aspnetrole>("Aspnetroles");
    oDataBuilderversacontrol.EntitySet<VersaControl.Server.Models.versacontrol.Aspnetuserclaim>("Aspnetuserclaims");
    oDataBuilderversacontrol.EntitySet<VersaControl.Server.Models.versacontrol.Aspnetuserlogin>("Aspnetuserlogins").EntityType.HasKey(entity => new { entity.LoginProvider, entity.ProviderKey });
    oDataBuilderversacontrol.EntitySet<VersaControl.Server.Models.versacontrol.Aspnetuserrole>("Aspnetuserroles").EntityType.HasKey(entity => new { entity.UserId, entity.RoleId });
    oDataBuilderversacontrol.EntitySet<VersaControl.Server.Models.versacontrol.Aspnetuser>("Aspnetusers");
    oDataBuilderversacontrol.EntitySet<VersaControl.Server.Models.versacontrol.Aspnetusertoken>("Aspnetusertokens").EntityType.HasKey(entity => new { entity.UserId, entity.LoginProvider, entity.Name });
    oDataBuilderversacontrol.EntitySet<VersaControl.Server.Models.versacontrol.Beneficiari>("Beneficiaris");
    oDataBuilderversacontrol.EntitySet<VersaControl.Server.Models.versacontrol.Contracte>("Contractes");
    oDataBuilderversacontrol.EntitySet<VersaControl.Server.Models.versacontrol.Contractori>("Contractoris");
    oDataBuilderversacontrol.EntitySet<VersaControl.Server.Models.versacontrol.Monede>("Monedes");
    oDataBuilderversacontrol.EntitySet<VersaControl.Server.Models.versacontrol.Roluri>("Roluris");
    oDataBuilderversacontrol.EntitySet<VersaControl.Server.Models.versacontrol.TipuriContract>("TipuriContracts");
    opt.AddRouteComponents("odata/versacontrol", oDataBuilderversacontrol.GetEdmModel()).Count().Filter().OrderBy().Expand().Select().SetMaxTop(null).TimeZone = TimeZoneInfo.Utc;
});
builder.Services.AddScoped<VersaControl.Client.versacontrolService>();
builder.Services.AddHttpClient("VersaControl.Server").ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler { UseCookies = false }).AddHeaderPropagation(o => o.Headers.Add("Cookie"));
builder.Services.AddHeaderPropagation(o => o.Headers.Add("Cookie"));
builder.Services.AddAuthentication();
builder.Services.AddAuthorization();
builder.Services.AddScoped<VersaControl.Client.SecurityService>();
builder.Services.AddDbContext<ApplicationIdentityDbContext>(options =>
{
    options.UseMySql(builder.Configuration.GetConnectionString("versacontrolConnection"), ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("versacontrolConnection")));
});
builder.Services.AddIdentity<ApplicationUser, ApplicationRole>().AddEntityFrameworkStores<ApplicationIdentityDbContext>().AddDefaultTokenProviders();
builder.Services.AddControllers().AddOData(o =>
{
    var oDataBuilder = new ODataConventionModelBuilder();
    oDataBuilder.EntitySet<ApplicationUser>("ApplicationUsers");
    var usersType = oDataBuilder.StructuralTypes.First(x => x.ClrType == typeof(ApplicationUser));
    usersType.AddProperty(typeof(ApplicationUser).GetProperty(nameof(ApplicationUser.Password)));
    usersType.AddProperty(typeof(ApplicationUser).GetProperty(nameof(ApplicationUser.ConfirmPassword)));
    oDataBuilder.EntitySet<ApplicationRole>("ApplicationRoles");
    o.AddRouteComponents("odata/Identity", oDataBuilder.GetEdmModel()).Count().Filter().OrderBy().Expand().Select().SetMaxTop(null).TimeZone = TimeZoneInfo.Utc;
});
builder.Services.AddScoped<AuthenticationStateProvider, VersaControl.Client.ApplicationAuthenticationStateProvider>();
var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseBlazorFrameworkFiles();
app.UseHeaderPropagation();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapRazorPages();
app.MapControllers();
app.MapFallbackToPage("/_Host");
app.Services.CreateScope().ServiceProvider.GetRequiredService<ApplicationIdentityDbContext>().Database.Migrate();
app.Run();