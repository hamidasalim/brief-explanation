using Microsoft.EntityFrameworkCore.Migrations;

namespace PlanPro.Business.Migrations
{
    public partial class updateDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ChefProjetID",
                table: "Projets",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "IDChef",
                table: "Equipes",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "ProjetID",
                table: "ApplicationUser",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Projets_ChefProjetID",
                table: "Projets",
                column: "ChefProjetID");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUser_ProjetID",
                table: "ApplicationUser",
                column: "ProjetID");

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationUser_Projets_ProjetID",
                table: "ApplicationUser",
                column: "ProjetID",
                principalTable: "Projets",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Projets_ApplicationUser_ChefProjetID",
                table: "Projets",
                column: "ChefProjetID",
                principalTable: "ApplicationUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationUser_Projets_ProjetID",
                table: "ApplicationUser");

            migrationBuilder.DropForeignKey(
                name: "FK_Projets_ApplicationUser_ChefProjetID",
                table: "Projets");

            migrationBuilder.DropIndex(
                name: "IX_Projets_ChefProjetID",
                table: "Projets");

            migrationBuilder.DropIndex(
                name: "IX_ApplicationUser_ProjetID",
                table: "ApplicationUser");

            migrationBuilder.DropColumn(
                name: "ChefProjetID",
                table: "Projets");

            migrationBuilder.DropColumn(
                name: "ProjetID",
                table: "ApplicationUser");

            migrationBuilder.AlterColumn<int>(
                name: "IDChef",
                table: "Equipes",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
