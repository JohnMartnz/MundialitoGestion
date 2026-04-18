using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mundialito.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddStatusCodeToIdempotencyRequest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Result",
                table: "IdempotencyRequests",
                newName: "Response");

            migrationBuilder.AddColumn<int>(
                name: "StatusCode",
                table: "IdempotencyRequests",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StatusCode",
                table: "IdempotencyRequests");

            migrationBuilder.RenameColumn(
                name: "Response",
                table: "IdempotencyRequests",
                newName: "Result");
        }
    }
}
