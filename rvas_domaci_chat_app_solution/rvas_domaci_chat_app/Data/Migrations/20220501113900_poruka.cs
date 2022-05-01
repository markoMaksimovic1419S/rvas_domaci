using Microsoft.EntityFrameworkCore.Migrations;

namespace rvas_domaci_chat_app.Data.Migrations
{
    public partial class poruka : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Poruka",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    poruku_poslao = table.Column<string>(nullable: true),
                    text_poruke = table.Column<string>(nullable: true),
                    id_sobe = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Poruka", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Poruka");
        }
    }
}
