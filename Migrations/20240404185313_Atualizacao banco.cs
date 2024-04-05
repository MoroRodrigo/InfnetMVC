using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfnetMVC.Migrations
{
    /// <inheritdoc />
    public partial class Atualizacaobanco : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DepartamentoId",
                table: "Funcionarios",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Funcionarios_DepartamentoId",
                table: "Funcionarios",
                column: "DepartamentoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Funcionarios_Departamentos_DepartamentoId",
                table: "Funcionarios",
                column: "DepartamentoId",
                principalTable: "Departamentos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Funcionarios_Departamentos_DepartamentoId",
                table: "Funcionarios");

            migrationBuilder.DropIndex(
                name: "IX_Funcionarios_DepartamentoId",
                table: "Funcionarios");

            migrationBuilder.DropColumn(
                name: "DepartamentoId",
                table: "Funcionarios");
        }
    }
}
