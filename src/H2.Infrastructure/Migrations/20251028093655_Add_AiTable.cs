using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace H2.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Add_AiTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GreenImageDetected",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    OriginalImageId = table.Column<Guid>(type: "uuid", nullable: false),
                    DeviceId = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    DetectedImageUrl = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    Labels = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    ProcessedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GreenImageDetected", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GreenImageDetected");
        }
    }
}
