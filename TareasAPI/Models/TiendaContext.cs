using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace TareasAPI.Models;

public partial class TiendaContext : DbContext
{
    private readonly TiendaContext _context;

    public TiendaContext()
    {
    }

    public TiendaContext(DbContextOptions<TiendaContext> options)
        : base(options)
    {
        _context = this;
    }

    public virtual DbSet<Producto> Productos { get; set; }
    public virtual DbSet<Categoria> Categorias { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Categoria>(entity =>
        {
            entity.HasKey(e => e.IdCategoria);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}