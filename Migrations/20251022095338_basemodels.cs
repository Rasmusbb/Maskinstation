using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Maskinstation.Migrations
{
    /// <inheritdoc />
    public partial class basemodels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Brands",
                columns: table => new
                {
                    BrandID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BrandName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brands", x => x.BrandID);
                });

            migrationBuilder.CreateTable(
                name: "images",
                columns: table => new
                {
                    ImageID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ImageURL = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MachineID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UserID = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_images", x => x.ImageID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    profilImageImageID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserID);
                    table.ForeignKey(
                        name: "FK_Users_images_profilImageImageID",
                        column: x => x.profilImageImageID,
                        principalTable: "images",
                        principalColumn: "ImageID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Machines",
                columns: table => new
                {
                    MachineID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Model = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    BrandID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Machines", x => x.MachineID);
                    table.ForeignKey(
                        name: "FK_Machines_Brands_BrandID",
                        column: x => x.BrandID,
                        principalTable: "Brands",
                        principalColumn: "BrandID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Machines_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID");
                });

            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    TagID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TagName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TagType = table.Column<int>(type: "int", nullable: false),
                    MachineID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UserID = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.TagID);
                    table.ForeignKey(
                        name: "FK_Tags_Machines_MachineID",
                        column: x => x.MachineID,
                        principalTable: "Machines",
                        principalColumn: "MachineID");
                    table.ForeignKey(
                        name: "FK_Tags_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Brands_ImageID",
                table: "Brands",
                column: "ImageID");

            migrationBuilder.CreateIndex(
                name: "IX_images_MachineID",
                table: "images",
                column: "MachineID");

            migrationBuilder.CreateIndex(
                name: "IX_images_UserID",
                table: "images",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Machines_BrandID",
                table: "Machines",
                column: "BrandID");

            migrationBuilder.CreateIndex(
                name: "IX_Machines_UserID",
                table: "Machines",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Tags_MachineID",
                table: "Tags",
                column: "MachineID");

            migrationBuilder.CreateIndex(
                name: "IX_Tags_UserID",
                table: "Tags",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Users_profilImageImageID",
                table: "Users",
                column: "profilImageImageID");

            migrationBuilder.AddForeignKey(
                name: "FK_Brands_images_ImageID",
                table: "Brands",
                column: "ImageID",
                principalTable: "images",
                principalColumn: "ImageID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_images_Machines_MachineID",
                table: "images",
                column: "MachineID",
                principalTable: "Machines",
                principalColumn: "MachineID");

            migrationBuilder.AddForeignKey(
                name: "FK_images_Users_UserID",
                table: "images",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "UserID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Brands_images_ImageID",
                table: "Brands");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_images_profilImageImageID",
                table: "Users");

            migrationBuilder.DropTable(
                name: "Tags");

            migrationBuilder.DropTable(
                name: "images");

            migrationBuilder.DropTable(
                name: "Machines");

            migrationBuilder.DropTable(
                name: "Brands");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
