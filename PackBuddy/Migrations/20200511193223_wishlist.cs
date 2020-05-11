using Microsoft.EntityFrameworkCore.Migrations;

namespace PackBuddy.Migrations
{
    public partial class wishlist : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Favorite",
                table: "WishLists");

            migrationBuilder.AddColumn<string>(
                name: "ProductId",
                table: "WishLists",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000000-ffff-ffff-ffff-ffffffffffff",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "e2412d6c-563f-4749-9009-bba87dd21dd2", "AQAAAAEAACcQAAAAEDjpzLuwSuGRRCtuetsEiOxDbuuLNl7ZMXq5dwwnld7k+lszKEDMQrDrl6xHlhRlfg==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "WishLists");

            migrationBuilder.AddColumn<bool>(
                name: "Favorite",
                table: "WishLists",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000000-ffff-ffff-ffff-ffffffffffff",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "b67f2625-4e23-4129-855e-d0ebcf44e36b", "AQAAAAEAACcQAAAAEPGdSdkiG8gRkviN93p8/THRsFCODmi8//t1LkSkfWd1OhuoDfxfJBKGBTFa//7sQg==" });
        }
    }
}
