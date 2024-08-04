using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Reflection.Metadata;
using System;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using ContactList.Model;

namespace Contactlist.Model
{
    public class Dbcontact : DbContext //The DbContext class is a crucial part of Entity Framework Core, a popular Object-Relational Mapping (ORM) framework for .NET applications. It serves as a bridge between your code and the database, managing the entity objects during runtime, including CRUD (Create, Read, Update, Delete) operations, change tracking, and database interactions.
                                       // The DbContext class is the backbone of Entity Framework Core, providing a way to interact with the database using entity objects.It simplifies data access by allowing you to work with strongly-typed objects instead of raw database commands, and it manages change tracking, relationships, and concurrency for you.By configuring DbContext and defining DbSet<TEntity> properties, you set up a context that Entity Framework uses to handle database operations in your application.
    {
        public Dbcontact(DbContextOptions<Dbcontact> options) : base(options) //This parameter takes an instance of DbContextOptions<dbcontact>. DbContextOptions<TContext> is a class provided by Entity Framework Core to configure the context. This generic class contains all the settings required to configure the dbcontact context, such as the database provider (e.g., SQL Server, SQLite), connection strings, and other options.
        //: The "options" parameter contains all the configuration needed by Entity Framework Core to set up the dbcontact context. This includes the database provider, connection string, and any other settings.
        { }


        public DbSet<Contact> Contacts { get; set; }
        public DbSet<ActivityLog> Activitylog { get; set; }

        public override int SaveChanges()
        {
            
            var entities = ChangeTracker.Entries<Contact>().Where(e => e.State == EntityState.Modified || e.State == EntityState.Added ||e.State==EntityState.Deleted).ToList();
            foreach (var entry in entities)
            {
                if (entry.State == EntityState.Deleted)
                {
                    entry.State = EntityState.Modified;
                    entry.Entity.IsDeleted = true;
                }
 
                var contact = entry.Entity;
                if (Contacts.Any(c => c.Name == contact.Name && c.Email == contact.Email && c.Id != contact.Id))
                {
                    throw new InvalidOperationException("A Contact with same Name and Email already exist");
                }
                var log = new ActivityLog
                {
                    Action = entry.State.ToString(),
                    EntityName = entry.Entity.GetType().Name,
                    Timestamp = DateTime.UtcNow,
                    Details = entry.Entity.ToString()
                };
                Activitylog.Add(log);
            }
            return base.SaveChanges();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Contact>().HasQueryFilter(c => !c.IsDeleted);
            base.OnModelCreating(modelBuilder);
        }


    }
}