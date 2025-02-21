using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MeloconjuntoApi.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Dificultades",
                columns: table => new
                {
                    DificultadId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DificultadNombre = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dificultades", x => x.DificultadId);
                });

            migrationBuilder.CreateTable(
                name: "Rankings",
                columns: table => new
                {
                    RankingId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RankingNombre = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rankings", x => x.RankingId);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    RolId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RolNombre = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.RolId);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    UsuarioId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UsuarioNombre = table.Column<string>(type: "text", nullable: false),
                    UsuarioApellido = table.Column<string>(type: "text", nullable: false),
                    UsuarioEdad = table.Column<byte>(type: "smallint", nullable: false),
                    UsuarioFechaRegistro = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UsuarioActivo = table.Column<bool>(type: "boolean", nullable: false),
                    UsuarioTokenActivacion = table.Column<string>(type: "text", nullable: true),
                    UsuarioTokenVence = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.UsuarioId);
                });

            migrationBuilder.CreateTable(
                name: "Mapas",
                columns: table => new
                {
                    MapaId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    MapaNombre = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    MapaImagen = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    DificultadId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mapas", x => x.MapaId);
                    table.ForeignKey(
                        name: "FK_Mapas_Dificultades_DificultadId",
                        column: x => x.DificultadId,
                        principalTable: "Dificultades",
                        principalColumn: "DificultadId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Credenciales",
                columns: table => new
                {
                    CredencialId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CredencialCorreo = table.Column<string>(type: "text", nullable: false),
                    CredencialPassword = table.Column<string>(type: "text", nullable: false),
                    UsuarioId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Credenciales", x => x.CredencialId);
                    table.ForeignKey(
                        name: "FK_Credenciales_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "UsuarioId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RankingsUsuarios",
                columns: table => new
                {
                    RankingUsuarioId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RankingId = table.Column<int>(type: "integer", nullable: false),
                    UsuarioId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RankingsUsuarios", x => x.RankingUsuarioId);
                    table.ForeignKey(
                        name: "FK_RankingsUsuarios_Rankings_RankingId",
                        column: x => x.RankingId,
                        principalTable: "Rankings",
                        principalColumn: "RankingId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RankingsUsuarios_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "UsuarioId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RecuperarPasswords",
                columns: table => new
                {
                    RecuperaPasswordId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RecuperaPasswordToken = table.Column<string>(type: "text", nullable: false),
                    RecuperaPasswordFechaSolicitud = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    RecuperaPasswordFechaLimite = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UsuarioId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecuperarPasswords", x => x.RecuperaPasswordId);
                    table.ForeignKey(
                        name: "FK_RecuperarPasswords_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "UsuarioId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UsuariosRoles",
                columns: table => new
                {
                    UsuarioRolId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UsuarioId = table.Column<int>(type: "integer", nullable: false),
                    RolId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuariosRoles", x => x.UsuarioRolId);
                    table.ForeignKey(
                        name: "FK_UsuariosRoles_Roles_RolId",
                        column: x => x.RolId,
                        principalTable: "Roles",
                        principalColumn: "RolId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsuariosRoles_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "UsuarioId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Niveles",
                columns: table => new
                {
                    NivelId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NivelName = table.Column<string>(type: "character varying(70)", maxLength: 70, nullable: false),
                    MapaId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Niveles", x => x.NivelId);
                    table.ForeignKey(
                        name: "FK_Niveles_Mapas_MapaId",
                        column: x => x.MapaId,
                        principalTable: "Mapas",
                        principalColumn: "MapaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Preguntas",
                columns: table => new
                {
                    PreguntaId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PreguntaSentencia = table.Column<string>(type: "character varying(225)", maxLength: 225, nullable: false),
                    NivelId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Preguntas", x => x.PreguntaId);
                    table.ForeignKey(
                        name: "FK_Preguntas_Niveles_NivelId",
                        column: x => x.NivelId,
                        principalTable: "Niveles",
                        principalColumn: "NivelId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Puntajes",
                columns: table => new
                {
                    PuntajeId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PuntajeAciertos = table.Column<int>(type: "integer", nullable: false),
                    PuntajePuntos = table.Column<int>(type: "integer", nullable: false),
                    PuntajeTime = table.Column<decimal>(type: "numeric(3,2)", nullable: false),
                    PuntajeErrores = table.Column<int>(type: "integer", nullable: false),
                    NivelId = table.Column<int>(type: "integer", nullable: false),
                    UsuarioId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Puntajes", x => x.PuntajeId);
                    table.ForeignKey(
                        name: "FK_Puntajes_Niveles_NivelId",
                        column: x => x.NivelId,
                        principalTable: "Niveles",
                        principalColumn: "NivelId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Puntajes_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "UsuarioId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Respuestas",
                columns: table => new
                {
                    RespuestaId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RespuestaSentencia = table.Column<string>(type: "character varying(225)", maxLength: 225, nullable: false),
                    RespuestaCorrecta = table.Column<bool>(type: "boolean", nullable: false),
                    PreguntaId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Respuestas", x => x.RespuestaId);
                    table.ForeignKey(
                        name: "FK_Respuestas_Preguntas_PreguntaId",
                        column: x => x.PreguntaId,
                        principalTable: "Preguntas",
                        principalColumn: "PreguntaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Credenciales_CredencialCorreo",
                table: "Credenciales",
                column: "CredencialCorreo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Credenciales_UsuarioId",
                table: "Credenciales",
                column: "UsuarioId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Mapas_DificultadId",
                table: "Mapas",
                column: "DificultadId");

            migrationBuilder.CreateIndex(
                name: "IX_Niveles_MapaId",
                table: "Niveles",
                column: "MapaId");

            migrationBuilder.CreateIndex(
                name: "IX_Preguntas_NivelId",
                table: "Preguntas",
                column: "NivelId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Puntajes_NivelId",
                table: "Puntajes",
                column: "NivelId");

            migrationBuilder.CreateIndex(
                name: "IX_Puntajes_UsuarioId",
                table: "Puntajes",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_RankingsUsuarios_RankingId",
                table: "RankingsUsuarios",
                column: "RankingId");

            migrationBuilder.CreateIndex(
                name: "IX_RankingsUsuarios_UsuarioId",
                table: "RankingsUsuarios",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_RecuperarPasswords_RecuperaPasswordToken",
                table: "RecuperarPasswords",
                column: "RecuperaPasswordToken",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RecuperarPasswords_UsuarioId",
                table: "RecuperarPasswords",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Respuestas_PreguntaId",
                table: "Respuestas",
                column: "PreguntaId");

            migrationBuilder.CreateIndex(
                name: "IX_UsuariosRoles_RolId",
                table: "UsuariosRoles",
                column: "RolId");

            migrationBuilder.CreateIndex(
                name: "IX_UsuariosRoles_UsuarioId",
                table: "UsuariosRoles",
                column: "UsuarioId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Credenciales");

            migrationBuilder.DropTable(
                name: "Puntajes");

            migrationBuilder.DropTable(
                name: "RankingsUsuarios");

            migrationBuilder.DropTable(
                name: "RecuperarPasswords");

            migrationBuilder.DropTable(
                name: "Respuestas");

            migrationBuilder.DropTable(
                name: "UsuariosRoles");

            migrationBuilder.DropTable(
                name: "Rankings");

            migrationBuilder.DropTable(
                name: "Preguntas");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Niveles");

            migrationBuilder.DropTable(
                name: "Mapas");

            migrationBuilder.DropTable(
                name: "Dificultades");
        }
    }
}
