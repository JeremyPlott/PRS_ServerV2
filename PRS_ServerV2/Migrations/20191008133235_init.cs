using Microsoft.EntityFrameworkCore.Migrations;

namespace PRS_ServerV2.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Vendors_VendorId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_VendorId",
                table: "Products");

            migrationBuilder.AddColumn<int>(
                name: "VendorsId",
                table: "Products",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_VendorsId",
                table: "Products",
                column: "VendorsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Vendors_VendorsId",
                table: "Products",
                column: "VendorsId",
                principalTable: "Vendors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Vendors_VendorsId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_VendorsId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "VendorsId",
                table: "Products");

            migrationBuilder.CreateIndex(
                name: "IX_Products_VendorId",
                table: "Products",
                column: "VendorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Vendors_VendorId",
                table: "Products",
                column: "VendorId",
                principalTable: "Vendors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
