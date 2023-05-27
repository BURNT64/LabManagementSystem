using Azure.Identity;
using LabManagementSystem.API.ActiveDirectory;
using LabManagementSystem.Data;
using LabManagementSystem.Data.Repositories.Buildings;
using LabManagementSystem.Data.Repositories.Campuses;
using LabManagementSystem.Data.Repositories.Chemicals;
using LabManagementSystem.Data.Repositories.Equipment;
using LabManagementSystem.Data.Repositories.Floors;
using LabManagementSystem.Data.Repositories.FormCategories;
using LabManagementSystem.Data.Repositories.FormDocumentationFiles;
using LabManagementSystem.Data.Repositories.FormDocumentationUrls;
using LabManagementSystem.Data.Repositories.LogbookEntries;
using LabManagementSystem.Data.Repositories.Orders;
using LabManagementSystem.Data.Repositories.PurchaseRequests;
using LabManagementSystem.Data.Repositories.RoomDocumentationFiles;
using LabManagementSystem.Data.Repositories.RoomDocumentationUrls;
using LabManagementSystem.Data.Repositories.RoomMapHotspots;
using LabManagementSystem.Data.Repositories.Rooms;
using LabManagementSystem.Data.Repositories.Staff;
using Microsoft.EntityFrameworkCore;
using Microsoft.Graph;
using Microsoft.Identity.Web;
using Microsoft.Identity.Web.UI;
using WebApplication = Microsoft.AspNetCore.Builder.WebApplication;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<LabManagementContext>(options =>
{
    options.UseLazyLoadingProxies()
        .UseSqlServer(
            builder.Configuration.GetConnectionString("LabManagementContext") ?? 
            throw new InvalidOperationException("Connection string \"LabManagementContext\" not found.")
        );
});
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddMicrosoftIdentityWebAppAuthentication(builder.Configuration)
    .EnableTokenAcquisitionToCallDownstreamApi()
    .AddInMemoryTokenCaches()
    .AddMicrosoftGraph(x =>
    {
        string tenantId = builder.Configuration.GetSection("AzureAd").GetValue<string>("TenantId") ?? 
                          throw new InvalidOperationException("AzureAd value \"TenantId\" must be configured.");
        string clientId = builder.Configuration.GetSection("AzureAd").GetValue<string>("ClientId") ?? 
                          throw new InvalidOperationException("AzureAd value \"ClientId\" must be configured.");
        string clientSecret = builder.Configuration.GetSection("AzureAd").GetValue<string>("ClientSecret") ?? 
                          throw new InvalidOperationException("AzureAd value \"ClientSecret\" must be configured.");
        ClientSecretCredential clientSecretCredential = new ClientSecretCredential(tenantId, clientId, clientSecret);
        return new GraphServiceClient(clientSecretCredential);
    }, new[] {".default"});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy(Role.Administrator, policy => policy.RequireRole(Role.Administrator));
});

builder.Services.AddRazorPages(options =>
    {
        options.Conventions.AuthorizeFolder("/Admin", Role.Administrator);
    })
    .AddMicrosoftIdentityUI();

// Register Active Directory User Repository with DI container.
builder.Services.AddScoped<IActiveDirectoryUserRepository, ActiveDirectoryUserRepository>();

// Register database repositories with DI container.
builder.Services.AddScoped<IBuildingRepository, BuildingRepository>();
builder.Services.AddScoped<ICampusRepository, CampusRepository>();
builder.Services.AddScoped<IChemicalRepository, ChemicalRepository>();
builder.Services.AddScoped<IEquipmentRepository, EquipmentRepository>();
builder.Services.AddScoped<IFloorRepository, FloorRepository>();
builder.Services.AddScoped<IFormCategoryRepository, FormCategoryRepository>();
builder.Services.AddScoped<IFormDocumentationFileRepository, FormDocumentationFileRepository>();
builder.Services.AddScoped<IFormDocumentationUrlRepository, FormDocumentationUrlRepository>();
builder.Services.AddScoped<ILogbookEntryRepository, LogbookEntryRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IPurchaseRequestRepository, PurchaseRequestRepository>();
builder.Services.AddScoped<IRoomRepository, RoomRepository>();
builder.Services.AddScoped<IRoomDocumentationFileRepository, RoomDocumentationFileRepository>();
builder.Services.AddScoped<IRoomDocumentationUrlRepository, RoomDocumentationUrlRepository>();
builder.Services.AddScoped<IRoomMapHotspotRepository, RoomMapHotspotRepository>();
builder.Services.AddScoped<IStaffRepository, StaffRepository>();

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

app.MapRazorPages();
app.MapControllers();

app.Run();
