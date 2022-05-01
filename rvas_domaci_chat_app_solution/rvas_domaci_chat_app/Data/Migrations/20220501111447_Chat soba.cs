using Microsoft.EntityFrameworkCore.Migrations;

namespace rvas_domaci_chat_app.Data.Migrations
{
    public partial class Chatsoba : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ChatSoba",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    naziv_sobe = table.Column<string>(nullable: true),
                    mail_kretora = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatSoba", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChatSoba");
        }
    }
}
