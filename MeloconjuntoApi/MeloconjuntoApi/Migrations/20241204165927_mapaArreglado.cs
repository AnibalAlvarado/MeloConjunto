using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MeloconjuntoApi.Migrations
{
    /// <inheritdoc />
    public partial class mapaArreglado : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MapaImagen",
                table: "Mapas");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MapaImagen",
                table: "Mapas",
                type: "character varying(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: "");
        }
    }
}
