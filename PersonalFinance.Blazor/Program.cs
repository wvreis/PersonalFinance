global using Radzen;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using PersonalFinance.Blazor.Areas.Identity;
using PersonalFinance.Blazor.Data;
using PersonalFinance.Blazor.Helpers;
using PersonalFinance.Blazor.Helpers.APIs;
using PersonalFinance.Blazor.Services;
using System.Net.Mime;

var builder = WebApplication.CreateBuilder(args);

// Get Configuration from envaironment command.
string databaseHost = builder.Configuration["DATABASEHOST"] ?? string.Empty;
string databasePort = builder.Configuration["DATABASEPORT"] ?? string.Empty;
string databaseName = builder.Configuration["DATABASENAME"] ?? string.Empty;
string databaseUser = builder.Configuration["DATABASEUSER"] ?? string.Empty;
string databasePassword = builder.Configuration["POSTGRES_PASSWORD"] ?? string.Empty;

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
var connectionStringDocker = $"Host={databaseHost};Port={databasePort};Database={databaseName};Username={databaseUser};Password={databasePassword}";

builder.Services.AddDbContext<AppDb>(options =>
    options.UseNpgsql(databaseHost.IsNullOrEmpty() ? connectionString : connectionStringDocker));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<AppDb>();
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<IdentityUser>>();
builder.Services.AddSwaggerGen(options => options.CustomSchemaIds(type => type.FullName));

builder.Services.AddScoped<HttpClient>(s => {
    try {
        var uriHelper = s.GetRequiredService<NavigationManager>();
        return new HttpClient {
            BaseAddress = new Uri(uriHelper.BaseUri),
            Timeout = TimeSpan.FromMinutes(15)
        };
    }
    catch {
        return new();
    }
    // Creating the URI helper needs to wait until the JS Runtime is initialized, so defer it.
});

builder.Services.AddScoped<HttpContextAccessor>();
builder.Services.AddScoped<DialogService>();

builder.Services.AddControllersWithViews().AddNewtonsoftJson(options => {
    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
    options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
});

builder.Services.AddControllers().ConfigureApiBehaviorOptions(options => {
    options.InvalidModelStateResponseFactory = context => {
        var result = new ValidationFailedResult(context.ModelState);
        result.ContentTypes.Add(MediaTypeNames.Application.Json);
        return result;
    };
});

builder.Services.AddScoped<HttpClient>(s => {
    try {
        var uriHelper = s.GetRequiredService<NavigationManager>();
        return new HttpClient {
            BaseAddress = new Uri(uriHelper.BaseUri),
            Timeout = TimeSpan.FromMinutes(15)
        };
    }
    catch {
        return new();
    }
});

#region APIs
builder.Services.AddScoped<AccountsAPI>();
builder.Services.AddScoped<TransactionsAPI>();
builder.Services.AddScoped<PanelAPI>();
#endregion

#region SERVICES
builder.Services.AddScoped<ISpellCheckerService, SpellCheckerService>();
#endregion

#region HOSTED SERVICES
builder.Services.AddHostedService<UpdataDbService>();
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
    app.UseMigrationsEndPoint();

    app.UseSwagger();
    app.UseSwaggerUI(c => {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Blazor API V1");
    });
}
else {
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllers();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");


app.Run();
