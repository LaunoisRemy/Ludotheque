using Microsoft.EntityFrameworkCore.Migrations;

namespace Ludotheque.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Jeu",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nom = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    NbMinJoueur = table.Column<int>(nullable: false),
                    NbMaxJoueur = table.Column<int>(nullable: false),
                    AgeMinimum = table.Column<int>(nullable: false),
                    TempsJeu = table.Column<string>(nullable: true),
                    Prix = table.Column<decimal>(nullable: false),
                    LienAchat = table.Column<string>(nullable: true),
                    LienVideo = table.Column<string>(nullable: true),
                    LienImg = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Jeu", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Jeu");
        }
    }
}
