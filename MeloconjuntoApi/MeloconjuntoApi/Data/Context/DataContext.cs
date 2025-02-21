using Microsoft.EntityFrameworkCore;
using MeloconjuntoApi.Models;
using Microsoft.VisualStudio.Web.CodeGeneration.EntityFrameworkCore;
using NpgsqlTypes;
using Npgsql;

namespace MeloconjuntoApi.Data.Context
{
    public class DataContext : DbContext //hace que la clase DaTaContext herede de la clase DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) // inicializa el contexto de la base de datos y configura el comportamiento de la clase dbcontext
        {

        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Credencial> Credenciales { get; set; }
        public DbSet<Dificultad> Dificultades { get; set; }
        public DbSet<Mapa> Mapas { get; set; }
        public DbSet<Nivel> Niveles { get; set; }
        public DbSet<Pregunta> Preguntas { get; set; }
        public DbSet<Puntaje> Puntajes { get; set; } 
        public DbSet<Ranking> Rankings { get; set; }
        public DbSet<RankingUsuario> RankingsUsuarios { get; set; }
        public DbSet<RecuperaPassword> RecuperarPasswords { get; set; }
        public DbSet<Respuesta> Respuestas { get; set; }
        public DbSet<Rol> Roles { get; set; }
        public DbSet<UsuarioRol> UsuariosRoles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>(entity => {


            // Relaciones uno a muchos
            entity.HasMany(u => u.RecuperarPasswords)
                .WithOne(r => r.Usuario)
                .HasForeignKey(r => r.UsuarioId);

            entity.HasMany(u => u.Puntajes)
                .WithOne(p => p.Usuario)
                .HasForeignKey(p => p.UsuarioId);


            // Relaciones muchos a muchos
            entity.HasMany(u => u.UsuariosRoles)
                .WithOne(ur => ur.Usuario)
                .HasForeignKey(ur => ur.UsuarioId);

            entity.HasMany(u => u.RankingsUsuarios)
                .WithOne(ru => ru.Usuario)
                .HasForeignKey(ru => ru.UsuarioId);

            // Relaciones uno a uno

            entity.HasOne(u => u.Credenciales)
                 .WithOne(c => c.Usuario)
                 .HasForeignKey<Credencial>(c => c.UsuarioId)
                 .OnDelete(DeleteBehavior.Cascade);  // Ajusta el comportamiento de eliminación 
             });
          
            


            modelBuilder.Entity<Credencial>(entity =>
            {
                entity.HasIndex(e => e.CredencialCorreo)
                .IsUnique();

                entity.HasOne(c => c.Usuario)
                    .WithOne(u => u.Credenciales)
                    .HasForeignKey<Credencial>(c => c.UsuarioId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Dificultad>(entity =>
            {
                // Relación uno a muchos con Mapa

                entity.HasMany(d => d.Mapas)
                    .WithOne(m => m.Dificultad)
                    .HasForeignKey(m => m.DificultadId);
                    
            });

            modelBuilder.Entity<Mapa>(entity =>
            {
                entity.HasOne(m => m.Dificultad)
                .WithMany(d => d.Mapas)
                .HasForeignKey(m => m.DificultadId);

                entity.HasMany(m => m.Niveles)
                .WithOne(n => n.Mapa)
                .HasForeignKey(n => n.MapaId);

            });

            modelBuilder.Entity<Nivel>(entity =>
            {
                entity.HasOne(n => n.Mapa)
                .WithMany(m => m.Niveles)
                .HasForeignKey(n => n.MapaId);

                entity.HasOne(n => n.Pregunta)
                .WithOne(p => p.Nivel)
                .HasForeignKey<Pregunta>(p => p.NivelId);

                entity.HasMany(n => n.Puntajes)
                .WithOne(pu => pu.Nivel)
                .HasForeignKey(pu => pu.NivelId);
            });

            modelBuilder.Entity<Pregunta>(entity =>
            {
                entity.HasOne(p => p.Nivel)
                .WithOne(n => n.Pregunta)
                .HasForeignKey<Pregunta>(p => p.NivelId);

                entity.HasMany(p => p.Respuestas)
                .WithOne(r => r.Pregunta)
                .HasForeignKey(r => r.PreguntaId);
            });

           

            modelBuilder.Entity<Puntaje>(entity =>
            {
                entity.HasOne(p => p.Nivel)
                .WithMany(n => n.Puntajes)
                .HasForeignKey(p => p.NivelId);

                entity.HasOne(p => p.Usuario)
                .WithMany(u => u.Puntajes)
                .HasForeignKey(p => p.UsuarioId);

                entity.Property(p => p.PuntajeTime)
                .HasColumnType("numeric(3,2)"); 

            });

            modelBuilder.Entity<Ranking>(entity =>
            {
                entity.HasMany(r => r.RankingsUsuarios)
                .WithOne(ru=> ru.Ranking)
                .HasForeignKey(ru => ru.RankingId);

            });

            modelBuilder.Entity<RankingUsuario>(entity =>
            {
                
                entity.HasOne(ru => ru.Ranking)
                .WithMany(r=> r.RankingsUsuarios)
                .HasForeignKey(ru => ru.RankingId);

                entity.HasOne(ru => ru.Usuario)
                .WithMany(u=> u.RankingsUsuarios)
                .HasForeignKey(ru => ru.UsuarioId);

            });

            modelBuilder.Entity<RecuperaPassword>(entity =>
            {
                entity.HasIndex(r => r.RecuperaPasswordToken)
                .IsUnique();

                entity.HasOne(r => r.Usuario)
                .WithMany(u => u.RecuperarPasswords)
                .HasForeignKey(r => r.UsuarioId);
            });

            modelBuilder.Entity<Respuesta>(entity =>
            {
                entity.HasOne(r => r.Pregunta)
                .WithMany(p => p.Respuestas)
                .HasForeignKey(r => r.PreguntaId);
            });


            modelBuilder.Entity<Rol>(entity =>
            {
                entity.HasMany(r => r.UsuariosRoles)
                .WithOne(ur => ur.Rol)
                .HasForeignKey(ur => ur.RolId);
            });


            modelBuilder.Entity<UsuarioRol>(entity =>
            {

                entity.HasOne(ur => ur.Rol)
                .WithMany(r => r.UsuariosRoles)
                .HasForeignKey(ur => ur.RolId);

                entity.HasOne(ur => ur.Usuario)
                .WithMany(u => u.UsuariosRoles)
                .HasForeignKey(ur => ur.UsuarioId);
            });

        }

    }
}
