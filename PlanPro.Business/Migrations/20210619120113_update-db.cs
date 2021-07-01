using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PlanPro.Business.Migrations
{
    public partial class updatedb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Projets_ProjetID",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_ProjetID",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ProjetID",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "CreatorId",
                table: "Taches",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "Taches",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "Taches",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "ChefId",
                table: "Equipes",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IdPaticipProject",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PaticipProjectID",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Taches_CreatorId",
                table: "Taches",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Equipes_ChefId",
                table: "Equipes",
                column: "ChefId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_PaticipProjectID",
                table: "AspNetUsers",
                column: "PaticipProjectID");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Projets_PaticipProjectID",
                table: "AspNetUsers",
                column: "PaticipProjectID",
                principalTable: "Projets",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Equipes_AspNetUsers_ChefId",
                table: "Equipes",
                column: "ChefId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Taches_AspNetUsers_CreatorId",
                table: "Taches",
                column: "CreatorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Projets_PaticipProjectID",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Equipes_AspNetUsers_ChefId",
                table: "Equipes");

            migrationBuilder.DropForeignKey(
                name: "FK_Taches_AspNetUsers_CreatorId",
                table: "Taches");

            migrationBuilder.DropIndex(
                name: "IX_Taches_CreatorId",
                table: "Taches");

            migrationBuilder.DropIndex(
                name: "IX_Equipes_ChefId",
                table: "Equipes");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_PaticipProjectID",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "Taches");

            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "Taches");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "Taches");

            migrationBuilder.DropColumn(
                name: "ChefId",
                table: "Equipes");

            migrationBuilder.DropColumn(
                name: "IdPaticipProject",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "PaticipProjectID",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<int>(
                name: "ProjetID",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_ProjetID",
                table: "AspNetUsers",
                column: "ProjetID");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Projets_ProjetID",
                table: "AspNetUsers",
                column: "ProjetID",
                principalTable: "Projets",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
