using Microsoft.EntityFrameworkCore.Migrations;

namespace PRS_ServerV2.Migrations
{
    public partial class uncommentedlinesinDbContext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RequestLines_Requests_RequestId",
                table: "RequestLines");

            migrationBuilder.DropForeignKey(
                name: "FK_Requests_Users_UserId",
                table: "Requests");

            migrationBuilder.AlterColumn<bool>(
                name: "IsReviewer",
                table: "Users",
                nullable: false,
                defaultValueSql: "((0))",
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<bool>(
                name: "IsAdmin",
                table: "Users",
                nullable: false,
                defaultValueSql: "((0))",
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Requests",
                maxLength: 10,
                nullable: false,
                defaultValueSql: "('NEW')",
                oldClrType: typeof(string),
                oldMaxLength: 10);

            migrationBuilder.AlterColumn<string>(
                name: "DeliveryMode",
                table: "Requests",
                maxLength: 20,
                nullable: false,
                defaultValueSql: "('Pickup')",
                oldClrType: typeof(string),
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<int>(
                name: "Quantity",
                table: "RequestLines",
                nullable: false,
                defaultValueSql: "((1))",
                oldClrType: typeof(int));

            migrationBuilder.CreateIndex(
                name: "IX_Vendors_Code",
                table: "Vendors",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Username",
                table: "Users",
                column: "Username",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "PartNbr",
                table: "Products",
                column: "PartNbr",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_RequestId",
                table: "RequestLines",
                column: "RequestId",
                principalTable: "Requests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserId",
                table: "Requests",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RequestId",
                table: "RequestLines");

            migrationBuilder.DropForeignKey(
                name: "FK_UserId",
                table: "Requests");

            migrationBuilder.DropIndex(
                name: "IX_Vendors_Code",
                table: "Vendors");

            migrationBuilder.DropIndex(
                name: "IX_Users_Username",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "PartNbr",
                table: "Products");

            migrationBuilder.AlterColumn<bool>(
                name: "IsReviewer",
                table: "Users",
                nullable: false,
                oldClrType: typeof(bool),
                oldDefaultValueSql: "((0))");

            migrationBuilder.AlterColumn<bool>(
                name: "IsAdmin",
                table: "Users",
                nullable: false,
                oldClrType: typeof(bool),
                oldDefaultValueSql: "((0))");

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Requests",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 10,
                oldDefaultValueSql: "('NEW')");

            migrationBuilder.AlterColumn<string>(
                name: "DeliveryMode",
                table: "Requests",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 20,
                oldDefaultValueSql: "('Pickup')");

            migrationBuilder.AlterColumn<int>(
                name: "Quantity",
                table: "RequestLines",
                nullable: false,
                oldClrType: typeof(int),
                oldDefaultValueSql: "((1))");

            migrationBuilder.AddForeignKey(
                name: "FK_RequestLines_Requests_RequestId",
                table: "RequestLines",
                column: "RequestId",
                principalTable: "Requests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_Users_UserId",
                table: "Requests",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
