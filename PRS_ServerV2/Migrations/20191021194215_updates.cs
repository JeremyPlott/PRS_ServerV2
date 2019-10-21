using Microsoft.EntityFrameworkCore.Migrations;

namespace PRS_ServerV2.Migrations
{
    public partial class updates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<bool>(
                name: "IsReviewer",
                table: "Users",
                nullable: true,
                defaultValueSql: "((0))",
                oldClrType: typeof(bool),
                oldDefaultValueSql: "((0))");

            migrationBuilder.AlterColumn<bool>(
                name: "IsAdmin",
                table: "Users",
                nullable: true,
                defaultValueSql: "((0))",
                oldClrType: typeof(bool),
                oldDefaultValueSql: "((0))");

            migrationBuilder.AlterColumn<decimal>(
                name: "Total",
                table: "Requests",
                type: "decimal(11,2)",
                nullable: false,
                defaultValueSql: "((0))",
                oldClrType: typeof(decimal),
                oldType: "decimal(11,2)");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Vendors_VendorId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_VendorId",
                table: "Products");

            migrationBuilder.AlterColumn<bool>(
                name: "IsReviewer",
                table: "Users",
                nullable: false,
                defaultValueSql: "((0))",
                oldClrType: typeof(bool),
                oldNullable: true,
                oldDefaultValueSql: "((0))");

            migrationBuilder.AlterColumn<bool>(
                name: "IsAdmin",
                table: "Users",
                nullable: false,
                defaultValueSql: "((0))",
                oldClrType: typeof(bool),
                oldNullable: true,
                oldDefaultValueSql: "((0))");

            migrationBuilder.AlterColumn<decimal>(
                name: "Total",
                table: "Requests",
                type: "decimal(11,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(11,2)",
                oldDefaultValueSql: "((0))");

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
    }
}
