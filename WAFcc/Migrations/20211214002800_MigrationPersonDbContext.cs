using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WAFcc.Migrations
{
    public partial class MigrationPersonDbContext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Relation_Person_InvitedId",
                table: "Relation");

            migrationBuilder.DropForeignKey(
                name: "FK_Relation_Person_InviterId",
                table: "Relation");

            migrationBuilder.DropIndex(
                name: "IX_Relation_InvitedId",
                table: "Relation");

            migrationBuilder.DropIndex(
                name: "IX_Relation_InviterId",
                table: "Relation");

            migrationBuilder.DropColumn(
                name: "InvitedId",
                table: "Relation");

            migrationBuilder.DropColumn(
                name: "InviterId",
                table: "Relation");

            migrationBuilder.CreateTable(
                name: "PersonRelation",
                columns: table => new
                {
                    RelationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PersonId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonRelation", x => new { x.RelationId, x.PersonId });
                    table.ForeignKey(
                        name: "FK_PersonRelation_Person_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Person",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PersonRelation_Relation_RelationId",
                        column: x => x.RelationId,
                        principalTable: "Relation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PersonRelation_PersonId",
                table: "PersonRelation",
                column: "PersonId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PersonRelation");

            migrationBuilder.AddColumn<Guid>(
                name: "InvitedId",
                table: "Relation",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "InviterId",
                table: "Relation",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Relation_InvitedId",
                table: "Relation",
                column: "InvitedId");

            migrationBuilder.CreateIndex(
                name: "IX_Relation_InviterId",
                table: "Relation",
                column: "InviterId");

            migrationBuilder.AddForeignKey(
                name: "FK_Relation_Person_InvitedId",
                table: "Relation",
                column: "InvitedId",
                principalTable: "Person",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Relation_Person_InviterId",
                table: "Relation",
                column: "InviterId",
                principalTable: "Person",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
