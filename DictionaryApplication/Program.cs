using DictionaryApplication.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using DictionaryApplication.Services;
using DictionaryApplication.Models;
using DictionaryApplication.Repositories;
using Microsoft.AspNetCore.Authentication.Cookies;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;

var builder = WebApplication.CreateBuilder(args);

// Authenticate to Key Vault using the Azure Identity library
var credential = new DefaultAzureCredential();

// Create a secret client using the Key Vault URL and the Azure Identity credential
var secretClient = new SecretClient(new Uri("https://customdictionarykeyvault.vault.azure.net/"), credential);

// Retrieve the secret value (connection string)
var secret = secretClient.GetSecret("CustomDictionaryDbConnectionString");

//var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(secret.Value.Value));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddTransient(typeof(IDbRepository<>), typeof(DbRepository<>));
builder.Services.AddScoped<IUserDictionaryRepository, UserDictionaryRepository>();
builder.Services.AddScoped<ILexemeDetailsRepository, LexemeDetailsRepository>();
builder.Services.AddScoped<ILexemeInputRepository, LexemeInputRepository>();
builder.Services.AddScoped<ILexemeTestAttemptRepository, LexemeTestAttemptRepository>();


builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultUI()
    .AddDefaultTokenProviders();
builder.Services.AddScoped<UserManager<ApplicationUser>>();
builder.Services.AddRazorPages();

builder.Services.Configure<IdentityOptions>(options =>
{
    // Password settings.
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength = 8;
    options.Password.RequiredUniqueChars = 0;
    options.SignIn.RequireConfirmedAccount = false;

    // User settings.
    options.User.AllowedUserNameCharacters =
    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
    options.User.RequireUniqueEmail = true;
});

builder.Services.AddScoped<KnowledgeTestService>();

builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(1800);
    //options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<ApplicationDbContext>();
    SeedData.Initialize(context);
}

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

app.UseStaticFiles();
app.UseSession();


app.MapRazorPages();

app.Run();
