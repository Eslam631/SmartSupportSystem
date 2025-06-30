using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddAdmin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "IsActive", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "RoleId", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "213566ac-e6de-4571-a2e2-1ec9b08f8aba", 0, "8fe81545-36c3-493e-9010-70f3f8ecfd7e", "EslamTarek55@gmail.com", false, "ESlam", true, "Tarek", false, null, null, null, "AQAAAAIAAYagAAAAEIUIGGcsb8+vIrQ8bDUr22MhKgyLT0+3lmldjuJZa3FzEby0b0tYHQXK9ZrgYXd2Kg==", null, false, "b8c10d5f-8671-469c-9dc8-7a31b415e607", "c80c46f7-9b00-4233-89e0-f24b31df48be", false, null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "213566ac-e6de-4571-a2e2-1ec9b08f8aba");
        }
    }
}
