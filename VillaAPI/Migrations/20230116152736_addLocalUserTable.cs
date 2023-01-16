using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VillaAPI.Migrations
{
    /// <inheritdoc />
    public partial class addLocalUserTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LocalUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NAme = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocalUsers", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "ImageUrl", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 1, 16, 8, 27, 36, 133, DateTimeKind.Local).AddTicks(76), "https://dotnetmastery.com/bluevillaimages/villa3.jpg", new DateTime(2023, 1, 16, 8, 27, 36, 133, DateTimeKind.Local).AddTicks(127) });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "ImageUrl", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 1, 16, 8, 27, 36, 133, DateTimeKind.Local).AddTicks(136), "https://dotnetmastery.com/bluevillaimages/villa1.jpg", new DateTime(2023, 1, 16, 8, 27, 36, 133, DateTimeKind.Local).AddTicks(137) });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "ImageUrl", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 1, 16, 8, 27, 36, 133, DateTimeKind.Local).AddTicks(139), "https://dotnetmastery.com/bluevillaimages/villa4.jpg", new DateTime(2023, 1, 16, 8, 27, 36, 133, DateTimeKind.Local).AddTicks(140) });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedDate", "ImageUrl", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 1, 16, 8, 27, 36, 133, DateTimeKind.Local).AddTicks(143), "https://dotnetmastery.com/bluevillaimages/villa5.jpg", new DateTime(2023, 1, 16, 8, 27, 36, 133, DateTimeKind.Local).AddTicks(144) });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedDate", "ImageUrl", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 1, 16, 8, 27, 36, 133, DateTimeKind.Local).AddTicks(146), "https://dotnetmastery.com/bluevillaimages/villa2.jpg", new DateTime(2023, 1, 16, 8, 27, 36, 133, DateTimeKind.Local).AddTicks(147) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LocalUsers");

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "ImageUrl", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 1, 11, 10, 12, 44, 412, DateTimeKind.Local).AddTicks(7351), "https://dotnetmasteryimages.blob.core.windows.net/bluevillaimages/villa3.jpg", new DateTime(2023, 1, 11, 10, 12, 44, 412, DateTimeKind.Local).AddTicks(7398) });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "ImageUrl", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 1, 11, 10, 12, 44, 412, DateTimeKind.Local).AddTicks(7404), "https://dotnetmasteryimages.blob.core.windows.net/bluevillaimages/villa1.jpg", new DateTime(2023, 1, 11, 10, 12, 44, 412, DateTimeKind.Local).AddTicks(7405) });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "ImageUrl", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 1, 11, 10, 12, 44, 412, DateTimeKind.Local).AddTicks(7409), "https://dotnetmasteryimages.blob.core.windows.net/bluevillaimages/villa4.jpg", new DateTime(2023, 1, 11, 10, 12, 44, 412, DateTimeKind.Local).AddTicks(7410) });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedDate", "ImageUrl", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 1, 11, 10, 12, 44, 412, DateTimeKind.Local).AddTicks(7412), "https://dotnetmasteryimages.blob.core.windows.net/bluevillaimages/villa5.jpg", new DateTime(2023, 1, 11, 10, 12, 44, 412, DateTimeKind.Local).AddTicks(7414) });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedDate", "ImageUrl", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 1, 11, 10, 12, 44, 412, DateTimeKind.Local).AddTicks(7416), "https://dotnetmasteryimages.blob.core.windows.net/bluevillaimages/villa2.jpg", new DateTime(2023, 1, 11, 10, 12, 44, 412, DateTimeKind.Local).AddTicks(7418) });
        }
    }
}
