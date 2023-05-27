using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LabManagementSystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Campuses",
                columns: table => new
                {
                    CampusName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Campuses", x => x.CampusName);
                });

            migrationBuilder.CreateTable(
                name: "FormCategories",
                columns: table => new
                {
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormCategories", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "Staff",
                columns: table => new
                {
                    UserObjectId = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Role = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Staff", x => x.UserObjectId);
                });

            migrationBuilder.CreateTable(
                name: "Buildings",
                columns: table => new
                {
                    BuildingName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Image = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    CampusName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Buildings", x => x.BuildingName);
                    table.ForeignKey(
                        name: "FK_Buildings_Campuses_CampusName",
                        column: x => x.CampusName,
                        principalTable: "Campuses",
                        principalColumn: "CampusName",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FormDocumentationFiles",
                columns: table => new
                {
                    FileDocumentationId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    File = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ContentType = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormDocumentationFiles", x => x.FileDocumentationId);
                    table.ForeignKey(
                        name: "FK_FormDocumentationFiles_FormCategories_CategoryName",
                        column: x => x.CategoryName,
                        principalTable: "FormCategories",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FormDocumentationUrls",
                columns: table => new
                {
                    UrlDocumentationId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Url = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormDocumentationUrls", x => x.UrlDocumentationId);
                    table.ForeignKey(
                        name: "FK_FormDocumentationUrls_FormCategories_CategoryName",
                        column: x => x.CategoryName,
                        principalTable: "FormCategories",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Floors",
                columns: table => new
                {
                    FloorId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    BuildingName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    MapImage = table.Column<byte[]>(type: "varbinary(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Floors", x => x.FloorId);
                    table.ForeignKey(
                        name: "FK_Floors_Buildings_BuildingName",
                        column: x => x.BuildingName,
                        principalTable: "Buildings",
                        principalColumn: "BuildingName",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PurchaseRequests",
                columns: table => new
                {
                    RequestId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RequesterObjectId = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ItemCode = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Supplier = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(5,2)", precision: 5, scale: 2, nullable: false),
                    Quantity = table.Column<int>(type: "integer", nullable: false),
                    BudgetCode = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    COSHHRequired = table.Column<bool>(type: "bit", nullable: false),
                    DeliveryBuildingName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Processed = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseRequests", x => x.RequestId);
                    table.ForeignKey(
                        name: "FK_PurchaseRequests_Buildings_DeliveryBuildingName",
                        column: x => x.DeliveryBuildingName,
                        principalTable: "Buildings",
                        principalColumn: "BuildingName",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Rooms",
                columns: table => new
                {
                    Code = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    TimetableUrl = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    FloorId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rooms", x => x.Code);
                    table.ForeignKey(
                        name: "FK_Rooms_Floors_FloorId",
                        column: x => x.FloorId,
                        principalTable: "Floors",
                        principalColumn: "FloorId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    PurchaseRequestId = table.Column<int>(type: "integer", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PredictedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Delivered = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.PurchaseRequestId);
                    table.ForeignKey(
                        name: "FK_Orders_PurchaseRequests_PurchaseRequestId",
                        column: x => x.PurchaseRequestId,
                        principalTable: "PurchaseRequests",
                        principalColumn: "RequestId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Chemicals",
                columns: table => new
                {
                    ChemicalId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Formula = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    LocationRoomCode = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    LocationDescription = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    UnitType = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    TriggerLevel = table.Column<decimal>(type: "decimal(20,10)", precision: 20, scale: 10, nullable: false),
                    StockLevel = table.Column<decimal>(type: "decimal(20,10)", precision: 20, scale: 10, nullable: false),
                    Supplier = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ReviewDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OrderedByObjectId = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Hazards = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    ExpireDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PurityGrade = table.Column<decimal>(type: "decimal(20,10)", precision: 20, scale: 10, nullable: false),
                    BatchCode = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    CasCode = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ProductCode = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Purpose = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    MsdsUrl = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    COSHHCompleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chemicals", x => x.ChemicalId);
                    table.ForeignKey(
                        name: "FK_Chemicals_Rooms_LocationRoomCode",
                        column: x => x.LocationRoomCode,
                        principalTable: "Rooms",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Equipment",
                columns: table => new
                {
                    EquipmentId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    CustodianUserObjectId = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    GeneralInfo = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    LocationRoomCode = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    DocumentationUrl = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    DocumentationFile = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    DocumentationFileContentType = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Image = table.Column<byte[]>(type: "varbinary(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Equipment", x => x.EquipmentId);
                    table.ForeignKey(
                        name: "FK_Equipment_Rooms_LocationRoomCode",
                        column: x => x.LocationRoomCode,
                        principalTable: "Rooms",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RoomDocumentationFiles",
                columns: table => new
                {
                    FileDocumentationId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoomCode = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    File = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ContentType = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomDocumentationFiles", x => x.FileDocumentationId);
                    table.ForeignKey(
                        name: "FK_RoomDocumentationFiles_Rooms_RoomCode",
                        column: x => x.RoomCode,
                        principalTable: "Rooms",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RoomDocumentationUrls",
                columns: table => new
                {
                    UrlDocumentationId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoomCode = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Url = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomDocumentationUrls", x => x.UrlDocumentationId);
                    table.ForeignKey(
                        name: "FK_RoomDocumentationUrls_Rooms_RoomCode",
                        column: x => x.RoomCode,
                        principalTable: "Rooms",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RoomMapHotspots",
                columns: table => new
                {
                    RoomCode = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    PointOneX = table.Column<int>(type: "integer", nullable: false),
                    PointOneY = table.Column<int>(type: "integer", nullable: false),
                    PointTwoX = table.Column<int>(type: "integer", nullable: false),
                    PointTwoY = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomMapHotspots", x => x.RoomCode);
                    table.ForeignKey(
                        name: "FK_RoomMapHotspots_Rooms_RoomCode",
                        column: x => x.RoomCode,
                        principalTable: "Rooms",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LogbookEntries",
                columns: table => new
                {
                    EntryId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserObjectId = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    EquipmentId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogbookEntries", x => x.EntryId);
                    table.ForeignKey(
                        name: "FK_LogbookEntries_Equipment_EquipmentId",
                        column: x => x.EquipmentId,
                        principalTable: "Equipment",
                        principalColumn: "EquipmentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Buildings_CampusName",
                table: "Buildings",
                column: "CampusName");

            migrationBuilder.CreateIndex(
                name: "IX_Chemicals_LocationRoomCode",
                table: "Chemicals",
                column: "LocationRoomCode");

            migrationBuilder.CreateIndex(
                name: "IX_Equipment_LocationRoomCode",
                table: "Equipment",
                column: "LocationRoomCode");

            migrationBuilder.CreateIndex(
                name: "IX_Floors_BuildingName",
                table: "Floors",
                column: "BuildingName");

            migrationBuilder.CreateIndex(
                name: "IX_FormDocumentationFiles_CategoryName",
                table: "FormDocumentationFiles",
                column: "CategoryName");

            migrationBuilder.CreateIndex(
                name: "IX_FormDocumentationUrls_CategoryName",
                table: "FormDocumentationUrls",
                column: "CategoryName");

            migrationBuilder.CreateIndex(
                name: "IX_LogbookEntries_EquipmentId",
                table: "LogbookEntries",
                column: "EquipmentId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseRequests_DeliveryBuildingName",
                table: "PurchaseRequests",
                column: "DeliveryBuildingName");

            migrationBuilder.CreateIndex(
                name: "IX_RoomDocumentationFiles_RoomCode",
                table: "RoomDocumentationFiles",
                column: "RoomCode");

            migrationBuilder.CreateIndex(
                name: "IX_RoomDocumentationUrls_RoomCode",
                table: "RoomDocumentationUrls",
                column: "RoomCode");

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_FloorId",
                table: "Rooms",
                column: "FloorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Chemicals");

            migrationBuilder.DropTable(
                name: "FormDocumentationFiles");

            migrationBuilder.DropTable(
                name: "FormDocumentationUrls");

            migrationBuilder.DropTable(
                name: "LogbookEntries");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "RoomDocumentationFiles");

            migrationBuilder.DropTable(
                name: "RoomDocumentationUrls");

            migrationBuilder.DropTable(
                name: "RoomMapHotspots");

            migrationBuilder.DropTable(
                name: "Staff");

            migrationBuilder.DropTable(
                name: "FormCategories");

            migrationBuilder.DropTable(
                name: "Equipment");

            migrationBuilder.DropTable(
                name: "PurchaseRequests");

            migrationBuilder.DropTable(
                name: "Rooms");

            migrationBuilder.DropTable(
                name: "Floors");

            migrationBuilder.DropTable(
                name: "Buildings");

            migrationBuilder.DropTable(
                name: "Campuses");
        }
    }
}
