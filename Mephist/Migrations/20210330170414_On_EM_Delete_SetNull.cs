using Microsoft.EntityFrameworkCore.Migrations;

namespace Mephist.Migrations
{
    public partial class On_EM_Delete_SetNull : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Medias_EducationalMaterials_EducationalMaterialId",
                table: "Medias");

            migrationBuilder.AddForeignKey(
                name: "FK_Medias_EducationalMaterials_EducationalMaterialId",
                table: "Medias",
                column: "EducationalMaterialId",
                principalTable: "EducationalMaterials",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Medias_EducationalMaterials_EducationalMaterialId",
                table: "Medias");

            migrationBuilder.AddForeignKey(
                name: "FK_Medias_EducationalMaterials_EducationalMaterialId",
                table: "Medias",
                column: "EducationalMaterialId",
                principalTable: "EducationalMaterials",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
