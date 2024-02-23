using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace apiMusicInfo.Migrations
{
    /// <inheritdoc />
    public partial class init1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Albums",
                columns: table => new
                {
                    Titol = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FrontCover = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BackCover = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Albums", x => new { x.Titol, x.Year });
                });

            migrationBuilder.CreateTable(
                name: "Bands",
                columns: table => new
                {
                    Name = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    FoundationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Origin = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    Genre = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bands", x => new { x.Name, x.FoundationDate });
                });

            migrationBuilder.CreateTable(
                name: "Extensions",
                columns: table => new
                {
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Extensions", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "Instruments",
                columns: table => new
                {
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Instruments", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "Musician",
                columns: table => new
                {
                    Name = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Age = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Musician", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "Playlists",
                columns: table => new
                {
                    Dispositiu = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PlaylistName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Playlists", x => new { x.Dispositiu, x.PlaylistName });
                });

            migrationBuilder.CreateTable(
                name: "Songs",
                columns: table => new
                {
                    UID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Language = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Duration = table.Column<int>(type: "int", nullable: true),
                    VersionOriginalId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    OriginalSongUID = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Songs", x => x.UID);
                    table.ForeignKey(
                        name: "FK_Songs_Songs_OriginalSongUID",
                        column: x => x.OriginalSongUID,
                        principalTable: "Songs",
                        principalColumn: "UID");
                });

            migrationBuilder.CreateTable(
                name: "BandMusician",
                columns: table => new
                {
                    MusiciansName = table.Column<string>(type: "nvarchar(15)", nullable: false),
                    BandsName = table.Column<string>(type: "nvarchar(15)", nullable: false),
                    BandsFoundationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BandMusician", x => new { x.MusiciansName, x.BandsName, x.BandsFoundationDate });
                    table.ForeignKey(
                        name: "FK_BandMusician_Bands_BandsName_BandsFoundationDate",
                        columns: x => new { x.BandsName, x.BandsFoundationDate },
                        principalTable: "Bands",
                        principalColumns: new[] { "Name", "FoundationDate" },
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BandMusician_Musician_MusiciansName",
                        column: x => x.MusiciansName,
                        principalTable: "Musician",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateTable(
                name: "ExtensionSong",
                columns: table => new
                {
                    ExtensionsName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SongsUID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExtensionSong", x => new { x.ExtensionsName, x.SongsUID });
                    table.ForeignKey(
                        name: "FK_ExtensionSong_Extensions_ExtensionsName",
                        column: x => x.ExtensionsName,
                        principalTable: "Extensions",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExtensionSong_Songs_SongsUID",
                        column: x => x.SongsUID,
                        principalTable: "Songs",
                        principalColumn: "UID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PlaylistSong",
                columns: table => new
                {
                    SongsUID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PlaylistsDispositiu = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PlaylistsPlaylistName = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlaylistSong", x => new { x.SongsUID, x.PlaylistsDispositiu, x.PlaylistsPlaylistName });
                    table.ForeignKey(
                        name: "FK_PlaylistSong_Playlists_PlaylistsDispositiu_PlaylistsPlaylistName",
                        columns: x => new { x.PlaylistsDispositiu, x.PlaylistsPlaylistName },
                        principalTable: "Playlists",
                        principalColumns: new[] { "Dispositiu", "PlaylistName" },
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlaylistSong_Songs_SongsUID",
                        column: x => x.SongsUID,
                        principalTable: "Songs",
                        principalColumn: "UID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Plays",
                columns: table => new
                {
                    BandName = table.Column<string>(type: "nvarchar(15)", nullable: false),
                    BandDateFoundation = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MusicianName = table.Column<string>(type: "nvarchar(15)", nullable: false),
                    InstrumentName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SongUID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Plays", x => new { x.SongUID, x.MusicianName, x.InstrumentName, x.BandName, x.BandDateFoundation });
                    table.ForeignKey(
                        name: "FK_Plays_Bands_BandName_BandDateFoundation",
                        columns: x => new { x.BandName, x.BandDateFoundation },
                        principalTable: "Bands",
                        principalColumns: new[] { "Name", "FoundationDate" },
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Plays_Instruments_InstrumentName",
                        column: x => x.InstrumentName,
                        principalTable: "Instruments",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Plays_Musician_MusicianName",
                        column: x => x.MusicianName,
                        principalTable: "Musician",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Plays_Songs_SongUID",
                        column: x => x.SongUID,
                        principalTable: "Songs",
                        principalColumn: "UID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AlbumSong_AlbumsTitol_AlbumsYear",
                table: "AlbumSong",
                columns: new[] { "AlbumsTitol", "AlbumsYear" });

            migrationBuilder.CreateIndex(
                name: "IX_BandMusician_BandsName_BandsFoundationDate",
                table: "BandMusician",
                columns: new[] { "BandsName", "BandsFoundationDate" });

            migrationBuilder.CreateIndex(
                name: "IX_Extensions_Name",
                table: "Extensions",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_ExtensionSong_SongsUID",
                table: "ExtensionSong",
                column: "SongsUID");

            migrationBuilder.CreateIndex(
                name: "IX_Musician_Name",
                table: "Musician",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Playlists_Dispositiu",
                table: "Playlists",
                column: "Dispositiu");

            migrationBuilder.CreateIndex(
                name: "IX_PlaylistSong_PlaylistsDispositiu_PlaylistsPlaylistName",
                table: "PlaylistSong",
                columns: new[] { "PlaylistsDispositiu", "PlaylistsPlaylistName" });

            migrationBuilder.CreateIndex(
                name: "IX_Plays_BandName_BandDateFoundation",
                table: "Plays",
                columns: new[] { "BandName", "BandDateFoundation" });

            migrationBuilder.CreateIndex(
                name: "IX_Plays_InstrumentName",
                table: "Plays",
                column: "InstrumentName");

            migrationBuilder.CreateIndex(
                name: "IX_Plays_MusicianName",
                table: "Plays",
                column: "MusicianName");

            migrationBuilder.CreateIndex(
                name: "IX_Songs_OriginalSongUID",
                table: "Songs",
                column: "OriginalSongUID");

            migrationBuilder.CreateIndex(
                name: "IX_Songs_UID",
                table: "Songs",
                column: "UID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AlbumSong");

            migrationBuilder.DropTable(
                name: "BandMusician");

            migrationBuilder.DropTable(
                name: "ExtensionSong");

            migrationBuilder.DropTable(
                name: "PlaylistSong");

            migrationBuilder.DropTable(
                name: "Plays");

            migrationBuilder.DropTable(
                name: "Albums");

            migrationBuilder.DropTable(
                name: "Extensions");

            migrationBuilder.DropTable(
                name: "Playlists");

            migrationBuilder.DropTable(
                name: "Bands");

            migrationBuilder.DropTable(
                name: "Instruments");

            migrationBuilder.DropTable(
                name: "Musician");

            migrationBuilder.DropTable(
                name: "Songs");
        }
    }
}
