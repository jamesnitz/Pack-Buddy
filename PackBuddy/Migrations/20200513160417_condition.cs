using Microsoft.EntityFrameworkCore.Migrations;

namespace PackBuddy.Migrations
{
    public partial class condition : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Condtion",
                table: "Gears");

            migrationBuilder.AddColumn<string>(
                name: "Condition",
                table: "Gears",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000000-ffff-ffff-ffff-ffffffffffff",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "3245bab5-571d-4640-9f92-19b4f62f0e8d", "AQAAAAEAACcQAAAAEHwPkwyUAdXFC/fNk10ZZNX0msUWS0xBlLe44Eiaq5oIsUl2DEXca6f1VjmZOrfsPg==" });

            migrationBuilder.UpdateData(
                table: "Gears",
                keyColumn: "Id",
                keyValue: 1,
                column: "Condition",
                value: "Like New");

            migrationBuilder.UpdateData(
                table: "Gears",
                keyColumn: "Id",
                keyValue: 2,
                column: "Condition",
                value: "Slightly used");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Condition",
                table: "Gears");

            migrationBuilder.AddColumn<string>(
                name: "Condtion",
                table: "Gears",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000000-ffff-ffff-ffff-ffffffffffff",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "e2412d6c-563f-4749-9009-bba87dd21dd2", "AQAAAAEAACcQAAAAEDjpzLuwSuGRRCtuetsEiOxDbuuLNl7ZMXq5dwwnld7k+lszKEDMQrDrl6xHlhRlfg==" });

            migrationBuilder.UpdateData(
                table: "Gears",
                keyColumn: "Id",
                keyValue: 1,
                column: "Condtion",
                value: "Like New");

            migrationBuilder.UpdateData(
                table: "Gears",
                keyColumn: "Id",
                keyValue: 2,
                column: "Condtion",
                value: "Slightly used");
        }
    }
}
