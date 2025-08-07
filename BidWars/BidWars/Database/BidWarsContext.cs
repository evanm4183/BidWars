using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace BidWars.Database;

public partial class BidWarsContext : DbContext
{
    public BidWarsContext()
    {
    }

    public BidWarsContext(DbContextOptions<BidWarsContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AspNetUser> AspNetUsers { get; set; }

    public virtual DbSet<Auction> Auctions { get; set; }

    public virtual DbSet<Bid> Bids { get; set; }

    public virtual DbSet<Location> Locations { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductCategory> ProductCategories { get; set; }

    public virtual DbSet<Invoice> Invoices { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<AspNetUser>(entity =>
        {
            entity.HasIndex(e => e.NormalizedEmail, "EmailIndex");

            entity.HasIndex(e => e.NormalizedUserName, "UserNameIndex")
                .IsUnique()
                .HasFilter("([NormalizedUserName] IS NOT NULL)");

            entity.Property(e => e.Email).HasMaxLength(256);
            entity.Property(e => e.NormalizedEmail).HasMaxLength(256);
            entity.Property(e => e.NormalizedUserName).HasMaxLength(256);
            entity.Property(e => e.UserName).HasMaxLength(256);
        });

        modelBuilder.Entity<Auction>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Auction__3214EC0782495173");

            entity.ToTable("Auction");

            entity.Property(e => e.StartingAmount).HasColumnType("money");

            entity.HasOne(d => d.Location).WithMany(p => p.Auctions)
                .HasForeignKey(d => d.LocationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Auction__Locatio__440B1D61");

            entity.HasOne(d => d.Product).WithMany(p => p.Auctions)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Auction__Product__412EB0B6");
        });

        modelBuilder.Entity<Bid>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Bid__3214EC07DC3BDC2D");

            entity.ToTable("Bid");

            entity.Property(e => e.BidderId).HasMaxLength(450);

            entity.HasOne(d => d.Auction).WithMany(p => p.Bids)
                .HasForeignKey(d => d.AuctionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Bid__AuctionId__4316F928");

            entity.HasOne(d => d.Bidder).WithMany(p => p.Bids)
                .HasForeignKey(d => d.BidderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Bid__BidderId__4222D4EF");
        });

        modelBuilder.Entity<Location>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Location__3214EC0764AEC11D");

            entity.ToTable("Location");

            entity.Property(e => e.City)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.State)
                .HasMaxLength(2)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.StreetAddress)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Zip)
                .HasMaxLength(5)
                .IsUnicode(false)
                .IsFixedLength();
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Product__3214EC07FE0DF557");

            entity.ToTable("Product");

            entity.Property(e => e.Description)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.ProductName)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.ProductCategory).WithMany(p => p.Products)
                .HasForeignKey(d => d.ProductCategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Product__Product__44FF419A");
        });

        modelBuilder.Entity<ProductCategory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ProductC__3214EC074AD44E01");

            entity.ToTable("ProductCategory");

            entity.Property(e => e.CategoryName)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Invoice>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Invoice__3214EC07D491F858");

            entity.ToTable("Invoice");

            entity.Property(e => e.AmountDue).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.UserId).HasMaxLength(450);

            entity.HasOne(d => d.User).WithMany(p => p.Invoices)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Invoice__UserId__4E88ABD4");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
