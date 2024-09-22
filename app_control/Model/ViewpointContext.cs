using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace app_control.Model;

public partial class ViewpointContext : DbContext
{
    public ViewpointContext()
    {
    }

    public ViewpointContext(DbContextOptions<ViewpointContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Ticket> Tickets { get; set; }

    public virtual DbSet<TicketType> TicketTypes { get; set; }

    public virtual DbSet<Tour> Tours { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=127.0.0.1;Port=5432;Database=viewpoint;Username=postgres;Password=1");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Ticket>(entity =>
        {
            entity.HasKey(e => e.IdTickets).HasName("tickets_pkey");

            entity.ToTable("tickets");

            entity.HasIndex(e => e.Email, "tickets_email_key").IsUnique();

            entity.Property(e => e.IdTickets).HasColumnName("id_tickets");
            entity.Property(e => e.Country)
                .HasMaxLength(50)
                .HasColumnName("country");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .HasColumnName("first_name");
            entity.Property(e => e.IdTicketType).HasColumnName("id_ticket_type");
            entity.Property(e => e.IdTours).HasColumnName("id_tours");
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .HasColumnName("last_name");
            entity.Property(e => e.Patronymic)
                .HasMaxLength(50)
                .HasColumnName("patronymic");
            entity.Property(e => e.PurchaseDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("purchase_date");

            entity.HasOne(d => d.IdTicketTypeNavigation).WithMany(p => p.Tickets)
                .HasForeignKey(d => d.IdTicketType)
                .HasConstraintName("tickets_id_ticket_type_fkey");

            entity.HasOne(d => d.IdToursNavigation).WithMany(p => p.Tickets)
                .HasForeignKey(d => d.IdTours)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("tickets_id_tours_fkey");
        });

        modelBuilder.Entity<TicketType>(entity =>
        {
            entity.HasKey(e => e.IdTicketType).HasName("ticket_type_pkey");

            entity.ToTable("ticket_type");

            entity.Property(e => e.IdTicketType).HasColumnName("id_ticket_type");
            entity.Property(e => e.TicketType1)
                .HasMaxLength(10)
                .HasColumnName("ticket_type");
        });

        modelBuilder.Entity<Tour>(entity =>
        {
            entity.HasKey(e => e.IdTours).HasName("tours_pkey");

            entity.ToTable("tours");

            entity.HasIndex(e => new { e.TourDate, e.TourTime }, "tours_tour_date_tour_time_key").IsUnique();

            entity.Property(e => e.IdTours).HasColumnName("id_tours");
            entity.Property(e => e.BookedTickets)
                .HasDefaultValue(0)
                .HasColumnName("booked_tickets");
            entity.Property(e => e.MaxCapacity)
                .HasDefaultValue(15)
                .HasColumnName("max_capacity");
            entity.Property(e => e.TourDate).HasColumnName("tour_date");
            entity.Property(e => e.TourTime).HasColumnName("tour_time");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
