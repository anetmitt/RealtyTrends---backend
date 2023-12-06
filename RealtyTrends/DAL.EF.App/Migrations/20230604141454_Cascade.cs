using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.EF.App.Migrations
{
    /// <inheritdoc />
    public partial class Cascade : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TriggerFilters_Filters_FilterId",
                table: "TriggerFilters");

            migrationBuilder.DropForeignKey(
                name: "FK_TriggerFilters_Triggers_TriggerId",
                table: "TriggerFilters");

            migrationBuilder.DropForeignKey(
                name: "FK_UserTriggers_Triggers_TriggerId",
                table: "UserTriggers");

            migrationBuilder.AddForeignKey(
                name: "FK_TriggerFilters_Filters_FilterId",
                table: "TriggerFilters",
                column: "FilterId",
                principalTable: "Filters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TriggerFilters_Triggers_TriggerId",
                table: "TriggerFilters",
                column: "TriggerId",
                principalTable: "Triggers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserTriggers_Triggers_TriggerId",
                table: "UserTriggers",
                column: "TriggerId",
                principalTable: "Triggers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TriggerFilters_Filters_FilterId",
                table: "TriggerFilters");

            migrationBuilder.DropForeignKey(
                name: "FK_TriggerFilters_Triggers_TriggerId",
                table: "TriggerFilters");

            migrationBuilder.DropForeignKey(
                name: "FK_UserTriggers_Triggers_TriggerId",
                table: "UserTriggers");

            migrationBuilder.AddForeignKey(
                name: "FK_TriggerFilters_Filters_FilterId",
                table: "TriggerFilters",
                column: "FilterId",
                principalTable: "Filters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TriggerFilters_Triggers_TriggerId",
                table: "TriggerFilters",
                column: "TriggerId",
                principalTable: "Triggers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserTriggers_Triggers_TriggerId",
                table: "UserTriggers",
                column: "TriggerId",
                principalTable: "Triggers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
