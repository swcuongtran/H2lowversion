using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace H2.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Add_GreenSensor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "greenSensorDatas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    DeviceId = table.Column<string>(type: "text", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_greenSensorDatas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "co2Datas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    GreenSensorDataId = table.Column<Guid>(type: "uuid", nullable: false),
                    Ppm = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_co2Datas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_co2Datas_greenSensorDatas_GreenSensorDataId",
                        column: x => x.GreenSensorDataId,
                        principalTable: "greenSensorDatas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "environmentDatas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    GreenSensorDataId = table.Column<Guid>(type: "uuid", nullable: false),
                    TemperatureC = table.Column<double>(type: "double precision", precision: 6, scale: 2, nullable: false),
                    HumidityPct = table.Column<double>(type: "double precision", precision: 6, scale: 2, nullable: false),
                    PressureHpa = table.Column<double>(type: "double precision", nullable: false),
                    Lux = table.Column<double>(type: "double precision", nullable: false),
                    UvMwCm2 = table.Column<double>(type: "double precision", nullable: false),
                    AltitudeM = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_environmentDatas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_environmentDatas_greenSensorDatas_GreenSensorDataId",
                        column: x => x.GreenSensorDataId,
                        principalTable: "greenSensorDatas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "soilDatas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    GreenSensorDataId = table.Column<Guid>(type: "uuid", nullable: false),
                    TemperatureC = table.Column<double>(type: "double precision", nullable: false),
                    HumidityPct = table.Column<double>(type: "double precision", nullable: false),
                    EcUSCm = table.Column<double>(type: "double precision", nullable: false),
                    Ph = table.Column<double>(type: "double precision", precision: 4, scale: 2, nullable: false),
                    N = table.Column<double>(type: "double precision", nullable: false),
                    P = table.Column<double>(type: "double precision", nullable: false),
                    K = table.Column<double>(type: "double precision", nullable: false),
                    SaltMgL = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_soilDatas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_soilDatas_greenSensorDatas_GreenSensorDataId",
                        column: x => x.GreenSensorDataId,
                        principalTable: "greenSensorDatas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_co2Datas_GreenSensorDataId",
                table: "co2Datas",
                column: "GreenSensorDataId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_environmentDatas_GreenSensorDataId",
                table: "environmentDatas",
                column: "GreenSensorDataId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_soilDatas_GreenSensorDataId",
                table: "soilDatas",
                column: "GreenSensorDataId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "co2Datas");

            migrationBuilder.DropTable(
                name: "environmentDatas");

            migrationBuilder.DropTable(
                name: "soilDatas");

            migrationBuilder.DropTable(
                name: "greenSensorDatas");
        }
    }
}
