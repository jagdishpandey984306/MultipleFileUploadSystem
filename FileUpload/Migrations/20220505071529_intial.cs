using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FileUpload.Migrations
{
    public partial class intial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AttachementDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FromMail = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    ToMail = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    SendDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttachementDetails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UploadFile",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    File = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    Extension = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UploadDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UploadFile", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AttachementDetails");

            migrationBuilder.DropTable(
                name: "UploadFile");
        }
    }
}
