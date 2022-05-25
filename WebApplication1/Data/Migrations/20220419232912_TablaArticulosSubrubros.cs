using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication1.Data.Migrations
{
    public partial class TablaArticulosSubrubros : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Articulo_SubRubro_SubRubroID",
                table: "Articulo");

            migrationBuilder.DropForeignKey(
                name: "FK_SubRubro_Rubro_RubroID",
                table: "SubRubro");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SubRubro",
                table: "SubRubro");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Rubro",
                table: "Rubro");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Articulo",
                table: "Articulo");

            migrationBuilder.RenameTable(
                name: "SubRubro",
                newName: "SubRubros");

            migrationBuilder.RenameTable(
                name: "Rubro",
                newName: "Rubros");

            migrationBuilder.RenameTable(
                name: "Articulo",
                newName: "Articulos");

            migrationBuilder.RenameIndex(
                name: "IX_SubRubro_RubroID",
                table: "SubRubros",
                newName: "IX_SubRubros_RubroID");

            migrationBuilder.RenameIndex(
                name: "IX_Articulo_SubRubroID",
                table: "Articulos",
                newName: "IX_Articulos_SubRubroID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SubRubros",
                table: "SubRubros",
                column: "SubRubroID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Rubros",
                table: "Rubros",
                column: "RubroID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Articulos",
                table: "Articulos",
                column: "ArticuloID");

            migrationBuilder.AddForeignKey(
                name: "FK_Articulos_SubRubros_SubRubroID",
                table: "Articulos",
                column: "SubRubroID",
                principalTable: "SubRubros",
                principalColumn: "SubRubroID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SubRubros_Rubros_RubroID",
                table: "SubRubros",
                column: "RubroID",
                principalTable: "Rubros",
                principalColumn: "RubroID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Articulos_SubRubros_SubRubroID",
                table: "Articulos");

            migrationBuilder.DropForeignKey(
                name: "FK_SubRubros_Rubros_RubroID",
                table: "SubRubros");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SubRubros",
                table: "SubRubros");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Rubros",
                table: "Rubros");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Articulos",
                table: "Articulos");

            migrationBuilder.RenameTable(
                name: "SubRubros",
                newName: "SubRubro");

            migrationBuilder.RenameTable(
                name: "Rubros",
                newName: "Rubro");

            migrationBuilder.RenameTable(
                name: "Articulos",
                newName: "Articulo");

            migrationBuilder.RenameIndex(
                name: "IX_SubRubros_RubroID",
                table: "SubRubro",
                newName: "IX_SubRubro_RubroID");

            migrationBuilder.RenameIndex(
                name: "IX_Articulos_SubRubroID",
                table: "Articulo",
                newName: "IX_Articulo_SubRubroID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SubRubro",
                table: "SubRubro",
                column: "SubRubroID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Rubro",
                table: "Rubro",
                column: "RubroID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Articulo",
                table: "Articulo",
                column: "ArticuloID");

            migrationBuilder.AddForeignKey(
                name: "FK_Articulo_SubRubro_SubRubroID",
                table: "Articulo",
                column: "SubRubroID",
                principalTable: "SubRubro",
                principalColumn: "SubRubroID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SubRubro_Rubro_RubroID",
                table: "SubRubro",
                column: "RubroID",
                principalTable: "Rubro",
                principalColumn: "RubroID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
