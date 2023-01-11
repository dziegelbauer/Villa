using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VillaAPI.Migrations
{
    /// <inheritdoc />
    public partial class updateForNullability : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 1, 11, 10, 12, 44, 412, DateTimeKind.Local).AddTicks(7351), new DateTime(2023, 1, 11, 10, 12, 44, 412, DateTimeKind.Local).AddTicks(7398) });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 1, 11, 10, 12, 44, 412, DateTimeKind.Local).AddTicks(7404), new DateTime(2023, 1, 11, 10, 12, 44, 412, DateTimeKind.Local).AddTicks(7405) });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 1, 11, 10, 12, 44, 412, DateTimeKind.Local).AddTicks(7409), new DateTime(2023, 1, 11, 10, 12, 44, 412, DateTimeKind.Local).AddTicks(7410) });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 1, 11, 10, 12, 44, 412, DateTimeKind.Local).AddTicks(7412), new DateTime(2023, 1, 11, 10, 12, 44, 412, DateTimeKind.Local).AddTicks(7414) });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 1, 11, 10, 12, 44, 412, DateTimeKind.Local).AddTicks(7416), new DateTime(2023, 1, 11, 10, 12, 44, 412, DateTimeKind.Local).AddTicks(7418) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 1, 4, 13, 22, 14, 633, DateTimeKind.Local).AddTicks(3851), new DateTime(2023, 1, 4, 13, 22, 14, 633, DateTimeKind.Local).AddTicks(3901) });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 1, 4, 13, 22, 14, 633, DateTimeKind.Local).AddTicks(3906), new DateTime(2023, 1, 4, 13, 22, 14, 633, DateTimeKind.Local).AddTicks(3907) });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 1, 4, 13, 22, 14, 633, DateTimeKind.Local).AddTicks(3910), new DateTime(2023, 1, 4, 13, 22, 14, 633, DateTimeKind.Local).AddTicks(3911) });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 1, 4, 13, 22, 14, 633, DateTimeKind.Local).AddTicks(3913), new DateTime(2023, 1, 4, 13, 22, 14, 633, DateTimeKind.Local).AddTicks(3914) });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 1, 4, 13, 22, 14, 633, DateTimeKind.Local).AddTicks(3916), new DateTime(2023, 1, 4, 13, 22, 14, 633, DateTimeKind.Local).AddTicks(3917) });
        }
    }
}
