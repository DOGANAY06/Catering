using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Catering.Migrations
{
    public partial class cateringserviceDBContext2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cateringler",
                columns: table => new
                {
                    cateringID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    cateringadi = table.Column<string>(maxLength: 100, nullable: true),
                    aktif = table.Column<bool>(nullable: true),
                    silindi = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cateringler", x => x.cateringID);
                });

            migrationBuilder.CreateTable(
                name: "Kullanicilar",
                columns: table => new
                {
                    kullaniciID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    adi = table.Column<string>(maxLength: 100, nullable: true),
                    soyadi = table.Column<string>(maxLength: 100, nullable: true),
                    eposta = table.Column<string>(maxLength: 100, nullable: true),
                    telefon = table.Column<string>(maxLength: 15, nullable: true),
                    parola = table.Column<string>(maxLength: 35, nullable: true),
                    yetki = table.Column<bool>(nullable: true),
                    aktif = table.Column<bool>(nullable: true),
                    silindi = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kullanicilar", x => x.kullaniciID);
                });

            migrationBuilder.CreateTable(
                name: "menuler",
                columns: table => new
                {
                    menuID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    baslik = table.Column<string>(maxLength: 250, nullable: true),
                    url = table.Column<string>(maxLength: 255, nullable: true),
                    sira = table.Column<byte>(nullable: true),
                    ustID = table.Column<int>(nullable: true),
                    aktif = table.Column<bool>(nullable: true),
                    silindi = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_menuler", x => x.menuID);
                });

            migrationBuilder.CreateTable(
                name: "Sayfalar",
                columns: table => new
                {
                    sayfaID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    baslik = table.Column<string>(maxLength: 250, nullable: true),
                    icerik = table.Column<string>(type: "ntext", nullable: true),
                    aktif = table.Column<bool>(nullable: true),
                    silindi = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sayfalar", x => x.sayfaID);
                });

            migrationBuilder.CreateTable(
                name: "CateringDetail",
                columns: table => new
                {
                    turID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    cateringfirmabilgi = table.Column<string>(maxLength: 250, nullable: true),
                    icerik = table.Column<string>(type: "ntext", nullable: true),
                    sira = table.Column<byte>(nullable: true),
                    cateringID = table.Column<int>(nullable: false),
                    eklemetarihi = table.Column<DateTime>(type: "datetime", nullable: true),
                    aktif = table.Column<bool>(nullable: true),
                    silindi = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CateringDetail", x => x.turID);
                    table.ForeignKey(
                        name: "FK_CateringDetail_Cateringler",
                        column: x => x.cateringID,
                        principalTable: "Cateringler",
                        principalColumn: "cateringID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Yorum",
                columns: table => new
                {
                    yorumID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    yorum = table.Column<string>(maxLength: 500, nullable: true),
                    eklemetarihi = table.Column<DateTime>(type: "datetime", nullable: true),
                    turID = table.Column<int>(nullable: false),
                    aktif = table.Column<bool>(nullable: true),
                    silindi = table.Column<bool>(nullable: true),
                    uyeID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Yorum", x => x.yorumID);
                    table.ForeignKey(
                        name: "FK_Yorum_CateringDetail",
                        column: x => x.turID,
                        principalTable: "CateringDetail",
                        principalColumn: "turID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Yorum_Kullanicilar",
                        column: x => x.uyeID,
                        principalTable: "Kullanicilar",
                        principalColumn: "kullaniciID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CateringDetail_cateringID",
                table: "CateringDetail",
                column: "cateringID");

            migrationBuilder.CreateIndex(
                name: "IX_Yorum_turID",
                table: "Yorum",
                column: "turID");

            migrationBuilder.CreateIndex(
                name: "IX_Yorum_uyeID",
                table: "Yorum",
                column: "uyeID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "menuler");

            migrationBuilder.DropTable(
                name: "Sayfalar");

            migrationBuilder.DropTable(
                name: "Yorum");

            migrationBuilder.DropTable(
                name: "CateringDetail");

            migrationBuilder.DropTable(
                name: "Kullanicilar");

            migrationBuilder.DropTable(
                name: "Cateringler");
        }
    }
}
