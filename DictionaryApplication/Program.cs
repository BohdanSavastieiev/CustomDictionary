using DictionaryApplication.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using DictionaryApplication.Services;
using DictionaryApplication.Models;
using DictionaryApplication.Repositories;
using Microsoft.AspNetCore.Authentication.Cookies;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using DictionaryApplication.Clients;
using DictionaryApplication.Mappers;
using AutoMapper;
using System;

var builder = WebApplication.CreateBuilder(args);


//var keyVaultEndpoint = "https://dictionarykeyvault.vault.azure.net/";
//var secretClient = new SecretClient(new Uri(keyVaultEndpoint), new DefaultAzureCredential());
//var emailHostSecret = secretClient.GetSecret("EmailHost");
//var emailUsernameSecret = secretClient.GetSecret("EmailUsername");
//var emailPasswordSecret = secretClient.GetSecret("EmailPassword");
//builder.Configuration["EmailHost"] = emailHostSecret.Value.Value;
//builder.Configuration["EmailUsername"] = emailUsernameSecret.Value.Value;
//builder.Configuration["EmailPassword"] = emailPasswordSecret.Value.Value;

//var connectionString = secretClient.GetSecret("DefaultConnection").Value.Value
//    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' was not found.");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(connectionString));


//builder.Services.AddDbContext<ApplicationDbContext>(options =>
//    options.UseSqlServer(connectionString, 
//        sqlServerOptionsAction: sqlOptions =>
//        {
//             sqlOptions.EnableRetryOnFailure(
//                maxRetryCount: 10,
//                maxRetryDelay: TimeSpan.FromSeconds(30),
//                errorNumbersToAdd: null);
//        }));
//builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddTransient(typeof(IDbRepository<>), typeof(DbRepository<>));
builder.Services.AddScoped<IUserDictionaryRepository, UserDictionaryRepository>();
builder.Services.AddScoped<ILexemeDetailsRepository, LexemeDetailsRepository>();
builder.Services.AddScoped<ILexemeInputRepository, LexemeInputRepository>();
builder.Services.AddScoped<ILexemeTestAttemptRepository, LexemeTestAttemptRepository>();


builder.Services.AddDefaultIdentity<ApplicationUser>()
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
    options.SignIn.RequireConfirmedAccount = true;

    // User settings.
    options.User.AllowedUserNameCharacters =
    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
    options.User.RequireUniqueEmail = true;
});

builder.Services.AddHttpClient("LingvoInfoApi", client =>
{
    client.BaseAddress = new Uri(builder.Configuration["LingvoInfoApiSettings:BaseUrl"]
        ?? throw new InvalidOperationException("BaseUrl for LingvoInfo API is not configured."));
})
.ConfigurePrimaryHttpMessageHandler(() =>
{
    return new HttpClientHandler
    {
        ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
    };
});

builder.Services.AddTransient<LingvoInfoApiClient>();

builder.Services.AddTransient<ILingvoInfoMapper, LingvoInfoToLexemeInputMapper>();

builder.Services.AddScoped<KnowledgeTestService>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddTransient<ILingvoInfoService, LingvoInfoService>();

var mappingConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new MappingProfile());
});

IMapper mapper = mappingConfig.CreateMapper();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());



builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(1800);
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<ApplicationDbContext>();
    var lingvoInfoService = services.GetRequiredService<ILingvoInfoService>();
    var lexemeInputRepository= services.GetRequiredService<ILexemeInputRepository>();
    await SeedData.Initialize(context, lingvoInfoService, lexemeInputRepository);
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

public partial class Program { }