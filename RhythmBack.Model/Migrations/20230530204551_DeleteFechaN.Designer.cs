﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RhythmBack.Model.Context;

#nullable disable

namespace RhythmBack.Model.Migrations
{
    [DbContext(typeof(RhythmDBContext))]
    [Migration("20230530204551_DeleteFechaN")]
    partial class DeleteFechaN
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("AlbumCancion", b =>
                {
                    b.Property<int>("AlbumId")
                        .HasColumnType("int");

                    b.Property<int>("CancionId")
                        .HasColumnType("int");

                    b.HasKey("AlbumId", "CancionId");

                    b.HasIndex("CancionId");

                    b.ToTable("AlbumCancion");
                });

            modelBuilder.Entity("AlbumUsuario", b =>
                {
                    b.Property<int>("AlbumId")
                        .HasColumnType("int");

                    b.Property<int>("UsuarioId")
                        .HasColumnType("int");

                    b.HasKey("AlbumId", "UsuarioId");

                    b.HasIndex("UsuarioId");

                    b.ToTable("AlbumUsuario");
                });

            modelBuilder.Entity("ArtistaCancion", b =>
                {
                    b.Property<int>("ArtistaId")
                        .HasColumnType("int");

                    b.Property<int>("CancionId")
                        .HasColumnType("int");

                    b.HasKey("ArtistaId", "CancionId");

                    b.HasIndex("CancionId");

                    b.ToTable("ArtistaCancion");
                });

            modelBuilder.Entity("ArtistaUsuario", b =>
                {
                    b.Property<int>("ArtistaId")
                        .HasColumnType("int");

                    b.Property<int>("UsuarioId")
                        .HasColumnType("int");

                    b.HasKey("ArtistaId", "UsuarioId");

                    b.HasIndex("UsuarioId");

                    b.ToTable("ArtistaUsuario");
                });

            modelBuilder.Entity("CancionGenero", b =>
                {
                    b.Property<int>("CancionId")
                        .HasColumnType("int");

                    b.Property<int>("GeneroId")
                        .HasColumnType("int");

                    b.HasKey("CancionId", "GeneroId");

                    b.HasIndex("GeneroId");

                    b.ToTable("CancionGenero");
                });

            modelBuilder.Entity("CancionLista", b =>
                {
                    b.Property<int>("CancionId")
                        .HasColumnType("int");

                    b.Property<int>("ListasId")
                        .HasColumnType("int");

                    b.HasKey("CancionId", "ListasId");

                    b.HasIndex("ListasId");

                    b.ToTable("CancionLista");
                });

            modelBuilder.Entity("ListaUsuario", b =>
                {
                    b.Property<int>("ListasId")
                        .HasColumnType("int");

                    b.Property<int>("UsuarioId")
                        .HasColumnType("int");

                    b.HasKey("ListasId", "UsuarioId");

                    b.HasIndex("UsuarioId");

                    b.ToTable("ListaUsuario");
                });

            modelBuilder.Entity("RhythmBack.Model.Models.Album", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime?>("Estreno")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Portada")
                        .HasColumnType("longtext");

                    b.Property<string>("Titulo")
                        .HasColumnType("longtext");

                    b.Property<int?>("Visitas")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Album");
                });

            modelBuilder.Entity("RhythmBack.Model.Models.Artista", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Portada")
                        .HasColumnType("longtext");

                    b.Property<string>("Titulo")
                        .HasColumnType("longtext");

                    b.Property<int?>("Visitas")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Artista");
                });

            modelBuilder.Entity("RhythmBack.Model.Models.Cancion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<TimeSpan?>("Duracion")
                        .HasColumnType("time(6)");

                    b.Property<string>("Lyrics")
                        .HasColumnType("longtext");

                    b.Property<string>("Titulo")
                        .HasColumnType("longtext");

                    b.Property<int?>("Visitas")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Cancion");
                });

            modelBuilder.Entity("RhythmBack.Model.Models.Genero", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Portada")
                        .HasColumnType("longtext");

                    b.Property<string>("Titulo")
                        .HasColumnType("longtext");

                    b.Property<int?>("Visitas")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Genero");
                });

            modelBuilder.Entity("RhythmBack.Model.Models.Lista", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("CreadorId")
                        .HasColumnType("int");

                    b.Property<string>("Portada")
                        .HasColumnType("longtext");

                    b.Property<string>("Titulo")
                        .HasColumnType("longtext");

                    b.Property<int?>("Visitas")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CreadorId");

                    b.ToTable("Lista");
                });

            modelBuilder.Entity("RhythmBack.Model.Models.Rol", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Descripcion")
                        .HasColumnType("longtext");

                    b.Property<string>("Nombre")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Rol");
                });

            modelBuilder.Entity("RhythmBack.Model.Models.Usuario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Nick")
                        .HasColumnType("longtext");

                    b.Property<string>("Nombre")
                        .HasColumnType("longtext");

                    b.Property<string>("Password")
                        .HasColumnType("longtext");

                    b.Property<int?>("RolId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("RolId");

                    b.ToTable("Usuario");
                });

            modelBuilder.Entity("AlbumCancion", b =>
                {
                    b.HasOne("RhythmBack.Model.Models.Album", null)
                        .WithMany()
                        .HasForeignKey("AlbumId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RhythmBack.Model.Models.Cancion", null)
                        .WithMany()
                        .HasForeignKey("CancionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("AlbumUsuario", b =>
                {
                    b.HasOne("RhythmBack.Model.Models.Album", null)
                        .WithMany()
                        .HasForeignKey("AlbumId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RhythmBack.Model.Models.Usuario", null)
                        .WithMany()
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ArtistaCancion", b =>
                {
                    b.HasOne("RhythmBack.Model.Models.Artista", null)
                        .WithMany()
                        .HasForeignKey("ArtistaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RhythmBack.Model.Models.Cancion", null)
                        .WithMany()
                        .HasForeignKey("CancionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ArtistaUsuario", b =>
                {
                    b.HasOne("RhythmBack.Model.Models.Artista", null)
                        .WithMany()
                        .HasForeignKey("ArtistaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RhythmBack.Model.Models.Usuario", null)
                        .WithMany()
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CancionGenero", b =>
                {
                    b.HasOne("RhythmBack.Model.Models.Cancion", null)
                        .WithMany()
                        .HasForeignKey("CancionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RhythmBack.Model.Models.Genero", null)
                        .WithMany()
                        .HasForeignKey("GeneroId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CancionLista", b =>
                {
                    b.HasOne("RhythmBack.Model.Models.Cancion", null)
                        .WithMany()
                        .HasForeignKey("CancionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RhythmBack.Model.Models.Lista", null)
                        .WithMany()
                        .HasForeignKey("ListasId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ListaUsuario", b =>
                {
                    b.HasOne("RhythmBack.Model.Models.Lista", null)
                        .WithMany()
                        .HasForeignKey("ListasId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RhythmBack.Model.Models.Usuario", null)
                        .WithMany()
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("RhythmBack.Model.Models.Lista", b =>
                {
                    b.HasOne("RhythmBack.Model.Models.Usuario", "Creador")
                        .WithMany("ListasCreadas")
                        .HasForeignKey("CreadorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Creador");
                });

            modelBuilder.Entity("RhythmBack.Model.Models.Usuario", b =>
                {
                    b.HasOne("RhythmBack.Model.Models.Rol", "Rol")
                        .WithMany("Usuarios")
                        .HasForeignKey("RolId");

                    b.Navigation("Rol");
                });

            modelBuilder.Entity("RhythmBack.Model.Models.Rol", b =>
                {
                    b.Navigation("Usuarios");
                });

            modelBuilder.Entity("RhythmBack.Model.Models.Usuario", b =>
                {
                    b.Navigation("ListasCreadas");
                });
#pragma warning restore 612, 618
        }
    }
}
