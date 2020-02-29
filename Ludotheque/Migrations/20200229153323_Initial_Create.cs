using Microsoft.EntityFrameworkCore.Migrations;

namespace Ludotheque.Migrations
{
    public partial class Initial_Create : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Difficulties",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    label = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Difficulties", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Editors",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Editors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Illustrators",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Illustrators", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MaterialSupports",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaterialSupports", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Mechanisms",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mechanisms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Games",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 60, nullable: false),
                    Description = table.Column<string>(nullable: true),
                    MinPlayer = table.Column<int>(nullable: false),
                    MaxPlayer = table.Column<int>(nullable: false),
                    MinimumAge = table.Column<int>(nullable: false),
                    GameTime = table.Column<string>(nullable: true),
                    Price = table.Column<decimal>(nullable: false),
                    ReleaseDate = table.Column<int>(nullable: false),
                    BuyLink = table.Column<string>(nullable: true),
                    VideoLink = table.Column<string>(nullable: true),
                    PictureLink = table.Column<string>(nullable: true),
                    Validate = table.Column<bool>(nullable: false),
                    DifficultyId = table.Column<int>(nullable: true),
                    IllustratorId = table.Column<int>(nullable: true),
                    EditorId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Games", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Games_Difficulties_DifficultyId",
                        column: x => x.DifficultyId,
                        principalTable: "Difficulties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Games_Editors_EditorId",
                        column: x => x.EditorId,
                        principalTable: "Editors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Games_Illustrators_IllustratorId",
                        column: x => x.IllustratorId,
                        principalTable: "Illustrators",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MaterialSupportsGames",
                columns: table => new
                {
                    GameId = table.Column<int>(nullable: false),
                    MaterialSupportId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaterialSupportsGames", x => new { x.GameId, x.MaterialSupportId });
                    table.ForeignKey(
                        name: "FK_MaterialSupportsGames_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MaterialSupportsGames_MaterialSupports_MaterialSupportId",
                        column: x => x.MaterialSupportId,
                        principalTable: "MaterialSupports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MechanismsGames",
                columns: table => new
                {
                    GameId = table.Column<int>(nullable: false),
                    MechanismId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MechanismsGames", x => new { x.GameId, x.MechanismId });
                    table.ForeignKey(
                        name: "FK_MechanismsGames_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MechanismsGames_Mechanisms_MechanismId",
                        column: x => x.MechanismId,
                        principalTable: "Mechanisms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Themes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    GameId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Themes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Themes_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ThemesGames",
                columns: table => new
                {
                    GameId = table.Column<int>(nullable: false),
                    ThemeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThemesGames", x => new { x.GameId, x.ThemeId });
                    table.ForeignKey(
                        name: "FK_ThemesGames_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ThemesGames_Themes_ThemeId",
                        column: x => x.ThemeId,
                        principalTable: "Themes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Games_DifficultyId",
                table: "Games",
                column: "DifficultyId");

            migrationBuilder.CreateIndex(
                name: "IX_Games_EditorId",
                table: "Games",
                column: "EditorId");

            migrationBuilder.CreateIndex(
                name: "IX_Games_IllustratorId",
                table: "Games",
                column: "IllustratorId");

            migrationBuilder.CreateIndex(
                name: "IX_MaterialSupportsGames_MaterialSupportId",
                table: "MaterialSupportsGames",
                column: "MaterialSupportId");

            migrationBuilder.CreateIndex(
                name: "IX_MechanismsGames_MechanismId",
                table: "MechanismsGames",
                column: "MechanismId");

            migrationBuilder.CreateIndex(
                name: "IX_Themes_GameId",
                table: "Themes",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_ThemesGames_ThemeId",
                table: "ThemesGames",
                column: "ThemeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MaterialSupportsGames");

            migrationBuilder.DropTable(
                name: "MechanismsGames");

            migrationBuilder.DropTable(
                name: "ThemesGames");

            migrationBuilder.DropTable(
                name: "MaterialSupports");

            migrationBuilder.DropTable(
                name: "Mechanisms");

            migrationBuilder.DropTable(
                name: "Themes");

            migrationBuilder.DropTable(
                name: "Games");

            migrationBuilder.DropTable(
                name: "Difficulties");

            migrationBuilder.DropTable(
                name: "Editors");

            migrationBuilder.DropTable(
                name: "Illustrators");
        }
    }
}
