using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace apiMusicInfo.Migrations
{
    /// <inheritdoc />
    public partial class init2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BandMusician_Band_BandsName",
                table: "BandMusician");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BandMusician",
                table: "BandMusician");

            migrationBuilder.DropIndex(
                name: "IX_BandMusician_MusiciansName",
                table: "BandMusician");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Band",
                table: "Band");

            migrationBuilder.AddColumn<DateTime>(
                name: "BandsFoundationDate",
                table: "BandMusician",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "FoundationDate",
                table: "Band",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddPrimaryKey(
                name: "PK_BandMusician",
                table: "BandMusician",
                columns: new[] { "MusiciansName", "BandsName", "BandsFoundationDate" });

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Band_Name",
                table: "Band",
                column: "Name");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Band",
                table: "Band",
                columns: new[] { "Name", "FoundationDate" });

            migrationBuilder.CreateIndex(
                name: "IX_BandMusician_BandsName_BandsFoundationDate",
                table: "BandMusician",
                columns: new[] { "BandsName", "BandsFoundationDate" });

            migrationBuilder.AddForeignKey(
                name: "FK_BandMusician_Band_BandsName_BandsFoundationDate",
                table: "BandMusician",
                columns: new[] { "BandsName", "BandsFoundationDate" },
                principalTable: "Band",
                principalColumns: new[] { "Name", "FoundationDate" },
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BandMusician_Band_BandsName_BandsFoundationDate",
                table: "BandMusician");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BandMusician",
                table: "BandMusician");

            migrationBuilder.DropIndex(
                name: "IX_BandMusician_BandsName_BandsFoundationDate",
                table: "BandMusician");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Band_Name",
                table: "Band");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Band",
                table: "Band");

            migrationBuilder.DropColumn(
                name: "BandsFoundationDate",
                table: "BandMusician");

            migrationBuilder.DropColumn(
                name: "FoundationDate",
                table: "Band");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BandMusician",
                table: "BandMusician",
                columns: new[] { "BandsName", "MusiciansName" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Band",
                table: "Band",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_BandMusician_MusiciansName",
                table: "BandMusician",
                column: "MusiciansName");

            migrationBuilder.AddForeignKey(
                name: "FK_BandMusician_Band_BandsName",
                table: "BandMusician",
                column: "BandsName",
                principalTable: "Band",
                principalColumn: "Name",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
