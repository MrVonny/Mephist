using Microsoft.EntityFrameworkCore.Migrations;

namespace Mephist.Migrations
{
    public partial class AWS : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Medias_AspNetUsers_UserId",
                table: "Medias");

            migrationBuilder.RenameColumn(
                name: "PartialMediaPath",
                table: "Medias",
                newName: "Key");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Medias",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Medias_AspNetUsers_UserId",
                table: "Medias",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Medias_AspNetUsers_UserId",
                table: "Medias");

            migrationBuilder.RenameColumn(
                name: "Key",
                table: "Medias",
                newName: "PartialMediaPath");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Medias",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_Medias_AspNetUsers_UserId",
                table: "Medias",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
