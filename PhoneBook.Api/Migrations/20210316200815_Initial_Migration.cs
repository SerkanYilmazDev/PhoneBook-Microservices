using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PhoneBook.Api.Migrations
{
    public partial class Initial_Migration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "person");

            migrationBuilder.CreateTable(
                name: "persons",
                schema: "person",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Surname = table.Column<string>(type: "text", nullable: true),
                    CompanyName = table.Column<string>(type: "text", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_persons", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "person_emails",
                schema: "person",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PersonId = table.Column<Guid>(type: "uuid", nullable: false),
                    EmailAdress = table.Column<string>(type: "text", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_person_emails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_person_emails_persons_PersonId",
                        column: x => x.PersonId,
                        principalSchema: "person",
                        principalTable: "persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "person_informations",
                schema: "person",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PersonId = table.Column<Guid>(type: "uuid", nullable: false),
                    Info = table.Column<int>(type: "integer", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_person_informations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_person_informations_persons_PersonId",
                        column: x => x.PersonId,
                        principalSchema: "person",
                        principalTable: "persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "person_locations",
                schema: "person",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PersonId = table.Column<Guid>(type: "uuid", nullable: false),
                    LocationName = table.Column<string>(type: "text", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_person_locations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_person_locations_persons_PersonId",
                        column: x => x.PersonId,
                        principalSchema: "person",
                        principalTable: "persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "person_phones",
                schema: "person",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PersonId = table.Column<Guid>(type: "uuid", nullable: false),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_person_phones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_person_phones_persons_PersonId",
                        column: x => x.PersonId,
                        principalSchema: "person",
                        principalTable: "persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_person_emails_PersonId",
                schema: "person",
                table: "person_emails",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_person_informations_PersonId",
                schema: "person",
                table: "person_informations",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_person_locations_PersonId",
                schema: "person",
                table: "person_locations",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_person_phones_PersonId",
                schema: "person",
                table: "person_phones",
                column: "PersonId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "person_emails",
                schema: "person");

            migrationBuilder.DropTable(
                name: "person_informations",
                schema: "person");

            migrationBuilder.DropTable(
                name: "person_locations",
                schema: "person");

            migrationBuilder.DropTable(
                name: "person_phones",
                schema: "person");

            migrationBuilder.DropTable(
                name: "persons",
                schema: "person");
        }
    }
}
