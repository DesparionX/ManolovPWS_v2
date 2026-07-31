using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ManolovPWS_v2.Infrastructure.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class Fixed_Messages_Date_ColumnType_Again : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SenderMetadata",
                table: "Messages");

            migrationBuilder.AddColumn<string>(
                name: "SenderIpAddress",
                table: "Messages",
                type: "character varying(45)",
                maxLength: 45,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SenderUserAgent",
                table: "Messages",
                type: "character varying(512)",
                maxLength: 512,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SenderIpAddress",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "SenderUserAgent",
                table: "Messages");

            migrationBuilder.AddColumn<string>(
                name: "SenderMetadata",
                table: "Messages",
                type: "jsonb",
                nullable: false,
                defaultValue: "");
        }
    }
}
