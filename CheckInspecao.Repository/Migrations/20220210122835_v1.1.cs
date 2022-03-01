using Microsoft.EntityFrameworkCore.Migrations;

namespace CheckInspecao.Repository.Migrations
{
    public partial class v11 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItensInspecao_Grupos_GrupoId",
                table: "ItensInspecao");

            migrationBuilder.AlterColumn<int>(
                name: "GrupoId",
                table: "ItensInspecao",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ItensInspecao_Grupos_GrupoId",
                table: "ItensInspecao",
                column: "GrupoId",
                principalTable: "Grupos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItensInspecao_Grupos_GrupoId",
                table: "ItensInspecao");

            migrationBuilder.AlterColumn<int>(
                name: "GrupoId",
                table: "ItensInspecao",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddForeignKey(
                name: "FK_ItensInspecao_Grupos_GrupoId",
                table: "ItensInspecao",
                column: "GrupoId",
                principalTable: "Grupos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
