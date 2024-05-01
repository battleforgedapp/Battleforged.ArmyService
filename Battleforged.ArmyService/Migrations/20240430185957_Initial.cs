using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Battleforged.ArmyService.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "armies",
                columns: table => new
                {
                    army_id = table.Column<Guid>(type: "uuid", nullable: false),
                    army_parent_id = table.Column<Guid>(type: "uuid", nullable: true),
                    army_name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    army_type = table.Column<int>(type: "integer", nullable: false),
                    created_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    deleted_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_armies", x => x.army_id);
                    table.ForeignKey(
                        name: "FK_armies_armies_army_parent_id",
                        column: x => x.army_parent_id,
                        principalTable: "armies",
                        principalColumn: "army_id");
                });

            migrationBuilder.CreateTable(
                name: "def_battle_sizes",
                columns: table => new
                {
                    battle_size_id = table.Column<Guid>(type: "uuid", nullable: false),
                    battle_size = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    points_limit = table.Column<int>(type: "integer", nullable: false),
                    created_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    deleted_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_def_battle_sizes", x => x.battle_size_id);
                });

            migrationBuilder.CreateTable(
                name: "event_outbox",
                columns: table => new
                {
                    outbox_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    event_name = table.Column<string>(type: "character varying(4098)", maxLength: 4098, nullable: false),
                    event_data = table.Column<string>(type: "text", nullable: false),
                    created_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    sent_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    total_attempts = table.Column<int>(type: "integer", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_event_outbox", x => x.outbox_id);
                });

            migrationBuilder.CreateTable(
                name: "detachments",
                columns: table => new
                {
                    detachment_id = table.Column<Guid>(type: "uuid", nullable: false),
                    army_id = table.Column<Guid>(type: "uuid", nullable: false),
                    detachment_name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    rule_name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    rule_text = table.Column<string>(type: "text", nullable: true),
                    created_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    deleted_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_detachments", x => x.detachment_id);
                    table.ForeignKey(
                        name: "FK_detachments_armies_army_id",
                        column: x => x.army_id,
                        principalTable: "armies",
                        principalColumn: "army_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "units",
                columns: table => new
                {
                    unit_id = table.Column<Guid>(type: "uuid", nullable: false),
                    army_id = table.Column<Guid>(type: "uuid", nullable: false),
                    unit_name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    created_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    deleted_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_units", x => x.unit_id);
                    table.ForeignKey(
                        name: "FK_units_armies_army_id",
                        column: x => x.army_id,
                        principalTable: "armies",
                        principalColumn: "army_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "detachment_enhancements",
                columns: table => new
                {
                    enhancement_id = table.Column<Guid>(type: "uuid", nullable: false),
                    detachment_id = table.Column<Guid>(type: "uuid", nullable: false),
                    enhancement_name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    enhancement_text = table.Column<string>(type: "text", nullable: true),
                    points_cost = table.Column<int>(type: "integer", nullable: false),
                    created_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    deleted_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_detachment_enhancements", x => x.enhancement_id);
                    table.ForeignKey(
                        name: "FK_detachment_enhancements_detachments_detachment_id",
                        column: x => x.detachment_id,
                        principalTable: "detachments",
                        principalColumn: "detachment_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "unit_groupings",
                columns: table => new
                {
                    unit_grouping_id = table.Column<Guid>(type: "uuid", nullable: false),
                    unit_id = table.Column<Guid>(type: "uuid", nullable: false),
                    model_count = table.Column<int>(type: "integer", nullable: false),
                    points_cost = table.Column<int>(type: "integer", nullable: false),
                    created_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    deleted_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_unit_groupings", x => x.unit_grouping_id);
                    table.ForeignKey(
                        name: "FK_unit_groupings_units_unit_id",
                        column: x => x.unit_id,
                        principalTable: "units",
                        principalColumn: "unit_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "def_battle_sizes",
                columns: new[] { "battle_size_id", "created_date", "deleted_date", "battle_size", "points_limit" },
                values: new object[,]
                {
                    { new Guid("19147fa2-3004-411d-9992-223c19eb8c9f"), new DateTime(2024, 4, 30, 18, 59, 57, 13, DateTimeKind.Utc).AddTicks(2640), null, "Strike Force", 2000 },
                    { new Guid("273bf3bf-714a-478f-bc44-d07bd9f7c480"), new DateTime(2024, 4, 30, 18, 59, 57, 13, DateTimeKind.Utc).AddTicks(2150), null, "Incursion", 1000 },
                    { new Guid("dcf594c8-a7b0-46af-9714-ebce6b58605b"), new DateTime(2024, 4, 30, 18, 59, 57, 13, DateTimeKind.Utc).AddTicks(2640), null, "Onslaught", 3000 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_armies_army_parent_id",
                table: "armies",
                column: "army_parent_id");

            migrationBuilder.CreateIndex(
                name: "IX_detachment_enhancements_detachment_id",
                table: "detachment_enhancements",
                column: "detachment_id");

            migrationBuilder.CreateIndex(
                name: "IX_detachments_army_id",
                table: "detachments",
                column: "army_id");

            migrationBuilder.CreateIndex(
                name: "IX_event_outbox_sent_date",
                table: "event_outbox",
                column: "sent_date");

            migrationBuilder.CreateIndex(
                name: "IX_unit_groupings_unit_id",
                table: "unit_groupings",
                column: "unit_id");

            migrationBuilder.CreateIndex(
                name: "IX_units_army_id",
                table: "units",
                column: "army_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "def_battle_sizes");

            migrationBuilder.DropTable(
                name: "detachment_enhancements");

            migrationBuilder.DropTable(
                name: "event_outbox");

            migrationBuilder.DropTable(
                name: "unit_groupings");

            migrationBuilder.DropTable(
                name: "detachments");

            migrationBuilder.DropTable(
                name: "units");

            migrationBuilder.DropTable(
                name: "armies");
        }
    }
}
