using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace apiMusicInfo.Migrations
{
    /// <inheritdoc />
    public partial class ModificacioAlbum : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Albums_Songs_SongUID",
                table: "Albums");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Albums",
                table: "Albums");

            migrationBuilder.DropIndex(
                name: "IX_Albums_SongUID",
                table: "Albums");

            migrationBuilder.DropColumn(
                name: "SongUID",
                table: "Albums");

            migrationBuilder.RenameColumn(
                name: "AlbumName",
                table: "Albums",
                newName: "Titol");

            migrationBuilder.AddColumn<string>(
                name: "BackCover",
                table: "Albums",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FrontCover",
                table: "Albums",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Gender",
                table: "Albums",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Albums",
                table: "Albums",
                columns: new[] { "Titol", "Year" });

            migrationBuilder.CreateTable(
                name: "AlbumSong",
                columns: table => new
                {
                    SongsUID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AlbumsTitol = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AlbumsYear = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlbumSong", x => new { x.SongsUID, x.AlbumsTitol, x.AlbumsYear });
                    table.ForeignKey(
                        name: "FK_AlbumSong_Albums_AlbumsTitol_AlbumsYear",
                        columns: x => new { x.AlbumsTitol, x.AlbumsYear },
                        principalTable: "Albums",
                        principalColumns: new[] { "Titol", "Year" },
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AlbumSong_Songs_SongsUID",
                        column: x => x.SongsUID,
                        principalTable: "Songs",
                        principalColumn: "UID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AlbumSong_AlbumsTitol_AlbumsYear",
                table: "AlbumSong",
                columns: new[] { "AlbumsTitol", "AlbumsYear" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AlbumSong");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Albums",
                table: "Albums");

            migrationBuilder.DropColumn(
                name: "BackCover",
                table: "Albums");

            migrationBuilder.DropColumn(
                name: "FrontCover",
                table: "Albums");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "Albums");

            migrationBuilder.RenameColumn(
                name: "Titol",
                table: "Albums",
                newName: "AlbumName");

            migrationBuilder.AddColumn<Guid>(
                name: "SongUID",
                table: "Albums",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Albums",
                table: "Albums",
                columns: new[] { "AlbumName", "Year", "SongUID" });

            migrationBuilder.CreateIndex(
                name: "IX_Albums_SongUID",
                table: "Albums",
                column: "SongUID");

            migrationBuilder.AddForeignKey(
                name: "FK_Albums_Songs_SongUID",
                table: "Albums",
                column: "SongUID",
                principalTable: "Songs",
                principalColumn: "UID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
