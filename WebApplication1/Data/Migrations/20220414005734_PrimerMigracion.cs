using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication1.Data.Migrations
{
    public partial class PrimerMigracion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Rubro",
                columns: table => new
                {
                    RubroID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rubro", x => x.RubroID);
                });

            migrationBuilder.CreateTable(
                name: "SubRubro",
                columns: table => new
                {
                    SubRubroID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(nullable: true),
                    RubroID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubRubro", x => x.SubRubroID);
                    table.ForeignKey(
                        name: "FK_SubRubro_Rubro_RubroID",
                        column: x => x.RubroID,
                        principalTable: "Rubro",
                        principalColumn: "RubroID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Articulo",
                columns: table => new
                {
                    ArticuloID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(nullable: true),
                    UltAct = table.Column<DateTime>(nullable: false),
                    PrecioCosto = table.Column<decimal>(nullable: false),
                    PorcentajeGanancia = table.Column<decimal>(nullable: false),
                    PrecioVenta = table.Column<decimal>(nullable: false),
                    SubRubroID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Articulo", x => x.ArticuloID);
                    table.ForeignKey(
                        name: "FK_Articulo_SubRubro_SubRubroID",
                        column: x => x.SubRubroID,
                        principalTable: "SubRubro",
                        principalColumn: "SubRubroID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Articulo_SubRubroID",
                table: "Articulo",
                column: "SubRubroID");

            migrationBuilder.CreateIndex(
                name: "IX_SubRubro_RubroID",
                table: "SubRubro",
                column: "RubroID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Articulo");

            migrationBuilder.DropTable(
                name: "SubRubro");

            migrationBuilder.DropTable(
                name: "Rubro");
        }
    }
}
