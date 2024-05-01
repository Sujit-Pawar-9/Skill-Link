using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using BOL; // Import your entity classes namespace

namespace IOCWebApp.Contexts
{
    public class CollectionContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        
        public DbSet<UserRequirement> UserRequirements { get; set; }
        public DbSet<BOL.ServiceProvider> ServiceProviders { get; set; }
        // public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<BookingList> BookingLists { get; set; }
        public DbSet<Status> Statuses { get; set; } 

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Configure the database provider and connection string
            string conString = "server=127.0.0.1;uid=root;pwd=Sujitpawar;database=test1";

            // Specify the MySQL server version
            ServerVersion serverVersion = ServerVersion.AutoDetect(conString);

            optionsBuilder.UseMySql(conString, serverVersion);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

modelBuilder.Entity<User>()
                .ToTable("Users");

            modelBuilder.Entity<BOL.ServiceProvider>()
                .ToTable("ServiceProviders");

            modelBuilder.Entity<BookingList>()
                .ToTable("BookingLists");

            modelBuilder.Entity<UserRequirement>()
                .ToTable("UserRequirements");

            // modelBuilder.Entity<Feedback>()
            //     .ToTable("Feedbacks");

            modelBuilder.Entity<Status>()
                .ToTable("Statuses");

            modelBuilder.Entity<BookingList>()
                .HasKey(bl => bl.RequirementID);

              modelBuilder.Entity<User>()
                .HasMany(u => u.UserRequirements)
                .WithOne(ur => ur.User)
                .HasForeignKey(ur => ur.UserID);

            modelBuilder.Entity<UserRequirement>().HasKey(ur => ur.RequirementID);

            // modelBuilder.Entity<BOL.ServiceProvider>()
            //     .HasMany(sp => sp.BookingLists)
            //     .WithOne(bl => bl.ServiceProvider)
            //     .HasForeignKey(bl => bl.ServiceProviderID);


    modelBuilder.Entity<BookingList>()
        .HasOne(bl => bl.User)
        .WithMany(u => u.BookingLists)
        .HasForeignKey(bl => bl.UserID);

//   modelBuilder.Entity<User>()
//         .HasOne(u => u.ServiceProvider)  
//         .WithOne(sp => sp.User)  // Assuming ServiceProvider has a navigation property to User
//         .HasForeignKey<BOL.ServiceProvider>(sp => sp.UserID);


            // modelBuilder.Entity<Feedback>()
            //     .HasKey(f => new { f.Username, f.ServiceProviderUsername });

            // modelBuilder.Entity<Status>()
            //     .HasKey(s => new { s.RequirementID, s.ServiceProviderUsername });

            // modelBuilder.Entity<Status>()
            //     .HasOne(s => s.ServiceProvider)
            //     .WithMany()
            //     .HasForeignKey(s => s.ServiceProviderUsername);
            //     }
    }
    }}
