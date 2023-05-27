using Elfie.Serialization;
using Microsoft.EntityFrameworkCore;
using LabManagementSystem.Data.Models;

namespace LabManagementSystem.Data;

public class LabManagementContext : DbContext
{
    public LabManagementContext(DbContextOptions<LabManagementContext> options) : base(options) {}

    public DbSet<BuildingModel> Buildings { get; set; } = null!;

    public DbSet<CampusModel> Campuses { get; set; } = null!;

    public DbSet<ChemicalModel> Chemicals { get; set; } = null!;

    public DbSet<EquipmentModel> Equipment { get; set; } = null!;

    public DbSet<FloorModel> Floors { get; set; } = null!;

    public DbSet<FormCategoryModel> FormCategories { get; set; } = null!;

    public DbSet<FormDocumentationFileModel> FormDocumentationFiles { get; set; } = null!;

    public DbSet<FormDocumentationUrlModel> FormDocumentationUrls { get; set; } = null!;

    public DbSet<LogbookEntryModel> LogbookEntries { get; set; } = null!;

    public DbSet<OrderModel> Orders { get; set; } = null!;

    public DbSet<PurchaseRequestModel> PurchaseRequests { get; set; } = null!;
    
    public DbSet<RoomModel> Rooms { get; set; } = null!;

    public DbSet<RoomDocumentationFileModel> RoomDocumentationFiles { get; set; } = null!;

    public DbSet<RoomDocumentationUrlModel> RoomDocumentationUrls { get; set; } = null!;

    public DbSet<RoomMapHotspotModel> RoomMapHotspots { get; set; } = null!;

    public DbSet<StaffModel> Staff { get; set; } = null!;
}