using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Programowanie_aplikacji_internetowych.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedMidifiedByandModifiedDateentitiesinMetaDataentity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Test",
                table: "Comment",
                newName: "Text");

            migrationBuilder.AddColumn<Guid>(
                name: "ModifiedById",
                table: "MetaData",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                table: "MetaData",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MetaData_ModifiedById",
                table: "MetaData",
                column: "ModifiedById");

            migrationBuilder.AddForeignKey(
                name: "FK_MetaData_Users_ModifiedById",
                table: "MetaData",
                column: "ModifiedById",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MetaData_Users_ModifiedById",
                table: "MetaData");

            migrationBuilder.DropIndex(
                name: "IX_MetaData_ModifiedById",
                table: "MetaData");

            migrationBuilder.DropColumn(
                name: "ModifiedById",
                table: "MetaData");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                table: "MetaData");

            migrationBuilder.RenameColumn(
                name: "Text",
                table: "Comment",
                newName: "Test");
        }
    }
}
