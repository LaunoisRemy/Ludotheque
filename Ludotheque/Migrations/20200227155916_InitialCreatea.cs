using Microsoft.EntityFrameworkCore.Migrations;

namespace Ludotheque.Migrations
{
    public partial class InitialCreatea : Migration
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
                name: "GameCategories",
                columns: table => new
                {
                    GameId = table.Column<int>(nullable: false),
                    CategoryId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameCategories", x => new { x.CategoryId, x.GameId });
                });

            migrationBuilder.CreateTable(
                name: "Gategories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(nullable: true),
                    Type = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gategories", x => x.Id);
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
                name: "Jeu",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nom = table.Column<string>(maxLength: 60, nullable: false),
                    Description = table.Column<string>(nullable: true),
                    NbMinJoueur = table.Column<int>(nullable: false),
                    NbMaxJoueur = table.Column<int>(nullable: false),
                    AgeMinimum = table.Column<int>(nullable: false),
                    TempsJeu = table.Column<string>(nullable: true),
                    Prix = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    LienAchat = table.Column<string>(nullable: true),
                    LienVideo = table.Column<string>(nullable: true),
                    LienImg = table.Column<string>(nullable: true),
                    Categorie = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Jeu", x => x.Id);
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GameCategories");

            migrationBuilder.DropTable(
                name: "Games");

            migrationBuilder.DropTable(
                name: "Gategories");

            migrationBuilder.DropTable(
                name: "Jeu");

            migrationBuilder.DropTable(
                name: "Difficulties");

            migrationBuilder.DropTable(
                name: "Editors");

            migrationBuilder.DropTable(
                name: "Illustrators");
        }
    }
}
