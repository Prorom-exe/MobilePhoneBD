using Microsoft.EntityFrameworkCore.Migrations;

namespace MobilePhoneBD.Migrations
{
    public partial class AddLink1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Мanufacturers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Мanufacturers_CategoryId",
                table: "Мanufacturers",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Мanufacturers_Category_CategoryId",
                table: "Мanufacturers",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Мanufacturers_Category_CategoryId",
                table: "Мanufacturers");

            migrationBuilder.DropIndex(
                name: "IX_Мanufacturers_CategoryId",
                table: "Мanufacturers");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Мanufacturers");
        }
    }
}
