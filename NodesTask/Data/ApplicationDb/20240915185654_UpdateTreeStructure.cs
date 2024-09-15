using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NodesTask.Data.ApplicationDb
{
    /// <inheritdoc />
    public partial class UpdateTreeStructure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Nodes_Nodes_ParentId",
                table: "Nodes");

            migrationBuilder.RenameColumn(
                name: "ParentId",
                table: "Nodes",
                newName: "ParentNodeId");

            migrationBuilder.RenameIndex(
                name: "IX_Nodes_ParentId",
                table: "Nodes",
                newName: "IX_Nodes_ParentNodeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Nodes_Nodes_ParentNodeId",
                table: "Nodes",
                column: "ParentNodeId",
                principalTable: "Nodes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Nodes_Nodes_ParentNodeId",
                table: "Nodes");

            migrationBuilder.RenameColumn(
                name: "ParentNodeId",
                table: "Nodes",
                newName: "ParentId");

            migrationBuilder.RenameIndex(
                name: "IX_Nodes_ParentNodeId",
                table: "Nodes",
                newName: "IX_Nodes_ParentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Nodes_Nodes_ParentId",
                table: "Nodes",
                column: "ParentId",
                principalTable: "Nodes",
                principalColumn: "Id");
        }
    }
}
