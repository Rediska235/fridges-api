using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fridges.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class StoredProcedure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var storedProcedure = @"CREATE PROCEDURE SearchProductsWithZeroQuantity
                                    AS
                                    BEGIN
	                                    select * from FridgeProducts where Quantity = 0
                                    END";

            migrationBuilder.Sql(storedProcedure);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            var storedProcedure = @"DROP PROCEDURE SearchProductsWithZeroQuantity";

            migrationBuilder.Sql(storedProcedure);
        }
    }
}
