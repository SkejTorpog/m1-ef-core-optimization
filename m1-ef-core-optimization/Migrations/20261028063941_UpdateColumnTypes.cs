using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace m1_ef_core_optimization.Migrations
{
    /// <inheritdoc />
    public partial class UpdateColumnTypes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_delivery_courier_courier_id",
                table: "delivery");

            migrationBuilder.DropIndex(
                name: "ix_delivery_courier_id",
                table: "delivery");

            migrationBuilder.AlterColumn<int>(
                name: "status",
                table: "delivery",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<int>(
                name: "vehicle_type",
                table: "courier",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.CreateIndex(
                name: "ix_delivery_courier_id_status",
                table: "delivery",
                columns: new[] { "courier_id", "status" });

            migrationBuilder.AddForeignKey(
                name: "fk_delivery_courier_courier_id",
                table: "delivery",
                column: "courier_id",
                principalTable: "courier",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_delivery_courier_courier_id",
                table: "delivery");

            migrationBuilder.DropIndex(
                name: "ix_delivery_courier_id_status",
                table: "delivery");

            migrationBuilder.AlterColumn<string>(
                name: "status",
                table: "delivery",
                type: "text",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<string>(
                name: "vehicle_type",
                table: "courier",
                type: "text",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.CreateIndex(
                name: "ix_delivery_courier_id",
                table: "delivery",
                column: "courier_id");

            migrationBuilder.AddForeignKey(
                name: "fk_delivery_courier_courier_id",
                table: "delivery",
                column: "courier_id",
                principalTable: "courier",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
