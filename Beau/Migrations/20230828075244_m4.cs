using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Beau.Migrations
{
    /// <inheritdoc />
    public partial class m4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Credentials_Users_UserId",
                table: "Credentials");

            migrationBuilder.DropIndex(
                name: "IX_Credentials_UserId",
                table: "Credentials");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Credentials");

            migrationBuilder.CreateIndex(
                name: "IX_Users_IdCred",
                table: "Users",
                column: "IdCred",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Credentials_IdCred",
                table: "Users",
                column: "IdCred",
                principalTable: "Credentials",
                principalColumn: "IdCred",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Credentials_IdCred",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_IdCred",
                table: "Users");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Credentials",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Credentials_UserId",
                table: "Credentials",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Credentials_Users_UserId",
                table: "Credentials",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
