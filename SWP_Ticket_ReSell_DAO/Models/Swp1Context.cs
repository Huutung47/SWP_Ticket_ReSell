using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace SWP_Ticket_ReSell_DAO.Models;

public partial class Swp1Context : DbContext
{
    public Swp1Context()
    {
    }

    public Swp1Context(DbContextOptions<Swp1Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Boxchat> Boxchats { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Feedback> Feedbacks { get; set; }

    public virtual DbSet<Notification> Notifications { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<Package> Packages { get; set; }

    public virtual DbSet<Report> Reports { get; set; }

    public virtual DbSet<Request> Requests { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Ticket> Tickets { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer(GetConnectionString());

    private string GetConnectionString()
    {
        IConfiguration config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", true, true)
            .Build();
        var strConn = config["ConnectionStrings:MyDB"];
        return strConn;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Boxchat>(entity =>
        {
            entity.HasKey(e => e.IdBoxchat).HasName("PK__Boxchat__23A1BC066D78CC8D");

            entity.ToTable("Boxchat");

            entity.Property(e => e.IdBoxchat).HasColumnName("ID_Boxchat");
            entity.Property(e => e.BuyerId).HasColumnName("Buyer_ID");
            entity.Property(e => e.ChatContent)
                .HasColumnType("text")
                .HasColumnName("Chat_content");
            entity.Property(e => e.IdTicket).HasColumnName("ID_Ticket");
            entity.Property(e => e.SellerId).HasColumnName("Seller_ID");

            entity.HasOne(d => d.Buyer).WithMany(p => p.BoxchatBuyers)
                .HasForeignKey(d => d.BuyerId)
                .HasConstraintName("FK__Boxchat__Buyer_I__35BCFE0A");

            entity.HasOne(d => d.IdTicketNavigation).WithMany(p => p.Boxchats)
                .HasForeignKey(d => d.IdTicket)
                .HasConstraintName("FK__Boxchat__ID_Tick__37A5467C");

            entity.HasOne(d => d.Seller).WithMany(p => p.BoxchatSellers)
                .HasForeignKey(d => d.SellerId)
                .HasConstraintName("FK__Boxchat__Seller___398D8EEE");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.IdCustomer).HasName("PK__Customer__2D8FDE5F79F79FE1");

            entity.ToTable("Customer");

            entity.Property(e => e.IdCustomer).HasColumnName("ID_Customer");
            entity.Property(e => e.AverageFeedback)
                .HasColumnType("decimal(3, 2)")
                .HasColumnName("Average_feedback");
            entity.Property(e => e.Contact)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.IdPackage).HasColumnName("ID_Package");
            entity.Property(e => e.IdRole).HasColumnName("ID_Role");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.PackageExpirationDate).HasColumnType("date");

            entity.HasOne(d => d.IdPackageNavigation).WithMany(p => p.Customers)
                .HasForeignKey(d => d.IdPackage)
                .HasConstraintName("FK__Customer__ID_Pac__3B75D760");

            entity.HasOne(d => d.IdRoleNavigation).WithMany(p => p.Customers)
                .HasForeignKey(d => d.IdRole)
                .HasConstraintName("FK__Customer__ID_Rol__3D5E1FD2");
        });

        modelBuilder.Entity<Feedback>(entity =>
        {
            entity.HasKey(e => e.IdFeedback).HasName("PK__Feedback__7CA05C3F8E29A895");

            entity.ToTable("Feedback");

            entity.Property(e => e.IdFeedback).HasColumnName("ID_Feedback");
            entity.Property(e => e.Comment).HasColumnType("text");
            entity.Property(e => e.IdOrder).HasColumnName("ID_Order");

            entity.HasOne(d => d.IdOrderNavigation).WithMany(p => p.Feedbacks)
                .HasForeignKey(d => d.IdOrder)
                .HasConstraintName("FK__Feedback__ID_Ord__3F466844");
        });

        modelBuilder.Entity<Notification>(entity =>
        {
            entity.HasKey(e => e.IdNotification).HasName("PK__Notifica__09D4F166CF01B009");

            entity.ToTable("Notification");

            entity.Property(e => e.IdNotification).HasColumnName("ID_Notification");
            entity.Property(e => e.Event)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.IdTicket).HasColumnName("ID_Ticket");
            entity.Property(e => e.OrganizationDay)
                .HasColumnType("date")
                .HasColumnName("Organization_day");
            entity.Property(e => e.OrganizingTime).HasColumnName("Organizing_time");
            entity.Property(e => e.Title)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.IdTicketNavigation).WithMany(p => p.Notifications)
                .HasForeignKey(d => d.IdTicket)
                .HasConstraintName("FK__Notificat__ID_Ti__412EB0B6");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.IdOrder).HasName("PK__Order__EC9FA955BB7403B2");

            entity.ToTable("Order");

            entity.Property(e => e.IdOrder).HasColumnName("ID_Order");
            entity.Property(e => e.Buyer)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.IdTicket).HasColumnName("ID_Ticket");
            entity.Property(e => e.OrderTime)
                .HasColumnType("datetime")
                .HasColumnName("Order_time");
            entity.Property(e => e.PaymentMethod)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("Payment_method");
            entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Seller)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.ShippingTime)
                .HasColumnType("datetime")
                .HasColumnName("Shipping_time");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.IdTicketNavigation).WithMany(p => p.Orders)
                .HasForeignKey(d => d.IdTicket)
                .HasConstraintName("FK__Order__ID_Ticket__4316F928");
        });

        modelBuilder.Entity<Package>(entity =>
        {
            entity.HasKey(e => e.IdPackage).HasName("PK__Package__10A648729D53F47C");

            entity.ToTable("Package");

            entity.Property(e => e.IdPackage).HasColumnName("ID_Package");
            entity.Property(e => e.NamePackage)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("Name_Package");
            entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");
        });

        modelBuilder.Entity<Report>(entity =>
        {
            entity.HasKey(e => new { e.IdCustomer, e.IdOrder }).HasName("PK__Report__634624CA6C58D340");

            entity.ToTable("Report");

            entity.Property(e => e.IdCustomer).HasColumnName("ID_Customer");
            entity.Property(e => e.IdOrder).HasColumnName("ID_Order");
            entity.Property(e => e.Comment).HasColumnType("text");

            entity.HasOne(d => d.IdCustomerNavigation).WithMany(p => p.Reports)
                .HasForeignKey(d => d.IdCustomer)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Report__ID_Custo__44FF419A");

            entity.HasOne(d => d.IdOrderNavigation).WithMany(p => p.Reports)
                .HasForeignKey(d => d.IdOrder)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Report__ID_Order__46E78A0C");
        });

        modelBuilder.Entity<Request>(entity =>
        {
            entity.HasKey(e => new { e.IdTicket, e.IdCustomer }).HasName("PK__Request__9B2D21ED5D0A3351");

            entity.ToTable("Request");

            entity.Property(e => e.IdTicket).HasColumnName("ID_Ticket");
            entity.Property(e => e.IdCustomer).HasColumnName("ID_Customer");
            entity.Property(e => e.History).HasColumnType("text");
            entity.Property(e => e.PriceWant)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("Price_want");

            entity.HasOne(d => d.IdCustomerNavigation).WithMany(p => p.Requests)
                .HasForeignKey(d => d.IdCustomer)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Request__ID_Cust__48CFD27E");

            entity.HasOne(d => d.IdTicketNavigation).WithMany(p => p.Requests)
                .HasForeignKey(d => d.IdTicket)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Request__ID_Tick__4AB81AF0");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.IdRole).HasName("PK__Role__43DCD32D8D5F9CA8");

            entity.ToTable("Role");

            entity.Property(e => e.IdRole).HasColumnName("ID_Role");
            entity.Property(e => e.NameRole)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("Name_role");
            entity.Property(e => e.Role1)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("Role");
        });

        modelBuilder.Entity<Ticket>(entity =>
        {
            entity.HasKey(e => e.IdTicket).HasName("PK__Ticket__79F5DC0841203606");

            entity.ToTable("Ticket");

            entity.Property(e => e.IdTicket).HasColumnName("ID_Ticket");
            entity.Property(e => e.Buyer)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.IdCustomer).HasColumnName("ID_Customer");
            entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.TicketCategory)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("Ticket_category");
            entity.Property(e => e.TicketHistory)
                .HasColumnType("text")
                .HasColumnName("Ticket_History");
            entity.Property(e => e.TicketType).HasColumnName("Ticket_type");

            entity.HasOne(d => d.IdCustomerNavigation).WithMany(p => p.Tickets)
                .HasForeignKey(d => d.IdCustomer)
                .HasConstraintName("FK__Ticket__ID_Custo__4CA06362");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
