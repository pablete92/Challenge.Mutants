using Microsoft.EntityFrameworkCore.Migrations;

namespace Challenge.Mutants.Domain.Migrations
{
    public partial class changeValueAdnProperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Adn",
                table: "ADN",
                unicode: false,
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(41)",
                oldUnicode: false,
                oldMaxLength: 41);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Adn",
                table: "ADN",
                type: "varchar(41)",
                unicode: false,
                maxLength: 41,
                nullable: false,
                oldClrType: typeof(string),
                oldUnicode: false,
                oldMaxLength: 100);
        }
    }
}
