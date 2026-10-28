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

            migrationBuilder.Sql("ALTER TABLE \"delivery\" ALTER COLUMN \"status\" TYPE integer USING status::integer;");
            
            migrationBuilder.Sql("ALTER TABLE \"courier\" ALTER COLUMN \"vehicle_type\" TYPE integer USING vehicle_type::integer;");

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

            migrationBuilder.Sql("ALTER TABLE \"delivery\" ALTER COLUMN \"status\" TYPE text;");

            migrationBuilder.Sql("ALTER TABLE \"courier\" ALTER COLUMN \"vehicle_type\" TYPE text;");

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
