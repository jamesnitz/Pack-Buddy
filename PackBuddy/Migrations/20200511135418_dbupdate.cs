using Microsoft.EntityFrameworkCore.Migrations;


namespace PackBuddy.Migrations
{
    public partial class dbupdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WishList_AspNetUsers_ApplicationuserId",
                table: "WishList");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WishList",
                table: "WishList");

            migrationBuilder.RenameTable(
                name: "WishList",
                newName: "WishLists");

            migrationBuilder.RenameIndex(
                name: "IX_WishList_ApplicationuserId",
                table: "WishLists",
                newName: "IX_WishLists_ApplicationuserId");

            migrationBuilder.AddColumn<bool>(
                name: "Favorite",
                table: "WishLists",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "PrimaryImage",
                table: "WishLists",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_WishLists",
                table: "WishLists",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000000-ffff-ffff-ffff-ffffffffffff",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "b67f2625-4e23-4129-855e-d0ebcf44e36b", "AQAAAAEAACcQAAAAEPGdSdkiG8gRkviN93p8/THRsFCODmi8//t1LkSkfWd1OhuoDfxfJBKGBTFa//7sQg==" });

            migrationBuilder.AddForeignKey(
                name: "FK_WishLists_AspNetUsers_ApplicationuserId",
                table: "WishLists",
                column: "ApplicationuserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WishLists_AspNetUsers_ApplicationuserId",
                table: "WishLists");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WishLists",
                table: "WishLists");

            migrationBuilder.DropColumn(
                name: "Favorite",
                table: "WishLists");

            migrationBuilder.DropColumn(
                name: "PrimaryImage",
                table: "WishLists");

            migrationBuilder.RenameTable(
                name: "WishLists",
                newName: "WishList");

            migrationBuilder.RenameIndex(
                name: "IX_WishLists_ApplicationuserId",
                table: "WishList",
                newName: "IX_WishList_ApplicationuserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WishList",
                table: "WishList",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000000-ffff-ffff-ffff-ffffffffffff",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "77b37d38-6118-49ec-9079-ea7f8850284c", "AQAAAAEAACcQAAAAEKLmiYEHhHI2CNfT6iypMurU957ctoDQhkonZPokG5n3HZpo7JwU0wxoF8IpNkoO2Q==" });

            migrationBuilder.AddForeignKey(
                name: "FK_WishList_AspNetUsers_ApplicationuserId",
                table: "WishList",
                column: "ApplicationuserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
