using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WAFcc.Migrations
{
    public partial class PersonDbContextMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FileContent",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BinaryContent = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    FileType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileContent", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Person",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Firstname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Lastname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Patronym = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeathDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Sex = table.Column<bool>(type: "bit", nullable: false),
                    MainPhotoId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Person", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PersonDocument",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PersonId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FileContentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonDocument", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PersonDocument_FileContent_FileContentId",
                        column: x => x.FileContentId,
                        principalTable: "FileContent",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PersonDocument_Person_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Person",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PersonName",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Firstname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Lastname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Patronym = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PersonId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DateNameChanged = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonName", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PersonName_Person_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Person",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PersonPhoto",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PersonId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FileContentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonPhoto", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PersonPhoto_FileContent_FileContentId",
                        column: x => x.FileContentId,
                        principalTable: "FileContent",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PersonPhoto_Person_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Person",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Relation",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InviterId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InvitedId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RelationType = table.Column<int>(type: "int", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Relation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Relation_Person_InvitedId",
                        column: x => x.InvitedId,
                        principalTable: "Person",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Relation_Person_InviterId",
                        column: x => x.InviterId,
                        principalTable: "Person",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Person_MainPhotoId",
                table: "Person",
                column: "MainPhotoId",
                unique: true,
                filter: "[MainPhotoId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_PersonDocument_FileContentId",
                table: "PersonDocument",
                column: "FileContentId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonDocument_PersonId",
                table: "PersonDocument",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonName_PersonId",
                table: "PersonName",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonPhoto_FileContentId",
                table: "PersonPhoto",
                column: "FileContentId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonPhoto_PersonId",
                table: "PersonPhoto",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_Relation_InvitedId",
                table: "Relation",
                column: "InvitedId");

            migrationBuilder.CreateIndex(
                name: "IX_Relation_InviterId",
                table: "Relation",
                column: "InviterId");

            migrationBuilder.AddForeignKey(
                name: "FK_Person_PersonPhoto_MainPhotoId",
                table: "Person",
                column: "MainPhotoId",
                principalTable: "PersonPhoto",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Person_PersonPhoto_MainPhotoId",
                table: "Person");

            migrationBuilder.DropTable(
                name: "PersonDocument");

            migrationBuilder.DropTable(
                name: "PersonName");

            migrationBuilder.DropTable(
                name: "Relation");

            migrationBuilder.DropTable(
                name: "PersonPhoto");

            migrationBuilder.DropTable(
                name: "FileContent");

            migrationBuilder.DropTable(
                name: "Person");
        }
    }
}
